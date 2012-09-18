<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XMLOneAlbum.aspx.cs" Inherits="NewPV.Web.OneAlbum" %><?xml version="1.0"?>
<asp:accessdatasource runat="server" DataFile="~/App_Data/photos.mdb" ID="AlbumListGenerator"/>
<Model><PhotoList><%PutPhotoList(); %>
</PhotoList>
</Model>