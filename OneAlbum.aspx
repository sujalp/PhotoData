<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OneAlbum.aspx.cs" Inherits="PhotoData.OneAlbum" %>
<asp:accessdatasource runat="server" DataFile="~/App_Data/photos.mdb" ID="AlbumListGenerator"/>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
        function donavigate(url) {
            document.location = url;
        }
    </script>
</head>
<body style="margin:0; background:black; color:white; font-family:Segoe UI Light;">
    <form id="form1" runat="server">
    <center><span style="font-size:40pt; cursor:pointer" onclick="donavigate('years.aspx')">Parikh Family Photos</span>
                   <span style="font-size:xx-small; cursor:pointer" onclick="donavigate('lucky.aspx')">I'm feeling Lucky</span></center>
    <p style="margin-top:50px"/>
<center><%PutPhotoList(); %></center>
</form>
</body>
</html>
