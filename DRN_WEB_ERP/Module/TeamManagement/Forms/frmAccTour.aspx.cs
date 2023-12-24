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
using DRN_WEB_ERP.Module.TeamManagement.DataSets;
using DRN_WEB_ERP.Module.TeamManagement.DataSets.dsTemManageTableAdapters;

namespace DRN_WEB_ERP.Module.TeamManagement.Forms
{
    public partial class frmAccTour : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtVisitDateTbl.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtTourFromDt.Text = DateTime.Now.AddDays(-15).ToString("dd/MM/yyyy");
            txtTourToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            var taEng = new View_Emp_BascTableAdapter();
            var dtEng = new DataTable();
            if (empRef == "000568" || empRef == "000210" || empRef == "000011")
                dtEng = taEng.GetDataByLocDeptSec("100002", "100055", "100079");
            else
                dtEng = taEng.GetDataBySupRef(empRef);
            foreach (dsEmpDet.View_Emp_BascRow dr in dtEng.Rows)
            {
                ddlEngName.Items.Add(new ListItem(dr.EmpName + " >>>" + dr.DesigName + " >>>" + dr.DeptName + " >>>" + dr.EmpId, dr.EmpRefNo));
                ddlEngName.DataBind();
            }
            ddlEngName.Items.Insert(0, "---Select---");

            ddlEngName.SelectedValue = empRef;

            ddlEngName.Enabled = empRef == "000011";

            LoadPendData();

