<%@ Page Title="" Language="C#" MasterPageFile="~/Maestras/Supervisor.Master" AutoEventWireup="true" CodeBehind="Supervisor.aspx.cs" Inherits="IPC2_Fase3_201314694.Paginas.Supervisor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <p>
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">VENTAS</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="degradado2">
            <br />
        <br />
        <h1 style="text-align:center; font-size:25px;">Reportes</h1>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Reporte ventasXMetas" /><br />
    <asp:Button ID="Button2" runat="server" Text="Reporte metasXcategoria" />
    </div>
</asp:Content>
