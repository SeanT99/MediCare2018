<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgetPasswordEmailConfirmation.aspx.cs" Inherits="Login_ForgetPasswordEmailConfirmation" %>

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

 

</head>
<body>
    
<form id="Form1" class="container-fluid" style="height: 100%;" runat="server">
        
    <div class ="row" style="height: 100%;">
    <div class="container-fluid col-sm-6" style="background-size: cover; background-image: url(../Img/ex.jpeg); background-repeat: no-repeat; background-position-x: center; opacity: 0.85; filter: grayscale(30%);">
        
    </div>
    
    <div class ="container col-sm-6 align-self-center">
        <h3 class="text-center mr-sm-6 pb-sm-5 forgotPasswordConfirmationTitle">Forgot Your Password ?</h3>   
        <asp:Label ID="EmailConfirmationMessage" runat="server" CssClass="EmailConfirmationLabel" Text="Confirmation Email Sent, Please Check Your Email To Reset Your Password." Width="420px"></asp:Label>

        <div class ="form-group ml-sm-5">

        </div>
    
    <p class="form-group ml-sm-5">
        <br />
        <asp:Button ID="BackToLoginPageButton" runat="server" Text="Proceed to change password" CssClass="btn btn-primary col-sm-9 color1" OnClick="BackToLoginPageButton_Click"  />
    </p>

        <div class ="align-text-bottom text-sm-center mr-sm-5">
        </div>

</div>
        
    </div>
</form>
       
    
    
</body>
</html>



