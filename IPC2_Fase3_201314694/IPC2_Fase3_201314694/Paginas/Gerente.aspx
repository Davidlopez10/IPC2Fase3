<%@ Page Title="" Language="C#" MasterPageFile="~/Maestras/Gerente.Master" AutoEventWireup="true" CodeBehind="Gerente.aspx.cs" Inherits="IPC2_Fase3_201314694.Paginas.Gerente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Ventas</asp:LinkButton><br /><br /><br />
    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Reportes</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server"></asp:View>
            <asp:View ID="View2" runat="server">
                <div id="degradado1">
                    <br />
                <h1 style="text-align:center; font-size:25px;">Reportes</h1>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Nit Cliente" Font-Bold="True" Font-Size="18px"></asp:Label>
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
                    <br />
                    <table>
                        <tr>
                            <td>Mes</td>
                            <td>Opcion</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                 <asp:Button ID="Button5" runat="server" Text="Reporte ventasXMetas" OnClick="Button5_Click" /><br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="Button6" runat="server" Text="Reporte metasXcategoria" OnClick="Button6_Click" /><br />
                            </td>
                        </tr>
                    </table>               
                </div>                
            </asp:View>
            <asp:View ID="View3" runat="server"></asp:View>

        </asp:MultiView>
        <br />
        <br />         
    
</asp:Content>
