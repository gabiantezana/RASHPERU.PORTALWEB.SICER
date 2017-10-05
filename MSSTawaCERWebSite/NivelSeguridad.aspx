<%@ Page Title="NivelAprobacion" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NivelSeguridad.aspx.cs" Inherits="NivelAprobacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 
<ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></ajaxtoolkit:toolkitscriptmanager>

 <table width="100%">
  <tr>
   <td align="center">
    <h1><asp:Label ID="lblCabezera" runat= "server"/></h1>
   </td>
  </tr>
 </table> 
 <br /> 
 <center>
  <table width="400px" style="font-family:Verdana, Arial, Helvetica, sans-serif;"> 
   <tr>
    <td width="175px" align="left"><label>Id Nivel</label></td>
    <td width="225px" align="center"><asp:TextBox ID="txtIdNivel" runat="server" Width="100%" Enabled="false" ></asp:TextBox></td>
   </tr>
   <tr>  
    <td align="left"><label>Nombre/Descripcion</label></td>
    <td align="center">
     <asp:TextBox ID="txtNivelSeguridad" runat="server" Width="100%" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtNivelSeguridad" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" ValidChars="Ññ " ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
   </tr>
   <tr>   
    <td align="left"><strong>Activo</strong></td>
    <td align="center">
        <asp:CheckBox ID="ChkActivo" runat="server" 
            oncheckedchanged="CheckBox1_CheckedChanged" />
       </td>
   </tr>
   <tr>
    <td align="left"><label>Caracteres Mayuscula</label></td>
    <td align="center">
        <asp:CheckBox ID="ChkMayuscula" runat="server" 
            oncheckedchanged="CheckBox1_CheckedChanged" />
     <asp:TextBox ID="txtNumMayusculas" runat="server" Width="29%" 
            style="text-align: right" ></asp:TextBox>
       </td>
   </tr>
   <tr>
    <td align="left"><strong>Caracteres Numericos</strong></td>
    <td align="center">
        <asp:CheckBox ID="ChkNumerico" runat="server" />
     <asp:TextBox ID="txtNumNumericos" runat="server" Width="29%" 
            style="text-align: right" ></asp:TextBox>
       </td>
   </tr>
   <tr>
    <td align="left"><strong>Caracteres Especiales</strong></td>
    <td align="center">
        <asp:CheckBox ID="ChkEspecial" runat="server" />
     <asp:TextBox ID="txtNumEspeciales" runat="server" Width="29%" 
            style="text-align: right" ></asp:TextBox>
    </td>
   </tr>
    <tr>
    <td align="left"><strong>Repetición Contraseña</strong></td>
    <td align="center">
     <asp:TextBox ID="txtRepContrsena" runat="server" Width="100%" ></asp:TextBox>
    </td>
   </tr>
   <tr>
    <td align="left"><strong>Dias Vencimiento</strong></td>
    <td align="center">
     <asp:TextBox ID="txtDiasVencimiento" runat="server" Width="100%" ></asp:TextBox>
    </td>
   </tr>
   <tr>
    <td align="left"><strong>Caracteres minimos</strong></td>
    <td align="center">
     <asp:TextBox ID="txtNumCarContrasena" runat="server" Width="100%" ></asp:TextBox>
    </td>
   </tr>
  </table>
 </center>
 <br />
 <center>
  <table width="400px">
   <tr>
    <td width="200px" align="center"><asp:Button ID="bCrear" runat="server" Text="Actualizar" CssClass="button" onclick="Crear_Click" /></td>
    <td width="200px" align="center"><asp:Button ID="bCancelar" runat="server" Text="Cancelar" CssClass="button" onclick="Cancelar_Click" /></td>
   </tr>
  </table>
 </center>

</asp:Content>