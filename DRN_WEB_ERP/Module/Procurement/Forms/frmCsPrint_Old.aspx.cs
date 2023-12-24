using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmCsPrint_Old : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        int i = 0;
        double totSup1 = 0;
        double[] totSumCol;

        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";

            if (Page.IsPostBack) return;

            if (Session["CsRefNo"] != null)
                generate_detail_data(Session["CsRefNo"].ToString());            
        }

        private void generate_comments(string csref)
        {
            tbl_Tran_ComTableAdapter com = new tbl_Tran_ComTableAdapter();
            dsProcTran.tbl_Tran_ComDataTable dt = new dsProcTran.tbl_Tran_ComDataTable();
            View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();

            dt = com.GetDataByRefNo(csref);
            phcomments.Controls.Clear();

            var qtnSeqNo = 0;
            foreach (dsProcTran.tbl_Tran_ComRow dr in dt.Rows)
            {
                qtnSeqNo = dr.Com_Seq_no;

                Module.Procurement.Forms.UserControl.ctlQtnCom ctl = (Module.Procurement.Forms.UserControl.ctlQtnCom)LoadControl("./UserControl/CtlQtnCom.ascx");
                
                Label lblname = (Label)ctl.FindControl("lblname");
                Label lbldate = (Label)ctl.FindControl("lbldate");
                HtmlTableCell celcomm = (HtmlTableCell)ctl.FindControl("celcomm");
                Image imgimage = (Image)ctl.FindControl("imgimage");

                //imgimage.ImageUrl = "./handler/hnd_image.ashx?id=" + dr.app_id;

                //imgimage.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dr.Com_App_Id + "'";

                var dtEmp = taEmp.GetDataByEmpId(dr.Com_App_Id.ToString());

                imgimage.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + (dtEmp.Rows.Count > 0 ? dtEmp[0].EmpRefNo : "") + "'";

                ctl.ID = "ctl_" + phcomments.Controls.Count.ToString();

                lblname.Text = dr.Com_App_Name.ToString() + " (" + dr.Com_App_Desig.ToString() + ")";
                lbldate.Text = dr.Com_App_Date.ToString();
                celcomm.InnerText = dr.Com_Gen_Com.ToString();

                phcomments.Controls.Add(ctl);
            }
        }

        private void generate_signature(string ref_no)
        {
            tbl_Tran_ComTableAdapter com = new tbl_Tran_ComTableAdapter();
            dsProcTran.tbl_Tran_ComDataTable dt = new dsProcTran.tbl_Tran_ComDataTable();            

            dt = com.GetDataByRefNo(ref_no);                        
            foreach (dsProcTran.tbl_Tran_ComRow dr in dt.Rows)
            {
                if (dr.Com_Seq_no.ToString() == "1") lblPepBy.Text = dr.Com_App_Name.ToString();
                if (dr.Com_Seq_no.ToString() == "2") lblVerBy.Text = dr.Com_App_Name.ToString();
            }
        }

        private void generate_detail_data(string csref)
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
                connection.ConnectionString = ConnectionString;
                connection.Open();

                string MprQryStr = "select distinct Pr_Det_Ref from tbl_PuTr_Pr_Det where Pr_Det_Quot_Ref='" + csref.ToString() + "'";
                SqlDataAdapter daMpr = new SqlDataAdapter(MprQryStr, connection);
                DataTable dtMprData = new DataTable();
                daMpr.Fill(dtMprData);
                
                var mprRef = "";
                foreach (DataRow dr in dtMprData.Rows)
                {                                        
                    mprRef = mprRef + dr[0].ToString() + ", ";                    
                }
                mprRef = mprRef.Substring(0, mprRef.Length - 2);
                lblmpr.Text = mprRef.ToString();
                lblcsref.Text = csref.ToString();

                string qtnSupStr = "select distinct Par_Adr_Qtn_Name from tbl_Qtn_Det left outer join tbl_PuMa_Par_Adr_Qtn on Qtn_Par_Code=Par_Adr_Qtn_Ref where Qtn_Ref_No='" + csref.ToString() + "'";
                SqlDataAdapter daSup = new SqlDataAdapter(qtnSupStr, connection);
                DataTable dtQtnSupData = new DataTable();
                daSup.Fill(dtQtnSupData);

                int r = dtQtnSupData.Rows.Count;
                if (dtQtnSupData.Rows.Count == 0) return;

                string t = "";
                string t1 = "";
                var supName = "";
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

                var qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_CS_APP_TEMP_" + Session["sessionUserId"].ToString() + "]')) DROP VIEW [dbo].[View_CS_APP_TEMP_" + Session["sessionUserId"].ToString() + "]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                string queryString1 = "CREATE VIEW View_CS_APP_TEMP_" + Session["sessionUserId"].ToString() + " as select Qtn_Req_No,Qtn_Itm_Code,Qtn_Itm_Qty, " + t +
                                      " from tbl_Qtn_Det left outer join tbl_PuMa_Par_Adr_Qtn on Qtn_Par_Code=Par_Adr_Qtn_Ref " +
                                      " where Qtn_Ref_No='" + csref.ToString() + "' group by Qtn_Req_No,Qtn_Itm_Code,Qtn_Itm_Qty";
                dbCon.ExecuteSQLStmt(queryString1);

                string queryString = "SELECT ROW_NUMBER() OVER(ORDER BY Pr_Det_Ref ASC) AS SL#, [Pr_Det_Ref] as [MPR. Ref],[Pr_Det_Icode] as [Item Code], " +
                                     "[Pr_Det_Itm_Desc] as [Item Name],[Pr_Det_Str_Code] as [Store],[Pr_Det_Spec] as [Specification],[Pr_Det_Brand] as [Brand], " +
                                     "[Pr_Det_Origin] as [Origin],[Pr_Det_Packing] as [Packing],[Pr_Det_Bat_No] as [MPR. No],[Pr_Det_Lin_Qty] as [Quantity], " +
                                     "[Pr_Det_Itm_Uom] as [Unit],View_CS_APP_TEMP_" + Session["sessionUserId"].ToString() + ".* FROM [DRN].[dbo].[tbl_PuTr_Pr_Det] " +
                                     "Left Outer Join View_CS_APP_TEMP_" + Session["sessionUserId"].ToString() + " On [Pr_Det_Ref]=[Qtn_Req_No] " +
                                     "and [Pr_Det_Icode]=[Qtn_Itm_Code] WHERE [Pr_Det_Quot_Ref]='" + csref.ToString() + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                DataTable dtCsData = new DataTable();
                adapter.Fill(dtCsData);
                gvCS.DataSource = dtCsData;
                gvCS.DataBind();

                var qryStr = "select Qtn_Par_Code,Par_Adr_Qtn_Name,Sum(Qtn_Tot_Amnt) as Qtn_Val from tbl_Qtn_Det " +
                             "left outer join tbl_PuMa_Par_Adr_Qtn on Qtn_Par_Code=Par_Adr_Qtn_Ref where Qtn_Ref_No='" + csref.ToString() +
                             "' group by Qtn_Par_Code,Par_Adr_Qtn_Name";

                SqlDataAdapter daCsSum = new SqlDataAdapter(qryStr, connection);
                DataTable dtCsSum = new DataTable();
                daCsSum.Fill(dtCsSum);
                gvCsSum.DataSource = dtCsSum;
                gvCsSum.DataBind();

                generate_comments(csref);
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvCS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var taPrDet = new tbl_PuTr_Pr_DetTableAdapter();

            if (e.Row.Cells.Count >= 14)
            {
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblAuditChk = (Label)e.Row.FindControl("lblAuditChk");

                var mpr = e.Row.Cells[1].Text.Trim();
                var itm = e.Row.Cells[2].Text.Trim();

                var dtPrDet = taPrDet.GetDataByPrRefItem(mpr, itm);
                if (dtPrDet.Rows.Count > 0)
                {
                    if (dtPrDet[0].Pr_Det_Status == "APR" || dtPrDet[0].Pr_Det_Status == "APP" || dtPrDet[0].Pr_Det_Status == "POC")                    
                        lblAuditChk.Text = "Ok";                    
                    else                    
                        lblAuditChk.Text = "Pending";                    
                }
                else                
                    lblAuditChk.Text = "";

                for (int cnt = 0; cnt < i; cnt++)
                {
                    totSup1 += Convert.ToDouble(e.Row.Cells[e.Row.Cells.Count - (2 + 0)].Text);
                    totSumCol[cnt] += Convert.ToDouble(e.Row.Cells[e.Row.Cells.Count - (2 + cnt)].Text);
                    e.Row.Cells[e.Row.Cells.Count - (2 + cnt)].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[e.Row.Cells.Count - (2 + cnt)].Font.Bold = true;
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int cnt = 0; cnt < i; cnt++)
                {
                    e.Row.Cells[e.Row.Cells.Count - (2 + cnt)].Text = totSumCol[cnt].ToString("");
                    e.Row.Cells[e.Row.Cells.Count - (2 + cnt)].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[e.Row.Cells.Count - (2 + cnt)].Font.Bold = true;
                }
                e.Row.Cells[e.Row.Cells.Count - (2 + i)].Text = "Total:";
                e.Row.Cells[e.Row.Cells.Count - (2 + i)].Font.Bold = true;
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
                var hfSuppRef = ((HiddenField)e.Row.FindControl("hfSuppRef")).Value.Trim();
                var lblQtnVal = ((Label)e.Row.FindControl("lblQtnVal")).Text.Trim();
                var lblCaryCrg = ((Label)e.Row.FindControl("lblCaryCrg"));
                var lblLoadCrg = ((Label)e.Row.FindControl("lblLoadCrg"));
                var lblDisAmt = ((Label)e.Row.FindControl("lblDisAmt"));
                var lblGrandTot = (Label)e.Row.FindControl("lblGrandTot");

                var taCsSum = new tbl_PuTr_CS_SumTableAdapter();
                var dtCsSum = taCsSum.GetDataByQtnPartyRef(lblcsref.Text.Trim().ToString(), hfSuppRef.ToString(), "APP");
                if (dtCsSum.Rows.Count > 0)
                {
                    lblCaryCrg.Text = dtCsSum[0].CS_Cary_Amt.ToString("N2");
                    lblLoadCrg.Text = dtCsSum[0].CS_Load_Amt.ToString("N2");
                    lblDisAmt.Text = dtCsSum[0].CS_Disc_Amt.ToString("N2");
                    //lblGrandTot.Text = dtCsSum[0].CS_Tot_Amt.ToString();
                }
                lblGrandTot.Text = ((Convert.ToDouble(lblQtnVal) + Convert.ToDouble(lblCaryCrg.Text.Trim().Length <= 0 ? "0" : lblCaryCrg.Text.Trim()) +
                    Convert.ToDouble(lblLoadCrg.Text.Trim().Length <= 0 ? "0" : lblLoadCrg.Text.Trim())) -
                    Convert.ToDouble(lblDisAmt.Text.Trim().Length <= 0 ? "0" : lblDisAmt.Text.Trim())).ToString("F2");

            }
        }                
    }
}