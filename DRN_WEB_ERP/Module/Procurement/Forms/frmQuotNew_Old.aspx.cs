using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
//using CKEditor.NET;
using FreeTextBoxControls;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmQuotNew_Old : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnsave.Attributes.Add("btnsave_Click", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnsave, null) + ";");

            ClientScript.RegisterOnSubmitStatement(this.GetType(), String.Concat(this.ClientID, "_OnSubmit"), "javascript: return OvrdSubmit();");

            set_data_from_cmd(Session["sessionSelvalforQuo"].ToString());

            if (Page.IsPostBack) return;

            get_master_tac(ddlpayterms.SelectedValue.ToString());
        }

        private void get_master_tac(string pay_type)
        {
            load_tandc_gen();
            load_tandc_spe();
            load_tandc_pay(pay_type);
        }

        private void load_tandc_gen()
        {
            tbl_Qtn_LogTableAdapter log = new tbl_Qtn_LogTableAdapter();
            dsProcTran.tbl_Qtn_LogDataTable logdt = new dsProcTran.tbl_Qtn_LogDataTable();

            tbl_TaC_MasTableAdapter tac = new tbl_TaC_MasTableAdapter();
            dsProcMas.tbl_TaC_MasDataTable dt = new dsProcMas.tbl_TaC_MasDataTable();

            dt = tac.GetDataByType("GEN");                      
            gdgen.DataSource = dt;
            gdgen.DataBind();

            //CKEditor.NET.CKEditorControl txt;
            //CheckBox chk;
            //foreach (dsProcMas.tbl_TaC_MasRow dr in dt.Rows)
            //{
            //    txt = new CKEditorControl();
            //    chk = new CheckBox();
            //    txt = (CKEditorControl)gdgen.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].FindControl("TextBox1");
            //    chk = (CheckBox)gdgen.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].FindControl("CheckBox1"); ;
            //    txt.Text = dr.TaC_Det;
            //    chk.Text = Convert.ToInt16(dr.TaC_Seq_No).ToString();
            //}
        }

        private void load_tandc_spe()
        {
            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable logdt = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_TaC_MasTableAdapter tac = new tbl_TaC_MasTableAdapter();
            dsProcMas.tbl_TaC_MasDataTable dt = new dsProcMas.tbl_TaC_MasDataTable();
            
            dt = tac.GetDataByType("SPE");            
            gdspe.DataSource = dt;
            gdspe.DataBind();

            //CKEditor.NET.CKEditorControl txt;
            //CheckBox chk;
            //foreach (dsProcMas.tbl_TaC_MasRow dr in dt.Rows)
            //{
            //    txt = new CKEditorControl();
            //    chk = new CheckBox();
            //    //txt = (FreeTextBox)gdspe.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].FindControl("TextBox2");
            //    //txt = (AjaxControlToolkit.HTMLEditor.Editor)gdspe.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].FindControl("TextBox2");
            //    txt = (CKEditorControl)gdspe.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].FindControl("TextBox2");
            //    chk = (CheckBox)gdspe.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].FindControl("CheckBox2");
            //    txt.Text = dr.TaC_Det;
            //    chk.Text = Convert.ToInt16(dr.TaC_Seq_No).ToString();
            //}
        }

        private void load_tandc_pay(string pay_type)
        {
            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable logdt = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_TaC_MasTableAdapter tac = new tbl_TaC_MasTableAdapter();
            dsProcMas.tbl_TaC_MasDataTable dt = new dsProcMas.tbl_TaC_MasDataTable();

            dt = tac.GetDataByType2("Pay", "Com", pay_type);            
            gdpay.DataSource = dt;
            gdpay.DataBind();

            //CKEditor.NET.CKEditorControl txt;
            //CheckBox chk;
            //int seq = 0;
            //foreach (dsProcMas.tbl_TaC_MasRow dr in dt.Rows)
            //{
            //    txt = new CKEditorControl();
            //    chk = new CheckBox();
            //    //txt = (FreeTextBox)gdpay.Rows[seq].FindControl("TextBox3");
            //    txt = (CKEditorControl)gdpay.Rows[seq].FindControl("TextBox3");
            //    chk = (CheckBox)gdpay.Rows[seq].FindControl("CheckBox3");
            //    //txt.Text = dr.TaC_Det;
            //    txt.Text = dr.TaC_Det;
            //    chk.Text = Convert.ToInt16(dr.TaC_Seq_No).ToString();
            //    seq++;
            //}
        }

        private void set_data_from_cmd(string itm_list)
        {
            tbl_PuTr_Pr_DetTableAdapter quot = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetRow drdet;

            tbl_Qtn_DetTableAdapter qdet = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetDataTable dtqdet;

            string[] tmparr, tmp;
            int cnt, i, dcnt;
            int tot = 0;
            string my_app = "QTN";
            string ref_no, icode;

            tmparr = itm_list.Split('+');

            cnt = tmparr.Length;

            celquo.Controls.Clear();

            for (i = 0; i < cnt; i++)
            {
                if (tmparr[i] != "")
                {
                    tot += 1;
                    tmp = tmparr[i].Split(':');
                    ref_no = tmp[0];
                    icode = tmp[1];

                    Module.Procurement.Forms.UserControl.CtlQtnEntry ctl = (Module.Procurement.Forms.UserControl.CtlQtnEntry)LoadControl("./UserControl/CtlQtnEntry.ascx");

                    Label lblref = (Label)ctl.FindControl("lblref");
                    Label lblreqtype = (Label)ctl.FindControl("lblreqtype");
                    Label lblitmcode = (Label)ctl.FindControl("lblitmcode");
                    Label lblsl = (Label)ctl.FindControl("lblsl");
                    Label lblproduct = (Label)ctl.FindControl("lblproduct");
                    Label lblqty = (Label)ctl.FindControl("lblqty");
                    Label lbltk = (Label)ctl.FindControl("lbltk");
                    TextBox txtspecification = (TextBox)ctl.FindControl("txtspecification");
                    TextBox txtbrand = (TextBox)ctl.FindControl("txtbrand");
                    TextBox txtorigin = (TextBox)ctl.FindControl("txtorigin");
                    TextBox txtpacking = (TextBox)ctl.FindControl("txtpacking");
                    Label Label1 = (Label)ctl.FindControl("Label1");
                    DropDownList DropDownList1 = (DropDownList)ctl.FindControl("DropDownList1");

                    ctl.ID = "ctl_entry" + celquo.Controls.Count.ToString();

                    drdet = quot.GetDataByPrRefItem(ref_no, icode)[0];

                    lblref.Text = ref_no;
                    lblreqtype.Text = drdet.Pr_Det_Code.ToString() + ", " + drdet.Pr_Det_Pur_Type.ToString();
                    lblsl.Text = tot.ToString() + ".";
                    lblitmcode.Text = drdet.Pr_Det_Icode.ToString();
                    lblproduct.Text = drdet.Pr_Det_Itm_Desc;
                    lblqty.Text = drdet.Pr_Det_Lin_Qty + " " + drdet.Pr_Det_Itm_Uom;
                    //lbltk.Text = "Tk. /" + drdet.Pr_Det_Itm_Uom;
                    //lbltk.Text = "Tk.           " + " Unit: " + drdet.Pr_Det_Itm_Uom;
                    lbltk.Text = "\t\t\t\t              Unit: " + drdet.Pr_Det_Itm_Uom;

                    txtspecification.Text = drdet.Pr_Det_Spec + " " + drdet.Pr_Det_Rem;
                    txtbrand.Text = drdet.Pr_Det_Brand;
                    txtorigin.Text = drdet.Pr_Det_Origin;
                    txtpacking.Text = drdet.Pr_Det_Packing;

                    Label1.Visible = true;
                    DropDownList1.Visible = true;

                    DropDownList1.Items.Clear();
                    dtqdet = new dsProcTran.tbl_Qtn_DetDataTable();
                    dtqdet = qdet.GetDataByQtnItem(drdet.Pr_Det_Icode.ToString());

                    dtqdet.DefaultView.Sort = dtqdet.Qtn_Ent_DateColumn.ColumnName + " DESC";

                    dcnt = 0;
                    foreach (dsProcTran.tbl_Qtn_DetRow qdr in dtqdet.Rows)
                    {
                        dcnt++;
                        DropDownList1.Items.Add(qdr.Qtn_Itm_Rate.ToString("N2") + " [" + qdr.Qtn_Par_Det.ToString() + "]");
                        if (dcnt > 50) break;
                    }

                    celquo.Controls.Add(ctl);
                }
            }
        }

        private void set_data_from_ddl(string logid, bool ftime)
        {
            tbl_PuTr_Pr_DetTableAdapter quot = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetRow drdet;

            tbl_Qtn_LogTableAdapter qlog = new tbl_Qtn_LogTableAdapter();
            dsProcTran.tbl_Qtn_LogRow logdr;

            logdr = qlog.GetDataByLogId(logid)[0];
            string[] tmparr;
            string pcode;
            int cnt, i;
            int tot = 0;
            string my_app = "TEN";

            pcode = logdr.Party_Code;

            tmparr = logdr.Item_Code_Det.Split(':');

            if (ftime)
                txtparty.Text = pcode;

            cnt = tmparr.Length;

            celquo.Controls.Clear();

            for (i = 0; i < cnt; i++)
            {

                if (tmparr[i] != "")
                {
                    if (quot.GetDataByItemStatus(tmparr[i], my_app).Rows.Count > 0)
                    {
                        tot += 1;

                        Module.Procurement.Forms.UserControl.CtlQtnEntry ctl = (Module.Procurement.Forms.UserControl.CtlQtnEntry)LoadControl("./UserControl/CtlQtnEntry.ascx");

                        Label lblreqtype = (Label)ctl.FindControl("lblreqtype");
                        Label lblitmcode = (Label)ctl.FindControl("lblitmcode");
                        Label lblsl = (Label)ctl.FindControl("lblsl");
                        Label lblproduct = (Label)ctl.FindControl("lblproduct");
                        Label lblqty = (Label)ctl.FindControl("lblqty");
                        Label lbltk = (Label)ctl.FindControl("lbltk");

                        ctl.ID = "ctl_entry" + celquo.Controls.Count.ToString();

                        drdet = quot.GetDataByItemStatus(tmparr[i], my_app)[0];

                        lblreqtype.Text = drdet.Pr_Det_Code.ToString() + ", " + drdet.Pr_Det_Pur_Type.ToString();
                        lblsl.Text = tot.ToString() + ".";
                        lblitmcode.Text = tmparr[i];
                        lblproduct.Text = drdet.Pr_Det_Itm_Desc;
                        lblqty.Text = drdet.Pr_Det_Lin_Qty + " " + drdet.Pr_Det_Itm_Uom;
                        lbltk.Text = "Tk. /" + drdet.Pr_Det_Itm_Uom;

                        celquo.Controls.Add(ctl);
                    }
                }
            }
        }

        private void ddlchange()
        {
            ddlpayterms.SelectedValue.ToString();
            load_tandc_pay(ddlpayterms.SelectedValue.ToString());
        }

        protected void ddlpayterms_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlchange();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            tbl_PuTr_Pr_DetTableAdapter srdet = new tbl_PuTr_Pr_DetTableAdapter();
            tbl_TaC_LogTableAdapter taclog = new tbl_TaC_LogTableAdapter();
            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();

            int i, cnt, max_seq, len, indx;
            string tmp_str, pcode, pdet, prefno, uom, req_type, pur_type, tac_ref_no, tac_log_ref_no;
            string[] tmp;
            double qnty, max_ref;

            CheckBox chk;
            //CKEditorControl txt;
            FreeTextBox txt;
            
            Module.Procurement.Forms.UserControl.CtlQtnEntry ctl;
            TextBox txtspecification, txtbrand, txtorigin, txtpacking, txtrate;
            Label lblref, lblitmcode, lblproduct, lblqty, lblreqtype;

            #region Form Validation
            //ckeck party
            tmp_str = txtparty.Text;
            tmp = tmp_str.Split(':');
            if (tmp.Length < 2)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Enter Valid Party Name";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }

            // check gen terms entry
            indx = 0;
            foreach (GridViewRow gr in gdgen.Rows)
            {
                chk = new CheckBox();
                chk = (CheckBox)gr.FindControl("CheckBox1");

                if (chk.Checked) indx++;
            }            
            if (indx < 1) 
            {
                //lblmsg.Visible = true; return; 

                tblMsg.Rows[0].Cells[0].InnerText = "Select General Terms & Condition.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }

            // check gen terms entry
            indx = 0;
            foreach (GridViewRow gr in gdpay.Rows)
            {
                chk = new CheckBox();
                chk = (CheckBox)gr.FindControl("CheckBox3");

                if (chk.Checked) indx++;
            }
            if (indx < 2) 
            { 
                //lblmsg.Visible = true; return;

                tblMsg.Rows[0].Cells[0].InnerText = "Select Pay Terms & Condition.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }


            //check valid days entry
            if (txtvaliddays.Text == "") 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Enter Quotation Valid Day(s).";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }

            if (Convert.ToInt32(txtvaliddays.Text) < 1) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Quotation Valid Day(s) must be greater than zero (0).";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }

            cnt = celquo.Controls.Count;
            for (i = 0; i < cnt; i++)
            {
                ctl = new Module.Procurement.Forms.UserControl.CtlQtnEntry();
                ctl = (Module.Procurement.Forms.UserControl.CtlQtnEntry)celquo.Controls[i];                

                lblitmcode = new Label();
                lblitmcode = (Label)ctl.FindControl("lblitmcode");

                lblqty = new Label();
                lblqty = (Label)ctl.FindControl("lblqty");

                lblproduct = new Label();
                lblproduct = (Label)ctl.FindControl("lblproduct");

                txtrate = new TextBox();
                txtrate = (TextBox)ctl.FindControl("txtrate");

                if (txtrate.Text == "" || txtrate.Text == "0" || txtrate.Text.Length <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter Rate for the Item " + lblproduct.Text;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
            }
            #endregion

            #region Insert TaC
            //Insert TAC
            max_ref = Convert.ToDouble(taclog.GetMaxTacLogRef_Old()) + 1;
            tac_ref_no = "QTN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + string.Format("{0:000000}", max_ref);
            tac_log_ref_no = "TMP-" + "QTN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + string.Format("{0:000000}", max_ref);

            indx = 0;
            foreach (GridViewRow gr in gdgen.Rows)
            {
                chk = new CheckBox();
                chk = (CheckBox)gr.FindControl("CheckBox1");

                //txt = new CKEditorControl();
                //txt = (CKEditorControl)gr.FindControl("TextBox1");

                txt = new FreeTextBox();
                txt = (FreeTextBox)gr.FindControl("TextBox1");

                if (chk.Checked)
                {
                    indx++;
                    taclog.InsertTacLog(tac_log_ref_no, "QTN", "GEN", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
                }
            }

            indx = 0;
            foreach (GridViewRow gr in gdspe.Rows)
            {
                chk = new CheckBox();
                chk = (CheckBox)gr.FindControl("CheckBox2");

                //txt = new CKEditorControl();
                //txt = (CKEditorControl)gr.FindControl("TextBox2");

                txt = new FreeTextBox();
                txt = (FreeTextBox)gr.FindControl("TextBox2");
                

                if (chk.Checked)
                {
                    indx++;
                    taclog.InsertTacLog(tac_log_ref_no, "QTN", "SPE", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
                }
            }

            indx = 0;
            foreach (GridViewRow gr in gdpay.Rows)
            {
                chk = new CheckBox();
                chk = (CheckBox)gr.FindControl("CheckBox3");

                //txt = new CKEditorControl();
                //txt = (CKEditorControl)gr.FindControl("TextBox3");
                
                txt = new FreeTextBox();
                txt = (FreeTextBox)gr.FindControl("TextBox3");

                if (chk.Checked)
                {
                    indx++;
                    taclog.InsertTacLog(tac_log_ref_no, "QTN", "PAY", ddlpayterms.SelectedValue.ToString(), Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
                }
            }
            #endregion

            cnt = celquo.Controls.Count;

            for (i = 0; i < cnt; i++)
            {
                ctl = new Module.Procurement.Forms.UserControl.CtlQtnEntry();
                ctl = (Module.Procurement.Forms.UserControl.CtlQtnEntry)celquo.Controls[i];

                lblref = new Label();
                lblref = (Label)ctl.FindControl("lblref");

                lblreqtype = new Label();
                lblreqtype = (Label)ctl.FindControl("lblreqtype");

                lblitmcode = new Label();
                lblitmcode = (Label)ctl.FindControl("lblitmcode");

                lblqty = new Label();
                lblqty = (Label)ctl.FindControl("lblqty");

                lblproduct = new Label();
                lblproduct = (Label)ctl.FindControl("lblproduct");

                txtspecification = new TextBox();
                txtspecification = (TextBox)ctl.FindControl("txtspecification");

                txtbrand = new TextBox();
                txtbrand = (TextBox)ctl.FindControl("txtbrand");

                txtorigin = new TextBox();
                txtorigin = (TextBox)ctl.FindControl("txtorigin");

                txtpacking = new TextBox();
                txtpacking = (TextBox)ctl.FindControl("txtpacking");

                txtrate = new TextBox();
                txtrate = (TextBox)ctl.FindControl("txtrate");

                tmp = lblreqtype.Text.Split(',');
                req_type = tmp[0].Trim();
                pur_type = tmp[1].Trim();

                tmp_str = txtparty.Text;
                tmp = tmp_str.Split(':');

                pcode = tmp[0];
                prefno = tmp[1].Trim();
                pdet = tmp[2].Trim();

                tmp_str = lblqty.Text;
                tmp = tmp_str.Split(' ');

                qnty = Convert.ToDouble(tmp[0]);
                uom = tmp[1];

                if (txtrate.Text != "")
                {
                    if (srdet.GetDataByPrRefItem(lblref.Text, lblitmcode.Text)[0].Pr_Det_Status == "TEN")
                    {
                        max_seq = Convert.ToInt32(quo.GetMaxSeqNo("", lblitmcode.Text, lblref.Text)) + 1;

                        if (quo.CheckExistingQtn("", lblitmcode.Text, pcode, lblref.Text).Count == 0)
                        {
                            quo.InsertQtnDet("", max_seq, lblref.Text, req_type, pur_type, lblitmcode.Text, lblproduct.Text, uom, qnty, pcode, pdet, Convert.ToDecimal(txtrate.Text), 0, txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, DateTime.Now, DateTime.Now, tac_log_ref_no, tac_log_ref_no, tac_log_ref_no, "", "", "");
                        }
                        else
                        {
                            quo.DeleteQtnByCsItemPartyMpr("", lblitmcode.Text, pcode, lblref.Text);
                            quo.InsertQtnDet("", max_seq, lblref.Text, req_type, pur_type, lblitmcode.Text, lblproduct.Text, uom, qnty, pcode, pdet, Convert.ToDecimal(txtrate.Text), 0, txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, DateTime.Now, DateTime.Now, tac_log_ref_no, tac_log_ref_no, tac_log_ref_no, "", "", "");
                        }
                    }
                }
            }

            tblMsg.Rows[0].Cells[0].InnerText = "Quotation Saved Successfully";
            tblMsg.Rows[1].Cells[0].InnerText = "";
            ModalPopupExtenderMsg.Show();

            Response.Redirect("./frmQuotEnt.aspx");
        }

        protected void gdgen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("CheckBox1")).ClientID + "','1')");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onClick", "ColorRow(this)");
            }
        }

        protected void gdspe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("CheckBox2")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("CheckBox2")).ClientID + "','2')");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((CheckBox)e.Row.FindControl("CheckBox2")).Attributes.Add("onClick", "ColorRow(this)");
            }
        }

        protected void gdpay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("CheckBox3")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("CheckBox3")).ClientID + "','3')");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((CheckBox)e.Row.FindControl("CheckBox3")).Attributes.Add("onClick", "ColorRow(this)");
            }
        }
    }
}