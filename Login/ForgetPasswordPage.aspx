<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgetPasswordPage.aspx.cs" Inherits="Login_ForgetPasswordPage" %>

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
        <h3 class="text-center mr-sm-6 pb-sm-5 forgotPasswordTitle">Forgot Your Password ?</h3>   
        <asp:Label ID="EmailAddressInformationLabel" runat="server" CssClass="EmailAddressInformationLabel" Text="Please enter the email address associated with your account, and we'll send you a link to reset your password" Width="420px"></asp:Label>

        <div class ="form-group ml-sm-5">

    <asp:Label ID="EmailAddress" runat="server" Text="Email Address:" CssClass="col-form-label"></asp:Label>
            
    <asp:TextBox ID="EmailAddressField" runat="server" CssClass="form-control col-sm-9 mt-sm-2" style="left: 0px; top: 0px" ></asp:TextBox>
        </div>
    
    <p class="form-group ml-sm-5">
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EmailAddressField" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Please Enter A Valid Email Address</asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="EmailAddressDoNotExistLabel" runat="server" Text="Email Address Do Not Exist" ForeColor="Red"></asp:Label>
        <br />
        <asp:Button ID="SubmitButton" runat="server" Text="SUBMIT" CssClass="btn btn-primary col-sm-9 color1" OnClick="SubmitButton_Click" style="left: 0px; top: 0px"  />


    </p>

        <div class ="align-text-bottom text-sm-center mr-sm-5">
        </div>

</div>
        
    </div>
</form>
       
    
    
</body>
</html>


