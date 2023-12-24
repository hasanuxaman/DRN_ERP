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
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesReturn : System.Web.UI.Page
    {
        //string rptFile;
        //string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {

            //reportInfo();

            //btnPrint.Attributes.Add("onclick", "javascript:w= window.open('frmShowSalesReport.aspx');");

            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrchCln.ContextKey = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();

            try
            {
                //Sales Return Ref
                var taInvHdrData = new tbl_InTr_Trn_HdrTableAdapter();
                var dtMaxRtnRef = taInvHdrData.GetMaxRtnRef(DateTime.Now.Year);
                var nextRtnRef = dtMaxRtnRef == null ? 1 : Convert.ToInt32(dtMaxRtnRef) + 1;
                var nextRtnRefNo = "ECIL-RTN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextRtnRef).ToString("000000");
                txtRtnRef.Text = nextRtnRefNo;
                txtRtnDate.Text = DateTime.Now.ToString();

                var taTransType = new tblTransportTypeTableAdapter();
                var dtTransType = taTransType.GetData();

                ddlRtnTranMode.DataSource = dtTransType;
                ddlRtnTranMode.DataTextField = "Trans_Type_Name";
                ddlRtnTranMode.DataValueField = "Trans_Type_Ref";
                ddlRtnTranMode.DataBind();

                ddlOrdTransMode.DataSource = dtTransType;
                ddlOrdTransMode.DataTextField = "Trans_Type_Name";
                ddlOrdTransMode.DataValueField = "Trans_Type_Ref";
                ddlOrdTransMode.DataBind();

                var taVslMas = new tbl_TrTr_Vsl_MasTableAdapter();
                cboRtnTruckNo.DataSource = taVslMas.GetDataByAsc();
                cboRtnTruckNo.DataTextField = "Vsl_Mas_No";
                cboRtnTruckNo.DataValueField = "Vsl_Mas_Ref";
                cboRtnTruckNo.DataBind();
                cboRtnTruckNo.Items.Insert(0, new ListItem("---Select---", "0"));

                for (Int64 year = 2014; year <= (DateTime.Now.Year); year++)
                {
                    cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }
                cboYear.SelectedValue = DateTime.Now.Year.ToString();

                for (int month = 1; month <= 12; month++)
                {
                    var strMonthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    cboMonth.Items.Add(new ListItem(strMonthName, month.ToString()));
                }
                cboMonth.SelectedValue = DateTime.Now.Month.ToString();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchChallanData();
        }

        private void SearchChallanData()
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new View_Challan_Details_NewTableAdapter();
            
            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var chlnRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    chlnRef = word;
                    break;
                }

                if (chlnRef.Length > 0)
                {
                    var dtInvHdr = taInvHdr.GetDataByChlnRef(chlnRef.ToString());
                    if (dtInvHdr.Rows.Count > 0)
                    {
                        //var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        //var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(dtInvHdr[0].Trn_Hdr_Pcode));
                        //if (dtPartyAdr.Rows.Count > 0) lblCustName.Text = dtPartyAdr[0].Par_Adr_Name;

                        txtChallanNo.Text = dtInvHdr[0].Trn_Hdr_Cno;
                        txtChallanDate.Text = dtInvHdr[0].Trn_Hdr_Date.ToString();
                        txtTruckNo.Text = dtInvHdr[0].Trn_Hdr_Com1;
                        txtDriverName.Text = dtInvHdr[0].Trn_Hdr_Com2;
                        txtDriverContact.Text = dtInvHdr[0].Trn_Hdr_Com3;
                        txtDelAddr.Text = dtInvHdr[0].Trn_Hdr_Com4;
                        ddlOrdTransMode.SelectedValue = dtInvHdr[0].Trn_Hdr_Exp_Typ;

                        var dtInvDet = taInvDet.GetDataByChlnRef(chlnRef.ToString());
                        gvChlnDet.DataSource = dtInvDet;
                        gvChlnDet.DataBind();
                        gvChlnDet.SelectedIndex = -1;

                        txtSearch.Enabled = false;
                        btnSaveChln.Enabled = gvChlnDet.Rows.Count > 0;
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Challan data not found";
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

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            try
            {
                //Sales Return Ref
                var taInvHdrData = new tbl_InTr_Trn_HdrTableAdapter();
                var dtMaxRtnRef = taInvHdrData.GetMaxRtnRef(DateTime.Now.Year);
                var nextRtnRef = dtMaxRtnRef == null ? 1 : Convert.ToInt32(dtMaxRtnRef) + 1;
                var nextRtnRefNo = "ECIL-RTN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextRtnRef).ToString("000000");
                txtRtnRef.Text = nextRtnRefNo;
                txtRtnDate.Text = DateTime.Now.ToString();

                var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
                var dtInvDet = taInvDet.GetDataByChlnRef("");
                gvChlnDet.DataSource = dtInvDet;
                gvChlnDet.DataBind();
                gvChlnDet.SelectedIndex = -1;

                var taInvDetNew = new View_InTr_Trn_Hdr_DetTableAdapter();
                gvPendRtn.DataSource = taInvDetNew.GetDataByRtnRefNo("");
                gvPendRtn.DataBind();

                txtSearch.Text = "";
                txtSearch.Enabled = true;
                txtChallanNo.Text = "";
                txtChallanDate.Text = "";
                txtTruckNo.Text = "";
                txtDriverName.Text = "";
                txtDriverContact.Text = "";
                txtDelAddr.Text = "";

                ddlRtnTranMode.SelectedIndex = 0;
                optTranBy.SelectedIndex = 0;
                cboRtnTruckNo.SelectedIndex = 0;
                cboRtnTruckNo.Visible = false;
                txtRtnTruckNo.Text = "";
                txtRtnTruckNo.Visible = true;
                txtRtnDriverName.Text = "";
                txtRtnDriverContact.Text = "";
                txtRemarks.Text = "";

                btnSaveChln.Enabled = false;

                cboYear.SelectedValue = DateTime.Now.Year.ToString();
                cboMonth.SelectedValue = DateTime.Now.Month.ToString();

                AutoCompleteExtenderSrchCln.ContextKey = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnSaveChln_Click(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();

            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                var chlnRef = "";
                var chlnRefNo = "";
                var chlnDate = "";
                var custRef = "";
                var custName = "";

                #region Form Data Validation
                if (txtSearch.Text.Trim().Length <= 0) return;
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    chlnRef = word;
                    break;
                }

                if (chlnRef.Length > 0)
                {
                    var taChlnChk = new View_Challan_DetailsTableAdapter();
                    var dtInvHdr = taChlnChk.GetDataByChlnRef(chlnRef.ToString());
                    if (dtInvHdr.Rows.Count > 0)
                    {
                        chlnRef = dtInvHdr[0].Trn_Hdr_DC_No;
                        chlnRefNo = dtInvHdr[0].Trn_Hdr_Cno;
                        custRef = dtInvHdr[0].Trn_Hdr_Pcode;
                        custName = dtInvHdr[0].T_C1;
                        chlnDate = dtInvHdr[0].Trn_Hdr_Date.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid challan number.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                var truckRef = "0";
                var truckNo = "";
                if (optTranBy.SelectedValue == "1")
                {
                    truckRef = "0";
                    truckNo = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToUpper(txtRtnTruckNo.Text.Trim());
                }
                else
                {
                    truckRef = cboRtnTruckNo.SelectedValue.ToString();
                    truckNo = cboRtnTruckNo.SelectedValue == "0" ? "" : cboRtnTruckNo.SelectedItem.ToString();
                }
                if (truckNo.ToString() == "" || truckNo.Length <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You have to select return Vehicle No first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var i = 0;
                foreach (GridViewRow gr in gvChlnDet.Rows)
                {
                    var chkChln = (CheckBox)(gr.FindControl("chkChln"));

                    if (chkChln.Checked) i++;
                }
                if (i <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You have to select challan first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                foreach (GridViewRow gr in gvChlnDet.Rows)
                {
                    var hfDelOrdRef = (HiddenField)(gr.FindControl("hfOrdRef"));
                    var hfChlnRef = (HiddenField)(gr.FindControl("hfChlnRef"));
                    var lblChlnRefNo = (Label)(gr.FindControl("lblChlnRefNo"));
                    var hfChlnDetLno = (HiddenField)(gr.FindControl("hfChlnDetLno"));
                    var lblPrevRtnQty = (Label)(gr.FindControl("lblPrevRtnQty"));
                    var txtRtnQty = (TextBox)(gr.FindControl("txtRtnQty"));
                    var txtRtnFreeQty = (TextBox)(gr.FindControl("txtRtnFreeQty"));
                    var chkChln = (CheckBox)(gr.FindControl("chkChln"));

                    if (chkChln.Checked)
                    {
                        if (txtRtnQty.Text.Trim() == "" || txtRtnQty.Text.Trim().Length <= 0 || Convert.ToDouble(txtRtnQty.Text.Trim()) <= 0)
                        {
                            if (txtRtnFreeQty.Text.Trim() == "" || txtRtnFreeQty.Text.Trim().Length <= 0 || Convert.ToDouble(txtRtnFreeQty.Text.Trim()) <= 0)
                            {
                                chkChln.BackColor = System.Drawing.Color.Red;
                                tblMsg.Rows[0].Cells[0].InnerText = "Please enter return quantity first.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                            else
                            {
                                var taChlnChk = new View_Challan_DetailsTableAdapter();
                                var dtChlnChk = taChlnChk.GetDataByChlnDoRef(hfChlnRef.Value.ToString(), hfDelOrdRef.Value.ToString());
                                if (dtChlnChk.Rows.Count > 0)
                                {
                                    if (Convert.ToDouble(txtRtnFreeQty.Text.Trim()) > Convert.ToDouble(dtChlnChk[0].FreeQty))
                                    {
                                        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to return free bag more than : " + dtChlnChk[0].FreeQty;
                                        tblMsg.Rows[1].Cells[0].InnerText = "";
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }
                                }
                                else
                                {
                                    chkChln.BackColor = System.Drawing.Color.Red;
                                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid challan number.";
                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            var taChlnChk = new View_Challan_DetailsTableAdapter();
                            //var taChlnChk = new tbl_InTr_Trn_DetTableAdapter();
                            var dtChlnChk = taChlnChk.GetDataByChlnDoRef(hfChlnRef.Value.ToString(), hfDelOrdRef.Value.ToString());
                            //var dtChlnChk = taChlnChk.GetDataByChlnRef(hfChlnRef.Value.ToString());
                            if (dtChlnChk.Rows.Count > 0)
                            {
                                if (Convert.ToDouble(txtRtnQty.Text.Trim()) > dtChlnChk[0].DelQty)
                                //if (Convert.ToDouble(txtRtnQty.Text.Trim()) > (dtChlnChk[0].Trn_Det_Bal_Qty))
                                {
                                    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to return qty more than : " + dtChlnChk[0].DelQty.ToString();
                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                            }
                            else
                            {
                                chkChln.BackColor = System.Drawing.Color.Red;
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid challan number.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }
                    }
                }                
                #endregion

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

                var nextHdrRef = 1;
                var nextHdrRefNo = "";

                var nextRtnRef = 1;
                var nextRtnRefNo = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);                

                //var truckNo = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToUpper(txtRtnTruckNo.Text.Trim());
                var driverName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtRtnDriverName.Text.Trim());

                if (hfEditStatus.Value == "N")
                {
                    //Inventory Header Ref
                    var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                    nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
                    nextHdrRefNo = "ECIL-RTN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");

                    //Sales Return Ref
                    var dtMaxRtnRef = taInvHdr.GetMaxRtnRef(DateTime.Now.Year);
                    nextRtnRef = dtMaxRtnRef == null ? 1 : Convert.ToInt32(dtMaxRtnRef) + 1;
                    nextRtnRefNo = "ECIL-RTN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextRtnRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, "RS", "SRT", nextRtnRefNo, custRef.ToString(), custRef.ToString(), custRef.ToString(),
                        nextRtnRefNo.ToString(), DateTime.Now, truckNo.ToString(), driverName.ToString(), txtRtnDriverContact.Text.Trim(),
                        "", "", "", "", "", "", "", 0, "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                        "ADM", "", ddlRtnTranMode.SelectedValue, "", chlnRef.ToString(), "", chlnRefNo, custName, chlnDate, "", 0, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    var dtMaxComSeqNo = taComm.GetMaxComSeqNo(nextHdrRefNo);
                    var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                    taComm.InsertTranCom(nextHdrRefNo, nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "CLN", "INI", "(Sales Return Initiated By: " + empName + ") " + txtRemarks.Text.Trim(), "", "1", "", "", "", "");

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvChlnDet.Rows)
                    {
                        var hfOrdRef = (HiddenField)(gr.FindControl("hfOrdRef"));
                        var hfOrdRefNo = (HiddenField)(gr.FindControl("hfOrdRefNo"));
                        var hfOrdDetLno = (HiddenField)(gr.FindControl("hfOrdDetLno"));
                        var hfChlnRef = (HiddenField)(gr.FindControl("hfChlnRef"));
                        var lblChlnRefNo = (Label)(gr.FindControl("lblChlnRefNo"));
                        var hfChlnDetLno = (HiddenField)(gr.FindControl("hfChlnDetLno"));
                        var hfChlnStrCode = (HiddenField)(gr.FindControl("hfChlnStrCode"));
                        var hfICode = (HiddenField)(gr.FindControl("hfICode"));
                        var lblIDesc = (Label)(gr.FindControl("lblIDesc"));
                        var lblUnit = (Label)(gr.FindControl("lblUnit"));
                        var txtRtnQty = (TextBox)(gr.FindControl("txtRtnQty"));
                        var txtRtnFreeQty = (TextBox)(gr.FindControl("txtRtnFreeQty"));
                        var chkChln = (CheckBox)(gr.FindControl("chkChln"));

                        if (chkChln.Checked)
                        {
                            var taDoDet = new tblSalesOrdDelDetTableAdapter();
                            Lno++;
                            if (txtRtnQty.Text.Trim() != "" || txtRtnQty.Text.Trim().Length > 0)
                            {
                                if (Convert.ToDouble(txtRtnQty.Text.Trim()) > 0)
                                {
                                    var dtDoDet = taDoDet.GetDataByDetLno(hfOrdRef.Value.ToString(), Convert.ToInt16(hfOrdDetLno.Value.ToString()));
                                    if (dtDoDet.Rows.Count > 0)
                                    {
                                        taInvDet.InsertInvDet(nextHdrRef.ToString(), "RS", "SRT", nextRtnRefNo.ToString(), Lno, "", dtDoDet[0].DO_Det_Lno,
                                            nextRtnRefNo.ToString(), Lno, hfICode.Value.ToString(), lblIDesc.Text.Trim(), lblUnit.Text.Trim(), hfChlnStrCode.Value.ToString(),
                                            hfOrdRef.Value.ToString(), chlnRef.ToString(), lblChlnRefNo.Text.ToString(), Convert.ToInt16(hfChlnDetLno.Value.ToString()),
                                            hfOrdRefNo.Value.ToString(), DateTime.Now, DateTime.Now, Convert.ToDouble(txtRtnQty.Text.Trim()),
                                            Convert.ToDouble(txtRtnFreeQty.Text.Trim()), dtDoDet[0].DO_Det_Lin_Rat,
                                            Convert.ToDecimal(txtRtnQty.Text.Trim()) * dtDoDet[0].DO_Det_Lin_Rat,
                                            Convert.ToDecimal(txtRtnQty.Text.Trim()) * dtDoDet[0].DO_Det_Lin_Rat, "", "", "", 0,
                                            Convert.ToDouble(txtRtnQty.Text.Trim()) + Convert.ToDouble(txtRtnFreeQty.Text.Trim()), "1", "");
                                    }
                                    else
                                    {
                                        myTran.Rollback();
                                        tblMsg.Rows[0].Cells[0].InnerText = "Sales Order does not match.";
                                        tblMsg.Rows[1].Cells[0].InnerText = "";
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }
                                }
                            }

                            if (txtRtnFreeQty.Text.Trim() != "" || txtRtnFreeQty.Text.Trim().Length > 0)
                            {
                                if (Convert.ToDouble(txtRtnFreeQty.Text.Trim()) > 0)
                                {
                                    var dtDoDet = taDoDet.GetDataByDetLno(hfOrdRef.Value.ToString(), Convert.ToInt16(hfOrdDetLno.Value.ToString()));
                                    if (dtDoDet.Rows.Count > 0)
                                    {
                                        taInvDet.InsertInvDet(nextHdrRef.ToString(), "RS", "BRT", nextRtnRefNo.ToString(), Lno, "", Convert.ToInt16(hfOrdDetLno.Value.ToString()),
                                            nextRtnRefNo.ToString(), Lno, hfICode.Value.ToString(), lblIDesc.Text.Trim(), lblUnit.Text.Trim(), hfChlnStrCode.Value.ToString(), hfOrdRef.Value.ToString(),
                                            chlnRef.ToString(), lblChlnRefNo.Text.ToString(), Convert.ToInt16(hfChlnDetLno.Value.ToString()), hfOrdRefNo.Value.ToString(),
                                            DateTime.Now, DateTime.Now, Convert.ToDouble(txtRtnFreeQty.Text.Trim()), 0, dtDoDet[0].DO_Det_Lin_Rat,
                                            Convert.ToDecimal(txtRtnFreeQty.Text.Trim()) * dtDoDet[0].DO_Det_Lin_Rat,
                                            Convert.ToDecimal(txtRtnFreeQty.Text.Trim()) * dtDoDet[0].DO_Det_Lin_Rat, "", "", "", 0, 0, "1", "");
                                    }
                                    else
                                    {
                                        myTran.Rollback();
                                        tblMsg.Rows[0].Cells[0].InnerText = "Sales Order does not match.";
                                        tblMsg.Rows[1].Cells[0].InnerText = "";
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    #endregion                    

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    var taInvDetNew = new View_InTr_Trn_Hdr_DetTableAdapter();
                    gvPendRtn.DataSource = taInvDetNew.GetDataByRtnRefNo(nextRtnRefNo.ToString());
                    gvPendRtn.DataBind();

                    //Sales Return Ref
                    var taInvHdrData = new tbl_InTr_Trn_HdrTableAdapter();
                    dtMaxRtnRef = taInvHdrData.GetMaxRtnRef(DateTime.Now.Year);
                    nextRtnRef = dtMaxRtnRef == null ? 1 : Convert.ToInt32(dtMaxRtnRef) + 1;
                    nextRtnRefNo = "ECIL-RTN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextRtnRef).ToString("000000");
                    txtRtnRef.Text = nextRtnRefNo;

                    var dtInvDet = taInvDet.GetDataByChlnRef("");
                    gvChlnDet.DataSource = dtInvDet;
                    gvChlnDet.DataBind();
                    gvChlnDet.SelectedIndex = -1;

                    txtSearch.Text = "";
                    txtChallanNo.Text = "";
                    txtChallanDate.Text = "";
                    txtTruckNo.Text = "";
                    txtDriverName.Text = "";
                    txtDriverContact.Text = "";
                    txtDelAddr.Text = "";
                    
                    txtSearch.Enabled = true;

                    //optTranBy.SelectedIndex = 0;
                    //cboRtnTruckNo.SelectedIndex = 0;
                    //cboRtnTruckNo.Visible = false;
                    //txtRtnTruckNo.Text = "";
                    //txtRtnTruckNo.Visible = true;
                    //txtRemarks.Text = "";

                    btnSaveChln.Enabled = false;                    

                    cboYear.SelectedValue = DateTime.Now.Year.ToString();
                    cboMonth.SelectedValue = DateTime.Now.Month.ToString();

                    AutoCompleteExtenderSrchCln.ContextKey = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();                    
                }
                else
                {
                    //if date is in edit mode
                }

                hfEditStatus.Value = "N";
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void optTranBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optTranBy.SelectedValue == "2")
            {
                txtRtnTruckNo.Text = "";
                txtRtnTruckNo.Visible = false;
                cboRtnTruckNo.SelectedIndex = 0;
                cboRtnTruckNo.Visible = true;
            }
            else
            {
                txtRtnTruckNo.Text = "";
                txtRtnTruckNo.Visible = true;
                cboRtnTruckNo.SelectedIndex = 0;
                cboRtnTruckNo.Visible = false;
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtenderSrchCln.ContextKey = cboYear.SelectedValue.ToString() + "-" + cboMonth.SelectedValue.ToString();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtenderSrchCln.ContextKey = cboYear.SelectedValue.ToString() + "-" + cboMonth.SelectedValue.ToString();
        }

        protected void gvChlnDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtRtnQty = (TextBox)e.Row.FindControl("txtRtnQty");
                TextBox txtRtnFreeQty = (TextBox)e.Row.FindControl("txtRtnFreeQty");
                TextBox txtTotRtnQty = (TextBox)e.Row.FindControl("txtTotRtnQty");

                Label lblChlnQty = (Label)e.Row.FindControl("lblChlnQty");
                Label lblChlnFreeQty = (Label)e.Row.FindControl("lblChlnFreeQty");

                txtRtnQty.Attributes.Add("onkeyup", "CalcFreeQty('" + txtRtnQty.ClientID + "', '" + txtRtnFreeQty.ClientID + "', '"
                    + lblChlnQty.Text.Trim() + "', '" + lblChlnFreeQty.Text.Trim() + "', '" + txtTotRtnQty.ClientID + "' )");

                txtRtnFreeQty.Attributes.Add("onkeyup", "CheckFreeQty('" + txtRtnQty.ClientID + "', '" + txtRtnFreeQty.ClientID + "', '"
                    + lblChlnQty.Text.Trim() + "', '" + lblChlnFreeQty.Text.Trim() + "', '" + txtTotRtnQty.ClientID + "' )");
            }
        }

        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //    reportInfo();
        //    var url = "frmShowSalesReport.aspx";
        //    Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
        //}

        //protected void reportInfo()
        //{
        //    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Ref}=" + ddlChallanList.SelectedValue;

        //    rptFile = "~/Module/Sales/Reports/rptDelChln.rpt";

        //    Session["RptDateFrom"] = DateTime.Now.ToString("dd/MM/yyyy");
        //    Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
        //    Session["RptFilePath"] = rptFile;
        //    Session["RptFormula"] = rptSelcFormula;
        //}        
    }
}
