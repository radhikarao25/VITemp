<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AttendanceWebApp.Login" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,800italic,400,700,800">
    <link type="text/css" rel="stylesheet" href="http://fonts.googleapis.com/css?family=Oswald:400,700,300">
    <link type="text/css" rel="stylesheet" href="styles/font-awesome.min.css">
    <link type="text/css" rel="stylesheet" href="styles/bootstrap.min.css">
    <link type="text/css" rel="stylesheet" href="styles/animate.css">
    <link type="text/css" rel="stylesheet" href="styles/all.css">
    <link type="text/css" rel="stylesheet" href="styles/main.css">
    <link type="text/css" rel="stylesheet" href="styles/style-responsive.css">
</head>
<body style="background: url('images/bg/bg.png');">
    <form id="form1" runat="server">
    <div class="page-form">
        <div class="panel panel-blue">
            <div class="panel-body pan">
                <form action="#" class="form-horizontal">
                <div class="form-body pal">
                    <div class="col-md-12 text-center">
                        <img src="images/logo.jpg" width="150px" />
                        <br />
                    </div>
                    <div class="form-group">
                        
                        <div class="col-md-12 text-center">
                            <h3>Please sign in to access the system</h3><br />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputName" class="col-md-3 control-label">
                            Username:</label>
                        <div class="col-md-9">
                            <div class="input-icon right">
                                <i class="fa fa-user"></i>
                                <asp:TextBox id="inputName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputPassword" class="col-md-3 control-label">
                            Password:</label>
                        <div class="col-md-9">
                            <div class="input-icon right">
                                <i class="fa fa-lock"></i>
                                <asp:TextBox id="inputPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mbn">
                        <div class="col-lg-12" align="right">
                            <div class="form-group mbn">
                                <div class="col-lg-3">
                                    &nbsp;
                                </div>
                                <div class="col-lg-9">
                                    <asp:Button CssClass="btn btn-default" OnClick="Login_Click" ID="SignIN" runat="server" Text="Sign In"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group ">
                        <div class="col-lg-12" >
                            <div class="form-group mbn">
                                <div class="col-lg-12" style="text-align:center; font-size:14pt;">
                                    <asp:Label ID="errorLabel" runat="server" CssClass="alert-danger"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </form>
            </div>
        </div>
        
    </div>
    </form>
</body>
</html>
