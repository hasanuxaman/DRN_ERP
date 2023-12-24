using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DRN_WEB_ERP.Module.SYS.DataSet;
using DRN_WEB_ERP.Module.SYS.DataSet.dsSysTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.SYS.Forms
{
    /// <summary>
    /// Summary description for wsAutoComSys
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
    public class wsAutoComSys : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string[] GetSrchUser(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taUser = new TBL_USER_INFOTableAdapter();
            var dtUser = taUser.GetDataBySrchList(prefixText);

            try
            {
                foreach (dsSys.TBL_USER_INFORow dr in dtUser.Rows)
                {
                    string strItem = dr.User_Ref_No.ToString() + ":" + dr.User_Code.ToString() + ":" + dr.User_Name.ToString() + ":[EMP-" + dr.User_Ext_Data1.ToString() + "]";
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }

        [WebMethod]
        public string[] GetSrchEmp(string prefixText, int count)
        {
            List<string> items = new List<string>(count);

            var taEmpDet = new tblHrmsEmpTableAdapter();
            var dtEmpDet = new dsEmpDet.tblHrmsEmpDataTable();

            try
            {
                dtEmpDet = taEmpDet.GetSrchMatchEmpList(prefixText);
                foreach (dsEmpDet.tblHrmsEmpRow dr in dtEmpDet.Rows)
                {
                    string strItem = dr.EmpRefNo.ToString() + ":" + dr.EmpId.ToString() + ":" + dr.EmpFirstName.ToString() + " " + dr.EmpLastName.ToString();
                    items.Add(strItem);
                }
                return items.ToArray();
            }
            catch (Exception) { return items.ToArray(); }
        }
    }
}
