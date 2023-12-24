using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmCustRetailer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrchRtlr.ContextKey = "0";

            var taParRtl = new tblSalesPartyRtlTableAdapter();
            var dtMaxRtlRef = taParRtl.GetMaxRtlRef();
            var nextRtlRef = dtMaxRtlRef == null ? "01.100001" : "01." + (Convert.ToInt32(dtMaxRtlRef) + 1).ToString();
            txtRtlrRefNo.Text = "RT-" + nextRtlRef.ToString();

            var taCustType = new tblSalesPartyTypeTableAdapter();
            cboRtlrType.DataSource = taCustType.GetDataByAsc();
            cboRtlrType.DataTextField = "CustTypeName";
            cboRtlrType.DataValueField = "CustTypeRef";
            cboRtlrType.DataBind();
            cboRtlrType.Items.Insert(0, new ListItem("---Select---", "0"));

            var taTransLoc = new tbl_TrTr_Loc_MasTableAdapter();
            cboRtlrLoc.DataSource = taTransLoc.GetDataByAsc();
            cboRtlrLoc.DataTextField = "TrTr_Loc_Name";
            cboRtlrLoc.DataValueField = "TrTr_Loc_Ref_No";
            cboRtlrLoc.DataBind();
            cboRtlrLoc.Items.Insert(0, new ListItem("---Select---", "0"));

            var taDist = new tblSalesDistrictTableAdapter();
            cboRtlrDist.DataSource = taDist.GetDataByAsc();
            cboRtlrDist.DataTextField = "DistName";
            cboRtlrDist.DataValueField = "DistRef";
            cboRtlrDist.DataBind();
            cboRtlrDist.Items.Insert(0, new ListItem("---Select---", "0"));

            cboRtlrThana.Items.Insert(0, new ListItem("---Select---", "0"));

            var taSalesZone = new tblSalesZoneTableAdapter();
            cboRtlrZone.DataSource = taSalesZone.GetDataByAsc();
            cboRtlrZone.DataTextField = "SalesZoneName";
            cboRtlrZone.DataValueField = "SalesZoneRef";
            cboRtlrZone.DataBind();
            cboRtlrZone.Items.Insert(0, new ListItem("---Select---", "0"));

            var taSalesDsm = new View_Sales_DSMTableAdapter();
            var dtSalesDsm = taSalesDsm.GetActDsm();
            foreach (dsSalesMas.View_Sales_DSMRow dr in dtSalesDsm.Rows)
            {
                cboDsm.Items.Add(new ListItem(dr.Dsm_Short_Name.ToString() + " :: " + dr.SalesZoneName.ToString(), dr.Dsm_Ref.ToString()));                
            }
            cboDsm.Items.Insert(0, new ListItem("---Select---", "0"));

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var taParRtlView = new View_Sales_Perty_RtlTableAdapter();
            var dtParRtl = taParRtlView.GetData();
            Session["data"] = dtParRtl;
            SetCustomerGridData();
        }

        protected void cboCustDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taThana = new tblSalesThanaTableAdapter();
            cboRtlrThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboRtlrDist.SelectedValue));
            cboRtlrThana.DataTextField = "ThanaName";
            cboRtlrThana.DataValueField = "ThanaRef";
            cboRtlrThana.DataBind();
            cboRtlrThana.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("Save");

            var taCustRtl = new tblSalesPartyRtlTableAdapter();
            var taCustAdr = new tblSalesPartyAdrTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taCustRtl.Connection);

            try
            {
                #region Validate Form Data
                var custRef = "";
                var srchWords = txtCustAdrCode.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var dtPartyAdr = taCustAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count <= 0)
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Dealer Name.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Dealer Name.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                #endregion

                #region Get Sales Person Details
                var spRef = "";
                var srchSpWords = txtSalesPer.Text.Trim().Split(':');
                foreach (string word in srchSpWords)
                {
                    spRef = word;
                    break;
                }

                if (spRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(spRef, out result))
                    {
                        var taSp = new tblSalesPersonMasTableAdapter();
                        var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(spRef));
                        if (dtSp.Rows.Count > 0)
                            spRef = dtSp[0].Sp_Ref.ToString();
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                taCustRtl.AttachTransaction(myTran);
                taCustAdr.AttachTransaction(myTran);

                var rtlrName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtRtlrName.Text.Trim());
                var cpName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtRtlrCp.Text.Trim());
                var distance = txtDistance.Text.Trim().Length <= 0 ? "0" : txtDistance.Text.Trim();

                if (hfEditStatus.Value == "N")
                {
                    #region Insert Customer
                    var dtMaxRtlRef = taCustRtl.GetMaxRtlRef();
                    var nextRtlRef = dtMaxRtlRef == null ? "01.100001" : "01." + (Convert.ToInt32(dtMaxRtlRef) + 1).ToString();
                    var nextRtlRefNo = "RT-" + nextRtlRef.ToString();

                    taCustRtl.InsertPartyRtl(nextRtlRef, nextRtlRefNo, rtlrName.Trim(), Convert.ToInt32(cboRtlrType.SelectedValue), nextRtlRef.ToString(), txtRtlrAdr.Text.Trim(),
                        cpName.ToString(), txtRtlrCpDesig.Text.Trim(), Convert.ToInt32(cboRtlrDist.SelectedValue), Convert.ToInt32(cboRtlrThana.SelectedValue),
                        Convert.ToInt32(cboRtlrZone.SelectedValue), txtRtlrCell.Text.Trim(), txtRtlrPhone.Text.Trim(), txtRtlrFax.Text.Trim(), txtRtlrEmail.Text.Trim(), 0,
                        Convert.ToDecimal(0), Convert.ToInt32(0), "", "", "", custRef.ToString(), cboDsm.SelectedValue.ToString(), "", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), optListRtlrStatus.SelectedValue, "", "N",
                        0, 0, "", "", "", 0, spRef.ToString(), "", "", cboRtlrLoc.SelectedValue.ToString(), distance);

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    //btnSave.Text = "Save";
                    //hfEditStatus.Value = "N";
                    //hfRefNo.Value = "0";
                    //SetCustomerGridData();
                    //ClearData();
                    //txtSearchDlr.Text = "";
                    //txtSearchRtl.Text = "";
                    //btnClearSrch.Visible = false;
                    #endregion
                }
                else
                {
                    #region Update Customer
                    var dtCustRtl = taCustRtl.GetDataByPartyRtlRef(hfRefNo.Value);
                    if (dtCustRtl.Rows.Count > 0)
                    {
                        taCustRtl.UpdatePartyRtl(rtlrName.Trim(), Convert.ToInt32(cboRtlrType.SelectedValue), hfRefNo.Value.ToString(), txtRtlrAdr.Text.Trim(),
                            cpName.ToString(), txtRtlrCpDesig.Text.Trim(), Convert.ToInt32(cboRtlrDist.SelectedValue), Convert.ToInt32(cboRtlrThana.SelectedValue),
                            Convert.ToInt32(cboRtlrZone.SelectedValue), txtRtlrCell.Text.Trim(), txtRtlrPhone.Text.Trim(), txtRtlrFax.Text.Trim(), txtRtlrEmail.Text.Trim(), 0,
                            Convert.ToDecimal(0), Convert.ToInt32(0), "", "", "", custRef.ToString(), cboDsm.SelectedValue.ToString(), "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), optListRtlrStatus.SelectedValue, "", "N",
                            0, 0, "", "", "", 0, spRef.ToString(), "", "", cboRtlrLoc.SelectedValue.ToString(), distance, hfRefNo.Value);
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Retailer.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    //btnSave.Text = "Save";
                    //hfEditStatus.Value = "N";
                    //hfRefNo.Value = "0";
                    //SetCustomerGridData();
                    //ClearData();
                    //txtSearchDlr.Text = "";
                    //txtSearchRtl.Text = "";
                    //btnClearSrch.Visible = false;
                    #endregion
                }

                ClearData();

                //var taParRtlView = new View_Sales_Perty_RtlTableAdapter();
                //var dtParRtl = taParRtlView.GetData();
                //Session["data"] = dtParRtl;
                //SetCustomerGridData();

                var taPartyRtl = new tblSalesPartyRtlTableAdapter();
                var taParRtlView = new View_Sales_Perty_RtlTableAdapter();

                var dtPartyRtlView = new dsSalesMas.View_Sales_Perty_RtlDataTable();

                var dlrRef = "";
                var rtlRef = "";
                if (txtSearchDlr.Text.Trim().Length > 0)
                {
                    var srchWordsdlr = txtSearchDlr.Text.Trim().Split(':');
                    foreach (string word in srchWordsdlr)
                    {
                        dlrRef = word;
                        break;
                    }

                    if (dlrRef.Length > 0)
                    {
                        var dtPartyAdr = taCustAdr.GetDataByPartyAdrRef(Convert.ToInt32(dlrRef));
                        if (dtPartyAdr.Rows.Count > 0)
                            dlrRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                    }
                }


                var srchWordsrtlr = txtSearchRtl.Text.Trim().Split(':');
                foreach (string word in srchWordsrtlr)
                {
                    rtlRef = word;
                    break;
                }

                if (rtlRef.Length > 0)
                {
                    var dtPartyRtl = taPartyRtl.GetDataByPartyRtlRef(rtlRef);
                    if (dtPartyRtl.Rows.Count > 0)
                        rtlRef = dtPartyRtl[0].Par_Rtl_Ref.ToString();
                }

                if (dlrRef.Length > 0)
                {
                    if (rtlRef.Length > 0)
                    {
                        dtPartyRtlView = taParRtlView.GetDataByDlrRtlRef(dlrRef, rtlRef);
                        Session["data"] = dtPartyRtlView;
                        SetCustomerGridData();
                        btnClearSrch.Visible = true;
                    }
                    else
                    {
                        dtPartyRtlView = taParRtlView.GetDataByDlrRef(dlrRef);
                        Session["data"] = dtPartyRtlView;
                        SetCustomerGridData();
                        btnClearSrch.Visible = true;
                    }
                }
                else
                {
                    if (rtlRef.Length > 0)
                    {
                        dtPartyRtlView = taParRtlView.GetDataByRtlRef(rtlRef);
                        Session["data"] = dtPartyRtlView;
                        SetCustomerGridData();
                        btnClearSrch.Visible = true;
                    }
                }                
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private void ClearData()
        {
            var taParRtl = new tblSalesPartyRtlTableAdapter();
            var dtMaxRtlRef = taParRtl.GetMaxRtlRef();
            var nextRtlRef = dtMaxRtlRef == null ? "01.100001" : "01." + (Convert.ToInt32(dtMaxRtlRef) + 1).ToString();
            txtRtlrRefNo.Text = "RT-" + nextRtlRef.ToString();

            txtRtlrName.Text = "";
            txtRtlrAdr.Text = "";
            cboRtlrLoc.SelectedIndex = 0;
            txtDistance.Text = "";
            txtRtlrCp.Text = "";
            txtRtlrCpDesig.Text = "";
            txtRtlrCell.Text = "";
            txtRtlrPhone.Text = "";
            txtRtlrFax.Text = "";
            txtRtlrEmail.Text = "";
            txtCustAdrCode.Text = "";
            txtSalesPer.Text = "";
            txtCustAdrCode.Enabled = true;

            cboDsm.SelectedIndex = 0;

            //cboRtlrType.SelectedIndex = 0;
            cboRtlrDist.SelectedIndex = 0;
            cboRtlrThana.Items.Clear();
            cboRtlrThana.Items.Insert(0, new ListItem("---Select---", "0"));
            cboRtlrThana.SelectedIndex = 0;
            cboRtlrZone.SelectedIndex = 0;

            optListRtlrStatus.SelectedValue = "1";

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var taParRtlView = new View_Sales_Perty_RtlTableAdapter();
            var dtParRtl = taParRtlView.GetData();
            Session["data"] = dtParRtl;
            SetCustomerGridData();            
        }

        protected void SetCustomerGridData()
        {
            //var taParRtl = new tblSalesPartyRtlTableAdapter();
            //var dtParRtl = taParRtl.GetData();
            var dtParRtl = Session["data"];
            gvRtl.DataSource = dtParRtl;
            gvRtl.DataBind();            
            gvRtl.SelectedIndex = -1;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
            var taPartyRtl = new tblSalesPartyRtlTableAdapter();
            var taParRtlView = new View_Sales_Perty_RtlTableAdapter();

            var dtPartyRtlView = new dsSalesMas.View_Sales_Perty_RtlDataTable();

            if (txtSearchDlr.Text.Trim().Length <= 0 && txtSearchRtl.Text.Trim().Length <= 0) return;

            try
            {
                var dlrRef = "";
                var rtlRef = "";
                if (txtSearchDlr.Text.Trim().Length > 0)
                {
                    var srchWordsdlr = txtSearchDlr.Text.Trim().Split(':');
                    foreach (string word in srchWordsdlr)
                    {
                        dlrRef = word;
                        break;
                    }

                    if (dlrRef.Length > 0)
                    {
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(dlrRef));
                        if (dtPartyAdr.Rows.Count > 0)
                            dlrRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                    }
                }


                var srchWordsrtlr = txtSearchRtl.Text.Trim().Split(':');
                foreach (string word in srchWordsrtlr)
                {
                    rtlRef = word;
                    break;
                }

                if (rtlRef.Length > 0)
                {
                    var dtPartyRtl = taPartyRtl.GetDataByPartyRtlRef(rtlRef);
                    if (dtPartyRtl.Rows.Count > 0)
                        rtlRef = dtPartyRtl[0].Par_Rtl_Ref.ToString();
                }

                if (dlrRef.Length > 0)
                {
                    if (rtlRef.Length > 0)
                    {
                        dtPartyRtlView = taParRtlView.GetDataByDlrRtlRef(dlrRef, rtlRef);
                        Session["data"] = dtPartyRtlView;
                        SetCustomerGridData();
                        btnClearSrch.Visible = true;
                    }
                    else
                    {
                        dtPartyRtlView = taParRtlView.GetDataByDlrRef(dlrRef);
                        Session["data"] = dtPartyRtlView;
                        SetCustomerGridData();
                        btnClearSrch.Visible = true;
                    }
                }
                else
                {
                    if (rtlRef.Length > 0)
                    {
                        dtPartyRtlView = taParRtlView.GetDataByRtlRef(rtlRef);
                        Session["data"] = dtPartyRtlView;
                        SetCustomerGridData();
                        btnClearSrch.Visible = true;
                    }                    
                }                
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            txtSearchDlr.Text = "";
            txtSearchRtl.Text = "";
            var taParRtlView = new View_Sales_Perty_RtlTableAdapter();
            var dtParRtl = taParRtlView.GetData();
            Session["data"] = dtParRtl;
            SetCustomerGridData();            
            btnClearSrch.Visible = false;
        }

        protected void gvRtl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);               
            }
        }

        protected void gvRtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRtl.PageIndex = e.NewPageIndex;
            SetCustomerGridData();
        }

        protected void gvRtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvRtl.SelectedIndex;

            if (indx != -1)
            {                
                var taCustRtlr = new tblSalesPartyRtlTableAdapter();

                try
                {
                    var userRef=Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString();
                    var userEmpRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                    HiddenField hfCustRef = (HiddenField)gvRtl.Rows[indx].FindControl("hfCustRef");
                    hfRefNo.Value = hfCustRef.Value;
                    hfEditStatus.Value = "Y";
                    btnSave.Text = "Edit";

                    var dtCustRtlr = taCustRtlr.GetDataByPartyRtlRef(hfRefNo.Value.ToString());
                    if (dtCustRtlr.Rows.Count > 0)
                    {
                        txtRtlrRefNo.Text = dtCustRtlr[0].Par_Rtl_Ref_No.ToString();
                        txtRtlrName.Text = dtCustRtlr[0].Par_Rtl_Name.ToString();
                        txtRtlrAdr.Text = dtCustRtlr[0].Par_Rtl_Addr.ToString();                        
                        txtDistance.Text = dtCustRtlr[0].Par_Rtl_Ext_Data5.ToString();
                        txtRtlrCp.Text = dtCustRtlr[0].Par_Rtl_Cont_Per.ToString();
                        txtRtlrCpDesig.Text = dtCustRtlr[0].Par_Rtl_Desig.ToString();
                        txtRtlrCell.Text = dtCustRtlr[0].Par_Rtl_Cell_No.ToString();
                        txtRtlrPhone.Text = dtCustRtlr[0].Par_Rtl_Tel_No.ToString();
                        txtRtlrFax.Text = dtCustRtlr[0].Par_Rtl_Fax_No.ToString();
                        txtRtlrEmail.Text = dtCustRtlr[0].Par_Rtl_Email_Id.ToString();

                        var custAdrRef = dtCustRtlr[0].Par_Rtl_Adr_Code.ToString();

                        var taCust = new tblSalesPartyAdrTableAdapter();
                        var dtCust = taCust.GetDataByPartyAdrRef(Convert.ToInt32(custAdrRef.Length > 0 ? custAdrRef : "0"));
                        if (dtCust.Rows.Count > 0)
                            txtCustAdrCode.Text = dtCust[0].Par_Adr_Ref + ":" + dtCust[0].Par_Adr_Ref_No + ":" + dtCust[0].Par_Adr_Name;
                        else
                            txtCustAdrCode.Text = "";

                        var taSp = new tblSalesPersonMasTableAdapter();
                        var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(dtCustRtlr[0].Par_Rtl_Ext_Data1 == "" ? "0" : dtCustRtlr[0].Par_Rtl_Ext_Data1));
                        if (dtSp.Rows.Count > 0)
                            txtSalesPer.Text = dtSp[0].Sp_Ref + ":" + dtSp[0].Sp_Short_Name + ":" + dtSp[0].Sp_Full_Name;
                        else
                            txtSalesPer.Text = "";

                        //if (userRef == "100001" || userEmpRef == "000011")
                        //{
                        //    txtRtlrName.Enabled = true;
                        //    txtCustAdrCode.Enabled = true;
                        //}
                        //else
                        //{
                        //    txtRtlrName.Enabled = false;
                        //    txtCustAdrCode.Enabled = false;
                        //}

                        try
                        {
                            //cboRtlrType.SelectedIndex = cboRtlrType.Items.IndexOf(cboRtlrType.Items.FindByValue(dtCustRtlr[0].IsPar_Rtl_TypeNull() ? "0" : dtCustRtlr[0].Par_Rtl_Type.ToString()));

                            cboRtlrLoc.SelectedIndex = cboRtlrLoc.Items.IndexOf(cboRtlrLoc.Items.FindByValue(dtCustRtlr[0].IsPar_Rtl_Ext_Data4Null() ? "0" : dtCustRtlr[0].Par_Rtl_Ext_Data4.ToString()));

                            cboRtlrDist.SelectedIndex = cboRtlrDist.Items.IndexOf(cboRtlrDist.Items.FindByValue(dtCustRtlr[0].IsPar_Rtl_DistNull() ? "0" : dtCustRtlr[0].Par_Rtl_Dist.ToString()));
                            //cboRtlrDist.SelectedValue = dtCustRtlr[0].Par_Rtl_Dist.ToString();                            

                            cboRtlrZone.SelectedIndex = cboRtlrZone.Items.IndexOf(cboRtlrZone.Items.FindByValue(dtCustRtlr[0].IsPar_Rtl_Sale_ZoneNull() ? "0" : dtCustRtlr[0].Par_Rtl_Sale_Zone.ToString()));

                            cboDsm.SelectedIndex = cboDsm.Items.IndexOf(cboDsm.Items.FindByValue(dtCustRtlr[0].IsPar_Rtl_Sls_PerNull() ? "0" : dtCustRtlr[0].Par_Rtl_Sls_Per.ToString()));

                            cboRtlrType.SelectedIndex = cboRtlrType.Items.IndexOf(cboRtlrType.Items.FindByValue(dtCustRtlr[0].IsPar_Rtl_TypeNull() ? "0" : dtCustRtlr[0].Par_Rtl_Type.ToString()));

                            optListRtlrStatus.SelectedValue = dtCustRtlr[0].Par_Rtl_Status.ToString();

                            var taThana = new tblSalesThanaTableAdapter();
                            cboRtlrThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboRtlrDist.SelectedValue));
                            cboRtlrThana.DataTextField = "ThanaName";
                            cboRtlrThana.DataValueField = "ThanaRef";
                            cboRtlrThana.DataBind();
                            cboRtlrThana.Items.Insert(0, new ListItem("---Select---", "0"));

                            cboRtlrThana.SelectedIndex = cboRtlrThana.Items.IndexOf(cboRtlrThana.Items.FindByValue(dtCustRtlr[0].IsPar_Rtl_ThanaNull() ? "0" : dtCustRtlr[0].Par_Rtl_Thana.ToString()));
                            //cboRtlrThana.SelectedValue = dtCustRtlr[0].Par_Rtl_Thana.ToString();
                        }
                        catch (Exception ex) 
                        {
                            ClearData();
                            tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClearData();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void gvRtl_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortexp = e.SortExpression;
            Session["sortexp"] = sortexp;
            if (Session["sortDirection"] != null && Session["sortDirection"].ToString() == SortDirection.Descending.ToString())
            {
                Session["sortDirection"] = SortDirection.Ascending;
                ConvertSortDirection(sortexp, "ASC");
            }
            else
            {
                Session["sortDirection"] = SortDirection.Descending;
                ConvertSortDirection(sortexp, "DESC");
            }
        }

        private void ConvertSortDirection(string soreExpression, string p)
        {
            DataTable dataTable = Session["data"] as DataTable;
            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = soreExpression + " " + p;
                gvRtl.DataSource = dataView;
                gvRtl.DataBind();
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var custRef = "";
                var srchWords = txtCustAdrCode.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var qryStr = "SELECT [Par_Rtl_Ref_No] as RTL_Code, [Par_Rtl_Name] as RTL_Name, [Par_Rtl_Addr] as RTL_Address, [Par_Rtl_Cont_Per] as RTL_CP_Name, " +
                             "[Par_Rtl_Desig] as RTL_CP_Desig, [Par_Rtl_Cell_No] as RTL_Cell_No, [Par_Rtl_Tel_No] as RTL_Tel_No, [Par_Rtl_Fax_No] as RTL_Fax_No, " +
                             "[Par_Rtl_Email_Id] as RTL_Email_Id, [SalesZoneName] as RTL_Sales_Zone, [DistName] as RTL_District, [ThanaName] as RTL_Thana, " +
                             "[Dsm_Full_Name] as RTL_DSM_Name, [Sp_Full_name] as MPO_Name, [Par_Adr_Name] as RTL_Dealer_Name, [Par_Rtl_Adr_Code] as RTL_Dealer_Code " +
                             "FROM [DRN].[dbo].[tblSalesPartyRtl] left outer join [DRN].[dbo].[tblSalesPartyAdr] on [Par_Rtl_Adr_Code]=CONVERT(varchar(50), [Par_Adr_Ref]) " +
                             "left outer join [DRN].[dbo].[tblSalesDistrict] on [Par_Rtl_Dist]=[DistRef] left outer join [DRN].[dbo].[tblSalesThana] " +
                             "on [Par_Rtl_Thana]=[ThanaRef] left outer join [DRN].[dbo].[tblSalesZone] on [Par_Rtl_Sale_Zone]=[SalesZoneRef] " +
                             "left outer join [DRN].[dbo].[tblSalesDsmMas] on [Par_Rtl_Sls_Per]=[Dsm_Ref] left outer join [DRN].[dbo].[tblSalesPersonMas] " +
                             "on [Par_Rtl_Ext_data1]=[Sp_Ref] order by [Par_Rtl_Name]";

                SqlCommand cmd = new SqlCommand(qryStr);
                DataTable dt = GetData(cmd);

                if (dt.Rows.Count > 65535)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                    return;
                }

                //First Method
                #region With Formating
                Response.Clear();
                Response.Buffer = true;
                string filename = String.Format("RetailerList_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //Create a dummy GridView
                    GridView GridView1 = new GridView();
                    GridView1.AllowPaging = false;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    GridView1.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in GridView1.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView1.HeaderStyle.BackColor;
                    }

                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        row.BackColor = System.Drawing.Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView1.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
                #endregion
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var spRef = "";
                var srchWords = txtSalesPer.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    spRef = word;
                    break;
                }

                if (spRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(spRef, out result))
                    {
                        var taSp = new tblSalesPersonMasTableAdapter();
                        var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(spRef));
                        if (dtSp.Rows.Count > 0)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void cboRtlrLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taLocMas = new tbl_TrTr_Loc_MasTableAdapter();
            var dtLocMasNew = taLocMas.GetDataByLocRefNo(cboRtlrLoc.SelectedValue.ToString());
            if (dtLocMasNew.Rows.Count > 0)
                txtDistance.Text = dtLocMasNew[0].TrTr_Loc_Distance_Km.ToString();
        }
    }
}