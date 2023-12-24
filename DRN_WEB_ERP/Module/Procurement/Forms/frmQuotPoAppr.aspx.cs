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
    public partial class frmQuotPoAppr : System.Web.UI.Page
    {
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

            tbl_PuMa_Par_Adr_QtnTableAdapter adr = new tbl_PuMa_Par_Adr_QtnTableAdapter();
            dsProcMas.tbl_PuMa_Par_Adr_QtnDataTable dtadr;

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

            HtmlInputRadioButton rdolist;
            Control ctl;

            if (mprref_itemcode == "") return;

            tmp = mprref_itemcode.Split(':');
            mprref = tmp[0].ToString();
            icode = tmp[1].ToString();
            
            dr = srdet.GetDataByPrRefItemStatus(mprref, icode, "APR")[0];

            btnapp.Visible = true;

            generate_comments(dr.Pr_Det_Quot_Ref.ToString());

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

            dt = quo.GetDataByMprRefItemCode(mprref, icode);
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

                dtadr = new dsProcMas.tbl_PuMa_Par_Adr_QtnDataTable();
                dtadr = adr.GetDataByQtnAdrRef(drquo.Qtn_Par_Code);

                tbl_party.Rows[cur_index].Cells[1].InnerText = drquo.Qtn_Par_Code;
                if (dtadr.Rows.Count == 0)
                {
                    tbl_party.Rows[cur_index].Cells[2].InnerText = drquo.Qtn_Par_Det;
                }
                else
                {
                    //tbl_party.Rows[cur_index].Cells[2].InnerText = drquo.Qtn_Par_Det + ", " + dtadr[0].Par_Adr_Addr  + " Ph: " + dtadr[0].Par_Adr_Tel_No + " " + dtadr[0].Par_Adr_Lst_No + " Fax: " + dtadr[0].Par_Adr_Fax_No + " Email: " + dtadr[0].Par_Adr_Email_Id;

                    var phNo = (dtadr[0].IsPar_Adr_Qtn_Tel_NoNull() || dtadr[0].Par_Adr_Qtn_Tel_No == "") ? "" : ", Ph: " + dtadr[0].Par_Adr_Qtn_Tel_No;
                    var faxNo = (dtadr[0].IsPar_Adr_Qtn_Fax_NoNull() || dtadr[0].Par_Adr_Qtn_Fax_No == "") ? "" : ", Fax: " + dtadr[0].Par_Adr_Qtn_Fax_No;
                    var emaiId = (dtadr[0].IsPar_Adr_Qtn_Email_IdNull() || dtadr[0].Par_Adr_Qtn_Email_Id == "") ? "" : ", Email: " + dtadr[0].Par_Adr_Qtn_Email_Id;

                    tbl_party.Rows[cur_index].Cells[2].InnerText = drquo.Qtn_Par_Det + ", " + dtadr[0].Par_Adr_Qtn_Addr + phNo + faxNo + emaiId;
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

            dtdet = indet.GetDataByStatus("APR","P");

            if (dtdet.Rows.Count == 0)
            {
                Response.Redirect("~/Default.aspx");
            }

            ddllist.Items.Clear();

            foreach (dsProcTran.tbl_PuTr_Pr_DetRow dr in dtdet.Rows)
            {
                lst = new ListItem();
                lst.Text = dr.Pr_Det_Itm_Desc.ToString() + "    [" + dr.Pr_Det_Lin_Qty.ToString() + " " + dr.Pr_Det_Itm_Uom.ToString() + "]";
                lst.Value = dr.Pr_Det_Ref.ToString() + ":" + dr.Pr_Det_Icode.ToString();

                if (dr.Pr_Det_Priority == "U")
                {
                    lst.Text = lst.Text + " [URGENT]";
                }
                lst.Text = lst.Text + " *** (" + dr.Pr_Det_Quot_Ref.ToString() + "--" + dr.Pr_Det_Ref.ToString() + ")";

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

        private bool savedata(string act_type, string icode, string qref, bool rej)
        {
            bool flg = false;

            tbl_PuTr_Pr_DetTableAdapter det = new tbl_PuTr_Pr_DetTableAdapter();

            tbl_Qtn_DetTableAdapter qtndet = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetDataTable dtqtndet = new dsProcTran.tbl_Qtn_DetDataTable();

            tbl_Tran_ComTableAdapter comm = new tbl_Tran_ComTableAdapter();

            View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();

            var taSup = new tbl_PuMa_Par_AdrTableAdapter();
            var dtSup = new dsProcMas.tbl_PuMa_Par_AdrDataTable();

            var taItm = new tbl_InMa_Item_DetTableAdapter();
            var dtItm = new dsInvMas.tbl_InMa_Item_DetDataTable();

            string my_app = get_my_app();
            string party_code, status, status1, party_det, comments_det;
            decimal lrate;
            int sel_index;

            string uid = "", uname = "", desig = "", sid = "", sname = "";

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
            if (dtEmp.Rows.Count > 0)
            {
                uid = dtEmp[0].EmpId.ToString();
                uname = dtEmp[0].EmpName.ToString();
                desig = dtEmp[0].DesigName.ToString();

                sid = dtEmp[0].EmpOffEmail.ToString();
                sname = dtEmp[0].EmpName.ToString();
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

            //comments_det = "(Procurement Selected Party: " + party_det + ") " + txtcomments.Text;

            var dtParty = taSup.GetDataBySupAdrRef(party_code.ToString());
            var partyName = dtParty.Rows.Count > 0 ? dtParty[0].Par_Adr_Name.ToString() : "";
            comments_det = "(Procurement Selected Party: " + partyName + ") " + txtcomments.Text.Trim();

            if (act_type == "APP")
            {
                status = "APP";
                status1 = "";
            }
            else
            {
                if (act_type == "REJ")
                {
                    status = "QTN";
                    status1 = "";
                }
                else
                {
                    status = "QTN";
                }

            }

            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(det.Connection);

            try
            {
                det.AttachTransaction(myTrn);
                comm.AttachTransaction(myTrn);

                var dtSrDet = det.GetDataByQtnRefItemStatus(qref, icode, "APR");
                if (dtSrDet.Rows.Count > 0)
                {
                    if (dtSrDet[0].Pr_Det_App_Party == party_code)
                    {
                        det.UpdatePrDetStatusFromCs(lrate, status, party_code, icode, qref);
                        var dtCom = comm.GetDataByRefSeqNo(qref, 3);
                        if (dtCom.Rows.Count <= 0)
                            comm.InsertTranCom(qref, 3, DateTime.Now, uid, uname, desig, 1, my_app, status, comments_det, "", "1", "", "", "", "");
                        myTrn.Commit();
                        flg = true;
                    }
                    else
                    {
                        dtqtndet = qtndet.GetDataByQtnRefParty(qref, party_code);
                        if (dtqtndet.Rows.Count > 0)
                        {
                            det.UpdatePrDetStatusFromCs(lrate, status, party_code, icode, qref);
                            var dtCom = comm.GetDataByRefSeqNo(qref, 3);
                            if (dtCom.Rows.Count <= 0)
                                comm.InsertTranCom(qref, 3, DateTime.Now, uid, uname, desig, 1, my_app, status, comments_det, "", "1", "", "", "", "");
                            myTrn.Commit();
                            flg = true;

                            dtSup = taSup.GetDataBySupAdrRef(dtSrDet[0].Pr_Det_App_Party);
                            var recmSup = dtSup[0].Par_Adr_Name;

                            dtSup = taSup.GetDataBySupAdrRef(party_code);
                            var appSup = dtSup[0].Par_Adr_Name;

                            dtItm = taItm.GetDataByItemRef(Convert.ToInt32(icode));
                            var itemName = dtItm[0].Itm_Det_Desc;

                            string msub, mbody;

                            dtqtndet = qtndet.GetDataByQtnRefPartyItem(qref, recmSup, icode);
                            var recmRate = dtqtndet.Rows.Count > 0 ? dtqtndet[0].Qtn_Itm_Rate.ToString() : "";

                            dtqtndet = qtndet.GetDataByQtnRefPartyItem(qref, party_code, icode);
                            var appRate = dtqtndet.Rows.Count > 0 ? dtqtndet[0].Qtn_Itm_Rate.ToString() : "";

                            msub = "-----------------Purchase Order Approval Notification-----------------";
                            mbody = "\n\n " + "Item Name: " + itemName + ", Qty: " + dtSrDet[0].Pr_Det_Lin_Qty;
                            mbody += "\n\n " + "MPR Ref No: " + dtSrDet[0].Pr_Det_Ref;
                            mbody += "\n\n " + "C/S Ref No: " + qref;
                            mbody += "\n\n " + "Recomended Supplier was: " + recmSup + ", Price: " + recmRate;
                            mbody += "\n\n " + "Selected Supplier is: " + appSup + ", Price: " + recmRate;
                            mbody += "\n\n " + "To view details please login in at http://182.160.110.139/DRNERP ";
                            mbody += "\n\n " + "**THIS IS AN AUTO GENERATED EMAIL AND DOES NOT REQUIRE A REPLY.**";

                            //DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(sid, "", "", msub, mbody);
                            DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail("towhid.hasan@doreen.com", "saleh@doreen.com.bd,alwashib@doreen.com.bd,riaz.uddin@doreen.com.bd", sid, msub, mbody);//-----Audit
                        }
                        else
                        {
                            lblComm.Text = "Quotation Data not found.";
                            lblComm.Visible = true;
                            myTrn.Rollback();
                            flg = false;
                        }
                    }
                }
                else
                {
                    lblComm.Text = "C/S Data not found.";
                    lblComm.Visible = true;
                    myTrn.Rollback();
                    flg = false;
                }
            }
            catch
            {
                lblComm.Text = "Data Processing Error.";
                lblComm.Visible = true;
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

            if (savedata("APP", icode, qref, false) == true)
            {
                get_pending();
                txtcomments.Text = "";
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else { lblComm.Text = "ERROR "; lblComm.Visible = true; }

            seturgentcolor();                                                
        }

        #region TnCLinkButtonClick
        protected void lnktc1_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc2_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc3_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc4_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc5_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc6_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc7_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc8_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc9_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc10_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc11_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc12_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc13_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc14_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }

        protected void lnktc15_Click(object sender, EventArgs e)
        {
            string mpricode = ddllist.SelectedValue.ToString();
            string pcode = tbl_party.Rows[1].Cells[1].InnerText;
            show_tooltip(mpricode, pcode);
        }
        #endregion

        private void show_tooltip(string mprref_icode, string pcode)
        {
            string my_app = get_my_app();

            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable dtlog = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_PuTr_Pr_DetTableAdapter srdet = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetRow dr;

            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetRow drquo;

            string[] tmp;
            string icode, mprref, pay_type, genstr, spestr, paystr, tac_ref;
            int gcnt, scnt, pcnt;
            int vdays = 0;

            tmp = mprref_icode.Split(':');
            mprref = tmp[0].ToString();
            icode = tmp[1].ToString();

            dr = srdet.GetDataByPrRefItemStatus(mprref, icode, "APR")[0];
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
           
            ModalPopupExtender5.Show();
        }
    }
}