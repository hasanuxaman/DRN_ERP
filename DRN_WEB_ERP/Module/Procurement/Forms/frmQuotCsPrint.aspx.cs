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
    public partial class frmQuotCsPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";

            if (Page.IsPostBack) return;

            if (Session["QtnRefNoPrint"] != null)
                generate_detail_data(Session["QtnRefNoPrint"].ToString());            
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

        private void generate_comments(string ref_no)
        {
            tbl_Tran_ComTableAdapter com = new tbl_Tran_ComTableAdapter();
            dsProcTran.tbl_Tran_ComDataTable dt = new dsProcTran.tbl_Tran_ComDataTable();
            View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();

            dt = com.GetDataByRefNo(ref_no);
            phcomments.Controls.Clear();

            var qtnSeqNo = 0;
            foreach (dsProcTran.tbl_Tran_ComRow dr in dt.Rows)
            {
                qtnSeqNo = dr.Com_Seq_no;

                Module.Procurement.Forms.UserControl.ctlQtnCom ctl = (Module.Procurement.Forms.UserControl.ctlQtnCom)LoadControl("./UserControl/CtlQtnCom.ascx");
                
                Label lblname = (Label)ctl.FindControl("lblname");
                Label lbldate = (Label)ctl.FindControl("lbldate");
                HtmlTableCell celcomm = (HtmlTableCell)ctl.FindControl("celcomm");
                Image imgimage = (Image)ctl.FindControl("imgimage");

                //imgimage.ImageUrl = "./handler/hnd_image.ashx?id=" + dr.app_id;

                //imgimage.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dr.Com_App_Id + "'";

                var dtEmp = taEmp.GetDataByEmpId(dr.Com_App_Id.ToString());

                imgimage.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + (dtEmp.Rows.Count > 0 ? dtEmp[0].EmpRefNo : "") + "'";

                ctl.ID = "ctl_" + phcomments.Controls.Count.ToString();

                lblname.Text = dr.Com_App_Name.ToString() + " (" + dr.Com_App_Desig.ToString() + ")";
                lbldate.Text = dr.Com_App_Date.ToString();
                celcomm.InnerText = dr.Com_Gen_Com.ToString();

                phcomments.Controls.Add(ctl);
            }
        }

        private void generate_signature(string ref_no)
        {
            tbl_Tran_ComTableAdapter com = new tbl_Tran_ComTableAdapter();
            dsProcTran.tbl_Tran_ComDataTable dt = new dsProcTran.tbl_Tran_ComDataTable();            

            dt = com.GetDataByRefNo(ref_no);                        
            foreach (dsProcTran.tbl_Tran_ComRow dr in dt.Rows)
            {
                if (dr.Com_Seq_no.ToString() == "1") lblPepBy.Text = dr.Com_App_Name.ToString();
                if (dr.Com_Seq_no.ToString() == "2") lblVerBy.Text = dr.Com_App_Name.ToString();
            }
        }

        private void generate_detail_data(string icode_quoref)
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
            string ref_no, icode, qref, tac_ref, genstr, spestr, paystr, pay_type, sel_party_code;
            string[] tmp;
            int cur_index, i, gcnt, scnt, pcnt;
            int vdays = 0;

            HtmlInputRadioButton rdolist;
            Control ctl;

            if (icode_quoref == "0")
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

            tmp = icode_quoref.Split(':');
            icode = tmp[0].ToString();
            qref = tmp[1].ToString();

            dr = srdet.GetDataByQtnRefItem(qref, icode)[0];            

            generate_comments(dr.Pr_Det_Quot_Ref.ToString());

            generate_signature(dr.Pr_Det_Quot_Ref.ToString());

            lblmpr.Text = dr.Pr_Det_Ref.ToString();
            lblqref.Text = dr.Pr_Det_Quot_Ref.ToString();
            lblproduct.Text = dr.Pr_Det_Icode.ToString() + " : " + dr.Pr_Det_Itm_Desc.ToString();
            lblqty.Text = dr.Pr_Det_Lin_Qty.ToString() + " " + dr.Pr_Det_Itm_Uom.ToString();

            dtstk = stk.GetDataByIcode(icode);

            if (dtstk.Rows.Count == 0)
            {
                lblcurstk.Text = "";
            }
            else
            {
                lblcurstk.Text = dtstk[0].Stk_Ctl_Free_Stk.ToString("N2") + " " + dr.Pr_Det_Itm_Uom.ToString();
            }


            //lblreq.Text = dr.Pr_Det_Code.ToString() + ", " + dr.Pr_Det_Pur_Type.ToString();
            lblremarks.Text = dr.Pr_Det_Rem;
            
            qty = (decimal)dr.Pr_Det_Lin_Qty;
            sel_party_code = dr.Pr_Det_App_Party.ToString();

            //quotation
            ref_no = dr.Pr_Det_Quot_Ref.ToString();

            dt = quo.GetDataByQtnRef(ref_no);
            cur_index = 1;

            tbl_party.Rows[0].Cells[1].Visible = false;
            foreach (dsProcTran.tbl_Qtn_DetRow drquo in dt.Rows)
            {
                if (cur_index == 16) break;

                if (Session["QtnRefNoPrint"].ToString() != ref_no)
                {
                    if (drquo.Qtn_Par_Code.ToString() == sel_party_code)
                    {
                        rdolist = new HtmlInputRadioButton();
                        ctl = new Control();
                        ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
                        rdolist = (HtmlInputRadioButton)ctl;
                        rdolist.Checked = true;
                    }
                    else
                    {
                        rdolist = new HtmlInputRadioButton();
                        ctl = new Control();
                        ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
                        rdolist = (HtmlInputRadioButton)ctl;
                        rdolist.Checked = false;
                    }
                }

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

                lbltooltip = new Label();
                lbltooltip.ForeColor = System.Drawing.Color.Black;
                lbltooltip.Text = cur_index.ToString() + ".";
                tbl_party.Rows[cur_index].Cells[0].Controls.Add(lbltooltip);
                ltrl = new Literal();
                toolt = new els.ToolTip();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(sw);
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

            Session["QtnRefNoPrint"] = ref_no;

            for (i = cur_index; i <= 15; i++)
            {
                rdolist = new HtmlInputRadioButton();
                ctl = new Control();
                ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
                rdolist = (HtmlInputRadioButton)ctl;
                rdolist.Checked = false;

                tbl_party.Rows[i].Visible = false;
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

        private void show_tooltip(string icode_qref, string pcode)
        {
            string my_app = get_my_app();

            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable dtlog = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_PuTr_Pr_DetTableAdapter srdet = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetRow dr;

            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetRow drquo;

            string[] tmp;
            string icode, qref, pay_type, genstr, spestr, paystr, tac_ref;
            int gcnt, scnt, pcnt;
            int vdays = 0;

            tmp = icode_qref.Split(':');
            icode = tmp[0].ToString();
            qref = tmp[1].ToString();

            dr = srdet.GetDataByQtnRefItem(qref, icode)[0];
            drquo = quo.GetDataByQtnRefParty(dr.Pr_Det_Quot_Ref, pcode)[0];

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
        }
    }
}