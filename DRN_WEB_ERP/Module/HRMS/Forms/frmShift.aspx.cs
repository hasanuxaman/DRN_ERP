using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmShift : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            TimeSpan stTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(18, 0, 0);
            txtShiftStart.Attributes.Add("value", stTime.ToString());
            txtShiftEnd.Attributes.Add("value", endTime.ToString());
            txtShiftTotal.Text = "9.00";

            var taSec = new tblHrmsSecTableAdapter();
            cboSection.DataSource = taSec.GetDataByAsc();
            cboSection.DataValueField = "SecRefNo";
            cboSection.DataTextField = "SecName";
            cboSection.DataBind();

            var taShift = new tblHrmsShiftTableAdapter();
            gvShift.DataSource = taShift.GetDataByAsc();
            gvShift.DataBind();
        }

        protected void btnSaveShift_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taShift = new tblHrmsShiftTableAdapter();
            try
            {
                var dtDept = taShift.GetDataByShiftRefNo(Convert.ToInt32(hfShiftRefNo.Value.ToString()));
                if (dtDept.Rows.Count > 0)
                {
                    taShift.UpdateShift(txtShiftCode.Text.Trim(), txtShiftName.Text.Trim(), cboSection.SelectedValue.ToString(), txtShiftDesc.Text.Trim(), txtShiftRem.Text.Trim(),
                            Convert.ToDateTime(txtShiftStart.Text.Trim()).ToString("hh:mm tt"), txtShiftGrace.Text.Trim(), 
                            Convert.ToDateTime(txtShiftEnd.Text.Trim()).ToString("hh:mm tt"), txtShiftGrace.Text.Trim(), "0", txtShiftTotal.Text.Trim(),
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "", Convert.ToInt32(hfShiftRefNo.Value));

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtDeptExist = taShift.GetDataByCodeName(txtShiftCode.Text.Trim(), txtShiftName.Text.Trim());
                    if (dtDeptExist.Rows.Count > 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Data Exists with the same Shift Code or Name";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    else
                    {
                        var dtMaxShiftRef = taShift.GetMaxShiftRefNo();
                        var nextShiftRef = dtMaxShiftRef == null ? 100001 : Convert.ToInt32(dtMaxShiftRef) + 1;

                        taShift.InsertShift(nextShiftRef, txtShiftCode.Text.Trim(), txtShiftName.Text.Trim(), cboSection.SelectedValue.ToString(), txtShiftDesc.Text.Trim(),
                            txtShiftRem.Text.Trim(), Convert.ToDateTime(txtShiftStart.Text.Trim()).ToString("hh:mm tt"), txtShiftGrace.Text.Trim(),
                            Convert.ToDateTime(txtShiftEnd.Text.Trim()).ToString("hh:mm tt"), txtShiftGrace.Text.Trim(), "0", txtShiftTotal.Text.Trim(), DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "");

                        tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                hfShiftRefNo.Value = "0";
                txtShiftCode.Text = "";
                txtShiftName.Text = "";
                txtShiftDesc.Text = "";
                txtShiftRem.Text = "";
                txtShiftStart.Text = "";
                txtShiftEnd.Text = "";
                txtShiftTotal.Text = "";
                txtShiftGrace.Text = "";
                gvShift.DataSource = taShift.GetDataByAsc();
                gvShift.DataBind();
                gvShift.SelectedIndex = -1;
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearShift_Click(object sender, EventArgs e)
        {
            hfShiftRefNo.Value = "0";
            txtShiftCode.Text = "";
            txtShiftName.Text = "";
            txtShiftDesc.Text = "";
            txtShiftRem.Text = "";
            txtShiftStart.Text = "";
            txtShiftEnd.Text = "";
            txtShiftTotal.Text = "";
            txtShiftGrace.Text = "";
            var taShift = new tblHrmsShiftTableAdapter();
            gvShift.DataSource = taShift.GetDataByAsc();
            gvShift.DataBind();
            gvShift.SelectedIndex = -1;
        }

        protected void gvShift_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvShift.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    hfShiftRefNo.Value = gvShift.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvShift.Rows[indx].Cells[0].Text.Trim();
                    cboSection.SelectedValue = gvShift.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvShift.Rows[indx].Cells[1].Text.Trim();
                    txtShiftCode.Text = gvShift.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvShift.Rows[indx].Cells[2].Text.Trim();
                    txtShiftName.Text = gvShift.Rows[indx].Cells[3].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvShift.Rows[indx].Cells[3].Text.Trim();
                    txtShiftDesc.Text = gvShift.Rows[indx].Cells[4].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvShift.Rows[indx].Cells[4].Text.Trim();
                    txtShiftRem.Text = gvShift.Rows[indx].Cells[5].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvShift.Rows[indx].Cells[5].Text.Trim();
                    
                    var stTime = gvShift.Rows[indx].Cells[6].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvShift.Rows[indx].Cells[6].Text.Trim();
                    DateTime sTime = DateTime.ParseExact(stTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    //if you really need a TimeSpan this will get the time elapsed since midnight:
                    TimeSpan ts = sTime.TimeOfDay;
                    txtShiftStart.Text = ts.ToString();

                    var endTime = gvShift.Rows[indx].Cells[7].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvShift.Rows[indx].Cells[7].Text.Trim();
                    DateTime eTime = DateTime.ParseExact(endTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    //if you really need a TimeSpan this will get the time elapsed since midnight:
                    TimeSpan te = eTime.TimeOfDay;
                    txtShiftEnd.Text = te.ToString();

                    txtShiftTotal.Text = gvShift.Rows[indx].Cells[8].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvShift.Rows[indx].Cells[8].Text.Trim();
                    txtShiftGrace.Text = gvShift.Rows[indx].Cells[9].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvShift.Rows[indx].Cells[9].Text.Trim();
                }
                catch (Exception ex)
                {
                    hfShiftRefNo.Value = "0";
                    txtShiftCode.Text = "";
                    txtShiftName.Text = "";
                    txtShiftDesc.Text = "";
                    txtShiftRem.Text = "";
                    txtShiftStart.Text = "";
                    txtShiftEnd.Text = "";
                    txtShiftTotal.Text = "";
                    txtShiftGrace.Text = "";
                    gvShift.SelectedIndex = -1;

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void txtShiftStart_TextChanged(object sender, EventArgs e)
        {
            if (txtShiftStart.Text.Trim().Length > 0 && txtShiftEnd.Text.Trim().Length > 0)
            {
                DateTime dtSt = Convert.ToDateTime(txtShiftEnd.Text.Trim());
                DateTime dtEnd = Convert.ToDateTime(txtShiftStart.Text.Trim());

                txtShiftTotal.Text = dtSt.Subtract(dtEnd).TotalHours.ToString("F2");
            }
        }

        protected void txtShiftEnd_TextChanged(object sender, EventArgs e)
        {
            if (txtShiftStart.Text.Trim().Length > 0 && txtShiftEnd.Text.Trim().Length > 0)
            {
                DateTime dtSt = Convert.ToDateTime(txtShiftEnd.Text.Trim());
                DateTime dtEnd = Convert.ToDateTime(txtShiftStart.Text.Trim());

                txtShiftTotal.Text = dtSt.Subtract(dtEnd).TotalHours.ToString("F2");
            }
        }

        protected void gvShift_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var shiftRef = gvShift.Rows[rowNum].Cells[0].Text.Trim() == "&nbsp;"
                                          ? "0"
                                          : gvShift.Rows[rowNum].Cells[0].Text.Trim();

            var taEmpOff = new tblHrmsEmpOfficeTableAdapter();
            var dtEmpOff = taEmpOff.GetDataByDeptRef(shiftRef);
            if (dtEmpOff.Rows.Count > 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to delete this Shift.";
                tblMsg.Rows[1].Cells[0].InnerText = "Shift already used.";
                ModalPopupExtenderMsg.Show();
                return;
            }
            else
            {
                var taShift = new tblHrmsShiftTableAdapter();
                var dtShift = taShift.GetDataByShiftRefNo(Convert.ToInt32(shiftRef));
                if (dtShift.Rows.Count > 0)
                {
                    taShift.DeleteShift(Convert.ToInt32(shiftRef));
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }

                gvShift.DataSource = taShift.GetDataByAsc();
                gvShift.DataBind();
                gvShift.SelectedIndex = -1;
            }
        }
    }
}