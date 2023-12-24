using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesTargetCust : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtTrgtDate.Text = DateTime.Now.ToString("MMMM/yyyy");

            var taSalesZone = new tblSalesZoneTableAdapter();
            var dtSalesZone = taSalesZone.GetDataByAsc();
            foreach (dsSalesMas.tblSalesZoneRow dr in dtSalesZone.Rows)
            {
                cboSalesZone.Items.Add(new ListItem(dr.SalesZoneName.ToString(), dr.SalesZoneRef.ToString()));
            }
            cboSalesZone.Items.Insert(0, new ListItem("---Select---", "0"));

            //var taDealerMas = new View_Sales_Party_AdrTableAdapter();
            //var dtDealerMas = new DataTable();
            //if (cboSalesZone.SelectedIndex == 0)
            //    dtDealerMas = taDealerMas.GetData();
            //else
            //    dtDealerMas = taDealerMas.GetDataBySalesZone(Convert.ToInt32(cboSalesZone.SelectedValue));
            //gvSalesTarget.DataSource = dtDealerMas;
            //gvSalesTarget.DataBind();
            //btnSaveAll.Visible = dtDealerMas.Rows.Count > 0;
        }

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            var taSalesTrgtDealer = new tblSalesTargetDealerTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesTrgtDealer.Connection);

            try
            {
                taSalesTrgtDealer.AttachTransaction(myTran);

                try
                {
                    var trgtMonth = Convert.ToDateTime(txtTrgtDate.Text).Month.ToString("00");
                    var trgtYear = Convert.ToDateTime(txtTrgtDate.Text).Year.ToString();

                    DateTime today = Convert.ToDateTime(txtTrgtDate.Text);
                    int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
                    DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
                    DateTime endOfMonth = new DateTime(today.Year, today.Month, daysInMonth);

                    taSalesTrgtDealer.DeleteSalesTargetDealer(trgtMonth, trgtYear, cboSalesZone.SelectedValue.ToString());

                    foreach (GridViewRow gr in gvSalesTarget.Rows)
                    {
                        var hfDealerRef = ((HiddenField)(gr.FindControl("hfDealerRef"))).Value;
                        var lblDealerName = ((Label)(gr.FindControl("lblDealerName"))).Text;
                        var hfSpRef = ((HiddenField)(gr.FindControl("hfSpRef"))).Value;
                        var lblSpName = ((Label)(gr.FindControl("lblSpName"))).Text;
                        var hfDsmRef = ((HiddenField)(gr.FindControl("hfDsmRef"))).Value;
                        var lblDsmName = ((Label)(gr.FindControl("lblDsmName"))).Text;
                        var hfSalesZoneRef = ((HiddenField)(gr.FindControl("hfSalesZoneRef"))).Value;
                        var lblSalesZoneName = ((Label)(gr.FindControl("lblSalesZoneName"))).Text;

                        var txtTrgReg = ((TextBox)(gr.FindControl("txtTrgReg"))).Text;
                        var txtTrgtSup = ((TextBox)(gr.FindControl("txtTrgtSup"))).Text;
                        var txtTrgtEstPcc = ((TextBox)(gr.FindControl("txtTrgtEstPcc"))).Text;
                        var txtTrgtEstOpc = ((TextBox)(gr.FindControl("txtTrgtEstOpc"))).Text;
                        var txtTrgtCollect = ((TextBox)(gr.FindControl("txtTrgtCollect"))).Text;

                        var hfItemCodeRegular = ((HiddenField)(gr.FindControl("hfItemCodeRegular"))).Value;
                        var hfItemCodeSupreme = ((HiddenField)(gr.FindControl("hfItemCodeSupreme"))).Value;
                        var hfItemCodeEasternP = ((HiddenField)(gr.FindControl("hfItemCodeEasternP"))).Value;
                        var hfItemCodeEasternO = ((HiddenField)(gr.FindControl("hfItemCodeEasternO"))).Value;
                        var hfItemCodeCollection = ((HiddenField)(gr.FindControl("hfItemCodeCollection"))).Value;

                        var hfItemNameRegular = ((HiddenField)(gr.FindControl("hfItemNameRegular"))).Value;
                        var hfItemNameSupreme = ((HiddenField)(gr.FindControl("hfItemNameSupreme"))).Value;
                        var hfItemNameEasternP = ((HiddenField)(gr.FindControl("hfItemNameEasternP"))).Value;
                        var hfItemNameEasternO = ((HiddenField)(gr.FindControl("hfItemNameEasternO"))).Value;
                        var hfItemNameCollection = ((HiddenField)(gr.FindControl("hfItemNameCollection"))).Value;

                        if (txtTrgReg.ToString().Trim() == "" || txtTrgReg.ToString().Trim().Length <= 0) txtTrgReg = "0";
                        if (txtTrgtSup.ToString().Trim() == "" || txtTrgtSup.ToString().Trim().Length <= 0) txtTrgtSup = "0";
                        if (txtTrgtEstPcc.ToString().Trim() == "" || txtTrgtEstPcc.ToString().Trim().Length <= 0) txtTrgtEstPcc = "0";
                        if (txtTrgtEstOpc.ToString().Trim() == "" || txtTrgtEstOpc.ToString().Trim().Length <= 0) txtTrgtEstOpc = "0";
                        if (txtTrgtCollect.ToString().Trim() == "" || txtTrgtCollect.ToString().Trim().Length <= 0) txtTrgtCollect = "0";

                        taSalesTrgtDealer.InsertSalesTargetDealer(hfDealerRef, lblDealerName, hfDsmRef, lblDsmName, hfSalesZoneRef, lblSalesZoneName, trgtMonth, trgtYear,
                            (trgtMonth + "/" + trgtYear), startOfMonth, endOfMonth, hfItemCodeRegular, hfItemNameRegular, Convert.ToDouble(txtTrgReg), "", "", "", "", "",
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        taSalesTrgtDealer.InsertSalesTargetDealer(hfDealerRef, lblDealerName, hfDsmRef, lblDsmName, hfSalesZoneRef, lblSalesZoneName, trgtMonth, trgtYear,
                            (trgtMonth + "/" + trgtYear), startOfMonth, endOfMonth, hfItemCodeSupreme, hfItemNameSupreme, Convert.ToDouble(txtTrgtSup), "", "", "", "", "",
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        taSalesTrgtDealer.InsertSalesTargetDealer(hfDealerRef, lblDealerName, hfDsmRef, lblDsmName, hfSalesZoneRef, lblSalesZoneName, trgtMonth, trgtYear,
                            (trgtMonth + "/" + trgtYear), startOfMonth, endOfMonth, hfItemCodeEasternP, hfItemNameEasternP, Convert.ToDouble(txtTrgtEstPcc), "", "", "", "", "",
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        taSalesTrgtDealer.InsertSalesTargetDealer(hfDealerRef, lblDealerName, hfDsmRef, lblDsmName, hfSalesZoneRef, lblSalesZoneName, trgtMonth, trgtYear,
                            (trgtMonth + "/" + trgtYear), startOfMonth, endOfMonth, hfItemCodeEasternO, hfItemNameEasternO, Convert.ToDouble(txtTrgtEstOpc), "", "", "", "", "",
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        taSalesTrgtDealer.InsertSalesTargetDealer(hfDealerRef, lblDealerName, hfDsmRef, lblDsmName, hfSalesZoneRef, lblSalesZoneName, trgtMonth, trgtYear,
                            (trgtMonth + "/" + trgtYear), startOfMonth, endOfMonth, hfItemCodeCollection, hfItemNameCollection, Convert.ToDouble(txtTrgtCollect), "", "", "", "", "",
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    //var taSpMas = new View_Sales_SpTableAdapter();
                    //var dtSpMas = new DataTable();
                    //if (cboDsm.SelectedIndex == 0)
                    //    dtSpMas = taSpMas.GetData();
                    //else
                    //    dtSpMas = taSpMas.GetDataByDsm(Convert.ToInt32(cboDsm.SelectedValue));
                    //gvSalesTarget.DataBind();
                    //btnSaveAll.Visible = dtSpMas.Rows.Count > 0;
                }
                catch (Exception ex)
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        #region GridData
        public string GetSlsTrgt(string DealerRef, string ItemCode)
        {
            var taSalesTrgtDealer = new tblSalesTargetDealerTableAdapter();

            string slsTrgt = "";
            try
            {
                var dtSalesTrgtDealer = taSalesTrgtDealer.GetDataByDealerItem(DealerRef, ItemCode, Convert.ToDateTime(txtTrgtDate.Text).Month.ToString("00"), Convert.ToDateTime(txtTrgtDate.Text).Year.ToString());
                if (dtSalesTrgtDealer.Rows.Count > 0)
                    slsTrgt = dtSalesTrgtDealer[0].IsTrgt_QtyNull() ? "" : (dtSalesTrgtDealer[0].Trgt_Qty > 0 ? dtSalesTrgtDealer[0].Trgt_Qty.ToString() : "");

                return slsTrgt;
            }
            catch (Exception ex) { return slsTrgt; }
        }

        public string GetSlsZone(string slsZoneRef)
        {
            var taSalesZone = new tblSalesZoneTableAdapter();

            string SalesZone = "";
            try
            {
                var dtSalesZone = taSalesZone.GetDataBySlsZoneRef(Convert.ToInt32(slsZoneRef));
                if (dtSalesZone.Rows.Count > 0)
                    SalesZone = dtSalesZone.Rows.Count > 0 ? dtSalesZone[0].SalesZoneName.ToString() : "";

                return SalesZone;
            }
            catch (Exception ex) { return SalesZone; }
        }

        public string GetDsmName(string dsmRef)
        {
            var taDsm = new tblSalesDsmMasTableAdapter();

            string DsmName = "";
            try
            {
                var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(dsmRef));
                if (dtDsm.Rows.Count > 0)
                    DsmName = dtDsm[0].Dsm_Full_Name.ToString();

                return DsmName;
            }
            catch (Exception ex) { return DsmName; }
        }

        public string GetSpName(string spRef)
        {
            var taSp = new tblSalesPersonMasTableAdapter();

            string SpName = "";
            try
            {
                var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(spRef));
                if (dtSp.Rows.Count > 0)
                    SpName = dtSp[0].Sp_Full_Name.ToString();

                return SpName;
            }
            catch (Exception ex) { return SpName; }
        }
        #endregion

        protected void txtTrgtDate_TextChanged(object sender, EventArgs e)
        {
            var taDealerMas = new View_Sales_Party_AdrTableAdapter();
            var dtDealerMas = new DataTable();
            if (cboSalesZone.SelectedIndex != 0)
            //    dtDealerMas = taDealerMas.GetData();
            //else
                dtDealerMas = taDealerMas.GetDataBySalesZone(Convert.ToInt32(cboSalesZone.SelectedValue));
            gvSalesTarget.DataSource = dtDealerMas;
            gvSalesTarget.DataBind();
            btnSaveAll.Visible = dtDealerMas.Rows.Count > 0;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            var taDealerMas = new View_Sales_Party_AdrTableAdapter();
            var dtDealerMas = new DataTable();
            if (cboSalesZone.SelectedIndex != 0)
                //dtDealerMas = taDealerMas.GetData();
            //else
                dtDealerMas = taDealerMas.GetDataBySalesZone(Convert.ToInt32(cboSalesZone.SelectedValue));
            gvSalesTarget.DataSource = dtDealerMas;
            gvSalesTarget.DataBind();
            btnSaveAll.Visible = dtDealerMas.Rows.Count > 0;
        }

        protected void cboSalesZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taDealerMas = new View_Sales_Party_AdrTableAdapter();
            var dtDealerMas = new DataTable();
            if (cboSalesZone.SelectedIndex != 0)
            //    dtDealerMas = taDealerMas.GetData();
            //else
                dtDealerMas = taDealerMas.GetDataBySalesZone(Convert.ToInt32(cboSalesZone.SelectedValue));
            gvSalesTarget.DataSource = dtDealerMas;
            gvSalesTarget.DataBind();
            btnSaveAll.Visible = dtDealerMas.Rows.Count > 0;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = Convert.ToDateTime(txtTrgtDate.Text.Trim());
                int daysInMonth = DateTime.DaysInMonth(fromDate.Year, fromDate.Month);
                DateTime startOfMonth = new DateTime(Convert.ToDateTime(txtTrgtDate.Text.Trim()).Year, Convert.ToDateTime(txtTrgtDate.Text.Trim()).Month, 1);
                DateTime endOfMonth = new DateTime(Convert.ToDateTime(txtTrgtDate.Text.Trim()).Year, Convert.ToDateTime(txtTrgtDate.Text.Trim()).Month, daysInMonth);
                int todayDay = 1;
                if (fromDate.Month == DateTime.Now.Month && fromDate.Year == DateTime.Now.Year)
                    todayDay = DateTime.Now.Day;
                else
                    todayDay = daysInMonth;

                var qryStr = "SELECT     Trgt_DLR_Ref,tblSalesPartyAdr.Par_Adr_Name, SUM(CASE WHEN Trgt_Item_Ref = '100001' THEN Trgt_Qty ELSE 0 END) AS Target_Supreme, " +
                             "SUM(CASE WHEN Trgt_Item_Ref = '100002' THEN Trgt_Qty ELSE 0 END) AS Target_Regular, SUM(CASE WHEN Trgt_Item_Ref = '100003' THEN Trgt_Qty ELSE 0 END) " +
                             "AS Target_Eastern_P, SUM(CASE WHEN Trgt_Item_Ref = '100004' THEN Trgt_Qty ELSE 0 END) AS Target_Eastern_O, SUM(CASE WHEN Trgt_Item_Ref IN ('100001', " +
                             "'100002', '100003', '100004') THEN Trgt_Qty ELSE 0 END) AS Total_Vol, ROUND(SUM(CASE WHEN Trgt_Item_Ref IN ('100001', '100002', '100003', '100004') " +
                             "THEN Trgt_Qty ELSE 0 END) / CONVERT(int, " + daysInMonth + "), 2) AS Terget_Per_Day, ROUND(ROUND(SUM(CASE WHEN Trgt_Item_Ref IN ('100001', '100002', '100003', '100004') " +
                             "THEN Trgt_Qty ELSE 0 END) / CONVERT(int, " + daysInMonth + "), 2) * " + todayDay + ", 2) AS Proj_Target_Till_Today, SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END) " +
                             "AS Target_Collection, ROUND(SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END) / CONVERT(int, " + daysInMonth + "), 2) AS Trgt_Collct_Per_Day, " +
                             "ROUND(ROUND(SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END) / CONVERT(int, " + daysInMonth + "), 2) * " + todayDay + ", 2) AS Proj_Target_Collect_Till_Today " +
                             "FROM         dbo.tblSalesTargetDealer left outer join tblSalesPartyAdr on Trgt_DLR_Ref=Par_Adr_Ref " +
                             "WHERE     (CONVERT(date, Trgt_From_Dt, 103) >= CONVERT(date, '" + startOfMonth.ToString("dd/MM/yyyy") + "', 103)) AND (CONVERT(date, Trgt_To_Dt, 103) <= CONVERT(date, '" + endOfMonth.ToString("dd/MM/yyyy") + "', 103)) " +
                             "and Trgt_Qty>0 GROUP BY Trgt_DLR_Ref,tblSalesPartyAdr.Par_Adr_Name";

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
                string filename = String.Format("Sales_Target_Dealer_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
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