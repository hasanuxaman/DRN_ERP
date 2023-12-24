using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    /// <summary>
    /// Summary description for wsAutoComSales
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
    public class wsAutoComSales : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string[] GetSrchActiveCustomer(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taUser = new tblSalesPartyAdrTableAdapter();
            var dtUser = taUser.GetDataBySrchActList(prefixText);

            try
            {
                foreach (dsSalesMas.tblSalesPartyAdrRow dr in dtUser.Rows)
                {
                    string strItem = dr.Par_Adr_Ref.ToString() + ":" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchCustomer(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taUser = new tblSalesPartyAdrTableAdapter();
            var dtUser = taUser.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsSalesMas.tblSalesPartyAdrRow dr in dtUser.Rows)
                {
                    string strItem = dr.Par_Adr_Ref.ToString() + ":" + dr.Par_Adr_Ref_No.ToString() + ":" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchCustomerByDsm(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taUser = new tblSalesPartyAdrTableAdapter();
            var dtUser = new dsSalesMas.tblSalesPartyAdrDataTable();

            if (contextKey != "0")
                dtUser = taUser.GetDataBySrchListByDsm(contextKey, prefixText);
            else
                dtUser = taUser.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsSalesMas.tblSalesPartyAdrRow dr in dtUser.Rows)
                {
                    string strItem = dr.Par_Adr_Ref.ToString() + ":" + dr.Par_Adr_Ref_No.ToString() + ":" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchCustomerByZone(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taUser = new tblSalesPartyAdrTableAdapter();
            var dtUser = new dsSalesMas.tblSalesPartyAdrDataTable();


            if (contextKey != "0")
            {
                int result;
                if (int.TryParse(contextKey, out result))
                {
                    dtUser = taUser.GetDataBySrchListByZone(Convert.ToInt32(contextKey), prefixText);
                }
            }
            else
                dtUser = taUser.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsSalesMas.tblSalesPartyAdrRow dr in dtUser.Rows)
                {
                    string strItem = dr.Par_Adr_Ref.ToString() + ":" + dr.Par_Adr_Ref_No.ToString() + ":" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchCustomerBySalesTree(string prefixText, int count, string contextKey)
        {
            //var empRef = "";
            //var mpoRefList = "";
            //var cnt = 0;
            //var srchWords = contextKey.ToString().Trim().Split(':');
            //foreach (string word in srchWords)
            //{
            //    if (cnt == 0)
            //        empRef = word;
            //    if (cnt == 1)
            //        mpoRefList = word;
            //    cnt++;
            //}

            List<string> items = new List<string>(count);

            SqlConnection connection = new SqlConnection();
            var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            var qryStr = "";
            //if (contextKey != "0")
            //    qryStr = "Select * from TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString() + " WHERE (Par_Adr_Ext_Data2 in " + mpoRefList.ToString() + ") " +
            //             "AND (Par_Adr_Ref_No LIKE '%" + prefixText + "%' OR Par_Adr_Name LIKE '%" + prefixText + "%')";

            qryStr = "Select * from TEMP_View_Sales_Tree_Mpo_Dealer_" + contextKey.ToString() + " WHERE (Par_Adr_Ref_No LIKE '%" + prefixText + "%' OR Par_Adr_Name LIKE '%" + prefixText + "%')";
            SqlDataAdapter daCsSum = new SqlDataAdapter(qryStr, connection);
            DataTable dtCsSum = new DataTable();
            daCsSum.Fill(dtCsSum);

            try
            {
                for (int i = 0; i < dtCsSum.Rows.Count; i++)
                {
                    string strItem = dtCsSum.Rows[i]["Par_Adr_Ref"].ToString() + ":" + dtCsSum.Rows[i]["Par_Adr_Ref_No"].ToString() + ":" + dtCsSum.Rows[i]["Par_Adr_Name"].ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchCustomerByWing(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taUser = new tblSalesPartyAdrTableAdapter();
            var dtUser = new dsSalesMas.tblSalesPartyAdrDataTable();

            if (contextKey != "")
                dtUser = taUser.GetDataBySrchListByWing(contextKey, prefixText);
            else
                dtUser = taUser.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsSalesMas.tblSalesPartyAdrRow dr in dtUser.Rows)
                {
                    string strItem = dr.Par_Adr_Ref.ToString() + ":" + dr.Par_Adr_Ref_No.ToString() + ":" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchRetailer(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taRtl = new tblSalesPartyRtlTableAdapter();
            var dtRtl = new dsSalesMas.tblSalesPartyRtlDataTable();

            if (contextKey == "0")
                dtRtl = taRtl.GetDataBySrchList(prefixText);
            else
                dtRtl = taRtl.GetDataBySrchListByDealer(contextKey, prefixText);

            try
            {
                foreach (dsSalesMas.tblSalesPartyRtlRow dr in dtRtl.Rows)
                {
                    string strItem = dr.Par_Rtl_Ref.ToString() + ":" + dr.Par_Rtl_Ref_No.ToString() + ":" + dr.Par_Rtl_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchDealerRetailer(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taUser = new View_Dealer_RelailerTableAdapter();
            var dtUser = taUser.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsSalesMas.View_Dealer_RelailerRow dr in dtUser.Rows)
                {
                    string strItem = dr.Par_Adr_Ref.ToString() + ":" + dr.Par_Adr_Ref_No.ToString() + ":" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchSalesLoc(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taLocMas = new tblSalesLocMasTableAdapter();
            var dtLocMas = taLocMas.GetSrchLocList(prefixText);

            try
            {
                foreach (dsSalesMas.tblSalesLocMasRow dr in dtLocMas.Rows)
                {
                    string strItem = dr.Loc_Mas_Ref.ToString() + ":" + dr.Loc_Mas_Code.ToString() + ":" + dr.Loc_Mas_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchSalesPer(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taSp = new tblSalesPersonMasTableAdapter();
            var dtSp = taSp.GetSrchSalesPerList(prefixText);

            try
            {
                foreach (dsSalesMas.tblSalesPersonMasRow dr in dtSp.Rows)
                {
                    string strItem = dr.Sp_Ref.ToString() + ":" + dr.Sp_Short_Name.ToString() + ":" + dr.Sp_Full_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchDSM(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taDSM = new tblSalesDsmMasTableAdapter();
            var dtDSM = taDSM.GetSearchDsmList(prefixText);

            try
            {
                foreach (dsSalesMas.tblSalesDsmMasRow dr in dtDSM.Rows)
                {
                    string strItem = dr.Dsm_Ref.ToString() + ":" + dr.Dsm_Short_Name.ToString() + ":" + dr.Dsm_Full_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchOrdList(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taSO = new VIEW_SALES_ORDERTableAdapter();
            var dtSO = new dsSalesTran.VIEW_SALES_ORDERDataTable();

            if (contextKey == "0")
                dtSO = taSO.GetDataBySrchList(prefixText);
            else
                dtSO = taSO.GetDataBySrchListAll(prefixText);

            try
            {
                foreach (dsSalesTran.VIEW_SALES_ORDERRow dr in dtSO.Rows)
                {
                    string strItem = dr.SO_Hdr_Ref.ToString() + ":" + dr.SO_Hdr_Ref_No.ToString() + ":" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchChlnListByYearMonth(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taChln = new View_Challan_DetailsTableAdapter();
            var dtChln = new dsInvTran.View_Challan_DetailsDataTable();

            try
            {
                var strYr = DateTime.Now.Year.ToString();
                var strMnt = DateTime.Now.Month.ToString();
                if (contextKey != "")
                {
                    var srchLocWords = contextKey.ToString().Trim().Split('-');
                    int cnt = 1;
                    foreach (string word in srchLocWords)
                    {
                        if (cnt == 1)
                            strYr = word;
                        if (cnt == 2)
                            strMnt = word;
                        cnt++;
                    }
                }
                dtChln = taChln.GetDataBySrchListByYearMonth(Convert.ToDecimal(strYr), Convert.ToDecimal(strMnt), prefixText);

                foreach (dsInvTran.View_Challan_DetailsRow dr in dtChln.Rows)
                {
                    string strItem = dr.Trn_Hdr_DC_No.ToString() + ":" + dr.Trn_Hdr_Cno.ToString() + ":" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchChlnList(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taChln = new View_Challan_DetailsTableAdapter();
            var dtChln = new dsInvTran.View_Challan_DetailsDataTable();

            try
            {
                dtChln = taChln.GetDataBySrchList(prefixText);

                foreach (dsInvTran.View_Challan_DetailsRow dr in dtChln.Rows)
                {
                    string strItem = dr.Trn_Hdr_DC_No.ToString() + ":" + dr.Trn_Hdr_Cno.ToString() + ":" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchUndeliverChlnList(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taChln = new View_Challan_DetailsTableAdapter();
            var dtChln = new dsInvTran.View_Challan_DetailsDataTable();

            try
            {
                dtChln = taChln.GetDataBySrchUndeliveredChlnListByCust(prefixText, contextKey);

                foreach (dsInvTran.View_Challan_DetailsRow dr in dtChln.Rows)
                {
                    string strItem = dr.Trn_Hdr_DC_No.ToString() + ":" + dr.Trn_Hdr_Cno.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchUndeliverChlnListAll(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taChln = new View_Challan_DetailsTableAdapter();
            var dtChln = new dsInvTran.View_Challan_DetailsDataTable();

            try
            {
                dtChln = taChln.GetDataBySrchUndeliveredChlnList(prefixText);

                foreach (dsInvTran.View_Challan_DetailsRow dr in dtChln.Rows)
                {
                    string strItem = dr.Trn_Hdr_DC_No.ToString() + ":" + dr.Trn_Hdr_Cno.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchItemList(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtItem = new dsInvMas.tbl_InMa_Item_DetDataTable();

            try
            {
                dtItem = taItem.GetDataBySrchItemList(prefixText);
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
        public string[] GetSrchFgItemList(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtItem = new dsInvMas.tbl_InMa_Item_DetDataTable();

            try
            {
                dtItem = taItem.GetDataByItemTypeSrchList("FG", prefixText);
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
        public string[] GetSrchDoList(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taDoDet = new VIEW_DELIVERY_ORDERTableAdapter();
            var dtDoDet = new dsSalesTran.VIEW_DELIVERY_ORDERDataTable();

            try
            {
                dtDoDet = taDoDet.GetDataBySrchList(prefixText);
                foreach (dsSalesTran.VIEW_DELIVERY_ORDERRow dr in dtDoDet.Rows)
                {
                    string strItem = dr.DO_Hdr_Ref.ToString() + ":" + dr.DO_Hdr_Ref_No.ToString() + ":" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }
    }
}
