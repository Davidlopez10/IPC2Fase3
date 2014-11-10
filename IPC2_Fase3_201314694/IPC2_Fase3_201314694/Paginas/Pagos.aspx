<%@ Page Title="" Language="C#" MasterPageFile="~/Maestras/Pago.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="IPC2_Fase3_201314694.Paginas.Pagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Font-Names="OCR A Extended" Font-Size="25px">Inicio</asp:LinkButton>
    <br />
    <br />
    <br />
    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" Font-Names="OCR A Extended" Font-Size="25px">PAGAR</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <br />
            <br />
            <h1 style="text-align:center;">REALIZAR PAGO</h1>
            <br />
            <br />
            <table style="width:300px; float:left;">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="NO DE ORDEN" Width="150px"></asp:Label></td>
                    <td></td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="Button5" runat="server" Text="Seleccionar Orden" OnClick="Button5_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="MONTO FALTANTE:"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label16" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="CODIGO DE PAGO"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="TextBox7" runat="server" TextMode="Number"></asp:TextBox>

                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="FORMAS DE PAGO"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem>---------</asp:ListItem>
                            <asp:ListItem>Cheque</asp:ListItem>
                            <asp:ListItem>Efectivo</asp:ListItem>
                            <asp:ListItem>Tarjeta de Credito</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="TIPO DE MONEDA"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="Monedas" DataTextField="NOMBRE" DataValueField="NOMBRE"></asp:DropDownList>
                        <asp:SqlDataSource ID="Monedas" runat="server" ConnectionString="<%$ ConnectionStrings:NuevaFase3 %>" SelectCommand="SELECT [NOMBRE] FROM [MONEDA]"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="CANTIDAD ABONAR"></asp:Label></td>
                    <td></td>                    
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="Number"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                 <tr>
                    <td>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="EMISOR" Visible="false"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server" Visible="false"></asp:TextBox></td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="NO DE AUTORIZACION" Visible="false"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="TextBox6" runat="server" Visible="false" TextMode="Number"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <br />
            <table style="float:left; width:300px;">
                 <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="INFORMACION BANCO" Visible="false"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox></td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="CUENTA BANCARIA" Visible="false"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="NO DE CHEQUE" Visible="false"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" Visible="false" TextMode="Number"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <br />
           <br />
            <br />
            <br />
            <br /><br />
            <br /><br />
            <br /><br />
            <br />
            <p>
                <asp:Button ID="Button1" runat="server" Text="PAGAR" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="EMITIR RECIBO DE PAGO" OnClick="Button2_Click" Visible="false" />
                <asp:Button ID="Button3" runat="server" Text="EMITIR FACTURA" Visible="false" OnClick="Button3_Click" />
                <br />
                <asp:Label ID="Label14" runat="server" Visible="False" Font-Bold="False" ForeColor="Red" Font-Names="Charlemagne Std" Font-Size="Larger"></asp:Label>
            </p>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <br />
            <br />
            <h1>EMITIR RECIBO</h1>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="CODIGO DEL RECIBO"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="TextBox8" runat="server" TextMode="Number"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="CODIGO DE LA ORDEN"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label17" runat="server" ForeColor="Red" Font-Names="Castellar" Font-Size="20px"></asp:Label>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
            <p>
                <asp:Button ID="Button4" runat="server" Text="EMITIR RECIBO" OnClick="Button4_Click" style="height: 26px" /></p>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <br />
            <br />
            <table>
                <tr>
                    <td>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View4" runat="server">
        </asp:View>
    </asp:MultiView>

</asp:Content>
