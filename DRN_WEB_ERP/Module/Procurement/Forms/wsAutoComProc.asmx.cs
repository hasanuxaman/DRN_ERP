using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    /// <summary>
    /// Summary description for wsAutoComProc
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsAutoComProc : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string[] GetSrchSupplier(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taSup = new tbl_PuMa_Par_AdrTableAdapter();
            var dtSup = taSup.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsProcMas.tbl_PuMa_Par_AdrRow dr in dtSup.Rows)
                {
                    string strItem = dr.Par_Adr_Ref.ToString() + ":" + dr.Par_Adr_Ref_No.ToString() + ":" + dr.Par_Adr_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchSupplierQtn(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taSup = new tbl_PuMa_Par_Adr_QtnTableAdapter();
            var dtSup = taSup.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsProcMas.tbl_PuMa_Par_Adr_QtnRow dr in dtSup.Rows)
                {
                    string strItem = dr.Par_Adr_Qtn_Ref.ToString() + ":" + dr.Par_Adr_Qtn_Ref_No.ToString() + ":" + dr.Par_Adr_Qtn_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchMpr(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taMpr = new tbl_PuTr_Pr_HdrTableAdapter();
            var dtMpr = taMpr.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsProcTran.tbl_PuTr_Pr_HdrRow dr in dtMpr.Rows)
                {
                    string strItem = dr.Pr_Hdr_Ref.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchAprCs(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taCs = new View_CS_ListTableAdapter();
            var dtCs = taCs.GetAprCsSrchList(prefixText);

            try
            {
                foreach (dsProcTran.View_CS_ListRow dr in dtCs.Rows)
                {
                    string strItem = dr.Pr_Det_Quot_Ref.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }
    }
}
