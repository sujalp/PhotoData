<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XMLYears.aspx.cs" Inherits="NewPV.Web.MainData" %><?xml version="1.0"?>
<asp:accessdatasource runat="server" DataFile="~/App_Data/photos.mdb" ID="AlbumListGenerator"/>
<Model><Years><% PutYearList(); %>
</Years>
</Model>