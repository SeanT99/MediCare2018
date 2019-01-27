<%@ Page Title="" Language="C#" MasterPageFile="~/Patient.master" AutoEventWireup="true" CodeFile="EditProfile_ChangePassword.aspx.cs" Inherits="Patient_EditProfile_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table class="w-100">
             
                <tr>
                    <td class="auto-style1" style="font-weight: bold;" colspan="2">Change Your Password</td>
                </tr>
             
                <tr>
                    <td class="auto-style8" style="font-weight: bold;"></td>
                    <td class="auto-style9">
                    </td>
                </tr>
                <tr>
                    <td class="auto-style8" style="font-weight: bold;">Old Password</td>
                    <td class="auto-style9">
                      <asp:TextBox ID="old_tb" runat="server" TextMode="Password" Width="280px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="old_tb" ErrorMessage="Please enter your old password" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" style="font-weight: bold;">New Password</td>
                    <td>
                        <asp:TextBox ID="ChangePasswordField" runat="server" Width="280px" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordValidator" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ControlToValidate="ChangePasswordField">New Password Cannot Be Empty</asp:RequiredFieldValidator>                  
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" style="font-weight: bold;">Confirm Password</td>
                    <td>
                        <asp:TextBox ID="VerifyPasswordTextBox" runat="server" Width="280px" TextMode="Password"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="VerifyNewPasswordValidator" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="VerifyPasswordTextBox" ForeColor="Red" Text="Please re-enter your new password" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6" colspan="2">
                    
                    <asp:Label ID="PasswordDoesNotMatchLabel" runat="server" ForeColor="Red" Text="New Passwords Do Not Match"></asp:Label><br />
                    <asp:Label ID="PasswordIncorrectLabel" runat="server" ForeColor="Red" Text="Your Old Password is Incorrect"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td class="auto-style6" style="font-weight: bold;">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
                    </td>
                    <td class="auto-style7">
                        </td>
                </tr>
                
            </table>
</asp:Content>

