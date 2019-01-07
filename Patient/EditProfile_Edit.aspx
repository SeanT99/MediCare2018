<%@ Page Title="" Language="C#" MasterPageFile="~/Patient.master" AutoEventWireup="true" CodeFile="EditProfile_Edit.aspx.cs" Inherits="Patient_EditProfile_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table style="width:100%;">
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
            <td style="width: 318px">Date of Birth (dd/mm/YYYY)</td>
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
                <asp:TextBox ID="emailTB" runat="server" autocomplete="off" Autopostback="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="emailTB" ErrorMessage="This is a required field" Font-Bold="True" ForeColor="Red"/>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="emailTB" ErrorMessage="This is an invalid email" Font-Bold="True" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Mobile Phone</td>
            <td>
                <asp:TextBox ID="mobileTB" runat="server" Width="265px" TextMode="Phone" MaxLength="8" autocomplete="off"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="mobileTB" ErrorMessage="Only numbers allowed" Font-Bold="True" ForeColor="Red" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">House Phone</td>
            <td>
                <asp:TextBox ID="homeTB" runat="server" Width="265px" TextMode="Phone" MaxLength="8" autocomplete="off"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="homeTB" ErrorMessage="Only numbers allowed" Font-Bold="True" ForeColor="Red" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Blk/House Number</td>
            <td>
                <asp:TextBox ID="blkTB" runat="server" Width="147px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 318px; height: 26px;">Street Name</td>
            <td style="height: 26px">
                <asp:TextBox ID="streetTB" runat="server" Width="515px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="streetTB" ErrorMessage="This is a required field" Font-Bold="True" ForeColor="Red"/>

            </td>
        </tr>
        <tr>
            <td style="width: 318px">Unit Number</td>
            <td>
                <asp:TextBox ID="unitTB" runat="server" Width="147px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Building Name</td>
            <td>
                <asp:TextBox ID="buildingTB" runat="server" Width="515px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Postal Code</td>
            <td>
                <asp:TextBox ID="postalTB" runat="server" Width="147px" MaxLength="6" autocomplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="postalTB" ErrorMessage="This is a required field" Font-Bold="True" ForeColor="Red"/>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="postalTB" ErrorMessage="Only numbers allowed" Font-Bold="True" ForeColor="Red" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
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
            <td style="width: 318px; height: 33px;">Name</td>
            <td style="height: 33px">
                <asp:TextBox ID="ecNameTB" runat="server" Width="515px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ecNameTB" ErrorMessage="This is a required field" Font-Bold="True" ForeColor="Red"/>
            </td>
        </tr>
        <tr>
            <td style="width: 318px">Contact number</td>
            <td>
                <asp:TextBox ID="ecNumberTB" runat="server" Width="265px" TextMode="Phone" MaxLength="8" autoComplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ecNumberTB" ErrorMessage="This is a required field" Font-Bold="True" ForeColor="Red"/>
                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ecNumberTB" ErrorMessage="Only numbers allowed" Font-Bold="True" ForeColor="Red" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 318px; height: 26px;">Relationship</td>
            <td style="height: 26px">
                <asp:TextBox ID="ecRelationshipTB" runat="server" Width="265px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ecRelationshipTB" ErrorMessage="This is a required field" Font-Bold="True" ForeColor="Red"/>
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
            <td style="width: 318px; height: 101px;">Food/Drug Allergies</td>
            <td style="height: 101px">
                <asp:TextBox ID="allergyTB" runat="server" Height="93px" TextMode="MultiLine" Width="481px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 318px; height: 101px;">Pre-existing medical conditions and history</td>
            <td style="height: 101px">
                <asp:TextBox ID="medHistTB" runat="server" Height="93px" TextMode="MultiLine" Width="481px"></asp:TextBox>
            </td>
        </tr>
       
        <tr>
            <td style="width: 318px">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
       
        <tr>
            <td style="width: 318px">
                <asp:Button ID="SaveBtn" runat="server" OnClick="SaveBtn_Click" Text="Save Changes" Width="120px" Autopostback="true"/>
            </td>
            <td>
                &nbsp;</td>
        </tr>
       
        </table>
</asp:Content>

