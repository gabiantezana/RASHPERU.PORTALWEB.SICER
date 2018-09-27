using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;
using MSS.TAWA.HP;
using System.Web.UI.HtmlControls;

public partial class ListadoDocumentos : System.Web.UI.Page
{
    TipoDocumentoWeb _TipoDocumentoWeb { get; set; }
    Int32 _IdDocumentoWeb { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
            Server.Transfer("~/Login.aspx");

        _TipoDocumentoWeb = (TipoDocumentoWeb)Convert.ToInt32(Request.QueryString[ConstantHelper.Keys.TipoDocumentoWeb].ToString());

        if (!this.IsPostBack)
        {
            ListarFiltro();
            ListarEstado();
            ListarUsuario();
            SetearTitulo();
            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = false;
            ValidarMenu();
        }
    }

    private void SetearTitulo()
    {
        lblTitle.InnerText = "Listado de ";
        switch (_TipoDocumentoWeb)
        {
            case TipoDocumentoWeb.CajaChica:
                lblTitle.InnerText += "Caja Chicas";
                break;
            case TipoDocumentoWeb.EntregaRendir:
                lblTitle.InnerText += "Entregas a Rendir";
                break;
            case TipoDocumentoWeb.Reembolso:
                lblTitle.InnerText += "Reembolsos";
                break;
        }
    }

