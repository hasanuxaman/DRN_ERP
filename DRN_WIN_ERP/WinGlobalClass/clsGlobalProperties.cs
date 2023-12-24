using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRN_WIN_ERP.WinGlobalClass
{
    class clsGlobalProperties
    {
        public static string sysConStr { get; set; }

        public static string drnConStr { get; set; }

        public static string sysDbServer { get; set; }

        public static string drnDbServer { get; set; }

        public static string sysDbServerName { get; set; }

        public static string drnDbServerName { get; set; }

        public static string CompanyCode { get; set; }

        public static string UserRef { get; set; }

        public static string AdminUserPass { get; set; }

        public static string rptFilePath { get; set; }

        public static string rptFormula { get; set; }

        public static string RptDateFrom { get; set; }

        public static string RptDateTo { get; set; }

        public static string RptHdr { get; set; }
    }
}
