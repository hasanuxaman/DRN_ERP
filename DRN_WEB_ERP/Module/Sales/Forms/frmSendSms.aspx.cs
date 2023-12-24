using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.bd.com.mobireach.user;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSendSms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taSalesDsm = new View_Sales_DSMTableAdapter();
            var dtSalesDsm = taSalesDsm.GetActDsm();
            foreach (dsSalesMas.View_Sales_DSMRow dr in dtSalesDsm.Rows)
            {
                cboDsm.Items.Add(new ListItem(dr.Dsm_Short_Name.ToString() + " :: " + dr.SalesZoneName.ToString(), dr.Dsm_Ref.ToString()));
            }
            cboDsm.Items.Insert(0, new ListItem("---All---", "0"));

            var taSP = new tblSalesPersonMasTableAdapter();
            var dtSP = taSP.GetActiveSpList();
            foreach (dsSalesMas.tblSalesPersonMasRow dr in dtSP.Rows)
            {
                cboSp.Items.Add(new ListItem(dr.Sp_Short_Name.ToString() + " :: " + dr.Sp_Full_Name.ToString(), dr.Sp_Ref.ToString()));
            }
            cboSp.Items.Insert(0, new ListItem("---All---", "0"));

            txtFromDate.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
        }

        protected void btnSendSmsDSM_Click(object sender, EventArgs e)
        {
            var dsmRef = cboDsm.SelectedValue.ToString();
            var stDate = new DateTime(Convert.ToDateTime(txtFromDate.Text.Trim()).AddDays(-1).Year, Convert.ToDateTime(txtFromDate.Text.Trim()).AddDays(-1).Month, 1).ToString("dd/MM/yyyy");
            var asOnDate = txtFromDate.Text.Trim();

            string mobNo = "01977669458";
            //string mobNo = "01714038083";//--------CEO SIR

            string dsmName = "", sls = "", coll = "", slsTgt = "0", slsAchv = "0", collTgt = "0", collAchv = "0";

            var taDsm = new tblSalesDsmMasTableAdapter();
            var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(dsmRef));
            if (dtDsm.Rows.Count > 0)
            {
                //mobNo = dtDsm[0].Dsm_Cell_No.ToString();
                dsmName = dtDsm[0].Dsm_Short_Name.ToString();

                string constr = ConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString;

                #region Sales
                using (SqlConnection con = new SqlConnection(constr))
                {
                    var sqlQry = "SELECT SUM(Trn_Det_Lin_Qty) AS TotSls FROM tbl_InTr_Trn_Hdr LEFT OUTER JOIN tbl_InTr_Trn_Det " +
                                 "ON tbl_InTr_Trn_Hdr.Trn_Hdr_Ref = tbl_InTr_Trn_Det.Trn_Hdr_Ref LEFT OUTER JOIN tblSalesPartyAdr " +
                                 "ON Trn_Hdr_Pcode = Par_Adr_Ref  WHERE Trn_Hdr_Code in ('SAL') AND convert(date,Trn_Hdr_Date,103) " +
                                 "between convert(date,'" + stDate + "',103) and convert(date,'" + asOnDate + "',103) " +
                                 "and Par_Adr_Sls_Per='" + dsmRef + "'GROUP BY Par_Adr_Sls_Per";

                    using (SqlCommand cmd = new SqlCommand(sqlQry))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                sls = dt.Rows[0]["TotSls"].ToString();
                            }
                        }
                    }
                }
                #endregion

                #region Collection
                using (SqlConnection con = new SqlConnection(constr))
                {
                    var sqlQry = "SELECT Dsm_Ref,SUM(CASE Trn_Trn_type WHEN 'C' THEN Trn_Amount ELSE 0 END) AS TotCol " +
                                 "FROM dbo.tbl_Acc_Fa_Te left outer join tblSalesPartyAdr on Trn_Ac_Code=Par_Adr_Acc_Code " +
                                 "left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref WHERE dbo.tbl_Acc_Fa_Te.Trn_Flag = 'RJV' " +
                                 "and Trn_Trn_type='C' AND dbo.tbl_Acc_Fa_Te.Trn_Narration <> 'Dummy Payment Receive' " +
                                 "AND CONVERT(date, dbo.tbl_Acc_Fa_Te.Trn_DATE, 103) BETWEEN CONVERT(datetime,'" + stDate + "',103) " +
                                 "AND CONVERT(datetime,'" + asOnDate + "',103) and dbo.tblSalesDsmMas.Dsm_Ref ='" + dsmRef + "' group by Dsm_Ref";

                    using (SqlCommand cmd = new SqlCommand(sqlQry))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                coll = Convert.ToDouble(dt.Rows[0]["TotCol"]).ToString("N2");
                            }
                        }
                    }
                }
                #endregion

                #region Sales Target
                using (SqlConnection con = new SqlConnection(constr))
                {
                    var sqlQry = "select Trgt_Dsm_Ref,SUM(Trgt_Qty) as SlsTgt from tblSalesTarget where Trgt_Dsm_Ref ='" + dsmRef + "' "
                                 + "and Trgt_Month='" + Convert.ToDateTime(asOnDate).Month.ToString("00") + "' and Trgt_Year='" + Convert.ToDateTime(asOnDate).Year
                                 + "' and Trgt_Item_Ref<>'100000' group by Trgt_Dsm_Ref";

                    using (SqlCommand cmd = new SqlCommand(sqlQry))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                slsTgt = dt.Rows[0]["SlsTgt"].ToString();
                            }
                        }
                    }
                }
                #endregion

                if (Convert.ToDouble(slsTgt) > 0)
                    slsAchv = ((Convert.ToDouble(sls) / Convert.ToDouble(slsTgt)) * 100).ToString("N2");

                #region Collection Target
                using (SqlConnection con = new SqlConnection(constr))
                {
                    var sqlQry = "select Trgt_Dsm_Ref,SUM(Trgt_Qty) as ColTgt from tblSalesTarget where Trgt_Dsm_Ref ='" + dsmRef + "' "
                                 + "and Trgt_Month='" + Convert.ToDateTime(asOnDate).Month.ToString("00") + "' and Trgt_Year='" + Convert.ToDateTime(asOnDate).Year
                                 + "' and Trgt_Item_Ref='100000' group by Trgt_Dsm_Ref";

                    using (SqlCommand cmd = new SqlCommand(sqlQry))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                collTgt = dt.Rows[0]["ColTgt"].ToString();
                            }
                        }
                    }
                }
                #endregion

                if (Convert.ToDouble(collTgt) > 0)
                    collAchv = ((Convert.ToDouble(coll) / Convert.ToDouble(collTgt)) * 100).ToString("N2");

                //Procedure one             
                String URI = "http://sms.sslwireless.com/pushapi/dynamic/server.php";
                String sid = "sevenhorse";
                String user = "sevenhorse";
                String pass = "123456";

                String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=88" + mobNo + "&sms[0][1]="
                                        + System.Web.HttpUtility.UrlEncode("Dear Mr. " + dsmName + ", as on "
                                        + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd-MM-yy") 
                                        + " SLS-" + sls + ", COLL-" + coll + ", SLS.TGT-" + slsTgt + ", ACHV-"
                                        + slsAchv + "%, COLL.TGT-" + collTgt + ", ACHV-" + collAchv + "%.(*as per ERP data* <Test SMS>)")
                                        + "&sms[0][2]=" + "1234567890" + "&sid=" + sid;

                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                    string HtmlResult = wc.UploadString(URI, myParameters);

                    Console.Write(HtmlResult);
                }


                //Procedure two
                //String sidNew = "STAKEHOLDER";
                //String userNew = "USER NAME";
                //String passNew = "PASSWORD";
                //string URINew = "http://sms.sslwireless.com/pushapi/dynamic/server.php";

                //string myParametersNew = "user=" + userNew + "&pass=" + passNew + "&sms[0][0]=8801913900620&sms[0][1]=" +
                //    System.Web.HttpUtility.UrlEncode("Test sms 2") + "&sms[0][2]=123456789&sms[1][0]=8801913900620&sms[1][1]=" +
                //    System.Web.HttpUtility.UrlEncode("Test sms 2") + "&sms[1][2]=123456790&sid=" + sidNew;

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URINew);
                //byte[] data = Encoding.ASCII.GetBytes(myParameters);
                //request.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded";
                //request.ContentLength = data.Length;
                //using (Stream stream = request.GetRequestStream())
                //{
                //    stream.Write(data, 0, data.Length);
                //}
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //String responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //Response.Write(responseString);

                //Mobile Number sms[0][0] This is the mobile phone number of the customer/user to receive the SMS message. 
                //Numeric 13 Mandatory 

                //Message Body sms[0][1] This is the SMS message body to be received by the customer/user. 
                //Alphanumeric 1-450 Mandatory 

                //Client Ref ID sms[0][2] For each sms there will be unique reference ID from Client 
                //Alphanumeric 1-25 Mandatory 
            }
        }

        protected void btnSendSmsSP_Click(object sender, EventArgs e)
        {
            //bd.com.mobireach.user.SmsControllerService service = new bd.com.mobireach.user.SmsControllerService();
            //var smsID = service.SendTextMessage("eci", "Abcd@321", "8801714038083", "8801977669458", "Dear Mr. Rishad, as on 15-10-17 SLS-5452, COLL-1730000, SLS.TGT-12000, ACHV-45%, COLL.TGT-3555000, ACHV-48%.(*as per ERP data* <Test SMS>)");
            //var smsMultiID = service.SendTextMessage("eci", "Abcd@321", "8801847121242", "8801977669458,8801711118800", "Test SMS from ECIL API");
            //Dhaka North
            /*
                select * from View_Sales_Do_Chln left outer join tblSalesPersonMas
                on  SO_Hdr_Com4=Sp_Ref
                where CONVERT(datetime,Trn_Hdr_Date,103) 
                between CONVERT(datetime,'01/09/2017',103) and CONVERT(datetime,GETDATE(),103) 
                and  SO_Hdr_Com4='7'

                select * from tblSalesDsmMas

                select * from tblSalesPersonMas

                select * from tblSalesPartyAdr 
             */

            //Procedure one             
            String sid = "sevenhorse";
            String user = "sevenhorse";
            String pass = "123456";

            String URI = "http://sms.sslwireless.com/pushapi/dynamic/server.php";
            String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=8801711118800&sms[0][1]="
                                   + System.Web.HttpUtility.UrlEncode("Dear Mr. Rishad, as on 17-10-17 SLS-5452, COLL-1730000, SLS.TGT-12000, ACHV-45%, COLL.TGT-3555000, ACHV-48%.(*as per ERP data* <Test SMS>)")
                                    + "&sms[0][2]=" + "1234567890" + "&sid=" + sid;

            //var str = @"Bgivb";
            //String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=8801711118800&sms[0][1]="
            //                       + System.Web.HttpUtility.UrlEncode(convertBanglatoUnicode(str)); 
            

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                string HtmlResult = wc.UploadString(URI, myParameters);

                Console.Write(HtmlResult);
            }

            //Procedure two
            //String sidNew = "STAKEHOLDER";
            //String userNew = "USER NAME";
            //String passNew = "PASSWORD";
            //string URINew = "http://sms.sslwireless.com/pushapi/dynamic/server.php";

            //string myParametersNew = "user=" + userNew + "&pass=" + passNew + "&sms[0][0]=8801913900620&sms[0][1]=" +
            //    System.Web.HttpUtility.UrlEncode("Test sms 2") + "&sms[0][2]=123456789&sms[1][0]=8801913900620&sms[1][1]=" +
            //    System.Web.HttpUtility.UrlEncode("Test sms 2") + "&sms[1][2]=123456790&sid=" + sidNew;

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URINew);
            //byte[] data = Encoding.ASCII.GetBytes(myParameters);
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;
            //using (Stream stream = request.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //String responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //Response.Write(responseString);

            //Mobile Number sms[0][0] This is the mobile phone number of the customer/user to receive the SMS message. 
            //Numeric 13 Mandatory 

            //Message Body sms[0][1] This is the SMS message body to be received by the customer/user. 
            //Alphanumeric 1-450 Mandatory 

            //Client Ref ID sms[0][2] For each sms there will be unique reference ID from Client 
            //Alphanumeric 1-25 Mandatory 
        }

        public string convertBanglatoUnicode(string banglaText) 
        { StringBuilder sb = new StringBuilder(); 
            foreach (char c in banglaText) 
            { 
                sb.AppendFormat("{1:x4}", c, (int)c); 
            } 
            string unicode = sb.ToString().ToUpper(); 
            return unicode; 
        } 

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ListBox1.Items.Count > 0)
            {
                for (int i = 0; i < ListBox1.Items.Count; i++)
                {
                    if (ListBox1.Items[i].Selected)
                        ListBox2.Items.Add(new ListItem(ListBox1.Items[i].Text, ListBox1.Items[i].Value.ToString()));
                }
            }

            if (ListBox1.Items.Count > 0)
            {
                for (int i = ListBox1.Items.Count - 1; i >= 0; i--)
                {
                    if (ListBox1.Items[i].Selected)
                        ListBox1.Items.RemoveAt(ListBox1.Items.IndexOf(ListBox1.Items[i]));
                }
            }

            List<ListItem> list1 = new List<ListItem>();
            foreach (ListItem li in ListBox1.Items)
            {
                list1.Add(li);
            }
            List<ListItem> sorted1 = list1.OrderBy(x => x.Text).ToList();
            ListBox1.Items.Clear();
            foreach (ListItem li in sorted1)
            {
                ListBox1.Items.Add(li);
            }

            List<ListItem> list2 = new List<ListItem>();
            foreach (ListItem li in ListBox2.Items)
            {
                list2.Add(li);
            }
            List<ListItem> sorted2 = list2.OrderBy(x => x.Text).ToList();
            ListBox2.Items.Clear();
            foreach (ListItem li in sorted2)
            {
                ListBox2.Items.Add(li);
            }        
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ListBox2.Items.Count > 0)
            {
                for (int i = 0; i < ListBox2.Items.Count; i++)
                {
                    if (ListBox2.Items[i].Selected)
                        ListBox1.Items.Add(new ListItem(ListBox2.Items[i].Text, ListBox2.Items[i].Value.ToString()));
                }
            }

            if (ListBox2.Items.Count > 0)
            {
                for (int i = ListBox2.Items.Count - 1; i >= 0; i--)
                {
                    if (ListBox2.Items[i].Selected)
                        ListBox2.Items.RemoveAt(ListBox2.Items.IndexOf(ListBox2.Items[i]));
                }
            }

            List<ListItem> list1 = new List<ListItem>();
            foreach (ListItem li in ListBox1.Items)
            {
                list1.Add(li);
            }
            List<ListItem> sorted1 = list1.OrderBy(x => x.Text).ToList();
            ListBox1.Items.Clear();
            foreach (ListItem li in sorted1)
            {
                ListBox1.Items.Add(li);
            }

            List<ListItem> list2 = new List<ListItem>();
            foreach (ListItem li in ListBox2.Items)
            {
                list2.Add(li);
            }
            List<ListItem> sorted2 = list2.OrderBy(x => x.Text).ToList();
            ListBox2.Items.Clear();
            foreach (ListItem li in sorted2)
            {
                ListBox2.Items.Add(li);
            }        
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://gpcmp.grameenphone.com/gpcmpapi/messageplatform/controller.home?username=Ecement380&password=D0r33n12$&apicode=1&msisdn=01711110088&countrycode=880&cli=DOREEN&messagetype=1&message=CMPTestMessage&messageid=0");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //Procedure one             
            String sid = "7HorseSuprmBng";
            String user = "sevenhorse";
            String pass = "123456";

            String URI = "http://sms.sslwireless.com/pushapi/dynamic/server.php";
            //String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=8801711118800&sms[0][1]="
            //                       + System.Web.HttpUtility.UrlEncode("Dear Mr. Rishad, as on 17-10-17 SLS-5452, COLL-1730000, SLS.TGT-12000, ACHV-45%, COLL.TGT-3555000, ACHV-48%.(*as per ERP data* <Test SMS>)")
            //                        + "&sms[0][2]=" + "1234567890" + "&sid=" + sid;

            String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=8801711118800&sms[0][1]="
                                   + System.Web.HttpUtility.UrlEncode(convertBanglatoUnicode("" + " ECIL-w876532"))
                                   +"&sms[0][2]=" + "1234567890" + "&sid=" + sid;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                string HtmlResult = wc.UploadString(URI, myParameters);

                Console.Write(HtmlResult);
            }
        }      
    }
}