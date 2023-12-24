using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.SYS.DataSet;
using DRN_WEB_ERP.Module.SYS.DataSet.dsSysTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmEmpMas : System.Web.UI.Page
    {
        const int maxSize=2097151;

        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                AutoCompleteExtenderEmp.ContextKey = "0";
                AutoCompleteExtenderSupr.ContextKey = "0";

                var taEmpGenInfo = new tblHrmsEmpTableAdapter();
                var dtMaxEmpRef = taEmpGenInfo.GetMaxEmpRefNo();
                var nextEmpRefNo = dtMaxEmpRef == null ? "000001" : (Convert.ToInt32(dtMaxEmpRef) + 1).ToString();
                var nextEmpId = "EMP-" + Convert.ToInt32(nextEmpRefNo).ToString("000000");
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

                var taEmpList = new View_Emp_BascTableAdapter();
                //cboSupr.DataSource = taEmpList.GetDataByAsc();
                //cboSupr.DataValueField = "EmpRefNo";
                //cboSupr.DataTextField = "EmpName";
                //cboSupr.DataBind();
                //cboSupr.Items.Insert(0, new ListItem("---Select---", "0"));

                /*---Blocked on 20-02-2020
                var dt = taEmpList.GetDataByAsc();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var empRef = dt.Rows[i]["EmpRefNo"].ToString();
                        var supNameId = dt.Rows[i]["EmpName"].ToString() + " - [" + dt.Rows[i]["EmpId"].ToString() + "]";
                        cboSupr.Items.Add(new ListItem(supNameId, empRef));
                    }
                }
                cboSupr.Items.Insert(0, new ListItem("---Select---", "0"));
                */

                //cboSupervisor.DataSource = taEmpList.GetDataByAsc();
                //cboSupervisor.DataValueField = "EmpRefNo";
                //cboSupervisor.DataTextField = "EmpName";
                //cboSupervisor.DataBind();
                //cboSupervisor.Items.Insert(0, new ListItem("---Select---", "0"));

                var taJobStat = new tblHrmsJobStatTableAdapter();
                cboJobStat.DataSource = taJobStat.GetDataByAsc();
                cboJobStat.DataValueField = "JobStatRef";
                cboJobStat.DataTextField = "JobStatName";
                cboJobStat.DataBind();
                cboJobStat.Items.Insert(0, new ListItem("---Select---", "0"));

                var taEmpType = new tblHrmsEmpTypeTableAdapter();
                cboEmpType.DataSource = taEmpType.GetDataByAsc();
                cboEmpType.DataValueField = "EmpTypeRef";
                cboEmpType.DataTextField = "EmpTypeName";
                cboEmpType.DataBind();
                cboEmpType.Items.Insert(0, new ListItem("---Select---", "0"));

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

                var taWrkStation = new tblHrmsWorkStationTableAdapter();
                cboWrkStation.DataSource = taWrkStation.GetDataBySortAsc();
                cboWrkStation.DataValueField = "Wrk_Station_Ref";
                cboWrkStation.DataTextField = "Wrk_Station_Name";
                cboWrkStation.DataBind();
                cboWrkStation.Items.Insert(0, new ListItem("---Select---", "0"));

                var taSettlType = new tblHrmsSettlmentTypeTableAdapter();
                ddlSettleType.DataSource = taSettlType.GetDataBySortName();
                ddlSettleType.DataValueField = "HrmsSettlTypeRef";
                ddlSettleType.DataTextField = "HrmsSettlTypeName";
                ddlSettleType.DataBind();
                ddlSettleType.Items.Insert(0, new ListItem("---Select---", "0"));
                #endregion

                AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabEmpInfo;
                foreach (object obj in container.Controls)
                {
                    if (obj is AjaxControlToolkit.TabPanel)
                    {
                        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                        //    foreach (object ctrl in tabPanel.Controls)
                        //    {
                        //        if (ctrl is Control)
                        //        {
                        //            Control c = (Control)ctrl;
                        //            foreach (object innerCtrl in c.Controls)
                        //            {
                        //                if (innerCtrl is System.Web.UI.WebControls.TextBox)
                        //                    Response.Write(((TextBox)innerCtrl).Text);
                        //            }
                        //        }
                        //    }

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
            var taEmpGenInfo =new  tblHrmsEmpTableAdapter();
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

                var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(empRef.ToString());
                if (dtEmpGenInfo.Rows.Count > 0)
                {                    
                    btnClear.Visible = true;

                    hfEmpRef.Value = dtEmpGenInfo[0].EmpRefNo.ToString();

                    txtEmpId.Text = dtEmpGenInfo[0].EmpId.ToString();
                    txtEmpFirstName.Text = dtEmpGenInfo[0].EmpFirstName.ToString();
                    txtEmpLastName.Text = dtEmpGenInfo[0].EmpLastName.ToString();
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
                    txtEmpFathersName.Text = dtEmpGenInfo[0].EmpFatherName.ToString();
                    txtEmpMothersName.Text = dtEmpGenInfo[0].EmpMotherName.ToString();
                    cboEmpGender.SelectedValue = dtEmpGenInfo[0].EmpGender.ToString();
                    cboEmpMaritalStat.SelectedValue = dtEmpGenInfo[0].EmpMaritalStatus.ToString();
                    txtEmpSpouseName.Text = dtEmpGenInfo[0].EmpSpouse.ToString();
                    txtEmpDOB.Text = dtEmpGenInfo[0].IsEmpDOBNull() ? "" : dtEmpGenInfo[0].EmpDOB.ToString("dd/MM/yyyy");
                    txtEmpPOB.Text = dtEmpGenInfo[0].EmpPOB.ToString();

                    btnSettle.Enabled = dtEmpGenInfo[0].EmpSetlFlag.ToString() == "N" ? true : false;

                    btnViewProfile.Enabled = true;

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

                    hlEmpPicPreview.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";
                    imgEmpPicPreview.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";

                    hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";
                    imgEmpPic.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";

                    hlEmpSig.NavigateUrl = "~/Module/HRMS/Tools/getEmpSig.ashx?EmpRefNo='" + empRef + "'";
                    imgEmpSig.ImageUrl = "~/Module/HRMS/Tools/getEmpSig.ashx?EmpRefNo='" + empRef + "'";

                    hlEmpNid.NavigateUrl = "~/Module/HRMS/Tools/getEmpNid.ashx?EmpRefNo='" + empRef + "'";
                    imgEmpNId.ImageUrl = "~/Module/HRMS/Tools/getEmpNid.ashx?EmpRefNo='" + empRef + "'";

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
                    var taEmpAdr = new tblHrmsEmpAdrTableAdapter();
                    var dtEmpAdr = taEmpAdr.GetDataByEmpRef(empRef);
                    if (dtEmpAdr.Rows.Count > 0)
                    {
                        txtEmpPresAdrHouse.Text = dtEmpAdr[0].EmpCurAdrHouseRoad.ToString();
                        txtEmpPresAdrPO.Text = dtEmpAdr[0].EmpCurAdrPO.ToString();
                        txtEmpPresAdrPOCode.Text = dtEmpAdr[0].EmpCurAdrPOCode.ToString();
                        txtEmpPresAdrThana.Text = dtEmpAdr[0].EmpCurAdrThana.ToString();
                        txtEmpPresAdrDist.Text = dtEmpAdr[0].EmpCurAdrDist.ToString();

                        txtEmpPrmAdrHouse.Text = dtEmpAdr[0].EmpPermAdrHouseRoad.ToString();
                        txtEmpPrmAdrPO.Text = dtEmpAdr[0].EmpPermAdrPO.ToString();
                        txtEmpPrmAdrPOCode.Text = dtEmpAdr[0].EmpPermAdrPOCode.ToString();
                        txtEmpPrmAdrThana.Text = dtEmpAdr[0].EmpPermAdrThana.ToString();
                        txtEmpPrmAdrDist.Text = dtEmpAdr[0].EmpPermAdrDist.ToString();

                        txtEmpEmerAdrName.Text = dtEmpAdr[0].EmpEmerCPName.ToString();
                        txtEmpEmerAdrRelation.Text = dtEmpAdr[0].EmpEmerCPRelation.ToString();
                        txtEmpEmerAdrHouse.Text = dtEmpAdr[0].EmpEmerCPHouseRoad.ToString();
                        txtEmpEmerAdrPO.Text = dtEmpAdr[0].EmpEmerCPPO.ToString();
                        txtEmpEmerAdrPOCode.Text = dtEmpAdr[0].EmpEmerCPPOCode.ToString();
                        txtEmpEmerAdrThana.Text = dtEmpAdr[0].EmpEmerCPThana.ToString();
                        txtEmpEmerAdrDist.Text = dtEmpAdr[0].EmpEmerCPDist.ToString();
                        txtEmpEmerAdrPhone.Text = dtEmpAdr[0].EmpEmerCPPhone.ToString();
                        txtEmpEmerAdrCell.Text = dtEmpAdr[0].EmpEmerCPCell.ToString();
                    }
                    #endregion

                    #region GetIndentification
                    var taEmpExt = new tblHrmsEmpExtTableAdapter();
                    var dtEmpExt = taEmpExt.GetDataByEmpRef(empRef);
                    if (dtEmpExt.Rows.Count > 0)
                    {
                        cboBloodGrp.SelectedValue = dtEmpExt[0].EmpBloodgrp.ToString();
                        cboReligion.SelectedValue = dtEmpExt[0].EmpReligion.ToString();
                        txtNationality.Text = dtEmpExt[0].EmpNationality.ToString();
                        txtHomePhone.Text = dtEmpExt[0].EmpHomephone.ToString();
                        txtPersonalCell.Text = dtEmpExt[0].EmpCellPhone.ToString();
                        txtPersonalEmail.Text = dtEmpExt[0].EmpPerEmail.ToString();
                        txtIdentiMark.Text = dtEmpExt[0].EmpIdentiMark.ToString();
                        txtHeight.Text = dtEmpExt[0].EmpHeight.ToString();
                        txtWeight.Text = dtEmpExt[0].EmpWeight.ToString();

                        txtNID.Text = dtEmpExt[0].EmpNID.ToString();
                        txtTIN.Text = dtEmpExt[0].EmpTIN.ToString();
                        txtPPNo.Text = dtEmpExt[0].EmpPasNo.ToString();
                        txtPPIsuDate.Text = dtEmpExt[0].IsEmpPasIssDateNull() ? "" : dtEmpExt[0].EmpPasIssDate.ToString("dd/MM/yyyy");
                        txtPPExpDate.Text = dtEmpExt[0].IsEmpPasExprDateNull() ? "" : dtEmpExt[0].EmpPasExprDate.ToString("dd/MM/yyyy");
                        txtPPIsuPlace.Text = dtEmpExt[0].EmpPasIssFrom.ToString();
                        txtDLNo.Text = dtEmpExt[0].EmpDLNo.ToString();
                        txtDLExprireDate.Text = dtEmpExt[0].IsEmpDLExpDateNull() ? "" : dtEmpExt[0].EmpDLExpDate.ToString("dd/MM/yyyy");
                        txtDLIssuPlace.Text = dtEmpExt[0].EmpDLIssFrom.ToString();
                    }
                    #endregion

                    #region GetQualInfo
                    var taEmpQual = new tblHrmsEmpQualTableAdapter();
                    var dtEmpQual = taEmpQual.GetDataByEmpRef(empRef);

                    LoadInitEmpQualGridData();
                    SetEmpQualGridData();

                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtEmpQual"];

                    foreach (dsEmpDet.tblHrmsEmpQualRow dr in dtEmpQual.Rows)
                    {
                        var qualTypeName = "";
                        var taQualType = new tblHrmsQualTypeTableAdapter();
                        var dtQualType = taQualType.GetDataByRefNo(Convert.ToInt32(dr.QualType));
                        if (dtQualType.Rows.Count > 0) qualTypeName = dtQualType[0].QualTypeName.ToString();

                        var statName = "";
                        var taStat = new tblStatusInfoTableAdapter();
                        var dtStat = taStat.GetDataByRefNo(Convert.ToInt32(dr.EmpQualStatus));
                        if (dtStat.Rows.Count > 0) statName = dtStat[0].StatusName.ToString();

                        dt.Rows.Add(dr.QualType.ToString(), qualTypeName, dr.QualRefNo.ToString(), dr.QualName.ToString(), dr.EmpQualInstName.ToString(),
                            dr.EmpQualGrpSub.ToString(), "", dr.EmpQualPassYear.ToString(), dr.EmpQualResult.ToString(), "", "", "0", dr.EmpQualSeqNo.ToString(),
                            dr.EmpQualExtData1.ToString(), "", "", dr.EmpQualStatus.ToString(), statName, "");
                    }

                    ViewState["dtEmpQual"] = dt;
                    SetEmpQualGridData();
                    #endregion

                    #region GetExpInfo
                    var taEmpExp = new tblHrmsEmpExpTableAdapter();
                    var dtEmpExp = taEmpExp.GetDataByEmpRefNo(empRef);

                    LoadInitEmpExpGridData();
                    SetEmpExpGridData();

                    var dtExp = new DataTable();
                    dtExp = (DataTable)ViewState["dtEmpExp"];

                    foreach (dsEmpDet.tblHrmsEmpExpRow dr in dtEmpExp.Rows)
                    {
                        dtExp.Rows.Add(dr.EmpExpCompName.ToString(), dr.EmpExpDept.ToString(), dr.EmpExpDesig.ToString(), dr.EmpExpCompAddr.ToString(),
                            dr.EmpExpDateFrom.ToString("dd/MM/yyyy"), dr.EmpExpDateTo.ToString("dd/MM/yyyy"), dr.EmpExpRem.ToString(),
                            dr.EmpExpRefName.ToString(), dr.EmpExpRefContact.ToString(), dr.EmpExpRefEmail.ToString());
                    }

                    ViewState["dtEmpExp"] = dtExp;
                    SetEmpExpGridData();
                    #endregion

                    #region OfficialInfo
                    var taEmpOffc = new tblHrmsEmpOfficeTableAdapter();
                    var dtEmpOffc = taEmpOffc.GetDataByEmpRef(empRef);
                    if (dtEmpOffc.Rows.Count > 0)
                    {
                        cboLoc.SelectedValue = dtEmpOffc[0].OffLocRefNo.ToString();
                        
                        var taDept = new tblHrmsDeptTableAdapter();
                        var dtDept = taDept.GetDataByLocRef(dtEmpOffc[0].OffLocRefNo.ToString());
                        cboDept.DataSource = dtDept;
                        cboDept.DataValueField = "DeptRefNo";
                        cboDept.DataTextField = "DeptName";
                        cboDept.DataBind();
                        cboDept.Items.Insert(0, new ListItem("---Select---", "0"));
                        cboDept.SelectedValue = cboDept.Items.FindByValue(dtEmpOffc[0].DeptRefNo.ToString()).ToString() != null ? dtEmpOffc[0].DeptRefNo.ToString() : "0";

                        var taSec = new tblHrmsSecTableAdapter();
                        var dtSec = taSec.GetDataByDeptRef(dtEmpOffc[0].DeptRefNo.ToString());
                        cboSec.DataSource = dtSec;
                        cboSec.DataValueField = "SecRefNo";
                        cboSec.DataTextField = "SecName";
                        cboSec.DataBind();
                        cboSec.Items.Insert(0, new ListItem("---Select---", "0"));
                        cboSec.SelectedValue = cboSec.Items.FindByValue(dtEmpOffc[0].SecRefNo.ToString()).ToString() != null ? dtEmpOffc[0].SecRefNo.ToString() : "0";
                        cboDesig.SelectedValue = cboDesig.Items.FindByValue(dtEmpOffc[0].DesigRefNo.ToString()).ToString() != null ? dtEmpOffc[0].DesigRefNo.ToString() : "0";
                        txtDOJ.Text = dtEmpOffc[0].IsEmpDOJNull() ? "" : dtEmpOffc[0].EmpDOJ.ToString("dd/MM/yyyy");
                        
                        var taViewEmp = new View_Emp_BascTableAdapter();
                        var dtSupr = taViewEmp.GetDataByEmpRef(Convert.ToInt32(dtEmpOffc[0].EmpSuprId.ToString()));
                        if (dtSupr.Rows.Count > 0)
                            txtSupr.Text = dtSupr[0].EmpRefNo.ToString() + ":" + dtSupr[0].EmpName.ToString();
                        //cboSupr.SelectedValue = cboSupr.Items.FindByValue(dtEmpOffc[0].EmpSuprId.ToString()).ToString() != null ? dtEmpOffc[0].EmpSuprId.ToString() : "0";
                        
                        cboJobStat.SelectedValue = cboJobStat.Items.FindByValue(dtEmpOffc[0].EmpJobStatus.ToString()).ToString() != null ? dtEmpOffc[0].EmpJobStatus.ToString() : "0";
                        txtConfDueDate.Text = dtEmpOffc[0].IsEmpConfDueDateNull() ? "" : dtEmpOffc[0].EmpConfDueDate.ToString("dd/MM/yyyy");
                        txtConfDate.Text = dtEmpOffc[0].IsEmpConfDateNull() ? "" : dtEmpOffc[0].EmpConfDate.ToString("dd/MM/yyyy");
                        cboEmpType.SelectedValue = cboEmpType.Items.FindByValue(dtEmpOffc[0].EmpType.ToString()).ToString() != null ? dtEmpOffc[0].EmpType.ToString() : "0";
                        txtIdCardNo.Text = dtEmpOffc[0].EmpCardId.ToString();
                        cboShift.SelectedValue = cboShift.Items.FindByValue(dtEmpOffc[0].ShiftRefNo.ToString()).ToString() != null ? dtEmpOffc[0].ShiftRefNo.ToString() : "0";
                        cboGrade.SelectedValue = cboGrade.Items.FindByValue(dtEmpOffc[0].EmpGrade.ToString()).ToString() != null ? dtEmpOffc[0].EmpGrade.ToString() : "0";
                        txtSalary.Text = dtEmpOffc[0].EmpSalary.ToString("F2");
                        txtSalBankAcc.Text = dtEmpOffc[0].BankAccNo.ToString();

                        var TA = "0";
                        var DA = "0";
                        var OTH = "0";
                        int cnt = 0;
                        var srchTA_DA = dtEmpOffc[0].EmpOffExtData2.ToString().Trim().Split('+');
                        foreach (string word in srchTA_DA)
                        {
                            if (cnt == 0 && word.Length > 0)
                                TA = word;
                            if (cnt == 1 && word.Length > 0)
                                DA = word;
                            if (cnt == 2 && word.Length > 0)
                                OTH = word;

                            cnt++;
                        }

                        txtTA.Text = Convert.ToDecimal(TA).ToString("F2");
                        txtDA.Text = Convert.ToDecimal(DA).ToString("F2");
                        txtOthAllowence.Text = Convert.ToDecimal(OTH).ToString("F2");

                        txtOffEmail.Text = dtEmpOffc[0].EmpOffEmail.ToString();
                        txtOffPhone.Text = dtEmpOffc[0].EmpWorkPhone.ToString();
                        txtOffPabx.Text = dtEmpOffc[0].EmpPabxNo.ToString();
                        txtOffIpPhone.Text = dtEmpOffc[0].EmpIpPhone.ToString();

                        if (dtEmpOffc[0].EmpOffExtData1.ToString() == "")
                            cboWrkStation.SelectedIndex = 0;
                        else
                            cboWrkStation.SelectedValue = dtEmpOffc[0].EmpOffExtData1.ToString();   
                        //cboWrkStation.Items.FindByValue(dtEmpOffc[0].EmpOffExtData1.ToString()).ToString();
                    }
                    #endregion

                    //hlEmpPicPreview.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";
                    //imgEmpPicPreview.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";

                    //hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";
                    //imgEmpPic.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";

                    //hlEmpSig.NavigateUrl = "~/Module/HRMS/Tools/getEmpSig.ashx?EmpRefNo='" + empRef + "'";
                    //imgEmpSig.ImageUrl = "~/Module/HRMS/Tools/getEmpSig.ashx?EmpRefNo='" + empRef + "'";

                    //hlEmpNid.NavigateUrl = "~/Module/HRMS/Tools/getEmpNid.ashx?EmpRefNo='" + empRef + "'";
                    //imgEmpNId.ImageUrl = "~/Module/HRMS/Tools/getEmpNid.ashx?EmpRefNo='" + empRef + "'";

                    //AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabEmpInfo;
                    //foreach (object obj in container.Controls)
                    //{
                    //    if (obj is AjaxControlToolkit.TabPanel)
                    //    {
                    //        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;                           
                    //        tabPanel.Enabled = true;
                    //    }
                    //}

                    //btnUpdatePic.Visible = true;
                    //btnUpdateSig.Visible = true;
                    //btnUpdateNid.Visible = true;

                    //btnDeletePic.Visible = true;
                    //btnDeleteSig.Visible = true;
                    //btnDeleteNid.Visible = true;

                    //btnSaveGenInfo.Visible = true;
                    //btnSaveIndeni.Visible = true;
                    //btnSaveQual.Visible = true;
                    //btnSaveExp.Visible = true;
                    //btnSaveOffInfo.Visible = true;
                    //btnSaveEmp.Visible = true;
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
            var taEmpGenInfo = new tblHrmsEmpTableAdapter();
            var dtMaxEmpRef = taEmpGenInfo.GetMaxEmpRefNo();
            var nextEmpRefNo = dtMaxEmpRef == null ? "000001" : (Convert.ToInt32(dtMaxEmpRef) + 1).ToString();
            var nextEmpId = "EMP-" + Convert.ToInt32(nextEmpRefNo).ToString("000000");
            txtEmpId.Text = nextEmpId.ToString();            
            hfEmpRef.Value = "0";
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

            cboEmpType.SelectedIndex = 0;
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
            //cboSupr.SelectedIndex = 0;
            txtSupr.Text = "";
            cboJobStat.SelectedIndex = 0;
            txtConfDueDate.Text = "";
            txtConfDate.Text = "";
            cboEmpType.SelectedIndex = 0;
            cboWrkStation.SelectedIndex = 0;
            txtIdCardNo.Text = "";
            cboShift.SelectedIndex = 0;
            cboGrade.SelectedIndex = 0;
            txtSalary.Text = "";
            txtSalBankAcc.Text = "";
            txtTA.Text = "";
            txtDA.Text = "";
            txtOthAllowence.Text = "";
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

            btnViewProfile.Enabled = false;
            btnSettle.Enabled = false;

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
            var taEmpGenInfo = new tblHrmsEmpTableAdapter();
            var taEmpAdrInfo = new tblHrmsEmpAdrTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpGenInfo.Connection);

            try
            {
                taEmpGenInfo.AttachTransaction(myTran);
                taEmpAdrInfo.AttachTransaction(myTran);

                var dtMaxEmpRef = taEmpGenInfo.GetMaxEmpRefNo();
                var nextEmpRefNo = dtMaxEmpRef == null ? "000001" : (Convert.ToInt32(dtMaxEmpRef) + 1).ToString("000000");
                var nextEmpId = "EMP-" + Convert.ToInt32(nextEmpRefNo).ToString("000000");

                var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(hfEmpRef.Value.ToString());
                if (dtEmpGenInfo.Rows.Count > 0)
                {
                    taEmpGenInfo.UpdateEmp(txtEmpFirstName.Text.Trim(), txtEmpLastName.Text.Trim(), txtEmpFathersName.Text.Trim(),
                        txtEmpMothersName.Text.Trim(), cboEmpGender.SelectedValue.ToString(), cboEmpMaritalStat.SelectedValue.ToString(), txtEmpSpouseName.Text.Trim(),
                        Convert.ToDateTime(txtEmpDOB.Text.Trim()), txtEmpPOB.Text.Trim(), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "", hfEmpRef.Value.ToString());
                }
                else
                {
                    taEmpGenInfo.InsertEmp(nextEmpRefNo, nextEmpId, txtEmpFirstName.Text.Trim(), txtEmpLastName.Text.Trim(), txtEmpFathersName.Text.Trim(),
                        txtEmpMothersName.Text.Trim(), cboEmpGender.SelectedValue.ToString(), cboEmpMaritalStat.SelectedValue.ToString(), txtEmpSpouseName.Text.Trim(),
                        Convert.ToDateTime(txtEmpDOB.Text.Trim()), txtEmpPOB.Text.Trim(), "Y", "N", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "");
                }

                var dtEmpAdrInfo = taEmpAdrInfo.GetDataByEmpRef(hfEmpRef.Value.ToString());
                if (dtEmpAdrInfo.Rows.Count > 0)
                {
                    taEmpAdrInfo.UpdateEmpAdr(txtEmpPresAdrHouse.Text.Trim(), txtEmpPresAdrPO.Text.Trim(), txtEmpPresAdrPOCode.Text.Trim(),
                            txtEmpPresAdrThana.Text.Trim(), txtEmpPresAdrDist.Text.Trim(), txtEmpPrmAdrHouse.Text.Trim(), txtEmpPrmAdrPO.Text.Trim(), txtEmpPrmAdrPOCode.Text.Trim(),
                            txtEmpPrmAdrThana.Text.Trim(), txtEmpPrmAdrDist.Text.Trim(), "", "", "", "", "", txtEmpEmerAdrName.Text.Trim(), txtEmpEmerAdrRelation.Text.Trim(),
                            txtEmpEmerAdrHouse.Text.Trim(), txtEmpEmerAdrPO.Text.Trim(), txtEmpEmerAdrPOCode.Text.Trim(), txtEmpEmerAdrThana.Text.Trim(),
                            txtEmpEmerAdrDist.Text.Trim(), txtEmpEmerAdrPhone.Text.Trim(), txtEmpEmerAdrCell.Text.Trim(), "1", "", hfEmpRef.Value.ToString());
                }
                else
                {
                    taEmpAdrInfo.InsertEmpAdr(nextEmpRefNo.ToString(), nextEmpId, txtEmpPresAdrHouse.Text.Trim(), txtEmpPresAdrPO.Text.Trim(), txtEmpPresAdrPOCode.Text.Trim(),
                            txtEmpPresAdrThana.Text.Trim(), txtEmpPresAdrDist.Text.Trim(), txtEmpPrmAdrHouse.Text.Trim(), txtEmpPrmAdrPO.Text.Trim(), txtEmpPrmAdrPOCode.Text.Trim(),
                            txtEmpPrmAdrThana.Text.Trim(), txtEmpPrmAdrDist.Text.Trim(), "", "", "", "", "", txtEmpEmerAdrName.Text.Trim(), txtEmpEmerAdrRelation.Text.Trim(),
                            txtEmpEmerAdrHouse.Text.Trim(), txtEmpEmerAdrPO.Text.Trim(), txtEmpEmerAdrPOCode.Text.Trim(), txtEmpEmerAdrThana.Text.Trim(),
                            txtEmpEmerAdrDist.Text.Trim(), txtEmpEmerAdrPhone.Text.Trim(), txtEmpEmerAdrCell.Text.Trim(), "1", "");
                }

                myTran.Commit();

                if (dtEmpGenInfo.Rows.Count > 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Employee Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Employee ID No: " + txtEmpId.Text.Trim();
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Employee Data Added Successfully.";
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
            var taEmpGenInfo = new tblHrmsEmpTableAdapter();
            var taEmpIdenti = new tblHrmsEmpExtTableAdapter();

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

                var dtEmpIdentiInfo = taEmpIdenti.GetDataByEmpRef(hfEmpRef.Value.ToString());
                if (dtEmpIdentiInfo.Rows.Count > 0)
                {
                    taEmpIdenti.UpdateEmpExt(cboReligion.SelectedValue.ToString(), txtNationality.Text.Trim(), cboBloodGrp.SelectedValue.ToString(), txtHomePhone.Text.Trim(),
                        txtPersonalCell.Text.Trim(), txtPersonalEmail.Text.Trim(), txtIdentiMark.Text.Trim(), txtHeight.Text.Trim(), txtWeight.Text.Trim(), "", txtNID.Text.Trim(),
                        txtTIN.Text.Trim(), txtDLNo.Text.Trim(), txtDLIssuPlace.Text.Trim(), ppDlExpDt, txtPPNo.Text.Trim(),
                        txtPPIsuPlace.Text.Trim(), ppIssDt, ppExpDt, "", "", "", hfEmpRef.Value);

                    myTran.Commit();

                    tblMsg.Rows[0].Cells[0].InnerText = "Employee Identification Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Employee ID No: " + txtEmpId.Text.Trim();
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    taEmpIdenti.InsertEmpExt(hfEmpRef.Value.ToString(), txtEmpId.Text.Trim(), cboReligion.SelectedValue.ToString(), txtNationality.Text.Trim(),
                        cboBloodGrp.SelectedValue.ToString(), txtHomePhone.Text.Trim(), txtPersonalCell.Text.Trim(), txtPersonalEmail.Text.Trim(), txtIdentiMark.Text.Trim(),
                        txtHeight.Text.Trim(), txtWeight.Text.Trim(), "", txtNID.Text.Trim(), txtTIN.Text.Trim(), txtDLNo.Text.Trim(), txtDLIssuPlace.Text.Trim(),
                        ppDlExpDt, txtPPNo.Text.Trim(), txtPPIsuPlace.Text.Trim(), ppIssDt, ppExpDt, "", "", "");

                    myTran.Commit();

                    tblMsg.Rows[0].Cells[0].InnerText = "Employee Identification Added Successfully.";
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

                cboEmpType.SelectedIndex = 0;
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
            var taEmpGenInfo = new tblHrmsEmpTableAdapter();
            var taEmpQual = new tblHrmsEmpQualTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpGenInfo.Connection);

            try
            {
                taEmpGenInfo.AttachTransaction(myTran);
                taEmpQual.AttachTransaction(myTran);

                taEmpQual.DeleteEmpQual(hfEmpRef.Value.ToString());

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

                    taEmpQual.InsertEmpQual(qualTypeRef, examRef, examName, hfEmpRef.Value.ToString(), instName, grpSub, certAuth, passYr, result, stDate, endDate, duration, Lno,
                        DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), extData1, extData2, extData3, "1", "");

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
            cboEmpType.SelectedIndex = 0;
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
            var taEmpGenInfo = new tblHrmsEmpTableAdapter();
            var taEmpExp = new tblHrmsEmpExpTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpGenInfo.Connection);

            try
            {
                taEmpGenInfo.AttachTransaction(myTran);
                taEmpExp.AttachTransaction(myTran);

                taEmpExp.DeleteEmpExp(hfEmpRef.Value.ToString());

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

                    taEmpExp.InsertEmpExp(hfEmpRef.Value.ToString(), expOrgName, expDept, expDesig, 0, expOrgAdr, expRem, Convert.ToDateTime(expFrom), Convert.ToDateTime(expTo),
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
            var taEmpGenInfo = new tblHrmsEmpTableAdapter();
            var taEmpOffcInfo = new tblHrmsEmpOfficeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpGenInfo.Connection);

            try
            {
                #region Validate Supervisor
                var supRef = "";
                var srchWords = txtSupr.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    supRef = word;
                    break;
                }

                if (supRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(supRef, out result))
                    {
                        var taViewEmp = new View_Emp_BascTableAdapter();
                        var dtViewEmp = taViewEmp.GetDataByEmpRef(Convert.ToInt32(supRef.ToString()));
                        if (dtViewEmp.Rows.Count > 0)
                        {
                            supRef = dtViewEmp[0].EmpRefNo.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Reporting person.";
                            tblMsg.Rows[1].Cells[0].InnerText = "Enter valid reporting person.";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Reporting person.";
                        tblMsg.Rows[1].Cells[0].InnerText = "Enter valid reporting person.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Reporting person.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Enter valid reporting person.";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                DateTime? confDt = null;
                if (txtConfDate.Text.Trim().Length > 0) confDt = Convert.ToDateTime(txtConfDate.Text.Trim());

                taEmpGenInfo.AttachTransaction(myTran);
                taEmpOffcInfo.AttachTransaction(myTran);

                var dtEmpOffcInfo = taEmpOffcInfo.GetDataByEmpRef(hfEmpRef.Value.ToString());
                if (dtEmpOffcInfo.Rows.Count > 0)
                {
                    taEmpOffcInfo.UpdateEmpOff("100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), Convert.ToDateTime(txtDOJ.Text.Trim()), supRef.ToString(),
                        cboJobStat.SelectedValue.ToString(), Convert.ToDateTime(txtConfDueDate.Text.Trim()), confDt, cboEmpType.SelectedValue.ToString(),
                        txtIdCardNo.Text.Trim(), cboShift.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                        txtSalBankAcc.Text.Trim(), "", txtOffEmail.Text.Trim(), txtOffPhone.Text.Trim(), txtOffPabx.Text.Trim(), txtOffIpPhone.Text.Trim(),
                        txtEmpRemarks.Text.Trim(), cboWrkStation.SelectedValue.ToString(), txtTA.Text + "+" + txtDA.Text + "+" + txtOthAllowence.Text, "", hfEmpRef.Value.ToString());

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Employee Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Employee ID No: " + txtEmpId.Text.Trim();
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    taEmpOffcInfo.InsertEmpOff(hfEmpRef.Value.ToString(), txtEmpId.Text.Trim(), "100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), Convert.ToDateTime(txtDOJ.Text.Trim()), supRef.ToString(),
                        cboJobStat.SelectedValue.ToString(), Convert.ToDateTime(txtConfDueDate.Text.Trim()), confDt, cboEmpType.SelectedValue.ToString(),
                        txtIdCardNo.Text.Trim(), cboShift.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                        txtSalBankAcc.Text.Trim(), "", txtOffEmail.Text.Trim(), txtOffPhone.Text.Trim(), txtOffPabx.Text.Trim(), txtOffIpPhone.Text.Trim(),
                        txtEmpRemarks.Text.Trim(), cboWrkStation.SelectedValue.ToString(), txtTA.Text + "+" + txtDA.Text + "+" + txtOthAllowence.Text, "");

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Employee Data Added Successfully.";
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
            var taEmpImg = new tblHrmsEmpImgTableAdapter();

            if (picUpload.HasFile)
            {
                try
                {
                    System.Drawing.Image imag = System.Drawing.Image.FromStream(picUpload.PostedFile.InputStream);

                    var dtEmpImg = taEmpImg.GetDataByEmpRef(hfEmpRef.Value.ToString());

                    if (dtEmpImg.Rows.Count > 0)
                    {
                        taEmpImg.UpdateEmpPic(ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                    }
                    else
                    {
                        var dtMaxImgRef = taEmpImg.GetMaxImgRef();
                        Int32 nextImgRef = dtMaxImgRef == null ? 100001 : Convert.ToInt32(dtMaxImgRef) + 1;

                        taEmpImg.InsertEmpImg(nextImgRef, hfEmpRef.Value.ToString(), txtEmpId.Text.ToString(), ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg),
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
            var taEmpImg = new tblHrmsEmpImgTableAdapter();

            if (sigUpload.HasFile)
            {
                try
                {
                    System.Drawing.Image imag = System.Drawing.Image.FromStream(sigUpload.PostedFile.InputStream);

                    var dtEmpImg = taEmpImg.GetDataByEmpRef(hfEmpRef.Value.ToString());

                    if (dtEmpImg.Rows.Count > 0)
                    {
                        taEmpImg.UpdateEmpSig(ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                    }
                    else
                    {
                        var dtMaxImgRef = taEmpImg.GetMaxImgRef();
                        Int32 nextImgRef = dtMaxImgRef == null ? 100001 : Convert.ToInt32(dtMaxImgRef) + 1;

                        taEmpImg.InsertEmpImg(nextImgRef, hfEmpRef.Value.ToString(), txtEmpId.Text.ToString(), null,
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
            var taEmpImg = new tblHrmsEmpImgTableAdapter();

            if (nidUpload.HasFile)
            {
                try
                {
                    System.Drawing.Image imag = System.Drawing.Image.FromStream(nidUpload.PostedFile.InputStream);

                    var dtEmpImg = taEmpImg.GetDataByEmpRef(hfEmpRef.Value.ToString());

                    if (dtEmpImg.Rows.Count > 0)
                    {
                        taEmpImg.UpdateEmpNid(ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                    }
                    else
                    {
                        var dtMaxImgRef = taEmpImg.GetMaxImgRef();
                        Int32 nextImgRef = dtMaxImgRef == null ? 100001 : Convert.ToInt32(dtMaxImgRef) + 1;

                        taEmpImg.InsertEmpImg(nextImgRef, hfEmpRef.Value.ToString(), txtEmpId.Text.ToString(), null,
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

            var taEmpGenInfo = new tblHrmsEmpTableAdapter();
            var taEmpAdrInfo = new tblHrmsEmpAdrTableAdapter();
            var taEmpIdenti = new tblHrmsEmpExtTableAdapter();
            var taEmpExp = new tblHrmsEmpExpTableAdapter();
            var taEmpQual = new tblHrmsEmpQualTableAdapter();
            var taEmpOffcInfo = new tblHrmsEmpOfficeTableAdapter();
            var taEmpImg = new tblHrmsEmpImgTableAdapter();
            var taEmpServLog = new tblHrmsEmpServLogTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpGenInfo.Connection);

            try
            {
                #region Validate Supervisor
                var supRef = "";
                var srchWords = txtSupr.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    supRef = word;
                    break;
                }

                if (supRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(supRef, out result))
                    {
                        var taViewEmp = new View_Emp_BascTableAdapter();
                        var dtViewEmp = taViewEmp.GetDataByEmpRef(Convert.ToInt32(supRef.ToString()));
                        if (dtViewEmp.Rows.Count > 0)
                        {
                            supRef = dtViewEmp[0].EmpRefNo.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Reporting person.";
                            tblMsg.Rows[1].Cells[0].InnerText = "Enter valid reporting person.";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Reporting person.";
                        tblMsg.Rows[1].Cells[0].InnerText = "Enter valid reporting person.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Reporting person.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Enter valid reporting person.";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                taEmpGenInfo.AttachTransaction(myTran);
                taEmpAdrInfo.AttachTransaction(myTran);
                taEmpIdenti.AttachTransaction(myTran);
                taEmpExp.AttachTransaction(myTran);
                taEmpQual.AttachTransaction(myTran);
                taEmpOffcInfo.AttachTransaction(myTran);
                taEmpImg.AttachTransaction(myTran);
                taEmpServLog.AttachTransaction(myTran);

                #region Save General Info
                var dtMaxEmpRef = taEmpGenInfo.GetMaxEmpRefNo();
                var nextEmpRefNo = dtMaxEmpRef == null ? "000001" : (Convert.ToInt32(dtMaxEmpRef) + 1).ToString("000000");
                var nextEmpId = "EMP-" + Convert.ToInt32(nextEmpRefNo).ToString("000000");
                 
                var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(hfEmpRef.Value.ToString());
                 
                if (dtEmpGenInfo.Rows.Count > 0)                 
                {
                     taEmpGenInfo.UpdateEmp(txtEmpFirstName.Text.Trim(), txtEmpLastName.Text.Trim(), txtEmpFathersName.Text.Trim(),
                         txtEmpMothersName.Text.Trim(), cboEmpGender.SelectedValue.ToString(), cboEmpMaritalStat.SelectedValue.ToString(), txtEmpSpouseName.Text.Trim(),
                         Convert.ToDateTime(txtEmpDOB.Text.Trim()), txtEmpPOB.Text.Trim(), DateTime.Now,
                         Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "", hfEmpRef.Value.ToString());                 
                }
                else
                {
                     taEmpGenInfo.InsertEmp(nextEmpRefNo, nextEmpId, txtEmpFirstName.Text.Trim(), txtEmpLastName.Text.Trim(), txtEmpFathersName.Text.Trim(),
                         txtEmpMothersName.Text.Trim(), cboEmpGender.SelectedValue.ToString(), cboEmpMaritalStat.SelectedValue.ToString(), txtEmpSpouseName.Text.Trim(),
                         Convert.ToDateTime(txtEmpDOB.Text.Trim()), txtEmpPOB.Text.Trim(), "Y", "N", DateTime.Now,
                         Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "");
                }
                #endregion

                #region Save Address Info
                var dtEmpAdrInfo = taEmpAdrInfo.GetDataByEmpRef(hfEmpRef.Value.ToString());
                if (dtEmpAdrInfo.Rows.Count > 0)
                {
                    taEmpAdrInfo.UpdateEmpAdr(txtEmpPresAdrHouse.Text.Trim(), txtEmpPresAdrPO.Text.Trim(), txtEmpPresAdrPOCode.Text.Trim(),
                            txtEmpPresAdrThana.Text.Trim(), txtEmpPresAdrDist.Text.Trim(), txtEmpPrmAdrHouse.Text.Trim(), txtEmpPrmAdrPO.Text.Trim(), txtEmpPrmAdrPOCode.Text.Trim(),
                            txtEmpPrmAdrThana.Text.Trim(), txtEmpPrmAdrDist.Text.Trim(), "", "", "", "", "", txtEmpEmerAdrName.Text.Trim(), txtEmpEmerAdrRelation.Text.Trim(),
                            txtEmpEmerAdrHouse.Text.Trim(), txtEmpEmerAdrPO.Text.Trim(), txtEmpEmerAdrPOCode.Text.Trim(), txtEmpEmerAdrThana.Text.Trim(),
                            txtEmpEmerAdrDist.Text.Trim(), txtEmpEmerAdrPhone.Text.Trim(), txtEmpEmerAdrCell.Text.Trim(), "1", "", hfEmpRef.Value.ToString());
                }
                else
                {
                    taEmpAdrInfo.InsertEmpAdr(nextEmpRefNo.ToString(), nextEmpId, txtEmpPresAdrHouse.Text.Trim(), txtEmpPresAdrPO.Text.Trim(), txtEmpPresAdrPOCode.Text.Trim(),
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

                var dtEmpIdenti = taEmpIdenti.GetDataByEmpRef(hfEmpRef.Value.ToString());
                if (dtEmpIdenti.Rows.Count > 0)
                {
                    taEmpIdenti.UpdateEmpExt(cboReligion.SelectedValue.ToString(), txtNationality.Text.Trim(), cboBloodGrp.SelectedValue.ToString(), txtHomePhone.Text.Trim(),
                        txtPersonalCell.Text.Trim(), txtPersonalEmail.Text.Trim(), txtIdentiMark.Text.Trim(), txtHeight.Text.Trim(), txtWeight.Text.Trim(), "", txtNID.Text.Trim(),
                        txtTIN.Text.Trim(), txtDLNo.Text.Trim(), txtDLIssuPlace.Text.Trim(), ppDlExpDt, txtPPNo.Text.Trim(),
                        txtPPIsuPlace.Text.Trim(), ppIssDt, ppExpDt, "", "", "", hfEmpRef.Value);
                }
                else
                {
                    taEmpIdenti.InsertEmpExt(nextEmpRefNo.ToString(), nextEmpId, cboReligion.SelectedValue.ToString(), txtNationality.Text.Trim(),
                        cboBloodGrp.SelectedValue.ToString(), txtHomePhone.Text.Trim(), txtPersonalCell.Text.Trim(), txtPersonalEmail.Text.Trim(), txtIdentiMark.Text.Trim(),
                        txtHeight.Text.Trim(), txtWeight.Text.Trim(), "", txtNID.Text.Trim(), txtTIN.Text.Trim(), txtDLNo.Text.Trim(), txtDLIssuPlace.Text.Trim(),
                        ppDlExpDt, txtPPNo.Text.Trim(), txtPPIsuPlace.Text.Trim(), ppIssDt, ppExpDt, "", "", "");
                }
                #endregion

                #region Save Qualification Details

                taEmpQual.DeleteEmpQual(hfEmpRef.Value.ToString());

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

                    taEmpQual.InsertEmpQual(qualTypeRef, examRef, examName, nextEmpRefNo.ToString(), instName, grpSub, certAuth, passYr, result, stDate, endDate, duration,
                        qualLno, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), extData1, extData2, extData3, "1", "");

                    qualLno++;
                }
                #endregion             

                #region Save Experience Details

                taEmpExp.DeleteEmpExp(hfEmpRef.Value.ToString());

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

                    taEmpExp.InsertEmpExp(nextEmpRefNo.ToString(), expOrgName, expDept, expDesig, 0, expOrgAdr, expRem, Convert.ToDateTime(expFrom), Convert.ToDateTime(expTo),
                        expRefName, expRefCont, expRefEmail, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                        "", "", "", "1", "");

                    expLno++;
                }
                #endregion

                #region Save OfficeInfo
                DateTime? confDt = null;
                if (txtConfDate.Text.Trim().Length > 0) confDt = Convert.ToDateTime(txtConfDate.Text.Trim());

                var dtEmpOffcInfo = taEmpOffcInfo.GetDataByEmpRef(hfEmpRef.Value.ToString());
                if (dtEmpOffcInfo.Rows.Count > 0)
                {
                    taEmpOffcInfo.UpdateEmpOff("100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), Convert.ToDateTime(txtDOJ.Text.Trim()), supRef.ToString(),
                        cboJobStat.SelectedValue.ToString(), Convert.ToDateTime(txtConfDueDate.Text.Trim()), confDt, cboEmpType.SelectedValue.ToString(),
                        txtIdCardNo.Text.Trim(), cboShift.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                        txtSalBankAcc.Text.Trim(), "", txtOffEmail.Text.Trim(), txtOffPhone.Text.Trim(), txtOffPabx.Text.Trim(), txtOffIpPhone.Text.Trim(),
                        txtEmpRemarks.Text.Trim(), cboWrkStation.SelectedValue.ToString(), txtTA.Text + "+" + txtDA.Text + "+" + txtOthAllowence.Text, "", hfEmpRef.Value.ToString());
                }
                else
                {
                    taEmpOffcInfo.InsertEmpOff(nextEmpRefNo.ToString(), nextEmpId, "100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), Convert.ToDateTime(txtDOJ.Text.Trim()), supRef.ToString(),
                        cboJobStat.SelectedValue.ToString(), Convert.ToDateTime(txtConfDueDate.Text.Trim()), confDt, cboEmpType.SelectedValue.ToString(),
                        txtIdCardNo.Text.Trim(), cboShift.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                        txtSalBankAcc.Text.Trim(), "", txtOffEmail.Text.Trim(), txtOffPhone.Text.Trim(), txtOffPabx.Text.Trim(), txtOffIpPhone.Text.Trim(),
                        txtEmpRemarks.Text.Trim(), cboWrkStation.SelectedValue.ToString(), txtTA.Text + "+" + txtDA.Text + "+" + txtOthAllowence.Text, "");
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

                var dtEmpImg = taEmpImg.GetDataByEmpRef(hfEmpRef.Value.ToString());
                if (dtEmpImg.Rows.Count > 0)
                {
                    if (picUpload.HasFile)
                    {
                        if (sigUpload.HasFile)
                        {
                            if (nidUpload.HasFile)
                            {
                                //update all
                                taEmpImg.UpdateEmpImg(ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    ConvertImageToByteArray(SigImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    ConvertImageToByteArray(NidImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    "", "", "", "1", "", hfEmpRef.Value.ToString());
                            }
                            else
                            {
                                //update pic_sig
                                taEmpImg.UpdateEmpPicSig(ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    ConvertImageToByteArray(SigImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                            }
                        }
                        else
                        {
                            if (nidUpload.HasFile)
                            {
                                //update pic_nid
                                taEmpImg.UpdateEmpPicNid(ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    ConvertImageToByteArray(NidImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                            }
                            else
                            {
                                //update pic
                                taEmpImg.UpdateEmpPic(ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
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
                                taEmpImg.UpdateEmpSigNid(ConvertImageToByteArray(SigImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                                    ConvertImageToByteArray(NidImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                            }
                            else
                            {
                                //update sig
                                taEmpImg.UpdateEmpSig(ConvertImageToByteArray(SigImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
                            }
                        }
                        else
                        {
                            if (nidUpload.HasFile)
                            {
                                //update nid
                                taEmpImg.UpdateEmpNid(ConvertImageToByteArray(NidImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfEmpRef.Value.ToString());
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
                    var dtMaxImgRef = taEmpImg.GetMaxImgRef();
                    Int32 nextImgRef = dtMaxImgRef == null ? 100001 : Convert.ToInt32(dtMaxImgRef) + 1;

                    taEmpImg.InsertEmpImg(nextImgRef, nextEmpRefNo.ToString(), nextEmpId.ToString(),
                        PicImg == null ? null : ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                        SigImg == null ? null : ConvertImageToByteArray(SigImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                        NidImg == null ? null : ConvertImageToByteArray(NidImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                        "", "", "", "1", "");
                }
                #endregion

                #region Save Service Log
                var dtMaxSerLogPos = taEmpServLog.GetMaxServPos(nextEmpRefNo.ToString());
                var maxtSerLogPos = dtMaxSerLogPos == null ? 1 : Convert.ToInt32(dtMaxSerLogPos);

                if (dtEmpGenInfo.Rows.Count > 0)
                {
                    taEmpServLog.UpdateEmpServLog("100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), supRef.ToString(), cboEmpType.SelectedValue.ToString(),
                        cboJobStat.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), cboWrkStation.SelectedValue.ToString(), "", "", "1", "", nextEmpRefNo.ToString(), maxtSerLogPos);
                }
                else
                {
                    taEmpServLog.InsertEmpServLog((maxtSerLogPos + 1), "Join", DateTime.Now, nextEmpRefNo.ToString(), "100001", cboLoc.SelectedValue.ToString(),
                        cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), supRef.ToString(),
                        cboEmpType.SelectedValue.ToString(), cboJobStat.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                        DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), cboWrkStation.SelectedValue.ToString(), "", "", "1", "");
                }
                #endregion

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
                    tblMsg.Rows[0].Cells[0].InnerText = "Employee Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Employee ID No: " + txtEmpId.Text.Trim();
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    EmpRef = nextEmpRefNo.ToString();
                    txtEmpId.Text = nextEmpId.ToString();
                    tblMsg.Rows[0].Cells[0].InnerText = "Employee Data Added Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Employee ID No: " + nextEmpId.ToString();
                    ModalPopupExtenderMsg.Show();
                }

                hlEmpPicPreview.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + EmpRef + "'";
                imgEmpPicPreview.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + EmpRef + "'";

                hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + EmpRef + "'";
                imgEmpPic.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + EmpRef + "'";

                hlEmpSig.NavigateUrl = "~/Module/HRMS/Tools/getEmpSig.ashx?EmpRefNo='" + EmpRef + "'";
                imgEmpSig.ImageUrl = "~/Module/HRMS/Tools/getEmpSig.ashx?EmpRefNo='" + EmpRef + "'";

                hlEmpNid.NavigateUrl = "~/Module/HRMS/Tools/getEmpNid.ashx?EmpRefNo='" + EmpRef + "'";
                imgEmpNId.ImageUrl = "~/Module/HRMS/Tools/getEmpNid.ashx?EmpRefNo='" + EmpRef + "'";

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
            var taEmpImg = new tblHrmsEmpImgTableAdapter();

            try
            {
                var dtEmpImg = taEmpImg.GetDataByEmpRef(hfEmpRef.Value.ToString());

                if (dtEmpImg.Rows.Count > 0)
                {
                    taEmpImg.UpdateEmpPic(null, hfEmpRef.Value.ToString());
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
            var taEmpImg = new tblHrmsEmpImgTableAdapter();

            try
            {
                var dtEmpImg = taEmpImg.GetDataByEmpRef(hfEmpRef.Value.ToString());

                if (dtEmpImg.Rows.Count > 0)
                {
                    taEmpImg.UpdateEmpSig(null, hfEmpRef.Value.ToString());
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
            var taEmpImg = new tblHrmsEmpImgTableAdapter();

            try
            {
                var dtEmpImg = taEmpImg.GetDataByEmpRef(hfEmpRef.Value.ToString());

                if (dtEmpImg.Rows.Count > 0)
                {
                    taEmpImg.UpdateEmpNid(null, hfEmpRef.Value.ToString());
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
            //ddlSettleType.SelectedIndex = 0;
            txtSettleDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFinalSettlAmt.Text = "";
            ModalPopupExtenderSettle.Show();
        }

        protected void empSettlement()
        {
            var taEmpMas = new tblHrmsEmpTableAdapter();
            var taEmpServLog = new tblHrmsEmpServLogTableAdapter();
            
            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpMas.Connection);

            try
            {
                #region Validate Supervisor
                var supRef = "";
                var srchWords = txtSupr.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    supRef = word;
                    break;
                }

                if (supRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(supRef, out result))
                    {
                        var taViewEmp = new View_Emp_BascTableAdapter();
                        var dtViewEmp = taViewEmp.GetDataByEmpRef(Convert.ToInt32(supRef.ToString()));
                        if (dtViewEmp.Rows.Count > 0)
                        {
                            supRef = dtViewEmp[0].EmpRefNo.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Reporting person.";
                            tblMsg.Rows[1].Cells[0].InnerText = "Enter valid reporting person.";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Reporting person.";
                        tblMsg.Rows[1].Cells[0].InnerText = "Enter valid reporting person.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Reporting person.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Enter valid reporting person.";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                taEmpMas.AttachTransaction(myTran);
                taEmpServLog.AttachTransaction(myTran);

                taEmpMas.UpdateSettlement("Y", Convert.ToDateTime(txtSettleDate.Text.Trim()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "0", "", hfEmpRef.Value.ToString());

                #region Update Service Log
                var dtMaxSerLogPos = taEmpServLog.GetMaxServPos(hfEmpRef.Value.ToString());
                var maxtSerLogPos = dtMaxSerLogPos == null ? 1 : Convert.ToInt32(dtMaxSerLogPos) + 1;
                //if (taEmpMas.Rows.Count > 0)
                //{
                //    taEmpServLog.UpdateEmpServLog("100001", cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                //        cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), cboSupr.SelectedValue.ToString(), cboEmpType.SelectedValue.ToString(),
                //        cboJobStat.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()), DateTime.Now,
                //        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "", nextEmpRefNo.ToString(), maxtSerLogPos);
                //}
                //else
                //{
                taEmpServLog.InsertEmpServLog(maxtSerLogPos, "Separation", Convert.ToDateTime(txtSettleDate.Text.Trim()), hfEmpRef.Value.ToString(), "100001", cboLoc.SelectedValue.ToString(),
                        cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString(), cboDesig.SelectedValue.ToString(), supRef.ToString(),
                        cboEmpType.SelectedValue.ToString(), cboJobStat.SelectedValue.ToString(), cboGrade.SelectedValue.ToString(), Convert.ToDecimal(txtSalary.Text.Trim()),
                        DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlSettleType.SelectedItem.Text, txtFinalSettlAmt.Text, ddlSettleType.SelectedItem.Text, "1", "");
                //}
                #endregion                
                
                myTran.Commit();

                btnSettle.Enabled = false;

                tblMsg.Rows[0].Cells[0].InnerText = "Employee separation has been done.";
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
                tblMsg.Rows[0].Cells[0].InnerText = "Enter Date First.";
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

        protected void btnViewProfile_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowHrmsReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
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

                rptFile = "~/Module/HRMS/Reports/rptEmpProfile.rpt";

                rptSelcFormula = "{View_Emp_Basc_Report.EmpId}='" + empRef.ToString() + "'";
                //Session["RptDateFrom"] = txtFromDate.Text.Trim();
                //Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}