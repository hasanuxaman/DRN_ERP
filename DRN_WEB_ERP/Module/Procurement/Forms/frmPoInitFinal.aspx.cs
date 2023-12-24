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
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmPoInitFinal : System.Web.UI.Page
    {
        GlobalClass.clsNumToText NumToText = new GlobalClass.clsNumToText();

        protected void Page_Load(object sender, EventArgs e)
        {            
            tblmaster.BgColor = "#f0f8ff";

            generate_data();  
        }

        private void generate_data()
        {
            rdovaliddays.Items.Clear();
            ListItem itm;
            string ddlval = Session["sessionPartySelForPO"].ToString();

            GlobalClass.clsMpo[] seldet = (GlobalClass.clsMpo[])Session["sessionItemSelForPO"];

            if (seldet == null) return;

            decimal qty, rate, tot, gtot;
            string tac_ref;
            string[] tmp = ddlval.Split(':');
            string cash_type = tmp[0].ToString();
            string pur_type = tmp[1].ToString();
            string app_party = tmp[2].ToString();
            int cnt, icnt, i, arraylen, vdays;

            Label lblseperator;

            RadioButtonList[] rdogen;
            rdogen = new RadioButtonList[20];

            RadioButtonList[] rdospe;
            rdospe = new RadioButtonList[20];

            RadioButtonList[] rdopay;
            rdopay = new RadioButtonList[20];

            for (i = 0; i < 20; i++)
            {
                rdogen[i] = new RadioButtonList();
                rdospe[i] = new RadioButtonList();
                rdopay[i] = new RadioButtonList();
            }

            ListItem lst;

            txtpurchasetype.Text = pur_type;
            txtreqtype.Text = cash_type;

            tbl_TaC_LogTableAdapter log = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogDataTable dtlog = new dsProcTran.tbl_TaC_LogDataTable();

            tbl_PuTr_Pr_DetTableAdapter dtdet = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetRow dr;

            tbl_PuMa_Par_Adr_QtnTableAdapter adr = new tbl_PuMa_Par_Adr_QtnTableAdapter();

            tbl_Qtn_DetTableAdapter qdet = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetRow qr;            

            DataTable dtgrd = new DataTable();

            dtgrd.Columns.Clear();
            dtgrd.Columns.Add("Sl", typeof(string));
            dtgrd.Columns.Add("Party", typeof(string));
            dtgrd.Columns.Add("Mpr", typeof(string));
            dtgrd.Columns.Add("Item", typeof(string));
            dtgrd.Columns.Add("Mpr Qty", typeof(string));
            dtgrd.Columns.Add("Avl Qty", typeof(string));
            dtgrd.Columns.Add("Po Qty", typeof(string));
            dtgrd.Columns.Add("Rate", typeof(string));
            dtgrd.Columns.Add("Amount", typeof(string));

            arraylen = seldet.Length;

            gtot = 0;
            cnt = 0;
            for (i = 0; i < arraylen; i++)
            {
                if (seldet[i] == null) goto skip_item;
                if (seldet[i].RefNo == null) goto skip_item;

                vdays = 0;
                dr = dtdet.GetDataByPrRefItem(seldet[i].RefNo, seldet[i].Icode)[0];
                if (dr.Pr_Det_Status != "APP") goto skip_item;
                if (Convert.ToDecimal(dr.Pr_Det_Bal_Qty) < seldet[i].Qnty) goto skip_item;
                cnt++;
                qty = seldet[i].Qnty;
                rate = Convert.ToDecimal(dr.Pr_Det_Lin_Rat);
                tot = qty * rate;
                gtot = gtot + tot;
                txtparty.Text = app_party + ":" + adr.GetDataByQtnAdrRef(app_party)[0].Par_Adr_Qtn_Name.ToString();

                qr = qdet.GetDataByQtnRefParty(dr.Pr_Det_Quot_Ref, dr.Pr_Det_App_Party)[0];

                dtgrd.Rows.Add(cnt.ToString(), app_party + ":" + adr.GetDataByQtnAdrRef(app_party)[0].Par_Adr_Qtn_Name.ToString(), dr.Pr_Det_Ref.ToString(), dr.Pr_Det_Icode.ToString() + ": " + dr.Pr_Det_Itm_Desc.ToString(), dr.Pr_Det_Lin_Qty.ToString("N2") + " " + dr.Pr_Det_Itm_Uom.ToString(), dr.Pr_Det_Bal_Qty.ToString("N2") + " " + dr.Pr_Det_Itm_Uom.ToString(), qty.ToString("N2") + " " + dr.Pr_Det_Itm_Uom.ToString(), rate.ToString("N2"), tot.ToString("N2"));

                tac_ref = qr.Qtn_Gen_Terms;

                dtlog = log.GetDataByRef(tac_ref);

                foreach (dsProcTran.tbl_TaC_LogRow drlog in dtlog.Rows)
                {
                    vdays = drlog.TaC_Valid_Days;
                    switch (drlog.TaC_Type)
                    {
                        case "GEN":
                            {
                                lst = new ListItem();
                                lst.Value = drlog.TaC_Tem_Seq_No.ToString() + ":" + drlog.TaC_Log_Id.ToString();
                                lst.Text = drlog.TaC_Tem_Seq_No.ToString() + "." + cnt.ToString() + ". " + drlog.TaC_Content_Det.ToString() + "</br>";
                                rdogen[drlog.TaC_Tem_Seq_No].Items.Add(lst);
                                break;
                            }

                        case "SPE":
                            {
                                lst = new ListItem();
                                lst.Value = drlog.TaC_Tem_Seq_No.ToString() + ":" + drlog.TaC_Log_Id.ToString();
                                lst.Text = drlog.TaC_Tem_Seq_No.ToString() + "." + cnt.ToString() + ". " + drlog.TaC_Content_Det.ToString() + "</br>";
                                rdospe[drlog.TaC_Tem_Seq_No].Items.Add(lst);
                                break;
                            }

                        case "PAY":
                            {
                                lst = new ListItem();
                                lst.Value = drlog.TaC_Tem_Seq_No.ToString() + ":" + drlog.TaC_Log_Id.ToString();
                                lst.Text = drlog.TaC_Tem_Seq_No.ToString() + "." + cnt.ToString() + ". " + drlog.TaC_Content_Det.ToString() + "</br>";
                                rdopay[drlog.TaC_Tem_Seq_No].Items.Add(lst);
                                break;
                            }
                    }
                }

                //add valid days items

                itm = new ListItem();
                itm.Value = vdays.ToString();
                itm.Text = vdays.ToString() + " Days";
                rdovaliddays.Items.Add(itm);

            skip_item: ;

            }

            icnt = rdogen.Length;
            for (i = 0; i < icnt; i++)
            {
                if (rdogen[i] != null)
                    if (rdogen[i].Items.Count > 0)
                    {
                        rdogen[i].SelectedIndex = 0;
                        rdogen[i].ID = "gen_" + i.ToString();
                        celgen.Controls.Add(rdogen[i]);
                        lblseperator = new Label();
                        lblseperator.Text = "----------------------------------------------------------------------------";
                        celgen.Controls.Add(lblseperator);
                    }
            }

            icnt = rdospe.Length;
            for (i = 0; i < icnt; i++)
            {
                if (rdospe[i] != null)
                    if (rdospe[i].Items.Count > 0)
                    {
                        rdospe[i].SelectedIndex = 0;
                        rdospe[i].ID = "spe_" + i.ToString();
                        celspe.Controls.Add(rdospe[i]);
                        lblseperator = new Label();
                        lblseperator.Text = "----------------------------------------------------------------------------";
                        celspe.Controls.Add(lblseperator);
                    }
            }

            icnt = rdopay.Length;
            for (i = 0; i < icnt; i++)
            {
                if (rdopay[i] != null)
                    if (rdopay[i].Items.Count > 0)
                    {
                        rdopay[i].SelectedIndex = 0;
                        rdopay[i].ID = "pay_" + i.ToString();
                        celpay.Controls.Add(rdopay[i]);
                        lblseperator = new Label();
                        lblseperator.Text = "----------------------------------------------------------------------------";
                        celpay.Controls.Add(lblseperator);
                    }
            }

            if (rdovaliddays.Items.Count > 0) rdovaliddays.SelectedIndex = 0;

            if (gtot == 0)
                btncreate.Visible = false;
            else
                btncreate.Visible = true;

            txtamount.Text = gtot.ToString("N2");
            lblinword.Text = NumToText.changeNumericToWords(gtot.ToString());

            gdItem.DataSource = dtgrd;
            gdItem.DataBind();
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            Response.Redirect("./frmPoInit.aspx");
        }

        protected void btncreate_Click(object sender, EventArgs e)
        {            
            GlobalClass.clsMpo[] seldet = (GlobalClass.clsMpo[])Session["sessionItemSelForPO"];

            tbl_PuTr_Pr_DetTableAdapter prdet = new tbl_PuTr_Pr_DetTableAdapter();
            dsProcTran.tbl_PuTr_Pr_DetRow[] prdr = new dsProcTran.tbl_PuTr_Pr_DetRow[seldet.Length];

            //InSu_Trn_SetTableAdapter scfset = new InSu_Trn_SetTableAdapter();

            tbl_PuMa_Par_Adr_QtnTableAdapter adr = new tbl_PuMa_Par_Adr_QtnTableAdapter();

            tbl_PuMa_Par_AdrTableAdapter paradr = new tbl_PuMa_Par_AdrTableAdapter();
            tbl_PuMa_Par_AccTableAdapter paracc = new tbl_PuMa_Par_AccTableAdapter();
            tbl_Acc_Fa_Gl_CoaTableAdapter glcoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();

            tbl_PuTr_PO_DetTableAdapter podet = new tbl_PuTr_PO_DetTableAdapter();
            tbl_PuTr_PO_HdrTableAdapter pohdr = new tbl_PuTr_PO_HdrTableAdapter();

            tbl_TaC_MasTableAdapter tac = new tbl_TaC_MasTableAdapter();
            tbl_TaC_LogTableAdapter taclog = new tbl_TaC_LogTableAdapter();
            dsProcTran.tbl_TaC_LogRow taclogdr;

            tbl_Acc_Chq_VoucherTableAdapter chqv = new tbl_Acc_Chq_VoucherTableAdapter();

            tbl_Qtn_DetTableAdapter qtndet = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetRow qr;

            tbl_Tran_ComTableAdapter comm = new tbl_Tran_ComTableAdapter();

            var taEmp = new View_Emp_BascTableAdapter();

            bool flg = true;
            int lno, i, tcnt, tem_seq, entry_seq, arraylen;
            string[] tmp;

            var empId = "";
            var empName="";
            var empDesig = "";
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt32(empRef));
            if (dtEmp.Rows.Count > 0)
            {
                empId = dtEmp[0].EmpId.ToString();
                empName = dtEmp[0].EmpName.ToString();
                empDesig = dtEmp[0].DesigName.ToString();
            }
            
            string period = DateTime.Now.Year.ToString() + "/" + string.Format("{0:00}", DateTime.Now.Month);
            decimal itmp, atot, dum, rate, linqty, orgqty, balqty;
            tmp = txtparty.Text.Split(':');

            int daycnt = Convert.ToInt32(rdovaliddays.SelectedValue.ToString());
            string cash_type = txtreqtype.Text;
            string pur_type = txtpurchasetype.Text;
            string app_party = tmp[0].ToString();
            string app_party_name = tmp[1].ToString();
            string app_party_acc_code = "";
            //string app_party_acc_code = adr.GetDataBySupAdrRef(app_party)[0].Par_Adr_Acc_Code.ToString();

            //if (app_party_acc_code == "") return;

            string cid, selval, selqref, newstatus, remarks;
            string pendingfor;
            string template_id;
            string ocflg, prefx;
            string pay_term_flg = "";
            RadioButtonList rdogen;
            RadioButtonList rdospe;
            RadioButtonList rdopay;

            prefx = "LPO-"; //"LPO" + txtreqtype.Text.Substring(0, 2);

            var dtmaxporef = pohdr.GetMaxPoRef("LPO", DateTime.Now.Year);
            string last_num = dtmaxporef == null ? "1" : (Convert.ToInt32(dtmaxporef)+1).ToString();            
            string scf_ref = prefx + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(last_num).ToString("000000");

            string next_num = string.Format("{0:000000}", Convert.ToInt32(last_num) + 1);

            arraylen = seldet.Length;

            for (i = 0; i < arraylen; i++)
            {
                if (seldet[i] != null)
                    if (seldet[i].RefNo != null)
                    {
                        prdr[i] = prdet.GetDataByPrRefItem(seldet[i].RefNo, seldet[i].Icode)[0];
                    }
            }

            atot = 0;
            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(prdet.Connection);

            try
            {
                adr.AttachTransaction(myTrn);
                paracc.AttachTransaction(myTrn);
                paradr.AttachTransaction(myTrn);
                glcoa.AttachTransaction(myTrn);
                prdet.AttachTransaction(myTrn);
                pohdr.AttachTransaction(myTrn);
                podet.AttachTransaction(myTrn);                
                taclog.AttachTransaction(myTrn);
                comm.AttachTransaction(myTrn);

                var dtParAdr = paradr.GetDataBySupAdrRef(app_party);
                if (dtParAdr.Rows.Count > 0)
                {
                    app_party = dtParAdr[0].Par_Adr_Ref.ToString();
                    app_party_name = dtParAdr[0].Par_Adr_Name.ToString();
                    app_party_acc_code = dtParAdr[0].Par_Adr_Acc_Code.ToString();
                }
                else
                {                    
                    var dtParAdrQtn = adr.GetDataByQtnAdrRef(app_party);
                    if (dtParAdrQtn.Rows.Count > 0)
                    {
                        //myTrn.Rollback();
                        //tblMsg.Rows[0].Cells[0].InnerText = "Supplier already exists with this name.";
                        //tblMsg.Rows[1].Cells[0].InnerText = "";
                        //ModalPopupExtenderMsg.Show();
                        //return;

                        app_party_name = dtParAdrQtn[0].Par_Adr_Qtn_Name.ToString();

                        #region Insert Supplier
                        var dtMaxAccRef = paracc.GetMaxAccRef();
                        var nextAccRef = dtMaxAccRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                        var nextAccRefNo = "SUP-ACC-" + nextAccRef.ToString();

                        paracc.InsertSupAcc(nextAccRef, nextAccRefNo, dtParAdrQtn[0].Par_Adr_Qtn_Name.ToString(), "", dtParAdrQtn[0].Par_Adr_Qtn_Type.ToString(), "", "",
                            DateTime.Now, "N", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", 0, 0, 0, 0, 0, 0, "", "BDT",
                            "", "", "", "", "", "", "", 0, "", "", "", "", "", "", "");

                        var dtMaxCoaRef = glcoa.GetMaxCoaRef();
                        var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                        var dtMaxCoaCode = glcoa.GetMaxCoaCode();
                        var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                        var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");
                        app_party_acc_code = nextCoaCode;

                        glcoa.InsertCoa(nextCoaRef, nextCoaCode, dtParAdrQtn[0].Par_Adr_Qtn_Name.ToString(), nextCoaCode, "S", "B", "N", DateTime.Now, "N", "N", "BDT",
                            DateTime.Now, "Y", "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        var dtMaxAdrRef = paradr.GetMaxAdrRef();
                        var nextAdrRef = dtMaxAdrRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAdrRef) + 1).ToString();
                        var nextAdrRefNo = "SUP-" + nextAdrRef.ToString();
                        app_party = nextAdrRef;

                        paradr.InsertSupAdr(nextAdrRef, nextAdrRefNo, dtParAdrQtn[0].Par_Adr_Qtn_Name.ToString(), Convert.ToInt32(dtParAdrQtn[0].Par_Adr_Qtn_Type),
                            nextAccRef.ToString(), dtParAdrQtn[0].Par_Adr_Qtn_Addr.ToString(), dtParAdrQtn[0].Par_Adr_Qtn_Cont_Per.ToString(),
                            dtParAdrQtn[0].Par_Adr_Qtn_Desig.ToString(), dtParAdrQtn[0].Par_Adr_Qtn_Cell_No, dtParAdrQtn[0].Par_Adr_Qtn_Tel_No.ToString(),
                            dtParAdrQtn[0].Par_Adr_Qtn_Fax_No.ToString(), dtParAdrQtn[0].Par_Adr_Qtn_Email_Id.ToString(),
                            "", "", "", nextCoaCode, "", "", DateTime.Now, 0, 0, "", "", "", 0, "", "", "", "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");
                        #endregion

                        qtndet.UpdateQtnTempAdrRef(app_party, dtParAdrQtn[0].Par_Adr_Qtn_Ref.ToString());

                        prdet.UpdatePrDetTempAppParty(app_party, dtParAdrQtn[0].Par_Adr_Qtn_Ref.ToString());

                        adr.UpdateQtnAdrTempRef(nextAdrRef, nextAdrRefNo, dtParAdrQtn[0].Par_Adr_Qtn_Ref.ToString());
                    }
                    else
                    {
                        myTrn.Rollback();
                        tblPopupMsg.Rows[0].Cells[0].InnerText = "Invalid QTN Supplier Data.";
                        tblPopupMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                lno = 0;

                for (i = 0; i < arraylen; i++)
                {
                    if (seldet[i] == null) goto skip_item;
                    if (seldet[i].RefNo == null) goto skip_item;

                    if (prdr[i].Pr_Det_Status != "APP") goto skip_item;
                    if (Convert.ToDecimal(prdr[i].Pr_Det_Bal_Qty) < seldet[i].Qnty) goto skip_item;

                    lno = lno + 1;

                    rate = Convert.ToDecimal(prdr[i].Pr_Det_Lin_Rat);
                    itmp = seldet[i].Qnty;

                    dum = rate * itmp;

                    if (dum == 0) { flg = false; goto Error_Hndlr; }
                    atot = atot + dum;

                    if (Convert.ToDecimal(prdr[i].Pr_Det_Bal_Qty) == seldet[i].Qnty)
                        newstatus = "POC";
                    else
                        newstatus = "APP";


                    if (prdr[i].Pr_Det_T_C1 == "")
                        remarks = scf_ref + "," + seldet[i].Qnty.ToString();
                    else
                        remarks = prdr[i].Pr_Det_T_C1 + ":" + scf_ref + "," + seldet[i].Qnty.ToString();

                    linqty = Convert.ToDecimal(prdr[i].Pr_Det_Lin_Qty);
                    orgqty = Convert.ToDecimal(prdr[i].Pr_Det_Org_QTY) + seldet[i].Qnty;
                    balqty = Convert.ToDecimal(prdr[i].Pr_Det_Bal_Qty) - seldet[i].Qnty;

                    //update scf qnty
                    if (balqty == 0) ocflg = "C"; else ocflg = "O";
                    //prdet.UpdateForPoCreate((double)balqty, (double)balqty, ocflg, "Y", prdr[i].Pr_Det_Ref, prdr[i].Pr_Det_Icode);
                    prdet.UpdateForPoCreate((double)linqty, (double)balqty, ocflg, "Y", prdr[i].Pr_Det_Ref, prdr[i].Pr_Det_Icode);

                    prdet.UpdateForPoCreate2(newstatus, (double)orgqty, (double)balqty, remarks, seldet[i].RefNo, prdr[i].Pr_Det_Icode);
                    qr = qtndet.GetDataByQtnRefParty(prdr[i].Pr_Det_Quot_Ref, app_party)[0];

                    podet.InsertPoDet("PO", pur_type, scf_ref, (short)lno, "", 0, prdr[i].Pr_Det_Icode, prdr[i].Pr_Det_Itm_Desc, prdr[i].Pr_Det_Itm_Uom,
                                        prdr[i].Pr_Det_Str_Code, prdr[i].Pr_Det_Bin_Code, prdr[i].Pr_Det_Ref, prdr[i].Pr_Det_Lno, prdr[i].Pr_Det_Quot_Ref, DateTime.Now, DateTime.Now,
                                        (double)itmp, 0, (double)itmp, 0, "O", "N", rate, dum, dum, "Post. By:- " + empName + " @" + DateTime.Now.ToString(), "", "P", 1);

                    var dtCom = comm.GetDataByRefSeqNo(prdr[i].Pr_Det_Quot_Ref, 4);
                    if (dtCom.Rows.Count <= 0)
                        comm.InsertTranCom(prdr[i].Pr_Det_Quot_Ref, 4, DateTime.Now, empId, empName, empDesig, 1, "PO", "APP", "(P/O Created against Party: " + app_party_name + ")", "", "1", "", "", "", "");

                skip_item: ;
                }

                if (atot == 0) { flg = false; goto Error_Hndlr; }

                template_id = "";
                pendingfor = "";

                pohdr.InsertPoHdr("PO", pur_type, scf_ref, app_party, app_party, app_party_acc_code, DateTime.Now, "", "",
                                    "", "", "", "", "", "", "", app_party_name, atot, "P", DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"), empRef.ToString(),
                                    DateTime.Now, "", "", "", DateTime.Now, DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"), "", "", "", 1, "BDT", 0);

                //scfset.UpdateNextNum_PO(next_num, "PO", prefx);

                #region insert terms and conditions
                //gen         
                tcnt = celgen.Controls.Count;
                entry_seq = 0;
                for (i = 0; i < 20; i++)
                {
                    cid = "gen_" + i.ToString();
                    rdogen = new RadioButtonList();
                    rdogen = (RadioButtonList)celgen.FindControl(cid);

                    if (rdogen != null)
                        if (rdogen.Items.Count > 0)
                        {
                            entry_seq++;
                            selval = rdogen.SelectedValue.ToString();
                            tmp = selval.Split(':');
                            tem_seq = Convert.ToInt32(tmp[0].ToString());
                            selqref = tmp[1].ToString();
                            taclogdr = taclog.GetDataByIdTypeSeq(selqref, "GEN", tem_seq)[0];
                            taclog.InsertTacLog(scf_ref, "PO", taclogdr.TaC_Type, taclogdr.TaC_Pay_Type, tem_seq, entry_seq, taclogdr.TaC_Content_Det, daycnt, taclogdr.TaC_Remarks);
                        }
                }

                //spe
                tcnt = celspe.Controls.Count;
                entry_seq = 0;
                for (i = 0; i < 20; i++)
                {
                    cid = "spe_" + i.ToString();
                    rdospe = new RadioButtonList();
                    rdospe = (RadioButtonList)celspe.FindControl(cid);

                    if (rdospe != null)
                        if (rdospe.Items.Count > 0)
                        {
                            entry_seq++;
                            selval = rdospe.SelectedValue.ToString();
                            tmp = selval.Split(':');
                            tem_seq = Convert.ToInt32(tmp[0].ToString());
                            selqref = tmp[1].ToString();
                            taclogdr = taclog.GetDataByIdTypeSeq(selqref, "SPE", tem_seq)[0];
                            taclog.InsertTacLog(scf_ref, "PO", taclogdr.TaC_Type, taclogdr.TaC_Pay_Type, tem_seq, entry_seq, taclogdr.TaC_Content_Det, daycnt, taclogdr.TaC_Remarks);
                        }
                }

                //pay
                tcnt = celpay.Controls.Count;
                entry_seq = 0;
                for (i = 0; i < 20; i++)
                {
                    cid = "pay_" + i.ToString();
                    rdopay = new RadioButtonList();
                    rdopay = (RadioButtonList)celpay.FindControl(cid);

                    if (rdopay != null)
                        if (rdopay.Items.Count > 0)
                        {
                            entry_seq++;
                            selval = rdopay.SelectedValue.ToString();
                            tmp = selval.Split(':');
                            tem_seq = Convert.ToInt32(tmp[0].ToString());
                            selqref = tmp[1].ToString();
                            taclogdr = taclog.GetDataByIdTypeSeq(selqref, "PAY", tem_seq)[0];
                            taclog.InsertTacLog(scf_ref, "PO", taclogdr.TaC_Type, taclogdr.TaC_Pay_Type, tem_seq, entry_seq, taclogdr.TaC_Content_Det, daycnt, taclogdr.TaC_Remarks);
                            pay_term_flg = taclogdr.TaC_Pay_Type.ToString();
                        }
                }
                #endregion                

                // insert chq voucher
                if ((pay_term_flg == "Full") || (pay_term_flg == "Part"))
                {
                    chqv.AttachTransaction(myTrn);
                    chqv.InsertCheqVoucher(scf_ref, app_party_acc_code, app_party_name, pay_term_flg, "INI", atot, 0, "", "", DateTime.Now, "");
                }

            Error_Hndlr:

                if (flg)
                {
                    myTrn.Commit();
                }
                else
                {
                    myTrn.Rollback();
                    tblPopupMsg.Rows[0].Cells[0].InnerText = "Data processing error.";
                    tblPopupMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch(Exception ex)
            {
                flg = false;
                myTrn.Rollback();
                tblPopupMsg.Rows[0].Cells[0].InnerText = "Data processing error.";
                tblPopupMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                ModalPopupExtenderMsg.Show();
            }
            finally
            {
                GlobalClass.clsDbHelper.CloseTransaction(prdet.Connection, myTrn);
            }

            if (flg)
            {
                Session["sessionItemSelForPO"] = null;
                lblporef.Text = scf_ref;
                ModalPopupExtender5.Show();
            }
            else
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            Response.Redirect("./frmPoSend.aspx?PoRefNo=" + lblporef.Text);
        }
    }
}