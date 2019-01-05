<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePasswordPage.aspx.cs" Inherits="Login_ChangePasswordPage" %>

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

        <div class="row" style="height: 100%;">
            <div class="container-fluid col-sm-6" style="background-size: cover; background-image: url(../Img/ex.jpeg); background-repeat: no-repeat; background-position-x: center; opacity: 0.85; filter: grayscale(30%);">
            </div>

            <div class="container col-sm-6 align-self-center ShiftMedicareSection ">
                <h3 class="text-center mr-sm-5 pb-sm-5 ShiftMedicareTitle">Change Password</h3>

                <div class="form-group ml-sm-5">

                    <asp:Label ID="ChangeUsernameLabel" runat="server" Text="Username:" CssClass="col-form-label"></asp:Label>

                    <asp:TextBox ID="ChangeUsernameField" runat="server" CssClass="form-control col-sm-9 mt-sm-2"></asp:TextBox>

                </div>

                <div class="form-group ml-sm-5">



                    <asp:Label ID="ChangePasswordLabel" runat="server" Text="New Password:" CssClass="col-form-label"></asp:Label>

                    <asp:TextBox ID="ChangePasswordField" runat="server" TextMode="Password" CssClass="form-control col-sm-9 mt-sm-2"></asp:TextBox>
                </div>

                <div class="form-group ml-sm-5">
                </div>

                <p class="form-group ml-sm-5">

                    <asp:Button ID="details" runat="server" Text="Submit" CssClass="btn btn-primary col-sm-9 color1" OnClick="details_Click" />


                </p>

                <div>

                </div>
                <div class="align-text-bottom text-sm-center mr-sm-5">
                </div>

            </div>

        </div>
    </form>



</body>
</html>
