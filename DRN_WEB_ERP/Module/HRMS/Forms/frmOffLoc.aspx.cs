using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmOffLoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taLoc = new tblHrmsOffLocTableAdapter();
            gvLoc.DataSource = taLoc.GetDataByAsc();
            gvLoc.DataBind();
        }

        protected void btnSaveCom_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            
            DateTime? locOpnDt = null;
            if (txtOpnDate.Text.Length > 0) locOpnDt = Convert.ToDateTime(txtOpnDate.Text.Trim());

            var taLoc = new tblHrmsOffLocTableAdapter();
            try
            {
                var dtLoc = taLoc.GetDataByLocRef(Convert.ToInt32(hfLocRefNo.Value.ToString()));
                if (dtLoc.Rows.Count > 0)
                {
                    taLoc.UpdateLoc(txtLocCode.Text.Trim(), txtLocName.Text.Trim(), "100001", txtLocAddr.Text.Trim(), txtLocPhone.Text.Trim(), txtLocFax.Text.Trim(),
                        txtLocEmail.Text.Trim(), "", "", txtLocRem.Text.Trim(), locOpnDt, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "", Convert.ToInt32(hfLocRefNo.Value));

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtMaxLocRef = taLoc.GetMaxLocRefNo();
                    var nextLocRef = dtMaxLocRef == null ? 100001 : Convert.ToInt32(dtMaxLocRef) + 1;
                    taLoc.InsertLoc(nextLocRef, txtLocCode.Text.Trim(), txtLocName.Text.Trim(), "100001", txtLocAddr.Text.Trim(), txtLocPhone.Text.Trim(),
                        txtLocFax.Text.Trim(), txtLocEmail.Text.Trim(), "", "", txtLocRem.Text.Trim(), locOpnDt, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "");

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                hfLocRefNo.Value = "0";
                txtLocCode.Text = "";
                txtLocName.Text = "";
                txtLocAddr.Text = "";
                txtLocPhone.Text = "";
                txtLocFax.Text = "";
                txtLocFax.Text = "";
                txtLocEmail.Text = "";
                txtOpnDate.Text = "";
                txtLocRem.Text = "";
                gvLoc.DataSource = taLoc.GetDataByAsc();
                gvLoc.DataBind();
                gvLoc.SelectedIndex = -1;
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearComp_Click(object sender, EventArgs e)
        {
            hfLocRefNo.Value = "0";
            txtLocCode.Text = "";
            txtLocName.Text = "";
            txtLocAddr.Text = "";
            txtLocPhone.Text = "";
            txtLocFax.Text = "";
            txtLocFax.Text = "";
            txtLocEmail.Text = "";
            txtOpnDate.Text = "";
            txtLocRem.Text = "";
            var taLoc = new tblHrmsOffLocTableAdapter();
            gvLoc.DataSource = taLoc.GetDataByAsc();
            gvLoc.DataBind();
            gvLoc.SelectedIndex = -1;
        }

        protected void gvLoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvLoc.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    hfLocRefNo.Value = gvLoc.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLoc.Rows[indx].Cells[0].Text.Trim();
                    txtLocCode.Text = gvLoc.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLoc.Rows[indx].Cells[1].Text.Trim();
                    txtLocName.Text = gvLoc.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLoc.Rows[indx].Cells[2].Text.Trim();
                    txtLocAddr.Text = gvLoc.Rows[indx].Cells[3].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLoc.Rows[indx].Cells[3].Text.Trim();
                    txtLocPhone.Text = gvLoc.Rows[indx].Cells[4].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLoc.Rows[indx].Cells[4].Text.Trim();
                    txtLocFax.Text = gvLoc.Rows[indx].Cells[5].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLoc.Rows[indx].Cells[5].Text.Trim();
                    txtLocEmail.Text = gvLoc.Rows[indx].Cells[6].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLoc.Rows[indx].Cells[6].Text.Trim();
                    txtOpnDate.Text = gvLoc.Rows[indx].Cells[7].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLoc.Rows[indx].Cells[7].Text.Trim();
                    txtLocRem.Text = gvLoc.Rows[indx].Cells[8].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLoc.Rows[indx].Cells[8].Text.Trim();
                }
                catch (Exception ex)
                {
                    hfLocRefNo.Value = "0";
                }
            }
        }

        protected void gvLoc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var locRef = gvLoc.Rows[rowNum].Cells[0].Text.Trim() == "&nbsp;"
                                          ? "0"
                                          : gvLoc.Rows[rowNum].Cells[0].Text.Trim();

            var taEmpOffc = new tblHrmsEmpOfficeTableAdapter();
            var dtEmpOffc = taEmpOffc.GetDataByLocRef(locRef);
            if (dtEmpOffc.Rows.Count > 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to delete this Location.";
                tblMsg.Rows[1].Cells[0].InnerText = "Location already used.";
                ModalPopupExtenderMsg.Show();
                return;
            }
            else
            {
                var taLoc = new tblHrmsOffLocTableAdapter();
                var dtLoc = taLoc.GetDataByLocRef(Convert.ToInt32(locRef));
                if (dtLoc.Rows.Count > 0)
                {
                    taLoc.DeleteLoc(Convert.ToInt32(locRef));
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }

                gvLoc.DataSource = taLoc.GetDataByAsc();
                gvLoc.DataBind();
                gvLoc.SelectedIndex = -1;
            }
        }
    }
}