<%@ Page Title="" Language="C#" MasterPageFile="~/Patient.master" AutoEventWireup="true" CodeFile="CheckoutPayment.aspx.cs" Inherits="Appointment_CheckoutPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style>
        .expiryDate_lbl {
               margin-right: 4%;
        }
    </style>

    <div class="container mt-sm-3" style="height: 642px; margin-bottom: 0px">
  
            <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="Booking Payment Confirmation" CssClass="font-weight-bold"></asp:Label>
   
        <div class="divAppt">
            <!-- comment -->
            <table class="table table-primary table-responsive-sm " style="height: 39%">
                
                <tr>
                    <td style="width: 521px; height: 111px;">
                        Available appointment timings:&nbsp;
                        <asp:DropDownList ID="ddlApptTime" runat="server" CssClass="form-control" Enabled="False">
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
    dataTextFormatString="{0:dd/MM/yyyy} " Enabled="False">
                            <asp:ListItem>05/05/1995</asp:ListItem>
                            <asp:ListItem>06/05/1995</asp:ListItem>
                            <asp:ListItem>07/05/1995</asp:ListItem>
                            <asp:ListItem>08/05/1995</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        Bookings Fee:
                        <asp:Label ID="fee_lbl" runat="server" Text="$15.00"></asp:Label>


                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="paymentDetails_lbl" runat="server" Text="Payment Details" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <br />
                        <asp:Label ID="cardholdername_lbl" runat="server" Text="Card Holder's Name"></asp:Label>
                        <br />
                        <asp:TextBox ID="cardholdername_tb" runat="server" Width="254px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Please enter your card holder name." ID="rfv_cardholdername" ControlToValidate="cardholdername_tb" ForeColor="Red"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Label ID="creditNo_lbl" runat="server" Text="Credit Card Number"></asp:Label>
                        <br />
                        <asp:TextBox ID="creditNo_tb" runat="server" Width="254px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_creditcardno" runat="server" ControlToValidate="creditNo_tb" ErrorMessage="Please enter your credit card number." ForeColor="Red"></asp:RequiredFieldValidator>
                        <br />       
                        <span class="expiryDate_lbl">Expiry Date</span>
                        <asp:Label ID="cvv_lbl" runat="server" Text="CCV/CVV"></asp:Label>
                        <br />
                        <asp:TextBox ID="expiryDateMM_tb" runat="server" Width="60px" placeholder="MM" MaxLength="2"></asp:TextBox>/
                        <asp:TextBox ID="expiryDateYY_tb" runat="server" Width="60px" placeholder="YY" ></asp:TextBox>
                        <asp:TextBox ID="cvv_tb" runat="server" Width="125px" placeholder="XXX"></asp:TextBox>

                        <br />

                    </td>
                </tr>
                </table>
            <div class="d-inline-block form-group">
                <asp:Button ID="buttonPaymentCancel" runat="server" Text="Cancel" style="margin-right: 5px" OnClick="buttonApptCancel_Click" CausesValidation="False"/>
                <asp:Button ID="buttonPaymentConfirm" runat="server" Text="Pay and Book Appointment" OnClick="buttonPaymentConfirm_Click" />
                
                
            </div>


        </div>
        </div>

</asp:Content>

