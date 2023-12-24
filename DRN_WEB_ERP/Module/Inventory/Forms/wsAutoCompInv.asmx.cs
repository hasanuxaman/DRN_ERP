using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    /// <summary>
    /// Summary description for wsAutoCompInv
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsAutoCompInv : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string[] GetSrchStore(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taStore = new tbl_InMa_Str_LocTableAdapter();
            var dtStore = taStore.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsInvMas.tbl_InMa_Str_LocRow dr in dtStore.Rows)
                {
                    string strItem = dr.Str_Loc_Ref.ToString() + ":" + dr.Str_Loc_Code.ToString() + ":" + dr.Str_Loc_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchItemCategory(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetSrachItemType(prefixText);

            try
            {
                foreach (dsInvMas.tbl_InMa_TypeRow dr in dtItemType.Rows)
                {
                    string strItem = dr.Item_Type_Code.ToString() + ":" + dr.Item_Type_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchItem(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtItem = taItem.GetDataBySrchItemList(prefixText);

            try
            {
                foreach (dsInvMas.tbl_InMa_Item_DetRow dr in dtItem.Rows)
                {
                    string strItem = dr.Itm_Det_Ref.ToString() + ":" + dr.Itm_Det_Code.ToString() + ":" + dr.Itm_Det_Desc.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchFilteredItem(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtItem = new dsInvMas.tbl_InMa_Item_DetDataTable();

            try
            {
                if (contextKey == "0")
                    dtItem = taItem.GetDataBySrchItemList(prefixText);
                else
                    dtItem = taItem.GetDataByItemTypeSrchList(contextKey.ToString(), prefixText.ToString());

                foreach (dsInvMas.tbl_InMa_Item_DetRow dr in dtItem.Rows)
                {
                    string strItem = dr.Itm_Det_Ref.ToString() + ":" + dr.Itm_Det_Code.ToString() + ":" + dr.Itm_Det_Desc.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchActiveFilteredItem(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtItem = new dsInvMas.tbl_InMa_Item_DetDataTable();

            try
            {
                if (contextKey == "0")
                    dtItem = taItem.GetDataBySrchItemList(prefixText);
                else
                    dtItem = taItem.GetDataByItemTypeSrchList(contextKey.ToString(), prefixText.ToString());

                foreach (dsInvMas.tbl_InMa_Item_DetRow dr in dtItem.Rows)
                {
                    string strItem = dr.Itm_Det_Ref.ToString() + ":" + dr.Itm_Det_Desc.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchProItem(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtItem = taItem.GetProItemSrchList(prefixText);

            try
            {
                foreach (dsInvMas.tbl_InMa_Item_DetRow dr in dtItem.Rows)
                {
                    string strItem = dr.Itm_Det_Ref.ToString() + ":" + dr.Itm_Det_Code.ToString() + ":" + dr.Itm_Det_Desc.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchActiveProItem(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtItem = taItem.GetProItemSrchList(prefixText);

            try
            {
                foreach (dsInvMas.tbl_InMa_Item_DetRow dr in dtItem.Rows)
                {
                    string strItem = dr.Itm_Det_Ref.ToString() + ":" + dr.Itm_Det_Desc.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchIssueHead(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taIsuHead = new View_Issue_HeadTableAdapter();
            var dtIsuHead = taIsuHead.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsInvMas.View_Issue_HeadRow dr in dtIsuHead.Rows)
                {
                    string strItem = dr.IsuHeadRefNo.ToString() + "::" + dr.IsuHeadCode.ToString() + "::" + dr.IsuHeadName.ToString() + "::" + dr.IsuHeadDet.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchPendMrr(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taPoHdrDet = new View_Pend_MRR_ListTableAdapter();
            var dtPoHdrDet = taPoHdrDet.GetSrchPendMrrList(prefixText);
            try
            {
                foreach (dsProcTran.View_Pend_MRR_ListRow dr in dtPoHdrDet.Rows)
                {
                    string strItem = dr.PO_Hdr_Ref.ToString() + "::" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchIsuHeadRpt(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taIsuHeadRpt = new View_Issue_Head_ReportTableAdapter();
            var dtIsuHeadRpt = taIsuHeadRpt.GetDataBySrchIssueHead(prefixText);
            try
            {
                foreach (dsInvMas.View_Issue_Head_ReportRow dr in dtIsuHeadRpt.Rows)
                {
                    string strItem = dr.IsuHeadRefNo.ToString() + "::" + dr.IsuHeadCode.ToString() + "::" + dr.IsuHeadName.ToString() + "::" + dr.IsuHeadDet.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }        
    }
}
