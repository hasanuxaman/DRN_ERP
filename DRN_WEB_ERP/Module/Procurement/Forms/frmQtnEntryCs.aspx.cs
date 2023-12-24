using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using FreeTextBoxControls;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmQtnEntryCs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //tblmaster.BgColor = "#f0f8ff";

            ClientScript.RegisterOnSubmitStatement(this.GetType(), String.Concat(this.ClientID, "_OnSubmit"), "javascript: return OvrdSubmit();");

            var mpr_item = "";
            if (Session["sessionQuotationRef"] != null)
                mpr_item = Session["sessionQuotationRef"].ToString();
            set_data_from_cmd(mpr_item);

            if (Page.IsPostBack) return;
            txtSubmitDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
        }

        private void set_data_from_cmd(string mpr_item)
        {
            tbl_PuTr_Pr_DetTableAdapter indet = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetRow drdet;

            tbl_Qtn_DetTableAdapter qdet = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetDataTable quodt;

            string mpr_ref_no, itmcode;

            int i = 1;

            DataTable dt = new DataTable();
            dt.Columns.Clear();
            dt.Columns.Add("SL", typeof(string));
            dt.Columns.Add("PCODE", typeof(string));
            dt.Columns.Add("PDET", typeof(string));
            dt.Columns.Add("QTY", typeof(string));
            dt.Columns.Add("RATE", typeof(string));
            dt.Columns.Add("AMNT", typeof(string));
            dt.Columns.Add("SPCFICAT", typeof(string));
            dt.Columns.Add("BRAND", typeof(string));
            dt.Columns.Add("ORIGIN", typeof(string));
            dt.Columns.Add("PACKING", typeof(string));

            string[] mprref_icode = Session["sessionQuotationRef"].ToString().Split(':');
            var mprref = mprref_icode[0].ToString();
            var icode = mprref_icode[1].ToString();

            quodt = qdet.GetDataByMprRefItemCode(mprref, icode);

            if (quodt.Rows.Count == 0) return;

            mpr_ref_no = quodt[0].Qtn_Req_No.ToString();
            itmcode = quodt[0].Qtn_Itm_Code.ToString();

            lblcurlist.Text = "CURRENT QUOT FOR ITEM- " + quodt[0].Qtn_Itm_Code.ToString() + ": " + quodt[0].Qtn_Itm_Det.ToString();

            foreach (dsProcTran.tbl_Qtn_DetRow dr in quodt.Rows)
            {
                dt.Rows.Add(i.ToString(), dr.Qtn_Par_Code, dr.Qtn_Par_Det, dr.Qtn_Itm_Qty.ToString("N2") + " " + dr.Qtn_Itm_Uom, dr.Qtn_Itm_Rate.ToString("N2"), 
                    ((decimal)dr.Qtn_Itm_Qty * dr.Qtn_Itm_Rate).ToString("N2"), dr.Qtn_Itm_Spec, dr.Qtn_Itm_Brand, dr.Qtn_Itm_Origin, dr.Qtn_Itm_Packing);
                i++;
            }

            gdItem.DataSource = dt;
            gdItem.DataBind();

            Label lblref = (Label)ctlquo.FindControl("lblref");
            Label lblreqtype = (Label)ctlquo.FindControl("lblreqtype");
            Label lblitmcode = (Label)ctlquo.FindControl("lblitmcode");
            Label lblsl = (Label)ctlquo.FindControl("lblsl");
            Label lblproduct = (Label)ctlquo.FindControl("lblproduct");
            Label lblqty = (Label)ctlquo.FindControl("lblqty");
            Label lbltk = (Label)ctlquo.FindControl("lbltk");

            TextBox txtspecification = (TextBox)ctlquo.FindControl("txtspecification");
            TextBox txtbrand = (TextBox)ctlquo.FindControl("txtbrand");
            TextBox txtorigin = (TextBox)ctlquo.FindControl("txtorigin");
            TextBox txtpacking = (TextBox)ctlquo.FindControl("txtpacking");
            Label Label1 = (Label)ctlquo.FindControl("Label1");
            DropDownList DropDownList1 = (DropDownList)ctlquo.FindControl("DropDownList1");

            drdet = indet.GetDataByPrRefItem(mpr_ref_no, itmcode)[0];

            lblref.Text = mpr_ref_no;
            lblreqtype.Text = drdet.Pr_Det_Code.ToString() + ", " + drdet.Pr_Det_Pur_Type.ToString();
            lblsl.Text = "1.";
            lblitmcode.Text = drdet.Pr_Det_Icode.ToString();
            lblproduct.Text = drdet.Pr_Det_Itm_Desc;
            lblqty.Text = drdet.Pr_Det_Lin_Qty + " " + drdet.Pr_Det_Itm_Uom;
            //lbltk.Text = "Tk. /" + drdet.Pr_Det_Itm_Uom;
            lbltk.Text = "\t\t\t\t              Unit: " + drdet.Pr_Det_Itm_Uom;

            txtspecification.Text = drdet.Pr_Det_Spec + " " + drdet.Pr_Det_Rem;
            txtbrand.Text = drdet.Pr_Det_Brand;
            txtorigin.Text = drdet.Pr_Det_Origin;
            txtpacking.Text = drdet.Pr_Det_Packing;

            Label1.Visible = true;
            DropDownList1.Visible = true;

            DropDownList1.Items.Clear();
            quodt = new dsProcTran.tbl_Qtn_DetDataTable();
            quodt = qdet.GetDataByQtnItem(drdet.Pr_Det_Icode.ToString());

            quodt.DefaultView.Sort = quodt.Qtn_Ent_DateColumn.ColumnName + " DESC";

            var dcnt = 0;
            foreach (dsProcTran.tbl_Qtn_DetRow qdr in quodt.Rows)
            {
                dcnt++;
                DropDownList1.Items.Add(qdr.Qtn_Itm_Rate.ToString("N2") + " [" + qdr.Qtn_Par_Det.ToString() + "]");
                if (dcnt > 50) break;
            }
        }

        protected void ddlpayterms_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlchange();
        }

        private void ddlchange()
        {
            ddlpayterms.SelectedValue.ToString();
            load_tandc_pay(ddlpayterms.SelectedValue.ToString());
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            tbl_PuTr_Pr_DetTableAdapter srdet = new tbl_PuTr_Pr_DetTableAdapter();
            tbl_TaC_LogTableAdapter taclog = new tbl_TaC_LogTableAdapter();
            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
            tbl_PuTr_CS_SumTableAdapter taCsSum = new tbl_PuTr_CS_SumTableAdapter();
            View_Qtn_Val_SumTableAdapter taQtnValSum = new View_Qtn_Val_SumTableAdapter();

            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(srdet.Connection);

            try
            {
                srdet.AttachTransaction(myTrn);
                taclog.AttachTransaction(myTrn);
                quo.AttachTransaction(myTrn);
                taCsSum.AttachTransaction(myTrn);
                taQtnValSum.AttachTransaction(myTrn);

                string csref = Session["sessionCsRef"].ToString();
                int max_seq, indx;
                string tmp_str, pcode, prefno, pdet, uom, req_type, pur_type, qtn_ref_no, tac_ref_no;
                string[] tmp;
                double qnty, max_ref;
                CheckBox chk;
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
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter Valid Party Name.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                else
                {
                    var supRef = "";
                    var srchWords = txtparty.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        supRef = word;
                        break;
                    }

                    if (supRef.Length > 0)
                    {
                        var taSupAdr = new tbl_PuMa_Par_Adr_QtnTableAdapter();
                        var dtSupAdr = taSupAdr.GetDataByQtnAdrRef(supRef);
                        if (dtSupAdr.Rows.Count <= 0)
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Enter Valid Party Name.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
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
                #endregion

                max_ref = Convert.ToDouble(quo.GetMaxQtnRef(DateTime.Now.Year)) + 1;
                qtn_ref_no = "QTN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + string.Format("{0:000000}", max_ref);

                ctl = new Module.Procurement.Forms.UserControl.CtlQtnEntry();
                ctl = ctlquo;

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

                //ckeck dupplicate quotation for same party
                var dt = quo.GetDataByMprRefItemCodeParty(lblref.Text.Trim(), lblitmcode.Text.Trim(), tmp[0].ToString());
                if (dt.Rows.Count > 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Quotation already submitted for the party: " + tmp[2].ToString();
                    tblMsg.Rows[1].Cells[0].InnerText = "MPR Ref. No: " + lblref.Text.Trim() + ", Item Name: " + lblproduct.Text.Trim();
                    ModalPopupExtenderMsg.Show();
                    return;
                }

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
                    if (srdet.GetDataByPrRefItem(lblref.Text, lblitmcode.Text)[0].Pr_Det_Status == "QTN")
                    {
                        max_seq = Convert.ToInt32(quo.GetMaxSeqNo(csref, lblitmcode.Text, lblref.Text)) + 1;

                        if (quo.CheckExistingQtn(csref, lblitmcode.Text, pcode, lblref.Text).Count == 0)
                        {
                            //quo.InsertQtnDet(quoref, max_seq, lblref.Text, req_type, pur_type, lblitmcode.Text, lblproduct.Text, uom, qnty, pcode, pdet, Convert.ToDecimal(txtrate.Text), 0, txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, DateTime.Now, DateTime.Now, tac_ref_no, tac_ref_no, tac_ref_no, "", "", "");
                            quo.InsertQtnDet(csref, max_seq, lblref.Text, req_type, pur_type, lblitmcode.Text, lblproduct.Text, uom, qnty, pcode, pdet, Convert.ToDecimal(txtrate.Text), Convert.ToDecimal(qnty) * Convert.ToDecimal(txtrate.Text), txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, DateTime.Now, DateTime.Now, qtn_ref_no, qtn_ref_no, qtn_ref_no, "", "", "");

                            var dtMaxLno = taCsSum.GetMaxLno(csref, "QTN");
                            var iCnt = dtMaxLno == null ? 1 : dtMaxLno + 1;

                            decimal qtnVal = 0;
                            var dtQtnValSum = taQtnValSum.GetData(csref);
                            if (dtQtnValSum.Rows.Count > 0) qtnVal = dtQtnValSum[0].Qtn_Val;

                            taCsSum.InsertCsSum(csref, iCnt, pcode, Convert.ToDecimal(qtnVal), Convert.ToDecimal(0), Convert.ToDecimal(0),
                                                Convert.ToDecimal(0), Convert.ToDecimal(qtnVal), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "1", "QTN");

                            #region Insert TaC
                            //Insert TAC
                            //max_ref = Convert.ToDouble(taclog.GetMaxTacLogRef()) + 1;
                            //tac_ref_no = "QTN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + string.Format("{0:000000}", max_ref);

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
                                    //taclog.InsertTacLog(tac_ref_no, "QTN", "GEN", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
                                    taclog.InsertTacLog(qtn_ref_no, "QTN", "GEN", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
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
                                    //taclog.InsertTacLog(tac_ref_no, "QTN", "SPE", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
                                    taclog.InsertTacLog(qtn_ref_no, "QTN", "SPE", "", Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
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
                                    //taclog.InsertTacLog(tac_ref_no, "QTN", "PAY", ddlpayterms.SelectedValue.ToString(), Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
                                    taclog.InsertTacLog(qtn_ref_no, "QTN", "PAY", ddlpayterms.SelectedValue.ToString(), Convert.ToInt32(chk.Text), indx, txt.Text, Convert.ToInt32(txtvaliddays.Text), "");
                                }
                            }
                            #endregion
                        }                        
                    }
                }                

                myTrn.Commit();

                tblMsg.Rows[0].Cells[0].InnerText = "Quotation added successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTrn.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }

            Response.Redirect(Request.Url.AbsoluteUri);
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

        protected void btngoback_Click(object sender, EventArgs e)
        {
            Session["sessionQuotationRef"] = null;
            Response.Redirect("./frmCsAppr.aspx");
        }

        protected void btnAddQtnSup_Click(object sender, EventArgs e)
        {
            var url = "frmSupplierQtn.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }
    }
}