using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmCsEntry : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        int i = 0;
        double totSup1 = 0;
        double[] totSumCol;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            if(Session["sessionUserId"]==null) return;

            if (Session["CsMpr"] != null || Session["CsItem"] != null)
            {                
                var mprList = Session["CsMpr"].ToString();
                var itemList = Session["CsItem"].ToString();

                SqlConnection connection = new SqlConnection();                
                var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
                connection.ConnectionString = ConnectionString;
                connection.Open();

                string qtnSupStr = "select distinct Par_Adr_Qtn_Name from tbl_Qtn_Det left outer join tbl_PuMa_Par_Adr_Qtn on Qtn_Par_Code=Par_Adr_Qtn_Ref where Qtn_Req_No in " + mprList + " and Qtn_Itm_Code in " + itemList + " and Qtn_Ref_No=''";
                SqlDataAdapter daSup = new SqlDataAdapter(qtnSupStr, connection);
                DataTable dtQtnSupData = new DataTable();
                daSup.Fill(dtQtnSupData);
                
                int r = dtQtnSupData.Rows.Count;                
                if (dtQtnSupData.Rows.Count == 0) return;

                string t = "";
                string t1 = "";
                var supName = "";
                i = 0;
                foreach (DataRow dr in dtQtnSupData.Rows)
                {
                    i = i + 1;
                    supName = dr[0].ToString();

                    t = t + "SUM(case When Par_Adr_Qtn_Name='" + supName + "' then Qtn_Itm_Rate else 0 end) as [" + supName + "],";

                    t1 = t1 + "SUM(case When Par_Adr_Qtn_Name='" + supName + "' then Qtn_Tot_Amnt else 0 end) as [" + supName + " (Total)],";
                }
                t = t + t1;
                t = t.Substring(0, t.Length - 1);

                totSumCol = new double[i];

                var qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_CS_TEMP_" + Session["sessionUserId"].ToString() + "]')) DROP VIEW [dbo].[View_CS_TEMP_" + Session["sessionUserId"].ToString() + "]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                string queryString1 = "CREATE VIEW View_CS_TEMP_" + Session["sessionUserId"].ToString() + " as select Qtn_Req_No,Qtn_Itm_Code,Qtn_Itm_Qty, " + t +                                      
                                      " from tbl_Qtn_Det left outer join tbl_PuMa_Par_Adr_Qtn on Qtn_Par_Code=Par_Adr_Qtn_Ref " +
                                      " where Qtn_Req_No in " + mprList + " and Qtn_Itm_Code in " + itemList + "and Qtn_Ref_No=''" +
                                      " group by Qtn_Req_No,Qtn_Itm_Code,Qtn_Itm_Qty";
                dbCon.ExecuteSQLStmt(queryString1);

                string queryString = "SELECT ROW_NUMBER() OVER(ORDER BY Pr_Det_Ref ASC) AS SL#, [Pr_Det_Ref] as [MPR. Ref],[Pr_Det_Icode] as [Item Code], " +
                                     "[Pr_Det_Itm_Desc] as [Item Name],[Pr_Det_Str_Code] as [Store],[Pr_Det_Spec] as [Specification],[Pr_Det_Brand] as [Brand], " +
                                     "[Pr_Det_Origin] as [Origin],[Pr_Det_Packing] as [Packing],[Pr_Det_Bat_No] as [MPR. No],[Pr_Det_Lin_Qty] as [Quantity], " +
                                     "[Pr_Det_Itm_Uom] as [Unit],View_CS_TEMP_" + Session["sessionUserId"].ToString() + ".* FROM [DRN].[dbo].[tbl_PuTr_Pr_Det] " +
                                     "Inner Join View_CS_TEMP_" + Session["sessionUserId"].ToString() + " On [Pr_Det_Ref]=[Qtn_Req_No] " +
                                     "and [Pr_Det_Icode]=[Qtn_Itm_Code] WHERE [Pr_Det_Ref] IN " + mprList + " AND [Pr_Det_Icode] IN " + itemList;

                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                DataTable dtCsData = new DataTable();
                adapter.Fill(dtCsData);
                gvCS.DataSource = dtCsData;
                gvCS.DataBind();

                var qryStr = "select Qtn_Par_Code,Par_Adr_Qtn_Name,Sum(Qtn_Tot_Amnt) as Qtn_Val from tbl_Qtn_Det " +
                             "left outer join tbl_PuMa_Par_Adr_Qtn on Qtn_Par_Code=Par_Adr_Qtn_Ref where Qtn_Req_No in " + mprList +
                             " and Qtn_Itm_Code in " + itemList + " and Qtn_Ref_No='' group by Qtn_Par_Code,Par_Adr_Qtn_Name";

                SqlDataAdapter daCsSum = new SqlDataAdapter(qryStr, connection);
                DataTable dtCsSum = new DataTable();
                daCsSum.Fill(dtCsSum);
                gvCsSum.DataSource = dtCsSum;
                gvCsSum.DataBind();
            }
        }

        protected void gvCS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 14)
            {
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;                
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int cnt = 0; cnt < i; cnt++)
                {
                    totSup1 += Convert.ToDouble(e.Row.Cells[e.Row.Cells.Count - (1 + 0)].Text);
                    totSumCol[cnt] += Convert.ToDouble(e.Row.Cells[e.Row.Cells.Count - (1 + cnt)].Text);
                    e.Row.Cells[e.Row.Cells.Count - (1 + cnt)].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[e.Row.Cells.Count - (1 + cnt)].Font.Bold = true;
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int cnt = 0; cnt < i; cnt++)
                {
                    e.Row.Cells[e.Row.Cells.Count - (1 + cnt)].Text = totSumCol[cnt].ToString("");
                    e.Row.Cells[e.Row.Cells.Count - (1 + cnt)].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[e.Row.Cells.Count - (1 + cnt)].Font.Bold = true;
                }
                e.Row.Cells[e.Row.Cells.Count - (1 + i)].Text = "Total:";
            }
        }

        protected void gvCS_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            foreach (DataControlField column in gvCS.Columns)
            {
                //Get the first Cell /Column
                TableCell cell = row.Cells[0];
                // Then Remove it after
                row.Cells.Remove(cell);
                //And Add it to the List Collections
                columns.Add(cell);
            }

            // Add cells
            row.Cells.AddRange(columns.ToArray());
        }

        protected void gvCsSum_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblQtnVal = ((Label)e.Row.FindControl("lblQtnVal")).Text.Trim();
                var txtCaryCrg = ((TextBox)e.Row.FindControl("txtCaryCrg"));
                var txtLoadCrg = ((TextBox)e.Row.FindControl("txtLoadCrg"));
                var txtDisAmt = ((TextBox)e.Row.FindControl("txtDisAmt"));
                var lblGrandTot = (Label)e.Row.FindControl("lblGrandTot");

                lblGrandTot.Text = (Convert.ToDouble(lblQtnVal) + Convert.ToDouble(txtCaryCrg.Text.Trim().Length <= 0 ? "0" : txtCaryCrg.Text.Trim()) + 
                    Convert.ToDouble(txtLoadCrg.Text.Trim().Length <= 0 ? "0" : txtLoadCrg.Text.Trim()) + 
                    Convert.ToDouble(txtDisAmt.Text.Trim().Length <= 0 ? "0" : txtDisAmt.Text.Trim())).ToString("F2");

                txtCaryCrg.Attributes.Add("onkeyup", "CalGrandTot('" + txtCaryCrg.ClientID + "', '" + txtLoadCrg.ClientID + "', '"
                    + txtDisAmt.ClientID + "', '" + lblQtnVal.ToString() + "', '" + lblGrandTot.ClientID + "' )");
                txtLoadCrg.Attributes.Add("onkeyup", "CalGrandTot('" + txtCaryCrg.ClientID + "', '" + txtLoadCrg.ClientID + "', '"
                    + txtDisAmt.ClientID + "', '" + lblQtnVal.ToString() + "', '" + lblGrandTot.ClientID + "' )");
                txtDisAmt.Attributes.Add("onkeyup", "CalGrandTot('" + txtCaryCrg.ClientID + "', '" + txtLoadCrg.ClientID + "', '"
                    + txtDisAmt.ClientID + "', '" + lblQtnVal.ToString() + "', '" + lblGrandTot.ClientID + "' )");
            }
        }

        protected void btnCsPost_Click(object sender, EventArgs e)
        {            
            string strcomments = txtComments.Text.Trim();
            string pr_priority = "";

            if (chkurgent.Checked) { pr_priority = "U"; strcomments = "[URGENT] " + strcomments; }

            bool flg = false;

            tbl_PuTr_Pr_DetTableAdapter In_Det = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetDataTable dt_srdet = new dsProcTran.tbl_PuTr_Pr_DetDataTable();

            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetDataTable qtbl = new dsProcTran.tbl_Qtn_DetDataTable();

            tbl_Tran_ComTableAdapter comm = new tbl_Tran_ComTableAdapter();

            tbl_PuTr_CS_SumTableAdapter taCsSum = new tbl_PuTr_CS_SumTableAdapter();

            string userid = "", user_name = "", desig = "", sid = "", sname = "", new_ref;
            double max_ref;

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();
            var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
            if (dtEmp.Rows.Count > 0)
            {
                userid = dtEmp[0].EmpId.ToString();
                user_name = dtEmp[0].EmpName.ToString();
                desig = dtEmp[0].DesigName.ToString();
            }            

            //var dtMaxCsRef = quo.GetMaxCsRef(DateTime.Now.Year);
            var dtMaxCsRef = In_Det.GetMaxCsRef(DateTime.Now.Year);
            max_ref = (dtMaxCsRef == null || dtMaxCsRef == "") ? 1 : Convert.ToDouble(dtMaxCsRef) + 1;
            new_ref = "CS-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + string.Format("{0:000000}", max_ref);

            strcomments = "(C/S Submited By:" + user_name + ") " + strcomments;

            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(In_Det.Connection);

            try
            {
                In_Det.AttachTransaction(myTrn);
                quo.AttachTransaction(myTrn);
                taCsSum.AttachTransaction(myTrn);

                foreach (GridViewRow gr in gvCS.Rows)
                {
                    var mpr_no = gr.Cells[1].Text.Trim();
                    var icode = gr.Cells[2].Text.Trim();

                    In_Det.UpdatePrDetStatus("QTN", new_ref, pr_priority, DateTime.Now.ToString(), mpr_no, icode, "TEN");
                    quo.UpdateQtnStatus(new_ref, mpr_no, icode, "");
                }

                int iCnt=0;
                foreach (GridViewRow gr in gvCsSum.Rows)
                {
                    iCnt++;
                    var supRef = ((Label)gr.FindControl("lblSupRef")).Text.Trim();
                    var qtnVal = ((Label)gr.FindControl("lblQtnVal")).Text.Trim();
                    var caryCrg = ((TextBox)gr.FindControl("txtCaryCrg")).Text.Trim();
                    var loadCrg = ((TextBox)gr.FindControl("txtLoadCrg")).Text.Trim();
                    var disAmt = ((TextBox)gr.FindControl("txtDisAmt")).Text.Trim();
                    var grandTot = ((Label)gr.FindControl("lblGrandTot")).Text.Trim();

                    qtnVal = qtnVal.Length <= 0 ? "0" : qtnVal;
                    caryCrg = caryCrg.Length <= 0 ? "0" : caryCrg;
                    loadCrg = loadCrg.Length <= 0 ? "0" : loadCrg;
                    disAmt = disAmt.Length <= 0 ? "0" : disAmt;
                    grandTot = grandTot.Length <= 0 ? "0" : grandTot;

                    taCsSum.InsertCsSum(new_ref, iCnt, supRef, Convert.ToDecimal(qtnVal), Convert.ToDecimal(caryCrg), Convert.ToDecimal(loadCrg),
                        Convert.ToDecimal(disAmt), Convert.ToDecimal(grandTot), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "1", "QTN");
                }
                comm.InsertTranCom(new_ref, 1, DateTime.Now, userid, user_name, desig, 1, "QTN", "RUN", strcomments, "", "1", "", "", "", "");
                
                myTrn.Commit();
                flg = true;
                lblcsref.Text = new_ref;
                ModalPopupExtender5.Show();
            }
            catch (Exception ex)
            {
                myTrn.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
            finally
            {
                GlobalClass.clsDbHelper.CloseTransaction(In_Det.Connection, myTrn);
            }

            if (flg)
            {
                try
                {
                    string msub = "", mbody = "";

                    sid = dtEmp[0].EmpOffEmail.ToString();
                    sname = dtEmp[0].EmpName.ToString();

                    msub = "A Comparative Statement created and pending for you.";
                    mbody = "A Comparative Statement (C/S) created and pending for you [" + new_ref.ToString() + "]";
                    mbody += "\nTo view details please login in at http://182.160.110.139/DRNERP";
                    mbody += "\n**THIS IS AN AUTO GENERATED EMAIL AND DOES NOT REQUIRE A REPLY.**";

                    DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail("towhid.hasan@doreen.com", "alwashib@doreen.com.bd,riaz.uddin@doreen.com.bd", sid, msub, mbody);
                }
                catch (Exception ex) 
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "E-Mail Notification Sending Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }            
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmQtnPendMpr.aspx");
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            Session["CsRefNoPrint"] = lblcsref.Text.Trim().ToString();            
            Response.Redirect("frmCsInq.aspx");        
        }
    }
}