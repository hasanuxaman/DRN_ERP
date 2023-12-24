using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DRN_WIN_ERP.DataSets;
using DRN_WIN_ERP.DataSets.dsWinSalesMasTableAdapters;
using DRN_WIN_ERP.DataSets.dsWinSalesTranTableAdapters;
using DRN_WIN_ERP.DataSets.dsWinInvMasTableAdapters;

namespace DRN_WIN_ERP.Modules.Sales
{
    public partial class frmSalesReport : Form
    {
        public frmSalesReport()
        {
            InitializeComponent();
        }

        private void frmSalesReport_Load(object sender, EventArgs e)
        {
            txtCustName.AutoCompleteCustomSource = Load_Customer_List();
            txtChlnFilter.AutoCompleteCustomSource = Load_Customer_List();
        }

        private AutoCompleteStringCollection Load_Customer_List()
        {
            var taCust = new tblSalesPartyAdrTableAdapter();
            var dtCust = new dsWinSalesMas.tblSalesPartyAdrDataTable();
            dtCust = taCust.GetData();

            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            foreach (dsWinSalesMas.tblSalesPartyAdrRow dr in dtCust.Rows)
            {
                MyCollection.Add(dr.Par_Adr_Name.ToString() + ":" + dr.Par_Adr_Ref_No.ToString() + ":" + dr.Par_Adr_Ref.ToString());
            }
            return MyCollection;
        }

        private AutoCompleteStringCollection Load_Item_List()
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtItem = new dsWinInvMas.tbl_InMa_Item_DetDataTable();
            dtItem = taItem.GetDataByItemType("FG");

            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            foreach (dsWinInvMas.tbl_InMa_Item_DetRow dr in dtItem.Rows)
            {
                MyCollection.Add(dr.Itm_Det_Desc.ToString() + ":" + dr.Itm_Det_Code.ToString() + ":" + dr.Itm_Det_Ref.ToString());
            }
            return MyCollection;
        }

        private AutoCompleteStringCollection Load_SP_List()
        {
            var taSp = new tblSalesPersonMasTableAdapter();
            var dtSp = new dsWinSalesMas.tblSalesPersonMasDataTable();
            dtSp = taSp.GetData();

            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            foreach (dsWinSalesMas.tblSalesPersonMasRow dr in dtSp.Rows)
            {
                MyCollection.Add(dr.Sp_Full_Name.ToString() + ":" + dr.Sp_Short_Name.ToString() + ":" + dr.Sp_Ref.ToString());
            }
            return MyCollection;
        }               

        private void btnPrintDo_Click(object sender, EventArgs e)
        {
            SalesDoReportInfo();
        }

        private void btnPrintChln_Click(object sender, EventArgs e)
        {
            SalesChallanReportInfo();
        }

