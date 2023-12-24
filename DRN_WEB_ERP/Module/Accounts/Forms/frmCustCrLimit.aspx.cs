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

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmCustCrLimit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
            var taCrLimit = new tblSalesPartyCrLimitTableAdapter();

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
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef.ToString()));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            txtCrLimit.Text = dtPartyAdr[0].Par_Adr_Cr_Limit.ToString("N2");

                            var dtCrLimit = taCrLimit.GetDataByCustRef(custRef.ToString());
                            gvCustCrLimit.DataSource = dtCrLimit;
                            gvCustCrLimit.DataBind();

                            txtSearch.Enabled = false;
                            btnClearSrch.Visible = true;
                            txtNewCrLimit.Enabled = true;
                            btnUpdate.Enabled = true;
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer";
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

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtSearch.Enabled = true;
            btnClearSrch.Visible = false;

            txtCrLimit.Text = "";
            txtNewCrLimit.Text = "";
            txtNewCrLimit.Enabled = false;
            btnUpdate.Enabled = false;

            var taCrLimit = new tblSalesPartyCrLimitTableAdapter();
            var dtCrLimit = taCrLimit.GetDataByCustRef("");
            gvCustCrLimit.DataSource = dtCrLimit;
            gvCustCrLimit.DataBind();
            gvCustCrLimit.Visible = dtCrLimit.Rows.Count > 0;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
            var taCrLimit = new tblSalesPartyCrLimitTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPartyAdr.Connection);

            try
            {
                taPartyAdr.AttachTransaction(myTran);
                taCrLimit.AttachTransaction(myTran);

                #region Get Cust
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
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef.ToString()));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                #endregion
                
                taPartyAdr.UpdatePartyCreditLimit(Convert.ToDecimal(txtCrLimit.Text.Trim()), Convert.ToDecimal(txtNewCrLimit.Text.Trim()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), Convert.ToInt32(custRef.ToString()));

                var dtNextLineNo = taCrLimit.GetMaxLineNo(custRef.ToString());
                var nextLineNo = dtNextLineNo == null ? 1 : Convert.ToInt32(dtNextLineNo) + 1;

                taCrLimit.InsertCreditLimit(custRef.ToString(), nextLineNo, Convert.ToDecimal(txtCrLimit.Text.Trim()), Convert.ToDecimal(txtNewCrLimit.Text.Trim()),
                    DateTime.Now, Session["sessionUserName"] == null ? "0" : Session["sessionUserName"].ToString(), "", "", "", "", "",
                    DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Credit Limit Updated Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                txtNewCrLimit.Text = "";

                var dtPartyCrLimit = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef.ToString()));
                if (dtPartyCrLimit.Rows.Count > 0)                
                    txtCrLimit.Text = dtPartyCrLimit[0].Par_Adr_Cr_Limit.ToString("N2"); 
               
                var dtCrLimit = taCrLimit.GetDataByCustRef(custRef.ToString());
                gvCustCrLimit.DataSource = dtCrLimit;
                gvCustCrLimit.DataBind();
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