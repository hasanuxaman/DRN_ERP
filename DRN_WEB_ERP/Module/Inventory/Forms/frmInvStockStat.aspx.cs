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
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmInvStockStat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrchItem.ContextKey = "0";

            try
            {
                var taItemType = new tbl_InMa_TypeTableAdapter();
                var dtItemType = taItemType.GetDataByAsc();
                cboItemType.DataSource = dtItemType;
                cboItemType.DataTextField = "Item_Type_Name";
                cboItemType.DataValueField = "Item_Type_Code";
                cboItemType.DataBind();
                cboItemType.Items.Insert(0, new ListItem("-----All-----", "0"));

                //var taItem = new tbl_InMa_Item_DetTableAdapter();
                //var dtSaleItem = taItem.GetDataBySortAsc();
                //cboItem.DataSource = dtSaleItem;
                //cboItem.DataTextField = "Itm_Det_Desc";
                //cboItem.DataValueField = "Itm_Det_Ref";
                //cboItem.DataBind();
                //cboItem.Items.Insert(0, new ListItem("----------All----------", "0"));

                var taStore = new tbl_InMa_Str_LocTableAdapter();
                var dtStore = taStore.GetDataBySortName();
                foreach (dsInvMas.tbl_InMa_Str_LocRow dr in dtStore.Rows)
                {
                    cboStore.Items.Add(new ListItem(dr.Str_Loc_Name, dr.Str_Loc_Ref.ToString()));
                }
                cboStore.Items.Insert(0, new ListItem("---All---", "0"));

                var taStkCtl = new View_Inv_Stk_CtlTableAdapter();
                var dtStkCtl = taStkCtl.GetDataBySortAsc();
                //gvStkStat.DataSource = dtStkCtl;
                Session["data"] = dtStkCtl;
                //gvStkStat.DataBind();

                SetItemGridData();
            }
            catch (Exception ex) { }
        }

        protected void cboItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtenderSrchItem.ContextKey = cboItemType.SelectedValue.ToString();
            txtItemName.Text = "";

            //var taItem = new tbl_InMa_Item_DetTableAdapter();
            //var taStkCtl = new View_Inv_Stk_CtlTableAdapter();

            //try
            //{
            //    if (cboItemType.SelectedIndex == 0)
            //    {
            //        cboItem.Items.Clear();

            //        var dtSaleItem = taItem.GetDataBySortAsc();
            //        cboItem.DataSource = dtSaleItem;
            //        cboItem.DataTextField = "Itm_Det_Desc";
            //        cboItem.DataValueField = "Itm_Det_Ref";
            //        cboItem.DataBind();
            //        cboItem.Items.Insert(0, new ListItem("----------All----------", "0"));

            //        if (cboStore.SelectedIndex == 0)
            //        {
            //            var dtStkCtl = taStkCtl.GetDataBySortAsc();
            //            //gvStkStat.DataSource = dtStkCtl;
            //            //gvStkStat.DataBind();
            //            Session["data"] = dtStkCtl;
            //            SetItemGridData();
            //        }
            //        else
            //        {
            //            var dtStkCtl = taStkCtl.GetDataByStore(cboStore.SelectedValue.ToString());
            //            //gvStkStat.DataSource = dtStkCtl;
            //            //gvStkStat.DataBind();
            //            Session["data"] = dtStkCtl;
            //            SetItemGridData();
            //        }
            //    }
            //    else
            //    {
            //        cboItem.Items.Clear();

            //        var dtSaleItem = taItem.GetDataByItemType(cboItemType.SelectedValue.ToString());
            //        cboItem.DataSource = dtSaleItem;
            //        cboItem.DataTextField = "Itm_Det_Desc";
            //        cboItem.DataValueField = "Itm_Det_Ref";
            //        cboItem.DataBind();
            //        cboItem.Items.Insert(0, new ListItem("----------All----------", "0"));

            //        if (cboStore.SelectedIndex == 0)
            //        {
            //            var dtStkCtl = taStkCtl.GetDataByItemType(cboItemType.SelectedValue.ToString());
            //            //gvStkStat.DataSource = dtStkCtl;
            //            //gvStkStat.DataBind();
            //            Session["data"] = dtStkCtl;
            //            SetItemGridData();
            //        }
            //        else
            //        {
            //            var dtStkCtl = taStkCtl.GetDataByItemTypeStore(cboItemType.SelectedValue.ToString(), cboStore.SelectedValue.ToString());
            //            //gvStkStat.DataSource = dtStkCtl;
            //            //gvStkStat.DataBind();
            //            Session["data"] = dtStkCtl;
            //            SetItemGridData();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
            //    tblMsg.Rows[1].Cells[0].InnerText = "";
            //    ModalPopupExtenderMsg.Show();
            //}
        }

        protected void ddlOrdItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            #region Old Code
            //var taStkCtl = new View_Inv_Stk_CtlTableAdapter();
            //try
            //{
            //    if (cboItem.SelectedIndex == 0)
            //    {
            //        var dtStkCtl = taStkCtl.GetDataByItemType(cboItemType.SelectedValue.ToString());                    
            //        //gvStkStat.DataSource = dtStkCtl;
            //        //Session["data"] = dtStkCtl;
            //        //gvStkStat.DataBind();
            //        Session["data"] = dtStkCtl;
            //        SetItemGridData();
            //    }
            //    else
            //    {
            //        var dtStkCtl = taStkCtl.GetDataByItemRef(Convert.ToInt32(cboItem.SelectedValue.ToString()));
            //        //gvStkStat.DataSource = dtStkCtl;
            //        //Session["data"] = dtStkCtl;
            //        //gvStkStat.DataBind();
            //        Session["data"] = dtStkCtl;
            //        SetItemGridData();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
            //    tblMsg.Rows[1].Cells[0].InnerText = "";
            //    ModalPopupExtenderMsg.Show();
            //}
            #endregion

            //var taStkCtl = new View_Inv_Stk_CtlTableAdapter();

            //try
            //{
            //    if (cboItemType.SelectedIndex == 0)
            //    {
            //        if (cboItem.SelectedIndex == 0)
            //        {
            //            #region Item Type and Item All Selected
            //            if (cboStore.SelectedIndex == 0)
            //            {
            //                var dtStkCtl = taStkCtl.GetDataBySortAsc();
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            else
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByStore(cboStore.SelectedValue.ToString());
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            #endregion
            //        }
            //        else
            //        {
            //            #region Item Type All Selected
            //            if (cboStore.SelectedIndex == 0)
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemRef(Convert.ToInt32(cboItem.SelectedValue.ToString()));
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            else
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemRefStore(Convert.ToInt32(cboItem.SelectedValue.ToString()), cboStore.SelectedValue.ToString());
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            #endregion
            //        }
            //    }
            //    else
            //    {
            //        if (cboItem.SelectedIndex == 0)
            //        {
            //            #region Item Type and Item Select
            //            if (cboStore.SelectedIndex == 0)
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemType(cboItemType.SelectedValue.ToString());
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            else
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemTypeStore(cboItemType.SelectedValue.ToString(), cboStore.SelectedValue.ToString());
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            #endregion
            //        }
            //        else
            //        {
            //            #region Item Type Select
            //            if (cboStore.SelectedIndex == 0)
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemRef(Convert.ToInt32(cboItem.SelectedValue.ToString()));
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            else
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemRefStore(Convert.ToInt32(cboItem.SelectedValue.ToString()), cboStore.SelectedValue.ToString());
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            #endregion
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
            //    tblMsg.Rows[1].Cells[0].InnerText = "";
            //    ModalPopupExtenderMsg.Show();
            //}
        }

        protected void cboStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var taStkCtl = new View_Inv_Stk_CtlTableAdapter();

            //try
            //{
            //    if (cboItemType.SelectedIndex == 0)
            //    {
            //        if (cboItem.SelectedIndex == 0)
            //        {
            //            #region Item Type and Item All Selected
            //            if (cboStore.SelectedIndex == 0)
            //            {
            //                var dtStkCtl = taStkCtl.GetDataBySortAsc();
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            else
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByStore(cboStore.SelectedValue.ToString());
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            #endregion
            //        }
            //        else
            //        {
            //            #region Item Type All Selected
            //            if (cboStore.SelectedIndex == 0)
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemRef(Convert.ToInt32(cboItem.SelectedValue.ToString()));
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            else
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemRefStore(Convert.ToInt32(cboItem.SelectedValue.ToString()), cboStore.SelectedValue.ToString());
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            #endregion
            //        }                    
            //    }
            //    else
            //    {
            //        if (cboItem.SelectedIndex == 0)
            //        {
            //            #region Item Type and Item Select
            //            if (cboStore.SelectedIndex == 0)
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemType(cboItemType.SelectedValue.ToString());
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            else
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemTypeStore(cboItemType.SelectedValue.ToString(), cboStore.SelectedValue.ToString());
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            #endregion
            //        }
            //        else
            //        {
            //            #region Item Type Select
            //            if (cboStore.SelectedIndex == 0)
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemRef(Convert.ToInt32(cboItem.SelectedValue.ToString()));
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            else
            //            {
            //                var dtStkCtl = taStkCtl.GetDataByItemRefStore(Convert.ToInt32(cboItem.SelectedValue.ToString()), cboStore.SelectedValue.ToString());
            //                //gvStkStat.DataSource = dtStkCtl;
            //                //gvStkStat.DataBind();
            //                Session["data"] = dtStkCtl;
            //                SetItemGridData();
            //            }
            //            #endregion
            //        }
            //    }                
            //}
            //catch (Exception ex)
            //{
            //    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
            //    tblMsg.Rows[1].Cells[0].InnerText = "";
            //    ModalPopupExtenderMsg.Show();
            //}
        }

        protected void gvStkStat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStkStat.PageIndex = e.NewPageIndex;
            SetItemGridData();
        }

        protected void SetItemGridData()
        {
            //var taStkCtl = new View_Inv_Stk_CtlTableAdapter();
            //var dtStkCtl = taStkCtl.GetDataBySortAsc();
            var dtStkCtl = Session["data"];
            gvStkStat.DataSource = dtStkCtl;            
            gvStkStat.DataBind();
        }

        protected void gvStkStat_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortexp = e.SortExpression;
            Session["sortexp"] = sortexp;
            if (Session["sortDirection"] != null && Session["sortDirection"].ToString() == SortDirection.Descending.ToString())
            {
                Session["sortDirection"] = SortDirection.Ascending;
                ConvertSortDirection(sortexp, "ASC");
            }
            else
            {
                Session["sortDirection"] = SortDirection.Descending;
                ConvertSortDirection(sortexp, "DESC");
            }
        }

        private void ConvertSortDirection(string soreExpression, string p)
        {
            DataTable dataTable = Session["data"] as DataTable;
            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = soreExpression + " " + p;
                gvStkStat.DataSource = dataView;
                gvStkStat.DataBind();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                #region Get Item Details
                var itemRef = "";
                var itemName = "";
                var srchWords = txtItemName.Text.Trim().Split(':');
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
                        if (dtItem.Rows.Count > 0)
                        {
                            itemRef = dtItem[0].Itm_Det_Ref.ToString();
                            itemName = dtItem[0].IsItm_Det_DescNull() ? "0" : dtItem[0].Itm_Det_Desc.ToString();
                        }
                        else
                        {
                            //tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                            //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //ModalPopupExtenderMsg.Show();
                            //return;
                        }
                    }
                    else
                    {
                        //tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                        //tblMsg.Rows[1].Cells[0].InnerText = "";
                        //ModalPopupExtenderMsg.Show();
                        //return;
                    }
                }
                else
                {
                    //tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                    //tblMsg.Rows[1].Cells[0].InnerText = "";
                    //ModalPopupExtenderMsg.Show();
                    //return;
                }
                #endregion

                var qryStr = "";

                if (cboItemType.SelectedIndex == 0)
                {
                    if (txtItemName.Text.Trim().Length <= 0)
                    {
                        #region Item Type and Item All Selected
                        if (cboStore.SelectedIndex == 0)
                        {
                            qryStr = "select Itm_Det_T_C1 as Item_Category,Str_Loc_Name as Store_Name, Itm_Det_Ref as Item_Ref,Itm_Det_Code as Item_Code,Itm_Det_Desc as Item_Name,Itm_Det_Stk_Unit as Item_Unit, " +
                                     "Stk_Ctl_Cur_Stk as Current_Stock,(isnull(Stk_Ctl_Cur_Stk,0)* isnull(Max_Rate,0)) as Stk_Value from tbl_InMa_Item_Det left outer join tbl_InMa_Stk_Ctl on Itm_Det_Ref=Stk_Ctl_ICode " +
                                     "left outer join tbl_InMa_Str_Loc on Stk_Ctl_SCode=Str_Loc_Ref left outer join View_Item_Max_Rate on Itm_Det_Ref=Trn_Det_Icode";
                        }
                        else
                        {
                            qryStr = "select Itm_Det_T_C1 as Item_Category,Str_Loc_Name as Store_Name, Itm_Det_Ref as Item_Ref,Itm_Det_Code as Item_Code,Itm_Det_Desc as Item_Name,Itm_Det_Stk_Unit as Item_Unit, " +
                                     "Stk_Ctl_Cur_Stk as Current_Stock,(isnull(Stk_Ctl_Cur_Stk,0)* isnull(Max_Rate,0)) as Stk_Value from tbl_InMa_Item_Det left outer join tbl_InMa_Stk_Ctl on Itm_Det_Ref=Stk_Ctl_ICode " +
                                     "left outer join tbl_InMa_Str_Loc on Stk_Ctl_SCode=Str_Loc_Ref left outer join View_Item_Max_Rate on Itm_Det_Ref=Trn_Det_Icode Where Str_Loc_Ref='" + cboStore.SelectedValue.ToString() + "'";
                        }
                        #endregion
                    }
                    else
                    {
                        #region Item Type All Selected
                        if (cboStore.SelectedIndex == 0)
                        {
                            qryStr = "select Itm_Det_T_C1 as Item_Category,Str_Loc_Name as Store_Name, Itm_Det_Ref as Item_Ref,Itm_Det_Code as Item_Code,Itm_Det_Desc as Item_Name,Itm_Det_Stk_Unit as Item_Unit, " +
                                     "Stk_Ctl_Cur_Stk as Current_Stock,(isnull(Stk_Ctl_Cur_Stk,0)* isnull(Max_Rate,0)) as Stk_Value from tbl_InMa_Item_Det left outer join tbl_InMa_Stk_Ctl on Itm_Det_Ref=Stk_Ctl_ICode " +
                                     "left outer join tbl_InMa_Str_Loc on Stk_Ctl_SCode=Str_Loc_Ref left outer join View_Item_Max_Rate on Itm_Det_Ref=Trn_Det_Icode Where Itm_Det_Ref='" + Convert.ToInt32(itemRef.ToString()) + "'";
                        }
                        else
                        {
                            qryStr = "select Itm_Det_T_C1 as Item_Category,Str_Loc_Name as Store_Name, Itm_Det_Ref as Item_Ref,Itm_Det_Code as Item_Code,Itm_Det_Desc as Item_Name,Itm_Det_Stk_Unit as Item_Unit, " +
                                     "Stk_Ctl_Cur_Stk as Current_Stock,(isnull(Stk_Ctl_Cur_Stk,0)* isnull(Max_Rate,0)) as Stk_Value from tbl_InMa_Item_Det left outer join tbl_InMa_Stk_Ctl on Itm_Det_Ref=Stk_Ctl_ICode " +
                                     "left outer join tbl_InMa_Str_Loc on Stk_Ctl_SCode=Str_Loc_Ref left outer join View_Item_Max_Rate on Itm_Det_Ref=Trn_Det_Icode Where Itm_Det_Ref='" + Convert.ToInt32(itemRef.ToString()) + "' and Str_Loc_Ref='" + cboStore.SelectedValue.ToString() + "'";
                        }
                        #endregion
                    }
                }
                else
                {
                    if (txtItemName.Text.Trim().Length <= 0)
                    {
                        #region Item Type and Item Select
                        if (cboStore.SelectedIndex == 0)
                        {
                            qryStr = "select Itm_Det_T_C1 as Item_Category,Str_Loc_Name as Store_Name, Itm_Det_Ref as Item_Ref,Itm_Det_Code as Item_Code,Itm_Det_Desc as Item_Name,Itm_Det_Stk_Unit as Item_Unit, " +
                                     "Stk_Ctl_Cur_Stk as Current_Stock,(isnull(Stk_Ctl_Cur_Stk,0)* isnull(Max_Rate,0)) as Stk_Value from tbl_InMa_Item_Det left outer join tbl_InMa_Stk_Ctl on Itm_Det_Ref=Stk_Ctl_ICode " +
                                     "left outer join tbl_InMa_Str_Loc on Stk_Ctl_SCode=Str_Loc_Ref left outer join View_Item_Max_Rate on Itm_Det_Ref=Trn_Det_Icode Where Itm_Det_Type='" + cboItemType.SelectedValue.ToString() + "'";
                        }
                        else
                        {
                            qryStr = "select Itm_Det_T_C1 as Item_Category,Str_Loc_Name as Store_Name, Itm_Det_Ref as Item_Ref,Itm_Det_Code as Item_Code,Itm_Det_Desc as Item_Name,Itm_Det_Stk_Unit as Item_Unit, " +
                                     "Stk_Ctl_Cur_Stk as Current_Stock,(isnull(Stk_Ctl_Cur_Stk,0)* isnull(Max_Rate,0)) as Stk_Value from tbl_InMa_Item_Det left outer join tbl_InMa_Stk_Ctl on Itm_Det_Ref=Stk_Ctl_ICode " +
                                     "left outer join tbl_InMa_Str_Loc on Stk_Ctl_SCode=Str_Loc_Ref left outer join View_Item_Max_Rate on Itm_Det_Ref=Trn_Det_Icode Where Itm_Det_Type='" + cboItemType.SelectedValue.ToString() + "' and Str_Loc_Ref='" + cboStore.SelectedValue.ToString() + "'";
                        }
                        #endregion
                    }
                    else
                    {
                        #region Item Type Select
                        if (cboStore.SelectedIndex == 0)
                        {
                            qryStr = "select Itm_Det_T_C1 as Item_Category,Str_Loc_Name as Store_Name, Itm_Det_Ref as Item_Ref,Itm_Det_Code as Item_Code,Itm_Det_Desc as Item_Name,Itm_Det_Stk_Unit as Item_Unit, " +
                                     "Stk_Ctl_Cur_Stk as Current_Stock,(isnull(Stk_Ctl_Cur_Stk,0)* isnull(Max_Rate,0)) as Stk_Value from tbl_InMa_Item_Det left outer join tbl_InMa_Stk_Ctl on Itm_Det_Ref=Stk_Ctl_ICode " +
                                     "left outer join tbl_InMa_Str_Loc on Stk_Ctl_SCode=Str_Loc_Ref left outer join View_Item_Max_Rate on Itm_Det_Ref=Trn_Det_Icode Where Itm_Det_Ref='" + Convert.ToInt32(itemRef.ToString()) + "'";
                        }
                        else
                        {
                            qryStr = "select Itm_Det_T_C1 as Item_Category,Str_Loc_Name as Store_Name, Itm_Det_Ref as Item_Ref,Itm_Det_Code as Item_Code,Itm_Det_Desc as Item_Name,Itm_Det_Stk_Unit as Item_Unit, " +
                                     "Stk_Ctl_Cur_Stk as Current_Stock,(isnull(Stk_Ctl_Cur_Stk,0)* isnull(Max_Rate,0)) as Stk_Value from tbl_InMa_Item_Det left outer join tbl_InMa_Stk_Ctl on Itm_Det_Ref=Stk_Ctl_ICode " +
                                     "left outer join tbl_InMa_Str_Loc on Stk_Ctl_SCode=Str_Loc_Ref left outer join View_Item_Max_Rate on Itm_Det_Ref=Trn_Det_Icode Where Itm_Det_Ref='" + Convert.ToInt32(itemRef.ToString()) + "' and Str_Loc_Ref='" + cboStore.SelectedValue.ToString() + "'";
                        }
                        #endregion
                    }
                }

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
                string filename = String.Format("Stock_status_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Page.Validate("btnSearch");

            if (!Page.IsValid) return;

            var taStkCtl = new View_Inv_Stk_CtlTableAdapter();

            try
            {
                #region Get Item Details
                var itemRef = "";
                var itemName = "";
                var srchWords = txtItemName.Text.Trim().Split(':');
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
                        if (dtItem.Rows.Count > 0)
                        {
                            itemRef = dtItem[0].Itm_Det_Ref.ToString();
                            itemName = dtItem[0].IsItm_Det_DescNull() ? "0" : dtItem[0].Itm_Det_Desc.ToString();
                        }
                        else
                        {
                            //tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                            //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //ModalPopupExtenderMsg.Show();
                            //return;
                        }
                    }
                    else
                    {
                        //tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                        //tblMsg.Rows[1].Cells[0].InnerText = "";
                        //ModalPopupExtenderMsg.Show();
                        //return;
                    }
                }
                else
                {
                    //tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                    //tblMsg.Rows[1].Cells[0].InnerText = "";
                    //ModalPopupExtenderMsg.Show();
                    //return;
                }
                #endregion

                if (cboItemType.SelectedIndex == 0)
                {
                    if (txtItemName.Text.Trim().Length <= 0)
                    {
                        #region Item Type and Item All Selected
                        if (cboStore.SelectedIndex == 0)
                        {
                            var dtStkCtl = taStkCtl.GetDataBySortAsc();
                            //gvStkStat.DataSource = dtStkCtl;
                            //gvStkStat.DataBind();
                            Session["data"] = dtStkCtl;
                            SetItemGridData();
                        }
                        else
                        {
                            var dtStkCtl = taStkCtl.GetDataByStore(cboStore.SelectedValue.ToString());
                            //gvStkStat.DataSource = dtStkCtl;
                            //gvStkStat.DataBind();
                            Session["data"] = dtStkCtl;
                            SetItemGridData();
                        }
                        #endregion
                    }
                    else
                    {
                        #region Item Type All Selected
                        if (cboStore.SelectedIndex == 0)
                        {
                            var dtStkCtl = taStkCtl.GetDataByItemRef(Convert.ToInt32(itemRef.ToString()));
                            //gvStkStat.DataSource = dtStkCtl;
                            //gvStkStat.DataBind();
                            Session["data"] = dtStkCtl;
                            SetItemGridData();
                        }
                        else
                        {
                            var dtStkCtl = taStkCtl.GetDataByItemRefStore(Convert.ToInt32(itemRef.ToString()), cboStore.SelectedValue.ToString());
                            //gvStkStat.DataSource = dtStkCtl;
                            //gvStkStat.DataBind();
                            Session["data"] = dtStkCtl;
                            SetItemGridData();
                        }
                        #endregion
                    }
                }
                else
                {
                    if (txtItemName.Text.Trim().Length <= 0)
                    {
                        #region Item Type and Item Select
                        if (cboStore.SelectedIndex == 0)
                        {
                            var dtStkCtl = taStkCtl.GetDataByItemType(cboItemType.SelectedValue.ToString());
                            //gvStkStat.DataSource = dtStkCtl;
                            //gvStkStat.DataBind();
                            Session["data"] = dtStkCtl;
                            SetItemGridData();
                        }
                        else
                        {
                            var dtStkCtl = taStkCtl.GetDataByItemTypeStore(cboItemType.SelectedValue.ToString(), cboStore.SelectedValue.ToString());
                            //gvStkStat.DataSource = dtStkCtl;
                            //gvStkStat.DataBind();
                            Session["data"] = dtStkCtl;
                            SetItemGridData();
                        }
                        #endregion
                    }
                    else
                    {
                        #region Item Type Select
                        if (cboStore.SelectedIndex == 0)
                        {
                            var dtStkCtl = taStkCtl.GetDataByItemRef(Convert.ToInt32(itemRef.ToString()));
                            //gvStkStat.DataSource = dtStkCtl;
                            //gvStkStat.DataBind();
                            Session["data"] = dtStkCtl;
                            SetItemGridData();
                        }
                        else
                        {
                            var dtStkCtl = taStkCtl.GetDataByItemRefStore(Convert.ToInt32(itemRef.ToString()), cboStore.SelectedValue.ToString());
                            //gvStkStat.DataSource = dtStkCtl;
                            //gvStkStat.DataBind();
                            Session["data"] = dtStkCtl;
                            SetItemGridData();
                        }
                        #endregion
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                AutoCompleteExtenderSrchItem.ContextKey = "0";
                cboItemType.SelectedIndex = 0;
                cboStore.SelectedIndex = 0;
                txtItemName.Text = "";

                var taStkCtl = new View_Inv_Stk_CtlTableAdapter();
                var dtStkCtl = taStkCtl.GetDataBySortAsc();
                //gvStkStat.DataSource = dtStkCtl;
                Session["data"] = dtStkCtl;
                //gvStkStat.DataBind();

                SetItemGridData();
            }
            catch (Exception ex) { }
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var itemRef = "";
                var srchWords = txtItemName.Text.Trim().Split(':');
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
                        if (dtItem.Rows.Count > 0)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }
    }
}