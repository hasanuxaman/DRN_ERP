using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DRN_WIN_ERP.WinGlobalClass
{
    public class clsUpdtDbConStr
    {
        public static void setSysDBCon(string sysConnstr)
        {
            DRN_WIN_ERP.Properties.Settings.Default["SYSConStr"] = sysConnstr;
            DRN_WIN_ERP.Properties.Settings.Default.Save();
        }

        public static void setAppDBCon(string appConnstr)
        {
            DRN_WIN_ERP.Properties.Settings.Default["DRNConStr"] = appConnstr;
            DRN_WIN_ERP.Properties.Settings.Default.Save();
        }
    }
}
