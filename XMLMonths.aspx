<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XMLMonths.aspx.cs" Inherits="PhotoData.Months" %><?xml version="1.0"?>
<asp:accessdatasource runat="server" DataFile="~/App_Data/photos.mdb" ID="AlbumListGenerator"/>
<Model><Months><% PutMonthList(); %>
</Months>
</Model>
