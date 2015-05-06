<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebAppWN.MainPage" %>
<meta http-equiv="refresh" content="5" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/mainPage.css" />
</head>
<body style="background-image:url(img/background.jpg); background-size:cover;">
    <form id="form1" runat="server">
    <div id="displayText">
        <br />
        <asp:Label runat="server" Visible="true" ID="SessionLabel" Text="Haliji" ForeColor="Black" font-size ="50pt"></asp:Label>
   <h1> : לקוח מספר</h1>
       <!-- <%int curr=new WebAppWN.DbDAL().getCurr(Convert.ToInt16( Session["business"])); %> -->
      <!--  <h1><%=curr %></h1>-->
             <asp:Label runat="server" Visible="true" ID="QUEUE" Text="" ForeColor="Black" font-size ="60pt"></asp:Label>
        <br />
        <br />
        <br />
        <asp:ImageButton ID="ImageButton1" runat="server" Height="194px" ImageUrl="~/img/ArrowButtonImg.png" OnClick="ImageButton1_Click" />
        <br />
                     <asp:Label runat="server" Visible="true" ID="hasOneInQueue" Text="" ForeColor="Black" font-size ="40pt"></asp:Label>
        <br />
            <asp:Label runat="server" Visible="true" ID="StartAndStopLabel" Text="" ForeColor="Red"></asp:Label>
        <br />

        לחץ על הכתפור על מנת לקדם תור</div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </form>
</body>
</html>
