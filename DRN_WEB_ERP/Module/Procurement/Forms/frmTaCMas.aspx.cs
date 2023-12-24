using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmTaCMas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";

            //StaticData.MsgConfirmBox(btnadd, "Are you sure to add/edit ?");
            //StaticData.MsgConfirmBox(btndel, "Are you sure to remove ?");

            if (Page.IsPostBack) return;

            LoadTaC();
        }

        private void LoadTaC()
        {
            tbl_TaC_MasTableAdapter tac = new tbl_TaC_MasTableAdapter();
            dsProcMas.tbl_TaC_MasDataTable dt = new dsProcMas.tbl_TaC_MasDataTable();

            DataTable dtgrid = new DataTable();

            dt = tac.GetData();

            dtgrid.Rows.Clear();
            dtgrid.Columns.Clear();

            dtgrid.Columns.Add("TAC_ID", typeof(string));
            dtgrid.Columns.Add("TYPE", typeof(string));
            dtgrid.Columns.Add("SEQ", typeof(string));
            dtgrid.Columns.Add("CAT", typeof(string));
            dtgrid.Columns.Add("DETAILS", typeof(string));

            foreach (dsProcMas.tbl_TaC_MasRow dr in dt.Rows)
            {

                dtgrid.Rows.Add(dr.TaC_Id, dr.TaC_Type, dr.TaC_Seq_No, dr.TaC_Sel_Cat, dr.TaC_Det);
            }

            gdtac.DataSource = dtgrid;
            gdtac.DataBind();
        }

        protected void gdtac_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = new Unit("150px");
                e.Row.Cells[1].Width = new Unit("150px");
                e.Row.Cells[2].Width = new Unit("50px");
                e.Row.Cells[3].Width = new Unit("150px");
                e.Row.Cells[4].Width = new Unit("2550px");

                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdtac, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gdtac_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbl_TaC_MasTableAdapter tac = new tbl_TaC_MasTableAdapter();

            int indx = gdtac.SelectedIndex;

            if (indx != -1)
            {

                if (gdtac.Rows[indx].Cells[0].Text.Trim() == "&nbsp;")
                    txtid.Text = "";
                else
                    txtid.Text = gdtac.Rows[indx].Cells[0].Text.Trim();

                if (gdtac.Rows[indx].Cells[1].Text.Trim() == "&nbsp;")
                    ddltype.Text = "";
                else
                    try
                    {
                        ddltype.SelectedValue = gdtac.Rows[indx].Cells[1].Text.Trim();
                    }
                    catch
                    {
                        ddltype.Text = "";
                    }

                if (gdtac.Rows[indx].Cells[2].Text.Trim() == "&nbsp;")
                    txtseq.Text = "";
                else
                    txtseq.Text = gdtac.Rows[indx].Cells[2].Text.Trim();

                if (gdtac.Rows[indx].Cells[3].Text.Trim() == "&nbsp;")
                    ddlcat.Text = "";
                else
                    try
                    {
                        ddlcat.SelectedValue = gdtac.Rows[indx].Cells[3].Text.Trim();
                    }
                    catch
                    {
                        ddlcat.Text = "";
                    }

                if (gdtac.Rows[indx].Cells[4].Text.Trim() == "&nbsp;")
                    txteditor.Content = "";
                else
                {
                    //txtdet.Text = gdtac.Rows[indx].Cells[4].Text;
                    txteditor.Content = tac.GetDataByTacId(txtid.Text)[0].TaC_Det;
                }
            }
        }

        private void ClearData()
        {
            txtseq.Text = "";
            txtid.Text = "";
            ddlcat.Text = "";
            ddltype.Text = "";
            txteditor.Content = "";
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            tbl_TaC_MasTableAdapter tac = new tbl_TaC_MasTableAdapter();
            int seq;
            if (txtid.Text == "") return;
            try
            {
                seq = Convert.ToInt32(txtseq.Text);
            }
            catch { return; }

            if (tac.GetDataByTacId(txtid.Text).Rows.Count > 0)
            {
                tac.UpdateTaC(ddltype.SelectedValue, seq, ddlcat.SelectedValue, txteditor.Content, txteditor.Content, DateTime.Now, "user", "", "", "", "1", "", txtid.Text);
            }
            else
            {
                tac.InsertTaC(txtid.Text, ddltype.SelectedValue, seq, ddlcat.SelectedValue, txteditor.Content, txteditor.Content, DateTime.Now, "user", "", "", "", "1", "");
            }

            LoadTaC();
            ClearData();
        }

        protected void btndel_Click(object sender, EventArgs e)
        {
            tbl_TaC_MasTableAdapter tac = new tbl_TaC_MasTableAdapter();

            if (txtid.Text == "") return;

            tac.DeleteTaCById(txtid.Text);

            LoadTaC();
            ClearData();
        }
    }
}