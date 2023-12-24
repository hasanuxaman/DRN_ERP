using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DRN_WIN_ERP
{
    public partial class frmAdminPass : Form
    {
        public frmAdminPass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == "admin@123" || txtPassword.Text.Trim() == "admin@007")
            {
                this.Close();
                frmDbConfig frmConfig = new frmDbConfig();
                frmConfig.ShowDialog(this);                
            }
            else
            {
                MessageBox.Show("Incorrect password.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
