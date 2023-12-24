using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmHrmAdm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdtServLog_Click(object sender, EventArgs e)
        {
            var taEmpServLog = new tblHrmsEmpServLogTableAdapter();
            var taEmpList = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpServLog.Connection);

            try
            {
                taEmpServLog.AttachTransaction(myTran);

                var dtEmpList = taEmpList.GetData();
                foreach (dsEmpDet.View_Emp_BascRow dr in dtEmpList.Rows)
                {
                    var dtServLog = taEmpServLog.GetDataByEmpRef(dr.EmpRefNo.ToString());
                    if (dtServLog.Rows.Count <= 0)
                        taEmpServLog.InsertEmpServLog(2, "Join", DateTime.Now, dr.EmpRefNo, "100001", dr.OffLocRefNo, dr.DeptRefNo, dr.SecRefNo, dr.DesigRefNo,
                            dr.EmpSuprId.ToString(), dr.EmpType.ToString(), dr.EmpJobStatus.ToString(), dr.EmpGrade.ToString(), Convert.ToDecimal(dr.EmpSalary),
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "");
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Employee Service Log Updated Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}