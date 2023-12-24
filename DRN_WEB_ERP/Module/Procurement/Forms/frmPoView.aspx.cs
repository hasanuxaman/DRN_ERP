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

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmPoView : System.Web.UI.Page
    {
        GlobalClass.clsNumToText NumToText = new GlobalClass.clsNumToText();

        protected void Page_Load(object sender, EventArgs e)
        {            
            tblmaster.BgColor = "#f0f8ff";
            GetPage();
            if (!Page.IsPostBack)
            {
                HtmlInputButton btnnewwindow = new HtmlInputButton();
                btnnewwindow.Attributes.Add("onclick ", "window.open('./frmPoPrint.aspx')");
                btnnewwindow.ID = "btnnewwindow";
                btnnewwindow.Value = "Print Version";
                btnnewwindow.Attributes.Add("class", "btn2");
                lnkView.Controls.Add(btnnewwindow);

                HtmlInputButton btnnewwindow2 = new HtmlInputButton();
                btnnewwindow2.Attributes.Add("onclick ", "window.open('./frm_po_send_mail.aspx')");
                btnnewwindow2.ID = "btnnewwindow2";
                btnnewwindow2.Value = "Email Version";
                btnnewwindow2.Attributes.Add("class", "btn2");
                lnkMail.Controls.Add(btnnewwindow2);
            }
        }

        private void GetPage()
        {
            HtmlTable htbl = (HtmlTable)Session["sessionTempHtmlTable"];
            HtmlTable gentbl = (HtmlTable)Session["sessionGenHtmlTable"];
            HtmlTable spetbl = (HtmlTable)Session["sessionSpeHtmlTable"];
            HtmlTable paytbl = (HtmlTable)Session["sessionPayHtmlTable"];
            CheckBox chk;

            HtmlTableRow hrow;

            string[] tempdata = (string[])Session["sessionTempPrintData"];
            decimal tot = 0;

            string genstr = "";
            string spestr = "";
            string paystr = "";

            int gcnt = 1;
            int scnt = 1;
            int pcnt = 1;

            foreach (HtmlTableRow hr in htbl.Rows)
            {
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

                hrow.Cells[0].InnerText = hr.Cells[0].InnerText;
                hrow.Cells[1].InnerText = hr.Cells[2].InnerText;

                try
                {
                    //if (hr.Cells[0].InnerText == "\r\n                            Sl\r\n                        ") goto skip;
                    if (hr.Cells[0].InnerText == "\r\n                            Sl\r\n                        " || hr.Cells[0].InnerText == "\r\n                                    Sl\r\n                                ") 
                        goto skip;

                    hrow.Cells[0].InnerText = hr.Cells[0].InnerText;
                    hrow.Cells[2].InnerText = hr.Cells[3].InnerText;
                    hrow.Cells[3].InnerText = hr.Cells[4].InnerText;
                    hrow.Cells[4].InnerText = hr.Cells[5].InnerText;
                    hrow.Cells[5].InnerText = hr.Cells[6].InnerText;
                    hrow.Cells[6].InnerText = hr.Cells[7].InnerText;
                    hrow.Cells[7].InnerText = hr.Cells[8].InnerText;
                    hrow.Cells[8].InnerText = hr.Cells[9].InnerText;
                    tblhtml.Rows.Add(hrow);
                    tot = tot + Convert.ToDecimal(hr.Cells[9].InnerText);

                skip: ;
                }
                catch (Exception ex)
                {
                }
            }

            lbldate.Text = tempdata[0];
            lblto.Text = tempdata[1];
            lblsub.Text = tempdata[2];
            lblfrom.Text = tempdata[3];
            lbladd.Text = tempdata[5];
            lblporef.Text = tempdata[10];

            lbltot.Text = tot.ToString("N2") + " [" + NumToText.changeNumericToWords(tot.ToString("N2")) + "]";

            foreach (HtmlTableRow hr in gentbl.Rows)
            {
                chk = new CheckBox();
                if (hr.Cells[0].InnerText == "\r\n                        Sl") goto skip;
                try
                {
                    chk = (CheckBox)hr.Cells[1].Controls[0];
                    if (chk != null)
                        if (chk.Checked)
                        {
                            genstr = genstr + gcnt.ToString() + ". " + hr.Cells[2].InnerText + "<br />";
                            gcnt++;
                        }
                }
                catch
                {
                }
            skip: ;
            }

            foreach (HtmlTableRow hr in spetbl.Rows)
            {
                chk = new CheckBox();
                if (hr.Cells[0].InnerText == "\r\n                            Sl") goto skip2;
                try
                {
                    chk = (CheckBox)hr.Cells[1].Controls[0];
                    if (chk != null)
                        if (chk.Checked)
                        {
                            spestr = spestr + scnt.ToString() + ". " + hr.Cells[2].InnerText + "<br />";
                            scnt++;
                        }
                }
                catch
                {
                }
            skip2: ;
            }


            foreach (HtmlTableRow hr in paytbl.Rows)
            {
                chk = new CheckBox();
                if (hr.Cells[0].InnerText == "\r\n                            Sl") goto skip3;
                try
                {
                    chk = (CheckBox)hr.Cells[1].Controls[0];
                    if (chk != null)
                        if (chk.Checked)
                        {
                            paystr = paystr + pcnt.ToString() + ". " + hr.Cells[2].InnerText + "<br />";
                            pcnt++;
                        }
                }
                catch
                {
                }
            skip3: ;
            }

            if (genstr != "") lblgen.Text = "GENERAL TERMS";
            if (spestr != "") lblspe.Text = "SPECIAL TERMS";
            if (paystr != "") lblpay.Text = "PAYMENT TERMS (" + tempdata[11] + " ADVANCE)";

            genterms.InnerHtml = genstr;
            spterms.InnerHtml = spestr;
            payterms.InnerHtml = paystr;

            //daycount.InnerHtml = tempdata[9];
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            var url = "frmPoPrint.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            //RegisterStartupScript("click", "<script>window.open('./frmPoPrint.aspx');</script>");
        }

        protected void lnkMail_Click(object sender, EventArgs e)
        {
            //RegisterStartupScript("click", "<script>window.open('./frm_po_send_mail.aspx');</script>");
        }   
    }
}