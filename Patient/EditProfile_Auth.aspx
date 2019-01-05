<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditProfile_Auth.aspx.cs" Inherits="Patient_EditProfile_Auth" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 294px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:100%;">
                <tr>
                    <td colspan="2" style="font-weight: bold;">Please answer the following security questions to edit your profile.</td>
                </tr>
                <tr>
                    <td class="auto-style1" style="font-weight: bolder">Question 1:</td>
                    <td>
                        <asp:Label id="Q1Lbl" Text="" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1" style="font-weight: bolder">Answer:</td>
                    <td>
                        <asp:TextBox ID="Ans1TB" runat="server" Width="261px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This is a required field" ControlToValidate="Ans1TB" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1" style="font-weight: bolder">Question 2:</td>
                    <td>
                        <asp:Label id="Q2Lbl" Text="" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1" style="font-weight: bolder">Answer:</td>
                    <td>
                        <asp:TextBox ID="Ans2TB" runat="server" Width="261px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This is a required field" ControlToValidate="Ans2TB" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>

        <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="Submit" />

    </form>
</body>
</html>