        protected void SalesDoReportInfo()
        {
            var rptFile = "";
            var rptSelcFormula = "";

            var custRef = "";
            if (txtCustName.Text.Trim().Length > 0)
            {
                if (txtCustName.Text.Trim().Length <= 0) return;
                
                var srchWords = txtCustName.Text.Trim().Split(':');
                if (srchWords.Length > 2) custRef = srchWords[2];

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0) custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                    }
                }
            }

            if (optDoCreated.Checked)
            {
                if (custRef == "")
                {
                    rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + dtpDoDateFrom.Text.Trim() + "') to Date ('" + dtpDoDateTo.Text.Trim()
                    + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P'";
                }
                else
                {
                    rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + dtpDoDateFrom.Text.Trim() + "') to Date ('" + dtpDoDateTo.Text.Trim()
                    + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Do.SO_Hdr_Pcode}='" + custRef + "'";
                }
                rptFile = @"C:\Program Files\DRN_WIN_ERP\Reports\Sales\rptDelOrd.rpt";
            }

            if (optDoPending.Checked)
            {
                if (custRef == "")
                {
                    rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + dtpDoDateFrom.Text.Trim() + "') to Date ('" + dtpDoDateTo.Text.Trim()
                    + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0)";
                }
                else
                {
                    rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + dtpDoDateFrom.Text.Trim() + "') to Date ('" + dtpDoDateTo.Text.Trim()
                    + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Do.SO_Hdr_Pcode}='" + custRef
                    + "' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0)";
                }
                rptFile = @"C:\Program Files\DRN_WIN_ERP\Reports\Sales\rptDelOrdPend.rpt";
            }

            if (optDoExecute.Checked)
            {
                if (custRef == "")
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + dtpDoDateFrom.Text.Trim() + "') to Date ('" + dtpDoDateTo.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                }
                else
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + dtpDoDateFrom.Text.Trim() + "') to Date ('" + dtpDoDateTo.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Hdr_Pcode}='" + custRef + "'";
                }
                rptFile = @"C:\Program Files\DRN_WIN_ERP\Reports\Sales\rptDelChlnList.rpt";
            }
            
            WinGlobalClass.clsGlobalProperties.RptDateFrom = dtpDoDateFrom.Text.Trim();
            WinGlobalClass.clsGlobalProperties.RptDateTo = dtpDoDateTo.Text.Trim();
            WinGlobalClass.clsGlobalProperties.rptFilePath = rptFile;
            WinGlobalClass.clsGlobalProperties.rptFormula = rptSelcFormula;

            frmShowSalesReport childForm = new frmShowSalesReport();
            childForm.MdiParent = this.ParentForm;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        protected void SalesChallanReportInfo()
        {
            var rptFile = "";
            var rptSelcFormula = "";            

            if (optCustomer.Checked)
            {
                #region Get Customer Ref
                var custRef = "";            
                if (txtChlnFilter.Text.Trim().Length > 0)            
                {
                    var srchWords = txtCustName.Text.Trim().Split(':');
                    if (srchWords.Length > 2) custRef = srchWords[2];
                
                    if (custRef.Length > 0)                
                    {                    
                        int result;                    
                        if (int.TryParse(custRef, out result))                    
                        {                        
                            var taPartyAdr = new tblSalesPartyAdrTableAdapter();                        
                            var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));                        
                            if (dtPartyAdr.Rows.Count > 0) custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();                    
                        }                
                    }            
                }            
                #endregion

                if (custRef == "")
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + dtpChlnDateFrom.Text.Trim() + "') to Date ('" + dtpChlnDateTo.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                }
                else
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + dtpChlnDateFrom.Text.Trim() + "') to Date ('" + dtpChlnDateTo.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Hdr_Pcode}='" + custRef + "'";
                }
                rptFile = @"C:\Program Files\DRN_WIN_ERP\Reports\Sales\rptDelChlnListCust.rpt";
            }

            if (optItemName.Checked)
            {
                #region Get Item Ref
                var itemRef = "";
                if (txtChlnFilter.Text.Trim().Length > 0)
                {
                    var srchWords = txtChlnFilter.Text.Trim().Split(':');
                    if (srchWords.Length > 2) itemRef = srchWords[2];                    

                    if (itemRef.Length > 0)
                    {
                        int result;
                        if (int.TryParse(itemRef, out result))
                        {
                            var taItem = new tbl_InMa_Item_DetTableAdapter();
                            var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                            if (dtItem.Rows.Count > 0) itemRef = dtItem[0].Itm_Det_Ref.ToString();
                        }
                    }
                }
                #endregion

                if (itemRef == "")
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + dtpChlnDateFrom.Text.Trim() + "') to Date ('" + dtpChlnDateTo.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                }
                else
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + dtpChlnDateFrom.Text.Trim() + "') to Date ('" + dtpChlnDateTo.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Det_Icode}='" + itemRef + "'";
                }
                rptFile = @"C:\Program Files\DRN_WIN_ERP\Reports\Sales\rptDelChlnListItem.rpt";
            }

            if (optSalesPerson.Checked)
            {
                #region Get SP Ref
                var spRef = "";
                if (txtChlnFilter.Text.Trim().Length > 0)
                {
                    var srchWords = txtChlnFilter.Text.Trim().Split(':');
                    if (srchWords.Length > 2) spRef = srchWords[2];  
                    if (spRef.Length > 0)
                    {
                        int result;
                        if (int.TryParse(spRef, out result))
                        {
                            var taSp = new tblSalesPersonMasTableAdapter();
                            var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(spRef));
                            if (dtSp.Rows.Count > 0) spRef = dtSp[0].Sp_Ref.ToString();
                        }
                    }
                }
                #endregion

                if (spRef == "")
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + dtpChlnDateFrom.Text.Trim() + "') to Date ('" + dtpChlnDateTo.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                }
                else
                {
                    rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + dtpChlnDateFrom.Text.Trim() + "') to Date ('" + dtpChlnDateTo.Text.Trim()
                    + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.SO_Hdr_Com4}='" + spRef + "'";
                }
                rptFile = @"C:\Program Files\DRN_WIN_ERP\Reports\Sales\rptDelChlnListSP.rpt";
            }

            WinGlobalClass.clsGlobalProperties.RptDateFrom = dtpChlnDateFrom.Text.Trim();
            WinGlobalClass.clsGlobalProperties.RptDateTo = dtpChlnDateTo.Text.Trim();
            WinGlobalClass.clsGlobalProperties.rptFilePath = rptFile;
            WinGlobalClass.clsGlobalProperties.rptFormula = rptSelcFormula;

            frmShowSalesReport childForm = new frmShowSalesReport();
            childForm.MdiParent = this.ParentForm;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void txtCustName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {             
                var connection = DRN_WIN_ERP.WinGlobalClass.clsGlobalProperties.drnConStr.ToString();             

                string sql = "select top 200 [Par_Adr_Ref],[Par_Adr_Ref_No],[Par_Adr_Name] from [tblSalesPartyAdr] order by Par_Adr_Name";
                int ptop = txtCustName.Bottom;
                int pleft = txtCustName.Left;
                frmBrowserCOA fb = new frmBrowserCOA(connection, sql, "Par_Adr_Ref,Par_Adr_Ref_No,Par_Adr_Name", "Par_Adr_Ref,Par_Adr_Ref_No,Par_Adr_Name", ptop, pleft, this.MdiParent);

                txtCustName.Text = fb.ReturnString2 + ":" + fb.ReturnString1 + ":" + fb.ReturnString;

                txtCustName.Focus();
            }
        }

        private void txtChlnFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                var connection = DRN_WIN_ERP.WinGlobalClass.clsGlobalProperties.drnConStr.ToString();
                if (optCustomer.Checked)
                {
                    string sql = "select top 200 [Par_Adr_Ref],[Par_Adr_Ref_No],[Par_Adr_Name] from [tblSalesPartyAdr] order by Par_Adr_Name";
                    int ptop = txtCustName.Bottom;
                    int pleft = txtCustName.Left;
                    frmBrowserCOA fb = new frmBrowserCOA(connection, sql, "Customer Ref. No,Customer Code,Customer Name","Par_Adr_Ref,Par_Adr_Ref_No,Par_Adr_Name", ptop, pleft, this.MdiParent);
                    txtChlnFilter.Text = fb.ReturnString2 + ":" + fb.ReturnString1 + ":" + fb.ReturnString;
                }

                if (optItemName.Checked)
                {
                    string sql = "select top 200 [Itm_Det_Ref],[Itm_Det_Desc],[Itm_Det_Stk_Unit],[Itm_Det_T_C1] FROM tbl_InMa_Item_Det";
                    int ptop = txtCustName.Bottom;
                    int pleft = txtCustName.Left;
                    frmBrowserCOA fb = new frmBrowserCOA(connection, sql, "Item Ref. No,Item Name,Unit,Item Type", "Itm_Det_Ref,Itm_Det_Desc,Itm_Det_T_C1", ptop, pleft, this.MdiParent);
                    txtChlnFilter.Text = fb.ReturnString2 + ":" + fb.ReturnString1 + ":" + fb.ReturnString;
                }

                if (optSalesPerson.Checked)
                {
                    string sql = "select top 200 [Sp_Ref],[Sp_Short_Name],[Sp_Full_Name],[Sp_Cell_No],[Sp_Supr_Name] FROM tblSalesPersonMas";
                    int ptop = txtCustName.Bottom;
                    int pleft = txtCustName.Left;
                    frmBrowserCOA fb = new frmBrowserCOA(connection, sql, "SP Ref. No,Short Name,Full Name, Cell No, Superisor", "", ptop, pleft, this.MdiParent);
                    txtChlnFilter.Text = fb.ReturnString2 + ":" + fb.ReturnString1 + ":" + fb.ReturnString;
                }
                txtChlnFilter.Focus();
            }
        }

        private void optCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (optCustomer.Checked)
                txtChlnFilter.AutoCompleteCustomSource = Load_Customer_List();
            else
                txtChlnFilter.Text = "";
        }

        private void optItemName_CheckedChanged(object sender, EventArgs e)
        {
            if(optItemName.Checked)
                txtChlnFilter.AutoCompleteCustomSource = Load_Item_List();
            else
                txtChlnFilter.Text = "";
        }

        private void optSalesPerson_CheckedChanged(object sender, EventArgs e)
        {
            if (optSalesPerson.Checked)
                txtChlnFilter.AutoCompleteCustomSource = Load_SP_List();
            else
                txtChlnFilter.Text = "";
        }        
    }
}
