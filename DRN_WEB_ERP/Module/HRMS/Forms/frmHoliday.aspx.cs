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
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmHoliday : System.Web.UI.Page
    {
        public static List<DateTime> list = new List<DateTime>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taLoc = new tblHrmsOffLocTableAdapter();
            cboLoc.DataSource = taLoc.GetDataByAsc();
            cboLoc.DataValueField = "LocRefNo";
            cboLoc.DataTextField = "LocName";
            cboLoc.DataBind();
            cboLoc.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));

            var curYear = DateTime.Now.Year;
            for (Int64 year = 2014; year <= (curYear + 2); year++)
            {
                cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            cboYear.SelectedValue = curYear.ToString();

            var taHoliday = new tblHrmsHolidayTableAdapter();
            gvHoliday.DataSource = taHoliday.GetDataByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            gvHoliday.DataBind();
        }

        protected void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taDept = new tblHrmsDeptTableAdapter();
            try
            {
                if (cboLoc.SelectedIndex == 0)
                {
                    cboDept.Items.Clear();
                    cboSec.Items.Clear();
                    cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
                    cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
                else
                {
                    var dtDept = taDept.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                    cboDept.DataSource = dtDept;
                    cboDept.DataValueField = "DeptRefNo";
                    cboDept.DataTextField = "DeptName";
                    cboDept.DataBind();
                    cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
                    
                    cboSec.Items.Clear();
                    cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                }                
            }
            catch (Exception ex) { }
        }

        protected void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taSec = new tblHrmsSecTableAdapter();
            try
            {
                if (cboLoc.SelectedIndex == 0)
                {
                    cboSec.Items.Clear();
                    cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
                else
                {
                    var dtSec = taSec.GetDataByDeptRef(cboDept.SelectedValue.ToString());
                    cboSec.DataSource = dtSec;
                    cboSec.DataValueField = "SecRefNo";
                    cboSec.DataTextField = "SecName";
                    cboSec.DataBind();
                    cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                }
            }
            catch (Exception ex) { }
        }

        protected void gvHoliday_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvHoliday_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var holRef = gvHoliday.Rows[rowNum].Cells[0].Text.Trim() == "&nbsp;"
                                          ? "0"
                                          : gvHoliday.Rows[rowNum].Cells[0].Text.Trim();

            var taHol = new tblHrmsHolidayTableAdapter();

            taHol.DeleteHoliday(Convert.ToInt32(holRef));
            tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully";
            tblMsg.Rows[1].Cells[0].InnerText = "";
            ModalPopupExtenderMsg.Show();

            gvHoliday.DataSource = taHol.GetDataByDesc();
            gvHoliday.DataBind();
            gvHoliday.SelectedIndex = -1;
        }

        protected void gvHoliday_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvHoliday.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    hfHolidayRefNo.Value = gvHoliday.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvHoliday.Rows[indx].Cells[0].Text.Trim();

                    lblChngDt.Visible = true;
                    txtHolDate.Visible = true;

                    txtHolDate.Text = gvHoliday.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvHoliday.Rows[indx].Cells[2].Text.Trim();

                    txtHolDesc.Text = gvHoliday.Rows[indx].Cells[3].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvHoliday.Rows[indx].Cells[3].Text.Trim();

                    cboLoc.SelectedValue = gvHoliday.Rows[indx].Cells[4].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvHoliday.Rows[indx].Cells[4].Text.Trim();

                    var taDep = new tblHrmsDeptTableAdapter();
                    cboDept.DataSource = taDep.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                    cboDept.DataValueField = "DeptRefNo";
                    cboDept.DataTextField = "DeptName";
                    cboDept.DataBind();
                    cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));

                    cboDept.SelectedValue = gvHoliday.Rows[indx].Cells[5].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvHoliday.Rows[indx].Cells[5].Text.Trim();

                    var taSec = new tblHrmsSecTableAdapter();
                    cboSec.DataSource = taSec.GetDataByDeptRef(cboDept.SelectedValue.ToString());
                    cboSec.DataValueField = "SecRefNo";
                    cboSec.DataTextField = "SecName";
                    cboSec.DataBind();
                    cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));

                    cboSec.Text = gvHoliday.Rows[indx].Cells[6].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvHoliday.Rows[indx].Cells[6].Text.Trim();

                    txtHolRem.Text = gvHoliday.Rows[indx].Cells[7].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvHoliday.Rows[indx].Cells[7].Text.Trim();
                    Calendar1.Visible = false;
                }
                catch (Exception ex)
                {
                    hfHolidayRefNo.Value = "0";
                    tblMsg.Rows[0].Cells[0].InnerText = "Data loading error.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void btnClearHoliday_Click(object sender, EventArgs e)
        {
            
            Label1.Text = "";

            hfHolidayRefNo.Value = "0";

            cboLoc.SelectedIndex = 0;
            cboDept.Items.Clear();
            cboSec.Items.Clear();
            cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
            cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
            txtHolDesc.Text = "";
            txtHolRem.Text = "";

            lblChngDt.Visible = false;
            txtHolDate.Text = "";
            txtHolDate.Visible = false;

            // Clear the selection and saved list
            SelectedDates = null;

            Calendar1.Visible = true;

            var taHoliday = new tblHrmsHolidayTableAdapter();
            gvHoliday.DataSource = taHoliday.GetDataByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            gvHoliday.DataBind();
            gvHoliday.SelectedIndex = -1;
        }

        protected void btnSaveHoliday_Click(object sender, EventArgs e)
        {
            var taLoc = new tblHrmsOffLocTableAdapter();
            var taDept = new tblHrmsDeptTableAdapter();
            var taSec = new tblHrmsSecTableAdapter();
            var taHoliday = new tblHrmsHolidayTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taHoliday.Connection);

            try
            {
                taHoliday.AttachTransaction(myTran);

                #region Check_Dupplicate
                var dtChk = new dsHrmsTran.tblHrmsHolidayDataTable();
                if (hfHolidayRefNo.Value.ToString() == "0" || hfHolidayRefNo.Value.ToString() == "")
                {
                    if (ViewState["Dates"] != null)
                    {
                        foreach (DateTime dt in SelectedDates)
                        {
                            if (cboLoc.SelectedIndex == 0)
                            {
                                var dtLoc = taLoc.GetData();
                                foreach (dsHrmsMas.tblHrmsOffLocRow drLoc in dtLoc.Rows)
                                {
                                    var dtDept = taDept.GetDataByLocRef(drLoc.LocRefNo.ToString());
                                    foreach (dsHrmsMas.tblHrmsDeptRow drDept in dtDept.Rows)
                                    {
                                        var dtSec = taSec.GetDataByDeptRef(drDept.DeptRefNo.ToString());
                                        foreach (dsHrmsMas.tblHrmsSecRow drSec in dtSec.Rows)
                                        {
                                            dtChk = taHoliday.GetDataByDate(Convert.ToDateTime(dt.ToShortDateString()), drLoc.LocRefNo.ToString(), drDept.DeptRefNo.ToString(), drSec.SecRefNo.ToString());
                                            if (dtChk.Rows.Count > 0)
                                            {
                                                tblMsg.Rows[0].Cells[0].InnerText = "Holiday already exists for the day " + dtChk[0].HolDate.ToString("dd/MM/yyyy");
                                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                                ModalPopupExtenderMsg.Show();
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (cboDept.SelectedIndex == 0)
                                {
                                    var dtDept = taDept.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                                    foreach (dsHrmsMas.tblHrmsDeptRow drDept in dtDept.Rows)
                                    {
                                        var dtSec = taSec.GetDataByDeptRef(drDept.DeptRefNo.ToString());
                                        foreach (dsHrmsMas.tblHrmsSecRow drSec in dtSec.Rows)
                                        {
                                            dtChk = taHoliday.GetDataByDate(Convert.ToDateTime(dt.ToShortDateString()), cboLoc.SelectedValue.ToString(), drDept.DeptRefNo.ToString(), drSec.SecRefNo.ToString());
                                            if (dtChk.Rows.Count > 0)
                                            {
                                                tblMsg.Rows[0].Cells[0].InnerText = "Holiday already exists for the day " + dtChk[0].HolDate.ToString("dd/MM/yyyy");
                                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                                ModalPopupExtenderMsg.Show();
                                                return;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (cboSec.SelectedIndex == 0)
                                    {
                                        var dtSec = taSec.GetDataByDeptRef(cboDept.SelectedValue.ToString());
                                        foreach (dsHrmsMas.tblHrmsSecRow drSec in dtSec.Rows)
                                        {
                                            dtChk = taHoliday.GetDataByDate(Convert.ToDateTime(dt.ToShortDateString()), cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(), drSec.SecRefNo.ToString());
                                            if (dtChk.Rows.Count > 0)
                                            {
                                                tblMsg.Rows[0].Cells[0].InnerText = "Holiday already exists for the day " + dtChk[0].HolDate.ToString("dd/MM/yyyy");
                                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                                ModalPopupExtenderMsg.Show();
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dtChk = taHoliday.GetDataByDate(Convert.ToDateTime(dt.ToShortDateString()), cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString());
                                        if (dtChk.Rows.Count > 0)
                                        {
                                            tblMsg.Rows[0].Cells[0].InnerText = "Holiday already exists for the day " + dtChk[0].HolDate.ToString("dd/MM/yyyy");
                                            tblMsg.Rows[1].Cells[0].InnerText = "";
                                            ModalPopupExtenderMsg.Show();
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "No date selected. Select date first.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                #endregion

                if (hfHolidayRefNo.Value.ToString() == "0" || hfHolidayRefNo.Value.ToString() == "")
                {
                    if (ViewState["Dates"] != null)
                    {
                        foreach (DateTime dt in SelectedDates)
                        {
                            if (cboLoc.SelectedIndex == 0)
                            {
                                var dtLoc = taLoc.GetData();
                                foreach (dsHrmsMas.tblHrmsOffLocRow drLoc in dtLoc.Rows)
                                {
                                    var dtDept = taDept.GetDataByLocRef(drLoc.LocRefNo.ToString());
                                    foreach (dsHrmsMas.tblHrmsDeptRow drDept in dtDept.Rows)
                                    {
                                        var dtSec = taSec.GetDataByDeptRef(drDept.DeptRefNo.ToString());
                                        foreach (dsHrmsMas.tblHrmsSecRow drSec in dtSec.Rows)
                                        {
                                            taHoliday.InsertHoliday(drLoc.LocRefNo.ToString(), drDept.DeptRefNo.ToString(), drSec.SecRefNo.ToString(),
                                                Convert.ToDateTime(dt.ToShortDateString()), txtHolDesc.Text.Trim(),
                                                txtHolRem.Text.Trim(), DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                                "", "", "", "1", "");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (cboDept.SelectedIndex == 0)
                                {
                                    var dtDept = taDept.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                                    foreach (dsHrmsMas.tblHrmsDeptRow drDept in dtDept.Rows)
                                    {
                                        var dtSec = taSec.GetDataByDeptRef(drDept.DeptRefNo.ToString());
                                        foreach (dsHrmsMas.tblHrmsSecRow drSec in dtSec.Rows)
                                        {
                                            taHoliday.InsertHoliday(cboLoc.SelectedValue.ToString(), drDept.DeptRefNo.ToString(), drSec.SecRefNo.ToString(),
                                                Convert.ToDateTime(dt.ToShortDateString()), txtHolDesc.Text.Trim(),
                                                txtHolRem.Text.Trim(), DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                                "", "", "", "1", "");
                                        }
                                    }
                                }
                                else
                                {
                                    if (cboSec.SelectedIndex == 0)
                                    {
                                        var dtSec = taSec.GetDataByDeptRef(cboDept.SelectedValue.ToString());
                                        foreach (dsHrmsMas.tblHrmsSecRow drSec in dtSec.Rows)
                                        {
                                            taHoliday.InsertHoliday(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(), drSec.SecRefNo.ToString(),
                                                Convert.ToDateTime(dt.ToShortDateString()), txtHolDesc.Text.Trim(),
                                                txtHolRem.Text.Trim(), DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                                "", "", "", "1", "");
                                        }
                                    }
                                    else
                                    {
                                        taHoliday.InsertHoliday(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString(),
                                                Convert.ToDateTime(dt.ToShortDateString()), txtHolDesc.Text.Trim(),
                                                txtHolRem.Text.Trim(), DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                                "", "", "", "1", "");
                                    }
                                }
                            }
                            //dates = dates + ", " + dt.ToShortDateString();
                        }



                        myTran.Commit();

                        tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "No date selected. Select date first.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    if (txtHolDate.Text.Trim().Length <= 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Enter date first.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    taHoliday.UpdateHoliday(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString(),
                                                Convert.ToDateTime(txtHolDate.Text.Trim()), txtHolDesc.Text.Trim(), txtHolRem.Text.Trim(), DateTime.Now,
                                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "",
                                                Convert.ToInt32(hfHolidayRefNo.Value.ToString()));

                    myTran.Commit();

                    tblMsg.Rows[0].Cells[0].InnerText = "Data updated successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }

                hfHolidayRefNo.Value = "0";

                cboLoc.SelectedIndex = 0;
                cboDept.Items.Clear();
                cboSec.Items.Clear();
                cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                txtHolDesc.Text = "";
                txtHolRem.Text = "";

                lblChngDt.Visible = false;
                txtHolDate.Text = "";
                txtHolDate.Visible = false;

                SelectedDates = null;


                gvHoliday.DataSource = taHoliday.GetDataByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                gvHoliday.DataBind();
                gvHoliday.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            finally { Calendar1.Visible = true; }
        }

        public List<DateTime> SelectedDates
        {
            get
            {
                if (ViewState["Dates"] != null)
                    return (List<DateTime>)ViewState["Dates"];
                else
                    // Add a hidden dateTime to clear the selection of Date when
                    // there is only one date in the selected Dates
                    return new List<DateTime>();//{ DateTime.MaxValue.AddDays(-2) };
            }
            set
            {
                ViewState["Dates"] = value;
            }
        }

        protected void Calendar1_PreRender(object sender, EventArgs e)
        {
            // Reset Selected Dates
            Calendar1.SelectedDates.Clear();
            // Select previously Selected Dates
            foreach (DateTime dt in SelectedDates)
                Calendar1.SelectedDates.Add(dt);
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //Check if selected Date is in the saved list
            // Remove the Selected Date from the saved list
            if (SelectedDates.Contains(Calendar1.SelectedDate))
                SelectedDates.Remove(Calendar1.SelectedDate);
            else
                SelectedDates.Add(Calendar1.SelectedDate);
            ViewState["Dates"] = SelectedDates;
        }

        protected void Calendar1_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
        {
            // Disable dates of past/future months
            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
                //e.Cell.Text = "X";
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taHoliday = new tblHrmsHolidayTableAdapter();
            gvHoliday.DataSource = taHoliday.GetDataByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            gvHoliday.DataBind();
        }        
    } 
}