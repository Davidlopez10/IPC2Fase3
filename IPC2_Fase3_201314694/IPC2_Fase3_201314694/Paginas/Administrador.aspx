<%@ Page Title="" Language="C#" MasterPageFile="~/Maestras/Anonimo.Master" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="IPC2_Fase3_201314694.Paginas.Administrador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align:center">Cargar Archivo</h1>
    <div >
        <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label><br /><br />
        <asp:FileUpload ID="cargarArchivo" runat="server" BorderStyle="Inset"/><br /><br />
        <asp:ImageButton ID="ImageButton1" runat="server" Height="80px" ImageUrl="~/Imagenes/subir-archivos-Hotmail.jpg" OnClick="ImageButton1_Click" Width="100px" ImageAlign="Baseline"/>
    </div>
</asp:Content>
