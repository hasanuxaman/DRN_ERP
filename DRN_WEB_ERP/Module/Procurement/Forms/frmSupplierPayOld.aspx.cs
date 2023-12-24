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
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmSupplierPayOld : System.Web.UI.Page
    {
        GlobalClass.clsNumToText NumToText = new GlobalClass.clsNumToText();

        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";

            if (!Page.IsPostBack)
            {
                txtPayDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                tbl_advance.Visible = false;
                load_pending_list();
                load_bank();
            }
        }

        private void load_bank()
        {
            tbl_Acc_Fa_Gl_CoaTableAdapter bank = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable dtbank = new dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable();
            ListItem lst;

            //dtbank = bank.GetAllBank();
            dtbank = bank.GetDataByCoaType("B");
            ddlbank.Items.Clear();
            ddlbank.Items.Add("");
            foreach (dsAccMas.tbl_Acc_Fa_Gl_CoaRow dr in dtbank.Rows)
            {
                lst = new ListItem();
                //lst.Value = dr.Bnk_info_Bnk_code;
                lst.Value = dr.Gl_Coa_Code;
                //lst.Text = dr.Bnk_info_Bnk_code + ":" + dr.Bnk_info_Bnk_Name;
                lst.Text = dr.Gl_Coa_Code + ":" + dr.Gl_Coa_Name;
                ddlbank.Items.Add(lst);
            }
        }

        private void load_pending_list()
        {
            var taPendList = new View_Due_Supp_ListTableAdapter();
            var dtPendList = taPendList.GetDataByAsc();

            ListItem lst;

            ddllist.Items.Clear();
            ddllist.Items.Add("");

            foreach (dsProcTran.View_Due_Supp_ListRow dr in dtPendList.Rows)
            {
                lst = new ListItem();
                lst.Value = dr.PO_Hdr_Acode.ToString();
                lst.Text = dr.PO_Hdr_Acode.ToString() + ":" + dr.Gl_Coa_Name.ToString();
                ddllist.Items.Add(lst);
            }

            lblcount.Text = "(" + dtPendList.Rows.Count.ToString() + ")";

            tbl_advance.Visible = false;
        }

        private void generate_detail_data(string ref_no)
        {
            var taPendPoList = new VIEW_PO_MRR_ACCTableAdapter();
            var dtPendPoList = taPendPoList.GetDataBySupAccCode(ddllist.SelectedValue.ToString());

            decimal totamnt = 0;

            foreach (dsProcTran.VIEW_PO_MRR_ACCRow dr in dtPendPoList.Rows)
            {
                totamnt = totamnt + dr.Bal_Amt;
            }

            lbltot.Text = totamnt.ToString("N2") + " [ " + NumToText.changeCurrencyToWords(totamnt.ToString()) + " ]";
            gdItem.DataSource = dtPendPoList;
            gdItem.DataBind();

            //txtvamnt.Text = totamnt.ToString("N2");
        }

        protected void ddllist_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data();
        }

        private void load_data()
        {
            System.Threading.Thread.Sleep(1000);
            string selitem = ddllist.SelectedItem.Value.ToString();

            if (selitem == "")
            {
                tbl_advance.Visible = false;
            }
            else
            {
                tbl_advance.Visible = true;
                generate_detail_data(selitem);
            }
        }

        protected void btnreload_Click(object sender, EventArgs e)
        {
            load_data();
        }

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            string uid = Session["sessionUserId"].ToString();
            string uname = Session["sessionUserName"].ToString();           

            #region Form Data Validation
            if (ddlbank.SelectedValue.ToString() == "")
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Select Bank Account First.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }

            if (txtvamnt.Text.Trim().Length <= 0 || Convert.ToDouble(txtvamnt.Text.Trim()) <= 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Enter Voucher Amount First.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
            #endregion

            #region Get GL Details
            var suppName = "";
            var bankName = "";
            tbl_Acc_Fa_Gl_CoaTableAdapter Gl = new tbl_Acc_Fa_Gl_CoaTableAdapter();

            var dtGlSup = Gl.GetDataByCoaCode(ddllist.SelectedValue.ToString());
            suppName = dtGlSup.Rows.Count > 0 ? dtGlSup[0].Gl_Coa_Name : "";
            
            var dtGlBnk = Gl.GetDataByCoaCode(ddlbank.SelectedValue.ToString());
            bankName = dtGlBnk.Rows.Count > 0 ? dtGlBnk[0].Gl_Coa_Name : "";
            #endregion

            //string narration = lblpaytype.Text.ToLower() + " for LPO # " + "ref_no" + " to " + ddllist.SelectedItem.Text.Split(':')[1] + ", Bank " + ddlbank.SelectedItem.Text.Split(':')[1];

            tbl_Acc_Fa_TeTableAdapter FaTe = new tbl_Acc_Fa_TeTableAdapter();

            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(FaTe.Connection);

            try
            {
                FaTe.AttachTransaction(myTrn);

                foreach (GridViewRow gr in gdItem.Rows)
                {
                    var payAmt = ((TextBox)gr.FindControl("txtPayAmt")).Text;
                    var poRefNo = ((Label)gr.FindControl("lblPoRefNo")).Text;

                    var jvType = "";
                    if (ddlPayMode.SelectedItem.ToString() == "Cheque")
                        jvType = "BPV";
                    else
                        jvType = "CPV";

                    if (payAmt.Length > 0)
                    {
                        #region Insert Accounts Voucher Entry

                        var dtMaxAccRef = FaTe.GetMaxRefNo(jvType, Convert.ToDateTime(txtPayDate.Text.Trim()).Year);
                        var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                        var nextAccRefNo = jvType + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                        //Debit-Supplier Account
                        FaTe.InsertAccData(ddllist.SelectedValue.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            nextAccRefNo.ToString(), 1, 1, bankName.ToString(), "D", Convert.ToDecimal(payAmt.ToString()),
                            ddlPayMode.SelectedItem.ToString(), txtChqNo.Text.Trim(), "BDT", 1, Convert.ToDecimal(payAmt.ToString()),
                            "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtPayDate.Text.Trim()), suppName.ToString(),
                            Convert.ToDateTime(txtPayDate.Text.Trim()), "ADM", "S", "",
                            DateTime.Now, jvType, "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(payAmt.ToString()), "",
                            txtBillNo.Text.Trim(), poRefNo.ToString(), "N", 1, 0, "", "", "", "J", 0, "1", jvType);

                        //Credit-Bank Account
                        FaTe.InsertAccData(ddlbank.SelectedValue.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            nextAccRefNo.ToString(), 2, 1, suppName.ToString(), "C", Convert.ToDecimal(payAmt.ToString()),
                            ddlPayMode.SelectedItem.ToString(), txtChqNo.Text.Trim(), "BDT", 1, Convert.ToDecimal(payAmt.ToString()),
                            "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtPayDate.Text.Trim()), bankName.ToString(),
                            Convert.ToDateTime(txtPayDate.Text.Trim()), "ADM", "B", "",
                            DateTime.Now, jvType, "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(payAmt.ToString()), "",
                            txtBillNo.Text.Trim(), poRefNo.ToString(), "N", 1, 0, "", "", "", "J", 0, "1", jvType);

                        #endregion
                    }
                }
                myTrn.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Payment Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                load_data();
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

            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}