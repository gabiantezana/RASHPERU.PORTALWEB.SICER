<%@ Page Title="CajaChicasH" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CajaChicasH.aspx.cs" Inherits="CajaChicasH" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" EnableEventValidation="false">

<ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></ajaxtoolkit:toolkitscriptmanager>

 <table width="100%">
  <tr>
   <td align="center">
    <h1><asp:Label ID="lblCabezera" runat= "server"/></h1>
   </td>
  </tr>
 </table> 
 <table> 
  <tr>
   <td align="center">
    <asp:LinkButton ID="lnkExportarReporte" runat="server" Font-Underline="false" onclick="lnkExportarReporte_Click" > 
     <img src="img/excel.png" alt="Exportar Reporte" width="50px"/><br />Exportar Reporte 
    </asp:LinkButton>
   </td>
   <td align="right" width="10%"><label>Filtro: </label></td>
   <td align="left" width="15%" ><asp:DropDownList ID="ddlFiltro" runat="server" Width="95%" ></asp:DropDownList></td>
   <td align="left" width="10%" ><asp:Button ID="bBuscar" runat="server" Text="Buscar" CssClass="button" onclick="Buscar_Click" /></td>  
  </tr>
 </table> 
 <center> 
 <table width="100%">
  <tr>
   <td align="center">
    <asp:GridView ID="gvCajaChicas" runat="server" 
     AutoGenerateColumns="false"
     GridLines="Both" 
     BorderStyle="Double" BorderColor="#989898" CellPadding="10" cellspacing="0"
     HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
     RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF" RowStyle-Font-Size="XX-Small"

     AllowPaging="true" 
     PageSize="20"  
     OnPageIndexChanging="gridView_PageIndexChanging"
     OnRowCommand="gvCajaChicas_RowCommand" >
     
     <Columns>
        
      <asp:BoundField DataField="Rendicion" HeaderText="Rendicion" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />   
      <%--<asp:BoundField DataField="IdCajaChicaDocumento" HeaderText="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />   --%>
      <asp:TemplateField  HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
       <ItemTemplate><%# SetearTipo(Convert.ToString(Eval("TipoDoc")))%></ItemTemplate>
      </asp:TemplateField> 
      <asp:BoundField DataField="SerieDoc" HeaderText="Serie" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
      <asp:BoundField DataField="CorrelativoDoc" HeaderText="Numero" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
      <asp:BoundField DataField="FechaDoc" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" /> 
      <asp:TemplateField  HeaderText="Proveedor" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearProveedor(Convert.ToString(Eval("IdProveedor")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Concepto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearConcepto(Convert.ToString(Eval("IdConcepto")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Centro Costos 3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearCentroCostos(Convert.ToString(Eval("IdCentroCostos3")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Centro Costos 4" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearCentroCostos(Convert.ToString(Eval("IdCentroCostos4")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="Centro Costos 5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearCentroCostos(Convert.ToString(Eval("IdCentroCostos5")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:BoundField DataField="MontoTotal" HeaderText="Total CC" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:TemplateField  HeaderText="Moneda" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearMoneda(Convert.ToString(Eval("IdMonedaDoc")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:BoundField DataField="MontoNoAfecto" HeaderText="No Afecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
      <asp:BoundField DataField="MontoAfecto" HeaderText="Afecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
      <asp:BoundField DataField="MontoIGV" HeaderText="IGV" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
      <asp:BoundField DataField="MontoDoc" HeaderText="Total Doc" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 

     </Columns>

     <EmptyDataTemplate>No hay informacion que mostrar<br /><img alt="noinfo" src="img/empty.png" width="100%" /></EmptyDataTemplate>

    </asp:GridView>
   </td>
  </tr>
 </table>
 </center>
 
 <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label4" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label5" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label6" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label7" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label8" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label9" runat="server" Visible="false"></asp:Label>
 
 <asp:Label ID="Label10" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label11" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label12" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label13" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label14" runat="server" Visible="false"></asp:Label>

 <asp:GridView ID="gvReporte" runat="server"
  AutoGenerateColumns="false"
  GridLines="Both" 
  BorderStyle="Double" BorderColor="#989898" CellPadding="10" cellspacing="0"
  HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
  RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF" RowStyle-Font-Size="XX-Small">
  
  <Columns>
     
   <asp:TemplateField  HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
    <ItemTemplate><%# SetearTipo(Convert.ToString(Eval("TipoDoc")))%></ItemTemplate>
   </asp:TemplateField> 
   <asp:BoundField DataField="SerieDoc" HeaderText="Serie" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
   <asp:BoundField DataField="CorrelativoDoc" HeaderText="Numero" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
   <asp:BoundField DataField="FechaDoc" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" />       
   <asp:TemplateField  HeaderText="Razon Social" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
    <ItemTemplate><%# SetearProveedor(Convert.ToString(Eval("IdProveedor")))%></ItemTemplate>
   </asp:TemplateField>
   <asp:TemplateField  HeaderText="RUC" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
    <ItemTemplate><%# SetearProveedorRUC(Convert.ToString(Eval("IdProveedor")), Convert.ToString(Eval("TipoDoc")))%></ItemTemplate>
   </asp:TemplateField>
   <asp:TemplateField  HeaderText="Concepto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
    <ItemTemplate><%# SetearConcepto(Convert.ToString(Eval("IdConcepto")))%></ItemTemplate>
   </asp:TemplateField>
   <asp:TemplateField  HeaderText="Centro Costos 5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
    <ItemTemplate><%# SetearCentroCostos(Convert.ToString(Eval("IdCentroCostos5")))%></ItemTemplate>
   </asp:TemplateField>
   <asp:BoundField DataField="MontoTotal" HeaderText="Total Entrega Rendir" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
   <asp:TemplateField  HeaderText="Moneda" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
    <ItemTemplate><%# SetearMoneda(Convert.ToString(Eval("IdMonedaDoc")))%></ItemTemplate>
   </asp:TemplateField>
   <asp:BoundField DataField="MontoNoAfecto" HeaderText="No Afecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
   <asp:BoundField DataField="MontoAfecto" HeaderText="Afecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
   <asp:BoundField DataField="MontoIGV" HeaderText="IGV" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
   <asp:BoundField DataField="MontoDoc" HeaderText="Total Doc" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 

  </Columns>

  <EmptyDataTemplate>No hay informacion que mostrar<br /><img alt="noinfo" src="img/empty.png" width="100%" /></EmptyDataTemplate>

 </asp:GridView>

</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>