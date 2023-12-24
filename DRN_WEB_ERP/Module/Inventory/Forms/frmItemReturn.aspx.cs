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
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmItemReturn : System.Web.UI.Page
    {
        //Type:RS	Code:SRT

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtIssDateFrm.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
            txtIssDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            var taInvTrn = new VIEW_INV_TRAN_DETTableAdapter();
            var dtInvTran = new dsInvTran.VIEW_INV_TRAN_DETDataTable();

            try
            {
                #region Get Location Details
                var locRef = "";
                var locName = "";
                var locAccCode = "";
                var srchWords = txtSearchIssLoc.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    locRef = word;
                    break;
                }

                if (locRef.Length > 0)
                {
                    var taIssLoc = new View_Issue_HeadTableAdapter();
                    var dtIssLoc = taIssLoc.GetDataByIsuHeadRef(locRef);
                    locRef = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].IsuHeadRefNo.ToString() : "";
                    locName = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].IsuHeadName.ToString() : "";
                    //locAccCode = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].Str_Loc_Ref.ToString() : "";   
                }
                #endregion

                #region Get Item Details
                var itemRef = "";
                var srchWordsItem = txtSearchItem.Text.Trim().Split(':');
                foreach (string word in srchWordsItem)
                {
                    itemRef = word;
                    break;
                }

                if (itemRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(itemRef, out result))
                    {
                        var taItem = new tbl_InMa_Item_DetTableAdapter();
                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                        if (dtItem.Rows.Count > 0)
                            itemRef = dtItem[0].Itm_Det_Ref.ToString();
                    }
                }
                #endregion

                if (txtSearchIssLoc.Text.Trim().Length > 0)
                {
                    if (txtSearchIssLoc.Text.Trim().Length > 0 && txtSearchItem.Text.Trim().Length > 0)
                        dtInvTran = taInvTrn.GetDataByDatePartyItem("IS", "ISU", locRef, itemRef, txtIssDateFrm.Text.Trim(), txtIssDateTo.Text.Trim());
                    else
                        dtInvTran = taInvTrn.GetDataByDateParty("IS", "ISU", locRef, txtIssDateFrm.Text.Trim(), txtIssDateTo.Text.Trim());
                }
                else
                {
                    if (txtSearchItem.Text.Trim().Length > 0)
                        dtInvTran = taInvTrn.GetDataByDateItem("IS", "ISU", itemRef, txtIssDateFrm.Text.Trim(), txtIssDateTo.Text.Trim());
                }
                gvItemIssue.DataSource = dtInvTran;
                gvItemIssue.DataBind();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            txtSearchIssLoc.Text = "";
            txtSearchItem.Text = "";
            txtIssDateFrm.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
            txtIssDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
            gvItemIssue.DataSource = null;
            gvItemIssue.DataBind();
        }

        #region GridData
        public string GetIssueTo(string issuHead)
        {
            string issuedTo = "";
            try
            {
                var taIssLoc = new View_Issue_HeadTableAdapter();
                var dtIssLoc = taIssLoc.GetDataByIsuHeadRef(issuHead.ToString());
                if (dtIssLoc.Rows.Count > 0)
                    issuedTo = dtIssLoc[0].IsuHeadCode.ToString() + "::" + dtIssLoc[0].IsuHeadName.ToString() + "::" + dtIssLoc[0].IsuHeadDet.ToString();
                return issuedTo;
            }
            catch (Exception ex) { return issuedTo; }
        }
        public string GetStore(string storeRef)
        {
            string strStore = "";
            try
            {
                var taStrLoc = new tbl_InMa_Str_LocTableAdapter();
                var dtStrLoc = taStrLoc.GetDataByLocRef(Convert.ToInt32(storeRef.ToString()));
                if (dtStrLoc.Rows.Count > 0)
                    strStore = dtStrLoc[0].Str_Loc_Code.ToString();
                return strStore;
            }
            catch (Exception ex) { return strStore; }
        }
        public string GetTotRtnQty(string issuRef, string itemRef)
        {
            double totRtnQty = 0;
            try
            {                
                var taIssDet = new tbl_InTr_Trn_DetTableAdapter();
                var dtIssDet = taIssDet.GetDataByRtnIsuRef(issuRef.ToString(), itemRef);
                foreach (dsInvTran.tbl_InTr_Trn_DetRow dr in dtIssDet)
                {
                    totRtnQty = totRtnQty + dr.Trn_Det_Lin_Qty;
                }
                return totRtnQty.ToString();
            }
            catch (Exception ex) { return totRtnQty.ToString(); }
        }
        public string GetTotRtnBalQty(string issuRef, string itemRef, string issuRtnRef)
        {
            double totIsuQty = 0;
            double totRtnQty = 0;
            double totRtnBalQty = 0;
            try
            {
                var taIssDet = new tbl_InTr_Trn_DetTableAdapter();
                var dtIssDet = taIssDet.GetDataByHdrRefItmRef(issuRef.ToString(), itemRef);
                foreach (dsInvTran.tbl_InTr_Trn_DetRow dr in dtIssDet)
                {
                    totIsuQty = totIsuQty + dr.Trn_Det_Lin_Qty;
                }

                var dtIssRtnDet = taIssDet.GetDataByRtnIsuRef(issuRtnRef.ToString(), itemRef);
                foreach (dsInvTran.tbl_InTr_Trn_DetRow dr in dtIssRtnDet)
                {
                    totRtnQty = totRtnQty + dr.Trn_Det_Lin_Qty;
                }

                totRtnBalQty = totIsuQty - totRtnQty;

                return totRtnBalQty.ToString();
            }
            catch (Exception ex) { return totRtnBalQty.ToString(); }
        }        
        #endregion
       
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var locRef = "";
                var srchWords = txtSearchIssLoc.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    locRef = word;
                    break;
                }

                if (locRef.Length > 0)
                {
                    var taSupAdr = new View_Issue_HeadTableAdapter();
                    var dtSupAdr = taSupAdr.GetDataByIsuHeadRef(locRef);
                    if (dtSupAdr.Rows.Count > 0)
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var itemRef = "";
                var srchWords = txtSearchItem.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    itemRef = word;
                    break;
                }

                if (itemRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(itemRef, out result))
                    {
                        var taItem = new tbl_InMa_Item_DetTableAdapter();
                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                        if (dtItem.Rows.Count > 0)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (txtSearchIssLoc.Text.Trim().Length > 0 || txtSearchItem.Text.Trim().Length > 0)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                GridViewRow gr = ((GridViewRow)((Button)sender).NamingContainer);
                var hfIsuHdrRef = (HiddenField)(gr.FindControl("hfIsuHdrRef"));
                var lblIsuHdrRefNo = (Label)(gr.FindControl("lblIsuHdrRefNo"));
                var lblIsuDetLno = (Label)(gr.FindControl("lblIsuDetLno"));
                var hfIsuLocRef = (HiddenField)(gr.FindControl("hfIsuLocRef"));
                var hfIsuDetIcode = (HiddenField)(gr.FindControl("hfIsuDetIcode"));
                var lblIsuDetItemName = (Label)(gr.FindControl("lblIsuDetItemName"));
                var lblIsuDetItemUnit = (Label)(gr.FindControl("lblIsuDetItemUnit"));
                var lblIsuStoreRef = (HiddenField)(gr.FindControl("lblIsuStoreRef"));
                var txtRtnQty = (TextBox)(gr.FindControl("txtRtnQty"));

                var RtnQty = Convert.ToDouble(txtRtnQty.Text.Trim());

                #region Data Validation
                double totIsuQty = 0;
                double totRtnQty = 0;
                double totRtnBalQty = 0;

                var taIssDet = new tbl_InTr_Trn_DetTableAdapter();
                var dtIssDet = taIssDet.GetDataByHdrRefItmRef(hfIsuHdrRef.Value.ToString(), hfIsuDetIcode.Value.ToString());
                foreach (dsInvTran.tbl_InTr_Trn_DetRow dr in dtIssDet)
                {
                    totIsuQty = totIsuQty + dr.Trn_Det_Lin_Qty;
                }

                var dtIssRtnDet = taIssDet.GetDataByRtnIsuRef(lblIsuHdrRefNo.Text.ToString(), hfIsuDetIcode.Value.ToString());
                foreach (dsInvTran.tbl_InTr_Trn_DetRow dr in dtIssRtnDet)
                {
                    totRtnQty = totRtnQty + dr.Trn_Det_Lin_Qty;
                }
                totRtnBalQty = totIsuQty - totRtnQty;

                if (RtnQty > totRtnBalQty)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Yoy are not allowed to return more than quantity " + totRtnBalQty.ToString();
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #region Get Location Details
                var locRef = "";
                var locName = "";
                var locAccCode = "";
                if (hfIsuLocRef.Value.ToString().Length > 0)
                {
                    var taIssLoc = new View_Issue_HeadTableAdapter();
                    var dtIssLoc = taIssLoc.GetDataByIsuHeadRef(locRef);
                    locRef = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].IsuHeadRefNo.ToString() : "";
                    locName = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].IsuHeadName.ToString() : "";
                    //locAccCode = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].Str_Loc_Ref.ToString() : "";   
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Location.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #region Get Item Details
                var itemName = "";
                var itemAcc = "";
                var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(hfIsuDetIcode.Value.Trim()));
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

                var nextHdrRef = 1;
                var nextHdrRefNo = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);

                //Inventory Header Ref
                var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("SRT", DateTime.Now.Year);
                var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                nextHdrRefNo = "ECIL-SRT-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                taInvHdr.InsertInvHdr(nextHdrRef, "RS", "SRT", nextHdrRefNo, locRef.ToString(), locRef.ToString(), locRef.ToString(),
                                    lblIsuHdrRefNo.Text.Trim(), DateTime.Now, "", "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "P",
                                    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                                    "", "", "", locName, "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                //var dtMaxAccRef = taAcc.GetMaxRefNo("MJV");
                //var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                //var nextAccRefNo = "MJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                #region Insert Inventory Details
                if (RtnQty > 0)
                {
                    taInvDet.InsertInvDet(nextHdrRef.ToString(), "RS", "SRT", nextHdrRefNo.ToString(), 1, "", 1, lblIsuHdrRefNo.Text.Trim(),
                        1, hfIsuDetIcode.Value.Trim(), lblIsuDetItemName.Text.Trim(), lblIsuDetItemUnit.Text.Trim(), lblIsuStoreRef.Value.ToString(), "",
                        lblIsuHdrRefNo.Text.Trim(), lblIsuHdrRefNo.Text.Trim(), 1, "", DateTime.Now, DateTime.Now,
                        Convert.ToDouble(RtnQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(RtnQty) * Convert.ToDecimal(0),
                        Convert.ToDecimal(RtnQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");

                    var dtStkCtl = taStkCtl.GetDataByStoreItem(lblIsuStoreRef.Value.ToString(), hfIsuDetIcode.Value.Trim());
                    if (dtStkCtl.Rows.Count > 0)
                        taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(RtnQty)), 4), lblIsuStoreRef.Value.ToString(), hfIsuDetIcode.Value.Trim());
                    else
                        taStkCtl.InsertItemStore(lblIsuStoreRef.Value.ToString(), hfIsuDetIcode.Value.Trim(), "", Math.Round((Convert.ToDouble(RtnQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                            DateTime.Now, DateTime.Now, "", "", "", 0);
                }
                #endregion

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                GetData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}