<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebAppWN.MainPage" %>
<!--<meta http-equiv="refresh" content="15" />-->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/mainPage.css" />

    

</head>
<body style="background-image:url(img/background.jpg); background-size:cover;">

    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer runat="server" id="UpdateTimer" interval="2000" ontick="UpdateTimer_Tick" />
        <asp:UpdatePanel runat="server" id="TimedPanel" updatemode="Conditional">
            
            <Triggers>
                <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
                <asp:AsyncPostBackTrigger controlid="ImageButton1" eventname="Click" />

            </Triggers>

            <ContentTemplate>
               <div id="displayText">
               <br />
               <asp:Label runat="server" Visible="true" ID="SessionLabel" Text="Haliji" ForeColor="Black" font-size ="50pt"></asp:Label>
               <h1> : לקוח מספר</h1>
               <asp:Label runat="server" Visible="true" ID="QUEUE" Text="" ForeColor="Black" font-size ="60pt"></asp:Label>
               <br />
               <br />
               <br />
               <asp:ImageButton ID="ImageButton1" runat="server" Height="194px" ImageUrl="~/img/ArrowButtonImg.png" OnClick="UpdateButton_Click"/>
               <br />
               <asp:Label runat="server" Visible="true" ID="hasOneInQueue" Text="" ForeColor="Black" font-size ="40pt"></asp:Label>
               <br />
               <asp:Label runat="server" Visible="true" ID="StartAndStopLabel" Text="" ForeColor="Red"></asp:Label>
               <br />

                 לחץ על הכתפור על מנת לקדם תור</div>
               <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                <asp:Label runat="server" ID="catchedQueueNum" Text="0" Visible="false" ></asp:Label>
                </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
