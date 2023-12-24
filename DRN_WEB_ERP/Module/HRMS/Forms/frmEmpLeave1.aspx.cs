using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmEmpLeave1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taLvMas = new tblHrmsLeaveMasTableAdapter();
            cboLeaveType.DataSource = taLvMas.GetDataByAsc();
            cboLeaveType.DataValueField = "LvMasRefNo";
            cboLeaveType.DataTextField = "LvMasName";
            cboLeaveType.DataBind();
            cboLeaveType.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully";
            tblMsg.Rows[1].Cells[0].InnerText = "";
            ModalPopupExtenderMsg.Show();
        }
    }
}