using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesOrderCancel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnSearchOrder_Click(object sender, EventArgs e)
        {
            gvOrdDet.DataSource = null;
            gvOrdDet.DataBind();

            gvDoDet.DataSource = null;
            gvDoDet.DataBind();

            gvChlnDet.DataSource = null;
            gvChlnDet.DataBind();

            var taPartyAdr = new tblSalesPartyAdrTableAdapter();

            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var custRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
                            var dtSalesOrd = taSalesOrd.GetDataByCustRefDate(custRef.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                            gvOrdDet.DataSource = dtSalesOrd;
                            gvOrdDet.DataBind();
                            gvOrdDet.SelectedIndex = -1;
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
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

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (txtSearch.Text.Length <= 0) return;

                var custRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
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

        #region Get Grid Data
        public string GetSpName(string SpRef)
        {
            string spName = "";
            try
            {
                var taSp = new tblSalesPersonMasTableAdapter();
                var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(SpRef));
                if (dtSp.Rows.Count > 0)
                    spName = dtSp[0].Sp_Short_Name.ToString();
                return spName;
            }
            catch (Exception) { return spName; }
        }

        public string GetDoRate(string doRefNo)
        {
            string doRate = "0";
            try
            {
                var taDo = new tblSalesOrdDelDetTableAdapter();
                var dtDo = taDo.GetDataByDoRefNo(doRefNo);
                if (dtDo.Rows.Count > 0)
                    doRate = dtDo[0].DO_Det_Lin_Rat.ToString("N2");
                return doRate;
            }
            catch (Exception) { return doRate; }
        }

        public string GetDoTransRate(string doRefNo)
        {
            string doTransRate = "0";
            try
            {
                var taDo = new tblSalesOrdDelDetTableAdapter();
                var dtDo = taDo.GetDataByDoRefNo(doRefNo);
                if (dtDo.Rows.Count > 0)
                    doTransRate = dtDo[0].DO_Det_Trans_Rat.ToString("N2");
                return doTransRate;
            }
            catch (Exception) { return doTransRate; }
        }

        public string GetClnLinAmt(string clnQty, string doRefNo)
        {
            string doLinAmt = "0";
            try
            {
                var taDo = new tblSalesOrdDelDetTableAdapter();
                var dtDo = taDo.GetDataByDoRefNo(doRefNo);
                if (dtDo.Rows.Count > 0)
                    doLinAmt = (Convert.ToDouble(dtDo[0].DO_Det_Lin_Rat.ToString()) * Convert.ToDouble(clnQty)).ToString("N2");
                return doLinAmt;
            }
            catch (Exception) { return doLinAmt; }
        }

        public string GetClnNetAmt(string clnQty, string doRefNo)
        {
            string doNetAmt = "0";
            try
            {
                var taDo = new tblSalesOrdDelDetTableAdapter();
                var dtDo = taDo.GetDataByDoRefNo(doRefNo);
                if (dtDo.Rows.Count > 0)
                    doNetAmt = ((Convert.ToDouble(dtDo[0].DO_Det_Lin_Rat.ToString()) + Convert.ToDouble(dtDo[0].DO_Det_Trans_Rat.ToString())) * Convert.ToDouble(clnQty)).ToString("N2");
                return doNetAmt;
            }
            catch (Exception) { return doNetAmt; }
        }
        #endregion

        protected void gvOrdDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvOrdDet.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    var hfOrdHdrRef = ((HiddenField)gvOrdDet.Rows[indx].FindControl("hfOrdHdrRef")).Value.Trim();
                    var lblOrdHdrRefNo = ((Label)gvOrdDet.Rows[indx].FindControl("lblOrdHdrRefNo")).Text.Trim();
                    var lblCustName = ((Label)gvOrdDet.Rows[indx].FindControl("lblCustName")).Text.Trim();

                    var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                    var dtDelOrd = taDelOrd.GetDataBySoRefNo(lblOrdHdrRefNo.ToString());
                    gvDoDet.DataSource = dtDelOrd;
                    gvDoDet.DataBind();
                    gvDoDet.SelectedIndex = -1;

                }
                catch (Exception ex)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }        

        protected void gvDoDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvDoDet.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    var hfDoHdrRef = ((HiddenField)gvDoDet.Rows[indx].FindControl("hfDoHdrRef")).Value.Trim();
                    var lblDoHdrRefNo = ((Label)gvDoDet.Rows[indx].FindControl("lblDoHdrRefNo")).Text.Trim();

                    var taChlnDet = new View_Challan_DetailsTableAdapter();
                    var dtChlnDet = taChlnDet.GetDataByDoRef(hfDoHdrRef.ToString());
                    gvChlnDet.DataSource = dtChlnDet;
                    gvChlnDet.DataBind();
                    gvChlnDet.SelectedIndex = -1;

                }
                catch (Exception ex)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void btnCancelOrd_Click(object sender, EventArgs e)
        {
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();

            var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
            var taEmp = new View_Emp_BascTableAdapter();
            var taSoDelHdrDet = new VIEW_DELIVERY_ORDERTableAdapter();            

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesOrdHdr.Connection);

            try
            {
                #region Get Employee Details
                string empId = "", empName = "", empDesig = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                }
                #endregion

                #region S/O Validation
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfCustRef = (HiddenField)(row.FindControl("hfCustRef"));
                var hfOrdHdrRef = (HiddenField)(row.FindControl("hfOrdHdrRef"));
                var lblOrdHdrRefNo = (Label)(row.FindControl("lblOrdHdrRefNo"));
                var hfOrdDetLno = (HiddenField)(row.FindControl("hfOrdDetLno"));
                var HfICode = (HiddenField)(row.FindControl("HfICode"));
                var lblOrdLinQty = (Label)(row.FindControl("lblOrdLinQty"));
                var lblSoNetAmt = (Label)(row.FindControl("lblSoNetAmt"));
                var lblDoBalQty = (Label)(row.FindControl("lblDoBalQty"));
                var lblLinRate = (Label)(row.FindControl("lblLinRate"));
                var lblTransRate = (Label)(row.FindControl("lblTransRate"));

                var checkSoBal = false;
                //var dtSoDelHdrDet = taSoDelHdrDet.GetDataBySoRefNo(lblOrdHdrRefNo.Text.ToString());
                var dtSoDelHdrDet = taSoDelHdrDet.GetDataBySoRefNoDetLno(lblOrdHdrRefNo.Text.ToString(), Convert.ToInt16(hfOrdDetLno.Value.ToString()));
                if (dtSoDelHdrDet.Rows.Count > 0)
                {
                    if (dtSoDelHdrDet[0].DO_Det_Lin_Qty > 0 || dtSoDelHdrDet[0].DO_Det_Free_Qty > 0)
                    {
                        var dtSalesOrdNew = taSalesOrd.GetPendSoByDetLno(hfOrdHdrRef.Value.ToString(), Convert.ToInt16(hfOrdDetLno.Value.ToString()));
                        if (dtSalesOrdNew.Rows.Count > 0)
                        {
                            if (dtSalesOrdNew[0].SO_Det_Bal_Qty <= 0 && Convert.ToDouble(dtSalesOrdNew[0].SO_Det_Ext_Data2) <= 0 && dtSalesOrdNew[0].SO_Det_DO_Bal_Qty <= 0)
                            {
                                checkSoBal = false;

                                tblMsg.Rows[0].Cells[0].InnerText = "D/O Created against this Sales Order."; // +lblOrdHdrRefNo.Text.Trim();
                                tblMsg.Rows[1].Cells[0].InnerText = "Cancel D/O first. Ref No:" + dtSoDelHdrDet[0].DO_Det_Ref_No.ToString();
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                            else
                            {
                                checkSoBal = true;
                            }
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Sales Order details not found.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                }
                #endregion

                taSalesOrdHdr.AttachTransaction(myTran);
                taSalesOrdDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);

                var dtSoHdr = taSalesOrdHdr.GetDataByHdrRef(hfOrdHdrRef.Value.ToString());
                if (dtSoHdr.Rows.Count > 0)
                {
                    if (checkSoBal == true)
                    {
                        taSalesOrdDet.UpdateSalesDetOrdBal(hfOrdHdrRef.Value.ToString(), Convert.ToInt16(hfOrdDetLno.Value));

                        var ordCancelAmt = (Convert.ToDouble(lblLinRate.Text.Trim()) + Convert.ToDouble(lblLinRate.Text.Trim())) * Convert.ToDouble(lblDoBalQty.Text.Trim());
                        var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblOrdHdrRefNo.Text.Trim());
                        var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                        taComm.InsertTranCom(lblOrdHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INIT",
                            "Order Partially Canceled [Qty:" + lblDoBalQty.Text.Trim() + ", Amount: " + ordCancelAmt.ToString() + "]", "", "1", "", "", "", "");

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Sales Order Partially Canceled Successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    else
                    {
                        taSalesOrdHdr.UpdateSalesHdrStat("H", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                            1, "1", hfOrdHdrRef.Value.ToString());

                        taSalesOrdDet.UpdateSalesDetStat(1, "1", "H", hfOrdHdrRef.Value.ToString(), Convert.ToInt16(hfOrdDetLno.Value));

                        var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblOrdHdrRefNo.Text.Trim());
                        var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                        taComm.InsertTranCom(lblOrdHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INIT",
                            "Order Canceled [Qty:" + lblOrdLinQty.Text.Trim() + ", Amount: " + lblSoNetAmt.Text.Trim() + "]", "", "1", "", "", "", "");

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Sales Order Canceled Successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }                    
                }
                else
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Sales Order details not found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }               
                
                var dtSalesOrd = taSalesOrd.GetDataByCustRefDate(hfCustRef.Value.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                gvOrdDet.DataSource = dtSalesOrd;
                gvOrdDet.DataBind();
                gvOrdDet.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnCancelDo_Click(object sender, EventArgs e)
        {
            var taSalesDelHdr = new tblSalesOrdDelHdrTableAdapter();
            var taSalesDelDet = new tblSalesOrdDelDetTableAdapter();
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();
            var taChlnDet = new View_Challan_DetailsTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesDelHdr.Connection);

            try
            {
                #region D/O Validation
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfDoHdrRef = (HiddenField)(row.FindControl("hfDoHdrRef"));
                var lblDoHdrRefNo = (Label)(row.FindControl("lblDoHdrRefNo"));
                var hfDoDetLno = (HiddenField)(row.FindControl("hfDoDetLno"));
                var lblDoQty = (Label)(row.FindControl("lblDoLinQty"));
                var lblDoFreeQty = (Label)(row.FindControl("lblDoLinFreeQty"));

                var ordRef = "";
                var ordRefNo = "";
                var ordDetLNo = "";
                var custRef = "";

                var dtSalesDelDet = taSalesDelDet.GetDataByDetLno(hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                if (dtSalesDelDet.Rows.Count > 0)
                {
                    if (dtSalesDelDet[0].DO_Det_Bal_Qty <= 0)
                    {
                        var dtChlnDet = taChlnDet.GetDataByDoRef(hfDoHdrRef.Value.ToString());
                        if (dtChlnDet.Rows.Count > 0)
                        {
                            var dtClnQty = taChlnDet.GetChlnQtyByDoRef(hfDoHdrRef.Value.ToString());
                            var clnQty = dtClnQty == null ? 0 : Convert.ToDouble(dtClnQty);
                            if (clnQty > 0)
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "D/O Ref No: " + lblDoHdrRefNo.Text.Trim() + " executed.";
                                tblMsg.Rows[1].Cells[0].InnerText = "Challan Ref No:" + dtChlnDet[0].Trn_Hdr_Tran_Ref.ToString();
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }
                    }

                    var dtSalesOrdHdr = taSalesOrdHdr.GetDataByHdrRef(dtSalesDelDet[0].DO_Det_SO_Ref.ToString());
                    if (dtSalesOrdHdr.Rows.Count > 0)
                    {
                        ordRef = dtSalesOrdHdr[0].SO_Hdr_Ref.ToString();
                        ordRefNo = dtSalesOrdHdr[0].SO_Hdr_Ref_No.ToString();
                        ordDetLNo = dtSalesDelDet[0].DO_Det_SO_Lno.ToString();
                        custRef = dtSalesOrdHdr[0].SO_Hdr_Pcode.ToString();
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid D/O Details.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                taSalesDelHdr.AttachTransaction(myTran);
                taSalesDelDet.AttachTransaction(myTran);
                taSalesOrdDet.AttachTransaction(myTran);

                var dtSoDet = taSalesOrdDet.GetDataByDetLno(ordRef.ToString(), Convert.ToInt16(ordDetLNo.ToString()));
                if (dtSoDet.Rows.Count > 0)
                {
                    var dtDoDet = taSalesDelDet.GetDataByDetLno(hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                    if (dtDoDet.Rows.Count > 0)
                    {
                        if (dtDoDet[0].DO_Det_Bal_Qty > 0)
                        {                                                        
                            if (dtDoDet[0].DO_Det_Del_Qty>0)
                            {
                                taSalesDelHdr.UpdateDoHdrStat("P", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "0", hfDoHdrRef.Value.ToString());

                                taSalesDelDet.UpdateDoBalQty(Convert.ToDouble(dtDoDet[0].DO_Det_Ext_Data1), Convert.ToDouble(dtDoDet[0].DO_Det_Org_QTY), 0, 0, "0",                                                                   
                                    hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));

                                taSalesOrdDet.UpdateSalesDetDoBal(dtSoDet[0].SO_Det_Org_QTY - dtDoDet[0].DO_Det_Bal_Qty,                              
                                    (dtSoDet[0].SO_Det_Lin_Qty - (dtSoDet[0].SO_Det_Org_QTY - dtDoDet[0].DO_Det_Bal_Qty)),                                
                                    (dtSoDet[0].SO_Det_DO_Qty - (dtDoDet[0].DO_Det_Bal_Qty + Convert.ToDouble(dtDoDet[0].DO_Det_Ext_Data2))),                                
                                    dtSoDet[0].SO_Det_DO_Bal_Qty + (dtDoDet[0].DO_Det_Bal_Qty + Convert.ToDouble(dtDoDet[0].DO_Det_Ext_Data2)),                                
                                    (Convert.ToDouble(dtSoDet[0].SO_Det_Ext_Data1) - Convert.ToDouble(dtDoDet[0].DO_Det_Ext_Data2)).ToString(),                                
                                    ((dtSoDet[0].SO_Det_Free_Qty) - (Convert.ToDouble(dtSoDet[0].SO_Det_Ext_Data1) - Convert.ToDouble(dtDoDet[0].DO_Det_Ext_Data2))).ToString(),                                
                                    ordRef.ToString(), Convert.ToInt16(ordDetLNo.ToString()));
                            }
                            else
                            {
                                taSalesOrdDet.UpdateSalesDetDoBal((dtSoDet[0].SO_Det_Org_QTY - Convert.ToDouble(lblDoQty.Text.Trim())),
                                    (dtSoDet[0].SO_Det_Lin_Qty) - (dtSoDet[0].SO_Det_Org_QTY - Convert.ToDouble(lblDoQty.Text.Trim())),
                                    (dtSoDet[0].SO_Det_DO_Qty - (Convert.ToDouble(lblDoQty.Text.Trim()) + Convert.ToDouble(lblDoFreeQty.Text.Trim()))),
                                    dtSoDet[0].SO_Det_DO_Bal_Qty + ((Convert.ToDouble(lblDoQty.Text.Trim()) + Convert.ToDouble(lblDoFreeQty.Text.Trim()))),
                                    (Convert.ToDouble(dtSoDet[0].SO_Det_Ext_Data1) - Convert.ToDouble(lblDoFreeQty.Text.Trim())).ToString(),
                                    ((dtSoDet[0].SO_Det_Free_Qty) - (Convert.ToDouble(dtSoDet[0].SO_Det_Ext_Data1) - Convert.ToDouble(lblDoFreeQty.Text.Trim()))).ToString(),
                                    ordRef.ToString(), Convert.ToInt16(ordDetLNo.ToString()));

                                taSalesDelHdr.UpdateDoHdrStat("C", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "0", hfDoHdrRef.Value.ToString());                              
                            }                            
                        }
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "D/O Canceled Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                    var dtDelOrd = taDelOrd.GetDataBySoRefNo(ordRefNo.ToString());
                    gvDoDet.DataSource = dtDelOrd;
                    gvDoDet.DataBind();
                    gvDoDet.SelectedIndex = -1;
                }
                else
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Sales Order details does not match.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }                
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnCancelChallan_Click(object sender, EventArgs e)
        {               
            var taInTrTrnDet = new tbl_InTr_Trn_DetTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
            var taCrReal = new tblSalesCreditRealizeTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();

            var taEmp = new View_Emp_BascTableAdapter();
            var taChlnDet = new View_Challan_DetailsTableAdapter();
            var taDoDet = new tblSalesOrdDelDetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInTrTrnDet.Connection);

            try
            {
                #region Get Employee Details
                string empId = "", empName = "", empDesig = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                }
                #endregion

                var DoRef = "";
                var CustRef = "";

                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfClnHdrRef = (HiddenField)(row.FindControl("hfClnHdrRef"));
                var lblClnHdrRefNo = (Label)(row.FindControl("lblClnHdrRefNo"));

                //taInTrTrnHdr.AttachTransaction(myTran);
                taInTrTrnDet.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);
                taFaTe.AttachTransaction(myTran);
                taCrReal.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);

                var dtChlnDet = taChlnDet.GetDataByChlnRef(hfClnHdrRef.Value.Trim());
                foreach (dsInvTran.View_Challan_DetailsRow dr in dtChlnDet.Rows)
                {
                    DoRef = dr.Trn_Det_Ord_Ref.ToString();
                    CustRef = dr.Par_Adr_Ref_No.ToString();

                    #region Update Stk_Ctl and SalesOrdDelDet for DO Qty
                    if (Convert.ToDouble(dr.DelQty) > 0)
                    {
                        var dtDoDet = taDoDet.GetDataByDetLno(dr.Trn_Det_Ord_Ref.ToString(), Convert.ToInt16(dr.Trn_Det_Ord_Det_Lno.ToString()));
                        if (dtDoDet.Rows.Count > 0)
                        {
                            taDoDet.UpdateDoDeliveryQty((dtDoDet[0].DO_Det_Org_QTY - Convert.ToDouble(dr.DelQty)),
                                (dtDoDet[0].DO_Det_Lin_Qty) - (dtDoDet[0].DO_Det_Org_QTY - Convert.ToDouble(dr.DelQty)),
                                (dtDoDet[0].DO_Det_Del_Qty - Convert.ToDouble(dr.DelQty)),
                                Convert.ToDouble(Convert.ToDecimal(dtDoDet[0].DO_Det_Del_Bal_Qty) + Convert.ToDecimal(dr.DelQty)),
                                dr.Trn_Det_Ord_Ref.ToString(), Convert.ToInt16(dr.Trn_Det_Ord_Det_Lno.ToString()));

                            var dtStkCtl = taStkCtl.GetDataByStoreItem(dr.Trn_Det_Str_Code.ToString(), dr.Trn_Det_Icode.ToString());
                            if (dtStkCtl.Rows.Count > 0)
                            {
                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(dr.DelQty)), 4), dr.Trn_Det_Str_Code.ToString(),
                                    dr.Trn_Det_Icode.ToString());
                            }
                            else
                            {
                                taStkCtl.InsertItemStore(dr.Trn_Det_Str_Code.ToString(), dr.Trn_Det_Icode.ToString(), "",
                                    Math.Round((Convert.ToDouble(dr.DelQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now, DateTime.Now, "", "", "", 0);
                            }
                        }
                        else
                        {
                            myTran.Rollback();
                            tblMsg.Rows[0].Cells[0].InnerText = "Delivery Order does not match.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    #endregion

                    #region Update Stk_Ctl and SalesOrdDelDet for DO Free Qty
                    if (Convert.ToDouble(dr.FreeQty) > 0)
                    {
                        var dtDoDet = taDoDet.GetDataByDetLno(dr.Trn_Det_Ord_Ref.ToString(), Convert.ToInt16(dr.Trn_Det_Ord_Det_Lno.ToString()));
                        if (dtDoDet.Rows.Count > 0)
                        {
                            taDoDet.UpdateDoFreeBagDeliveryQty(dtDoDet[0].DO_Det_Del_Qty - Convert.ToDouble(dr.FreeQty),
                                    dtDoDet[0].DO_Det_Bal_Qty + Convert.ToDouble(dr.FreeQty),
                                    (Convert.ToDouble(dtDoDet[0].DO_Det_Ext_Data1) - Convert.ToDouble(dr.FreeQty)).ToString(),
                                    (Convert.ToDouble(dtDoDet[0].DO_Det_Ext_Data2) + Convert.ToDouble(dr.FreeQty)).ToString(),
                                    dr.Trn_Det_Ord_Ref.ToString(), Convert.ToInt16(dr.Trn_Det_Ord_Det_Lno.ToString()));

                            var dtStkCtl = taStkCtl.GetDataByStoreItem(dr.Trn_Det_Str_Code.ToString(), dr.Trn_Det_Icode.ToString());
                            if (dtStkCtl.Rows.Count > 0)
                            {

                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(dr.FreeQty)), 4), dr.Trn_Det_Str_Code.ToString(),
                                    dr.Trn_Det_Icode.ToString());
                            }
                            else
                            {
                                taStkCtl.InsertItemStore(dr.Trn_Det_Str_Code.ToString(), dr.Trn_Det_Icode.ToString(), "",
                                    Math.Round((Convert.ToDouble(dr.FreeQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now, DateTime.Now, "", "", "", 0);
                            }
                        }
                        else
                        {
                            myTran.Rollback();
                            tblMsg.Rows[0].Cells[0].InnerText = "Delivery Order does not match.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    #endregion
                }

                if (dtChlnDet.Rows.Count > 0)
                {
                    //--------Update Challan Qty
                    taInTrTrnDet.UpdateCancelChallanQty(0, 0, 0, 0, 0, 0, hfClnHdrRef.Value.Trim());

                    //--------Update Challan Invoice Value
                    taFaTe.UpdateChallanValue(0, 0, 0, lblClnHdrRefNo.Text.Trim());

                    var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblClnHdrRefNo.Text.Trim());
                    var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                    taComm.InsertTranCom(lblClnHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "CLN-DEL", "APP", "(Cancel Sales Challan By: " + empName + ")", "", "1", "", "", "", "");

                    #region Credit Realization Entry Old
                    //var dtTotChlnAmt = taFaTe.GetTotChlnAmt(lblClnHdrRefNo.Text.Trim());
                    //var rcvAmt = dtTotChlnAmt == null ? 0 : Convert.ToDecimal(dtTotChlnAmt);
                    //var dtCrReal = taCrReal.GetRelizedChlnByCustRef(CustRef.ToString());
                    //foreach (dsSalesTran.tblSalesCreditRealizeRow drNew in dtCrReal.Rows)
                    //{
                    //    if (rcvAmt > (drNew.Sales_Chln_Amt - drNew.Sales_Chln_Due_Amt))
                    //    {
                    //        rcvAmt = rcvAmt - (drNew.Sales_Chln_Amt - drNew.Sales_Chln_Due_Amt);
                    //        taCrReal.UpdateCreditRealizeAmt((drNew.Sales_Chln_Amt - drNew.Sales_Chln_Due_Amt), drNew.Sales_Chln_Pay_Rcv_Ref + "Cln. Cncl Amt: " + (drNew.Sales_Chln_Amt - drNew.Sales_Chln_Due_Amt).ToString("N2") + ", ", drNew.Sales_Chln_Ref);                            
                    //    }
                    //    else
                    //    {
                    //        taCrReal.UpdateCreditRealizeAmt(drNew.Sales_Chln_Due_Amt + rcvAmt, drNew.Sales_Chln_Pay_Rcv_Ref + "Chln. Cncl Amt: " + (drNew.Sales_Chln_Due_Amt + rcvAmt).ToString("N2") + ", ", drNew.Sales_Chln_Ref);
                    //        rcvAmt = 0;
                    //        break;
                    //    }
                    //}
                    #endregion

                    #region Credit Realization Entry
                    var dtTotChlnAmt = taFaTe.GetTotChlnAmt(lblClnHdrRefNo.Text.Trim());
                    var rcvAmt = dtTotChlnAmt == null ? 0 : Convert.ToDecimal(dtTotChlnAmt);
                    var dtCrReal = taCrReal.GetPendChlnByCustRef(CustRef.ToString());
                    foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                    {
                        if (rcvAmt > dr.Sales_Chln_Due_Amt)
                        {
                            rcvAmt = rcvAmt - dr.Sales_Chln_Due_Amt;
                            taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "CNCL: " + lblClnHdrRefNo.Text + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                        }
                        else
                        {
                            taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "CNCL: " + lblClnHdrRefNo.Text + " Amt: " + rcvAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                            rcvAmt = 0;
                            break;
                        }
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Sales Challan Canceled Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    var dtChlnDetNew = taChlnDet.GetDataByDoRef(DoRef.ToString());
                    gvChlnDet.DataSource = dtChlnDetNew;
                    gvChlnDet.DataBind();
                    gvChlnDet.SelectedIndex = -1;
                }
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