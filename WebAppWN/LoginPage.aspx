<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="WebAppWN.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/loginCSS.css" />
    <link href="css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
            <div class="row">
                    <div class="col-lg-6 col-lg-push-3">
                        &nbsp;</div>
            </div>
            <div class="login">
                <div class="login-screen">
                    <div class="app-title">
                        <h1>כניסה למערכת</h1>
                    </div>
                    <div class="login-form">
                        <div class="control-group">
                            <asp:TextBox runat="server" CssClass="login-field" ID="userNameLogin"></asp:TextBox>
                            <label class="login-field-icon fui-user" for="login-name"></label>
                        </div>
                        <div class="control-group">
                            <asp:TextBox runat="server" CssClass="login-field" TextMode="Password" ID="passwordLogin"></asp:TextBox>
                            <label class="login-field-icon fui-lock" for="login-pass"></label>
                        </div>
                        <asp:LinkButton CssClass="btn btn-primary btn-large btn-block" runat="server" OnClick="loginButtonClick" ID="loginButton">התחבר</asp:LinkButton>
                       
                    </div>
                    <div style="text-align:center">
                      <asp:Label runat="server" Visible="false" ID="ErrorLabel" Text="מספר הזהות שגוי או שאינו קיים במערכת" ForeColor="Red"></asp:Label>
                </div>
                    </div>
            </div>
        </form>
    </div>
</body>
</html>
