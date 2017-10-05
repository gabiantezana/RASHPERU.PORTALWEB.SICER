<%@ Page Title="NivelAprobacion" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NivelAprobacion.aspx.cs" Inherits="NivelAprobacion" %>
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
    <td width="225px" align="center"><asp:TextBox ID="txtIdPerfilUsuario" runat="server" Width="100%" Enabled="false" ></asp:TextBox></td>
   </tr>
   <tr>  
    <td align="left"><label>Nombre/Descripcion</label></td>
    <td align="center">
     <asp:TextBox ID="txtDescripcion" runat="server" Width="100%" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtDescripcion" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" ValidChars="Ññ " ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
   </tr>
   <tr>   
    <td align="left"><label>Nivel</label></td>
    <td align="center"><asp:DropDownList ID="ddlNivel" runat="server" Width="100%" ></asp:DropDownList></td>
   </tr>
   <tr>
    <td align="left"><label>Documento</label></td>
    <td align="center"><asp:DropDownList ID="ddlDocumento" runat="server" Width="100%" ></asp:DropDownList></td>
   </tr>
   <tr>
    <td align="left"><label>Es De Monto?</label></td>
    <td align="center"><asp:DropDownList ID="ddlEsDeMonto" runat="server" Width="100%" OnSelectedIndexChanged="ddlEsDeMonto_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList></td>
   </tr>
   <tr>
    <td align="left"><label>Monto</label></td>
    <td align="center">
     <asp:TextBox ID="txtMonto" runat="server" Width="100%" Enabled="false" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtMonto" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
   </tr>
  </table>
 </center>
 <br />
 <center>
  <table width="400px">
   <tr>
    <td width="200px" align="center"><asp:Button ID="bCrear" runat="server" Text="Crear" CssClass="button" onclick="Crear_Click" /></td>
    <td width="200px" align="center"><asp:Button ID="bCancelar" runat="server" Text="Cancelar" CssClass="button" onclick="Cancelar_Click" /></td>
   </tr>
  </table>
 </center>

</asp:Content>