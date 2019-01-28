<%@ Page Title="" Language="C#" MasterPageFile="~/Patient.master" AutoEventWireup="true" CodeFile="EditProfile_ChangeSecQn.aspx.cs" Inherits="Patient_EditProfile_ChangeSecQn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table style="width:100%;">
        <tr>
        <td style="font-weight: bold; height: 23px;" colspan="2">Please select your new security questions and answers below</td>
        </tr>
        <tr>
        <td style="width: 318px; font-weight: bold; height: 23px;">OTP</td>
            <td >
                <asp:TextBox ID="otpTB" runat="server" AutoCompleteType="Disabled" MaxLength="6" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="otpTB" ErrorMessage="Please enter your OTP" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
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
                <asp:TextBox ID="sqAns1TB" runat="server" Width="883px" AutoCompleteType="Disabled"></asp:TextBox>
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
                <asp:TextBox ID="sqAns2TB" runat="server" Width="883px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="sqAns2TB" ErrorMessage="This is a required field" Font-Bold="True" ForeColor="Red" />
            </td>
        </tr>
       
        <tr>
            <td style="width: 318px; font-weight: bold; ">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </td>
            <td style="height: 26px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 318px; font-weight: bold; ">
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="Submit" />
            </td>
            <td style="height: 26px">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

