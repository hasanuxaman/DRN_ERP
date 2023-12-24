using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    /// <summary>
    /// Summary description for wsAutoComAcc
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
    public class wsAutoComAcc : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string[] GetSrchCoa(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtCoa = taCoa.GetDataBySrchList(prefixText);

            try
            {
                var coaType = "";
                foreach (dsAccMas.tbl_Acc_Fa_Gl_CoaRow dr in dtCoa.Rows)
                {
                    switch (dr.Gl_Coa_Type.ToString())
                    {
                        case "P":
                            coaType = "[Customer]";                            
                            break;
                        case "S":
                            coaType = "[Supplier]";
                            break;
                        case "B":
                            coaType = "[Bank]";
                            break;
                        case "I":
                            coaType = "[Inventory]";
                            break;
                        case "T":
                            coaType = "[Transport]";
                            break;
                        case "O":
                            coaType = "[Others]";
                            break;
                        default:
                            coaType = "[]";
                            break;
                    }

                    string strItem = dr.Gl_Coa_Code.ToString() + ":" + coaType.ToString() + ":" + dr.Gl_Coa_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchCoaByType(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtCoa = new dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable();

            try
            {
                if (contextKey.Length > 0)
                    dtCoa = taCoa.GetDataBySrchListByType(contextKey, prefixText);
                else
                    dtCoa = taCoa.GetDataBySrchList(prefixText);

                foreach (dsAccMas.tbl_Acc_Fa_Gl_CoaRow dr in dtCoa.Rows)
                {
                    string strItem = dr.Gl_Coa_Code.ToString() + ":" + dr.Gl_Coa_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchCoaByPayRcv(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtCoa = new dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable();

            try
            {
                dtCoa = taCoa.GetDataBySrchListByPayRcv(prefixText);
                foreach (dsAccMas.tbl_Acc_Fa_Gl_CoaRow dr in dtCoa.Rows)
                {
                    string strItem = dr.Gl_Coa_Code.ToString() + ":" + dr.Gl_Coa_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchCoaByPayBank(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtCoa = new dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable();

            try
            {
                dtCoa = taCoa.GetDataBySrchListByPayBnk(prefixText);
                foreach (dsAccMas.tbl_Acc_Fa_Gl_CoaRow dr in dtCoa.Rows)
                {
                    string strItem = dr.Gl_Coa_Code.ToString() + ":" + dr.Gl_Coa_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchJvList(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taJvList = new View_Jv_Det_ListTableAdapter();

            var dtJvList = taJvList.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsAccTran.View_Jv_Det_ListRow dr in dtJvList.Rows)
                {
                    string strItem = dr.Trn_Ref_No.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchJvListByCategoryByManualEntry(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taJvList = new View_Jv_Det_ListTableAdapter();

            var dtJvList = taJvList.GetDataBySrchListByJvCategoryByManualEntry(prefixText, contextKey);

            try
            {
                foreach (dsAccTran.View_Jv_Det_ListRow dr in dtJvList.Rows)
                {
                    string strItem = dr.Trn_Ref_No.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }
    }
}
