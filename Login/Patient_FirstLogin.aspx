<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Patient_FirstLogin.aspx.cs" Inherits="Login_ChangePasswordPage" %>

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
     <link href="../CSS/NewPatientFirstLogin.css" rel="stylesheet" />

    <title>Login</title>



</head>
<body>

    <form id="Form1" class="container-fluid" style="height: 100%;" runat="server">

        <div class="row" style="height: 100%;">
            <div class="container-fluid col-sm-6" style="background-size: cover; background-image: url(../Img/ex.jpeg); background-repeat: no-repeat; background-position-x: center; opacity: 0.85; filter: grayscale(30%);">
            </div>

            <div class="container col-sm-6 align-self-center ShiftMedicareSection ">
                <h3 class="text-center mr-sm-5 pb-sm-5 ShiftMedicareTitle">Change Password</h3>

                <p class="form-group ml-sm-5">

                <table id="FirstChangePasswordLoginTable" style="width:100%;">
             
                <tr>
                    <td class="auto-style1" style="font-weight: bold;" colspan="2">Hi! Welcome to your new MediCare portal!</td>
                </tr>
             
                <tr>
                    <td class="auto-style1" colspan="2">To start using this portal, you would need to first choose a new password, and then set your security questions and answer</td>
                </tr>
                <tr>
                    <td class="auto-style8" style="font-weight: bold;"></td>
                    <td class="auto-style9">
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" style="font-weight: bold;">Old Password</td>
                    <td>
                        <asp:TextBox ID="OldPasswordTB" runat="server" Width="280px" CssClass="OldPasswordTB" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                         <asp:Label ID="PasswordIncorrectLabel" runat="server" ForeColor="Red" Text="Your Old Password is Incorrect" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="auto-style3" style="font-weight: bold;">New Password</td>
                    <td>
                        <asp:TextBox ID="NewPasswordTB" runat="server" Width="280px" CssClass="NewPasswordTB" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" style="font-weight: bold;">Confirm Password</td>
                    <td>
                        <asp:TextBox ID="ConfirmPasswordTB" runat="server" Width="280px" CssClass="ConfirmPasswordTB" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6" style="font-weight: bold;"></td>
                    <td class="auto-style7">
                        </td>
                </tr>
                <tr>
            <td style="width: 318px; font-weight: bold; height: 23px;">Question 1</td>
            <td >
                <asp:DropDownList ID="sq1DDL" runat="server" >
                    <asp:ListItem Selected="True">-Select Question-</asp:ListItem>
                    <asp:ListItem>What was your childhood nickname?</asp:ListItem>
                    <asp:ListItem>Where did you attend primary school?</asp:ListItem>
                    <asp:ListItem>Where were you when you had your first kiss?</asp:ListItem>
                    <asp:ListItem>What is your favourite teacher&#39;s name?</asp:ListItem>
                    <asp:ListItem>Where were you during 9/11?</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="sq1DDL" InitialValue="-Select Question-" ErrorMessage="Please select a question" Font-Bold="True" ForeColor="Red"/>
            </td>
        </tr>
        <tr>
            <td style="width: 318px; font-weight: bold; ">Answer </td>
            <td style="height: 26px">
                <asp:TextBox ID="sqAns1TB" runat="server" Width="623px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="sqAns1TB" ErrorMessage="This is a required field" Font-Bold="True" ForeColor="Red"/>
            </td>
        </tr>
        <tr>
            <td style="width: 318px; font-weight: bold; ">Question 2</td>
            <td style="height: 26px">
                <asp:DropDownList ID="sq2DDL" runat="server">
                    <asp:ListItem Selected="True">-Select Question-</asp:ListItem>
                    <asp:ListItem>What was your childhood nickname?</asp:ListItem>
                    <asp:ListItem>Where did you attend primary school?</asp:ListItem>
                    <asp:ListItem>Where were you when you had your first kiss?</asp:ListItem>
                    <asp:ListItem>What is your favourite teacher&#39;s name?</asp:ListItem>
                    <asp:ListItem>Where were you during 9/11?</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="sq2DDL" InitialValue="-Select Question-" ErrorMessage="Please select a question" Font-Bold="True" ForeColor="Red"/>
            </td>
        </tr>
        <tr>
            <td style="width: 318px; font-weight: bold; ">Answer</td>
            <td style="height: 26px">
                <asp:TextBox ID="sqAns2TB" runat="server" Width="623px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="sqAns2TB" ErrorMessage="This is a required field" Font-Bold="True" ForeColor="Red" />
            </td>
        </tr>
       
            </table>



                    <asp:Button ID="details" runat="server" Text="Submit" CssClass="btn btn-primary col-sm-9 color1" style="margin-left:100px; left: 0px; top: 0px;" OnClick="details_Click" />


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
