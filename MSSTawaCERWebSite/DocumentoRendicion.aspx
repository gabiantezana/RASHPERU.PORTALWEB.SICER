<%@ Page Title="DocumentoRendicion" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DocumentoRendicion.aspx.cs" Inherits="DocumentoRendicion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 298px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" EnableEventValidation="false">

    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align="center">
                        <h1>
                            <asp:Label ID="lblCabezera" runat="server" /></h1>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="lblIdDocumentoDetalle" value="0" Text="0" runat="server" Visible="false"></asp:Label><asp:Label ID="lblIdProveedor" runat="server" Visible="false"></asp:Label>
            <table width="1100px">
                <tr>
                    <td width="15%">
                        <table>
                            <%-- TIPO  --%>
                            <tr>
                                <td>
                                    <label>Tipo</label></td>
                                <td width="140px">
                                    <asp:DropDownList ID="ddlTipoDocumentoWeb" runat="server" Width="95%" OnSelectedIndexChanged="ddlTipoDocumentoWeb_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <%-- SERIE  --%>
                            <tr>
                                <td>
                                    <label>Serie</label></td>
                                <td>
                                    <asp:TextBox ID="txtSerie" runat="server" Width="95%" MaxLength="4"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="txtSerie" runat="server" Enabled="True" FilterType="Numbers,LowercaseLetters,UppercaseLetters"></ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <%-- NUMERO  --%>
                            <tr>
                                <td>
                                    <label>Número</label></td>
                                <td>
                                    <asp:TextBox ID="txtNumero" runat="server" Width="95%" MaxLength="9"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="txtNumero" runat="server" Enabled="True" FilterType="Numbers" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <%-- FECHA  --%>
                            <tr>
                                <td>
                                    <label>Fecha</label></td>
                                <td>
                                    <asp:TextBox ID="txtFecha" runat="server" Width="95%"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFecha" CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="img/calendar.png"></ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Cuenta contable<label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCuentaContableDevolucion" runat="server" Width="95%" Enabled="false"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="25%">
                        <table>
                            <%-- CONCEPTO --%>
                            <tr>
                                <td>
                                    <label>Concepto</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlConcepto" runat="server" Width="95%" OnSelectedIndexChanged="ddlConcepto_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                            <%-- RUC  --%>
                            <tr>
                                <td>
                                    <label>RUC</label>
                                </td>
                                <td width="200px">
                                    <asp:TextBox ID="txtProveedor" runat="server" Width="95%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Label ID="lblProveedor" runat="server" Text="Sin validar"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnValidar" runat="server" Text="Validar" CssClass="button" OnClick="Validar_Click" Width="" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>

                        </table>
                    </td>
                    <td width="25%">
                        <table>

                            <%-- MONEDA ORIGINAL --%>
                            <tr>
                                <td width="20px">
                                    <label>Moneda Original</label>
                                </td>
                                <td width="100px">
                                    <asp:DropDownList ID="ddlIdMonedaOriginal" runat="server" Width="95%" Enabled="false"></asp:DropDownList>
                                </td>
                            </tr>
                            <%-- MONEDA DOCUMENTO --%>
                            <tr>
                                <td>
                                    <label>Moneda doc</label>
                                </td>
                                <td width="100px">
                                    <asp:DropDownList ID="ddlIdMonedaDoc" runat="server" Width="95%" OnSelectedIndexChanged="ddlIdMonedaDoc_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <%-- TASA DE CAMBIO--%>
                            <tr>
                                <td width="100px">
                                    <label>T. cambio</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTasaCambio" runat="server" Width="95%"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtTasaCambio" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                </td>

                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>

                        </table>
                    </td>
                    <td width="15%">
                        <table>
                            <%-- AFECTA --%>
                            <tr>
                                <td>
                                    <label>Afecta</label>
                                </td>
                                <td width="100px">
                                    <asp:TextBox ID="txtMontoAfecta" runat="server" Width="95%"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtMontoAfecta" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <%-- NO AFECTA --%>
                            <tr>
                                <td width="100px">
                                    <label>No afecta</label></td>
                                <td width="100px">
                                    <asp:TextBox ID="txtMontoNoAfecta" runat="server" Width="95%"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtMontoNoAfecta" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <%-- IGV --%>
                            <tr>
                                <td>
                                    <label>IGV</label></td>
                                <td width="100px">
                                    <asp:TextBox ID="txtMontoIGV" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtMontoIGV" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <%-- TOTAL MONEDA --%>
                            <tr>
                                <td>
                                    <label>Total S/</label></td>
                                <td width="100px">
                                    <asp:TextBox ID="txtMontoTotal" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtMontoTotal" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <%-- TOTAL DOCUMENTO --%>
                            <tr>
                                <td>
                                    <label>Total moneda doc.</label></td>
                                <td width="100px">
                                    <asp:TextBox ID="txtMontoDoc" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtMontoDoc" runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <%-- VALIDAR IMPORTE--%>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="bValidarImporte" runat="server" Text="Validar" CssClass="button" OnClick="ValidarImporte_Click" Width="98px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="25%">
                        <%-- CENTROS DE COSTO --%>
                        <table>
                            <tr>
                                <td>
                                    <label>CC 1</label>
                                </td>
                                <td width="180px">
                                    <asp:DropDownList ID="ddlCentroCostos1" runat="server" Width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddlCentroCostos1_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>CC 2</label>
                                </td>
                                <td width="180px">
                                    <asp:DropDownList ID="ddlCentroCostos2" runat="server" Width="95%" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>CC 3</label>
                                </td>
                                <td width="180px">
                                    <asp:DropDownList ID="ddlCentroCostos3" runat="server" Width="95%" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>CC 4</label>
                                </td>
                                <td width="180px">
                                    <asp:DropDownList ID="ddlCentroCostos4" runat="server" Width="95%" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>CC 5</label>
                                </td>
                                <td width="180px">
                                    <asp:DropDownList ID="ddlCentroCostos5" runat="server" Width="95%" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <%-- PARTIDA PRESUPUESTAL --%>
                            <tr id="TrPartidaPresupuestal" style="display: normal" runat="server">
                                <td>
                                    <label>Partida pres.</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPartidaPresupuestal" runat="server" Width="95%"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
            </table>

            <!-- DivTable.com -->
            <br />
            <table width="1008px">
                <tr>
                    <td align="center">
                        <asp:Button ID="bAgregar" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Agregar" CssClass="button2" OnClick="Agregar_Click" />
                        <asp:Button ID="bGuardar" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Guardar" CssClass="button2" OnClick="Guardar_Click" />
                        <asp:Button ID="bCancelar" runat="server" Text="Regresar" CssClass="button" OnClick="Cancelar_Click" /></td>
                </tr>
            </table>
            <div style="display: none">
                <br />
                <table width="1008px" style="border-top: thick solid #000000;" visible="false">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                                BorderStyle="Double" BorderColor="#989898" CellPadding="10" CellSpacing="0"
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
                            <asp:Label ID="blbResultadoMasivo" runat="server"></asp:Label>
                            <br />
                            <asp:Button ID="bMasivo" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Subir Masivamente" CssClass="button" />
                            <asp:Button ID="bPreliminar4" runat="server" Visible="false" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Vista Preliminar" CssClass="button" />
                            <asp:Button ID="bAgregar4" runat="server" Visible="false" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Agregar" CssClass="button" />
                            <asp:Button ID="bCancelar4" runat="server" Visible="false" Text="Cancelar" CssClass="button" OnClick="Cancelar4_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <table width="1100" style="border-bottom: thick solid #000000;">
                <tr>
                    <td></td>
                </tr>
            </table>
            <br />

            <table width="1008px">
                <tr>
                    <td width="252px"></td>
                    <td width="140px" align="left">
                        <label>Nombre Proveedor</label></td>
                    <td width="196px" align="left">
                        <asp:TextBox ID="txtCardName" runat="server" Width="95%" MaxLength="100"></asp:TextBox></td>
                    <td width="140px" align="left">
                        <label>RUC Proveedor</label></td>
                    <td width="196px" align="left">
                        <asp:TextBox ID="txtDocumento" runat="server" Width="95%" MaxLength="11"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtDocumento" runat="server" Enabled="True" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td width="252px"></td>
                </tr>
            </table>
            <table width="1008px">
                <tr>
                    <td align="center">
                        <asp:Button ID="bAgregar2" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Agregar" CssClass="button" OnClick="Agregar2_Click" />
                        <asp:Button ID="bGuardar2" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Guardar" CssClass="button" OnClick="Guardar2_Click" Visible="false" /></td>
                </tr>
            </table>
            <table width="1008px">
                <tr>
                    <td align="center">
                        <asp:GridView ID="gvProveedor" runat="server"
                            AutoGenerateColumns="false"
                            GridLines="Both"
                            BorderStyle="Double" BorderColor="#989898" CellPadding="10" CellSpacing="0"
                            HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
                            RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF" RowStyle-Font-Size="XX-Small"
                            AllowPaging="true"
                            PageSize="5"
                            OnPageIndexChanging="gridViewP_PageIndexChanging"
                            OnRowCommand="gvProveedor_RowCommand">

                            <Columns>

                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("IdProveedor")%>'>
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
            <table width="1100" style="border-bottom: thick solid #000000;">
                <tr>
                    <td></td>
                </tr>
            </table>
            <br />
            <table width="1008px" style="display: none">
                <tr>
                    <td align="center">
                        <asp:LinkButton ID="lnkExportarReporte" runat="server" Font-Underline="false"> 
     <img src="img/excel.png" alt="Exportar Reporte" width="50px"/><br />Exportar Reporte 
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
            <table width="1008px">
                <tr>
                    <td width="336px"></td>
                    <td width="140px" align="left">
                        <label>Fecha de Contabilizacion:</label></td>
                    <td width="196px" align="left">
                        <asp:TextBox ID="txtFechaContabilizacion" runat="server" Enabled="false"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFechaContabilizacion" CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="img/calendar.png"></ajaxToolkit:CalendarExtender>
                    </td>
                    <td width="336px"></td>
                </tr>
            </table>
            <table width="1008px">
                <tr>
                    <td align="center">
                        <asp:GridView ID="gvDocumentos" runat="server"
                            AutoGenerateColumns="false"
                            GridLines="Both"
                            BorderStyle="Double" BorderColor="#989898" CellPadding="10" CellSpacing="0"
                            HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
                            RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF" RowStyle-Font-Size="XX-Small"
                            AllowPaging="true"
                            PageSize="20"
                            OnPageIndexChanging="gridView_PageIndexChanging"
                            OnRowCommand="gvDocumentos_RowCommand">

                            <Columns>
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("IdDocumentoWebRendicion")%>'> <img src="img/edit.png" alt="Editar" width="20px" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%#Eval("IdDocumentoWebRendicion")%>'> <img src="img/delete.png" alt="Editar" width="20px" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="IdDocumentoWebRendicion" HeaderText="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="SerieDoc" HeaderText="Serie" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="CorrelativoDoc" HeaderText="Numero" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="FechaDoc" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" />

                                <asp:BoundField DataField="SAPProveedor" HeaderText="Proveedor" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="IdConcepto" HeaderText="IdConcepto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="IdCentroCostos1" HeaderText="Centro de costos 1" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="IdCentroCostos2" HeaderText="Centro de costos 2" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="IdCentroCostos3" HeaderText="Centro de costos 3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="IdMonedaDoc" HeaderText="Moneda" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />




                                <%--   <asp:TemplateField HeaderText="Proveedor" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# SetearProveedor(Convert.ToString(Eval("SAPProveedor")))%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# SetearConcepto(Convert.ToString(Eval("IdConcepto")))%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Centro Costos 3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# SetearCentroCostos(Convert.ToString(Eval("IdCentroCostos1")))%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Centro Costos 3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# SetearCentroCostos(Convert.ToString(Eval("IdCentroCostos2")))%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Centro Costos 3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# SetearCentroCostos(Convert.ToString(Eval("IdCentroCostos3")))%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Centro Costos 4" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# SetearCentroCostos(Convert.ToString(Eval("IdCentroCostos4")))%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Centro Costos 5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# SetearCentroCostos(Convert.ToString(Eval("IdCentroCostos5")))%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MontoTotal" HeaderText="Total" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Moneda" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# SetearMoneda(Convert.ToInt32(Eval("IdMonedaDoc")))%></ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="MontoNoAfecto" HeaderText="No Afecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="MontoAfecto" HeaderText="Afecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="MontoIGV" HeaderText="IGV" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="MontoDoc" HeaderText="Total Doc" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />

                            </Columns>

                            <EmptyDataTemplate>
                                No hay informacion que mostrar<br />
                                <img alt="noinfo" src="img/empty.png" width="100%" />
                            </EmptyDataTemplate>

                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table width="1008px">
                <tr>
                    <td width="254px"></td>
                    <td width="100px">
                        <label>
                            <asp:Label ID="lblComentario" runat="server" Text="Observacion"></asp:Label></label></td>
                    <td width="400px">
                        <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Width="95%" Height="50px" MaxLength="1000"></asp:TextBox></td>
                    <td width="254px"></td>
                </tr>
            </table>
            <table width="1008px">
                <tr>
                    <td align="center">
                        <asp:Button ID="bEnviar" runat="server" OnClientClick="this.disabled = true; this.value = 'Procesando...';" UseSubmitBehavior="false" Text="Enviar Rendicion" CssClass="button" OnClick="Enviar_Click" /></td>
                </tr>
            </table>
            <table width="1008px">
                <tr>
                    <td align="center">
                        <asp:Button ID="bAprobar" runat="server" OnClick="Aprobar_Click" OnClientClick="javascript: return ConfirmacionContabilizacion();" Text="Aprobar" CssClass="button" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="bLiquidar" runat="server" OnClick="bLiquidar_Click" OnClientClick="javascript: return ConfirmacionLiquidarCC();" Text="Aprobar y Liquidar" CssClass="button" />
                        <asp:Button ID="bRechazar" runat="server" OnClick="Rechazar_Click" Text="Rechazar" CssClass="button" />
                    </td>
                </tr>
            </table>

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
                BorderStyle="Double" BorderColor="#989898" CellPadding="10" CellSpacing="0"
                HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF"
                HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
                RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF"
                RowStyle-Font-Size="XX-Small">

                <Columns>


                    <asp:BoundField DataField="SerieDoc" HeaderText="Serie" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="CorrelativoDoc" HeaderText="Numero" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="FechaDoc" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" />

                    <asp:BoundField DataField="MontoNoAfecto" HeaderText="No Afecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="MontoAfecto" HeaderText="Afecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="MontoIGV" HeaderText="IGV" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="MontoDoc" HeaderText="Total Doc" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />

                </Columns>

                <EmptyDataTemplate>
                    No hay informacion que mostrar<br />
                    <img alt="noinfo" src="img/empty.png" width="100%" />
                </EmptyDataTemplate>

            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
