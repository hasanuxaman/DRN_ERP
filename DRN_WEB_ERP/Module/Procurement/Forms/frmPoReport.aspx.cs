using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmPoReport : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            chkListRpt.DataSource = SqlDataSource1;
            chkListRpt.DataValueField = "PO_Hdr_Code";
            chkListRpt.DataTextField = "PO_Hdr_Code";
            chkListRpt.DataBind();

            foreach (ListItem li in chkListRpt.Items)
            {
                if (li.Text == "LPO") li.Selected = true;
            }
        }

        protected void optRptFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optRptFilter.SelectedValue == "1")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Procurement/Forms/wsAutoComProc.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchSupplier";
            }

            if (optRptFilter.SelectedValue == "2")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Inventory/Forms/wsAutoCompInv.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchItem";
            }

            if (optRptFilter.SelectedValue == "3")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Inventory/Forms/wsAutoCompInv.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchItemCategory";
            }
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (ListItem li in chkListRpt.Items)
            {
                if (li.Selected) cnt++;
            }

            if (cnt > 0)
            {
                reportInfo();
                var url = "frmShowProcReport.aspx";
                //Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            }
            else
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Select PO Type First.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void reportInfo()
        {
            if (txtFromDate.Text.Trim().Length > 0 && txtToDate.Text.Trim().Length > 0)
            {
                #region Get Supplier Ref
                var supRef = "";
                if (txtSearch.Text.Trim().Length > 0)
                {
                    var srchWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        supRef = word;
                        break;
                    }

                    if (supRef.Length > 0)
                    {
                        int result;
                        if (int.TryParse(supRef, out result))
                        {
                            var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
                            var dtPartyAdr = taSupAdr.GetDataBySupAdrRef(supRef);
                            if (dtPartyAdr.Rows.Count > 0) supRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
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

                #region Get Item Type
                var itemType = "";
                if (txtSearch.Text.Trim().Length > 0)
                {
                    var srchWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        itemType = word;
                        break;
                    }

                    if (itemType.Length > 0)
                    {
                        var taItemType = new tbl_InMa_TypeTableAdapter();
                        var dtItemType = taItemType.GetDataByItemTypeCode(itemType);
                        if (dtItemType.Rows.Count > 0) itemType = dtItemType[0].Item_Type_Code.ToString();
                    }
                }
                #endregion

                var poType = "";
                foreach (ListItem li in chkListRpt.Items)
                {
                    if (li.Selected)
                        poType = poType + "'" + li.Value + "',";
                }

                poType = "[" + poType.Substring(0, (poType.Length - 1)) + "]";

                if (optRptFilter.SelectedValue == "1")
                {
                    if (chkIncMrr.Checked)
                    {
                        if (supRef == "")
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_PuTr_Po_Hdr_Det.PO_Det_Org_QTY}>0 and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }
                        else
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_PuTr_Po_Hdr_Det.PO_Det_Org_QTY}>0 and {View_PuTr_Po_Hdr_Det.PO_Hdr_Pcode}='" + supRef + "' and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }                        

                        if (optRptDetSum.SelectedValue == "1")
                            rptFile = "~/Module/Procurement/Reports/rptProcPoDetSupRcv.rpt";
                        else
                            rptFile = "~/Module/Procurement/Reports/rptProcPoSumSupRcv.rpt";
                    }
                    else
                    {
                        if (supRef == "")
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }
                        else
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_PuTr_Po_Hdr_Det.PO_Hdr_Pcode}='" + supRef + "' and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }

                        if (optRptDetSum.SelectedValue == "1")
                            rptFile = "~/Module/Procurement/Reports/rptProcPoDetSup.rpt";
                        else
                            rptFile = "~/Module/Procurement/Reports/rptProcPoSumSup.rpt";
                    }
                }

                if (optRptFilter.SelectedValue == "2")
                {                   
                    if (chkIncMrr.Checked)
                    {
                        if (itemRef == "")
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_PuTr_Po_Hdr_Det.PO_Det_Org_QTY}>0 and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }
                        else
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_PuTr_Po_Hdr_Det.PO_Det_Org_QTY}>0 and {View_PuTr_Po_Hdr_Det.PO_Det_Icode}='" + itemRef + "' and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }

                        if (optRptDetSum.SelectedValue == "1")
                            rptFile = "~/Module/Procurement/Reports/rptProcPoDetItmRcv.rpt";
                        else
                            rptFile = "~/Module/Procurement/Reports/rptProcPoSumItmRcv.rpt";
                    }
                    else
                    {
                        if (itemRef == "")
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }
                        else
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_PuTr_Po_Hdr_Det.PO_Det_Icode}='" + itemRef + "' and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }

                        if (optRptDetSum.SelectedValue == "1")
                            rptFile = "~/Module/Procurement/Reports/rptProcPoDetItm.rpt";
                        else
                            rptFile = "~/Module/Procurement/Reports/rptProcPoSumItm.rpt";
                    }
                }

                if (optRptFilter.SelectedValue == "3")
                {                    
                    if (chkIncMrr.Checked)
                    {
                        if (itemType == "")
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_PuTr_Po_Hdr_Det.PO_Det_Org_QTY}>0 and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }
                        else
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_PuTr_Po_Hdr_Det.PO_Det_Org_QTY}>0 and {View_InMa_Item_Det.Itm_Det_Type}='" + itemType + "' and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }

                        if (optRptDetSum.SelectedValue == "1")
                            rptFile = "~/Module/Procurement/Reports/rptProcPoDetItmTypeRcv.rpt";
                        else
                            rptFile = "~/Module/Procurement/Reports/rptProcPoSumItmTypeRcv.rpt";
                    }
                    else
                    {
                        if (itemType == "")
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }
                        else
                        {
                            rptSelcFormula = "{View_PuTr_Po_Hdr_Det.PO_Hdr_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_PuTr_Po_Hdr_Det.PO_Hdr_HPC_Flag}='P' and {View_InMa_Item_Det.Itm_Det_Type}='" + itemType + "' and {View_PuTr_Po_Hdr_Det.PO_Hdr_Code} in " + poType;
                        }

                        if (optRptDetSum.SelectedValue == "1")
                            rptFile = "~/Module/Procurement/Reports/rptProcPoDetItmType.rpt";
                        else
                            rptFile = "~/Module/Procurement/Reports/rptProcPoSumItmType.rpt";
                    }
                }

                Session["RptDateFrom"] = txtFromDate.Text.Trim();
                Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
        }        
    }
}