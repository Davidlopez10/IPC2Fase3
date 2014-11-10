<%@ Page Title="" Language="C#" MasterPageFile="~/Maestras/Ventas.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="IPC2_Fase3_201314694.Paginas.Ventas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 133px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br /><br />
    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click">Inicio</asp:LinkButton><br /><br /><br />
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Crear Orden</asp:LinkButton><br /><br /><br />
    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Cerrar Orden</asp:LinkButton><br /><br /><br />
    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Anular Orden</asp:LinkButton><br /><br /><br />
    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click">Realizar Pagos</asp:LinkButton><br /><br /><br />
    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Aprobar Orden</asp:LinkButton><br /><br /><br />
    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click">Anular Venta</asp:LinkButton>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language='JavaScript'>
        document.onkeydown = function (evt) { return (evt ? evt.which : event.keyCode) != 13; }
</script>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="-1">
        <asp:View ID="View1" runat="server">
             <br />
            <br />
             <br />
            <h1 style="text-align:center">CREAR ORDEN</h1>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="NIT CLIENTE:"></asp:Label>
                    </td>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" Width="50px"></asp:Panel>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="NO. ORDEN"></asp:Label>
                    </td>
                    <td>
                        <asp:Panel ID="Panel2" runat="server" Width="50px"></asp:Panel>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="FECHA"></asp:Label>
                    </td>
                    <td>

                    </td>
                    <td>
                        <asp:Label ID="Label28" runat="server" Text="LISTA DE PRECIOS"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td>
             <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="CLIENTES" DataTextField="NITCLIENTE" DataValueField="NITCLIENTE" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
                        <asp:SqlDataSource ID="CLIENTES" runat="server" ConnectionString="<%$ ConnectionStrings:NuevaFase3 %>" SelectCommand="SELECT [NITCLIENTE] FROM [CLIENTE] WHERE ([LIMITECREDITO] &gt; @LIMITECREDITO)">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="LIMITECREDITO" Type="Double" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td>

                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="Number"></asp:TextBox>
                    </td>
                    <td>

                    </td>
                    <td>
                        <asp:Label ID="Label27" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Panel ID="Panel6" runat="server" Width="50px"></asp:Panel>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList3" runat="server"  Enabled="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="Button11" runat="server" Text="Seleccionar Lista" OnClick="Button11_Click" Enabled="false"/><br />
                        <asp:Button ID="Button12" runat="server" Text="Crear Lista" OnClick="Button12_Click" Visible="false" />
                    </td>
                    
                </tr>

            </table>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="LIMITE DE CREDITO: "></asp:Label>
                    </td>
                    <td>
                        <asp:Panel ID="Panel3" runat="server" Width="50px"></asp:Panel>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="CANTIDAD DE ORDENES VENCIDAS:"></asp:Label>
                    </td>
                    <td>
                        <asp:Panel ID="Panel4" runat="server" Width="50px"></asp:Panel>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="TOTAL"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>

                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>

                    </td>
                    <td>
                       Q: <asp:Label ID="Label11" runat="server" Text=" 0"></asp:Label>
                    </td>
                </tr>

            </table>
            <br />

             <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="CANTIDAD: "></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="NOMBRE DE PRODUCTO"></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Enabled="false" TextMode="Number"></asp:TextBox>
                    </td>
                    <td><asp:Panel runat="server" Width="50PX"></asp:Panel></td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="AGREGAR AL CARRITO" Width="161px" OnClick="Button1_Click" Enabled="false"/><br />
                        <br />
                    </td>
                </tr>
                
            </table>
             <br />
            <table>
                <tr>                    
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataKeyNames="IDDETALLE" DataSourceID="SqlDataSource3" GridLines="Horizontal" >
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:BoundField DataField="NOMBREPRODUCTO" HeaderText="PRODUCTO" SortExpression="NOMBREPRODUCTO" ReadOnly="True" />
                                <asp:BoundField DataField="VALOR" HeaderText="PRECIO UNITARIO" ReadOnly="True" SortExpression="VALOR" />
                                <asp:BoundField DataField="CANTIDAD" HeaderText="CANTIDAD" SortExpression="CANTIDAD" />
                            </Columns>                            
                            <EmptyDataTemplate>
                                <asp:Label ID="Label26" runat="server" Text="No hay Productos"></asp:Label>
                            </EmptyDataTemplate>
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <PagerTemplate>
                                No hay ningun registro
                            </PagerTemplate>
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#487575" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#275353" />
                        </asp:GridView> 
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:NuevaFase3 %>" DeleteCommand="DELETE FROM [DETALLEPRODUCTOORDEN] WHERE [IDDETALLE] = @original_IDDETALLE AND (([NOMBREPRODUCTO] = @original_NOMBREPRODUCTO) OR ([NOMBREPRODUCTO] IS NULL AND @original_NOMBREPRODUCTO IS NULL)) AND (([VALOR] = @original_VALOR) OR ([VALOR] IS NULL AND @original_VALOR IS NULL)) AND [CANTIDAD] = @original_CANTIDAD" InsertCommand="INSERT INTO [DETALLEPRODUCTOORDEN] ([NOMBREPRODUCTO], [VALOR], [CANTIDAD]) VALUES (@NOMBREPRODUCTO, @VALOR, @CANTIDAD)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT DETALLEPRODUCTOORDEN.IDDETALLE, DETALLEPRODUCTOORDEN.NOMBREPRODUCTO, DETALLEPRODUCTOORDEN.VALOR, DETALLEPRODUCTOORDEN.CANTIDAD FROM DETALLEPRODUCTOORDEN INNER JOIN ORDEN ON DETALLEPRODUCTOORDEN.CODIGOORDEN = ORDEN.CODIGOORDEN WHERE (DETALLEPRODUCTOORDEN.CODIGOORDEN = @CODIGOORDEN) AND (ORDEN.ESTADOAPROBACION = 'Procesando') AND (ORDEN.NITCLIENTE = @NITCLIENTE)" UpdateCommand="UPDATE [DETALLEPRODUCTOORDEN] SET  [CANTIDAD] = @CANTIDAD WHERE [IDDETALLE] = @original_IDDETALLE AND [CANTIDAD] = @original_CANTIDAD" OnSelecting="SqlDataSource3_Selecting">
                            <DeleteParameters>
                                <asp:Parameter Name="original_IDDETALLE" Type="Int32" />
                                <asp:Parameter Name="original_NOMBREPRODUCTO" Type="String" />
                                <asp:Parameter Name="original_VALOR" Type="String" />
                                <asp:Parameter Name="original_CANTIDAD" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="NOMBREPRODUCTO" Type="String" />
                                <asp:Parameter Name="VALOR" Type="String" />
                                <asp:Parameter Name="CANTIDAD" Type="Int32" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="TextBox2" Name="CODIGOORDEN" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="DropDownList1" Name="NITCLIENTE" PropertyName="SelectedValue" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="CANTIDAD" Type="Int32" />
                                <asp:Parameter Name="original_IDDETALLE" Type="Int32" />
                                <asp:Parameter Name="original_CANTIDAD" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>                
            </table>
            <br />
            <p style="text-align:center;">
                <asp:Label ID="Label35" runat="server" Text="" Font-Names="Arial Black" Font-Size="25px" ForeColor="Red" Visible="false"></asp:Label>
            </p>
            <br />
                 <p>
                     <asp:Button ID="Button10" runat="server" Text="Finalizacion de Orden" OnClick="Button10_Click" Enabled="false"/></p>      
        </asp:View>
        <asp:View ID="View2" runat="server">
            <br />
            <br />
            <h1 style="text-align:center">CERRAR ORDEN</h1>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="No ORDEN "></asp:Label>
                    </td>
                    <td>
                        <asp:Panel ID="Panel5" runat="server" Width="50px"></asp:Panel>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="true" ></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="Seleccionar Orden" OnClick="Button2_Click" />
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                    <td>

                    </td>
                    <td>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="FECHA PARA CERRAR"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label29" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>

                    <td>
                        <asp:Label ID="Label13" runat="server" Text="TOTAL: "></asp:Label>
                    </td>
                    <td></td>
                    <td class="auto-style3">
                        <asp:Label ID="Label14" runat="server" Text=""></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="CREDITO DISPONIBLE:"></asp:Label>
                    </td>
                    <td>            </td>
                    <td><asp:Label ID="Label16" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="APLICA MONTO MINIMO"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="SI" />
                        <asp:CheckBox ID="CheckBox2" runat="server"  Text="NO"/>
                    </td>
                      <td> <asp:Label ID="Label31" runat="server" Text=""></asp:Label>           </td>
                </tr>
            </table>
            <br />
            <br />
            <p style="text-align:center">
                <asp:Label ID="Label36" runat="server" Text="" ForeColor="Red" Font-Names="Arial Black" Font-Size="25px" Visible="false"></asp:Label>
            </p>
            <p>
                <asp:Button ID="Button4" runat="server" Text="CERRAR ORDEN" OnClick="Button4_Click" Enabled="false" /><asp:Button ID="Button5" runat="server" Text="CANCELAR" OnClick="Button5_Click" style="height: 26px" />
            </p>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <br />
            <br />
            <h1 style="text-align:center">APROBAR ORDEN</h1>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label18" runat="server" Text="NO ORDEN"></asp:Label>
                    </td>
                    <td>

                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="true" ></asp:DropDownList>
                    </td>
                    <td>

                        <asp:Button ID="Button3" runat="server" Text="Seleccionar Orden" OnClick="Button3_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label20" runat="server" Text="FECHA DE APROBACION"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label30" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="NOMBRE: "></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label23" runat="server" Text="PUESTO"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label24" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label33" runat="server" Text="Orden Aprobada" Visible="False" ForeColor="#0066FF"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label34" runat="server" Visible="False" Font-Names="Adobe Gothic Std B" Font-Size="20px" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <p style="text-align:center">
                <asp:Label ID="Label37" runat="server" Font-Names="Arial Black" Font-Size="25px" ForeColor="Red" Visible="false"></asp:Label>
            </p>
            <p>
                <asp:Button ID="Button6" runat="server" Text="APROBAR ORDEN" OnClick="Button6_Click" Enabled="false" />
                <asp:Button ID="Button7" runat="server" Text="GENERAR REPORTE" OnClick="Button7_Click" Visible="false" />
                <asp:Button ID="Button8" runat="server" Text="CANCELAR" OnClick="Button8_Click" />
            </p>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <br />
            <br />
            <h1 style="text-align:center">ANULAR ORDEN</h1>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label25" runat="server" Text="NO ORDEN"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:DropDownList ID="DropDownList14" runat="server" OnSelectedIndexChanged="DropDownList14_SelectedIndexChanged" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button9" runat="server" Text="ANULAR ORDEN" OnClick="Button9_Click" /></td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label32" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <p style="text-align:center">
                <asp:Label ID="Label38" runat="server" ForeColor="Red" Font-Names="Aharoni" Font-Size="28px"></asp:Label>
            </p>
        </asp:View>
        <asp:View ID="View5" runat="server">
            <br />
            <br />
            <h1 style="text-align:center">Crear Lista</h1><br />
            <div style="margin-left:10px; font-size:15px;">                
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label39" runat="server" Text="Fecha Inicio"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label44" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label40" runat="server" Text="Fecha Fin"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label45" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label41" runat="server" Text="Codigo de Lista" Width="150px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList15" runat="server" DataSourceID="ListaPrecios" DataTextField="CODIGO" DataValueField="CODIGO">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="ListaPrecios" runat="server" ConnectionString="<%$ ConnectionStrings:NuevaFase3 %>" SelectCommand="SELECT [CODIGO], [NOMBRE] FROM [LISTAPRECIOS]"></asp:SqlDataSource>
                    </td>
                 </tr>
                  <tr>
                      <td>
                        <asp:Label ID="Label42" runat="server" Text="Nit Cliente"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label43" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
              </table>
                <p>
                    <asp:Button ID="Button13" runat="server" Text="Asignar Lista de Precios" OnClick="Button13_Click" />
                </p>
            </div>

        </asp:View>
    </asp:MultiView>
</asp:Content>
