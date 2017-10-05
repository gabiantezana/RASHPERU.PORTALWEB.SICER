<%@ Page Title="PerfilUsuarios" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PerfilUsuarios.aspx.cs" Inherits="PerfilUsuarios" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 
<ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></ajaxtoolkit:toolkitscriptmanager>

 <table width="100%">
  <tr>
   <td align="center">
    <asp:LinkButton ID="lnkNuevaPerfilUsario" runat="server" Font-Underline="false" onclick="lnkNuevaPerfilUsuario_Click" > 
     <img src="img/agregar.png" alt="Nueva Usuario" width="50px"/><br />Nuevo<br/>Perfil Usuario 
    </asp:LinkButton>
   </td>
  </tr>
 </table>
 <br />
 <table width="100%">
  <tr>
   <td align="center">
    <asp:GridView ID="gvPerfilUsuarios" runat="server" AutoGenerateColumns="false"
     GridLines="Both" 
     BorderStyle="Double" BorderColor="#989898" CellPadding="10" cellspacing="0"
     HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true"
     RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF"

     AllowPaging="true" 
     PageSize="20"  
     OnPageIndexChanging="gridView_PageIndexChanging"
     OnRowCommand="gvPerfilUsuarios_RowCommand" >
     
     <Columns>
        
      <asp:BoundField DataField="IdPerfilUsuario" HeaderText="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />          
      <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:TemplateField  HeaderText="Administrador" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearMod(Convert.ToString(Eval("ModAdministrador")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Aprobar Caja Chica" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearMod(Convert.ToString(Eval("ModCajaChica")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Aprobar Entrega a Rendir" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearMod(Convert.ToString(Eval("ModEntregaRendir")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Aprobar Reembolso" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearMod(Convert.ToString(Eval("ModReembolso")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Crear Caja Chica" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearMod(Convert.ToString(Eval("CreaCajaChica")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Crear Entrega a Rendir" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearMod(Convert.ToString(Eval("CreaEntregaRendir")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Crear Reembolso" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearMod(Convert.ToString(Eval("CreaReembolso")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearTipo(Convert.ToString(Eval("TipoAprobador")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
        <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("IdPerfilUsuario")%>' >
         <img src="img/edit.png" alt="Editar" width="20px" />
        </asp:LinkButton>
       </ItemTemplate>
      </asp:TemplateField> 

     </Columns>

     <EmptyDataTemplate>No hay informacion que mostrar<br /><img alt="noinfo" src="img/empty.png" width="100%" /></EmptyDataTemplate>

    </asp:GridView>
   </td>
  </tr>
 </table>

</asp:Content>