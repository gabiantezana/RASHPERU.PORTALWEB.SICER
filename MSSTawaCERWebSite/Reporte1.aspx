<%@ Page Title="Reporte1" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reporte1.aspx.cs" Inherits="Reporte1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" EnableEventValidation="false">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>

 <ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true" ></ajaxtoolkit:toolkitscriptmanager>

 <table width="1008px">
  <tr>
   <td width="12.5%"><label>Empresa:</label></td>
   <td width="12.5%"><asp:DropDownList ID="ddlIdEmpresa" runat="server" OnSelectedIndexChanged="ddlIdEmpresa_SelectedIndexChanged" AutoPostBack="true" Width="95%" ></asp:DropDownList></td>
   <td width="12.5%"><label>Nombre Solicitante:</label></td>
   <td width="12.5%"><asp:DropDownList ID="ddlNombre_Solicitante" runat="server" Width="95%" ></asp:DropDownList></td>
   <td width="12.5%"><label>Es Facturable:</label></td>
   <td width="12.5%"><asp:DropDownList ID="ddlEsFacturable" runat="server" Width="95%" ></asp:DropDownList></td>
   <td width="12.5%"><label>Estado:</label></td>
   <td width="12.5%"><asp:DropDownList ID="ddlEstado" runat="server" Width="95%" ></asp:DropDownList></td>
  </tr>
  <tr>
   <td width="12.5%"><label>Documento:</label></td>
   <td width="12.5%"><asp:DropDownList ID="ddlDocumento" runat="server" Width="95%" ></asp:DropDownList></td>
   <td width="12.5%"><label>Centro Costos 3:</label></td>
   <td width="12.5%"><asp:DropDownList ID="ddlCentroCostos3" runat="server" Width="95%" OnSelectedIndexChanged="ddlCentroCostos3_SelectedIndexChanged" AutoPostBack="true" Enabled="false" ></asp:DropDownList></td>
   <td width="12.5%"><label>Centro Costos 4:</label></td>
   <td width="12.5%"><asp:DropDownList ID="ddlCentroCostos4" runat="server" Width="95%" OnSelectedIndexChanged="ddlCentroCosto4_SelectedIndexChanged" AutoPostBack="true" Enabled="false" ></asp:DropDownList></td>
   <td width="12.5%"><label>Centro Costos 5:</label></td>
   <td width="12.5%"><asp:DropDownList ID="ddlCentroCostos5" runat="server" Width="95%" Enabled="false" ></asp:DropDownList></td>
  </tr>
  <tr>
   <td width="12.5%"><label>Fecha Solicitud Inicial:</label></td>
   <td>
    <asp:TextBox ID="txtFechaSolicitudIni" runat="server" ></asp:TextBox>
    <ajaxToolkit:calendarextender ID="CalendarExtender1" runat="server" 
           TargetControlID="txtFechaSolicitudIni" CssClass="MyCalendar" 
           Format="dd/MM/yyyy" ></ajaxToolkit:calendarextender>
   </td>
   <td width="12.5%"><label>Fecha Solicitud Final:</label></td>
   <td>
    <asp:TextBox ID="txtFechaSolicitudFin" runat="server" ></asp:TextBox>
    <ajaxToolkit:calendarextender ID="CalendarExtender2" runat="server" 
           TargetControlID="txtFechaSolicitudFin" CssClass="MyCalendar" 
           Format="dd/MM/yyyy" ></ajaxToolkit:calendarextender>
   </td>
   <td><asp:Button ID="bBuscar" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Buscar" CssClass="button" onclick="Buscar_Click" /></td>
  </tr>
 </table>
  <table width="1008px">
   <tr>
    <td align="center">    
     <asp:LinkButton ID="lnkExportarReporte" runat="server" Font-Underline="false" onclick="lnkExportarReporte_Click" > 
      <img src="img/excel.png" alt="Exportar Reporte" width="50px"/><br />Exportar Reporte 
     </asp:LinkButton>
    </td>
   </tr>
  </table>
 <table width="100%">
  <tr>
   <td align="center">
    <asp:GridView ID="gvReporte1" runat="server" 
     AutoGenerateColumns="False" 
     BorderStyle="Double" BorderColor="#989898" CellPadding="10"
     HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" 
           HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
     RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF" RowStyle-Font-Size="X-Small"
     
     AllowPaging="True" 
     PageSize="50"
     OnPageIndexChanging="gridView_PageIndexChanging" >

        <AlternatingRowStyle BackColor="#EFEFEF" />

     <Columns>

      <asp:BoundField DataField="DNI_Creador" HeaderText="DNI Creador" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Nombre_Creador" HeaderText="Nombre Creador" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="DNI_Solicitante" HeaderText="DNI Solicitante" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Nombre_Solicitante" HeaderText="Nombre Solicitante" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Empresa" HeaderText="Empresa" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="CentroCostos3" HeaderText="Centro Costos 3" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="CentroCostos4" HeaderText="Centro Costos 4" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="CentroCostos5" HeaderText="Centro Costos 5" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Numero" HeaderText="Numero" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="EsFacturable" HeaderText="Es Facturable" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="MomentoFacturable" HeaderText="Momento Facturable" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Moneda" HeaderText="Moneda" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Importe_Inicial" HeaderText="Importe Inicial" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha Solicitud" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
         <asp:BoundField DataField="FechaAprobacionN1" 
             HeaderText="Fecha Aprobación N1" />
         <asp:BoundField DataField="FechaAprobacionN2" 
             HeaderText="Fecha Aprobación N2" />
         <asp:BoundField DataField="FechaAprobacionN3" 
             HeaderText="Fecha Aprobación N3" />
      <asp:BoundField DataField="Motivo" HeaderText="Motivo" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Asunto" HeaderText="Asunto" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Estado" HeaderText="Estado" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="MontoNoAfecto" HeaderText="Importe No Afecto" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="MontoAfecto" HeaderText="Importe Afecto" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="MontoIGV" HeaderText="Importe IGV" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Importe_Rendido" HeaderText="Importe Rendido" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Aprobador_Nivel_1" HeaderText="Aprobador Nivel 1" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Aprobador_Nivel_2" HeaderText="Aprobador Nivel 2" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
      <asp:BoundField DataField="Aprobador_Nivel_3" HeaderText="Aprobador Nivel 3" 
             HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >

         <HeaderStyle HorizontalAlign="Center" />
         <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>

     </Columns>

     <EmptyDataTemplate>No hay informacion que mostrar<br /><img alt="noinfo" src="img/empty.png" width="100%" /></EmptyDataTemplate>

        <HeaderStyle BackColor="#059BD8" Font-Bold="True" Font-Size="X-Small" 
            ForeColor="White" />
        <RowStyle BackColor="White" Font-Size="X-Small" />

    </asp:GridView>
   </td>
  </tr>
 </table>

    <asp:GridView ID="gvReporte" runat="server" 
     AutoGenerateColumns="false"
     GridLines="Both" 
     BorderStyle="Double" BorderColor="#989898" CellPadding="10" cellspacing="0"
     HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
     RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF" RowStyle-Font-Size="X-Small">

     <Columns>

      <asp:BoundField DataField="DNI_Creador" HeaderText="DNI Creador" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Nombre_Creador" HeaderText="Nombre Creador" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="DNI_Solicitante" HeaderText="DNI Solicitante" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Nombre_Solicitante" HeaderText="Nombre Solicitante" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Empresa" HeaderText="Empresa" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="CentroCostos3" HeaderText="Centro Costos 3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="CentroCostos4" HeaderText="Centro Costos 4" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="CentroCostos5" HeaderText="Centro Costos 5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Numero" HeaderText="Numero" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="EsFacturable" HeaderText="Es Facturable" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="MomentoFacturable" HeaderText="Momento Facturable" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Moneda" HeaderText="Moneda" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Importe_Inicial" HeaderText="Importe Inicial" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha Solicitud" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="FechaAprobacionN1" HeaderText="Fecha Aprobación N1" />
      <asp:BoundField DataField="FechaAprobacionN2" HeaderText="Fecha Aprobación N2" />
      <asp:BoundField DataField="FechaAprobacionN3" HeaderText="Fecha Aprobación N3" />
      <asp:BoundField DataField="Motivo" HeaderText="Motivo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Asunto" HeaderText="Asunto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Estado" HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="MontoNoAfecto" HeaderText="Importe No Afecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="MontoAfecto" HeaderText="Importe Afecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="MontoIGV" HeaderText="Importe IGV" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Importe_Rendido" HeaderText="Importe Rendido" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Aprobador_Nivel_1" HeaderText="Aprobador Nivel 1" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Aprobador_Nivel_2" HeaderText="Aprobador Nivel 2" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Aprobador_Nivel_3" HeaderText="Aprobador Nivel 3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />

     </Columns>
    </asp:GridView>

</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>