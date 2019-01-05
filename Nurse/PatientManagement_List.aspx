<%@ Page Title="" Language="C#" MasterPageFile="~/Nurse.master" AutoEventWireup="true" CodeFile="PatientManagement_List.aspx.cs" Inherits="Nurse_PatientInfoListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label runat="server" Text="*No Patients Found*" ID="notFoundLbl" Font-Bold="True" ViewStateMode="Disabled" Visible="False"></asp:Label>
    <asp:GridView ID="gvPatient" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvPatient_SelectedIndexChanged" DataKeyNames="id" OnRowDeleting="gvPatient_RowDeleting">
         <Columns>
            <asp:BoundField DataField="id" HeaderText="ID Number" />
            <asp:BoundField DataField="given_Name" HeaderText="Given Name" />
            <asp:BoundField DataField="family_Name" HeaderText="Family Name" />
            <asp:BoundField DataField="gender" HeaderText="Gender" />
            <asp:BoundField DataField="mobileNumber" HeaderText="Mobile Number" />
            <asp:BoundField DataField="kin_contact" HeaderText="Emergency Contact" />
            <asp:BoundField DataField="medical_allergies" HeaderText="Allergy" />
            <asp:CommandField ShowCancelButton="False" ShowDeleteButton="True" SelectText="View Details" ShowSelectButton="True" EditText="" />
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

