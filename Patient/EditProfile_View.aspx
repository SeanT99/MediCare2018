﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Patient.master" AutoEventWireup="true" CodeFile="EditProfile_View.aspx.cs" Inherits="Patient_EditProfile_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <table style="width:36%; border-style: solid; margin-left: 35%; margin-top: 2%;">
         <tr>
            <td colspan="2" style="font-weight: bold; height: 26px; margin-top:1%; font-size: x-large; text-transform: none; text-decoration: underline;">Your Profile</td>
        </tr>
        <tr>
            <td colspan="2" style="font-weight: bold">Basic Patient Info&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 318px">Given Name</td>
            <td>
                <asp:Label ID="given_NameLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Family Name</td>
            <td>
                <asp:Label ID="family_NameLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Date of Birth (YYYY-mm-dd)</td>
            <td>
                <asp:Label ID="dobLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Gender</td>
            <td>
                <asp:Label ID="genderLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td style="width: 318px">ID Type</td>
            <td>
                <asp:Label ID="idTypeLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">ID No</td>
            <td>
                <asp:Label ID="idLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="font-weight: bold">Contact Details</td>
        </tr>
        <tr>
            <td style="width: 318px">Email</td>
            <td>
                <asp:Label ID="emailLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Mobile Phone</td>
            <td>
                <asp:Label ID="mobileLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">House Phone</td>
            <td>
                <asp:Label ID="homeLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Blk/House Number</td>
            <td>
                <asp:Label ID="blkLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Street Name</td>
            <td>
                <asp:Label ID="streetLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Unit Number</td>
            <td>
                <asp:Label ID="unitLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Building Name</td>
            <td>
                <asp:Label ID="buildingLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Postal Code</td>
            <td>
                <asp:Label ID="postalLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="font-weight: bold">Emergency Contact</td>
        </tr>
        <tr>
            <td style="width: 318px">Name</td>
            <td>
                <asp:Label ID="ecNameLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Contact Number </td>
            <td>
                <asp:Label ID="ecContactLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Relationship</td>
            <td>
                <asp:Label ID="ecRelationshipLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="font-weight: bold; height: 26px;">Medical Info</td>
        </tr>
        <tr>
            <td style="width: 318px">Food/Drug Allergies</td>
            <td>
                <asp:Label ID="allergyLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Pre-existing medical conditions and history</td>
            <td>
                <asp:Label ID="historyLBL" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
       
        <tr>
            <td colspan="2">
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="Edit Profile" />
                <asp:Button ID="ChangeQnBtn" runat="server" Text="Change Security Questions" OnClick="ChangeQnBtn_Click" Width="221px" />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Change Password" Width="152px" />
            </td>
        </tr>
       
        <tr>
            <td style="width: 318px">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
       
        </table>

</asp:Content>

