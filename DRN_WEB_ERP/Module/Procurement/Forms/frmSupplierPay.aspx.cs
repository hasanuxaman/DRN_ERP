using System;
using System.Data;
using System.Globalization;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmSupplierPay : System.Web.UI.Page
    {
        GlobalClass.clsNumToText NumToText = new GlobalClass.clsNumToText();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtPayDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                tbl_pay.Visible = false;
            }
        }

        private void generate_detail_data(string ref_no)
        {
            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
            var dtFaTeCr = taFaTe.GetTotCrAmt(ref_no.ToString());
            var totCrAmt = dtFaTeCr == null ? 0 : Convert.ToDouble(dtFaTeCr);
            var dtFaTeDr = taFaTe.GetTotDrAmt(ref_no.ToString());
            var totDrAmt = dtFaTeDr == null ? 0 : Convert.ToDouble(dtFaTeDr);
            var partyBalance = totCrAmt - totDrAmt;
            if (partyBalance >= 0)
            {
                lblPartyBal.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0033CC");
                lblPartyBal.Text = "Balance: " + partyBalance.ToString("F2");
            }
            else
            {
                lblPartyBal.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
                lblPartyBal.Text = "Balance: " + partyBalance.ToString("F2") + " (Dr)";
            }

            var taPendBillList = new tbl_PuTr_Bill_HdrTableAdapter();
            var dtPendBillList = taPendBillList.GetDueBillByParty(ref_no.ToString());

            decimal totamnt = 0;

            foreach (dsProcTran.tbl_PuTr_Bill_HdrRow dr in dtPendBillList.Rows)
            {
                totamnt = totamnt + dr.Bill_Hdr_Due_Amount;
            }

            lbltot.Text = "Total Due Amount: " + totamnt.ToString("N2") + " [ " + NumToText.changeNumericToWords(totamnt.ToString()) + " ]";
            gdDueBillList.DataSource = dtPendBillList;
            gdDueBillList.DataBind();
        }

        protected void btnUpdtPayment_Click(object sender, EventArgs e)
        {
            tbl_Acc_Fa_TeTableAdapter FaTe = new tbl_Acc_Fa_TeTableAdapter();

            tbl_PuTr_Bill_HdrTableAdapter taBill = new tbl_PuTr_Bill_HdrTableAdapter();

            tbl_PuTr_PO_HdrTableAdapter taPoHdr=new tbl_PuTr_PO_HdrTableAdapter();

            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(FaTe.Connection);

            try
            {
                FaTe.AttachTransaction(myTrn);
                taBill.AttachTransaction(myTrn);

                string uid = Session["sessionUserId"].ToString();
                string uname = Session["sessionUserName"].ToString();

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

                #region Form Data Validation
                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dtFaTeCr = taFaTe.GetTotCrAmt(partyAcc.ToString());
                var totCrAmt = dtFaTeCr == null ? 0 : Convert.ToDouble(dtFaTeCr);
                var dtFaTeDr = taFaTe.GetTotDrAmt(partyAcc.ToString());
                var totDrAmt = dtFaTeDr == null ? 0 : Convert.ToDouble(dtFaTeDr);
                var partyBalance = totCrAmt - totDrAmt;

                if (Convert.ToDouble(txtvamnt.Text.Trim()) <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter Pay Ammount First.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                if (Convert.ToDouble(txtvamnt.Text.Trim()) > Convert.ToDouble(partyBalance))
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to pay more than: " + partyBalance;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                if (txtCrAcc.Text.Trim() == "")
                {
                    if (ddlPayMode.SelectedItem.ToString() == "Cheque")
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Enter Bank Account First.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Enter Cr Account First.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (txtvamnt.Text.Trim().Length <= 0 || Convert.ToDouble(txtvamnt.Text.Trim()) <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter Voucher Amount First.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #region Get Bank Account Details
                var bankAcc = "";
                var bankName = "";
                var bankAccType = "";
                var srchAccWords = txtCrAcc.Text.Trim().Split(':');
                foreach (string word in srchAccWords)
                {
                    bankAcc = word;
                    break;
                }

                if (bankAcc.Length > 0)
                {
                    var taGl = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtGl = taGl.GetDataByCoaCode(bankAcc);
                    if (dtGl.Rows.Count > 0)
                    {
                        bankAcc = dtGl[0].Gl_Coa_Code.ToString();
                        bankName = dtGl[0].Gl_Coa_Name.ToString();
                        bankAccType = dtGl[0].Gl_Coa_Type.ToString();
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Bank Account.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Bank Account.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion          

                var jvType = "";
                if (ddlPayMode.SelectedItem.ToString() == "Cheque")
                    jvType = "BPV";
                else
                    jvType = "CPV";

                var dtMaxAccRef = FaTe.GetMaxRefNo(jvType.ToString(), Convert.ToDateTime(txtPayDate.Text.Trim()).Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextAccRefNo = jvType.ToString() + (Convert.ToDateTime(txtPayDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtPayDate.Text.Trim()).ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                var i = 1;
                foreach (GridViewRow gr in gdDueBillList.Rows)
                {
                    var hfBillRef = ((HiddenField)gr.FindControl("hfBillRef")).Value;
                    var lblBillNo = ((Label)gr.FindControl("lblBillNo")).Text;
                    var txtPayAmt = ((TextBox)gr.FindControl("txtPayAmt")).Text;
                    var tvPoList = ((TreeView)gr.FindControl("tvPoList"));

                    var payAmt = txtPayAmt.Length > 0 ? Convert.ToDecimal(txtPayAmt) : 0;

                    var poRefNo = "";
                    foreach (TreeNode tn in tvPoList.Nodes)
                    {
                        if (tn.ChildNodes.Count > 0)
                        {
                            foreach (TreeNode tnc in tn.ChildNodes)
                            {
                                if (tnc.Checked) poRefNo = tnc.Value;
                            }
                        }
                        else
                        {
                            if (tn.Checked) poRefNo = tn.Value;
                        }
                    }

                    var dtPoDet = taPoHdr.GetDataByHdrRef(poRefNo.ToString());
                    if (dtPoDet.Rows.Count > 0)
                    {
                        if (payAmt > 0)
                        {
                            #region Insert Accounts Voucher Entry
                            //Debit-Supplier Account
                            FaTe.InsertAccData(partyAcc.ToString(), (Convert.ToDateTime(txtPayDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtPayDate.Text.Trim()).Year.ToString()).ToString(),
                                nextAccRefNo.ToString(), i, 1, bankName.ToString(), "D", Convert.ToDecimal(payAmt.ToString()),
                                ddlPayMode.SelectedItem.ToString(), txtChqNo.Text.Trim(), "BDT", 1, Convert.ToDecimal(payAmt.ToString()),
                                "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtPayDate.Text.Trim()),
                                partyName.ToString(), Convert.ToDateTime(txtPayDate.Text.Trim()), "ADM", "S", "",
                                Convert.ToDateTime(txtPayDate.Text.Trim()), jvType, "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(payAmt.ToString()), "",
                                lblBillNo.ToString(), poRefNo.ToString(), "N", 1, 0, "", "", "", "J", 0, "1", jvType);

                            //Credit-Given Account
                            FaTe.InsertAccData(bankAcc.ToString(), (Convert.ToDateTime(txtPayDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtPayDate.Text.Trim()).Year.ToString()).ToString(),
                                nextAccRefNo.ToString(), i + 1, 1, partyName.ToString(), "C", Convert.ToDecimal(payAmt.ToString()),
                                ddlPayMode.SelectedItem.ToString(), txtChqNo.Text.Trim(), "BDT", 1, Convert.ToDecimal(payAmt.ToString()),
                                "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtPayDate.Text.Trim()), bankName.ToString(),
                                Convert.ToDateTime(txtPayDate.Text.Trim()), "ADM", bankAccType, "",
                                Convert.ToDateTime(txtPayDate.Text.Trim()), jvType, "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(payAmt.ToString()), "",
                                lblBillNo.ToString(), poRefNo.ToString(), "N", 1, 0, "", "", "", "J", 0, "1", jvType);
                            #endregion

                            var dtBill = taBill.GetDataByBillRef(Convert.ToInt32(hfBillRef));
                            if (dtBill.Rows.Count > 0)
                            {
                                taBill.UpdateBillAmt(dtBill[0].Bill_Hdr_Pay_Amount + payAmt, dtBill[0].Bill_Hdr_Due_Amount - payAmt, Convert.ToInt32(hfBillRef));
                            }
                            else
                            {
                                myTrn.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Bill Data Not Found.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }
                    }
                    else
                    {
                        myTrn.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "PO Data Not Found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                    i++;
                }

                txtSearchSupplier.Text = "";
                lblPartyBal.Text = "";
                lblPartyBal.Visible = false;
                lbltot.Text = "";
                lbltot.Visible = false;
                txtPayDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlPayMode.SelectedIndex = 0;
                txtCrAcc.Text = "";
                txtChqNo.Text = "";
                txtvamnt.Text = "";
                tbl_pay.Visible = false;
                generate_detail_data("");

                myTrn.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Payment Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();                
            }
            catch (Exception ex)
            {
                myTrn.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            finally
            {
                GlobalClass.clsDbHelper.CloseTransaction(FaTe.Connection, myTrn);
            }
            //Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void gdDueBillList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var taPoList = new View_Bill_Wise_Distinct_PoTableAdapter();
            var taMrrList = new View_Bill_Wise_Distinct_MrrTableAdapter();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hfBillRef = ((HiddenField)e.Row.FindControl("hfBillRef"));
                var lblBillMrrNo = ((Label)e.Row.FindControl("lblBillMrrNo"));
                var tvPoList = ((TreeView)e.Row.FindControl("tvPoList"));
                var tvMrrList = ((TreeView)e.Row.FindControl("tvMrrList"));

                var dtPoList = taPoList.GetData(hfBillRef.Value);               
                TreeNode tnPoMain = new TreeNode();
                tnPoMain.Text = "Total PO: (" + dtPoList.Rows.Count + ")";
                tnPoMain.Value = "PO No";
                tvPoList.Nodes.Add(tnPoMain);              
                foreach (dsProcTran.View_Bill_Wise_Distinct_PoRow dr in dtPoList.Rows)
                {
                    TreeNode tnPo = new TreeNode();
                    tnPo.Text = dr.Bill_Det_Po_No.ToString();
                    tnPo.Value = dr.Bill_Det_Po_No.ToString();
                    tnPo.ShowCheckBox = true;
                    tnPo.Checked = true;
                    tnPoMain.ChildNodes.Add(tnPo);
                }

                var dtMrrList = taMrrList.GetData(hfBillRef.Value); 
                TreeNode tnMrrMain = new TreeNode();
                tnMrrMain.Text = "Total MRR: (" + dtMrrList.Rows.Count + ")";
                tnMrrMain.Value = "MRR No";
                tvMrrList.Nodes.Add(tnMrrMain);
                foreach (dsProcTran.View_Bill_Wise_Distinct_MrrRow dr in dtMrrList.Rows)
                {
                    TreeNode tnMrr = new TreeNode();
                    tnMrr.Text = dr.Bill_Det_Mrr_No.ToString();
                    tnMrr.Value = dr.Bill_Det_Mrr_No.ToString();
                    tnMrrMain.ChildNodes.Add(tnMrr);
                }
                //tvPoList.ExpandAll();      
                //tvMrrList.ExpandAll();   
            }
        }

        protected void btnHoldBill_Click(object sender, EventArgs e)
        {
            var taBillHdr = new tbl_PuTr_Bill_HdrTableAdapter();
            try
            {                
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfBillRef = (HiddenField)(row.FindControl("hfBillRef"));
                var lblBillNo = (Label)(row.FindControl("lblBillNo"));                
                var txtPayAmt = (TextBox)(row.FindControl("txtPayAmt"));
                var btnHoldBill = (Button)(row.FindControl("btnHoldBill"));

                var dtBillHdr = taBillHdr.GetDataByBillRef(Convert.ToInt32(hfBillRef.Value.ToString()));
                if (dtBillHdr.Rows.Count > 0)
                {
                    if (dtBillHdr[0].Bill_Hdr_Flag == "A")
                    {
                        taBillHdr.UpdateBillFlag("I", Convert.ToInt32(hfBillRef.Value.ToString()));                                                
                        txtPayAmt.Enabled = false;
                        btnHoldBill.Text = "Unhold";
                    }
                    else
                    {
                        taBillHdr.UpdateBillFlag("A", Convert.ToInt32(hfBillRef.Value.ToString()));                        
                        txtPayAmt.Text = "";
                        txtPayAmt.Enabled = true;
                        btnHoldBill.Text = "Hold";
                    }
                    
                    txtPayAmt.Text = "";
                    double totPay = 0;
                    foreach (GridViewRow gr in gdDueBillList.Rows)
                    {
                        double payAmt = 0;                        
                        var lblTotal = (Label)(gr.FindControl("lblTotal"));
                        var grPayAmt = ((TextBox)(gr.FindControl("txtPayAmt"))).Text.Trim();
                        if (grPayAmt.Length > 0) payAmt = Convert.ToDouble(grPayAmt);
                        totPay = totPay + payAmt;
                        lblTotal.Text = payAmt.ToString();
                    }
                    txtvamnt.Text = totPay.ToString();
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();

            if (txtSearchSupplier.Text.Trim().Length <= 0) return;

            try
            {
                var supRef = "";
                var supAcc = "";
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
                        supAcc = dtSupAdr[0].Par_Adr_Acc_Code.ToString();
                        if (supAcc == "")
                        {
                            lblPartyBal.Text = "";
                            lblPartyBal.Visible = false;
                            lbltot.Text = "";
                            lbltot.Visible = false;
                            txtPayDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            ddlPayMode.SelectedIndex = 0;
                            txtCrAcc.Text = "";
                            txtChqNo.Text = "";
                            txtvamnt.Text = "";
                            tbl_pay.Visible = false;
                            generate_detail_data(supAcc);
                        }
                        else
                        {
                            tbl_pay.Visible = true;
                            generate_detail_data(supAcc);
                        }
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

        protected void btnClear_Click(object sender, EventArgs e)
        {            
            txtSearchSupplier.Text = "";
            lblPartyBal.Text = "";
            lblPartyBal.Visible = false;
            lbltot.Text = "";
            lbltot.Visible = false;
            txtPayDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlPayMode.SelectedIndex = 0;
            txtCrAcc.Text = "";
            txtChqNo.Text = "";
            txtvamnt.Text = "";
            tbl_pay.Visible = false;
            generate_detail_data("");
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var accRef = "";
                var srchAccWords = txtCrAcc.Text.Trim().Split(':');
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

        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPayMode.SelectedItem.ToString() == "Cheque")
            {
                lblPayType.Text = "BANK";
                AutoCompleteExtender1.ServiceMethod = "GetSrchCoaByPayBank";
            }
            else
            {
                lblPayType.Text = "CR ACCOUNT";
                AutoCompleteExtender1.ServiceMethod = "GetSrchCoa";
            }
        }
    }
}