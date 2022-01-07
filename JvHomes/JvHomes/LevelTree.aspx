<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LevelTree.aspx.cs" Inherits="JvHomes.LevelTree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="hdf">
    <!-- Required meta tags -->
   
    <title>VNV INFRA STRCUTURE</title>
   
</head><body>
    <form id="form1" runat="server">
        <div id="BrokerTree">
            <asp:TreeView ID="trvBroker" runat="server" ExpandDepth="1" ImageSet="Simple" ShowLines="True">
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <NodeStyle CssClass="gridViewToolTip" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
            </asp:TreeView>


        </div>
    </form>
      
</body>
</html>
