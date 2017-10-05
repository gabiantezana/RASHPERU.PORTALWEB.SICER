<%@ Page Title="PerfilUsuario" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PerfilUsuario.aspx.cs" Inherits="PerfilUsuario" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 
<ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></ajaxtoolkit:toolkitscriptmanager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>

 <table width="100%">
  <tr>
   <td align="center">
    <h1><asp:Label ID="lblCabezera" runat= "server"/></h1>
   </td>
  </tr>
 </table> 
 <br /> 
 <center>
  <table width="800px" style="font-family:Verdana, Arial, Helvetica, sans-serif;"> 
   <tr>
    <td width="225px" align="left"><label>Id Perfil Usuario</label></td>
    <td width="175px" align="left"><asp:TextBox ID="txtIdPerfilUsuario" runat="server" Width="95%" Enabled="false" ></asp:TextBox></td>
    <td width="225px" align="left"><label>Nombre/Descripcion</label></td>
    <td width="175px" align="left">
     <asp:TextBox ID="txtDescripcion" runat="server" Width="95%" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtDescripcion" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars="Ññ " ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
   </tr>
   <tr>   
    <td align="left"><label>Modulo Administrador</label></td>
    <td align="left"><asp:DropDownList ID="ddlModAdministrador" runat="server" Width="95%" ></asp:DropDownList></td>
    <td align="left"><label>Tipo</label></td>
    <td align="left"><asp:DropDownList ID="ddlTipoAprobador" runat="server" Width="95%" OnSelectedIndexChanged="ddlTipoAprobador_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList></td>
   </tr>
   <tr>
    <td align="left"><label>Aprobador Caja Chica</label></td>
    <td align="left"><asp:DropDownList ID="ddlModCajaChica" runat="server" Width="95%" ></asp:DropDownList></td>
    <td align="left"><label>Creador Caja Chica</label></td>
    <td align="left"><asp:DropDownList ID="ddlCreaCajaChica" runat="server" Width="95%" ></asp:DropDownList></td>
   </tr>
   <tr>
    <td align="left"><label>Aprobador Entrega a Rendir</label></td>
    <td align="left"><asp:DropDownList ID="ddlModEntregaRendir" runat="server" Width="95%" ></asp:DropDownList></td>
    <td align="left"><label>Creador Entrega a Rendir</label></td>
    <td align="left"><asp:DropDownList ID="ddlCreaEntregaRendir" runat="server" Width="95%" ></asp:DropDownList></td>
   </tr>
   <tr>
    <td align="left"><label>Aprobador Reembolso</label></td>
    <td align="left"><asp:DropDownList ID="ddlModReembolso" runat="server" Width="95%" ></asp:DropDownList></td>
    <td align="left"><label>Creador Reembolso</label></td>
    <td align="left"><asp:DropDownList ID="ddlCreaReembolso" runat="server" Width="95%" ></asp:DropDownList></td>
   </tr>
  </table>
 </center>
 <br />
 <center>
  <table width="400px">
   <tr>
    <td width="200px" align="center"><asp:Button ID="bCrear" runat="server" Text="Crear" CssClass="button" onclick="Crear_Click" /></td>
    <td width="200px" align="center"><asp:Button ID="bCancelar" runat="server" Text="Regresar" CssClass="button" onclick="Cancelar_Click" /></td>
   </tr>
  </table>
 </center>
 
 </ContentTemplate>
 </asp:UpdatePanel>

</asp:Content>