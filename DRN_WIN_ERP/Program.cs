using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace DRN_WIN_ERP
{
    class Program : WindowsFormsApplicationBase
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            var app = new Program();
            //app.EnableVisualStyles = true;            
            app.ShutdownStyle = ShutdownMode.AfterAllFormsClose;
            app.MainForm = new frmLogin();
            app.Run(args);
        }
    }
}
