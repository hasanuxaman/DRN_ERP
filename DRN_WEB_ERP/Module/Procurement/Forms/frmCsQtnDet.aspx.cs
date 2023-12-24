using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmCsQtnDet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tbltooltip.Visible = false;
            tblmaster.BgColor = "#f0f8ff";

            if (!Page.IsPostBack)
            {
                Session["sessionPartySelFlag"] = "";

                string mprRef = "", itemCode = "";
                if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                {
                    mprRef = Session["QtnMprRefNo"].ToString(); itemCode = Session["QtnItemCode"].ToString();
                    //tblquotation.Visible = true;
                }
                generate_detail_data(mprRef + ":" + itemCode);

                tbltooltip.Visible = false;
            }
        }

        private string get_my_app()
        {
            //User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
            //SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();

            string my_app = "";
            //udt = urole.GetRoleByUser(Session["sessionUserId"].ToString(), "CS");
            //if (udt.Rows.Count > 0) my_app = udt[0].role_as.ToString();

            return my_app;
        }

        private bool getbatchper(string app_code)
        {
            //tbl_app_ruleTableAdapter app = new tbl_app_ruleTableAdapter();
            //SCBLDataSet.tbl_app_ruleDataTable dt = new SCBLDataSet.tbl_app_ruleDataTable();
            //bool aper;
            //dt = app.GetDataByAppId(app_code);

            //if (dt.Rows.Count == 0)
            //{
            //    aper = false;
            //}
            //else
            //{
            //    if (dt[0].app_per == 1) aper = true; else aper = false;
            //}

            bool aper = true;
            return aper;
        }

        private bool getaper(string ttype, string app_code)
        {
            //tbl_app_ruleTableAdapter app = new tbl_app_ruleTableAdapter();
            //SCBLDataSet.tbl_app_ruleDataTable dt = new SCBLDataSet.tbl_app_ruleDataTable();
            //bool aper;
            //dt = app.GetDataByTypeApp(ttype, app_code);

            //if (dt.Rows.Count == 0)
            //{
            //    aper = false;
            //}
            //else
            //{
            //    if (dt[0].app_per == 1) aper = true; else aper = false;
            //}

            bool aper = true;
            return aper;
        }

        private bool getedtper(string ttype, string app_code)
        {
            //tbl_app_ruleTableAdapter app = new tbl_app_ruleTableAdapter();
            //SCBLDataSet.tbl_app_ruleDataTable dt = new SCBLDataSet.tbl_app_ruleDataTable();
            //bool edtper;
            //dt = app.GetDataByTypeApp(ttype, app_code);

            //if (dt.Rows.Count == 0)
            //{
            //    edtper = false;
            //}
            //else
            //{
            //    if (dt[0].edit_per == 1) edtper = true; else edtper = false;
            //}

            bool edtper = true;
            return edtper;
        }

        private bool getrper(string ttype, string app_code)
        {
            //tbl_app_ruleTableAdapter app = new tbl_app_ruleTableAdapter();
            //SCBLDataSet.tbl_app_ruleDataTable dt = new SCBLDataSet.tbl_app_ruleDataTable();
            //bool rper;
            //dt = app.GetDataByTypeApp(ttype, app_code);

            //if (dt.Rows.Count == 0)
            //{
            //    rper = false;
            //}
            //else
            //{
            //    if (dt[0].rej_per == 1) rper = true; else rper = false;
            //}
            bool rper = true;
            return rper;
        }

        private void generate_detail_data(string mprref_itemcode)
        {
            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable dtlog = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_PuTr_Pr_DetTableAdapter srdet = new tbl_PuTr_Pr_DetTableAdapter();

            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetDataTable dt = new dsProcTran.tbl_Qtn_DetDataTable();

            dsProcTran.tbl_PuTr_Pr_DetRow dr;

            tbl_PuTr_PO_DetTableAdapter podet = new tbl_PuTr_PO_DetTableAdapter();
            dsProcTran.tbl_PuTr_PO_DetDataTable dtrecent = new dsProcTran.tbl_PuTr_PO_DetDataTable();

            tbl_PuMa_Par_AdrTableAdapter adr = new tbl_PuMa_Par_AdrTableAdapter();
            dsProcMas.tbl_PuMa_Par_AdrDataTable dtadr;

            tbl_InMa_Stk_CtlTableAdapter stk = new tbl_InMa_Stk_CtlTableAdapter();
            dsInvTran.tbl_InMa_Stk_CtlDataTable dtstk = new dsInvTran.tbl_InMa_Stk_CtlDataTable();

            ListItem lst;
            string my_app = get_my_app();
            decimal qty;
            Label lbltooltip;
            Literal ltrl;
            els.ToolTip toolt;
            string ref_no, icode, mprref, tac_ref, genstr, spestr, paystr, pay_type, sel_party_code;
            string[] tmp;
            int cur_index, i, gcnt, scnt, pcnt;
            int vdays = 0;

            CheckBox chklist;
            Control ctl;

            if (mprref_itemcode == "0")
            {
                tbl_product.Visible = false;
                tbl_party.Visible = false;
                return;
            }
            else
            {
                tbl_product.Visible = true;
                tbl_party.Visible = true;
            }

            tmp = mprref_itemcode.Split(':');
            mprref = tmp[0].ToString();
            icode = tmp[1].ToString();

            dr = srdet.GetDataByPrRefItem(mprref, icode)[0];

            lblmpr.Text = dr.Pr_Det_Ref.ToString();
            lblqref.Text = dr.Pr_Det_Quot_Ref.ToString();
            lblproduct.Text = dr.Pr_Det_Icode.ToString() + " : " + dr.Pr_Det_Itm_Desc.ToString();
            lblqty.Text = dr.Pr_Det_Lin_Qty.ToString() + " " + dr.Pr_Det_Itm_Uom.ToString();

            var taViewPoDet = new View_PuTr_Po_Hdr_DetTableAdapter();
            var dtViewPoDet = taViewPoDet.GetDataByItemCode(dr.Pr_Det_Icode.ToString());
            gvRecentRate.DataSource = dtViewPoDet;
            gvRecentRate.DataBind();

            dtstk = stk.GetDataByIcode(icode);

            if (dtstk.Rows.Count == 0)
            {
                lblcurstk.Text = "";
            }
            else
            {
                lblcurstk.Text = dtstk[0].Stk_Ctl_Free_Stk.ToString("N2") + " " + dr.Pr_Det_Itm_Uom.ToString();
            }

            lblreq.Text = dr.Pr_Det_Code.ToString() + ", " + dr.Pr_Det_Pur_Type.ToString();
            lblremarks.Text = dr.Pr_Det_Rem;

            qty = (decimal)dr.Pr_Det_Lin_Qty;
            sel_party_code = dr.Pr_Det_App_Party.ToString();

            //quotation
            ref_no = dr.Pr_Det_Quot_Ref.ToString();

            dt = quo.GetDataByMprRefItemCode(mprref, icode);
            cur_index = 1;

            tbl_party.Rows[0].Cells[1].Visible = false;
            foreach (dsProcTran.tbl_Qtn_DetRow drquo in dt.Rows)
            {
                if (cur_index == 16) break;

                //if (Session["sessionPartySelFlag"].ToString() != ref_no)
                //{
                //    if (drquo.Qtn_Par_Code.ToString() == sel_party_code)
                //    {
                //        rdolist = new HtmlInputRadioButton();
                //        ctl = new Control();
                //        ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
                //        rdolist = (HtmlInputRadioButton)ctl;
                //        rdolist.Checked = true;
                //    }
                //    else
                //    {
                //        rdolist = new HtmlInputRadioButton();
                //        ctl = new Control();
                //        ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
                //        rdolist = (HtmlInputRadioButton)ctl;
                //        rdolist.Checked = false;
                //    }
                //}

                dtadr = new dsProcMas.tbl_PuMa_Par_AdrDataTable();
                dtadr = adr.GetDataBySupAdrRef(drquo.Qtn_Par_Code);

                tbl_party.Rows[cur_index].Cells[1].InnerText = drquo.Qtn_Par_Code;
                if (dtadr.Rows.Count == 0)
                {
                    tbl_party.Rows[cur_index].Cells[2].InnerText = drquo.Qtn_Par_Det;
                }
                else
                {
                    //tbl_party.Rows[cur_index].Cells[2].InnerText = drquo.Qtn_Par_Det + ", " + dtadr[0].Par_Adr_Addr  + " Ph: " + dtadr[0].Par_Adr_Tel_No + " " + dtadr[0].Par_Adr_Lst_No + " Fax: " + dtadr[0].Par_Adr_Fax_No + " Email: " + dtadr[0].Par_Adr_Email_Id;

                    var phNo = (dtadr[0].IsPar_Adr_Tel_NoNull() || dtadr[0].Par_Adr_Tel_No == "") ? "" : ", Ph: " + dtadr[0].Par_Adr_Tel_No;
                    var faxNo = (dtadr[0].IsPar_Adr_Fax_NoNull() || dtadr[0].Par_Adr_Fax_No == "") ? "" : ", Fax: " + dtadr[0].Par_Adr_Fax_No;
                    var emaiId = (dtadr[0].IsPar_Adr_Email_IdNull() || dtadr[0].Par_Adr_Email_Id == "") ? "" : ", Email: " + dtadr[0].Par_Adr_Email_Id;

                    tbl_party.Rows[cur_index].Cells[2].InnerText = drquo.Qtn_Par_Det + ", " + dtadr[0].Par_Adr_Addr + phNo + faxNo + emaiId;
                }
                //tooltip

                genstr = "";
                spestr = "";
                paystr = "";
                gcnt = 0;
                scnt = 0;
                pcnt = 0;

                tac_ref = drquo.Qtn_Gen_Terms;
                dtlog = log.GetDataByRef(tac_ref);

                foreach (dsProcTran.tbl_TaC_LogRow drlog in dtlog.Rows)
                {
                    vdays = drlog.TaC_Valid_Days;
                    switch (drlog.TaC_Type)
                    {
                        case "GEN":
                            {
                                gcnt++;
                                genstr = genstr + gcnt.ToString() + ". " + drlog.TaC_Content_Det.ToString() + "</br>";
                                break;
                            }

                        case "SPE":
                            {
                                scnt++;
                                spestr = spestr + scnt.ToString() + ". " + drlog.TaC_Content_Det.ToString() + "</br>";
                                break;
                            }

                        case "PAY":
                            {
                                pcnt++;
                                paystr = paystr + pcnt.ToString() + ". " + drlog.TaC_Content_Det.ToString() + "</br>";
                                break;
                            }
                    }
                }

                pay_type = "";

                if (log.GetDataByPayType(tac_ref, "PAY", "Full").Rows.Count > 0)
                    pay_type = "Full";
                else if (log.GetDataByPayType(tac_ref, "PAY", "Part").Rows.Count > 0)
                    pay_type = "Part";
                else
                    pay_type = "No";

                tbltooltip.Rows[0].Cells[0].InnerText = drquo.Qtn_Par_Det.ToUpper();
                tbltooltip.Rows[2].Cells[1].InnerHtml = genstr;
                tbltooltip.Rows[3].Cells[1].InnerHtml = spestr;
                tbltooltip.Rows[4].Cells[0].InnerHtml = "Pay Terms(" + pay_type + ")";
                tbltooltip.Rows[4].Cells[1].InnerHtml = paystr;
                tbltooltip.Rows[5].Cells[1].InnerHtml = vdays.ToString();

                lbltooltip = new Label();
                lbltooltip.ForeColor = System.Drawing.Color.Black;
                lbltooltip.Text = cur_index.ToString() + ".";
                tbl_party.Rows[cur_index].Cells[0].Controls.Add(lbltooltip);
                ltrl = new Literal();
                toolt = new els.ToolTip();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                tbltooltip.RenderControl(hw);
                toolt.Add(lbltooltip, ltrl, sb.ToString());
                toolt.Build();
                tbl_party.Rows[cur_index].Cells[0].Controls.Add(ltrl);

                tbl_party.Rows[cur_index].Cells[3].InnerText = drquo.Qtn_Itm_Rate.ToString("N2");
                tbl_party.Rows[cur_index].Cells[4].InnerText = (drquo.Qtn_Itm_Rate * qty).ToString("N2");
                tbl_party.Rows[cur_index].Cells[5].InnerText = drquo.Qtn_Itm_Spec;
                tbl_party.Rows[cur_index].Cells[6].InnerText = drquo.Qtn_Itm_Brand;
                tbl_party.Rows[cur_index].Cells[7].InnerText = drquo.Qtn_Itm_Origin;
                tbl_party.Rows[cur_index].Cells[8].InnerText = drquo.Qtn_Itm_Packing;

                tbl_party.Rows[cur_index].Cells[1].Visible = false;
                tbl_party.Rows[cur_index].Visible = true;

                cur_index += 1;
            }

            Session["sessionPartySelFlag"] = ref_no;

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            for (i = 1; i <= 15; i++)
            {
                chklist = new CheckBox();
                ctl = new Control();
                ctl = tbl_party.Rows[i].Cells[0].Controls[1];
                chklist = (CheckBox)ctl;
                chklist.Checked = false;

                if (empRef == "000002" || empRef == "000480" || empRef == "000134" || empRef == "000075" || empRef == "000277" || empRef == "000442" || empRef == "000683")//mamun,tanvir,hahib,mogakkir,noor,tariqul,emran
                {
                    if (dr.Pr_Det_Quot_Ref == "")
                        chklist.Enabled = true;
                    else
                        chklist.Enabled = false;
                }

                if (empRef == "000914" || empRef == "000509" || empRef == "000510" || empRef == "000535" || empRef == "000549" || empRef == "000732" || empRef == "000011") //sohag,alwashib,riaz,Alif,Saroar,kawshik-----Audit
                {
                    chklist.Enabled = false;
                }

                if (dr.Pr_Det_Status == "APR" || dr.Pr_Det_Status == "POC")
                    chklist.Enabled = false;

                if (i >= cur_index)
                    tbl_party.Rows[i].Visible = false;
            }

            if (dr.Pr_Det_Status == "APR" || dr.Pr_Det_Status == "POC")
            {
                btnAddQtn.Enabled = false;
                btnDeleteQtn.Enabled = false;
                btnedit.Enabled = false;
                btneditval.Enabled = false;
            }

            if (empRef == "000002" || empRef == "000134" || empRef == "000442" || empRef == "000683")//mamun,hahib,tariqul,emran
            {
                if (dr.Pr_Det_Quot_Ref == "")
                {
                    btnAddQtn.Enabled = true;
                    btnedit.Enabled = true;
                    btneditval.Enabled = true;
                }
            }            
        }

        private bool check_approval_validity(string icode, string quoref)
        {
            tbl_PuTr_Pr_DetTableAdapter det = new tbl_PuTr_Pr_DetTableAdapter();
            bool flg = false;
            string my_app = get_my_app();

            if (det.GetDataByQtnRefItemStatus(quoref, icode, "APR").Count > 0) flg = true;

            return flg;
        }

        private int GetSelectedIndex()
        {
            int indx = -1;
            int i;

            HtmlInputRadioButton rdolist;
            Control ctl;

            for (i = 1; i < 11; i++)
            {
                rdolist = new HtmlInputRadioButton();
                ctl = new Control();

                ctl = tbl_party.Rows[i].Cells[0].Controls[1];

                rdolist = (HtmlInputRadioButton)ctl;

                if (rdolist.Checked) { indx = i; }
            }

            return indx;
        }        

        #region TnCLinkButtonClick
        protected void lnktc1_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc2_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[2].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc3_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[3].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc4_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[4].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc5_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[5].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc6_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[6].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc7_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[7].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc8_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[8].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc9_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[9].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc10_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[10].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc11_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[11].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc12_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[12].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc13_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[13].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc14_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[14].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }

        protected void lnktc15_Click(object sender, EventArgs e)
        {
            string mprref = "", icode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();
            string pcode = tbl_party.Rows[15].Cells[1].InnerText;
            show_tooltip(mprref, icode, pcode);
        }
        #endregion

        private void show_tooltip(string Mprref,string Icode, string Pcode)
        {
            string my_app = get_my_app();

            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable dtlog = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_PuTr_Pr_DetTableAdapter srdet = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetRow dr;

            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetRow drquo;

            string icode, prref, pay_type, genstr, spestr, paystr, tac_ref;
            int gcnt, scnt, pcnt;
            int vdays = 0;

            icode = Icode;
            prref = Mprref;

            dr = srdet.GetDataByPrRefItem(prref, icode)[0];
            drquo = quo.GetDataByQtnRefParty(dr.Pr_Det_Quot_Ref, Pcode)[0];

            genstr = "";
            spestr = "";
            paystr = "";
            gcnt = 0;
            scnt = 0;
            pcnt = 0;

            tac_ref = drquo.Qtn_Gen_Terms;
            dtlog = log.GetDataByRef(tac_ref);

            foreach (dsProcTran.tbl_TaC_LogRow drlog in dtlog.Rows)
            {
                vdays = drlog.TaC_Valid_Days;
                switch (drlog.TaC_Type)
                {
                    case "GEN":
                        {
                            gcnt++;
                            genstr = genstr + gcnt.ToString() + ". " + drlog.TaC_Content_Det.ToString() + "<br />";
                            break;
                        }

                    case "SPE":
                        {
                            scnt++;
                            spestr = spestr + scnt.ToString() + ". " + drlog.TaC_Content_Det.ToString() + "<br />";
                            break;
                        }

                    case "PAY":
                        {
                            pcnt++;
                            paystr = paystr + pcnt.ToString() + ". " + drlog.TaC_Content_Det.ToString() + "<br />";
                            break;
                        }
                }
            }

            pay_type = "";

            if (log.GetDataByPayType(tac_ref, "PAY", "Full").Rows.Count > 0)
                pay_type = "Full";
            else if (log.GetDataByPayType(tac_ref, "PAY", "Part").Rows.Count > 0)
                pay_type = "Part";
            else
                pay_type = "No";

            tbltooltippnl.Rows[0].Cells[0].InnerText = drquo.Qtn_Par_Det.ToUpper();
            tbltooltippnl.Rows[2].Cells[1].InnerHtml = genstr;
            tbltooltippnl.Rows[3].Cells[1].InnerHtml = spestr;
            tbltooltippnl.Rows[4].Cells[0].InnerHtml = "Pay Terms(" + pay_type + ")";
            tbltooltippnl.Rows[4].Cells[1].InnerHtml = paystr;
            tbltooltippnl.Rows[5].Cells[1].InnerHtml = vdays.ToString();

            btnedit.PostBackUrl = "./frmQtnTaCEdit.aspx?tac_log_id=" + tac_ref + "&mprref=" + prref + "&icode=" + icode + "&pcode=" + Pcode + "&focus_ref_no=" + lblqref.Text.Trim();
            btneditval.PostBackUrl = "./frmQtnValEdit.aspx?mprref=" + prref + "&icode=" + icode + "&pcode=" + Pcode + "&focusref=" + lblqref.Text.Trim();

            if (dr.Pr_Det_Status == "APR" || dr.Pr_Det_Status == "POC")
            {
                btnedit.Enabled = false;
                btneditval.Enabled = false;
            }

            ModalPopupExtender5.Show();
        }

        protected void btnQtnPrint_Click(object sender, EventArgs e)
        {
            //Session["QtnRefNoPrint"] = ddllist.SelectedValue.ToString();
            //var url = "frmQuotCsPrint.aspx";
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void btnedit_Click(object sender, EventArgs e)
        {

        }

        protected void btneditval_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string qref;
            string[] tmp;
            tmp = lblproduct.Text.Split(':');
            qref = lblqref.Text;           
            string mprRef = "", itemCode = "";
            if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)            
                mprRef = Session["QtnMprRefNo"].ToString(); itemCode = Session["QtnItemCode"].ToString();
            var mpr_icode = mprRef + ":" + itemCode;
            Session["sessionQuotationRef"] = mpr_icode;
            Session["sessionCsRef"] = lblqref.Text.Trim();
            Session["sessionCurrentRefFocus"] = "";
            Response.Redirect("./frmQtnEntryCs.aspx");
        }

        protected void lnkPoRef_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var PoRefNo = ((LinkButton)row.FindControl("lnkPoRef")).Text.Trim();
            Session["PoRefNoPrint"] = PoRefNo.ToString();
            var url = "frmPoInquery.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);

            //if (ddllist.SelectedItem.Text.Trim().Substring(0, 3) == "LPO")
            //    Response.Redirect("./frmPoView.aspx");
            //else if (ddllist.SelectedItem.Text.Trim().Substring(0, 3) == "SPO")
            //{
            //    var url = "frmPoPrintSPO.aspx?PO_REF=" + ddllist.SelectedValue.ToString();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            //}
        }

        protected void lnkCsRef_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var CsRefNo = ((LinkButton)row.FindControl("lnkCsRef")).Text.Trim();
            Session["CsRefNoPrint"] = CsRefNo.ToString();
            var url = "frmCsInq.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        private void ReadyData()
        {
            /*
            tbl_PuMa_Par_AdrTableAdapter adr = new tbl_PuMa_Par_AdrTableAdapter();
            dsProcMas.tbl_PuMa_Par_AdrRow row;
            tbl_PuTr_PO_HdrTableAdapter hdr = new tbl_PuTr_PO_HdrTableAdapter();
            string[] tempdata, tmp;

            tmp = txtparty.Text.Split(':');

            if (tmp.Length < 3) return;

            string pdet = tmp[1] + ":" + tmp[2];

            string pcode = hdr.GetDataByHdrRef(ddllist.SelectedValue.ToString())[0].PO_Hdr_Pcode.ToString();

            tempdata = new string[12];
            tempdata[0] = txtdate.Text;
            tempdata[1] = tmp[2].ToString();
            tempdata[2] = "Purchase Order";
            tempdata[3] = txtfrom.Text;
            row = adr.GetDataBySupAdrRef(pcode)[0];

            tempdata[4] = pdet;
            tempdata[5] = row.Par_Adr_Addr.ToString();

            //tempdata[9] = celvaliddays.InnerHtml;
            tempdata[10] = tmp[1];
            tempdata[11] = lblpaytype.Text;
            Session["sessionTempPrintData"] = tempdata;
            Session["sessionTempHtmlTable"] = tblhtml;
            Session["sessionGenHtmlTable"] = tblgen;
            Session["sessionSpeHtmlTable"] = tblspe;
            Session["sessionPayHtmlTable"] = tblpay;              
             */
        }

        private void checkQtn()
        {
            CheckBox chklist;
            Control ctl;

            for (int i = 1; i <= 15; i++)
            {
                chklist = new CheckBox();
                ctl = new Control();
                ctl = tbl_party.Rows[i].Cells[0].Controls[1];
                chklist = (CheckBox)ctl;

                if (chklist.Checked)
                {
                    btnDeleteQtn.Enabled = true;
                    break;
                }
                else
                    btnDeleteQtn.Enabled = false;
            }
        }

        #region QuotationCheckboxClick
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox11_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox12_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox13_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox14_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }

        protected void CheckBox15_CheckedChanged(object sender, EventArgs e)
        {
            checkQtn();
        }
        #endregion

        protected void btnDeleteQtn_Click(object sender, EventArgs e)
        {
            var taQtnDet = new tbl_Qtn_DetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taQtnDet.Connection);

            try
            {
                taQtnDet.AttachTransaction(myTran);

                string mprref = "", icode = "";
                if (Session["QtnMprRefNo"] != null || Session["QtnItemCode"] != null)
                    mprref = Session["QtnMprRefNo"].ToString(); icode = Session["QtnItemCode"].ToString();

                CheckBox chklist;
                Control ctl;

                for (int i = 1; i <= 15; i++)
                {
                    chklist = new CheckBox();
                    ctl = new Control();
                    ctl = tbl_party.Rows[i].Cells[0].Controls[1];
                    chklist = (CheckBox)ctl;

                    if (chklist.Checked)
                    {
                        string pcode = tbl_party.Rows[i].Cells[1].InnerText;
                        taQtnDet.DeleteQtnByMprItemParty(mprref.ToString(), icode.ToString(), pcode.ToString()); 
                    }
                }
                               
                myTran.Commit();
                
                generate_detail_data(mprref + ":" + icode);
            }
            catch(Exception ex)
            {
                myTran.Rollback();
            }
        }
    }
}