            LoadTourData();
        }

        private void LoadPendData()
        {
            var taTourPlan = new tbl_Team_Tour_PlanTableAdapter();
            try
            {
                var dtPlanDet = taTourPlan.GetDataByPend(ddlEngName.SelectedValue.ToString(), "CRD");
                gvPend.DataSource = dtPlanDet;
                gvPend.DataBind();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void ddlEngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPendData();
            LoadTourData();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPendData();
        }

        private void LoadTourData()
        {
            var taTour = new tbl_Team_TourTableAdapter();
            try
            {
                var dtTourDet = taTour.GetDataByEmp(ddlEngName.SelectedValue.ToString(), Convert.ToDateTime(txtTourFromDt.Text), Convert.ToDateTime(txtTourToDt.Text), "CRD");
                gvTour.DataSource = dtTourDet;
                gvTour.DataBind();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvPend_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region Update Requisition Status
            var taTourPlan = new tbl_Team_Tour_PlanTableAdapter();
            var taTour = new tbl_Team_TourTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taTourPlan.Connection);

            try
            {
                taTourPlan.AttachTransaction(myTran);
                taTour.AttachTransaction(myTran);

                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvPend.Rows[index];

                var hfPlanRef = (HiddenField)(row.FindControl("hfPlanRef"));
                var txtVisitDt = (TextBox)(row.FindControl("txtVisitDt"));

                var dtTourPlan = taTourPlan.GetDataByPlanRef(hfPlanRef.Value.ToString());
                if (dtTourPlan.Rows.Count > 0)
                {
                    #region Approve
                    if (e.CommandName == "Accept")
                    {
                        txtRemVisitDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtPendRem.Text = "";

                        hfRemPlanRef.Value = hfPlanRef.Value.ToString();
                        ModalPopupExtenderRem.Show();                        

                        //var dtTour = taTour.GetMaxTourRef(Convert.ToDateTime(txtVisitDt.Text).Year, "P");
                        //var tourRef = dtTour == null ? 1 : Convert.ToInt32(dtTour) + 1;
                        //var nextTourRef = Convert.ToDateTime(txtVisitDt.Text).ToString("MM") + Convert.ToDateTime(txtVisitDt.Text).ToString("yy") + "-" + tourRef.ToString("000000");

                        //taTour.InsertTour("TUR-" + nextTourRef.ToString(), ddlEngName.SelectedValue.ToString(), dtTourPlan[0].Tem_Plan_Ref, dtTourPlan[0].Tem_Plan_Com,
                        //    dtTourPlan[0].Tem_Plan_Com_Adr, dtTourPlan[0].Tem_Plan_Com_Cont_No, dtTourPlan[0].Tem_Plan_Com_Cont_Name, dtTourPlan[0].Tem_Plan_Com_Cont_Ref,
                        //    dtTourPlan[0].Tem_Plan_Tour_Date, Convert.ToDateTime(txtVisitDt.Text), dtTourPlan[0].Tem_Plan_Tour_Type,
                        //    Convert.ToDateTime(txtVisitDt.Text).ToString("MM") + "/" + Convert.ToDateTime(txtVisitDt.Text).Year, "", "", "", "",
                        //    DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "P");

                        //taTourPlan.UpdatePlanStatus("2", hfPlanRef.Value.ToString());

                        //myTran.Commit();
                        //tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        //tblMsg.Rows[1].Cells[0].InnerText = "";
                        //ModalPopupExtenderMsg.Show();
                    }
                    #endregion

                    #region Reject
                    if (e.CommandName == "Dismiss")
                    {
                        taTourPlan.UpdatePlanStatus("0", hfPlanRef.Value.ToString());

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Data Rejected successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    #endregion
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Tour Plan Data Not Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                LoadPendData();
                LoadTourData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            #endregion
        }

        protected void gvPend_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txtVisitDt")).Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //var taTourPlan = new tbl_Team_Tour_PlanTableAdapter();
            var taTour = new tbl_Team_TourTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taTour.Connection);

            try
            {
                if (ddlEngName.SelectedIndex == 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Select Employee Name First";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                taTour.AttachTransaction(myTran);

                var dtTour = taTour.GetMaxTourRef(Convert.ToDateTime(txtVisitDateTbl.Text).Year, "P", "CRD");
                var tourRef = dtTour == null ? 1 : Convert.ToInt32(dtTour) + 1;
                var nextTourRef = Convert.ToDateTime(txtVisitDateTbl.Text).ToString("MM") + Convert.ToDateTime(txtVisitDateTbl.Text).ToString("yy") + "-" + tourRef.ToString("000000");

                taTour.InsertTour("TUR-" + nextTourRef.ToString(), ddlEngName.SelectedValue.ToString(), "", txtCompNameTbl.Text,
                                txtCompAdrTbl.Text, txtCompContNoTbl.Text, txtCompContNameTbl.Text, txtCompContDesigTbl.Text.ToString(),
                                null, Convert.ToDateTime(txtVisitDateTbl.Text), ddlVisitPurposeTbl.SelectedItem.ToString(),
                                Convert.ToDateTime(txtVisitDateTbl.Text).ToString("MM") + "/" + Convert.ToDateTime(txtVisitDateTbl.Text).Year, txtVisitNoTbl.Text.Trim(),
                                txtRemTbl.Text.Trim(), "", "CRD", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "P");
                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                LoadTourData();

                txtCompNameTbl.Text = "";
                txtCompAdrTbl.Text = "";
                txtCompContNoTbl.Text = "";
                txtCompContNameTbl.Text = "";
                txtCompContDesigTbl.Text = "";
                txtVisitNoTbl.Text = "";
                txtRemTbl.Text = "";
                txtVisitDateTbl.Text = DateTime.Now.ToString("dd/MM/yyyy");                
                ddlVisitPurposeTbl.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnRemOk_Click(object sender, EventArgs e)
        {
            var taTourPlan = new tbl_Team_Tour_PlanTableAdapter();
            var taTour = new tbl_Team_TourTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taTourPlan.Connection);

            try
            {
                taTourPlan.AttachTransaction(myTran);
                taTour.AttachTransaction(myTran);

                var dtTourPlan = taTourPlan.GetDataByPlanRef(hfRemPlanRef.Value.ToString());
                if (dtTourPlan.Rows.Count > 0)
                {
                    var dtTour = taTour.GetMaxTourRef(Convert.ToDateTime(txtRemVisitDt.Text).Year, "P", "CRD");
                    var tourRef = dtTour == null ? 1 : Convert.ToInt32(dtTour) + 1;
                    var nextTourRef = Convert.ToDateTime(txtRemVisitDt.Text).ToString("MM") + Convert.ToDateTime(txtRemVisitDt.Text).ToString("yy") + "-" + tourRef.ToString("000000");

                    taTour.InsertTour("TUR-" + nextTourRef.ToString(), ddlEngName.SelectedValue.ToString(), dtTourPlan[0].Tem_Plan_Ref, dtTourPlan[0].Tem_Plan_Com,
                        dtTourPlan[0].Tem_Plan_Com_Adr, dtTourPlan[0].Tem_Plan_Com_Cont_No, dtTourPlan[0].Tem_Plan_Com_Cont_Name, dtTourPlan[0].Tem_Plan_Com_Cont_Ref,
                        dtTourPlan[0].Tem_Plan_Tour_Date, Convert.ToDateTime(txtRemVisitDt.Text), dtTourPlan[0].Tem_Plan_Tour_Type,
                        Convert.ToDateTime(txtRemVisitDt.Text).ToString("MM") + "/" + Convert.ToDateTime(txtRemVisitDt.Text).Year, dtTourPlan[0].Tem_Plan_Ext_Data2,
                        txtPendRem.Text.Trim(), "", "CRD", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "P");

                    taTourPlan.UpdatePlanStatus("2", hfRemPlanRef.Value.ToString());

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    LoadPendData();
                    LoadTourData();
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Plan Data Not Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            LoadTourData();
        }
    }
}