<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoirImage.aspx.cs" Inherits="ClientWeb.VoirImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            <asp:Image ID="ImageCourante" runat="server" />
        </p>
        <p>
            Image&nbsp;ID&nbsp;:&nbsp;
            <asp:TextBox ID="ImageIDBox" runat="server" />
            <asp:Button ID="Visualiser" runat="server" OnClick="Visualiser_Click"
            Text="Visualiser" />
        </p>
    </div>
    </form>
</body>
</html>
