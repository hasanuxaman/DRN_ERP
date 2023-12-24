using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmEmpRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taEmpBasc = new dt_View_Emp_BascTableAdapter();
            var dtEmpBasc = taEmpBasc.GetDataByActList();
            Session["data"] = dtEmpBasc;
            SetEmpGridData();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var taEmpBasc = new dt_View_Emp_BascTableAdapter();
            var dtEmpBasc = new dsEmpDet.dt_View_Emp_BascDataTable();

            try
            {
                var qryStr = "";
                //if (optListAll.Checked)
                //{
                //    //qryStr = "SELECT emp.[EmpRefNo] as EMP_REF,emp.[EmpId] as EMP_ID,emp.[EmpName] as EMP_NAME,emp.[EmpFatherName] as FATHER_NAME" +
                //    //              ",emp.[EmpMotherName] as MOTHER_NAME,emp.[GenderName] as GENDER,emp.[MaritalStatusName] as MARITAL_STATUS " +
                //    //              ",emp.[EmpSpouse] as SPOUSE_NAME,emp.[EmpDOB] as DATE_OF_BIRTH,emp.[EmpPOB] as PLACE_OF_BIRTH " +
                //    //              ",emp.[EmpIsAct] as IS_ACTIVE,emp.[EmpInactDate] as INACTIVE_DATE " +
                //    //              ",emp.[EmpSetlFlag] as SETTELEMENT_STATUS,emp.[EmpSetlDate] as SETTELEMENT_DATE " +
                //    //              ",'H#:'+emp.[EmpCurAdrHouseRoad]+', PO:'+emp.[EmpCurAdrPO]+'-'+emp.[EmpCurAdrPOCode]+', THANA:'+emp.[EmpCurAdrThana] " +
                //    //              "+', DIST:'+emp.[EmpCurAdrDist] as PRESENT_ADDRESS " +
                //    //              ",'H#:'+emp.[EmpPermAdrHouseRoad]+', PO:'+emp.[EmpPermAdrPO]+'-'+emp.[EmpPermAdrPOCode]+', THANA:'+emp.[EmpPermAdrThana] " +
                //    //              "+', DIST:'+emp.[EmpPermAdrDist] as PERMANENT_ADDRESS " +
                //    //              ",emp.[EmpEmerCPName] as EMERGENCY_CONTACT_NAME,emp.[EmpEmerCPRelation] as  EMERGENCY_CONTACT_RELATION " +
                //    //              ",'H#:'+emp.[EmpEmerCPHouseRoad]+', PO:'+emp.[EmpEmerCPPO]+'-'+emp.[EmpEmerCPPOCode]+', THANA:'+emp.[EmpEmerCPThana] " +
                //    //              "+', DIST:'+emp.[EmpEmerCPDist] as EMERGENCY_CONTACT_ADDRESS,emp.[EmpEmerCPPhone] as EMERGENCY_CONTACT_CELL_PH_NO " +
                //    //              ",emp.[EmpEmerCPCell] EMERGENCY_CONTACT_CELL_CELL_NO,emp.[ReligionName] as RELIGION,emp.[EmpNationality] as NATIONALITY " +
                //    //              ",emp.[BldGrpName] as BLOOD_GRP,emp.[EmpHomephone] as HOME_PHONE,emp.[EmpCellPhone] as CELL_PHONE,emp.[EmpPerEmail] as E_MAIL " +
                //    //              ",emp.[EmpNID] as NID_NO,emp.[EmpTIN] as TIN_NO,emp.[EmpDLNo] as DRIVING_LICENSE_NO,emp.[EmpDLIssFrom] as DRIVING_LICENSE_ISU_FROM " +
                //    //              ",emp.[EmpDLExpDate] as DRIVING_LICENSE_EXP_DATE,emp.[EmpPasNo] as PASSPORT_NO,emp.[EmpPasIssFrom] as PASSPORT_ISU_FROM " +
                //    //              ",emp.[EmpPasIssDate] as PASSPORT_ISU_DATE,emp.[EmpPasExprDate] as PASSPORT_EXP_DATE,emp.[LocName] as BUSINESS_UNIT " +
                //    //              ",emp.[DeptName] as DEPARTMENT,emp.[SecName] as SECTION,emp.[DesigName] as DESIGNATION,emp.[EmpDOJ] as DATE_OF_JOIN " +
                //    //              ",emp.[EmpSuprId] as SUPERVISOR_ID,sup.EmpName as SUPERVISOR_NAME,emp.[JobStatName] as JOB_STATUS,emp.[EmpConfDueDate] as CONFIRM_DUE_DATE " +
                //    //              ",emp.[EmpConfDate] as CONFIRMATION_DATE,emp.[EmpTypeName] as EMP_TYPE,emp.[ShiftName] as SHIFT,emp.[GrdDefName] as GRADE " +
                //    //              ",emp.[EmpSalary] as SALARY,emp.[BankAccNo] as BANK_AC_NO,emp.[EmpOffEmail] as OFFICE_EMAIL,emp.[EmpWorkPhone] as OFFICE_PHONE " +
                //    //              ",emp.[EmpPabxNo] as OFFICE_PABX,emp.[EmpIpPhone] as OFFICE_IP_PHONE,[Wrk_Station_Name] as WORK_STATION " +
                //    //              ",emp.[EmpOffExtData2] as TA_DA_OTHER_ALLOWENCE FROM [DRN].[dbo].[View_Emp_Basc] emp left outer join tblHrmsWorkStation " +
                //    //              "on emp.[EmpOffExtData1]=Wrk_Station_Code left outer join [View_Emp_Basc] sup on emp.[EmpSuprId]=sup.EmpRefNo";
                //    dtEmpBasc = taEmpBasc.GetData();
                //}
                //else
                //{
                //    //qryStr = "SELECT emp.[EmpRefNo] as EMP_REF,emp.[EmpId] as EMP_ID,emp.[EmpName] as EMP_NAME,emp.[EmpFatherName] as FATHER_NAME" +
                //    //              ",emp.[EmpMotherName] as MOTHER_NAME,emp.[GenderName] as GENDER,emp.[MaritalStatusName] as MARITAL_STATUS " +
                //    //              ",emp.[EmpSpouse] as SPOUSE_NAME,emp.[EmpDOB] as DATE_OF_BIRTH,emp.[EmpPOB] as PLACE_OF_BIRTH " +
                //    //              ",emp.[EmpIsAct] as IS_ACTIVE,emp.[EmpInactDate] as INACTIVE_DATE " +
                //    //              ",emp.[EmpSetlFlag] as SETTELEMENT_STATUS,emp.[EmpSetlDate] as SETTELEMENT_DATE " +
                //    //              ",'H#:'+emp.[EmpCurAdrHouseRoad]+', PO:'+emp.[EmpCurAdrPO]+'-'+emp.[EmpCurAdrPOCode]+', THANA:'+emp.[EmpCurAdrThana] " +
                //    //              "+', DIST:'+emp.[EmpCurAdrDist] as PRESENT_ADDRESS " +
                //    //              ",'H#:'+emp.[EmpPermAdrHouseRoad]+', PO:'+emp.[EmpPermAdrPO]+'-'+emp.[EmpPermAdrPOCode]+', THANA:'+emp.[EmpPermAdrThana] " +
                //    //              "+', DIST:'+emp.[EmpPermAdrDist] as PERMANENT_ADDRESS " +
                //    //              ",emp.[EmpEmerCPName] as EMERGENCY_CONTACT_NAME,emp.[EmpEmerCPRelation] as  EMERGENCY_CONTACT_RELATION " +
                //    //              ",'H#:'+emp.[EmpEmerCPHouseRoad]+', PO:'+emp.[EmpEmerCPPO]+'-'+emp.[EmpEmerCPPOCode]+', THANA:'+emp.[EmpEmerCPThana] " +
                //    //              "+', DIST:'+emp.[EmpEmerCPDist] as EMERGENCY_CONTACT_ADDRESS,emp.[EmpEmerCPPhone] as EMERGENCY_CONTACT_CELL_PH_NO " +
                //    //              ",emp.[EmpEmerCPCell] EMERGENCY_CONTACT_CELL_CELL_NO,emp.[ReligionName] as RELIGION,emp.[EmpNationality] as NATIONALITY " +
                //    //              ",emp.[BldGrpName] as BLOOD_GRP,emp.[EmpHomephone] as HOME_PHONE,emp.[EmpCellPhone] as CELL_PHONE,emp.[EmpPerEmail] as E_MAIL " +
                //    //              ",emp.[EmpNID] as NID_NO,emp.[EmpTIN] as TIN_NO,emp.[EmpDLNo] as DRIVING_LICENSE_NO,emp.[EmpDLIssFrom] as DRIVING_LICENSE_ISU_FROM " +
                //    //              ",emp.[EmpDLExpDate] as DRIVING_LICENSE_EXP_DATE,emp.[EmpPasNo] as PASSPORT_NO,emp.[EmpPasIssFrom] as PASSPORT_ISU_FROM " +
                //    //              ",emp.[EmpPasIssDate] as PASSPORT_ISU_DATE,emp.[EmpPasExprDate] as PASSPORT_EXP_DATE,emp.[LocName] as BUSINESS_UNIT " +
                //    //              ",emp.[DeptName] as DEPARTMENT,emp.[SecName] as SECTION,emp.[DesigName] as DESIGNATION,emp.[EmpDOJ] as DATE_OF_JOIN " +
                //    //              ",emp.[EmpSuprId] as SUPERVISOR_ID,sup.EmpName as SUPERVISOR_NAME,emp.[JobStatName] as JOB_STATUS,emp.[EmpConfDueDate] as CONFIRM_DUE_DATE " +
                //    //              ",emp.[EmpConfDate] as CONFIRMATION_DATE,emp.[EmpTypeName] as EMP_TYPE,emp.[ShiftName] as SHIFT,emp.[GrdDefName] as GRADE " +
                //    //              ",emp.[EmpSalary] as SALARY,emp.[BankAccNo] as BANK_AC_NO,emp.[EmpOffEmail] as OFFICE_EMAIL,emp.[EmpWorkPhone] as OFFICE_PHONE " +
                //    //              ",emp.[EmpPabxNo] as OFFICE_PABX,emp.[EmpIpPhone] as OFFICE_IP_PHONE,[Wrk_Station_Name] as WORK_STATION " +
                //    //              ",emp.[EmpOffExtData2] as TA_DA_OTHER_ALLOWENCE FROM [DRN].[dbo].[View_Emp_Basc] emp left outer join tblHrmsWorkStation " +
                //    //              "on emp.[EmpOffExtData1]=Wrk_Station_Code left outer join [View_Emp_Basc] sup on emp.[EmpSuprId]=sup.EmpRefNo where emp.[EmpSetlFlag]='N'";
                //    dtEmpBasc = taEmpBasc.GetDataByActList();
                //}

                var dtEmp = (DataTable)Session["data"];

                SqlCommand cmd = new SqlCommand(qryStr);
                //DataTable dt = GetData(cmd);

                DataTable dt = dtEmp;

                if (dt.Rows.Count > 65535)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                    return;
                }

                //First Method
                #region With Formating
                Response.Clear();
                Response.Buffer = true;
                string filename = String.Format("Employee_List_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //Create a dummy GridView
                    GridView GridView1 = new GridView();
                    GridView1.AllowPaging = false;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    GridView1.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in GridView1.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView1.HeaderStyle.BackColor;
                    }

                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        row.BackColor = System.Drawing.Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView1.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
                #endregion
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        protected void gvEmpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmpList.PageIndex = e.NewPageIndex;
            SetEmpGridData();
        }

        protected void SetEmpGridData()
        {
            var dtEmp = Session["data"];
            gvEmpList.DataSource = dtEmp;
            gvEmpList.DataBind();
            gvEmpList.SelectedIndex = -1;
        }

        protected void optActive_CheckedChanged(object sender, EventArgs e)
        {
            var taEmpBasc = new dt_View_Emp_BascTableAdapter();
            var dtEmpBasc = taEmpBasc.GetDataByActList();
            Session["data"] = dtEmpBasc;
            SetEmpGridData();
        }

        protected void optListAll_CheckedChanged(object sender, EventArgs e)
        {
            var taEmpBasc = new dt_View_Emp_BascTableAdapter();
            var dtEmpBasc = taEmpBasc.GetData();
            Session["data"] = dtEmpBasc;
            SetEmpGridData();
        }
    }
}