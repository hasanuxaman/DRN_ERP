using System;
using System.Data;
using System.Globalization;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmSupplierPayAdv : System.Web.UI.Page
    {
        GlobalClass.clsNumToText NumToText = new GlobalClass.clsNumToText();

        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";

            if (!Page.IsPostBack)
            {
                txtPayDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                tbl_advance.Visible = false;
                load_pending_list();
                load_bank();
                load_crdcode();
            }

            load_tac();
        }

        private void load_crdcode()
        {
            //Ac_Code_For_Chq_voucherTableAdapter chq = new Ac_Code_For_Chq_voucherTableAdapter();
            //SCF2DataSet.Ac_Code_For_Chq_voucherDataTable dtchq = new SCF2DataSet.Ac_Code_For_Chq_voucherDataTable();

            //ListItem lst;
            //dtchq = chq.GetData();

            //ddlcrdcode.Items.Clear();
            //ddlcrdcode.Items.Add("");

            //foreach (SCF2DataSet.Ac_Code_For_Chq_voucherRow dr in dtchq.Rows)
            //{
            //    lst = new ListItem();
            //    lst.Value = dr.Gl_Coa_Code;
            //    lst.Text = dr.Gl_Coa_Code + ":" + dr.Gl_Coa_Name;
            //    ddlcrdcode.Items.Add(lst);
            //}
        }

        private void load_bank()
        {
            tbl_Acc_Fa_Gl_CoaTableAdapter bank = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable dtbank = new dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable();
            ListItem lst;

            //dtbank = bank.GetAllBank();
            dtbank = bank.GetDataByCoaType("B");
            ddlbank.Items.Clear();
            ddlbank.Items.Add("");
            foreach (dsAccMas.tbl_Acc_Fa_Gl_CoaRow dr in dtbank.Rows)
            {
                lst = new ListItem();
                //lst.Value = dr.Bnk_info_Bnk_code;
                lst.Value = dr.Gl_Coa_Code;
                //lst.Text = dr.Bnk_info_Bnk_code + ":" + dr.Bnk_info_Bnk_Name;
                lst.Text = dr.Gl_Coa_Code + ":" + dr.Gl_Coa_Name;
                ddlbank.Items.Add(lst);
            }

            //FA_BNK_INFOTableAdapter bank = new FA_BNK_INFOTableAdapter();
            //SCF2DataSet.FA_BNK_INFODataTable dtbank = new SCF2DataSet.FA_BNK_INFODataTable();
            //ListItem lst;

            //dtbank = bank.GetAllBank();
            //ddlbank.Items.Clear();
            //ddlbank.Items.Add("");
            //foreach (SCF2DataSet.FA_BNK_INFORow dr in dtbank.Rows)
            //{
            //    lst = new ListItem();
            //    lst.Value = dr.Bnk_info_Bnk_code;
            //    lst.Text = dr.Bnk_info_Bnk_code + ":" + dr.Bnk_info_Bnk_Name;
            //    ddlbank.Items.Add(lst);
            //}
        }

        private int load_tac()
        {
            int ret;
            string selitem = ddllist.SelectedItem.Value.ToString();
            if (selitem == "") return -1;

            string tac = "";
            int cnt = 0;

            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable dtlog = new dsProcTran.tbl_TaC_LogDataTable();

            dtlog = log.GetDataByLogIdType(selitem, "PAY");

            if (dtlog.Rows.Count == 0) return -1;

            if (dtlog[0].TaC_Pay_Type == "Full")
            {
                ret = 2;
                lblpaytype.Text = "FULL ADVANCE";
            }
            else if (dtlog[0].TaC_Pay_Type == "Part")
            {
                ret = 1;
                lblpaytype.Text = "PART ADVANCE";
            }
            else
            {
                ret = 0;
                lblpaytype.Text = "NO ADVANCE";
            }


            foreach (dsProcTran.tbl_TaC_LogRow dr in dtlog.Rows)
            {
                cnt++;
                tac = tac + cnt.ToString() + ". " + dr.TaC_Content_Det + "<br />";
            }

            celtc.InnerHtml = tac;
            return ret;
        }

        private void load_pending_list()
        {
            tbl_Acc_Chq_VoucherTableAdapter hdr = new tbl_Acc_Chq_VoucherTableAdapter();
            dsAccTran.tbl_Acc_Chq_VoucherDataTable dt = new dsAccTran.tbl_Acc_Chq_VoucherDataTable();
            ListItem lst;
            int cnt;

            dt = hdr.GetDataByStatus("INI");

            cnt = dt.Rows.Count;

            ddllist.Items.Clear();
            ddllist.Items.Add("");

            foreach (dsAccTran.tbl_Acc_Chq_VoucherRow dr in dt.Rows)
            {
                lst = new ListItem();
                lst.Value = dr.PO_Ref_no.ToString();
                lst.Text = dr.PO_Ref_no.ToString() + ":" + dr.Party_det.ToString();
                ddllist.Items.Add(lst);
            }

            //if (dt.Rows.Count == 0)
            //{
            //    Response.Redirect("./Default.aspx");
            //}

            lblcount.Text = "(" + dt.Rows.Count.ToString() + ")";

            tbl_advance.Visible = false;
        }

        private void generate_detail_data(string ref_no)
        {
            tbl_PuTr_PO_DetTableAdapter podet = new tbl_PuTr_PO_DetTableAdapter();
            tbl_PuTr_PO_HdrTableAdapter pohdr = new tbl_PuTr_PO_HdrTableAdapter();
            dsProcTran.tbl_PuTr_PO_DetDataTable dtdet = new dsProcTran.tbl_PuTr_PO_DetDataTable();
            //dsProcTran.tbl_PuTr_PO_HdrRow drhdr;

            int ret;

            DataTable dt = new DataTable();

            //drhdr = pohdr.GetDataByHdrRef(ref_no)[0];
            dtdet = podet.GetDataByDetRef(ref_no);

            decimal totamnt = 0;

            dt.Rows.Clear();
            dt.Columns.Clear();
            dt.Columns.Add("Sl", typeof(int));
            dt.Columns.Add("Item Desc", typeof(string));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("UOM", typeof(string));
            dt.Columns.Add("Rate", typeof(string));
            dt.Columns.Add("Amount", typeof(string));

            foreach (dsProcTran.tbl_PuTr_PO_DetRow dr in dtdet.Rows)
            {
                dt.Rows.Add((int)dr.PO_Det_Lno, dr.PO_Det_Itm_Desc, dr.PO_Det_Lin_Qty, dr.PO_Det_Itm_Uom, dr.PO_Det_Lin_Rat.ToString("N2"), dr.PO_Det_Lin_Amt.ToString("N2"));
                totamnt = totamnt + dr.PO_Det_Lin_Amt;
            }

            lbltot.Text = totamnt.ToString("N2") + " [ " + NumToText.changeCurrencyToWords(totamnt.ToString()) + " ]";
            gdItem.DataSource = dt;
            gdItem.DataBind();

            ret = load_tac();

            if (ret == 2)
                txtvamnt.Text = totamnt.ToString("N2");
            else if (ret == 1)
                txtvamnt.Text = (totamnt / 2).ToString("N2");
            else
                txtvamnt.Text = "0.00";
        }

        protected void ddllist_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data();
        }

        private void load_data()
        {
            System.Threading.Thread.Sleep(1000);
            string selitem = ddllist.SelectedItem.Value.ToString();

            if (selitem == "")
            {
                tbl_advance.Visible = false;
            }
            else
            {
                tbl_advance.Visible = true;
                generate_detail_data(selitem);
            }
        }

        protected void btnreload_Click(object sender, EventArgs e)
        {
            load_data();
        }

        public static string GetMonthName()
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
            return info.GetAbbreviatedMonthName(DateTime.Now.Month).ToUpper();
        }

        private string get_fate_ref()
        {
            tbl_Acc_Fa_Te_WhTableAdapter wh = new tbl_Acc_Fa_Te_WhTableAdapter();
            string ref_no = "";

            double maxref = Convert.ToDouble(wh.GetMaxRef("CHQ", DateTime.Now.Year)) + 1;

            ref_no = "SCHQ" + string.Format("{0:00}", DateTime.Now.Year.ToString().Substring(2, 2)) + GetMonthName() + string.Format("{0:000000}", maxref);

            return ref_no;
        }

        private string GetMaxMatch(string trn_jrn_type)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            System.Data.DataSet ds = new System.Data.DataSet();
            DataTable dt = new DataTable();
            double ret;

            string SCFconnStr = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();

            SqlConnection conn = new SqlConnection(SCFconnStr);

            conn.Open();

            cmd.Connection = conn;
            cmd.CommandTimeout = 200;
            da.SelectCommand = cmd;

            cmd.CommandText = "Com_GetMaxMatch";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Trn_Flag", trn_jrn_type);
            cmd.Parameters.AddWithValue("@Trn_Year", DateTime.Now.Year);

            da.Fill(dt);

            try
            {
                ret = Convert.ToDouble(dt.Rows[0].ItemArray[0]);
            }
            catch
            {
                ret = 0;
            }

            ret = ret + 1;

            string wrk_match = trn_jrn_type + DateTime.Now.Year.ToString().Substring(2, 2) + string.Format("{0:00}", DateTime.Now.Month) + "-" + ret.ToString("000000");

            return wrk_match;
        }

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            string uid = Session["sessionUserId"].ToString();
            string uname = Session["sessionUserName"].ToString();

            tbl_Acc_Fa_Te_WhTableAdapter whead = new tbl_Acc_Fa_Te_WhTableAdapter();
            tbl_Acc_Fa_Te_WdTableAdapter wdet = new tbl_Acc_Fa_Te_WdTableAdapter();

            tbl_Acc_Chq_VoucherTableAdapter hdr = new tbl_Acc_Chq_VoucherTableAdapter();
            dsAccTran.tbl_Acc_Chq_VoucherRow drhdr;

            tbl_Acc_Fa_Gl_CoaTableAdapter gl = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable dtgl = new dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable();

            tbl_Acc_Fa_TeTableAdapter FaTe = new tbl_Acc_Fa_TeTableAdapter();

            if (ddlbank.SelectedValue.ToString() == "")
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Select Bank Account First.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
            //if (ddlbranch.SelectedValue.ToString() == "") return;
            //if (ddlcrdcode.SelectedValue.ToString() == "") return;

            if (txtvamnt.Text.Trim().Length <= 0 || Convert.ToDouble(txtvamnt.Text.Trim()) <= 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Enter Voucher Amount First.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }

            string ref_no = ddllist.SelectedItem.Value.ToString();

            drhdr = hdr.GetDataByPoRef(ref_no)[0];

            if (drhdr.Status != "INI") Response.Redirect(Request.Url.AbsoluteUri);            

            string bankcode = ddlbank.SelectedValue.ToString();
            //string branchcode = ddlbranch.SelectedValue.ToString();
            string branchcode = "";
            string pcode = drhdr.Party_code;
            string pdet = drhdr.Party_det;

            //dtgl = gl.GetDataByCoaCode(glcode);
            dtgl = gl.GetDataByCoaCode(bankcode);

            string glcode = dtgl[0].Gl_Coa_Code;
            string glname = dtgl[0].Gl_Coa_Name;
            string gltype = dtgl[0].Gl_Coa_Type;
            string max_match = GetMaxMatch("CHQ");

            if (txtvamnt.Text == "") return;
            if (Convert.ToDecimal(txtvamnt.Text) == 0) return;

            string period = string.Format("{0:00}", DateTime.Now.Month) + "/" + string.Format("{0:0000}", DateTime.Now.Year);

            string fa_te_ref = get_fate_ref();
            decimal wrk_amnt = Convert.ToDecimal(txtvamnt.Text);


            //string narration = lblpaytype.Text.ToLower() + " for LPO # " + ref_no + " to " + ddllist.SelectedItem.Text.Split(':')[1] + ", Bank " + ddlbank.SelectedItem.Text.Split(':')[1] + ", Branch " + ddlbranch.SelectedItem.Text.Split(':')[1];
            string narration = lblpaytype.Text.ToLower() + " for LPO # " + ref_no + " to " + ddllist.SelectedItem.Text.Split(':')[1] + ", Bank " + ddlbank.SelectedItem.Text.Split(':')[1];

            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(hdr.Connection);

            try
            {
                hdr.AttachTransaction(myTrn);
                whead.AttachTransaction(myTrn);
                wdet.AttachTransaction(myTrn);


                FaTe.AttachTransaction(myTrn);

                //header data
                whead.InsertWHdet(uid, fa_te_ref, "CHQ", Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()), period, "T", "N", "", "I", 0);                

                //Dr data
                wdet.InsertWDdet(fa_te_ref, 1, pcode, narration, "D", wrk_amnt, max_match, 0, "", 0, 0, "", "", "", "", "", "", "", "", "", "", "", pdet, "S", DateTime.Now, bankcode, ref_no, "", branchcode, "C", "", "", 0);

                //Cr data
                wdet.InsertWDdet(fa_te_ref, 2, glcode, narration, "C", wrk_amnt, max_match, 0, "", 0, 0, "", "", "", "", "", "", "", "", "", "", "", glname, gltype, DateTime.Now, "", ref_no, "", "", "", "", "", 0);

                hdr.UpdateCheqVoucher("PAY", wrk_amnt, uid, uname, DateTime.Now, "", ref_no);


                var jvType = "";
                if (ddlPayMode.SelectedItem.ToString() == "Cheque")
                    jvType = "BPV";
                else
                    jvType = "CPV";

                #region Insert Accounts Voucher Entry
                var dtMaxAccRef = FaTe.GetMaxRefNo(jvType, Convert.ToDateTime(txtPayDate.Text.Trim()).Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextAccRefNo = jvType + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                //Debit-Supplier Account
                FaTe.InsertAccData(pcode.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                    nextAccRefNo.ToString(), 1, 1, glname.ToString(), "D", Convert.ToDecimal(txtvamnt.Text.Trim()),
                    ddlPayMode.SelectedItem.ToString(), txtChqNo.Text.Trim(), "BDT", 1, Convert.ToDecimal(txtvamnt.Text.Trim()),
                    "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtPayDate.Text.Trim()), pdet.ToString(),
                    Convert.ToDateTime(txtPayDate.Text.Trim()), "ADM", "S", "",
                    DateTime.Now, jvType, "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtvamnt.Text.Trim()), "",
                    txtMnyRctNo.Text.Trim(), ref_no, "N", 1, 0, "", "", "", "J", 0, "1", jvType);

                //Credit-Given Account
                FaTe.InsertAccData(glcode.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                    nextAccRefNo.ToString(), 2, 1, pdet.ToString(), "C", Convert.ToDecimal(txtvamnt.Text.Trim()),
                    ddlPayMode.SelectedItem.ToString(), txtChqNo.Text.Trim(), "BDT", 1, Convert.ToDecimal(txtvamnt.Text.Trim()),
                    "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtPayDate.Text.Trim()), glname.ToString(),
                    Convert.ToDateTime(txtPayDate.Text.Trim()), "ADM", gltype, "",
                    DateTime.Now, jvType, "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtvamnt.Text.Trim()), "",
                    txtMnyRctNo.Text.Trim(), ref_no, "N", 1, 0, "", "", "", "J", 0, "1", jvType);

                #endregion

                myTrn.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Payment Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTrn.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            finally
            {
                GlobalClass.clsDbHelper.CloseTransaction(hdr.Connection, myTrn);
            }

            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnreject_Click(object sender, EventArgs e)
        {
            tbl_Acc_Chq_VoucherTableAdapter hdr = new tbl_Acc_Chq_VoucherTableAdapter();

            string uid = Session["sessionUserId"].ToString();
            string uname = Session["sessionUserName"].ToString();

            string ref_no = ddllist.SelectedItem.Value.ToString();
            if (ref_no == "") return;

            if (hdr.GetDataByPoRef(ref_no)[0].Status != "INI") Response.Redirect(Request.Url.AbsoluteUri);

            hdr.UpdateCheqVoucher("REJ", 0, uid, uname, DateTime.Now, "", ref_no);

            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void ddlbank_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlbank.SelectedValue.ToString() == "") { ddlbranch.Items.Clear(); return; }

            //FA_BNK_INFO1TableAdapter branch = new FA_BNK_INFO1TableAdapter();
            //SCF2DataSet.FA_BNK_INFO1DataTable dtbranch = new SCF2DataSet.FA_BNK_INFO1DataTable();
            //ListItem lst;

            //dtbranch = branch.GetBranceDet(ddlbank.SelectedValue.ToString());
            //ddlbranch.Items.Clear();
            //ddlbranch.Items.Add("");

            //foreach (SCF2DataSet.FA_BNK_INFO1Row dr in dtbranch.Rows)
            //{
            //    lst = new ListItem();
            //    lst.Value = dr.Bnk_info_Branch_Code;
            //    lst.Text = dr.Bnk_info_Branch_Code + ":" + dr.Bnk_info_Branch_name;
            //    ddlbranch.Items.Add(lst);
            //}
        }
    }
}