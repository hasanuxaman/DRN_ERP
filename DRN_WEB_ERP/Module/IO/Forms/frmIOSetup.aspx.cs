using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.IO.Forms
{
    public partial class frmIOSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taEmpList = new View_Emp_BascTableAdapter();
            var dt = taEmpList.GetDataByAsc();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var empRef = dt.Rows[i]["EmpRefNo"].ToString();
                    var supNameId = dt.Rows[i]["EmpName"].ToString() + " - [" + dt.Rows[i]["EmpId"].ToString() + "]";
                    DropDownList1.Items.Add(new ListItem(supNameId, empRef));
                    DropDownList2.Items.Add(new ListItem(supNameId, empRef));
                    DropDownList3.Items.Add(new ListItem(supNameId, empRef));
                    DropDownList4.Items.Add(new ListItem(supNameId, empRef));
                    DropDownList5.Items.Add(new ListItem(supNameId, empRef));
                    DropDownList6.Items.Add(new ListItem(supNameId, empRef));
                    DropDownList7.Items.Add(new ListItem(supNameId, empRef));
                    DropDownList8.Items.Add(new ListItem(supNameId, empRef));
                    DropDownList9.Items.Add(new ListItem(supNameId, empRef));
                    DropDownList10.Items.Add(new ListItem(supNameId, empRef));
                }
            }
            DropDownList1.Items.Insert(0, new ListItem("---Select---", "0"));
            DropDownList2.Items.Insert(0, new ListItem("---Select---", "0"));
            DropDownList3.Items.Insert(0, new ListItem("---Select---", "0"));
            DropDownList4.Items.Insert(0, new ListItem("---Select---", "0"));
            DropDownList5.Items.Insert(0, new ListItem("---Select---", "0"));
            DropDownList6.Items.Insert(0, new ListItem("---Select---", "0"));
            DropDownList7.Items.Insert(0, new ListItem("---Select---", "0"));
            DropDownList8.Items.Insert(0, new ListItem("---Select---", "0"));
            DropDownList9.Items.Insert(0, new ListItem("---Select---", "0"));
            DropDownList10.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}