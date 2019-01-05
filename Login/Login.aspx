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

    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>

 <script src='https://www.google.com/recaptcha/api.js?render=6LetDoIUAAAAADPHkmCAdQq3jjGep2_c1WZWhQhF'></script>

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
                    <asp:Label ID="IncorrectUsernameAndPasswordLabel" runat="server" ForeColor="Red">UserName or Password invalid</asp:Label>
                </div>

                <%--For Security Table--%>
                <div class="form-group ml-sm-5" runat="server" id="SecurityTable">

                    <table>
                        <tr>
                            <td style="font-weight: bold;" colspan="2" class="auto-style1">Security Questions</td>
                        </tr>
                        <tr>
                            <td style="width: 318px; font-weight: bold; height: 23px;">Question 1</td>
                            <td style="height: 23px">
                                <asp:DropDownList ID="sq1DDL" runat="server" CssClass="ShiftSecurityOptionAndTextField">
                                    <asp:ListItem Selected="True">-Select Question-</asp:ListItem>
                                    <asp:ListItem>What was your childhood nickname?</asp:ListItem>
                                    <asp:ListItem>Where did you attend primary school?</asp:ListItem>
                                    <asp:ListItem>Where were you when you had your first kiss?</asp:ListItem>
                                    <asp:ListItem>What is your favourite teacher&#39;s name?</asp:ListItem>
                                    <asp:ListItem>Where were you during 9/11?</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 318px; font-weight: bold; height: 26px;">Answer </td>
                            <td style="height: 26px">
                                <asp:TextBox ID="sqAns1TB" runat="server" CssClass="ShiftSecurityOptionAndTextField" Width="640px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 318px; font-weight: bold; height: 26px;">Question 2</td>
                            <td style="height: 26px">
                                <asp:DropDownList ID="sq2DDL" runat="server" CssClass="ShiftSecurityOptionAndTextField">
                                    <asp:ListItem Selected="True">-Select Question-</asp:ListItem>
                                    <asp:ListItem>What was your childhood nickname?</asp:ListItem>
                                    <asp:ListItem>Where did you attend primary school?</asp:ListItem>
                                    <asp:ListItem>Where were you when you had your first kiss?</asp:ListItem>
                                    <asp:ListItem>What is your favourite teacher&#39;s name?</asp:ListItem>
                                    <asp:ListItem>Where were you during 9/11?</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 318px; font-weight: bold; height: 26px;">Answer</td>
                            <td style="height: 26px">
                                <asp:TextBox ID="sqAns2TB" runat="server" CssClass="ShiftSecurityOptionAndTextField" Width="640px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 318px; font-weight: bold; height: 26px;">Question 3</td>
                            <td style="height: 26px">
                                <asp:DropDownList ID="sq3DDL" runat="server" CssClass="ShiftSecurityOptionAndTextField">
                                    <asp:ListItem Selected="True">-Select Question-</asp:ListItem>
                                    <asp:ListItem>What was your childhood nickname?</asp:ListItem>
                                    <asp:ListItem>Where did you attend primary school?</asp:ListItem>
                                    <asp:ListItem>Where were you when you had your first kiss?</asp:ListItem>
                                    <asp:ListItem>What is your favourite teacher&#39;s name?</asp:ListItem>
                                    <asp:ListItem>Where were you during 9/11?</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 318px; font-weight: bold; height: 26px;">Answer</td>
                            <td style="height: 26px">
                                <asp:TextBox ID="sqAns3TB" runat="server" CssClass="ShiftSecurityOptionAndTextField" Width="640px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <%-- End Of Security Table --%>
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
