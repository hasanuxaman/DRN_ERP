<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmEmpList.aspx.cs" Inherits="DRN_WEB_ERP.frmEmpList" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Width="100%"
        InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" Height="100%">
        <LocalReport ReportPath="empList.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
        
        SelectCommand="SELECT EmpRefNo, EmpId, EmpName, EmpFatherName, EmpMotherName, EmpGender, GenderCode, GenderName, EmpMaritalStatus, MaritalStatusCode, MaritalStatusName, EmpSpouse, EmpDOB, EmpPOB, EmpIsAct, EmpInactDate, EmpSetlFlag, EmpSetlDate, EmpEntryDate, EmpEntryUser, EmpUpdateDate, EmpUpdateUser, EmpMasExtData1, EmpMasExtData2, EmpMasExtData3, EmpStatus, EmpFlag, EmpAdrRefNo, EmpCurAdrHouseRoad, EmpCurAdrPO, EmpCurAdrPOCode, EmpCurAdrThana, EmpCurAdrDist, EmpPermAdrHouseRoad, EmpPermAdrPO, EmpPermAdrPOCode, EmpPermAdrThana, EmpPermAdrDist, EmpMailAdrHouseRoad, EmpMailAdrPO, EmpMailAdrPOCode, EmpMailAdrThana, EmpMailAdrDist, EmpEmerCPName, EmpEmerCPRelation, EmpEmerCPHouseRoad, EmpEmerCPPO, EmpEmerCPPOCode, EmpEmerCPThana, EmpEmerCPDist, EmpEmerCPPhone, EmpEmerCPCell, EmpAdrStatus, EmpAdrFlag, EmpExtRefNo, EmpReligion, ReligionCode, ReligionName, EmpNationality, EmpBloodgrp, BldGrpCode, BldGrpName, EmpHomephone, EmpCellPhone, EmpPerEmail, EmpIdentiMark, EmpHeight, EmpWeight, EmpColor, EmpNID, EmpTIN, EmpDLNo, EmpDLIssFrom, EmpDLExpDate, EmpPasNo, EmpPasIssFrom, EmpPasIssDate, EmpPasExprDate, EmpExtData1, EmpExtData2, EmpExtData3, EmpOffRefNo, CompRefNo, CompCode, CompName, CompAddr, OffLocRefNo, LocCode, LocName, LocAddr, DeptRefNo, DeptCode, DeptName, DeptRem, SecRefNo, SecCode, SecName, SecRem, DesigRefNo, DesigCode, DesigName, DesigDesc, DesigRem, EmpDOJ, EmpSuprId, EmpJobStatus, JobStatCode, JobStatName, JobStatRem, EmpConfDueDate, EmpConfDate, EmpType, EmpTypeCode, EmpTypeName, EmpTypeRem, EmpCardId, ShiftRefNo, ShiftCode, ShiftName, ShiftDesc, ShiftRem, EmpGrade, GrdDefCode, GrdDefName, EmpSalary, BankAccRef, BankAccNo, EmpOffEmail, EmpWorkPhone, EmpPabxNo, EmpIpPhone, EmpRem, EmpOffExtData1, EmpOffExtData2, EmpOffExtData3 FROM View_Emp_Basc WHERE (EmpSetlFlag = 'N')"></asp:SqlDataSource>
    </asp:Content>
