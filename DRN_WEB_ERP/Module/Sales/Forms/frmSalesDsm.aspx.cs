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
    public partial class frmSalesDsm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                var taDsm = new tblSalesDsmMasTableAdapter();
                var dtDsmRef = taDsm.GetMaxDsmRef();
                var nextRef = dtDsmRef == null ? 100001 : Convert.ToInt32(dtDsmRef) + 1;
                txtSpId.Text = nextRef.ToString();

                var taEmpList = new View_Emp_BascTableAdapter();
                var dtEmp = taEmpList.GetDataByAsc();
                if (dtEmp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmp.Rows.Count; i++)
                    {
                        var empRef = dtEmp.Rows[i]["EmpRefNo"].ToString();
                        var empName = dtEmp.Rows[i]["EmpName"].ToString() + " - [" + dtEmp.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtEmp.Rows[i]["DesigName"].ToString() + "]";
                        cboEmpId.Items.Add(new ListItem(empName, empRef));
                    }
                }
                cboEmpId.Items.Insert(0, new ListItem("---Select---", "0"));

                var dtSup = taEmpList.GetDataByAsc();
                if (dtSup.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSup.Rows.Count; i++)
                    {
                        var empRef = dtSup.Rows[i]["EmpRefNo"].ToString();
                        var supName = dtSup.Rows[i]["EmpName"].ToString() + " - [" + dtSup.Rows[i]["EmpId"].ToString() + "]" + " - [" + dtEmp.Rows[i]["DesigName"].ToString() + "]";
                        ddlSupervisor.Items.Add(new ListItem(supName, empRef));
                    }
                }
                ddlSupervisor.Items.Insert(0, new ListItem("---Select---", "0"));

                var taSalesZone = new tblSalesZoneTableAdapter();
                var dtSalesZone = taSalesZone.GetDataByAsc();
                foreach (dsSalesMas.tblSalesZoneRow dr in dtSalesZone.Rows)
                {
                    ddlSalesZone.Items.Add(new ListItem(dr.SalesZoneCode.ToString() + " :: " + dr.SalesZoneName.ToString(), dr.SalesZoneRef.ToString()));
                }
                ddlSalesZone.Items.Insert(0, new ListItem("---Select---", "0"));

                var dtRespDsm = taDsm.GetActiveDsm();
                foreach (dsSalesMas.tblSalesDsmMasRow dr in dtRespDsm.Rows)
                {
                    ddlRespDsm.Items.Add(new ListItem(dr.Dsm_Short_Name.ToString() + " :: " + dr.Dsm_Full_Name.ToString(), dr.Dsm_Ref.ToString()));
                }
                ddlRespDsm.Items.Insert(0, new ListItem("---Select---", "0"));


                hfDsmEditRef.Value = "0";

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
                var taDsm = new tblSalesDsmMasTableAdapter();
                var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(hfDsmEditRef.Value));
                if (dtDsm.Rows.Count > 0)
                {
                    txtSpId.Text = hfDsmEditRef.Value.ToString();

                    txtDsmShortName.Text = dtDsm[0].Dsm_Short_Name.ToString();
                    txtSpFullName.Text = dtDsm[0].Dsm_Full_Name.ToString();
                    txtSpCell.Text = dtDsm[0].Dsm_Cell_No.ToString();
                    txtSpRemarks.Text = dtDsm[0].Dsm_Remarks.ToString();
                    
                    if (dtDsm[0].Dsm_Is_Active == 1)
                    {
                        //optActive.Checked = true;
                        optListStat.SelectedValue = "1";
                        txtInactiveDate.Text = "";
                        txtInactiveDate.Enabled = false;                        
                    }
                    else
                    {
                        //optInactive.Checked = true;
                        optListStat.SelectedValue = "0";
                        txtInactiveDate.Text = dtDsm[0].IsDsm_Inactive_DateNull() ? "" : dtDsm[0].Dsm_Inactive_Date.ToString("dd/MM/yyyy");
                        txtInactiveDate.Enabled = true;
                    }
                    ddlRespDsm.SelectedIndex = 0;
                    ddlRespDsm.Enabled = false;

                    cboEmpId.SelectedIndex = cboEmpId.Items.IndexOf(cboEmpId.Items.FindByValue(dtDsm[0].Dsm_Emp_Ref.ToString()));
                    ddlSupervisor.SelectedIndex = ddlSupervisor.Items.IndexOf(ddlSupervisor.Items.FindByValue(dtDsm[0].Dsm_Supr_Ref.ToString()));
                    ddlSalesZone.SelectedIndex = ddlSalesZone.Items.IndexOf(ddlSalesZone.Items.FindByValue(dtDsm[0].Dsm_Sls_Zone.ToString()));
                }
            }
            catch (Exception ex)
            {
                hfDsmEditRef.Value = "0";

                txtSpId.Text = hfDsmEditRef.Value.ToString();

                txtDsmShortName.Text = "";
                txtSpFullName.Text = "";
                txtSpCell.Text = "";
                txtSpRemarks.Text = "";
                txtInactiveDate.Text = "";

                //optActive.Checked = true;
                optListStat.SelectedValue = "1";
                cboEmpId.SelectedIndex = 0;
                ddlSupervisor.SelectedIndex = 0;
                ddlSalesZone.SelectedIndex = 0;
                ddlRespDsm.SelectedIndex = 0;
                ddlRespDsm.Enabled = false;

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

            var taDsm = new tblSalesDsmMasTableAdapter();
            var taSp = new tblSalesPersonMasTableAdapter();
            var taParAdr = new tblSalesPartyAdrTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taDsm.Connection);

            try
            {
                taDsm.AttachTransaction(myTran);
                taParAdr.AttachTransaction(myTran);
                
                if (optListStat.SelectedValue == "0")
                {
                    if (ddlRespDsm.SelectedIndex == 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Select Next Responsible DSM First.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    if (txtInactiveDate.Text.Trim() == "")
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Enter Inactive Date First.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
              
                DateTime? inactiveDate = null;
                if (optListStat.SelectedValue == "0") inactiveDate = Convert.ToDateTime(txtInactiveDate.Text.Trim());

                var slsZone = "";
                if (hfDsmEditRef.Value != "0")
                {                    
                    var dtSlsZome = taDsm.GetDataByDsmRef(Convert.ToInt32(hfDsmEditRef.Value));
                    if (dtSlsZome.Rows.Count > 0)
                    {
                        slsZone = dtSlsZome[0].Dsm_Sls_Zone.ToString();
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Next Responsible DSM selection is not valid.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (hfDsmEditRef.Value != "0")
                {
                    if (ddlRespDsm.SelectedIndex != 0)
                    {
                        var nextRespDsmRef = "";
                        var nextRespDsmShortName = "";
                        //var nextRespDsmFullName = "";
                        //var nextRespDsmSalesZone = "";

                        var dtNextRespDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(ddlRespDsm.SelectedValue.ToString()));
                        if (dtNextRespDsm.Rows.Count > 0)
                        {
                            nextRespDsmRef = dtNextRespDsm[0].Dsm_Ref.ToString();
                            nextRespDsmShortName = dtNextRespDsm[0].Dsm_Short_Name.ToString();
                            //nextRespDsmFullName = dtNextRespDsm[0].Dsm_Full_Name.ToString();
                            //nextRespDsmSalesZone = dtNextRespDsm[0].Dsm_Ref.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Next Responsible DSM selection is not valid.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }

                        taDsm.UpdateDsm(txtDsmShortName.Text.Trim(), txtSpFullName.Text.Trim(), ddlSalesZone.SelectedValue.ToString(), txtSpCell.Text.Trim(), "",
                                    txtSpRemarks.Text.Trim(), ddlSupervisor.SelectedValue.ToString(), ddlSupervisor.SelectedItem.ToString(), "0",
                                    cboEmpId.SelectedValue.ToString(), DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                    Convert.ToInt32(optListStat.SelectedValue), "", "", "", ddlRespDsm.SelectedItem.ToString(), Convert.ToInt32(optListStat.SelectedValue),
                                    inactiveDate, Convert.ToInt32(hfDsmEditRef.Value));

                        taSp.UpdateDsmInfo(ddlRespDsm.SelectedValue.ToString(), nextRespDsmShortName.ToString() + " :: " + ddlSalesZone.SelectedItem.ToString(), DateTime.Now,
                                    Convert.ToInt32(Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString()), hfDsmEditRef.Value.ToString());

                        taParAdr.UpdateSalesDsm(Convert.ToInt32(slsZone), ddlRespDsm.SelectedValue.ToString(), DateTime.Now,
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), hfDsmEditRef.Value.ToString());
                    }
                    else
                    {
                        taDsm.UpdateDsm(txtDsmShortName.Text.Trim(), txtSpFullName.Text.Trim(), ddlSalesZone.SelectedValue.ToString(), txtSpCell.Text.Trim(), "",
                                    txtSpRemarks.Text.Trim(), ddlSupervisor.SelectedValue.ToString(), ddlSupervisor.SelectedItem.ToString(), "0",
                                    cboEmpId.SelectedValue.ToString(), DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                    Convert.ToInt32(optListStat.SelectedValue), "", "", "", "", Convert.ToInt32(optListStat.SelectedValue),
                                    inactiveDate, Convert.ToInt32(hfDsmEditRef.Value));
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "DSM Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtDsmName = taDsm.GetDataByDsmName(txtDsmShortName.Text.Trim(), txtSpFullName.Text.Trim());
                    if (dtDsmName.Rows.Count > 0)
                    {
                        ViewState["CurrentAlphabet"] = txtDsmShortName.Text;
                        this.GenerateAlphabets();
                        gvDsmList.PageIndex = 0;
                        this.BindGrid();

                        tblMsg.Rows[0].Cells[0].InnerText = "DSM already exists with this name.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    else
                    {
                        taDsm.InsertDsm(txtDsmShortName.Text.Trim(), txtSpFullName.Text.Trim(), ddlSalesZone.SelectedValue.ToString(), txtSpCell.Text.Trim(), "",
                                        txtSpRemarks.Text.Trim(), ddlSupervisor.SelectedValue.ToString(), ddlSupervisor.SelectedItem.ToString(), "0",
                                        cboEmpId.SelectedValue.ToString(), DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                        Convert.ToInt32(optListStat.SelectedValue), "", "", "", "", Convert.ToInt32(optListStat.SelectedValue), inactiveDate);

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "DSM Added Successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }

                hfDsmEditRef.Value = "0";

                txtSrchEmp.Text = "";

                txtDsmShortName.Text = "";
                txtSpFullName.Text = "";
                txtSpCell.Text = "";
                txtSpRemarks.Text = "";
                cboEmpId.SelectedIndex = 0;
                ddlSupervisor.SelectedIndex = 0;
                ddlSalesZone.SelectedIndex = 0;
                //optActive.Checked = true;
                //optInactive.Checked = false;
                optListStat.SelectedValue = "1";
                txtInactiveDate.Text = "";
                txtInactiveDate.Enabled = false;
                ddlRespDsm.SelectedIndex = 0;
                ddlRespDsm.Enabled = false;

                ViewState["CurrentAlphabet"] = "ALL";
                GenerateAlphabets();
                BindGrid();
                
                var dtMaxSpRef = taDsm.GetMaxDsmRef();
                var nextSpRef = dtMaxSpRef == null ? 100001 : Convert.ToInt32(dtMaxSpRef) + 1;
                txtSpId.Text = nextSpRef.ToString();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                tblMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvDsmList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDsmList.PageIndex = e.NewPageIndex;
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblSalesDsmMas WHERE (Dsm_Short_Name LIKE @Alphabet + '%' OR @Alphabet = 'ALL') OR (Dsm_Full_Name LIKE @Alphabet + '%' OR @Alphabet = 'ALL') ORDER BY Dsm_Is_Active DESC, Dsm_Sls_Zone, Dsm_Short_Name"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Alphabet", ViewState["CurrentAlphabet"]);
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvDsmList.DataSource = dt;
                            gvDsmList.DataBind();

                            gvDsmList.SelectedIndex = -1;
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
            gvDsmList.PageIndex = 0;
            this.BindGrid();
        }

        protected void gvDsmList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvDsmList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvDsmList.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    txtSrchEmp.Text = "";

                    HiddenField hfPartyRef = (HiddenField)gvDsmList.Rows[indx].FindControl("hfDsmRef");
                    var partyRef = hfPartyRef.Value;

                    hfDsmEditRef.Value = partyRef.ToString();

                    GetData();
                }
                catch (Exception ex)
                {
                    hfDsmEditRef.Value = "0";
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
                        var taDsm = new tblSalesDsmMasTableAdapter();
                        var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(hfDsmEditRef.Value));
                        if (dtDsm.Rows.Count > 0)
                        {
                            btnClearEmp.Visible = true;

                            txtDsmShortName.Text = dtDsm[0].Dsm_Short_Name.ToString();
                            txtSpFullName.Text = dtDsm[0].Dsm_Full_Name.ToString();
                            txtSpCell.Text = dtDsm[0].Dsm_Cell_No.ToString();
                            txtSpRemarks.Text = dtDsm[0].Dsm_Remarks.ToString();

                            if (dtDsm[0].Dsm_Is_Active == 1)
                            {
                                //optActive.Checked = true;
                                optListStat.SelectedValue = "1";
                                txtInactiveDate.Text = "";
                                txtInactiveDate.Enabled = false;
                            }
                            else
                            {
                                //optInactive.Checked = true;
                                optListStat.SelectedValue = "0";
                                txtInactiveDate.Text = dtDsm[0].IsDsm_Inactive_DateNull() ? "" : dtDsm[0].Dsm_Inactive_Date.ToString("dd/MM/yyyy");
                                txtInactiveDate.Enabled = true;
                            }
                            ddlRespDsm.SelectedIndex = 0;
                            ddlRespDsm.Enabled = false;

                            cboEmpId.SelectedIndex = cboEmpId.Items.IndexOf(cboEmpId.Items.FindByValue(dtDsm[0].Dsm_User_Ref.ToString()));
                            ddlSupervisor.SelectedIndex = ddlSupervisor.Items.IndexOf(ddlSupervisor.Items.FindByValue(dtDsm[0].Dsm_Supr_Ref.ToString()));
                            ddlSalesZone.SelectedIndex = ddlSalesZone.Items.IndexOf(ddlSalesZone.Items.FindByValue(dtDsm[0].Dsm_Sls_Zone.ToString()));                            

                            tblMsg.Rows[0].Cells[0].InnerText = "DSM already created with this employee.";
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
                                ddlSupervisor.SelectedValue = dtEmp[0].EmpSuprId.ToString();

                                //var dtEmpSup = taEmp.GetDataByEmpRef(Convert.ToInt32(dtEmp[0].EmpSuprId));
                                //ddlSalesZone.SelectedValue = dtEmpSup.Rows.Count > 0 ? dtEmpSup[0].EmpRefNo.ToString() : "0";
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
            hfDsmEditRef.Value = "0";

            txtSrchEmp.Text = "";

            txtDsmShortName.Text = "";
            txtSpFullName.Text = "";
            txtSpCell.Text = "";
            txtSpRemarks.Text = "";
            cboEmpId.SelectedIndex = 0;
            ddlSupervisor.SelectedIndex = 0;
            ddlSalesZone.SelectedIndex = 0;
            //optActive.Checked = true;
            //optInactive.Checked = false;
            optListStat.SelectedValue = "1";
            txtInactiveDate.Text = "";
            txtInactiveDate.Enabled = false;
            ddlRespDsm.SelectedIndex = 0;
            ddlRespDsm.Enabled = false;

            ViewState["CurrentAlphabet"] = "ALL";
            GenerateAlphabets();
            BindGrid();
        }

        protected void btnClearEmp_Click(object sender, EventArgs e)
        {
            hfDsmEditRef.Value = "0";

            txtSrchEmp.Text = "";

            txtDsmShortName.Text = "";
            txtSpFullName.Text = "";
            txtSpCell.Text = "";
            txtSpRemarks.Text = "";
            cboEmpId.SelectedIndex = 0;
            ddlSupervisor.SelectedIndex = 0;
            ddlSalesZone.SelectedIndex = 0;
            //optActive1.Checked = true;
            //optInactive1.Checked = false;
            optListStat.SelectedValue = "1";
            txtInactiveDate.Text = "";

            ViewState["CurrentAlphabet"] = "ALL";
            GenerateAlphabets();
            BindGrid();
        }

        protected void optListStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optListStat.SelectedValue == "1")
            {
                txtInactiveDate.Enabled = false;
                ddlRespDsm.SelectedIndex = 0;
                ddlRespDsm.Enabled = false;
            }
            else
            {
                txtInactiveDate.Enabled = true;
                ddlRespDsm.SelectedIndex = 0;
                ddlRespDsm.Enabled = true;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var qryStr = "SELECT [Dsm_Ref] as DSM_Ref_No, [Dsm_Full_Name] as Full_Name, [Dsm_Short_Name] as Short_Name, [SalesZoneName] as Sales_Zone, " +
                             "[EmpName] + ' (' + DesigName + ')' as Supervisor, [Dsm_Cell_No] as Cell_No, [Dsm_Phone_No] as Phone_No, [Dsm_Remarks] as Remarks, " +
                             "(case when [Dsm_Status]='1' then 'Active' else 'Inactive' end) as [Status], convert(date,[Dsm_Inactive_Date],103) as Inactive_Date " +
                             "FROM [DRN].[dbo].[View_Sales_Dsm] left outer join tblSalesZone on Dsm_Sls_Zone=SalesZoneRef left outer join View_Emp_Basc " +
                             "on Dsm_Supr_Ref=EmpRefNo order by Dsm_Status desc,SalesZoneName,Dsm_Full_Name";

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
                string filename = String.Format("DSM_List_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
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
    }
}