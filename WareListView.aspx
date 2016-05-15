<%@ Page Title="" Language="C#" MasterPageFile="~/Shop/Layout.master" AutoEventWireup="true" CodeFile="WareListView.aspx.cs" Inherits="Shop_WareListView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DataList ID="DataList1" DataKeyField="Ware_id" runat="server" RepeatColumns="3">
        <ItemTemplate>
            <table style="width:100%;">
                <tr>
                    <td rowspan="6">
                        <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("Ware_Image") %>' Width="120" />
                    </td>
                    <td class="auto-style1">商品编号</td>
                    <td class="auto-style1">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Ware_Number") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>商品名</td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Ware_Name") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>商品价格</td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Ware_Price") %>'></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="31px" 
                            ImageUrl="~/image/buy.png" Width="90px" 
                            CommandArgument='<%# Eval("Ware_id") %>' onclick="ImageButton1_Click" />
                     </td>
                    <td>&nbsp;</td>
                </tr>
                 <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                 <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
        <asp:Label ID="lblCurrent" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblCount" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="lbtnFirst" runat="server" Text="第一页" OnClick="lbtnFirst_Click" />
        <asp:Button ID="lbtnPrev" runat="server" Text="上一页" OnClick="lbtnPrev_Click" />
        <asp:Button ID="lbtnNext" runat="server" Text="下一页" OnClick="lbtnNext_Click" />
        <asp:Button ID="lbtnLast" runat="server" Text="最后页" OnClick="lbtnLast_Click" />
</asp:Content>

