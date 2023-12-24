using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taDelOrd = new VIEW_DELIVERY_ORDER_TCTableAdapter();
            var dtDelOrd = taDelOrd.GetDataByPendDoForTC();
            gvDetails.DataSource = dtDelOrd;
            gvDetails.DataBind();
            gvDetails.SelectedIndex = -1;
        }
    }
}