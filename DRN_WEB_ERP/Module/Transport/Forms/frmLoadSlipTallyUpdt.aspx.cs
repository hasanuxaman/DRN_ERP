using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmLoadSlipTallyUpdt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtender1.ContextKey = "FG";
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();

            try
            {
                var itemRef = "";
                var srchWords = txtTallyItemName.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    itemRef = word;
                    break;
                }

                if (itemRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(itemRef, out result))
                    {
                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                        if (dtItem.Rows.Count > 0)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                args.IsValid = false;
            }
        }

        protected void GetLoadSlipData()
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = taLs.GetDataByLsRefNo(hfLsRefNo.Value.ToString());
            if (dtLS.Rows.Count > 0)
            {
                btnClear.Visible = true;

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
                if (dtLS[0].IsLS_TLY_Updt_TimeNull())
                {
                    txtTallyItemName.Text = dtLS[0].LS_DO_Item.ToString();
                    txtTallyQty.Text = Convert.ToInt32(dtLS[0].LS_DO_Qty) > 0 ? dtLS[0].LS_DO_Qty.ToString() : "";
                }
                else
                {
                    txtTallyItemName.Text = dtLS[0].LS_TLY_Item_Name.ToString();
                    txtTallyQty.Text = Convert.ToInt32(dtLS[0].LS_TLY_Item_Qty) > 0 ? dtLS[0].LS_TLY_Item_Qty.ToString() : "";
                }

                if (!dtLS[0].IsLS_WS_Empty_Wgt_Updt_TimeNull() && dtLS[0].IsLS_TLY_Updt_TimeNull())
                    txtTallyItemName.Enabled = true;
                else
                    txtTallyItemName.Enabled = false;

                if (!dtLS[0].IsLS_WS_Empty_Wgt_Updt_TimeNull() && dtLS[0].IsLS_TLY_Updt_TimeNull())
                    txtTallyQty.Enabled = true;
                else
                    txtTallyQty.Enabled = false;

                if (!dtLS[0].IsLS_WS_Empty_Wgt_Updt_TimeNull() && dtLS[0].IsLS_TLY_Updt_TimeNull())
                    btnUpdateTally.Enabled = true;
                else
                    btnUpdateTally.Enabled = false;

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
            txtTallyItemName.Text = "";
            txtTallyItemName.Enabled = false;
            txtTallyQty.Text = "";
            txtTallyQty.Enabled = false;
            lblLsStatusMsg.Text = "";
            btnClear.Visible = false;
            btnUpdateTally.Enabled = false;
        }

        protected void btnUpdateTally_Click(object sender, EventArgs e)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();

            taLs.UpdateLsTally(txtTallyItemName.Text.Trim(), Convert.ToDecimal(txtTallyQty.Text.Trim()), DateTime.Now, "", hfLsRefNo.Value.ToString());

            txtTallyItemName.Text = "";
            txtTallyQty.Text = "";

            GetLoadSlipData();

            tblMsg.Rows[0].Cells[0].InnerText = "Data updated successfully.";
            tblMsg.Rows[1].Cells[0].InnerText = "";
            ModalPopupExtenderMsg.Show();
        }
    }
}