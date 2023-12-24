using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmSupplierBillEntry : System.Web.UI.Page
    {
        double totMrrAmt;
        double totBillAmt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
           
            //var taSupList = new View_Pend_Mrr_Bill_Sup_ListTableAdapter();
            //var dtSupList = taSupList.GetData();
            //ddlparty.DataSource = dtSupList;
            //ddlparty.DataValueField = "Wrk_Ac_Code";
            //ddlparty.DataTextField = "Par_Adr_Name";
            //ddlparty.DataBind();
            //ddlparty.Items.Insert(0, new ListItem("---Select---", "0"));

            txtBillEntDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taBillHdr = new tbl_PuTr_Bill_HdrTableAdapter();
            var dtMaxBillRef = taBillHdr.GetMaxBillHdrRefNo(DateTime.Now.Year);
            var nextBillRef = dtMaxBillRef == null ? 1 : Convert.ToInt32(dtMaxBillRef) + 1;
            var nextBillRefNo = "ECIL-BILL-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextBillRef).ToString("000000");

            txtBillRefNo.Text = nextBillRefNo.ToString();

            //txtBillAdjAmt.Attributes.Add("onkeyup", "CalcBillAmt('" + txtBillAmt.ClientID + "', '" + txtBillAdjAmt.ClientID + "', '" + txtTotBillAmt.ClientID + "' )");

            txtTotBillAmt.Attributes.Add("onkeyup", "CalcBillAdjAmt('" + txtBillAmt.ClientID + "', '" + txtBillAdjAmt.ClientID + "', '" + txtTotBillAmt.ClientID + "' )");
        }

        #region Mrr Details Gridview
        protected void LoadInitMrrDetGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Trn_Hdr_Ref_No", typeof(string));
            dt.Columns.Add("Trn_Hdr_Date", typeof(string));
            dt.Columns.Add("Trn_Det_Lno", typeof(string));
            dt.Columns.Add("Trn_Hdr_Tran_Ref", typeof(string));
            dt.Columns.Add("Trn_Hdr_Com1", typeof(string));
            dt.Columns.Add("Trn_Det_Icode", typeof(string));
            dt.Columns.Add("Trn_Det_Itm_Desc", typeof(string));
            dt.Columns.Add("Trn_Det_Itm_Uom", typeof(string));
            dt.Columns.Add("Trn_Det_Lin_Qty", typeof(string));
            dt.Columns.Add("Trn_Det_Lin_Rat", typeof(string));
            dt.Columns.Add("Trn_Det_Lin_Amt", typeof(string));            
            ViewState["dtMrrDet"] = dt;
        }

        protected void SetMrrDetGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtMrrDet"];

                gvMrrDet.DataSource = dt;
                gvMrrDet.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region Bill Details Gridview
        protected void LoadInitBillDetGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Trn_Hdr_Ref_No", typeof(string));
            dt.Columns.Add("Trn_Hdr_Date", typeof(string));
            dt.Columns.Add("Trn_Det_Lno", typeof(string));
            dt.Columns.Add("Trn_Hdr_Tran_Ref", typeof(string));
            dt.Columns.Add("Trn_Hdr_Com1", typeof(string));
            dt.Columns.Add("Trn_Det_Icode", typeof(string));
            dt.Columns.Add("Trn_Det_Itm_Desc", typeof(string));
            dt.Columns.Add("Trn_Det_Itm_Uom", typeof(string));
            dt.Columns.Add("Trn_Det_Lin_Qty", typeof(string));
            dt.Columns.Add("Trn_Det_Lin_Rat", typeof(string));
            dt.Columns.Add("Trn_Det_Lin_Amt", typeof(string));
            ViewState["dtBillDet"] = dt;
        }

        protected void SetBillDetGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtBillDet"];

                gvBillDet.DataSource = dt;
                gvBillDet.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtBillAdjAmt.Text = "0";
            //txtBillAmt.Text = "0";
            //txtTotBillAmt.Text = "0";

            //LoadInitMrrDetGridData();
            //SetMrrDetGridData();

            //LoadInitBillDetGridData();
            //SetBillDetGridData();

            //var dt = new DataTable();
            //dt = (DataTable)ViewState["dtMrrDet"];

            //var taSupList = new View_Acc_Fa_Te_Wh_Wd_MrrTableAdapter();
            //var dtSupList = taSupList.GetDataByPartyPendMrrBill(ddlparty.SelectedValue.ToString());
            //foreach (dsAccTran.View_Acc_Fa_Te_Wh_Wd_MrrRow dr in dtSupList.Rows)
            //{
            //    dt.Rows.Add(dr.Wrk_Match, dr.Wrk_Line_No, dr.Wrk_Dc_No, dr.Wrk_DepositNo, dr.Wrk_Adr_Code, dr.Wrk_Narration,
            //        dr.Wd_T_C1, dr.Wd_T_C2, dr.Wrk_Curr_Rate, dr.Wrk_Amount);
            //}

            //ViewState["dtMrrDet"] = dt;
            //SetMrrDetGridData();
        }

        protected void chkMrr_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                var lblMrrRefNo = (Label)(row.FindControl("lblMrrRefNo"));
                var lblMrrDate = (Label)(row.FindControl("lblMrrDate"));
                var hfMrrTrnDetLno = (HiddenField)(row.FindControl("hfMrrTrnDetLno"));
                var lblMrrPoRef = (Label)(row.FindControl("lblMrrPoRef"));
                var lblMrrChlnNo = (Label)(row.FindControl("lblMrrChlnNo"));
                var lblMrrItemCode = (Label)(row.FindControl("lblMrrItemCode"));
                var lblMrrItemName = (Label)(row.FindControl("lblMrrItemName"));
                var lblMrrItemUnit = (Label)(row.FindControl("lblMrrItemUnit"));
                var lblMrrQty = (Label)(row.FindControl("lblMrrQty"));
                var lblMrrRate = (Label)(row.FindControl("lblMrrRate"));
                var lblMrrAmount = (Label)(row.FindControl("lblMrrAmount"));

                var dtMrr = new DataTable();
                dtMrr = (DataTable)ViewState["dtMrrDet"];
                dtMrr.Rows[row.RowIndex].Delete();
                dtMrr.AcceptChanges();
                SetMrrDetGridData();

                var dtBill = new DataTable();
                dtBill = (DataTable)ViewState["dtBillDet"];
                dtBill.Rows.Add(lblMrrRefNo.Text, Convert.ToDateTime(lblMrrDate.Text).ToString("dd/MM/yyyy"), hfMrrTrnDetLno.Value, lblMrrPoRef.Text, lblMrrChlnNo.Text,
                    lblMrrItemCode.Text, lblMrrItemName.Text, lblMrrItemUnit.Text, lblMrrQty.Text, lblMrrRate.Text, lblMrrAmount.Text);
                ViewState["dtBillDet"] = dtBill;
                SetBillDetGridData();

                txtBillAdjAmt.Text = txtBillAdjAmt.Text.Trim().Length > 0 ? txtBillAdjAmt.Text.Trim() : "0";
                txtBillAmt.Text= totBillAmt.ToString("N2");
                txtTotBillAmt.Text = (Convert.ToDouble(totBillAmt) - Convert.ToDouble(txtBillAdjAmt.Text.Trim())).ToString("N2");                

                //btnHold.Visible = gvBillDet.Rows.Count > 0;
                btnPost.Visible = gvBillDet.Rows.Count > 0;
            }
            catch (Exception ex)
            {
            }
        }

        protected void chkBll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                var lblBillMrrRefNo = (Label)(row.FindControl("lblBillMrrRefNo"));
                var lblBillMrrDate = (Label)(row.FindControl("lblBillMrrDate"));
                var hfBillMrrTrnDetLno = (HiddenField)(row.FindControl("hfBillMrrTrnDetLno"));
                var lblBillPoRef = (Label)(row.FindControl("lblBillPoRef"));
                var lblBillChlnNo = (Label)(row.FindControl("lblBillChlnNo"));
                var lblBillItemCode = (Label)(row.FindControl("lblBillItemCode"));
                var lblBillItemName = (Label)(row.FindControl("lblBillItemName"));
                var lblBillItemUnit = (Label)(row.FindControl("lblBillItemUnit"));
                var lblBillQty = (Label)(row.FindControl("lblBillQty"));
                var lblBillRate = (Label)(row.FindControl("lblBillRate"));
                var lblBillAmount = (Label)(row.FindControl("lblBillAmount"));

                var dtBill = new DataTable();
                dtBill = (DataTable)ViewState["dtBillDet"];
                dtBill.Rows[row.RowIndex].Delete();
                dtBill.AcceptChanges();
                SetBillDetGridData();

                var dtMrr = new DataTable();
                dtMrr = (DataTable)ViewState["dtMrrDet"];
                dtMrr.Rows.Add(lblBillMrrRefNo.Text, Convert.ToDateTime(lblBillMrrDate.Text).ToString("dd/MM/yyyy"), hfBillMrrTrnDetLno.Value, lblBillPoRef.Text,
                    lblBillChlnNo.Text, lblBillItemCode.Text, lblBillItemName.Text, lblBillItemUnit.Text, lblBillQty.Text, lblBillRate.Text, lblBillAmount.Text);
                ViewState["dtMrrDet"] = dtMrr;
                SetMrrDetGridData();

                txtBillAdjAmt.Text = txtBillAdjAmt.Text.Trim().Length > 0 ? txtBillAdjAmt.Text.Trim() : "0";
                txtBillAmt.Text = totBillAmt.ToString("N2");
                txtTotBillAmt.Text = (Convert.ToDouble(totBillAmt) - Convert.ToDouble(txtBillAdjAmt.Text.Trim())).ToString("N2");                

                //btnHold.Visible = gvBillDet.Rows.Count > 0;
                btnPost.Visible = gvBillDet.Rows.Count > 0;
            }
            catch (Exception ex)
            {
            }
        }

        protected void gvBillDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblBillAmount = ((Label)e.Row.FindControl("lblBillAmount"));
                totBillAmt += Convert.ToDouble(lblBillAmount.Text.Trim());

                txtBillAdjAmt.Attributes.Add("onkeyup", "CalcBillAmt('" + txtBillAmt.ClientID + "', '" + txtBillAdjAmt.ClientID + "', '" + txtTotBillAmt.ClientID + "' )");
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotAmt = (Label)e.Row.FindControl("lblTotAmt");
                lblTotAmt.Text = totBillAmt.ToString("N2");
            }
        }

        protected void gvMrrDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblMrrAmount = ((Label)e.Row.FindControl("lblMrrAmount"));
                totMrrAmt += Convert.ToDouble(lblMrrAmount.Text.Trim());

                txtBillAdjAmt.Attributes.Add("onkeyup", "CalcBillAmt('" + txtBillAmt.ClientID + "', '" + txtBillAdjAmt.ClientID + "', '" + txtTotBillAmt.ClientID + "' )");
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotMrrAmt = (Label)e.Row.FindControl("lblTotMrrAmt");
                lblTotMrrAmt.Text = totMrrAmt.ToString("N2");
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            var taBillHdr = new tbl_PuTr_Bill_HdrTableAdapter();
            var taBillDet = new tbl_PuTr_Bill_DetTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taAccMrrHdr = new tbl_Acc_Fa_Te_Wh_MrrTableAdapter();

            var taPartyMas = new tbl_PuMa_Par_AdrTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taBillHdr.Connection);

            try
            {
                #region Get Party Details
                var partyCode = "";
                var partyName = "";
                var partyAcc = "";

                var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
                if (txtSearchSupplier.Text.Trim().Length <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Supplier data not found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                try
                {
                    var supRef = "";
                    var srchWords = txtSearchSupplier.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        supRef = word;
                        break;
                    }

                    if (supRef.Length > 0)
                    {
                        var dtSupAdr = taSupAdr.GetDataBySupAdrRef(supRef);
                        if (dtSupAdr.Rows.Count > 0)
                        {
                            partyCode = dtSupAdr[0].Par_Adr_Ref.ToString();
                            partyName = dtSupAdr[0].Par_Adr_Name.ToString();
                            partyAcc = dtSupAdr[0].Par_Adr_Acc_Code.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Supplier data not found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }            
                #endregion

                #region Get Tot Bill Amount
                foreach(GridViewRow gr in gvBillDet.Rows)
                {
                    var lblBillAmount = ((Label)gr.FindControl("lblBillAmount"));
                    totBillAmt += Convert.ToDouble(lblBillAmount.Text.Trim());
                }
                #endregion

                taBillHdr.AttachTransaction(myTran);
                taBillDet.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);
                taAccMrrHdr.AttachTransaction(myTran);

                var dtMaxBillHdrRef = taBillHdr.GetMaxBillHdrRef(Convert.ToDateTime(txtBillDate.Text).Year);
                var maxBillHdrRef = dtMaxBillHdrRef == null ? 100001 : Convert.ToInt32(dtMaxBillHdrRef) + 1;
                var nextBillHdrRef = Convert.ToInt32(Convert.ToDateTime(txtBillDate.Text).Year + "" + maxBillHdrRef);

                var dtMaxBillHdrRefNo = taBillHdr.GetMaxBillHdrRefNo(Convert.ToDateTime(txtBillDate.Text).Year);
                var maxBillHdrRefNo = dtMaxBillHdrRefNo == null ? 1 : Convert.ToInt32(dtMaxBillHdrRefNo) + 1;
                var nextBillHdrRefNo = "ECIL-BILL-" + Convert.ToDateTime(txtBillDate.Text).Month.ToString("00") + Convert.ToDateTime(txtBillDate.Text).ToString("yy") + "-" + Convert.ToInt32(maxBillHdrRefNo).ToString("000000");

                taBillHdr.InsertBillHdr(nextBillHdrRef, nextBillHdrRefNo, Convert.ToDateTime(txtBillDate.Text.Trim()), partyAcc.ToString(),
                    partyAcc.ToString(), partyAcc.ToString(), txtSupBillNo.Text.Trim(), Convert.ToDecimal(txtBillAmt.Text.Trim()),
                    Convert.ToDecimal(txtBillAdjAmt.Text.Trim()), Convert.ToDecimal(txtTotBillAmt.Text.Trim()), 0, Convert.ToDecimal(txtTotBillAmt.Text.Trim()), "P",
                    Convert.ToDateTime(txtBillDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtBillDate.Text).Year, partyName.ToString(), "", "", "", "",
                    DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "A");

                var dtMaxAccRef = taAcc.GetMaxRefNo("BILV", Convert.ToDateTime(txtBillDate.Text).Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextAccRefNo = "BILV" + (Convert.ToDateTime(txtBillDate.Text).Month.ToString("00") + Convert.ToDateTime(txtBillDate.Text).ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                #region Insert Bill Details
                var jvLNo = 0;
                foreach (GridViewRow gr in gvBillDet.Rows)
                {
                    var lblBillMrrRefNo = (Label)(gr.FindControl("lblBillMrrRefNo"));
                    var hfBillMrrTrnDetLno = (HiddenField)(gr.FindControl("hfBillMrrTrnDetLno"));
                    var lblBillPoRef = (Label)(gr.FindControl("lblBillPoRef"));
                    var lblBillChlnNo = (Label)(gr.FindControl("lblBillChlnNo"));
                    var lblBillItemCode = (Label)(gr.FindControl("lblBillItemCode"));
                    var lblBillItemName = (Label)(gr.FindControl("lblBillItemName"));
                    var lblBillItemUnit = (Label)(gr.FindControl("lblBillItemUnit"));
                    var lblBillQty = (Label)(gr.FindControl("lblBillQty"));
                    var lblBillRate = (Label)(gr.FindControl("lblBillRate"));
                    var lblBillAmount = (Label)(gr.FindControl("lblBillAmount"));

                    #region Get Item Details
                    var itemName = "";
                    var itemAcc = "";
                    var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                    var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(lblBillItemCode.Text.Trim()));
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

                    taBillDet.InsertBillDet(nextBillHdrRef.ToString(), nextBillHdrRefNo, lblBillMrrRefNo.Text, Convert.ToInt32(hfBillMrrTrnDetLno.Value), lblBillPoRef.Text,
                        lblBillChlnNo.Text, lblBillItemCode.Text, lblBillItemName.Text, lblBillItemUnit.Text, Convert.ToDouble(lblBillQty.Text),
                        Convert.ToDecimal(lblBillRate.Text), Convert.ToDecimal(lblBillAmount.Text), "", "", "", "", "", "1", "P");

                    taInvDet.UpdateTrnDetFlag("B", "RC", "PO", lblBillMrrRefNo.Text, Convert.ToInt16(hfBillMrrTrnDetLno.Value));

                    taAccMrrHdr.UpdateWhMrrFlag("P", lblBillMrrRefNo.Text);

                    #region Insert Accounts Voucher Entry
                    jvLNo++;
                    //Debit-Item Account
                    taAcc.InsertAccData(itemAcc.ToString(), (Convert.ToDateTime(txtBillDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtBillDate.Text).Year.ToString()).ToString(),
                        nextAccRefNo.ToString(), jvLNo, 1, partyName.ToString(), "D", Convert.ToDecimal(lblBillAmount.Text), nextBillHdrRefNo.ToString(), "0",
                        "BDT", 1, Convert.ToDecimal(lblBillAmount.Text), "", "", "", "", "", "", "", "", "", "", "",
                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtBillDate.Text), itemName, Convert.ToDateTime(txtBillDate.Text),
                        "ADM", "I", "", Convert.ToDateTime(txtBillDate.Text), "BILV", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(lblBillAmount.Text), "",
                        lblBillPoRef.Text, lblBillMrrRefNo.Text, "N", 1, 0, "", "", "", "J", 0, "1", "BILV");
                    #endregion
                }
                jvLNo++;
                //Credit-Supplier Account
                taAcc.InsertAccData(partyAcc.ToString(), (Convert.ToDateTime(txtBillDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtBillDate.Text).Year.ToString()).ToString(),
                    nextAccRefNo.ToString(), jvLNo, 1, "Bill against - " + nextBillHdrRefNo.ToString(), "C", Convert.ToDecimal(txtTotBillAmt.Text),
                    nextBillHdrRefNo.ToString(), "0", "BDT", 1, Convert.ToDecimal(txtTotBillAmt.Text), "", "", "", "", "", "", "", "", "", "", "",
                    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtBillDate.Text), partyName.ToString(), Convert.ToDateTime(txtBillDate.Text),
                    "ADM", "S", "", Convert.ToDateTime(txtBillDate.Text), "BILV", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtTotBillAmt.Text), "",
                    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "BILV");

                if (Convert.ToDouble(txtBillAdjAmt.Text.Trim()) > 0)
                {
                    var trnType = Convert.ToDecimal(txtTotBillAmt.Text) > Convert.ToDecimal(txtBillAmt.Text.Trim()) ? "D" : "C";
                    jvLNo++;
                    //Credit-Adjustment Account
                    //taAcc.InsertAccData("Adjustment Account", (Convert.ToDateTime(txtBillDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtBillDate.Text).Year.ToString()).ToString(),
                    //    nextAccRefNo.ToString(), jvLNo, 1, "Bill adjustment against - " + nextBillHdrRefNo.ToString(), "C", Convert.ToDecimal(txtBillAdjAmt.Text),
                    //    nextBillHdrRefNo.ToString(), "0", "BDT", 1, Convert.ToDecimal(txtBillAdjAmt.Text), "", "", "", "", "", "", "", "", "", "", "",
                    //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                    //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtBillDate.Text), "Adjustment Account", Convert.ToDateTime(txtBillDate.Text),
                    //    "ADM", "O", "", Convert.ToDateTime(txtBillDate.Text), "BILV", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtBillAdjAmt.Text), "",
                    //    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "BILV");
                    taAcc.InsertAccData("Adjustment Account", (Convert.ToDateTime(txtBillDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtBillDate.Text).Year.ToString()).ToString(),
                        nextAccRefNo.ToString(), jvLNo, 1, "Bill adjustment against - " + nextBillHdrRefNo.ToString(), trnType, Convert.ToDecimal(txtBillAdjAmt.Text),
                        nextBillHdrRefNo.ToString(), "0", "BDT", 1, Convert.ToDecimal(txtBillAdjAmt.Text), "", "", "", "", "", "", "", "", "", "", "",
                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtBillDate.Text), "Adjustment Account", Convert.ToDateTime(txtBillDate.Text),
                        "ADM", "O", "", Convert.ToDateTime(txtBillDate.Text), "BILV", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtBillAdjAmt.Text), "",
                        "", "", "N", 1, 0, "", "", "", "J", 0, "1", "BILV");
                }
                #endregion                               

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Bill Posted Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                LoadInitMrrDetGridData();
                SetMrrDetGridData();

                LoadInitBillDetGridData();
                SetBillDetGridData();

                //var taSupList = new View_Pend_Mrr_Bill_Sup_ListTableAdapter();
                //var dtSupList = taSupList.GetData();
                //ddlparty.DataSource = dtSupList;
                //ddlparty.DataValueField = "Wrk_Ac_Code";
                //ddlparty.DataTextField = "Par_Adr_Name";
                //ddlparty.DataBind();
                //ddlparty.Items.Insert(0, new ListItem("---Select---", "0"));

                txtSearchSupplier.Text = "";

                txtBillEntDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                var dtMaxBillRef = taBillHdr.GetMaxBillHdrRefNo(DateTime.Now.Year);
                var nextBillRef = dtMaxBillRef == null ? 1 : Convert.ToInt32(dtMaxBillRef) + 1;
                var nextBillRefNo = "ECIL-BILL-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextBillRef).ToString("000000");
                txtBillRefNo.Text = nextBillRefNo.ToString();

                txtSupBillNo.Text = "";

                txtBillAdjAmt.Text = "0";
                txtBillAmt.Text = "0";
                txtTotBillAmt.Text = "0";

                //btnHold.Visible = gvBillDet.Rows.Count > 0;
                btnPost.Visible = gvBillDet.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvMrrDet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvMrrDet.Rows[index];

                var lblMrrRefNo = (Label)(row.FindControl("lblMrrRefNo"));
                var lblMrrDate = (Label)(row.FindControl("lblMrrDate"));
                var hfMrrTrnDetLno = (HiddenField)(row.FindControl("hfMrrTrnDetLno"));
                var lblMrrPoRef = (Label)(row.FindControl("lblMrrPoRef"));
                var lblMrrChlnNo = (Label)(row.FindControl("lblMrrChlnNo"));
                var lblMrrItemCode = (Label)(row.FindControl("lblMrrItemCode"));
                var lblMrrItemName = (Label)(row.FindControl("lblMrrItemName"));
                var lblMrrItemUnit = (Label)(row.FindControl("lblMrrItemUnit"));
                var lblMrrQty = (Label)(row.FindControl("lblMrrQty"));
                var lblMrrRate = (Label)(row.FindControl("lblMrrRate"));
                var lblMrrAmount = (Label)(row.FindControl("lblMrrAmount"));

                var dtMrr = new DataTable();
                dtMrr = (DataTable)ViewState["dtMrrDet"];
                dtMrr.Rows[row.RowIndex].Delete();
                dtMrr.AcceptChanges();
                SetMrrDetGridData();

                var dtBill = new DataTable();
                dtBill = (DataTable)ViewState["dtBillDet"];
                dtBill.Rows.Add(lblMrrRefNo.Text, Convert.ToDateTime(lblMrrDate.Text).ToString("dd/MM/yyyy"), hfMrrTrnDetLno.Value, lblMrrPoRef.Text, lblMrrChlnNo.Text,
                    lblMrrItemCode.Text, lblMrrItemName.Text, lblMrrItemUnit.Text, lblMrrQty.Text, lblMrrRate.Text, lblMrrAmount.Text);
                ViewState["dtBillDet"] = dtBill;
                SetBillDetGridData();

                txtBillAdjAmt.Text = txtBillAdjAmt.Text.Trim().Length > 0 ? txtBillAdjAmt.Text.Trim() : "0";
                txtBillAmt.Text = totBillAmt.ToString("N2");
                txtTotBillAmt.Text = (Convert.ToDouble(totBillAmt) - Convert.ToDouble(txtBillAdjAmt.Text.Trim())).ToString("N2");

                //btnHold.Visible = gvBillDet.Rows.Count > 0;
                btnPost.Visible = gvBillDet.Rows.Count > 0;
            }
            catch (Exception ex)
            {         
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvBillDet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvBillDet.Rows[index];
                
                var lblBillMrrRefNo = (Label)(row.FindControl("lblBillMrrRefNo"));
                var lblBillMrrDate = (Label)(row.FindControl("lblBillMrrDate"));
                var hfBillMrrTrnDetLno = (HiddenField)(row.FindControl("hfBillMrrTrnDetLno"));
                var lblBillPoRef = (Label)(row.FindControl("lblBillPoRef"));
                var lblBillChlnNo = (Label)(row.FindControl("lblBillChlnNo"));
                var lblBillItemCode = (Label)(row.FindControl("lblBillItemCode"));
                var lblBillItemName = (Label)(row.FindControl("lblBillItemName"));
                var lblBillItemUnit = (Label)(row.FindControl("lblBillItemUnit"));
                var lblBillQty = (Label)(row.FindControl("lblBillQty"));
                var lblBillRate = (Label)(row.FindControl("lblBillRate"));
                var lblBillAmount = (Label)(row.FindControl("lblBillAmount"));

                var dtBill = new DataTable();
                dtBill = (DataTable)ViewState["dtBillDet"];
                dtBill.Rows[row.RowIndex].Delete();
                dtBill.AcceptChanges();
                SetBillDetGridData();

                var dtMrr = new DataTable();
                dtMrr = (DataTable)ViewState["dtMrrDet"];
                dtMrr.Rows.Add(lblBillMrrRefNo.Text, Convert.ToDateTime(lblBillMrrDate.Text).ToString("dd/MM/yyyy"), hfBillMrrTrnDetLno.Value, lblBillPoRef.Text,
                    lblBillChlnNo.Text, lblBillItemCode.Text, lblBillItemName.Text, lblBillItemUnit.Text, lblBillQty.Text, lblBillRate.Text, lblBillAmount.Text);
                ViewState["dtMrrDet"] = dtMrr;
                SetMrrDetGridData();

                txtBillAdjAmt.Text = txtBillAdjAmt.Text.Trim().Length > 0 ? txtBillAdjAmt.Text.Trim() : "0";
                txtBillAmt.Text = totBillAmt.ToString("N2");
                txtTotBillAmt.Text = (Convert.ToDouble(totBillAmt) - Convert.ToDouble(txtBillAdjAmt.Text.Trim())).ToString("N2");

                //btnHold.Visible = gvBillDet.Rows.Count > 0;
                btnPost.Visible = gvBillDet.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchSupplier.Text = "";

            txtBillAdjAmt.Text = "0";
            txtBillAmt.Text = "0";
            txtTotBillAmt.Text = "0";

            LoadInitMrrDetGridData();
            SetMrrDetGridData();

            LoadInitBillDetGridData();
            SetBillDetGridData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();

            if (txtSearchSupplier.Text.Trim().Length <= 0) return;

            try
            {
                var supRef = "";
                var srchWords = txtSearchSupplier.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    supRef = word;
                    break;
                }

                if (supRef.Length > 0)
                {
                    var dtSupAdr = taSupAdr.GetDataBySupAdrRef(supRef);
                    if (dtSupAdr.Rows.Count > 0)
                    {
                        txtBillAdjAmt.Text = "0";
                        txtBillAmt.Text = "0";
                        txtTotBillAmt.Text = "0";

                        LoadInitMrrDetGridData();
                        SetMrrDetGridData();

                        LoadInitBillDetGridData();
                        SetBillDetGridData();

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtMrrDet"];

                        var taSupList = new View_Acc_Fa_Te_Wh_Wd_MrrTableAdapter();
                        var dtSupList = taSupList.GetDataByPartyPendMrrBill(dtSupAdr[0].Par_Adr_Acc_Code.ToString());
                        foreach (dsAccTran.View_Acc_Fa_Te_Wh_Wd_MrrRow dr in dtSupList.Rows)
                        {
                            dt.Rows.Add(dr.Wrk_Match, dr.Wrk_Trn_DATE.ToString("dd/MM/yyyy"), dr.Wrk_Line_No, dr.Wrk_Dc_No, dr.Wrk_DepositNo, dr.Wrk_Adr_Code, dr.Wrk_Narration,
                                dr.Wd_T_C1, dr.Wd_T_C2, dr.Wrk_Curr_Rate, dr.Wrk_Amount);
                        }

                        ViewState["dtMrrDet"] = dt;
                        SetMrrDetGridData();
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }            
        }
    }
}