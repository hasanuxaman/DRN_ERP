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
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmEmpAttnd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            Label1.BackColor = System.Drawing.Color.White;
            Label1.ForeColor = System.Drawing.Color.White;
            Label2.Text = "Not Saved";
            Label3.BackColor = System.Drawing.Color.LightGreen;
            Label3.ForeColor = System.Drawing.Color.LightGreen;
            Label4.Text = "Saved";
            Label5.BackColor = System.Drawing.Color.Orange;
            Label5.ForeColor = System.Drawing.Color.Orange;
            Label6.Text = "Leave";
            Label7.BackColor = System.Drawing.Color.Red;
            Label7.ForeColor = System.Drawing.Color.Red;
            Label8.Text = "Error";
            //Label9.BackColor = System.Drawing.Color.Red;
            //Label9.ForeColor = System.Drawing.Color.Red;
            //Label10.Text = "";

            txtAttndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taLoc = new tblHrmsOffLocTableAdapter();
            cboLoc.DataSource = taLoc.GetDataByAsc();
            cboLoc.DataValueField = "LocRefNo";
            cboLoc.DataTextField = "LocName";
            cboLoc.DataBind();
            cboLoc.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        protected void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoc.SelectedIndex != 0)
            {
                cboDept.Items.Clear();
                cboSec.Items.Clear();
                cboShift.Items.Clear();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taDep = new tblHrmsDeptTableAdapter();
                cboDept.DataSource = taDep.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                cboDept.DataValueField = "DeptRefNo";
                cboDept.DataTextField = "DeptName";
                cboDept.DataBind();
                cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            else
            {
                cboDept.Items.Clear();
                cboSec.Items.Clear();
                cboShift.Items.Clear();
                cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
        }

        protected void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDept.SelectedIndex != 0)
            {
                cboSec.Items.Clear();
                cboShift.Items.Clear();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taSec = new tblHrmsSecTableAdapter();
                cboSec.DataSource = taSec.GetDataByDeptRef(cboDept.SelectedValue.ToString());
                cboSec.DataValueField = "SecRefNo";
                cboSec.DataTextField = "SecName";
                cboSec.DataBind();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            else
            {
                cboSec.Items.Clear();
                cboShift.Items.Clear();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
        }

        protected void cboSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSec.SelectedIndex != 0)
            {
                cboShift.Items.Clear();
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taShift = new tblHrmsShiftTableAdapter();
                cboShift.DataSource = taShift.GetDataBySecRef(cboSec.SelectedValue.ToString());
                cboShift.DataValueField = "ShiftRefNo";
                cboShift.DataTextField = "ShiftName";
                cboShift.DataBind();
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            else
            {
                cboShift.Items.Clear();
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
        }

        #region GridData
        public string GetEmpId(int empRef)
        {
            string empId = "";
            try
            {
                var taEmp = new tblHrmsEmpTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                if (dtEmp.Rows.Count > 0)
                    empId = dtEmp[0].EmpId.ToString();
                return empId;
            }
            catch (Exception) { return empId; }
        }

        public string GetEmpName(int empRef)
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

        public string GetLocName(int empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new tblHrmsEmpOfficeTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                if (dtEmp.Rows.Count > 0)
                {
                    var taLoc=new tblHrmsOffLocTableAdapter();
                    var dtLoc=taLoc.GetDataByLocRef(Convert.ToInt32(dtEmp[0].OffLocRefNo));
                    if (dtLoc.Rows.Count > 0)
                        empStr = dtLoc[0].LocName.ToString();
                }
                return empStr;
            }
            catch (Exception ex) { return empStr; }
        }

        public string GetDeptName(int empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new tblHrmsEmpOfficeTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                if (dtEmp.Rows.Count > 0)
                {
                    var taDept = new tblHrmsDeptTableAdapter();
                    var dtDept = taDept.GetDataByDeptRef(Convert.ToInt32(dtEmp[0].DeptRefNo));
                    if (dtDept.Rows.Count > 0)
                        empStr = dtDept[0].DeptName.ToString();
                }
                return empStr;
            }
            catch (Exception ex) { return empStr; }
        }

        public string GetSecName(int empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new tblHrmsEmpOfficeTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                if (dtEmp.Rows.Count > 0)
                {
                    var taSec = new tblHrmsSecTableAdapter();
                    var dtSec = taSec.GetDataBySecRef(Convert.ToInt32(dtEmp[0].SecRefNo));
                    if (dtSec.Rows.Count > 0)
                        empStr = dtSec[0].SecName.ToString();
                }
                return empStr;
            }
            catch (Exception ex) { return empStr; }
        }

        public string GetShiftRef(int empRef)
        {
            string empStr = "0";
            try
            {
                var taEmp = new tblHrmsEmpOfficeTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                if (dtEmp.Rows.Count > 0)
                {
                    empStr = dtEmp[0].ShiftRefNo.ToString();
                }
                return empStr;
            }
            catch (Exception ex) { return empStr; }
        }

        public string GetShiftName(int empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new tblHrmsEmpOfficeTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                if (dtEmp.Rows.Count > 0)
                {
                    var taShift = new tblHrmsShiftTableAdapter();
                    var dtShift = taShift.GetDataByShiftRefNo(Convert.ToInt32(dtEmp[0].ShiftRefNo));
                    if (dtShift.Rows.Count > 0)
                        empStr = dtShift[0].ShiftName.ToString();
                }
                return empStr;
            }
            catch (Exception ex) { return empStr; }
        }
        #endregion

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                var taEmpMas = new View_Emp_BascTableAdapter();
                var dtEmpMas = new dsEmpDet.View_Emp_BascDataTable();

                var taAtnd = new tblHrmsEmpDayAttndTableAdapter();
                var taEmp = new tblHrmsEmpOfficeTableAdapter();

                #region Load Emp List
                if (cboLoc.SelectedIndex == 0)
                    //---ALL Location
                    dtEmpMas = taEmpMas.GetData();
                else
                    if (cboDept.SelectedIndex == 0)
                        //---Selected Location
                        dtEmpMas = taEmpMas.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                    else
                        if (cboSec.SelectedIndex == 0)
                            //---Selected Department
                            dtEmpMas = taEmpMas.GetDataByLocDept(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString());
                        else
                            if (cboShift.SelectedIndex == 0)
                                //---Selected Section
                                dtEmpMas = taEmpMas.GetDataByLocDeptSec(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString());
                            else
                                //---Selected Shift
                                dtEmpMas = taEmpMas.GetDataByLocDeptSecShift(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                                    cboSec.SelectedValue.ToString(), cboShift.SelectedValue.ToString());
                gvDayAttnd.DataSource = dtEmpMas;
                gvDayAttnd.DataBind();
                #endregion                

                foreach (GridViewRow gr in gvDayAttnd.Rows)
                {
                    var hfEmpRef = (HiddenField)gr.FindControl("hfEmpRef");
                    var txtIn = ((TextBox)gr.FindControl("txtInTime"));
                    var txtOut = ((TextBox)gr.FindControl("txtOutTime"));
                    var txtTot = (TextBox)gr.FindControl("txtTotHr");
                    var lblRem = (Label)(gr.Cells[2].FindControl("lblRem"));

                    var dtAtnd = taAtnd.GetDataByEmp(hfEmpRef.Value.ToString(), Convert.ToDateTime(txtAttndDate.Text.Trim()));
                    if (dtAtnd.Rows.Count > 0)
                    {
                        var inTime = dtAtnd[0].AttndInTime.ToString();
                        //DateTime iTime = DateTime.ParseExact(inTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime iTime = Convert.ToDateTime(inTime);
                        //if you really need a TimeSpan this will get the time elapsed since midnight:
                        TimeSpan ti = iTime.TimeOfDay;
                        txtIn.Text = ti.ToString();

                        var outTime = dtAtnd[0].AttndOutTime.ToString();
                        //DateTime oTime = DateTime.ParseExact(outTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime oTime = Convert.ToDateTime(outTime);
                        //if you really need a TimeSpan this will get the time elapsed since midnight:
                        TimeSpan to = oTime.TimeOfDay;
                        txtOut.Text = to.ToString();

                        txtTot.Text = dtAtnd[0].AttndTotHr.ToString();

                        if (dtAtnd[0].AttndFlag.ToString() == "L")
                        {
                            lblRem.Text = "Leave taken for the date " + txtAttndDate.Text.Trim();
                            gr.BackColor = System.Drawing.Color.Orange;
                        }
                        else
                        {
                            gr.BackColor = System.Drawing.Color.LightGreen;
                        }
                    }
                    else
                    {
                        #region Load Default Data
                        var dtEmp = taEmp.GetDataByEmpRef(hfEmpRef.Value.ToString());
                        if (dtEmp.Rows.Count > 0)
                        {
                            var taShift = new tblHrmsShiftTableAdapter();
                            var dtShift = taShift.GetDataByShiftRefNo(Convert.ToInt32(dtEmp[0].ShiftRefNo));
                            if (dtShift.Rows.Count > 0)
                            {
                                var stTime = dtShift[0].ShiftStart.ToString();
                                DateTime sTime = DateTime.ParseExact(stTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                                //if you really need a TimeSpan this will get the time elapsed since midnight:
                                TimeSpan ts = sTime.TimeOfDay;
                                txtIn.Text = ts.ToString();

                                var endTime = dtShift[0].ShiftEnd.ToString();
                                DateTime eTime = DateTime.ParseExact(endTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                                //if you really need a TimeSpan this will get the time elapsed since midnight:
                                TimeSpan te = eTime.TimeOfDay;
                                txtOut.Text = te.ToString();

                                txtTot.Text = dtShift[0].ShiftTotal.ToString();
                            }
                            else
                            {
                                gr.BackColor = System.Drawing.Color.Red;
                            }
                        }
                        #endregion
                    }
                }

                if (gvDayAttnd.Rows.Count > 0)
                {
                    cboLoc.Enabled = false;
                    cboDept.Enabled = false;
                    cboSec.Enabled = false;
                    cboShift.Enabled = false;
                    txtAttndDate.Enabled = false;
                    btnSaveAll.Visible = gvDayAttnd.Rows.Count > 0;
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvDayAttnd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hfEmpRef = (HiddenField)e.Row.FindControl("hfEmpRef");
                var txtIn = ((TextBox)e.Row.FindControl("txtInTime"));
                var txtOut = ((TextBox)e.Row.FindControl("txtOutTime"));
                var txtTot = (TextBox)e.Row.FindControl("txtTotHr");

                var btnLv = (Button)e.Row.FindControl("btnLeave");
                var imgSave = (Image)e.Row.FindControl("imgBtnSave");

                var taAttnd = new tblHrmsEmpDayAttndTableAdapter();
                var dtAttnd = taAttnd.GetDataByEmp(hfEmpRef.Value.ToString(), Convert.ToDateTime(txtAttndDate.Text.Trim()));
                if (dtAttnd.Rows.Count > 0)
                {
                    if (dtAttnd[0].AttndFlag == "L")
                    {
                        btnLv.Visible = false;
                        imgSave.Visible = false;
                        e.Row.BackColor = System.Drawing.Color.Orange;
                    }
                }

                txtIn.Attributes.Add("onkeyup", "TotAttndHr('" + txtIn.ClientID + "', '" + txtOut.ClientID + "','" + txtTot.ClientID + "' )");
                txtOut.Attributes.Add("onkeyup", "TotAttndHr('" + txtIn.ClientID + "', '" + txtOut.ClientID + "','" + txtTot.ClientID + "' )");

                txtIn.Attributes.Add("onchange", "TotAttndHr('" + txtIn.ClientID + "', '" + txtOut.ClientID + "','" + txtTot.ClientID + "' )");
                txtOut.Attributes.Add("onchange", "TotAttndHr('" + txtIn.ClientID + "', '" + txtOut.ClientID + "','" + txtTot.ClientID + "' )");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            cboLoc.SelectedIndex = 0;
            cboDept.Items.Clear();
            cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
            cboSec.Items.Clear();
            cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
            cboShift.Items.Clear();
            cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));
            cboLoc.Enabled = true;
            cboDept.Enabled = true;
            cboSec.Enabled = true;
            cboShift.Enabled = true;

            txtAttndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");            
            txtAttndDate.Enabled = true;

            //var taEmpMas = new View_Emp_BascTableAdapter();
            //gvDayAttnd.DataSource = taEmpMas.GetDataByEmpRef(0);
            gvDayAttnd.DataSource = null;
            gvDayAttnd.DataBind();

            btnSaveAll.Visible = gvDayAttnd.Rows.Count > 0;
        }

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taAttnd = new tblHrmsEmpDayAttndTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAttnd.Connection);

            try
            {
                taAttnd.AttachTransaction(myTran);

                foreach (GridViewRow gr in gvDayAttnd.Rows)
                {
                    var lblRem = (Label)(gr.Cells[2].FindControl("lblRem"));

                    var empRef = ((HiddenField)(gr.Cells[2].FindControl("hfEmpRef"))).Value;
                    var shiftRef = ((HiddenField)(gr.Cells[2].FindControl("hfShiftRef"))).Value;
                    var inTime = Convert.ToDateTime(((TextBox)(gr.Cells[2].FindControl("txtInTime"))).Text).ToString("hh:mm tt");
                    var shiftIn = DateTime.Now.ToString("hh:mm tt");
                    double shiftInGrace = 0;
                    double shiftLate = 0;
                    var outTime = Convert.ToDateTime(((TextBox)(gr.Cells[2].FindControl("txtOutTime"))).Text).ToString("hh:mm tt");
                    var shiftOut = DateTime.Now.ToString("hh:mm tt");
                    double shiftOutGrace = 0;
                    double shiftEarly = 0;
                    var totHr = ((TextBox)(gr.Cells[2].FindControl("txtTotHr"))).Text;
                    double otMin = 0;                    
                    var lnkY = (LinkButton)(gr.Cells[2].FindControl("lnkYes"));
                    var lnkN = (LinkButton)(gr.Cells[2].FindControl("lnkNo"));

                    var taShift = new tblHrmsShiftTableAdapter();
                    var dtShift = taShift.GetDataByShiftRefNo(Convert.ToInt32(shiftRef.ToString()));
                    if (dtShift.Rows.Count > 0)
                    {
                        shiftIn = Convert.ToDateTime(dtShift[0].ShiftStart.ToString()).ToString("hh:mm tt");
                        shiftInGrace = Convert.ToDouble(dtShift[0].ShiftStartAdd.ToString());

                        shiftOut = Convert.ToDateTime(dtShift[0].ShiftEnd.ToString()).ToString("hh:mm tt");
                        shiftOutGrace = Convert.ToDouble(dtShift[0].ShiftEndAdd.ToString());
                    }

                    DateTime atnInTime = DateTime.ParseExact(inTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime sftInTime = DateTime.ParseExact(shiftIn, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    TimeSpan inDiff = atnInTime - sftInTime;
                    //shiftLate = (inDiff.Hours * 60) + inDiff.Minutes;
                    shiftLate = ((inDiff.TotalMinutes) >= shiftInGrace) ? (inDiff.TotalMinutes - shiftInGrace) : 0;

                    DateTime atnOutTime = DateTime.ParseExact(outTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime sftOutTime = DateTime.ParseExact(shiftOut, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    TimeSpan outDiff = sftOutTime - atnOutTime;
                    //shiftEarly = (outDiff.Hours * 60) + outDiff.Minutes;
                    shiftEarly = ((outDiff.TotalMinutes) >= shiftOutGrace) ? (outDiff.TotalMinutes - shiftOutGrace) : 0;

                    otMin = (((atnOutTime - sftOutTime).TotalMinutes) > 0) ? (atnOutTime - sftOutTime).TotalMinutes : 0;
                    
                    var dtAttnd = taAttnd.GetDataByEmp(empRef, Convert.ToDateTime(txtAttndDate.Text.Trim()));
                    if (dtAttnd.Rows.Count > 0)
                    {
                        if (dtAttnd[0].AttndFlag == "L")
                        {
                            lblRem.Text = "Leave taken for the date " + txtAttndDate.Text.Trim();
                            //lnkY.Visible = true;
                            //lnkN.Visible = true;
                            gr.BackColor = System.Drawing.Color.Orange;
                        }
                        else
                        {
                            taAttnd.UpdateAttnd(shiftRef.ToString(), inTime.ToString(), shiftIn.ToString(), shiftInGrace.ToString(), shiftLate.ToString(),
                                outTime.ToString(), shiftOut.ToString(), shiftOutGrace.ToString(), shiftEarly.ToString(), totHr.ToString(),
                                otMin.ToString(), 1, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                               "", "", "", "1", "P", empRef.ToString(), Convert.ToDateTime(txtAttndDate.Text.Trim()));
                            gr.BackColor = System.Drawing.Color.LightGreen;
                        }
                    }
                    else
                    {
                        taAttnd.InsertAttnd(empRef.ToString(), Convert.ToDateTime(txtAttndDate.Text.Trim()), shiftRef.ToString(), inTime.ToString(), shiftIn.ToString(),
                           shiftInGrace.ToString(), shiftLate.ToString(), outTime.ToString(), shiftOut.ToString(), shiftOutGrace.ToString(), shiftEarly.ToString(),
                           totHr.ToString(), otMin.ToString(), 1, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                           "", "", "", "1", "P");
                        gr.BackColor = System.Drawing.Color.LightGreen;
                    }
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
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

        protected void gvDayAttnd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                #region Save Attendance
                if (e.CommandName == "Save")
                {
                    // Convert the row index stored in the CommandArgument
                    // property to an Integer.
                    int index = Convert.ToInt32(e.CommandArgument);

                    // Retrieve the row that contains the button clicked 
                    // by the user from the Rows collection.
                    GridViewRow row = gvDayAttnd.Rows[index];

                    var lblRem = (Label)(row.Cells[2].FindControl("lblRem"));                    

                    var empRef = ((HiddenField)(row.Cells[2].FindControl("hfEmpRef"))).Value;
                    var shiftRef = ((HiddenField)(row.Cells[2].FindControl("hfShiftRef"))).Value;
                    var inTime = Convert.ToDateTime(((TextBox)(row.Cells[2].FindControl("txtInTime"))).Text).ToString("h:mm tt");
                    var shiftIn = DateTime.Now.ToString("h:mm tt");
                    double shiftInGrace = 0;
                    double shiftLate = 0;
                    var outTime = Convert.ToDateTime(((TextBox)(row.Cells[2].FindControl("txtOutTime"))).Text).ToString("h:mm tt");
                    var shiftOut = DateTime.Now.ToString("h:mm tt");
                    double shiftOutGrace = 0;
                    double shiftEarly = 0;
                    var totHr = ((TextBox)(row.Cells[2].FindControl("txtTotHr"))).Text;
                    double otMin = 0;                    
                    var lnkY = (LinkButton)(row.Cells[2].FindControl("lnkYes"));
                    var lnkN = (LinkButton)(row.Cells[2].FindControl("lnkNo"));

                    var taShift = new tblHrmsShiftTableAdapter();
                    var dtShift = taShift.GetDataByShiftRefNo(Convert.ToInt32(shiftRef.ToString()));
                    if (dtShift.Rows.Count > 0)
                    {
                        shiftIn = Convert.ToDateTime(dtShift[0].ShiftStart.ToString()).ToString("h:mm tt");
                        shiftInGrace = Convert.ToDouble(dtShift[0].ShiftStartAdd.ToString());

                        shiftOut =  Convert.ToDateTime(dtShift[0].ShiftEnd.ToString()).ToString("h:mm tt");
                        shiftOutGrace = Convert.ToDouble(dtShift[0].ShiftEndAdd.ToString());
                    }

                    DateTime atnInTime = DateTime.ParseExact(inTime, "h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime sftInTime = DateTime.ParseExact(shiftIn, "h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    TimeSpan inDiff = atnInTime - sftInTime;
                    //shiftLate = (inDiff.Hours * 60) + inDiff.Minutes;
                    shiftLate = ((inDiff.TotalMinutes) >= shiftInGrace) ? (inDiff.TotalMinutes - shiftInGrace) : 0;

                    DateTime atnOutTime = DateTime.ParseExact(outTime, "h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime sftOutTime = DateTime.ParseExact(shiftOut, "h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    TimeSpan outDiff = sftOutTime - atnOutTime;
                    //shiftEarly = (outDiff.Hours * 60) + outDiff.Minutes;
                    shiftEarly = ((outDiff.TotalMinutes) >= shiftOutGrace) ? (outDiff.TotalMinutes - shiftOutGrace) : 0;

                    otMin = (((atnOutTime - sftOutTime).TotalMinutes) > 0) ? (atnOutTime - sftOutTime).TotalMinutes : 0;

                    var taAttnd = new tblHrmsEmpDayAttndTableAdapter();
                    var dtAttnd = taAttnd.GetDataByEmp(empRef, Convert.ToDateTime(txtAttndDate.Text.Trim()));
                    if (dtAttnd.Rows.Count > 0)
                    {
                        if (dtAttnd[0].AttndFlag == "L")
                        {
                            lblRem.Text = "Already leave taken for the date " + txtAttndDate.Text.Trim();
                            //lnkY.Visible = true;
                            //lnkN.Visible = true;
                            row.BackColor = System.Drawing.Color.Orange;
                        }
                        else
                        {
                            taAttnd.UpdateAttnd(shiftRef.ToString(), inTime.ToString(), shiftIn.ToString(), shiftInGrace.ToString(), shiftLate.ToString(),
                                outTime.ToString(), shiftOut.ToString(), shiftOutGrace.ToString(), shiftEarly.ToString(), totHr.ToString(),
                                otMin.ToString(), 1, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                               "", "", "", "1", "P", empRef.ToString(), Convert.ToDateTime(txtAttndDate.Text.Trim()));

                            row.BackColor = System.Drawing.Color.LightGreen;

                            tblMsg.Rows[0].Cells[0].InnerText = "Data updated successfully.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                    else
                    {
                        taAttnd.InsertAttnd(empRef.ToString(), Convert.ToDateTime(txtAttndDate.Text.Trim()), shiftRef.ToString(), inTime.ToString(), shiftIn.ToString(),
                           shiftInGrace.ToString(), shiftLate.ToString(), outTime.ToString(), shiftOut.ToString(), shiftOutGrace.ToString(), shiftEarly.ToString(),
                           totHr.ToString(), otMin.ToString(), 1, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), 
                           "", "", "", "1", "P");

                        row.BackColor = System.Drawing.Color.LightGreen;

                        tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                #endregion

                #region Save Leave
                if (e.CommandName == "Leave")
                {
                    // Convert the row index stored in the CommandArgument
                    // property to an Integer.
                    int index = Convert.ToInt32(e.CommandArgument);

                    // Retrieve the row that contains the button clicked 
                    // by the user from the Rows collection.
                    GridViewRow row = gvDayAttnd.Rows[index];

                    var lblRem = (Label)(row.Cells[2].FindControl("lblRem"));

                    var btnLv = (Button)(row.Cells[2].FindControl("btnLeave"));
                    var imgSave = (Image)(row.Cells[2].FindControl("imgBtnSave"));

                    var empRef = ((HiddenField)(row.Cells[2].FindControl("hfEmpRef"))).Value;
                    var shiftRef = ((HiddenField)(row.Cells[2].FindControl("hfShiftRef"))).Value;
                    var inTime = Convert.ToDateTime(((TextBox)(row.Cells[2].FindControl("txtInTime"))).Text).ToString("hh:mm tt");
                    var shiftIn = DateTime.Now.ToString("hh:mm tt");
                    double shiftInGrace = 0;
                    double shiftLate = 0;
                    var outTime = Convert.ToDateTime(((TextBox)(row.Cells[2].FindControl("txtOutTime"))).Text).ToString("hh:mm tt");
                    var shiftOut = DateTime.Now.ToString("hh:mm tt");
                    double shiftOutGrace = 0;
                    double shiftEarly = 0;
                    var totHr = ((TextBox)(row.Cells[2].FindControl("txtTotHr"))).Text;
                    double otMin = 0;
                    var lnkY = (LinkButton)(row.Cells[2].FindControl("lnkYes"));
                    var lnkN = (LinkButton)(row.Cells[2].FindControl("lnkNo"));

                    var taShift = new tblHrmsShiftTableAdapter();
                    var dtShift = taShift.GetDataByShiftRefNo(Convert.ToInt32(shiftRef.ToString()));
                    if (dtShift.Rows.Count > 0)
                    {
                        shiftIn = Convert.ToDateTime(dtShift[0].ShiftStart.ToString()).ToString("hh:mm tt");
                        shiftInGrace = Convert.ToDouble(dtShift[0].ShiftStartAdd.ToString());

                        shiftOut = Convert.ToDateTime(dtShift[0].ShiftEnd.ToString()).ToString("hh:mm tt");
                        shiftOutGrace = Convert.ToDouble(dtShift[0].ShiftEndAdd.ToString());
                    }

                    DateTime atnInTime = DateTime.ParseExact(inTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime sftInTime = DateTime.ParseExact(shiftIn, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    TimeSpan inDiff = atnInTime - sftInTime;
                    //shiftLate = (inDiff.Hours * 60) + inDiff.Minutes;
                    shiftLate = ((inDiff.TotalMinutes) >= shiftInGrace) ? (inDiff.TotalMinutes - shiftInGrace) : 0;

                    DateTime atnOutTime = DateTime.ParseExact(outTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime sftOutTime = DateTime.ParseExact(shiftOut, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    TimeSpan outDiff = sftOutTime - atnOutTime;
                    //shiftEarly = (outDiff.Hours * 60) + outDiff.Minutes;
                    shiftEarly = ((outDiff.TotalMinutes) >= shiftOutGrace) ? (outDiff.TotalMinutes - shiftOutGrace) : 0;

                    otMin = (((atnOutTime - sftOutTime).TotalMinutes) > 0) ? (atnOutTime - sftOutTime).TotalMinutes : 0;

                    var taAttnd = new tblHrmsEmpDayAttndTableAdapter();
                    var dtAttnd = taAttnd.GetDataByEmp(empRef, Convert.ToDateTime(txtAttndDate.Text.Trim()));
                    if (dtAttnd.Rows.Count > 0)
                    {


                        taAttnd.UpdateAttnd(shiftRef.ToString(), inTime.ToString(), shiftIn.ToString(), shiftInGrace.ToString(), shiftLate.ToString(),
                            outTime.ToString(), shiftOut.ToString(), shiftOutGrace.ToString(), shiftEarly.ToString(), totHr.ToString(),
                            otMin.ToString(), 1, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                           "", "", "", "1", "L", empRef.ToString(), Convert.ToDateTime(txtAttndDate.Text.Trim()));                        

                        tblMsg.Rows[0].Cells[0].InnerText = "Leave updated successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    else
                    {
                        taAttnd.InsertAttnd(empRef.ToString(), Convert.ToDateTime(txtAttndDate.Text.Trim()), shiftRef.ToString(), inTime.ToString(), shiftIn.ToString(),
                           shiftInGrace.ToString(), shiftLate.ToString(), outTime.ToString(), shiftOut.ToString(), shiftOutGrace.ToString(), shiftEarly.ToString(),
                           totHr.ToString(), otMin.ToString(), 1, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                           "", "", "", "1", "L");

                        tblMsg.Rows[0].Cells[0].InnerText = "Leave allocated successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }

                    row.BackColor = System.Drawing.Color.Orange;

                    btnLv.Visible = false;
                    imgSave.Visible = false;

                    lblRem.Text = "Leave taken for the date " + txtAttndDate.Text.Trim();
                    //lnkY.Visible = true;
                    //lnkN.Visible = true;
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

        protected void gvDayAttnd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var rowNum = e.RowIndex;

                if (rowNum == -1) return;

                var empRef = ((HiddenField)(gvDayAttnd.Rows[rowNum].Cells[2].FindControl("hfEmpRef"))).Value;

                var taAtnd = new tblHrmsEmpDayAttndTableAdapter();
                taAtnd.DeleteAttndByEmp(empRef, Convert.ToDateTime(txtAttndDate.Text.Trim()));

                var taEmpMas = new View_Emp_BascTableAdapter();
                var dtEmpMas = new dsEmpDet.View_Emp_BascDataTable();

                var taEmp = new tblHrmsEmpOfficeTableAdapter();

                #region Load Emp List
                if (cboLoc.SelectedIndex == 0)
                    //---ALL Location
                    dtEmpMas = taEmpMas.GetData();
                else
                    if (cboDept.SelectedIndex == 0)
                        //---Selected Location
                        dtEmpMas = taEmpMas.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                    else
                        if (cboSec.SelectedIndex == 0)
                            //---Selected Department
                            dtEmpMas = taEmpMas.GetDataByLocDept(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString());
                        else
                            if (cboShift.SelectedIndex == 0)
                                //---Selected Section
                                dtEmpMas = taEmpMas.GetDataByLocDeptSec(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString());
                            else
                                //---Selected Shift
                                dtEmpMas = taEmpMas.GetDataByLocDeptSecShift(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                                    cboSec.SelectedValue.ToString(), cboShift.SelectedValue.ToString());
                gvDayAttnd.DataSource = dtEmpMas;
                gvDayAttnd.DataBind();
                #endregion

                foreach (GridViewRow gr in gvDayAttnd.Rows)
                {
                    var hfEmpRef = (HiddenField)gr.FindControl("hfEmpRef");
                    var txtIn = ((TextBox)gr.FindControl("txtInTime"));
                    var txtOut = ((TextBox)gr.FindControl("txtOutTime"));
                    var txtTot = (TextBox)gr.FindControl("txtTotHr");
                    var lblRem = (Label)(gr.Cells[2].FindControl("lblRem"));

                    var dtAtnd = taAtnd.GetDataByEmp(hfEmpRef.Value.ToString(), Convert.ToDateTime(txtAttndDate.Text.Trim()));
                    if (dtAtnd.Rows.Count > 0)
                    {
                        var inTime = dtAtnd[0].AttndInTime.ToString();
                        DateTime iTime = DateTime.ParseExact(inTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                        //if you really need a TimeSpan this will get the time elapsed since midnight:
                        TimeSpan ti = iTime.TimeOfDay;
                        txtIn.Text = ti.ToString();

                        var outTime = dtAtnd[0].AttndOutTime.ToString();
                        DateTime oTime = DateTime.ParseExact(outTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                        //if you really need a TimeSpan this will get the time elapsed since midnight:
                        TimeSpan to = oTime.TimeOfDay;
                        txtOut.Text = to.ToString();

                        txtTot.Text = dtAtnd[0].AttndTotHr.ToString();

                        if (dtAtnd[0].AttndFlag.ToString() == "L")
                        {
                            lblRem.Text = "Leave taken for the date " + txtAttndDate.Text.Trim();
                            gr.BackColor = System.Drawing.Color.Orange;
                        }
                        else
                        {
                            gr.BackColor = System.Drawing.Color.LightGreen;
                        }
                    }
                    else
                    {
                        #region Load Default Data
                        var dtEmp = taEmp.GetDataByEmpRef(hfEmpRef.Value.ToString());
                        if (dtEmp.Rows.Count > 0)
                        {
                            var taShift = new tblHrmsShiftTableAdapter();
                            var dtShift = taShift.GetDataByShiftRefNo(Convert.ToInt32(dtEmp[0].ShiftRefNo));
                            if (dtShift.Rows.Count > 0)
                            {
                                var stTime = dtShift[0].ShiftStart.ToString();
                                DateTime sTime = DateTime.ParseExact(stTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                                //if you really need a TimeSpan this will get the time elapsed since midnight:
                                TimeSpan ts = sTime.TimeOfDay;
                                txtIn.Text = ts.ToString();

                                var endTime = dtShift[0].ShiftEnd.ToString();
                                DateTime eTime = DateTime.ParseExact(endTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                                //if you really need a TimeSpan this will get the time elapsed since midnight:
                                TimeSpan te = eTime.TimeOfDay;
                                txtOut.Text = te.ToString();

                                txtTot.Text = dtShift[0].ShiftTotal.ToString();
                            }
                            else
                            {
                                gr.BackColor = System.Drawing.Color.Red;
                            }
                        }
                        #endregion
                    }
                }

                if (gvDayAttnd.Rows.Count > 0)
                {
                    cboLoc.Enabled = false;
                    cboDept.Enabled = false;
                    cboSec.Enabled = false;
                    cboShift.Enabled = false;
                    txtAttndDate.Enabled = false;
                    btnSaveAll.Visible = gvDayAttnd.Rows.Count > 0;
                }

                tblMsg.Rows[0].Cells[0].InnerText = "Data deleted successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }      
    }
}