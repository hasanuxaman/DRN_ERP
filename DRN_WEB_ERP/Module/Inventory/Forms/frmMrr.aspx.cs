using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmMrr : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var curYear = DateTime.Now.Year;
            for (Int64 year = 2014; year <= (curYear); year++)
            {
                cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            cboYear.SelectedValue = curYear.ToString();

            var curMonth = DateTime.Now.Month;
            for (int month = 1; month <= 12; month++)
            {
                var monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                cboMonth.Items.Add(new ListItem(monthName.ToString(), month.ToString()));
            }
            cboMonth.Items.Insert(0, new ListItem("-----Select-----", "0"));
            cboMonth.SelectedValue = curMonth.ToString();

            txtMrrDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("PO", DateTime.Now.Year);
            var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
            var nextHdrRefNo = "ECIL-MRR-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");
            txtMrrNo.Text = nextHdrRefNo.ToString();            

            GetMrrList();
        }       

        protected void btnPost_Click(object sender, EventArgs e)
        {            
            Page.Validate("btnSave");

            if (!Page.IsValid) return;

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
            var taPoDet = new tbl_PuTr_PO_DetTableAdapter();

            var taWhMrr = new tbl_Acc_Fa_Te_Wh_MrrTableAdapter();
            var taWdMrr = new tbl_Acc_Fa_Te_Wd_MrrTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                var UserCode = Session["sessionUserCode"] == null ? "" : Session["sessionUserCode"].ToString();

                #region Get PO Ref No and Supplier Details
                var taPoHdr = new tbl_PuTr_PO_HdrTableAdapter();
                var poRef = "";
                var supRef = "";
                var supName = "";
                var supAccCode = "";
                try
                {                    
                    var srchWords = txtSrcPendPo.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        poRef = word;
                        break;
                    }

                    if (poRef.Length > 0)
                    {
                        var dtPoHdr = taPoHdr.GetDataByHdrRef(poRef);
                        if (dtPoHdr.Rows.Count > 0)
                        {
                            poRef = dtPoHdr[0].PO_Hdr_Ref.ToString();

                            #region Get Supplier Details
                            supRef = dtPoHdr[0].IsPO_Hdr_PcodeNull() ? "" : dtPoHdr[0].PO_Hdr_Pcode.ToString();                            
                            if (supRef.Length > 0)
                            {
                                var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
                                var dtSupAdr = taSupAdr.GetDataBySupAdrRef(supRef);
                                if (dtSupAdr.Rows.Count > 0)
                                {
                                    supRef = dtSupAdr[0].Par_Adr_Ref.ToString();
                                    supName = dtSupAdr[0].Par_Adr_Name.ToString();
                                    supAccCode = dtSupAdr[0].Par_Adr_Acc_Code.ToString();
                                }
                                else
                                {
                                    tblMsg.Rows[0].Cells[0].InnerText = "Supplier Data Not Found.";
                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                            }
                            else
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Supplier.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "PO Data Not Found.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "PO Data Not Found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                catch (Exception ex) 
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "PO Data Not Found. " + ex.Message ;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion                

                #region Check Selected Quantity
                var chkCount = 0;
                foreach (GridViewRow gr in gvPoDet.Rows)
                {
                    var chkMrrItem = ((CheckBox)(gr.FindControl("chkMrrItem")));
                    if (chkMrrItem.Checked) chkCount++;
                }

                if (chkCount <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Select item first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                var nextHdrRef = 1;
                var nextHdrRefNo = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);
                taPoDet.AttachTransaction(myTran);
                taWhMrr.AttachTransaction(myTran);
                taWdMrr.AttachTransaction(myTran);

                //Inventory Header Ref
                var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("PO", Convert.ToDateTime(txtMrrDate.Text).Year);
                var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                nextHdrRefNo = "ECIL-MRR-" + Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00") + Convert.ToDateTime(txtMrrDate.Text).ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                taInvHdr.InsertInvHdr(nextHdrRef, "RC", "PO", nextHdrRefNo, supRef.ToString(), supRef.ToString(), supAccCode.ToString(),
                    txtPoRef.Text.Trim(), Convert.ToDateTime(txtMrrDate.Text), txtChlnNo.Text.Trim(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "P",
                    (Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtMrrDate.Text).Year.ToString()).ToString(), "ADM", "", "1", "",
                    poRef.ToString(), "", poRef.ToString(), supName, "", "", 0, DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                //var dtMaxAccRef = taAcc.GetMaxRefNo("MJV", DateTime.Now.Year);
                //var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                //var nextAccRefNo = "MJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                var dtMaxWhRef = taWhMrr.GetMaxRef("MRR", Convert.ToDateTime(txtMrrDate.Text).Year);
                var max_wref = dtMaxWhRef == null ? "000001" : (Convert.ToInt32(dtMaxWhRef) + 1).ToString();
                var fate_ref = "MJV" + (Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00") + Convert.ToDateTime(txtMrrDate.Text).ToString("yy")).ToString() + "-" + Convert.ToInt32(max_wref).ToString("000000");
                var fate_period = string.Format("{0:00}", Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00")) + "/" + Convert.ToDateTime(txtMrrDate.Text).ToString("yy").ToString();

                taWhMrr.InsertWhMrr(UserCode, fate_ref, "MRR", Convert.ToDateTime(txtMrrDate.Text), DateTime.Now, fate_period, "H", "Y", nextHdrRefNo, "J", 0);

                #region Insert Inventory Details
                var jvLNo = 0;
                short Lno = 0;
                foreach (GridViewRow gr in gvPoDet.Rows)
                {
                    var lblMprRefNo = (Label)(gr.FindControl("lblMprRefNo"));
                    var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                    var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                    var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                    var ddlMrrStore = (DropDownList)(gr.FindControl("ddlMrrStore"));
                    var txtMrrQty = (TextBox)(gr.FindControl("txtMrrQty"));
                    var hfPoRate = ((HiddenField)(gr.FindControl("hfPoRate")));
                    var chkMrrItem = ((CheckBox)(gr.FindControl("chkMrrItem")));
                    
                    var MrrQty = Convert.ToDouble(txtMrrQty.Text.Trim());

                    //var lblMrrRate = (Label)(gr.FindControl("lblMrrRate"));
                    //var MrrRate = Convert.ToDouble(lblMrrRate.Text.Trim());
                    //var MrrAmt = Convert.ToDecimal(MrrQty * MrrRate);

                    #region Get Item Details
                    var itemName = "";
                    var itemAcc = "";
                    var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                    var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(lblItemCode.Text.Trim()));
                    if (dtItemDet.Rows.Count > 0)
                    {
                        itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                        itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion                    

                    if (chkMrrItem.Checked)
                    {
                        if (MrrQty > 0)
                        {
                            var dtPoDet = taPoDet.GetDataByPoItemMpr(poRef.ToString(), lblItemCode.Text.Trim(), lblMprRefNo.Text.Trim());
                            if (dtPoDet.Rows.Count > 0)
                            {
                                Lno++;

                                taPoDet.UpdatePoDetBalQty(dtPoDet[0].PO_Det_Org_QTY + MrrQty, dtPoDet[0].PO_Det_Bal_Qty - MrrQty, "Y", poRef.ToString(), lblMprRefNo.Text.ToString(), lblItemCode.Text.Trim());

                                taInvDet.InsertInvDet(nextHdrRef.ToString(), "RC", "PO", nextHdrRefNo.ToString(), Lno, "", 1, txtPoRef.Text.Trim(),
                                    Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), ddlMrrStore.SelectedValue.ToString(), "",
                                    txtPoRef.Text.Trim(), txtPoRef.Text.Trim(), Lno, lblMprRefNo.Text.Trim(), DateTime.Now, Convert.ToDateTime(txtMrrDate.Text),
                                    Convert.ToDouble(MrrQty), 0, Convert.ToDecimal(hfPoRate.Value), Convert.ToDecimal(MrrQty) * Convert.ToDecimal(hfPoRate.Value),
                                    Convert.ToDecimal(MrrQty) * Convert.ToDecimal(hfPoRate.Value), "", "", "", 0, 0, "1", "E");

                                var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlMrrStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                                if (dtStkCtl.Rows.Count > 0)
                                    taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(MrrQty)), 4), ddlMrrStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                                else
                                    taStkCtl.InsertItemStore(ddlMrrStore.SelectedValue.ToString(), lblItemCode.Text.Trim(), "", Math.Round((Convert.ToDouble(MrrQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                        Convert.ToDateTime(txtMrrDate.Text), DateTime.Now, "", "", "", 0);

                                //#region Insert Accounts Voucher Entry
                                //jvLNo++;
                                ////Debit-Customer Account
                                //taAcc.InsertAccData(supAccCode.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "D", MrrAmt, nextHdrRefNo.ToString(), "0",
                                //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                                //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, itemName.ToString(), DateTime.Now,
                                //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                                //    "", txtPoRef.Text.Trim(), "N", 1, 0, "", "", "", "J", 0, "1", "MJV");

                                //jvLNo++;
                                ////Credit-Item Account
                                //taAcc.InsertAccData(itemAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "C", MrrAmt, nextHdrRefNo.ToString(), "0",
                                //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                                //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, supName.ToString(), DateTime.Now,
                                //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                                //    "", txtPoRef.Text.Trim(), "N", 1, 0, "", "", "", "J", 0, "1", "MJV");
                                //#endregion

                                jvLNo++;
                                taWdMrr.InsertWdMrr(fate_ref, jvLNo, supAccCode, itemName, "C", Convert.ToDecimal(MrrQty) * Convert.ToDecimal(hfPoRate.Value), nextHdrRefNo, 0, "BDT", Convert.ToDecimal(hfPoRate.Value), Convert.ToDecimal(MrrQty) * Convert.ToDecimal(hfPoRate.Value), "", "", "", "", "", "", "", "", "", "", "", supName, "P", Convert.ToDateTime(txtMrrDate.Text), lblItemCode.Text.Trim(), poRef.ToString(), nextHdrRefNo, txtChlnNo.Text.Trim(), lblItemUnit.Text.Trim(), MrrQty.ToString(), "H", 1);
                                jvLNo++;
                                taWdMrr.InsertWdMrr(fate_ref, jvLNo, itemAcc, supName, "D", Convert.ToDecimal(MrrQty) * Convert.ToDecimal(hfPoRate.Value), nextHdrRefNo, 0, "BDT", Convert.ToDecimal(hfPoRate.Value), Convert.ToDecimal(MrrQty) * Convert.ToDecimal(hfPoRate.Value), "", "", "", "", "", "", "", "", "", "", "", itemName, "I", Convert.ToDateTime(txtMrrDate.Text), supRef.ToString(), poRef.ToString(), nextHdrRefNo, txtChlnNo.Text.Trim(), lblItemUnit.Text.Trim(), MrrQty.ToString(), "H", 1);
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "PO Data not found.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }
                    }
                }
                #endregion

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                txtSrcPendPo.Text = "";
                txtPoRef.Text = "";
                lblSup.Text = "";
                txtChlnNo.Text = "";
                txtSrcPendPo.Enabled = true;
                btnPost.Visible = false;

                //ddlPoList.Items.Clear();
                //var taPoHdrDet = new View_Pend_MRR_ListTableAdapter();
                //var dtPoHdrDet = taPoHdrDet.GetData();
                //foreach (dsProcTran.View_Pend_MRR_ListRow dr in dtPoHdrDet.Rows)
                //{
                //    ddlPoList.Items.Add(new ListItem(dr.PO_Hdr_Ref + ":" + dr.Par_Adr_Name, dr.PO_Hdr_Ref));
                //}
                //ddlPoList.DataBind();
                //ddlPoList.Items.Insert(0, new ListItem("-------Select-------", "0"));
                //ddlPoList.SelectedIndex = 0;

                var taPoDetNew = new tbl_PuTr_PO_DetTableAdapter();
                var dtPoDetNew = taPoDetNew.GetDataByPoDetRef("");
                gvPoDet.DataSource = dtPoDetNew;
                gvPoDet.DataBind();

                GetMrrList();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowInvReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
        {
            rptSelcFormula = "{View_InTr_Trn_Hdr_Det_Tran.Trn_Hdr_Ref_No}='" + ddlMrrList.SelectedValue.ToString() + "'";

            rptFile = "~/Module/Inventory/Reports/rptInvMrr.rpt";

            Session["RptDateFrom"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMrrList();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMrrList();
        }

        private void GetMrrList()
        {
            var taTrnDet = new VIEW_INV_TRAN_DETTableAdapter();
            var dtTrnDet = new dsInvTran.VIEW_INV_TRAN_DETDataTable();
            dtTrnDet = taTrnDet.GetDataByTypeCodeRef("RC", "PO", "0");
            gvMrrDet.DataSource = dtTrnDet;
            gvMrrDet.DataBind();

            tbl_InTr_Trn_HdrTableAdapter taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            dsInvTran.tbl_InTr_Trn_HdrDataTable dtInvHdr = new dsInvTran.tbl_InTr_Trn_HdrDataTable();
            ListItem lst;

            if (cboMonth.SelectedIndex == 0)
                dtInvHdr = taInvHdr.GetDataByTranRefListByYear("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            else
                dtInvHdr = taInvHdr.GetDataByTrnRefListByYearMonth("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));

            ddlMrrList.Items.Clear();
            foreach (dsInvTran.tbl_InTr_Trn_HdrRow dr in dtInvHdr.Rows)
            {
                lst = new ListItem();
                lst.Text = dr.Trn_Hdr_Ref_No.ToString() + ":  [" + dr.T_C1.ToString() + "]";
                lst.Value = dr.Trn_Hdr_Ref_No.ToString();
                ddlMrrList.Items.Add(lst);
            }
            ddlMrrList.Items.Insert(0, new ListItem("-----Select-----", "0"));

            lblcount.Text = "(" + (ddlMrrList.Items.Count - 1).ToString() + ")";
        }

        protected void ddlMrrList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taTrnDet = new VIEW_INV_TRAN_DETTableAdapter();
            var dtTrnDet = new dsInvTran.VIEW_INV_TRAN_DETDataTable();

            if (ddlMrrList.SelectedIndex == 0)
            {
                btnPrint.Visible = false;

                dtTrnDet = taTrnDet.GetDataByTypeCodeRef("RC", "PO", "0");
                gvMrrDet.DataSource = dtTrnDet;
                gvMrrDet.DataBind();
            }
            else
            {
                dtTrnDet = taTrnDet.GetDataByTypeCodeRef("RC", "PO", ddlMrrList.SelectedValue.ToString());
                gvMrrDet.DataSource = dtTrnDet;
                gvMrrDet.DataBind();
                btnPrint.Visible = dtTrnDet.Rows.Count > 0;
            }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            txtSrcPendPo.Text = "";
            txtPoRef.Text = "";
            lblSup.Text = "";
            txtChlnNo.Text = "";
            txtSrcPendPo.Enabled = true;
            var taPoDetNew = new tbl_PuTr_PO_DetTableAdapter();
            var dtPoDetNew = taPoDetNew.GetDataByPoDetRef("");
            gvPoDet.DataSource = dtPoDetNew;
            gvPoDet.DataBind();
            btnPost.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Page.Validate("btnSearch");

            var taPoHdr = new tbl_PuTr_PO_HdrTableAdapter();
            var taPoDet = new View_PuTr_Po_Hdr_DetTableAdapter();
            var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();

            var poRef = "";
            var srchWords = txtSrcPendPo.Text.Trim().Split(':');
            foreach (string word in srchWords)
            {
                poRef = word;
                break;
            }

            var dtPoHdr = taPoHdr.GetDataByPoHdrRef(poRef);
            if (dtPoHdr.Rows.Count > 0)
            {
                txtSrcPendPo.Enabled = false;

                txtPoRef.Text = dtPoHdr[0].PO_Hdr_Ref.ToString();

                var dtSupAdr = taSupAdr.GetDataBySupAdrRef(dtPoHdr[0].PO_Hdr_Dcode);
                var supRef = dtSupAdr.Rows.Count > 0 ? dtSupAdr[0].Par_Adr_Ref.ToString() : "";
                var supCode = dtSupAdr.Rows.Count > 0 ? dtSupAdr[0].Par_Adr_Ref_No.ToString() : "";
                var supName = dtSupAdr.Rows.Count > 0 ? dtSupAdr[0].Par_Adr_Name.ToString() : "";
                var supAccCode = dtSupAdr.Rows.Count > 0 ? dtSupAdr[0].Par_Adr_Acc_Code.ToString() : "";

                lblSup.Text = supRef + ":" + supName;

                var dtPoDet = taPoDet.GetDataByPendMrrByPoRef(poRef.ToString());
                gvPoDet.DataSource = dtPoDet;
                gvPoDet.DataBind();
                
                btnPost.Visible = true;
            }
            else
            {
                txtSrcPendPo.Enabled = true;
                tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var taPoHdr = new tbl_PuTr_PO_HdrTableAdapter();
            var taPoDet = new tbl_PuTr_PO_DetTableAdapter();            
            try
            {
                var poRef = "";
                var srchWords = txtSrcPendPo.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    poRef = word;
                    break;
                }

                if (poRef.Length > 0)
                {
                    var dtPoHdr = taPoHdr.GetDataByHdrRef(poRef);
                    if (dtPoHdr.Rows.Count > 0)
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void gvPoDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TextBox txtMrrQty = (TextBox)e.Row.FindControl("txtMrrQty");
                //HiddenField hfMrrBalQty = (HiddenField)e.Row.FindControl("hfMrrBalQty");

                //txtMrrQty.Attributes.Add("onkeyup", "MrrBalQty('" + txtMrrQty.ClientID + "', '" + hfMrrBalQty.Value.Trim() + "' )");
            }
        }
    }
}