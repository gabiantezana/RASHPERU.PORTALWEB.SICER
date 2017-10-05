<%@ Page Title="NivelAprobaciones.aspx" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NivelAprobaciones.aspx.cs" Inherits="NivelAprobaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></ajaxtoolkit:toolkitscriptmanager>

 <table width="100%">
  <tr>
   <td align="center">
    <asp:LinkButton ID="lnkNuevaNivelAprobacion" runat="server" Font-Underline="false" onclick="lnkNuevaNivelAprobacion_Click" > 
     <img src="img/agregar.png" alt="Nueva Usuario" width="50px"/><br />Nuevo<br/>Nivel Aprobacion
    </asp:LinkButton>
   </td>
  </tr>
 </table>
 <br />
 <table width="100%">
  <tr>
   <td align="center">
    <asp:GridView ID="gvNivelAprobaciones" runat="server" AutoGenerateColumns="false"
     GridLines="Both" 
     BorderStyle="Double" BorderColor="#989898" CellPadding="10" cellspacing="0"
     HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true"
     RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF"

     AllowPaging="true" 
     PageSize="20"  
     OnPageIndexChanging="gridView_PageIndexChanging"
     OnRowCommand="gvNivelAprobaciones_RowCommand" >
     
     <Columns>
        
      <asp:BoundField DataField="IdNivel" HeaderText="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />          
      <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
      <asp:BoundField DataField="Nivel" HeaderText="Nivel" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />    
      <asp:TemplateField  HeaderText="Documento" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearDocumento(Convert.ToString(Eval("Documento")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Es De Monto?" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearEsDeMonto(Convert.ToString(Eval("EsDeMonto")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:BoundField DataField="Monto" HeaderText="Monto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
      <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
        <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("IdNivel")%>' >
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