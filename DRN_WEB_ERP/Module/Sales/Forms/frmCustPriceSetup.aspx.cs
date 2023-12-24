using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmCustPriceSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
            var taItemDet = new tbl_InMa_Item_DetTableAdapter();

            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var custRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            var dtItemDet = taItemDet.GetFgItemData();
                            gvItemDet.DataSource = dtItemDet;
                            gvItemDet.DataBind();

                            btnSaveAll.Visible = dtItemDet.Rows.Count > 0;
                        }

                        txtSearch.Enabled = false;
                        btnClearSrch.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtSearch.Enabled = true;
            btnClearSrch.Visible = false;

            var taItemDet = new tbl_InMa_Item_DetTableAdapter();
            var dtItemDet = taItemDet.GetDataByItemRef(0);
            gvItemDet.DataSource = dtItemDet;
            gvItemDet.DataBind();
            btnSaveAll.Visible = dtItemDet.Rows.Count > 0;
        }

        #region GridData
        public string GetPrice(int itemCode)
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
            var taItemDet = new tbl_InMa_Item_DetTableAdapter();

            string itmPrice = "";
            try
            {
                var custRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            var taPrice = new tblSalesPriceCustTableAdapter();
                            var dtPrice = taPrice.GetDataByPartyRef(dtPartyAdr[0].Par_Adr_Ref.ToString(), itemCode.ToString());
                            if (dtPrice.Rows.Count > 0)
                                itmPrice = dtPrice[0].IsPar_Adr_PriceNull() ? "" : (dtPrice[0].Par_Adr_Price > 0 ? dtPrice[0].Par_Adr_Price.ToString() : "");
                            else
                            {
                                //var taItemPrice = new tbl_InMa_Item_DetTableAdapter();
                                //var dtItemPrice = taItemPrice.GetDataByItemRef(Convert.ToInt32(itemCode.ToString()));
                                //if (dtItemPrice.Rows.Count > 0)
                                //    itmPrice = dtItemPrice[0].IsItm_Det_Ext_Data1Null() ? "0" : dtItemPrice[0].Itm_Det_Ext_Data1.ToString();
                                itmPrice = "";
                            }
                        }
                    }
                }
                return itmPrice;
            }
            catch (Exception ex) { return itmPrice; }
        }

        public string GetBns(int itemCode)
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
            var taItemDet = new tbl_InMa_Item_DetTableAdapter();

            string bnsPerc = "";
            try
            {
                var custRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            var taPrice = new tblSalesPriceCustTableAdapter();
                            var dtPrice = taPrice.GetDataByPartyRef(dtPartyAdr[0].Par_Adr_Ref.ToString(), itemCode.ToString());
                            if (dtPrice.Rows.Count > 0)
                                bnsPerc = dtPrice[0].IsPar_Adr_BonusNull() ? "" : (dtPrice[0].Par_Adr_Bonus > 0 ? dtPrice[0].Par_Adr_Bonus.ToString() : "");
                            else
                            {
                                //var taItemPrice = new tbl_InMa_Item_DetTableAdapter();
                                //var dtItemPrice = taItemPrice.GetDataByItemRef(Convert.ToInt32(itemCode.ToString()));
                                //if (dtItemPrice.Rows.Count > 0)
                                //    bnsPerc = dtItemPrice[0].IsItm_Det_Ext_Data2Null() ? "0" : dtItemPrice[0].Itm_Det_Ext_Data2.ToString();
                            }
                        }
                    }
                }
                return bnsPerc;
            }
            catch (Exception ex) { return bnsPerc; }
        }
        #endregion

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
            var taPrice = new tblSalesPriceCustTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPrice.Connection);

            try
            {
                if (txtSearch.Text.Trim().Length <= 0) return;

                var custRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            taPartyAdr.AttachTransaction(myTran);
                            taPrice.AttachTransaction(myTran);

                            taPrice.DeleteSalesPrice(custRef.ToString());

                            try
                            {
                                var i = 0;
                                foreach (GridViewRow gr in gvItemDet.Rows)
                                {
                                    var itemCode = ((Label)(gr.FindControl("lblIcode"))).Text;
                                    var newPrice = ((TextBox)(gr.FindControl("txtNewPrice"))).Text;
                                    var newPriceValidDt = ((TextBox)(gr.FindControl("txtPriceValidDate"))).Text;
                                    var bnsPerc = ((TextBox)(gr.FindControl("txtBnsPerc"))).Text;
                                    var bnsPercValidDt = ((TextBox)(gr.FindControl("txtBonusValidDate"))).Text;

                                    DateTime? priceValidDt = null;
                                    if (newPriceValidDt.Length > 0) priceValidDt = Convert.ToDateTime(newPriceValidDt);

                                    DateTime? bnsValidDt = null;
                                    if (bnsPercValidDt.Length > 0) bnsValidDt = Convert.ToDateTime(bnsPercValidDt);

                                    if (newPrice.ToString().Trim() == "" || newPrice.ToString().Trim().Length <= 0) newPrice = "0";
                                    if (bnsPerc.ToString().Trim() == "" || bnsPerc.ToString().Trim().Length <= 0) bnsPerc = "0";

                                    if (newPrice != "0" || bnsPerc != "0")
                                    {
                                        var dtPrice = taPrice.GetDataByPartyRef(dtPartyAdr[0].Par_Adr_Ref.ToString(), itemCode.ToString());
                                        if (dtPrice.Rows.Count > 0)
                                        {
                                            taPrice.UpdateSalesPrice(Convert.ToDecimal(newPrice), priceValidDt, Convert.ToDecimal(bnsPerc),
                                                bnsValidDt, "", "", "", "", "", DateTime.Now,
                                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                                                dtPartyAdr[0].Par_Adr_Ref.ToString(), itemCode.ToString());
                                        }
                                        else
                                        {
                                            taPrice.InsertSalesPrice(dtPartyAdr[0].Par_Adr_Ref.ToString(), itemCode.ToString(), Convert.ToDecimal(newPrice), priceValidDt,
                                                Convert.ToDecimal(bnsPerc), bnsValidDt, "", "", "", "", "", DateTime.Now,
                                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");
                                        }
                                        i++;
                                    }
                                }
                                if (i > 0)
                                {
                                    myTran.Commit();
                                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                    ModalPopupExtenderMsg.Show();
                                }
                                else
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[0].Cells[0].InnerText = "There is no valid data. Data not saved.";
                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                    ModalPopupExtenderMsg.Show();
                                }

                                var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                                var dtItemDet = taItemDet.GetFgItemData();
                                gvItemDet.DataSource = dtItemDet;
                                gvItemDet.DataBind();
                                btnSaveAll.Visible = dtItemDet.Rows.Count > 0;
                            }
                            catch (Exception ex)
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var qryStr = "SELECT [Par_Adr_Ref] as Dealer_Ref,[Par_Adr_Ref_No] as Dealer_Code,[Par_Adr_Name] as Dealer_Name,CustTypeName as [Type], [Item_Det_Ref] as Item_Ref " +
                                  ",[Itm_Det_Code] as Item_Code,[Itm_Det_Desc] as Item_Name,[Itm_Det_Stk_Unit] as Unit,[Par_Adr_Price] as DO_Price " +
                                  ",ISNULL(CONVERT(varchar, [Par_Adr_Price_Valid]),'') as Valid_Till_Date,[Par_Adr_Bonus] as Bonus_Qty " +
                                  ",ISNULL(CONVERT(varchar, [Par_Adr_Bonus_Valid]),'') as Valid_Till_Date,[Par_Price_Status] as [Status] " +
                                  ",[Par_Price_Flag] as Flag FROM [DRN].[dbo].[View_Sales_Price_Dealer] order by Dealer_Name,Item_Code";

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
                string filename = String.Format("Price_List_Dealer_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}