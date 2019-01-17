<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-grid.css" rel="stylesheet" />
    <link href="../Content/bootstrap-reboot.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.bundle.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
    <link href="../CSS/LoginOrRegister.css" rel="stylesheet" />
    <link href="../CSS/ForgotPassword.css" rel="stylesheet" />

    <title>Login</title>


    <script src='https://www.google.com/recaptcha/api.js'></script>
    
</head>
<body>

    <form id="Form1" class="container-fluid" style="height: 100%;" runat="server">

        <div class="row" style="height: 100%;">
            <div class="container-fluid col-sm-6" style="background-size: cover; background-image: url(../Img/ex.jpeg); background-repeat: no-repeat; background-position-x: center; opacity: 0.85; filter: grayscale(30%);">
            </div>

            <div class="container col-sm-6 align-self-center ShiftMedicareSection ">
                <h3 class="text-center mr-sm-5 pb-sm-5 ShiftMedicareTitle">MediCare</h3>

                <div class="form-group ml-sm-5">

                    <asp:Label ID="Label1" runat="server" Text="Username:" CssClass="col-form-label"></asp:Label>

                    <asp:TextBox ID="UsernameField" runat="server" CssClass="form-control col-sm-9 mt-sm-2"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="UsernameValidator" runat="server" ErrorMessage="Please Enter A Valid Username" ControlToValidate="UsernameField" ForeColor="Red"></asp:RequiredFieldValidator>

                </div>

                <div class="form-group ml-sm-5">
                    <asp:Label ID="Label2" runat="server" Text="Password:" CssClass="col-form-label"></asp:Label>

                    <asp:TextBox ID="PasswordField" runat="server" TextMode="Password" CssClass="form-control col-sm-9 mt-sm-2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordValidator" runat="server" ErrorMessage="Please Enter A Valid Password" ControlToValidate="PasswordField" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group ml-sm-5">
                    <asp:Label ID="IncorrectUsernameAndPasswordLabel" runat="server" ForeColor="Red" Text="Incorrect / Invalid Username or Password"></asp:Label>
                    
                </div>

               
                   <%--Try v2Captcha--%>
                <div id="CaptchaClass" runat="server">
                    <div class="g-recaptcha" data-sitekey="6LdCW4UUAAAAADK9eQFh6LdFvhWaxgji0dv9iyc6"></div>
                    <asp:Label ID="CaptchaNotCompletedLabel" runat="server" Text="Please Complete The Captcha. To Ensure You Are A Human. " ForeColor="Red"></asp:Label>
                </div>



                <p class="form-group ml-sm-5">

                    <asp:Button ID="LoginButton" runat="server" Text="LOGIN" CssClass="btn btn-primary col-sm-9 color1" OnClick="Login_Method" />


                </p>

                <div>

                    <asp:Button ID="forgotPassword" runat="server" Text="Forgot Password?" CssClass="btn forgotPassword" Width="170px" OnClick="forgotPassword_Click" CausesValidation="false" />

                </div>
                <div class="align-text-bottom text-sm-center mr-sm-5">
                </div>

             



            </div>

        </div>
    </form>



</body>
</html>
