<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthorsInfo.aspx.cs" Inherits="BookApp.Views.AuthorsInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added.">
<Columns>
    <asp:TemplateField HeaderText="Name" ItemStyle-Width="150">
        <ItemTemplate>
            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
        </EditItemTemplate>
    </asp:TemplateField>
    

    <asp:TemplateField HeaderText="LastName" ItemStyle-Width="150">
        <ItemTemplate>
            <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:TextBox>
        </EditItemTemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="BirthDate" ItemStyle-Width="150">
        <ItemTemplate>
            <asp:Label ID="lblBirthDate" runat="server" Text='<%# Eval("BirthDate") %>'></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
       <input type="date"  name="txtBirthdate" width="140"/>
        </EditItemTemplate>
    </asp:TemplateField>
    
    

    <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150"/>
</Columns>
</asp:GridView>
       <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
<tr>
    <td style="width: 150px">
        Name:<br />
        <asp:TextBox ID="txtName" runat="server" Width="140" />
    </td>
    <td style="width: 150px">
        LastName:<br />
        <asp:TextBox ID="txtLastName" runat="server" Width="140" />
    </td>
    <td style="width: 150px">
        BirthDate:<br />
       <input type="date"  name="txtBirthdate"  required width="140"/>
    </td>
    

    <td style="width: 100px">
        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Insert" />
    </td>
</tr>
<asp:Label runat="server" ID="errorMe" ></asp:Label>
</table>
        
    </div>
    </form>
</body>
</html>
