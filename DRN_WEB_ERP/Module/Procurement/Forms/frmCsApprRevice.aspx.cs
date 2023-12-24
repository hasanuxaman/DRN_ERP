using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmCsApprRevice : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        int i = 0;
        double totSup1 = 0;
        double[] totSumCol;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;        
        }

        private void Get_Details_Data(string csRefNo )
        {
            try
            {

                gvCS.DataSource = null;
                gvCS.DataBind();

                gvCsSum.DataSource = null;
                gvCsSum.DataBind();

                Get_Comments_Data("");
                txtcomments.Text = "";
                txtcomments.Visible = false;

                btnApp.Visible = false;
                btnRej.Visible = false;

                SqlConnection connection = new SqlConnection();
                var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
                connection.ConnectionString = ConnectionString;
                connection.Open();

                string qtnSupStr = "select distinct Par_Adr_Name from tbl_Qtn_Det left outer join tbl_PuMa_Par_Adr on Qtn_Par_Code=Par_Adr_Ref where Qtn_Ref_No='" + csRefNo.ToString() + "'";
                SqlDataAdapter daSup = new SqlDataAdapter(qtnSupStr, connection);
                DataTable dtQtnSupData = new DataTable();
                daSup.Fill(dtQtnSupData);

                int r = dtQtnSupData.Rows.Count;
                if (dtQtnSupData.Rows.Count == 0) return;

                string t = "";
                string t1 = "";
                var supName = "";
                i = 0;
                foreach (DataRow dr in dtQtnSupData.Rows)
                {
                    i = i + 1;
                    supName = dr[0].ToString();

                    t = t + "SUM(case When Par_Adr_Name='" + supName + "' then Qtn_Itm_Rate else 0 end) as [" + supName + "],";

                    t1 = t1 + "SUM(case When Par_Adr_Name='" + supName + "' then Qtn_Tot_Amnt else 0 end) as [" + supName + " (Total)],";
                }
                t = t + t1;
                t = t.Substring(0, t.Length - 1);

                totSumCol = new double[i];

                var qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_CS_APP_TEMP_" + Session["sessionUserId"].ToString() + "]')) DROP VIEW [dbo].[View_CS_APP_TEMP_" + Session["sessionUserId"].ToString() + "]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                string queryString1 = "CREATE VIEW View_CS_APP_TEMP_" + Session["sessionUserId"].ToString() + " as select Qtn_Req_No,Qtn_Itm_Code,Qtn_Itm_Qty, " + t +
                                      " from tbl_Qtn_Det left outer join tbl_PuMa_Par_Adr on Qtn_Par_Code=Par_Adr_Ref " +
                                      " where Qtn_Ref_No='" + csRefNo.ToString() + "' group by Qtn_Req_No,Qtn_Itm_Code,Qtn_Itm_Qty";
                dbCon.ExecuteSQLStmt(queryString1);

                string queryString = "SELECT ROW_NUMBER() OVER(ORDER BY Pr_Det_Ref ASC) AS SL#, [Pr_Det_Ref] as [MPR. Ref],[Pr_Det_Icode] as [Item Code], " +
                                     "[Pr_Det_Itm_Desc] as [Item Name],[Pr_Det_Str_Code] as [Store],[Pr_Det_Spec] as [Specification],[Pr_Det_Brand] as [Brand], " +
                                     "[Pr_Det_Origin] as [Origin],[Pr_Det_Packing] as [Packing],[Pr_Det_Bat_No] as [MPR. No],[Pr_Det_Lin_Qty] as [Quantity], " +
                                     "[Pr_Det_Itm_Uom] as [Unit],View_CS_APP_TEMP_" + Session["sessionUserId"].ToString() + ".* FROM [DRN].[dbo].[tbl_PuTr_Pr_Det] " +
                                     "Inner Join View_CS_APP_TEMP_" + Session["sessionUserId"].ToString() + " On [Pr_Det_Ref]=[Qtn_Req_No] " +
                                     "and [Pr_Det_Icode]=[Qtn_Itm_Code] WHERE [Pr_Det_Quot_Ref]='" + csRefNo.ToString() + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                DataTable dtCsData = new DataTable();
                adapter.Fill(dtCsData);
                gvCS.DataSource = dtCsData;
                gvCS.DataBind();

                var qryStr = "select Qtn_Par_Code,Par_Adr_Name,Sum(Qtn_Tot_Amnt) as Qtn_Val from tbl_Qtn_Det " +
                             "left outer join tbl_PuMa_Par_Adr on Qtn_Par_Code=Par_Adr_Ref where Qtn_Ref_No='" + csRefNo.ToString() +
                             "' group by Qtn_Par_Code,Par_Adr_Name";

                SqlDataAdapter daCsSum = new SqlDataAdapter(qryStr, connection);
                DataTable dtCsSum = new DataTable();
                daCsSum.Fill(dtCsSum);
                gvCsSum.DataSource = dtCsSum;
                gvCsSum.DataBind();

                Get_Comments_Data(csRefNo.ToString());
                txtcomments.Visible = true;
                btnApp.Visible = true;
                btnRej.Visible = true;
            }
            catch (Exception ex)
            {
                gvCS.DataSource = null;
                gvCS.DataBind();

                gvCsSum.DataSource = null;
                gvCsSum.DataBind();

                Get_Comments_Data("");
                txtcomments.Text = "";
                txtcomments.Visible = false;

                btnApp.Visible = false;
                btnRej.Visible = false;

                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private void Get_Comments_Data(string csref)
        {
            tbl_Tran_ComTableAdapter com = new tbl_Tran_ComTableAdapter();
            dsProcTran.tbl_Tran_ComDataTable dt = new dsProcTran.tbl_Tran_ComDataTable();
            View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();

            dt = com.GetDataByRefNo(csref);
            phcomments.Controls.Clear();

            var qtnSeqNo = 0;
            foreach (dsProcTran.tbl_Tran_ComRow dr in dt.Rows)
            {
                qtnSeqNo = dr.Com_Seq_no;

                Module.Procurement.Forms.UserControl.ctlQtnCom ctl = (Module.Procurement.Forms.UserControl.ctlQtnCom)LoadControl("./UserControl/CtlQtnCom.ascx");

                Label lblname = (Label)ctl.FindControl("lblname");
                Label lbldate = (Label)ctl.FindControl("lbldate");
                HtmlTableCell celcomm = (HtmlTableCell)ctl.FindControl("celcomm");
                Image imgimage = (Image)ctl.FindControl("imgimage");

                //imgimage.ImageUrl = "./handler/hnd_image.ashx?id=" + dr.app_id;

                //imgimage.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dr.Com_App_Id + "'";

                var dtEmp = taEmp.GetDataByEmpId(dr.Com_App_Id.ToString());

                imgimage.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + (dtEmp.Rows.Count > 0 ? dtEmp[0].EmpRefNo : "") + "'";

                ctl.ID = "ctl_" + phcomments.Controls.Count.ToString();

                lblname.Text = dr.Com_App_Name.ToString() + " (" + dr.Com_App_Desig.ToString() + ")";
                lbldate.Text = dr.Com_App_Date.ToString();
                celcomm.InnerText = dr.Com_Gen_Com.ToString();

                phcomments.Controls.Add(ctl);
            }
        }

        protected void gvCS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var taPrDet = new tbl_PuTr_Pr_DetTableAdapter();

            if (e.Row.Cells.Count >= 14)
            {
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var chk = (CheckBox)e.Row.FindControl("chkCsItem");

                var mpr = e.Row.Cells[1].Text.Trim();
                var itm = e.Row.Cells[2].Text.Trim();
                
                var dtPrDet = taPrDet.GetDataByPrRefItem(mpr, itm);
                if (dtPrDet.Rows.Count > 0)
                {
                    if (dtPrDet[0].Pr_Det_Status == "APR")
                    {
                        chk.Enabled = true;
                        chk.Checked = true;
                    }
                    else if (dtPrDet[0].Pr_Det_Status == "QTN")
                    {
                        chk.Enabled = true;
                        chk.Checked = false;
                    }
                    else
                    {
                        chk.Enabled = false;
                        chk.Checked = false;
                    }
                }
                else
                {
                    chk.Enabled = false;
                    chk.Checked = false;
                }

                for (int cnt = 0; cnt < i; cnt++)
                {
                    totSup1 += Convert.ToDouble(e.Row.Cells[e.Row.Cells.Count - (3 + 0)].Text);
                    totSumCol[cnt] += Convert.ToDouble(e.Row.Cells[e.Row.Cells.Count - (3 + cnt)].Text);
                    e.Row.Cells[e.Row.Cells.Count - (3 + cnt)].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[e.Row.Cells.Count - (3 + cnt)].Font.Bold = true;
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int cnt = 0; cnt < i; cnt++)
                {
                    e.Row.Cells[e.Row.Cells.Count - (3 + cnt)].Text = totSumCol[cnt].ToString("");
                    e.Row.Cells[e.Row.Cells.Count - (3 + cnt)].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[e.Row.Cells.Count - (3 + cnt)].Font.Bold = true;
                }
                e.Row.Cells[e.Row.Cells.Count - (3 + i)].Text = "Total:";
            }
        }

        protected void gvCS_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            foreach (DataControlField column in gvCS.Columns)
            {
                //Get the first Cell /Column
                TableCell cell = row.Cells[0];
                // Then Remove it after
                row.Cells.Remove(cell);
                //And Add it to the List Collections
                columns.Add(cell);
            }

            // Add cells
            row.Cells.AddRange(columns.ToArray());
        }

        protected void gvCsSum_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblSupRef = ((Label)e.Row.FindControl("lblSupRef")).Text.Trim();
                var lblQtnVal = ((Label)e.Row.FindControl("lblQtnVal")).Text.Trim();
                var txtCaryCrg = ((TextBox)e.Row.FindControl("txtCaryCrg"));
                var txtLoadCrg = ((TextBox)e.Row.FindControl("txtLoadCrg"));
                var txtDisAmt = ((TextBox)e.Row.FindControl("txtDisAmt"));
                var lblGrandTot = (Label)e.Row.FindControl("lblGrandTot");

                var taCsSum = new tbl_PuTr_CS_SumTableAdapter();
                var dtCsSum = taCsSum.GetDataByQtnPartyRef(hfCsRef.Value.Trim().ToString(), lblSupRef.ToString(), "QTN");
                if (dtCsSum.Rows.Count > 0)
                {
                    txtCaryCrg.Text = dtCsSum[0].CS_Cary_Amt.ToString("N2");
                    txtLoadCrg.Text = dtCsSum[0].CS_Load_Amt.ToString("N2");
                    txtDisAmt.Text = dtCsSum[0].CS_Disc_Amt.ToString("N2");
                    //lblGrandTot.Text = dtCsSum[0].CS_Tot_Amt.ToString();
                }
                lblGrandTot.Text = ((Convert.ToDouble(lblQtnVal) + Convert.ToDouble(txtCaryCrg.Text.Trim().Length <= 0 ? "0" : txtCaryCrg.Text.Trim()) +
                                        Convert.ToDouble(txtLoadCrg.Text.Trim().Length <= 0 ? "0" : txtLoadCrg.Text.Trim())) -
                                        Convert.ToDouble(txtDisAmt.Text.Trim().Length <= 0 ? "0" : txtDisAmt.Text.Trim())).ToString("F2");

                txtCaryCrg.Attributes.Add("onkeyup", "CalGrandTot('" + txtCaryCrg.ClientID + "', '" + txtLoadCrg.ClientID + "', '"
                    + txtDisAmt.ClientID + "', '" + lblQtnVal.ToString() + "', '" + lblGrandTot.ClientID + "' )");
                txtLoadCrg.Attributes.Add("onkeyup", "CalGrandTot('" + txtCaryCrg.ClientID + "', '" + txtLoadCrg.ClientID + "', '"
                    + txtDisAmt.ClientID + "', '" + lblQtnVal.ToString() + "', '" + lblGrandTot.ClientID + "' )");
                txtDisAmt.Attributes.Add("onkeyup", "CalGrandTot('" + txtCaryCrg.ClientID + "', '" + txtLoadCrg.ClientID + "', '"
                    + txtDisAmt.ClientID + "', '" + lblQtnVal.ToString() + "', '" + lblGrandTot.ClientID + "' )");
            }
        }

        protected void btnQtnPrint_Click(object sender, EventArgs e)
        {
            Session["CsRefNo"] = hfCsRef.Value.Trim().ToString();
            var url = "frmCsPrint.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void btnAppAll_Click(object sender, EventArgs e)
        {
            optListSupp.Items.Clear();

            SqlConnection connection = new SqlConnection();
            var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            string qtnSupStr = "select distinct Par_Adr_Ref,Par_Adr_Name from tbl_Qtn_Det left outer join tbl_PuMa_Par_Adr on Qtn_Par_Code=Par_Adr_Ref where Qtn_Ref_No='" + hfCsRef.Value.Trim().ToString() + "'";
            SqlDataAdapter daSup = new SqlDataAdapter(qtnSupStr, connection);
            DataTable dtQtnSupData = new DataTable();
            daSup.Fill(dtQtnSupData);
            if (dtQtnSupData.Rows.Count > 0)
            {
                foreach (DataRow dr in dtQtnSupData.Rows)
                {
                    optListSupp.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
                optListSupp.SelectedIndex = 0;
                ModalPopupExtender1.Show();
            }
            else
            {
                tblMsg.Rows[0].Cells[0].InnerText = "No Supplier data found.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            ButtonOk.Enabled = false;
            ButtonCancel.Enabled = false;

            tbl_PuTr_Pr_DetTableAdapter det = new tbl_PuTr_Pr_DetTableAdapter();
            tbl_Tran_ComTableAdapter comm = new tbl_Tran_ComTableAdapter();
            View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();
            tbl_PuMa_Par_AdrTableAdapter taParAdr = new tbl_PuMa_Par_AdrTableAdapter();
            tbl_PuTr_CS_SumTableAdapter taCsSum = new tbl_PuTr_CS_SumTableAdapter();

            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(det.Connection);

            var i = 0;
            foreach (GridViewRow gr in gvCS.Rows)
            {
                var chk = (CheckBox)gr.FindControl("chkCsItem");
                if (chk.Checked) i++;
            }

            if (i <= 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Select item first.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }

            string uid = "", uname = "", desig = "";
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
            if (dtEmp.Rows.Count > 0)
            {
                uid = dtEmp[0].EmpId.ToString();
                uname = dtEmp[0].EmpName.ToString();
                desig = dtEmp[0].DesigName.ToString();
            }

            string csref, party_code, party_det;
            csref = hfCsRef.Value.Trim().ToString();
            party_code = optListSupp.SelectedValue.ToString();
            var dtParty = taParAdr.GetDataBySupAdrRef(party_code.ToString());
            party_det = dtParty.Rows.Count > 0 ? dtParty[0].Par_Adr_Name.ToString() : "";

            string status, comments_det = "";
            string my_app = get_my_app();
            status = "APR";
                          
            try
            {
                det.AttachTransaction(myTrn);
                comm.AttachTransaction(myTrn);
                taCsSum.AttachTransaction(myTrn);

                var itemSelected = "";
                int rowCnt = 0, selectCnt = 0;
                foreach (GridViewRow gr in gvCS.Rows)
                {
                    rowCnt++;
                    var chk = (CheckBox)gr.FindControl("chkCsItem");
                    if (chk.Checked)
                    {
                        selectCnt++;
                        string icode;
                        decimal lrate;

                        icode = gr.Cells[2].Text;
                        itemSelected = icode + "," + itemSelected;

                        //if (check_approval_validity(icode, csref) == false) { return; }                        
                        lrate = Convert.ToDecimal(0);
                        det.UpdatePrDetStatusFromCs(lrate, status, party_code, icode, csref);
                    }
                }

                if (rowCnt > selectCnt)
                    comments_det = "(Audit Reviced Party: " + party_det + " for the item code " + itemSelected.Substring(0, itemSelected.Length - 1) + ") " + txtcomments.Text.Trim();
                else
                    comments_det = "(Audit Reviced Party: " + party_det + ") " + txtcomments.Text.Trim();

                foreach (GridViewRow gr in gvCsSum.Rows)
                {
                    var supRef = ((Label)gr.FindControl("lblSupRef")).Text.Trim();
                    var qtnVal = ((Label)gr.FindControl("lblQtnVal")).Text.Trim();
                    var caryCrg = ((TextBox)gr.FindControl("txtCaryCrg")).Text.Trim();
                    var loadCrg = ((TextBox)gr.FindControl("txtLoadCrg")).Text.Trim();
                    var disAmt = ((TextBox)gr.FindControl("txtDisAmt")).Text.Trim();
                    var grandTot = ((Label)gr.FindControl("lblGrandTot")).Text.Trim();

                    qtnVal = qtnVal.Length <= 0 ? "0" : qtnVal;
                    caryCrg = caryCrg.Length <= 0 ? "0" : caryCrg;
                    loadCrg = loadCrg.Length <= 0 ? "0" : loadCrg;
                    disAmt = disAmt.Length <= 0 ? "0" : disAmt;
                    grandTot = grandTot.Length <= 0 ? "0" : grandTot;

                    //if (party_code.ToString() == supRef.ToString())
                    //{
                    taCsSum.InsertCsSum(csref, 1, supRef, Convert.ToDecimal(qtnVal), Convert.ToDecimal(caryCrg), Convert.ToDecimal(loadCrg),
                        Convert.ToDecimal(disAmt), Convert.ToDecimal(grandTot), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "1", "APP");

                    //    break;
                    //}
                }

                comm.InsertTranCom(csref, 2, DateTime.Now, uid, uname, desig, 1, my_app, status, comments_det, "", "1", "", "", "", "");
                
                myTrn.Commit();

                Get_Details_Data(hfCsRef.Value.Trim().ToString());               
                txtcomments.Text = "";
                ButtonOk.Enabled = true;
                ButtonCancel.Enabled = true;
            }
            catch (Exception ex)
            {
                myTrn.Rollback();
                ButtonOk.Enabled = true;
                ButtonCancel.Enabled = true;
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();             
            }
            finally
            {
                GlobalClass.clsDbHelper.CloseTransaction(det.Connection, myTrn);
                ButtonOk.Enabled = true;
                ButtonCancel.Enabled = true;
            }
        }

        private bool check_approval_validity(string icode, string quoref)
        {
            tbl_PuTr_Pr_DetTableAdapter det = new tbl_PuTr_Pr_DetTableAdapter();
            bool flg = false;
            string my_app = get_my_app();

            if (det.GetDataByQtnRefItemStatus(quoref, icode, "QTN").Count > 0) flg = true;

            return flg;
        }

        private string get_my_app()
        {
            //User_Role_DefinitionTableAdapter urole = new User_Role_DefinitionTableAdapter();
            //SCBLDataSet.User_Role_DefinitionDataTable udt = new SCBLDataSet.User_Role_DefinitionDataTable();

            string my_app = "";
            //udt = urole.GetRoleByUser(Session["sessionUserId"].ToString(), "CS");
            //if (udt.Rows.Count > 0) my_app = udt[0].role_as.ToString();

            return my_app;
        }

        private bool savedata(string act_type, string icode, string qref,string party, bool rej)
        {
            bool flg = true;

            tbl_PuTr_Pr_DetTableAdapter det = new tbl_PuTr_Pr_DetTableAdapter();

            tbl_Tran_ComTableAdapter comm = new tbl_Tran_ComTableAdapter();

            View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();

            tbl_PuMa_Par_AdrTableAdapter taParAdr = new tbl_PuMa_Par_AdrTableAdapter();

            string my_app = get_my_app();
            string party_code, status, status1, party_det, comments_det;
            decimal lrate;
            
            string uid = "", uname = "", desig = "";

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
            if (dtEmp.Rows.Count > 0)
            {
                uid = dtEmp[0].EmpId.ToString();
                uname = dtEmp[0].EmpName.ToString();
                desig = dtEmp[0].DesigName.ToString();
            }

            party_code = party;
            lrate = Convert.ToDecimal(0);            
            
            var dtParty = taParAdr.GetDataBySupAdrRef(party_code.ToString());
            var partyName = dtParty.Rows.Count > 0 ? dtParty[0].Par_Adr_Name.ToString() : "";
            party_det = partyName;

            if (act_type == "APR")
            {
                comments_det = "(Audit Recomended Party: " + partyName + ") " + txtcomments.Text.Trim();
                status = "APR";
                status1 = "";
            }
            else
            {
                if (act_type == "REJ")
                {
                    comments_det = "(Audit Rejected) " + txtcomments.Text.Trim();
                    status = "TEN";
                    status1 = "";
                }
                else
                {
                    comments_det = "(Audit Recomended Party: " + partyName + ") " + txtcomments.Text.Trim();
                    status = "QTN";
                }

            }
            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(det.Connection);

            try
            {
                det.AttachTransaction(myTrn);
                comm.AttachTransaction(myTrn);

                det.UpdatePrDetStatusFromCs(lrate, status, party_code, icode, qref);
                comm.InsertTranCom(qref, 2, DateTime.Now, uid, uname, desig, 1, my_app, status, comments_det, "", "1", "", "", "", "");

                myTrn.Commit();
            }
            catch
            {
                myTrn.Rollback();
                flg = false;
            }

            finally
            {
                GlobalClass.clsDbHelper.CloseTransaction(det.Connection, myTrn);
            }

        skip_all:

            return flg;
        }

        protected void btnApp_Click(object sender, EventArgs e)
        {
            optListSupp.Items.Clear();

            SqlConnection connection = new SqlConnection();
            var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
            connection.ConnectionString = ConnectionString;
            connection.Open();

            string qtnSupStr = "select distinct Par_Adr_Ref,Par_Adr_Name from tbl_Qtn_Det left outer join tbl_PuMa_Par_Adr on Qtn_Par_Code=Par_Adr_Ref where Qtn_Ref_No='" + hfCsRef.Value.Trim().ToString() + "'";
            SqlDataAdapter daSup = new SqlDataAdapter(qtnSupStr, connection);
            DataTable dtQtnSupData = new DataTable();
            daSup.Fill(dtQtnSupData);
            if (dtQtnSupData.Rows.Count > 0)
            {
                foreach (DataRow dr in dtQtnSupData.Rows)
                {
                    optListSupp.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
                optListSupp.SelectedIndex = 0;
                ModalPopupExtender1.Show();
            }
            else
            {
                tblMsg.Rows[0].Cells[0].InnerText = "No Supplier data found.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }            
        }

        protected void btnRej_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Show();
        }

        protected void lntQtnDet_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var mprRefNo = row.Cells[1].Text.Trim().ToString();
            var itmCode = row.Cells[2].Text.Trim().ToString();

            Session["QtnMprRefNo"] = mprRefNo.ToString();
            Session["QtnItemCode"] = itmCode.ToString();
            var url = "frmCsQtnDet.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void btnRejOk_Click(object sender, EventArgs e)
        {
            btnRejOk.Enabled = false;
            btnRejCancel.Enabled = false;

            tbl_PuTr_Pr_DetTableAdapter det = new tbl_PuTr_Pr_DetTableAdapter();
            tbl_Tran_ComTableAdapter comm = new tbl_Tran_ComTableAdapter();
            View_Emp_BascTableAdapter taEmp = new View_Emp_BascTableAdapter();
            tbl_PuMa_Par_AdrTableAdapter taParAdr = new tbl_PuMa_Par_AdrTableAdapter();
            tbl_PuTr_CS_SumTableAdapter taCsSum = new tbl_PuTr_CS_SumTableAdapter();
            tbl_Qtn_DetTableAdapter qtn = new tbl_Qtn_DetTableAdapter();
            View_Qtn_Val_SumTableAdapter taQtnValSum = new View_Qtn_Val_SumTableAdapter();

            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(det.Connection);

            var itemDesc = "";
            var i = 0;
            foreach (GridViewRow gr in gvCS.Rows)
            {
                var chk = (CheckBox)gr.FindControl("chkCsItem");
                if (chk.Checked)
                {
                    itemDesc = itemDesc + "[" + gr.Cells[2].Text.ToString().Trim() + "] " + gr.Cells[3].Text.ToString().Trim() + ", ";
                    i++;
                }
            }

            if (i <= 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Select item first.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }

            string uid = "", uname = "", desig = "";
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
            if (dtEmp.Rows.Count > 0)
            {
                uid = dtEmp[0].EmpId.ToString();
                uname = dtEmp[0].EmpName.ToString();
                desig = dtEmp[0].DesigName.ToString();
            }

            string csref;
            csref = hfCsRef.Value.Trim().ToString();

            string status, comments_det;
            status = "TEN";
            comments_det = i == gvCS.Rows.Count ? "(Audit Rejected) " + txtcomments.Text.Trim() : "(Audit Rejected Partially for item " + itemDesc + ") " + txtcomments.Text.Trim();

            try
            {                                
                det.AttachTransaction(myTrn);
                comm.AttachTransaction(myTrn);
                taCsSum.AttachTransaction(myTrn);
                qtn.AttachTransaction(myTrn);
                taQtnValSum.AttachTransaction(myTrn);

                foreach (GridViewRow gr in gvCS.Rows)
                {
                    var chk = (CheckBox)gr.FindControl("chkCsItem");
                    if (chk.Checked)
                    {
                        string mprref;
                        string icode;
                        decimal lrate;

                        mprref = gr.Cells[1].Text;
                        icode = gr.Cells[2].Text;
                        lrate = Convert.ToDecimal(0);
                        det.UpdatePrDetStatusFromCsRej(lrate, status, "", "", "REJ", icode, csref);
                        qtn.UpdateQtnStatus("", mprref, icode, csref);
                    }
                }

                taCsSum.DeleteCsSum(csref);

                var cnt = 0;
                var dtQtnValSum = taQtnValSum.GetData(csref);
                foreach (dsProcTran.View_Qtn_Val_SumRow dr in dtQtnValSum.Rows)
                {
                    foreach (GridViewRow gr in gvCsSum.Rows)
                    {
                        var pcode = ((Label)gr.FindControl("lblSupRef")).Text.Trim();

                        var caryCrg = ((TextBox)gr.FindControl("txtCaryCrg")).Text.Trim();
                        var loadCrg = ((TextBox)gr.FindControl("txtLoadCrg")).Text.Trim();
                        var disAmt = ((TextBox)gr.FindControl("txtDisAmt")).Text.Trim();
                        var grandTot = ((Label)gr.FindControl("lblGrandTot")).Text.Trim();

                        caryCrg = caryCrg.Length <= 0 ? "0" : caryCrg;
                        loadCrg = loadCrg.Length <= 0 ? "0" : loadCrg;
                        disAmt = disAmt.Length <= 0 ? "0" : disAmt;
                        grandTot = grandTot.Length <= 0 ? "0" : grandTot;

                        if (dr.Qtn_Par_Code == pcode)
                        {
                            cnt++;
                            var totAmt = (Convert.ToDecimal(dr.Qtn_Val) + Convert.ToDecimal(caryCrg) + Convert.ToDecimal(loadCrg)) - Convert.ToDecimal(disAmt);

                            taCsSum.InsertCsSum(csref, cnt, dr.Qtn_Par_Code, Convert.ToDecimal(dr.Qtn_Val), Convert.ToDecimal(caryCrg), Convert.ToDecimal(loadCrg),
                                                    Convert.ToDecimal(disAmt), Convert.ToDecimal(totAmt), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "1", "QTN");
                        }
                        //else
                        //{
                        //    cnt++;
                        //    taCsSum.InsertCsSum(csref, 1, dr.Qtn_Par_Code, Convert.ToDecimal(dr.Qtn_Val), Convert.ToDecimal(0), Convert.ToDecimal(0),
                        //                                Convert.ToDecimal(0), Convert.ToDecimal(dr.Qtn_Val), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "1", "QTN");
                        //}
                    }
                }

                comm.InsertTranCom(csref, 2, DateTime.Now, uid, uname, desig, 1, "", status, comments_det, "", "1", "", "", "", "");

                myTrn.Commit();

                Get_Details_Data(hfCsRef.Value.Trim().ToString());
                txtcomments.Text = "";
                btnRejOk.Enabled = true;
                btnRejCancel.Enabled = true;
            }
            catch (Exception ex)
            {
                myTrn.Rollback();
                btnRejOk.Enabled = true;
                btnRejCancel.Enabled = true;
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            finally
            {
                GlobalClass.clsDbHelper.CloseTransaction(det.Connection, myTrn);
                btnRejOk.Enabled = true;
                btnRejCancel.Enabled = true;
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchCs.Text = "";
            hfCsRef.Value = "0";
            Get_Details_Data(hfCsRef.Value.Trim().ToString());
        }

        protected void txtSearchCs_TextChanged(object sender, EventArgs e)
        {
            var csRef = "";
            var srchWords = txtSearchCs.Text.Trim().Split(':');
            foreach (string word in srchWords)
            {
                csRef = word;
                break;
            }

            if (csRef.Length > 0)
            {
                var taCs = new tbl_PuTr_Pr_DetTableAdapter();
                var dtCs = taCs.GetDataByCsRefNo(csRef.ToString());
                if (dtCs.Rows.Count > 0)
                {
                    hfCsRef.Value = dtCs[0].Pr_Det_Quot_Ref.ToString();
                    Get_Details_Data(hfCsRef.Value.Trim().ToString());
                }
                else
                {
                    hfCsRef.Value = "0";
                    tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            var csRef = "";
            var srchWords = txtSearchCs.Text.Trim().Split(':');
            foreach (string word in srchWords)
            {
                csRef = word;
                break;
            }

            if (csRef.Length > 0)
            {
                var taCs = new tbl_PuTr_Pr_DetTableAdapter();
                var dtCs = taCs.GetDataByCsRefNo(csRef.ToString());
                if (dtCs.Rows.Count > 0)
                {
                    hfCsRef.Value = dtCs[0].Pr_Det_Quot_Ref.ToString();
                    Get_Details_Data(hfCsRef.Value.Trim().ToString());
                }
                else
                {
                    hfCsRef.Value = "0";
                    tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }
    }
}