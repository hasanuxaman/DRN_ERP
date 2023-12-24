using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP
{
    public partial class frmDashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            cboSalesDate.SelectedIndex = 3;
            cboDeliveryDate.SelectedIndex = 3;
            cboCollectionDate.SelectedIndex = 3;
            cboSalesTreand.SelectedIndex = 3;

            GetSalesData();
            GetDeliveryData();
            GetCollectionData();
            GetTrendData();
        }

        protected void cboChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSalesData();
            GetDeliveryData();
            GetCollectionData();
            GetTrendData();
        }

        protected void cbUse3D_CheckedChanged(object sender, EventArgs e)
        {
            GetSalesData();
            GetDeliveryData();
            GetCollectionData();
            GetTrendData();
        }        

        protected void cboSalesDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSalesData();
            GetDeliveryData();
            GetCollectionData();
            GetTrendData();
        }

        protected void cboDeliveryDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSalesData();
            GetDeliveryData();
            GetCollectionData();
            GetTrendData();
        }

        protected void cboCollectionDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSalesData();
            GetDeliveryData();
            GetCollectionData();
            GetTrendData();
        }

        protected void cboSalesTreand_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSalesData();
            GetDeliveryData();
            GetCollectionData();
            GetTrendData();
        }  

        private void GetSalesData()
        {
            try
            {
                // add and format the title
                //Chart1.Titles.Add("Sales Report");
                //Chart1.Titles[0].Font = new Font("Utopia", 16);

                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart1.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = false;
                Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -90;
                Chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;

                //Chart1.Series["Sales"].ChartType = SeriesChartType.Column;
                Chart1.Series["Sales"].ChartTypeName = cboChartType.SelectedValue;
                Chart1.Series["Sales"]["DrawingStyle"] = "Emboss";
                //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart1.ChartAreas[0].Area3DStyle.Enable3D = cbUse3D.Checked;
                Chart1.Series["Sales"].IsValueShownAsLabel = true;

                Legend leg = new Legend();
                Chart1.Legends.Add(leg);

                var salesDate = DateTime.Now;
                if (cboSalesDate.SelectedValue == "1") salesDate = DateTime.Now;
                if (cboSalesDate.SelectedValue == "2") salesDate = DateTime.Now.AddDays(-1);
                if (cboSalesDate.SelectedValue == "3") salesDate = DateTime.Now.AddDays(-2);
                if (cboSalesDate.SelectedValue == "4") salesDate = DateTime.Now.AddDays(-7);
                if (cboSalesDate.SelectedValue == "5") salesDate = DateTime.Now.AddDays(-15);
                if (cboSalesDate.SelectedValue == "6") salesDate = DateTime.Now.AddDays(-30);

                using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString))
                {
                    string CmdString = "SELECT convert(date,SO_Hdr_Date,103) as SO_Hdr_Date,sum(SO_Det_Lin_Qty) as SalesQty FROM View_Sales_Details " +
                        "where convert(date,SO_Hdr_Date,103) between convert(date,'" + salesDate + "',103) and convert(date,'" + DateTime.Now + "',103) " +
                        "and SO_Hdr_HPC_Flag='P' group by convert(date,SO_Hdr_Date,103)";
                    SqlDataAdapter sda = new SqlDataAdapter(CmdString, con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    Chart1.DataSource = ds;
                    //Chart1.Series["Sales"].IsValueShownAsLabel= true;
                    Chart1.Series["Sales"].XValueMember = "SO_Hdr_Date";
                    Chart1.Series["Sales"].YValueMembers = "SalesQty";
                    Chart1.DataBind();
                }
            }
            catch (Exception ex) { }
        }

        private void GetDeliveryData()
        {
            try
            {
                // add and format the title
                //Chart3.Titles.Add("Sales Report");
                //Chart3.Titles[0].Font = new Font("Utopia", 16);

                Chart3.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart3.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = false;
                Chart3.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -90;
                Chart3.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;

                //Chart3.Series["Delivery"].ChartType = SeriesChartType.Column;
                Chart3.Series["Delivery"].ChartTypeName = cboChartType.SelectedValue;
                Chart3.Series["Delivery"]["DrawingStyle"] = "Emboss";
                //Chart3.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart3.ChartAreas[0].Area3DStyle.Enable3D = cbUse3D.Checked;
                Chart3.Series["Delivery"].IsValueShownAsLabel = true;

                Legend leg = new Legend();
                Chart3.Legends.Add(leg);

                var deliveryDate = DateTime.Now;
                if (cboDeliveryDate.SelectedValue == "1") deliveryDate = DateTime.Now;
                if (cboDeliveryDate.SelectedValue == "2") deliveryDate = DateTime.Now.AddDays(-1);
                if (cboDeliveryDate.SelectedValue == "3") deliveryDate = DateTime.Now.AddDays(-2);
                if (cboDeliveryDate.SelectedValue == "4") deliveryDate = DateTime.Now.AddDays(-7);
                if (cboDeliveryDate.SelectedValue == "5") deliveryDate = DateTime.Now.AddDays(-15);
                if (cboDeliveryDate.SelectedValue == "6") deliveryDate = DateTime.Now.AddDays(-30);

                using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString))
                {
                    string CmdString = "SELECT convert(date,Trn_Hdr_Date,103) as Trn_Hdr_Date,sum(DelQty) as DeliveryQty FROM View_Challan_Details " +
                        "where convert(date,Trn_Hdr_Date,103) between convert(date,'" + deliveryDate + "',103) and convert(date,'" + DateTime.Now + "',103) " +
                        "and Trn_Hdr_HRPB_Flag='P' group by convert(date,Trn_Hdr_Date,103)";
                    SqlDataAdapter sda = new SqlDataAdapter(CmdString, con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    Chart3.DataSource = ds;
                    //Chart1.Series["Delivery"].IsValueShownAsLabel= true;
                    Chart3.Series["Delivery"].XValueMember = "Trn_Hdr_Date";
                    Chart3.Series["Delivery"].YValueMembers = "DeliveryQty";
                    Chart3.DataBind();
                }
            }
            catch (Exception ex) { }
        }

        private void GetCollectionData()
        {
            try
            {
                // add and format the title
                //Chart2.Titles.Add("Sales Report");
                //Chart2.Titles[0].Font = new Font("Utopia", 16);

                Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart2.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = false;
                Chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -90;
                Chart2.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;

                //Chart3.Series["Collection"].ChartType = SeriesChartType.Column;
                Chart2.Series["Collection"].ChartTypeName = cboChartType.SelectedValue;
                Chart2.Series["Collection"]["DrawingStyle"] = "Emboss";
                //Chart3.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart2.ChartAreas[0].Area3DStyle.Enable3D = cbUse3D.Checked;
                Chart2.Series["Collection"].IsValueShownAsLabel = true;
                
                Legend leg = new Legend();
                Chart2.Legends.Add(leg);

                var collectionDate = DateTime.Now;
                if (cboCollectionDate.SelectedValue == "1") collectionDate = DateTime.Now;
                if (cboCollectionDate.SelectedValue == "2") collectionDate = DateTime.Now.AddDays(-1);
                if (cboCollectionDate.SelectedValue == "3") collectionDate = DateTime.Now.AddDays(-2);
                if (cboCollectionDate.SelectedValue == "4") collectionDate = DateTime.Now.AddDays(-7);
                if (cboCollectionDate.SelectedValue == "5") collectionDate = DateTime.Now.AddDays(-15);
                if (cboCollectionDate.SelectedValue == "6") collectionDate = DateTime.Now.AddDays(-30);

                using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString))
                {
                    string CmdString = "SELECT convert(date,Trn_DATE,103) as Trn_DATE,sum(Trn_Amount) as Trn_Amount FROM tbl_Acc_Fa_Te " +
                        "where convert(date,Trn_DATE,103) between convert(date,'" + collectionDate + "',103) and convert(date,'" + DateTime.Now + "',103) " +
                        "and Trn_Flag='RJV' and Trn_Trn_type='C' group by convert(date,Trn_DATE,103)";
                    SqlDataAdapter sda = new SqlDataAdapter(CmdString, con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    Chart2.DataSource = ds;
                    Chart2.Series["Collection"].XValueMember = "Trn_DATE";
                    Chart2.Series["Collection"].YValueMembers = "Trn_Amount";
                    Chart2.DataBind();
                }
            }
            catch (Exception ex) { }
        }

        private void GetTrendData()
        {
            try
            {

                Chart4.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart4.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = false;
                Chart4.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -90;
                Chart4.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;

                //Chart4.Series["Sales"].IsValueShownAsLabel = true;
                //Chart4.Series["Delivery"].IsValueShownAsLabel = true;
                //Chart4.Series["Collection"].IsValueShownAsLabel = true;

                Chart4.Series["Sales"].BorderWidth = 2;
                Chart4.Series["Delivery"].BorderWidth = 2;

                Chart4.ChartAreas[0].Area3DStyle.Enable3D = cbUse3D.Checked;

                Legend leg = new Legend();
                Chart4.Legends.Add(leg);

                var trendDate = DateTime.Now;
                if (cboSalesTreand.SelectedValue == "1") trendDate = DateTime.Now;
                if (cboSalesTreand.SelectedValue == "2") trendDate = DateTime.Now.AddDays(-1);
                if (cboSalesTreand.SelectedValue == "3") trendDate = DateTime.Now.AddDays(-2);
                if (cboSalesTreand.SelectedValue == "4") trendDate = DateTime.Now.AddDays(-7);
                if (cboSalesTreand.SelectedValue == "5") trendDate = DateTime.Now.AddDays(-15);
                if (cboSalesTreand.SelectedValue == "6") trendDate = DateTime.Now.AddDays(-30);

                using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString))
                {
                    string CmdSalesString = "SELECT convert(date,SO_Hdr_Date,103) as SO_Hdr_Date,sum(SO_Det_Lin_Qty) as SalesQty FROM View_Sales_Details " +
                        "where convert(date,SO_Hdr_Date,103) between convert(date,'" + trendDate + "',103) and convert(date,'" + DateTime.Now + "',103) " +
                        "and SO_Hdr_HPC_Flag='P' group by convert(date,SO_Hdr_Date,103)";
                    SqlCommand cmd1 = null;
                    cmd1 = new SqlCommand(CmdSalesString, con);
                    cmd1.CommandType = CommandType.Text;

                    SqlDataAdapter myAdapter1 = new SqlDataAdapter();
                    DataSet myDataSet1 = new DataSet();
                    myAdapter1.SelectCommand = cmd1;
                    myAdapter1.Fill(myDataSet1);
                    DataView dataView1 = new DataView(myDataSet1.Tables[0]);

                    string CmdDelString = "SELECT convert(date,Trn_Hdr_Date,103) as Trn_Hdr_Date,sum(DelQty) as DeliveryQty FROM View_Challan_Details " +
                        "where convert(date,Trn_Hdr_Date,103) between convert(date,'" + trendDate + "',103) and convert(date,'" + DateTime.Now + "',103) " +
                        "and Trn_Hdr_HRPB_Flag='P' group by convert(date,Trn_Hdr_Date,103)";
                    SqlCommand cmd2 = null;
                    cmd2 = new SqlCommand(CmdDelString, con);
                    cmd2.CommandType = CommandType.Text;
                 
                    SqlDataAdapter myAdapter2 = new SqlDataAdapter();
                    DataSet myDataSet2 = new DataSet();
                    myAdapter2.SelectCommand = cmd2;
                    myAdapter2.Fill(myDataSet2);
                    DataView dataView2 = new DataView(myDataSet2.Tables[0]);

                    //string CmdRcvString = "SELECT convert(date,Trn_DATE,103) as Trn_DATE,sum(Trn_Amount) as Trn_Amount FROM tbl_Acc_Fa_Te " +
                    //    "where convert(date,Trn_DATE,103) between convert(date,'" + trendDate + "',103) and convert(date,'" + DateTime.Now + "',103) " +
                    //    "and Trn_Flag='RJV' and Trn_Trn_type='C' group by convert(date,Trn_DATE,103)";
                    //SqlCommand cmd3 = null;
                    //cmd3 = new SqlCommand(CmdRcvString, con);
                    //cmd3.CommandType = CommandType.Text;
                 
                    //SqlDataAdapter myAdapter3 = new SqlDataAdapter();
                    //DataSet myDataSet3 = new DataSet();
                    //myAdapter3.SelectCommand = cmd3;
                    //myAdapter3.Fill(myDataSet3);
                    //DataView dataView3 = new DataView(myDataSet3.Tables[0]);

                    Chart4.Series["Sales"].ChartType = SeriesChartType.Line;
                    Chart4.Series["Delivery"].ChartType = SeriesChartType.Line;
                    //Chart4.Series["Collection"].ChartType = SeriesChartType.Line;

                    Chart4.Series[0].Points.DataBindXY(dataView1, "SO_Hdr_Date", dataView1, "SalesQty");
                    Chart4.Series[1].Points.DataBindXY(dataView2, "Trn_Hdr_Date", dataView2, "DeliveryQty");
                    //Chart4.Series[2].Points.DataBindXY(dataView3, "Trn_DATE", dataView3, "Trn_Amount");

                    //Chart4.Series[0].LabelBackColor = System.Drawing.Color.Blue;
                    //Chart4.Series[1].LabelBackColor = System.Drawing.Color.Green;
                    //Chart4.Series[2].LabelBackColor = System.Drawing.Color.Yellow;
                }
            }
            catch (Exception ex) { }
        }        
    }
}