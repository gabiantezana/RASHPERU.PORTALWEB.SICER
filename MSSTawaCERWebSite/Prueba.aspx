<%@ Page Title="Prueba" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Prueba.aspx.cs" Inherits="Prueba" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<script type="text/javascript">
    window.onload = function () {
        document.getElementById("<%=txtCopied.ClientID %>").onpaste = function () {
            var txt = this;
            setTimeout(function () {
                __doPostBack(txt.name, '');
            }, 100);
        }
    };
</script>

<%--<ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></ajaxtoolkit:toolkitscriptmanager>--%>

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
<Columns>
    <asp:BoundField DataField="Tipo_Documento" HeaderText="Tipo Documento" ItemStyle-Width="150" />
    <asp:BoundField DataField="Serie" HeaderText="Serie" ItemStyle-Width="150" />
    <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-Width="150" />
    <asp:BoundField DataField="Numero" HeaderText="Numero" ItemStyle-Width="150" />
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
<asp:TextBox ID="txtCopied" runat="server" TextMode="MultiLine" AutoPostBack="true" OnTextChanged="PasteToGridView" Height="200" Width="400" />

</asp:Content>