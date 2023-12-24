<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmLogin.aspx.cs" Inherits="DRN_WEB_ERP.frmLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="Javascript">
        function setfocus(utxtbox, ptxtbox) {
            utxtbox.value = utxtbox.value.toUpperCase();
            if (utxtbox.value.length > 9) {
                ptxtbox.focus();
            }
        }
        function setfocus2(utxtbox, ptxtbox) {
            eval("utxtbox.focus()");
        }   
    </script>
    <div id="BodyContent" runat="server" class="loginFrame">
        <h1 style="left: 10px; position: relative;">
            Log In
        </h1>
        <hr />
        <p style="left: 10px; position: relative;">
            Please enter your User Name and Password.
        </p>
        <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="true">
            <LayoutTemplate>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="LoginUserValidationGroup" />
                <div class="accountInfo">
                    <fieldset class="login" style="width: 270px">
                        <legend>Account Information</legend>
                        <p>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                SetFocusOnError="true" CssClass="failureNotification" ErrorMessage="User Name is required."
                                ToolTip="User Name is required." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>--%>
                        </p>
                        <p>
                            <asp:CheckBox ID="RememberMe" runat="server" />
                            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in   </asp:Label>
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" Width="80px"
                                CssClass="inline" ValidationGroup="LoginUserValidationGroup" OnClick="LoginButton_Click" />
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="UserName"
                                CssClass="failureNotification" Display="Dynamic" ErrorMessage="User Name & Password does not match"
                                OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="LoginUserValidationGroup"
                                SetFocusOnError="True">*</asp:CustomValidator>
                        </p>
                    </fieldset>
                </div>
                <br />
                <br />
            </LayoutTemplate>
        </asp:Login>
    </div>
</asp:Content>
