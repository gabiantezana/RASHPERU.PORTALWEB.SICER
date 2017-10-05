using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class CajaChicasH : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        try
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.RegisterPostBackControl(lnkExportarReporte);

            String strModo = "";
            String strIdCajaChica = "";

            if (!this.IsPostBack)
            {
                strModo = Context.Items["Modo"].ToString();
                strIdCajaChica = Context.Items["IdCajaChica"].ToString();

                ViewState["Modo"] = strModo;
                ViewState["IdCajaChica"] = strIdCajaChica;

                CajaChicaBC objCajaChicaBC = new CajaChicaBC();
                CajaChicaBE objCajaChicaBE = new CajaChicaBE();
                objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);
                lblCabezera.Text = "Historico Rendicion Caja Chica: " + objCajaChicaBE.CodigoCajaChica;
                ListarRendicion();
                ListarFiltro();
                LlenarCamposCaberaExcel1();
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
        }
    }

    private void ListarRendicion()
    {
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
        gvCajaChicas.DataSource = objCajaChicaDocumentoBC.ListarCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 2, 0);
        gvCajaChicas.DataBind();
    }

    private void ListarFiltro()
    {
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
        ddlFiltro.DataSource = objCajaChicaDocumentoBC.ListarCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 5, 0);
        ddlFiltro.DataTextField = "CorrelativoDoc";
        ddlFiltro.DataValueField = "Rendicion";
        ddlFiltro.DataBind();
    }

    protected void gvCajaChicas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int IdCajaChicaDocumento;

        try
        {
            IdCajaChicaDocumento = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.Equals("Editar"))
            {                
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCajaChicas.PageIndex = e.NewPageIndex;
        ListarRendicion();
    } 

    public String SetearTipo(String sId)
    {
        DocumentoBC objDocumentoBC = new DocumentoBC();
        DocumentoBE objDocumentoBE = new DocumentoBE();
        objDocumentoBE = objDocumentoBC.ObtenerDocumento(Convert.ToInt32(sId));
        if (objDocumentoBE != null) return objDocumentoBE.Descripcion;
        else return "";
    }

    public String SetearProveedor(String sId)
    {
        ProveedorBC objProveedorBC = new ProveedorBC();
        ProveedorBE objProveedorBE = new ProveedorBE();
        objProveedorBE = objProveedorBC.ObtenerProveedor(Convert.ToInt32(sId), 0, "");
        if (objProveedorBE != null) return objProveedorBE.CardName;
        else return "";
    }

    public String SetearProveedorRUC(String sIdProveedor, String sTipoDoc)
    {
        ProveedorBC objProveedorBC = new ProveedorBC();
        ProveedorBE objProveedorBE = new ProveedorBE();
        objProveedorBE = objProveedorBC.ObtenerProveedor(Convert.ToInt32(sIdProveedor), 0, "");
        if (objProveedorBE != null) return objProveedorBE.Documento;
        else return "";
    }

    public String SetearConcepto(String sId)
    {
        ConceptoBC objConceptoBC = new ConceptoBC();
        ConceptoBE objConceptoBE = new ConceptoBE();
        objConceptoBE = objConceptoBC.ObtenerConcepto(Convert.ToInt32(sId));
        if (objConceptoBE != null) return objConceptoBE.Descripcion;
        else return "";
    }

    public String SetearCentroCostos(String sId)
    {
        CentroCostosBC objCentroCostosBC = new CentroCostosBC();
        CentroCostosBE objCentroCostosBE = new CentroCostosBE();
        objCentroCostosBE = objCentroCostosBC.ObtenerCentroCostos(Convert.ToInt32(sId));
        if (objCentroCostosBE != null) return objCentroCostosBE.Descripcion;
        else return "";
    }

    public String SetearMoneda(String sId)
    {
        MonedaBC objMonedaBC = new MonedaBC();
        MonedaBE objMonedaBE = new MonedaBE();
        objMonedaBE = objMonedaBC.ObtenerMoneda(Convert.ToInt32(sId));
        if (objMonedaBE != null) return objMonedaBE.Descripcion;
        else return "";
    }

    protected void Buscar_Click(object sender, EventArgs e)
    {
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();        
        if (ddlFiltro.SelectedItem.Value == "0")
        {
            gvCajaChicas.DataSource = objCajaChicaDocumentoBC.ListarCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 2, 0);
            gvCajaChicas.DataBind();
        }
        else
        {
            gvCajaChicas.DataSource = objCajaChicaDocumentoBC.ListarCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 6, Convert.ToInt32(ddlFiltro.SelectedItem.Value));
            gvCajaChicas.DataBind();
        }
    }

    protected void lnkExportarReporte_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlFiltro.SelectedItem.Value != "0")
            {
                ListarRendicion2();
                LlenarCamposCaberaExcel2();

                //HttpResponse responsePage = new HttpResponse();
                //responsePage= Response;
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pageToRender = new Page();
                HtmlForm form = new HtmlForm();
                form.Controls.Add(gvReporte);
                pageToRender.Controls.Add(form);
                String nameReport = Label9.Text;
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Pragma", "no-cache");
                Response.AddHeader("Content-Disposition", "attachment;filename=" + nameReport + ".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                pageToRender.RenderControl(htw);

                string headerTable = @"<table width='100%'><tr><td></td><td></td></tr>";
                headerTable = headerTable + @"<tr><td><b>Empresa:</b></td><td colspan=4>" + Label1.Text + "</td></tr>";
                headerTable = headerTable + @"<tr><td><b>Nombre:</b></td><td colspan=4>" + Label2.Text + "</td></tr>";
                headerTable = headerTable + @"<tr><td><b>Motivo:</b></td><td colspan=4>" + Label3.Text + "</td></tr>";
                headerTable = headerTable + @"<tr><td><b>Fecha Solicitud:</b></td><td colspan=4>" + Label4.Text + "</td></tr>";
                headerTable = headerTable + @"<tr><td><b>Fecha Liquidacion:</b></td><td colspan=4>" + Label5.Text + "</td></tr>";
                headerTable = headerTable + @"<tr><td><b>Moneda:</b></td><td colspan=4>" + Label6.Text + "</td></tr>";
                headerTable = headerTable + @"<tr><td><b>Total Caja Chica:</b></td><td colspan=4>" + Label7.Text + "</td></tr>";
                headerTable = headerTable + @"<tr><td><b>Total Gastado:</b></td><td colspan=4>" + Label8.Text + "</td></tr>";
                headerTable = headerTable + @"<tr><td><b>Caja Chica:</b></td><td colspan=4>" + Label9.Text + "</td></tr>";
                headerTable = headerTable + @"</table>";
                string footerTable = @"<table width='100%'><tr>";
                footerTable = footerTable + @"<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>Total</td>";
                footerTable = footerTable + @"<td>" + Label10.Text + "</td>";
                footerTable = footerTable + @"<td></td>";
                footerTable = footerTable + @"<td>" + Label11.Text + "</td>";
                footerTable = footerTable + @"<td>" + Label12.Text + "</td>";
                footerTable = footerTable + @"<td>" + Label13.Text + "</td>";
                footerTable = footerTable + @"<td>" + Label14.Text + "</td>";
                footerTable = footerTable + @"</tr></table>";
                Response.Write(headerTable);
                Response.Write(sw.ToString().Normalize());
                Response.Write(footerTable);
                Response.End();
            }
            else
            {
                Mensaje("Debe seleccionar una rendicion");
            }
        }
        catch (Exception ex)
        {
            Mensaje("El Excel a guardar no debe estar abierto: " + ex.Message);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    private void LlenarCamposCaberaExcel1()
    {
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaBC objCajaChicaBC = new CajaChicaBC();
        CajaChicaBE objCajaChicaBE = new CajaChicaBE();
        objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);

        EmpresaBC objEmpresaBC = new EmpresaBC();
        EmpresaBE objEmpresaBE = new EmpresaBE();
        objEmpresaBE = objEmpresaBC.ObtenerEmpresa(objCajaChicaBE.IdEmpresa);

        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = objUsuarioBC.ObtenerUsuario(objCajaChicaBE.IdUsuarioSolicitante, 0);

        //MotivoBC objMotivoBC = new MotivoBC();
        //MotivoBE objMotivoBE = new MotivoBE();
        //objMotivoBE = objMotivoBC.ObtenerMotivo(objCajaChicaBE.IdMotivo);

        MonedaBC objMonedaBC = new MonedaBC();
        MonedaBE objMonedaBE = new MonedaBE();
        objMonedaBE = objMonedaBC.ObtenerMoneda(Convert.ToInt32(objCajaChicaBE.Moneda));

        Label1.Text = objEmpresaBE.Descripcion;
        Label2.Text = objUsuarioBE.CardName;
        //Label3.Text = objMotivoBE.Descripcion;
        Label4.Text = (objCajaChicaBE.FechaSolicitud).ToString("dd/MM/yyyy");
        Label5.Text = (objCajaChicaBE.UpdateDate).ToString("dd/MM/yyyy");
        Label6.Text = objMonedaBE.Descripcion;
        Label7.Text = ""; //objCajaChicaBE.MontoInicial;
        Label8.Text = "";
        Label9.Text = objCajaChicaBE.CodigoCajaChica;
    }

    private void LlenarCamposCaberaExcel2()
    {
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
        List<CajaChicaDocumentoBE> lstCajaChicaDocumentoBE = new List<CajaChicaDocumentoBE>();
        lstCajaChicaDocumentoBE = objCajaChicaDocumentoBC.ListarCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 7, Convert.ToInt32(ddlFiltro.SelectedItem.Value));

        Label7.Text = lstCajaChicaDocumentoBE[0].CorrelativoDoc;
        Label8.Text = lstCajaChicaDocumentoBE[0].MontoTotal;
        Label10.Text = lstCajaChicaDocumentoBE[0].MontoTotal;
        Label11.Text = lstCajaChicaDocumentoBE[0].MontoNoAfecto;
        Label12.Text = lstCajaChicaDocumentoBE[0].MontoAfecto;
        Label13.Text = lstCajaChicaDocumentoBE[0].MontoIGV;
        Label14.Text = lstCajaChicaDocumentoBE[0].MontoDoc;
    }

    private void ListarRendicion2()
    {
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
        gvReporte.DataSource = objCajaChicaDocumentoBC.ListarCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 6, Convert.ToInt32(ddlFiltro.SelectedItem.Value));
        gvReporte.DataBind();
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
}