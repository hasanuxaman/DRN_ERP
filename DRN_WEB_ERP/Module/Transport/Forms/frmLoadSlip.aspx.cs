using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmLoadSlip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLs = taLs.GetMaxLsRef();
            var nextLsRef = dtLs == null ? 1 : Convert.ToInt32(dtLs) + 1;
            var nextLsRefNo = DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("00") + nextLsRef.ToString("00000000");

            txtLoadSlipRefNo.Text = nextLsRefNo.ToString();
            txtLoadSlipDate.Text = DateTime.Now.ToString();

            var taVslMas = new tbl_TrTr_Vsl_MasTableAdapter();
            cboTruckNo.DataSource = taVslMas.GetDataByDistributionTrans();
            cboTruckNo.DataTextField = "Vsl_Mas_No";
            cboTruckNo.DataValueField = "Vsl_Mas_Ref";
            cboTruckNo.DataBind();
            cboTruckNo.Items.Insert(0, new ListItem("---Select---", "0"));

            GenerateBarCode(txtLoadSlipRefNo.Text.Trim());

            //if (!GlobalClass.clsBXLAPI.ConnectPrinter("BIXOLON SRP-F310II"))
            //{
            //    btnConnectPrinter.Visible = true;
            //}
        }

        private void GenerateBarCode(string barCodeNo)
        {
            string barCode = barCodeNo.Trim();
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            using (Bitmap bitMap = new Bitmap(350, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitMap))
                {
                    Font oFont = new Font("IDAutomationHC39M", 16);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();

                    Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                plBarCode.Controls.Add(imgBarCode);
            }
        }

        protected void btnIsuLoadSlip_Click(object sender, EventArgs e)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();

            if (!Page.IsValid) return;

            try
            {
                var truckRef = "0";
                var truckNo = "";
                if (optTranBy.SelectedValue == "1")
                {
                    truckRef = cboTruckNo.SelectedValue.ToString();
                    truckNo = cboTruckNo.SelectedValue == "0" ? "" : cboTruckNo.SelectedItem.ToString();
                }
                else
                {
                    truckRef = "0";
                    truckNo = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToUpper(txtTruckNo.Text.Trim());
                }
               
                var dtLs = taLs.GetMaxLsRef();
                var nextLsRef = dtLs == null ? 1 : Convert.ToInt32(dtLs) + 1;
                var nextLsRefNo = DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("00") + nextLsRef.ToString("00000000");

                taLs.InsertLs(nextLsRefNo.ToString(), truckNo.ToString(), DateTime.Now, "User", "", "", "", 0, null, "", "", null, "", 0, null, "", 0, null, "", "", 0,
                    null, "", "", null, "", "", 0, 0, null, "", "", null, "", null, "", "", truckRef.ToString(), "", "", txtTruckSlNo.Text.Trim());

                var dtLsNew = taLs.GetMaxLsRef();
                var nextLsRefNew = dtLsNew == null ? 1 : Convert.ToInt32(dtLsNew) + 1;
                var nextLsRefNoNew = DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("00") + nextLsRef.ToString("00000000");

                txtLoadSlipRefNo.Text = nextLsRefNoNew.ToString();
                txtLoadSlipDate.Text = DateTime.Now.ToString();
                txtTruckNo.Text = "";
                txtTruckSlNo.Text = "";
                txtTruckNo.Visible = false;
                cboTruckNo.SelectedIndex = 0;
                cboTruckNo.Visible = true;
                optTranBy.SelectedValue = "1";
                btnIsuLoadSlip.Visible = true;
                btnIsuPrintLoadSlip.Visible = true;
                btnGateOut.Visible = false;
                btnRePrint.Visible = false;

                tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing Error.\n" + ex.Message;
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
                        lsRef = dtLS[0].LS_Ref_No.ToString();
                        GetLoadSlipData(lsRef.ToString());
                    }
                    else
                    {
                        dtLS = taLs.GetDataByLsRefNo(lsRef.ToString().Substring(0, lsRef.ToString().Length - 1));
                        if (dtLS.Rows.Count > 0)
                        {
                            lsRef = dtLS[0].LS_Ref_No.ToString();
                            GetLoadSlipData(lsRef.ToString());
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

        private void GetLoadSlipData(string lsRefNo)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = new dsTransMas.tbl_TrTr_Load_SlipDataTable();

            dtLS = taLs.GetDataByLsRefNo(lsRefNo.ToString());
            if (dtLS.Rows.Count > 0)
            {
                txtLoadSlipRefNo.Text = dtLS[0].LS_Ref_No.ToString();
                txtLoadSlipDate.Text = dtLS[0].LS_Date_Time.ToString();
                txtTruckSlNo.Text = dtLS[0].LS_Flag.ToString();
                if (dtLS[0].LS_Ext_Data2.ToString() == "0")
                {
                    optTranBy.SelectedValue = "2";
                    txtTruckNo.Text = dtLS[0].LS_Truck_No.ToString();
                    txtTruckNo.Visible = true;
                    cboTruckNo.SelectedIndex = 0;
                    cboTruckNo.Visible = false;
                }
                else
                {
                    optTranBy.SelectedValue = "1";
                    txtTruckNo.Text = "";
                    txtTruckNo.Visible = false;
                    cboTruckNo.SelectedValue = dtLS[0].LS_Ext_Data2.ToString();
                    cboTruckNo.Visible = true;
                }

                btnClear.Visible = true;
                btnRePrint.Visible = true;
                btnIsuLoadSlip.Visible = false;
                btnIsuPrintLoadSlip.Visible = false;

                if (!dtLS[0].IsLS_ACC_Updt_TimeNull())
                    if (!dtLS[0].IsLS_GATE_PASS_TimeNull())
                        btnGateOut.Visible = false;
                    else
                        btnGateOut.Visible = true;
                else
                    btnGateOut.Visible = false;

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

                GenerateBarCode(lsRefNo.ToString());
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtLoadSlipSearch.Text = "";

            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLs = taLs.GetMaxLsRef();
            var nextLsRef = dtLs == null ? 1 : Convert.ToInt32(dtLs) + 1;
            var nextLsRefNo = DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("00") + nextLsRef.ToString("00000000");

            txtLoadSlipRefNo.Text = nextLsRefNo.ToString();
            txtLoadSlipDate.Text = DateTime.Now.ToString();

            txtTruckNo.Text = "";
            txtTruckSlNo.Text = "";
            txtTruckNo.Visible = false;
            cboTruckNo.SelectedIndex = 0;
            cboTruckNo.Visible = true;
            optTranBy.SelectedValue = "1";
            lblLsStatusMsg.Text = "";
            btnClear.Visible = false;
            btnIsuLoadSlip.Visible = true;
            btnIsuPrintLoadSlip.Visible = true;
            btnGateOut.Visible = false;
            btnRePrint.Visible = false;

            GenerateBarCode(txtLoadSlipRefNo.Text.Trim());
        }

        protected void btnGateOut_Click(object sender, EventArgs e)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();

            taLs.UpdateLsGatePass(DateTime.Now, "", "1", txtLoadSlipRefNo.Text.Trim());

            var dtLS = taLs.GetDataByLsRefNo(txtLoadSlipRefNo.Text.Trim());
            if (dtLS.Rows.Count > 0)
            {
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

            btnGateOut.Visible = false;

            tblMsg.Rows[0].Cells[0].InnerText = "Data updated successfully.";
            tblMsg.Rows[1].Cells[0].InnerText = "";
            ModalPopupExtenderMsg.Show();
        }

        protected void btnIsuPrintLoadSlip_Click(object sender, EventArgs e)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();

            try
            {
                var truckRef = "0";
                var truckNo = "";
                if (optTranBy.SelectedValue == "1")
                {
                    truckRef = cboTruckNo.SelectedValue.ToString();
                    truckNo = cboTruckNo.SelectedValue == "0" ? "" : cboTruckNo.SelectedItem.ToString();
                }
                else
                {
                    truckRef = "0";
                    truckNo = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToUpper(txtTruckNo.Text.Trim());                    
                }

                var dtLs = taLs.GetMaxLsRef();
                var nextLsRef = dtLs == null ? 1 : Convert.ToInt32(dtLs) + 1;
                var nextLsRefNo = DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("00") + nextLsRef.ToString("00000000");

                taLs.InsertLs(nextLsRefNo.ToString(), truckNo.ToString(), DateTime.Now, "User", "", "", "", 0, null, "", "", null, "", 0, null, "", 0, null, "", "", 0,
                    null, "", "", null, "", "", 0, 0, null, "", "", null, "", null, "", "", truckRef.ToString(), "", "", txtTruckSlNo.Text.Trim());

                PrintLoadSlip(nextLsRefNo.ToString());

                var dtLsNew = taLs.GetMaxLsRef();
                var nextLsRefNew = dtLsNew == null ? 1 : Convert.ToInt32(dtLsNew) + 1;
                var nextLsRefNoNew = DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("00") + nextLsRef.ToString("00000000");

                txtLoadSlipRefNo.Text = nextLsRefNoNew.ToString();
                txtLoadSlipDate.Text = DateTime.Now.ToString();

                txtTruckNo.Text = "";
                txtTruckSlNo.Text = "";
                txtTruckNo.Visible = false;
                cboTruckNo.SelectedIndex = 0;
                cboTruckNo.Visible = true;
                optTranBy.SelectedValue = "1";
                btnIsuLoadSlip.Visible = true;
                btnIsuPrintLoadSlip.Visible = true;
                btnGateOut.Visible = false;
                btnRePrint.Visible = false;

                GenerateBarCode(txtLoadSlipRefNo.Text.Trim());

                tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void optTranBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optTranBy.SelectedValue == "1")
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

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (optTranBy.SelectedValue == "1" && cboTruckNo.SelectedValue == "0")
                args.IsValid = false;
            else if (optTranBy.SelectedValue == "2" && txtTruckNo.Text.Trim() == "")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void btnRePrint_Click(object sender, EventArgs e)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = new dsTransMas.tbl_TrTr_Load_SlipDataTable();            

            if (txtLoadSlipSearch.Text.Trim().Length <= 0) return;

            var lsRef = "";

            try
            {
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
                        lsRef = dtLS[0].LS_Ref_No.ToString();                        
                    }
                    else
                    {
                        dtLS = taLs.GetDataByLsRefNo(lsRef.ToString().Trim().Substring(0, lsRef.ToString().Trim().Length - 1));
                        if (dtLS.Rows.Count > 0)
                        {
                            lsRef = dtLS[0].LS_Ref_No.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }

                    PrintLoadSlip(lsRef.ToString());
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
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

        protected void btnConnectPrinter_Click(object sender, EventArgs e)
        {
            if (GlobalClass.clsBXLAPI.ConnectPrinter("BIXOLON SRP-F310II"))
            {
                btnConnectPrinter.Visible = false;
            }
        }
    }
}