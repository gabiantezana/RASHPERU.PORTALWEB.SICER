<%@ Page Title="ListadoDocumentos" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ListadoDocumentos.aspx.cs" Inherits="ListadoDocumentos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style2 {
            width: 14%;
        }

        .style3 {
            width: 20%;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>

    <table width="100%">
        <tr>
              <td align="center" colspan="7">
                  <h1 runat="server" id="lblTitle" style="align-content:center" ></h1></td>
        </tr>
        <tr>
          
            <td class="style2">
                <asp:LinkButton ID="lnkNuevoDocumento" runat="server" Font-Underline="false" OnClick="lnkNuevoDocumento_Click"> 
            <img src="img/money.png" alt="" width="50px"/><br />NUEVO
                </asp:LinkButton>
            </td>
            <td style="display:none" align="right" width="10%">
                <label>Filtro:</label></td>
            <td   style="display:none"  align ="left" width="15%">
                <label>
                    <asp:DropDownList ID="ddlFiltro" runat="server" Width="95%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                    </asp:DropDownList></label>
            </td>
            <td align="left" class="style2">
                <label>&nbsp;</label></td>
        </tr>
        <%-- INICIO FILTROS --%>
        <tr style="display:none">
            <td class="style2">
                <label>
                    DNI:<br />
                    <br />
                    Código Documento:
                </label>
            </td>
            <td width="12.5%">
                <asp:TextBox ID="txtDni" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
            </td>
            <td width="12.5%">
                <label>
                    Nombre Solicitante:<br />
                    <br />
                    Es Facturable:</label></td>
            <td class="style2">
                <asp:DropDownList ID="ddlNombre_Solicitante" runat="server" Width="95%"></asp:DropDownList>
                <br />
                <br />
                <asp:DropDownList ID="ddlEsFacturable" runat="server" Width="95%"></asp:DropDownList></td>
            <td class="style3">
                <label>
                    Estado:
                    <asp:DropDownList ID="ddlEstado" runat="server"
                        Width="73%" Height="16px">
                    </asp:DropDownList>
                    <br />
                    <br />
                </label>
            </td>
            <td width="12.5%">
                <asp:Button ID="bBuscar" runat="server" Text="Buscar" CssClass="button" OnClick="Buscar_Click" />
            </td>
            <td width="12.5%">&nbsp;</td>
            <td width="12.5%">&nbsp;</td>
            <td>
                <br />
            </td>
        </tr>
        <%-- FIN FILTROS --%>
    </table>
    <br />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:GridView ID="gvDocumentos" runat="server"
                    AutoGenerateColumns="false"
                    GridLines="Both"
                    BorderStyle="Double" BorderColor="#989898" CellPadding="10" CellSpacing="0"
                    HeaderStyle-BackColor="#059BD8" HeaderStyle-ForeColor="#FFFFFF"
                    HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
                    RowStyle-BackColor="#FFFFFF" AlternatingRowStyle-BackColor="#EFEFEF" RowStyle-Font-Size="X-Small"
                    AllowPaging="true"
                    PageSize="20"
                    OnPageIndexChanging="gridView_PageIndexChanging"
                    OnRowCommand="gvDocumentos_RowCommand"
                    OnSelectedIndexChanged="gvDocumentos_SelectedIndexChanged">

                    <Columns>
                        <asp:BoundField DataField="CodigoDocumento" HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderText="Empresa" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate><%# SetearIdEmpresa(Convert.ToString(Eval("IdEmpresa")))%></ItemTemplate>
                        </asp:TemplateField>
                  <%--      <asp:TemplateField HeaderText="Usuario Creador" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate><%# SetearIdUsuarioSolicitante(Convert.ToString(Eval("IdUsuarioCreador")))%></ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Usuario Solicitante" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate><%# SetearIdUsuarioSolicitante(Convert.ToString(Eval("IdUsuarioSolicitante")))%></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Moneda" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate><%# SetearMoneda(Convert.ToString(Eval("Moneda")))%></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MontoInicial" HeaderText="Monto Inicial" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="MontoGastado" HeaderText="Ultimo Monto Gastado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="MontoActual" HeaderText="Monto Actual" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha Solicitud" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate><%# SetearEstado(Convert.ToString(Eval("Estado")))%></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Comentario" HeaderText="Comentario" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderText="Solicitud" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSolicitud" runat="server" CommandName="Solicitud"  CommandArgument='<% #Eval("IdDocumentoWeb")%>'>
         <img src="img/detail.png" alt="Editar" width="20px" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Aprobacion" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAprobacion" runat="server" CommandName="Aprobacion"  CommandArgument='<% #Eval("IdDocumentoWeb")%>'>
         <img src="img/apply.png" alt="Editar" width="20px" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rendir" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRendir" runat="server" CommandName="Rendir"    CommandArgument='<% #Eval("IdDocumentoWeb")%>'>
         <img src="img/cashRegister.png" alt="Editar" width="20px" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                  <%--     
                        <asp:TemplateField HeaderText="Historial" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkHistorial" runat="server" CommandName="Historial"   CommandArgument='<% #Eval("IdDocumentoWeb")%>'>
         <img src="img/details.png" alt="Editar" width="20px" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    </Columns>

                    <EmptyDataTemplate>
                        No hay informacion que mostrar<br />
                        <img alt="noinfo" src="img/empty.png" width="100%" />
                    </EmptyDataTemplate>

                </asp:GridView>
            </td>
        </tr>
    </table>

</asp:Content>
