using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmSalaryCalc : System.Web.UI.Page
    {
        //CONTAINS ALL FORMULA CODES DEFINED IN FORMULA MASTER
        string[] Code = null;

        //CONTAINS VALUES FOR ALL FORMULA CODES
        double[] Values = null;

        //FLAG TO CHECK VALUE FOR A FORMULA ALREADY CALCULATED
        bool[] Flag = null;

        //TOTAL PERKS 
        double Perks = 0;

        //TOTAL DEDUCTIONS
        double Deds = 0;
        
        //CONTAINS EMPLOYEE TYPE  
        string emp_type = "";

        //CONTAINS ALL FORMULA NAMES DEFINED IN FORMULA MASTER
        string[] CodeName = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;          

            txtSalDate.Text = DateTime.Now.ToString("MMMM/yyyy");

            var taLoc = new tblHrmsOffLocTableAdapter();
            cboLoc.DataSource = taLoc.GetDataByAsc();
            cboLoc.DataValueField = "LocRefNo";
            cboLoc.DataTextField = "LocName";
            cboLoc.DataBind();
            cboLoc.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));            
        }

        protected void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoc.SelectedIndex != 0)
            {
                cboDept.Items.Clear();
                cboSec.Items.Clear();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taDep = new tblHrmsDeptTableAdapter();
                cboDept.DataSource = taDep.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                cboDept.DataValueField = "DeptRefNo";
                cboDept.DataTextField = "DeptName";
                cboDept.DataBind();
                cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            else
            {
                cboDept.Items.Clear();
                cboSec.Items.Clear();
                cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
        }

        protected void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDept.SelectedIndex != 0)
            {
                cboSec.Items.Clear();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taSec = new tblHrmsSecTableAdapter();
                cboSec.DataSource = taSec.GetDataByDeptRef(cboDept.SelectedValue.ToString());
                cboSec.DataValueField = "SecRefNo";
                cboSec.DataTextField = "SecName";
                cboSec.DataBind();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            else
            {
                cboSec.Items.Clear();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            cboLoc.SelectedIndex = 0;
            cboDept.Items.Clear();
            cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
            cboSec.Items.Clear();
            cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));

            txtSalDate.Text = DateTime.Now.ToString("MMMM/yyyy");

            gvSalHdr.DataSource = null;
            gvSalHdr.DataBind();
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            var taEmpView = new View_Emp_BascTableAdapter();
            var taGrdDetView = new View_Grade_DetTableAdapter();
            var taEmpPaySet = new tblHrmsEmpPaySetTableAdapter();
            var taPayHead = new View_Pay_HeadTableAdapter();
            var taSalCal = new tblHrmsTrialSalaryTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalCal.Connection);

            DateTime StDate = DateTime.Now;
            DateTime EnDate = DateTime.Now;

            StDate = Convert.ToDateTime("01/" + Convert.ToDateTime(txtSalDate.Text).Month + "/" + Convert.ToDateTime(txtSalDate.Text).Year);
            var DaysInMonth = DateTime.DaysInMonth(StDate.Year, StDate.Month);
            EnDate = Convert.ToDateTime(DaysInMonth + "/" + Convert.ToDateTime(txtSalDate.Text).Month + "/" + Convert.ToDateTime(txtSalDate.Text).Year);

            int i = 0;
            var GradeRef = "";
            var Forstr = "";
            var EmpCnt = 1;
            var Fcnt = 0;
            var Cnt = 0;
            double Ctemp = 0;
            int Pos = 0;
            int TempCnt = 0;

            string[] EmpForCodes = null;
            bool[] CodeShowFlag = null;

            var dtEmpWiew = taEmpView.GetData();

            try
            {
                taSalCal.AttachTransaction(myTran);

                foreach (dsEmpDet.View_Emp_BascRow dr in dtEmpWiew.Rows)
                {
                    Forstr = "";

                    #region To get grade attached with the employee
                    GradeRef = dr.EmpGrade.ToString();
                    #endregion

                    #region TO GET CODES DEFINED IN GRADE DEFINITION
                    var dtGrdDetView = taGrdDetView.GetDataByGrdRef(GradeRef.ToString());
                    foreach (dsHrmsMas.View_Grade_DetRow grdRow in dtGrdDetView.Rows)
                    {
                        Forstr = Forstr + "," + grdRow.PayHeadRef.ToString();
                        //Need to split
                    }
                    if (Forstr.Length > 0) Forstr = Forstr.Substring(1, Forstr.Length - 1);
                    Forstr = "(" + Forstr + ")";
                    Fcnt = dtGrdDetView.Rows.Count;
                    #endregion

                    if (Fcnt <= 0) continue;

                    string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRN"].ToString();
                    SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr);

                    #region TO GET CODES ASSIGNED TO EMPLOYEE AND NOT IN GRADE DEFINITION
                    SqlCommand cmd = new System.Data.SqlClient.SqlCommand("Select DISTINCT PaySetPayHeadRef from" + " tblHrmsEmpPaySet where PaySetEmpRef = '" + dr.EmpRefNo + "' and PaySetPayHeadRef not in " + Forstr, conn);
                    conn.Open();
                    int numberOfRecords = cmd.ExecuteNonQuery();
                    Fcnt = Fcnt + numberOfRecords;
                    conn.Close();
                    #endregion

                    //TOTAL CODES ASSIGNED TO THE EMPLOYEE
                    EmpForCodes = new string[Fcnt + 1];
                    //Boolean variable to check the code to be shown in payroll
                    CodeShowFlag = new bool[Fcnt + 1];

                    #region TO GET CODE SHOW FLAG IN GRADE DEFINITION
                    i = 0;
                    var dtGrdDetView1 = taGrdDetView.GetDataByGrdRef(GradeRef.ToString());
                    foreach (dsHrmsMas.View_Grade_DetRow grdRow in dtGrdDetView1.Rows)
                    {
                        EmpForCodes[i] = grdRow.PayHeadRef;

                        var dtEmpPaySet = taEmpPaySet.GetDataByPayHeadRef(dr.EmpRefNo.ToString(), grdRow.PayHeadRef.ToString());
                        if (dtEmpPaySet.Rows.Count > 0)
                            CodeShowFlag[i] = dtEmpPaySet[0].PaySetShowFlag.ToString() == "T" ? true : false;
                        else
                            CodeShowFlag[i] = grdRow.GrdDetShowFlg.ToString() == "T" ? true : false;
                        i++;
                    }
                    #endregion

                    #region TO GET CODE SHOW FLAG NOT DEFINED IN GRADE DEFINITION
                    SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("Select DISTINCT PaySetPayHeadRef from" + " tblHrmsEmpPaySet where PaySetEmpRef = '" + dr.EmpRefNo + "' and PaySetPayHeadRef not in " + Forstr, conn);
                    conn.Open();
                    SqlDataReader rdr = cmd1.ExecuteReader();
                    while (rdr.Read())
                    {
                        EmpForCodes[i] = rdr["PaySetPayHeadRef"].ToString();
                        CodeShowFlag[i] = rdr["PaySetShowFlag"].ToString() == "T" ? true : false;
                    }
                    conn.Close();
                    #endregion

                    #region GET ALL FORMULA CODES AND ASSIGN IN ARRAY
                    //Get all formula codes
                    var dtPayHead = taPayHead.GetData();

                    //contains all formula code names
                    Code = new string[dtPayHead.Rows.Count];
                    //stores values for all formula codes
                    Values = new double[dtPayHead.Rows.Count];
                    //flag to check value is already calculated or not
                    Flag = new bool[dtPayHead.Rows.Count];
                    //flag to check value is already calculated or not
                    CodeName = new string[dtPayHead.Rows.Count];

                    Cnt = 0;
                    foreach (dsHrmsMas.View_Pay_HeadRow headRow in dtPayHead.Rows)
                    {
                        Code[Cnt] = headRow.PayHeadRef.ToString();
                        Flag[Cnt] = false;
                        CodeName[Cnt] = headRow.PayHeadName.ToString();
                        Cnt = Cnt + 1;
                    }
                    #endregion

                    //'''''' TO GET VALUE FOR STANDARD CODES LIKE ATTNDAYS,TOTDAYS ETC..            
                    GetStdValues(dr.EmpRefNo.ToString(), StDate.Date);   //to get values for standard codes which cannot be calculated using formula

                    #region GET ALL FORMULA VALUES THAT ASSIGNED IN EMPLOYEE
                    Cnt = 0;
                    Perks = 0;
                    Deds = 0;

                    for (TempCnt = 0; TempCnt <= Fcnt; TempCnt++)
                    {
                        var dtGetPayHead = taPayHead.GetDataByRef(Convert.ToInt32(EmpForCodes[TempCnt]));
                        var payCode = dtGetPayHead.Rows.Count > 0 ? dtGetPayHead[0].PayHeadCode : "";
                        //CODES NET ,Earns,Deducs ETC NOT CALCULATED USING FORMULA
                        //if (EmpForCodes[TempCnt] == "NET")
                        if (payCode == "NETAMT")
                        {
                            //goto NextStep;
                            //if NET then no processing,goto next Formula code
                            continue;
                        }
                        //else if (EmpForCodes[TempCnt] == "EARNS")
                        if (payCode == "EARNS")
                        {
                            Ctemp = Perks;
                        }
                        //else if (EmpForCodes[TempCnt] == "DEDUCS")
                        if (payCode == "DEDUCS")
                        {
                            Ctemp = Deds;
                        }
                        else
                        {
                            ///'TO GET USER DEFINED FORMULA VALUES
                            Ctemp = GetVal(EmpForCodes[TempCnt].ToString(), dr.EmpRefNo.ToString(), GradeRef.ToString());

                            // to get value using formula
                            for (i = 0; i <= Code.Length - 1; i++)
                            {
                                if (Code[i] == EmpForCodes[TempCnt])
                                {
                                    Values[i] = Ctemp;
                                    Flag[i] = true;
                                    break;
                                }
                            }
                        }

                        //Adding to accumulation codes  like NET etc. 
                        //All '+' will be added to Earns
                        //All '-' will be added to Deducs
                        //if (CodeShowFlag[TempCnt] == true)
                        //{
                        var dtPayHead1 = taPayHead.GetDataByRef(Convert.ToInt32(EmpForCodes[TempCnt]));
                        for (Pos = 0; Pos <= Code.Length - 1; Pos++)
                        {
                            if (Code[Pos] == EmpForCodes[TempCnt])
                            {
                                break;
                            }
                        }
                        if (dtPayHead1.Rows.Count > 0)
                        {
                            if (dtPayHead1[0].PayHeadAcc != "")
                            {
                                for (int Ant = 0; Ant <= Code.Length - 1; Ant++)
                                {
                                    if (dtPayHead1[0].PayHeadAccRef == Code[Ant])
                                    {
                                        switch (dtPayHead1[0].PayHeadAccOpr)
                                        {
                                            case "+":
                                                Values[Ant] = Values[Ant] + Values[Pos];
                                                Perks = Perks + Values[Pos];
                                                break;
                                            case "-":
                                                Values[Ant] = Values[Ant] - Values[Pos];
                                                Deds = Deds + Values[Pos];
                                                break;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    //}
                    //NextStep:
                    //    Cnt = Cnt + 1;
                    }
                    #endregion

                    #region Adding perks and deductions
                    for (i = 0; i <= Code.Length - 1; i++)
                    {
                        var dtGetPayHead = taPayHead.GetDataByRef(Convert.ToInt32(Code[i]));
                        var payCode = dtGetPayHead.Rows.Count > 0 ? dtGetPayHead[0].PayHeadCode : "";
                        //if (Code[i] == "EARNS")
                        if (payCode == "EARNS")
                        {
                            Values[i] = Perks;
                        }
                        //else if (Code[i] == "DEDUCS")
                        else if (payCode == "DEDUCS")
                        {
                            Values[i] = Deds;
                        }
                    }
                    #endregion

                    taSalCal.DeleteSalaryByEmpRef(dr.EmpRefNo.ToString(), Convert.ToDecimal(Convert.ToDateTime(txtSalDate.Text.Trim()).Month), Convert.ToDecimal(Convert.ToDateTime(txtSalDate.Text.Trim()).Year));

                    for (TempCnt = 0; TempCnt <= Fcnt; TempCnt++)
                    {
                        for (i = 0; i <= Code.Length - 1; i++)
                        {
                            if (EmpForCodes[TempCnt] == Code[i])
                            {
                                taSalCal.InsertSalary(dr.EmpRefNo.ToString(), Convert.ToDateTime(txtSalDate.Text.Trim()), Code[i].ToString(), CodeName[i].ToString(),
                                    Convert.ToDecimal(Values[i]), CodeShowFlag[TempCnt] == true ? "T" : "F", dr.EmpGrade, "N",
                                    Convert.ToDateTime(txtSalDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtSalDate.Text.Trim()).Year,
                                    "", "", "", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");
                            }
                        }
                    }

                    taSalCal.InsertSalary(dr.EmpRefNo.ToString(), Convert.ToDateTime(txtSalDate.Text.Trim()), "100001", "Total Earnings",
                                    Convert.ToDecimal(Perks), "T", dr.EmpGrade, "N",
                                    Convert.ToDateTime(txtSalDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtSalDate.Text.Trim()).Year,
                                    "", "", "", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    taSalCal.InsertSalary(dr.EmpRefNo.ToString(), Convert.ToDateTime(txtSalDate.Text.Trim()), "100002", "Total Deductions",
                                    Convert.ToDecimal(Deds), "T", dr.EmpGrade, "N",
                                    Convert.ToDateTime(txtSalDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtSalDate.Text.Trim()).Year,
                                    "", "", "", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    taSalCal.InsertSalary(dr.EmpRefNo.ToString(), Convert.ToDateTime(txtSalDate.Text.Trim()), "100003", "Net Payable Amount",
                                    Convert.ToDecimal(Perks - Deds), "T", dr.EmpGrade, "N",
                                    Convert.ToDateTime(txtSalDate.Text.Trim()).ToString("MM") + "/" + Convert.ToDateTime(txtSalDate.Text.Trim()).Year,
                                    "", "", "", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");
                }
                myTran.Commit();

                tblMsg.Rows[0].Cells[0].InnerText = "Salary Calculated Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            var taEmpMas = new View_Emp_BascTableAdapter();
            var dtEmpMas = new dsEmpDet.View_Emp_BascDataTable();

            #region Load Emp List
            if (cboLoc.SelectedIndex == 0)
                //---ALL Location
                dtEmpMas = taEmpMas.GetData();
            else
                if (cboDept.SelectedIndex == 0)
                    //---Selected Location
                    dtEmpMas = taEmpMas.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                else
                    if (cboSec.SelectedIndex == 0)
                        //---Selected Department
                        dtEmpMas = taEmpMas.GetDataByLocDept(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString());
                    else
                        //---Selected Section
                        dtEmpMas = taEmpMas.GetDataByLocDeptSec(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString());

            gvSalHdr.DataSource = dtEmpMas;
            gvSalHdr.DataBind();
            #endregion
        }

        #region GetStdVal
        private void GetStdValues(string EmpCode, System.DateTime dt)
        {
            //THIS PROCEDURE IS USED TO CALCULATE VALUES FOR ALL STANDARD CODES
            
            //STANDARD CODES LIST
            //Some codes are given standard for use in calculations.this cannot be get using formula master.
            //codes and description:
            //1  ATNDYS - Total days present in the month (ie. marked in attendence)
            //2  ABSDYS - Total Absent Days in the month.
            //3  LEVDYS - Total leaves taken in the month(ie. entered in leave details ,includes all leave types)
            //3  HCTDYS - Total Half Days leaves taken in the month(ie. entered in leave details ,includes all leave types)
            //4  HOLDYS - Total no of holidays in the present month(ie. declared in holiday master)            
            //5  PRSDYS - Total days present (ie.can be used for canteen deductions etc)
            //6  WRKDYS - Total Working Day
            //7  TOTDYS - Total days in month
            //8  GRSSAL - Gross Salary 
            ///''''''''

            DateTime StDate = DateTime.Now;
            DateTime EnDate = DateTime.Now;
            string empType = "";

            int Vcnt = 0;

            var taGetEmpRec = new View_Emp_BascTableAdapter();
            var taGetAtnd = new tblHrmsEmpDayAttndTableAdapter();
            var taGetHolDay = new tblHrmsHolidayTableAdapter();
            var taPayHead = new View_Pay_HeadTableAdapter();

            var dtGetEmpRec = taGetEmpRec.GetDataByEmpRef(Convert.ToInt32(EmpCode));
            empType = dtGetEmpRec.Rows.Count > 0 ? (dtGetEmpRec[0].IsEmpTypeNull() ? "" : dtGetEmpRec[0].EmpType) : "";

            StDate = Convert.ToDateTime("01/" + Convert.ToDateTime(dt).Month + "/" + Convert.ToDateTime(dt).Year);
            var DaysInMonth = DateTime.DaysInMonth(StDate.Year, StDate.Month);
            EnDate = Convert.ToDateTime(DaysInMonth + "/" + Convert.ToDateTime(txtSalDate.Text).Month + "/" + Convert.ToDateTime(txtSalDate.Text).Year);

            for (Vcnt = 0; Vcnt <= Code.Length - 1; Vcnt++)
            {
                var dtPayHead = taPayHead.GetDataByRef(Convert.ToInt32(Code[Vcnt]));
                var payCode = dtPayHead.Rows.Count > 0 ? dtPayHead[0].PayHeadCode : "";

                //switch (Code[Vcnt])
                switch (payCode)
                {
                    case "ATNDYS":
                        //1.ATNDYS - Total days present in the month (ie. marked in attendence)                        
                        var dtAtnd = taGetAtnd.GetTotAttndDays(EmpCode, dt.Year, dt.Month);
                        Values[Vcnt] = dtAtnd == null ? 0 : Convert.ToDouble(dtAtnd);
                        Flag[Vcnt] = true;
                        break;

                    case "ABSDYS":
                        //2.ABSDYS - Total Absent Days in the month.
                        double absDays = 0;

                        //ATNDDAYS
                        double TotAtnDays = 0;
                        var dtAtnDys = taGetAtnd.GetTotAttndDays(EmpCode, dt.Year, dt.Month);
                        TotAtnDays = dtAtnDys == null ? 0 : Convert.ToDouble(dtAtnDys);

                        //LVDYS
                        double TotLevDays = 0;
                        var dtTotLevDays = taGetAtnd.GetTotLvDays(EmpCode, dt.Year, dt.Month);
                        TotLevDays = dtTotLevDays == null ? 0 : Convert.ToDouble(dtTotLevDays);

                        //HALFLVDYS
                        double TotHfLvDays = 0;
                        var dtHfLvDys = taGetAtnd.GetTotHalfLvDays(EmpCode, dt.Year, dt.Month);
                        TotHfLvDays = dtHfLvDys == null ? 0 : Convert.ToDouble(dtHfLvDys);

                        //HOLDYS
                        double TotHoliDays = 0;
                        var dtTotHoliDys = taGetHolDay.GetTotHolDay(dt.Year, dt.Month, dtGetEmpRec[0].SecRefNo);
                        TotHoliDays = dtTotHoliDys == null ? 0 : Convert.ToDouble(dtTotHoliDys);

                        //WRKDYS
                        double totWorkDays = EnDate.Day;

                        absDays = totWorkDays - (TotAtnDays + TotHfLvDays + TotHoliDays + TotLevDays);

                        Values[Vcnt] = absDays;
                        Flag[Vcnt] = true;
                        break;

                    case "LEVDYS":
                        //3.LEVDYS - Total leaves taken in the month(ie. entered in leave details ,includes all leave types)
                        double totLvDays = 0;

                        double fLvDays = 0;
                        var dtfLvDays = taGetAtnd.GetTotLvDays(EmpCode, dt.Year, dt.Month);
                        fLvDays = dtfLvDays == null ? 0 : Convert.ToDouble(dtfLvDays);

                        double hLvDays = 0;
                        var dthLvDys = taGetAtnd.GetTotHalfLvDays(EmpCode, dt.Year, dt.Month);
                        hLvDays = dthLvDys == null ? 0 : Convert.ToDouble(dthLvDys);

                        totLvDays = fLvDays + hLvDays;

                        Values[Vcnt] = totLvDays;
                        Flag[Vcnt] = true;
                        break;

                    case "HCTDYS":
                        //4.HCTDYS -  Hulf Cut Days
                        double TotHlfLvDays = 0;
                        var dtHlfLvDys = taGetAtnd.GetTotHalfLvDays(EmpCode, dt.Year, dt.Month);
                        TotHlfLvDays = dtHlfLvDys == null ? 0 : Convert.ToDouble(dtHlfLvDys);
                        Values[Vcnt] = TotHlfLvDays;
                        Flag[Vcnt] = true;
                        break;

                    case "HOLDYS":
                        //4.HOLDYS - Total no of holidays in the present month(ie. declared in holiday master)
                        double HolDays = 0;
                        var dtHolDys = taGetHolDay.GetTotHolDay(dt.Year, dt.Month, dtGetEmpRec[0].SecRefNo);
                        HolDays = dtHolDys == null ? 0 : Convert.ToDouble(dtHolDys);
                        Values[Vcnt] = HolDays;
                        Flag[Vcnt] = true;
                        break;

                    case "PRSDYS":
                        //5.PRSDYS - Total days present 

                        //ATNDDAYS
                        double TotAtndDays = 0;
                        var dtAtndDys = taGetAtnd.GetTotAttndDays(EmpCode, dt.Year, dt.Month);
                        TotAtndDays = dtAtndDys == null ? 0 : Convert.ToDouble(dtAtndDys);

                        //LVDYS
                        double TotLvDays = 0;
                        var dtTotLvDays = taGetAtnd.GetTotLvDays(EmpCode, dt.Year, dt.Month);
                        TotLvDays = dtTotLvDays == null ? 0 : Convert.ToDouble(dtTotLvDays);

                        ////HALFLVDYS
                        double TotHalfLvDays = 0;
                        var dtHalfLvDys = taGetAtnd.GetTotHalfLvDays(EmpCode, dt.Year, dt.Month);
                        TotHalfLvDays = dtHalfLvDys == null ? 0 : Convert.ToDouble(dtHalfLvDys);

                        //HOLDYS
                        double TotHolDays = 0;
                        var dtTotHolDys = taGetHolDay.GetTotHolDay(dt.Year, dt.Month, dtGetEmpRec[0].SecRefNo);
                        TotHolDays = dtTotHolDys == null ? 0 : Convert.ToDouble(dtTotHolDys);

                        Values[Vcnt] = TotAtndDays + TotHalfLvDays + TotHolDays + TotLvDays;
                        //Values(Vcnt) = TotAtndDays
                        Flag[Vcnt] = true;
                        break;

                    case "WRKDYS":
                        //6.WRKDYS - Total Working Day
                        int WorkDays = 0;
                        //'For Joining in Current Month
                        //If GetRec.State = 1 Then GetRec.Close()
                        //GetRec.Open("select * from Hrms_Employee where EmpCode='" & EmpCode & "' and " _
                        //& " month(EmpJoinDate)=" & Month(Mnth) & " and year(EmpJoinDate)=" & Year(Mnth), conn, 3, 2)
                        //If GetRec.EOF = False Then
                        //    WorkDays = DateDiff(DateInterval.Day, GetRec.Fields("EmpJoinDate").Value, EndDate.Date) + 1
                        //Else
                        int totDays = EnDate.Day;
                        //End If

                        var dtHolDays = taGetHolDay.GetTotHolDay(dt.Year, dt.Month, dtGetEmpRec[0].SecRefNo);
                        int holDays = dtHolDays == null ? 0 : Convert.ToInt32(dtHolDays);

                        WorkDays = totDays - holDays;

                        Values[Vcnt] = WorkDays;
                        Flag[Vcnt] = true;
                        break;

                    case "TOTDYS":
                        //7.TOTDYS - Total days in month
                        Values[Vcnt] = EnDate.Day;
                        Flag[Vcnt] = true;
                        break;

                    case "GRSSAL":
                        //8.GRSSAL - Gross Salary 
                        var dtEmpRec = taGetEmpRec.GetDataByEmpRef(Convert.ToInt32(EmpCode));
                        if (dtEmpRec.Rows.Count > 0)
                            Values[Vcnt] = Convert.ToDouble(dtEmpRec[0].EmpSalary);
                        else
                            Values[Vcnt] = 0;
                        Flag[Vcnt] = true;
                        break;
                }
            }
        }
        #endregion

        #region GetVal
        private double GetVal(string forcode, string EmpCode, string Grade)
        {
            double GetValue = 0;
            //Recursive function to calculate the value of a formula code.This program calls itself
            //to findout the value of a Formula code

            int iCnt = 0;
            int inCnt = 0;
            int Gcnt = 0;

            var taEmpPaySet = new tblHrmsEmpPaySetTableAdapter();
            //RESULTSET FOR EMPLOYEE FORMULA DETAILS TABLE

            var taGrdRecView = new View_Grade_DetTableAdapter();
            //RESULTSET FOR GRADE MASTER

            var taPayHeadView = new View_Pay_HeadTableAdapter();
            //RESULTSET FOR FORMULA MASTER

            //num1 and num2 are used for formula calculation
            double Num1 = 0;
            double Num2 = 0;

            //numc1,numc2,numc3 are used to check condition specified for code
            double NumC1 = 0;
            double NumC2 = 0;
            double NumC3 = 0;

            double NumC21 = 0;
            double NumC22 = 0;
            double NumC23 = 0;

            bool rES = false;
            //result of first condition check
            bool res2 = false;
            //result of second condition check

            int LineNo = 0;
            //Line number in grade detail

            int subno = 0;
            //Sub Line number in grade detail

            string AndOrFlg = null;

            bool CalcResult = false;

            //To calculate result
            double TNum1 = 0;

            //To calculate result
            double TNum2 = 0;

            var taPayHead = new tblHrmsPayHeadTableAdapter();
            var dtPayHead = taPayHead.GetDataByRef(Convert.ToInt32(forcode));
            var payCode = dtPayHead.Rows.Count > 0 ? dtPayHead[0].PayHeadCode : "";
            //if (string.IsNullOrEmpty(forcode))
            if (string.IsNullOrEmpty(payCode))
            {
                GetValue = 0;
                return GetValue;
            }
            //if (IsNumber(forcode))
            if (IsNumber(payCode))
            {
                GetValue = Convert.ToDouble(forcode);
                return GetValue;
            }
            //if (forcode == "Earns")
            if (payCode == "EARNS")
            {
                GetValue = Perks;
                return GetValue;
            }
            //else if (forcode == "Deducs")
            else if (payCode == "DEDUCS")
            {
                GetValue = Deds;
                return GetValue;
            }

            //CHECKING IN EMPLOYEE-FORMULA DETAILS
            var dtEmpPaySetRec = taEmpPaySet.GetDataByPayHeadRef(EmpCode, forcode);
            if (dtEmpPaySetRec.Rows.Count > 0)
            {
                if (dtEmpPaySetRec[0].PaySetValFlag == "Y")
                {
                    GetValue = dtEmpPaySetRec[0].IsPaySetValueNull() ? 0 : Convert.ToDouble(dtEmpPaySetRec[0].PaySetValue);
                    ModFlg(forcode, Convert.ToDouble(dtEmpPaySetRec[0].PaySetValue));
                    return GetValue;
                }
                else
                {
                    goto para1;
                }
            }
            else
            {
                goto para1;
            }

        para1:
            for (iCnt = 0; iCnt <= Code.Length - 1; iCnt++)
            {
                if (Code[iCnt] == forcode)
                {
                    if (Flag[iCnt] == true)
                    {
                        GetValue = Values[iCnt];
                        ModFlg(forcode, Values[iCnt]);
                        return GetValue;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //GRADE MASTER TABLE

            var dtGrdRecView = taGrdRecView.GetDataByValFlag(Grade, forcode);
            //checking in grade master table

            if (dtGrdRecView.Rows.Count > 0)
            {
                if (dtGrdRecView[0].GrdDetFlag == "V")
                {
                    //IF VALUE SPECIFIED FOR THIS CODE IN GRADE MASTER
                    GetValue = Convert.ToDouble(dtGrdRecView[0].GrdDetBasePayHead);
                    return GetValue;
                }
                else if (dtGrdRecView[0].GrdDetFlag == "C")
                {
                    foreach (dsHrmsMas.View_Grade_DetRow grdRow in dtGrdRecView.Rows)
                    {
                        AndOrFlg = dtGrdRecView[0].GrdDetAndOrFlag;
                        for (inCnt = 0; inCnt <= Code.Length - 1; inCnt++)
                        {
                            if (dtGrdRecView[0].GrdDetCompPayHead == Code[inCnt])
                            {
                                if (Flag[inCnt] == true)
                                {
                                    NumC1 = Values[inCnt];
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        //GETTING VALUE FOR COMPARISON CODE
                        NumC1 = GetVal(dtGrdRecView[0].GrdDetCompPayHead, EmpCode, Grade);
                        ModFlg(dtGrdRecView[0].GrdDetCompPayHead, NumC1);
                        for (inCnt = 0; inCnt <= Code.Length - 1; inCnt++)
                        {
                            if (dtGrdRecView[0].GrdDetCompPayHead == Code[inCnt])
                            {
                                if (Flag[inCnt] == true)
                                {
                                    NumC1 = Values[inCnt];
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        //GETTING VALUE FOR SECOND CODE
                        NumC2 = GetVal(dtGrdRecView[0].GrdDetCompVal1, EmpCode, Grade);
                        ModFlg(dtGrdRecView[0].GrdDetCompVal1, NumC2);

                        //GETTING VALUE FOR THIRD COMPARISON CODE
                        NumC3 = GetVal(dtGrdRecView[0].IsGrdDetCompVal2Null() ? "" : dtGrdRecView[0].GrdDetCompVal2, EmpCode, Grade);
                        ModFlg(dtGrdRecView[0].GrdDetCompVal2, NumC3);

                        //checking for specified conditions  in grade master
                        switch (dtGrdRecView[0].GrdDetCompOpr)
                        {
                            case ">":
                                rES = (NumC1 > NumC2 ? true : false);
                                break;
                            case "<":
                                rES = (NumC1 < NumC2 ? true : false);
                                break;
                            case "<=":
                                rES = (NumC1 <= NumC2 ? true : false);
                                break;
                            case "=>":
                                rES = (NumC1 >= NumC2 ? true : false);
                                break;
                            case "<>":
                                rES = (NumC1 != NumC2 ? true : false);
                                break;
                            case "bet":
                            case "between":
                                rES = (NumC1 >= NumC2 & NumC1 <= NumC3 ? true : false);
                                break;
                            case "=":
                                rES = (NumC1 == NumC2 ? true : false);
                                break;
                            default:
                                rES = true;
                                break;
                        }

                        CalcResult = rES;

                        if (!string.IsNullOrEmpty(AndOrFlg))
                        {
                            //** if Flag = "O" (ie .OR) then if first condition is satisfied then
                            //** no need to calculate the second one.If Flag = "A" (ie .AND) then
                            //** if first condition is false then also no need to calculate the second
                            if (AndOrFlg == "O" & rES == true)
                            {
                                CalcResult = true;
                                //**Calculate the result
                                goto GetResult;
                            }
                            if (AndOrFlg == "A" & rES == false)
                            {
                                CalcResult = false;
                                //** Condition failed .dont calculate result
                                //GrdRec.MoveNext();
                                goto GetResult;
                            }
                            //GrdRec.MoveNext();

                            //** Checking for second condition
                            for (inCnt = 0; inCnt <= Code.Length - 1; inCnt++)
                            {
                                if (dtGrdRecView[0].GrdDetCompPayHead == Code[inCnt])
                                {
                                    if (Flag[inCnt] == true)
                                    {
                                        NumC1 = Values[inCnt];
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            //GETTING VALUE FOR COMPARISON CODE
                            NumC21 = GetVal(dtGrdRecView[0].GrdDetCompPayHead.ToString(), EmpCode.ToString(), Grade.ToString());
                            ModFlg(dtGrdRecView[0].GrdDetCompPayHead.ToString(), NumC21);

                            for (inCnt = 0; inCnt <= Code.Length - 1; inCnt++)
                            {
                                if (dtGrdRecView[0].GrdDetCompPayHead == Code[inCnt])
                                {
                                    if (Flag[inCnt] == true)
                                    {
                                        NumC21 = Values[inCnt];
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            //GETTING VALUE FOR SECOND CODE
                            NumC22 = GetVal(dtGrdRecView[0].GrdDetCompVal1, EmpCode, Grade);
                            ModFlg(dtGrdRecView[0].GrdDetCompVal1, NumC22);

                            NumC23 = GetVal(dtGrdRecView[0].IsGrdDetCompVal2Null() ? "" : dtGrdRecView[0].GrdDetCompVal2, EmpCode, Grade);
                            ModFlg(dtGrdRecView[0].GrdDetCompVal2, NumC23);

                            //checking for specified conditions  in grade master
                            switch (dtGrdRecView[0].GrdDetCompOpr)
                            {
                                case ">":
                                    res2 = (NumC21 > NumC22 ? true : false);
                                    break;
                                case "<":
                                    res2 = (NumC21 < NumC22 ? true : false);
                                    break;
                                case "<=":
                                    res2 = (NumC21 <= NumC22 ? true : false);
                                    break;
                                case "=>":
                                    res2 = (NumC21 >= NumC22 ? true : false);
                                    break;
                                case "<>":
                                    res2 = (NumC21 != NumC22 ? true : false);
                                    break;
                                case "bet":
                                case "between":
                                    res2 = (NumC21 >= NumC22 & NumC21 <= NumC23 ? true : false);
                                    break;
                                case "=":
                                    res2 = (NumC21 == NumC22 ? true : false);
                                    break;
                                default:
                                    res2 = true;
                                    break;
                            }
                            if (res2 == true)
                                //GrdRec.MovePrevious();
                                CalcResult = res2;
                        }

                    GetResult:
                        if (CalcResult == true)
                        {
                            if (!IsNumber(dtGrdRecView[0].GrdDetBasePayHead))
                            {
                                if (string.IsNullOrEmpty(dtGrdRecView[0].GrdDetBasePayHead))
                                {
                                    TNum1 = 0;
                                }
                                else
                                {
                                    TNum1 = GetVal(dtGrdRecView[0].GrdDetBasePayHead, EmpCode, Grade);
                                    ModFlg(dtGrdRecView[0].GrdDetBasePayHead, TNum1);
                                }
                            }
                            else
                            {
                                TNum1 = Convert.ToDouble(dtGrdRecView[0].GrdDetBasePayHead);
                            }
                            if (!IsNumber(dtGrdRecView[0].GrdDetDepndPayHead))
                            {
                                if (string.IsNullOrEmpty(dtGrdRecView[0].GrdDetDepndPayHead))
                                {
                                    TNum2 = 0;
                                }
                                else
                                {
                                    TNum2 = GetVal(dtGrdRecView[0].GrdDetDepndPayHead, EmpCode, Grade);
                                    ModFlg(dtGrdRecView[0].GrdDetDepndPayHead, TNum2);
                                }
                            }
                            else
                            {
                                TNum2 = Convert.ToDouble(dtGrdRecView[0].GrdDetDepndPayHead);
                            }

                            //calculation using formula specified in grade master
                            switch (dtGrdRecView[0].GrdDetOpr)
                            {
                                case "*":
                                    GetValue = TNum1 * TNum2;
                                    break;
                                case "/":
                                    if (TNum2 > 0)
                                    {
                                        GetValue = TNum1 / TNum2;
                                    }
                                    else
                                    {
                                        GetValue = 0;
                                    }
                                    break;
                                case "%":
                                    GetValue = TNum1 * (TNum2 / 100);
                                    break;
                                case "+":
                                    GetValue = TNum1 + TNum2;
                                    break;
                                case "-":
                                    GetValue = TNum1 - TNum2;
                                    break;
                                default:
                                    GetValue = TNum1;
                                    break;
                            }
                            for (Gcnt = 0; Gcnt <= Code.Length - 1; Gcnt++)
                            {
                                if (forcode == Code[Gcnt])
                                {
                                    Values[Gcnt] = GetValue;
                                    ModFlg(forcode, Convert.ToDouble(GetValue));
                                    return GetValue;
                                }
                            }
                        }
                        //GrdRec.MoveNext();
                    }
                }
                //GetValue = 0;
                return GetValue;
            }

            //*** If not specified in grade master then using formula master to calculate the formula
            var dtPayHeadView = taPayHeadView.GetDataByRef(Convert.ToInt32(forcode));

            if (!IsNumber(dtPayHeadView[0].PayHeadBaseVal))
            {
                if (string.IsNullOrEmpty(dtPayHeadView[0].PayHeadBaseVal))
                {
                    Num1 = 0;
                }
                else
                {
                    Num1 = GetVal(dtPayHeadView[0].PayHeadBaseValRef, EmpCode, Grade);
                    ModFlg(dtPayHeadView[0].PayHeadBaseValRef, Num1);
                }
            }
            else
            {
                Num1 = Convert.ToDouble(dtPayHeadView[0].PayHeadBaseVal);
            }

            if (!IsNumber(dtPayHeadView[0].PayHeadDepnd))
            {
                if (string.IsNullOrEmpty(dtPayHeadView[0].PayHeadDepnd))
                {
                    Num2 = 0;
                }
                else
                {
                    Num2 = GetVal(dtPayHeadView[0].PayHeadDpndRef, EmpCode, Grade);
                    ModFlg(dtPayHeadView[0].PayHeadDpndRef, Num2);
                }
            }
            else
            {
                Num2 = Convert.ToDouble(dtPayHeadView[0].PayHeadDepnd);
            }

            switch (dtPayHeadView[0].PayHeadOpr)
            {
                case "*":
                    GetValue = Num1 * Num2;
                    break;
                case "/":
                    if (Num2 > 0)
                    {
                        GetValue = Num1 / Num2;
                    }
                    else
                    {
                        GetValue = 0;
                    }
                    break;
                case "%":
                    GetValue = Num1 * (Num2 / 100);
                    break;
                case "+":
                    GetValue = Num1 + Num2;
                    break;
                case "-":
                    GetValue = Num1 - Num2;
                    break;
            }
            return GetValue;
        }
        #endregion

        private void ModFlg(string Fcode, double Fval)
        {
            //THIS PROCEDURE IS TO MODIFY THE FLAG AFTER CALCULATION
            long Fcnt = 0;
            if (IsNumber(Fcode))
                return;
            for (Fcnt = 1; Fcnt <= Code.Length - 1; Fcnt++)
            {
                if (Fcode == Code[Fcnt])
                {
                    Values[Fcnt] = Fval;
                    Flag[Fcnt] = true;
                    break;
                }
            }
        }

        public static bool IsNumber(String str)
        {
            try
            {
                Double.Parse(str);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void gvSalHdr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                var mnth = Convert.ToDateTime(txtSalDate.Text.Trim()).Month;
                var year = Convert.ToDateTime(txtSalDate.Text.Trim()).Year;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var EmpRef = ((HiddenField)e.Row.FindControl("hfEmpRef")).Value;

                    GridView gvSalDet = (GridView)e.Row.FindControl("gvSalDet");

                    var taSalCal = new tblHrmsTrialSalaryTableAdapter();
                    gvSalDet.DataSource = taSalCal.GetDataByEmpRef(EmpRef, Convert.ToDecimal(mnth), Convert.ToDecimal(year));
                    gvSalDet.DataBind();
                }
            }
            catch (Exception ex) { }
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            var taEmpMas = new View_Emp_BascTableAdapter();
            var dtEmpMas = new dsEmpDet.View_Emp_BascDataTable();

            #region Load Emp List
            if (cboLoc.SelectedIndex == 0)
                //---ALL Location
                dtEmpMas = taEmpMas.GetData();
            else
                if (cboDept.SelectedIndex == 0)
                    //---Selected Location
                    dtEmpMas = taEmpMas.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                else
                    if (cboSec.SelectedIndex == 0)
                        //---Selected Department
                        dtEmpMas = taEmpMas.GetDataByLocDept(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString());
                    else
                            //---Selected Section
                            dtEmpMas = taEmpMas.GetDataByLocDeptSec(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString());

            gvSalHdr.DataSource = dtEmpMas;
            gvSalHdr.DataBind();
            #endregion
        }

        public string GetNetVal(string empRef)
        {
            string netVal = "0";
            try
            {
                var mnth = Convert.ToDateTime(txtSalDate.Text.Trim()).Month;
                var year = Convert.ToDateTime(txtSalDate.Text.Trim()).Year;

                var taSal = new tblHrmsTrialSalaryTableAdapter();
                var dtSal = taSal.GetNetVal(empRef, Convert.ToDecimal(mnth), Convert.ToDecimal(year));
                if (dtSal.Rows.Count > 0)
                    netVal = dtSal[0].IsCalValueNull() ? "0" : dtSal[0].CalValue.ToString();
                return netVal;
            }
            catch (Exception) { return netVal; }
        }

        protected void gvSalHdr_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var empRef = ((HiddenField)(gvSalHdr.Rows[rowNum].FindControl("hfEmpRef"))).Value;

            var taSalCal = new tblHrmsTrialSalaryTableAdapter();
            var dtDept = taSalCal.GetDataByEmpRef(empRef, Convert.ToDecimal(Convert.ToDateTime(txtSalDate.Text.Trim()).Month), Convert.ToDecimal(Convert.ToDateTime(txtSalDate.Text.Trim()).Year));
            if (dtDept.Rows.Count > 0)
            {
                taSalCal.DeleteSalaryByEmpRef(empRef, Convert.ToDecimal(Convert.ToDateTime(txtSalDate.Text.Trim()).Month), Convert.ToDecimal(Convert.ToDateTime(txtSalDate.Text.Trim()).Year));

                tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }

            var taEmpMas = new View_Emp_BascTableAdapter();
            var dtEmpMas = new dsEmpDet.View_Emp_BascDataTable();

            #region Load Emp List
            if (cboLoc.SelectedIndex == 0)
                //---ALL Location
                dtEmpMas = taEmpMas.GetData();
            else
                if (cboDept.SelectedIndex == 0)
                    //---Selected Location
                    dtEmpMas = taEmpMas.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                else
                    if (cboSec.SelectedIndex == 0)
                        //---Selected Department
                        dtEmpMas = taEmpMas.GetDataByLocDept(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString());
                    else
                        //---Selected Section
                        dtEmpMas = taEmpMas.GetDataByLocDeptSec(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(), cboSec.SelectedValue.ToString());

            gvSalHdr.DataSource = dtEmpMas;
            gvSalHdr.DataBind();
            gvSalHdr.SelectedIndex = -1;
            #endregion            
        }
    }
}