using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    /// <summary>
    /// Summary description for wsAutoCompleteHrms
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsAutoCompleteHrms : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string[] GetSrchEmp(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taEmpDet = new tblHrmsEmpTableAdapter();
            var dtEmpDet = new dsEmpDet.tblHrmsEmpDataTable();

            try
            {
                if (contextKey == "1")
                    dtEmpDet = taEmpDet.GetSrchMatchEmpList(prefixText);
                else
                    dtEmpDet = taEmpDet.GetSrchMatchEmpListAct(prefixText);

                foreach (dsEmpDet.tblHrmsEmpRow dr in dtEmpDet.Rows)
                {
                    string strItem = dr.EmpRefNo.ToString() + ":" + dr.EmpId.ToString() + ":" + dr.EmpFirstName.ToString() + " " + dr.EmpLastName.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchDeptEmp(string prefixText, int count, string contextKey)
        {
            List<string> items = new List<string>(count);

            var taEmpDet = new View_Emp_BascTableAdapter();
            var dtEmpDet = new dsEmpDet.View_Emp_BascDataTable();

            try
            {
                dtEmpDet = taEmpDet.GetDataSrchByDeptRefAct(contextKey, prefixText);

                foreach (dsEmpDet.View_Emp_BascRow dr in dtEmpDet.Rows)
                {
                    string strItem = dr.EmpRefNo.ToString() + ":" + dr.EmpId.ToString() + ":" + dr.EmpName.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchLoc(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taLoc = new tblHrmsOffLocTableAdapter();
            var dtLoc = new dsHrmsMas.tblHrmsOffLocDataTable();

            try
            {
                dtLoc = taLoc.GetSrchLoc(prefixText);

                foreach (dsHrmsMas.tblHrmsOffLocRow dr in dtLoc.Rows)
                {
                    string strItem = dr.LocRefNo.ToString() + ":" + dr.LocCode.ToString() + ":" + dr.LocName.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchPayHead(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taPayHead = new tblHrmsPayHeadTableAdapter();
            var dtPayHead = new dsHrmsMas.tblHrmsPayHeadDataTable();

            try
            {
                dtPayHead = taPayHead.GetSrchPayHead(prefixText);

                foreach (dsHrmsMas.tblHrmsPayHeadRow dr in dtPayHead.Rows)
                {
                    string strItem = dr.PayHeadRef.ToString() + ":" + dr.PayHeadCode.ToString() + ":" + dr.PayHeadName.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }
    }
}
