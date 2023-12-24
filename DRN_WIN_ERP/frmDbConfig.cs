using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Data.SqlClient;

namespace DRN_WIN_ERP
{
    public partial class frmDbConfig : Form
    {
        public frmDbConfig()
        {
            InitializeComponent();
        }

        private void frmDbConfig_Load(object sender, EventArgs e)
        {
            txtServerName.Focus();
        }

        private void frmDbConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var sysConStr = WinGlobalClass.clsEncryptDecrypt.Encrypt("Data Source=" + txtServerName.Text.Trim() + ";Initial Catalog=" + cboSysDb.SelectedValue.ToString() + ";User Id=" + txtUserId.Text.Trim() + ";Password=" + txtPass.Text.Trim());
                var drnConStr = WinGlobalClass.clsEncryptDecrypt.Encrypt("Data Source=" + txtServerName.Text.Trim() + ";Initial Catalog=" + cboAppDb.SelectedValue.ToString() + ";User Id=" + txtUserId.Text.Trim() + ";Password=" + txtPass.Text.Trim());

                //string progFilesPath = Environment.ExpandEnvironmentVariables("%ProgramW6432%") + @"\DRN_WIN_ERP";
                //string programFilesX86 = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%");

                ////string progFilesPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) + @"\DRN_WIN_ERP";
                //if (!Directory.Exists(progFilesPath))
                //{
                //    DirectoryInfo di = Directory.CreateDirectory(progFilesPath);
                //}
                //string yourpath = progFilesPath + @"\ConStr.txt";

                string yourpath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\ConStr.txt";
                if (!File.Exists(yourpath))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(yourpath))
                    {
                        sw.WriteLine(sysConStr);
                        sw.WriteLine(drnConStr);
                    }
                }
                if (File.Exists(yourpath))
                {
                    using (StreamWriter sw = File.CreateText(yourpath))
                    {
                        sw.WriteLine(sysConStr);
                        sw.WriteLine(drnConStr);
                    }
                    //this.Hide();
                    MessageBox.Show("Configuration file has been created successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //frmLogin frmlogin = new frmLogin();
                    //frmlogin.Visible = true;
                    //frmlogin.Activate();
                    //frmlogin.BringToFront();                    
                    //this.Close();
                    Application.Restart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create Database configuration file. " + ex.Message , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void btnTestCon_Click(object sender, EventArgs e)
        {
            lblConStatMsg.Visible = true;

            if (txtServerName.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Enter Server Name First.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtUserId.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Enter User Id First.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string ConnectionString;
            SqlConnection connChk = new SqlConnection();
            try
            {
                // Open the connection
                if (connChk.State == ConnectionState.Open) connChk.Close();

                ConnectionString = "Data Source=" + txtServerName.Text.Trim() + "; User Id=" + txtUserId.Text.Trim() + "; Password=" + txtPass.Text.Trim();
                connChk.ConnectionString = ConnectionString;
                connChk.Open();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {                    
                    using (SqlCommand cmd = new SqlCommand("SELECT name FROM master.sys.databases", conn))
                    {
                        conn.Open();
                        System.Data.SqlClient.SqlDataReader SqlDRSys;
                        SqlDRSys = cmd.ExecuteReader();

                        #region Load_Company_Data
                        // Create new DataTable and DataSource objects.
                        DataTable dtSys = new DataTable();
                        // Declare DataColumn and DataRow variables.
                        DataColumn columnSys;
                        DataRow rowSys;
                        // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                        columnSys = new DataColumn();
                        columnSys.DataType = System.Type.GetType("System.String");
                        columnSys.ColumnName = "Value";
                        dtSys.Columns.Add(columnSys);
                        // Create second column.
                        columnSys = new DataColumn();
                        columnSys.DataType = Type.GetType("System.String");
                        columnSys.ColumnName = "Text";
                        dtSys.Columns.Add(columnSys);
                        while (SqlDRSys.Read())
                        {
                            rowSys = dtSys.NewRow();
                            rowSys["Value"] = SqlDRSys.GetString(0);
                            rowSys["Text"] = SqlDRSys.GetString(0);
                            dtSys.Rows.Add(rowSys);
                        }
                        cboSysDb.DataSource = dtSys;
                        cboSysDb.DisplayMember = "Text";
                        cboSysDb.ValueMember = "Value";
                        cboSysDb.SelectedIndex = 0;
                        #endregion
                    }
                    
                    conn.Close();
                    using (SqlCommand cmd = new SqlCommand("SELECT name FROM master.sys.databases", conn))
                    {
                        conn.Open();
                        System.Data.SqlClient.SqlDataReader SqlDRApp;
                        SqlDRApp = cmd.ExecuteReader();

                        #region Load_Company_Data
                        // Create new DataTable and DataSource objects.
                        DataTable dtApp = new DataTable();
                        // Declare DataColumn and DataRow variables.
                        DataColumn columnApp;
                        DataRow rowApp;
                        // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                        columnApp = new DataColumn();
                        columnApp.DataType = System.Type.GetType("System.String");
                        columnApp.ColumnName = "Value";
                        dtApp.Columns.Add(columnApp);
                        // Create second column.
                        columnApp = new DataColumn();
                        columnApp.DataType = Type.GetType("System.String");
                        columnApp.ColumnName = "Text";
                        dtApp.Columns.Add(columnApp);
                        while (SqlDRApp.Read())
                        {
                            rowApp = dtApp.NewRow();
                            rowApp["Value"] = SqlDRApp.GetString(0);
                            rowApp["Text"] = SqlDRApp.GetString(0);
                            dtApp.Rows.Add(rowApp);
                        }
                        cboAppDb.DataSource = dtApp;
                        cboAppDb.DisplayMember = "Text";
                        cboAppDb.ValueMember = "Value";
                        #endregion                
                    }
                }

                txtServerName.Enabled = false;
                txtUserId.Enabled = false;
                txtPass.Enabled = false;
                btnSave.Enabled = true;

                lblConStatMsg.Text = "Data connection successful.";
                lblConStatMsg.ForeColor = Color.Green;
                lblConStatMsg.Visible = true;                
            }
            catch (Exception ex)
            {
                txtServerName.Enabled = true;
                txtUserId.Enabled = true;
                txtPass.Enabled = true;
                btnSave.Enabled = false;

                lblConStatMsg.Text = "Data connection fail.";
                lblConStatMsg.ForeColor = Color.Red;
                lblConStatMsg.Visible = true;
            }            
        }
    }
}
