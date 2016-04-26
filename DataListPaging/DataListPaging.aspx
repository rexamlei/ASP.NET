<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataListPaging.aspx.cs" Inherits="DataListPaging"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>DataList分页实例</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height:200px;"> 
        <asp:DataList ID="DataList1" runat="server">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("sname") %>'></asp:Label>
            </ItemTemplate>
        </asp:DataList>
     </div>
        <asp:Label ID="lblCurrent" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblCount" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="lbtnFirst" runat="server" Text="第一页" OnClick="lbtnFirst_Click" />
        <asp:Button ID="lbtnPrev" runat="server" Text="上一页" OnClick="lbtnPrev_Click" />
        <asp:Button ID="lbtnNext" runat="server" Text="下一页" OnClick="lbtnNext_Click" />
        <asp:Button ID="lbtnLast" runat="server" Text="最后页" OnClick="lbtnLast_Click" />
    </form>
</body>
</html>
