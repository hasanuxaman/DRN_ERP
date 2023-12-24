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
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmPoSend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            tblmaster.BgColor = "#f0f8ff";
            if (!Page.IsPostBack)
            {
                txtdate.Text = DateTime.Now.ToShortDateString();
                //txtfrom.Text = "Mamun Hasan" + "\n" + "[Group Head of SCM]";
                txtfrom.Text = "\n\n" + "[Ali Akbar, ADO]";
                txtsub.Text = "Purchase Order";

                if (Request.QueryString.Count == 1)
                    if (Request.QueryString["PoRefNo"] != null)
                    {
                        tbl_PuTr_PO_HdrTableAdapter hdr = new tbl_PuTr_PO_HdrTableAdapter();
                        dsProcTran.tbl_PuTr_PO_HdrDataTable dthdr = new dsProcTran.tbl_PuTr_PO_HdrDataTable();

                        dthdr = hdr.GetDataByHdrRef(Request.QueryString["PoRefNo"].ToString());

                        //dthdr = hdr.GetDataByHdrRef("LPOMP0916-000006");

                        if (dthdr.Rows.Count > 0)
                        {
                            txtparty.Text = dthdr[0].PO_Hdr_Code + ":" + dthdr[0].PO_Hdr_Ref + ":" + dthdr[0].PO_Hdr_Com10;
                            GenerateItems();
                        }
                    }
            }
            else
            {
                GenerateItems();
            }
        }

        private void GenerateItems()
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
            if (tmp.Length < 3) { btnproceed.Visible = false; return; } else { btnproceed.Visible = true; }

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
                    quodt = qdet.GetDataByQtnRefParty(dr.PO_Det_Bat_No.ToString(), pcode);
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

        private void ReadyData()
        {
            tbl_PuMa_Par_AdrTableAdapter adr = new tbl_PuMa_Par_AdrTableAdapter();
            dsProcMas.tbl_PuMa_Par_AdrRow row;
            tbl_PuTr_PO_HdrTableAdapter hdr = new tbl_PuTr_PO_HdrTableAdapter();
            string[] tempdata, tmp;

            tmp = txtparty.Text.Split(':');

            if (tmp.Length < 3) return;

            string pdet = tmp[1] + ":" + tmp[2];

            string pcode = hdr.GetDataByHdrRef(tmp[1])[0].PO_Hdr_Pcode.ToString();

            tempdata = new string[12];
            tempdata[0] = txtdate.Text;
            tempdata[1] = tmp[2].ToString();
            tempdata[2] = txtsub.Text;
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

        protected void btnproceed_Click(object sender, EventArgs e)
        {
            ReadyData();
            Response.Redirect("./frmPoView.aspx");
        }

        protected void txtparty_TextChanged(object sender, EventArgs e)
        {

        }
    }
}