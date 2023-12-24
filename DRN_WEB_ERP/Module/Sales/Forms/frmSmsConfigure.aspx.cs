using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSmsConfigure : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                AutoCompleteExtenderEmp.ContextKey = "0";

                var taSmsConfig = new tbl_Sms_ConfigTableAdapter();
                var dtSmsConfig = taSmsConfig.GetDataBySortTranType();
                Session["data"] = dtSmsConfig;
                SetConfigGridData();
            }
            catch (Exception ex) { }
        }

        #region GridData
        public string GetStatus(int statRef)
        {
            string strStat = "";
            try
            {
                strStat = statRef == 0 ? "Inactive" : "Active";
                return strStat;
            }
            catch (Exception) { return strStat; }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("btnSave");

            var taSmsConfig = new tbl_Sms_ConfigTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSmsConfig.Connection);

            try
            {                
                var empRef = "";
                var empName = "";

                if (cboReceiverType.SelectedValue == "SINGLE")
                {
                    if (txtEmpName.Text == "")
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Enter Employee Name.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                    #region Validate Employee
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
                            var taViewEmp = new View_Emp_BascTableAdapter();
                            var dtViewEmp = taViewEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                            if (dtViewEmp.Rows.Count > 0)
                            {
                                empRef = dtViewEmp[0].EmpRefNo.ToString();
                                empName = dtViewEmp[0].EmpName.ToString();
                            }
                            else
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion

                    if (txtCellNo.Text == "")
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Enter cell no.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                    if (txtCellNo.Text.Trim().Substring(0,2) != "01")
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Cell no should start with 01.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                    if (txtCellNo.Text.Trim().Length != 11)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Cell no should be 11 digit.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    if (cboReceiverGroup.SelectedValue == "INDV")
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "You have to select receiver group except Individual.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }                

                var ConfigStat = "0";
                if (optActive.Checked)
                    ConfigStat = "1";

                taSmsConfig.AttachTransaction(myTran);

                if (hfConfigEditRef.Value != "0")
                {
                    taSmsConfig.UpdateSmsConfig(cboTranType.SelectedValue.ToString(), cboReceiverType.SelectedValue.ToString(), cboReceiverGroup.SelectedValue.ToString(),
                       empRef.ToString(), empName.ToString(), txtDesignation.Text.Trim(), txtCellNo.Text.Trim(), ConfigStat, "", Convert.ToInt32(hfConfigEditRef.Value));

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Configuration Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtSmsConfigChk = taSmsConfig.GetDataByDupplicateValue(cboTranType.SelectedValue.ToString(), cboReceiverType.SelectedValue.ToString(), cboReceiverGroup.SelectedValue.ToString(), empRef.ToString());
                    if (dtSmsConfigChk.Rows.Count > 0)
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Same configuration already exists.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                    taSmsConfig.InsertSmsConfig(cboTranType.SelectedValue.ToString(), cboReceiverType.SelectedValue.ToString(), cboReceiverGroup.SelectedValue.ToString(),                
                        empRef.ToString(), empName.ToString(), txtDesignation.Text.Trim(), txtCellNo.Text.Trim(), ConfigStat, "");

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Configuration Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }

                hfConfigEditRef.Value = "0";

                cboTranType.SelectedIndex = 0;
                cboReceiverType.SelectedIndex = 0;
                cboReceiverGroup.SelectedIndex = 0;
                cboReceiverType.Enabled = true;
                cboReceiverGroup.Enabled = false;

                txtEmpName.Text = "";
                txtEmpName.Enabled = false;
                txtDesignation.Text = "";
                //txtDesignation.Enabled = false;
                txtCellNo.Text = "";
                txtCellNo.Enabled = false;

                optActive.Checked = true;
                optInactive.Checked = false;

                var dtSmsConfig = taSmsConfig.GetDataBySortTranType();
                Session["data"] = dtSmsConfig;
                SetConfigGridData();            
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                tblMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hfConfigEditRef.Value = "0";

            cboTranType.SelectedIndex = 0;
            cboReceiverType.SelectedIndex = 0;
            cboReceiverGroup.SelectedIndex = 0;
            cboReceiverType.Enabled = true;
            cboReceiverGroup.Enabled = false;

            txtEmpName.Text = "";
            txtEmpName.Enabled = false;
            txtDesignation.Text = "";
            //txtDesignation.Enabled = false;
            txtCellNo.Text = "";
            txtCellNo.Enabled = false;

            optActive.Checked = true;
            optInactive.Checked = false;

            var taSmsConfig = new tbl_Sms_ConfigTableAdapter();
            var dtSmsConfig = taSmsConfig.GetDataBySortTranType();
            Session["data"] = dtSmsConfig;
            SetConfigGridData();
        }

        protected void SetConfigGridData()
        {
            var dtConfig = Session["data"];
            gvSmsConfigList.DataSource = dtConfig;
            gvSmsConfigList.DataBind();
            gvSmsConfigList.SelectedIndex = -1;
        }

        protected void gvSmsConfigList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSmsConfigList.PageIndex = e.NewPageIndex;
            SetConfigGridData();
        }

        protected void gvSmsConfigList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvSmsConfigList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvSmsConfigList.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    HiddenField hfConfigRefNo = (HiddenField)gvSmsConfigList.Rows[indx].FindControl("hfConfigRefNo");
                    var configRefNo = hfConfigRefNo.Value;

                    hfConfigEditRef.Value = configRefNo.ToString();

                    var taConfig = new tbl_Sms_ConfigTableAdapter();
                    var dtConfig = taConfig.GetDataByConfigRefNo(Convert.ToInt32(hfConfigEditRef.Value.ToString()));
                    if (dtConfig.Rows.Count > 0)
                    {
                        cboTranType.SelectedValue = dtConfig[0].Config_Tran_Type.ToString();
                        cboReceiverType.SelectedValue = dtConfig[0].Config_Receiver_Type.ToString();
                        cboReceiverGroup.SelectedValue = dtConfig[0].Config_Receiver_Grp_Name.ToString();

                        if (dtConfig[0].Config_Receiver_Type.ToString() == "SINGLE")
                            txtEmpName.Text = dtConfig[0].Config_Receiver_Emp_Ref.ToString() + ":" + dtConfig[0].Config_Receiver_Name.ToString();
                        else
                            txtEmpName.Text = "";

                        txtDesignation.Text = dtConfig[0].Config_Receiver_Desig.ToString();
                        txtCellNo.Text = dtConfig[0].Config_Receiver_Cell_No.ToString();

                        if (dtConfig[0].Config_Status.ToString() == "1")
                        {
                            optActive.Checked = true;
                            optInactive.Checked = false;
                        }
                        else
                        {
                            optInactive.Checked = true;
                            optActive.Checked = false;
                        }

                        if (dtConfig[0].Config_Receiver_Type.ToString() == "SINGLE")
                        {
                            cboReceiverType.Enabled = false;
                            txtEmpName.Enabled = true;
                            //txtDesignation.Enabled = true;
                            txtCellNo.Enabled = true;
                        }
                        else
                        {
                            txtEmpName.Enabled = false;
                            //txtDesignation.Enabled = false;
                            txtCellNo.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    hfConfigEditRef.Value = "0";
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                    tblMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void cboReceiverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboReceiverType.SelectedIndex == 0)
            {
                cboReceiverGroup.SelectedIndex = 0;
                cboReceiverGroup.Enabled = false;
                txtEmpName.Text = "";
                txtEmpName.Enabled = false;
                txtDesignation.Text = "";
                //txtDesignation.Enabled = false;
                txtCellNo.Text = "";
                txtCellNo.Enabled = false;
            }
            else
            {
                if (cboReceiverType.SelectedValue == "SINGLE")
                {
                    cboReceiverGroup.SelectedValue = "INDV";
                    cboReceiverGroup.Enabled = false;
                    txtEmpName.Text = "";
                    txtEmpName.Enabled = true;
                    txtDesignation.Text = "";
                    //txtDesignation.Enabled = true;
                    txtCellNo.Text = "";
                    txtCellNo.Enabled = true;
                }
                else
                {
                    cboReceiverGroup.SelectedIndex = 0;
                    cboReceiverGroup.Enabled = true;
                    txtEmpName.Text = "";
                    txtEmpName.Enabled = false;
                    txtDesignation.Text = "";
                    //txtDesignation.Enabled = false;
                    txtCellNo.Text = "";
                    txtCellNo.Enabled = false;
                }
            }
        }

        protected void txtEmpName_TextChanged(object sender, EventArgs e)
        {
            #region Validate Employee
            var empRef = "";
            var empName = "";
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
                    var taViewEmp = new View_Emp_BascTableAdapter();
                    var dtViewEmp = taViewEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                    if (dtViewEmp.Rows.Count > 0)
                    {
                        empRef = dtViewEmp[0].EmpRefNo.ToString();
                        empName = dtViewEmp[0].EmpName.ToString();
                        txtDesignation.Text = dtViewEmp[0].DesigName.ToString();
                        txtCellNo.Focus();
                    }
                }
            }
            #endregion
        }
    }
}