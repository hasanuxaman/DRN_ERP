using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransDetTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmUploadWeighbridgeData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            string connectionString = "";
            if (FileUpload1.HasFile)
            {
                //Upload and save the file
                //string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                //FileUpload1.SaveAs(excelPath);

                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string fileLocation = Server.MapPath("~/Upload_Files/" + fileName);
                FileUpload1.SaveAs(fileLocation);

                //Check whether file extension is xls or xslx               
                if (fileExtension == ".xls")
                {
                    //connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + @";Extended Properties=" + Convert.ToChar(34).ToString() + @"Excel 8.0;Imex=1;HDR=Yes;" + Convert.ToChar(34).ToString();
                }
                else if (fileExtension == ".xlsx")
                {
                  //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=" + Convert.ToChar(34).ToString() + @"Excel 12.0;Imex=1;HDR=Yes;" + Convert.ToChar(34).ToString();
                }

                //Create OleDB Connection and OleDb Command

                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                DataTable dtExcelRecords = new DataTable();
                con.Open();
                DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                dAdapter.SelectCommand = cmd;
                dAdapter.Fill(dtExcelRecords);
                con.Close();
                gvWeighbridgeData.DataSource = dtExcelRecords;
                gvWeighbridgeData.DataBind();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            var taWeightData = new tbl_Weighbridge_DataTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taWeightData.Connection);
            try
            {
                taWeightData.AttachTransaction(myTran);

                int chk = 0;
                int chk1 = 0;
                foreach (GridViewRow gr in gvWeighbridgeData.Rows)
                {
                    if (gr.Cells[0].Text.Trim() == "Weight Id")
                    {
                        chk = 1;
                        continue;
                    }
                    if (chk == 1)
                    {
                        if (gr.Cells[0].Text.Trim() == "&nbsp;")
                            continue;
                        else
                        {
                            var WgtId = gr.Cells[0].Text.Trim().Replace(",", "");

                            var dtWeightData = taWeightData.GetDataByWeightIdDate(WgtId.ToString(), gr.Cells[7].Text.Trim());
                            if (dtWeightData.Rows.Count > 0)
                            {
                                taWeightData.UpdateWeight(gr.Cells[1].Text.Trim(), gr.Cells[2].Text.Trim() == "&nbsp;" ? 0 : Convert.ToDecimal(gr.Cells[2].Text.Trim()),
                                    gr.Cells[3].Text.Trim(), Convert.ToDecimal(gr.Cells[4].Text.Trim()), Convert.ToDecimal(gr.Cells[5].Text.Trim()),
                                    Convert.ToDecimal(gr.Cells[6].Text.Trim()), "1", "", DateTime.Now,
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), WgtId.ToString(), gr.Cells[7].Text.Trim());
                                chk1 = 1;
                            }
                            else
                            {
                                taWeightData.InsertWeight(WgtId.ToString(), gr.Cells[1].Text.Trim(), gr.Cells[2].Text.Trim() == "&nbsp;" ? 0 : Convert.ToDecimal(gr.Cells[2].Text.Trim()),
                                    gr.Cells[3].Text.Trim(), Convert.ToDecimal(gr.Cells[4].Text.Trim()), Convert.ToDecimal(gr.Cells[5].Text.Trim()),
                                    Convert.ToDecimal(gr.Cells[6].Text.Trim()), gr.Cells[7].Text.Trim(), "1", "",
                                    DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString());
                                chk1 = 1;
                            }
                        }
                    }                    
                }
                if (chk1 == 1)
                {
                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Uploaded Successsfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}