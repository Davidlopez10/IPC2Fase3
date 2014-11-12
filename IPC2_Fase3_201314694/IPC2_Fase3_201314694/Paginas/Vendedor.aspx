<%@ Page Title="" Language="C#" MasterPageFile="~/Maestras/Vendedor.Master" AutoEventWireup="true" CodeBehind="Vendedor.aspx.cs" Inherits="IPC2_Fase3_201314694.Paginas.Vendedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <p>
     <br />
    </p>
    <p>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">VENTAS</asp:LinkButton>
    </p>
    <p>
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">REPORTES</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="-1">
        <asp:View ID="View1" runat="server">
            <div id="degradado">
                 <br />
                <h1 style="text-align:center; font-size:30px;">REPORTES</h1>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Reporte ventasXMetas" OnClick="Button1_Click" /><br />
                <asp:Button ID="Button2" runat="server" Text="Reporte metasXcategoria" OnClick="Button2_Click" />

            </div>         
        </asp:View>
    </asp:MultiView>
</asp:Content>
