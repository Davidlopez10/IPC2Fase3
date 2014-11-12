<%@ Page Title="" Language="C#" MasterPageFile="~/Maestras/Gerente.Master" AutoEventWireup="true" CodeBehind="Gerente.aspx.cs" Inherits="IPC2_Fase3_201314694.Paginas.Gerente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />

    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">INICIO</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ventas" />
                <asp:Button ID="Button2" runat="server" Text="Reportes" OnClick="Button2_Click" />
                <br />
            </asp:View>
            <asp:View ID="View2" runat="server">
                <br />
                <h1 style="text-align:center; font-size:25px;">Reportes</h1>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Nit Cliente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="NitClientes" DataTextField="NITCLIENTE" DataValueField="NITCLIENTE"></asp:DropDownList>
                            <asp:SqlDataSource ID="NitClientes" runat="server" ConnectionString="<%$ ConnectionStrings:NuevaFase3 %>" SelectCommand="SELECT DISTINCT CLIENTE.NITCLIENTE FROM CLIENTE INNER JOIN ORDEN ON ORDEN.NITCLIENTE = CLIENTE.NITCLIENTE WHERE (CLIENTE.CANTIDADORDENES &gt; 0) AND (ORDEN.TOTALPAGAR &gt; 0)">
                            </asp:SqlDataSource>
                        </td>
                        <td>
                            <asp:Button ID="Button3" runat="server" Text="Reporte ventasXcliente" OnClick="Button3_Click" /><br />
                        </td>
                    </tr>
                    
                </table>
                <br />
                <br />
               <table>
                   <tr>
                       <td>
                           <asp:Label ID="Label2" runat="server" Text="CATEGORIA: "></asp:Label>
                       </td>
                       <td>
                           <asp:Button ID="Button7" runat="server" Text="REPORTE ventasXcategoria" OnClick="Button7_Click" />     
                       </td>
                   </tr>
                   <tr>
                       <td>
                           <asp:Label ID="Label3" runat="server" Text="PRODUCTOS: "></asp:Label>
                       </td>
                       <td>
                         <asp:Button ID="Button4" runat="server" Text="Reporte ventasXproducto" OnClick="Button4_Click" /><br /></td>
                   </tr>

               </table>
               
                <asp:Button ID="Button5" runat="server" Text="Reporte ventasXMetas" /><br />
                <asp:Button ID="Button6" runat="server" Text="Reporte metasXcategoria" /><br />
            </asp:View>
            <asp:View ID="View3" runat="server"></asp:View>

        </asp:MultiView>
        <br />
        <br />         
    
</asp:Content>
