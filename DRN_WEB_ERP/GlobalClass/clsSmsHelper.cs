using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;

namespace DRN_WEB_ERP.GlobalClass
{
    public class clsSmsHelper
    {
        private clsSmsHelper()
        {
            
        }

        public static string convertBanglatoUnicode(string banglaText)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in banglaText)
            {
                sb.AppendFormat("{1:x4}", c, (int)c);
            }
            string unicode = sb.ToString().ToUpper();
            return unicode;
        } 
    }
}