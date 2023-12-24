using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmLoadSlipWgtScaleUpdt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
        }        

        protected void GetLoadSlipData()
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = taLs.GetDataByLsRefNo(hfLsRefNo.Value.ToString());
            if (dtLS.Rows.Count > 0)
            {
                btnClear.Visible = true;
                btnPrint.Visible = true;

                txtLsTruckNo.Text = dtLS[0].LS_Truck_No.ToString();
                txtLsDateTime.Text = dtLS[0].IsLS_Date_TimeNull() ? "" : dtLS[0].LS_Date_Time.ToString();
                txtLsTruckSlNo.Text = dtLS[0].LS_Flag.ToString();
                txtLsDoDealerName.Text = dtLS[0].LS_DO_Dealer.ToString();
                txtLsDoItemName.Text = dtLS[0].LS_DO_Item.ToString();
                txtLsDoBagType.Text = dtLS[0].LS_DO_Bag_Type.ToString();
                txtLsDoQty.Text = Convert.ToInt32(dtLS[0].LS_DO_Qty) > 0 ? dtLS[0].LS_DO_Qty.ToString() : "";
                txtLsDoChlnNo.Text = dtLS[0].LS_DO_Chln.ToString();
                txtLsDoChallanUpdtTime.Text = dtLS[0].IsLS_DO_Chln_Updt_TimeNull() ? "" : dtLS[0].LS_DO_Chln_Updt_Time.ToString();
                txtLsWsEmptyWgt.Text = Convert.ToInt32(dtLS[0].LS_WS_Empty_Wgt) > 0 ? dtLS[0].LS_WS_Empty_Wgt.ToString() : "";
                txtLsWsEmptyWgtUpdtTime.Text = dtLS[0].IsLS_WS_Empty_Wgt_Updt_TimeNull() ? "" : dtLS[0].LS_WS_Empty_Wgt_Updt_Time.ToString();
                txtLsWsLoadedWgt.Text = Convert.ToInt32(dtLS[0].LS_WS_Load_Wgt) > 0 ? dtLS[0].LS_WS_Load_Wgt.ToString() : "";
                txtLsWsLoadedWgtUpdtTime.Text = dtLS[0].IsLS_WS_Load_Wgt_Updt_TimeNull() ? "" : dtLS[0].LS_WS_Load_Wgt_Updt_Time.ToString();
                txtLsTallyItemName.Text = dtLS[0].LS_TLY_Item_Name.ToString();
                txtLsTallyQty.Text = Convert.ToInt32(dtLS[0].LS_TLY_Item_Qty) > 0 ? dtLS[0].LS_TLY_Item_Qty.ToString() : "";
                txtLsTallyUpdtTime.Text = dtLS[0].IsLS_TLY_Updt_TimeNull() ? "" : dtLS[0].LS_TLY_Updt_Time.ToString();
                txtLsVatChlnStat.Text = dtLS[0].LS_VAT_Chln_Status.ToString();
                txtLsVatChlnUpdtTime.Text = dtLS[0].IsLS_VAT_Chln_Updt_TimeNull() ? "" : dtLS[0].LS_VAT_Chln_Updt_Time.ToString();
                txtLsDispGiftItemName.Text = dtLS[0].LS_DISP_Gift_Item_Name.ToString();
                txtLsDispAdvAmt.Text = Convert.ToInt32(dtLS[0].LS_DISP_Adv_Amt) > 0 ? dtLS[0].LS_DISP_Adv_Amt.ToString() : "";
                txtLsDispFuelQty.Text = Convert.ToInt32(dtLS[0].LS_DISP_Fuel_Qty) > 0 ? dtLS[0].LS_DISP_Fuel_Qty.ToString() : "";
                txtLsDispUpdtTime.Text = dtLS[0].IsLS_DISP_Updt_TimeNull() ? "" : dtLS[0].LS_DISP_Updt_Time.ToString();
                txtLsAccVerityStatus.Text = dtLS[0].LS_ACC_Verify_Status.ToString();
                txtLsAccVerifyUpdtTime.Text = dtLS[0].IsLS_ACC_Updt_TimeNull() ? "" : dtLS[0].LS_ACC_Updt_Time.ToString();
                txtLsGatePassTime.Text = dtLS[0].IsLS_GATE_PASS_TimeNull() ? "" : dtLS[0].LS_GATE_PASS_Time.ToString();
                txtEmptyWgt.Text = Convert.ToInt32(dtLS[0].LS_WS_Empty_Wgt) > 0 ? dtLS[0].LS_WS_Empty_Wgt.ToString() : "";
                txtLoadWgt.Text = Convert.ToInt32(dtLS[0].LS_WS_Load_Wgt) > 0 ? dtLS[0].LS_WS_Load_Wgt.ToString() : "";

                if (!dtLS[0].IsLS_DO_Updt_TimeNull() && dtLS[0].IsLS_WS_Empty_Wgt_Updt_TimeNull())
                    txtEmptyWgt.Enabled = true;
                else
                    txtEmptyWgt.Enabled = false;

                if (!dtLS[0].IsLS_TLY_Updt_TimeNull() && dtLS[0].IsLS_WS_Load_Wgt_Updt_TimeNull())
                    txtLoadWgt.Enabled = true;
                else
                    txtLoadWgt.Enabled = false;

                if (!dtLS[0].IsLS_DO_Updt_TimeNull() && (dtLS[0].IsLS_WS_Empty_Wgt_Updt_TimeNull() || dtLS[0].IsLS_WS_Load_Wgt_Updt_TimeNull()))
                    btnUpdateWgtScale.Enabled = true;
                else
                    btnUpdateWgtScale.Enabled = false;

                #region Get Load Slip Status
                if (!dtLS[0].IsLS_DO_Updt_TimeNull())
                {
                    if (!dtLS[0].IsLS_WS_Empty_Wgt_Updt_TimeNull())
                    {
                        if (!dtLS[0].IsLS_TLY_Updt_TimeNull())
                        {
                            if (!dtLS[0].IsLS_WS_Load_Wgt_Updt_TimeNull())
                            {
                                if (!dtLS[0].IsLS_DO_Chln_Updt_TimeNull())
                                {
                                    if (!dtLS[0].IsLS_VAT_Chln_Updt_TimeNull())
                                    {
                                        if (!dtLS[0].IsLS_DISP_Updt_TimeNull())
                                        {
                                            if (!dtLS[0].IsLS_ACC_Updt_TimeNull())
                                            {
                                                if (!dtLS[0].IsLS_GATE_PASS_TimeNull())
                                                    lblLsStatusMsg.Text = "Gate Passed at " + dtLS[0].LS_GATE_PASS_Time.ToString();
                                                else
                                                    lblLsStatusMsg.Text = "Waiting for Gate Pass.";
                                            }
                                            else
                                                lblLsStatusMsg.Text = "Waiting for Accounts Section Update.";

                                        }
                                        else
                                            lblLsStatusMsg.Text = "Waiting for Dispatch Section Update.";

                                    }
                                    else
                                        lblLsStatusMsg.Text = "Waiting for VAT Challan Update.";

                                }
                                else
                                    lblLsStatusMsg.Text = "Waiting for D/O Challan Update.";

                            }
                            else
                                lblLsStatusMsg.Text = "Waiting for Loaded Truck Weight Update.";

                        }
                        else
                            lblLsStatusMsg.Text = "Waiting for Tally Section Update.";

                    }
                    else
                        lblLsStatusMsg.Text = "Waiting for Empty Truck Weight Update.";
                }
                else
                    lblLsStatusMsg.Text = "Waiting for D/O Section Update.";
                #endregion
            }
            else
            {
                hfLsRefNo.Value = "";
                tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchLoadSlipRefNo();
        }

        private void SearchLoadSlipRefNo()
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = new dsTransMas.tbl_TrTr_Load_SlipDataTable();

            if (txtLoadSlipSearch.Text.Trim().Length <= 0) return;

            try
            {
                var lsRef = "";
                var srchWords = txtLoadSlipSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    lsRef = word;
                    break;
                }

                if (lsRef.Length > 0)
                {
                    dtLS = taLs.GetDataByLsRefNo(lsRef.ToString());
                    if (dtLS.Rows.Count > 0)
                    {
                        hfLsRefNo.Value = dtLS[0].LS_Ref_No.ToString();
                        GetLoadSlipData();
                    }
                    else
                    {
                        dtLS = taLs.GetDataByLsRefNo(lsRef.ToString().Substring(0, lsRef.ToString().Length - 1));
                        if (dtLS.Rows.Count > 0)
                        {
                            hfLsRefNo.Value = dtLS[0].LS_Ref_No.ToString();
                            GetLoadSlipData();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Load Slip Data Not Found.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtLoadSlipSearch.Text = "";
            hfLsRefNo.Value = "0";
            txtLsTruckNo.Text = "";
            txtLsDateTime.Text =
            txtLsTruckSlNo.Text = "";
            txtLsDoDealerName.Text = "";
            txtLsDoItemName.Text = "";
            txtLsDoBagType.Text = "";
            txtLsDoQty.Text = "";
            txtLsDoChlnNo.Text = "";
            txtLsDoChallanUpdtTime.Text = "";
            txtLsWsEmptyWgt.Text = "";
            txtLsWsEmptyWgtUpdtTime.Text = "";
            txtLsWsLoadedWgt.Text = "";
            txtLsWsLoadedWgtUpdtTime.Text = "";
            txtLsTallyItemName.Text = "";
            txtLsTallyQty.Text = "";
            txtLsTallyUpdtTime.Text = "";
            txtLsVatChlnStat.Text = "";
            txtLsVatChlnUpdtTime.Text = "";
            txtLsDispGiftItemName.Text = "";
            txtLsDispAdvAmt.Text = "";
            txtLsDispFuelQty.Text = "";
            txtLsDispUpdtTime.Text = "";
            txtLsAccVerityStatus.Text = "";
            txtLsAccVerifyUpdtTime.Text = "";
            txtLsGatePassTime.Text = "";
            txtEmptyWgt.Text = "";
            txtEmptyWgt.Enabled = false;
            txtLoadWgt.Text = "";
            txtLoadWgt.Enabled = false;
            lblLsStatusMsg.Text = "";
            btnClear.Visible = false;
            btnPrint.Visible = false;
            btnUpdateWgtScale.Enabled = false;
        }

        protected void btnUpdateWgtScale_Click(object sender, EventArgs e)
        {
            try
            {
                var taLs = new tbl_TrTr_Load_SlipTableAdapter();
                if (txtEmptyWgt.Enabled && txtEmptyWgt.Text.Trim().Length > 0)
                {
                    taLs.UpdateLsWsEmptyWgt(Convert.ToDecimal(txtEmptyWgt.Text.Trim()), DateTime.Now, "", hfLsRefNo.Value.ToString());

                    txtEmptyWgt.Text = "";
                    txtLoadWgt.Text = "";

                    GetLoadSlipData();                    

                    tblMsg.Rows[0].Cells[0].InnerText = "Data updated successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else if (txtLoadWgt.Enabled && txtLoadWgt.Text.Trim().Length > 0)
                {
                    taLs.UpdateLsWsLoadWgt(Convert.ToDecimal(txtLoadWgt.Text.Trim()), DateTime.Now, "", hfLsRefNo.Value.ToString());

                    txtEmptyWgt.Text = "";
                    txtLoadWgt.Text = "";

                    GetLoadSlipData();

                    tblMsg.Rows[0].Cells[0].InnerText = "Data updated successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                //else
                //{
                //    tblMsg.Rows[0].Cells[0].InnerText = "Enter valid data.";
                //    tblMsg.Rows[1].Cells[0].InnerText = "";
                //    ModalPopupExtenderMsg.Show();
                //}                
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = taLs.GetDataByLsRefNo(hfLsRefNo.Value.ToString());
            if (dtLS.Rows.Count > 0)
                if (!dtLS[0].IsLS_DO_Updt_TimeNull() && txtEmptyWgt.Text.Trim().Length <= 0)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = taLs.GetDataByLsRefNo(hfLsRefNo.Value.ToString());
            if (dtLS.Rows.Count > 0)
                if (!dtLS[0].IsLS_WS_Empty_Wgt_Updt_TimeNull() && txtLoadWgt.Text.Trim().Length <= 0)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            PrintLoadSlip(hfLsRefNo.Value.ToString());
        }

        private void PrintLoadSlip(string lsRefNo)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = new dsTransMas.tbl_TrTr_Load_SlipDataTable();

            int nPositionX = 0;
            int nPositionY = 0;
            int nTextHeight = 0;

            dtLS = taLs.GetDataByLsRefNo(lsRefNo.ToString());
            if (dtLS.Rows.Count > 0)
            {
                if (GlobalClass.clsBXLAPI.ConnectPrinter("BIXOLON SRP-F310II"))
                {
                    // Start Document
                    if (GlobalClass.clsBXLAPI.Start_Doc("Print Receipt") == false)
                        return;
                    // Start Page
                    GlobalClass.clsBXLAPI.Start_Page();

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontControl", 0, "x");		// ALIGNS TEXT TO THE CENTER                    

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Eastern Cement Industries Ltd.");

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Sumilpara, Siddirgonj, Narayangonj");

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Email:info@doreen.com, Web:www.doreen.com");

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Tel) 9665338, Fax) +88 02 8614645");

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "");

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA22", 0, "* LOAD SLIP *");

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA8", 0, "");

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontControl", 0, "x");	// ALIGNS TEXT TO THE CENTER

                    nPositionY += nTextHeight;
                    //nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "Barcode1", 0, "180700000002");	// PRINT BARCODE 
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "Barcode3", 0, dtLS[0].LS_Ref_No);	// PRINT BARCODE 

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Load Slip Ref No:" + dtLS[0].LS_Ref_No);	// PRINT BARCODE 

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontControl", 0, "w");	// ALIGNS TEXT TO THE LEFT

                    nPositionY += nTextHeight * 2;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "TRUCK NO: " + dtLS[0].LS_Truck_No.ToString());

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "DATE & TIME: " + (dtLS[0].IsLS_Date_TimeNull() ? "" : dtLS[0].LS_Date_Time.ToString()));

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "SL #: " + dtLS[0].LS_Flag.ToString());

                    nPositionY += nTextHeight;
                    nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");

                    if (!dtLS[0].IsLS_DO_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Dealer: " + dtLS[0].LS_DO_Dealer.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Item: " + dtLS[0].LS_DO_Item.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Bag Type: " + dtLS[0].LS_DO_Bag_Type.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Qty: " + dtLS[0].LS_DO_Qty.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_DO_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Challan No: " + dtLS[0].LS_DO_Chln.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + (dtLS[0].IsLS_DO_Chln_Updt_TimeNull() ? "" : dtLS[0].LS_DO_Chln_Updt_Time.ToString()));

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_WS_Empty_Wgt_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Empty Weight: " + dtLS[0].LS_WS_Empty_Wgt.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_WS_Empty_Wgt_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Loaded Weight: " + dtLS[0].LS_WS_Load_Wgt.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + (dtLS[0].IsLS_WS_Load_Wgt_Updt_TimeNull() ? "" : dtLS[0].LS_WS_Empty_Wgt_Updt_Time.ToString()));

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_TLY_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Item Name: " + dtLS[0].LS_TLY_Item_Name.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Qty: " + dtLS[0].LS_TLY_Item_Qty.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_TLY_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_VAT_Chln_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "VAT Update: " + dtLS[0].LS_VAT_Chln_Status.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_VAT_Chln_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_DISP_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Gift Item: " + dtLS[0].LS_DISP_Gift_Item_Name.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Gift Item Qty: " + dtLS[0].LS_Ext_Data1.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Advance Amt: " + dtLS[0].LS_DISP_Adv_Amt.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Fuel (Ltr.): " + dtLS[0].LS_DISP_Fuel_Qty.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_DISP_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_ACC_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Acc. Verified: " + dtLS[0].LS_ACC_Verify_Status.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_ACC_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_GATE_PASS_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintTrueFont(nPositionX, nPositionY, "Arial", 10, "Gate Out Time: " + dtLS[0].LS_GATE_PASS_Time.ToString(), true, 0, false, false);

                        nPositionY += nTextHeight;
                        nTextHeight = GlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    nPositionY += nTextHeight;
                    //nTextHeight = BXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "HAVE A NICE DAY!");
                    nTextHeight = GlobalClass.clsBXLAPI.PrintTrueFont(nPositionX, nPositionY, "Arial", 10, "HAVE A NICE DAY!", false, 0, true, false);

                    nPositionY += nTextHeight;
                    //nTextHeight = BXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Sale Date: 07/01/03");
                    nTextHeight = GlobalClass.clsBXLAPI.PrintTrueFont(nPositionX, nPositionY, "Arial", 10, "Print Date & Time: " + DateTime.Now.ToString(), false, 0, true, false);

                    GlobalClass.clsBXLAPI.End_Page();	// End Page
                    GlobalClass.clsBXLAPI.End_Doc();	// End Document
                }
            }
        }
    }
}