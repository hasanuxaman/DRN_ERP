using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FreeTextBoxControls;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmQtnTaCEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";

            if (!Page.IsPostBack)
            {
                string tac_log_id = Request.QueryString["tac_log_id"].ToString();
                string mprref = Request.QueryString["mprref"].ToString();
                string icode = Request.QueryString["icode"].ToString();
                string pcode = Request.QueryString["pcode"].ToString();                

                Session["sessionCurrentRefFocus"] = Request.QueryString["focus_ref_no"].ToString();

                tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
                dsProcTran.tbl_Qtn_DetRow dr;

                dr = quo.GetDataByMprRefItemCodeParty(mprref, icode, pcode)[0];
                txtMprRef.Text = mprref;
                txtitem.Text = dr.Qtn_Itm_Code.ToString() + ": " + dr.Qtn_Itm_Det.ToString();                          
                txtid.Text = tac_log_id;
                txtparty.Text = dr.Qtn_Par_Code.ToString() + ": " + dr.Qtn_Par_Det.ToString();     

                load_tandc_gen(tac_log_id);
                load_tandc_spe(tac_log_id);
                load_tandc_pay(tac_log_id);

                load_valid_days(tac_log_id);

                tbltac.Visible = false;
                btnupdate.Visible = false;

            }
        }

        private void load_valid_days(string tac_log_id)
        {
            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            txtvaliddays.Text = log.GetDataByRef(tac_log_id)[0].TaC_Valid_Days.ToString();
        }

        private void load_tandc_gen(string tac_log_id)
        {
            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable logdt = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_TaC_MasTableAdapter tac = new tbl_TaC_MasTableAdapter();
            dsProcMas.tbl_TaC_MasDataTable dt = new dsProcMas.tbl_TaC_MasDataTable();

            dt = tac.GetDataByType("GEN");
            FreeTextBox txt;
            CheckBox chk;

            gdgen.DataSource = dt;
            gdgen.DataBind();

            foreach (dsProcMas.tbl_TaC_MasRow dr in dt.Rows)
            {
                txt = new FreeTextBox();
                chk = new CheckBox();
                txt = (FreeTextBox)gdgen.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].FindControl("TextBox1");
                chk = (CheckBox)gdgen.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].FindControl("CheckBox1");
                txt.Text = dr.TaC_Det;
                chk.Text = Convert.ToInt16(dr.TaC_Seq_No).ToString();

                logdt = log.GetDataByIdTypeSeq(tac_log_id, "GEN", dr.TaC_Seq_No);
                if (logdt.Rows.Count > 0)
                {
                    chk.Checked = true;
                    txt.Text = logdt[0].TaC_Content_Det;
                    gdgen.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#88AAFF");
                }
            }
        }

        private void load_tandc_spe(string tac_log_id)
        {
            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable logdt = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_TaC_MasTableAdapter tac = new tbl_TaC_MasTableAdapter();
            dsProcMas.tbl_TaC_MasDataTable dt = new dsProcMas.tbl_TaC_MasDataTable();

            dt = tac.GetDataByType("SPE");
            FreeTextBox txt;
            CheckBox chk;

            gdspe.DataSource = dt;
            gdspe.DataBind();

            foreach (dsProcMas.tbl_TaC_MasRow dr in dt.Rows)
            {
                txt = new FreeTextBox();
                chk = new CheckBox();
                txt = (FreeTextBox)gdspe.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].FindControl("TextBox2");
                chk = (CheckBox)gdspe.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].FindControl("CheckBox2");
                txt.Text = dr.TaC_Det;
                chk.Text = Convert.ToInt16(dr.TaC_Seq_No).ToString();

                logdt = log.GetDataByIdTypeSeq(tac_log_id, "SPE", dr.TaC_Seq_No);
                if (logdt.Rows.Count > 0)
                {
                    chk.Checked = true;
                    txt.Text = logdt[0].TaC_Content_Det;
                    gdspe.Rows[Convert.ToInt16(dr.TaC_Seq_No) - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#88AAFF");
                }
            }
        }

        private void load_tandc_pay(string tac_log_id)
        {
            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable logdt = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_TaC_MasTableAdapter tac = new tbl_TaC_MasTableAdapter();
            dsProcMas.tbl_TaC_MasDataTable dt = new dsProcMas.tbl_TaC_MasDataTable();

            dt = tac.GetDataByType2("PAY", "Com", ddlpayterms.SelectedValue.ToString());
            FreeTextBox txt;
            CheckBox chk;
            int seq = 0;

            gdpay.DataSource = dt;
            gdpay.DataBind();

            foreach (dsProcMas.tbl_TaC_MasRow dr in dt.Rows)
            {
                txt = new FreeTextBox();
                chk = new CheckBox();
                txt = (FreeTextBox)gdpay.Rows[seq].FindControl("TextBox3");
                chk = (CheckBox)gdpay.Rows[seq].FindControl("CheckBox3");
                txt.Text = dr.TaC_Det;
                chk.Text = Convert.ToInt16(dr.TaC_Seq_No).ToString();
                seq++;

                logdt = log.GetDataByIdTypeSeq(tac_log_id, "PAY", dr.TaC_Seq_No);
                if (logdt.Rows.Count > 0)
                {
                    chk.Checked = true;
                    txt.Text = logdt[0].TaC_Content_Det;
                    gdpay.Rows[Convert.ToInt16(seq) - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#88AAFF");
                }
            }
        }

        private void load_party(string tac_log_id)
        {
            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetDataTable dt = new dsProcTran.tbl_Qtn_DetDataTable();

            dt = quo.GetDataByTaCId(tac_log_id);
            if (dt.Rows.Count > 0)
            {
                txtid.Text = tac_log_id;
                txtparty.Text = dt[0].Qtn_Par_Code + ": " + dt[0].Qtn_Par_Det;
            }
        }

        private void ddlchange()
        {
            load_tandc_pay(txtid.Text);
        }

        protected void ddlpayterms_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlchange();
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            tbl_TaC_LogTableAdapter taclog = new tbl_TaC_LogTableAdapter();
            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();

            CheckBox chk;
            FreeTextBox txt;

            int indx;
            string tac_ref_no = txtid.Text;

            //check valid days entry
            if (txtvaliddays.Text == "") return;

            if (Convert.ToInt32(txtvaliddays.Text) < 1) return;

            if (tac_ref_no == "") return;


            // check pay terms entry

            indx = 0;
            foreach (GridViewRow gr in gdgen.Rows)
            {
                chk = new CheckBox();
                chk = (CheckBox)gr.FindControl("CheckBox1");

                if (chk.Checked) indx++;

            }
            if (indx < 1) 
            {
                lblmsg.Text = "Please Select General Terms.";
                lblmsg.Visible = true; 
                return; 
            }


            indx = 0;
            foreach (GridViewRow gr in gdpay.Rows)
            {
                chk = new CheckBox();
                chk = (CheckBox)gr.FindControl("CheckBox3");

                if (chk.Checked) indx++;
            }

            if (indx < 2) 
            {
                lblmsg.Text = "Please Select at Least Two(2) Payment Terms."; 
                lblmsg.Visible = true; 
                return; 
            }
            
            taclog.DeleteTacLogById(tac_ref_no);

            indx = 0;
            foreach (GridViewRow gr in gdgen.Rows)
            {
                chk = new CheckBox();
                txt = new FreeTextBox();

                txt = (FreeTextBox)gr.FindControl("TextBox1");
                chk = (CheckBox)gr.FindControl("CheckBox1");

                if (chk.Checked)
                {
                    indx++;
                    taclog.InsertTacLog(tac_ref_no, "QTN", "GEN", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
                }
            }

            indx = 0;
            foreach (GridViewRow gr in gdspe.Rows)
            {
                chk = new CheckBox();
                txt = new FreeTextBox();

                txt = (FreeTextBox)gr.FindControl("TextBox2");
                chk = (CheckBox)gr.FindControl("CheckBox2");

                if (chk.Checked)
                {
                    indx++;
                    taclog.InsertTacLog(tac_ref_no, "QTN", "SPE", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
                }
            }

            indx = 0;
            foreach (GridViewRow gr in gdpay.Rows)
            {
                chk = new CheckBox();
                txt = new FreeTextBox();

                txt = (FreeTextBox)gr.FindControl("TextBox3");
                chk = (CheckBox)gr.FindControl("CheckBox3");

                if (chk.Checked)
                {
                    indx++;
                    taclog.InsertTacLog(tac_ref_no, "QTN", "PAY", ddlpayterms.SelectedValue.ToString(), Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
                }
            }

            //Response.Redirect("./frmCsAppr.aspx");

            string mprref = Request.QueryString["mprref"].ToString();
            string icode = Request.QueryString["icode"].ToString();
            Session["QtnMprRefNo"] = mprref.ToString();
            Session["QtnItemCode"] = icode.ToString();
            Response.Redirect("./frmCsQtnDet.aspx", false);
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();

            string pay_type = "";
            string tac_log_id = txtid.Text;

            if (log.GetDataByPayType(tac_log_id, "PAY", "Full").Rows.Count > 0)
                pay_type = "Full";
            else if (log.GetDataByPayType(tac_log_id, "PAY", "part").Rows.Count > 0)
                pay_type = "Part";
            else
                pay_type = "No";

            ddlpayterms.SelectedValue = pay_type;

            ddlchange();

            tbltac.Visible = true;
            btnupdate.Visible = true;
        }

        protected void gdgen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((CheckBox)e.Row.FindControl("CheckBox1")).Attributes.Add("onClick", "ColorRow(this)");
            }
        }

        protected void gdspe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((CheckBox)e.Row.FindControl("CheckBox2")).Attributes.Add("onClick", "ColorRow(this)");
            }
        }

        protected void gdpay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((CheckBox)e.Row.FindControl("CheckBox3")).Attributes.Add("onClick", "ColorRow(this)");
            }
        }
    }
}