using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmStoreReq : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var taEmpGenInfo = new View_Emp_BascTableAdapter();
                var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRefAct(Session["sessionEmpRef"] == null ? 0 : Convert.ToInt32(Session["sessionEmpRef"].ToString()));
                if (dtEmpGenInfo.Rows.Count > 0)                
                    txtFromDept.Text = dtEmpGenInfo[0].EmpRefNo.ToString() + ":" + dtEmpGenInfo[0].EmpId.ToString() + ":" + dtEmpGenInfo[0].EmpName.ToString();
                
                txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtRequiredDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblStkQty.Text = "0.00";

                var taStrReqFor = new tbl_Str_Req_ForTableAdapter();
                var dtStrReqFor = taStrReqFor.GetData();
                cboReqFor.DataSource = dtStrReqFor;
                cboReqFor.DataTextField = "Str_Req_For_Name";
                cboReqFor.DataValueField = "Str_Req_For_Code";
                cboReqFor.DataBind();
                cboReqFor.Items.Insert(0, new ListItem("-----Select-----", "0"));

                LoadInitGrid();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();

            try
            {
                var itemCode = "";
                var itemName = "";

                var srchWords = txtItem.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    itemCode = word;
                    break;
                }

                if (itemCode.Length > 0)
                {
                    int result;
                    if (int.TryParse(itemCode, out result))
                    {
                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemCode));
                        if (dtItem.Rows.Count > 0)
                        {
                            itemName = dtItem[0].Itm_Det_Desc;
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }                

                var dt = new DataTable();
                dt = (DataTable)ViewState["datatable"];

                dt.Rows.Add(dt.Rows.Count + 1, itemCode, itemName, txtUom.Text.Trim(), "1001", txtQuantity.Text.Trim(), "0");
                ViewState["datatable"] = dt;
                SetGridData();

                ClearFieldData("");
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {            
            string prefix = "", yr = "", mn = "";
            bool new_period = false;
            DateTime chkPeriod = DateTime.Now;

            var taPoHdr = new tbl_InTr_Pro_Sr_HdrTableAdapter();
            var taPoDet = new tbl_InTr_Pro_Sr_DetTableAdapter();
            //var taGetTranPerm = new InSu_Trn_Set_PstTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPoHdr.Connection);

            try
            {
                var empRef = "";
                var empName = "";
                var srchWords = txtFromDept.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    empRef = word;
                    break;
                }

                if (empRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(empRef, out result))
                    {
                        var taEmpGenInfo = new View_Emp_BascTableAdapter();
                        var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                        if (dtEmpGenInfo.Rows.Count > 0)
                        {
                            empRef = dtEmpGenInfo[0].EmpRefNo;
                            empName = dtEmpGenInfo[0].EmpName;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var delrRtlRef = "";
                var delrRtlName = "";
                var srchWords1 = txtDealer.Text.Trim().Split(':');
                foreach (string word in srchWords1)
                {
                    delrRtlRef = word;
                    break;
                }

                if (delrRtlRef.Length > 0)
                {
                    var taDearRet = new View_Dealer_RelailerTableAdapter();
                    var dtDearRet = taDearRet.GetDataByRef(delrRtlRef.ToString());
                    if (dtDearRet.Rows.Count > 0)
                    {
                        delrRtlRef = dtDearRet[0].Par_Adr_Ref;
                        delrRtlName = dtDearRet[0].Par_Adr_Name;
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Dealer/Retailer.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                

                var issTo = empRef;
                if (delrRtlRef != "") issTo = delrRtlRef;

                taPoHdr.AttachTransaction(myTran);
                taPoDet.AttachTransaction(myTran);

                //var dtGetTranPerm = taGetTranPerm.GetData("RC", "PO", "ADM");

                //if (dtGetTranPerm.Rows.Count > 0)
                //{
                //    mn = DateTime.Now.Month.ToString("00");
                //    yr = DateTime.Now.ToString("yy");

                //    var Spr = dtGetTranPerm[0].Trn_Set_Ord_Pfix.Trim();

                //    prefix = Spr + yr + mn;

                //    chkPeriod = Convert.ToInt32(mn) < 7 ? Convert.ToDateTime("01/07/" + (Convert.ToInt32(yr) - 1)) : Convert.ToDateTime("01/07/" + yr);
                //}

                mn = DateTime.Now.Month.ToString("00");
                yr = DateTime.Now.ToString("yy");

                var Spr = "PO";

                prefix = Spr + yr + mn;

                chkPeriod = Convert.ToInt32(mn) < 7 ? Convert.ToDateTime("01/07/" + (Convert.ToInt32(yr) - 1)) : Convert.ToDateTime("01/07/" + yr);


                var dtRefNo = taPoHdr.GetMaxPoRef(Convert.ToDateTime(chkPeriod));
                var nextRefNo = (dtRefNo == null || Convert.ToInt32(dtRefNo) == 0) ? 1 : Convert.ToInt32(dtRefNo) + 1;

                taPoHdr.InsertPoHdr("PO", "PO", prefix + nextRefNo.ToString("000000"), issTo, issTo,
                                    empRef, Convert.ToDateTime(txtEntryDate.Text.Trim()), txtRemarks.Text.Trim(), cboReqFor.SelectedValue,
                                    cboReqFor.SelectedItem.ToString(), "", "", "", "", "", delrRtlName, empName, 0, "P",
                                    DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"), "ADM",
                                    Convert.ToDateTime(txtEntryDate.Text.Trim()), "", "", "",
                                    Convert.ToDateTime(txtRequiredDate.Text.Trim()),
                                    Convert.ToDateTime(txtRequiredDate.Text.Trim()).Year + "/" +
                                    Convert.ToDateTime(txtRequiredDate.Text.Trim()).Month.ToString("00"), "", "", "", 1, "BDT", 0);

                var dt = new DataTable();
                dt = (DataTable)ViewState["datatable"];

                short lNo = 1;
                foreach (DataRow row in dt.Rows)
                {
                    taPoDet.InsertPoDet("PO", "PO", prefix + nextRefNo.ToString("000000"),
                                        lNo, "", 0, row["Item Code"].ToString(), row["Item Name"].ToString(), row["UOM"].ToString(),
                                        row["Store"].ToString(), "", "", 0, "", DateTime.Now, DateTime.Now,
                                        Convert.ToInt32(row["Quantity"].ToString()), 0,
                                        Convert.ToInt32(row["Quantity"].ToString()), Convert.ToInt32(row["Quantity"].ToString()), "O", "N",
                                        Convert.ToInt32(row["Rate"].ToString()),
                                        Convert.ToInt32(row["Quantity"].ToString()) * Convert.ToInt32(row["Rate"].ToString()),
                                        Convert.ToInt32(row["Quantity"].ToString()) * Convert.ToInt32(row["Rate"].ToString()),
                                        "Req. By:- " + empName + " @" + DateTime.Now.ToString(), "", "H", 1);
                    lNo++;
                }
                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Requsition Posted Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();               

                //// Expand);
                //this.CollapsiblePanelExtenderHdr.Collapsed = false;
                //this.CollapsiblePanelExtenderHdr.ClientState = "false";
                //// Collapse
                //// Expand
                //this.CollapsiblePanelExtenderDet.Collapsed = true;
                //this.CollapsiblePanelExtenderDet.ClientState = "true";

                ClearFieldData("P");
                LoadInitGrid();
                SetGridData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        private void LoadInitGrid()
        {
            var dt = new DataTable();
            dt.Rows.Clear();
            dt.Columns.Add("LineNo", typeof(string));
            dt.Columns.Add("Item Code", typeof(string));
            dt.Columns.Add("Item Name", typeof(string));
            dt.Columns.Add("UOM", typeof(string));
            dt.Columns.Add("Store", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("Rate", typeof(string));
            ViewState["datatable"] = dt;
        }

        private void SetGridData()
        {
            var dt = new DataTable();
            dt = (DataTable)ViewState["datatable"];
            gvSR.DataSource = dt;
            gvSR.DataBind();

            btnPost.Visible = gvSR.Rows.Count > 0;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var indx = gvSR.SelectedIndex;
            if (indx != -1)
            {
                var dt = new DataTable();
                dt = (DataTable)ViewState["datatable"];
                dt.Rows.RemoveAt(indx);
                ViewState["datatable"] = dt;
                SetGridData();
                gvSR.SelectedIndex = -1;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            // Expand
            this.CollapsiblePanelExtenderHdr.Collapsed = true;
            this.CollapsiblePanelExtenderHdr.ClientState = "true";
            // Collapse
            // Expand
            this.CollapsiblePanelExtenderDet.Collapsed = false;
            this.CollapsiblePanelExtenderDet.ClientState = "false";
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            var itemCode = "";
            if (txtItem.Text.ToUpper() != "ALL")
            {
                try
                {
                    String[] temp = txtItem.Text.Split(':');
                    if (temp.Length < 2) return;
                    itemCode = temp[0];
                }
                catch (Exception ex)
                {

                    //throw;
                }
            }
            if (itemCode != "")
            {
                try
                {
                    var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
                    var dtStkCtl = taStkCtl.GetCurStkByItem(itemCode.ToString());
                    lblStkQty.Text = dtStkCtl == null ? "0.00" : Convert.ToInt32(dtStkCtl).ToString();

                    var taItem = new tbl_InMa_Item_DetTableAdapter();
                    var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemCode));

                    txtUom.Text = dtItem[0].Itm_Det_Stk_Unit;

                    this.txtQuantity.Focus();
                }
                catch (Exception ex)
                {

                    //throw;
                }
            }
        }

        private void ClearFieldData(string Pst_Flg)
        {
            if (Pst_Flg == "P")
            {
                txtFromDept.Text = "";
                txtDealer.Text = "";
                txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtRequiredDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtRemarks.Text = "";
            }
            txtItem.Text = "";
            txtUom.Text = "";
            txtQuantity.Text = "";
            lblStkQty.Text = "0.00";
            txtSrSearch.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taPoHdr = new tbl_InTr_Pro_Sr_HdrTableAdapter();
            var taPoDet = new tbl_InTr_Pro_Sr_DetTableAdapter();

            try
            {
                var SrRef = "";
                if (txtSrSearch.Text.ToUpper() != "")
                {
                    try
                    {
                        String[] temp = txtSrSearch.Text.Split(':');
                        if (temp.Length < 2) return;
                        SrRef = temp[0];
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }
                var dtPoHdr = taPoHdr.GetDataByPoRef(SrRef);
                if (dtPoHdr.Rows.Count > 0)
                {
                    txtFromDept.Text = dtPoHdr[0].PO_Hdr_Pcode;
                    //txtToDept.Text = dtPoHdr[0].PO_Hdr_Pcode;
                    txtEntryDate.Text = dtPoHdr[0].PO_Hdr_DATE.ToString("dd/MM/yyyy");
                    txtRequiredDate.Text = dtPoHdr[0].po_hdr_due_date.ToString("dd/MM/yyyy");
                    txtRemarks.Text = dtPoHdr[0].PO_Hdr_Com1;

                    var dt = new DataTable();
                    LoadInitGrid();
                    dt = (DataTable)ViewState["datatable"];

                    var dtPoDet = taPoDet.GetPoDetByPoRef(SrRef);
                    int i = 0;
                    foreach (DataRow row in dtPoDet.Rows)
                    {
                        dt.Rows.Add(dtPoDet[i].PO_Det_Lno, dtPoDet[i].PO_Det_Icode, dtPoDet[i].PO_Det_Itm_Desc,
                                    dtPoDet[i].PO_Det_Itm_Uom,
                                    dtPoDet[i].PO_Det_Str_Code, dtPoDet[i].PO_Det_Org_QTY.ToString("##.##"),
                                    dtPoDet[i].PO_Det_Lin_Rat.ToString("##.##"),
                                    dtPoDet[i].PO_Det_Lin_Amt.ToString("##.##"));
                        ViewState["datatable"] = dt;
                        SetGridData();
                        i++;
                    }

                    btnClear.Visible = true;
                    pHdrBody.Enabled = false;
                    pDetBody.Enabled = false;
                    gvSR.Columns[0].Visible = false;
                    gvSR.Enabled = false;
                    btnPost.Visible = false;
                    // Expand);
                    this.CollapsiblePanelExtenderHdr.Collapsed = false;
                    this.CollapsiblePanelExtenderHdr.ClientState = "false";
                    // Collapse
                    // Expand
                    this.CollapsiblePanelExtenderDet.Collapsed = true;
                    this.CollapsiblePanelExtenderDet.ClientState = "true";
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFieldData("P");
            pHdrBody.Enabled = true;
            pDetBody.Enabled = true;
            LoadInitGrid();
            SetGridData();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var delRetRef = "";
                var srchWords = txtDealer.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    delRetRef = word;
                    break;
                }

                if (delRetRef.Length > 0)
                {
                    var taDelRet = new View_Dealer_RelailerTableAdapter();
                    var dtDelRet = taDelRet.GetDataByRef(delRetRef);
                    if (dtDelRet.Rows.Count > 0)
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }
    }
}