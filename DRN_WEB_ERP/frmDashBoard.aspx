<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmDashBoard.aspx.cs" Inherits="DRN_WEB_ERP.frmDashBoard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        h2
        {
            margin-left: 5px;
        }
        
        .clearfix:after
        {
            content: ".";
            display: block;
            clear: both;
            visibility: hidden;
            line-height: 0;
            height: 0;
        }
        
        .clearfix
        {
            display: inline-block;
        }
        
        .box
        {
            float: left;
            width: 510px;
            margin: 5px;
            padding: 10px;
            border: 1px solid #ccc;
        }
    </style>
    <div align="center">
        Chart Type:
        <asp:DropDownList ID="cboChartType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboChartType_SelectedIndexChanged">
            <asp:ListItem>Point</asp:ListItem>
            <asp:ListItem>FastPoint</asp:ListItem>
            <asp:ListItem>Bubble</asp:ListItem>
            <asp:ListItem>Line</asp:ListItem>
            <asp:ListItem>Spline</asp:ListItem>
            <asp:ListItem>StepLine</asp:ListItem>
            <asp:ListItem>FastLine</asp:ListItem>
            <asp:ListItem>Bar</asp:ListItem>
            <asp:ListItem>StackedBar</asp:ListItem>
            <asp:ListItem>Column</asp:ListItem>
            <asp:ListItem>StackedColumn</asp:ListItem>
            <asp:ListItem>Area</asp:ListItem>
            <asp:ListItem>SplineArea</asp:ListItem>
            <asp:ListItem>StackedArea</asp:ListItem>
            <asp:ListItem Selected="True">Pie</asp:ListItem>
            <asp:ListItem>Doughnut</asp:ListItem>
            <asp:ListItem>Stock</asp:ListItem>
            <asp:ListItem>Candlestick</asp:ListItem>
            <asp:ListItem>Range</asp:ListItem>
            <asp:ListItem>SplineRange</asp:ListItem>
            <asp:ListItem>RangeBar</asp:ListItem>
            <asp:ListItem>RangeColumn</asp:ListItem>
            <asp:ListItem>Radar</asp:ListItem>
            <asp:ListItem>Polar</asp:ListItem>
            <asp:ListItem>ErrorBar</asp:ListItem>
            <asp:ListItem>BoxPlot</asp:ListItem>
            <asp:ListItem>Renko</asp:ListItem>
            <asp:ListItem>ThreeLineBreak</asp:ListItem>
            <asp:ListItem>Kagi</asp:ListItem>
            <asp:ListItem>PointAndFigure</asp:ListItem>
            <asp:ListItem>Funnel</asp:ListItem>
            <asp:ListItem>Pyramid</asp:ListItem>
        </asp:DropDownList>
        <asp:CheckBox ID="cbUse3D" runat="server" AutoPostBack="True" Text="Use 3D Chart"
            Checked="true" OnCheckedChanged="cbUse3D_CheckedChanged" />
    </div>
    <div class="clearfix">
        <div style="float: left; width: 540px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="box">
                        <h3>
                            Sales:
                            <asp:DropDownList ID="cboSalesDate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSalesDate_SelectedIndexChanged">
                                <asp:ListItem Value="1">Today</asp:ListItem>
                                <asp:ListItem Value="2">Yesterday</asp:ListItem>
                                <asp:ListItem Value="3">Last 3 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="4">Last 7 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="5">Last 15 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="6">Last Month</asp:ListItem>
                                <asp:ListItem Value="7">Custom Date</asp:ListItem>
                            </asp:DropDownList>
                        </h3>
                        <asp:Chart ID="Chart1" runat="server" Width="510">
                            <Series>
                                <asp:Series Name="Sales">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="true">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cboSalesDate" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="box">
                        <h3>
                            Collection:
                            <asp:DropDownList ID="cboCollectionDate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboCollectionDate_SelectedIndexChanged">
                                <asp:ListItem Value="1">Today</asp:ListItem>
                                <asp:ListItem Value="2">Yesterday</asp:ListItem>
                                <asp:ListItem Value="3">Last 3 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="4">Last 7 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="5">Last 15 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="6">Last Month</asp:ListItem>
                                <asp:ListItem Value="7">Custom Date</asp:ListItem>
                            </asp:DropDownList>
                        </h3>
                        <asp:Chart ID="Chart2" runat="server" Width="510">
                            <Series>
                                <asp:Series Name="Collection">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="true">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cboCollectionDate" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div style="float: right; width: 540px;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="box">
                        <h3>
                            Delivery:
                            <asp:DropDownList ID="cboDeliveryDate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboDeliveryDate_SelectedIndexChanged">
                                <asp:ListItem Value="1">Today</asp:ListItem>
                                <asp:ListItem Value="2">Yesterday</asp:ListItem>
                                <asp:ListItem Value="3">Last 3 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="4">Last 7 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="5">Last 15 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="6">Last Month</asp:ListItem>
                                <asp:ListItem Value="7">Custom Date</asp:ListItem>
                            </asp:DropDownList>
                        </h3>
                        <asp:Chart ID="Chart3" runat="server" Width="510">
                            <Series>
                                <asp:Series Name="Delivery">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="true">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cboDeliveryDate" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="box">
                        <h3>
                            Sales vs Delivery Treand:
                            <asp:DropDownList ID="cboSalesTreand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSalesTreand_SelectedIndexChanged">
                                <asp:ListItem Value="1">Today</asp:ListItem>
                                <asp:ListItem Value="2">Yesterday</asp:ListItem>
                                <asp:ListItem Value="3">Last 3 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="4">Last 7 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="5">Last 15 Day&#39;s</asp:ListItem>
                                <asp:ListItem Value="6">Last Month</asp:ListItem>
                                <asp:ListItem Value="7">Custom Date</asp:ListItem>
                            </asp:DropDownList>
                        </h3>
                        <asp:Chart ID="Chart4" runat="server" Width="510">
                            <Series>
                                <asp:Series Name="Sales">
                                </asp:Series>
                                <asp:Series Name="Delivery">
                                </asp:Series>
                                <%--<asp:Series Name="Collection">
                                </asp:Series>--%>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="true">
                                    <Area3DStyle Enable3D="True" />
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cboSalesTreand" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
