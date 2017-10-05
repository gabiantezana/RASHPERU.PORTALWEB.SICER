<%@ Page Title="RendirEntregaRendir" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RendirEntregaRendir.aspx.cs" Inherits="RendirEntregaRendir" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" EnableEventValidation="false">

    <ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></ajaxtoolkit:toolkitscriptmanager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>

 <table width="1008px">
  <tr>
   <td align="center">
    <h1><asp:Label ID="lblCabezera" runat= "server"/></h1>
    <br /><asp:Label ID="lblIdEntregaRendirDocumento" runat="server" Visible="false"></asp:Label><asp:Label ID="lblIdProveedor" runat="server" Visible="false"></asp:Label>
   </td>
  </tr>
 </table>  
 <table width="1008px">  
  <tr>
   <td width="100px" align="left"><label>Tipo</label></td>
   <td width="152px" align="left"><asp:DropDownList ID="ddlTipo" runat="server" Width="95%" ></asp:DropDownList></td>
   <td width="100px" align="left"><label>Serie</label></td>
   <td width="152px" align="left">
    <asp:TextBox ID="txtSerie" runat="server" Width="95%" MaxLength="4" ></asp:TextBox>
    <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="txtSerie" runat="server" Enabled="True" FilterType="Numbers" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
   </td>
   <td width="100px" align="left"><label>Numero</label></td>
   <td width="152px" align="left">
    <asp:TextBox ID="txtNumero" runat="server" Width="95%" MaxLength="9" ></asp:TextBox>
    <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="txtNumero" runat="server" Enabled="True" FilterType="Numbers" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
   </td>
   <td width="100px" align="left"><label>Fecha</label></td>
   <td width="152px" align="left">
    <asp:TextBox ID="txtFecha" runat="server" Width="95%" ></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFecha" CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="img/calendar.png" ></ajaxToolkit:CalendarExtender>
   </td>
  </tr>
 </table>
 <table width="1008px">
  <tr>
   <td width="140px" align="left"><label>RUC</label></td>
   <td width="196px" align="left"><asp:TextBox ID="txtProveedor" runat="server" Width="95%" ></asp:TextBox></td>
   <td width="140px" align="left"><asp:Button ID="btnValidar" runat="server" Text="Validar" CssClass="button" OnClick="Validar_Click" /></td>
   <td width="196px" align="left"><asp:Label ID="lblProveedor" runat="server" Text="sin validar" ></asp:Label></td>
   <td width="140px" align="left"><label>Concepto</label></td>
   <td width="196px" align="left"><asp:DropDownList ID="ddlConcepto" runat="server" Width="95%" ></asp:DropDownList></td>
  </tr>
  <tr>
   <td align="left"><label>Centro Costo Nivel 3</label></td>
   <td align="left"><asp:DropDownList ID="ddlCentroCostos3" runat="server" Width="95%" Enabled="false" ></asp:DropDownList></td>
   <td align="left"><label>Centro Costo Nivel 4</label></td>
   <td align="left"><asp:DropDownList ID="ddlCentroCostos4" runat="server" Width="95%" Enabled="false" ></asp:DropDownList></td>
   <td align="left"><label>Centro Costo Nivel 5</label></td>
   <td align="left"><asp:DropDownList ID="ddlCentroCostos5" runat="server" Width="95%" Enabled="false" ></asp:DropDownList></td>
  </tr>
  <tr>
   <td align="left"><label>Moneda de Entrega a Rendir</label></td>
   <td align="left"><asp:DropDownList ID="ddlIdMonedaOriginal" runat="server" Width="95%" Enabled="false" ></asp:DropDownList></td>
   <td align="left"><label>Moneda del Documento</label></td>
   <td align="left"><asp:DropDownList ID="ddlIdMonedaDoc" runat="server" Width="95%" OnSelectedIndexChanged="ddlIdMonedaDoc_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList></td>
   <td align="left"><label>Tasa de Cambio</label></td>
   <td align="left">
    <asp:TextBox ID="txtTasaCambio" runat="server" Width="95%" ></asp:TextBox>
    <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtTasaCambio" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
   </td>
  </tr>
  <tr>
   <td align="left"><label>Importe Afecta</label></td>
   <td align="left">
    <asp:TextBox ID="txtMontoAfecta" runat="server" Width="95%" ></asp:TextBox>
    <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtMontoAfecta" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
   </td>
   <td align="left"><label>Importe No Afecta</label></td>
   <td align="left">
    <asp:TextBox ID="txtMontoNoAfecta" runat="server" Width="95%" ></asp:TextBox>
    <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtMontoNoAfecta" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
   </td>
   <td align="left"><label>Importe IGV</label></td>
   <td align="left">
    <asp:TextBox ID="txtMontoIGV" runat="server" Width="95%" Enabled="false" ></asp:TextBox>
    <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtMontoIGV" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
   </td>
  </tr>
  <tr>
   <td align="left"><label>Total Documento</label></td>
   <td align="left">
    <asp:TextBox ID="txtMontoDoc" runat="server" Width="95%" Enabled="false" ></asp:TextBox>
    <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtMontoDoc" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
   </td>
   <td align="left"><label>Importe Total Moneda E.R.</label></td>
   <td align="left">
    <asp:TextBox ID="txtMontoTotal" runat="server" Width="95%" Enabled="false" ></asp:TextBox>
    <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtMontoTotal" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
   </td>
   <td colspan="2" align="left"><asp:Button ID="bValidarImporte" runat="server" Text="Validar Importes" CssClass="button" OnClick="ValidarImporte_Click" /></td>
  </tr>
 </table>
 <br />
 <table width="1008px">
  <tr>
   <td align="center"><asp:Button ID="bAgregar" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Agregar" CssClass="button" onclick="Agregar_Click" />
   <asp:Button ID="bGuardar" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Guardar" CssClass="button" onclick="Guardar_Click" />
   <asp:Button ID="bCancelar" runat="server" Text="Regresar" CssClass="button" onclick="Cancelar_Click" /></td>
  </tr>
 </table>
 <br />
 <table width="1008px" width="1008px" style="border-top: thick solid #000000;">
  <tr>   
   <td align="center">   
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
      BorderStyle="Double" BorderColor="#989898" CellPadding="10" cellspacing="0"
      HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
      RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF" RowStyle-Font-Size="XX-Small">
    <Columns>
        <asp:BoundField DataField="Tipo_Documento" HeaderText="Tipo Documento" ItemStyle-Width="150" />
        <asp:BoundField DataField="Serie" HeaderText="Serie" ItemStyle-Width="150" />
        <asp:BoundField DataField="Numero" HeaderText="Numero" ItemStyle-Width="150" />
        <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-Width="150" />
        <asp:BoundField DataField="Ruc" HeaderText="Ruc" ItemStyle-Width="150" />
        <asp:BoundField DataField="Razon_Social" HeaderText="Razon Social" ItemStyle-Width="150" />
        <asp:BoundField DataField="Concepto" HeaderText="Concepto" ItemStyle-Width="150" />
        <asp:BoundField DataField="Moneda_Documento" HeaderText="Moneda Documento" ItemStyle-Width="150" />
        <asp:BoundField DataField="Tasa_Cambio" HeaderText="Tasa Cambio" ItemStyle-Width="150" />
        <asp:BoundField DataField="No_Afecta" HeaderText="No Afecta" ItemStyle-Width="150" />
        <asp:BoundField DataField="Afecta" HeaderText="Afecta" ItemStyle-Width="150" />
        <asp:BoundField DataField="IGV" HeaderText="IGV" ItemStyle-Width="150" />
        <asp:BoundField DataField="Total_Documento" HeaderText="Total Documento" ItemStyle-Width="150" />
        <asp:BoundField DataField="Total_Moneda_Origen" HeaderText="Total Moneda Origen" ItemStyle-Width="150" />
    </Columns>
    </asp:GridView>
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
 <br />
 <table width="1008px" style="border-top: thick solid #000000;">
 <tr>
  <td align="left" colspan="10"><label>Registrar Pago</label></td>
 </tr>
  <tr>
   <td width="140px" align="left"><label>Moneda</label></td>
   <td width="196px" align="left"><asp:DropDownList ID="ddlIdMonedaPago" runat="server" Width="95%" OnSelectedIndexChanged="ddlIdMonedaPago_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList></td>
   <td width="140px" align="left"><label>Tasa Cambio</label></td>
   <td width="196px" align="left">
    <asp:TextBox ID="txtTasaCambioPago" runat="server" Width="95%" ></asp:TextBox>
    <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="txtTasaCambioPago" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
   </td>
   <td width="140px" align="left"><label>Importe Pagado</label></td>
   <td width="196px" align="left">
    <asp:TextBox ID="txtMontoPago" runat="server" Width="95%" ></asp:TextBox>
    <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtMontoPago" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="." ></ajaxtoolkit:FilteredTextBoxExtender>
   </td>
   <td width="140px" align="left"><label>Importe Total Moneda ER</label></td>
   <td width="196px" align="left"><asp:TextBox ID="txtMontoTotalPago" runat="server" Width="95%" Enabled="false" ></asp:TextBox></td>
  </tr>
  <tr>
   <td width="140px" align="left"><label>Fecha Pago</label></td>
   <td width="152px" align="left">
    <asp:TextBox ID="txtFechaPago" runat="server" Width="95%" ></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFechaPago" CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="img/calendar.png" ></ajaxToolkit:CalendarExtender>
   </td>
   <td width="140px" align="left"><label>Banco</label></td>
   <td width="196px" align="left"><asp:DropDownList ID="ddlIdBanco" runat="server" Width="95%" Enabled="false" ></asp:DropDownList></td>        
   <td align="left"><asp:Button ID="bValidarImporte2" runat="server" Text="Validar Importes" CssClass="button" OnClick="ValidarImporte2_Click" /></td>
  </tr>
 </table>
 <br />
 <table width="1008px">
  <tr>
   <td align="center">
    <asp:Button ID="bAgregar3" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Agregar" CssClass="button" onclick="Agregar3_Click" />
    <asp:Button ID="bGuardar3" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Guardar" CssClass="button" onclick="Guardar3_Click" Visible="false" />
   </td>
  </tr>
 </table>
 <table width="1008px" style="border-bottom: thick solid #000000;">
  <tr>
   <td></td>
  </tr>
 </table>
 <br />
 <table width="1008px">
  <tr>
   <td width="252px"></td>
   <td width="140px" align="left"><label>Nombre Proveedor</label></td>
   <td width="196px" align="left"><asp:TextBox ID="txtCardName" runat="server" Width="95%" MaxLength="100" ></asp:TextBox></td>
   <td width="140px" align="left"><label>RUC Proveedor</label></td>
    <td width="196px" align="left">
     <asp:TextBox ID="txtDocumento" runat="server" Width="95%" MaxLength="11" ></asp:TextBox>
     <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" TargetControlID="txtDocumento" runat="server" Enabled="True" FilterType="Numbers" ></ajaxtoolkit:FilteredTextBoxExtender>
    </td>
   <td width="252px"></td>
  </tr>
 </table>
 <table width="1008px">
  <tr>
   <td align="center">
    <asp:Button ID="bAgregar2" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Agregar" CssClass="button" onclick="Agregar2_Click" />
    <asp:Button ID="bGuardar2" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Guardar" CssClass="button" onclick="Guardar2_Click" Visible="false" />
   </td>
  </tr>
 </table>
 <center>
 <table width="100%">
  <tr>
   <td align="center">
    <asp:GridView ID="gvProveedor" runat="server" 
     AutoGenerateColumns="false"
     GridLines="Both" 
     BorderStyle="Double" BorderColor="#989898" CellPadding="10" cellspacing="0"
     HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
     RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF" RowStyle-Font-Size="XX-Small"
     
     AllowPaging="true" 
     PageSize="5"  
     OnPageIndexChanging="gridViewP_PageIndexChanging"
     OnRowCommand="gvProveedor_RowCommand" >
    
     <Columns>
      
      <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
        <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("IdProveedor")%>' >
         <img src="img/edit.png" alt="Editar" width="20px" />
        </asp:LinkButton>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:BoundField DataField="IdProveedor" HeaderText="Id PRoveedor" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="CardCode" HeaderText="Codigo SAP" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="CardName" HeaderText="Razon Social" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
      <asp:BoundField DataField="Documento" HeaderText="RUC" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />

     </Columns>

    </asp:GridView>
   </td>
  </tr>
 </table>
 </center>
 <table width="1008px" style="border-bottom: thick solid #000000;">
  <tr>
   <td></td>
  </tr>
 </table> 
 <br />
 <center> 
  <table width="100%">
   <tr>
    <td align="center">
     <asp:LinkButton ID="lnkExportarReporte" runat="server" Font-Underline="false" onclick="lnkExportarReporte_Click" > 
      <img src="img/excel.png" alt="Exportar Reporte" width="50px"/><br />Exportar Reporte 
     </asp:LinkButton>
    </td>
   </tr>
  </table>
 </center>
 <center> 
  <table width="100%">
   <tr>
    <td align="right"><label>Fecha de Contabilizacion:</label></td>
    <td align="left">
     <asp:TextBox ID="txtFechaContabilizacion" runat="server" Enabled="false" ></asp:TextBox>
     <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFechaContabilizacion" CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="img/calendar.png" ></ajaxToolkit:CalendarExtender>
    </td>
   </tr>
  </table>
 </center>
 <br />
 <center> 
 <table width="100%">
  <tr>
   <td align="center">
    <asp:GridView ID="gvEntregaRendir" runat="server" 
     AutoGenerateColumns="false"
     GridLines="Both" 
     BorderStyle="Double" BorderColor="#989898" CellPadding="10" cellspacing="0"
     HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
     RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF" RowStyle-Font-Size="XX-Small"

     AllowPaging="true" 
     PageSize="20"  
     OnPageIndexChanging="gridView_PageIndexChanging"
     OnRowCommand="gvEntregaRendir_RowCommand" >
     
     <Columns>
      
      <asp:TemplateField HeaderText="S/N" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
       <ItemTemplate>
        <asp:CheckBox ID="chkRow" runat="server" Checked='<%# SetearCheck(Convert.ToString(Eval("Estado")))%>' OnCheckedChanged="chkRow_OnCheckedChanged" AutoPostBack="true" />
       </ItemTemplate>
      </asp:TemplateField>  
      <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
        <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("IdEntregaRendirDocumento")%>' >
          <img src="img/edit.png" alt="Editar" width="20px" />
        </asp:LinkButton>
       </ItemTemplate>
      </asp:TemplateField>  
      <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
        <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%#Eval("IdEntregaRendirDocumento")%>' > <img src="img/delete.png" alt="Editar" width="20px" /></asp:LinkButton>
       </ItemTemplate>
      </asp:TemplateField>   
      <asp:BoundField DataField="IdEntregaRendirDocumento" HeaderText="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />   
      <asp:TemplateField  HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
       <ItemTemplate><%# SetearTipo(Convert.ToString(Eval("TipoDoc")))%></ItemTemplate>
      </asp:TemplateField> 
      <asp:BoundField DataField="SerieDoc" HeaderText="Serie" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
      <asp:BoundField DataField="CorrelativoDoc" HeaderText="Numero" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
      <asp:BoundField DataField="FechaDoc" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"  DataFormatString="{0:dd/MM/yyyy}" />      
      <asp:TemplateField  HeaderText="Razon Social" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearProveedor(Convert.ToString(Eval("IdProveedor")), Convert.ToString(Eval("TipoDoc")))%></ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField  HeaderText="RUC" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
       <ItemTemplate><%# SetearProveedorRUC(Convert.ToString(Eval("IdProveedor")), Convert.ToString(Eval("TipoDoc")))%></ItemTemplate>
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
      <asp:BoundField DataField="MontoTotal" HeaderText="Total ER" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" /> 
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
 <center> 
 <table width="500px" >
  <tr>
   <td width="100px"><label><asp:Label ID="lblComentario" runat="server" Text="Observacion" ></asp:Label></label></td>
   <td ><asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Width="95%" Height="50px" MaxLength="1000" ></asp:TextBox></td>
  </tr>
 </table>
 <table>
  <tr>
    <td><asp:Button ID="bEnviar" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Enviar Rendicion" CssClass="button" onclick="Enviar_Click" /></td>
  </tr>
 </table>
 <table width="300px" >
  <tr>
    <td align="center"><asp:Button ID="bAprobar" runat="server" OnClick="Aprobar_Click" OnClientClick="javascript: return ConfirmacionContabilizacion(); this.disabled = true; this.value = 'Procesando...';" Text="Aprobar" CssClass="button"  /></td>
    <td align="center"><asp:Button ID="bObservacion" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Observacion" CssClass="button" OnClick="Observacion_Click" /></td>
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
    <ItemTemplate><%# SetearProveedor(Convert.ToString(Eval("IdProveedor")), Convert.ToString(Eval("TipoDoc")))%></ItemTemplate>
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
