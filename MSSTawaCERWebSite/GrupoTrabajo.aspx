<%@ Page Title="GrupoTrabajo" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GrupoTrabajo.aspx.cs" Inherits="GrupoTrabajo" %>
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
  <table width="400px">
   <tr>
    <td width="100px"><label>Empleado:</label></td>
    <td width="200px"><asp:DropDownList ID="ddlEmpleados" runat="server" Width="100%" ></asp:DropDownList></td>
    <td width="100px" align="center" ><asp:Button ID="bAgregar" runat="server" Text="Agregar" CssClass="button" onclick="bAgregar_Click" /></td>
   </tr>
  </table>
   <table width="400px">
    <tr>
     <td width="10%"><label>Filtro:</label></td>
     <td width="30%"><asp:DropDownList ID="ddlFiltro" runat="server" Width="95%" ></asp:DropDownList></td>
     <td width="40%">
      <asp:TextBox ID="txtPalabra" runat="server" MaxLength="100" Width="95%" ></asp:TextBox>
      <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtPalabra" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" ValidChars="ÑñáéíóúÁÉÍÓÚ " ></ajaxtoolkit:FilteredTextBoxExtender>
     </td>
     <td width="20%"><asp:Button ID="bBuscar2" runat="server" Text="Buscar" CssClass="button" onclick="Buscar2_Click" /></td>
    </tr>
   </table>
 </center>
 <br />
 <table width="100%">
  <tr>
   <td align="center">
    <asp:GridView ID="gvGrupoTrabajos" runat="server" AutoGenerateColumns="false"
     GridLines="Both" 
     BorderStyle="Double" BorderColor="#989898" CellPadding="10" cellspacing="0"
     HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true"
     RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF"

     AllowPaging="true" 
     PageSize="20"  
     OnPageIndexChanging="gridView_PageIndexChanging"
     OnRowCommand="gvGrupoTrabajos_RowCommand" >
     
     <Columns>
        
      <asp:BoundField DataField="CardCode" HeaderText="Usuario" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />          
      <asp:BoundField DataField="CardName" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Mail" HeaderText="Mail" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:TemplateField  HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <%# SetearEstado(Convert.ToString(Eval("Estado")))%>
         </ItemTemplate>
       </asp:TemplateField> 
       <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Eliminar" CommandArgument='<%#Eval("IdUsuario")%>' >
            <img src="img/delete.png" alt="Editar" width="20px" />
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