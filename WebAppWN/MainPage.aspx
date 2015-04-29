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
        <asp:Label runat="server" Visible="false" ID="SessionLabel" Text="Haliji" ForeColor="Red"></asp:Label>
   <h1> : לקוח מספר</h1>
        <%int curr=new WebAppWN.DbDAL().getCurr(Convert.ToInt16( Session["business"])); %>
        <h1><%=curr %></h1>
        <br />
        <asp:ImageButton ID="ImageButton1" runat="server" Height="194px" ImageUrl="~/img/ArrowButtonImg.png" OnClick="ImageButton1_Click" />
        <br />
            <%bool dis=new WebAppWN.DbDAL().noOneInQueue();
              if (dis) 
              {%>
              <h1>אין אף לקוח בתור</h1>
              <%}
                else
                { %>
            <h1>יש לקוחות</h1>
                <%}
                   %>
        <br />
        לחץ על הכתפור על מנת לקדם תור</div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </form>
</body>
</html>
