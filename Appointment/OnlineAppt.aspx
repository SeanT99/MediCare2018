<%@ Page Title="" Language="C#" MasterPageFile="~/Patient.master" AutoEventWireup="true" CodeFile="OnlineAppt.aspx.cs" Inherits="Appointment_OnlineAppt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
   
        <div class="container mt-sm-3">
  
            <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="Online Appointment" CssClass="font-weight-bold"></asp:Label>
   
        <div class="divAppt">
            <!-- comment -->
            <table class="table table-primary table-responsive-sm " style="height: 39%">
                <tr>
                    <td style="width: 521px; height: 27px;">Please enter your details! Don&#39;t leave any field blank thanks</td>
                </tr>
                <tr>
                    <td style="width: 521px; height: 67px;">
                        Patient ID: &nbsp;<asp:TextBox ID="tbPatientID" runat="server"  CssClass="form-control"></asp:TextBox>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 521px; height: 111px;">
                        Available appointment timings:&nbsp;
                        <asp:DropDownList ID="ddlApptTime" runat="server" CssClass="form-control">
                            <asp:ListItem>12:00 PM</asp:ListItem>
                            <asp:ListItem>1:00 PM</asp:ListItem>
                            <asp:ListItem>2:00 PM</asp:ListItem>
                            <asp:ListItem>3:00 PM</asp:ListItem>
                            <asp:ListItem>4:00 PM</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        Available appointment dates:&nbsp; <asp:DropDownList CssClass="form-control"
    id="ddlApptDate"
    runat="server"
    dataTextFormatString="{0:dd/MM/yyyy} ">
                            <asp:ListItem>05/05/1995</asp:ListItem>
                            <asp:ListItem>06/05/1995</asp:ListItem>
                            <asp:ListItem>07/05/1995</asp:ListItem>
                            <asp:ListItem>08/05/1995</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </td>
                </tr>
                
                </table>
            
            
           <div class="d-inline-block form-group">
                <asp:Button ID="buttonApptCancel" runat="server" Text="Cancel" style="margin-right: 5px" />
                <asp:Button ID="buttonApptConfirm" runat="server" Text="Confirm" OnClick="buttonApptConfirm_Click"  />
          
                
                
            </div>
            
        </div>
        </div>
</asp:Content>

