using System;
using System.IO;
using System.Data;
using System.Text;
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
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmQtnPendMpr : System.Web.UI.Page
    {
        tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
        dsProcTran.tbl_Qtn_DetDataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";
            //tbltooltip2.Visible = false;

            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrchItem.ContextKey = "0";

            tbltooltip.Visible = false;
            tbltooltip2.Visible = false;

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            cboItemType.DataSource = dtItemType;
            cboItemType.DataTextField = "Item_Type_Name";
            cboItemType.DataValueField = "Item_Type_Code";
            cboItemType.DataBind();
            cboItemType.Items.Insert(0, new ListItem("-----Select-----", "0"));  

            //var taItem = new tbl_InMa_Item_DetTableAdapter();
            //var dtItem = taItem.GetDataBySortAsc();
            //foreach (dsInvMas.tbl_InMa_Item_DetRow dr in dtItem.Rows)
            //{
            //    cboItem.Items.Add(new ListItem(dr.Itm_Det_Desc.ToString() + " [" + dr.Itm_Det_Ref.ToString() + "]", dr.Itm_Det_Ref.ToString()));
            //}
            //cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));

            var curYear = DateTime.Now.Year;
            for (Int64 year = 2014; year <= (curYear); year++)
            {
                cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            cboYear.Items.Insert(0, new ListItem("---Select---", "0"));
            //cboYear.SelectedValue = curYear.ToString();

            //var curMonth = DateTime.Now.Month;
            //for (int month = 1; month <= 12; month++)
            //{
            //    var monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            //    cboMonth.Items.Add(new ListItem(monthName.ToString(), month.ToString()));
            //}
            cboMonth.Items.Insert(0, new ListItem("---Select---", "0"));
            //cboMonth.SelectedValue = curMonth.ToString();

            //LoadQuotationLog();
            GetPendMprList();
            get_pending();
        }

        //private void LoadQuotationLog()
        //{
        //    tbl_Qtn_LogTableAdapter log = new tbl_Qtn_LogTableAdapter();
        //    dsProcTran.tbl_Qtn_LogDataTable dt = new dsProcTran.tbl_Qtn_LogDataTable();

        //    ListItem lst;

        //    dt = log.GetRecentData(Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddDays(-5));

        //    ddlquotlog.Items.Clear();
        //    ddlquotlog.Items.Add("");

        //    foreach (dsProcTran.tbl_Qtn_LogRow dr in dt.Rows)
        //    {
        //        lst = new ListItem();
        //        lst.Value = dr.Log_Id;
        //        lst.Text = dr.Party_Code + " : " + dr.Party_Name;
        //        ddlquotlog.Items.Add(lst);
        //    }
        //}

        private void GetPendMprList()
        {
            View_PuTr_Pr_Hdr_DetTableAdapter srdet = new View_PuTr_Pr_Hdr_DetTableAdapter();
            dsProcTran.View_PuTr_Pr_Hdr_DetDataTable dtdet = new dsProcTran.View_PuTr_Pr_Hdr_DetDataTable();

            int cnt;

            dtdet = srdet.GetDataByQtnStatusWithinNinetyDays("TEN","P");

            cnt = dtdet.Rows.Count;

            if (dtdet.Rows.Count < 1)
            {
                btnQuotation.Visible = false;
                btnQuotation0.Visible = false;
                btnCSEntry.Visible = false;
                btnCSEntry0.Visible = false;
            }
            else
            {
                btnQuotation.Visible = true;
                btnQuotation0.Visible = true;
                btnCSEntry.Visible = true;
                btnCSEntry0.Visible = true;
            }

            GetMprDetailsData(dtdet);
        }

        private void GetMprDetailsData(dsProcTran.View_PuTr_Pr_Hdr_DetDataTable dtdet)
        {
            DataTable dt = new DataTable();

            //int qcnt;

            dt.Columns.Add("MPR", typeof(string));            
            dt.Columns.Add("ICODE", typeof(string));
            dt.Columns.Add("IDET", typeof(string));
            dt.Columns.Add("QTY", typeof(double));
            dt.Columns.Add("UOM", typeof(string));
            dt.Columns.Add("SPECIFICATION", typeof(string));
            dt.Columns.Add("BRAND", typeof(string));
            dt.Columns.Add("ORIGIN", typeof(string));
            dt.Columns.Add("PACKING", typeof(string));
            dt.Columns.Add("ETR", typeof(string));
            dt.Columns.Add("REMARKS", typeof(string));
            dt.Columns.Add("STAT", typeof(string));

            foreach (dsProcTran.View_PuTr_Pr_Hdr_DetRow dr in dtdet.Rows)
            {
                //qcnt = quo.GetActiveQuot("", dr.Pr_Det_Icode.ToString(), dr.Pr_Det_Ref.ToString()).Rows.Count;
                dt.Rows.Add(dr.Pr_Det_Ref, dr.Pr_Det_Icode, dr.Pr_Det_Itm_Desc, dr.Pr_Det_Lin_Qty, dr.Pr_Det_Itm_Uom, dr.Pr_Det_Spec, dr.Pr_Det_Brand, dr.Pr_Det_Origin, dr.Pr_Det_Packing, Convert.ToDateTime(dr.Pr_Det_Exp_Dat).ToString("dd/MM/yyyy"), dr.Pr_Det_Rem, dr.Pr_Det_Scm_Com);
            }

            gdItem.DataSource = dt;
            gdItem.DataBind();

            ViewState["ViewStateDataTable"] = dt;
        }
        
        protected void gdItem_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["ViewStateSortExpression"] = e.SortExpression;
            AddSortImage(gdItem.HeaderRow);
        }

        protected void gdItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                DataTable dttemp = new DataTable();
                dttemp = (DataTable)ViewState["ViewStateDataTable"];


                if (ViewState["ViewStateSortDirection"] != null)
                    if ((SortDirection)ViewState["ViewStateSortDirection"] == SortDirection.Descending)
                    {

                        dttemp.DefaultView.Sort = e.CommandArgument + "  ASC";
                        ViewState["ViewStateSortDirection"] = SortDirection.Ascending;
                    }
                    else
                    {
                        dttemp.DefaultView.Sort = e.CommandArgument + "  DESC";
                        ViewState["ViewStateSortDirection"] = SortDirection.Descending;
                    }
                else
                {
                    dttemp.DefaultView.Sort = e.CommandArgument + "  ASC";
                    ViewState["ViewStateSortDirection"] = SortDirection.Ascending;
                }

                gdItem.DataSource = dttemp;
                gdItem.DataBind();
            }
        }

        private void AddSortImage(GridViewRow headerRow)
        {
            if (ViewState["ViewStateSortExpression"] == null) return;

            int columnIndex = 1;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["ViewStateDataTable"];
            if (dt == null) return;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].Caption == ViewState["ViewStateSortExpression"].ToString())
                {
                    columnIndex = i;
                }
            }

            Image sortImage = new Image();

            if (ViewState["ViewStateSortDirection"] == null) return;

            if ((SortDirection)ViewState["ViewStateSortDirection"] == SortDirection.Ascending)
                sortImage.ImageUrl = "~/Image/group_arrow_top.gif";
            else
                sortImage.ImageUrl = "~/Image/group_arrow_bottom.gif";

            headerRow.Cells[columnIndex + 2].Controls.Add(sortImage);
        }

        protected void gdItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblref = ((Label)e.Row.FindControl("lblMpr")).Text.Trim();
                var lblcode = ((Label)e.Row.FindControl("lblIcode")).Text.Trim();                

                dt = new dsProcTran.tbl_Qtn_DetDataTable();
                //dt = quo.GetActiveQuot("", e.Row.Cells[4].Text, e.Row.Cells[3].Text);
                dt = quo.GetActiveQuot("", lblcode, lblref);
                int qcnt = dt.Rows.Count;
                int i = 0;
                int j;
                Button btn = new Button();
                btn = (Button)e.Row.Cells[2].Controls[1];

                Module.Procurement.Forms.UserControl.CtlQtnView ctl = (Module.Procurement.Forms.UserControl.CtlQtnView)e.Row.Cells[2].FindControl("ctl1");

                if (qcnt != 0)
                {
                    Label lbltooltip;
                    GlobalClass.clsToolTip toolt;
                    Literal ltrl;
                    HtmlTable htbl;

                    lbltooltip = new Label();
                    lbltooltip.ForeColor = System.Drawing.Color.Black;
                    lbltooltip.Text = qcnt.ToString();
                    e.Row.Cells[0].Controls.Add(lbltooltip);
                    ltrl = new Literal();
                    toolt = new GlobalClass.clsToolTip();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    htbl = new HtmlTable();
                    htbl = tbltooltip;
                    htbl.Visible = true;

                    foreach (dsProcTran.tbl_Qtn_DetRow dr in dt.Rows)
                    {
                        i++;
                        htbl.Rows[i].Cells[0].InnerText = i.ToString();
                        htbl.Rows[i].Cells[1].InnerText = dr.Qtn_Par_Det;
                        htbl.Rows[i].Cells[2].InnerText = dr.Qtn_Itm_Rate.ToString("N2");
                        htbl.Rows[i].Cells[3].InnerText = dr.Qtn_Itm_Spec;
                        htbl.Rows[i].Cells[4].InnerText = dr.Qtn_Itm_Brand;
                        htbl.Rows[i].Cells[5].InnerText = dr.Qtn_Itm_Origin;
                        htbl.Rows[i].Cells[6].InnerText = dr.Qtn_Itm_Packing;
                        htbl.Rows[i].Visible = true;
                    }

                    for (j = i + 1; j < 14; j++)
                    {
                        htbl.Rows[j].Visible = false;
                    }

                    htbl.RenderControl(hw);
                    toolt.Add(lbltooltip, ltrl, sb.ToString());
                    toolt.Build();
                    e.Row.Cells[0].Controls.Add(ltrl);

                    //btn.CommandArgument = e.Row.Cells[3].Text + ":" + e.Row.Cells[4].Text + ":" + e.Row.Cells[5].Text;
                    btn.CommandArgument = lblref + ":" + lblcode + ":" + e.Row.Cells[5].Text;
                    tbltooltip.Visible = false;

                    // view qotation

                    HtmlTable tbl_party = (HtmlTable)ctl.FindControl("tbl_party");

                    tbltooltip2.Visible = true;
                    tbl_party = set_quot(lblcode, lblref, tbl_party);
                    tbltooltip2.Visible = false;
                }
                else
                {
                    ctl.Visible = false;
                    btn.Visible = false;
                }
            }
        }

        private HtmlTable set_quot(string product_code, string req_id, HtmlTable tbl_party)
        {
            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable dtlog = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetDataTable dt = new dsProcTran.tbl_Qtn_DetDataTable();

            Label lbltooltip;
            Literal ltrl;
            els.ToolTip toolt;
            string tac_ref, genstr, spestr, paystr, pay_type;

            int cur_index, i, gcnt, scnt, pcnt;
            int vdays = 0;
            HtmlInputRadioButton rdolist;
            Control ctl;

            //quotation

            dt = quo.GetActiveQuot("", product_code, req_id);

            cur_index = 1;
            tbl_party.Rows[0].Cells[1].Visible = false;

            foreach (dsProcTran.tbl_Qtn_DetRow drquo in dt.Rows)
            {
                if (cur_index == 16) break;

                rdolist = new HtmlInputRadioButton();
                ctl = new Control();
                ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
                rdolist = (HtmlInputRadioButton)ctl;
                rdolist.Visible = false;
                //totamount = (drquo.rate * qty).ToString("N2");

                tbl_party.Rows[cur_index].Cells[1].InnerText = drquo.Qtn_Par_Code;
                tbl_party.Rows[cur_index].Cells[2].InnerText = drquo.Qtn_Par_Det;

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

                tbltooltip2.Rows[0].Cells[0].InnerText = drquo.Qtn_Par_Det.ToUpper();
                tbltooltip2.Rows[2].Cells[1].InnerHtml = genstr;
                tbltooltip2.Rows[3].Cells[1].InnerHtml = spestr;
                tbltooltip2.Rows[4].Cells[0].InnerHtml = "Pay Terms(" + pay_type + ")";
                tbltooltip2.Rows[4].Cells[1].InnerHtml = paystr;
                tbltooltip2.Rows[5].Cells[1].InnerHtml = vdays.ToString();

                lbltooltip = new Label();
                lbltooltip.ForeColor = System.Drawing.Color.Black;
                lbltooltip.Text = cur_index.ToString() + ".";
                tbl_party.Rows[cur_index].Cells[0].Controls.Add(lbltooltip);
                ltrl = new Literal();
                toolt = new els.ToolTip();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                tbltooltip2.RenderControl(hw);
                toolt.Add(lbltooltip, ltrl, sb.ToString());
                // toolt.Add(tbl_party.Rows[cur_index].Cells[2], ltrl, sb.ToString());
                toolt.Build();
                tbl_party.Rows[cur_index].Cells[0].Controls.Add(ltrl);

                tbl_party.Rows[cur_index].Cells[3].InnerText = drquo.Qtn_Itm_Rate.ToString("N2");

                tbl_party.Rows[cur_index].Cells[1].Visible = false;
                tbl_party.Rows[cur_index].Visible = true;

                cur_index += 1;
            }

            for (i = cur_index; i <= 15; i++)
            {
                rdolist = new HtmlInputRadioButton();
                ctl = new Control();
                ctl = tbl_party.Rows[cur_index].Cells[0].Controls[1];
                rdolist = (HtmlInputRadioButton)ctl;
                rdolist.Checked = false;

                tbl_party.Rows[i].Visible = false;
            }

            return tbl_party;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] tmp = btn.CommandArgument.Split(':');
            if (tmp.Length < 3) return;

            string mpr_no = tmp[0].ToString();
            string icode = tmp[1].ToString();
            string idet = tmp[2].ToString();
            lblmpr.Text = mpr_no;
            lblitem.Text = icode + ":" + idet;

            txtcomments.Text = "";
            chkurgent.Checked = false;

            ModalPopupExtender1.Show();
        }

        protected void btnQuotation_Click(object sender, EventArgs e)
        {
            int tot = 0;

            CheckBox chk;
            string lblcode, lblref;
            string item_list = "";

            //ddlquotlog.Text = "";

            foreach (GridViewRow gr in gdItem.Rows)
            {
                chk = new CheckBox();
                chk = (CheckBox)gr.FindControl("CheckBox1");

                //lblref = gr.Cells[3].Text;
                //lblcode = gr.Cells[4].Text;

                lblref = ((Label)gr.FindControl("lblMpr")).Text.Trim();
                lblcode = ((Label)gr.FindControl("lblIcode")).Text.Trim();

                if (chk.Checked)
                {                    
                    item_list = item_list + lblref + ":" + lblcode + "+";
                    tot += 1;
                }
            }

            if (tot == 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Select MPR item first.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }

            Session["sessionSelvalforQuo"] = item_list;
            Response.Redirect("./frmQtnEntry.aspx");
        }

        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            string[] tmp = lblitem.Text.Split(':');
            string mpr_no = lblmpr.Text;
            string icode = tmp[0].ToString();
            string mprLno = hfPrDetLno.Value.ToString();
            string strcomments = txtcomments.Text.Trim();
            string pr_priority = "";

            if (chkurgent.Checked) { pr_priority = "U"; strcomments = "[URGENT] " + strcomments; }

            bool flg = false;
            
            tbl_PuTr_Pr_DetTableAdapter In_Det = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetDataTable dt_srdet = new dsProcTran.tbl_PuTr_Pr_DetDataTable();

            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetDataTable qtbl=new dsProcTran.tbl_Qtn_DetDataTable();

            tbl_Tran_ComTableAdapter comm = new tbl_Tran_ComTableAdapter();

            string userid = "", user_name = "", desig = "", sid = "", sname = "", new_ref;
            double max_ref;

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();
            var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
            if (dtEmp.Rows.Count > 0)
            {
                userid = dtEmp[0].EmpId.ToString();
                user_name = dtEmp[0].EmpName.ToString();
                desig = dtEmp[0].DesigName.ToString();
            }

            dt_srdet = In_Det.GetDataByPrRefItem(mpr_no, icode);

            if (dt_srdet[0].Pr_Det_Quot_Ref.Length > 0)
            {
                qtbl = quo.GetActiveQuotRate(dt_srdet[0].Pr_Det_Quot_Ref, icode, mpr_no);

                if (qtbl.Rows.Count == 0 || dt_srdet[0].Pr_Det_Status != "TEN")
                    return;

                new_ref = dt_srdet[0].Pr_Det_Quot_Ref;

                strcomments = "(C/S Re-Submited:) " + strcomments;
            }
            else
            {
                qtbl = quo.GetActiveQuotRate("", icode, mpr_no);

                if (qtbl.Rows.Count == 0 || dt_srdet[0].Pr_Det_Status != "TEN")
                    return;

                max_ref = quo.GetMaxCsRef_OLD(DateTime.Now.Year) == "" ? 1 : Convert.ToDouble(quo.GetMaxCsRef_OLD(DateTime.Now.Year)) + 1;
                new_ref = "QTN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + string.Format("{0:000000}", max_ref);

                strcomments = "(C/S Submited:) " + strcomments;
            }

            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(In_Det.Connection);

            try
            {
                In_Det.AttachTransaction(myTrn);
                quo.AttachTransaction(myTrn);

                In_Det.UpdatePrDetStatus("QTN", new_ref, pr_priority, DateTime.Now.ToString(), mpr_no, icode, "TEN");
                quo.UpdateQtnStatus(new_ref, mpr_no, icode, "");
                comm.InsertTranCom(new_ref, 1, DateTime.Now, userid, user_name, desig, 1, "QTN", "RUN", strcomments, "", "1", "", "", "", "");
                myTrn.Commit();
                flg = true;
            }
            catch
            {
                myTrn.Rollback();
            }
            finally
            {
              GlobalClass.clsDbHelper.CloseTransaction(In_Det.Connection, myTrn);
            }

            if (flg)
            {
                try
                {
                    string msub = "", mbody = "";

                    sid = dtEmp[0].EmpOffEmail.ToString();
                    sname = dtEmp[0].EmpName.ToString();

                    msub = "A Comparative Statement created and pending for you. [" + mpr_no.ToString() + "]";
                    mbody = "\n\nA Comparative Statement created and pending for you [" + mpr_no.ToString() + "]";
                    mbody += "\nTo view details please login in at http://182.160.110.139/DRNERP ";
                    mbody += "\n**THIS IS AN AUTO GENERATED EMAIL AND DOES NOT REQUIRE A REPLY.**";

                    DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail("towhid.hasan@doreen.com", "alwashib@doreen.com.bd,riaz.uddin@doreen.com.bd", sid, msub, mbody);//-----Audit
                }
                catch (Exception ex) { }
            }

            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMonth.Items.Clear();
            if (cboYear.SelectedIndex != 0)
            {
                var curMonth = DateTime.Now.Month;
                for (int month = 1; month <= 12; month++)
                {
                    var monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    cboMonth.Items.Add(new ListItem(monthName.ToString(), month.ToString()));
                }                
            }
            cboMonth.Items.Insert(0, new ListItem("---Select---", "0"));
            //cboMonth.SelectedValue = curMonth.ToString();

            get_pending();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_pending();
        }

        private void get_pending()
        {
            if (optPendMprList.SelectedValue != "1") return;

            View_PuTr_Pr_Hdr_DetTableAdapter srdet = new View_PuTr_Pr_Hdr_DetTableAdapter();
            dsProcTran.View_PuTr_Pr_Hdr_DetDataTable dtdet = new dsProcTran.View_PuTr_Pr_Hdr_DetDataTable();

            int cnt;

            var itemRef = "";
            var itemName = "";
            var srchWords = txtItemName.Text.Trim().Split(':');
            foreach (string word in srchWords)
            {
                itemRef = word;
                break;
            }

            if (itemRef.Length > 0)
            {
                int result;
                if (int.TryParse(itemRef, out result))
                {
                    var taItem = new tbl_InMa_Item_DetTableAdapter();
                    var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                    if (dtItem.Rows.Count > 0)
                    {
                        itemRef = dtItem[0].Itm_Det_Ref.ToString();
                        itemName = dtItem[0].IsItm_Det_DescNull() ? "0" : dtItem[0].Itm_Det_Desc.ToString();
                    }
                }
            }

            dtdet = srdet.GetDataByPendMprByMprRef("TEN", "P", txtMpr.Text.Trim());
            if (dtdet.Rows.Count <= 0)
            {
                if (cboItemType.SelectedIndex != 0)
                {
                    if (itemRef.Trim().ToString() != "")
                        if (cboYear.SelectedIndex != 0)
                            if (cboMonth.SelectedIndex != 0)
                                dtdet = srdet.GetDataByPendMprByItemYearMonth("TEN", "P", itemRef.Trim().ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                            else
                                dtdet = srdet.GetDataByPendMprByItemYear("TEN", "P", itemRef.Trim().ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                        else
                            dtdet = srdet.GetDataByPendMprByItemRef("TEN", "P", itemRef.Trim().ToString());
                    else
                        if (cboYear.SelectedIndex != 0)
                            if (cboMonth.SelectedIndex != 0)
                                dtdet = srdet.GetDataByPendMprByItemTypeYearMonth("TEN", "P", cboItemType.SelectedValue.ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                            else
                                dtdet = srdet.GetDataByPendMprByItemTypeYear("TEN", "P", cboItemType.SelectedValue.ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                        else
                            dtdet = srdet.GetDataByPendMprByItemType("TEN", "P", cboItemType.SelectedValue.ToString());
                }
                else
                {
                    if (itemRef.Trim().ToString() != "")
                        if (cboYear.SelectedIndex != 0)
                            if (cboMonth.SelectedIndex != 0)
                                dtdet = srdet.GetDataByPendMprByItemYearMonth("TEN", "P", itemRef.Trim().ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                            else
                                dtdet = srdet.GetDataByPendMprByItemYear("TEN", "P", itemRef.Trim().ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                        else
                            dtdet = srdet.GetDataByPendMprByItemRef("TEN", "P", itemRef.Trim().ToString());
                    else
                        if (cboYear.SelectedIndex != 0)
                            if (cboMonth.SelectedIndex != 0)
                                dtdet = srdet.GetDataByPendMprByYearMonth("TEN", "P", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                            else
                                dtdet = srdet.GetDataByPendMprByYear("TEN", "P", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                        //else
                        //    dtdet = srdet.GetDataByQtnStatus("TEN", "P");
                }
            }
            cnt = dtdet.Rows.Count;

            if (dtdet.Rows.Count < 1)
            {
                btnQuotation.Visible = false;
                btnQuotation0.Visible = false;
                btnCSEntry.Visible = false;
                btnCSEntry0.Visible = false;
            }
            else
            {
                btnQuotation.Visible = true;
                btnQuotation0.Visible = true;
                btnCSEntry.Visible = true;
                btnCSEntry0.Visible = true;
            }

            GetMprDetailsData(dtdet);
        }

        protected void cboItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtItem = new dsInvMas.tbl_InMa_Item_DetDataTable();

            try
            {
                AutoCompleteExtenderSrchItem.ContextKey = cboItemType.SelectedValue.ToString();
                txtItemName.Text = "";
                //cboItem.Items.Clear();

                //if (cboItemType.SelectedIndex == 0)
                //    dtItem = taItem.GetDataBySortAsc();
                //else
                //    dtItem = taItem.GetDataByItemType(cboItemType.SelectedValue.ToString());

                //foreach (dsInvMas.tbl_InMa_Item_DetRow dr in dtItem.Rows)
                //{
                //    cboItem.Items.Add(new ListItem(dr.Itm_Det_Desc.ToString() + " [" + dr.Itm_Det_Ref.ToString() + "]", dr.Itm_Det_Ref.ToString()));
                //}
                //cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));
                //cboItem.SelectedIndex = 0;

                get_pending();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnMprSort_Click(object sender, EventArgs e)
        {
            get_pending();
        }

        protected void btnCSEntry_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["ViewStateDataTable"];
         
            var MprList = "(";
            var ItemList = "(";
            
            var cnt=0;
            foreach (GridViewRow gr in gdItem.Rows)
            {               
                var chk = (CheckBox)gr.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    var lblMpr = ((Label)gr.FindControl("lblMpr")).Text.Trim();
                    var lblIcode = ((Label)gr.FindControl("lblIcode")).Text.Trim();

                    MprList = MprList + "'" + lblMpr.ToString() + "',";
                    ItemList = ItemList + "'" + lblIcode.ToString() + "',";
                    cnt++;
                }
            }
            MprList = MprList.Substring(0, MprList.Length - 1) + ")";
            ItemList = ItemList.Substring(0, ItemList.Length - 1) + ")";

            if (cnt <= 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Select MPR item first.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }

            Session["CsMpr"] = MprList;
            Session["CsItem"] = ItemList;
            Response.Redirect("frmCsEntry.aspx");
        }

        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            if (optPendMprList.SelectedValue != "1") return;
            get_pending();
        }

        protected void txtMpr_TextChanged(object sender, EventArgs e)
        {
            if (optPendMprList.SelectedValue != "1") return;
            get_pending();
        }

        protected void optPendMprList_SelectedIndexChanged(object sender, EventArgs e)
        {
            View_PuTr_Pr_Hdr_DetTableAdapter srdet = new View_PuTr_Pr_Hdr_DetTableAdapter();
            dsProcTran.View_PuTr_Pr_Hdr_DetDataTable dtdet = new dsProcTran.View_PuTr_Pr_Hdr_DetDataTable();

            if (optPendMprList.SelectedValue == "1")
            {
                get_pending();
                btnMprSort.Enabled = true;
            }
            if (optPendMprList.SelectedValue == "2")
            {
                btnMprSort.Enabled = false;
                dtdet = srdet.GetDataByQtnStatusWithinNinetyDays("TEN", "P");
                GetMprDetailsData(dtdet);
                if (dtdet.Rows.Count < 1)
                {
                    btnQuotation.Visible = false;
                    btnQuotation0.Visible = false;
                    btnCSEntry.Visible = false;
                    btnCSEntry0.Visible = false;
                }
                else
                {
                    btnQuotation.Visible = true;
                    btnQuotation0.Visible = true;
                    btnCSEntry.Visible = true;
                    btnCSEntry0.Visible = true;
                }
            }
            if (optPendMprList.SelectedValue == "3")
            {
                btnMprSort.Enabled = false;
                dtdet = srdet.GetDataByQtnStatusWithinNinetyDays("TEN", "P");

                DataTable dt = new DataTable();

                int qcnt;

                dt.Columns.Add("MPR", typeof(string));
                dt.Columns.Add("ICODE", typeof(string));
                dt.Columns.Add("IDET", typeof(string));
                dt.Columns.Add("QTY", typeof(double));
                dt.Columns.Add("UOM", typeof(string));
                dt.Columns.Add("SPECIFICATION", typeof(string));
                dt.Columns.Add("BRAND", typeof(string));
                dt.Columns.Add("ORIGIN", typeof(string));
                dt.Columns.Add("PACKING", typeof(string));
                dt.Columns.Add("ETR", typeof(string));
                dt.Columns.Add("REMARKS", typeof(string));
                dt.Columns.Add("STAT", typeof(string));

                foreach (dsProcTran.View_PuTr_Pr_Hdr_DetRow dr in dtdet.Rows)
                {
                    qcnt = quo.GetActiveQuot("", dr.Pr_Det_Icode.ToString(), dr.Pr_Det_Ref.ToString()).Rows.Count;
                    if (qcnt > 0)
                        dt.Rows.Add(dr.Pr_Det_Ref, dr.Pr_Det_Icode, dr.Pr_Det_Itm_Desc, dr.Pr_Det_Lin_Qty, dr.Pr_Det_Itm_Uom, dr.Pr_Det_Spec, dr.Pr_Det_Brand, dr.Pr_Det_Origin, dr.Pr_Det_Packing, Convert.ToDateTime(dr.Pr_Det_Exp_Dat).ToString("dd/MM/yyyy"), dr.Pr_Det_Rem, dr.Pr_Det_Scm_Com);
                }

                gdItem.DataSource = dt;
                gdItem.DataBind();

                ViewState["ViewStateDataTable"] = dt;

                if (dtdet.Rows.Count < 1)
                {
                    btnQuotation.Visible = false;
                    btnQuotation0.Visible = false;
                    btnCSEntry.Visible = false;
                    btnCSEntry0.Visible = false;
                }
                else
                {
                    btnQuotation.Visible = true;
                    btnQuotation0.Visible = true;
                    btnCSEntry.Visible = true;
                    btnCSEntry0.Visible = true;
                }
            }
        }

        protected void btnEditQtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] tmp = btn.CommandArgument.Split(':');
            if (tmp.Length < 3) return;

            string mpr_no = tmp[0].ToString();
            string icode = tmp[1].ToString();
            string idet = tmp[2].ToString();

            Session["QtnMprRefNo"] = mpr_no.ToString();
            Session["QtnItemCode"] = icode.ToString();
            var url = "frmCsQtnDet.aspx";
            Response.Redirect(url);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }
    }
}