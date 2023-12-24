using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet.dsStaffMasTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    /// <summary>
    /// Summary description for wsAutoCompleteTransport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsAutoCompleteTransport : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string[] LoadSlipSrchList(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLs = taLs.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsTransMas.tbl_TrTr_Load_SlipRow dr in dtLs.Rows)
                {
                    string strItem = dr.LS_Ref_No.ToString() + ":(" + dr.LS_Truck_No.ToString() + ") - [" + dr.LS_Date_Time.ToString() + "]";
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] VslSrchList(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taVsl = new tbl_TrTr_Vsl_MasTableAdapter();
            var dtVasl = taVsl.GetDataByVslSearchList(prefixText);

            try
            {
                foreach (dsTransMas.tbl_TrTr_Vsl_MasRow dr in dtVasl.Rows)
                {
                    string strItem = dr.Vsl_Mas_No.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] TruckSrchList(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taVsl = new tbl_TrTr_Vsl_MasTableAdapter();
            var dtVasl = taVsl.GetDataByTruckSearchList(prefixText);

            try
            {
                foreach (dsTransMas.tbl_TrTr_Vsl_MasRow dr in dtVasl.Rows)
                {
                    string strItem = dr.Vsl_Mas_Ref.ToString() + ":" + dr.Vsl_Mas_No.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] DelLocSrchList(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taLocMas = new View_TrTr_Loc_MasTableAdapter();
            var dtLocMas = taLocMas.GetDataBySearchList(prefixText);

            try
            {
                foreach (dsTransMas.View_TrTr_Loc_MasRow dr in dtLocMas.Rows)
                {
                    string strItem = dr.TrTr_Loc_Ref_No.ToString() + ":" + dr.TrTr_Loc_Name.ToString() + " [Dist:" + dr.DistName + ", Thana:" + dr.ThanaName + "]";
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception ex) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchEmp(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taEmpDet = new tbl_TrTr_Staff_MasTableAdapter();
            var dtEmpDet = new dsStaffMas.tbl_TrTr_Staff_MasDataTable();

            try
            {
                if (contextKey == "1")
                    dtEmpDet = taEmpDet.GetSearchStaffList(prefixText);
                else
                    dtEmpDet = taEmpDet.GetSearchStaffListAct(prefixText);

                foreach (dsStaffMas.tbl_TrTr_Staff_MasRow dr in dtEmpDet.Rows)
                {
                    string strItem = dr.Staff_RefNo.ToString() + ":" + dr.Staff_Id.ToString() + ":" + dr.Staff_First_Name.ToString() + " " + dr.Staff_Last_Name.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }
    }
}
