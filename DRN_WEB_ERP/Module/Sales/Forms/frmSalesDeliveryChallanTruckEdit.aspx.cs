using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesDeliveryChallanTruckEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                var taTransType = new tblTransportTypeTableAdapter();
                var dtTransType = taTransType.GetData();
                ddlOrdTransMode.DataSource = dtTransType;
                ddlOrdTransMode.DataTextField = "Trans_Type_Name";
                ddlOrdTransMode.DataValueField = "Trans_Type_Ref";
                ddlOrdTransMode.DataBind();

                var taDist = new tblSalesDistrictTableAdapter();
                cboCustDist.DataSource = taDist.GetDataByAsc();
                cboCustDist.DataTextField = "DistName";
                cboCustDist.DataValueField = "DistRef";
                cboCustDist.DataBind();
                cboCustDist.Items.Insert(0, new ListItem("---Select---", "0"));

                cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));

                var taVslMas = new tbl_TrTr_Vsl_MasTableAdapter();
                cboTruckNo.DataSource = taVslMas.GetDataByDistributionTrans();
                cboTruckNo.DataTextField = "Vsl_Mas_No";
                cboTruckNo.DataValueField = "Vsl_Mas_Ref";
                cboTruckNo.DataBind();
                cboTruckNo.Items.Insert(0, new ListItem("---Select---", "0"));

                var curYear = DateTime.Now.Year;
                for (Int64 year = 2014; year <= (curYear); year++)
                {
                    cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }
                cboYear.SelectedValue = curYear.ToString();

                var curMonth = DateTime.Now.Month;
                for (int month = 1; month <= 12; month++)
                {
                    var monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    cboMonth.Items.Add(new ListItem(monthName.ToString(), month.ToString()));
                }
                cboMonth.Items.Insert(0, new ListItem("-----Select-----", "0"));
                cboMonth.SelectedValue = curMonth.ToString();

                Get_CLN_List();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void optTranBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optTranBy.SelectedValue == "2")
            {
                txtTruckNo.Text = "";
                txtTruckNo.Visible = false;
                cboTruckNo.SelectedIndex = 0;
                cboTruckNo.Visible = true;
            }
            else
            {
                txtTruckNo.Text = "";
                txtTruckNo.Visible = true;
                cboTruckNo.SelectedIndex = 0;
                cboTruckNo.Visible = false;
            }
        }

        protected void cboCustDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCustThana.Items.Clear();
            var taThana = new tblSalesThanaTableAdapter();
            cboCustThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboCustDist.SelectedValue));
            cboCustThana.DataTextField = "ThanaName";
            cboCustThana.DataValueField = "ThanaRef";
            cboCustThana.DataBind();
            cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();

            var dtInvDet = taInvDet.GetDataByChlnRef(ddlChallanList.SelectedValue.ToString());
            gvChlnDet.DataSource = dtInvDet;
            gvChlnDet.DataBind();
        }

        protected void ddlChallanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taSalesByRtl = new tblSalesByRetailerTableAdapter();

            var dtInvHdr = new dsInvTran.tbl_InTr_Trn_HdrDataTable();
            var dtInvDet = new dsInvTran.tbl_InTr_Trn_DetDataTable();
            var dtSalesByRtl = new dsSalesTran.tblSalesByRetailerDataTable();

            if (ddlChallanList.SelectedIndex == 0)
            {
                txtChallanNo.Text = "";
                txtChallanDate.Text = "";
                ddlOrdTransMode.SelectedIndex = 0;
                txtDelAddr.Text = "";
                optTranBy.SelectedValue = "1";
                cboTruckNo.SelectedIndex = 0;
                cboTruckNo.Visible = false;               
                cboCustDist.SelectedIndex = 0;
                cboCustThana.Items.Clear();
                cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));
                txtTruckNo.Text = "";
                txtTruckNo.Visible = true;
                txtDriverName.Text = "";
                txtDriverContact.Text = "";

                dtInvDet = taInvDet.GetDataByChlnRef("0");
                gvChlnDet.DataSource = dtInvDet;
                gvChlnDet.DataBind();
            }
            else
            {
                dtInvHdr = taInvHdr.GetDataByChlnRef(ddlChallanList.SelectedValue.ToString());
                if (dtInvHdr.Rows.Count > 0)
                {
                    txtChallanNo.Text =dtInvHdr[0].Trn_Hdr_Cno.ToString();
                    txtChallanDate.Text = dtInvHdr[0].Trn_Hdr_Date.ToString();
                    ddlOrdTransMode.SelectedValue = dtInvHdr[0].Trn_Hdr_Exp_Typ.ToString();
                    txtDelAddr.Text = dtInvHdr[0].Trn_Hdr_Com4.ToString();
                    optTranBy.SelectedValue = dtInvHdr[0].Trn_Hdr_Com9.ToString();
                    cboCustDist.SelectedValue = dtInvHdr[0].Trn_Hdr_Com5.ToString();

                    cboCustThana.Items.Clear();
                    var taThana = new tblSalesThanaTableAdapter();
                    cboCustThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboCustDist.SelectedValue));
                    cboCustThana.DataTextField = "ThanaName";
                    cboCustThana.DataValueField = "ThanaRef";
                    cboCustThana.DataBind();
                    cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));
                    cboCustThana.SelectedValue = dtInvHdr[0].Trn_Hdr_Com6.ToString();

                    txtTruckNo.Text = dtInvHdr[0].Trn_Hdr_Com1.ToString();
                    txtDriverName.Text = dtInvHdr[0].Trn_Hdr_Com2.ToString();
                    txtDriverContact.Text = dtInvHdr[0].Trn_Hdr_Com3.ToString();
                }

                dtSalesByRtl = taSalesByRtl.GetDataByTranRefNo(ddlChallanList.SelectedValue.ToString());
                if (dtSalesByRtl.Rows.Count > 0)
                {
                    if (dtSalesByRtl[0].Tran_Lno == 1)
                    {
                        txtRetailer1.Text = dtSalesByRtl[0].Tran_Adr_Ref_No.ToString();
                        txtDelQty1.Text = dtSalesByRtl[0].Tran_Qty.ToString();
                    }
                    if (dtSalesByRtl[0].Tran_Lno == 2)
                    {
                        txtRetailer2.Text = dtSalesByRtl[0].Tran_Adr_Ref_No.ToString();
                        txtDelQty1.Text = dtSalesByRtl[0].Tran_Qty.ToString();
                    }
                }

                dtInvDet = taInvDet.GetDataByChlnRef(ddlChallanList.SelectedValue.ToString());
                gvChlnDet.DataSource = dtInvDet;
                gvChlnDet.DataBind();
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_CLN_List();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_CLN_List();
        }

        private void Get_CLN_List()
        {
            gvChlnDet.DataSource = null;
            gvChlnDet.DataBind();

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var dtInvHdr = new dsInvTran.tbl_InTr_Trn_HdrDataTable();

            if (cboMonth.SelectedIndex == 0)
                dtInvHdr = taInvHdr.GetChallanListByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            else
                dtInvHdr = taInvHdr.GetChallanListByYearMonth(Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));

            ddlChallanList.DataSource = dtInvHdr;
            ddlChallanList.DataValueField = "Trn_Hdr_DC_No";
            ddlChallanList.DataTextField = "Trn_Hdr_Cno";
            ddlChallanList.DataBind();
            ddlChallanList.Items.Insert(0, "----------Select----------");
        }

        protected void btnUpdateVslNo_Click(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();

            try
            {
                var truckRef = "0";
                var truckNo = "";
                if (optTranBy.SelectedValue == "1")
                {
                    truckRef = "0";
                    truckNo = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToUpper(txtTruckNo.Text.Trim());
                }
                else
                {
                    truckRef = cboTruckNo.SelectedValue.ToString();
                    truckNo = cboTruckNo.SelectedValue == "0" ? "" : cboTruckNo.SelectedItem.ToString();
                }
                var driverName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtDriverName.Text.Trim());

                taInvHdr.UpdateChallanVehicleInfo(truckNo.ToString(), driverName.ToString(), txtDriverContact.Text, optTranBy.SelectedValue.ToString(), truckRef.ToString(),
                    DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlChallanList.SelectedValue.ToString());

                tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}