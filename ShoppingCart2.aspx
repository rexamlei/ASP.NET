<%@ Page Title="" Language="C#" MasterPageFile="~/Shop/Layout.master" AutoEventWireup="true" CodeFile="ShoppingCart2.aspx.cs" Inherits="Shop_ShoppingCart2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.gvList{ margin:0 auto; width:700px;text-align:center;line-height:20px;}
.divBorder{width:700px;margin:0 auto;}
</style>
    <script type="text/javascript">
        //点击+号图，数量+1
        function Plus(obj) {
            obj.value = parseInt(obj.value) + 1;
        }
        //数量-1
        function Reduce(obj) {
            if (obj.value > 1) {
                obj.value = obj.value - 1;
            }
        }
        //替换txtAmount文本框非整数的输入
        //数据整个不合法时置1
        function CheckValue(obj) {
            var v = obj.value.replace(/[^\d]/g, '');
            if (v == '' || v == 'NaN') {
                obj.value = "1";
            }
            else {
                obj.value = v;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div id="zone">
        <div align="left" class="divBorder">
            <img src="../image/back.jpg" onclick="javascript:location.href='WareListView.aspx';"
                style="cursor: hand" alt="返回产品列表" border="0" title="返回产品列表" />
            <img src="../image/cart_001.jpg" alt="我的购物车" />
        </div>
        <div class="divBorder">
            <asp:GridView ID="gvCart" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" CssClass="gvList"
                ShowFooter="True" Width="100%" OnRowDataBound="gvCart_RowDataBound" OnRowDeleting="gvCart_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ProductNo" HeaderText="产品编号">
                        <ItemStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductName" HeaderText="产品名称" />
                    <asp:TemplateField HeaderText="产品单价">
                        <ItemStyle Width="80px" />
                        <ItemTemplate>
                            <%# Convert.ToInt32(Eval("BuyPrice")).ToString("c2") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="数量">
                        <ItemStyle Width="130px" />
                        <ItemTemplate>
                            <img src="../image/bg_close.jpg" id="imgReduce"  runat="server" />
                            <asp:TextBox ID="txtAmount" Width="20px" Height="16px" runat="server" Text='<%# Eval("Amount") %>'
                                onkeyup="CheckValue(this)"></asp:TextBox>
                            <img src="../image/bg_open.jpg" id="imgPlus" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:CommandField ShowDeleteButton="True" />
                </Columns>
                <EmptyDataTemplate>
                    您的购物车中没有任何商品。
                </EmptyDataTemplate>
                <AlternatingRowStyle BackColor="WhiteSmoke" />
            </asp:GridView>
        </div>
        <div class="divBorder">
             <a href="WareListView.aspx"><img src="../image/go_on.jpg" border="0" /></a>&nbsp;
             <asp:ImageButton ID="imgbtnTotal" runat="server" ImageUrl="~/image/updateCart.jpg" OnClick="imgbtnTotal_Click" />&nbsp;
             <a href="Order.aspx"><img src="../image/pay.jpg" /></a>
        </div> 
    </div>
</asp:Content>

