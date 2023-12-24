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

namespace DRN_WIN_ERP.Modules.Sales
{
    public partial class frmDeliveryChallan : Form
    {
        private bool _isDirty = false;

        private bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }            
        }

        public frmDeliveryChallan()
        {
            InitializeComponent();
        }

        private void frmSalesChallan_Load(object sender, EventArgs e)
        {
            try
            {
                //begin by setting IsDirty to false
                IsDirty = false;

                GenerateDataGridView(); 

                //Challan Header Ref
                var taInvHdrData = new tbl_InTr_Trn_HdrTableAdapter();
                var dtGetMaxChlnRef = taInvHdrData.GetMaxChlnRef(DateTime.Now.Year);
                var nextChlnRef = dtGetMaxChlnRef == null ? "000001" : (Convert.ToInt32(dtGetMaxChlnRef) + 1).ToString("000000");
                var nextChlnRefNo = "ECIL-CLN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextChlnRef).ToString("000000");
                txtChlnRef.Text = nextChlnRefNo;
                dtpChlnDate.Value = DateTime.Now;
                
                #region Load_Year_Data
                // Create new DataTable and DataSource objects.
                DataTable dtYear = new DataTable();
                // Declare DataColumn and DataRow variables.
                DataColumn columnYear;
                DataRow rowYear;
                // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                columnYear = new DataColumn();
                columnYear.DataType = System.Type.GetType("System.String");
                columnYear.ColumnName = "Value";
                dtYear.Columns.Add(columnYear);
                // Create second column.
                columnYear = new DataColumn();
                columnYear.DataType = Type.GetType("System.String");
                columnYear.ColumnName = "Text";
                dtYear.Columns.Add(columnYear);                
                var curYear = DateTime.Now.Year;
                for (int year = 2014; year <= (curYear); year++)
                {
                    rowYear = dtYear.NewRow();
                    rowYear["Value"] = year.ToString();
                    rowYear["Text"] = year.ToString();
                    dtYear.Rows.Add(rowYear);
                }
                cboYear.DisplayMember = "Text";
                cboYear.ValueMember = "Value";
                cboYear.DataSource = dtYear;
                cboYear.SelectedValue = curYear.ToString();
                #endregion                                

                #region Load_Month_Data
                // Create new DataTable and DataSource objects.
                DataTable dtMonth = new DataTable();
                // Declare DataColumn and DataRow variables.
                DataColumn columnMonth;
                DataRow rowMonth;
                // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                columnMonth = new DataColumn();
                columnMonth.DataType = System.Type.GetType("System.String");
                columnMonth.ColumnName = "Value";
                dtMonth.Columns.Add(columnMonth);
                // Create second column.
                columnMonth = new DataColumn();
                columnMonth.DataType = Type.GetType("System.String");
                columnMonth.ColumnName = "Text";
                dtMonth.Columns.Add(columnMonth);
                rowMonth = dtMonth.NewRow();
                rowMonth["Value"] = "0";
                rowMonth["Text"] = "---Select---";
                dtMonth.Rows.Add(rowMonth);
                var curMonth = DateTime.Now.Month;
                for (int month = 1; month <= 12; month++)
                {
                    var monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    rowMonth = dtMonth.NewRow();
                    rowMonth["Value"] = month.ToString();
                    rowMonth["Text"] = monthName.ToString();
                    dtMonth.Rows.Add(rowMonth);
                }
                cboMonth.DisplayMember = "Text";
                cboMonth.ValueMember = "Value";
                cboMonth.DataSource = dtMonth;
                cboMonth.SelectedValue = curMonth.ToString();
                #endregion                
                
                Load_Challan_List();

                /*                
                #region Load_Challan_Data
                // Create new DataTable and DataSource objects.
                DataTable dtChln = new DataTable();
                // Declare DataColumn and DataRow variables.
                DataColumn columnChln;
                DataRow rowChln;
                // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                columnChln = new DataColumn();
                columnChln.DataType = System.Type.GetType("System.String");
                columnChln.ColumnName = "Value";
                dtChln.Columns.Add(columnChln);
                // Create second column.
                columnChln = new DataColumn();
                columnChln.DataType = Type.GetType("System.String");
                columnChln.ColumnName = "Text";
                dtChln.Columns.Add(columnChln);
                rowChln = dtChln.NewRow();
                rowChln["Value"] = "0";
                rowChln["Text"] = "---Select---";
                dtChln.Rows.Add(rowChln);                
                cboChln.DisplayMember = "Text";
                cboChln.ValueMember = "Value";
                cboChln.DataSource = dtChln;
                #endregion                                
               */
 
                var taTransType = new tblTransportTypeTableAdapter();
                var dtTransType = taTransType.GetData();
                cboTransType.DisplayMember = "Trans_Type_Name";
                cboTransType.ValueMember = "Trans_Type_Ref";
                cboTransType.DataSource = dtTransType;

                #region Load_District_Data
                // Create new DataTable and DataSource objects.
                DataTable dtDist = new DataTable();
                // Declare DataColumn and DataRow variables.
                DataColumn columnDist;
                DataRow rowDist;                
                // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                columnDist = new DataColumn();
                columnDist.DataType = System.Type.GetType("System.String");
                columnDist.ColumnName = "Value";
                dtDist.Columns.Add(columnDist);
                // Create second column.
                columnDist = new DataColumn();
                columnDist.DataType = Type.GetType("System.String");
                columnDist.ColumnName = "Text";
                dtDist.Columns.Add(columnDist);
                rowDist = dtDist.NewRow();
                rowDist["Value"] = "0";
                rowDist["Text"] = "---Select---";
                dtDist.Rows.Add(rowDist);
                var ta = new tblSalesDistrictTableAdapter();
                var dt = ta.GetDataByAsc();
                foreach (dsWinSalesMas.tblSalesDistrictRow dr in dt.Rows)
                {
                    rowDist = dtDist.NewRow();
                    rowDist["Value"] = dr.DistRef.ToString();
                    rowDist["Text"] = dr.DistName.ToString();
                    dtDist.Rows.Add(rowDist);
                }                
                cboDist.DisplayMember = "Text";
                cboDist.ValueMember = "Value";
                cboDist.DataSource = dtDist;
                #endregion                

                #region Load_Thana_Data
                // Create new DataTable and DataSource objects.
                DataTable dtThana = new DataTable();
                // Declare DataColumn and DataRow variables.
                DataColumn columnThana;
                DataRow rowThana;
                // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                columnThana = new DataColumn();
                columnThana.DataType = System.Type.GetType("System.String");
                columnThana.ColumnName = "Value";
                dtThana.Columns.Add(columnThana);
                // Create second column.
                columnThana = new DataColumn();
                columnThana.DataType = Type.GetType("System.String");
                columnThana.ColumnName = "Text";
                dtThana.Columns.Add(columnThana);
                rowThana = dtThana.NewRow();
                rowThana["Value"] = "0";
                rowThana["Text"] = "---Select---";
                dtThana.Rows.Add(rowThana);
                cboThana.DataSource = dtThana;
                cboThana.DisplayMember = "Text";
                cboThana.ValueMember = "Value";                
                #endregion                

                #region Load_Transport_Data
                // Create new DataTable and DataSource objects.
                DataTable dtTran = new DataTable();
                // Declare DataColumn and DataRow variables.
                DataColumn columnTran;
                DataRow rowTran;
                // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                columnTran = new DataColumn();
                columnTran.DataType = System.Type.GetType("System.String");
                columnTran.ColumnName = "Value";
                dtTran.Columns.Add(columnTran);
                // Create second column.
                columnTran = new DataColumn();
                columnTran.DataType = Type.GetType("System.String");
                columnTran.ColumnName = "Text";
                dtTran.Columns.Add(columnTran);
                rowTran = dtTran.NewRow();
                rowTran["Value"] = "0";
                rowTran["Text"] = "---Select---";
                dtTran.Rows.Add(rowTran);
                var taTrn = new tbl_TrTr_Vsl_MasTableAdapter();
                var dtTrn = taTrn.GetDataByAsc();
                foreach (dsWinTransMas.tbl_TrTr_Vsl_MasRow dr in dtTrn.Rows)
                {
                    rowTran = dtTran.NewRow();
                    rowTran["Value"] = dr.Vsl_Mas_Ref.ToString();
                    rowTran["Text"] = dr.Vsl_Mas_No.ToString();
                    dtTran.Rows.Add(rowTran);
                }
                cboTransList.DataSource = dtTran;
                cboTransList.DisplayMember = "Text";
                cboTransList.ValueMember = "Value";                
                #endregion                

                var taStore = new tbl_InMa_Str_LocTableAdapter();
                var dtStore = taStore.GetDataByStoreType("FIN");
                cboDelStore.DisplayMember = "Str_Loc_Name";
                cboDelStore.ValueMember = "Str_Loc_Ref";
                cboDelStore.DataSource = dtStore;                
                cboDelStore.SelectedValue = "1007";

                Load_Customer_List();

                Load_Retailer_List();

                this.ActiveControl = txtCustName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data loading error." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void optCust_CheckedChanged(object sender, EventArgs e)
        {            
            if (optCust.Checked)
            {
                txtTransNo.Text = "";
                txtTransNo.Visible = true;
                cboTransList.Visible = false;
            }
            else
            {
                cboTransList.Visible = true;
                txtTransNo.Text = "";
                txtTransNo.Visible = false;
            }
        }

        private void optComp_CheckedChanged(object sender, EventArgs e)
        {            
            if (optComp.Checked)
            {
                txtTransNo.Text = "";
                txtTransNo.Visible = false;
                cboTransList.Visible = true;
            }
            else
            {
                cboTransList.Visible = false;
                txtTransNo.Text = "";
                txtTransNo.Visible = true;                
            }
        }

        private void Load_Customer_List()
        {
            var taCust = new tblSalesPartyAdrTableAdapter();
            var dtCust = new dsWinSalesMas.tblSalesPartyAdrDataTable();
            dtCust = taCust.GetData();

            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            foreach (dsWinSalesMas.tblSalesPartyAdrRow dr in dtCust.Rows)
            {
                MyCollection.Add(dr.Par_Adr_Name.ToString() + ":" + dr.Par_Adr_Ref_No.ToString() + ":" + dr.Par_Adr_Ref.ToString());
            }
            txtCustName.AutoCompleteCustomSource = MyCollection;            
        }

        private void Load_Retailer_List()
        {
            var taRtl = new tblSalesPartyRtlTableAdapter();
            var dtRtl = new dsWinSalesMas.tblSalesPartyRtlDataTable();
            dtRtl = taRtl.GetData();

            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            foreach (dsWinSalesMas.tblSalesPartyRtlRow dr in dtRtl.Rows)
            {
                MyCollection.Add(dr.Par_Rtl_Name.ToString() + ":" + dr.Par_Rtl_Addr.ToString() + ":" + dr.Par_Rtl_Ref.ToString());
            }
            txtRetailer1.AutoCompleteCustomSource = MyCollection;
            txtRetailer2.AutoCompleteCustomSource = MyCollection;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var taDelOrd = new View_Sales_Pend_DoTableAdapter();
            var dtDelOrd = new dsWinSalesTran.View_Sales_Pend_DoDataTable();

            if (txtCustName.Text.Trim().Length <= 0) return;

            btnClear.Visible = true;

            try
            {
                var custRef = "";
                var srchWords = txtCustName.Text.Trim().Split(':');
                if (srchWords.Length > 2) custRef = srchWords[2];

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        dtDelOrd = taDelOrd.GetDataByCustRef(custRef.ToString());
                        gvPendDo1.DataSource = dtDelOrd;

                        if (dtDelOrd.Rows.Count > 0)
                        {
                            txtCustName.Enabled = false;
                            btnSaveChln.Enabled = true;
                            gvPendDo1.Focus();
                            this.gvPendDo1.CurrentCell = this.gvPendDo1[17, 0];
                        }
                        else
                        {
                            txtCustName.Enabled = true;
                            btnSaveChln.Enabled = false;
                            MessageBox.Show("There is no pending D/O for this customer.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        foreach (DataGridViewRow gr in gvPendDo1.Rows)
                        {
                            //var hfDoHdrRef = gr.Cells["DO_Hdr_Ref"].Value.ToString();
                            //var lblDoHdrRefNo = gr.Cells["DO_Hdr_Ref_No"].Value.ToString();
                            //var hfDoDetLno = gr.Cells["DO_Det_Lno"].Value.ToString();
                            //var hfIcode = gr.Cells["DO_Det_Icode"].Value.ToString();
                            //var lblItemDesc = gr.Cells["Itm_Det_Desc"].Value.ToString();
                            //var lblItemUom = gr.Cells["SO_Det_Itm_Uom"].Value.ToString();

                            var txtDoQty = gr.Cells["DO_Det_Bal_Qty"].Value.ToString();
                            var txtDoFreeQty = gr.Cells["DO_Det_Ext_Data2"].Value.ToString();

                            gr.Cells["Challan_Qty"].Value = txtDoQty.ToString();
                            gr.Cells["Challan_Free_Bag"].Value = txtDoFreeQty.ToString();

                            //chalanQty.Value = txtDoQty.ToString();
                            //chalanFreeQty.Value = txtDoFreeQty.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Customer data not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Customer data not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Processing Error.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //Challan Header Ref
                var taInvHdrData = new tbl_InTr_Trn_HdrTableAdapter();
                var dtGetMaxChlnRef = taInvHdrData.GetMaxChlnRef(DateTime.Now.Year);
                var nextChlnRef = dtGetMaxChlnRef == null ? "000001" : (Convert.ToInt32(dtGetMaxChlnRef) + 1).ToString("000000");
                var nextChlnRefNo = "ECIL-CLN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextChlnRef).ToString("000000");
                txtChlnRef.Text = nextChlnRefNo;
                dtpChlnDate.Value = DateTime.Now;

                var taDelOrd = new View_Sales_Pend_DoTableAdapter();
                var dtDelOrd = taDelOrd.GetDataByCustRef("");
                gvPendDo1.DataSource = dtDelOrd;
                txtCustName.Text = "";
                txtTransNo.Text = "";
                txtDriverName.Text = "";
                txtDriverContact.Text = "";
                txtDelAddr.Text = "";
                txtCustName.Enabled = true;
                btnClear.Visible = false;
                btnSaveChln.Enabled = dtDelOrd.Rows.Count > 0;
                txtCustName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Processing Error.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvPendDo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {     
            //make changes
            gvPendDo1.EndEdit();         
        }

        private void btnSaveChln_Click(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taDoDet = new tblSalesOrdDelDetTableAdapter();
            var taSoHdr = new tblSalesOrderHdrTableAdapter();
            var taSoDet = new tblSalesOrderDetTableAdapter();
            var taCrReal = new tblSalesCreditRealizeTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

            SqlTransaction myTran = DRN_WIN_ERP.WinGlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                #region Form Data Validation

                #region Chack Entry Field
                if (txtDelAddr.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("You have to enter delivery address first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }                

                var truckRef = "0";
                var truckNo = "";
                if (optCust.Checked)
                {
                    truckRef = "0";
                    truckNo = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToUpper(txtTransNo.Text.Trim());
                }
                else
                {
                    truckRef = cboTransList.SelectedValue.ToString();
                    truckNo = cboTransList.SelectedValue.ToString() == "0" ? "" : cboTransList.Text.ToString();
                }
                var driverName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtDriverName.Text.Trim());

                if (truckNo == "")
                {
                    MessageBox.Show("You have to enter vehile no first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboDist.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("Select District first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboThana.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("Select Thana first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboDelStore.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("Select Store first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                #region Check DO Selection
                var i = 0;
                foreach (DataGridViewRow gr in gvPendDo1.Rows)
                {
                    DataGridViewCheckBoxCell chkDoDelQty = (DataGridViewCheckBoxCell)gr.Cells[17];
                    if ((bool)chkDoDelQty.Value) i++;
                }
                if (i <= 0)
                {
                    MessageBox.Show("You have to select D/O first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                #region Check Challan Quantity
                foreach (DataGridViewRow gr in gvPendDo1.Rows)
                {
                    var hfDoHdrRef = gr.Cells["DO_Hdr_Ref"].Value.ToString();
                    var lblDoHdrRefNo = gr.Cells["DO_Hdr_Ref_No"].Value.ToString();
                    var hfDoDetLno = gr.Cells["DO_Det_Lno"].Value.ToString();
                    var txtDoQty = gr.Cells["DO_Det_Bal_Qty"].Value.ToString();
                    var txtDoFreeQty = gr.Cells["DO_Det_Ext_Data2"].Value.ToString();
                    var chkDelQty = (DataGridViewCheckBoxCell)gr.Cells[17];

                    if ((bool)chkDelQty.Value)
                    {
                        if (txtDoQty.ToString().Trim() == "" || txtDoQty.ToString().Trim().Length <= 0 || Convert.ToDouble(txtDoQty.ToString().Trim()) <= 0)
                        {
                            if (txtDoFreeQty.ToString().Trim() == "" || txtDoFreeQty.ToString().Trim().Length <= 0 || Convert.ToDouble(txtDoFreeQty.ToString().Trim()) <= 0)
                            {
                                //chkDelQty.BackColor = System.Drawing.Color.Red;
                                MessageBox.Show("Please enter challan quantity first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                var taDoDetChk = new tblSalesOrdDelDetTableAdapter();
                                var dtDoDetChk = taDoDetChk.GetDataByDetLno(hfDoHdrRef.ToString(), Convert.ToInt16(hfDoDetLno.ToString()));
                                if (dtDoDetChk.Rows.Count > 0)
                                {
                                    if (Convert.ToDouble(txtDoFreeQty.ToString().Trim()) > Convert.ToDouble(dtDoDetChk[0].DO_Det_Ext_Data2))
                                    {
                                        MessageBox.Show("You are not allowed to deliver free bag more than : " + dtDoDetChk[0].DO_Det_Ext_Data1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                                else
                                {
                                    //chkDelQty.BackColor = System.Drawing.Color.Red;
                                    MessageBox.Show("Invalid D/O number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            var taDoDetChk = new tblSalesOrdDelDetTableAdapter();
                            var dtDoDetChk = taDoDetChk.GetDataByDetLno(hfDoHdrRef.ToString(), Convert.ToInt16(hfDoDetLno.ToString()));
                            if (dtDoDetChk.Rows.Count > 0)
                            {
                                if (Convert.ToDouble(txtDoQty.ToString().Trim()) > dtDoDetChk[0].DO_Det_Del_Bal_Qty)
                                {
                                    MessageBox.Show("You are not allowed to deliver qty more than : " + dtDoDetChk[0].DO_Det_Del_Bal_Qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                //chkDelQty.BackColor = System.Drawing.Color.Red;
                                MessageBox.Show("Invalid D/O number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region Get Customer Details
                var custRef = "";
                var custAcc = "";
                var custName = "";
                var srchWords = txtCustName.Text.Trim().Split(':');
                if (srchWords.Length > 2) custRef = srchWords[2];

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                            custAcc = dtPartyAdr[0].Par_Adr_Acc_Code.ToString();
                            custName = dtPartyAdr[0].Par_Adr_Name.ToString();
                        }
                        else
                        {
                            //myTran.Rollback();
                            MessageBox.Show("Invalid Customer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        //myTran.Rollback();
                        MessageBox.Show("Invalid Customer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Customer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                #region Get Retailer Details
                var rtlRef = "";
                var srchSpWords = txtRetailer1.Text.Trim().Split(':');
                foreach (string word in srchSpWords)
                {
                    rtlRef = word;
                    break;
                }

                if (rtlRef.Length > 0)
                {
                    var taSp = new tblSalesPersonMasTableAdapter();
                    var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(rtlRef));
                    if (dtSp.Rows.Count > 0)
                        rtlRef = dtSp[0].Sp_Ref.ToString();
                    else
                    {                        
                        MessageBox.Show("Invalid Retailer.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Retailer.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                btnSaveChln.Enabled = false;

                var nextHdrRef = 1;
                var nextHdrRefNo = "";

                var nextChlnRef = "000001";
                var nextChlnRefNo = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taDoDet.AttachTransaction(myTran);
                taSoHdr.AttachTransaction(myTran);
                taSoDet.AttachTransaction(myTran);
                taCrReal.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);               

                //Inventory Header Ref
                var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                var dtMaxSalRef = taInvHdr.GetMaxHdrRefNo("SAL", DateTime.Now.Year);
                var nextSalRef = dtMaxSalRef == null ? 1 : Convert.ToInt32(dtMaxSalRef) + 1;
                nextHdrRefNo = "ECIL-SAL-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextSalRef).ToString("000000");

                //Challan Header Ref
                var dtMaxChlnRef = taInvHdr.GetMaxChlnRef(DateTime.Now.Year);
                nextChlnRef = dtMaxChlnRef == null ? "000001" : (Convert.ToInt32(dtMaxChlnRef) + 1).ToString("000000");
                nextChlnRefNo = "ECIL-CLN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextChlnRef).ToString("000000");

                taInvHdr.InsertInvHdr(nextHdrRef, "IS", "SAL", nextHdrRefNo, custRef.ToString(), custRef.ToString(), custRef.ToString(),
                            nextChlnRefNo.ToString(), DateTime.Now, truckNo.ToString(), driverName.ToString(), txtDriverContact.Text.Trim(),
                            txtDelAddr.Text.Trim(), cboDist.SelectedValue.ToString(), cboThana.SelectedValue.ToString(), "", "",
                            optCust.Checked ? "1" : "2", truckRef.ToString(), 0, "P", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            "ADM", "", cboTransType.SelectedValue.ToString(), "", DateTime.Now.Year + nextChlnRef, "", nextChlnRefNo, custName, "", "", 0, DateTime.Now,
                            WinGlobalClass.clsGlobalProperties.UserRef.ToString() == null ? "0" : WinGlobalClass.clsGlobalProperties.UserRef.ToString(), "1", "");

                var dtMaxAccRef = taAcc.GetMaxRefNo("SJV", DateTime.Now.Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextAccRefNo = "SJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                var dtCrSum = taAcc.GetTotCrAmt(custAcc.ToString());
                var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                var dtDrSum = taAcc.GetTotDrAmt(custAcc.ToString());
                var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);
                var CrBal = (crAmt - drAmt);

                #region Insert Inventory Details and Update D/O Balance
                var jvLNo = 0;
                short Lno = 0;
                double totChlnAmt = 0;
                foreach (DataGridViewRow gr in gvPendDo1.Rows)
                {
                    var hfDoHdrRef = gr.Cells["DO_Hdr_Ref"].Value.ToString();
                    var lblDoHdrRefNo = gr.Cells["DO_Hdr_Ref_No"].Value.ToString();
                    var hfDoDetLno = gr.Cells["DO_Det_Lno"].Value.ToString();

                    var hfIcode = gr.Cells["DO_Det_Icode"].Value.ToString();
                    var lblItemDesc = gr.Cells["Itm_Det_Desc"].Value.ToString();
                    var lblItemUom = gr.Cells["SO_Det_Itm_Uom"].Value.ToString();

                    var txtDoQty = gr.Cells["Challan_Qty"].Value.ToString();
                    var txtDoFreeQty = gr.Cells["Challan_Free_Bag"].Value.ToString();
                    var chkDelQty = (DataGridViewCheckBoxCell)gr.Cells[17];

                    #region Get Item Details
                    var itemName = "";
                    var itemAcc = "";
                    var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                    var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(hfIcode.ToString().Trim()));
                    if (dtItemDet.Rows.Count > 0)
                    {
                        itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                        itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();
                    }
                    else
                    {
                        myTran.Rollback();
                        MessageBox.Show("Invalid Item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    #endregion

                    if ((bool)chkDelQty.Value)
                    {
                        Lno++;

                        if (txtDoQty.ToString().Trim() != "" || txtDoQty.ToString().Trim().Length > 0)
                        {
                            if (Convert.ToDouble(txtDoQty.ToString().Trim()) > 0)
                            {
                                var dtDoDet = taDoDet.GetDataByDetLno(hfDoHdrRef.ToString(), Convert.ToInt16(hfDoDetLno.ToString()));
                                if (dtDoDet.Rows.Count > 0 && dtDoDet[0].DO_Det_Bal_Qty >= Convert.ToDouble(txtDoQty.ToString().Trim()))
                                {
                                    var dtStkCtl = taStkCtl.GetDataByStoreItem(cboDelStore.SelectedValue.ToString(), hfIcode.ToString().Trim());
                                    if (dtStkCtl.Rows.Count > 0)
                                    {
                                        if (dtStkCtl[0].Stk_Ctl_Cur_Stk >= Convert.ToDouble(txtDoQty.ToString().Trim()))
                                        {
                                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "IS", "SAL", nextHdrRefNo.ToString(), Lno, "", 1, nextChlnRefNo.ToString(),
                                                Lno, hfIcode.ToString().Trim(), lblItemDesc.ToString().Trim(), lblItemUom.ToString().Trim(), cboDelStore.SelectedValue.ToString(), "",
                                                hfDoHdrRef.ToString().ToString(), lblDoHdrRefNo.ToString().Trim(), Convert.ToInt16(hfDoDetLno.ToString()), DateTime.Now.Year + nextChlnRef.ToString(),
                                                DateTime.Now, DateTime.Now, Convert.ToDouble(txtDoQty.ToString().Trim()), 0, dtDoDet[0].DO_Det_Lin_Rat,
                                                Convert.ToDecimal(txtDoQty.ToString().Trim()) * dtDoDet[0].DO_Det_Lin_Rat,
                                                Convert.ToDecimal(txtDoQty.ToString().Trim()) * dtDoDet[0].DO_Det_Lin_Rat, "", "", "", 0, 0, "1", "");

                                            taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(txtDoQty.ToString().Trim())), 4), cboDelStore.SelectedValue.ToString(), hfIcode.ToString().Trim());

                                            taDoDet.UpdateDoDeliveryQty((dtDoDet[0].DO_Det_Org_QTY + Convert.ToDouble(txtDoQty.ToString().Trim())),
                                                (dtDoDet[0].DO_Det_Lin_Qty) - (dtDoDet[0].DO_Det_Org_QTY + Convert.ToDouble(txtDoQty.ToString().Trim())),
                                                (dtDoDet[0].DO_Det_Del_Qty + Convert.ToDouble(txtDoQty.ToString().Trim())),
                                                Convert.ToDouble(Convert.ToDecimal(dtDoDet[0].DO_Det_Del_Bal_Qty) - Convert.ToDecimal(txtDoQty.ToString().Trim())),
                                                hfDoHdrRef.ToString(), Convert.ToInt16(hfDoDetLno.ToString()));

                                            #region Insert Accounts Voucher Entry
                                            var dtSoDet = taSoDet.GetDataByDetLno(dtDoDet[0].DO_Det_SO_Ref.ToString(), Convert.ToInt16(dtDoDet[0].DO_Det_SO_Lno.ToString()));
                                            if (dtSoDet.Rows.Count > 0)
                                            {
                                                var OrderDelQty = Convert.ToDouble(txtDoQty.ToString().Trim());
                                                var ItemRate = Convert.ToDouble(dtSoDet[0].SO_Det_Lin_Rat);
                                                //var TransRate=Convert.ToDouble(dtSoDet[0].SO_Det_Trans_Rat);
                                                var TransRate = Convert.ToDouble(dtDoDet[0].DO_Det_Trans_Rat);
                                                var OrdDiscount = (Convert.ToDouble(dtSoDet[0].SO_Det_Lin_Dis) * OrderDelQty) / Convert.ToDouble(dtSoDet[0].SO_Det_Lin_Qty);
                                                var SalesAmt = Convert.ToDecimal((OrderDelQty * ItemRate) - OrdDiscount);
                                                var TransAmt = Convert.ToDecimal(OrderDelQty * TransRate);
                                                var NetAmt = Convert.ToDecimal(((OrderDelQty * ItemRate) + (OrderDelQty * TransRate)) - OrdDiscount);

                                                totChlnAmt = totChlnAmt + Convert.ToDouble(SalesAmt);

                                                #region Sales Journal
                                                jvLNo++;
                                                //Debit-Customer Account
                                                taAcc.InsertAccData(custAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "D", SalesAmt, nextHdrRefNo.ToString(), "0",
                                                    "BDT", 1, SalesAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                    WinGlobalClass.clsGlobalProperties.UserRef, "", DateTime.Now, custName.ToString(), DateTime.Now,
                                                    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, SalesAmt, "",
                                                    nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.ToString().Trim(), "", "J", 0, "1", "SJV");

                                                jvLNo++;
                                                //Credit-Item Account
                                                taAcc.InsertAccData(itemAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                    nextAccRefNo.ToString(), jvLNo, 1, custName.ToString(), "C", SalesAmt, nextHdrRefNo.ToString(), "0",
                                                    "BDT", 1, SalesAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                    WinGlobalClass.clsGlobalProperties.UserRef, "", DateTime.Now, itemName.ToString(), DateTime.Now,
                                                    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, SalesAmt, "",
                                                    nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.ToString().Trim(), "", "J", 0, "1", "SJV");
                                                #endregion

                                                if (TransRate > 0)
                                                {
                                                    totChlnAmt = totChlnAmt + Convert.ToDouble(TransAmt);

                                                    #region Carrying Journal
                                                    jvLNo++;
                                                    //Debit-Customer Account
                                                    taAcc.InsertAccData(custAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                        nextAccRefNo.ToString(), jvLNo, 1, "Sales Carrying Charge", "D", TransAmt, nextHdrRefNo.ToString(), "0",
                                                        "BDT", 1, TransAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                        WinGlobalClass.clsGlobalProperties.UserRef, "", DateTime.Now, custName.ToString(), DateTime.Now,
                                                        "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, TransAmt, "",
                                                        nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.ToString().Trim(), "", "J", 0, "1", "SJV");

                                                    jvLNo++;
                                                    //Credit-Sales Carrying
                                                    taAcc.InsertAccData("01.001.001.0246".ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                        nextAccRefNo.ToString(), jvLNo, 1, custName.ToString(), "C", TransAmt, nextHdrRefNo.ToString(), "0",
                                                        "BDT", 1, TransAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                        WinGlobalClass.clsGlobalProperties.UserRef, "", DateTime.Now, "Sales Carrying Charge", DateTime.Now,
                                                        "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, TransAmt, "",
                                                        nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.ToString().Trim(), "", "J", 0, "1", "SJV");
                                                    #endregion
                                                }
                                            }
                                            else
                                            {
                                                myTran.Rollback();
                                                MessageBox.Show("Sales Order does not match.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                return;
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            myTran.Rollback();
                                            MessageBox.Show("There is not sufficient stock for the item [" + lblItemDesc.ToString().Trim() + "]\nCurrent Stock is: " + dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        myTran.Rollback();
                                        MessageBox.Show("Data Processing Error.\n Inventory data not found for the item " + lblItemDesc.ToString().Trim(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                                else
                                {
                                    myTran.Rollback();
                                    MessageBox.Show("Delivery Order data does not match.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }

                        if (txtDoFreeQty.ToString().Trim() != "" || txtDoFreeQty.ToString().Trim().Length > 0)
                        {
                            if (Convert.ToDouble(txtDoFreeQty.ToString().Trim()) > 0)
                            {
                                var dtDoDet = taDoDet.GetDataByDetLno(hfDoHdrRef.ToString(), Convert.ToInt16(hfDoDetLno.ToString()));
                                if (dtDoDet.Rows.Count > 0 && Convert.ToDouble(dtDoDet[0].DO_Det_Ext_Data2) >= Convert.ToDouble(txtDoFreeQty.ToString().Trim()))
                                {
                                    var dtStkCtl = taStkCtl.GetDataByStoreItem(cboDelStore.SelectedValue.ToString(), hfIcode.ToString().Trim());
                                    if (dtStkCtl.Rows.Count > 0)
                                    {
                                        if (dtStkCtl[0].Stk_Ctl_Cur_Stk >= Convert.ToDouble(txtDoFreeQty.ToString().Trim()))
                                        {
                                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "IS", "BNS", nextHdrRefNo.ToString(), Lno, "", 1, nextChlnRefNo.ToString(),
                                                Lno, hfIcode.ToString().Trim(), lblItemDesc.ToString().Trim(), lblItemUom.ToString().Trim(), cboDelStore.SelectedValue.ToString(), "",
                                                hfDoHdrRef.ToString(), lblDoHdrRefNo.ToString().Trim(), Convert.ToInt16(hfDoDetLno.ToString()), DateTime.Now.Year + nextChlnRef.ToString(),
                                                DateTime.Now, DateTime.Now, Convert.ToDouble(txtDoFreeQty.ToString().Trim()), 0, dtDoDet[0].DO_Det_Lin_Rat,
                                                Convert.ToDecimal(txtDoFreeQty.ToString().Trim()) * dtDoDet[0].DO_Det_Lin_Rat,
                                                Convert.ToDecimal(txtDoFreeQty.ToString().Trim()) * dtDoDet[0].DO_Det_Lin_Rat, "", "", "", 0, 0, "1", "");

                                            taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(txtDoFreeQty.ToString().Trim())), 4), cboDelStore.SelectedValue.ToString(), hfIcode.ToString().Trim());

                                            taDoDet.UpdateDoFreeBagDeliveryQty(Convert.ToDouble(dtDoDet[0].DO_Det_Del_Qty) + Convert.ToDouble(txtDoFreeQty.ToString().Trim()),
                                                Convert.ToDouble(Convert.ToDecimal(dtDoDet[0].DO_Det_Del_Bal_Qty) - Convert.ToDecimal(txtDoFreeQty.ToString().Trim())),
                                                (Convert.ToDouble(dtDoDet[0].DO_Det_Ext_Data1) + Convert.ToDouble(txtDoFreeQty.ToString().Trim())).ToString(),
                                                Convert.ToDouble(Convert.ToDecimal(dtDoDet[0].DO_Det_Ext_Data2) - Convert.ToDecimal(txtDoFreeQty.ToString().Trim())).ToString(),
                                                hfDoHdrRef.ToString(), Convert.ToInt16(hfDoDetLno.ToString()));
                                        }
                                        else
                                        {
                                            myTran.Rollback();
                                            MessageBox.Show("There is not sufficient stock for the item [" + lblItemDesc.ToString().Trim() + "]\nCurrent Stock is: " + dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        myTran.Rollback();
                                        MessageBox.Show("Data Processing Error.\n Inventory data not found for the item " + lblItemDesc.ToString().Trim(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                                else
                                {
                                    myTran.Rollback();
                                    MessageBox.Show("Delivery Order data does not match.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }
                }
                #endregion

                if (CrBal > 0)
                {
                    if (CrBal > Convert.ToDouble(totChlnAmt))
                        taCrReal.InsertCreditRealize(DateTime.Now.Year + nextChlnRef.ToString(), nextChlnRefNo.ToString(), custRef.ToString(), 0,
                            Convert.ToDateTime(dtpChlnDate.Value.ToString()), "1", "");
                    else
                        taCrReal.InsertCreditRealize(DateTime.Now.Year + nextChlnRef.ToString(), nextChlnRefNo.ToString(), custRef.ToString(),
                             Convert.ToDecimal(totChlnAmt) - Convert.ToDecimal(CrBal), Convert.ToDateTime(dtpChlnDate.Value.ToString()), "1", "");
                }
                else
                {
                    taCrReal.InsertCreditRealize(DateTime.Now.Year + nextChlnRef.ToString(), nextChlnRefNo.ToString(), custRef.ToString(), Convert.ToDecimal(totChlnAmt),
                        Convert.ToDateTime(dtpChlnDate.Value.ToString()), "1", "");
                }

                myTran.Commit();                           

                //Challan Header Ref
                var dtGetMaxChlnRef = taInvHdr.GetMaxChlnRef(DateTime.Now.Year);
                var nextChlnRefNew = dtGetMaxChlnRef == null ? "000001" : (Convert.ToInt32(dtGetMaxChlnRef) + 1).ToString("000000");
                var nextChlnRefNoNew = "ECIL-CLN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextChlnRefNew).ToString("000000");
                txtChlnRef.Text = nextChlnRefNoNew;
                dtpChlnDate.Value = DateTime.Now;                

                txtTransNo.Text = "";
                txtDriverName.Text = "";
                txtDriverContact.Text = "";
                txtDelAddr.Text = "";

                txtCustName.Enabled = false;
                btnClear.Visible = true;
                
                cboDelStore.SelectedValue = "1007";

                var taDelOrd = new View_Sales_Pend_DoTableAdapter();
                var dtDelOrd = new dsWinSalesTran.View_Sales_Pend_DoDataTable();
                dtDelOrd = taDelOrd.GetDataByCustRef(custRef.ToString());
                gvPendDo1.DataSource = dtDelOrd;                

                foreach (DataGridViewRow gr in gvPendDo1.Rows)
                {
                    var txtDoQty = gr.Cells["DO_Det_Bal_Qty"].Value.ToString();
                    var txtDoFreeQty = gr.Cells["DO_Det_Ext_Data2"].Value.ToString();

                    gr.Cells["Challan_Qty"].Value = txtDoQty.ToString();
                    gr.Cells["Challan_Free_Bag"].Value = txtDoFreeQty.ToString();
                }
                btnSaveChln.Enabled = dtDelOrd.Rows.Count > 0;

                if (MessageBox.Show("Data Saved Successfully. Do you want to print ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Print_Challan(DateTime.Now.Year.ToString() + nextChlnRef.ToString());
                }                     

                Load_Challan_List();
                cboChln.SelectedValue = DateTime.Now.Year + nextChlnRef.ToString();                      
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                MessageBox.Show("Data Processing Error.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboThana.DataSource = null;
            
            #region Load_Thana_Data
            // Create new DataTable and DataSource objects.
            DataTable dtThana = new DataTable();
            // Declare DataColumn and DataRow variables.
            DataColumn columnThana;
            DataRow rowThana;
            // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
            columnThana = new DataColumn();
            columnThana.DataType = System.Type.GetType("System.String");
            columnThana.ColumnName = "Value";
            dtThana.Columns.Add(columnThana);
            // Create second column.
            columnThana = new DataColumn();
            columnThana.DataType = Type.GetType("System.String");
            columnThana.ColumnName = "Text";
            dtThana.Columns.Add(columnThana);
            rowThana = dtThana.NewRow();
            rowThana["Value"] = "0";
            rowThana["Text"] = "---Select---";
            dtThana.Rows.Add(rowThana);
            var ta = new tblSalesThanaTableAdapter();
            var dt = ta.GetDataByDistRef(Convert.ToInt32(cboDist.SelectedValue.ToString()));
            foreach (dsWinSalesMas.tblSalesThanaRow dr in dt.Rows)
            {
                rowThana = dtThana.NewRow();
                rowThana["Value"] = dr.ThanaRef.ToString();
                rowThana["Text"] = dr.ThanaName.ToString();
                dtThana.Rows.Add(rowThana);
            }
            cboThana.DataSource = dtThana;
            cboThana.DisplayMember = "Text";
            cboThana.ValueMember = "Value";
            #endregion            
        }

        private void GenerateDataGridView()
        {
            gvPendDo1.AutoGenerateColumns = false;

            //Set Columns Count
            gvPendDo1.ColumnCount = 17;

            // Initialize basic DataGridView properties.            
            gvPendDo1.BackgroundColor = Color.Ivory;
            gvPendDo1.BorderStyle = BorderStyle.Fixed3D;            

            // Set the background color for all rows and for alternating rows. 
            // The value for alternating rows overrides the value for all rows. 
            gvPendDo1.RowsDefaultCellStyle.BackColor = Color.Violet;
            gvPendDo1.AlternatingRowsDefaultCellStyle.BackColor = Color.Violet;

            // Set the selection background color for all the cells.
            gvPendDo1.DefaultCellStyle.SelectionBackColor = Color.Violet;
            gvPendDo1.DefaultCellStyle.SelectionForeColor = Color.Black;            

            // Set the row and column header styles.
            gvPendDo1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gvPendDo1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            gvPendDo1.RowHeadersDefaultCellStyle.BackColor = Color.Black;

            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            gvPendDo1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;

            //Add Columns
            gvPendDo1.Columns[0].Name = "DO_Hdr_Ref";
            gvPendDo1.Columns[0].HeaderText = "D/O Ref";
            gvPendDo1.Columns[0].DataPropertyName = "DO_Hdr_Ref";
            gvPendDo1.Columns[0].ReadOnly = true;
            gvPendDo1.Columns[0].Visible = false;

            gvPendDo1.Columns[1].Name = "DO_Hdr_Ref_No";
            gvPendDo1.Columns[1].HeaderText = "D/O Ref. No";
            gvPendDo1.Columns[1].DataPropertyName = "DO_Hdr_Ref_No";
            gvPendDo1.Columns[1].Width = 120;
            gvPendDo1.Columns[1].ReadOnly = true;

            gvPendDo1.Columns[2].Name = "DO_Det_Lno";
            gvPendDo1.Columns[2].HeaderText = "D/O L#";
            gvPendDo1.Columns[2].DataPropertyName = "DO_Det_Lno";
            gvPendDo1.Columns[2].ReadOnly = true;
            gvPendDo1.Columns[2].Visible = false;

            gvPendDo1.Columns[3].Name = "DO_Hdr_T_C1";
            gvPendDo1.Columns[3].HeaderText = "Delivey Address";
            gvPendDo1.Columns[3].DataPropertyName = "DO_Hdr_T_C1";
            gvPendDo1.Columns[3].Width = 200;
            gvPendDo1.Columns[3].ReadOnly = true;

            gvPendDo1.Columns[4].Name = "DO_Hdr_Date";
            gvPendDo1.Columns[4].HeaderText = "D/O Date";
            gvPendDo1.Columns[4].DataPropertyName = "DO_Hdr_Date";
            //gvPendDo1.Columns[4].ValueType = typeof(System.DateTime);
            gvPendDo1.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
            gvPendDo1.Columns[4].Width = 80;
            gvPendDo1.Columns[4].ReadOnly = true;

            gvPendDo1.Columns[5].Name = "DO_Det_Icode";
            gvPendDo1.Columns[5].HeaderText = "Item Code";
            gvPendDo1.Columns[5].DataPropertyName = "DO_Det_Icode";
            gvPendDo1.Columns[5].ReadOnly = true;
            gvPendDo1.Columns[5].Visible = false;

            gvPendDo1.Columns[6].Name = "Itm_Det_Desc";
            gvPendDo1.Columns[6].HeaderText = "Item Name";
            gvPendDo1.Columns[6].DataPropertyName = "Itm_Det_Desc";
            gvPendDo1.Columns[6].Width = 170;
            gvPendDo1.Columns[6].ReadOnly = true;

            gvPendDo1.Columns[7].Name = "SO_Det_Itm_Uom";
            gvPendDo1.Columns[7].HeaderText = "Unit";
            gvPendDo1.Columns[7].DataPropertyName = "SO_Det_Itm_Uom";
            gvPendDo1.Columns[7].ReadOnly = true;
            gvPendDo1.Columns[7].Width = 30;

            gvPendDo1.Columns[8].Name = "DO_Det_Lin_Qty";
            gvPendDo1.Columns[8].HeaderText = "D/O Qty";
            gvPendDo1.Columns[8].DataPropertyName = "DO_Det_Lin_Qty";
            gvPendDo1.Columns[8].Width = 80;
            gvPendDo1.Columns[8].ReadOnly = true;

            gvPendDo1.Columns[9].Name = "DO_Det_Free_Qty";
            gvPendDo1.Columns[9].HeaderText = "Free Qty";
            gvPendDo1.Columns[9].DataPropertyName = "DO_Det_Free_Qty";
            gvPendDo1.Columns[9].Width = 80;
            gvPendDo1.Columns[9].ReadOnly = true;

            gvPendDo1.Columns[10].Name = "DO_Tot_Qty";
            gvPendDo1.Columns[10].HeaderText = "Tot. D/O Qty";
            gvPendDo1.Columns[10].DataPropertyName = "DO_Tot_Qty";
            gvPendDo1.Columns[10].Width = 80;
            gvPendDo1.Columns[10].ReadOnly = true;

            gvPendDo1.Columns[11].Name = "DO_Det_Del_Qty";
            gvPendDo1.Columns[11].HeaderText = "Tot. Del Qty";
            gvPendDo1.Columns[11].DataPropertyName = "DO_Det_Del_Qty";
            gvPendDo1.Columns[11].Width = 70;
            gvPendDo1.Columns[11].ReadOnly = true;

            gvPendDo1.Columns[12].Name = "DO_Det_Del_Bal_Qty";
            gvPendDo1.Columns[12].HeaderText = "Tot. Balance";
            gvPendDo1.Columns[12].DataPropertyName = "DO_Det_Del_Bal_Qty";
            gvPendDo1.Columns[12].ReadOnly = true;

            gvPendDo1.Columns[13].Name = "DO_Det_Bal_Qty";
            gvPendDo1.Columns[13].HeaderText = "D/O Bal Qty";
            gvPendDo1.Columns[13].DataPropertyName = "DO_Det_Bal_Qty";
            gvPendDo1.Columns[13].Width = 70;
            gvPendDo1.Columns[13].ReadOnly = true;
            gvPendDo1.Columns[13].Visible = false;

            gvPendDo1.Columns[14].Name = "DO_Det_Ext_Data2";
            gvPendDo1.Columns[14].HeaderText = "D/O Free Bal Qty";
            gvPendDo1.Columns[14].DataPropertyName = "DO_Det_Ext_Data2";
            gvPendDo1.Columns[14].ReadOnly = true;
            gvPendDo1.Columns[14].Visible = false;

            //Text Box
            gvPendDo1.Columns[15].Name = "Challan_Qty";
            gvPendDo1.Columns[15].HeaderText = "Challan Qty";
            //gvPendDo1.Columns[15].DataPropertyName = "DO_Det_Bal_Qty";            

            //Text Box
            gvPendDo1.Columns[16].Name = "Challan_Free_Bag";
            gvPendDo1.Columns[16].HeaderText = "Free Bag";
            //gvPendDo1.Columns[16].DataPropertyName = "DO_Det_Ext_Data2";            

            //Check Box
            DataGridViewCheckBoxColumn CheckBoxColumn = new DataGridViewCheckBoxColumn();
            CheckBoxColumn.Name = "checkDO";
            CheckBoxColumn.HeaderText = "";
            CheckBoxColumn.Width = 50;
            gvPendDo1.Columns.Add(CheckBoxColumn);

            // Specify Backgroung Color for the "Challan_Qty" column. 
            //gvPendDo1.Columns["Challan_Qty"].DefaultCellStyle.BackColor = Color.Red;
        }

        private void gvPendDo1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //check that row isn't -1, i.e. creating datagrid header
                if (e.RowIndex == -1)
                    return;

                if (this.gvPendDo1.CurrentCell.ColumnIndex == 15)
                {
                    var doOrgQty = gvPendDo1.CurrentRow.Cells["DO_Det_Lin_Qty"].Value.ToString();
                    var doOrgFreeQty = gvPendDo1.CurrentRow.Cells["DO_Det_Free_Qty"].Value.ToString();
                    var doBalQty = gvPendDo1.CurrentRow.Cells["DO_Det_Bal_Qty"].Value.ToString();
                    var doBalFreeQty = gvPendDo1.CurrentRow.Cells["DO_Det_Ext_Data2"].Value.ToString();

                    var chalanQty = gvPendDo1.CurrentRow.Cells["Challan_Qty"].Value.ToString() == null ? "0" : gvPendDo1.CurrentRow.Cells["Challan_Qty"].Value.ToString();
                    var chalanFreeQty = gvPendDo1.CurrentRow.Cells["Challan_Free_Bag"].Value.ToString() == null ? "0" : gvPendDo1.CurrentRow.Cells["Challan_Free_Bag"].Value.ToString();

                    if (Convert.ToDouble(chalanQty) > Convert.ToDouble(doBalQty))
                    {
                        MessageBox.Show("You are not allowed to deliver qty more than: " + doBalQty.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //gvPendDo1.CurrentRow.Cells["Challan_Free_Bag"].Value = doBalFreeQty.ToString();
                        gvPendDo1.CurrentRow.Cells["Challan_Qty"].Value = doBalQty.ToString();
                        //return;
                    }

                    var ChallanFreeQty = (Convert.ToDouble(chalanQty) * Convert.ToDouble(doOrgFreeQty)) / Convert.ToDouble(doOrgQty);
                    if (Convert.ToDouble(ChallanFreeQty) > Convert.ToDouble(doBalFreeQty))
                    {
                        MessageBox.Show("You are not allowed to deliver free qty more than: " + doBalFreeQty.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvPendDo1.CurrentRow.Cells["Challan_Free_Bag"].Value = doBalFreeQty.ToString();
                        //return;
                    }
                    else
                    {
                        gvPendDo1.CurrentRow.Cells["Challan_Free_Bag"].Value = ChallanFreeQty.ToString();
                    }
                }

                if (this.gvPendDo1.CurrentCell.ColumnIndex == 16)
                {
                    var doFreeBalQty = gvPendDo1.CurrentRow.Cells["DO_Det_Ext_Data2"].Value.ToString();
                    var chalanQty = gvPendDo1.CurrentRow.Cells["Challan_Free_Bag"].Value.ToString() == null ? "0" : gvPendDo1.CurrentRow.Cells["Challan_Free_Bag"].Value.ToString();

                    if (Convert.ToDouble(chalanQty) > Convert.ToDouble(doFreeBalQty))
                    {
                        gvPendDo1.CurrentRow.Cells["Challan_Free_Bag"].Value = "0";
                        MessageBox.Show("You are not allowed to deliver free qty more than: " + doFreeBalQty.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }


                if (e.ColumnIndex == 17)
                {
                    //Set other columns values            
                    if ((bool)gvPendDo1.Rows[e.RowIndex].Cells[17].Value == true)
                    {
                        var hfDoHdrRef = gvPendDo1.Rows[e.RowIndex].Cells["DO_Hdr_Ref"].Value.ToString();
                        var taDoHdr = new tblSalesOrdDelHdrTableAdapter();
                        var dtDoHdr = taDoHdr.GetDataByHdrRef(hfDoHdrRef.ToString());
                        if (dtDoHdr.Rows.Count > 0)
                        {
                            if (dtDoHdr[0].DO_Hdr_Com9 == "1")
                            {
                                optCust.Checked = true;
                                txtTransNo.Text = dtDoHdr[0].DO_Hdr_Com1;
                                txtTransNo.Visible = true;
                                cboTransList.SelectedIndex = 0;
                                cboTransList.Visible = false;
                            }
                            else
                            {
                                optComp.Checked = true;
                                txtTransNo.Text = "";
                                txtTransNo.Visible = false;
                                cboTransList.SelectedValue = dtDoHdr[0].DO_Hdr_Com10;
                                cboTransList.Visible = true;
                            }
                            txtDriverName.Text = dtDoHdr[0].DO_Hdr_Com2;
                            txtDriverContact.Text = dtDoHdr[0].DO_Hdr_Com3;
                            txtDelAddr.Text = dtDoHdr[0].DO_Hdr_T_C1;
                            cboTransType.SelectedValue = dtDoHdr[0].SO_Hdr_Exp_Type;
                            if (dtDoHdr[0].DO_Hdr_Com4.ToString() == "0")
                                cboDist.SelectedIndex = 0;
                            else
                                cboDist.SelectedValue = dtDoHdr[0].DO_Hdr_Com4.ToString();

                            if (cboDist.SelectedIndex != 0)
                            {
                                cboThana.DataSource = null;

                                #region Load_Thana_Data
                                // Create new DataTable and DataSource objects.
                                DataTable dtThana = new DataTable();
                                // Declare DataColumn and DataRow variables.
                                DataColumn columnThana;
                                DataRow rowThana;
                                // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                                columnThana = new DataColumn();
                                columnThana.DataType = System.Type.GetType("System.String");
                                columnThana.ColumnName = "Value";
                                dtThana.Columns.Add(columnThana);
                                // Create second column.
                                columnThana = new DataColumn();
                                columnThana.DataType = Type.GetType("System.String");
                                columnThana.ColumnName = "Text";
                                dtThana.Columns.Add(columnThana);
                                rowThana = dtThana.NewRow();
                                rowThana["Value"] = "0";
                                rowThana["Text"] = "---Select---";
                                dtThana.Rows.Add(rowThana);
                                var ta = new tblSalesThanaTableAdapter();
                                var dt = ta.GetDataByDistRef(Convert.ToInt32(cboDist.SelectedValue.ToString()));
                                foreach (dsWinSalesMas.tblSalesThanaRow dr in dt.Rows)
                                {
                                    rowThana = dtThana.NewRow();
                                    rowThana["Value"] = dr.ThanaRef.ToString();
                                    rowThana["Text"] = dr.ThanaName.ToString();
                                    dtThana.Rows.Add(rowThana);
                                }
                                cboThana.DataSource = dtThana;
                                cboThana.DisplayMember = "Text";
                                cboThana.ValueMember = "Value";
                                cboThana.SelectedValue = dtDoHdr[0].DO_Hdr_Com5.ToString();
                                #endregion
                            }
                        }
                        else
                        {
                            MessageBox.Show("D/O data not found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        txtTransNo.Text = "";
                        txtDriverName.Text = "";
                        txtDriverContact.Text = "";
                        txtDelAddr.Text = "";
                    }

                    //mark as dirty
                    IsDirty = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data processing error.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool eventHookedUp; 

        private void gvPendDo1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //I just want to pick up on changes to column 15 & 16  
            if (this.gvPendDo1.CurrentCell.ColumnIndex == 15 || this.gvPendDo1.CurrentCell.ColumnIndex == 16)
            {
                if (!this.eventHookedUp)
                {
                    e.Control.KeyPress += this.dataGridViewTextBox_KeyPress;
                    this.eventHookedUp = true;
                }
            }
            else
            {
                e.Control.KeyPress -= this.dataGridViewTextBox_KeyPress;
                this.eventHookedUp = false;
            }
        }

        private void dataGridViewTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 46)
                e.Handled = false;
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCustName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                /*
                txtCustName.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtCustName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                
                var taCust = new tblSalesDistrictTableAdapter();
                var dtCust = new dsWinSalesMas.tblSalesDistrictDataTable();
                dtCust = taCust.GetDataByAsc();

                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                foreach (dsWinSalesMas.tblSalesDistrictRow dr in dtCust.Rows)
                {
                    MyCollection.Add(dr.DistName.ToString() + ":" + dr.DistCode.ToString() + ":" + dr.DistRef.ToString());
                }
                txtCustName.AutoCompleteCustomSource = MyCollection;
               */
            }
            catch (Exception ex)
            {
            }            
        }

        private void txtCustName_KeyPress(object sender, KeyPressEventArgs e)
        {         
            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString;

            //    //frmBrowser fb = new frmBrowser(DBConnector.ConnectionString, "select Gl_Coa_Code,Gl_Coa_Name from AccCOA", "Accounts Code,Accounts Name");

            //    string sql = "select top 200 [Par_Adr_Ref],[Par_Adr_Ref_No],[Par_Adr_Name] from [tblSalesPartyAdr] order by Par_Adr_Name";
            //    int ptop = txtCustName.Bottom;
            //    int pleft = txtCustName.Left;
            //    frmBrowserCOA fb = new frmBrowserCOA(connection, sql, "Par_Adr_Ref,Par_Adr_Ref_No,Par_Adr_Name", ptop, pleft, this.MdiParent);

            //    txtCustName.Text = fb.ReturnString;
            //    //getDataByCoaCode(Convert.ToString(txtAccountCode.Text));

            //}
        }

        private void txtCustName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                //var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString;

                var connection = DRN_WIN_ERP.WinGlobalClass.clsGlobalProperties.drnConStr.ToString();

                //frmBrowser fb = new frmBrowser(DBConnector.ConnectionString, "select Gl_Coa_Code,Gl_Coa_Name from AccCOA", "Accounts Code,Accounts Name");

                string sql = "select top 200 [Par_Adr_Ref],[Par_Adr_Ref_No],[Par_Adr_Name] from [tblSalesPartyAdr] order by Par_Adr_Name";
                int ptop = txtCustName.Bottom;
                int pleft = txtCustName.Left;
                frmBrowserCOA fb = new frmBrowserCOA(connection, sql, "Customer Ref,Customer Ref No,Customer Name", "Par_Adr_Ref,Par_Adr_Ref_No,Par_Adr_Name", ptop, pleft, this.MdiParent);

                txtCustName.Text = fb.ReturnString2 + ":" + fb.ReturnString1 + ":" + fb.ReturnString;

                txtCustName.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print_Challan(cboChln.SelectedValue.ToString());
        }

        private void Print_Challan(string strChlnRef)
        {
            if (strChlnRef.Trim().Length <= 0 || strChlnRef.Trim().ToString() != "0")
            {                
                var rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_DC_No}='" + strChlnRef.Trim().ToString() + "'";
               
                //var rptFile = @"C:\Program Files\DRN_WIN_ERP\Reports\Sales\rptDelChln.rpt";

                string rptFile = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\Sales\rptDelChln.rpt";

                WinGlobalClass.clsGlobalProperties.RptDateFrom = DateTime.Now.ToString("dd/MM/yyyy");
                WinGlobalClass.clsGlobalProperties.RptDateTo = DateTime.Now.ToString("dd/MM/yyyy");
                WinGlobalClass.clsGlobalProperties.rptFilePath = rptFile;
                WinGlobalClass.clsGlobalProperties.rptFormula = rptSelcFormula;

                frmShowSalesReport childForm = new frmShowSalesReport();
                childForm.MdiParent = this.ParentForm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
            else
            {
                MessageBox.Show("Select Challan No first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }        

        private void cboYear_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Load_Challan_List();

            /*
            #region Load_Challan_Data
            // Create new DataTable and DataSource objects.
            DataTable dtChln = new DataTable();
            // Declare DataColumn and DataRow variables.
            DataColumn columnChln;
            DataRow rowChln;
            // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
            columnChln = new DataColumn();
            columnChln.DataType = System.Type.GetType("System.String");
            columnChln.ColumnName = "Value";
            dtChln.Columns.Add(columnChln);
            // Create second column.
            columnChln = new DataColumn();
            columnChln.DataType = Type.GetType("System.String");
            columnChln.ColumnName = "Text";
            dtChln.Columns.Add(columnChln);
            rowChln = dtChln.NewRow();
            rowChln["Value"] = "0";
            rowChln["Text"] = "---Select---";
            dtChln.Rows.Add(rowChln);
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var dtInvDet = taInvHdr.GetChallanDataListDesc(Convert.ToInt32(cboYear.SelectedValue));
            foreach (dsWinInvTran.tbl_InTr_Trn_HdrRow dr in dtInvDet.Rows)
            {
                rowChln = dtChln.NewRow();
                rowChln["Value"] = dr.Trn_Hdr_DC_No.ToString();
                rowChln["Text"] = dr.Trn_Hdr_Cno.ToString() + ":" + dr.T_C1.ToString() + " [" + dr.Trn_Hdr_Date.ToString("dd/MM/yyyy") + "]";
                dtChln.Rows.Add(rowChln);
            }
            cboChln.DisplayMember = "Text";
            cboChln.ValueMember = "Value";
            cboChln.DataSource = dtChln;
            #endregion                                            
             * */
        }

        private void cboChln_Click(object sender, EventArgs e)
        {
            /*
            if (cboChln.Items.Count <= 1)
            {
                #region Load_Challan_Data
                // Create new DataTable and DataSource objects.
                DataTable dtChln = new DataTable();
                // Declare DataColumn and DataRow variables.
                DataColumn columnChln;
                DataRow rowChln;
                // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                columnChln = new DataColumn();
                columnChln.DataType = System.Type.GetType("System.String");
                columnChln.ColumnName = "Value";
                dtChln.Columns.Add(columnChln);
                // Create second column.
                columnChln = new DataColumn();
                columnChln.DataType = Type.GetType("System.String");
                columnChln.ColumnName = "Text";
                dtChln.Columns.Add(columnChln);
                rowChln = dtChln.NewRow();
                rowChln["Value"] = "0";
                rowChln["Text"] = "---Select---";
                dtChln.Rows.Add(rowChln);
                var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
                var dtInvDet = taInvHdr.GetChallanDataListDesc(DateTime.Now.Year);
                foreach (dsWinInvTran.tbl_InTr_Trn_HdrRow dr in dtInvDet.Rows)
                {
                    rowChln = dtChln.NewRow();
                    rowChln["Value"] = dr.Trn_Hdr_DC_No.ToString();
                    rowChln["Text"] = dr.Trn_Hdr_Cno.ToString() + ":" + dr.T_C1.ToString() + " [" + dr.Trn_Hdr_Date.ToString("dd/MM/yyyy") + "]";
                    dtChln.Rows.Add(rowChln);
                }
                cboChln.DisplayMember = "Text";
                cboChln.ValueMember = "Value";
                cboChln.DataSource = dtChln;
                #endregion
            }
            */
        }

        private void gvPendDo1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 15)
            {
                e.CellStyle.SelectionBackColor = Color.White;
                e.CellStyle.BackColor = Color.White;
            }
            if (e.ColumnIndex == 16)
            {
                e.CellStyle.SelectionBackColor = Color.White;
                e.CellStyle.BackColor = Color.White;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close this window ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Load_Challan_List()
        {
            #region Load_Challan_Data
            // Create new DataTable and DataSource objects.
            DataTable dtChln = new DataTable();
            // Declare DataColumn and DataRow variables.
            DataColumn columnChln;
            DataRow rowChln;
            // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
            columnChln = new DataColumn();
            columnChln.DataType = System.Type.GetType("System.String");
            columnChln.ColumnName = "Value";
            dtChln.Columns.Add(columnChln);
            // Create second column.
            columnChln = new DataColumn();
            columnChln.DataType = Type.GetType("System.String");
            columnChln.ColumnName = "Text";
            dtChln.Columns.Add(columnChln);
            rowChln = dtChln.NewRow();
            rowChln["Value"] = "0";
            rowChln["Text"] = "---Select---";
            dtChln.Rows.Add(rowChln);
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var dtInvDet = new DataTable();
            if (cboMonth.SelectedIndex == 0)
                dtInvDet = taInvHdr.GetChallanDataListDesc(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            else
                dtInvDet = taInvHdr.GetChallanDataListDescByYearMonth(Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
            foreach (dsWinInvTran.tbl_InTr_Trn_HdrRow dr in dtInvDet.Rows)
            {
                rowChln = dtChln.NewRow();
                rowChln["Value"] = dr.Trn_Hdr_DC_No.ToString();
                rowChln["Text"] = dr.Trn_Hdr_Cno.ToString() + ":" + dr.T_C1.ToString() + " [" + dr.Trn_Hdr_Date.ToString("dd/MM/yyyy") + "]";
                dtChln.Rows.Add(rowChln);
            }
            cboChln.DisplayMember = "Text";
            cboChln.ValueMember = "Value";
            cboChln.DataSource = dtChln;
            #endregion
        }

        private void cboMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Load_Challan_List();
        }

        private void txtRetailer1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {             
                var connection = DRN_WIN_ERP.WinGlobalClass.clsGlobalProperties.drnConStr.ToString();

                //frmBrowser fb = new frmBrowser(DBConnector.ConnectionString, "select Gl_Coa_Code,Gl_Coa_Name from AccCOA", "Accounts Code,Accounts Name");

                string sql = "select top 200 Par_Rtl_Ref,Par_Rtl_Name,Par_Rtl_Addr,Par_Adr_Name,DistName,ThanaName,Dsm_Full_Name,Sp_Full_Name,SalesZoneName from View_Party_Retailer order by Par_Rtl_Name";
                int ptop = txtCustName.Bottom;
                int pleft = txtCustName.Left;
                frmBrowserCOA fb = new frmBrowserCOA(connection, sql, "Retailer Ref,Retailer Name,Address,Dealer Name,Dristrict,Thana,DSM Name,SP Name,Sales Zone", "Par_Rtl_Ref,Par_Rtl_Name,Par_Rtl_Addr,Par_Adr_Name,DistName,ThanaName,Dsm_Full_Name,Sp_Full_Name,SalesZoneName", ptop, pleft, this.MdiParent);

                txtRetailer1.Text = fb.ReturnString1 + ":" + fb.ReturnString2 + ":" + fb.ReturnString;

                txtRetailer1.Focus();
            }
        }

        private void txtRetailer2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                var connection = DRN_WIN_ERP.WinGlobalClass.clsGlobalProperties.drnConStr.ToString();

                //frmBrowser fb = new frmBrowser(DBConnector.ConnectionString, "select Gl_Coa_Code,Gl_Coa_Name from AccCOA", "Accounts Code,Accounts Name");

                string sql = "select top 200 Par_Rtl_Ref,Par_Rtl_Name,Par_Rtl_Addr,Par_Adr_Name,DistName,ThanaName,Dsm_Full_Name,Sp_Full_Name,SalesZoneName from View_Party_Retailer order by Par_Rtl_Name";
                int ptop = txtCustName.Bottom;
                int pleft = txtCustName.Left;
                frmBrowserCOA fb = new frmBrowserCOA(connection, sql, "Retailer Ref,Retailer Name,Address,Dealer Name,Dristrict,Thana,DSM Name,SP Name,Sales Zone", "Par_Rtl_Ref,Par_Rtl_Name,Par_Rtl_Addr,Par_Adr_Name,DistName,ThanaName,Dsm_Full_Name,Sp_Full_Name,SalesZoneName", ptop, pleft, this.MdiParent);

                txtRetailer2.Text = fb.ReturnString1 + ":" + fb.ReturnString2 + ":" + fb.ReturnString;

                txtRetailer2.Focus();
            }
        }       
    }
}
