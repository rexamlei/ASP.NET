<%@ Page Title="" Language="C#" MasterPageFile="~/Shop/Layout.master" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Shop_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function DisplayAddr() {
        document.getElementById("<%=divadd.ClientID %>").style.display = "block";
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:50%;">
        <tr>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td align="right" class="style1">订单编号：</td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">收件人：</td>
            <td>
                <asp:Label ID="lblUser" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">地址：</td>
            <td>
                <asp:DropDownList ID="ddlAddress" runat="server">
                </asp:DropDownList>
                <img id="imgExtend" runat="server" src="../image/play.jpg" style="height:20px;width:20px;vertical-align:middle;" /></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <div id="divadd" runat="server" style="display:none">
                新地址：
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" 
                    ImageUrl="~/image/bg_open.jpg" onclick="ImageButton2_Click" />
                </div>                
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:GridView ID="gvInfo" runat="server">
                </asp:GridView>
            </td>
        </tr>
        <tr>
             <td align="right"></td>
            <td>
               
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/image/pay.jpg" 
                    onclick="ImageButton1_Click" />
               
            </td>
        </tr>
    </table>
</asp:Content>

