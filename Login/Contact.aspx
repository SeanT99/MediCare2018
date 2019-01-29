<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Login_ConfirmChangedPassword" %>

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
    <link href="../CSS/ChangePassword.css" rel="stylesheet" />
    <title>Login</title>



    <style type="text/css">
        .auto-style1 {
            height: 27px;
        }
    </style>



</head>
<body>

    <form id="Form1" class="container-fluid" style="height: 100%;" runat="server">

        <div class="row" style="height: 100%;">
            <div class="container-fluid col-sm-6" style="background-size: cover; background-image: url('../Img/ex.jpeg'); background-repeat: no-repeat; background-position-x: center; opacity: 0.85; filter: grayscale(30%); left: 0px; top: 0px;">
            </div>

            <div class="container col-sm-6 align-self-center">
                <h3 class="text-center mr-sm-6 pb-sm-5 forgotPasswordConfirmationTitle" style="margin-right: 40px;">Unlock Of Account</h3>
                <table style="width: 100%;">
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="namelabel" runat="server" Text="Username"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="NameTB" runat="server" CssClass="form-control col-sm-9 mt-sm-2"></asp:TextBox>
                        </td>
                        <td class="auto-style1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NameTB" ErrorMessage="RequiredFieldValidator" ForeColor="Red">This Is Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="emaillabel" runat="server" Text="Email Addresses"></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="emailTB" runat="server" CssClass="form-control col-sm-9 mt-sm-2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="emailTB" ErrorMessage="RequiredFieldValidator" ForeColor="Red">This Is Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="emailTB">Invalid Email Format</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                            <asp:Label ID="DobLabel" runat="server" Text="Date-Of-Birth"></asp:Label>
                        </td>
                        <td class="auto-style4">
                            <asp:TextBox ID="DobTB" runat="server" TextMode="Date" CssClass="form-control col-sm-9 mt-sm-2"></asp:TextBox>
                        </td>
                        <td class="auto-style5">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DobTB" ErrorMessage="RequiredFieldValidator" ForeColor="Red">This Is Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Unlock / Reset Account</td>
                        <td class="auto-style2">
                            <asp:RadioButtonList ID="OptionRadio" runat="server">
                                <asp:ListItem>Unblock Of Account</asp:ListItem>
                                <asp:ListItem>Reset Password</asp:ListItem>

                            </asp:RadioButtonList>
                            <br />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="OptionRadio" ForeColor="Red">This Is Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <br />
                <asp:Button ID="details" runat="server" Text="Submit" CssClass="btn btn-primary col-sm-9 color1" Style="left: 0px; top: 0px" OnClick="details_Click" />



            </div>


        </div>

        </div>
    </form>



</body>
</html>
