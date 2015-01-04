<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebAppWN.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/mainPage.css" />
</head>
<body style="background-image:url(img/background.jpg); background-size:cover;">
    <form id="form1" runat="server">
    <div id="displayText">
   <h1> : לקוח מספר</h1>
        <asp:Label runat="server" ID="clientNum"></asp:Label>
    </div>
    </form>
</body>
</html>
