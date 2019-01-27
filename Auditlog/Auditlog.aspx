<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Auditlog.aspx.cs" Inherits="Auditlog_Auditlog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <center>
        <h2>Audit Log</h2>
        <form id="form1" runat="server" style="margin-top:20px; width:100%">
            <asp:Button ID="btDownload" runat="server" Text="Download PDF" Visible="false" OnClick="btDownload_Click"></asp:Button><br /><br />
            <asp:GridView ID="GridView1" runat="server" EmptyDataText="No data" DataKeyNames="Id" Width="90%" AutoGenerateColumns="false" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" cellpadding="10" cellspacing="5">
                <Columns>
                    <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Label ID="lbAction" runat="server" Text='<%# Eval("Action") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                                <asp:TextBox ID="txtAction" runat="server" Text='<%# Eval("Action") %>' Width="200px"></asp:TextBox>
                            </EditItemTemplate>
                            </asp:TemplateField>

                    <asp:TemplateField HeaderText="Log">
                        <ItemStyle Width="40%"/>
                            <ItemTemplate>
                                <asp:Label ID="lbLog" runat="server" Text='<%# Eval("Log") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                                <asp:TextBox ID="txtLog" runat="server" Text='<%# Eval("Log") %>' Width="500px"></asp:TextBox>
                            </EditItemTemplate>
                            </asp:TemplateField>

                    <asp:TemplateField HeaderText="Timestamp">
                        <ItemStyle Width="15%"/>
                            <ItemTemplate>
                                <asp:Label ID="lbTimestamp" runat="server" Text='<%# Eval("Timestamp") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>


                    <asp:TemplateField HeaderText="Edit">
                            <ItemStyle HorizontalAlign="Center" Width="10%"/>
                             <ItemTemplate>
                                <asp:Button ID="btEdit" runat="server" Text="Edit" CommandName="Edit"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btUpdate" runat="server" Text="Update" CommandName="Update"/>
                                <asp:Button ID="btCancel" runat="server" Text="Cancel" CommandName="Cancel"/>
                           </EditItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delete">
                              <ItemStyle HorizontalAlign="Center" Width="10%"/>
                             <ItemTemplate>
                                 <asp:Button ID="btDelete" runat="server" Text="Delete" CommandName="Delete"/>
                             </ItemTemplate>
                          </asp:TemplateField>
                    </Columns>
            </asp:GridView>
        </form>
    </center>
</body>
</html>
