<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs" Inherits="Usuarios" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

 <ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></ajaxtoolkit:toolkitscriptmanager>
 
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>--%>

 <table width="100%">
  <tr>
   <td align="center" width="50%">
    <asp:LinkButton ID="lnkNuevaUsario" runat="server" Font-Underline="false" onclick="lnkNuevaUsuario_Click" > 
     <img src="img/agregar.png" alt="Nueva Usuario" width="50px"/><br />Nuevo<br/>Usuario 
    </asp:LinkButton>
   </td>
   <td width="50%">
   <table width="100%">
    <%--<tr>
     <td width="20%" colspan="2"><label>Nombre:</label></td>
     <td width="20%"><asp:DropDownList ID="ddlNombre" runat="server" Width="95%" ></asp:DropDownList></td>
     <td width="10%"><asp:Button ID="bBuscar" runat="server" Text="Buscar" CssClass="button" onclick="Buscar_Click" /></td>
    </tr>--%>
    <tr>
     <td width="5%"><label>Filtro:</label></td>
     <td width="10%"><asp:DropDownList ID="ddlFiltro" runat="server" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged" AutoPostBack="true" Width="95%" ></asp:DropDownList></td>
     <td width="15%">
      <asp:TextBox ID="txtPalabra" runat="server" Width="95%" MaxLength="100" ></asp:TextBox>
      <asp:DropDownList ID="ddlFiltro2" runat="server" Visible="false" Width="95%" ></asp:DropDownList>
      <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtPalabra" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" ValidChars="ÑñáéíóúÁÉÍÓÚ " ></ajaxtoolkit:FilteredTextBoxExtender>      
     </td>
     <td width="10%"><asp:Button ID="bBuscar2" runat="server" Text="Buscar" CssClass="button" onclick="Buscar2_Click" /></td>
    </tr>
   </table>
   </td>
  </tr>
 </table>
 <br />
 <table width="100%">
  <tr>
   <td align="center">
    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false"
     GridLines="Both" 
     BorderStyle="Double" BorderColor="#989898" CellPadding="10" cellspacing="0"
     HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true"
     RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF"

     AllowPaging="true" 
     PageSize="20"  
     OnPageIndexChanging="gridView_PageIndexChanging"
     OnRowCommand="gvUsuarios_RowCommand" >
     
     <Columns>
        
      <%--<asp:BoundField DataField="IdUsuario" HeaderText="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />--%>
      <asp:BoundField DataField="CardCode" HeaderText="Usuario" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="CardName" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Mail" HeaderText="Mail" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:TemplateField  HeaderText="Tipo de Usuario" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate>
        <%# SetearTipo(Convert.ToString(Eval("Tipo")))%>
       </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Perfil Usuario" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate>
        <%# SetearPerfilUsuario(Convert.ToString(Eval("IdPerfilUsuario")))%>
       </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <%# SetearEstado(Convert.ToString(Eval("Estado")))%>
         </ItemTemplate>
       </asp:TemplateField>
       <asp:TemplateField  HeaderText="Usuario a Cargo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
         <%# SetearCargoUsuario(Convert.ToString(Eval("IdUsuario")))%>
        </ItemTemplate>
       </asp:TemplateField>
       <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("IdUsuario")%>' >
            <img src="img/edit.png" alt="Editar" width="20px" />
           </asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField> 
       <asp:TemplateField HeaderText="Grupo Trabajo" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="lnkGrupoTrabajo" runat="server" CommandName="GrupoTrabajo" CommandArgument='<%#Eval("IdUsuario")%>' >
            <img src="img/group.png" alt="Editar" width="20px" />
           </asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField> 

     </Columns>
    </asp:GridView>
   </td>
  </tr>
 </table>
 <br />
 
<%--</ContentTemplate>
</asp:UpdatePanel>--%>

</asp:Content>