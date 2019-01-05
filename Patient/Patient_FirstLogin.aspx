<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Patient_FirstLogin.aspx.cs" Inherits="Patient_Patient_FirstLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
        .auto-style3 {
            width: 158px;
        }
        .auto-style5 {
            
        }
        .auto-style6 {
            width: 158px;
            height: 10px;
        }
        .auto-style7 {
            height: 10px;
        }
        .auto-style8 {
            width: 158px;
            height: 3px;
        }
        .auto-style9 {
            height: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:100%;">
             
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
                    <td class="auto-style3" style="font-weight: bold;">New Password</td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Width="280px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" style="font-weight: bold;">Confirm Password</td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" Width="280px"></asp:TextBox>
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
                <asp:TextBox ID="sqAns1TB" runat="server" Width="883px"></asp:TextBox>
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
                <asp:TextBox ID="sqAns2TB" runat="server" Width="883px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="sqAns2TB" ErrorMessage="This is a required field" Font-Bold="True" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold; "d>Question 3</td>
            <td class="auto-style5">
                <asp:DropDownList ID="sq3DDL" runat="server">
                    <asp:ListItem Selected="True">-Select Question-</asp:ListItem>
                    <asp:ListItem>What was your childhood nickname?</asp:ListItem>
                    <asp:ListItem>Where did you attend primary school?</asp:ListItem>
                    <asp:ListItem>Where were you when you had your first kiss?</asp:ListItem>
                    <asp:ListItem>What is your favourite teacher&#39;s name?</asp:ListItem>
                    <asp:ListItem>Where were you during 9/11?</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="sq3DDL" InitialValue="-Select Question-" ErrorMessage="Please select a question" Font-Bold="True" ForeColor="Red"/>

            </td>
        </tr>
        <tr>
            <td style="width: 318px; font-weight: bold; ">Answer</td>
            <td style="height: 26px">
                <asp:TextBox ID="sqAns3TB" runat="server" Width="883px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="sqAns3TB" ErrorMessage="This is a required field" Font-Bold="True" ForeColor="Red" />
            </td>
        </tr>
            </table>
        </div>
        <asp:Button ID="SubmitBtn" runat="server" Text="Submit" />
    </form>
</body>
</html>
