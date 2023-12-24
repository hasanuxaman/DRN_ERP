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
    public partial class frmSalesPerson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                var taSp = new tblSalesPersonMasTableAdapter();
                var dtSpRef = taSp.GetMaxSpRef();
                var nextRef = dtSpRef == null ? 1 : Convert.ToInt32(dtSpRef) + 1;
                txtSpId.Text = nextRef.ToString();

                var taEmpList = new View_Emp_BascTableAdapter();
                var dtEmp = taEmpList.GetDataByAsc();
                if (dtEmp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmp.Rows.Count; i++)
                    {
                        var empRef = dtEmp.Rows[i]["EmpRefNo"].ToString();
                        var supNameId = dtEmp.Rows[i]["EmpName"].ToString() + " - [" + dtEmp.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtEmp.Rows[i]["DesigName"].ToString() + "]";
                        cboEmpId.Items.Add(new ListItem(supNameId, empRef));
                    }
                }
                cboEmpId.Items.Insert(0, new ListItem("---Select---", "0"));

                var taSpList = new View_Sales_Sp_List_Emp_NameTableAdapter();
                var dtSpList = taSpList.GetDataByListAllBySpStat(Convert.ToInt32(1));
                if (dtSpList.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSpList.Rows.Count; i++)
                    {
                        var spRef = dtSpList.Rows[i]["Sp_Ref"].ToString();
                        var empRef = dtSpList.Rows[i]["EmpRefNo"].ToString();
                        var supNameId = dtSpList.Rows[i]["EmpName"].ToString() + " - [" + dtSpList.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtSpList.Rows[i]["DesigName"].ToString() + "]";
                        ddlSupervisor.Items.Add(new ListItem(supNameId, empRef));

                        ddlReplaceMpoBy.Items.Add(new ListItem(supNameId, spRef));

                        ddlReplaceMpoList.Items.Add(new ListItem(supNameId, spRef));
                        ddlReplaceMpoByList.Items.Add(new ListItem(supNameId, spRef));

                        ddlReplaceSupervisorBy.Items.Add(new ListItem(supNameId, empRef));
                    }
                }
                ddlSupervisor.Items.Insert(0, new ListItem("---Select---", "0"));

                ddlReplaceMpoBy.Items.Insert(0, new ListItem("---Select---", "0"));

                ddlReplaceMpoList.Items.Insert(0, new ListItem("---Select---", "0"));
                ddlReplaceMpoByList.Items.Insert(0, new ListItem("---Select---", "0"));

                ddlReplaceSupervisorBy.Items.Insert(0, new ListItem("---Select---", "0"));

                var taSpSupListAll = new View_Sales_Sp_Supr_List_AllTableAdapter();
                var dtSpSupListAll = taSpSupListAll.GetData();
                if (dtSpSupListAll.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSpSupListAll.Rows.Count; i++)
                    {
                        var empRef = dtSpSupListAll.Rows[i]["EmpRefNo"].ToString();
                        var supNameId = dtSpSupListAll.Rows[i]["EmpName"].ToString() + " - [" + dtSpSupListAll.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtSpSupListAll.Rows[i]["DesigName"].ToString() + "]";
                        ddlReplaceSupervisorList.Items.Add(new ListItem(supNameId, empRef));
                    }
                }
                ddlReplaceSupervisorList.Items.Insert(0, new ListItem("---Select---", "0"));

                var taSalesDsm = new View_Sales_DSMTableAdapter();
                var dtSalesDsm = taSalesDsm.GetActDsm();
                foreach (dsSalesMas.View_Sales_DSMRow dr in dtSalesDsm.Rows)
                {
                    ddlDgm.Items.Add(new ListItem(dr.Dsm_Short_Name.ToString() + " :: " + dr.SalesZoneName.ToString(), dr.Dsm_Ref.ToString()));
                }
                ddlDgm.Items.Insert(0, new ListItem("---Select---", "0"));

                hfSpEditRef.Value = "0";

                ViewState["CurrentAlphabet"] = "ALL";
                GenerateAlphabets();
                BindGrid();
            }
            catch (Exception ex) { }
        }

        #region GetData
        protected void GetData()
        {
            try
            {
                var taSp = new tblSalesPersonMasTableAdapter();
                var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(hfSpEditRef.Value));
                if (dtSp.Rows.Count > 0)
                {
                    txtSpId.Text = hfSpEditRef.Value.ToString();

                    txtSpShortName.Text = dtSp[0].Sp_Short_Name.ToString();
                    txtSpFullName.Text = dtSp[0].Sp_Full_Name.ToString();                                        
                    txtSpCell.Text = dtSp[0].Sp_Cell_No.ToString();
                    txtSpRemarks.Text = dtSp[0].Sp_Remarks.ToString();

                    txtInactiveDate.Text = dtSp[0].IsSp_Inactive_DateNull() ? "" : dtSp[0].Sp_Inactive_Date.ToString("dd/MM/yyyy");

                    if (dtSp[0].Sp_Is_Active == 1)
                    {
                        optActive.Checked = true;
                        optInactive.Checked = false;
                    }
                    else
                    {
                        optInactive.Checked = true;
                        optActive.Checked = false;
                    }

                    cboEmpId.SelectedIndex = cboEmpId.Items.IndexOf(cboEmpId.Items.FindByValue(dtSp[0].Sp_User_Ref.ToString()));
                    ddlSupervisor.Items.Clear();
                    var taSpList = new View_Sales_Sp_List_Emp_NameTableAdapter();
                    var dtSpList = taSpList.GetDataByListAllBySpStatExceptSelf(cboEmpId.SelectedValue.ToString(), Convert.ToInt32(1));
                    if (dtSpList.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtSpList.Rows.Count; i++)
                        {
                            var empRef = dtSpList.Rows[i]["EmpRefNo"].ToString();
                            var supNameId = dtSpList.Rows[i]["EmpName"].ToString() + " - [" + dtSpList.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtSpList.Rows[i]["DesigName"].ToString() + "]";
                            ddlSupervisor.Items.Add(new ListItem(supNameId, empRef));
                        }
                        var dtSpListNew = taSpList.GetDataByEmpRef(dtSp[0].Sp_Supr_Ref.ToString());
                        if (dtSpListNew.Rows.Count > 0)
                        {
                            var empRef = dtSpListNew.Rows[0]["EmpRefNo"].ToString();
                            var supNameId = dtSpListNew.Rows[0]["EmpName"].ToString() + " - [" + dtSpListNew.Rows[0]["EmpId"].ToString() + "]" + " - [" + dtSpListNew.Rows[0]["DesigName"].ToString() + "]";
                            ddlSupervisor.Items.Add(new ListItem(supNameId, empRef));
                        }
                    }                    
                    ddlSupervisor.Items.Insert(0, new ListItem("---Select---", "0"));
                    ddlSupervisor.SelectedIndex = ddlSupervisor.Items.IndexOf(ddlSupervisor.Items.FindByValue(dtSp[0].Sp_Supr_Ref.ToString()));
                    ddlDgm.SelectedIndex = ddlDgm.Items.IndexOf(ddlDgm.Items.FindByValue(dtSp[0].Sp_Flag1.ToString()));                    
                }
            }
            catch (Exception ex) 
            {
                hfSpEditRef.Value = "0";

                txtSpId.Text = hfSpEditRef.Value.ToString();

                txtSpShortName.Text = "";
                txtSpFullName.Text = "";                             
                txtSpCell.Text = "";
                txtSpRemarks.Text = "";
                txtInactiveDate.Text = "";

                optActive.Checked = true;
                cboEmpId.SelectedIndex = 0;
                ddlSupervisor.SelectedIndex = 0;
                ddlDgm.SelectedIndex = 0;

                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
        #endregion

        #region GridData
        public string GetSalesZone(int slsZoneRef)
        {
            string slsZone = "";
            try
            {
                var taSlsZone = new tblSalesZoneTableAdapter();
                var dtSlsZone = taSlsZone.GetDataBySlsZoneRef(Convert.ToInt32(slsZoneRef));
                if (dtSlsZone.Rows.Count > 0)
                    slsZone = dtSlsZone[0].SalesZoneName.ToString();
                return slsZone;
            }
            catch (Exception) { return slsZone; }
        }

        public string GetDesignation(string empRef)
        {
            string strDesignation = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    strDesignation = dtEmp[0].DesigName.ToString();
                return strDesignation;
            }
            catch (Exception) { return strDesignation; }
        }

        public string GetSupervisor(string empRef)
        {
            string strSupervisor = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    strSupervisor = dtEmp[0].EmpName.ToString();
                return strSupervisor;
            }
            catch (Exception) { return strSupervisor; }
        }

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

            var taSp = new tblSalesPersonMasTableAdapter();
            var taCust = new tblSalesPartyAdrTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSp.Connection);

            try
            {                
                var spStat = 0;
                if (optActive.Checked)
                    spStat = 1;
                else
                {
                    if (txtInactiveDate.Text.Length <= 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Enter Inactive date first.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                    if (ddlReplaceMpoBy.SelectedIndex==0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Select replaced MPO first.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                    if (txtSpId.Text.Trim() == ddlReplaceMpoBy.SelectedValue.ToString())
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to inactive this MPO.";
                        tblMsg.Rows[1].Cells[0].InnerText = "Inactive MPO and Replaced MPO are same.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                DateTime? inactiveDate = null;
                if (optInactive.Checked) inactiveDate = Convert.ToDateTime(txtInactiveDate.Text.Trim());

                taSp.AttachTransaction(myTran);
                taCust.AttachTransaction(myTran);

                if (hfSpEditRef.Value != "0")
                {
                    taSp.UpdateSp(txtSpShortName.Text.Trim(), txtSpFullName.Text.Trim(), txtSpCell.Text.Trim(), "", txtSpRemarks.Text.Trim(), ddlSupervisor.SelectedValue,
                                    ddlSupervisor.SelectedItem.ToString(), cboEmpId.SelectedValue, DateTime.Now,
                                    Session["sessionUserId"] == null ? 0 : Convert.ToInt32(Session["sessionUserId"].ToString()),
                                    spStat, "", ddlDgm.SelectedValue, ddlDgm.SelectedItem.ToString(), "", spStat, inactiveDate, Convert.ToInt32(hfSpEditRef.Value));

                    if (spStat == 0)                    
                        taCust.UpdateSalesSP(ddlReplaceMpoBy.SelectedValue.ToString(), DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), hfSpEditRef.Value.ToString());

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Sales Person Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtSpName = taSp.GetDataBySpName(txtSpShortName.Text.Trim(), txtSpFullName.Text.Trim());
                    if (dtSpName.Rows.Count > 0)
                    {
                        ViewState["CurrentAlphabet"] = txtSpShortName.Text;
                        this.GenerateAlphabets();
                        gvSpList.PageIndex = 0;
                        this.BindGrid();

                        tblMsg.Rows[0].Cells[0].InnerText = "Sales Person already exists with this name.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    else
                    {
                        var dtSpRef = taSp.GetMaxSpRef();
                        var nextRef = dtSpRef == null ? 1 : Convert.ToInt32(dtSpRef) + 1;

                        taSp.InsertSp(nextRef, txtSpShortName.Text.Trim(), txtSpFullName.Text.Trim(), txtSpCell.Text.Trim(),
                            "", txtSpRemarks.Text.Trim(), ddlSupervisor.SelectedValue,
                            ddlSupervisor.SelectedItem.ToString(), cboEmpId.SelectedValue, DateTime.Now,
                            Session["sessionUserId"] == null ? 0 : Convert.ToInt32(Session["sessionUserId"].ToString()),
                            spStat, "", ddlDgm.SelectedValue, ddlDgm.SelectedItem.ToString(), "", spStat, inactiveDate);

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Sales Person Added Successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }

                hfSpEditRef.Value = "0";

                txtSrchEmp.Text = "";
                txtSpShortName.Text = "";
                txtSpFullName.Text = "";
                txtSpCell.Text = "";
                txtSpRemarks.Text = "";
                txtInactiveDate.Text = "";
                optActive.Checked = true;
                optInactive.Checked = false;
                ddlReplaceMpoBy.Enabled = false;
                
                ddlSupervisor.Items.Clear();
                ddlReplaceSupervisorList.Items.Clear();
                ddlReplaceSupervisorBy.Items.Clear();

                ddlReplaceMpoBy.Items.Clear();
                ddlReplaceMpoList.Items.Clear();
                ddlReplaceMpoByList.Items.Clear();

                var taSpList = new View_Sales_Sp_List_Emp_NameTableAdapter();
                var dtSpList = taSpList.GetDataByListAllBySpStat(Convert.ToInt32(1));
                if (dtSpList.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSpList.Rows.Count; i++)
                    {
                        var spRef = dtSpList.Rows[i]["Sp_Ref"].ToString();
                        var empRef = dtSpList.Rows[i]["EmpRefNo"].ToString();
                        var supNameId = dtSpList.Rows[i]["EmpName"].ToString() + " - [" + dtSpList.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtSpList.Rows[i]["DesigName"].ToString() + "]";
                        ddlSupervisor.Items.Add(new ListItem(supNameId, empRef));

                        ddlReplaceMpoBy.Items.Add(new ListItem(supNameId, spRef));

                        ddlReplaceMpoList.Items.Add(new ListItem(supNameId, spRef));
                        ddlReplaceMpoByList.Items.Add(new ListItem(supNameId, spRef));

                        ddlReplaceSupervisorBy.Items.Add(new ListItem(supNameId, empRef));
                    }
                }
                ddlSupervisor.Items.Insert(0, new ListItem("---Select---", "0"));

                ddlReplaceMpoBy.Items.Insert(0, new ListItem("---Select---", "0"));

                ddlReplaceMpoList.Items.Insert(0, new ListItem("---Select---", "0"));
                ddlReplaceMpoByList.Items.Insert(0, new ListItem("---Select---", "0"));

                ddlReplaceSupervisorBy.Items.Insert(0, new ListItem("---Select---", "0"));

                var taSpSupListAll = new View_Sales_Sp_Supr_List_AllTableAdapter();
                var dtSpSupListAll = taSpSupListAll.GetData();
                if (dtSpSupListAll.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSpSupListAll.Rows.Count; i++)
                    {
                        var empRef = dtSpSupListAll.Rows[i]["EmpRefNo"].ToString();
                        var supNameId = dtSpSupListAll.Rows[i]["EmpName"].ToString() + " - [" + dtSpSupListAll.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtSpSupListAll.Rows[i]["DesigName"].ToString() + "]";
                        ddlReplaceSupervisorList.Items.Add(new ListItem(supNameId, empRef));
                    }
                }
                ddlReplaceSupervisorList.Items.Insert(0, new ListItem("---Select---", "0"));

                cboEmpId.SelectedIndex = 0;

                ddlSupervisor.SelectedIndex = 0;
                ddlReplaceSupervisorList.SelectedIndex = 0;
                ddlReplaceSupervisorBy.SelectedIndex = 0;

                ddlReplaceMpoBy.SelectedIndex = 0;
                ddlReplaceMpoList.SelectedIndex = 0;
                ddlReplaceMpoByList.SelectedIndex = 0;

                ddlDgm.SelectedIndex = 0;

                var dtMaxSpRef = taSp.GetMaxSpRef();
                var nextSpRef = dtMaxSpRef == null ? 1 : Convert.ToInt32(dtMaxSpRef) + 1;
                txtSpId.Text = nextSpRef.ToString();                

                ViewState["CurrentAlphabet"] = "ALL";
                GenerateAlphabets();
                BindGrid();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                tblMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvSpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSpList.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        private void GenerateAlphabets()
        {
            List<ListItem> alphabets = new List<ListItem>();
            ListItem alphabet = new ListItem();
            alphabet.Value = "ALL";
            alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
            alphabets.Add(alphabet);
            for (int i = 65; i <= 90; i++)
            {
                alphabet = new ListItem();
                alphabet.Value = Char.ConvertFromUtf32(i);
                alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
                alphabets.Add(alphabet);
            }
            rptAlphabets.DataSource = alphabets;
            rptAlphabets.DataBind();
        }

        private void BindGrid()
        {
            string strConnString = ConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [View_Sales_Sp] WHERE (Sp_Short_Name LIKE @Alphabet + '%' OR @Alphabet = 'ALL') OR (Sp_Full_Name LIKE @Alphabet + '%' OR @Alphabet = 'ALL') Order BY [Sp_Is_Active] DESC,[SalesZoneName],[Sp_Full_Name]"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Alphabet", ViewState["CurrentAlphabet"]);
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvSpList.DataSource = dt;
                            gvSpList.DataBind();

                            gvSpList.SelectedIndex = -1;
                        }
                    }
                }
            }
        }

        protected void Alphabet_Click(object sender, EventArgs e)
        {
            LinkButton lnkAlphabet = (LinkButton)sender;
            ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
            this.GenerateAlphabets();
            gvSpList.PageIndex = 0;
            this.BindGrid();
        }

        protected void gvSpList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvSpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvSpList.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    txtSrchEmp.Text = "";

                    HiddenField hfPartyRef = (HiddenField)gvSpList.Rows[indx].FindControl("hfSpRef");
                    var partyRef = hfPartyRef.Value;

                    hfSpEditRef.Value = partyRef.ToString();

                    GetData();
                }
                catch (Exception ex)
                {
                    hfSpEditRef.Value = "0";
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                    tblMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void btnSearchEmp_Click(object sender, EventArgs e)
        {
            try
            {
                var empRef = "";
                var srchWords = txtSrchEmp.Text.Trim().Split(':');
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
                        var taSp = new tblSalesPersonMasTableAdapter();
                        var dtSp = taSp.GetDataByEmpRef(empRef.ToString());
                        if (dtSp.Rows.Count > 0)
                        {
                            btnClearEmp.Visible = true;

                            hfSpEditRef.Value = dtSp[0].Sp_Ref.ToString();

                            GetData();

                            tblMsg.Rows[0].Cells[0].InnerText = "Sales Person account already created with this employee.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                        else
                        {
                            #region GetEmpDetails
                            var taEmp = new View_Emp_BascTableAdapter();
                            var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                            if (dtEmp.Rows.Count > 0)
                            {
                                txtSpFullName.Text = dtEmp[0].EmpName.ToString();
                                txtSpCell.Text = dtEmp[0].EmpCellPhone.ToString();

                                cboEmpId.SelectedValue = dtEmp[0].EmpRefNo.ToString();
                                //ddlSupervisor.SelectedValue = dtEmp[0].EmpSuprId.ToString();

                                //var dtEmpSup = taEmp.GetDataByEmpRef(Convert.ToInt32(dtEmp[0].EmpSuprId));
                                //ddlDgm.SelectedValue = dtEmpSup.Rows.Count > 0 ? dtEmpSup[0].EmpRefNo.ToString() : "0";
                            }
                            else
                            {
                                txtSpFullName.Text = "";
                                txtSpCell.Text = "";
                            }
                            #endregion
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
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hfSpEditRef.Value = "0";

            txtSrchEmp.Text = "";

            txtSpShortName.Text = "";
            txtSpFullName.Text = "";
            txtSpCell.Text = "";
            txtSpRemarks.Text = "";
            txtInactiveDate.Text = "";
            optActive.Checked = true;
            optInactive.Checked = false;
            ddlReplaceMpoBy.Enabled = false;

            ddlSupervisor.Items.Clear();
            ddlReplaceSupervisorList.Items.Clear();
            ddlReplaceSupervisorBy.Items.Clear();

            ddlReplaceMpoBy.Items.Clear();
            ddlReplaceMpoList.Items.Clear();
            ddlReplaceMpoByList.Items.Clear();

            var taSpList = new View_Sales_Sp_List_Emp_NameTableAdapter();
            var dtSpList = taSpList.GetDataByListAllBySpStat(Convert.ToInt32(1));
            if (dtSpList.Rows.Count > 0)
            {
                for (int i = 0; i < dtSpList.Rows.Count; i++)
                {
                    var spRef = dtSpList.Rows[i]["Sp_Ref"].ToString();
                    var empRef = dtSpList.Rows[i]["EmpRefNo"].ToString();
                    var supNameId = dtSpList.Rows[i]["EmpName"].ToString() + " - [" + dtSpList.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtSpList.Rows[i]["DesigName"].ToString() + "]";
                    ddlSupervisor.Items.Add(new ListItem(supNameId, empRef));

                    ddlReplaceMpoBy.Items.Add(new ListItem(supNameId, spRef));

                    ddlReplaceMpoList.Items.Add(new ListItem(supNameId, spRef));
                    ddlReplaceMpoByList.Items.Add(new ListItem(supNameId, spRef));

                    ddlReplaceSupervisorBy.Items.Add(new ListItem(supNameId, empRef));
                }
            }
            ddlSupervisor.Items.Insert(0, new ListItem("---Select---", "0"));

            ddlReplaceMpoBy.Items.Insert(0, new ListItem("---Select---", "0"));

            ddlReplaceMpoList.Items.Insert(0, new ListItem("---Select---", "0"));
            ddlReplaceMpoByList.Items.Insert(0, new ListItem("---Select---", "0"));

            ddlReplaceSupervisorBy.Items.Insert(0, new ListItem("---Select---", "0"));

            var taSpSupListAll = new View_Sales_Sp_Supr_List_AllTableAdapter();
            var dtSpSupListAll = taSpSupListAll.GetData();
            if (dtSpSupListAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtSpSupListAll.Rows.Count; i++)
                {
                    var empRef = dtSpSupListAll.Rows[i]["EmpRefNo"].ToString();
                    var supNameId = dtSpSupListAll.Rows[i]["EmpName"].ToString() + " - [" + dtSpSupListAll.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtSpSupListAll.Rows[i]["DesigName"].ToString() + "]";
                    ddlReplaceSupervisorList.Items.Add(new ListItem(supNameId, empRef));
                }
            }
            ddlReplaceSupervisorList.Items.Insert(0, new ListItem("---Select---", "0"));

            cboEmpId.SelectedIndex = 0;

            ddlSupervisor.SelectedIndex = 0;
            ddlReplaceSupervisorList.SelectedIndex = 0;
            ddlReplaceSupervisorBy.SelectedIndex = 0;

            ddlReplaceMpoBy.SelectedIndex = 0;
            ddlReplaceMpoList.SelectedIndex = 0;
            ddlReplaceMpoByList.SelectedIndex = 0;

            ddlDgm.SelectedIndex = 0;

            ViewState["CurrentAlphabet"] = "ALL";
            GenerateAlphabets();
            BindGrid();
        }

        protected void btnClearEmp_Click(object sender, EventArgs e)
        {
            hfSpEditRef.Value = "0";

            txtSrchEmp.Text = "";

            txtSpShortName.Text = "";
            txtSpFullName.Text = "";
            txtSpCell.Text = "";
            txtSpRemarks.Text = "";
            txtInactiveDate.Text = "";
            optActive.Checked = true;
            optInactive.Checked = false;
            ddlReplaceMpoBy.Enabled = false;

            ddlSupervisor.Items.Clear();
            ddlReplaceSupervisorList.Items.Clear();
            ddlReplaceSupervisorBy.Items.Clear();

            ddlReplaceMpoBy.Items.Clear();
            ddlReplaceMpoList.Items.Clear();
            ddlReplaceMpoByList.Items.Clear();

            var taSpList = new View_Sales_Sp_List_Emp_NameTableAdapter();
            var dtSpList = taSpList.GetDataByListAllBySpStat(Convert.ToInt32(1));
            if (dtSpList.Rows.Count > 0)
            {
                for (int i = 0; i < dtSpList.Rows.Count; i++)
                {
                    var spRef = dtSpList.Rows[i]["Sp_Ref"].ToString();
                    var empRef = dtSpList.Rows[i]["EmpRefNo"].ToString();
                    var supNameId = dtSpList.Rows[i]["EmpName"].ToString() + " - [" + dtSpList.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtSpList.Rows[i]["DesigName"].ToString() + "]";
                    ddlSupervisor.Items.Add(new ListItem(supNameId, empRef));

                    ddlReplaceMpoBy.Items.Add(new ListItem(supNameId, spRef));

                    ddlReplaceMpoList.Items.Add(new ListItem(supNameId, spRef));
                    ddlReplaceMpoByList.Items.Add(new ListItem(supNameId, spRef));

                    ddlReplaceSupervisorBy.Items.Add(new ListItem(supNameId, empRef));
                }
            }
            ddlSupervisor.Items.Insert(0, new ListItem("---Select---", "0"));

            ddlReplaceMpoBy.Items.Insert(0, new ListItem("---Select---", "0"));

            ddlReplaceMpoList.Items.Insert(0, new ListItem("---Select---", "0"));
            ddlReplaceMpoByList.Items.Insert(0, new ListItem("---Select---", "0"));

            ddlReplaceSupervisorBy.Items.Insert(0, new ListItem("---Select---", "0"));

            var taSpSupListAll = new View_Sales_Sp_Supr_List_AllTableAdapter();
            var dtSpSupListAll = taSpSupListAll.GetData();
            if (dtSpSupListAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtSpSupListAll.Rows.Count; i++)
                {
                    var empRef = dtSpSupListAll.Rows[i]["EmpRefNo"].ToString();
                    var supNameId = dtSpSupListAll.Rows[i]["EmpName"].ToString() + " - [" + dtSpSupListAll.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtSpSupListAll.Rows[i]["DesigName"].ToString() + "]";
                    ddlReplaceSupervisorList.Items.Add(new ListItem(supNameId, empRef));
                }
            }
            ddlReplaceSupervisorList.Items.Insert(0, new ListItem("---Select---", "0"));

            cboEmpId.SelectedIndex = 0;

            ddlSupervisor.SelectedIndex = 0;
            ddlReplaceSupervisorList.SelectedIndex = 0;
            ddlReplaceSupervisorBy.SelectedIndex = 0;

            ddlReplaceMpoBy.SelectedIndex = 0;
            ddlReplaceMpoList.SelectedIndex = 0;
            ddlReplaceMpoByList.SelectedIndex = 0;

            ViewState["CurrentAlphabet"] = "ALL";
            GenerateAlphabets();
            BindGrid();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var qryStr = "SELECT [Sp_Ref_Str] as MPO_Ref,Sp_User_Ref as Emp_ID, [Sp_Full_Name] as Full_Name, [Sp_Short_Name] as Short_Name, " +
                             "SP.DesigName as Designation, [Sp_Cell_No] as Cell_No, [Sp_Phone_No] as Phone_No, [Sp_Remarks] as Remarks,Sp_Supr_Ref as Supervisor_ID, " +
                             "SUP.EmpName Supervisor_Name, SUP.DesigName as Suprvisor_Designation, [Dsm_Full_Name] as DSM_Name, [SalesZoneName] as Sales_Zone, " +
                             "(case when [Sp_Status]='1' then 'Active' else 'Inactive' end) as [Status], convert(date,[Sp_Inactive_Date],103) as Inactive_Date " +
                             "FROM [DRN].[dbo].[View_Sales_Sp] left outer join View_Emp_Basc SUP on Sp_Supr_Ref=SUP.EmpRefNo left outer join View_Emp_Basc SP " +
                             "on Sp_User_Ref=SP.EmpRefNo Order BY [Sp_Is_Active] DESC,[SalesZoneName],[Sp_Full_Name]";

                SqlCommand cmd = new SqlCommand(qryStr);
                DataTable dt = GetData(cmd);

                if (dt.Rows.Count > 65535)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                    return;
                }

                //First Method
                #region With Formating
                Response.Clear();
                Response.Buffer = true;
                string filename = String.Format("MPO_List_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
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

        protected void btnClearReplace_Click(object sender, EventArgs e)
        {
            ddlReplaceSupervisorList.SelectedIndex = 0;
            ddlReplaceSupervisorBy.SelectedIndex = 0;
        }

        protected void btnReplaceSupervisor_Click(object sender, EventArgs e)
        {
            var taReplace = new tblSalesPersonMasTableAdapter();
            try
            {
                if (ddlReplaceSupervisorList.SelectedValue.ToString() == ddlReplaceSupervisorBy.SelectedValue.ToString())
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to replace by own.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }

                taReplace.UpdateSupervisor(ddlReplaceSupervisorBy.SelectedValue.ToString(), ddlReplaceSupervisorBy.SelectedItem.ToString(), DateTime.Now,
                    Session["sessionUserId"] == null ? 0 : Convert.ToInt32(Session["sessionUserId"].ToString()), ddlReplaceSupervisorList.SelectedValue.ToString(), 1);

                ddlReplaceSupervisorList.Items.Clear();
                var taSpSupListAll = new View_Sales_Sp_Supr_List_AllTableAdapter();
                var dtSpSupListAll = taSpSupListAll.GetData();
                if (dtSpSupListAll.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSpSupListAll.Rows.Count; i++)
                    {
                        var empRef = dtSpSupListAll.Rows[i]["EmpRefNo"].ToString();
                        var supNameId = dtSpSupListAll.Rows[i]["EmpName"].ToString() + " - [" + dtSpSupListAll.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtSpSupListAll.Rows[i]["DesigName"].ToString() + "]";
                        ddlReplaceSupervisorList.Items.Add(new ListItem(supNameId, empRef));
                    }
                }
                ddlReplaceSupervisorList.Items.Insert(0, new ListItem("---Select---", "0"));

                ddlReplaceSupervisorBy.SelectedIndex = 0;

                tblMsg.Rows[0].Cells[0].InnerText = "Supervisor Replaced Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void optInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (optInactive.Checked)
            {
                txtInactiveDate.Enabled = true;
                ddlReplaceMpoBy.Enabled = true;
            }
            else
            {
                txtInactiveDate.Text = "";
                txtInactiveDate.Enabled = false;
                ddlReplaceMpoBy.SelectedIndex = 0;
                ddlReplaceMpoBy.Enabled = false;
            }
        }

        protected void optActive_CheckedChanged(object sender, EventArgs e)
        {
            if (optActive.Checked)
            {
                txtInactiveDate.Text = "";
                txtInactiveDate.Enabled = false;
                ddlReplaceMpoBy.SelectedIndex = 0;
                ddlReplaceMpoBy.Enabled = false;
            }
            else
            {                
                txtInactiveDate.Enabled = true;
                ddlReplaceMpoBy.Enabled = true;
            }
        }

        protected void btnReplaceMpo_Click(object sender, EventArgs e)
        {
             var taCust = new tblSalesPartyAdrTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taCust.Connection);

            try
            {
                if (ddlReplaceMpoList.SelectedIndex == 0)
                    return;
                if (ddlReplaceMpoByList.SelectedIndex == 0)
                    return;

                if (ddlReplaceMpoList.SelectedValue.ToString() == ddlReplaceMpoByList.SelectedValue.ToString())
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to Replace this MPO.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Previous MPO and Replaced MPO are same.";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                taCust.AttachTransaction(myTran);

                taCust.UpdateSalesSP(ddlReplaceMpoByList.SelectedValue.ToString(), DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlReplaceMpoList.SelectedValue.ToString());

                ddlReplaceMpoList.SelectedIndex = 0;
                ddlReplaceMpoByList.SelectedIndex = 0;

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "MPO Replaced Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearReplaceMpo_Click(object sender, EventArgs e)
        {
            ddlReplaceMpoList.SelectedIndex = 0;
            ddlReplaceMpoByList.SelectedIndex = 0;
        }
    }
}