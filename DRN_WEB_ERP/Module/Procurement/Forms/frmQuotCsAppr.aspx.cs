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
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmQuotCsAppr : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            tbltooltip.Visible = true;
            tblmaster.BgColor = "#f0f8ff";

            if (!Page.IsPostBack)
            {
                get_pending();
                Session["sessionPartySelFlag"] = "";

                if (Session["sessionCurrentRefFocus"] == null)
                {

                }
                else
                {
                    if (Session["sessionCurrentRefFocus"].ToString() == "")
                    {

                    }
                    else
                    {
                        ddllist.SelectedValue = Session["sessionCurrentRefFocus"].ToString();
                        generate_detail_data(ddllist.SelectedValue.ToString());
                        Session["sessionCurrentRefFocus"] = null;
                        ddllist_SelectedIndexChanged(sender, e);
                    }
                }
            }
            else
            {
            }

            generate_detail_data(ddllist.SelectedValue.ToString());

            tbltooltip.Visible = false;
            seturgentcolor();
        }

        private void seturgentcolor()
        {
            foreach (ListItem lst in ddllist.Items)
            {
                if (lst.Text.IndexOf("URGENT") != -1) lst.Attributes.Add("style", "color:red");
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

        private void generate_comments(string ref_no)
        {            
            tbl_Tran_ComTableAdapter com = new tbl_Tran_ComTableAdapter();
            dsProcTran.tbl_Tran_ComDataTable dt = new dsProcTran.tbl_Tran_ComDataTable();
            View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();

            dt = com.GetDataByRefNo(ref_no);
            phcomments.Controls.Clear();

            foreach (dsProcTran.tbl_Tran_ComRow dr in dt.Rows)
            {
                Module.Procurement.Forms.UserControl.ctlQtnCom ctl = (Module.Procurement.Forms.UserControl.ctlQtnCom)LoadControl("./UserControl/CtlQtnCom.ascx");
                
                Label lblname = (Label)ctl.FindControl("lblname");
                Label lbldate = (Label)ctl.FindControl("lbldate");
                HtmlTableCell celcomm = (HtmlTableCell)ctl.FindControl("celcomm");
                Image imgimage = (Image)ctl.FindControl("imgimage");

                //imgimage.ImageUrl = "./handler/hnd_image.ashx?id=" + dr.app_id;


                var dtEmp = taEmp.GetDataByEmpId(dr.Com_App_Id.ToString());

                imgimage.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + (dtEmp.Rows.Count > 0 ? dtEmp[0].EmpRefNo : "") + "'";

                ctl.ID = "ctl_" + phcomments.Controls.Count.ToString();

                lblname.Text = dr.Com_App_Name.ToString() + " (" + dr.Com_App_Desig.ToString() + ")";
                lbldate.Text = dr.Com_App_Date.ToString();
                celcomm.InnerText = dr.Com_Gen_Com.ToString();

                phcomments.Controls.Add(ctl);
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

            if (icode_quoref == "") return;

            tmp = icode_quoref.Split(':');
            icode = tmp[0].ToString();
            qref = tmp[1].ToString();

            dr = srdet.GetDataByQtnRefItemStatus(qref, icode, "QTN")[0];

            //btnapp.Visible = getaper(dr.In_Det_Quo_Flow, my_app);            
            //btnreject.Visible = getrper(dr.In_Det_Quo_Flow, my_app);
            //btnaddquo.Visible = getedtper(dr.In_Det_Quo_Flow, my_app);

            btnapp.Visible = true;
            btnreject.Visible = true;
            btnaddquo.Visible = true;  

            generate_comments(dr.Pr_Det_Quot_Ref.ToString());

            lblreqref.Text = dr.Pr_Det_Bat_No.ToString();
            //lblmpr.Text = dr.Pr_Det_Ref.ToString();
            lnkbtnmpr.Text = dr.Pr_Det_Ref.ToString();
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


            lblreq.Text = dr.Pr_Det_Code.ToString() + ", " + dr.Pr_Det_Pur_Type.ToString();
            lblremarks.Text = dr.Pr_Det_Rem;
            //set recent price

            dtrecent = podet.GetPoPriceLogByItemZ(dr.Pr_Det_Icode.ToString());
            ddlrecentlist.Items.Clear();

            foreach (dsProcTran.tbl_PuTr_PO_DetRow drr in dtrecent.Rows)
            {
                lst = new ListItem();
                lst.Text = drr.PO_Det_Lin_Rat.ToString("N2") + " [" + drr.PO_Det_Ref.ToString() + "]";
                lst.Value = drr.PO_Det_Lin_Rat.ToString("N2") + " [" + drr.PO_Det_Ref.ToString() + "]";
                ddlrecentlist.Items.Add(lst);
                if (ddlrecentlist.Items.Count > 20) break;
            }

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

                if (Session["sessionPartySelFlag"].ToString() != ref_no)
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

        private void get_pending()
        {
            tbl_PuTr_Pr_DetTableAdapter indet = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetDataTable dtdet = new dsProcTran.tbl_PuTr_Pr_DetDataTable();
            ListItem lst;

            //string my_app = get_my_app();

            //dtdet = indet.GetPendingForQuoApp("QTN", my_app);

            dtdet = indet.GetDataByStatus("QTN","P");

            if (dtdet.Rows.Count == 0)
            {
                Response.Redirect("~/Default.aspx");
            }

            ddllist.Items.Clear();

            foreach (dsProcTran.tbl_PuTr_Pr_DetRow dr in dtdet.Rows)
            {
                lst = new ListItem();
                lst.Text = dr.Pr_Det_Itm_Desc.ToString() + "    [" + dr.Pr_Det_Lin_Qty.ToString() + " " + dr.Pr_Det_Itm_Uom.ToString() + "]";
                lst.Value = dr.Pr_Det_Icode.ToString() + ":" + dr.Pr_Det_Quot_Ref.ToString();

                if (dr.Pr_Det_Priority == "U")
                {
                    lst.Text = lst.Text + " [URGENT]";
                }

                ddllist.Items.Add(lst);
            }

            tblquotation.Visible = false;
            lblcount.Text = "(" + (ddllist.Items.Count).ToString() + ")";
            call_ddl_change();
        }

        private void call_ddl_change()
        {
            string selval = ddllist.SelectedValue.ToString();

            if (selval == "")
            {
                tblquotation.Visible = false; 
                return;
            }
            else
                tblquotation.Visible = true;
        }

        protected void ddllist_SelectedIndexChanged(object sender, EventArgs e)
        {
            call_ddl_change();
        }

        private bool check_approval_validity(string icode, string quoref)
        {
            tbl_PuTr_Pr_DetTableAdapter det = new tbl_PuTr_Pr_DetTableAdapter();
            bool flg = false;
            string my_app = get_my_app();

            if (det.GetDataByQtnRefItemStatus(quoref, icode, "QTN").Count > 0) flg = true;

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

        private bool savedata(string act_type, string icode, string qref, bool rej)
        {
            bool flg = true;

            tbl_PuTr_Pr_DetTableAdapter det = new tbl_PuTr_Pr_DetTableAdapter();

            tbl_Tran_ComTableAdapter comm = new tbl_Tran_ComTableAdapter();

            View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();

            tbl_PuMa_Par_AdrTableAdapter taParAdr = new tbl_PuMa_Par_AdrTableAdapter();

            string my_app = get_my_app();
            string party_code, status, status1, party_det, comments_det;
            decimal lrate;
            int sel_index;

            string uid = "", uname = "", desig = "";

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
            if (dtEmp.Rows.Count > 0)
            {
                uid = dtEmp[0].EmpId.ToString();
                uname = dtEmp[0].EmpName.ToString();
                desig = dtEmp[0].DesigName.ToString();
            }

            sel_index = GetSelectedIndex();
            if (rej)
            {
                if (sel_index == -1)
                {
                    sel_index = 1;
                }
            }
            else
            {
                if (sel_index == -1)
                {
                    flg = false;
                    goto skip_all;
                }
            }

            party_code = tbl_party.Rows[sel_index].Cells[1].InnerText;
            lrate = Convert.ToDecimal(tbl_party.Rows[sel_index].Cells[3].InnerText);
            party_det = tbl_party.Rows[sel_index].Cells[2].InnerText;

            //comments_det = "(Audit Recomended Party: " + party_det + ") " + txtcomments.Text;

            var dtParty = taParAdr.GetDataBySupAdrRef(party_code.ToString());
            var partyName = dtParty.Rows.Count > 0 ? dtParty[0].Par_Adr_Name.ToString() : "";
            
            if (act_type == "APR")
            {
                comments_det = "(Audit Recomended Party: " + partyName + ") " + txtcomments.Text.Trim();
                status = "APR";
                status1 = "";
            }
            else
            {
                if (act_type == "REJ")
                {
                    comments_det = "(Audit Rejected) " + txtcomments.Text.Trim();
                    status = "TEN";
                    status1 = "";
                }
                else
                {
                    comments_det = "(Audit Recomended Party: " + partyName + ") " + txtcomments.Text.Trim();
                    status = "QTN";
                }

            }
            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(det.Connection);

            try
            {
                det.AttachTransaction(myTrn);
                comm.AttachTransaction(myTrn);

                det.UpdatePrDetStatusFromCs(lrate, status, party_code, icode, qref);
                comm.InsertTranCom(qref, 2, DateTime.Now, uid, uname, desig, 1, my_app, status, comments_det, "", "1", "", "", "", "");

                myTrn.Commit();
            }
            catch
            {
                myTrn.Rollback();
                flg = false;
            }

            finally
            {
                GlobalClass.clsDbHelper.CloseTransaction(det.Connection, myTrn);
            }

        skip_all:

            return flg;
        }

        protected void btnapp_Click(object sender, EventArgs e)
        {
            string icode, qref;
            string[] tmp;

            tmp = lblproduct.Text.Split(':');
            icode = tmp[0].Trim();
            qref = lblqref.Text;

            if (check_approval_validity(icode, qref) == false) { return; }

            if (savedata("APR", icode, qref, false) == true)
            {
                get_pending();
                txtcomments.Text = "";
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else { lblComm.Text = "ERROR "; lblComm.Visible = true; }

            seturgentcolor();
        }

        protected void btnforward_Click(object sender, EventArgs e)
        {
            bool flg = false;
            string icode, qref;
            string[] tmp;
            tmp = lblproduct.Text.Split(':');
            icode = tmp[0].Trim();
            qref = lblqref.Text;

            if (check_approval_validity(icode, qref) == false) { return; }

            if (GetSelectedIndex() == -1) return;

            if (savedata("FWD", icode, qref, false) == true)
            {
                get_pending();
                txtcomments.Text = "";
                flg = true;
            }
            else { lblComm.Text = "ERROR "; lblComm.Visible = true; }

            if (flg)
            {
                string msub, mbody;
                string sid = "", sname = "";

                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();
               
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    sid = dtEmp[0].EmpOffEmail.ToString();
                    sname = dtEmp[0].EmpName.ToString();
                }
                
                msub = "A Comparative Statement (CS) forwarded to you [" + lnkbtnmpr.Text.Trim() + "]";
                mbody = "\n\n " + "A Comparative Statement (CS) forwarded to you [" + lnkbtnmpr.Text.Trim() + "]";
                mbody += "\n " + "To view details please login in at http://192.168.7.7/com or http://203.76.114.131/com";
                mbody += "\n " + "**THIS IS AN AUTO GENERATED EMAIL AND DOES NOT REQUIRE A REPLY.**";

                DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(sid, "", "", msub, mbody);
                //DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail("towhid.hasan@doreen.com", "alwashib@doreen.com.bd,riaz.uddin@doreen.com.bd", sid, msub, mbody);//-----Audit

                Response.Redirect(Request.Url.AbsoluteUri);
            }

            seturgentcolor();
        }

        protected void btnreject_Click(object sender, EventArgs e)
        {
            if (txtcomments.Text == "") { lblComm.Text = "Pls type your comments "; lblComm.Visible = true; return; }
            string icode, qref;
            string[] tmp;
            tmp = lblproduct.Text.Split(':');
            icode = tmp[0].Trim();
            qref = lblqref.Text;

            if (check_approval_validity(icode, qref) == false) { return; }

            if (savedata("REJ", icode, qref, true) == true)
            {
                get_pending();
                txtcomments.Text = "";
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else { lblComm.Text = "ERROR "; lblComm.Visible = true; }

            seturgentcolor();
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

            dr = srdet.GetDataByQtnRefItemStatus(qref, icode, "QTN")[0];
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

            tbltooltippnl.Rows[0].Cells[0].InnerText = drquo.Qtn_Par_Det.ToUpper();
            tbltooltippnl.Rows[2].Cells[1].InnerHtml = genstr;
            tbltooltippnl.Rows[3].Cells[1].InnerHtml = spestr;
            tbltooltippnl.Rows[4].Cells[0].InnerHtml = "Pay Terms(" + pay_type + ")";
            tbltooltippnl.Rows[4].Cells[1].InnerHtml = paystr;
            tbltooltippnl.Rows[5].Cells[1].InnerHtml = vdays.ToString();

            btnedit.PostBackUrl = "./frmQtnTaCEdit.aspx?tac_log_id=" + tac_ref + "&focus_ref_no=" + ddllist.SelectedValue.ToString() + "&party_name=" + drquo.Qtn_Par_Det.ToUpper();
            btneditval.PostBackUrl = "./frmQtnValEdit.aspx?quot_ref=" + dr.Pr_Det_Quot_Ref.ToString() + "&pcode=" + pcode + "&focusref=" + ddllist.SelectedValue.ToString();

            ModalPopupExtender5.Show();
        }

        #region TnCLinkButtonClick
        protected void lnktc1_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc2_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[2].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc3_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[3].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc4_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[4].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc5_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[5].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc6_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[6].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc7_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[7].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc8_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[8].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc9_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[9].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc10_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[10].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc11_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[11].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc12_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[12].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc13_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[13].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc14_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[14].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }

        protected void lnktc15_Click(object sender, EventArgs e)
        {
            string icode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[15].Cells[1].InnerText;
            show_tooltip(icode, pcode);
        }
        #endregion

        protected void btneditval_Click(object sender, EventArgs e)
        {

        }

        protected void btnedit_Click(object sender, EventArgs e)
        {

        }

        protected void btnaddquo_Click(object sender, EventArgs e)
        {
            string qref;
            string[] tmp;
            tmp = lblproduct.Text.Split(':');
            qref = lblqref.Text;
            Session["sessionQuotationRef"] = qref;
            Session["sessionCurrentRefFocus"] = ddllist.SelectedValue.ToString();
            Response.Redirect("./frmQtnEntryCs.aspx");
        }

        protected void btnQtnPrint_Click(object sender, EventArgs e)
        {
            reportInfo();

            Session["QtnRefNoPrint"] = ddllist.SelectedValue.ToString();
            var url = "frmQuotCsPrint.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
        {
            //if (txtFromDt.Text.Trim().Length > 0 && txtToDt.Text.Trim().Length > 0)
            //{
            //    var qrySqlStr = "";

            //    qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Prod_Det]')) DROP VIEW [dbo].[View_Prod_Det]";
            //    dbCon.ExecuteSQLStmt(qrySqlStr);

            //    qrySqlStr = " create view View_Prod_Det as SELECT tbl_InTr_Trn_Hdr.Trn_Hdr_Ref, tbl_InTr_Trn_Hdr.Trn_Hdr_Type, tbl_InTr_Trn_Hdr.Trn_Hdr_Code, tbl_InTr_Trn_Hdr.Trn_Hdr_Ref_No, " +
            //                " tbl_InTr_Trn_Hdr.Trn_Hdr_Pcode, tbl_InTr_Trn_Hdr.Trn_Hdr_Dcode, tbl_InTr_Trn_Hdr.Trn_Hdr_Acode, tbl_InTr_Trn_Hdr.Trn_Hdr_Date, " +
            //                " tbl_InTr_Trn_Hdr.Trn_Hdr_Com1, tbl_InTr_Trn_Hdr.Trn_Hdr_Com2, tbl_InTr_Trn_Hdr.Trn_Hdr_Com3, tbl_InTr_Trn_Hdr.Trn_Hdr_Com4, " +
            //                " tbl_InTr_Trn_Hdr.Trn_Hdr_Com5, tbl_InTr_Trn_Hdr.Trn_Hdr_Com6, tbl_InTr_Trn_Hdr.Trn_Hdr_Com7, tbl_InTr_Trn_Hdr.Trn_Hdr_Com8, " +
            //                " tbl_InTr_Trn_Hdr.Trn_Hdr_Com9, tbl_InTr_Trn_Hdr.Trn_Hdr_Com10, tbl_InTr_Trn_Hdr.Trn_Hdr_Value, tbl_InTr_Trn_Hdr.Trn_Hdr_HRPB_Flag, " +
            //                " tbl_InTr_Trn_Hdr.Trn_Hdr_Ent_Prd, tbl_InTr_Trn_Hdr.Trn_Hdr_Opr_Code, tbl_InTr_Trn_Hdr.Trn_Hdr_Prd_Cld, tbl_InTr_Trn_Hdr.Trn_Hdr_Exp_Typ, " +
            //                " tbl_InTr_Trn_Hdr.Trn_Hdr_Led_Int, tbl_InTr_Trn_Hdr.Trn_Hdr_DC_No, tbl_InTr_Trn_Hdr.Trn_Hdr_EI_Flg, tbl_InTr_Trn_Hdr.Trn_Hdr_Cno, " +
            //                " tbl_InTr_Trn_Hdr.T_C1 AS Expr3, tbl_InTr_Trn_Hdr.T_C2 AS Expr4, tbl_InTr_Trn_Hdr.T_Fl AS Expr5, tbl_InTr_Trn_Hdr.T_In AS Expr6, " +
            //                " tbl_InTr_Trn_Hdr.Trn_Hdr_Ent_Date, tbl_InTr_Trn_Hdr.Trn_Hdr_Ent_User, tbl_InTr_Trn_Hdr.Trn_Hdr_Updt_Date, tbl_InTr_Trn_Hdr.Trn_Hdr_Updt_User, " +
            //                " tbl_InTr_Trn_Hdr.Trn_Hdr_Status, tbl_InTr_Trn_Hdr.Trn_Hdr_Flag, tbl_InTr_Trn_Det.Trn_Det_Type, tbl_InTr_Trn_Det.Trn_Det_Code, tbl_InTr_Trn_Det.Trn_Det_Ref, " +
            //                " tbl_InTr_Trn_Det.Trn_Det_Lno, tbl_InTr_Trn_Det.Trn_Det_Sfx, tbl_InTr_Trn_Det.Trn_Det_Exp_Lno, tbl_InTr_Trn_Det.Trn_Hdr_Tran_Ref, " +
            //                " tbl_InTr_Trn_Det.Trn_Hdr_Tran_Ref_Lno, tbl_InTr_Trn_Det.Trn_Det_Icode, tbl_InTr_Trn_Det.Trn_Det_Itm_Desc, tbl_InTr_Trn_Det.Trn_Det_Itm_Uom, " +
            //                " tbl_InTr_Trn_Det.Trn_Det_Str_Code, tbl_InTr_Trn_Det.Trn_Det_Bin_Code, tbl_InTr_Trn_Det.Trn_Det_Ord_Ref, tbl_InTr_Trn_Det.Trn_Det_Ord_Ref_No, " +
            //                " tbl_InTr_Trn_Det.Trn_Det_Ord_Det_Lno, tbl_InTr_Trn_Det.Trn_Det_Bat_No, tbl_InTr_Trn_Det.Trn_Det_Exp_Dat, tbl_InTr_Trn_Det.Trn_Det_Book_Dat, " +
            //                " tbl_InTr_Trn_Det.Trn_Det_Lin_Qty, tbl_InTr_Trn_Det.Trn_Det_Unt_Wgt, tbl_InTr_Trn_Det.Trn_Det_Lin_Rat, tbl_InTr_Trn_Det.Trn_Det_Lin_Amt, " +
            //                " tbl_InTr_Trn_Det.Trn_Det_Lin_Net, tbl_InTr_Trn_Det.T_C1, tbl_InTr_Trn_Det.T_C2, tbl_InTr_Trn_Det.T_Fl, tbl_InTr_Trn_Det.T_In, tbl_InTr_Trn_Det.Trn_Det_Bal_Qty, " +
            //                " tbl_InTr_Trn_Det.Trn_Det_Status, tbl_InTr_Trn_Det.Trn_Det_Flag,Itm_Det_Ref,Itm_Det_Code,Itm_Det_Desc,Itm_Det_PUSA_Unit,Itm_Det_Stk_Unit,Itm_Det_Type,Itm_Det_T_C1,Itm_Det_Status,Itm_Det_Flag " +
            //                " FROM tbl_InTr_Trn_Hdr LEFT OUTER JOIN tbl_InTr_Trn_Det ON tbl_InTr_Trn_Hdr.Trn_Hdr_Ref = tbl_InTr_Trn_Det.Trn_Hdr_Ref " +
            //                " LEFT OUTER JOIN tbl_InMa_Item_Det ON tbl_InTr_Trn_Det.Trn_Det_Icode = tbl_InMa_Item_Det.Itm_Det_Ref " +
            //                " WHERE (tbl_InTr_Trn_Hdr.Trn_Hdr_Type ='" + optProdRpt.SelectedValue + "') AND (tbl_InTr_Trn_Hdr.Trn_Hdr_Code = 'PRD') " +
            //                " AND (CONVERT(date, tbl_InTr_Trn_Hdr.Trn_Hdr_Date, 103)>= CONVERT(date, '" + txtFromDt.Text.Trim() + "', 103)) " +
            //                " AND (CONVERT(date, tbl_InTr_Trn_Hdr.Trn_Hdr_Date, 103) <= CONVERT(date, '" + txtToDt.Text.Trim() + "', 103))";
            //    dbCon.ExecuteSQLStmt(qrySqlStr);

            //    if (ddlItem.SelectedIndex == 0)
            //    {
            //        rptSelcFormula = "{View_Prod_Det.Trn_Hdr_Date} in Date('" + txtFromDt.Text.Trim() + "') to Date ('" + txtToDt.Text.Trim()
            //        + "') and {View_Prod_Det.Trn_Hdr_HRPB_Flag}='P'";
            //    }
            //    else
            //    {
            //        rptSelcFormula = "{View_Prod_Det.Trn_Hdr_Date} in Date('" + txtFromDt.Text.Trim() + "') to Date ('" + txtToDt.Text.Trim()
            //            + "') and {View_Prod_Det.Trn_Hdr_HRPB_Flag}='P' and {View_Prod_Det.Itm_Det_Type}='" + ddlItem.SelectedValue.ToString() + "'";
            //    }

            //    rptFile = "~/Module/Production/Reports/rptProdRpt.rpt";

            //    Session["RptDateFrom"] = txtFromDt.Text.Trim();
            //    Session["RptDateTo"] = txtToDt.Text.Trim();
            //    Session["RptFilePath"] = rptFile;
            //    Session["RptFormula"] = rptSelcFormula;
            //}
        }

        protected void lnkbtnmpr_Click(object sender, EventArgs e)
        {
            rptSelcFormula = "{View_PuTr_Pr_Hdr_Det.Pr_Hdr_Ref}='" + lnkbtnmpr.Text.Trim().ToString() + "'";
            rptFile = "~/Module/Procurement/Reports/rptProcMpr.rpt";
            Session["RptDateFrom"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
            var url = "frmShowProcReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);            
        }
    }
}