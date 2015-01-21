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
        <%int curr=new WebAppWN.DbDAL().getCurr(); %>
        <h1><%=curr %></h1>
        <br />
        <asp:ImageButton ID="ImageButton1" runat="server" Height="194px" ImageUrl="~/img/ArrowButtonImg.png" OnClick="ImageButton1_Click" />
        <br />
        <br />
        לחץ על הכתפור על מנת לקדם תור</div>
    </form>
</body>
</html>
