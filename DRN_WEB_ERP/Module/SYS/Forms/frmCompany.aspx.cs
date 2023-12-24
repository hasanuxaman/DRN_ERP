using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.SYS.DataSet;
using DRN_WEB_ERP.Module.SYS.DataSet.dsSysTableAdapters;

namespace DRN_WEB_ERP
{
    public partial class frmCompany : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            SetCompanyGridData();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var taComp = new TBL_COMPANYTableAdapter();

            var dtMaxCompRefNo = taComp.GetMaxCompNo();
            Int32 nextCompRefNo = dtMaxCompRefNo == null ? 100001 : Convert.ToInt32(dtMaxCompRefNo) + 1;

            taComp.InsertCompany(nextCompRefNo, txtCompCode.Text.Trim(), txtCompName.Text.Trim(), txtCompRemarks.Text.Trim());
            SetCompanyGridData();
        }

        protected void SetCompanyGridData()
        {
            var taComp = new TBL_COMPANYTableAdapter();
            var dtComp = taComp.GetData();
            gvCompany.DataSource = dtComp;
            gvCompany.DataBind();
        }
    }
}