using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmPaymentRcvEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtender1.ContextKey = "P";
            AutoCompleteExtender2.ContextKey = "P";

            txtFrmDate.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                optUpdtType.Enabled = false;
                txtGlCode.Text = "";
                txtTranRefNo.Text = "";
                txtNewAmt.Text = "";
                txtTrnDrCrType.Text = "";
                txtNewDate.Text = "";
                txtNewCust.Text = "";
                txtNewAmt.Enabled = false;
                txtNewCust.Enabled = false;
                txtNewDate.Enabled = false;
                btnUpdt.Enabled = false;

                var custCode = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custCode = word;
                    break;
                }

                if (custCode.Length > 0)
                {
                    var taGlCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtGlCoa = taGlCoa.GetDataByCoaCode(custCode.ToString());
                    if (dtGlCoa.Rows.Count > 0)
                    {
                        var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        var dtFaTe = taFaTe.GetDataByDateRangeAccCodeTrnFlag(dtGlCoa[0].Gl_Coa_Code.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), "RJV");
                        gvPayDet.DataSource = dtFaTe;
                        gvPayDet.DataBind();
                        gvPayDet.SelectedIndex = -1;
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer GL Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer Name.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void optUpdtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (optUpdtType.SelectedValue == "0")
                {
                    txtNewAmt.Enabled = false;
                    txtNewCust.Enabled = false;
                    txtNewDate.Enabled = false;
                    btnUpdt.Enabled = false;
                }
                if (optUpdtType.SelectedValue == "1")
                {
                    txtNewAmt.Enabled = true;
                    txtNewCust.Enabled = false;
                    txtNewDate.Enabled = false;
                    btnUpdt.Enabled = true;
                }
                if (optUpdtType.SelectedValue == "2")
                {
                    txtNewAmt.Enabled = false;
                    txtNewCust.Enabled = false;
                    txtNewDate.Enabled = true;
                    btnUpdt.Enabled = true;
                }
                if (optUpdtType.SelectedValue == "3")
                {
                    txtNewAmt.Enabled = false;
                    txtNewCust.Enabled = true;
                    txtNewDate.Enabled = false;
                    btnUpdt.Enabled = true;
                }

                //var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                //var dtFaTe = taFaTe.GetDataByJvRef(txtTranRefNo.Text.ToString());
                //gvPayDet.DataSource = dtFaTe;
                //gvPayDet.DataBind();

                //if (dtFaTe.Rows.Count > 0)
                //{
                //    optUpdtType.Enabled = true;
                //    txtGlCode.Text = txtGlCode.Text.ToString();
                //    txtTranRefNo.Text = dtFaTe[0].Trn_Ref_No.ToString();
                //    txtNewAmt.Text = dtFaTe[0].Trn_Amount.ToString("N2");
                //    txtNewDate.Text = dtFaTe[0].Trn_DATE.ToString("dd/MM/yyyy");
                //    var taGlCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                //    var dtGlCoa = taGlCoa.GetDataByCoaCode(txtGlCode.Text.Trim());
                //    txtNewCust.Text = dtGlCoa.Rows.Count > 0 ? (dtGlCoa[0].Gl_Coa_Code.ToString() + ":" + dtGlCoa[0].Gl_Coa_Name.ToString()) : "";
                //}
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvPayDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvPayDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvPayDet.SelectedIndex;

            if (indx != -1)
            {                                               
                try
                {
                    Label lblTrnRefNo = (Label)gvPayDet.Rows[indx].FindControl("lblTrnRefNo");
                    HiddenField hfGlCode = (HiddenField)gvPayDet.Rows[indx].FindControl("hfGlCode");
                    Label lblTrnDrCrType = (Label)gvPayDet.Rows[indx].FindControl("lblTrnDrCrType");

                    var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                    var dtFaTe = taFaTe.GetDataByJvRef(lblTrnRefNo.Text.ToString());
                    gvPayDet.DataSource = dtFaTe;
                    gvPayDet.DataBind();
                    gvPayDet.SelectedIndex = -1;

                    if (dtFaTe.Rows.Count > 0)
                    {
                        optUpdtType.Enabled = true;
                        txtGlCode.Text = hfGlCode.Value.ToString();
                        txtTranRefNo.Text = dtFaTe[0].Trn_Ref_No.ToString();
                        txtNewAmt.Text = dtFaTe[0].Trn_Amount.ToString("N2");
                        txtTrnDrCrType.Text = lblTrnDrCrType.Text.Trim();
                        txtNewDate.Text = dtFaTe[0].Trn_DATE.ToString("dd/MM/yyyy");
                        var taGlCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                        var dtGlCoa = taGlCoa.GetDataByCoaCode(hfGlCode.Value.ToString());
                        txtNewCust.Text = dtGlCoa.Rows.Count > 0 ? (dtGlCoa[0].Gl_Coa_Code.ToString() + ":" + dtGlCoa[0].Gl_Coa_Name.ToString()) : "";
                    }                    

                    //optUpdtType.SelectedValue = "0";                    
                    //txtNewAmt.Enabled = false;
                    //txtNewCust.Enabled = false;
                    //txtNewDate.Enabled = false;
                    //btnUpdt.Enabled = false;
                }
                catch (Exception ex)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void btnUpdt_Click(object sender, EventArgs e)
        {
            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
            var taCrReal = new tblSalesCreditRealizeTableAdapter();
            var dtFaTe = new dsAccTran.tbl_Acc_Fa_TeDataTable();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taFaTe.Connection);

            var userCode = Session["sessionUserCode"] == null ? "0" : Session["sessionUserCode"].ToString();

            try
            {
                var custPrevRef = "";                
                var taCust = new tblSalesPartyAdrTableAdapter();
                var dtCust = new dsSalesMas.tblSalesPartyAdrDataTable();

                dtCust = taCust.GetDataByPartyAccRef(txtGlCode.Text.Trim());
                custPrevRef = dtCust.Rows.Count > 0 ? dtCust[0].Par_Adr_Ref.ToString() : "0";                

                taFaTe.AttachTransaction(myTran);
                taCrReal.AttachTransaction(myTran);
                
                if (optUpdtType.SelectedValue == "0") return;

                var dtFaTeNew = taFaTe.GetDataByJvRef(txtTranRefNo.Text.Trim());
                if (dtFaTeNew.Rows.Count > 0)
                {
                    if (optUpdtType.SelectedValue == "1")
                    {
                        dtFaTe = taFaTe.GetDataByJvRefAccCode(txtTranRefNo.Text.Trim(), txtGlCode.Text.Trim());
                        if (dtFaTe.Rows.Count > 0)
                        {
                            if (dtFaTe[0].Trn_Ac_Type == "P")
                            {
                                if (Convert.ToDecimal(dtFaTe[0].Trn_Amount.ToString("N2")) != Convert.ToDecimal(txtNewAmt.Text.Trim()))
                                {
                                    if (Convert.ToDecimal(txtNewAmt.Text.Trim()) > Convert.ToDecimal(dtFaTe[0].Trn_Amount))
                                    {
                                        #region Credit Realization Entry
                                        var rcvAmt = Convert.ToDecimal(txtNewAmt.Text.Trim()) - Convert.ToDecimal(dtFaTe[0].Trn_Amount);
                                        var dtCrReal = taCrReal.GetPendChlnByCustRef(custPrevRef.ToString());
                                        foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                        {
                                            if (rcvAmt > dr.Sales_Chln_Due_Amt)
                                            {
                                                rcvAmt = rcvAmt - dr.Sales_Chln_Due_Amt;
                                                taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Rev.Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                            }
                                            else
                                            {
                                                taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Rev. Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + rcvAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                rcvAmt = 0;
                                                break;
                                            }
                                        }
                                        #endregion
                                    }

                                    if (Convert.ToDecimal(txtNewAmt.Text.Trim()) < Convert.ToDecimal(dtFaTe[0].Trn_Amount))
                                    {
                                        #region Credit Realization Entry
                                        var rcvAmt = Convert.ToDecimal(dtFaTe[0].Trn_Amount) - Convert.ToDecimal(txtNewAmt.Text.Trim());
                                        var dtCrReal = taCrReal.GetRelizedChlnByCustRef(custPrevRef.ToString());
                                        foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                        {
                                            if (rcvAmt > (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt))
                                            {
                                                rcvAmt = rcvAmt - (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt);
                                                taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt + (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt), dr.Sales_Chln_Pay_Rcv_Ref + "Rev.Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt).ToString("N2") + ", ", dr.Sales_Chln_Ref);                                                
                                            }
                                            else
                                            {
                                                taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt + rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Rev.Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + (dr.Sales_Chln_Due_Amt + rcvAmt).ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                rcvAmt = 0;
                                                break;
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                        taFaTe.UpdatePayRcvVal(Convert.ToDecimal(txtNewAmt.Text), Convert.ToDecimal(txtNewAmt.Text), Convert.ToDecimal(txtNewAmt.Text),
                            dtFaTeNew[0].Trn_Narration + " [Amount Revised (" + dtFaTeNew[0].Trn_Amount.ToString("N2") + "),(" + userCode.ToString() + ")]", txtTranRefNo.Text.Trim());
                        myTran.Commit();
                    }

                    if (optUpdtType.SelectedValue == "2")
                    {
                        taFaTe.UpdatePayRcvDate(Convert.ToDateTime(txtNewDate.Text), dtFaTeNew[0].Trn_Narration + " [Date Revised (" + dtFaTeNew[0].Trn_DATE.ToString("dd/MM/yyyy") + "),(" + userCode.ToString() + ")]",
                            txtTranRefNo.Text.Trim());
                        myTran.Commit();
                    }

                    if (optUpdtType.SelectedValue == "3")
                    {
                        //verify code again---------------
                        var taGlCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                        var dtGlCoa = new dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable();

                        #region Get Prev GL Code
                        var glPrevCode = "";
                        var glPrevName = "";
                        var glPrevType = "";
                        dtGlCoa = taGlCoa.GetDataByCoaCode(txtGlCode.Text.Trim());
                        if (dtGlCoa.Rows.Count > 0)
                        {
                            glPrevCode = dtGlCoa[0].Gl_Coa_Code.ToString();
                            glPrevName = dtGlCoa[0].Gl_Coa_Name.ToString();
                            glPrevType = dtGlCoa[0].Gl_Coa_Type.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Prev. GL Code.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                        #endregion

                        #region Get New GL Code
                        var glNewCode = "";
                        var glNewName = "";
                        var glNewType = "";
                        var srchGlWords = txtNewCust.Text.Trim().Split(':');
                        foreach (string word in srchGlWords)
                        {
                            glNewCode = word;
                            break;
                        }

                        if (glNewCode.Length > 0)
                        {
                            dtGlCoa = taGlCoa.GetDataByCoaCode(glNewCode.ToString());
                            if (dtGlCoa.Rows.Count > 0)
                            {
                                glNewCode = dtGlCoa[0].Gl_Coa_Code.ToString();
                                glNewName = dtGlCoa[0].Gl_Coa_Name.ToString();
                                glNewType = dtGlCoa[0].Gl_Coa_Type.ToString();
                            }
                            else
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid GL Code.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid GL Code.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                        #endregion

                        if (glPrevCode.ToString() != glNewCode.ToString())
                        {
                            dtFaTe = taFaTe.GetDataByJvRefAccCode(txtTranRefNo.Text.Trim(), txtGlCode.Text.Trim());
                            if (dtFaTe.Rows.Count > 0)
                            {
                                if (glPrevType == "P")
                                {
                                    #region Credit Realization Entry
                                    //var rcvAmt = Convert.ToDecimal(dtFaTe[0].Trn_Amount) - Convert.ToDecimal(txtNewAmt.Text.Trim());
                                    var rcvAmt = Convert.ToDecimal(dtFaTe[0].Trn_Amount);
                                    var dtCrReal = taCrReal.GetRelizedChlnByCustRef(custPrevRef.ToString());
                                    foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                    {
                                        if (rcvAmt > (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt))
                                        {
                                            rcvAmt = rcvAmt - (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt);
                                            taCrReal.UpdateCreditRealizeAmt((dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt), dr.Sales_Chln_Pay_Rcv_Ref + "Rev.Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt).ToString("N2") + ", ", dr.Sales_Chln_Ref);                                            
                                        }
                                        else
                                        {
                                            taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt + rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Rev.Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + (dr.Sales_Chln_Due_Amt + rcvAmt).ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                            rcvAmt = 0;
                                            break;
                                        }
                                    }
                                    #endregion
                                }

                                if (glNewType == "P")
                                {
                                    var custNewRef = "";
                                    dtCust = taCust.GetDataByPartyAccRef(glNewCode.ToString());
                                    custNewRef = dtCust.Rows.Count > 0 ? dtCust[0].Par_Adr_Ref.ToString() : "0";

                                    #region Credit Realization Entry
                                    //var rcvAmt = Convert.ToDecimal(txtNewAmt.Text.Trim()) - Convert.ToDecimal(dtFaTe[0].Trn_Amount);
                                    var rcvAmt = Convert.ToDecimal(dtFaTe[0].Trn_Amount);
                                    var dtCrReal = taCrReal.GetPendChlnByCustRef(custNewRef.ToString());
                                    foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                    {
                                        if (rcvAmt > dr.Sales_Chln_Due_Amt)
                                        {
                                            rcvAmt = rcvAmt - dr.Sales_Chln_Due_Amt;
                                            taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Rev.Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);                                            
                                        }
                                        else
                                        {
                                            taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Rev.Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + rcvAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                            rcvAmt = 0;
                                            break;
                                        }
                                    }
                                    #endregion
                                }
                            }

                            taFaTe.UpdatePayRcvGLCode(glNewCode.ToString(), glNewName.ToString(),
                                dtFaTe[0].Trn_Narration + " [GL Code Revised (" + dtFaTeNew[0].Trn_Ac_Code.ToString() + "),(" + userCode.ToString() + ")]", 
                                txtTranRefNo.Text.Trim(), txtGlCode.Text.Trim());

                            taFaTe.UpdatePayRcvGLCodeNarration(glNewName.ToString() + " [GL Code Revised (" + dtFaTeNew[0].Trn_Ac_Code.ToString() + "),(" + userCode.ToString() + ")]",
                                txtTranRefNo.Text.Trim(), glNewCode.ToString());
                        }
                        myTran.Commit();
                    }
                }

                optUpdtType.SelectedValue = "0";
                optUpdtType.Enabled = false;
                txtNewAmt.Enabled = false;
                txtNewCust.Enabled = false;
                txtNewDate.Enabled = false;
                btnUpdt.Enabled = false;

                dtFaTe = taFaTe.GetDataByJvRef(txtTranRefNo.Text.ToString());
                gvPayDet.DataSource = dtFaTe;
                gvPayDet.DataBind();

                tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var accRef = "";
                var srchAccWords = txtNewCust.Text.Trim().Split(':');
                foreach (string word in srchAccWords)
                {
                    accRef = word;
                    break;
                }

                if (accRef.Length > 0)
                {
                    var taGl = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtGl = taGl.GetDataByCoaCode(accRef);
                    if (dtGl.Rows.Count > 0)
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex)
            {
                args.IsValid = false;
            }
        }
    }
}