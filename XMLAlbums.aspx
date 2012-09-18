<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XMLAlbums.aspx.cs" Inherits="PhotoData.XMLAlbums" %><?xml version="1.0"?>
<asp:accessdatasource runat="server" DataFile="~/App_Data/photos.mdb" ID="AlbumListGenerator"/>
<Model><Albums><% PutAlbumList(); %>
</Albums>
</Model>
