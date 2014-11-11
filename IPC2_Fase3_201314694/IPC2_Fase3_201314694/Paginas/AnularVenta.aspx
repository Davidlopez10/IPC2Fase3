<%@ Page Title="" Language="C#" MasterPageFile="~/Maestras/Ventas.Master" AutoEventWireup="true" CodeBehind="AnularVenta.aspx.cs" Inherits="IPC2_Fase3_201314694.Paginas.AnularVenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">INICIO</asp:LinkButton>
    <br />
    <br />
    <br />
    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">ANULAR VENTA</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="-1">
        <asp:View ID="View1" runat="server">
            <br /><br />
<h1 style="text-align:center">ANULAR VENTA</h1>
    <br />
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="NO DE ORDEN"></asp:Label>
            </td>
            <td>

            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="FECHA DE ANULACION DE VENTA"></asp:Label></td>
            <td></td>
            <td>
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            </td>
        </tr>        
    </table>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Razon de Anulacion" Font-Size="20px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="73px" Width="240px"></asp:TextBox>
                    </td>
                </tr>
            </table>
    <p>
        <asp:Button ID="Button1" runat="server" Text="ANULAR VENTA" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="EMITIR RECIBO" OnClick="Button2_Click" Visible="false"/>
        <asp:Button ID="Button3" runat="server" Text="EMITIR FACTURA" OnClick="Button3_Click" Visible="false" />
    </p>
            <p style="text-align:center;">
                <asp:Label ID="Label4" runat="server" ForeColor="Red" Font-Bold="True" Font-Italic="False" Font-Names="Bell MT" Font-Size="30px"></asp:Label>
            </p>
        </asp:View>
        <asp:View ID="View2" runat="server">
        </asp:View>
    </asp:MultiView>
    <br />
    
</asp:Content>
