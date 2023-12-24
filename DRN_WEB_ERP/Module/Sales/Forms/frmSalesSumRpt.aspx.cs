using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesSumRpt : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            //reportInfo();
            //reportInfoCollect();

            //btnShowRpt.Attributes.Add("onclick", "javascript:w= window.open('frmShowSalesReport.aspx');");

            if (Page.IsPostBack) return;

            //DateTime today = DateTime.Today;
            //int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
            //DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
            //DateTime endOfMonth = new DateTime(today.Year, today.Month, daysInMonth);

            txtFromDate.Text = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, 1).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");


            var taSalesDsm = new View_Sales_DSMTableAdapter();
            var dtSalesDsm = taSalesDsm.GetActDsm();
            foreach (dsSalesMas.View_Sales_DSMRow dr in dtSalesDsm.Rows)
            {
                cboDsm.Items.Add(new ListItem(dr.SalesZoneName.ToString() + " :: " + dr.Dsm_Short_Name.ToString(), dr.Dsm_Sls_Zone.ToString()));
            }
            cboDsm.Items.Insert(0, new ListItem("---All---", "0"));
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowSalesReport.aspx";
            //Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
        {
            var qrySqlStr = "";

            try
            {
                if (txtFromDate.Text == "" || txtToDate.Text == "") return;

                DateTime fromDate = Convert.ToDateTime(txtToDate.Text.Trim());
                int daysInMonth = DateTime.DaysInMonth(fromDate.Year, fromDate.Month);
                DateTime startOfMonth = new DateTime(Convert.ToDateTime(txtToDate.Text.Trim()).Year, Convert.ToDateTime(txtToDate.Text.Trim()).Month, 1);
                DateTime endOfMonth = new DateTime(Convert.ToDateTime(txtToDate.Text.Trim()).Year, Convert.ToDateTime(txtToDate.Text.Trim()).Month, daysInMonth);

                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Dsm_Wise_Sales]')) DROP VIEW [dbo].[View_Dsm_Wise_Sales]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "CREATE view [dbo].[View_Dsm_Wise_Sales] as SELECT dbo.tblSalesDsmMas.Dsm_Ref, " +
                      "SUM(CASE WHEN Trn_Det_Icode in ('100001','100005') THEN dbo.tbl_InTr_Trn_Det.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Supreme, " +
                      "SUM(CASE WHEN Trn_Det_Icode in ('100002','100006') THEN dbo.tbl_InTr_Trn_Det.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Regular, " +
                      "SUM(CASE WHEN Trn_Det_Icode = '100003' THEN dbo.tbl_InTr_Trn_Det.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Eastern_P, " +
                      "SUM(CASE WHEN Trn_Det_Icode = '100004' THEN dbo.tbl_InTr_Trn_Det.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Eastern_O, " +
                      "SUM(CASE WHEN Trn_Det_Icode IN ('100001', '100002', '100003', '100004','100005','100006') THEN dbo.tbl_InTr_Trn_Det.Trn_Det_Lin_Qty ELSE 0 END) AS Total_Sale " +
                      "FROM dbo.tbl_InTr_Trn_Hdr LEFT OUTER JOIN dbo.tbl_InTr_Trn_Det ON dbo.tbl_InTr_Trn_Hdr.Trn_Hdr_Ref = dbo.tbl_InTr_Trn_Det.Trn_Hdr_Ref " +
                      "LEFT OUTER JOIN dbo.tblSalesPartyAdr ON dbo.tbl_InTr_Trn_Hdr.Trn_Hdr_Pcode = dbo.tblSalesPartyAdr.Par_Adr_Ref LEFT OUTER JOIN " +
                      "dbo.tblSalesDsmMas ON dbo.tblSalesPartyAdr.Par_Adr_Sls_Per = dbo.tblSalesDsmMas.Dsm_Ref " +
                      "WHERE Trn_Hdr_Code in ('SAL','BNS') AND convert(date,Trn_Hdr_Date,103) between convert(date,'" + startOfMonth.ToString("dd/MM/yyyy") + "',103) " +
                      "and convert(date,'" + endOfMonth.ToString("dd/MM/yyyy") + "',103) and dbo.tblSalesDsmMas.Dsm_Is_Active = '1' GROUP BY dbo.tblSalesDsmMas.Dsm_Ref";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Collection_Det]')) DROP VIEW [dbo].[View_Collection_Det]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "CREATE view [dbo].[View_Collection_Det] as SELECT * FROM dbo.tbl_Acc_Fa_Te left outer join tblSalesPartyAdr " +
                            "on Trn_Ac_Code=Par_Adr_Acc_Code left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref WHERE (dbo.tbl_Acc_Fa_Te.Trn_Flag = 'RJV') " +
                            "and Trn_Trn_type='C' AND (dbo.tbl_Acc_Fa_Te.Trn_Narration <> 'Dummy Payment Receive') AND (CONVERT(date, dbo.tbl_Acc_Fa_Te.Trn_DATE, 103) " +
                            "BETWEEN CONVERT(date, '" + startOfMonth.ToString("dd/MM/yyyy") + "', 103) AND CONVERT(date, '" + endOfMonth.ToString("dd/MM/yyyy") + "', 103)) " +
                            "and dbo.tblSalesDsmMas.Dsm_Is_Active = '1'";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Dsm_Wise_Collection]')) DROP VIEW [dbo].[View_Dsm_Wise_Collection]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "CREATE view [dbo].[View_Dsm_Wise_Collection] as SELECT dbo.View_Collection_Det.Dsm_Ref, " +
                            "SUM(CASE Trn_Trn_type WHEN 'C' THEN Trn_Amount ELSE 0 END) AS CollectionAmount " +
                            "FROM dbo.View_Collection_Det WHERE CONVERT(date, dbo.View_Collection_Det.Trn_DATE, 103) " +
                            "between convert(date,'" + startOfMonth.ToString("dd/MM/yyyy") + "',103) and convert(date,'" + endOfMonth.ToString("dd/MM/yyyy") + "',103) " +
                            "GROUP BY dbo.View_Collection_Det.Dsm_Ref ";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Dsm_Wise_Target]')) DROP VIEW [dbo].[View_Dsm_Wise_Target]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "CREATE view [dbo].[View_Dsm_Wise_Target] as SELECT Trgt_Dsm_Ref, " +
                      "SUM(CASE WHEN Trgt_Item_Ref = '100001' THEN Trgt_Qty ELSE 0 END) AS Target_Supreme, " +
                      "SUM(CASE WHEN Trgt_Item_Ref = '100002' THEN Trgt_Qty ELSE 0 END) AS Target_Regular, " +
                      "SUM(CASE WHEN Trgt_Item_Ref = '100003' THEN Trgt_Qty ELSE 0 END) AS Target_Eastern_P, " +
                      "SUM(CASE WHEN Trgt_Item_Ref = '100004' THEN Trgt_Qty ELSE 0 END) AS Target_Eastern_O, " +
                      "SUM(CASE WHEN Trgt_Item_Ref IN ('100001','100002','100003','100004') THEN Trgt_Qty ELSE 0 END) as Total_Vol, " +
                      "Round(SUM(CASE WHEN Trgt_Item_Ref IN ('100001','100002','100003','100004') THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) as Terget_Per_Day, " +
                      "Round(Round(SUM(CASE WHEN Trgt_Item_Ref IN ('100001','100002','100003','100004') THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) * " + Convert.ToDateTime(txtToDate.Text).Day + ",2) AS Proj_Target_Till_Today, " +
                      "SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END) AS Target_Collection, " +
                      "Round(SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) as Trgt_Collct_Per_Day, " +
                      "Round(Round(SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) * " + Convert.ToDateTime(txtToDate.Text).Day + ",2) AS Proj_Target_Collect_Till_Today " +
                      "FROM dbo.tblSalesTarget WHERE convert(date,Trgt_From_Dt,103) >= convert(date,'" + startOfMonth.ToString("dd/MM/yyyy") + "',103) " +
                      "and convert(date,Trgt_To_Dt,103)<= convert(date,'" + endOfMonth.ToString("dd/MM/yyyy") + "',103) GROUP BY Trgt_Dsm_Ref";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                if (cboDsm.SelectedIndex == 0)
                    rptSelcFormula = "";
                else
                    rptSelcFormula = "{View_Dsm_Wise_Trgt_Vs_Sls.Dsm_Sls_Zone}='" + cboDsm.SelectedValue.ToString() + "'";

                rptFile = "~/Module/Sales/Reports/rptDsmTrgtVsSales_Collect.rpt";

                //Session["RptDateFrom"] = txtFromDate.Text.Trim();
                //Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptDateFrom"] = startOfMonth.ToString("dd/MM/yyyy");
                Session["RptDateTo"] = endOfMonth.ToString("dd/MM/yyyy");
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void reportInfoSp()
        {
            var qrySqlStr = "";

            try
            {
                if (txtFromDate.Text == "" || txtToDate.Text == "") return;

                DateTime fromDate = Convert.ToDateTime(txtToDate.Text.Trim());
                int daysInMonth = DateTime.DaysInMonth(fromDate.Year, fromDate.Month);
                DateTime startOfMonth = new DateTime(Convert.ToDateTime(txtToDate.Text.Trim()).Year, Convert.ToDateTime(txtToDate.Text.Trim()).Month, 1);
                DateTime endOfMonth = new DateTime(Convert.ToDateTime(txtToDate.Text.Trim()).Year, Convert.ToDateTime(txtToDate.Text.Trim()).Month, daysInMonth);

                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Sp_Wise_Sales]')) DROP VIEW [dbo].[View_Sp_Wise_Sales]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "create view View_Sp_Wise_Sales as SELECT [dbo].[tblSalesPersonMas].Sp_Ref, " +
                            "SUM(CASE WHEN dbo.View_Sales_Do_Chln.Trn_Det_Icode IN ('100001', '100005') THEN dbo.View_Sales_Do_Chln.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Supreme, " +
                            "SUM(CASE WHEN dbo.View_Sales_Do_Chln.Trn_Det_Icode IN ('100002', '100006') THEN dbo.View_Sales_Do_Chln.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Regular, " +
                            "SUM(CASE WHEN dbo.View_Sales_Do_Chln.Trn_Det_Icode = '100003' THEN dbo.View_Sales_Do_Chln.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Eastern_P, " +
                            "SUM(CASE WHEN dbo.View_Sales_Do_Chln.Trn_Det_Icode = '100004' THEN dbo.View_Sales_Do_Chln.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Eastern_O, " +
                            "SUM(CASE WHEN dbo.View_Sales_Do_Chln.Trn_Det_Icode IN ('100001', '100002', '100003', '100004', '100005', '100006') THEN dbo.View_Sales_Do_Chln.Trn_Det_Lin_Qty ELSE 0 END) AS Total_Sale " +
                            "FROM [DRN].[dbo].[View_Sales_Do_Chln] LEFT OUTER JOIN dbo.tblSalesPersonMas ON dbo.View_Sales_Do_Chln.SO_Hdr_Com4 = dbo.tblSalesPersonMas.Sp_Ref " +
                            "WHERE (dbo.View_Sales_Do_Chln.Trn_Hdr_Code IN ('SAL', 'BNS')) AND (CONVERT(date, dbo.View_Sales_Do_Chln.Trn_Hdr_Date, 103) " +
                            "between convert(date,'" + startOfMonth.ToString("dd/MM/yyyy") + "',103) and convert(date,'" + endOfMonth.ToString("dd/MM/yyyy") + "',103)) " +
                            "GROUP BY [dbo].[tblSalesPersonMas].Sp_Ref";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                //qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Collection_Det]')) DROP VIEW [dbo].[View_Collection_Det]";
                //dbCon.ExecuteSQLStmt(qrySqlStr);

                //qrySqlStr = "CREATE view [dbo].[View_Collection_Det] as SELECT * FROM dbo.tbl_Acc_Fa_Te left outer join tblSalesPartyAdr " +
                //            "on Trn_Ac_Code=Par_Adr_Acc_Code left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref WHERE (dbo.tbl_Acc_Fa_Te.Trn_Flag = 'RJV') " +
                //            "and Trn_Trn_type='C' AND (dbo.tbl_Acc_Fa_Te.Trn_Narration <> 'Dummy Payment Receive') AND (CONVERT(date, dbo.tbl_Acc_Fa_Te.Trn_DATE, 103) " +
                //            "BETWEEN CONVERT(date, '" + txtFromDate.Text.Trim() + "', 103) AND CONVERT(date, '" + txtToDate.Text.Trim() + "', 103))";
                //dbCon.ExecuteSQLStmt(qrySqlStr);

                //qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Dsm_Wise_Collection]')) DROP VIEW [dbo].[View_Dsm_Wise_Collection]";
                //dbCon.ExecuteSQLStmt(qrySqlStr);

                //qrySqlStr = "CREATE view [dbo].[View_Dsm_Wise_Collection] as SELECT dbo.View_Collection_Det.Dsm_Ref, " +
                //            "SUM(CASE Trn_Trn_type WHEN 'C' THEN Trn_Amount ELSE 0 END) AS CollectionAmount " +
                //            "FROM dbo.View_Collection_Det WHERE CONVERT(date, dbo.View_Collection_Det.Trn_DATE, 103) " +
                //            "between convert(date,'" + txtFromDate.Text.Trim() + "',103) and convert(date,'" + txtToDate.Text.Trim() + "',103) " +
                //            "GROUP BY dbo.View_Collection_Det.Dsm_Ref ";
                //dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Sp_Wise_Target]')) DROP VIEW [dbo].[View_Sp_Wise_Target]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "CREATE view [dbo].[View_Sp_Wise_Target] as SELECT Trgt_Sp_Ref, " +
                      "SUM(CASE WHEN Trgt_Item_Ref = '100001' THEN Trgt_Qty ELSE 0 END) AS Target_Supreme, " +
                      "SUM(CASE WHEN Trgt_Item_Ref = '100002' THEN Trgt_Qty ELSE 0 END) AS Target_Regular, " +
                      "SUM(CASE WHEN Trgt_Item_Ref = '100003' THEN Trgt_Qty ELSE 0 END) AS Target_Eastern_P, " +
                      "SUM(CASE WHEN Trgt_Item_Ref = '100004' THEN Trgt_Qty ELSE 0 END) AS Target_Eastern_O, " +
                      "SUM(CASE WHEN Trgt_Item_Ref IN ('100001','100002','100003','100004') THEN Trgt_Qty ELSE 0 END) as Total_Vol, " +
                      "Round(SUM(CASE WHEN Trgt_Item_Ref IN ('100001','100002','100003','100004') THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) as Terget_Per_Day, " +
                      "Round(Round(SUM(CASE WHEN Trgt_Item_Ref IN ('100001','100002','100003','100004') THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) * " + Convert.ToDateTime(txtToDate.Text).Day + ",2) AS Proj_Target_Till_Today, " +
                      "SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END) AS Target_Collection, " +
                      "Round(SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) as Trgt_Collct_Per_Day, " +
                      "Round(Round(SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) * " + Convert.ToDateTime(txtToDate.Text).Day + ",2) AS Proj_Target_Collect_Till_Today " +
                      "FROM dbo.tblSalesTargetSp WHERE convert(date,Trgt_From_Dt,103) >= convert(date,'" + startOfMonth.ToString("dd/MM/yyyy") + "',103) " +
                      "and convert(date,Trgt_To_Dt,103)<= convert(date,'" + endOfMonth.ToString("dd/MM/yyyy") + "',103) GROUP BY Trgt_Sp_Ref";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                if (cboDsm.SelectedIndex == 0)
                    rptSelcFormula = "";
                else
                    rptSelcFormula = "{View_Dsm_Wise_Trgt_Vs_Sls.Dsm_Sls_Zone}='" + cboDsm.SelectedValue.ToString() + "'";

                rptFile = "~/Module/Sales/Reports/rptTrgtVsSalesSp.rpt";

                //Session["RptDateFrom"] = txtFromDate.Text.Trim();
                //Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptDateFrom"] = startOfMonth.ToString("dd/MM/yyyy");
                Session["RptDateTo"] = endOfMonth.ToString("dd/MM/yyyy");
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnShowRptSp_Click(object sender, EventArgs e)
        {
            reportInfoSp();
            var url = "frmShowSalesReport.aspx";
            //Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        //protected void btnShowCollectRpt_Click(object sender, EventArgs e)
        //{
        //    reportInfoCollect();
        //    var url = "frmShowSalesReport.aspx";
        //    //Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
        //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        //}

        //protected void reportInfoCollect()
        //{
        //    var qrySqlStr = "";

        //    try
        //    {
        //        if (txtFromDate.Text == "" || txtToDate.Text == "") return;

        //        qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Dsm_Wise_Sales]')) DROP VIEW [dbo].[View_Dsm_Wise_Sales]";
        //        dbCon.ExecuteSQLStmt(qrySqlStr);

        //        qrySqlStr = "CREATE view [dbo].[View_Dsm_Wise_Sales] as SELECT dbo.tblSalesDsmMas.Dsm_Ref, " +
        //              "SUM(CASE WHEN Trn_Det_Icode = '100001' THEN dbo.tbl_InTr_Trn_Det.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Supreme, " +
        //              "SUM(CASE WHEN Trn_Det_Icode = '100002' THEN dbo.tbl_InTr_Trn_Det.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Regular, " +
        //              "SUM(CASE WHEN Trn_Det_Icode = '100003' THEN dbo.tbl_InTr_Trn_Det.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Eastern_P, " +
        //              "SUM(CASE WHEN Trn_Det_Icode = '100004' THEN dbo.tbl_InTr_Trn_Det.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Eastern_O, " +
        //              "SUM(CASE WHEN Trn_Det_Icode IN ('100001', '100002', '100003', '100004') THEN dbo.tbl_InTr_Trn_Det.Trn_Det_Lin_Qty ELSE 0 END) AS Total_Sale " +
        //              "FROM dbo.tbl_InTr_Trn_Hdr LEFT OUTER JOIN dbo.tbl_InTr_Trn_Det ON dbo.tbl_InTr_Trn_Hdr.Trn_Hdr_Ref = dbo.tbl_InTr_Trn_Det.Trn_Hdr_Ref " +
        //              "LEFT OUTER JOIN dbo.tblSalesPartyAdr ON dbo.tbl_InTr_Trn_Hdr.Trn_Hdr_Pcode = dbo.tblSalesPartyAdr.Par_Adr_Ref LEFT OUTER JOIN " +
        //              "dbo.tblSalesDsmMas ON dbo.tblSalesPartyAdr.Par_Adr_Sls_Per = dbo.tblSalesDsmMas.Dsm_Ref " +
        //              "WHERE Trn_Det_Code in ('SAL','BNS') AND convert(date,Trn_Hdr_Date,103) between convert(date,'" + txtFromDate.Text.Trim() + "',103) " +
        //              "and convert(date,'" + txtToDate.Text.Trim() + "',103) GROUP BY dbo.tblSalesDsmMas.Dsm_Ref";
        //        dbCon.ExecuteSQLStmt(qrySqlStr);

        //        qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Dsm_Wise_Collection]')) DROP VIEW [dbo].[View_Dsm_Wise_Collection]";
        //        dbCon.ExecuteSQLStmt(qrySqlStr);

        //        qrySqlStr = "CREATE view [dbo].[View_Dsm_Wise_Collection] as select Dsm_Ref,Sum(CASE Trn_Trn_type WHEN 'C' THEN Trn_Amount ELSE 0 END) AS CollectionAmount " +
        //                    "from tbl_Acc_Fa_Te left outer join tblSalesPartyAdr on Trn_Ac_Code=Par_Adr_Acc_Code left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref " +
        //                    "where Trn_Flag='RJV' and Trn_Narration<>'Dummy Payment Receive' " +
        //                    "and convert(date,Trn_DATE,103) between convert(date,'" + txtFromDate.Text.Trim() + "',103) " +
        //                    "and convert(date,'" + txtToDate.Text.Trim() + "',103) group by Dsm_Ref ";
        //        dbCon.ExecuteSQLStmt(qrySqlStr);

        //        DateTime fromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
        //        int daysInMonth = DateTime.DaysInMonth(fromDate.Year, fromDate.Month);
        //        DateTime endOfMonth = new DateTime(Convert.ToDateTime(txtToDate.Text.Trim()).Year, Convert.ToDateTime(txtToDate.Text.Trim()).Month, daysInMonth);

        //        qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Dsm_Wise_Target]')) DROP VIEW [dbo].[View_Dsm_Wise_Target]";
        //        dbCon.ExecuteSQLStmt(qrySqlStr);

        //        qrySqlStr = "CREATE view [dbo].[View_Dsm_Wise_Target] as SELECT Trgt_Dsm_Ref, " +
        //              "SUM(CASE WHEN Trgt_Item_Ref = '100001' THEN Trgt_Qty ELSE 0 END) AS Target_Supreme, " +
        //              "SUM(CASE WHEN Trgt_Item_Ref = '100002' THEN Trgt_Qty ELSE 0 END) AS Target_Regular, " +
        //              "SUM(CASE WHEN Trgt_Item_Ref = '100003' THEN Trgt_Qty ELSE 0 END) AS Target_Eastern_P, " +
        //              "SUM(CASE WHEN Trgt_Item_Ref = '100004' THEN Trgt_Qty ELSE 0 END) AS Target_Eastern_O, " +
        //              "SUM(CASE WHEN Trgt_Item_Ref IN ('100001','100002','100003','100004') THEN Trgt_Qty ELSE 0 END) as Total_Vol, " +
        //              "Round(SUM(CASE WHEN Trgt_Item_Ref IN ('100001','100002','100003','100004') THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) as Terget_Per_Day, " +
        //              "Round(Round(SUM(CASE WHEN Trgt_Item_Ref IN ('100001','100002','100003','100004') THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) * " + Convert.ToDateTime(txtToDate.Text).Day + ",2) AS Proj_Target_Till_Today, " +
        //              "SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END) AS Target_Collection, " +
        //              "Round(SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) as Trgt_Collct_Per_Day, " +
        //              "Round(Round(SUM(CASE WHEN Trgt_Item_Ref = '100000' THEN Trgt_Qty ELSE 0 END)/convert(int," + daysInMonth + "),2) * " + Convert.ToDateTime(txtToDate.Text).Day + ",2) AS Proj_Target_Collect_Till_Today " +
        //              "FROM dbo.tblSalesTarget WHERE convert(date,Trgt_From_Dt,103) >= convert(date,'" + txtFromDate.Text.Trim() + "',103) " +
        //              "and convert(date,Trgt_To_Dt,103)<= convert(date,'" + endOfMonth.ToString("dd/MM/yyyy") + "',103) GROUP BY Trgt_Dsm_Ref";
        //        dbCon.ExecuteSQLStmt(qrySqlStr);

        //        //rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
        //        //+ "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Det_DO_Bal_Qty}>0";

        //        rptSelcFormula = "";

        //        rptFile = "~/Module/Sales/Reports/rptDsmTrgtVsSales_Collect.rpt";

        //        Session["RptDateFrom"] = txtFromDate.Text.Trim();
        //        Session["RptDateTo"] = txtToDate.Text.Trim();
        //        Session["RptFilePath"] = rptFile;
        //        Session["RptFormula"] = rptSelcFormula;
        //    }
        //    catch (Exception ex)
        //    {
        //        tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
        //        tblMsg.Rows[1].Cells[0].InnerText = "";
        //        ModalPopupExtenderMsg.Show();
        //    }
        //}
    }
}