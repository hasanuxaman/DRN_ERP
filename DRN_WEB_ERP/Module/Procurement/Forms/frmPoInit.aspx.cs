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
//using DRN_WEB_ERP.DRNDataSetTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
//using DRN_WEB_ERP.Module.Inventory.DataSet;
//using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
//using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmPoInit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";

            if (!Page.IsPostBack)
            {
                load_party();
            }             
        }

        private void SortDDL(ref DropDownList objDDL)
        {
            ArrayList textList = new ArrayList();
            ArrayList valueList = new ArrayList();

            foreach (ListItem li in objDDL.Items)
            {
                textList.Add(li.Text);
            }

            textList.Sort();

            foreach (object item in textList)
            {
                string value = objDDL.Items.FindByText(item.ToString()).Value;
                valueList.Add(value);
            }
            objDDL.Items.Clear();

            for (int i = 0; i < textList.Count; i++)
            {
                ListItem objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
                objDDL.Items.Add(objItem);
            }
        }

        private void load_party()
        {
            tbl_PuTr_Pr_DetTableAdapter det = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetDataTable dt = new dsProcTran.tbl_PuTr_Pr_DetDataTable();

            tbl_PuMa_Par_Adr_QtnTableAdapter adr = new tbl_PuMa_Par_Adr_QtnTableAdapter();

            ListItem lst;
            string tmpstr;
            bool dupp;

            ddlparty.Items.Clear();
            ddlparty.Items.Add("");

            dt = det.GetDataByStatus("APP","P");

            foreach (dsProcTran.tbl_PuTr_Pr_DetRow dr in dt.Rows)
            {
                dupp = false;
                lst = new ListItem();
                try
                {
                    lst.Text = adr.GetDataByQtnAdrRef(dr.Pr_Det_App_Party.ToString())[0].Par_Adr_Qtn_Name.ToString() + ": " + dr.Pr_Det_Code + ": " + dr.Pr_Det_Pur_Type;
                    tmpstr = dr.Pr_Det_Code + ":" + dr.Pr_Det_Pur_Type + ":" + dr.Pr_Det_App_Party;
                    lst.Value = tmpstr;
                    foreach (ListItem ls in ddlparty.Items)
                    {
                        if (ls.Value.ToString() == tmpstr) dupp = true;
                    }
                    if (!dupp)
                        ddlparty.Items.Add(lst);
                }
                catch { }
            }

            SortDDL(ref this.ddlparty);
        }

        protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pcode = ddlparty.SelectedValue.ToString();

            btncreate.Visible = false;
            lbltot.Visible = false;
            gdItem.Visible = false;

            if (pcode == "")
            {
                return;
            }

            lbltot.Visible = true;
            gdItem.Visible = true;
            btncreate.Visible = true;
            generate_data(pcode);
        }

        private void generate_data(string party_det)
        {
            decimal qty, rate, tot, gtot;

            string[] tmp = party_det.Split(':');
            string cash_type = tmp[0].ToString();
            string pur_type = tmp[1].ToString();
            string app_party = tmp[2].ToString();
            int icnt = 0;

            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable dtlog = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_PuMa_Par_Adr_QtnTableAdapter adr = new tbl_PuMa_Par_Adr_QtnTableAdapter();

            tbl_Qtn_DetTableAdapter qdet = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetRow qr;

            tbl_PuTr_Pr_DetTableAdapter det = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetDataTable dtdet = new dsProcTran.tbl_PuTr_Pr_DetDataTable();            
            dsProcTran.tbl_PuTr_Pr_DetRow dr;

            dtdet = det.GetDataByPartyToCreatePo("APP", app_party, cash_type, pur_type);

            if (dtdet.Rows.Count == 0) { btncreate.Visible = false; lbltot.Visible = false; return; }

            btncreate.Visible = true;
            lbltot.Visible = true;

            gtot = 0;

            gdItem.DataSource = dtdet;
            gdItem.DataBind();

            foreach (GridViewRow gr in gdItem.Rows)
            {
                Label lblitem = (Label)gr.FindControl("Label1");
                Label lblrefno = (Label)gr.FindControl("Label2");
                Label lblparty = (Label)gr.FindControl("Label3");
                Label lblqty = (Label)gr.FindControl("Label4");
                Label lblavqty = (Label)gr.FindControl("Label5");
                Label lblrate = (Label)gr.FindControl("Label6");
                Label lblamount = (Label)gr.FindControl("Label7");
                Label lblspe = (Label)gr.FindControl("Label8");
                Label lblbrand = (Label)gr.FindControl("Label9");
                Label lblorigin = (Label)gr.FindControl("Label10");
                Label lblpacking = (Label)gr.FindControl("Label11");
                CheckBox chksel = (CheckBox)gr.FindControl("CheckBox1");
                TextBox txtpoqty = (TextBox)gr.FindControl("TextBox1");

                chksel.Checked = true;

                dr = dtdet[icnt];

                qty = Convert.ToDecimal(dr.Pr_Det_Bal_Qty);
                rate = Convert.ToDecimal(dr.Pr_Det_Lin_Rat);
                tot = qty * rate;

                gtot = gtot + tot;

                lblrefno.Text = dr.Pr_Det_Ref.ToString();
                lblparty.Text = app_party + ":" + adr.GetDataByQtnAdrRef(app_party)[0].Par_Adr_Qtn_Name.ToString();
                lblitem.Text = dr.Pr_Det_Icode.ToString() + ": " + dr.Pr_Det_Itm_Desc.ToString();
                lblqty.Text = dr.Pr_Det_Lin_Qty.ToString("N2") + " " + dr.Pr_Det_Itm_Uom.ToString();
                lblavqty.Text = dr.Pr_Det_Bal_Qty.ToString("N2");
                txtpoqty.Text = dr.Pr_Det_Bal_Qty.ToString("N2");
                lblrate.Text = rate.ToString("N2");
                lblamount.Text = tot.ToString("N2");

                var dtqdet = qdet.GetDataByQtnRefParty(dr.Pr_Det_Quot_Ref, dr.Pr_Det_App_Party);
                if (dtqdet.Rows.Count > 0)
                {
                    qr = qdet.GetDataByQtnRefParty(dr.Pr_Det_Quot_Ref, dr.Pr_Det_App_Party)[0];
                    lblspe.Text = qr.Qtn_Itm_Spec;
                    lblbrand.Text = qr.Qtn_Itm_Brand;
                    lblorigin.Text = qr.Qtn_Itm_Origin;
                    lblpacking.Text = qr.Qtn_Itm_Packing;
                }
                icnt++;
            }

            if (gtot == 0)
                btncreate.Visible = false;
            else
                btncreate.Visible = true;

            lbltot.Text = "Total Amount TK: " + gtot.ToString("N2");
        }

        protected void btncreate_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            GlobalClass.clsMpo [] seldet;
            seldet = new GlobalClass.clsMpo[gdItem.Rows.Count];

            foreach (GridViewRow gr in gdItem.Rows)
            {
                CheckBox chksel = (CheckBox)gr.FindControl("CheckBox1");

                if (chksel.Checked)
                {
                    Label lblrefno = (Label)gr.FindControl("Label2");
                    Label lblitem = (Label)gr.FindControl("Label1");
                    TextBox txtpoqty = (TextBox)gr.FindControl("TextBox1");
                    Label lblavqty = (Label)gr.FindControl("Label5");

                    if (txtpoqty.Text != "")
                    {
                        if ((Convert.ToDecimal(txtpoqty.Text) > 0) && (Convert.ToDecimal(lblavqty.Text) >= Convert.ToDecimal(txtpoqty.Text)))
                        {
                            seldet[cnt] = new GlobalClass.clsMpo();

                            seldet[cnt].Seq = cnt;
                            seldet[cnt].RefNo = lblrefno.Text;
                            seldet[cnt].Icode = lblitem.Text.Split(':')[0];
                            seldet[cnt].Qnty = Convert.ToDecimal(txtpoqty.Text);

                            cnt = cnt + 1;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            string[] tmp = ddlparty.SelectedValue.ToString().Split(':');
            string app_party = tmp[2].ToString();

            if (app_party.Substring(0, 4) == "APN-") return;

            if (cnt > 0)
            {
                Session["sessionItemSelForPO"] = seldet;
                Session["sessionPartySelForPO"] = ddlparty.SelectedValue.ToString();

                Response.Redirect("./frmPoInitFinal.aspx");
            }
        }
    }
}