<%@ Page Title="Documento" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Documento.aspx.cs" Inherits="Documento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align="center">
                        <h1>
                            <asp:Label ID="lblCabezera" runat="server" />
                        </h1>
                    </td>
                </tr>
            </table>
            <br />
            <center>
  <table width="800px" style="font-family:Verdana, Arial, Helvetica, sans-serif;"> 
   <tr>
    <td width="175px" align="left"><label>Id CajaChica?? //TODO:</label></td>
    <td width="225px" align="left"><asp:TextBox ID="txtIdDocumento" runat="server" Width="95%" Enabled="false" ></asp:TextBox></td>
    <td width="175px" align="left"><label>Codigo</label></td>
    <td width="225px" align="left"><asp:TextBox ID="txtCodigoDocumento" runat="server" Width="95%" Enabled="false" ></asp:TextBox></td>
   </tr>
   <tr>   
    <td align="left"><label>Usuario Solicitante</label></td>
    <td align="left"><asp:DropDownList ID="ddlIdUsuarioSolicitante" runat="server" AutoPostBack="true" Width="95%" ></asp:DropDownList></td>
    <td align="left"><label>Empresa</label></td>
    <td align="left"><asp:DropDownList ID="ddlIdEmpresa" runat="server" AutoPostBack="true" Width="95%" OnSelectedIndexChanged="ddlIdEmpresa_SelectedIndexChanged1" ></asp:DropDownList></td>
   </tr>
   <tr>
    <td align="left"><label>Moneda</label></td>
    <td align="left"><asp:DropDownList ID="ddlMoneda" runat="server" Width="95%" ></asp:DropDownList></td>
    <td align="left"><label>Monto</label></td>
    <td align="left">
     <asp:TextBox ID="txtMontoInicial" runat="server" Width="95%" MaxLength="20" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtMontoInicial" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
   </tr>
   <tr>
    <td align="left">&nbsp;</td>
    <td align="left">&nbsp;</td>
    <td align="left">&nbsp;</td>
    <td align="left">&nbsp;</td>
   </tr>
   <tr>
    <td align="left"><label>Centro Costo Nivel 1</label></td>
    <td align="left"><asp:DropDownList ID="ddlCentroCostos1" runat="server" Width="95%" Enabled="false" ></asp:DropDownList></td>
    <td align="left"><label>Centro Costo Nivel 2</label></td>
    <td align="left"><asp:DropDownList ID="ddlCentroCostos2" runat="server" Width="95%" Enabled="false" ></asp:DropDownList></td>
   </tr>
   <tr>
    <td align="left"><label>Centro Costo Nivel 3</label></td>
    <td align="left"><asp:DropDownList ID="ddlCentroCostos3" runat="server" Width="95%" AutoPostBack="true" ></asp:DropDownList></td>
    <td align="left"><label>Centro Costo Nivel 4</label></td>
    <td align="left"><asp:DropDownList ID="ddlCentroCostos4" runat="server" Width="95%" AutoPostBack="true" ></asp:DropDownList></td>
   </tr>
   <tr>  
    <td align="left"><label>Centro Costo Nivel 5</label></td>
    <td align="left"><asp:DropDownList ID="ddlCentroCostos5" runat="server" Width="95%" ></asp:DropDownList></td>
    <td align="left"><label>Metodo de Pago</label></td>
    <td align="left"><asp:DropDownList ID="ddlIdMetodoPago" runat="server" Width="95%" ></asp:DropDownList></td>
   </tr>
   <tr>    
    <td align="left" valign="top"><label>Asunto</label></td>
    <td align="left" valign="top">
     <asp:TextBox ID="txtAsunto" runat="server" Width="95%" MaxLength="100" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtAsunto" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" ValidChars="Ññ " ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>  
   </tr>
   <tr>
    <td align="left" valign="top"><label>Motivo Detallado</label></td>
    <td align="left" colspan="3">
     <asp:TextBox ID="txtMotivoDetalle" runat="server" TextMode="MultiLine" Width="95%" Height="100px" MaxLength="5000" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtMotivoDetalle" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" ValidChars="Ññ.:,-+*?¿¡!$%&/()=áéíóúÁÉÍÓÚ " ></ajaxtoolkit:FilteredTextBoxExtender>
    </td> 
   </tr>
   <tr>    
    <td align="left" valign="top"><label>Observaciones</label></td>
    <td align="left" colspan="3">
     <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Width="95%" Height="50px" MaxLength="1000" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtComentario" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" ValidChars="Ññ " ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>   
   </tr>
  </table>
 </center>
            <br />
            <center>
  <table width="400px">
   <tr>
    <td width="200px" align="center">
     <asp:Button ID="bCrear" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" OnClick="Crear_Click" Text="Crear" CssClass="button" />
    </td>
    <td width="200px" align="center">
     <asp:Button ID="bCancelar" runat="server" Text="Regresar" CssClass="button" onclick="Cancelar_Click" />
     </td>
   </tr>
  </table>
  <table>
   <tr>
    <td align="center">
     <asp:Button ID="bAprobar" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" OnClick="Aprobar_Click" Text="Aprobar" CssClass="button" />
    </td>
    <td align="center">
     <asp:Button ID="bObservacion" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" OnClick="Observacion_Click" Text="Observacion" CssClass="button" />
    </td>
    <td align="center">
     <asp:Button ID="bRechazar" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" OnClick="Rechazar_Click" Text="Rechazar" CssClass="button" />
    </td>
    <td align="center"><asp:Button ID="bCancelar2" runat="server" Text="Regresar" CssClass="button" onclick="Cancelar_Click" /></td>
   </tr>
  </table>
 </center>
            <br />

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
