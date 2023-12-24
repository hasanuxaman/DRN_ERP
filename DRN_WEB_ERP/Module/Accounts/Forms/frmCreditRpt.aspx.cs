using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmCreditRpt : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDt.Text = "01/01/2014";
            txtToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnShowCollectRpt_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowAccReport.aspx";
            //Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
        {
            if (txtFromDt.Text == "" || txtToDt.Text == "") return;

            var custRef = "";
            if (txtSearch.Text.Trim().Length > 0)
            {
                if (txtSearch.Text.Trim().Length <= 0) return;

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
                        if (dtPartyAdr.Rows.Count > 0) custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                    }
                }
            }

            if (custRef == "")
            {
                rptSelcFormula = "{View_Credit_Report.Sales_Chln_Date} in Date('" + txtFromDt.Text.Trim() + "') to Date ('" + txtToDt.Text.Trim() + "')";
            }
            else
            {
                rptSelcFormula = "{View_Credit_Report.Sales_Chln_Date} in Date('" + txtFromDt.Text.Trim() + "') to Date ('" + txtToDt.Text.Trim() + "') " +
                    "and {View_Credit_Report.Sales_Par_Adr_Ref}='" + custRef + "'";
            }

            if (optRpt.SelectedValue == "1")
                rptFile = "~/Module/Accounts/Reports/rptCreditDet.rpt";
            else
                rptFile = "~/Module/Accounts/Reports/rptCreditSum.rpt";

            Session["RptDateFrom"] = txtFromDt.Text.Trim();
            Session["RptDateTo"] = txtToDt.Text.Trim();
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (txtSearch.Text.Trim().Length <= 0) args.IsValid = true;

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
    }
}