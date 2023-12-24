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
    public partial class frmDeliveryChallanRptGhat : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            reportInfo();

            //btnShowRpt.Attributes.Add("onclick", "javascript:w= window.open('frmShowSalesReport.aspx');");

            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taStore = new tbl_InMa_Str_LocTableAdapter();
            var dtStore = taStore.GetDataByStoreGroup("FIN");
            foreach (dsInvMas.tbl_InMa_Str_LocRow dr in dtStore.Rows)
            {
                ddlDelStore.Items.Add(new ListItem(dr.Str_Loc_Name, dr.Str_Loc_Ref.ToString()));
            }
            ddlDelStore.Items.Insert(0, new ListItem("---All---", "0"));
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
                            if (dtSp.Rows.Count > 0) spRef = dtSp[0].Sp_Ref.ToString();
                        }
                    }
                }
                #endregion


                if (optRpt.SelectedValue == "0")
                {
                    if (ddlDelStore.SelectedIndex == 0)
                    {
                        rptSelcFormula = "{View_Challan_Details.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Challan_Details.Trn_Hdr_HRPB_Flag}='P'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Challan_Details.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_Challan_Details.Trn_Hdr_HRPB_Flag}='P' and {View_Challan_Details.Trn_Det_Str_Code}='" + ddlDelStore.SelectedValue.ToString() + "'";
                    }
                    rptFile = "~/Module/Sales/Reports/rptDelChlnListDate.rpt";
                }

                if (optRpt.SelectedValue == "1")
                {
                    if (custRef == "")
                    {
                        if (ddlDelStore.SelectedIndex == 0)
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Challan_Details.Trn_Det_Str_Code}='" + ddlDelStore.SelectedValue.ToString() + "'";
                        }
                    }
                    else
                    {
                        if (ddlDelStore.SelectedIndex == 0)
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Hdr_Pcode}='" + custRef + "'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Hdr_Pcode}='" + custRef + "' and {View_Challan_Details.Trn_Det_Str_Code}='" + ddlDelStore.SelectedValue.ToString() + "'";
                        }
                    }
                    rptFile = "~/Module/Sales/Reports/rptDelChlnListCust.rpt";
                }

                if (optRpt.SelectedValue == "2")
                {
                    if (itemRef == "")
                    {
                        if (ddlDelStore.SelectedIndex == 0)
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Challan_Details.Trn_Det_Str_Code}='" + ddlDelStore.SelectedValue.ToString() + "'";
                        }
                    }
                    else
                    {
                        if (ddlDelStore.SelectedIndex == 0)
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Det_Icode}='" + itemRef + "'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Det_Icode}='" + itemRef + "' and {View_Sales_Do_Chln.Trn_Det_Icode}='" + itemRef + "'";
                        }
                    }
                    rptFile = "~/Module/Sales/Reports/rptDelChlnListItem.rpt";
                }

                if (optRpt.SelectedValue == "3")
                {
                    if (spRef == "")
                    {
                        if (ddlDelStore.SelectedIndex == 0)
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Challan_Details.Trn_Det_Str_Code}='" + ddlDelStore.SelectedValue.ToString() + "'";
                        }
                    }
                    else
                    {
                        if (ddlDelStore.SelectedIndex == 0)
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.SO_Hdr_Com4}='" + spRef + "'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.SO_Hdr_Com4}='" + spRef + "' and {View_Challan_Details.Trn_Det_Str_Code}='" + ddlDelStore.SelectedValue.ToString() + "'";
                        }
                    }
                    rptFile = "~/Module/Sales/Reports/rptDelChlnListSP.rpt";
                }

                if (optRpt.SelectedValue == "4")
                {
                    if (custRef == "")
                    {
                        if (ddlDelStore.SelectedIndex == 0)
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Challan_Details.Trn_Det_Str_Code}='" + ddlDelStore.SelectedValue.ToString() + "'";
                        }
                    }
                    else
                    {
                        if (ddlDelStore.SelectedIndex == 0)
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Hdr_Pcode}='" + custRef + "'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Hdr_Pcode}='" + custRef + "' and {View_Challan_Details.Trn_Det_Str_Code}='" + ddlDelStore.SelectedValue.ToString() + "'";
                        }
                    }
                    rptFile = "~/Module/Sales/Reports/rptDelChlnListCustVal.rpt";
                }

                Session["fRptHdr"] = "Delivery Store/Location " + ddlDelStore.SelectedItem.Text.ToString();
                Session["RptDateFrom"] = txtFromDate.Text.Trim();
                Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
        }

        protected void optRpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optRpt.SelectedValue == "1")
            {
                txtSearch.Text = "";
                //AutoCompleteExtenderSrch.ServicePath = "~/Module/Sales/Forms/wsAutoComSales.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchCustomer";
            }
            if (optRpt.SelectedValue == "2")
            {
                txtSearch.Text = "";
                //AutoCompleteExtenderSrch.ServicePath = "~/Module/Sales/Forms/wsAutoComSales.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchItemList";
            }
            if (optRpt.SelectedValue == "3")
            {
                txtSearch.Text = "";
                //AutoCompleteExtenderSrch.ServicePath = "~/Module/Sales/Forms/wsAutoComSales.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchSalesPer";
            }
        }
    }
}