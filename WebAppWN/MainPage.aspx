<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebAppWN.MainPage" %>
<!--<meta http-equiv="refresh" content="15" />-->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/mainPage.css" />

    <script type="text/javascript">
        
        var clicked = false;
        
        function CheckBrowser() {
            if (clicked == false) {
                //Browser closed   
            } else {
                //redirected
                clicked = false;
            }
        }
        function bodyUnload() {
            if (clicked == false)//browser is closed  
            {
                var request = GetRequest();
                request.open("POST", "../LogOut.aspx", false);
                request.send();
            }
        }

        function GetRequest() {
            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            return xmlhttp;
        }

</script>

    <script language="JavaScript">
        window.onbeforeunload = confirmExit;
        function confirmExit() {
            return "האם הנך בטוח שברצונך להתנתק?";
        }
</script>

    

</head>
<body onunload="bodyUnload();" onclick="clicked=true;" style="background-image:url(img/background.jpg); background-size:cover;">

    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer runat="server" id="UpdateTimer" interval="2000" ontick="UpdateTimer_Tick" />
        <asp:Timer runat="server" id="Timer1" interval="2000" ontick="TTT" />

        <asp:UpdatePanel runat="server" id="TimedPanel" updatemode="Conditional">
            
            <Triggers>
                <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
                <asp:AsyncPostBackTrigger controlid="ImageButton1" eventname="Click" />
                <asp:AsyncPostBackTrigger controlid="Timer1" eventname="Tick" />

            </Triggers>

            <ContentTemplate>
               <div id="displayText">
               <br />
               <asp:Label runat="server" Visible="true" ID="BusinessNameLabel" Text="Business Name" ForeColor="Black" font-size ="50pt"></asp:Label>
               <h1> : לקוח מספר</h1>
               <asp:Label runat="server" Visible="true" ID="QueueNumberLabel" Text="" ForeColor="Black" font-size ="60pt"></asp:Label>
               <br />
               <br />
               <br />
               <asp:ImageButton ID="ImageButton1" runat="server" Height="194px" ImageUrl="~/img/ArrowButtonImg.png" OnClick="UpdateButton_Click"/>
               <br />
               <asp:Label runat="server" Visible="true" ID="hasOneInQueue" Text="" ForeColor="Black" font-size ="40pt"></asp:Label>
               <br />
               <asp:Label runat="server" Visible="true" ID="StartAndStopLabel" Text="" ForeColor="Red"></asp:Label>
               <br />

                 לחץ על הכתפור על מנת להתחיל או לסיים טיפול בלקוח</div>
               <asp:Button ID="Button1" runat="server" font-size ="15pt" OnClick="Restart_Click" Text="סוף יום עבודה" Height="49px" OnClientClick="return confirm('האם אתה בטוח שברצונך לאפס את נתוני התור?');"/>
                <asp:Label runat="server" ID="ServingClients" Text="false" Visible="false" ></asp:Label>
                </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
