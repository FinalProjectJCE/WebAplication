<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="WebAppWN.LoginPage"%>

<!DOCTYPE html>
<html lang="en">
	<head>
		<meta http-equiv="content-type" content="text/html; charset=UTF-8">
		<meta charset="utf-8">
		<title>Bootstrap Login Form</title>
		<meta name="generator" content="Bootply" />
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
		<link href="css/bootstrap.min.css" rel="stylesheet">
		<!--[if lt IE 9]>
			<script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
		<![endif]-->
		<link href="css/styles.css" rel="stylesheet">
	</head>
	<body>
<!--login modal-->
<div id="loginModal" class="modal show" tabindex="-1" role="dialog" aria-hidden="true">
  <div class="modal-dialog">
  <div class="modal-content">
      <div class="modal-header">
          <!--
          <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
          -->
          <h1 class="text-center">כניסה למערכת</h1>
      </div>
      <div class="modal-body">
          <form class="form col-md-12 center-block">
            <div class="form-group">
              <input type="text" class="form-control input-lg text-right" ID="OfficeLogin" placeholder="מזהה סניף">
            </div>
            <div class="form-group">
              <input type="password" class="form-control input-lg text-right" ID="PassLogin" placeholder="סיסמא">
            </div>
       
            <div class="form-group">  
                
                <!--<button OnClick="Button1_Click" runat="server" ID="loginButton1111" class="btn btn-primary btn-lg btn-block">התחבר</button>-->
                <div>
                <form id="formi" runat="server">
                    <asp:LinkButton CssClass="btn btn-primary btn-large btn-block" runat="server" OnClick="Button1_Click" ID="loginButton1">התחבר</asp:LinkButton>
                </form>
                </div>
                    <div style="text-align:center">
                      <asp:Label runat="server" Visible="false" ID="ErrorLabel" Text="מספר הזהות שגוי או שאינו קיים במערכת" ForeColor="Red"></asp:Label>
                </div>
                <!--
              <span class="pull-right"><a href="#">Register</a></span><span><a href="#">Need help?</a></span>
                -->
            </div>
          </form>
                    
      </div>
      <div class="modal-footer">
          <div class="col-md-12">
          <!--button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button-->
		  </div>	
      </div>
  </div>
  </div>
</div>
	<!-- script references -->
		<script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
		<script src="js/bootstrap.min.js"></script>
	</body>
</html>