<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="SalesReportsSystem.UserMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .myddls {
            align-items: center;
            background-color: rgb(255, 255, 255);
            border-bottom-color: rgb(204, 204, 204);
            border-bottom-left-radius: 4px;
            border-bottom-right-radius: 0px;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-collapse: collapse;
            border-image-outset: 0px;
            border-image-repeat: stretch;
            border-image-slice: 100%;
            border-image-source: none;
            border-image-width: 1;
            border-left-color: rgb(204, 204, 204);
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: rgb(204, 204, 204);
            border-right-style: solid;
            border-right-width: 1px;
            border-top-color: rgb(204, 204, 204);
            border-top-left-radius: 4px;
            border-top-right-radius: 0px;
            border-top-style: solid;
            border-top-width: 1px;
            box-shadow: rgba(0, 0, 0, 0.0745098) 0px 1px 1px 0px inset;
            box-sizing: border-box;
            color: rgb(85, 85, 85);
            cursor: default;
            display: inline-block;
            font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
            font-size: 14px;
            font-stretch: normal;
            font-style: normal;
            font-variant: normal;
            font-weight: normal;
            height: 34px;
            letter-spacing: normal;
            line-height: normal;
            margin-bottom: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-top: 0px;
            max-width: 280px;
            overflow-x: visible;
            overflow-y: visible;
            padding-bottom: 6px;
            padding-left: 12px;
            padding-right: 12px;
            padding-top: 6px;
            text-align: start;
            text-indent: 0px;
            text-rendering: auto;
            text-shadow: none;
            text-transform: none;
            transition-delay: 0s, 0s;
            transition-duration: 0.15s, 0.15s;
            transition-property: border-color, box-shadow;
            transition-timing-function: ease-in-out, ease-in-out;
            vertical-align: middle;
            white-space: pre;
            width: 48px;
            word-spacing: 0px;
            writing-mode: horizontal-tb;
            -webkit-appearance: menulist-button;
            -webkit-rtl-ordering: logical;
        }
    </style>
    <script  src="https://code.jquery.com/jquery-3.1.1.js" crossorigin="anonymous"></script>
    <link href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <script>
        function filter2(phrase, _id) {
            var words = phrase.value.toLowerCase().split(" ");
            var table = document.getElementById(_id);
            var ele;
            for (var r = 1; r < table.rows.length; r++) {
                ele = table.rows[r].innerHTML.replace(/<[^>]+>/g, "");
                var displayStyle = 'none';
                for (var i = 0; i < words.length; i++) {
                    if (ele.toLowerCase().indexOf(words[i]) >= 0)
                        displayStyle = '';
                    else {
                        displayStyle = 'none';
                        break;
                    }
                }
                table.rows[r].style.display = displayStyle;
            }
        }
        $(function () {
            $("#GridView1").DataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <div>
            <br />
            <br />
            
            <div class="input-group">
                <div class="form-inline">    
                    <h3 class="alert alert-info">Users Maintenance</h3>          
                    <table border="0" cellpadding="5" cellspacing="5" class="table bg-success table-responsive ">
                         <tr class="alert-danger">
                            <td>
                                <span>Login ID</span>
                            </td>
                            <td>
                                <span>Password</span>
                            </td>
                             <td>
                                <span>Staff Name</span>
                            </td>
                             <td>
                                <span>Role</span>
                            </td>
                             <td></td>
                            </tr>
                        
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="txtLoginID"  placeholder="Staff ID" required  CssClass="form-control" MaxLength="20"/>
                            </td>
                            <td>
                                 <asp:TextBox runat="server" ID="txtpwd"  placeholder="Password" TextMode="Password" required CssClass="form-control"/>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtName"  placeholder="Name" required  CssClass="form-control" MaxLength="500"/>
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlRole" runat="server" required  CssClass="form-control">
                                     <asp:ListItem Value="ADMIN">ADMIN</asp:ListItem>
                                     <asp:ListItem Value="SALES">SALES</asp:ListItem>
                                     <asp:ListItem Value="MANAGER">MANAGER</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button Text="Add" runat="server" ID="btnAdd" 
                                    class="btn btn-sm btn-primary" OnClick="btnAdd_Click" />
                            </td>
                          </tr>
                    </table>
                </div>
            </div>
            <br />
            <br />
            <div style="float:left;padding:10px;">
                Search : <input name="txtTerm" onkeyup="filter2(this, '<%=GridView1.ClientID %>')" type="text" />
            </div>
          <asp:GridView ID="GridView1" runat="server" 
                CssClass="table table-responsive table-striped" AutoGenerateColumns="False" 
                DataKeyNames="Id" DataSourceID="LibraryDb">
              <Columns>
                  <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                  <asp:BoundField DataField="Id" HeaderText="Id" 
                      SortExpression="Id" ReadOnly="True" Visible="False" InsertVisible="False"  />
                  <asp:BoundField DataField="No" HeaderText="No" SortExpression="No" 
                      ReadOnly="True" InsertVisible="False"  />
                  <asp:TemplateField HeaderText="Login ID">
                   <ItemTemplate>
                      <asp:label ID="txtLoginStaffID" runat="server"  Text='<%# Bind("LoginStaffID")%>'></asp:label>
                   </ItemTemplate>
                   </asp:TemplateField>
                  <asp:TemplateField HeaderText="Password">
                  <EditItemTemplate>
                      <asp:label ID="txtPwd" runat="server"  Text="******"></asp:label>
                  </EditItemTemplate>
                   <ItemTemplate>
                      <asp:label ID="txtPwd" runat="server"  Text="******"></asp:label>
                   </ItemTemplate>
                   </asp:TemplateField>
                  <asp:TemplateField HeaderText="Name">
                  <EditItemTemplate>
                      <asp:TextBox ID="txtStaffName" runat="server"  Text='<%# Bind("StaffName")%>' CssClass="form-control" MaxLength="500"></asp:TextBox>
                  </EditItemTemplate>
                   <ItemTemplate>
                      <asp:label ID="txtStaffName" runat="server"  Text='<%# Bind("StaffName")%>'></asp:label>
                   </ItemTemplate>
                   </asp:TemplateField>
                  <asp:TemplateField HeaderText="Role">
                    <EditItemTemplate>
                       <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" SelectedValue='<%# Bind("Role") %>'>
                          <asp:ListItem Value="ADMIN">ADMIN</asp:ListItem>
                          <asp:ListItem Value="SALES">SALES</asp:ListItem>
                          <asp:ListItem Value="MANAGER">MANAGER</asp:ListItem>
                       </asp:DropDownList>
                     </EditItemTemplate>
                    <ItemTemplate>
                        <asp:label ID="txtRole" ReadOnly="True" runat="server" Text='<%# Bind("Role")%>'></asp:label>
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField DataField="UpdDate" ReadOnly="True" HeaderText="Updated Date" 
                      SortExpression="UpdDate" />
              </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="LibraryDb" runat="server" 
                ConnectionString="<%$ ConnectionStrings:SalesConnectionString %>" 
                SelectCommand=" Select Row_Number() Over (Order By LoginStaffID) As No,ID,LoginStaffID,Pwd,StaffName,Role,UpdDate,UserID from UserMaster"
                DeleteCommand="DELETE FROM [UserMaster] WHERE [Id] = @Id" 
                UpdateCommand="UPDATE [UserMaster] SET [StaffName] = @StaffName,[Role] = @Role,[UpdDate]=GETDATE() WHERE [Id] = @Id" >
                <DeleteParameters>
                    <asp:Parameter Name="Id"/>
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="LoginStaffID" />
                    <asp:Parameter Name="Pwd"/>
                    <asp:Parameter Name="StaffName" />
                    <asp:Parameter Name="Role" />
                    <asp:Parameter Name="UpdDate" />
                    <asp:Parameter Name="UserId" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="StaffName" />
                    <asp:Parameter Name="Role" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </center>
</asp:Content>




