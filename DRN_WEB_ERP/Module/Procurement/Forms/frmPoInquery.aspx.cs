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
using AjaxControlToolkit;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmPoInquery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";

            GeneratePoItemsLPO();

            //GeneratePoItemsSPO();

            if (!Page.IsPostBack)
            {
                try
                {
                    txtdate.Text = DateTime.Now.ToShortDateString();
                    //txtfrom.Text = "Mamun Hasan" + "\n\n" + "[Group Head of SCM]";
                    txtfrom.Text = "\n\n" + "[Ali Akbar, ADO]";

                    var curYear = DateTime.Now.Year;
                    for (Int64 year = 2014; year <= (curYear); year++)
                    {
                        cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                    }

                    var curMonth = DateTime.Now.Month;
                    for (int month = 1; month <= 12; month++)
                    {
                        var monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                        cboMonth.Items.Add(new ListItem(monthName.ToString(), month.ToString()));
                    }
                    cboMonth.Items.Insert(0, new ListItem("-----Select-----", "0"));

                    if (Session["PoRefNoPrint"] != null)
                    {
                        var taPo = new tbl_PuTr_PO_HdrTableAdapter();
                        var dtPo = taPo.GetDataByHdrRef(Session["PoRefNoPrint"].ToString());
                        if (dtPo.Rows.Count > 0)
                        {
                            cboYear.SelectedValue = Convert.ToDateTime(dtPo[0].PO_Hdr_DATE.ToString()).Year.ToString();
                            cboMonth.SelectedValue = Convert.ToDateTime(dtPo[0].PO_Hdr_DATE.ToString()).Month.ToString();

                            load_po_list();

                            ddllist.SelectedValue = Session["PoRefNoPrint"].ToString();
                            ddllist_SelectedIndexChanged(sender, e);

                            Session["PoRefNoPrint"] = null;
                        }
                    }
                    else
                    {
                        cboYear.SelectedValue = curYear.ToString();
                        cboMonth.SelectedValue = curMonth.ToString();
                        load_po_list();
                    }
                }
                catch (Exception ex) { }                
            }
        }

        private void load_po_list()
        {
            txtparty.Text = "";
            txtdate.Text = "";
            if (tblhtml.Rows.Count > 1)
            {
                for (int i = tblhtml.Rows.Count - 1; i > 0; i--)
                {
                    tblhtml.Rows.RemoveAt(i);
                }
            }

            tblgen.Rows.Clear();
            tblspe.Rows.Clear();
            tblpay.Rows.Clear();

            tbl_PuTr_PO_HdrTableAdapter det = new tbl_PuTr_PO_HdrTableAdapter();
            dsProcTran.tbl_PuTr_PO_HdrDataTable dt = new dsProcTran.tbl_PuTr_PO_HdrDataTable();

            tbl_PuMa_Par_AdrTableAdapter adr = new tbl_PuMa_Par_AdrTableAdapter();

            ListItem lst;

            if (cboMonth.SelectedIndex == 0)
                dt = det.GetDataByPoListAllByYear("PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            else
                dt = det.GetDataByPoListAllByYearMonth("PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));

            ddllist.Items.Clear();
            foreach (dsProcTran.tbl_PuTr_PO_HdrRow dr in dt.Rows)
            {
                lst = new ListItem();
                lst.Text = dr.PO_Hdr_Ref.ToString() + ":" + dr.PO_Hdr_Com10.ToString() + ":   [" + dr.PO_Hdr_DATE.ToString("dd/MM/yyyy") + "]";
                lst.Value = dr.PO_Hdr_Ref.ToString();
                ddllist.Items.Add(lst);
            }
            ddllist.Items.Insert(0, new ListItem("-----Select-----", "0"));
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PoRefNoPrint"] = null;
            load_po_list();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PoRefNoPrint"] = null;
            load_po_list();
        }

        protected void ddllist_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtparty.Text = "";
            txtdate.Text = "";
            if (tblhtml.Rows.Count > 1)
            {
                for (int i = tblhtml.Rows.Count - 1; i > 0; i--)
                {
                    tblhtml.Rows.RemoveAt(i);
                }
            }
            tblgen.Rows.Clear();
            tblspe.Rows.Clear();
            tblpay.Rows.Clear();

            //if (ddllist.SelectedValue.ToString().Substring(0, 3) == "SPO") return;

            tbl_PuTr_PO_HdrTableAdapter hdr = new tbl_PuTr_PO_HdrTableAdapter();
            dsProcTran.tbl_PuTr_PO_HdrDataTable dthdr = new dsProcTran.tbl_PuTr_PO_HdrDataTable();

            dthdr = hdr.GetDataByHdrRef(ddllist.SelectedValue.ToString());

            if (dthdr.Rows.Count > 0)
            {
                txtparty.Text = dthdr[0].PO_Hdr_Code + ":" + dthdr[0].PO_Hdr_Ref + ":" + dthdr[0].PO_Hdr_Com10;
                txtdate.Text = dthdr[0].PO_Hdr_DATE.ToString("dd/MM/yyyy");

                if (ddllist.SelectedValue.ToString().Substring(0, 3) == "LPO")
                    GeneratePoItemsLPO();
                else if (ddllist.SelectedValue.ToString().Substring(0, 3) == "SPO")
                    GeneratePoItemsSPO();
            }
        }

        private void GeneratePoItemsLPO()
        {
            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable dtlog = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_PuTr_PO_HdrTableAdapter hdr = new tbl_PuTr_PO_HdrTableAdapter();
            tbl_PuTr_PO_DetTableAdapter det = new tbl_PuTr_PO_DetTableAdapter();
            dsProcTran.tbl_PuTr_PO_DetDataTable itm = new dsProcTran.tbl_PuTr_PO_DetDataTable();

            tbl_Qtn_DetTableAdapter qdet = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetDataTable quodt;

            int gcnt, scnt, pcnt;
            int daycnt = 0;
            string tac_ref;
            string pay_type = "";
            CheckBox chk;

            string pcode;
            string[] tmp = txtparty.Text.Split(':');
            if (tmp.Length < 3) return; ;

            string ref_no = tmp[1];

            itm = det.GetDataByDetRef(ref_no);

            if (itm.Rows.Count < 1)
            {
                return;
            }
            else
            {
                int slno = 0;
                string itemdet;

                HtmlTableRow hrow;

                //tblhtml.Rows.Clear();

                foreach (dsProcTran.tbl_PuTr_PO_DetRow dr in itm.Rows)
                {
                    slno = slno + 1;

                    pcode = hdr.GetDataByHdrRef(dr.PO_Det_Ref)[0].PO_Hdr_Pcode.ToString();
                    quodt = new dsProcTran.tbl_Qtn_DetDataTable();
                    //quodt = qdet.GetDataByQtnRefParty(dr.PO_Det_Bat_No.ToString(), pcode);
                    quodt = qdet.GetDataByQtnRefPartyItem(dr.PO_Det_Bat_No.ToString(), pcode, dr.PO_Det_Icode);
                    itemdet = dr.PO_Det_Itm_Desc.ToString();

                    hrow = new HtmlTableRow();

                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());

                    hrow.Cells[0].InnerText = slno.ToString();
                    hrow.Cells[1].InnerText = dr.PO_Det_Icode;
                    hrow.Cells[2].InnerText = itemdet.ToString();

                    if (quodt.Rows.Count > 0)
                    {
                        if (quodt[0].Qtn_Itm_Spec == "")
                            hrow.Cells[3].InnerText = ".";
                        else
                            hrow.Cells[3].InnerText = quodt[0].Qtn_Itm_Spec;

                        if (quodt[0].Qtn_Itm_Brand == "")
                            hrow.Cells[4].InnerText = ".";
                        else
                            hrow.Cells[4].InnerText = quodt[0].Qtn_Itm_Brand;

                        if (quodt[0].Qtn_Itm_Origin == ".")
                            hrow.Cells[5].InnerText = "";
                        else
                            hrow.Cells[5].InnerText = quodt[0].Qtn_Itm_Origin;

                        if (quodt[0].Qtn_Itm_Packing == "")
                            hrow.Cells[6].InnerText = "";
                        else
                            hrow.Cells[6].InnerText = quodt[0].Qtn_Itm_Packing;
                    }

                    hrow.Cells[7].InnerText = dr.PO_Det_Lin_Qty.ToString() + " " + dr.PO_Det_Itm_Uom.ToString();
                    hrow.Cells[8].InnerText = dr.PO_Det_Lin_Rat.ToString("N2");
                    hrow.Cells[9].InnerText = ((decimal)dr.PO_Det_Lin_Qty * dr.PO_Det_Lin_Rat).ToString("N2");
                    tblhtml.Rows.Add(hrow);
                }
                //

                gcnt = 1;
                scnt = 1;
                pcnt = 1;
                tac_ref = ref_no;

                tblgen.Rows.Clear();
                tblspe.Rows.Clear();
                tblpay.Rows.Clear();

                dtlog = log.GetDataByRef(tac_ref);

                foreach (dsProcTran.tbl_TaC_LogRow drlog in dtlog.Rows)
                {
                    daycnt = drlog.TaC_Valid_Days;
                    hrow = new HtmlTableRow();
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());

                    chk = new CheckBox();
                    chk.Checked = true;
                    chk.ID = gcnt.ToString() + scnt.ToString() + pcnt.ToString();

                    hrow.Cells[1].Controls.Add(chk);
                    hrow.Cells[2].InnerHtml = drlog.TaC_Content_Det.ToString();

                    switch (drlog.TaC_Type)
                    {

                        case "GEN":
                            {
                                hrow.Cells[0].InnerText = gcnt.ToString();
                                tblgen.Rows.Add(hrow);
                                gcnt++;
                                break;
                            }

                        case "SPE":
                            {
                                hrow.Cells[0].InnerText = scnt.ToString();
                                tblspe.Rows.Add(hrow);
                                scnt++;
                                break;
                            }

                        case "PAY":
                            {
                                pay_type = drlog.TaC_Pay_Type.ToUpper();
                                hrow.Cells[0].InnerText = pcnt.ToString();
                                tblpay.Rows.Add(hrow);
                                pcnt++;
                                break;
                            }
                    }
                }
                lblpaytype.Text = pay_type;
            }
        }

        private void GeneratePoItemsSPO()
        {
            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable dtlog = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_PuTr_PO_HdrTableAdapter hdr = new tbl_PuTr_PO_HdrTableAdapter();
            tbl_PuTr_PO_DetTableAdapter det = new tbl_PuTr_PO_DetTableAdapter();
            dsProcTran.tbl_PuTr_PO_DetDataTable itm = new dsProcTran.tbl_PuTr_PO_DetDataTable();

            tbl_Qtn_DetTableAdapter qdet = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetDataTable quodt;

            int gcnt, scnt, pcnt;
            int daycnt = 0;
            string tac_ref;
            string pay_type = "";
            CheckBox chk;

            string pcode;
            string[] tmp = txtparty.Text.Split(':');
            if (tmp.Length < 3) return; ;

            string ref_no = tmp[1];

            itm = det.GetDataByDetRef(ref_no);

            if (itm.Rows.Count < 1)
            {
                return;
            }
            else
            {
                int slno = 0;
                string itemdet;

                HtmlTableRow hrow;

                //tblhtml.Rows.Clear();

                foreach (dsProcTran.tbl_PuTr_PO_DetRow dr in itm.Rows)
                {
                    slno = slno + 1;

                    pcode = hdr.GetDataByHdrRef(dr.PO_Det_Ref)[0].PO_Hdr_Pcode.ToString();
                    //quodt = new dsProcTran.tbl_Qtn_DetDataTable();
                    ////quodt = qdet.GetDataByQtnRefParty(dr.PO_Det_Bat_No.ToString(), pcode);
                    //quodt = qdet.GetDataByQtnRefPartyItem(dr.PO_Det_Bat_No.ToString(), pcode, dr.PO_Det_Icode);
                    itemdet = dr.PO_Det_Itm_Desc.ToString();

                    hrow = new HtmlTableRow();

                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());

                    hrow.Cells[0].InnerText = slno.ToString();
                    hrow.Cells[1].InnerText = dr.PO_Det_Icode;
                    hrow.Cells[2].InnerText = itemdet.ToString();

                    //if (quodt[0].Qtn_Itm_Spec == "")
                    //    hrow.Cells[3].InnerText = ".";
                    //else
                    //    hrow.Cells[3].InnerText = quodt[0].Qtn_Itm_Spec;

                    //if (quodt[0].Qtn_Itm_Brand == "")
                    //    hrow.Cells[4].InnerText = ".";
                    //else
                    //    hrow.Cells[4].InnerText = quodt[0].Qtn_Itm_Brand;

                    //if (quodt[0].Qtn_Itm_Origin == ".")
                    //    hrow.Cells[5].InnerText = "";
                    //else
                    //    hrow.Cells[5].InnerText = quodt[0].Qtn_Itm_Origin;

                    //if (quodt[0].Qtn_Itm_Packing == "")
                    //    hrow.Cells[6].InnerText = "";
                    //else
                    //    hrow.Cells[6].InnerText = quodt[0].Qtn_Itm_Packing;


                    hrow.Cells[7].InnerText = dr.PO_Det_Lin_Qty.ToString() + " " + dr.PO_Det_Itm_Uom.ToString();
                    hrow.Cells[8].InnerText = dr.PO_Det_Lin_Rat.ToString("N2");
                    hrow.Cells[9].InnerText = ((decimal)dr.PO_Det_Lin_Qty * dr.PO_Det_Lin_Rat).ToString("N2");
                    tblhtml.Rows.Add(hrow);
                }
                //

                gcnt = 1;
                scnt = 1;
                pcnt = 1;
                tac_ref = ref_no;

                tblgen.Rows.Clear();
                tblspe.Rows.Clear();
                tblpay.Rows.Clear();

                dtlog = log.GetDataByRef(tac_ref);

                foreach (dsProcTran.tbl_TaC_LogRow drlog in dtlog.Rows)
                {
                    daycnt = drlog.TaC_Valid_Days;
                    hrow = new HtmlTableRow();
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());
                    hrow.Cells.Add(new HtmlTableCell());

                    chk = new CheckBox();
                    chk.Checked = true;
                    chk.ID = gcnt.ToString() + scnt.ToString() + pcnt.ToString();

                    hrow.Cells[1].Controls.Add(chk);
                    hrow.Cells[2].InnerHtml = drlog.TaC_Content_Det.ToString();

                    switch (drlog.TaC_Type)
                    {

                        case "GEN":
                            {
                                hrow.Cells[0].InnerText = gcnt.ToString();
                                tblgen.Rows.Add(hrow);
                                gcnt++;
                                break;
                            }

                        case "SPE":
                            {
                                hrow.Cells[0].InnerText = scnt.ToString();
                                tblspe.Rows.Add(hrow);
                                scnt++;
                                break;
                            }

                        case "PAY":
                            {
                                pay_type = drlog.TaC_Pay_Type.ToUpper();
                                hrow.Cells[0].InnerText = pcnt.ToString();
                                tblpay.Rows.Add(hrow);
                                pcnt++;
                                break;
                            }
                    }
                }
                lblpaytype.Text = pay_type;
            }
        }

        private void ReadyData()
        {
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
        }

        protected void btnPoPrint_Click(object sender, EventArgs e)
        {
            if (ddllist.SelectedItem.Text.Trim().Substring(0, 3) == "LPO")
            {
                ReadyData();
                Response.Redirect("./frmPoView.aspx");
            }
            else if (ddllist.SelectedItem.Text.Trim().Substring(0, 3) == "SPO")
            {
                var url = "frmPoPrintSPO.aspx?PO_REF=" + ddllist.SelectedValue.ToString();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            }
        }
    }
}