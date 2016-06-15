<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.master" AutoEventWireup="true" CodeFile="AddItem.aspx.cs" Inherits="Admin_AddItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td align="right">
                <span style="color: rgb(0, 0, 0); font-family: Simsun; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: -webkit-right; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
                商品编码：</span></td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span style="color: rgb(0, 0, 0); font-family: Simsun; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: -webkit-right; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
                商品名称：</span></td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span style="color: rgb(0, 0, 0); font-family: Simsun; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: -webkit-right; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
                计量单位：</span></td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>台</asp:ListItem>
                    <asp:ListItem>件</asp:ListItem>
                    <asp:ListItem>个</asp:ListItem>
                    <asp:ListItem>包</asp:ListItem>
                    <asp:ListItem>套</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span style="color: rgb(0, 0, 0); font-family: Simsun; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: -webkit-right; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
                库存：</span></td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span style="color: rgb(0, 0, 0); font-family: Simsun; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: -webkit-right; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
                单价：</span></td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span style="color: rgb(0, 0, 0); font-family: Simsun; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: -webkit-right; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
                所属类别：</span></td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span style="color: rgb(0, 0, 0); font-family: Simsun; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: -webkit-right; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
                具体类别：</span></td>
            <td>
                <asp:DropDownList ID="ddlJDLX" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlJDLX_SelectedIndexChanged">
                </asp:DropDownList>
&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" Height="30px" 
                    ImageUrl="~/image/Show.jpg" Width="22px" onclick="ImageButton1_Click" />
                <br />
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Column_Name" />
                        <asp:TemplateField HeaderText="信息">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Data_Type" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:ImageButton ID="ImageButton2" runat="server" 
                        ImageUrl="~/image/bg_open.jpg" onclick="ImageButton2_Click" />
                    &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" 
                        ImageUrl="~/image/bg_close.jpg" onclick="ImageButton3_Click" />
                    <br />
                    表 名：<br /> 
                    <asp:TextBox ID="TxtTableName" runat="server"></asp:TextBox>
                    <br />
                    表类型：<br /> 
                    <asp:TextBox ID="TxtTypeName" runat="server"></asp:TextBox>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkID" runat="server" /></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="字段名">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="数据类型">
                                <ItemTemplate>
                                <asp:DropDownList ID="DropDownList4" runat="server">
                                    <asp:ListItem>nvarchar(500)</asp:ListItem>
                                    <asp:ListItem>int</asp:ListItem>
                                    <asp:ListItem>float</asp:ListItem>             
                                 </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:ImageButton ID="BtnSaveInfo" runat="server" Height="37px" 
                        ImageUrl="~/image/Save.jpg" Width="32px" onclick="BtnSaveInfo_Click" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Image ID="Image1" runat="server" Height="120px" Width="131px" />
                <br />
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:ImageButton ID="ImageButton5" runat="server" Height="21px" 
                    ImageUrl="~/image/Upload.jpg" Width="22px" onclick="ImageButton5_Click" />
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:ImageButton ID="ImageButton6" runat="server" Height="45px" 
                    ImageUrl="~/image/Add.jpg" Width="37px" onclick="ImageButton6_Click" />
            </td>
        </tr>

    </table>
</asp:Content>

