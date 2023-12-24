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
    public partial class frmSalesOrderRpt : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            //reportInfo();

            //reportChallanInfo();

            //btnShowRpt.Attributes.Add("onclick", "javascript:w= window.open('frmShowSalesReport.aspx');");

            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtDsmFromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDsmToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtSpDtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtSpDtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtDistDateFrm.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDistDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taSalesDsm = new View_Sales_DSMTableAdapter();
            var dtSalesDsm = taSalesDsm.GetActDsm();
            foreach (dsSalesMas.View_Sales_DSMRow dr in dtSalesDsm.Rows)
            {
                cboDsm.Items.Add(new ListItem(dr.Dsm_Short_Name.ToString() + " :: " + dr.SalesZoneName.ToString(), dr.Dsm_Ref.ToString()));
            }
            cboDsm.Items.Insert(0, new ListItem("---All---", "0"));

            var taSP = new tblSalesPersonMasTableAdapter();
            var dtSP = taSP.GetSortingData();
            foreach (dsSalesMas.tblSalesPersonMasRow dr in dtSP.Rows)
            {
                cboSp.Items.Add(new ListItem(dr.Sp_Short_Name.ToString() + " :: " + dr.Sp_Full_Name.ToString(), dr.Sp_Ref.ToString()));
            }
            cboSp.Items.Insert(0, new ListItem("---All---", "0"));

            var taDist = new tblSalesDistrictTableAdapter();
            cboCustDist.DataSource = taDist.GetDataByAsc();
            cboCustDist.DataTextField = "DistName";
            cboCustDist.DataValueField = "DistRef";
            cboCustDist.DataBind();
            cboCustDist.Items.Insert(0, new ListItem("---Select---", "0"));

            cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void optRpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optRpt.SelectedValue == "1")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Sales/Forms/wsAutoComSales.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchCustomer";
            }
            if (optRpt.SelectedValue == "2")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Sales/Forms/wsAutoComSales.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchItemListByType";
            }
            if (optRpt.SelectedValue == "3")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Sales/Forms/wsAutoComSales.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchSalesPer";
            }
            if (optRpt.SelectedValue == "5")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Sales/Forms/wsAutoComSales.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchDSM";
            }
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
            if (txtFromDate.Text.Trim().Length > 0 && txtToDate.Text.Trim().Length > 0)
            {
                #region Get Customer Ref
                var custRef = "";
                if (txtSearch.Text.Trim().Length > 0)
                {                    
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
                            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                            var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                            if (dtPartyAdr.Rows.Count > 0) custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                        }
                    }
                }
                #endregion

                #region Get Item Ref
                var itemRef = "";
                if (txtSearch.Text.Trim().Length > 0)
                {
                    var srchWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        itemRef = word;
                        break;
                    }

                    if (itemRef.Length > 0)
                    {
                        int result;
                        if (int.TryParse(itemRef, out result))
                        {
                            var taItem = new tbl_InMa_Item_DetTableAdapter();
                            var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                            if (dtItem.Rows.Count > 0) itemRef = dtItem[0].Itm_Det_Ref.ToString();
                        }
                    }
                }
                #endregion

                #region Get DSM Ref
                var dsmRef = "";
                if (txtSearch.Text.Trim().Length > 0)
                {
                    if (txtSearch.Text.Trim().Length <= 0) return;

                    var srchWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        dsmRef = word;
                        break;
                    }

                    if (dsmRef.Length > 0)
                    {
                        int result;
                        if (int.TryParse(dsmRef, out result))
                        {
                            var taDsm = new tblSalesDsmMasTableAdapter();
                            var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(dsmRef));
                            if (dtDsm.Rows.Count > 0) dsmRef = dtDsm[0].Dsm_Ref.ToString();
                        }
                    }
                }
                #endregion

                #region Get SP Ref
                var spRef = "";
                if (txtSearch.Text.Trim().Length > 0)
                {
                    var srchWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        spRef = word;
                        break;
                    }

                    if (spRef.Length > 0)
                    {
                        int result;
                        if (int.TryParse(spRef, out result))
                        {
                            var taSp = new tblSalesPersonMasTableAdapter();
                            var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(spRef));
                            if (dtSp.Rows.Count > 0) custRef = dtSp[0].Sp_Ref.ToString();
                        }
                    }
                }
                #endregion

                if (optRpt.SelectedValue == "1")
                {
                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Hdr_Pcode}='" + custRef + "'";
                    }
                    if (optRptTypeSo.SelectedValue == "1")
                        rptFile = "~/Module/Sales/Reports/rptSalesOrdByCust.rpt";
                    else
                        rptFile = "~/Module/Sales/Reports/rptSalesOrdByCustSum.rpt";
                }

                if (optRpt.SelectedValue == "2")
                {
                    if (itemRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Det_Icode}='" + itemRef + "'";
                    }
                    if (optRptTypeSo.SelectedValue == "1")
                        rptFile = "~/Module/Sales/Reports/rptSalesOrdByItem.rpt";
                    else
                        rptFile = "~/Module/Sales/Reports/rptSalesOrdByItemSum.rpt";
                }

                if (optRpt.SelectedValue == "3")
                {
                    if (spRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Hdr_Com4}='" + spRef + "'";
                    }
                    if (optRptTypeSo.SelectedValue == "1")
                        rptFile = "~/Module/Sales/Reports/rptSalesOrdBySP.rpt";
                    else
                        rptFile = "~/Module/Sales/Reports/rptSalesOrdBySpSum.rpt";
                }

                if (optRpt.SelectedValue == "4")
                {
                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Det_DO_Bal_Qty}>0";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Det_DO_Bal_Qty}>0 and {View_Sales_Details.SO_Hdr_Pcode}='" + custRef + "'";
                    }
                    if (optRptTypeSo.SelectedValue == "1")
                        rptFile = "~/Module/Sales/Reports/rptSalesOrdByPendDo.rpt";
                    else
                        rptFile = "~/Module/Sales/Reports/rptSalesOrdByPendDoSum.rpt";
                }

                if (optRpt.SelectedValue == "5")
                {
                    if (dsmRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Dsm.Dsm_Ref_Str}='" + dsmRef + "'";
                    }
                    if (optRptTypeSo.SelectedValue == "1")
                        rptFile = "~/Module/Sales/Reports/rptSalesOrdByDsm.rpt";
                    else
                        rptFile = "~/Module/Sales/Reports/rptSalesOrdByDsmSum.rpt";
                }

                Session["RptDateFrom"] = txtFromDate.Text.Trim();
                Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
        }

        protected void btnShowChlnRpt_Click(object sender, EventArgs e)
        {
            reportChallanInfo();
            var url = "frmShowSalesReport.aspx";
            //Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportChallanInfo()
        {
            if (txtFromDate.Text.Trim().Length > 0 && txtToDate.Text.Trim().Length > 0)
            {
                if (cboDsm.SelectedValue == "0")
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtDsmFromDt.Text.Trim() + "') to Date ('" + txtDsmToDt.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                }
                else
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtDsmFromDt.Text.Trim() + "') to Date ('" + txtDsmToDt.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Dsm.Dsm_Ref_Str}='" + cboDsm.SelectedValue + "'";
                }

                if (optRptType.SelectedValue.ToString() == "1")
                    rptFile = "~/Module/Sales/Reports/rptDelChlnListDsm.rpt";
                else
                    rptFile = "~/Module/Sales/Reports/rptDelChlnListDsmSum.rpt";


                Session["RptDateFrom"] = txtDsmFromDt.Text.Trim();
                Session["RptDateTo"] = txtDsmToDt.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
        }

        protected void btnShowCollectRpt_Click(object sender, EventArgs e)
        {
            reportCollectionInfo();
            var url = "frmShowSalesReport.aspx";
            //Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportCollectionInfo()
        {
            if (txtFromDate.Text == "" || txtToDate.Text == "") return;

            var qrySqlStr = "";

            qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Collection_Det]')) DROP VIEW [dbo].[View_Collection_Det]";
            dbCon.ExecuteSQLStmt(qrySqlStr);

            qrySqlStr = "CREATE view [dbo].[View_Collection_Det] as SELECT * FROM dbo.tbl_Acc_Fa_Te left outer join tblSalesPartyAdr " +
                        "on Trn_Ac_Code=Par_Adr_Acc_Code left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref WHERE (dbo.tbl_Acc_Fa_Te.Trn_Flag = 'RJV') " +
                        "and Trn_Trn_type='C' AND (dbo.tbl_Acc_Fa_Te.Trn_Narration <> 'Dummy Payment Receive') AND (CONVERT(date, dbo.tbl_Acc_Fa_Te.Trn_DATE, 103) " +
                        "BETWEEN CONVERT(date, '" + txtDsmFromDt.Text.Trim() + "', 103) AND CONVERT(date, '" + txtDsmToDt.Text.Trim() + "', 103))";
            dbCon.ExecuteSQLStmt(qrySqlStr);

            if (cboDsm.SelectedValue == "0")
            {
                rptSelcFormula = ""; //"{View_Collection_Det.Trn_DATE} in Date('" + txtDsmFromDt.Text.Trim() + "') to Date ('" + txtDsmToDt.Text.Trim() + "')";
            }
            else
            {
                rptSelcFormula = "{View_Collection_Det.Trn_DATE} in Date('" + txtDsmFromDt.Text.Trim() + "') to Date ('" + txtDsmToDt.Text.Trim() + "') " +
                    "and {View_Collection_Det.Dsm_Ref}=" + cboDsm.SelectedValue + "";
            }

            if (optSpSumDet.SelectedValue.ToString() == "1")
                rptFile = "~/Module/Sales/Reports/rptDsmWiseCollectionDet.rpt";
            else
                rptFile = "~/Module/Sales/Reports/rptDsmWiseCollectionSum.rpt";

            Session["RptDateFrom"] = txtDsmFromDt.Text.Trim();
            Session["RptDateTo"] = txtDsmToDt.Text.Trim();
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
        }

        protected void btnSp_Click(object sender, EventArgs e)
        {
            reportSpDeliveryInfo();
            var url = "frmShowSalesReport.aspx";
            //Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }


        protected void reportSpDeliveryInfo()
        {
            //var qrySqlStr = "";

            try
            {
                if (txtSpDtFrom.Text.Trim().Length <= 0 || txtSpDtTo.Text.Trim().Length <= 0) return;

                //qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_SP_Wise_Sales]')) DROP VIEW [dbo].[View_SP_Wise_Sales]";
                //dbCon.ExecuteSQLStmt(qrySqlStr);

                //qrySqlStr = "create view View_SP_Wise_Sales as SELECT [dbo].[tblSalesPersonMas].Sp_Ref, " +
                //            "SUM(CASE WHEN dbo.View_Sales_Do_Chln.Trn_Det_Icode IN ('100001', '100005') THEN dbo.View_Sales_Do_Chln.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Supreme, " +
                //            "SUM(CASE WHEN dbo.View_Sales_Do_Chln.Trn_Det_Icode IN ('100002', '100006') THEN dbo.View_Sales_Do_Chln.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Regular, " +
                //            "SUM(CASE WHEN dbo.View_Sales_Do_Chln.Trn_Det_Icode = '100003' THEN dbo.View_Sales_Do_Chln.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Eastern_P, " +
                //            "SUM(CASE WHEN dbo.View_Sales_Do_Chln.Trn_Det_Icode = '100004' THEN dbo.View_Sales_Do_Chln.Trn_Det_Lin_Qty ELSE 0 END) AS Sale_Eastern_O, " +
                //            "SUM(CASE WHEN dbo.View_Sales_Do_Chln.Trn_Det_Icode IN ('100001', '100002', '100003', '100004', '100005', '100006') THEN dbo.View_Sales_Do_Chln.Trn_Det_Lin_Qty ELSE 0 END) AS Total_Sale " +
                //            "FROM [DRN].[dbo].[View_Sales_Do_Chln] LEFT OUTER JOIN dbo.tblSalesPersonMas ON dbo.View_Sales_Do_Chln.SO_Hdr_Com4 = dbo.tblSalesPersonMas.Sp_Ref " +
                //            "WHERE (dbo.View_Sales_Do_Chln.Trn_Hdr_Code IN ('SAL', 'BNS')) AND (CONVERT(date, dbo.View_Sales_Do_Chln.Trn_Hdr_Date, 103) " +
                //            "between convert(date,'" + txtSpDtFrom.Text.Trim() + "',103) and convert(date,'" + txtSpDtTo.Text.Trim() + "',103)) " +
                //            "GROUP BY [dbo].[tblSalesPersonMas].Sp_Ref";                
                //dbCon.ExecuteSQLStmt(qrySqlStr);

                if (cboSp.SelectedValue == "0")
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtSpDtFrom.Text.Trim() + "') to Date ('" + txtSpDtTo.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                }
                else
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtSpDtFrom.Text.Trim() + "') to Date ('" + txtSpDtTo.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.SO_Hdr_Com4}='" + cboSp.SelectedValue + "'";
                }

                if (optRptType.SelectedValue.ToString() == "1")
                    rptFile = "~/Module/Sales/Reports/rptDelChlnListSpDet.rpt";
                else
                    rptFile = "~/Module/Sales/Reports/rptDelChlnListSpSum.rpt";

                Session["RptDateFrom"] = txtSpDtFrom.Text.Trim();
                Session["RptDateTo"] = txtSpDtTo.Text.Trim();
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

        protected void cboCustDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taThana = new tblSalesThanaTableAdapter();
            cboCustThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboCustDist.SelectedValue));
            cboCustThana.DataTextField = "ThanaName";
            cboCustThana.DataValueField = "ThanaRef";
            cboCustThana.DataBind();
            cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void btnShowDistRpt_Click(object sender, EventArgs e)
        {
            reportDistThanaDeliveryInfo();
            var url = "frmShowSalesReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportDistThanaDeliveryInfo()
        {
            try
            {
                if (txtDistDateFrm.Text.Trim().Length <= 0 || txtDistDateTo.Text.Trim().Length <= 0) return;

                if (cboCustDist.SelectedValue == "0")
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtDistDateFrm.Text.Trim() + "') to Date ('" + txtDistDateTo.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                }
                else
                {
                    if (cboCustThana.SelectedValue == "0")
                    {
                        rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtDistDateFrm.Text.Trim() + "') to Date ('" + txtDistDateTo.Text.Trim()
                        + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Hdr_Com5}='" + cboCustDist.SelectedValue + "'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtDistDateFrm.Text.Trim() + "') to Date ('" + txtDistDateTo.Text.Trim()
                        + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Hdr_Com6}='" + cboCustThana.SelectedValue + "'";
                    }
                }

                if (optDistThana.SelectedValue.ToString() == "1")
                    rptFile = "~/Module/Sales/Reports/rptSalesDistThanaDet.rpt";
                else
                {
                    if (optDistThanaSum.SelectedValue == "1")
                        rptFile = "~/Module/Sales/Reports/rptSalesDistThanaSumDist.rpt";
                    else
                        rptFile = "~/Module/Sales/Reports/rptSalesDistThanaSumThana.rpt";
                }

                Session["RptDateFrom"] = txtDistDateFrm.Text.Trim();
                Session["RptDateTo"] = txtDistDateTo.Text.Trim();
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

        protected void optDistThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optDistThana.SelectedValue == "1")
                optDistThanaSum.Visible = false;
            else
                optDistThanaSum.Visible = true;
        }
    }
}