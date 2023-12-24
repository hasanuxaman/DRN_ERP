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
    public partial class frmDesig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taDesigType = new tblHrmsDesigTypeTableAdapter();
            cboDesigType.DataSource = taDesigType.GetData();
            cboDesigType.DataValueField = "DesigTypeRefNo";
            cboDesigType.DataTextField = "DesigTypeName";
            cboDesigType.DataBind();

            var taDesig = new tblHrmsDesigTableAdapter();
            gvDesig.DataSource = taDesig.GetDataByAsc();
            gvDesig.DataBind();
        }

        protected void btnSaveDesig_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taDesig = new tblHrmsDesigTableAdapter();
            try
            {
                var dtDesig = taDesig.GetDataByDesigRef(Convert.ToInt32(hfDesigRefNo.Value.ToString()));
                if (dtDesig.Rows.Count > 0)
                {
                    taDesig.UpdateDesig(txtDesigCode.Text.Trim(), txtDesigName.Text.Trim(), cboDesigType.SelectedValue.ToString(), "", txtDesigRem.Text.Trim(),
                        DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "", Convert.ToInt32(hfDesigRefNo.Value));

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtDesigExist = taDesig.GetDataByCodeName(txtDesigCode.Text.Trim(), txtDesigName.Text.Trim());
                    if (dtDesigExist.Rows.Count > 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Data Exists with the same Designation Code or Name";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    else
                    {
                        var dtMaxDesigRef = taDesig.GetMaxDesigRefNo();
                        var nextDesigRef = dtMaxDesigRef == null ? 100001 : Convert.ToInt32(dtMaxDesigRef) + 1;

                        taDesig.InsertDesig(nextDesigRef, txtDesigCode.Text.Trim(), txtDesigName.Text.Trim(), cboDesigType.SelectedValue.ToString(), "", txtDesigRem.Text.Trim(),
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                hfDesigRefNo.Value = "0";
                txtDesigCode.Text = "";
                txtDesigName.Text = "";
                txtDesigRem.Text = "";
                gvDesig.DataSource = taDesig.GetDataByAsc();
                gvDesig.DataBind();
                gvDesig.SelectedIndex = -1;
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearDesig_Click(object sender, EventArgs e)
        {
            hfDesigRefNo.Value = "0";
            txtDesigCode.Text = "";
            txtDesigName.Text = "";
            txtDesigRem.Text = "";
            var taDesig = new tblHrmsDesigTableAdapter();
            gvDesig.DataSource = taDesig.GetDataByAsc();
            gvDesig.DataBind();
            gvDesig.SelectedIndex = -1;
        }

        protected void gvDesig_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvDesig.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    hfDesigRefNo.Value = gvDesig.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDesig.Rows[indx].Cells[0].Text.Trim();
                    txtDesigCode.Text = gvDesig.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDesig.Rows[indx].Cells[1].Text.Trim();
                    txtDesigName.Text = gvDesig.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDesig.Rows[indx].Cells[2].Text.Trim();
                    cboDesigType.SelectedValue = gvDesig.Rows[indx].Cells[3].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDesig.Rows[indx].Cells[3].Text.Trim();
                    txtDesigRem.Text = gvDesig.Rows[indx].Cells[4].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDesig.Rows[indx].Cells[4].Text.Trim();
                }
                catch (Exception ex)
                {
                    hfDesigRefNo.Value = "0";
                }
            }
        }

        protected void gvDesig_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var desigRef = gvDesig.Rows[rowNum].Cells[0].Text.Trim() == "&nbsp;"
                                          ? "0"
                                          : gvDesig.Rows[rowNum].Cells[0].Text.Trim();

            var taEmpOff = new tblHrmsEmpOfficeTableAdapter();
            var dtEmpOff = taEmpOff.GetDataByDesigRef(desigRef.ToString());
            if (dtEmpOff.Rows.Count > 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to delete this Designation.";
                tblMsg.Rows[1].Cells[0].InnerText = "Designation already used.";
                ModalPopupExtenderMsg.Show();
                return;
            }
            else
            {
                var taDesig = new tblHrmsDesigTableAdapter();
                var dtDesig = taDesig.GetDataByDesigRef(Convert.ToInt32(desigRef));
                if (dtDesig.Rows.Count > 0)
                {
                    taDesig.DeleteDesig(Convert.ToInt32(desigRef));
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }

                gvDesig.DataSource = taDesig.GetDataByAsc();
                gvDesig.DataBind();
                gvDesig.SelectedIndex = -1;
            }
        }
    }
}