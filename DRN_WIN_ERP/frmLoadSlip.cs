using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using DRN_WIN_ERP.DataSets;
using DRN_WIN_ERP.DataSets.dsWinSalesMasTableAdapters;
using DRN_WIN_ERP.DataSets.dsWinSalesTranTableAdapters;
using DRN_WIN_ERP.DataSets.dsWinInvMasTableAdapters;
using DRN_WIN_ERP.DataSets.dsWinInvTranTableAdapters;
using DRN_WIN_ERP.DataSets.dsWinAccTranTableAdapters;
using DRN_WIN_ERP.DataSets.dsWinTransMasTableAdapters;

namespace DRN_WIN_ERP
{
    public partial class frmLoadSlip : Form
    {
        public frmLoadSlip()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintLoadSlip("180700000001");
        }

        private void PrintLoadSlip(string lsRefNo)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = new dsWinTransMas.tbl_TrTr_Load_SlipDataTable();

            int nPositionX = 0;
            int nPositionY = 0;
            int nTextHeight = 0;

            dtLS = taLs.GetDataByLsRefNo(lsRefNo.ToString());
            if (dtLS.Rows.Count > 0)
            {
                if (WinGlobalClass.clsBXLAPI.ConnectPrinter("BIXOLON SRP-F310II"))
                {
                    // Start Document
                    if (WinGlobalClass.clsBXLAPI.Start_Doc("Print Receipt") == false)
                        return;
                    // Start Page
                    WinGlobalClass.clsBXLAPI.Start_Page();

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontControl", 0, "x");		// ALIGNS TEXT TO THE CENTER                    

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Eastern Cement Industries Ltd.");

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Sumilpara, Siddirgonj, Narayangonj");

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Email:info@doreen.com, Web:www.doreen.com");

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Tel) 9665338, Fax) +88 02 8614645");

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "");

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA22", 0, "* LOAD SLIP *");

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA8", 0, "");

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontControl", 0, "x");	// ALIGNS TEXT TO THE CENTER

                    nPositionY += nTextHeight;
                    //nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "Barcode1", 0, "180700000002");	// PRINT BARCODE 
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "Barcode3", 0, dtLS[0].LS_Ref_No);	// PRINT BARCODE 

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Load Slip Ref No:" + dtLS[0].LS_Ref_No);	// PRINT BARCODE 

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontControl", 0, "w");	// ALIGNS TEXT TO THE LEFT

                    nPositionY += nTextHeight * 2;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "TRUCK NO: " + dtLS[0].LS_Truck_No.ToString());

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "DATE & TIME: " + (dtLS[0].IsLS_Date_TimeNull() ? "" : dtLS[0].LS_Date_Time.ToString()));

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "SL #: " + dtLS[0].LS_Flag.ToString());

                    nPositionY += nTextHeight;
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");

                    if (!dtLS[0].IsLS_DO_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Dealer: " + dtLS[0].LS_DO_Dealer.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Item: " + dtLS[0].LS_DO_Item.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Bag Type: " + dtLS[0].LS_DO_Bag_Type.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Qty: " + dtLS[0].LS_DO_Qty.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_DO_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Challan No: " + dtLS[0].LS_DO_Chln.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + (dtLS[0].IsLS_DO_Chln_Updt_TimeNull() ? "" : dtLS[0].LS_DO_Chln_Updt_Time.ToString()));

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_WS_Empty_Wgt_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Empty Weight: " + dtLS[0].LS_WS_Empty_Wgt.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_WS_Empty_Wgt_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Loaded Weight: " + dtLS[0].LS_WS_Load_Wgt.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + (dtLS[0].IsLS_WS_Load_Wgt_Updt_TimeNull() ? "" : dtLS[0].LS_WS_Empty_Wgt_Updt_Time.ToString()));

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_TLY_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Item Name: " + dtLS[0].LS_TLY_Item_Name.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Qty: " + dtLS[0].LS_TLY_Item_Qty.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_TLY_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_VAT_Chln_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "VAT Update: " + dtLS[0].LS_VAT_Chln_Status.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_VAT_Chln_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_DISP_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Gift Item: " + dtLS[0].LS_DISP_Gift_Item_Name.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Gift Item Qty: " + dtLS[0].LS_Ext_Data1.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Advance Amt: " + dtLS[0].LS_DISP_Adv_Amt.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Fuel (Ltr.): " + dtLS[0].LS_DISP_Fuel_Qty.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_DISP_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_ACC_Updt_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Acc. Verified: " + dtLS[0].LS_ACC_Verify_Status.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Updt. Time: " + dtLS[0].LS_ACC_Updt_Time.ToString());

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    if (!dtLS[0].IsLS_GATE_PASS_TimeNull())
                    {
                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintTrueFont(nPositionX, nPositionY, "Arial", 10, "Gate Out Time: " + dtLS[0].LS_GATE_PASS_Time.ToString(), true, 0, false, false);

                        nPositionY += nTextHeight;
                        nTextHeight = WinGlobalClass.clsBXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "------------------------------------------");
                    }

                    nPositionY += nTextHeight;
                    //nTextHeight = BXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "HAVE A NICE DAY!");
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintTrueFont(nPositionX, nPositionY, "Arial", 10, "HAVE A NICE DAY!", false, 0, true, false);

                    nPositionY += nTextHeight;
                    //nTextHeight = BXLAPI.PrintDeviceFont(nPositionX, nPositionY, "FontA11", 0, "Sale Date: 07/01/03");
                    nTextHeight = WinGlobalClass.clsBXLAPI.PrintTrueFont(nPositionX, nPositionY, "Arial", 10, "Print Date & Time: " + DateTime.Now.ToString(), false, 0, true, false);

                    WinGlobalClass.clsBXLAPI.End_Page();	// End Page
                    WinGlobalClass.clsBXLAPI.End_Doc();	// End Document
                }
            }
        }
    }
}
