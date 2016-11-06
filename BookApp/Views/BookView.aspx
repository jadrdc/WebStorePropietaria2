<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookView.aspx.cs" Inherits="BookApp.Views.BookView" %>

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


                    <asp:TemplateField HeaderText="ISBN" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblISBN" runat="server" Text='<%# Eval("ISBN") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtISBN" runat="server" Text='<%# Eval("ISBN") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Genre" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblGenre" runat="server" Text='<%# Eval("Genre.Description") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList name="genre" runat="server" ID="GenreDropDown" />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Editorial" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblEdito" runat="server" Text='<%# Eval("Editorial.Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList  name="editorial" runat="server" ID="EditorialDropDown" />
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Published Date" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblEditorial" runat="server" Text='<%# Eval("PUblished_Date") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <input type="date" name="date" required />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Autor" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblAutor" runat="server" Text='<%# Eval("Author.Name").ToString()+" "+Eval("Author.LastName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                        <asp:DropDownList  name="author" runat="server" ID="AuthorDropDown" />
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150" />
                </Columns>
            </asp:GridView>
 
     <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="width: 150px">Name:<br />
                        <asp:TextBox ID="txtName" runat="server" Width="140" />
                    </td>
                    <td style="width: 150px">ISBN:<br />
                        <asp:TextBox ID="txtISBN" runat="server" Width="140" />

                    </td>

                    <td style="width: 150px">Genre:<br />
                        <asp:DropDownList runat="server" ID="dpGenre" />
                    </td>

                    <td style="width: 150px">Editorial:<br />
                        <asp:DropDownList runat="server" ID="dpEditorial" />
                    </td>

                    <td style="width: 150px">Published Date:<br />
                        <input type="date" name="date" required />
                    </td>
                    
                    <td style="width: 150px">Autor:<br />
                        <asp:DropDownList runat="server" ID="dpAuthor" />
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
