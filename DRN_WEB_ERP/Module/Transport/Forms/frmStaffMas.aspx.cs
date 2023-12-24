using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet.dsStaffMasTableAdapters;
using DRN_WEB_ERP.Module.SYS.DataSet;
using DRN_WEB_ERP.Module.SYS.DataSet.dsSysTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmStaffMas : System.Web.UI.Page
    {
        const int maxSize=2097151;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                AutoCompleteExtenderEmp.ContextKey = "0";

                var taEmpGenInfo = new tbl_TrTr_Staff_MasTableAdapter();
                var dtMaxEmpRef = taEmpGenInfo.GetMaxStaffRef();
                var nextEmpRefNo = dtMaxEmpRef == null ? "000001" : (Convert.ToInt32(dtMaxEmpRef) + 1).ToString();
                var nextEmpId = "TRN-" + Convert.ToInt32(nextEmpRefNo).ToString("000000");
                txtEmpId.Text = nextEmpId.ToString();

                #region LoadComboData
                var taGender = new tblHrmsGenderTableAdapter();
                cboEmpGender.DataSource = taGender.GetDataByAsc();
                cboEmpGender.DataValueField = "GenderRef";
                cboEmpGender.DataTextField = "GenderName";
                cboEmpGender.DataBind();
                cboEmpGender.Items.Insert(0, new ListItem("---Select---", "0"));

                var taMaritgalStat = new tblHrmsMaritalStatusTableAdapter();
                cboEmpMaritalStat.DataSource = taMaritgalStat.GetDataByAsc();
                cboEmpMaritalStat.DataValueField = "MaritalStatusRef";
                cboEmpMaritalStat.DataTextField = "MaritalStatusName";
                cboEmpMaritalStat.DataBind();
                cboEmpMaritalStat.Items.Insert(0, new ListItem("---Select---", "0"));

                var taBloodGrp = new tblHrmsBldGrpTableAdapter();
                cboBloodGrp.DataSource = taBloodGrp.GetDataByAsc();
                cboBloodGrp.DataValueField = "BldGrpRef";
                cboBloodGrp.DataTextField = "BldGrpName";
                cboBloodGrp.DataBind();
                cboBloodGrp.Items.Insert(0, new ListItem("---Select---", "0"));

                var taReligion = new tblHrmsReligionTableAdapter();
                cboReligion.DataSource = taReligion.GetDataByAsc();
                cboReligion.DataValueField = "ReligionRef";
                cboReligion.DataTextField = "ReligionName";
                cboReligion.DataBind();
                cboReligion.Items.Insert(0, new ListItem("---Select---", "0"));

                var taLoc = new tblHrmsOffLocTableAdapter();
                cboLoc.DataSource = taLoc.GetDataByAsc();
                cboLoc.DataValueField = "LocRefNo";
                cboLoc.DataTextField = "LocName";
                cboLoc.DataBind();
                cboLoc.Items.Insert(0, new ListItem("---Select---", "0"));

                //var taDept = new tblHrmsDeptTableAdapter();
                //cboDept.DataSource = taDept.GetDataByAsc();
                //cboDept.DataValueField = "DeptRefNo";
                //cboDept.DataTextField = "DeptName";
                //cboDept.DataBind();
                //cboDept.Items.Insert(0, new ListItem("---Select---", "0"));

                //var taSec = new tblHrmsSecTableAdapter();
                //cboSec.DataSource = taSec.GetDataByAsc();
                //cboSec.DataValueField = "SecRefNo";
                //cboSec.DataTextField = "SecName";
                //cboSec.DataBind();
                //cboSec.Items.Insert(0, new ListItem("---Select---", "0"));

                var taDesig = new tblHrmsDesigTableAdapter();
                cboDesig.DataSource = taDesig.GetDataByAsc();
                cboDesig.DataValueField = "DesigRefNo";
                cboDesig.DataTextField = "DesigName";
                cboDesig.DataBind();
                cboDesig.Items.Insert(0, new ListItem("---Select---", "0"));

                //var taEmpList = new View_Emp_BascTableAdapter();
                //var dt = taEmpList.GetDataByAsc();
                //if (dt.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        var empRef = dt.Rows[i]["EmpRefNo"].ToString();
                //        var supNameId = dt.Rows[i]["EmpName"].ToString() + " - [" + dt.Rows[i]["EmpId"].ToString() + "]";
                //        cboSupr.Items.Add(new ListItem(supNameId, empRef));
                //    }
                //}
                cboSupr.Items.Insert(0, new ListItem("---Select---", "0"));

                var taJobStat = new tblHrmsJobStatTableAdapter();
                cboJobStat.DataSource = taJobStat.GetDataByAsc();
                cboJobStat.DataValueField = "JobStatRef";
                cboJobStat.DataTextField = "JobStatName";
                cboJobStat.DataBind();
                cboJobStat.Items.Insert(0, new ListItem("---Select---", "0"));

                //var taEmpType = new tblHrmsEmpTypeTableAdapter();
                //cboEmpType.DataSource = taEmpType.GetDataByAsc();
                //cboEmpType.DataValueField = "EmpTypeRef";
                //cboEmpType.DataTextField = "EmpTypeName";
                //cboEmpType.DataBind();
                //cboEmpType.Items.Insert(0, new ListItem("---Select---", "0"));

                var taShift = new tblHrmsShiftTableAdapter();
                cboShift.DataSource = taShift.GetDataByAsc();
                cboShift.DataValueField = "ShiftRefNo";
                cboShift.DataTextField = "ShiftName";
                cboShift.DataBind();
                cboShift.Items.Insert(0, new ListItem("---Select---", "0"));

                var taGrade = new tblHrmsGradeDefTableAdapter();
                cboGrade.DataSource = taGrade.GetDataByAsc();
                cboGrade.DataValueField = "GrdDefRefNo";
                cboGrade.DataTextField = "GrdDefName";
                cboGrade.DataBind();
                cboGrade.Items.Insert(0, new ListItem("---Select---", "0"));

                var taQualType = new tblHrmsQualTypeTableAdapter();
                cboQualType.DataSource = taQualType.GetDataByAsc();
                cboQualType.DataValueField = "QualTypeRefNo";
                cboQualType.DataTextField = "QualTypeName";
                cboQualType.DataBind();
                cboQualType.Items.Insert(0, new ListItem("---Select---", "0"));
                #endregion

                AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabEmpInfo;
                foreach (object obj in container.Controls)
                {
                    if (obj is AjaxControlToolkit.TabPanel)
                    {
                        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                        tabPanel.Enabled = false;
                    }
                }

                TabPnlPersonal.Enabled = true;

                LoadInitEmpQualGridData();
                SetEmpQualGridData();

                LoadInitEmpExpGridData();
                SetEmpExpGridData();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        #region Qualification Gridview Initialization
        protected void LoadInitEmpQualGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("QUAL_TYPE_REF", typeof(string));
            dt.Columns.Add("QUAL_TYPE", typeof(string));
            dt.Columns.Add("EXAM_REF", typeof(string));
            dt.Columns.Add("EXAM_NAME", typeof(string));
            dt.Columns.Add("INST_NAME", typeof(string));
            dt.Columns.Add("GRP_SUB", typeof(string));
            dt.Columns.Add("CERT_AUTH", typeof(string));
            dt.Columns.Add("PASS_YEAR", typeof(string));
            dt.Columns.Add("RESULT", typeof(string));
            dt.Columns.Add("ST_DT", typeof(string));
            dt.Columns.Add("END_DT", typeof(string));
            dt.Columns.Add("DURATION", typeof(string));
            dt.Columns.Add("SEQUENCE", typeof(string));                        
            dt.Columns.Add("EXT_DATA1", typeof(string));
            dt.Columns.Add("EXT_DATA2", typeof(string));
            dt.Columns.Add("EXT_DATA3", typeof(string));
            dt.Columns.Add("STAT_REF", typeof(string));
            dt.Columns.Add("STATUS", typeof(string));
            dt.Columns.Add("FLAG", typeof(string));
            ViewState["dtEmpQual"] = dt;
        }

        protected void SetEmpQualGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtEmpQual"];

                gvEmpQual.DataSource = dt;
                gvEmpQual.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region Experience Gridview Initialization
        protected void LoadInitEmpExpGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EXP_ORG", typeof(string));
            dt.Columns.Add("EXP_DEPT", typeof(string));
            dt.Columns.Add("EXP_DESIG", typeof(string));
            dt.Columns.Add("EXP_ADR", typeof(string));
            dt.Columns.Add("EXP_FROM", typeof(string));
            dt.Columns.Add("EXP_TO", typeof(string));
            dt.Columns.Add("EXP_REM", typeof(string));
            dt.Columns.Add("EXP_REF_NAME", typeof(string));
            dt.Columns.Add("EXP_REF_CONT", typeof(string));
            dt.Columns.Add("EXP_REF_EMAIL", typeof(string));
            ViewState["dtEmpExp"] = dt;
        }

        protected void SetEmpExpGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtEmpExp"];

                gvEmpExp.DataSource = dt;
                gvEmpExp.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion
        
        #region Search Employee
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taEmpGenInfo = new tbl_TrTr_Staff_MasTableAdapter();
            try
            {
                var userRef = Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString();
                var userEmpRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var empRef = "";
                var srchWords = txtSrchEmp.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    empRef = word;
                    break;
                }

                var dtEmpGenInfo = taEmpGenInfo.GetDataBySraffRef(empRef.ToString());
                if (dtEmpGenInfo.Rows.Count > 0)
                {                    
                    btnClear.Visible = true;

                    hfEmpRef.Value = dtEmpGenInfo[0].Staff_RefNo.ToString();

                    txtEmpId.Text = dtEmpGenInfo[0].Staff_Id.ToString();
                    txtEmpFirstName.Text = dtEmpGenInfo[0].Staff_First_Name.ToString();
                    txtEmpLastName.Text = dtEmpGenInfo[0].Staff_Last_Name.ToString();
                    if (userRef == "100001" || userEmpRef == "000011")
                    {
                        txtEmpFirstName.Enabled = true;
                        txtEmpLastName.Enabled = true;
                    }
                    else
                    {
                        txtEmpFirstName.Enabled = false;
                        txtEmpLastName.Enabled = false;
                    }
                    txtEmpFathersName.Text = dtEmpGenInfo[0].Staff_Father_Name.ToString();
                    txtEmpMothersName.Text = dtEmpGenInfo[0].Staff_Mother_Name.ToString();
                    cboEmpGender.SelectedValue = dtEmpGenInfo[0].Staff_Gender.ToString();
                    cboEmpMaritalStat.SelectedValue = dtEmpGenInfo[0].Staff_Marital_Status.ToString();
                    txtEmpSpouseName.Text = dtEmpGenInfo[0].Staff_Spouse_Name.ToString();
                    txtEmpDOB.Text = dtEmpGenInfo[0].IsStaff_DOBNull() ? "" : dtEmpGenInfo[0].Staff_DOB.ToString("dd/MM/yyyy");
                    txtEmpPOB.Text = dtEmpGenInfo[0].Staff_POB.ToString();

                    btnSettle.Visible = dtEmpGenInfo[0].Staff_Setl_Flag.ToString() == "N" ? true : false;

                    cboStaffType.SelectedValue = dtEmpGenInfo[0].Staff_Ext_Data1.ToString();

                    btnUpdatePic.Visible = true;
                    btnUpdateSig.Visible = true;
                    btnUpdateNid.Visible = true;

                    btnDeletePic.Visible = true;
                    btnDeleteSig.Visible = true;
                    btnDeleteNid.Visible = true;

                    btnSaveGenInfo.Visible = true;
                    btnSaveIndeni.Visible = true;
                    btnSaveQual.Visible = true;
                    btnSaveExp.Visible = true;
                    btnSaveOffInfo.Visible = true;
                    btnSaveEmp.Visible = true;

                    hlEmpPicPreview.NavigateUrl = "~/Module/Transport/Tools/getStaffPic.ashx?StaffRefNo='" + empRef + "'";
                    imgEmpPicPreview.ImageUrl = "~/Module/Transport/Tools/getStaffPic.ashx?StaffRefNo='" + empRef + "'";

                    hlEmpPic.NavigateUrl = "~/Module/Transport/Tools/getStaffPic.ashx?StaffRefNo='" + empRef + "'";
                    imgEmpPic.ImageUrl = "~/Module/Transport/Tools/getStaffPic.ashx?StaffRefNo='" + empRef + "'";

                    hlEmpSig.NavigateUrl = "~/Module/Transport/Tools/getStaffDL.ashx?StaffRefNo='" + empRef + "'";
                    imgEmpSig.ImageUrl = "~/Module/Transport/Tools/getStaffDL.ashx?StaffRefNo='" + empRef + "'";

                    hlEmpNid.NavigateUrl = "~/Module/Transport/Tools/getStaffNid.ashx?StaffRefNo='" + empRef + "'";
                    imgEmpNId.ImageUrl = "~/Module/Transport/Tools/getStaffNid.ashx?StaffRefNo='" + empRef + "'";

                    AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabEmpInfo;
                    foreach (object obj in container.Controls)
                    {
                        if (obj is AjaxControlToolkit.TabPanel)
                        {
                            AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                            tabPanel.Enabled = true;
                        }
                    }

                    #region GetAddress
                    var taEmpAdr = new tbl_TrTr_Staff_AdrTableAdapter();
                    var dtEmpAdr = taEmpAdr.GetDataByStaffRef(empRef);
                    if (dtEmpAdr.Rows.Count > 0)
                    {
                        txtEmpPresAdrHouse.Text = dtEmpAdr[0].Staff_Cur_Adr_House.ToString();
                        txtEmpPresAdrPO.Text = dtEmpAdr[0].Staff_Cur_Adr_PO.ToString();
                        txtEmpPresAdrPOCode.Text = dtEmpAdr[0].Staff_Cur_Adr_POCode.ToString();
                        txtEmpPresAdrThana.Text = dtEmpAdr[0].Staff_Cur_Adr_Thana.ToString();
                        txtEmpPresAdrDist.Text = dtEmpAdr[0].Staff_Cur_Adr_Dist.ToString();

                        txtEmpPrmAdrHouse.Text = dtEmpAdr[0].Staff_Perm_Adr_House.ToString();
                        txtEmpPrmAdrPO.Text = dtEmpAdr[0].Staff_Perm_Adr_PO.ToString();
                        txtEmpPrmAdrPOCode.Text = dtEmpAdr[0].Staff_Perm_Adr_POCode.ToString();
                        txtEmpPrmAdrThana.Text = dtEmpAdr[0].Staff_Perm_Adr_Thana.ToString();
                        txtEmpPrmAdrDist.Text = dtEmpAdr[0].Staff_Perm_Adr_Dist.ToString();

                        txtEmpEmerAdrName.Text = dtEmpAdr[0].Staff_Emer_CP_Name.ToString();
                        txtEmpEmerAdrRelation.Text = dtEmpAdr[0].Staff_Emer_CP_Relation.ToString();
                        txtEmpEmerAdrHouse.Text = dtEmpAdr[0].Staff_Emer_CP_House.ToString();
                        txtEmpEmerAdrPO.Text = dtEmpAdr[0].Staff_Emer_CP_PO.ToString();
                        txtEmpEmerAdrPOCode.Text = dtEmpAdr[0].Staff_Emer_CP_POCode.ToString();
                        txtEmpEmerAdrThana.Text = dtEmpAdr[0].Staff_Emer_CP_Thana.ToString();
                        txtEmpEmerAdrDist.Text = dtEmpAdr[0].Staff_Emer_CP_Dist.ToString();
                        txtEmpEmerAdrPhone.Text = dtEmpAdr[0].Staff_Emer_CP_Phone.ToString();
                        txtEmpEmerAdrCell.Text = dtEmpAdr[0].Staff_Emer_CP_Cell.ToString();
                    }
                    #endregion

                    #region GetIndentification
                    var taEmpExt = new tbl_TrTr_Staff_ExtTableAdapter();
                    var dtEmpExt = taEmpExt.GetDataByStaffRef(empRef);
                    if (dtEmpExt.Rows.Count > 0)
                    {
                        cboBloodGrp.SelectedValue = dtEmpExt[0].Staff_Blood_Grp.ToString();
                        cboReligion.SelectedValue = dtEmpExt[0].Staff_Religion.ToString();
                        txtNationality.Text = dtEmpExt[0].Staff_Nationality.ToString();
                        txtHomePhone.Text = dtEmpExt[0].Staff_Home_Phone.ToString();
                        txtPersonalCell.Text = dtEmpExt[0].Staff_Cell_Phone.ToString();
                        txtPersonalEmail.Text = dtEmpExt[0].Staff_Personal_Email.ToString();
                        txtIdentiMark.Text = dtEmpExt[0].Staff_Identi_Mark.ToString();
                        txtHeight.Text = dtEmpExt[0].Staff_Height.ToString();
                        txtWeight.Text = dtEmpExt[0].Staff_Weight.ToString();

                        txtNID.Text = dtEmpExt[0].Staff_NID.ToString();
                        txtTIN.Text = dtEmpExt[0].Staff_TIN.ToString();
                        txtPPNo.Text = dtEmpExt[0].Staff_Passport_No.ToString();
                        txtPPIsuDate.Text = dtEmpExt[0].IsStaff_Passport_Issu_DateNull() ? "" : dtEmpExt[0].Staff_Passport_Issu_Date.ToString("dd/MM/yyyy");
                        txtPPExpDate.Text = dtEmpExt[0].IsStaff_Passport_Expr_DateNull() ? "" : dtEmpExt[0].Staff_Passport_Expr_Date.ToString("dd/MM/yyyy");
                        txtPPIsuPlace.Text = dtEmpExt[0].Staff_Passport_Issu_From.ToString();
                        txtDLNo.Text = dtEmpExt[0].Staff_DL_No.ToString();
                        txtDLExprireDate.Text = dtEmpExt[0].IsStaff_DL_Exp_DateNull() ? "" : dtEmpExt[0].Staff_DL_Exp_Date.ToString("dd/MM/yyyy");
                        txtDLIssuPlace.Text = dtEmpExt[0].Staff_DL_Issu_From.ToString();
                    }
                    #endregion

                    #region GetQualInfo
                    var taEmpQual = new tbl_TrTr_Staff_QualTableAdapter();
                    var dtEmpQual = taEmpQual.GetDataByStaffRef(empRef);

                    LoadInitEmpQualGridData();
                    SetEmpQualGridData();

                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtEmpQual"];

                    foreach (dsStaffMas.tbl_TrTr_Staff_QualRow dr in dtEmpQual.Rows)
                    {
                        var qualTypeName = "";
                        var taQualType = new tblHrmsQualTypeTableAdapter();
                        var dtQualType = taQualType.GetDataByRefNo(Convert.ToInt32(dr.Staff_Qual_Type));
                        if (dtQualType.Rows.Count > 0) qualTypeName = dtQualType[0].QualTypeName.ToString();

                        var statName = "";
                        var taStat = new tblStatusInfoTableAdapter();
                        var dtStat = taStat.GetDataByRefNo(Convert.ToInt32(dr.Staff_Qual_Status));
                        if (dtStat.Rows.Count > 0) statName = dtStat[0].StatusName.ToString();

                        dt.Rows.Add(dr.Staff_Qual_Type.ToString(), qualTypeName, dr.Staff_Qual_Ref_No.ToString(), dr.Staff_Qual_Name.ToString(), dr.Staff_Qual_Inst_Name.ToString(),
                            dr.Staff_Qual_Grp_Sub.ToString(), "", dr.Staff_Qual_Pass_Year.ToString(), dr.Staff_Qual_Result.ToString(), "", "", "0", dr.Staff_Qual_Seq_No.ToString(),
                            dr.Staff_Qual_Ext_Data1.ToString(), "", "", dr.Staff_Qual_Status.ToString(), statName, "");
                    }

                    ViewState["dtEmpQual"] = dt;
                    SetEmpQualGridData();
                    #endregion

                    #region GetExpInfo
                    var taEmpExp = new tbl_TrTr_Staff_ExpTableAdapter();
                    var dtEmpExp = taEmpExp.GetDataByStaffRef(empRef);

                    LoadInitEmpExpGridData();
                    SetEmpExpGridData();

                    var dtExp = new DataTable();
                    dtExp = (DataTable)ViewState["dtEmpExp"];

                    foreach (dsStaffMas.tbl_TrTr_Staff_ExpRow dr in dtEmpExp.Rows)
                    {
                        dtExp.Rows.Add(dr.Staff_Exp_Comp_Name.ToString(), dr.Staff_Exp_Dept.ToString(), dr.Staff_Exp_Desig.ToString(), dr.Staff_Exp_Comp_Addr.ToString(),
                            dr.Staff_Exp_Date_From.ToString("dd/MM/yyyy"), dr.Staff_Exp_Date_To.ToString("dd/MM/yyyy"), dr.Staff_Exp_Rem.ToString(),
                            dr.Staff_Exp_Ref_Name.ToString(), dr.Staff_Exp_Ref_Contact.ToString(), dr.Staff_Exp_Ref_Email.ToString());
                    }

                    ViewState["dtEmpExp"] = dtExp;
                    SetEmpExpGridData();
                    #endregion

                    #region OfficialInfo
                    var taEmpOffc = new tbl_TrTr_Staff_OfficeTableAdapter();
                    var dtEmpOffc = taEmpOffc.GetDataByStaffRef(empRef);
                    if (dtEmpOffc.Rows.Count > 0)
                    {
                        cboLoc.SelectedValue = dtEmpOffc[0].Staff_Off_Loc_Ref_No.ToString();
                        
                        var taDept = new tblHrmsDeptTableAdapter();
                        var dtDept = taDept.GetDataByLocRef(dtEmpOffc[0].Staff_Off_Loc_Ref_No.ToString());
                        cboDept.DataSource = dtDept;
                        cboDept.DataValueField = "DeptRefNo";
                        cboDept.DataTextField = "DeptName";
                        cboDept.DataBind();
                        cboDept.Items.Insert(0, new ListItem("---Select---", "0"));
                        cboDept.SelectedValue = cboDept.Items.FindByValue(dtEmpOffc[0].Staff_Dept_Ref_No.ToString()).ToString() != null ? dtEmpOffc[0].Staff_Dept_Ref_No.ToString() : "0";

                        var taSec = new tblHrmsSecTableAdapter();
                        var dtSec = taSec.GetDataByDeptRef(dtEmpOffc[0].Staff_Dept_Ref_No.ToString());
                        cboSec.DataSource = dtSec;
                        cboSec.DataValueField = "SecRefNo";
                        cboSec.DataTextField = "SecName";
                        cboSec.DataBind();
                        cboSec.Items.Insert(0, new ListItem("---Select---", "0"));
                        cboSec.SelectedValue = cboSec.Items.FindByValue(dtEmpOffc[0].Staff_Sec_Ref_No.ToString()).ToString() != null ? dtEmpOffc[0].Staff_Sec_Ref_No.ToString() : "0";
                        cboDesig.SelectedValue = cboDesig.Items.FindByValue(dtEmpOffc[0].Staff_Desig_Ref_No.ToString()).ToString() != null ? dtEmpOffc[0].Staff_Desig_Ref_No.ToString() : "0";
                        txtDOJ.Text = dtEmpOffc[0].IsStaff_DOJNull() ? "" : dtEmpOffc[0].Staff_DOJ.ToString("dd/MM/yyyy");
                        cboSupr.SelectedValue = cboSupr.Items.FindByValue(dtEmpOffc[0].Staff_Supr_Id.ToString()).ToString() != null ? dtEmpOffc[0].Staff_Supr_Id.ToString() : "0";
                        cboJobStat.SelectedValue = cboJobStat.Items.FindByValue(dtEmpOffc[0].Staff_Job_Status.ToString()).ToString() != null ? dtEmpOffc[0].Staff_Job_Status.ToString() : "0";
                        txtConfDueDate.Text = dtEmpOffc[0].IsStaff_Conf_Due_DateNull() ? "" : dtEmpOffc[0].Staff_Conf_Due_Date.ToString("dd/MM/yyyy");
                        txtConfDate.Text = dtEmpOffc[0].IsStaff_Conf_DateNull() ? "" : dtEmpOffc[0].Staff_Conf_Date.ToString("dd/MM/yyyy");
                        cboWorkStation.SelectedValue = cboWorkStation.Items.FindByValue(dtEmpOffc[0].Staff_Work_Station.ToString()).ToString() != null ? dtEmpOffc[0].Staff_Work_Station.ToString() : "0";
                        txtIdCardNo.Text = dtEmpOffc[0].Staff_Card_Id.ToString();
                        cboShift.SelectedValue = cboShift.Items.FindByValue(dtEmpOffc[0].Staff_Shift_Ref_No.ToString()).ToString() != null ? dtEmpOffc[0].Staff_Shift_Ref_No.ToString() : "0";
                        cboGrade.SelectedValue = cboGrade.Items.FindByValue(dtEmpOffc[0].Staff_Sal_Grade.ToString()).ToString() != null ? dtEmpOffc[0].Staff_Sal_Grade.ToString() : "0";
                        txtSalary.Text = dtEmpOffc[0].Staff_Salary.ToString("F2");
                        txtSalBankAcc.Text = dtEmpOffc[0].Staff_Bank_Acc_No.ToString();
                        txtOffEmail.Text = dtEmpOffc[0].Staff_Off_Email.ToString();
                        txtOffPhone.Text = dtEmpOffc[0].Staff_Work_Phone.ToString();
                        txtOffPabx.Text = dtEmpOffc[0].Staff_Pabx_No.ToString();
                        txtOffIpPhone.Text = dtEmpOffc[0].Staff_Ip_Phone.ToString();
                    }
                    #endregion
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "No Record Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
        #endregion
        
        #region Clear Data
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSrchEmp.Text = "";
            txtEmpId.Text = "";
            var taEmpGenInfo = new tbl_TrTr_Staff_MasTableAdapter();
            var dtMaxEmpRef = taEmpGenInfo.GetMaxStaffRef();
            var nextEmpRefNo = dtMaxEmpRef == null ? "000001" : (Convert.ToInt32(dtMaxEmpRef) + 1).ToString();
            var nextEmpId = "TRN-" + Convert.ToInt32(nextEmpRefNo).ToString("000000");
            txtEmpId.Text = nextEmpId.ToString();            
            hfEmpRef.Value = "0";
            cboStaffType.SelectedIndex = 0;
            txtEmpFirstName.Text = "";
            txtEmpLastName.Text = "";
            txtEmpFirstName.Enabled = true;
            txtEmpLastName.Enabled = true;
            txtEmpFathersName.Text = "";
            txtEmpMothersName.Text = "";
            cboEmpGender.SelectedIndex = 0;
            cboEmpMaritalStat.SelectedIndex = 0;
            txtEmpSpouseName.Text = "";
            txtEmpDOB.Text = "";
            txtEmpPOB.Text = "";

            txtEmpPresAdrHouse.Text = "";
            txtEmpPresAdrPO.Text = "";
            txtEmpPresAdrPOCode.Text = "";
            txtEmpPresAdrThana.Text = "";
            txtEmpPresAdrDist.Text = "";

            txtEmpPrmAdrHouse.Text = "";
            txtEmpPrmAdrPO.Text = "";
            txtEmpPrmAdrPOCode.Text = "";
            txtEmpPrmAdrThana.Text = "";
            txtEmpPrmAdrDist.Text = "";

            txtEmpEmerAdrName.Text = "";
            txtEmpEmerAdrRelation.Text = "";
            txtEmpEmerAdrHouse.Text = "";
            txtEmpEmerAdrPO.Text = "";
            txtEmpEmerAdrPOCode.Text = "";
            txtEmpEmerAdrThana.Text = "";
            txtEmpEmerAdrDist.Text = "";
            txtEmpEmerAdrPhone.Text = "";
            txtEmpEmerAdrCell.Text = "";

            cboBloodGrp.SelectedIndex = 0;
            cboReligion.SelectedIndex = 0;
            txtNationality.Text = "";
            txtHomePhone.Text = "";
            txtPersonalCell.Text = "";
            txtPersonalEmail.Text = "";
            txtIdentiMark.Text = "";
            txtHeight.Text = "";
            txtWeight.Text = "";

            txtNID.Text = "";
            txtTIN.Text = "";
            txtPPNo.Text = "";
            txtPPIsuDate.Text = "";
            txtPPExpDate.Text = "";
            txtPPIsuPlace.Text = "";
            txtDLNo.Text = "";
            txtDLExprireDate.Text = "";
            txtDLIssuPlace.Text = "";

            cboWorkStation.SelectedIndex = 0;
            cboQualExam.Items.Clear();
            txtQualGrpSub.Text = "";
            txtQualInst.Text = "";
            txtQualPassYr.Text = "";
            txtQualResult.Text = "";
            cboQualExamStat.SelectedIndex = 0;
            txtQualRem.Text = "";            
            LoadInitEmpQualGridData();
            SetEmpQualGridData();
            hfEditQualFlag.Value = "N";
            gvEmpQual.SelectedIndex = -1;

            txtExpOrgName.Text = "";
            txtExpDept.Text = "";
            txtExpDesig.Text = "";
            txtExpOrgAdr.Text = "";
            txtExpRem.Text = "";
            txtExpFromDate.Text = "";
            txtExpToDate.Text = "";
            txtExpRefName.Text = "";
            txtExpRefContact.Text = "";
            txtExpRefMail.Text = "";
            LoadInitEmpExpGridData();
            SetEmpExpGridData();
            hfEditExpFlag.Value = "N";
            gvEmpExp.SelectedIndex = -1;

            cboLoc.SelectedIndex = 0;
            cboDept.Items.Clear();
            cboSec.Items.Clear();
            cboDesig.SelectedIndex = 0; 
            txtDOJ.Text = "";
            cboSupr.SelectedIndex = 0;
            cboJobStat.SelectedIndex = 0;
            txtConfDueDate.Text = "";
            txtConfDate.Text = "";
            cboWorkStation.SelectedIndex = 0;
            txtIdCardNo.Text = "";
            cboShift.SelectedIndex = 0;
            cboGrade.SelectedIndex = 0;
            txtSalary.Text = "";
            txtSalBankAcc.Text = "";
            txtOffEmail.Text = "";
            txtOffPhone.Text = "";
            txtOffPabx.Text = "";
            txtOffIpPhone.Text = "";

            txtEmpRemarks.Text = "";

            picUpload.Dispose();
            sigUpload.Dispose();
            nidUpload.Dispose();

            hlEmpPicPreview.NavigateUrl = "~/Image/NoImage.gif";
            imgEmpPicPreview.ImageUrl = "~/Image/NoImage.gif";

            hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
            imgEmpPic.ImageUrl = "~/Image/NoImage.gif";

            hlEmpSig.NavigateUrl = "~/Image/NoImage.gif";
            imgEmpSig.ImageUrl = "~/Image/NoImage.gif";

            hlEmpNid.NavigateUrl = "~/Image/NoImage.gif";
            imgEmpNId.ImageUrl = "~/Image/NoImage.gif";

            btnUpdatePic.Visible = false;
            btnUpdateSig.Visible = false;
            btnUpdateNid.Visible = false;

            btnDeletePic.Visible = false;
            btnDeleteSig.Visible = false;
            btnDeleteNid.Visible = false;

            btnClear.Visible = false;

            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabEmpInfo;
            foreach (object obj in container.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    tabPanel.Enabled = false;
                }
            }
            TabPnlPersonal.Enabled = true;           

            btnSaveGenInfo.Visible = false;
            btnSaveIndeni.Visible = false;
            btnSaveQual.Visible = false;
            btnSaveExp.Visible = false;
            btnSaveOffInfo.Visible = false;
            btnSaveEmp.Visible = true;

            chkIncTermEmp.Checked = false;
            AutoCompleteExtenderEmp.ContextKey = "0";
        }
        #endregion        

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cboJobStat.SelectedItem.ToString() == "Confirmed")
                if (txtConfDate.Text.Trim().Length <= 0)
                    args.IsValid = false;
                else
                    args.IsValid = true;
        }

        #region Update General Info
        protected void btnSaveGenInfo_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            SaveGenInfo();
        }

        private void SaveGenInfo()
        {
            var taEmpGenInfo = new tbl_TrTr_Staff_MasTableAdapter();
            var taEmpAdrInfo = new tbl_TrTr_Staff_AdrTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpGenInfo.Connection);

            try
            {
                taEmpGenInfo.AttachTransaction(myTran);
                taEmpAdrInfo.AttachTransaction(myTran);

                var dtMaxEmpRef = taEmpGenInfo.GetMaxStaffRef();
                var nextEmpRefNo = dtMaxEmpRef == null ? "000001" : (Convert.ToInt32(dtMaxEmpRef) + 1).ToString("000000");
                var nextEmpId = "TRN-" + Convert.ToInt32(nextEmpRefNo).ToString("000000");

                var dtEmpGenInfo = taEmpGenInfo.GetDataBySraffRef(hfEmpRef.Value.ToString());
                if (dtEmpGenInfo.Rows.Count > 0)
                {
                    taEmpGenInfo.UpdateStaffMas(cboStaffType.Text.ToString(), txtEmpFirstName.Text.Trim(), txtEmpLastName.Text.Trim(), txtEmpFathersName.Text.Trim(),
                        txtEmpMothersName.Text.Trim(), cboEmpGender.SelectedValue.ToString(), cboEmpMaritalStat.SelectedValue.ToString(), txtEmpSpouseName.Text.Trim(),
                        Convert.ToDateTime(txtEmpDOB.Text.Trim()), txtEmpPOB.Text.Trim(), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), cboStaffType.SelectedValue.ToString(), "", "", "1", "", hfEmpRef.Value.ToString());
                }
                else
                {
                    taEmpGenInfo.InsertStaffMas(nextEmpRefNo, nextEmpId, cboStaffType.Text.ToString(), txtEmpFirstName.Text.Trim(), txtEmpLastName.Text.Trim(), txtEmpFathersName.Text.Trim(),
                        txtEmpMothersName.Text.Trim(), cboEmpGender.SelectedValue.ToString(), cboEmpMaritalStat.SelectedValue.ToString(), txtEmpSpouseName.Text.Trim(),
                        Convert.ToDateTime(txtEmpDOB.Text.Trim()), txtEmpPOB.Text.Trim(), "Y", "N", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), cboStaffType.SelectedValue.ToString(), "", "", "1", "");
                }

                var dtEmpAdrInfo = taEmpAdrInfo.GetDataByStaffRef(hfEmpRef.Value.ToString());
                if (dtEmpAdrInfo.Rows.Count > 0)
                {
                    taEmpAdrInfo.UpdateStaffAdr(txtEmpPresAdrHouse.Text.Trim(), txtEmpPresAdrPO.Text.Trim(), txtEmpPresAdrPOCode.Text.Trim(),
                            txtEmpPresAdrThana.Text.Trim(), txtEmpPresAdrDist.Text.Trim(), txtEmpPrmAdrHouse.Text.Trim(), txtEmpPrmAdrPO.Text.Trim(), txtEmpPrmAdrPOCode.Text.Trim(),
                            txtEmpPrmAdrThana.Text.Trim(), txtEmpPrmAdrDist.Text.Trim(), "", "", "", "", "", txtEmpEmerAdrName.Text.Trim(), txtEmpEmerAdrRelation.Text.Trim(),
                            txtEmpEmerAdrHouse.Text.Trim(), txtEmpEmerAdrPO.Text.Trim(), txtEmpEmerAdrPOCode.Text.Trim(), txtEmpEmerAdrThana.Text.Trim(),
                            txtEmpEmerAdrDist.Text.Trim(), txtEmpEmerAdrPhone.Text.Trim(), txtEmpEmerAdrCell.Text.Trim(), "1", "", hfEmpRef.Value.ToString());
                }
                else
                {
                    taEmpAdrInfo.InsertStaffAdr(nextEmpRefNo, nextEmpId, txtEmpPresAdrHouse.Text.Trim(), txtEmpPresAdrPO.Text.Trim(), txtEmpPresAdrPOCode.Text.Trim(),
                            txtEmpPresAdrThana.Text.Trim(), txtEmpPresAdrDist.Text.Trim(), txtEmpPrmAdrHouse.Text.Trim(), txtEmpPrmAdrPO.Text.Trim(), txtEmpPrmAdrPOCode.Text.Trim(),
                            txtEmpPrmAdrThana.Text.Trim(), txtEmpPrmAdrDist.Text.Trim(), "", "", "", "", "", txtEmpEmerAdrName.Text.Trim(), txtEmpEmerAdrRelation.Text.Trim(),
                            txtEmpEmerAdrHouse.Text.Trim(), txtEmpEmerAdrPO.Text.Trim(), txtEmpEmerAdrPOCode.Text.Trim(), txtEmpEmerAdrThana.Text.Trim(),
                            txtEmpEmerAdrDist.Text.Trim(), txtEmpEmerAdrPhone.Text.Trim(), txtEmpEmerAdrCell.Text.Trim(), "1", "");
                }

                myTran.Commit();

                if (dtEmpGenInfo.Rows.Count > 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Staff Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Employee ID No: " + txtEmpId.Text.Trim();
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Staff Data Added Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Employee ID No: " + nextEmpId;
                    ModalPopupExtenderMsg.Show();
                }
                //btnClear.Visible = false;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
        #endregion

        #region Update Indentification
        protected void btnSaveIndeni_Click(object sender, EventArgs e)
        {
            var taEmpGenInfo = new tbl_TrTr_Staff_MasTableAdapter();
            var taEmpIdenti = new tbl_TrTr_Staff_ExtTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpGenInfo.Connection);

            try
            {
                DateTime? ppIssDt = null;
                if (txtPPIsuDate.Text.Trim().Length > 0) ppIssDt = Convert.ToDateTime(txtPPIsuDate.Text.Trim());

                DateTime? ppExpDt = null;
                if (txtPPExpDate.Text.Trim().Length > 0) ppExpDt = Convert.ToDateTime(txtPPExpDate.Text.Trim());

                DateTime? ppDlExpDt = null;
                if (txtDLExprireDate.Text.Trim().Length > 0) ppDlExpDt = Convert.ToDateTime(txtDLExprireDate.Text.Trim());

                taEmpGenInfo.AttachTransaction(myTran);
                taEmpIdenti.AttachTransaction(myTran);

                var dtEmpIdentiInfo = taEmpIdenti.GetDataByStaffRef(hfEmpRef.Value.ToString());
                if (dtEmpIdentiInfo.Rows.Count > 0)
                {
                    taEmpIdenti.UpdateStaffExt(cboReligion.SelectedValue.ToString(), txtNationality.Text.Trim(), cboBloodGrp.SelectedValue.ToString(), txtHomePhone.Text.Trim(),
                        txtPersonalCell.Text.Trim(), txtPersonalEmail.Text.Trim(), txtIdentiMark.Text.Trim(), txtHeight.Text.Trim(), txtWeight.Text.Trim(), "", txtNID.Text.Trim(),
                        txtTIN.Text.Trim(), txtDLNo.Text.Trim(), txtDLIssuPlace.Text.Trim(), ppDlExpDt, txtPPNo.Text.Trim(),
                        txtPPIsuPlace.Text.Trim(), ppIssDt, ppExpDt, "", "", "", hfEmpRef.Value);

                    myTran.Commit();

                    tblMsg.Rows[0].Cells[0].InnerText = "Staff Identification Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Staff ID No: " + txtEmpId.Text.Trim();
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    taEmpIdenti.InsertStaffExt(hfEmpRef.Value.ToString(), txtEmpId.Text.Trim(), cboReligion.SelectedValue.ToString(), txtNationality.Text.Trim(),
                        cboBloodGrp.SelectedValue.ToString(), txtHomePhone.Text.Trim(), txtPersonalCell.Text.Trim(), txtPersonalEmail.Text.Trim(), txtIdentiMark.Text.Trim(),
                        txtHeight.Text.Trim(), txtWeight.Text.Trim(), "", txtNID.Text.Trim(), txtTIN.Text.Trim(), txtDLNo.Text.Trim(), txtDLIssuPlace.Text.Trim(),
                        ppDlExpDt, txtPPNo.Text.Trim(), txtPPIsuPlace.Text.Trim(), ppIssDt, ppExpDt, "", "", "");

                    myTran.Commit();

                    tblMsg.Rows[0].Cells[0].InnerText = "Staff Identification Added Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Staff ID No: " + txtEmpId.Text.Trim();
                    ModalPopupExtenderMsg.Show();
                }
                //btnClear.Visible = false;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
        #endregion

        #region Update Qualification
        protected void cboQualType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taQual = new tblHrmsQualTableAdapter();
            try
            {
                var dtQual = taQual.GetDataByQualType(Convert.ToInt32(cboQualType.SelectedValue.ToString()));
                cboQualExam.DataSource = dtQual;
                cboQualExam.DataValueField = "QualRefNo";
                cboQualExam.DataTextField = "QualCode";
                cboQualExam.DataBind();
                cboQualExam.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            catch(Exception ex) { }
        }

        protected void btnAddQual_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfEditQualFlag.Value == "N")
                {
                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtEmpQual"];

                    dt.Rows.Add(cboQualType.SelectedValue.ToString(), cboQualType.SelectedItem.ToString(), cboQualExam.SelectedValue.ToString(), cboQualExam.SelectedItem.ToString(),
                        txtQualInst.Text.Trim(), txtQualGrpSub.Text.Trim(), "", txtQualPassYr.Text.Trim(), txtQualResult.Text.Trim(), "", "", "0", dt.Rows.Count + 1,
                        txtQualRem.Text.Trim(), "", "", cboQualExamStat.SelectedValue.ToString(), cboQualExamStat.SelectedItem.ToString(), "");
                    ViewState["dtEmpQual"] = dt;
                }
                else
                {
                    var rowNum = gvEmpQual.SelectedIndex;

                    if (rowNum == -1) return;

                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtEmpQual"];

                    dt.Rows[rowNum]["QUAL_TYPE_REF"] = cboQualType.SelectedValue.ToString();
                    dt.Rows[rowNum]["QUAL_TYPE"] = cboQualType.SelectedItem.ToString();
                    dt.Rows[rowNum]["EXAM_REF"] = cboQualExam.SelectedValue.ToString();
                    dt.Rows[rowNum]["EXAM_NAME"] = cboQualExam.SelectedItem.ToString();
                    dt.Rows[rowNum]["INST_NAME"] = txtQualInst.Text.Trim();
                    dt.Rows[rowNum]["GRP_SUB"] = txtQualGrpSub.Text.Trim();
                    dt.Rows[rowNum]["CERT_AUTH"] = "";
                    dt.Rows[rowNum]["PASS_YEAR"] = txtQualPassYr.Text.Trim();
                    dt.Rows[rowNum]["RESULT"] = txtQualResult.Text.Trim();
                    dt.Rows[rowNum]["ST_DT"] = "";
                    dt.Rows[rowNum]["END_DT"] = "";
                    dt.Rows[rowNum]["DURATION"] = "";
                    dt.Rows[rowNum]["SEQUENCE"] = rowNum + 1;
                    dt.Rows[rowNum]["EXT_DATA1"] = txtQualRem.Text.Trim();
                    dt.Rows[rowNum]["EXT_DATA2"] = "";
                    dt.Rows[rowNum]["EXT_DATA3"] = "";
                    dt.Rows[rowNum]["STAT_REF"] = cboQualExamStat.SelectedValue.ToString();
                    dt.Rows[rowNum]["STATUS"] = cboQualExamStat.SelectedItem.ToString();
                    dt.Rows[rowNum]["FLAG"] = "";

                    dt.AcceptChanges();
                }

                SetEmpQualGridData();

                cboWorkStation.SelectedIndex = 0;
                cboQualExam.Items.Clear();
                txtQualGrpSub.Text = "";
                txtQualInst.Text = "";
                txtQualPassYr.Text = "";
                txtQualResult.Text = "";
                cboQualExamStat.SelectedIndex = 0;
                txtQualRem.Text = "";
                hfEditQualFlag.Value = "N";
                gvEmpQual.SelectedIndex = -1;
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvEmpQual_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtEmpQual"];

            dt.Rows[rowNum].Delete();
            dt.AcceptChanges();

            gvEmpQual.EditIndex = -1;
            SetEmpQualGridData();
        }

        protected void btnSaveQual_Click(object sender, EventArgs e)
        {
            var taEmpGenInfo = new tbl_TrTr_Staff_MasTableAdapter();
            var taEmpQual = new tbl_TrTr_Staff_QualTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpGenInfo.Connection);

            try
            {
                taEmpGenInfo.AttachTransaction(myTran);
                taEmpQual.AttachTransaction(myTran);

                taEmpQual.DeleteStaffQual(hfEmpRef.Value.ToString());

                #region Save Qualification Details
                var Lno = 1;
                var dtQual = new DataTable();
                dtQual = (DataTable)ViewState["dtEmpQual"];
                foreach (DataRow row in dtQual.Rows)
                {
                    var qualTypeRef = row["QUAL_TYPE_REF"].ToString();
                    var qualType = row["QUAL_TYPE"].ToString();
                    var examRef = row["EXAM_REF"].ToString();
                    var examName = row["EXAM_NAME"].ToString();
                    var instName = row["INST_NAME"].ToString();
                    var grpSub = row["GRP_SUB"].ToString();
                    var certAuth = row["CERT_AUTH"].ToString();
                    var passYr = row["PASS_YEAR"].ToString();
                    var result = row["RESULT"].ToString();
                    var stDt = row["ST_DT"].ToString();
                    var endDt = row["END_DT"].ToString();
                    var duration = row["DURATION"].ToString();
                    var sequence = row["SEQUENCE"].ToString();
                    var extData1 = row["EXT_DATA1"].ToString();
                    var extData2 = row["EXT_DATA2"].ToString();
                    var extData3 = row["EXT_DATA3"].ToString();
                    var statRef = row["STAT_REF"].ToString(); 
                    var stat = row["STATUS"].ToString(); 
                    var flag = row["FLAG"].ToString();

                    DateTime? stDate = null;
                    DateTime? endDate = null;

                    taEmpQual.InsertStaffQual(qualTypeRef, examRef, examName, hfEmpRef.Value.ToString(), instName, grpSub, certAuth, passYr, result, stDate, endDate, duration, Lno,
                        DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), extData1, extData2, extData3, "1", "");

                    Lno++;
                }
                #endregion             

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Staff Data Added Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "Staff ID No: " + txtEmpId.Text.Trim();
                ModalPopupExtenderMsg.Show();

                //btnClear.Visible = false;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvEmpQual_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvEmpQual_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvEmpQual.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    HiddenField hfTypeRef = (HiddenField)gvEmpQual.Rows[indx].FindControl("hfQualTypeRef");
                    cboQualType.SelectedValue = hfTypeRef.Value.ToString();

                    var taQual = new tblHrmsQualTableAdapter();
                    var dtQual = taQual.GetDataByQualType(Convert.ToInt32(hfTypeRef.Value.ToString()));
                    cboQualExam.DataSource = dtQual;
                    cboQualExam.DataValueField = "QualRefNo";
                    cboQualExam.DataTextField = "QualCode";
                    cboQualExam.DataBind();
                    cboQualExam.Items.Insert(0, new ListItem("---Select---", "0"));

                    HiddenField hfExamRef = (HiddenField)gvEmpQual.Rows[indx].FindControl("hfExamRef");
                    cboQualExam.SelectedValue = hfExamRef.Value.ToString();

                    Label lblGrpSub = (Label)gvEmpQual.Rows[indx].FindControl("lblGrpSub");
                    txtQualGrpSub.Text = lblGrpSub.Text.Trim();

                    Label lblInstName = (Label)gvEmpQual.Rows[indx].FindControl("lblInstName");
                    txtQualInst.Text = lblInstName.Text.Trim();

                    Label lblPassYr = (Label)gvEmpQual.Rows[indx].FindControl("lblPassYr");
                    txtQualPassYr.Text = lblPassYr.Text.Trim();

                    Label lblRslt = (Label)gvEmpQual.Rows[indx].FindControl("lblResult");
                    txtQualResult.Text = lblRslt.Text.Trim();

                    HiddenField hfStatus = (HiddenField)gvEmpQual.Rows[indx].FindControl("hfStatRef");
                    cboQualExamStat.SelectedValue = hfStatus.Value.ToString();

                    Label lblRem = (Label)gvEmpQual.Rows[indx].FindControl("lblRemarks");
                    txtQualRem.Text = lblRem.Text.Trim();

                    hfEditQualFlag.Value = "Y";
                }
                catch (Exception ex) 
                {
                    hfEditQualFlag.Value = "N";
                }
            }
        }

        protected void btnClearQual_Click(object sender, EventArgs e)
        {
            cboWorkStation.SelectedIndex = 0;
            cboQualExam.Items.Clear();
            txtQualGrpSub.Text = "";
            txtQualInst.Text = "";
            txtQualPassYr.Text = "";
            txtQualResult.Text = "";
            cboQualExamStat.SelectedIndex = 0;
            txtQualRem.Text = "";
            hfEditQualFlag.Value = "N";
            gvEmpQual.SelectedIndex = -1;
        }
        #endregion

        #region Update Experience
        protected void btnAddExp_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfEditQualFlag.Value == "N")
                {
                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtEmpExp"];

                    dt.Rows.Add(txtExpOrgName.Text.Trim(), txtExpDept.Text.Trim(), txtExpDesig.Text.Trim(), txtExpOrgAdr.Text.Trim(), txtExpFromDate.Text.Trim(),
                        txtExpToDate.Text.Trim(), txtExpRem.Text.Trim(), txtExpRefName.Text.Trim(), txtExpRefContact.Text.Trim(), txtExpRefMail.Text.Trim());
                    ViewState["dtEmpExp"] = dt;
                }
                else
                {
                    var rowNum = gvEmpExp.SelectedIndex;

                    if (rowNum == -1) return;

                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtEmpExp"];

                    dt.Rows[rowNum]["EXP_ORG"] = txtExpOrgName.Text.Trim();
                    dt.Rows[rowNum]["EXP_DEPT"] = txtExpDept.Text.Trim();
                    dt.Rows[rowNum]["EXP_DESIG"] = txtExpDesig.Text.Trim();
                    dt.Rows[rowNum]["EXP_ADR"] = txtExpOrgAdr.Text.Trim();
                    dt.Rows[rowNum]["EXP_FROM"] = txtExpFromDate.Text.Trim();
                    dt.Rows[rowNum]["EXP_TO"] = txtExpToDate.Text.Trim();
                    dt.Rows[rowNum]["EXP_REM"] = txtExpRem.Text.Trim();
                    dt.Rows[rowNum]["EXP_REF_NAME"] = txtExpRefName.Text.Trim();
                    dt.Rows[rowNum]["EXP_REF_CONT"] = txtExpRefContact.Text.Trim();
                    dt.Rows[rowNum]["EXP_REF_EMAIL"] = txtExpRefMail.Text.Trim();

                    dt.AcceptChanges();
                }

                SetEmpExpGridData();

                txtExpOrgName.Text = "";
                txtExpDept.Text = "";
                txtExpDesig.Text = "";
                txtExpOrgAdr.Text = "";
                txtExpRem.Text = "";
                txtExpFromDate.Text = "";
                txtExpToDate.Text = "";
                txtExpRefName.Text = "";
                txtExpRefContact.Text = "";
                txtExpRefMail.Text = "";
                hfEditExpFlag.Value = "N";
                gvEmpExp.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearExp_Click(object sender, EventArgs e)
        {
            txtExpOrgName.Text = "";
            txtExpDept.Text = "";
            txtExpDesig.Text = "";
            txtExpOrgAdr.Text = "";
            txtExpRem.Text = "";
            txtExpFromDate.Text = "";
            txtExpToDate.Text = "";
            txtExpRefName.Text = "";
            txtExpRefContact.Text = "";
            txtExpRefMail.Text = "";
            hfEditExpFlag.Value = "N";
            gvEmpExp.SelectedIndex = -1;
        }

        protected void gvEmpExp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvEmpExp_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtEmpExp"];

            dt.Rows[rowNum].Delete();
            dt.AcceptChanges();

            gvEmpExp.EditIndex = -1;
            SetEmpExpGridData();
        }

        protected void gvEmpExp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvEmpExp.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    Label lblExpOrg = (Label)gvEmpExp.Rows[indx].FindControl("lblExpOrg");
                    txtExpOrgName.Text = lblExpOrg.Text.Trim();

                    Label lblExpDept = (Label)gvEmpExp.Rows[indx].FindControl("lblExpDept");
                    txtExpDept.Text = lblExpDept.Text.Trim();

                    Label lblExpDesig = (Label)gvEmpExp.Rows[indx].FindControl("lblExpDesig");
                    txtExpDesig.Text = lblExpDesig.Text.Trim();

                    Label lblExpOrgAdr = (Label)gvEmpExp.Rows[indx].FindControl("lblExpAdr");
                    txtExpOrgAdr.Text = lblExpOrgAdr.Text.Trim();

                    Label lblExpRem = (Label)gvEmpExp.Rows[indx].FindControl("lblExpRem");
                    txtExpRem.Text = lblExpRem.Text.Trim();

                    Label lblExpFrom = (Label)gvEmpExp.Rows[indx].FindControl("lblExpFrom");
                    txtExpFromDate.Text = lblExpFrom.Text.Trim();

                    Label lblExpTo = (Label)gvEmpExp.Rows[indx].FindControl("lblExpTo");
                    txtExpToDate.Text = lblExpTo.Text.ToString();

                    Label lblRefName = (Label)gvEmpExp.Rows[indx].FindControl("lblExpRefName");
                    txtExpRefName.Text = lblRefName.Text.Trim();

                    Label lblRefContact = (Label)gvEmpExp.Rows[indx].FindControl("lblExpRefContact");
                    txtExpRefContact.Text = lblRefContact.Text.Trim();

                    Label lblRefEmail = (Label)gvEmpExp.Rows[indx].FindControl("lblExpRefEmail");
                    txtExpRefMail.Text = lblRefEmail.Text.Trim();

                    hfEditExpFlag.Value = "Y";
                }
                catch (Exception ex)
                {
                    hfEditExpFlag.Value = "N";
                }
            }
        }

        protected void btnSaveExp_Click(object sender, EventArgs e)
        {
            var taEmpGenInfo = new tbl_TrTr_Staff_MasTableAdapter();
            var taEmpExp = new tbl_TrTr_Staff_ExpTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpGenInfo.Connection);

            try
            {
                taEmpGenInfo.AttachTransaction(myTran);
                taEmpExp.AttachTransaction(myTran);

                taEmpExp.DeleteStaffExp(hfEmpRef.Value.ToString());

                #region Save Experience Details
                var Lno = 1;
                var dtExp = new DataTable();
                dtExp = (DataTable)ViewState["dtEmpExp"];
                foreach (DataRow row in dtExp.Rows)
                {
                    var expOrgName = row["EXP_ORG"].ToString();
                    var expDept = row["EXP_DEPT"].ToString();
                    var expDesig = row["EXP_DESIG"].ToString();
                    var expOrgAdr = row["EXP_ADR"].ToString();
                    var expRem = row["EXP_REM"].ToString();
                    var expFrom = row["EXP_FROM"].ToString();
                    var expTo = row["EXP_TO"].ToString();
                    var expRefName = row["EXP_REF_NAME"].ToString();
                    var expRefCont = row["EXP_REF_CONT"].ToString();
                    var expRefEmail = row["EXP_REF_EMAIL"].ToString();

                    taEmpExp.InsertStaffExp(hfEmpRef.Value.ToString(), expOrgName, expDept, expDesig, 0, expOrgAdr, expRem, Convert.ToDateTime(expFrom), Convert.ToDateTime(expTo),
                        expRefName, expRefCont, expRefEmail, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                        "", "", "", "1", "");

                    Lno++;
                }
                #endregion

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Employee Data Added Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "Employee ID No: " + txtEmpId.Text.Trim();
                ModalPopupExtenderMsg.Show();

                //btnClear.Visible = false;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
        #endregion

        #region Update Office Info
        protected void btnSaveOffInfo_Click(object sender, EventArgs e)
        {
            var taEmpGenInfo = new tbl_TrTr_Staff_MasTableAdapter();
            var taEmpOffcInfo = new tbl_TrTr_Staff_OfficeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpGenInfo.Connection);

            try
            {
                DateTime? confDt = null;
                if (txtConfDate.Text.Trim().Length > 0) confDt = Convert.ToDateTime(txtConfDate.Text.Trim());

                taEmpGenInfo.AttachTransaction(myTran);
                taEmpOffcInfo.AttachTransaction(myTran);

                var dtEmpOffcInfo = taEmpOffcInfo.GetDataByStaffRef(hfEmpRef.Value.ToString());
                if (dtEmpOffcInfo.Rows.Count > 0)
                {
                    taEmpOffcInfo.UpdateStaffOffc("100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), Convert.ToDateTime(txtDOJ.Text.Trim()), cboSupr.SelectedValue.ToString(),
                        cboJobStat.SelectedValue.ToString(), Convert.ToDateTime(txtConfDueDate.Text.Trim()), confDt, cboWorkStation.SelectedValue.ToString(),
                        txtIdCardNo.Text.Trim(), cboShift.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                        txtSalBankAcc.Text.Trim(), "", txtOffEmail.Text.Trim(), txtOffPhone.Text.Trim(), txtOffPabx.Text.Trim(), txtOffIpPhone.Text.Trim(),
                        txtEmpRemarks.Text.Trim(), "", "", "", hfEmpRef.Value.ToString());

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Staff Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Employee ID No: " + txtEmpId.Text.Trim();
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    taEmpOffcInfo.InsertStaffOffc(hfEmpRef.Value.ToString(), txtEmpId.Text.Trim(), "100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), Convert.ToDateTime(txtDOJ.Text.Trim()), cboSupr.SelectedValue.ToString(),
                        cboJobStat.SelectedValue.ToString(), Convert.ToDateTime(txtConfDueDate.Text.Trim()), confDt, cboWorkStation.SelectedValue.ToString(),
                        txtIdCardNo.Text.Trim(), cboShift.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                        txtSalBankAcc.Text.Trim(), "", txtOffEmail.Text.Trim(), txtOffPhone.Text.Trim(), txtOffPabx.Text.Trim(), txtOffIpPhone.Text.Trim(),
                        txtEmpRemarks.Text.Trim(), "", "", "");

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Staff Data Added Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Employee ID No: " + txtEmpId.Text.Trim();
                    ModalPopupExtenderMsg.Show();
                }
                //btnClear.Visible = false;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
        #endregion

        #region Update Pic_Sig_Nid
        protected void btnUpdatePic_Click(object sender, EventArgs e)
        {
            var taEmpImg = new tbl_TrTr_Staff_ImgTableAdapter();

            if (picUpload.HasFile)
            {
                try
                {
                    System.Drawing.Image imag = System.Drawing.Image.FromStream(picUpload.PostedFile.InputStream);

                    var dtEmpImg = taEmpImg.GetDataByStaffRef(hfEmpRef.Value.ToString());

                    if (dtEmpImg.Rows.Count > 0)
                    {
                        taEmpImg.UpdateStaffImg(ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                    }
                    else
                    {
                        taEmpImg.InsertStaffImg(hfEmpRef.Value.ToString(), txtEmpId.Text.ToString(), ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg),
                            null, null, "", "", "", "1", "");
                    }
                }
                catch (Exception ex) { }
            }
            else
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Select file first.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdateSig_Click(object sender, EventArgs e)
        {
            var taEmpImg = new tbl_TrTr_Staff_ImgTableAdapter();

            if (sigUpload.HasFile)
            {
                try
                {
                    System.Drawing.Image imag = System.Drawing.Image.FromStream(sigUpload.PostedFile.InputStream);

                    var dtEmpImg = taEmpImg.GetDataByStaffRef(hfEmpRef.Value.ToString());

                    if (dtEmpImg.Rows.Count > 0)
                    {
                        taEmpImg.UpdateStaffDL(ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                    }
                    else
                    {
                        taEmpImg.InsertStaffImg(hfEmpRef.Value.ToString(), txtEmpId.Text.ToString(), null,
                            ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg), null, "", "", "", "1", "");
                    }
                }
                catch (Exception ex) { }
            }
            else
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Select file first.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdateNid_Click(object sender, EventArgs e)
        {
            var taEmpImg = new tbl_TrTr_Staff_ImgTableAdapter();

            if (nidUpload.HasFile)
            {
                try
                {
                    System.Drawing.Image imag = System.Drawing.Image.FromStream(nidUpload.PostedFile.InputStream);

                    var dtEmpImg = taEmpImg.GetDataByStaffRef(hfEmpRef.Value.ToString());

                    if (dtEmpImg.Rows.Count > 0)
                    {
                        taEmpImg.UpdateStaffNID(ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                    }
                    else
                    {
                        taEmpImg.InsertStaffImg(hfEmpRef.Value.ToString(), txtEmpId.Text.ToString(), null,
                            null, ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg), "", "", "", "1", "");
                    }
                }
                catch (Exception ex) { }
            }
            else
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Select file first.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
        #endregion

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (tabEmpInfo.ActiveTabIndex == 0) Page.Validate("SaveGenInfo");
            if (tabEmpInfo.ActiveTabIndex == 1) Page.Validate("SaveExtInfo");
            if (tabEmpInfo.ActiveTabIndex == 4) Page.Validate("SaveOffcInfo");
            if (Page.IsValid)
            {
                if ((tabEmpInfo.ActiveTabIndex) < (tabEmpInfo.Tabs.Count-1))
                {
                    tabEmpInfo.Tabs[(tabEmpInfo.ActiveTabIndex) + 1].Enabled = true;
                    tabEmpInfo.ActiveTabIndex++;
                }
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            tabEmpInfo.ActiveTabIndex--;
        }

        private byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception ex) { throw; }
            return Ret;
        }

        private bool IsImage(HttpPostedFile file)
        {        
            //Checks for image type... you could also do filename extension checks and other things        
            return ((file != null) && System.Text.RegularExpressions.Regex.IsMatch(file.ContentType, "image/\\S+") && (file.ContentLength > 0));
        }
        
        #region Save_Update Employee
        protected void btnSaveEmp_Click(object sender, EventArgs e)
        {
            Page.Validate("SaveGenInfo");
            Page.Validate("SaveExtInfo");
            Page.Validate("SaveOffcInfo");

            if (!Page.IsValid) return;

            var taEmpGenInfo = new tbl_TrTr_Staff_MasTableAdapter();
            var taEmpAdrInfo = new tbl_TrTr_Staff_AdrTableAdapter();
            var taEmpIdenti = new tbl_TrTr_Staff_ExtTableAdapter();
            var taEmpExp = new tbl_TrTr_Staff_ExpTableAdapter();
            var taEmpQual = new tbl_TrTr_Staff_QualTableAdapter();
            var taEmpOffcInfo = new tbl_TrTr_Staff_OfficeTableAdapter();
            var taEmpImg = new tbl_TrTr_Staff_ImgTableAdapter();
            //var taEmpServLog = new tblHrmsEmpServLogTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpGenInfo.Connection);

            try
            {
                taEmpGenInfo.AttachTransaction(myTran);
                taEmpAdrInfo.AttachTransaction(myTran);
                taEmpIdenti.AttachTransaction(myTran);
                taEmpExp.AttachTransaction(myTran);
                taEmpQual.AttachTransaction(myTran);
                taEmpOffcInfo.AttachTransaction(myTran);
                taEmpImg.AttachTransaction(myTran);
                //taEmpServLog.AttachTransaction(myTran);

                #region Save General Info
                var dtMaxEmpRef = taEmpGenInfo.GetMaxStaffRef();
                var nextEmpRefNo = dtMaxEmpRef == null ? "000001" : (Convert.ToInt32(dtMaxEmpRef) + 1).ToString("000000");
                var nextEmpId = "TRN-" + Convert.ToInt32(nextEmpRefNo).ToString("000000");
                 
                var dtEmpGenInfo = taEmpGenInfo.GetDataBySraffRef(hfEmpRef.Value.ToString());
                 
                if (dtEmpGenInfo.Rows.Count > 0)                 
                {
                    taEmpGenInfo.UpdateStaffMas(cboStaffType.Text.Trim(), txtEmpFirstName.Text.Trim(), txtEmpLastName.Text.Trim(), txtEmpFathersName.Text.Trim(),
                        txtEmpMothersName.Text.Trim(), cboEmpGender.SelectedValue.ToString(), cboEmpMaritalStat.SelectedValue.ToString(), txtEmpSpouseName.Text.Trim(),
                        Convert.ToDateTime(txtEmpDOB.Text.Trim()), txtEmpPOB.Text.Trim(), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), cboStaffType.SelectedValue.ToString(), "", "", "1", "", hfEmpRef.Value.ToString());
                }
                else
                {
                    taEmpGenInfo.InsertStaffMas(nextEmpRefNo, nextEmpId, cboStaffType.Text.Trim(), txtEmpFirstName.Text.Trim(), txtEmpLastName.Text.Trim(), txtEmpFathersName.Text.Trim(),
                         txtEmpMothersName.Text.Trim(), cboEmpGender.SelectedValue.ToString(), cboEmpMaritalStat.SelectedValue.ToString(), txtEmpSpouseName.Text.Trim(),
                         Convert.ToDateTime(txtEmpDOB.Text.Trim()), txtEmpPOB.Text.Trim(), "Y", "N", DateTime.Now,
                         Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), cboStaffType.SelectedValue.ToString(), "", "", "1", "");
                }
                #endregion

                #region Save Address Info
                var dtEmpAdrInfo = taEmpAdrInfo.GetDataByStaffRef(hfEmpRef.Value.ToString());
                if (dtEmpAdrInfo.Rows.Count > 0)
                {
                    taEmpAdrInfo.UpdateStaffAdr(txtEmpPresAdrHouse.Text.Trim(), txtEmpPresAdrPO.Text.Trim(), txtEmpPresAdrPOCode.Text.Trim(),
                            txtEmpPresAdrThana.Text.Trim(), txtEmpPresAdrDist.Text.Trim(), txtEmpPrmAdrHouse.Text.Trim(), txtEmpPrmAdrPO.Text.Trim(), txtEmpPrmAdrPOCode.Text.Trim(),
                            txtEmpPrmAdrThana.Text.Trim(), txtEmpPrmAdrDist.Text.Trim(), "", "", "", "", "", txtEmpEmerAdrName.Text.Trim(), txtEmpEmerAdrRelation.Text.Trim(),
                            txtEmpEmerAdrHouse.Text.Trim(), txtEmpEmerAdrPO.Text.Trim(), txtEmpEmerAdrPOCode.Text.Trim(), txtEmpEmerAdrThana.Text.Trim(),
                            txtEmpEmerAdrDist.Text.Trim(), txtEmpEmerAdrPhone.Text.Trim(), txtEmpEmerAdrCell.Text.Trim(), "1", "", hfEmpRef.Value.ToString());
                }
                else
                {
                    taEmpAdrInfo.InsertStaffAdr(nextEmpRefNo.ToString(), nextEmpId, txtEmpPresAdrHouse.Text.Trim(), txtEmpPresAdrPO.Text.Trim(), txtEmpPresAdrPOCode.Text.Trim(),
                             txtEmpPresAdrThana.Text.Trim(), txtEmpPresAdrDist.Text.Trim(), txtEmpPrmAdrHouse.Text.Trim(), txtEmpPrmAdrPO.Text.Trim(), txtEmpPrmAdrPOCode.Text.Trim(),
                             txtEmpPrmAdrThana.Text.Trim(), txtEmpPrmAdrDist.Text.Trim(), "", "", "", "", "", txtEmpEmerAdrName.Text.Trim(), txtEmpEmerAdrRelation.Text.Trim(),
                             txtEmpEmerAdrHouse.Text.Trim(), txtEmpEmerAdrPO.Text.Trim(), txtEmpEmerAdrPOCode.Text.Trim(), txtEmpEmerAdrThana.Text.Trim(),
                             txtEmpEmerAdrDist.Text.Trim(), txtEmpEmerAdrPhone.Text.Trim(), txtEmpEmerAdrCell.Text.Trim(), "1", "");
                }
                #endregion

                #region Save Indentification
                DateTime? ppIssDt = null;
                if (txtPPIsuDate.Text.Trim().Length > 0) ppIssDt = Convert.ToDateTime(txtPPIsuDate.Text.Trim());

                DateTime? ppExpDt = null;
                if (txtPPExpDate.Text.Trim().Length > 0) ppExpDt = Convert.ToDateTime(txtPPExpDate.Text.Trim());

                DateTime? ppDlExpDt = null;
                if (txtDLExprireDate.Text.Trim().Length > 0) ppDlExpDt = Convert.ToDateTime(txtDLExprireDate.Text.Trim());

                var dtEmpIdenti = taEmpIdenti.GetDataByStaffRef(hfEmpRef.Value.ToString());
                if (dtEmpIdenti.Rows.Count > 0)
                {
                    taEmpIdenti.UpdateStaffExt(cboReligion.SelectedValue.ToString(), txtNationality.Text.Trim(), cboBloodGrp.SelectedValue.ToString(), txtHomePhone.Text.Trim(),
                        txtPersonalCell.Text.Trim(), txtPersonalEmail.Text.Trim(), txtIdentiMark.Text.Trim(), txtHeight.Text.Trim(), txtWeight.Text.Trim(), "", txtNID.Text.Trim(),
                        txtTIN.Text.Trim(), txtDLNo.Text.Trim(), txtDLIssuPlace.Text.Trim(), ppDlExpDt, txtPPNo.Text.Trim(),
                        txtPPIsuPlace.Text.Trim(), ppIssDt, ppExpDt, "", "", "", hfEmpRef.Value);
                }
                else
                {
                    taEmpIdenti.InsertStaffExt(nextEmpRefNo.ToString(), nextEmpId, cboReligion.SelectedValue.ToString(), txtNationality.Text.Trim(),
                        cboBloodGrp.SelectedValue.ToString(), txtHomePhone.Text.Trim(), txtPersonalCell.Text.Trim(), txtPersonalEmail.Text.Trim(), txtIdentiMark.Text.Trim(),
                        txtHeight.Text.Trim(), txtWeight.Text.Trim(), "", txtNID.Text.Trim(), txtTIN.Text.Trim(), txtDLNo.Text.Trim(), txtDLIssuPlace.Text.Trim(),
                        ppDlExpDt, txtPPNo.Text.Trim(), txtPPIsuPlace.Text.Trim(), ppIssDt, ppExpDt, "", "", "");
                }
                #endregion

                #region Save Qualification Details

                taEmpQual.DeleteStaffQual(hfEmpRef.Value.ToString());

                var qualLno = 1;
                var dtQual = new DataTable();
                dtQual = (DataTable)ViewState["dtEmpQual"];
                foreach (DataRow row in dtQual.Rows)
                {
                    var qualTypeRef = row["QUAL_TYPE_REF"].ToString();
                    var qualType = row["QUAL_TYPE"].ToString();
                    var examRef = row["EXAM_REF"].ToString();
                    var examName = row["EXAM_NAME"].ToString();
                    var instName = row["INST_NAME"].ToString();
                    var grpSub = row["GRP_SUB"].ToString();
                    var certAuth = row["CERT_AUTH"].ToString();
                    var passYr = row["PASS_YEAR"].ToString();
                    var result = row["RESULT"].ToString();
                    var stDt = row["ST_DT"].ToString();
                    var endDt = row["END_DT"].ToString();
                    var duration = row["DURATION"].ToString();
                    var sequence = row["SEQUENCE"].ToString();
                    var extData1 = row["EXT_DATA1"].ToString();
                    var extData2 = row["EXT_DATA2"].ToString();
                    var extData3 = row["EXT_DATA3"].ToString();
                    var statRef = row["STAT_REF"].ToString();
                    var stat = row["STATUS"].ToString();
                    var flag = row["FLAG"].ToString();

                    DateTime? stDate = null;
                    DateTime? endDate = null;

                    taEmpQual.InsertStaffQual(qualTypeRef, examRef, examName, nextEmpRefNo.ToString(), instName, grpSub, certAuth, passYr, result, stDate, endDate, duration,
                        qualLno, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), extData1, extData2, extData3, "1", "");

                    qualLno++;
                }
                #endregion             

                #region Save Experience Details

                taEmpExp.DeleteStaffExp(hfEmpRef.Value.ToString());

                var expLno = 1;
                var dtExp = new DataTable();
                dtExp = (DataTable)ViewState["dtEmpExp"];
                foreach (DataRow row in dtExp.Rows)
                {
                    var expOrgName = row["EXP_ORG"].ToString();
                    var expDept = row["EXP_DEPT"].ToString();
                    var expDesig = row["EXP_DESIG"].ToString();
                    var expOrgAdr = row["EXP_ADR"].ToString();
                    var expRem = row["EXP_REM"].ToString();
                    var expFrom = row["EXP_FROM"].ToString();
                    var expTo = row["EXP_TO"].ToString();
                    var expRefName = row["EXP_REF_NAME"].ToString();
                    var expRefCont = row["EXP_REF_CONT"].ToString();
                    var expRefEmail = row["EXP_REF_EMAIL"].ToString();

                    taEmpExp.InsertStaffExp(nextEmpRefNo.ToString(), expOrgName, expDept, expDesig, 0, expOrgAdr, expRem, Convert.ToDateTime(expFrom), Convert.ToDateTime(expTo),
                        expRefName, expRefCont, expRefEmail, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                        "", "", "", "1", "");

                    expLno++;
                }
                #endregion

                #region Save OfficeInfo
                DateTime? confDt = null;
                if (txtConfDate.Text.Trim().Length > 0) confDt = Convert.ToDateTime(txtConfDate.Text.Trim());

                var dtEmpOffcInfo = taEmpOffcInfo.GetDataByStaffRef(hfEmpRef.Value.ToString());
                if (dtEmpOffcInfo.Rows.Count > 0)
                {
                    taEmpOffcInfo.UpdateStaffOffc("100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), Convert.ToDateTime(txtDOJ.Text.Trim()), cboSupr.SelectedValue.ToString(),
                        cboJobStat.SelectedValue.ToString(), Convert.ToDateTime(txtConfDueDate.Text.Trim()), confDt, cboWorkStation.SelectedValue.ToString(),
                        txtIdCardNo.Text.Trim(), cboShift.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                        txtSalBankAcc.Text.Trim(), "", txtOffEmail.Text.Trim(), txtOffPhone.Text.Trim(), txtOffPabx.Text.Trim(), txtOffIpPhone.Text.Trim(),
                        txtEmpRemarks.Text.Trim(), "", "", "", hfEmpRef.Value.ToString());
                }
                else
                {
                    taEmpOffcInfo.InsertStaffOffc(nextEmpRefNo.ToString(), nextEmpId, "100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), Convert.ToDateTime(txtDOJ.Text.Trim()), cboSupr.SelectedValue.ToString(),
                        cboJobStat.SelectedValue.ToString(), Convert.ToDateTime(txtConfDueDate.Text.Trim()), confDt, cboWorkStation.SelectedValue.ToString(),
                        txtIdCardNo.Text.Trim(), cboShift.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                        txtSalBankAcc.Text.Trim(), "", txtOffEmail.Text.Trim(), txtOffPhone.Text.Trim(), txtOffPabx.Text.Trim(), txtOffIpPhone.Text.Trim(),
                        txtEmpRemarks.Text.Trim(), "", "", "");
                }
                #endregion

                #region Save Image
                System.Drawing.Image PicImg = null;
                System.Drawing.Image SigImg = null;
                System.Drawing.Image NidImg = null;

                if (picUpload.HasFile)
                    PicImg = System.Drawing.Image.FromStream(picUpload.PostedFile.InputStream);                
                
                if (sigUpload.HasFile)                
                    SigImg = System.Drawing.Image.FromStream(sigUpload.PostedFile.InputStream);

                if (nidUpload.HasFile)
                    NidImg = System.Drawing.Image.FromStream(nidUpload.PostedFile.InputStream);

                var dtEmpImg = taEmpImg.GetDataByStaffRef(hfEmpRef.Value.ToString());
                if (dtEmpImg.Rows.Count > 0)
                {
                    if (picUpload.HasFile)
                    {
                        if (sigUpload.HasFile)
                        {
                            if (nidUpload.HasFile)
                            {
                                //update all
                                taEmpImg.UpdateStaffPicDlNid(ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    ConvertImageToByteArray(SigImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    ConvertImageToByteArray(NidImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    "", "", "", "1", "", hfEmpRef.Value.ToString());
                            }
                            else
                            {
                                //update pic_sig
                                taEmpImg.UpdatestaffPicDL(ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    ConvertImageToByteArray(SigImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                            }
                        }
                        else
                        {
                            if (nidUpload.HasFile)
                            {
                                //update pic_nid
                                taEmpImg.UpdateStaffPicNid(ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    ConvertImageToByteArray(NidImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                            }
                            else
                            {
                                //update pic
                                taEmpImg.UpdateStaffImg(ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                            }
                        }
                    }
                    else
                    {
                        if (sigUpload.HasFile)
                        {
                            if (nidUpload.HasFile)
                            {
                                //update sig_nid
                                taEmpImg.UpdateStaffDlNid(ConvertImageToByteArray(SigImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    ConvertImageToByteArray(NidImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                            }
                            else
                            {
                                //update sig
                                taEmpImg.UpdateStaffDL(ConvertImageToByteArray(SigImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                            }
                        }
                        else
                        {
                            if (nidUpload.HasFile)
                            {
                                //update nid
                                taEmpImg.UpdateStaffNID(ConvertImageToByteArray(NidImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                            }
                            else
                            {
                                //update nothing
                            }
                        }
                    }
                }
                else
                {
                    taEmpImg.InsertStaffImg(nextEmpRefNo.ToString(), nextEmpId.ToString(),
                        PicImg == null ? null : ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                        SigImg == null ? null : ConvertImageToByteArray(SigImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                        NidImg == null ? null : ConvertImageToByteArray(NidImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                        "", "", "", "1", "");
                }
                #endregion

                //#region Save Service Log
                //var dtMaxSerLogPos = taEmpServLog.GetMaxServPos(nextEmpRefNo.ToString());
                //var maxtSerLogPos = dtMaxSerLogPos == null ? 1 : Convert.ToInt32(dtMaxSerLogPos);

                //if (dtEmpGenInfo.Rows.Count > 0)
                //{
                //    taEmpServLog.UpdateEmpServLog("100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                //        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), cboSupr.SelectedValue.ToString(), cboEmpType.SelectedValue.ToString(),
                //        cboJobStat.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()), DateTime.Now,
                //        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "", nextEmpRefNo.ToString(), maxtSerLogPos);
                //}
                //else
                //{
                //    taEmpServLog.InsertEmpServLog((maxtSerLogPos + 1), "Join", DateTime.Now, nextEmpRefNo.ToString(), "100001", cboLoc.SelectedValue.ToString(),
                //        cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), cboSupr.SelectedValue.ToString(),
                //        cboEmpType.SelectedValue.ToString(), cboJobStat.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                //        DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "");
                //}
                //#endregion

                myTran.Commit();

                if (hfEmpRef.Value == "0")
                {
                    hfEmpRef.Value = nextEmpRefNo.ToString();
                    txtEmpFirstName.Enabled = false;
                    txtEmpLastName.Enabled = false;
                }

                var EmpRef = "";
                if (dtEmpGenInfo.Rows.Count > 0)
                {
                    EmpRef = hfEmpRef.Value.ToString();
                    tblMsg.Rows[0].Cells[0].InnerText = "Staff Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Staff ID No: " + txtEmpId.Text.Trim();
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    EmpRef = nextEmpRefNo.ToString();
                    txtEmpId.Text = nextEmpId.ToString();
                    tblMsg.Rows[0].Cells[0].InnerText = "Staff Data Added Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Staff ID No: " + nextEmpId.ToString();
                    ModalPopupExtenderMsg.Show();
                }

                hlEmpPicPreview.NavigateUrl = "~/Module/Transport/Tools/getStaffPic.ashx?StaffRefNo='" + EmpRef + "'";
                imgEmpPicPreview.ImageUrl = "~/Module/Transport/Tools/getStaffPic.ashx?StaffRefNo='" + EmpRef + "'";

                hlEmpPic.NavigateUrl = "~/Module/Transport/Tools/getStaffPic.ashx?StaffRefNo='" + EmpRef + "'";
                imgEmpPic.ImageUrl = "~/Module/Transport/Tools/getStaffPic.ashx?StaffRefNo='" + EmpRef + "'";

                hlEmpSig.NavigateUrl = "~/Module/Transport/Tools/getStaffDL.ashx?StaffRefNo='" + EmpRef + "'";
                imgEmpSig.ImageUrl = "~/Module/Transport/Tools/getStaffDL.ashx?StaffRefNo='" + EmpRef + "'";

                hlEmpNid.NavigateUrl = "~/Module/Transport/Tools/getStaffNid.ashx?StaffRefNo='" + EmpRef + "'";
                imgEmpNId.ImageUrl = "~/Module/Transport/Tools/getStaffNid.ashx?StaffRefNo='" + EmpRef + "'";

                btnUpdatePic.Visible = true;
                btnUpdateSig.Visible = true;
                btnUpdateNid.Visible = true;

                btnDeletePic.Visible = true;
                btnDeleteSig.Visible = true;
                btnDeleteNid.Visible = true;

                btnSaveGenInfo.Visible = true;
                btnSaveIndeni.Visible = true;
                btnSaveQual.Visible = true;
                btnSaveExp.Visible = true;
                btnSaveOffInfo.Visible = true;
                btnSaveEmp.Visible = true;

                btnClear.Visible = true;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
        #endregion

        #region Delete Pic_Sig_Nid
        protected void btnDeletePic_Click(object sender, EventArgs e)
        {
            var taEmpImg = new tbl_TrTr_Staff_ImgTableAdapter();

            try
            {
                var dtEmpImg = taEmpImg.GetDataByStaffRef(hfEmpRef.Value.ToString());

                if (dtEmpImg.Rows.Count > 0)
                {
                    taEmpImg.UpdateStaffImg(null, hfEmpRef.Value.ToString());
                }

                tblMsg.Rows[0].Cells[0].InnerText = "Picture deleted successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error" + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnDeleteSig_Click(object sender, EventArgs e)
        {
            var taEmpImg = new tbl_TrTr_Staff_ImgTableAdapter();

            try
            {
                var dtEmpImg = taEmpImg.GetDataByStaffRef(hfEmpRef.Value.ToString());

                if (dtEmpImg.Rows.Count > 0)
                {
                    taEmpImg.UpdateStaffDL(null, hfEmpRef.Value.ToString());
                }
                tblMsg.Rows[0].Cells[0].InnerText = "Signature deleted successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error" + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnDeleteNid_Click(object sender, EventArgs e)
        {
            var taEmpImg = new tbl_TrTr_Staff_ImgTableAdapter();

            try
            {
                var dtEmpImg = taEmpImg.GetDataByStaffRef(hfEmpRef.Value.ToString());

                if (dtEmpImg.Rows.Count > 0)
                {
                    taEmpImg.UpdateStaffNID(null, hfEmpRef.Value.ToString());
                }
                tblMsg.Rows[0].Cells[0].InnerText = "NID deleted successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error" + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
        #endregion

        protected void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taDept = new tblHrmsDeptTableAdapter();
            try
            {
                var dtDept = taDept.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                cboDept.DataSource = dtDept;
                cboDept.DataValueField = "DeptRefNo";
                cboDept.DataTextField = "DeptName";
                cboDept.DataBind();
                cboDept.Items.Insert(0, new ListItem("---Select---", "0"));

                cboSec.Items.Clear();
                cboSec.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            catch (Exception ex) { }
        }

        protected void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taSec = new tblHrmsSecTableAdapter();
            try
            {
                var dtSec = taSec.GetDataByDeptRef(cboDept.SelectedValue.ToString());
                cboSec.DataSource = dtSec;
                cboSec.DataValueField = "SecRefNo";
                cboSec.DataTextField = "SecName";
                cboSec.DataBind();
                cboSec.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            catch (Exception ex) { }
        }

        protected void btnSettle_Click(object sender, EventArgs e)
        {
            ddlSettleType.SelectedIndex = 0;
            txtSettleDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ModalPopupExtenderSettle.Show();
        }

        protected void empSettlement()
        {
            var taEmpMas = new tbl_TrTr_Staff_MasTableAdapter();
            //var taEmpServLog = new tblHrmsEmpServLogTableAdapter();
            
            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpMas.Connection);

            try
            {
                taEmpMas.AttachTransaction(myTran);
                //taEmpServLog.AttachTransaction(myTran);

                taEmpMas.UpdateSettlement("Y", Convert.ToDateTime(txtSettleDate.Text.Trim()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "0", "", hfEmpRef.Value.ToString());

                //#region Update Service Log
                //var dtMaxSerLogPos = taEmpServLog.GetMaxServPos(hfEmpRef.Value.ToString());
                //var maxtSerLogPos = dtMaxSerLogPos == null ? 1 : Convert.ToInt32(dtMaxSerLogPos) + 1;
                ////if (taEmpMas.Rows.Count > 0)
                ////{
                ////    taEmpServLog.UpdateEmpServLog("100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                ////        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), cboSupr.SelectedValue.ToString(), cboEmpType.SelectedValue.ToString(),
                ////        cboJobStat.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()), DateTime.Now,
                ////        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "", nextEmpRefNo.ToString(), maxtSerLogPos);
                ////}
                ////else
                ////{
                //taEmpServLog.InsertEmpServLog(maxtSerLogPos, ddlSettleType.SelectedItem.Text, Convert.ToDateTime(txtSettleDate.Text.Trim()), hfEmpRef.Value.ToString(), "100001", cboLoc.SelectedValue.ToString(),
                //        cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), cboSupr.SelectedValue.ToString(),
                //        cboEmpType.SelectedValue.ToString(), cboJobStat.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                //        DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "");
                ////}
                //#endregion                
                
                myTran.Commit();

                btnSettle.Visible = false;

                tblMsg.Rows[0].Cells[0].InnerText = "Staff Settelement has been done.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                try
                {
                    var taEmpUserInfo = new TBL_USER_INFOTableAdapter();
                    var dtEmpUserInfo = taEmpUserInfo.GetDataByEmpRef(hfEmpRef.Value.ToString());
                    if (dtEmpUserInfo.Rows.Count > 0)
                        taEmpUserInfo.UpdateUserStatus(2, Convert.ToDateTime(txtSettleDate.Text.Trim()), dtEmpUserInfo[0].User_Ref_No);
                }
                catch (Exception ex)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (txtSettleDate.Text.Trim() == "")
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Enter Settle Date First.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
            empSettlement();
        }

        protected void chkIncTermEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncTermEmp.Checked)
                AutoCompleteExtenderEmp.ContextKey = "1";
            else
                AutoCompleteExtenderEmp.ContextKey = "0";
        }        
    }
}