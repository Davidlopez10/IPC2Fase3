<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IPC2_Fase3_201314694.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="App_Themes/Tema1/Estilo1.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="abrigo_general" align="center">
    <div id="content_login">
        <table>
            <tr>
                <td>
                    <asp:Image ID="ImageLogin" runat="server" ImageUrl="~/Imagenes/guardar.jpg" Width="100px" Height="100px"/>
                </td>
                <td align="left"> 
                    Utilice una cuenta<br /> 
                    Para iniciar sesion
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">Nombre de Usuario </td>
            </tr>
             <tr>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtUsuario" runat="server" Width="230px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">Password </td>
            </tr>
             <tr>
                <td colspan="2" align="left">
                    <asp:TextBox ID="txtpassword" runat="server" Width="230px" TextMode="Password"></asp:TextBox>
                </td>
                 <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lblError" ForeColor="Red" Visible="false" runat="server" Text="Label"></asp:Label> </td>
            </tr>
             <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnAceptar" runat="server" Text="Iniciar Session" Width="100px" align="center" OnClick="btnAceptar_Click" />
                </td>
            </tr>
            </tr>
        </table>
    </div>
    </div>
    </form>
</body>
</html>
