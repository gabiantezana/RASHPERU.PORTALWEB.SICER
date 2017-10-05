<%@ Page Title="Usuario" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Usuario.aspx.cs" Inherits="Usuario" %>
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
  <table width="975px" style="font-family:Verdana, Arial, Helvetica, sans-serif;"> 
   <tr>
    <td width="175px" align="left"><label>Id Usuario</label></td>
    <td width="150px" align="left"><asp:TextBox ID="txtIdUsuario" runat="server" Width="95%" Enabled="false" ></asp:TextBox></td>
    <td width="175px" align="left"><label>Estado</label></td>
    <td width="150px" align="left"><asp:DropDownList ID="ddlEstado" runat="server" Width="95%" ></asp:DropDownList></td>
    <td width="175px" align="left"><label>Tipo Usuario</label></td>
    <td width="150px" align="left"><asp:DropDownList ID="ddlTipoUsuario" runat="server" Width="95%" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList></td>
   </tr>
   <tr>
    <td align="left"><label>Codigo Usuario</label></td>
    <td align="left">
     <asp:TextBox ID="txtCardCode" runat="server" Width="95%" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtCardCode" runat="server" Enabled="true" FilterType="Numbers,UppercaseLetters, LowercaseLetters"></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
    <td align="left"><label>Contraseña</label></td>
    <td align="left">
     <asp:TextBox ID="txtPass" runat="server" Width="95%" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtPass" runat="server" Enabled="True" FilterType="Numbers, LowercaseLetters, Custom,UppercaseLetters"
    ValidChars="!|#$%&/(),;:{}[]´+*.@"></ajaxtoolkit:FilteredTextBoxExtender> 
    </td>
    <td align="left"><label>Nombre Usuario</label></td>
    <td align="left">
     <asp:TextBox ID="txtCardName" runat="server" Width="95%" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtCardName" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars="ÑñáéíóúÁÉÍÓÚ " ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
   </tr>
   <tr>
    <td align="left"><label>Perfil Usuario</label></td>
    <td align="left"><asp:DropDownList ID="ddlPerfilUsuario" runat="server" Width="95%" OnSelectedIndexChanged="ddlPerfilUsuario_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList></td>
    <td align="left"><label>Cantidad de Cajas Chicas </label></td>
    <td align="left">
     <asp:TextBox ID="txtCantMaxCC" runat="server" Width="95%" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtCantMaxCC" runat="server" Enabled="True" FilterType="Numbers" ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
    <td align="left"><label>Cantidad de Entregas a Rendir</label></td>
    <td align="left">
     <asp:TextBox ID="txtCantMaxER" runat="server" Width="95%" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtCantMaxER" runat="server" Enabled="True" FilterType="Numbers" ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
   </tr>
   <tr>
    <td align="left"><label>Cantidad de Reembolsos</label></td>
    <td align="left">
     <asp:TextBox ID="txtCantMaxRE" runat="server" Width="95%" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtCantMaxRE" runat="server" Enabled="True" FilterType="Numbers" ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
    <td align="left"><label>Telefono</label></td>
    <td align="center">
     <asp:TextBox ID="txtPhone" runat="server" Width="95%" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtPhone" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars="#- " ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
    <td align="left"><label>Correo Electronico</label></td>
    <td align="center">
     <asp:TextBox ID="txtMail" runat="server" Width="95%" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="txtMail" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars="@._" ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
   </tr>
   <tr>
    <td align="left" valign="top">
     <label>Centro Costos Nivel 3</label>
    </td>
    <td align="left" valign="top">
     <asp:DropDownList ID="ddlCentroCostos1" runat="server" Width="95%" ></asp:DropDownList>
     <asp:DropDownList ID="ddlCentroCostos2" runat="server" Width="95%" ></asp:DropDownList>
     <asp:DropDownList ID="ddlCentroCostos3" runat="server" Width="95%" ></asp:DropDownList>
    </td>
    <td align="left" valign="top">
     <asp:DropDownList ID="ddlCentroCostos4" runat="server" Width="85%" ></asp:DropDownList>
     <asp:DropDownList ID="ddlCentroCostos5" runat="server" Width="85%" ></asp:DropDownList>
     <asp:DropDownList ID="ddlCentroCostos6" runat="server" Width="85%" ></asp:DropDownList>
    </td>
    <td>
     <asp:DropDownList ID="ddlCentroCostos7" runat="server" Width="95%" ></asp:DropDownList>
     <asp:DropDownList ID="ddlCentroCostos8" runat="server" Width="95%" ></asp:DropDownList>
     <asp:DropDownList ID="ddlCentroCostos9" runat="server" Width="95%" ></asp:DropDownList>    
    </td>
    <td>
     <asp:DropDownList ID="ddlCentroCostos10" runat="server" Width="95%" ></asp:DropDownList>
     <asp:DropDownList ID="ddlCentroCostos11" runat="server" Width="95%" ></asp:DropDownList>
     <asp:DropDownList ID="ddlCentroCostos12" runat="server" Width="95%" ></asp:DropDownList>    
    </td>
    <td>
     <asp:DropDownList ID="ddlCentroCostos13" runat="server" Width="95%" ></asp:DropDownList>
     <asp:DropDownList ID="ddlCentroCostos14" runat="server" Width="95%" ></asp:DropDownList>
     <asp:DropDownList ID="ddlCentroCostos15" runat="server" Width="95%" ></asp:DropDownList>    
    </td>
   </tr>
  </table>
 </center>   
 <br />
 <center>
 <table width="975px">
  <tr>
   <td width="255px"><label>Nivel Aprobacion</label></td>
   <td width="240px"><label>Caja Chica</label></td>
   <td width="240px"><label>Entrega Rendir</label></td>
   <td width="240px"><label>Reembolso</label></td>
  </tr>
  <tr>
   <td>Nivel 1</td>
   <td><asp:DropDownList ID="ddlIdUsuarioCC1" runat="server" Width="95%" OnSelectedIndexChanged="ddlIdUsuarioCC1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
   <td><asp:DropDownList ID="ddlIdUsuarioER1" runat="server" Width="95%" OnSelectedIndexChanged="ddlIdUsuarioER1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
   <td><asp:DropDownList ID="ddlIdUsuarioRE1" runat="server" Width="95%" OnSelectedIndexChanged="ddlIdUsuarioRE1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
  </tr>
  <tr>
   <td>Nivel 2</td>
   <td><asp:DropDownList ID="ddlIdUsuarioCC2" runat="server" Width="95%" Enabled="false" OnSelectedIndexChanged="ddlIdUsuarioCC2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
   <td><asp:DropDownList ID="ddlIdUsuarioER2" runat="server" Width="95%" Enabled="false" OnSelectedIndexChanged="ddlIdUsuarioER2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
   <td><asp:DropDownList ID="ddlIdUsuarioRE2" runat="server" Width="95%" Enabled="false" OnSelectedIndexChanged="ddlIdUsuarioRE2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
  </tr>
  <tr>
   <td>Nivel 3</td>
   <td><asp:DropDownList ID="ddlIdUsuarioCC3" runat="server" Width="95%" Visible="false"></asp:DropDownList></td>
   <td><asp:DropDownList ID="ddlIdUsuarioER3" runat="server" Width="95%" Enabled="false"></asp:DropDownList></td>
   <td><asp:DropDownList ID="ddlIdUsuarioRE3" runat="server" Width="95%" Enabled="false"></asp:DropDownList></td>
  </tr>
 </table>
 </center>

 <center>
  <table width= "800px">
   <tr>
    <td align="center">&nbsp;</td>
    <td align="center">
        <asp:Button ID="bCrear" runat="server" CssClass="button" onclick="Crear_Click" 
            Text="Crear" />
        <asp:Button ID="bCancelar" runat="server" Text="Regresar" CssClass="button" onclick="Cancelar_Click" />
         <div style="width: 800px; height: 70%; overflow: auto">
        <asp:GridView ID="GridView1" runat="server" 
            AlternatingRowStyle-BackColor="#EFEFEF" AutoGenerateColumns="false" 
            BorderColor="#989898" BorderStyle="Double" CellPadding="5" cellspacing="0" 
            HeaderStyle-BackColor="#059BD8" HeaderStyle-Font-Bold="true" 
            HeaderStyle-Font-Size="X-Small" HeaderStyle-ForeColor="#FFFFFF" 
            RowStyle-BackColor="#FFFFFF" RowStyle-Font-Size="XX-Small" 
            ScrollBars="Horizontal" Width="800px">
            <Columns>
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                <asp:BoundField DataField="Tipo_Usuario" HeaderText="Tipo_Usuario" />
                <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                <asp:BoundField DataField="Contrasena" HeaderText="Contrasena" />
                <asp:BoundField DataField="Nombre_Usuario" HeaderText="Nombre_Usuario" />
                <asp:BoundField DataField="Perfil_Usuario" HeaderText="Perfil_Usuario" />
                <asp:BoundField DataField="Cantidad_CC" HeaderText="Cantidad_CC" />
                <asp:BoundField DataField="Cantidad_ER" HeaderText="Cantidad_ER" />
                <asp:BoundField DataField="Cantidad_RE" HeaderText="Cantidad_RE" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                                <asp:BoundField DataField="Correo" HeaderText="Correo" />
                <asp:BoundField DataField="Centro_Costos3_1" HeaderText="Centro_Costos3_1" />
                <asp:BoundField DataField="Centro_Costos3_2" HeaderText="Centro_Costos3_2" />
                <asp:BoundField DataField="Centro_Costos3_3" HeaderText="Centro_Costos3_3" />
                <asp:BoundField DataField="Nivel_Aprobacion_1_CC" 
                    HeaderText="Nivel_Aprobacion_1_CC" />
                <asp:BoundField DataField="Nivel_Aprobacion_2_CC" 
                    HeaderText="Nivel_Aprobacion_2_CC" />
                <asp:BoundField DataField="Nivel_Aprobacion_3_CC" 
                    HeaderText="Nivel_Aprobacion_3_CC" />
                                    <asp:BoundField DataField="Nivel_Aprobacion_1_ER" 
                    HeaderText="Nivel_Aprobacion_1_ER" />
                <asp:BoundField DataField="Nivel_Aprobacion_2_ER" 
                    HeaderText="Nivel_Aprobacion_2_ER" />
                <asp:BoundField DataField="Nivel_Aprobacion_3_ER" 
                    HeaderText="Nivel_Aprobacion_3_ER" />
                <asp:BoundField DataField="Nivel_Aprobacion_1_RE" 
                    HeaderText="Nivel_Aprobacion_1_RE" />
                <asp:BoundField DataField="Nivel_Aprobacion_2_RE" 
                    HeaderText="Nivel_Aprobacion_2_RE" />
                <asp:BoundField DataField="Nivel_Aprobacion_3_RE" 
                    HeaderText="Nivel_Aprobacion_3_RE" />


            </Columns>

        </asp:GridView>
        <br />

        <br />

        
        <br />
        <br />

        <br />


        
        <br />

        <br />
        <br />

       </td>
   </tr>
  </table>
   <table width="800px">
   <tr>
    <td>
    <br />
    <asp:TextBox ID="txtCopied" runat="server" Visible="false" TextMode="MultiLine" Height="100px" Width="1008px" />
    <br />
    <asp:Label ID="blbResultadoMasivo" runat="server" ></asp:Label>
    <br />
    <asp:Button ID="bMasivo" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Subir Masivamente" CssClass="button" onclick="Masivo_Click" />
    <asp:Button ID="bPreliminar4" runat="server" Visible="false" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Vista Preliminar" CssClass="button" onclick="Preliminar4_Click" />
    <asp:Button ID="bAgregar4" runat="server" Visible="false" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Agregar" CssClass="button" onclick="Agregar4_Click" />
    <asp:Button ID="bCancelar4" runat="server" Visible="false" Text="Cancelar" CssClass="button" onclick="Cancelar4_Click" />
    </td>
   </tr>
  </table>








 </center>
 
 </ContentTemplate>
 </asp:UpdatePanel>

&nbsp;&nbsp;&nbsp;

</asp:Content>