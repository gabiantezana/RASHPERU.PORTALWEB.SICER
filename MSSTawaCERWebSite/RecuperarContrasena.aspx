<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RecuperarContrasena.aspx.cs" Inherits="Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
       
        .style2
        {
            width: 50%;
        }
       
        .style3
        {
            width: 189px;
        }
               
        .style4
        {
            width: 144px;
        }
        .style5
        {
            height: 30px;
        }
               
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></ajaxtoolkit:toolkitscriptmanager> 

 <div class="login-form"> 
  <br />
  <table style="border-radius: 5px 5px 5px 5px; border: 1px solid #DADADA; width: 100%;">
     <tr>
    <td colspan="4" class="style5"><h3>Validar Datos</h3></td>
   </tr>
  <tr>
   <td class="style4">   <label>&nbsp;&nbsp;&nbsp; <asp:Label ID='lblDni' runat="server" Text="DNI"></asp:Label></label></td>
    <td width="60%" align="center">
     <asp:TextBox ID="txtDni" runat="server" CssClass="text" Height="22px" 
            Width="130px" />
     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDni" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>      
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtDni" runat="server" Enabled="True" FilterType="Numbers" ValidChars="Ññ" ></ajaxtoolkit:FilteredTextBoxExtender>   
    </td>
    <br />
    </tr>
    <tr>
   <td class="style4"><label>&nbsp;&nbsp;&nbsp; <asp:Label ID='lblCorreo' runat="server" Text="Correo"></asp:Label></label></td>
    <td width="60%" align="center" class="style3">
     <asp:TextBox ID="txtCorreo" runat="server" CssClass="text" Height="22px" 
            Width="130px" />
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCorreo" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
    </td>
    </tr>
      <tr>
   <td class="style4"><label>&nbsp;&nbsp;&nbsp; <asp:Label ID='lblNuevaContrasena1' runat="server" Text="Nueva Contraseña"></asp:Label></label></td>
    <td align="center" class="style6">
     <asp:TextBox ID="txtNuevaContrasena" runat="server" CssClass="text" 
            Width="123px" />
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtDni" runat="server" Enabled="True" FilterType="Numbers, LowercaseLetters, Custom,UppercaseLetters"
    ValidChars="!|#$%&/(),;:{}[]´+*.@"></ajaxtoolkit:FilteredTextBoxExtender>   
    </td>
    </br>
    </tr>
  <tr>
   <td class="style4"><label>&nbsp;&nbsp;&nbsp; <asp:Label ID='lblNuevaContrasena2' runat="server" Text="Repita Contraseña"></asp:Label></label></td>
    <td align="center" class="style5">
     <asp:TextBox ID="txtNuevaContrasena2" runat="server" CssClass="text" 
            Width="123px" />
    </td>
    </tr>
    <tr>
    <td colspan="2" class="style3">
    <table>
      <tr>
       
       <td width="55%" align="left"><asp:Button ID="btnRecuperarContrasena" runat="server" 
               Text="Validar Datos" CssClass="button" 
               OnClick="btnRecuperarContrasena_Click" Width="154px" /></td>
       <td align="right" class="style2"><asp:Button ID="btnCancelar" runat="server" 
               Text="Cancelar" CssClass="button" OnClick="btnCancelar_Click" 
               Width="135px" CausesValidation="False" /></td>
      
      </tr>
            <tr>
      <td class="style7">
      <asp:Label ID='lblErrorDatos' runat="server" Text="" forecolor="RED"></asp:Label>
      </td>
      </tr>
     </table>  
    </td>
    </tr>
  </table>

  <br />


 </div>

<br /><br /><br /><br /><br /><br /><br /><br />

    </div>

</asp:Content>
