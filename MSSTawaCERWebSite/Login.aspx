<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></ajaxtoolkit:toolkitscriptmanager> 

 <div class="login-form"> 
  <table style="border-radius: 5px 5px 5px 5px; border: 1px solid #DADADA; width: 100%;">
   <tr>
    <td colspan="4"><h3>Iniciar Sesion</h3></td>
   </tr>      
   <tr>
    <td width="5%"></td>
    <td width="25%"><label><asp:Label ID='lblUsuario' runat="server" Text="Usuario"></asp:Label></label></td>
    <td width="60%" align="center">
     <asp:TextBox ID="txtusuario" runat="server" CssClass="text" />      
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtusuario" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars="Ññ" ></ajaxtoolkit:FilteredTextBoxExtender>   
    </td>
    <td class="style2">
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtusuario" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
    </td>
   </tr>
   <tr>
    <td></td>
    <td><label>Contraseña</label></td>
    <td align="center"><asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="text"/></td>
 <td>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpassword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtpassword" runat="server" Enabled="True" FilterType="Numbers, LowercaseLetters, Custom,UppercaseLetters"
    ValidChars="!|#$%&/(),;:{}[]´+*.@"></ajaxtoolkit:FilteredTextBoxExtender> 
    </td>
   </tr>
   <tr>
    <td></td>
    <td  colspan="2">
     <table width="100%">
      <tr>
       <td width="55%" align="left"></td>
       <td width="45%" align="right"><asp:Button ID="bLogin" runat="server" Text="Entrar" CssClass="button" OnClick="Login_Click" /></td>
           <br />
           <tr>
           <td width="55%" align="left"></td>
       <td width="45%" align="right"><asp:LinkButton ID="lnkRecuperarContrasena" 
               runat="server" Text="Olvidé Contraseña" OnClick="lnkRecuperarContrasena_Click" 
               CausesValidation="False" /></td>
       </tr>
      </tr>
     </table>  
    </td>
    <td></td>
   </tr>
  </table>
  <br />


  <br />


 </div>

<br /><br /><br /><br /><br /><br /><br /><br />

    </div>

</asp:Content>
