<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SalesMaster.aspx.cs" Inherits="SalesReportsSystem.SalesMaster" %>
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
<link href="https://code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css" rel="stylesheet" />
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>
<script type="text/javascript">
    function AllowDotNumericOnly(e) {
        if (event.keyCode > 47 && event.keyCode < 58 || event.keyCode == 46) {
            var txtbx = document.getElementById(txt);
            var amount = document.getElementById(txt).value;
            var present = 0;
            var count = 0;

            if (amount.indexOf(".", present) || amount.indexOf(".", present + 1));
            {
            }
            do {
                present = amount.indexOf(".", present);
                if (present != -1) {
                    count++;
                    present++;
                }
            }
            while (present != -1);
            if (present == -1 && amount.length == 0 && event.keyCode == 46) {
                event.keyCode = 0;
                return false;
            }
            if (count >= 1 && event.keyCode == 46) {

                event.keyCode = 0;
                return false;
            }
            if (count == 1) {
                var lastdigits = amount.substring(amount.indexOf(".") + 1, amount.length);
                if (lastdigits.length >= 2) {
                    event.keyCode = 0;
                    return false;
                }
            }
            return true;
        }
        else {
            event.keyCode = 0;
            return false;
        }
    }
    $(document).ready(function () {
        $('input[id*=txtEditSalesDate]').datepicker({
            dateFormat: 'd/m/yy',
            changeMonth: true,
            changeYear: true,
            maxDate: 0,
        });
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
                    <h3 class="alert alert-danger">Sales Maintenance</h3>          
                    <table border="0" cellpadding="5" cellspacing="5" class="table bg-success table-responsive ">
                        <tr class="alert-info">
                            <td>
                                <span>Sales Date</span>
                            </td>
                            <td>

                            </td>
                            <td>
                                <span>Sales Item </span>
                            </td>
                            <td>
                                <span>Qty</span>
                            </td>
                            <td>
                                <span>Unit Price</span>
                            </td>
                          </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="txtSalesDate"  placeholder="d/m/yyyy" CssClass="form-control" Enabled="false"/>
                            </td>
                            <td>
                                <asp:Calendar ID="Calendar1" runat="server" Visible ="false" OnSelectionChanged="OnSelectionChanged_Date" OnDayRender="Calendar1_DayRender"></asp:Calendar> 
                                <asp:LinkButton runat="server" ID="DateLink" OnClick="DateLink_Click"><asp:Image ID="Cal1" runat="server" ImageUrl="~/Images/cal.gif"/></asp:LinkButton>
                            </td>
                            <td>
                               <asp:dropdownlist runat="server" ID="ddlItems"  required CssClass="form-control" 
                                    DataSourceID="ItemsDb" DataTextField="ItemName" DataValueField="ItemName"></asp:dropdownlist>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtQty"  placeholder="numeric only" required  CssClass="form-control" OnKeyPress="return AllowDotNumericOnly(this);"/>
                            </td>
                            <td>
                                 <asp:TextBox runat="server" ID="txtUnitPrice"  placeholder="eg: 2.45" required  CssClass="form-control" OnKeyPress="return AllowDotNumericOnly(this);"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                              <asp:Button Text="Add" runat="server" ID="btnAdd" class="btn btn-sm btn-primary" OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <br />
          <asp:GridView ID="GridView1" runat="server" 
                CssClass="table table-responsive table-striped" AutoGenerateColumns="False" 
                DataKeyNames="Id" DataSourceID="LibraryDb" OnRowUpdating="UpdateSales" >
              <Columns>
                  <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                  <asp:BoundField DataField="Id" HeaderText="Id" 
                      SortExpression="Id" ReadOnly="True" Visible="False" InsertVisible="False"  />
                  <asp:BoundField DataField="No" HeaderText="No" SortExpression="No" 
                      ReadOnly="True" InsertVisible="False"  />
                 <asp:TemplateField HeaderText="Sales Date">
                 <EditItemTemplate>
                 <%-- <asp:TextBox ID="txtEditSalesDate" runat="server" Text='<%# Bind("SalesDate")%>'/>--%>
                     <asp:label ID="txtSalesDate" runat="server"  Text='<%# Bind("SalesDate")%>'></asp:label>
                </EditItemTemplate>
                   <ItemTemplate>
                      <asp:label ID="txtSalesDate" runat="server"  Text='<%# Bind("SalesDate")%>'></asp:label>
                   </ItemTemplate>
                   </asp:TemplateField>
                 <asp:TemplateField HeaderText="Sales Item">
                    <EditItemTemplate>
                    <asp:dropdownlist runat="server" ID="ddlItems"  required CssClass="form-control" SelectedValue='<%# Bind("SalesItem") %>'
                      DataSourceID="ItemsDb" DataTextField="ItemName" DataValueField="ItemName"></asp:dropdownlist>
                     </EditItemTemplate>
                    <ItemTemplate>
                        <asp:label ID="txtitem" ReadOnly="True" runat="server" Text='<%# Bind("SalesItem")%>'></asp:label>
                    </ItemTemplate>
                  </asp:TemplateField>
                 <asp:TemplateField HeaderText="Qty">
                  <EditItemTemplate>
                      <asp:TextBox ID="txtQty" runat="server"  Text='<%# Bind("Qty")%>' CssClass="form-control" MaxLength="20" OnKeyPress="return AllowDotNumericOnly(this);"></asp:TextBox>
                  </EditItemTemplate>
                   <ItemTemplate>
                      <asp:label ID="txtQty" runat="server"  Text='<%# Bind("Qty")%>'></asp:label>
                   </ItemTemplate>
                   </asp:TemplateField>
                 <asp:TemplateField HeaderText="Unit Price">
                  <EditItemTemplate>
                      <asp:TextBox ID="txtUnitPrice" runat="server"  Text='<%# Bind("UnitPrice")%>' CssClass="form-control" MaxLength="15" OnKeyPress="return AllowDotNumericOnly(this);"></asp:TextBox>
                  </EditItemTemplate>
                   <ItemTemplate>
                      <asp:label ID="txtUnitPrice" runat="server"  Text='<%# Bind("UnitPrice")%>'></asp:label>
                   </ItemTemplate>
                   </asp:TemplateField>
                  <asp:BoundField DataField="TotalPrice" ReadOnly="True" HeaderText="Total Price" 
                      SortExpression="TotalPrice" />
                  <asp:BoundField DataField="UpdDate" ReadOnly="True" HeaderText="Updated Date" 
                      SortExpression="UpdDate" />
              </Columns>
            </asp:GridView>
          <asp:SqlDataSource ID="ItemsDb" runat="server" 
               ConnectionString="<%$ ConnectionStrings:SalesConnectionString %>">
            </asp:SqlDataSource>
          <asp:SqlDataSource ID="LibraryDb" runat="server" 
                ConnectionString="<%$ ConnectionStrings:SalesConnectionString %>" 
                DeleteCommand="DELETE FROM [SalesTrans] WHERE [Id] = @Id" 
                InsertCommand="INSERT INTO [SalesTrans] ([LoginStaffID], [SalesDate], [SalesItem], [Qty], [UnitPrice], [UpdDate], [UserID]) VALUES (@LoginStaffID, @SalesDate, @SalesItem, @Qty, @UnitPrice, @UpdDate, @UserID)" >
                <DeleteParameters>
                    <asp:Parameter Name="Id"/>
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="LoginStaffID" />
                    <asp:Parameter Name="SalesDate" />
                    <asp:Parameter Name="SalesItem" />
                    <asp:Parameter Name="Qty" />
                    <asp:Parameter Name="UnitPrice" />
                    <asp:Parameter Name="UpdDate" />
                    <asp:Parameter Name="UserId" />
                </InsertParameters>
            </asp:SqlDataSource>
        </div>
    </center>
</asp:Content>

