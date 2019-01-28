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
                    <td style="width: 521px; height: 67px;">
                        Patient ID: &nbsp;<asp:TextBox ID="tbPatientID" runat="server"  CssClass="form-control" Enabled="False"></asp:TextBox>
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
                        Available appointment dates:&nbsp;
                        <br />
                        <%--<asp:TextBox ID="apptDate_tb" runat="server" TextMode="Date" ></asp:TextBox>--%>
<%--                       <asp:RequiredFieldValidator ID="apptDate_rfv" runat="server" ErrorMessage="Please select an appointment date" ForeColor="Red" ControlToValidate="apptDate_tb"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="apptDate_cv" runat="server" ErrorMessage="Please select an appointment date that is today/after today" ForeColor="Red" ControlToValidate="apptDate_tb" OnServerValidate="apptDateValid_ServerValidate"></asp:CustomValidator>--%>
                         <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender"></asp:Calendar>
                        <%--<asp:DropDownList CssClass="form-control"
    id="ddlApptDate"
    runat="server"
    dataTextFormatString="{0:dd/MM/yyyy} ">
                            <asp:ListItem>21/05/2019</asp:ListItem>
                            <asp:ListItem>22/05/2019</asp:ListItem>
                            <asp:ListItem>23/05/2019</asp:ListItem>
                            <asp:ListItem>24/05/2019</asp:ListItem>
                            <asp:ListItem>25/05/2019</asp:ListItem>
                            <asp:ListItem>26/05/2019</asp:ListItem>
                            <asp:ListItem>27/05/2019</asp:ListItem>
                        </asp:DropDownList>--%>
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

