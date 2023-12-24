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
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrch.ContextKey = "0";

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            //if (empRef == "000011")//-----Imran
            //    btnCrSecurity.Visible = true;
            //else
            //    btnCrSecurity.Visible = false;

            var taParAcc = new tblSalesPartyAccTableAdapter();
            var dtMaxAccRef = taParAcc.GetMaxAccRef();
            var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
            txtCustRefNo.Text = "DL-" + Convert.ToInt32(nextAccRef).ToString("000000");

            var taCustType = new tblSalesPartyTypeTableAdapter();
            cboCustType.DataSource = taCustType.GetDataByAsc();
            cboCustType.DataTextField = "CustTypeName";
            cboCustType.DataValueField = "CustTypeRef";
            cboCustType.DataBind();
            cboCustType.Items.Insert(0, new ListItem("---Select---", "0"));

            var taDist = new tblSalesDistrictTableAdapter();
            cboCustDist.DataSource = taDist.GetDataByAsc();
            cboCustDist.DataTextField = "DistName";
            cboCustDist.DataValueField = "DistRef";
            cboCustDist.DataBind();
            cboCustDist.Items.Insert(0, new ListItem("---Select---", "0"));

            //var taThana = new tblSalesThanaTableAdapter();
            //cboCustThana.DataSource = taThana.GetDataByAsc();
            //cboCustThana.DataTextField = "ThanaName";
            //cboCustThana.DataValueField = "ThanaRef";
            //cboCustThana.DataBind();
            cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));

            var taSalesZone = new tblSalesZoneTableAdapter();
            cboCustZone.DataSource = taSalesZone.GetDataByAsc();
            cboCustZone.DataTextField = "SalesZoneName";
            cboCustZone.DataValueField = "SalesZoneRef";
            cboCustZone.DataBind();
            cboCustZone.Items.Insert(0, new ListItem("---Select---", "0"));

            cboCustZoneSrch.DataSource = taSalesZone.GetDataByAsc();
            cboCustZoneSrch.DataTextField = "SalesZoneName";
            cboCustZoneSrch.DataValueField = "SalesZoneRef";
            cboCustZoneSrch.DataBind();
            cboCustZoneSrch.Items.Insert(0, new ListItem("---Select---", "0"));

            var taSalesWing = new tblSalesWingTableAdapter();
            cboSalesWing.DataSource = taSalesWing.GetData();
            cboSalesWing.DataTextField = "SalesWingName";
            cboSalesWing.DataValueField = "SalesWingRef";
            cboSalesWing.DataBind();
            cboSalesWing.Items.Insert(0, new ListItem("---Select---", "0"));

            var taSalesDsm = new View_Sales_DSMTableAdapter();
            var dtSalesDsm = taSalesDsm.GetActDsm();
            foreach (dsSalesMas.View_Sales_DSMRow dr in dtSalesDsm.Rows)
            {
                cboDsm.Items.Add(new ListItem(dr.Dsm_Short_Name.ToString() + " :: " + dr.SalesZoneName.ToString(), dr.Dsm_Ref.ToString()));                
            }
            cboDsm.Items.Insert(0, new ListItem("---Select---", "0"));

            cboSpo.Items.Insert(0, new ListItem("---Select---", "0"));

            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtMaxCoaCode = taCoa.GetMaxCoaCode();
            var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
            var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");
            txtCustAccCode.Text = nextCoaCode.ToString();

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var taParAdr = new tblSalesPartyAdrTableAdapter();
            var dtParAdr = taParAdr.GetData();
            Session["data"] = dtParAdr;
            SetCustomerGridData();

            btnExport.Enabled = dtParAdr.Rows.Count > 0;

            txtCrSecBG.Attributes.Add("onkeyup", "CalcTotSecAmount('" + txtCrSecBG.ClientID + "', '" + txtCrSecFDR.ClientID + "', '" + txtCrSecSDR.ClientID + "', '" + txtCrSecLAND.ClientID + "', '" + txtCrSecCHQ.ClientID + "', '" + txtCrSecNOA.ClientID + "', '" + txtCrSecTotAmt.ClientID + "' )");
            txtCrSecFDR.Attributes.Add("onkeyup", "CalcTotSecAmount('" + txtCrSecBG.ClientID + "', '" + txtCrSecFDR.ClientID + "', '" + txtCrSecSDR.ClientID + "', '" + txtCrSecLAND.ClientID + "', '" + txtCrSecCHQ.ClientID + "', '" + txtCrSecNOA.ClientID + "', '" + txtCrSecTotAmt.ClientID + "' )");
            txtCrSecSDR.Attributes.Add("onkeyup", "CalcTotSecAmount('" + txtCrSecBG.ClientID + "', '" + txtCrSecFDR.ClientID + "', '" + txtCrSecSDR.ClientID + "', '" + txtCrSecLAND.ClientID + "', '" + txtCrSecCHQ.ClientID + "', '" + txtCrSecNOA.ClientID + "', '" + txtCrSecTotAmt.ClientID + "' )");
            txtCrSecLAND.Attributes.Add("onkeyup", "CalcTotSecAmount('" + txtCrSecBG.ClientID + "', '" + txtCrSecFDR.ClientID + "', '" + txtCrSecSDR.ClientID + "', '" + txtCrSecLAND.ClientID + "', '" + txtCrSecCHQ.ClientID + "', '" + txtCrSecNOA.ClientID + "', '" + txtCrSecTotAmt.ClientID + "' )");
            txtCrSecCHQ.Attributes.Add("onkeyup", "CalcTotSecAmount('" + txtCrSecBG.ClientID + "', '" + txtCrSecFDR.ClientID + "', '" + txtCrSecSDR.ClientID + "', '" + txtCrSecLAND.ClientID + "', '" + txtCrSecCHQ.ClientID + "', '" + txtCrSecNOA.ClientID + "', '" + txtCrSecTotAmt.ClientID + "' )");
            txtCrSecNOA.Attributes.Add("onkeyup", "CalcTotSecAmount('" + txtCrSecBG.ClientID + "', '" + txtCrSecFDR.ClientID + "', '" + txtCrSecSDR.ClientID + "', '" + txtCrSecLAND.ClientID + "', '" + txtCrSecCHQ.ClientID + "', '" + txtCrSecNOA.ClientID + "', '" + txtCrSecTotAmt.ClientID + "' )");
        }

        public string GetSalesZone(string slsZoneRef)
        {
            var taSlsZone = new tblSalesZoneTableAdapter();

            string SalesZone = "";
            try
            {
                var dtSlsZone = taSlsZone.GetDataBySlsZoneRef(Convert.ToInt32(slsZoneRef.ToString()));
                if (dtSlsZone.Rows.Count > 0)
                    SalesZone = dtSlsZone[0].SalesZoneName.ToString();

                return SalesZone;
            }
            catch (Exception ex) { return SalesZone; }
        }

        public string GetMpoName(string mpoRef)
        {
            var taMpo = new tblSalesPersonMasTableAdapter();

            string MpoName = "";
            try
            {
                var dtSupervisor = taMpo.GetDataBySpRef(Convert.ToInt32(mpoRef.ToString()));
                if (dtSupervisor.Rows.Count > 0)
                    MpoName = dtSupervisor[0].Sp_Full_Name.ToString();

                return MpoName;
            }
            catch (Exception ex) { return MpoName; }
        }

        protected void cboCustDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taThana = new tblSalesThanaTableAdapter();
            cboCustThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboCustDist.SelectedValue));
            cboCustThana.DataTextField = "ThanaName";
            cboCustThana.DataValueField = "ThanaRef";
            cboCustThana.DataBind();
            cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taCustAcc = new tblSalesPartyAccTableAdapter();
            var taCustAdr = new tblSalesPartyAdrTableAdapter();
            var taCustCrSec=new tblSalesCrLimitSecurityTableAdapter();
            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taCustAcc.Connection);

            try
            {
                taCustAcc.AttachTransaction(myTran);
                taCustAdr.AttachTransaction(myTran);
                taCustCrSec.AttachTransaction(myTran);
                taCoa.AttachTransaction(myTran);                

                var custName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtCustName.Text.Trim());
                var cpName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtCustCp.Text.Trim());

                if (hfEditStatus.Value == "N")
                {
                    #region Form Data Validation
                    var dtCustAdr = taCustAdr.GetDataByPartyName(txtCustName.Text.Trim());
                    if (dtCustAdr.Rows.Count > 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Customer already exists with this name.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion

                    #region Insert Customer
                    var dtMaxCoaRef = taCoa.GetMaxCoaRef();
                    var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                    var dtMaxCoaCode = taCoa.GetMaxCoaCode();
                    var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                    var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                    taCoa.InsertCoa(nextCoaRef, nextCoaCode, custName.Trim(), nextCoaCode, "P", "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                        "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    var dtMaxAccRef = taCustAcc.GetMaxAccRef();
                    var nextAccRef = dtMaxAccRef == null ? 100001 : Convert.ToInt32(dtMaxAccRef) + 1;
                    var nextAccRefNo = "DL-ACC-" + Convert.ToInt32(nextAccRef).ToString("000000");

                    taCustAcc.InsertPartyAcc(nextAccRef, nextAccRefNo, custName.Trim(), "", Convert.ToInt32(cboCustType.SelectedValue), "", "", "N", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", 0, 0, 0, 0, 0, 0, "", "BDT", "", "", "", "", "", "", "", 0, "", "", "", "", "");

                    //var crLimitDoc = "";
                    //foreach (ListItem li in chkListCrLimitDoc.Items)
                    //{
                    //    if (li.Selected) crLimitDoc = crLimitDoc + li.Text + ",";
                    //}
                    //crLimitDoc = crLimitDoc.Length > 0 ? crLimitDoc.Substring(0, crLimitDoc.Length - 1) : "";

                    var dtMaxAdrRef = taCustAdr.GetMaxAdrRef();
                    var nextAdrRef = dtMaxAdrRef == null ? 100001 : Convert.ToInt32(dtMaxAdrRef) + 1;
                    var nextAdrRefNo = "DL-" + Convert.ToInt32(nextAdrRef).ToString("000000");

                    taCustAdr.InsertPartyAdr(nextAdrRef, nextAdrRefNo, custName.Trim(), Convert.ToInt32(cboCustType.SelectedValue), nextAccRef.ToString(), txtCustAdr.Text.Trim(),
                        cpName.ToString(), txtCustCpDesig.Text.Trim(), Convert.ToInt32(cboCustDist.SelectedValue), Convert.ToInt32(cboCustThana.SelectedValue),
                        Convert.ToInt32(cboCustZone.SelectedValue), txtCustCell.Text.Trim(), txtCustPhone.Text.Trim(), txtCustFax.Text.Trim(), txtCustEmail.Text.Trim(), 0,
                        Convert.ToDecimal(txtCustCrLimit.Text.Trim()), Convert.ToInt32(txtCustCrPeriod.Text.Trim()), txtCustCrPayTerm.Text.Trim(),
                        "", "", nextCoaCode.ToString(), cboDsm.SelectedValue.ToString(), "", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), optListCustStatus.SelectedValue,
                        "", "N", 0, 0, "", "", "", 0, txtCrSecTotAmt.Text.Trim(), cboSpo.SelectedValue.ToString(), cboSalesWing.SelectedValue.ToString(), "", "");

                    var dtCustCrSec = taCustCrSec.GetDataByPartyRef(Convert.ToInt32(hfRefNo.Value.ToString()));
                    if (dtCustCrSec.Rows.Count > 0)
                    {
                        taCustCrSec.UpdateSecDet(txtCrSecBG.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecBG.Text.Trim()),
                                txtCrSecFDR.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecFDR.Text.Trim()),
                                txtCrSecSDR.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecSDR.Text.Trim()),
                                txtCrSecCHQ.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecCHQ.Text.Trim()),
                                txtCrSecLAND.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecLAND.Text.Trim()),
                                txtCrSecNOA.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecNOA.Text.Trim()), 0,
                                txtCrSecTotAmt.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecTotAmt.Text.Trim()), Convert.ToInt32(hfRefNo.Value));
                    }
                    else
                    {
                        taCustCrSec.InsertSecDet(nextAdrRef, nextAdrRefNo, custName, txtCrSecBG.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecBG.Text.Trim()),
                            txtCrSecFDR.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecFDR.Text.Trim()),
                            txtCrSecSDR.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecSDR.Text.Trim()),
                            txtCrSecCHQ.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecCHQ.Text.Trim()),
                            txtCrSecLAND.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecLAND.Text.Trim()),
                            txtCrSecNOA.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecNOA.Text.Trim()), 0,
                            txtCrSecTotAmt.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecTotAmt.Text.Trim()));
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnSave.Text = "Save";
                    hfEditStatus.Value = "N";
                    hfRefNo.Value = "0";
                    SetCustomerGridData();
                    ClearData();
                    txtSearch.Text = "";
                    btnClearSrch.Visible = false;
                    #endregion
                }
                else
                {
                    #region Update Customer
                    var dtParAcc = taCustAcc.GetDataByPartyAccRef(Convert.ToInt32(hfRefNo.Value));
                    if (dtParAcc.Rows.Count > 0)
                    {
                        taCustAcc.UpdatePartyAcc(custName.Trim(), "", Convert.ToInt32(cboCustType.SelectedValue), "", "", "N", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", 0, 0, 0, 0, 0, 0, "", "BDT",
                            "", "", "", "", "", "", "", 0, "", "", "", "", "", Convert.ToInt32(hfRefNo.Value));

                        //var crLimitDoc = "";
                        //foreach (ListItem li in chkListCrLimitDoc.Items)
                        //{
                        //    if (li.Selected) crLimitDoc = crLimitDoc + li.Text + ",";
                        //}
                        //crLimitDoc = crLimitDoc.Length > 0 ? crLimitDoc.Substring(0, crLimitDoc.Length - 1) : "";

                        var dtParAdr = taCustAdr.GetDataByPartyAdrRef(Convert.ToInt32(hfRefNo.Value));
                        if (dtParAdr.Rows.Count > 0)
                        {
                            taCustAdr.UpdateAprtyAdr(custName.Trim(), Convert.ToInt32(cboCustType.SelectedValue), hfRefNo.Value.ToString(), txtCustAdr.Text.Trim(),
                                cpName.ToString(), txtCustCpDesig.Text.Trim(), Convert.ToInt32(cboCustDist.SelectedValue), Convert.ToInt32(cboCustThana.SelectedValue),
                                Convert.ToInt32(cboCustZone.SelectedValue), txtCustCell.Text.Trim(), txtCustPhone.Text.Trim(), txtCustFax.Text.Trim(), txtCustEmail.Text.Trim(), 0,
                                Convert.ToDecimal(txtCustCrLimit.Text.Trim()), Convert.ToInt32(txtCustCrPeriod.Text.Trim()), txtCustCrPayTerm.Text.Trim(),
                                "", "", txtCustAccCode.Text.Trim(), cboDsm.SelectedValue.ToString(), "", DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), optListCustStatus.SelectedValue, "", "N",
                                0, 0, "", "", "", 0, txtCrSecTotAmt.Text.Trim(), cboSpo.SelectedValue.ToString(), cboSalesWing.SelectedValue.ToString(), "", "", Convert.ToInt32(hfRefNo.Value));

                            var dtCustCrSec = taCustCrSec.GetDataByPartyRef(Convert.ToInt32(hfRefNo.Value.ToString()));
                            if (dtCustCrSec.Rows.Count > 0)
                            {
                                taCustCrSec.UpdateSecDet(txtCrSecBG.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecBG.Text.Trim()),
                                    txtCrSecFDR.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecFDR.Text.Trim()),
                                    txtCrSecSDR.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecSDR.Text.Trim()),
                                    txtCrSecCHQ.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecCHQ.Text.Trim()),
                                    txtCrSecLAND.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecLAND.Text.Trim()),
                                    txtCrSecNOA.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecNOA.Text.Trim()), 0,
                                    txtCrSecTotAmt.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecTotAmt.Text.Trim()), Convert.ToInt32(hfRefNo.Value));

                            }
                            else
                            {
                                taCustCrSec.InsertSecDet(Convert.ToInt32(hfRefNo.Value), txtCustRefNo.Text.Trim(), custName,
                                    txtCrSecBG.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecBG.Text.Trim()),
                                    txtCrSecFDR.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecFDR.Text.Trim()),
                                    txtCrSecSDR.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecSDR.Text.Trim()),
                                    txtCrSecCHQ.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecCHQ.Text.Trim()),
                                    txtCrSecLAND.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecLAND.Text.Trim()),
                                    txtCrSecNOA.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecNOA.Text.Trim()), 0,
                                    txtCrSecTotAmt.Text.Trim().Length <= 0 ? 0 : Convert.ToDecimal(txtCrSecTotAmt.Text.Trim()));
                            }

                            var dtGlCoa = taCoa.GetDataByCoaCode(txtCustAccCode.Text.Trim());
                            if (dtGlCoa.Rows.Count > 0)
                            {
                                taCoa.UpdateCoa(custName.Trim(), "P", "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                                    "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "", txtCustAccCode.Text.Trim());
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Party GL Code.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                            }
                        }
                        else
                        {
                            myTran.Rollback();
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Party Address Code.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();

                        btnSave.Text = "Save";
                        hfEditStatus.Value = "N";
                        hfRefNo.Value = "0";
                        SetCustomerGridData();
                        ClearData();
                        txtSearch.Text = "";
                        btnClearSrch.Visible = false;
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Party Account Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    #endregion
                }

                var taParAdrNew = new tblSalesPartyAdrTableAdapter();
                var dtParAdrNew = new dsSalesMas.tblSalesPartyAdrDataTable();

                #region Get Customer Ref
                var custRef = "0";
                var srchWords = txtSearch.Text.Trim().Split(':');
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
                        var dtParAdrNew1 = taParAdrNew.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtParAdrNew.Rows.Count > 0)
                            custRef = dtParAdrNew1[0].Par_Adr_Ref.ToString();
                    }
                    else
                        custRef = "0";
                }
                #endregion

                if (cboCustZoneSrch.SelectedIndex == 0)
                {
                    if (custRef.Length > 0)
                        dtParAdrNew = taParAdrNew.GetDataByPartyAdrRef(Convert.ToInt32(custRef.ToString()));
                    else
                        dtParAdrNew = taParAdrNew.GetDataByNameSort();
                }
                else
                {
                    if (custRef.Length > 0)
                        dtParAdrNew = taParAdrNew.GetDataBySaleZoneParAdrRef(Convert.ToInt32(cboCustZoneSrch.SelectedValue.ToString()), Convert.ToInt32(custRef.ToString()));
                    else
                        dtParAdrNew = taParAdrNew.GetDataBySalesZone(Convert.ToInt32(cboCustZoneSrch.SelectedValue.ToString()));
                }

                Session["data"] = dtParAdrNew;
                SetCustomerGridData();

                btnExport.Enabled = dtParAdrNew.Rows.Count > 0;

                //var taParAdr = new tblSalesPartyAdrTableAdapter();
                //var dtSaMaParAdr = taParAdr.GetData();
                //Session["data"] = dtSaMaParAdr;
                //SetCustomerGridData();
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
            txtCustRefNo.Text = "";
            var taParAcc = new tblSalesPartyAccTableAdapter();
            var dtMaxAccRef = taParAcc.GetMaxAccRef();
            var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
            txtCustRefNo.Text = "DL-" + Convert.ToInt32(nextAccRef).ToString("000000");

            txtCustAccCode.Text = "";
            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtMaxCoaCode = taCoa.GetMaxCoaCode();
            var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
            var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");
            txtCustAccCode.Text = nextCoaCode.ToString();

            txtCustName.Text = "";
            txtCustAdr.Text = "";
            txtCustCp.Text = "";
            txtCustCpDesig.Text = "";
            txtCustCell.Text = "";
            txtCustPhone.Text = "";
            txtCustFax.Text = "";
            txtCustEmail.Text = "";
            txtCustCrLimit.Text = "";
            txtCustCrLimit.Enabled = true;
            txtCustCrPeriod.Text = "";
            txtCustCrPayTerm.Text = "";
            txtCrSecBG.Text = "";
            txtCrSecFDR.Text = "";
            txtCrSecSDR.Text = "";
            txtCrSecLAND.Text = "";
            txtCrSecCHQ.Text = "";
            txtCrSecNOA.Text = "";
            txtCrSecTotAmt.Text = "";
            //txtCrLimitDocEtc.Text = "";            
            //txtCustSalesPer.Text = "";
            cboDsm.SelectedIndex = 0;
            cboSpo.Items.Clear();
            cboSpo.Items.Insert(0, new ListItem("---Select---", "0"));

            cboCustType.SelectedIndex = 0;
            cboCustDist.SelectedIndex = 0;
            cboCustThana.Items.Clear();
            cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));
            cboCustThana.SelectedIndex = 0;
            cboCustZone.SelectedIndex = 0;
            cboSalesWing.SelectedIndex = 0;

            //foreach (ListItem li in chkListCrLimitDoc.Items)
            //{
            //    li.Selected = false;
            //}

            optListCustStatus.SelectedValue = "1";

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var taParAdr = new tblSalesPartyAdrTableAdapter();
            var dtSaMaParAdr = taParAdr.GetData();
            Session["data"] = dtSaMaParAdr;
            SetCustomerGridData();

            btnExport.Enabled = dtSaMaParAdr.Rows.Count > 0;
        }

        protected void SetCustomerGridData()
        {
            //var taParAdr = new tblSalesPartyAdrTableAdapter();
            //var dtParAdr = taParAdr.GetData();
            var dtParAdr = Session["data"];
            gvCust.DataSource = dtParAdr;
            gvCust.DataBind();            
            gvCust.SelectedIndex = -1;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();

            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var custRef = "0";
                var srchWords = txtSearch.Text.Trim().Split(':');
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
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            gvCust.DataSource = dtPartyAdr;
                            Session["data"] = dtPartyAdr;
                            SetCustomerGridData();
                            btnClearSrch.Visible = true;
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                        btnExport.Enabled = dtPartyAdr.Rows.Count > 0;
                    }
                    else
                        custRef = "0";
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
            txtSearch.Text = "";
            cboCustZoneSrch.SelectedIndex = 0;
            var taParAdr = new tblSalesPartyAdrTableAdapter();
            var dtSaMaParAdr = taParAdr.GetData();
            Session["data"] = dtSaMaParAdr;
            SetCustomerGridData();
            btnClearSrch.Visible = false;
            btnExport.Enabled = dtSaMaParAdr.Rows.Count > 0;
        }

        protected void gvCust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);               
            }
        }

        protected void gvCust_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCust.PageIndex = e.NewPageIndex;
            SetCustomerGridData();
        }

        protected void gvCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvCust.SelectedIndex;

            if (indx != -1)
            {                
                var taCustAdr = new tblSalesPartyAdrTableAdapter();

                try
                {
                    var userRef=Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString();
                    var userEmpRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                    HiddenField hfCustRef = (HiddenField)gvCust.Rows[indx].FindControl("hfCustRef");
                    hfRefNo.Value = hfCustRef.Value;
                    hfEditStatus.Value = "Y";
                    btnSave.Text = "Edit";

                    var dtCustAdr = taCustAdr.GetDataByPartyAdrRef(Convert.ToInt32(hfRefNo.Value.ToString()));
                    if (dtCustAdr.Rows.Count > 0)
                    {
                        txtCustRefNo.Text = dtCustAdr[0].Par_Adr_Ref_No.ToString();
                        txtCustName.Text = dtCustAdr[0].Par_Adr_Name.ToString();
                        txtCustAdr.Text = dtCustAdr[0].Par_Adr_Addr.ToString();
                        txtCustCp.Text = dtCustAdr[0].Par_Adr_Cont_Per.ToString();
                        txtCustCpDesig.Text = dtCustAdr[0].Par_Adr_Desig.ToString();
                        txtCustCell.Text = dtCustAdr[0].Par_Adr_Cell_No.ToString();
                        txtCustPhone.Text = dtCustAdr[0].Par_Adr_Tel_No.ToString();
                        txtCustFax.Text = dtCustAdr[0].Par_Adr_Fax_No.ToString();
                        txtCustEmail.Text = dtCustAdr[0].Par_Adr_Email_Id.ToString();
                        txtCustCrLimit.Text = dtCustAdr[0].Par_Adr_Cr_Limit.ToString("N2");                        
                        txtCustCrPeriod.Text = dtCustAdr[0].Par_Adr_Cr_Days.ToString();
                        txtCustCrPayTerm.Text = dtCustAdr[0].Par_Adr_Pay_Trms.ToString();
                        //txtCrLimitDocEtc.Text = dtCustAdr[0].Par_Adr_Cr_Doc_Oth.ToString();
                        //txtCrSecTotAmt.Text = dtCustAdr[0].Par_Adr_Ext_Data1.ToString();
                        txtCustAccCode.Text = dtCustAdr[0].Par_Adr_Acc_Code.ToString();
                        //txtCustSalesPer.Text = dtCustAdr[0].Par_Adr_Sls_Per.ToString();                        

                        var taCrSecurity = new tblSalesCrLimitSecurityTableAdapter();
                        var dtCrSecurity = taCrSecurity.GetDataByPartyRef(Convert.ToInt32(hfRefNo.Value.ToString()));
                        if (dtCrSecurity.Rows.Count > 0)
                        {
                            txtCrSecBG.Text = dtCrSecurity[0].Sec_BG.ToString("N2");
                            txtCrSecFDR.Text = dtCrSecurity[0].Sec_FDR.ToString("N2");
                            txtCrSecSDR.Text = dtCrSecurity[0].Sec_SDR.ToString("N2");
                            txtCrSecLAND.Text = dtCrSecurity[0].Sec_LAND.ToString("N2");
                            txtCrSecCHQ.Text = dtCrSecurity[0].Sec_CHQ.ToString("N2");
                            txtCrSecNOA.Text = dtCrSecurity[0].Sec_NOA.ToString("N2");
                            txtCrSecTotAmt.Text = dtCrSecurity[0].Sec_Tot_Amt.ToString("N2");
                        }

                        cboCustType.SelectedIndex = cboCustType.Items.IndexOf(cboCustType.Items.FindByValue(dtCustAdr[0].IsPar_Adr_TypeNull() ? "0" : dtCustAdr[0].Par_Adr_Type.ToString()));
                        //cboCustType.SelectedValue = dtCustAdr[0].Par_Adr_Type.ToString();

                        cboCustDist.SelectedIndex = cboCustDist.Items.IndexOf(cboCustDist.Items.FindByValue(dtCustAdr[0].IsPar_Adr_DistNull() ? "0" : dtCustAdr[0].Par_Adr_Dist.ToString()));
                        //cboCustDist.SelectedValue = dtCustAdr[0].Par_Adr_Dist.ToString();

                        if (userRef == "100001" || userEmpRef == "000011")
                        {
                            txtCustName.Enabled = true;
                            txtCustCrLimit.Enabled = true;
                        }
                        else
                        {
                            txtCustName.Enabled = false;
                            txtCustCrLimit.Enabled = false;
                        }

                        try
                        {
                            cboCustZone.SelectedIndex = cboCustZone.Items.IndexOf(cboCustZone.Items.FindByValue(dtCustAdr[0].IsPar_Adr_Sale_ZoneNull() ? "0" : dtCustAdr[0].Par_Adr_Sale_Zone.ToString()));
                            //cboCustZone.SelectedValue = dtCustAdr[0].Par_Adr_Sale_Zone.ToString();

                            cboDsm.SelectedIndex = cboDsm.Items.IndexOf(cboDsm.Items.FindByValue(dtCustAdr[0].IsPar_Adr_Sls_PerNull() ? "0" : dtCustAdr[0].Par_Adr_Sls_Per.ToString()));
                            //cboDsm.SelectedValue = dtCustAdr[0].Par_Adr_Sls_Per.ToString();

                            //var srchWords = dtCustAdr[0].Par_Adr_Cr_Doc.ToString().Trim().Split(',');
                            //foreach (string word in srchWords)
                            //{
                            //    foreach (ListItem li in chkListCrLimitDoc.Items)
                            //    {
                            //        if (li.Text == word) li.Selected = true;
                            //    }
                            //}

                            var taDsm = new tblSalesDsmMasTableAdapter();
                            var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(cboDsm.SelectedValue.ToString()));
                            if (dtDsm.Rows.Count > 0)
                            {
                                cboSpo.Items.Clear();
                                var taMpo = new View_Sales_SpTableAdapter();
                                var dtMpo = taMpo.GetDataBySalesZoneStatus(Convert.ToInt32(dtDsm[0].Dsm_Sls_Zone.ToString()), 1);
                                foreach (dsSalesMas.View_Sales_SpRow dr in dtMpo.Rows)
                                {
                                    cboSpo.Items.Add(new ListItem(dr.Sp_Full_Name.ToString() + "-[" + dr.Sp_User_Ref.ToString() + "]", dr.Sp_Ref.ToString()));
                                }
                                cboSpo.Items.Insert(0, new ListItem("---Select---", "0"));

                                //cboSpo.DataTextField = "Sp_Full_Name";
                                //cboSpo.DataValueField = "Sp_Ref";
                                //cboSpo.DataBind();
                                //cboSpo.Items.Insert(0, new ListItem("---Select---", "0"));
                            }

                            //var taSpo = new tblSalesPersonMasTableAdapter();
                            //cboSpo.DataSource = taSpo.GetDataByDsmRef(cboDsm.SelectedValue);
                            //cboSpo.DataTextField = "Sp_Full_Name";
                            //cboSpo.DataValueField = "Sp_Ref";
                            //cboSpo.DataBind();
                            //cboSpo.Items.Insert(0, new ListItem("---Select---", "0"));

                            cboSpo.SelectedIndex = cboSpo.Items.IndexOf(cboSpo.Items.FindByValue(dtCustAdr[0].IsPar_Adr_Ext_Data2Null() ? "0" : dtCustAdr[0].Par_Adr_Ext_Data2.ToString()));

                            optListCustStatus.SelectedValue = dtCustAdr[0].Par_Adr_Status.ToString();

                            var taThana = new tblSalesThanaTableAdapter();
                            cboCustThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboCustDist.SelectedValue));
                            cboCustThana.DataTextField = "ThanaName";
                            cboCustThana.DataValueField = "ThanaRef";
                            cboCustThana.DataBind();
                            cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));

                            cboCustThana.SelectedIndex = cboCustThana.Items.IndexOf(cboCustThana.Items.FindByValue(dtCustAdr[0].IsPar_Adr_ThanaNull() ? "0" : dtCustAdr[0].Par_Adr_Thana.ToString()));
                            //cboCustThana.SelectedValue = dtCustAdr[0].Par_Adr_Thana.ToString();        

                            cboSalesWing.SelectedIndex = cboSalesWing.Items.IndexOf(cboSalesWing.Items.FindByValue(dtCustAdr[0].IsPar_Adr_Ext_Data3Null() ? "0" : dtCustAdr[0].Par_Adr_Ext_Data3.ToString()));

                        }
                        catch (Exception ex) 
                        {
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

        protected void gvCust_Sorting(object sender, GridViewSortEventArgs e)
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
                gvCust.DataSource = dataView;
                gvCust.DataBind();
            }
        }

        protected void btnUpdtCustGl_Click(object sender, EventArgs e)
        {
            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taCoa.Connection);

            try
            {
                taCoa.AttachTransaction(myTran);
                taPartyAdr.AttachTransaction(myTran);

                var i = 0;
                var dtPartyAdr = taPartyAdr.GetDataByNoAccCode();
                foreach (dsSalesMas.tblSalesPartyAdrRow dr in dtPartyAdr.Rows)
                {
                    var dtMaxCoaRef = taCoa.GetMaxCoaRef();
                    var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                    var dtMaxCoaCode = taCoa.GetMaxCoaCode();
                    var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                    var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                    taCoa.InsertCoa(nextCoaRef, nextCoaCode, dr.Par_Adr_Name, nextCoaCode, "B", "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                        "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    taPartyAdr.UpdatePartyAccCode(nextCoaCode, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), dr.Par_Adr_Ref);
                    
                    i++;
                }
                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully. " + i.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex) 
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();

            try
            {
                var custRef = "0";
                var srchWords = txtSearch.Text.Trim().Split(':');
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
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                            custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                    }
                    else
                        custRef = "0";
                }

                var qryStr = "";
                if (cboCustZoneSrch.SelectedIndex == 0)
                {
                    if (custRef.Length > 0)
                    {
                        qryStr = "SELECT [Par_Adr_Ref_No] as DLR_Ref, [Par_Adr_Name] as DLR_Name, [CustTypeName] as DLR_Type, [Par_Adr_Addr] as DLR_Address, " +
                               "[Par_Adr_Cont_Per] as DLR_Contact, [Par_Adr_Cell_No] as DLR_Cell, [Par_Adr_Tel_No] as DLR_Tel, [Par_Adr_Email_Id] as DLR_Email, " +
                               "[DistName] as DLR_District, [ThanaName] as DLR_Thana, [Par_Adr_Cr_Limit] as Cr_Limit, [Par_Adr_Cr_Days] as Cr_Days, [Dsm_Full_Name] as DSM_Name, " +
                               "[SalesZoneName] as Sales_Zone,Par_Adr_Ext_Data2 as MPO_Ref, Sp_Full_Name as MPO_Name FROM [tblSalesPartyAdr] left outer join [tblSalesPartyType] " +
                               "on [Par_Adr_Type]=[CustTypeRef] left outer join [tblSalesDistrict] on [Par_Adr_Dist]=[DistRef] left outer join [tblSalesThana] " +
                               "on [Par_Adr_Thana]=[ThanaRef] left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref left outer join tblSalesZone on Dsm_Sls_Zone=SalesZoneRef " +
                               "left outer join [tblSalesPersonMas] on Par_Adr_Ext_Data2=Sp_Ref where Par_Adr_Ref='" + custRef.ToString() + "' order by [Par_Adr_Name]";
                    }
                    else
                    {
                        qryStr = "SELECT [Par_Adr_Ref_No] as DLR_Ref, [Par_Adr_Name] as DLR_Name, [CustTypeName] as DLR_Type, [Par_Adr_Addr] as DLR_Address, " +
                               "[Par_Adr_Cont_Per] as DLR_Contact, [Par_Adr_Cell_No] as DLR_Cell, [Par_Adr_Tel_No] as DLR_Tel, [Par_Adr_Email_Id] as DLR_Email, " +
                               "[DistName] as DLR_District, [ThanaName] as DLR_Thana, [Par_Adr_Cr_Limit] as Cr_Limit, [Par_Adr_Cr_Days] as Cr_Days, [Dsm_Full_Name] as DSM_Name, " +
                               "[SalesZoneName] as Sales_Zone,Par_Adr_Ext_Data2 as MPO_Ref, Sp_Full_Name as MPO_Name FROM [tblSalesPartyAdr] left outer join [tblSalesPartyType] " +
                               "on [Par_Adr_Type]=[CustTypeRef] left outer join [tblSalesDistrict] on [Par_Adr_Dist]=[DistRef] left outer join [tblSalesThana] " +
                               "on [Par_Adr_Thana]=[ThanaRef] left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref left outer join tblSalesZone on Dsm_Sls_Zone=SalesZoneRef " +
                               "left outer join [tblSalesPersonMas] on Par_Adr_Ext_Data2=Sp_Ref order by [Par_Adr_Name]";
                    }
                }
                else
                {
                    if (custRef.Length > 0)
                    {
                        qryStr = "SELECT [Par_Adr_Ref_No] as DLR_Ref, [Par_Adr_Name] as DLR_Name, [CustTypeName] as DLR_Type, [Par_Adr_Addr] as DLR_Address, " +
                                   "[Par_Adr_Cont_Per] as DLR_Contact, [Par_Adr_Cell_No] as DLR_Cell, [Par_Adr_Tel_No] as DLR_Tel, [Par_Adr_Email_Id] as DLR_Email, " +
                                   "[DistName] as DLR_District, [ThanaName] as DLR_Thana, [Par_Adr_Cr_Limit] as Cr_Limit, [Par_Adr_Cr_Days] as Cr_Days, [Dsm_Full_Name] as DSM_Name, " +
                                   "[SalesZoneName] as Sales_Zone,Par_Adr_Ext_Data2 as MPO_Ref, Sp_Full_Name as MPO_Name FROM [tblSalesPartyAdr] left outer join [tblSalesPartyType] " +
                                   "on [Par_Adr_Type]=[CustTypeRef] left outer join [tblSalesDistrict] on [Par_Adr_Dist]=[DistRef] left outer join [tblSalesThana] " +
                                   "on [Par_Adr_Thana]=[ThanaRef] left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref left outer join tblSalesZone on Dsm_Sls_Zone=SalesZoneRef " +
                                   "left outer join [tblSalesPersonMas] on Par_Adr_Ext_Data2=Sp_Ref where Par_Adr_Sale_Zone='" + cboCustZoneSrch.SelectedValue.ToString() +
                                   "' and Par_Adr_Ref='" + custRef.ToString() + "' order by [Par_Adr_Name]";
                    }
                    else
                    {
                        qryStr = "SELECT [Par_Adr_Ref_No] as DLR_Ref, [Par_Adr_Name] as DLR_Name, [CustTypeName] as DLR_Type, [Par_Adr_Addr] as DLR_Address, " +
                                   "[Par_Adr_Cont_Per] as DLR_Contact, [Par_Adr_Cell_No] as DLR_Cell, [Par_Adr_Tel_No] as DLR_Tel, [Par_Adr_Email_Id] as DLR_Email, " +
                                   "[DistName] as DLR_District, [ThanaName] as DLR_Thana, [Par_Adr_Cr_Limit] as Cr_Limit, [Par_Adr_Cr_Days] as Cr_Days, [Dsm_Full_Name] as DSM_Name, " +
                                   "[SalesZoneName] as Sales_Zone,Par_Adr_Ext_Data2 as MPO_Ref, Sp_Full_Name as MPO_Name FROM [tblSalesPartyAdr] left outer join [tblSalesPartyType] " +
                                   "on [Par_Adr_Type]=[CustTypeRef] left outer join [tblSalesDistrict] on [Par_Adr_Dist]=[DistRef] left outer join [tblSalesThana] " +
                                   "on [Par_Adr_Thana]=[ThanaRef] left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref left outer join tblSalesZone on Dsm_Sls_Zone=SalesZoneRef " +
                                   "left outer join [tblSalesPersonMas] on Par_Adr_Ext_Data2=Sp_Ref where Par_Adr_Sale_Zone='" + cboCustZoneSrch.SelectedValue.ToString() + "' order by [Par_Adr_Name]";
                    }
                }

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
                string filename = String.Format("Dealer_List_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
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

        protected void cboDsm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var taSpo = new tblSalesPersonMasTableAdapter();
            //cboSpo.DataSource = taSpo.GetDataByDsmRef(cboDsm.SelectedValue);
            //cboSpo.DataTextField = "Sp_Full_Name";
            //cboSpo.DataValueField = "Sp_Ref";
            //cboSpo.DataBind();
            //cboSpo.Items.Insert(0, new ListItem("---Select---", "0"));

            var taDsm = new tblSalesDsmMasTableAdapter();
            var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(cboDsm.SelectedValue.ToString()));
            if (dtDsm.Rows.Count > 0)
            {
                //var taMpo = new View_Sales_SpTableAdapter();
                //cboSpo.DataSource = taMpo.GetDataBySalesZoneStatus(Convert.ToInt32(dtDsm[0].Dsm_Sls_Zone.ToString()), 1);
                //cboSpo.DataTextField = "Sp_Full_Name";
                //cboSpo.DataValueField = "Sp_Ref";
                //cboSpo.DataBind();
                //cboSpo.Items.Insert(0, new ListItem("---Select---", "0"));

                cboSpo.Items.Clear();
                var taMpo = new View_Sales_SpTableAdapter();
                var dtMpo = taMpo.GetDataBySalesZoneStatus(Convert.ToInt32(dtDsm[0].Dsm_Sls_Zone.ToString()), 1);
                foreach (dsSalesMas.View_Sales_SpRow dr in dtMpo.Rows)
                {
                    cboSpo.Items.Add(new ListItem(dr.Sp_Full_Name.ToString() + "-[" + dr.Sp_User_Ref.ToString() + "]", dr.Sp_Ref.ToString()));
                }
                cboSpo.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }

        protected void btnCrSecurity_Click(object sender, EventArgs e)
        {
            var url = "frmSalesCrSecurity.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void cboCustZoneSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taParAdr = new tblSalesPartyAdrTableAdapter();
            var dtParAdr = new dsSalesMas.tblSalesPartyAdrDataTable();

            #region Get Customer Ref
            var custRef = "0";
            var srchWords = txtSearch.Text.Trim().Split(':');
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
                    var dtParAdrNew = taParAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                    if (dtParAdrNew.Rows.Count > 0)
                        custRef = dtParAdrNew[0].Par_Adr_Ref.ToString();
                }
                else
                    custRef = "0";
            }
            #endregion

            if (cboCustZoneSrch.SelectedIndex == 0)
            {
                if (custRef.Length > 0)
                    dtParAdr = taParAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef.ToString()));
                else
                    dtParAdr = taParAdr.GetDataByNameSort();
            }
            else
            {
                if (custRef.Length > 0)
                    dtParAdr = taParAdr.GetDataBySaleZoneParAdrRef(Convert.ToInt32(cboCustZoneSrch.SelectedValue.ToString()), Convert.ToInt32(custRef.ToString()));
                else
                    dtParAdr = taParAdr.GetDataBySalesZone(Convert.ToInt32(cboCustZoneSrch.SelectedValue.ToString()));                
            }

            Session["data"] = dtParAdr;
            SetCustomerGridData();

            AutoCompleteExtenderSrch.ContextKey = cboCustZoneSrch.SelectedValue.ToString();

            btnExport.Enabled = dtParAdr.Rows.Count > 0;
        }
    }
}