    private void ListarDocumentosTodo()
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];
        DocumentoWebBC documentBC = new DocumentoWebBC();
        gvDocumentos.DataSource = documentBC.GetList(objUsuarioBE.IdUsuario, TipoDocumentoWeb.CajaChica);
        gvDocumentos.DataBind();
    }

    private void ListarDocumentos(int idPerfil)
    {
        Int32 _IdUsuario = ((UsuarioBE)Session["Usuario"]).IdUsuario;
        gvDocumentos.DataSource = new DocumentoWebBC().GetList(_IdUsuario, _TipoDocumentoWeb);
        gvDocumentos.DataBind();

        /*
        if (idPerfil != 2
        && idPerfil != 1002
        && idPerfil != 1008)
            gvDocumentos.DataSource = new DocumentoWebBC().GetList(_IdUsuario);
        else
        {
            //ddlFiltro.SelectedIndex = 5;
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 8;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = true;
            bBuscar.Enabled = true;
            gvDocumentos.DataSource = documentBC.GetList(objUsuarioBE.IdUsuario);
        }

        gvDocumentos.DataBind();*/
    }

    private void LimpiarDocumento()
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];

        DocumentoWebBC documentBC = new DocumentoWebBC();
        gvDocumentos.DataSource = null;
        gvDocumentos.DataBind();
    }

    private void ListarFiltro()
    {
        try
        {
            ddlFiltro.Items.Clear();
            ListItem oItem = new ListItem("Filtrar Por", "0");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Código Documento", "1");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("DNI", "2");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Nombre Solicitante", "3");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Es Facturable", "4");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Estado", "5");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Sin Filtro", "6");
            ddlFiltro.Items.Add(oItem);

        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    private void ListarUsuario()
    {
        UsuarioBC objUsarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsarioBE = new List<UsuarioBE>();

        ddlNombre_Solicitante.DataSource = objUsarioBC.ListarUsuario(12, 0, 0);
        ddlNombre_Solicitante.DataTextField = "CardName";
        ddlNombre_Solicitante.DataValueField = "IdUsuario";
        ddlNombre_Solicitante.DataBind();
    }

    private void ListarEstado()
    {
        ddlEstado.Items.Clear();
        ListItem oItem = new ListItem("Todos", "0");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Por Aprobar Solicitud", "1"); //"Por Aprobar Nivel 1" "Por Aprobar Nivel 2" "Por Aprobar Nivel 3" 
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Aprobado", "4");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Rechazado", "5");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Liquidada", "16");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Observaciones a Solicitud", "8"); //"Observaciones Nivel 1" "Observaciones Nivel 2" "Observaciones Nivel 3" 
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Por Aprobar Rendicion", "11"); //"Rendir: Por Aprobar Nivel 1" "Rendir: Por Aprobar Nivel 2" "Rendir: Por Aprobar Nivel 3"        
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Observaciones Rendicion", "12"); //"Rendir: Observaciones Nivel 1" "Rendir: Observaciones Nivel 2" "Rendir: Observaciones Nivel 3"        
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Por Aprobar Contabilidad", "17");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Observaciones Contabilidad", "18");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Rendicion Aprobadas", "19");
        ddlEstado.Items.Add(oItem);
    }

    private void ValidarMenu()
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];
        objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);

        PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
        PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBE();
        objPerfilUsuarioBE = objPerfilUsuarioBC.ObtenerPerfilUsuario(objUsuarioBE.IdPerfilUsuario);

        ListarDocumentos(objPerfilUsuarioBE.IdPerfilUsuario);

        switch (_TipoDocumentoWeb)
        {
            case TipoDocumentoWeb.CajaChica:
                if (objPerfilUsuarioBE.CreaCajaChica == "1")
                    lnkNuevoDocumento.Visible = true;
                else
                    lnkNuevoDocumento.Visible = false;
                break;
            case TipoDocumentoWeb.EntregaRendir:
                if (objPerfilUsuarioBE.CreaEntregaRendir == "1")
                    lnkNuevoDocumento.Visible = true;
                else
                    lnkNuevoDocumento.Visible = false;
                break;
            case TipoDocumentoWeb.Reembolso:
                if (objPerfilUsuarioBE.CreaReembolso == "1")
                    lnkNuevoDocumento.Visible = true;
                else
                    lnkNuevoDocumento.Visible = false;
                break;
            default:
                throw new NotImplementedException();

        }
    }

    protected void gvDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            DocumentoWebBC documentBC = new DocumentoWebBC();

            Int32 IdDocumentoWeb = Convert.ToInt32(e.CommandArgument.ToString());
            //Int32 IdDocumentoRendicion = Convert.ToInt32(commandArgs[1]);

            Context.Items.Add(ConstantHelper.Keys.TipoDocumentoWeb, _TipoDocumentoWeb);
            Context.Items.Add(ConstantHelper.Keys.IdDocumentoWeb, IdDocumentoWeb);

            if (e.CommandName.Equals("Aprobacion"))
            {
                DocumentoWebBE documentBE = documentBC.GetDocumentoWeb(IdDocumentoWeb);
                EstadoDocumento estadoDocumento = (EstadoDocumento)Convert.ToInt32(documentBE.Estado);

                switch (estadoDocumento)
                {
                    case EstadoDocumento.PorAprobarNivel1:
                    case EstadoDocumento.PorAprobarNivel2:
                    case EstadoDocumento.PorAprobarNivel3:
                        Context.Items.Add(ConstantHelper.Keys.Modo, Modo.Editar);
                        Server.Transfer("~/Documento.aspx");
                        break;
                    case EstadoDocumento.Rechazado:
                    case EstadoDocumento.Liquidado:
                        throw new NotImplementedException();
                    case EstadoDocumento.Aprobado:
                    case EstadoDocumento.RendirPorAprobarJefeArea:
                    case EstadoDocumento.RendirPorAprobarContabilidad:
                    case EstadoDocumento.RendirAprobado:
                        Context.Items.Add(ConstantHelper.Keys.Modo, Modo.Crear);
                        Server.Transfer("~/DocumentoRendicion.aspx");
                        break;
                    default:
                        throw new NotImplementedException();
                }


            }
            if (e.CommandName.Equals("Rendir"))
            {

                DocumentoWebBE documentBE = documentBC.GetDocumentoWeb(IdDocumentoWeb);
                EstadoDocumento estadoDocumento = (EstadoDocumento)Convert.ToInt32(documentBE.Estado);

                if (new ValidationHelper().DocumentoSePuedeRendir(estadoDocumento))
                {
                    Context.Items.Add(ConstantHelper.Keys.Modo, Modo.Crear);
                    Server.Transfer("~/DocumentoRendicion.aspx");
                }
                else 
                    Mensaje("El documento aún no ha sido aprobado.");
            }
            if (e.CommandName.Equals("Historial"))
            {
                Context.Items.Add(ConstantHelper.Keys.Modo, Modo.Editar);
                Server.Transfer("~/DocumentoRendicionH.aspx");
            }
            if (e.CommandName.Equals("Solicitud"))
            {
                Context.Items.Add(ConstantHelper.Keys.Modo, Modo.Editar);
                Server.Transfer("~/Documento.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error:" + ex.Message);
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDocumentos.PageIndex = e.NewPageIndex;
        Buscar_Click(null, null);

    }

    public String SetearIdUsuarioSolicitante(String sId)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = objUsuarioBC.ObtenerUsuario(Convert.ToInt32(sId), 0);
        return objUsuarioBE.CardName;
    }

    protected void ddlIdEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public String SetearIdArea(String sId)
    {
        //AreaBC objAreaBC = new AreaBC();
        //AreaBE objAreaBE = new AreaBE();
        //objAreaBE = objAreaBC.ObtenerArea(Convert.ToInt32(sId));
        //return objAreaBE.Descripcion;
        return "";
    }

    public String SetearMoneda(String sId)
    {
        MonedaBC objMonedaBC = new MonedaBC();
        MonedaBE objMonedaBE = new MonedaBE();
        objMonedaBE = objMonedaBC.ObtenerMoneda(Convert.ToInt32(sId));
        return objMonedaBE.Descripcion;
    }

    public String SetearEsFacturable(String sId)
    {
        String texto = "";
        switch (sId)
        {
            case "0": texto = "Falta Seleccionar"; break;
            case "1": texto = "Si"; break;
            case "2": texto = "No"; break;
        }
        return texto;
    }

    public String SetearFiltro(String sId)
    {
        String texto = "";
        switch (sId)
        {
            case "0": texto = "Filtrar por"; break;
            case "1": texto = "Código Documento"; break;
            case "2": texto = "DNI"; break;
            case "3": texto = "Nombre Solicitante"; break;
            case "4": texto = "Es Facturable"; break;
            case "5": texto = "Estado"; break;


        }
        return texto;
    }

    public String SetearMomentoFacturable(String sId)
    {
        String texto = "";
        switch (sId)
        {
            case "0": texto = "Falta Seleccionar"; break;
            case "1": texto = "En la apertura y la rendicion."; break;
            case "2": texto = "En la rendicion."; break;
        }
        return texto;
    }

    public String SetearEstado(String sId)
    {
        String texto = "";
        switch (sId)
        {
            case "1": texto = "Por Aprobar Nivel 1"; break;
            case "2": texto = "Por Aprobar Nivel 2"; break;
            case "3": texto = "Por Aprobar Nivel 3"; break;
            case "4": texto = "Aprobado"; break;
            case "5": texto = "Rechazado"; break;
            case "8": texto = "Observaciones Nivel 1"; break;
            case "9": texto = "Observaciones Nivel 2"; break;
            case "10": texto = "Observaciones Nivel 3"; break;
            case "11": texto = "Rendir: Por Aprobar Nivel 1"; break;
            case "12": texto = "Rendir: Observaciones Nivel 1"; break;
            case "13": texto = "Rendir: Por Aprobar Contabilidad"; break;
            case "14": texto = "Rendir: Observaciones Contabilidad"; break;
            case "15": texto = "Rendir: Aprobado"; break;
            case "16": texto = "Liquidado"; break;
        }
        return texto;
    }


    public String SetearIdEmpresa(String sId)
    {
        EmpresaBC objEmpresaBC = new EmpresaBC();
        EmpresaBE objEmpresaBE = new EmpresaBE();
        objEmpresaBE = objEmpresaBC.ObtenerEmpresa(Convert.ToInt32(1));
        return objEmpresaBE.Descripcion;
    }

    protected void lnkNuevoDocumento_Click(object sender, EventArgs e)
    {
        Context.Items.Add(ConstantHelper.Keys.TipoDocumentoWeb, _TipoDocumentoWeb);
        Context.Items.Add(ConstantHelper.Keys.Modo, Modo.Crear);
        Context.Items.Add(ConstantHelper.Keys.IdDocumento, 0);

        Server.Transfer("~/Documento.aspx");
    }

    protected void Buscar_Click(object sender, EventArgs e)
    {
        /*
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];

        DocumentoWebBC documentBC = new DocumentoWebBC();
        if (ddlFiltro.SelectedItem.Value == "1")
        {
            gvDocumentos.DataSource = documentBC.ListarDocumentos(objUsuarioBE.IdUsuario, 1, 0, txtCodigo.Text, "", "", "", "");
            gvDocumentos.DataBind();
        }
        else if (ddlFiltro.SelectedItem.Value == "2")
        {
            gvDocumentos.DataSource = documentBC.ListarDocumentos(objUsuarioBE.IdUsuario, 1, 0, "", txtDni.Text, "", "", "");
            gvDocumentos.DataBind();
        }
        else if (ddlFiltro.SelectedItem.Value == "3")
        {
            gvDocumentos.DataSource = documentBC.ListarDocumentos(objUsuarioBE.IdUsuario, 1, 0, "", "", ddlNombre_Solicitante.SelectedValue, "", "");
            gvDocumentos.DataBind();
        }

        else if (ddlFiltro.SelectedItem.Value == "5")
        {
            gvDocumentos.DataSource = documentBC.ListarDocumentos(objUsuarioBE.IdUsuario, 3, 0, "", "", "", "", ddlEstado.SelectedValue);
            gvDocumentos.DataBind();
        }
        else
        {
            ListarDocumentosTodo();
        }
        */
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }

    protected void gvDocumentos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFiltro.SelectedValue == "0")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEsFacturable.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = false;
            LimpiarDocumento();
            ListarDocumentosTodo();


        }
        if (ddlFiltro.SelectedValue == "1")
        {

            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = true;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarDocumento();
        }
        if (ddlFiltro.SelectedValue == "2")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = true;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarDocumento();
        }
        if (ddlFiltro.SelectedValue == "3")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = true;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarDocumento();
        }
        if (ddlFiltro.SelectedValue == "4")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarDocumento();
        }
        if (ddlFiltro.SelectedValue == "5")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = true;
            bBuscar.Enabled = true;
            LimpiarDocumento();
        }
        if (ddlFiltro.SelectedValue == "6")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;

            ddlEstado.SelectedIndex = 0;
            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = false;
            ListarDocumentosTodo();
        }
    }
}
