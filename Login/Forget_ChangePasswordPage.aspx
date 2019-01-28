<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forget_ChangePasswordPage.aspx.cs" Inherits="Login_ChangePasswordPage" %>

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

    <form id="Form1" class="container-fluid" style="height: 100%;" runat="server" defaultbutton="details">

        <div class="row" style="height: 100%;">
            <div class="container-fluid col-sm-6" style="background-size: cover; background-image: url(../Img/ex.jpeg); background-repeat: no-repeat; background-position-x: center; opacity: 0.85; filter: grayscale(30%);">
            </div>

            <div class="container col-sm-6 align-self-center ShiftMedicareSection ">
                <h3 class="text-center mr-sm-5 pb-sm-5 ShiftMedicareTitle">Change Password</h3>


                <div class="form-group ml-sm-5">
                    <asp:Label ID="ChangePassUsernameLabel" runat="server" Text="Username"></asp:Label>
                    <br />
                    <asp:TextBox ID="ChangePassUsernameField" runat="server" CssClass="form-control col-sm-9 mt-sm-2" Style="left: 0px; top: 0px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValidateChangePassUsernameField" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="ChangePassUsernameField" ForeColor="Red">Username Cannot Be Empty</asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="ChangePassUserErrorLabel" runat="server" Text="Username Does Not Exist" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Label ID="otp_lbl" runat="server" Text="OTP:"></asp:Label>
                    <br />
                    <asp:TextBox ID="otp_tb" runat="server" CssClass="form-control col-sm-9 mt-sm-2" style="left: 0px; top: 0px" MaxLength="6"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="OTP Cannot Be Empty" ForeColor="Red" ControlToValidate="otp_tb"></asp:RequiredFieldValidator>
                    <asp:Button ID="resend_btn" runat="server" CssClass="btn btn-primary col-sm-3 ml-sm-5 mt-sm-2 color1" CausesValidation="False" OnClick="resend_btn_Click" Text="RESEND" />
                    <br />
                </div>

                <div class="form-group ml-sm-5">

                    <asp:Label ID="ChangePasswordLabel" runat="server" Text="New Password:" CssClass="col-form-label"></asp:Label>



                    <asp:TextBox ID="ChangePasswordField" runat="server" TextMode="Password" CssClass="form-control col-sm-9 mt-sm-2" Style="left: 0px; top: 0px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NewPasswordValidator" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ControlToValidate="ChangePasswordField">New Password Cannot Be Empty</asp:RequiredFieldValidator>                  
                    <br />
                    



                </div>

                <div class="form-group ml-sm-5">

                    <asp:Label ID="VerifyPasswordLabel" runat="server" Text="Verify New Password"></asp:Label>

                </div>

                <p class="form-group ml-sm-5">

                    <asp:TextBox ID="VerifyPasswordTextBox" runat="server" TextMode="Password" CssClass="form-control col-sm-9 mt-sm-2" style="left: 0px; top: 0px"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="VerifyNewPasswordValidator" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="VerifyPasswordTextBox" ForeColor="Red">Verify Password Field Cannot Be Empty</asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="NewPasswordDoesNotMatchLabel" runat="server" ForeColor="Red" Text="New Password Does Not Match"></asp:Label>
                    <br />
                    <asp:Label ID="PasswordUsedPreviouslyLabel" runat="server" ForeColor="Red" Text="Password Used Previously, Please Choose Another Password"></asp:Label>
                    
                    <br />
                    <asp:Label ID="AlphaNumericLabel" runat="server" Text="Password Must Be Alphanumeric" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>

                    <asp:Button ID="details" runat="server" Text="Submit" CssClass="btn btn-primary col-sm-9 color1" OnClick="details_Click" Style="left: 0px; top: 0px" />


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
