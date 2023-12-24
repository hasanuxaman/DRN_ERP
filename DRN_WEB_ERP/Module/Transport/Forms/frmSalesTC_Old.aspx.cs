using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmSalesTC_Old : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSelectDo_Click(object sender, EventArgs e)
        {
            var taDelOrd = new VIEW_DELIVERY_ORDER_TCTableAdapter();
            var dtDelOrd = taDelOrd.GetDataByPendDoForTC();
            gvPendDoDet.DataSource = dtDelOrd;
            gvPendDoDet.DataBind();
            gvPendDoDet.SelectedIndex = -1;
            ModalPopupExtenderDoList.Show();
        }

        protected void ButtonOk_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearchDo_Click(object sender, EventArgs e)
        {
            var taDelOrd = new VIEW_DELIVERY_ORDER_TCTableAdapter();
            var dtDelOrd = taDelOrd.GetDataByPendDoForTCSearch(txtSearchDo.Text.Trim());
            gvPendDoDet.DataSource = dtDelOrd;
            gvPendDoDet.DataBind();
            gvPendDoDet.SelectedIndex = -1;
            ModalPopupExtenderDoList.Show();
        }
    }
}