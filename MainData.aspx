<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainData.aspx.cs" Inherits="NewPV.Web.MainData" %><?xml version="1.0"?>
<asp:accessdatasource runat="server" DataFile="~/App_Data/photos.mdb" ID="AlbumListGenerator"/>
<Model><AlbumList>
<asp:Repeater id="Albums" runat="server">
<ItemTemplate><Album Title="<%# GetString('T', Container.DataItem) %>" Year="<%# GetString('Y', Container.DataItem) %>" Month="<%# GetString('M', Container.DataItem) %>" Hash="<%# GetString('H', Container.DataItem) %>" Count="<%# GetString('C', Container.DataItem) %>" Photo="<%# GetString('P', Container.DataItem) %>"/>
</ItemTemplate>
</asp:Repeater></AlbumList>
<PhotoList>
<asp:Repeater id="Photos" runat="server">
<ItemTemplate><Photo Source="<%# GetPhotoString('U', Container.DataItem) %>" AlbumTitle="<%# GetPhotoString('A', Container.DataItem) %>" People="<%# GetPhotoString('P', Container.DataItem) %>" Place="<%# GetPhotoString('L', Container.DataItem) %>" DateTime="<%# GetPhotoString('D', Container.DataItem) %>" Title="<%# GetPhotoString('T', Container.DataItem) %>" FI="<%# GetPhotoString('I', Container.DataItem) %>" FS="<%# GetPhotoString('S', Container.DataItem) %>" FO="<%# GetPhotoString('O', Container.DataItem) %>" FF="<%# GetPhotoString('F', Container.DataItem) %>" FV="<%# GetPhotoString('V', Container.DataItem) %>"/>
</ItemTemplate>
</asp:Repeater></PhotoList>
</Model>
