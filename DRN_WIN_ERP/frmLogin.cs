using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using DRN_WIN_ERP.DataSets.dsSysMasTranTableAdapters;

namespace DRN_WIN_ERP
{
    public partial class frmLogin : Form
    {        
        public frmLogin()
        {
            InitializeComponent();           

            lblFooterTitle.Text = "[" + this.Text + "]";
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                //this.Hide();
                //frmSplash frmSplash = new frmSplash();
                //frmSplash.Show();
                //frmSplash.Update();
                //Thread.Sleep(5000);

                // Read the file and display it line by line.            
                string drnConStr = "";
                string sysConStr = "";
                string sysServer = "";
                string drnServer = "";

                //string progFilesPath = Environment.ExpandEnvironmentVariables("%ProgramW6432%") + @"\DRN_WIN_ERP";
                //string programFilesX86 = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%");

                //string progFilesPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) + @"\DRN_WIN_ERP";
                //if (!Directory.Exists(progFilesPath))
                //{
                //    DirectoryInfo di = Directory.CreateDirectory(progFilesPath);
                //}

                //string yourpath = progFilesPath + @"\ConStr.txt";
                string yourpath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\ConStr.txt";
                if (File.Exists(yourpath))
                {
                    string[] lines = System.IO.File.ReadAllLines(yourpath);
                    if (lines.Length >= 2)
                    {                        
                        sysConStr = WinGlobalClass.clsEncryptDecrypt.Decrypt(lines[0].ToString());
                        drnConStr = WinGlobalClass.clsEncryptDecrypt.Decrypt(lines[1].ToString());

                        DRN_WIN_ERP.WinGlobalClass.clsUpdtDbConStr.setSysDBCon(sysConStr.Trim());
                        DRN_WIN_ERP.WinGlobalClass.clsUpdtDbConStr.setAppDBCon(drnConStr.Trim());

                        DRN_WIN_ERP.WinGlobalClass.clsGlobalProperties.sysConStr = sysConStr.Trim();
                        DRN_WIN_ERP.WinGlobalClass.clsGlobalProperties.drnConStr = drnConStr.Trim();

                        #region Load_Company_Data
                        // Create new DataTable and DataSource objects.
                        DataTable dtComp = new DataTable();
                        // Declare DataColumn and DataRow variables.
                        DataColumn columnComp;
                        DataRow rowComp;
                        // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                        columnComp = new DataColumn();
                        columnComp.DataType = System.Type.GetType("System.String");
                        columnComp.ColumnName = "Value";
                        dtComp.Columns.Add(columnComp);
                        // Create second column.
                        columnComp = new DataColumn();
                        columnComp.DataType = Type.GetType("System.String");
                        columnComp.ColumnName = "Text";
                        dtComp.Columns.Add(columnComp);
                        rowComp = dtComp.NewRow();
                        rowComp["Value"] = "ECIL";
                        rowComp["Text"] = "Eastern Cement Industries Ltd.";
                        dtComp.Rows.Add(rowComp);
                        cboCompany.DataSource = dtComp;
                        cboCompany.DisplayMember = "Text";
                        cboCompany.ValueMember = "Value";
                        cboCompany.SelectedIndex = 0;
                        #endregion

                        try
                        {
                            string[] _dataSourceSys = sysConStr.Split(';');
                            if (_dataSourceSys.Length >= 1)
                            {
                                string[] _serverName = _dataSourceSys[0].Split('=');
                                if (_serverName.Length >= 2) sysServer = _serverName[1].ToString();
                                WinGlobalClass.clsGlobalProperties.sysDbServer = sysServer;
                            }

                            string[] _dataSourceDrn = drnConStr.Split(';');
                            if (_dataSourceDrn.Length >= 1)
                            {
                                string[] _serverName = _dataSourceDrn[0].Split('=');
                                if (_serverName.Length >= 2) drnServer = _serverName[1].ToString();
                            }

                            string sysServerName = string.Empty;
                            string drnServerName = string.Empty;
                            try
                            {
                                IPHostEntry SysDbHost = Dns.GetHostEntry(sysServer == "" ? "." : sysServer);
                                IPHostEntry DrnDbHost = Dns.GetHostEntry(drnServer == "" ? "." : drnServer);

                                sysServerName = SysDbHost.HostName;
                                drnServerName = DrnDbHost.HostName;

                                WinGlobalClass.clsGlobalProperties.sysDbServerName = sysServerName;
                                WinGlobalClass.clsGlobalProperties.drnDbServerName = drnServerName;
                            }
                            catch (Exception ex)
                            {
                                WinGlobalClass.clsGlobalProperties.sysDbServerName = "Server Error";
                                WinGlobalClass.clsGlobalProperties.drnDbServerName = "Server Error";
                            }
                        }
                        catch (Exception ex)
                        {
                            WinGlobalClass.clsGlobalProperties.sysDbServerName = "";
                            WinGlobalClass.clsGlobalProperties.drnDbServerName = "";
                        }

                        lblServer.Text = " :: " + WinGlobalClass.clsGlobalProperties.sysDbServerName.ToString() + " :: ";

                        //frmSplash.Close();
                        this.Visible = true;
                        this.Activate();
                        this.BringToFront();
                        txtUserCode.Focus();
                    }
                    else
                    {
                        if (MessageBox.Show("Invalid Database Connection. Do you want to configure now ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //frmSplash.Close();
                            this.Hide();
                            frmDbConfig frmDbConf = new frmDbConfig();
                            frmDbConf.Show();
                            this.Close();
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }                    
                }
                else
                {
                    if (MessageBox.Show("Database configuration file does not found. Do you want to configure now ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //frmSplash.Close();
                        this.Hide();
                        frmDbConfig frmDbConf = new frmDbConfig();
                        frmDbConf.Show();
                        this.Close();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Error. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult msgresult = MessageBox.Show("Are you sure want to exit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes) Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var taUser = new TBL_USER_INFOTableAdapter();
                var dtUser = taUser.GetUserByCode(txtUserCode.Text.Trim().ToUpper());

                if (dtUser.Rows.Count > 0)
                {
                    if (txtPass.Text.Trim() == dtUser[0].User_Pass.ToString())
                    {
                        WinGlobalClass.clsGlobalProperties.CompanyCode = txtUserCode.Text.Trim();
                        WinGlobalClass.clsGlobalProperties.UserRef = dtUser[0].User_Ref_No.ToString();

                        frmMain frmMain = new frmMain();
                        frmMain.Show();
                        frmMain.Activate();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Password.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid User.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data processing error. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsUserExists(string userid)
        {
            var taUser = new TBL_USER_INFOTableAdapter();
            var dtUser = taUser.GetUserByCode(userid.Trim().ToUpper());           

            bool userExists = dtUser.Rows.Count > 0;

            return userExists;
        }
    }
}    

