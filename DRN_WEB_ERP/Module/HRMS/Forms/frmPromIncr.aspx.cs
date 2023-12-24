using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmPromIncr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderEmp.ContextKey = "0";

            txtEffectDt.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taDesig = new tblHrmsDesigTableAdapter();
            cboDesig.DataSource = taDesig.GetDataByAsc();
            cboDesig.DataValueField = "DesigRefNo";
            cboDesig.DataTextField = "DesigName";
            cboDesig.DataBind();
            cboDesig.Items.Insert(0, new ListItem("---Select---", "0"));


            var taDept = new tblHrmsDeptTableAdapter();
            var dtDept = taDept.GetDataByAsc();
            Session["data"] = dtDept;
            SetDepGridData();

            txtIncrAmt.Attributes.Add("onkeyup", "CalcIncrPrec('" + txtIncrAmt.ClientID + "', '" + lblPresGrossAmt.ClientID + "', '" + lblIncrPrec.ClientID + "' )");
        }

        #region Get_Grid_Data
        public string GetCompanyName(string companyRef)
        {
            string companyName = "";
            try
            {
                var taCompany = new tblHrmsOffLocTableAdapter();
                var dtCompany = taCompany.GetDataByLocRef(Convert.ToInt32(companyRef));
                if (dtCompany.Rows.Count > 0)
                    companyName = dtCompany[0].LocName;
                return companyName;
            }
            catch (Exception ex)
            {
                return companyName;
            }
        }
        #endregion

        protected void btnSaveCom_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnClearComp_Click(object sender, EventArgs e)
        {
            
        }

        protected void gvDept_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void gvDept_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //var rowNum = e.RowIndex;

            //if (rowNum == -1) return;

            //var deptRef = gvDept.Rows[rowNum].Cells[0].Text.Trim() == "&nbsp;"
            //                              ? "0"
            //                              : gvDept.Rows[rowNum].Cells[0].Text.Trim();

            //var taEmpOff = new tblHrmsEmpOfficeTableAdapter();
            //var dtEmpOff = taEmpOff.GetDataByDeptRef(deptRef);
            //if (dtEmpOff.Rows.Count > 0)
            //{
            //    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to delete this Department.";
            //    tblMsg.Rows[1].Cells[0].InnerText = "Department already used.";
            //    ModalPopupExtenderMsg.Show();
            //    return;
            //}
            //else
            //{
            //    var taDept = new tblHrmsDeptTableAdapter();
            //    var dtDept = taDept.GetDataByDeptRef(Convert.ToInt32(deptRef));
            //    if (dtDept.Rows.Count > 0)
            //    {
            //        taDept.DeleteDept(Convert.ToInt32(deptRef));
            //        tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
            //        tblMsg.Rows[1].Cells[0].InnerText = "";
            //        ModalPopupExtenderMsg.Show();
            //    }

            //    var dtDeptNew = taDept.GetDataByAsc();
            //    Session["data"] = dtDeptNew;
            //    SetDepGridData();
            //}
        }

        protected void gvDept_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvDept.PageIndex = e.NewPageIndex;
            //SetDepGridData();
        }

        protected void SetDepGridData()
        {
            //var dtItem = Session["data"];
            //gvDept.DataSource = dtItem;
            //gvDept.DataBind();
            //gvDept.SelectedIndex = -1;
        }

        protected void txtEmpName_TextChanged(object sender, EventArgs e)
        {
            var empRef = "";
            var srchWords = txtEmpName.Text.Trim().Split(':');
            foreach (string word in srchWords)
            {
                empRef = word;
                break;
            }

            if (empRef.Length > 0)
            {
                int result;
                if (int.TryParse(empRef, out result))
                {
                    var taEmp = new View_Emp_BascTableAdapter();
                    var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt32(empRef.ToString()));
                    if (dtEmp.Rows.Count > 0)
                    {
                        AutoCompleteExtenderEmp.ContextKey = dtEmp[0].DeptRefNo.ToString();
                        
                        lblDesig.Text = dtEmp[0].DesigName.ToString();
                        lblDept.Text = dtEmp[0].DeptName.ToString();
                        lblSec.Text = dtEmp[0].SecName.ToString();
                        lblShift.Text = dtEmp[0].ShiftName.ToString();

                        lblPresGrossAmt.Text = dtEmp[0].EmpSalary.ToString();
                        lblPresDesignation.Text = dtEmp[0].DesigName.ToString();

                        var taEmpNew = new View_Emp_BascTableAdapter();
                        var dtEmpSup = taEmpNew.GetDataByEmpRef(Convert.ToInt32(dtEmp[0].EmpSuprId.ToString()));
                        lblSup.Text = dtEmpSup.Rows.Count > 0 ? dtEmpSup[0].EmpName.ToString() + " (" + dtEmpSup[0].DesigName + ")" : "";

                        hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";
                        imgEmp.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";

                        txtIncrAmt.Text = "";
                        lblIncrPrec.Text = "Increament: 0.00%" ;
                    }
                    else
                    {
                        hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
                        imgEmp.ImageUrl = "~/Image/NoImage.gif";

                        lblDesig.Text = "";
                        lblDept.Text = "";
                        lblSec.Text = "";
                        lblShift.Text = "";
                        lblSup.Text = "";
                    }
                }
                else
                {
                    hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
                    imgEmp.ImageUrl = "~/Image/NoImage.gif";

                    lblDesig.Text = "";
                    lblDept.Text = "";
                    lblSec.Text = "";
                    lblShift.Text = "";
                    lblSup.Text = "";
                }
            }
            else
            {
                hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
                imgEmp.ImageUrl = "~/Image/NoImage.gif";

                lblDesig.Text = "";
                lblDept.Text = "";
                lblSec.Text = "";
                lblShift.Text = "";
                lblSup.Text = "";
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var empRef = "";
            var srchWords = txtEmpName.Text.Trim().Split(':');
            foreach (string word in srchWords)
            {
                empRef = word;
                break;
            }

            if (empRef.Length > 0)
            {
                int result;
                if (int.TryParse(empRef, out result))
                {
                    var taEmpGenInfo = new tblHrmsEmpTableAdapter();

                    var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(empRef.ToString());
                    if (dtEmpGenInfo.Rows.Count > 0)
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
        }

        protected void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedValue == "1")
            {
                lblIncr.Visible = true;
                txtIncrAmt.Text = "";
                txtIncrAmt.Visible = true;                
                lblPresGross.Visible = true;
                lblPresGrossAmt.Visible = true;
                lblIncrPrec.Visible = true;
                lblIncrPrec.Text = "Increament: 0.00%";

                lblProm.Visible = false;
                cboDesig.SelectedIndex = 0;
                cboDesig.Visible = false;
                lblPresDesig.Visible = false;
                lblPresDesignation.Visible = false;
            }

            if (cboType.SelectedValue == "2")
            {
                lblIncr.Visible = false;
                txtIncrAmt.Text = "";
                txtIncrAmt.Visible = false;
                lblPresGross.Visible = false;
                lblPresGrossAmt.Visible = false;
                lblIncrPrec.Visible = false;
                lblIncrPrec.Text = "Increament: 0.00%";

                lblProm.Visible = true;
                cboDesig.SelectedIndex = 0;
                cboDesig.Visible = true;
                lblPresDesig.Visible = true;
                lblPresDesignation.Visible = true;
            }

            if (cboType.SelectedValue == "3")
            {
                lblIncr.Visible = true;
                txtIncrAmt.Text = "";
                txtIncrAmt.Visible = true;
                lblProm.Visible = true;
                cboDesig.SelectedIndex = 0;
                cboDesig.Visible = true;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtEmpName.Text = "";

            cboType.SelectedIndex = 0;

            lblIncr.Visible = true;            
            txtIncrAmt.Text = "";
            txtIncrAmt.Visible = true;
            lblPresGross.Visible = true;
            lblPresGrossAmt.Text = "";
            lblPresGrossAmt.Visible = true;
            lblIncrPrec.Visible = true;
            lblIncrPrec.Text = "Increament: 0.00%";

            lblProm.Visible = false;
            cboDesig.SelectedIndex = 0;
            cboDesig.Visible = false;
            lblPresDesig.Visible = false;
            lblPresDesignation.Visible = false;

            txtEffectDt.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtRem.Text = "";

            hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
            imgEmp.ImageUrl = "~/Image/NoImage.gif";

            lblDesig.Text = "";
            lblDept.Text = "";
            lblSec.Text = "";
            lblShift.Text = "";
            lblSup.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taEmpOff = new tblHrmsEmpOfficeTableAdapter();
            var taEmpServLog = new tblHrmsEmpServLogTableAdapter();            

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpOff.Connection);

            try
            {               
                var empRef = ""; var CompRefNo = ""; var OffLocRefNo = ""; var DeptRefNo = ""; var DeptName=""; var SecRefNo = ""; var DesigRefNo = "";
                var DesigName = ""; var EmpSuprId = ""; var EmpType = ""; var EmpJobStatus = ""; var EmpGrade = ""; var EmpSalary = ""; var WorkStation = "";

                var srchWords = txtEmpName.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    empRef = word;
                    break;
                }

                var taViewEmp = new View_Emp_BascTableAdapter();
                var dtViewEmp = taViewEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtViewEmp.Rows.Count > 0)
                {
                    empRef = dtViewEmp[0].EmpRefNo.ToString();
                    CompRefNo = dtViewEmp[0].CompRefNo.ToString();
                    OffLocRefNo = dtViewEmp[0].OffLocRefNo.ToString();
                    DeptRefNo = dtViewEmp[0].DeptRefNo.ToString();
                    DeptName = dtViewEmp[0].DeptName.ToString();
                    SecRefNo = dtViewEmp[0].SecRefNo.ToString();
                    DesigRefNo = dtViewEmp[0].DesigRefNo.ToString();
                    DesigName = dtViewEmp[0].DesigName.ToString();
                    EmpSuprId = dtViewEmp[0].EmpSuprId.ToString();
                    EmpType = dtViewEmp[0].EmpType.ToString();
                    EmpJobStatus = dtViewEmp[0].EmpJobStatus.ToString();
                    EmpGrade = dtViewEmp[0].EmpGrade.ToString();
                    EmpSalary = dtViewEmp[0].EmpSalary.ToString("N2");
                    WorkStation = dtViewEmp[0].EmpExtData1.ToString();                    
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Enter valid Employee.";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                taEmpOff.AttachTransaction(myTran);
                taEmpServLog.AttachTransaction(myTran);

                #region Update Service Log
                var dtMaxSerLogPos = taEmpServLog.GetMaxServPos(empRef.ToString());
                var maxtSerLogPos = dtMaxSerLogPos == null ? 1 : Convert.ToInt32(dtMaxSerLogPos) + 1;

                if (cboType.SelectedValue == "1")//------Increament
                {
                    taEmpServLog.InsertEmpServLog(maxtSerLogPos, cboType.SelectedItem.Text, Convert.ToDateTime(txtEffectDt.Text.Trim()), empRef.ToString(), CompRefNo.ToString(),
                        OffLocRefNo.ToString(), DeptRefNo.ToString(), SecRefNo.ToString(), DesigRefNo.ToString(), EmpSuprId.ToString(), EmpType.ToString(), EmpJobStatus.ToString(),
                        EmpGrade.ToString(), Convert.ToDecimal(EmpSalary), DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                        EmpSalary.ToString(), Convert.ToDecimal(txtIncrAmt.Text).ToString("N2"), "Increament Amount: " + (Convert.ToDecimal(txtIncrAmt.Text) - Convert.ToDecimal(lblPresGrossAmt.Text)).ToString("N2") +
                        " BDT [" + (((Convert.ToDecimal(txtIncrAmt.Text) - Convert.ToDecimal(lblPresGrossAmt.Text)) / Convert.ToDecimal(lblPresGrossAmt.Text)) * 100).ToString("N2") + "%]", "1", "");

                    taEmpOff.UpdateSalary(Convert.ToDecimal(txtIncrAmt.Text), empRef.ToString());
                }

                if (cboType.SelectedValue == "2")//------Promotion
                {
                    taEmpServLog.InsertEmpServLog(maxtSerLogPos, cboType.SelectedItem.Text, Convert.ToDateTime(txtEffectDt.Text.Trim()), empRef.ToString(), CompRefNo.ToString(),
                        OffLocRefNo.ToString(), DeptRefNo.ToString(), SecRefNo.ToString(), cboDesig.SelectedValue.ToString(), EmpSuprId.ToString(), EmpType.ToString(), EmpJobStatus.ToString(),
                        EmpGrade.ToString(), Convert.ToDecimal(EmpSalary), DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                        DesigName.ToString(), cboDesig.SelectedItem.Text.ToString(), "Promoted as: " + cboDesig.SelectedItem.Text + ", " + DeptName.ToString(), "1", "");

                    taEmpOff.UpdateDesig(cboDesig.SelectedValue.ToString(), empRef.ToString());
                }
                #endregion

                myTran.Commit();

                tblMsg.Rows[0].Cells[0].InnerText = "Data updated successfully.";
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
    }
}