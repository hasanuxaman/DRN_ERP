using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DRN_WIN_ERP
{
    public partial class frmBrowserCOA : Form
    {
        private string ConnectionString;
        private string QueryString;
        private string LikeColumnString;
        private string GridColName;
        private string[] TableColumnName;
        private string[] GridColumnNameArray;
        private int searchColumn = 0;
        private string returnString;
        private string returnString1;
        private string returnString2;
        private string returnString3;
        private string rowindx;
        private int rHH;
        private int rLL;

        public frmBrowserCOA(string ConnectionStr, string QueryStr, string GridColumnName, string likeColumn, int HH, int LL, Form par)
        {
            InitializeComponent();
            LoadDataInGrid(ConnectionStr, QueryStr, GridColumnName);
            ConnectionString = ConnectionStr;
            QueryString = QueryStr;
            LikeColumnString = likeColumn;
            GridColName = GridColumnName;
            rHH = HH;
            rLL = LL;
            //this.MdiParent = par;
            this.CenterToScreen();
            this.ShowDialog();
            //this.Top = 200;
            //this.Left = 100;
        }

        public string ReturnString
        {
            get { return returnString; }
            set { returnString = value; }
        }

        public string ReturnString1
        {
            get { return returnString1; }
            set { returnString1 = value; }
        }

        public string ReturnString2
        {
            get { return returnString2; }
            set { returnString2 = value; }
        }
        public string ReturnString3
        {
            get { return returnString3; }
            set { returnString3 = value; }
        }
        private string rRowindx
        {
            get { return rowindx; }
            set { rowindx = value; }
 
        }

        //public int RHH
        //{
        //    get { return rHH; }
        //    set { rHH = value; } 
        //}
        //public int RLL
        //{
        //    get { return rLL; }
        //    set { rLL = value; }
        //}

        private void LoadDataInGrid(string ConnectionStr, string QueryStr, string GridColumnName)
        {
            string[] gdvcol = GridColumnName.Split(',');
            GridColumnNameArray = GridColumnName.Split(',');
            TableColumnName = new string[GridColumnNameArray.Length];
            if (gdvcol.Length == 0)
                return;

            SqlConnection sqlConn = null;
            try
            {
                string selectQuery = QueryStr;
                sqlConn = new SqlConnection(ConnectionStr);
                sqlConn.Open();
                SqlDataAdapter sqlDataAdapterObj = new SqlDataAdapter(selectQuery, sqlConn);
                DataTable dataTableObj = new DataTable();
                sqlDataAdapterObj.Fill(dataTableObj);

                if (dataTableObj.Columns.Count != gdvcol.Length)
                {
                    System.Windows.Forms.MessageBox.Show("Grid Columns and Table Columns are not Equal");
                    return;
                }

                for (int i = 0; i < gdvcol.Length; i++)
                {
                    TableColumnName[i] = dataTableObj.Columns[i].ColumnName;
                    dataTableObj.Columns[i].ColumnName = gdvcol[i].ToString();
                }
                BrowserGrid.DataSource = dataTableObj;
                SelectedGridColumn();
                txtSearch.Focus();
            }
            catch (SqlException sqlExceptionObject)
            {
                MessageBox.Show(sqlExceptionObject.Message.ToString());
            }
            catch (Exception exceptionObject)
            {
                MessageBox.Show(exceptionObject.Message.ToString());
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }

        }

        private void SearchByText(string str)
        {
            if (GridColumnNameArray.Length == 0)
                return;

            SqlConnection sqlConn = null;
            try
            {
                string selectQuery = PreparQueryStr(QueryString, str, LikeColumnString);
                sqlConn = new SqlConnection(ConnectionString);
                sqlConn.Open();
                SqlDataAdapter sqlDataAdapterObj = new SqlDataAdapter(selectQuery, sqlConn);
                DataTable dataTableObj = new DataTable();
                sqlDataAdapterObj.Fill(dataTableObj);

                if (dataTableObj.Columns.Count != GridColumnNameArray.Length)
                {
                    System.Windows.Forms.MessageBox.Show("Grid Columns and Table Columns are not Equal");
                    return;
                }

                for (int i = 0; i < GridColumnNameArray.Length; i++)
                {
                    TableColumnName[i] = dataTableObj.Columns[i].ColumnName;
                    dataTableObj.Columns[i].ColumnName = GridColumnNameArray[i].ToString();
                }
                BrowserGrid.DataSource = dataTableObj;
                               
                txtSearch.Focus();
            }
            catch (SqlException sqlExceptionObject)
            {
                MessageBox.Show(sqlExceptionObject.Message.ToString());
            }
            catch (Exception exceptionObject)
            {
                MessageBox.Show(exceptionObject.Message.ToString());
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }

        }

        private string PreparQueryStr(string qrystr, string searchstr, string likeColumn)
        {
            string qstring = RemoveSpace(qrystr.ToLower());
            string str1 = "", str2 = "", str3 = "", str4 = "", str5 = "", retStr = "", likepart = "";

            str1 = RemoveSpace(qrystr.ToLower());

            string[] likeCol = likeColumn.Split(',');
            string[] LikeColumnArray = new string[likeCol.Length];

            if (likeCol.Length > 1)
            {
                for (int i = 0; i < likeCol.Length; i++)
                {
                    if (likepart == "")
                        likepart = likeCol[i].ToString() + " like '" + searchstr + "%' ";
                    else
                        likepart = likepart + " or " + likeCol[i].ToString() + " like '" + searchstr + "%' ";
                }
            }
            else
            {
                //likepart = "(Par_Adr_Ref like '" + searchstr + "%' or Par_Adr_Ref_No like '" + searchstr + "%' or Par_Adr_Name like '" + searchstr + "%')";
                //likepart = "(" + TableColumnName[0].ToString() + " like '" + searchstr + "%')";
               
                if (TableColumnName.Length > 1)
                {
                    for (int i = 0; i < TableColumnName.Length; i++)
                    {
                        if (likepart == "")
                            likepart = TableColumnName[i].ToString() + " like '" + searchstr + "%' ";
                        else
                            likepart = likepart + " or " + TableColumnName[i].ToString() + " like '" + searchstr + "%' ";
                    }
                }
                else
                    likepart = "";
            }

            if (qstring.IndexOf("order by") > 0)
            {
                str2 = qstring.Substring(0, qstring.IndexOf("order by"));
                str5 = qstring.Substring(qstring.IndexOf("order by") + ("order by").Length);

                if (qstring.IndexOf("where") > 0)
                {
                    str3 = qstring.Substring(0, str2.IndexOf("where"));
                    str4 = str2.Substring(str2.IndexOf("where") + ("where").Length);
                    retStr = str3 + " where " + str4 + " and " + likepart + " Order by " + str5;
                }
                else
                {
                    str3 = str2;
                    retStr = str3 + " where " + likepart + " order by " + str5;
                }

            }
            else
            {
                if (qstring.IndexOf("where") > 0)
                {
                    str3 = qstring.Substring(0, qstring.IndexOf("where"));
                    str4 = qstring.Substring(qstring.IndexOf("where") + ("where").Length);
                    retStr = str3 + " where " + str4 + " and " + likepart;
                }
                else
                {
                    retStr = str1 + " where " + likepart;
                }
            }

            return retStr;
        }

        private void SelectedGridColumn()
        {
            lblSearchString.Text = GridColumnNameArray[searchColumn];
            BrowserGrid.Columns[searchColumn].DefaultCellStyle.BackColor = Color.Yellow;
        }

        private void BrowserGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            BrowserGrid.Columns[searchColumn].DefaultCellStyle.BackColor = Color.Empty;
            searchColumn = e.ColumnIndex;
            SelectedGridColumn();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {

        }

        private string RemoveSpace(string inputString)
        {
            string[] ips;
            string retStr = "";
            try
            {
                ips = inputString.Trim().Split(' ');
                for (int i = 0; i < ips.Length; i++)
                {
                    if (ips[i] != "")
                    {
                        retStr = retStr + " " + ips[i];
                    }

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            return retStr.Trim();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {           
            int indx = 0;
            if(btnok.Tag!="")
            {
                indx = Convert.ToInt32(btnok.Tag);
            }

            if (e.KeyChar.ToString() == "\r") //Enter
            {
                Enter_Key(indx);
            }
        }

        private void Enter_Key(int indx)
        {
            if (BrowserGrid.Rows.Count > 0)
            {
                if (txtSearch.Text != "")
                {                  

                    if (BrowserGrid.Rows.Count <= indx)
                        indx = 0;
                    ReturnString = BrowserGrid.Rows[indx].Cells[0].Value.ToString();

                    if (BrowserGrid.Columns.Count > 1)
                    {                        
                        ReturnString1 = BrowserGrid.Rows[indx].Cells[1].Value.ToString();

                        if (BrowserGrid.Columns.Count > 2)                           
                            ReturnString2 = BrowserGrid.Rows[indx].Cells[2].Value.ToString();
                        else
                            ReturnString2 = "";
                    }
                    else
                    {
                        ReturnString1 = "";
                        ReturnString2 = "";
                    }

                    this.Close();
                }
            }
            else
            {
                ReturnString = "";
                ReturnString1 = "";
                ReturnString2 = "";
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            int indx = 0;
            if (btnok.Tag != "")
            {
                indx = Convert.ToInt32(btnok.Tag);
            }
            
            Enter_Key(indx);
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchByText(txtSearch.Text.ToString());
        }

        private void BrowserGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtSearch.Text = BrowserGrid[searchColumn, e.RowIndex].Value.ToString();
                btnok.Tag = e.RowIndex;
            }           
            
        }

        private void frmBrowser_Activated(object sender, EventArgs e)
        {
            this.txtSearch.Focus();
        }

        private void BrowserGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtSearch.Text = BrowserGrid[searchColumn, e.RowIndex].Value.ToString();
            }

            int indx = 0;
            btnok.Tag = e.RowIndex;           
            if (btnok.Tag != "")
            {
                indx = Convert.ToInt32(btnok.Tag);
            }

            Enter_Key(indx);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
            }

            if (e.KeyValue == 40)
            {
                BrowserGrid.Focus();
                BrowserGrid.CurrentCell = BrowserGrid[searchColumn,0]; 
            }

            
        }

        // Option 1 - used to preseve TAB functionality of jumping between controls

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                BrowserGrid.Columns[searchColumn].DefaultCellStyle.BackColor = Color.Empty;

                if (searchColumn + 1 < GridColumnNameArray.Length)
                {
                    searchColumn = searchColumn + 1;
                }
                else
                    searchColumn = 0;
                SelectedGridColumn();
                txtSearch.Focus();                                

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Option 2 - ignoring standard TAB functionality

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                return false;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Prevent textbox beeping

            if (e.KeyChar == '\t')
            {
                e.Handled = true;
            }
        }

        private void BrowserGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BrowserGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\t")
            {
                e.Handled = true;
            }
            else if (e.KeyChar.ToString() == "\r")  //Enter
            {
                searchColumn = 0;

                int a = BrowserGrid.Rows.Count;

                int indx = BrowserGrid.SelectedCells[0].RowIndex;

                txtSearch.Text = BrowserGrid.Rows[indx].Cells[searchColumn].Value.ToString();

                Enter_Key(indx);                         
                               
                               
                //if (e.KeyChar.ToString() == "\r") //Enter
                //{
                //    Enter_Key(indx);
                //}
            }
        }

        private void BrowserGrid_KeyDown(object sender, KeyEventArgs e)
        {
            int indx = 0;


            if (e.KeyCode == Keys.Up) //38
            {
                int a = e.KeyValue;

                if (BrowserGrid.SelectedCells[0].RowIndex == 0)
                {
                    txtSearch.Focus(); 
                }

            }
            else if (e.KeyCode == Keys.Down)  //40
            {
                int a = e.KeyValue;               
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
            else 
            {
            }           

        }

        private void frmBrowserCOA_Load(object sender, EventArgs e)
        {
            //this.Top = rHH;
            //this.Left =rLL;

            Rearrangegrid();
        }

        private void Rearrangegrid()
        {
            for (int i = 0; i < BrowserGrid.Columns.Count; i++)
            {
                if (i == 0)
                {
                    BrowserGrid.Columns[i].Width = 105;
                }
                if (i == 1)
                {
                    BrowserGrid.Columns[i].Width = 150; 
                }
                if (i == 2)
                {
                    BrowserGrid.Columns[i].Width = 150;
                }
                if (i == 3)
                {
                    BrowserGrid.Columns[i].Width = 130;
                }
                if (i == 4)
                {
                    BrowserGrid.Columns[i].Width = 130;
                }
                if (i == 5)
                {
                    BrowserGrid.Columns[i].Width = 120;
                }

                this.BrowserGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
               
            }
        }


    }

}
