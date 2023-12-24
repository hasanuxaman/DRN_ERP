using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.IO.DataSet.dsIOTableAdapters;

namespace DRN_WEB_ERP.Module.IO.Forms
{
    public partial class frmIOLimit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                AutoCompleteExtenderEmp.ContextKey = "0";

                var taIoLimit = new tblAccIoLimitTableAdapter();
                gvIoLimit.DataSource = taIoLimit.GetData();
                gvIoLimit.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var taEmp = new tblHrmsEmpTableAdapter();
            var empRef = "";
            var srchWords2 = txtSrchEmp.Text.Trim().Split(':');
            foreach (string word in srchWords2)
            {
                empRef = word;
                break;
            }

            if (empRef.Length > 0)
            {
                int result;
                if (int.TryParse(empRef, out result))
                {
                    var dtPayCode = taEmp.GetDataByEmpRef(empRef.ToString());
                    if (dtPayCode.Rows.Count > 0)
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

        protected void chkIncTermEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncTermEmp.Checked)
                AutoCompleteExtenderEmp.ContextKey = "1";
            else
                AutoCompleteExtenderEmp.ContextKey = "0";
        }

        protected void gvIoLimit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);

                System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("AttachImage");
                var empRefNo = ((HiddenField)e.Row.FindControl("hfEmpRef")).Value;
                UsrImages.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRefNo + "'";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taEmp = new View_Emp_BascTableAdapter();
            var taIoLimit = new tblAccIoLimitTableAdapter();
            var taIoReq = new tblAccIoReqTableAdapter();
            try
            {
                var empRef = "";
                var srchWords2 = txtSrchEmp.Text.Trim().Split(':');
                foreach (string word in srchWords2)
                {
                    empRef = word;
                    break;
                }

                if (empRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(empRef, out result))
                    {
                        var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                        if (dtEmp.Rows.Count > 0)
                        {
                            var dtIo=taIoLimit.GetDataByEmpRef(empRef.ToString());
                            if (dtIo.Rows.Count > 0)
                            {
                                taIoLimit.UpdateIoLimit(Convert.ToDecimal(txtLimitAmt.Text.Trim()), "", "", "", "", "", DateTime.Now,
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "0", "", empRef.ToString());

                                tblMsg.Rows[0].Cells[0].InnerText = "Employee I/O Limit Updated Successfully.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();                                
                            }
                            else
                            {
                                taIoLimit.InsertIoLimit(empRef.ToString(), Convert.ToDecimal(txtLimitAmt.Text.Trim()), "", "", "", "", "", DateTime.Now,
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "0", "");

                                tblMsg.Rows[0].Cells[0].InnerText = "Employee I/O Limit Inserted Successfully.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                            }

                            gvIoLimit.DataSource = taIoLimit.GetData();
                            gvIoLimit.DataBind();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex) { }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSrchEmp.Text = "";
            txtLimitAmt.Text = "";
            gvIoLimit.SelectedIndex = -1;
        }

        #region GridData
        public string GetEmpName(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new tblHrmsEmpTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].EmpFirstName.ToString() + " " + dtEmp[0].EmpLastName.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetEmpId(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].EmpId.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetEmpDesig(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].DesigName.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetEmpDept(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].DeptName.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }
        #endregion

        protected void gvIoLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvIoLimit.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    var empRef = ((HiddenField)gvIoLimit.Rows[indx].FindControl("hfEmpRef")).Value.ToString();

                    var taEmp = new View_Emp_BascTableAdapter();
                    var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt32(empRef.ToString()));
                    if (dtEmp.Rows.Count > 0)
                        txtSrchEmp.Text = dtEmp[0].EmpRefNo.ToString() + ":" + dtEmp[0].EmpId.ToString() + ":" + dtEmp[0].EmpName.ToString();
                    else
                        txtSrchEmp.Text = "";

                    txtLimitAmt.Text = gvIoLimit.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvIoLimit.Rows[indx].Cells[2].Text.Trim();
                }
                catch (Exception ex) { }                
            }
        }
    }
}