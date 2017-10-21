using MSS.TAWA.BC;
using MSS.TAWA.BE;
using MSS.TAWA.HP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Documento : System.Web.UI.Page
{
    private TipoDocumentoWeb _TipoDocumentoWeb { get; set; }
    private Modo _Modo { get; set; }
    private Int32 _IdDocumento { get; set; }

    #region On Load Page

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
            Response.Redirect("~/Login.aspx");
        try
        {
            if (!this.IsPostBack)
            {
                //Get from context
                _TipoDocumentoWeb = (TipoDocumentoWeb)Context.Items[ConstantHelper.Keys.TipoDocumentoWeb];
                _Modo = (Modo)Context.Items[ConstantHelper.Keys.Modo];
                _IdDocumento = Convert.ToInt32(Context.Items[ConstantHelper.Keys.IdDocumento]);

                //Set to viewState
                ViewState[ConstantHelper.Keys.TipoDocumentoWeb] = _TipoDocumentoWeb;
                ViewState[ConstantHelper.Keys.Modo] = _Modo;
                ViewState[ConstantHelper.Keys.IdDocumento] = _IdDocumento;

                SetCrearOEditar();
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    private void SetCrearOEditar()
    {
        try
        {
            ListarUsuarioSolicitante();
            ListarMoneda();
            ListarEmpresa();

            switch (_Modo)
            {
                case Modo.Crear:
                    switch (_TipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica:
                            lblCabezera.Text = "Crear Nueva Caja Chica";
                            break;
                        case TipoDocumentoWeb.EntregaRendir:
                            lblCabezera.Text = "Crear Nueva Entrega Rendir";
                            break;
                        case TipoDocumentoWeb.Reembolso:
                            lblCabezera.Text = "Crear Nuevo Reembolso";
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    LimpiarCampos();
                    break;
                case Modo.Editar:
                    switch (_TipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica:
                            lblCabezera.Text = "Caja Chica";
                            break;
                        case TipoDocumentoWeb.EntregaRendir:
                            lblCabezera.Text = "Entrega Rendir";
                            break;
                        case TipoDocumentoWeb.Reembolso:
                            lblCabezera.Text = "Reembolso";
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    bCrear.Text = "Guardar";
                    EditarDocumento_Fill();
                    break;
            }
            SetModadlidadBotones();
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error: " + ex.Message);
        }
    }

    private void SetModadlidadBotones()
    {
        if (Session["Usuario"] == null)
            Response.Redirect("~/Login.aspx");
        else
        {
            switch (_Modo)
            {
                case Modo.Crear:
                    bCrear.Visible = true;
                    bCancelar.Visible = true;
                    bAprobar.Visible = false;
                    bObservacion.Visible = false;
                    bRechazar.Visible = false;
                    bCancelar2.Visible = false;

                    ddlCentroCostos3.Enabled = false;
                    ddlCentroCostos4.Enabled = false;
                    ddlCentroCostos5.Enabled = false;
                    ddlIdMetodoPago.Enabled = false;
                    txtComentario.Enabled = false;
                    break;

                case Modo.Editar:
                    UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                    objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                    objUsuarioSesionBE = new UsuarioBC().ObtenerUsuario(objUsuarioSesionBE.IdUsuario, 0);

                    Boolean habilitarBotonesDeAprobacion = false;
                    Boolean habilitarOBservacion = false;

                    DocumentBE objDocumentoBE = new DocumentBC(_TipoDocumentoWeb).ObtenerDocumento(_IdDocumento, 0);
                    UsuarioBE objUsuarioSolicitanteBE = new UsuarioBC().ObtenerUsuario(objDocumentoBE.IdUsuarioSolicitante, 0);
                    PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBC().ObtenerPerfilUsuario(objUsuarioSesionBE.IdPerfilUsuario);

                    EstadoDocumento estadoDocumento = (EstadoDocumento)Enum.Parse(typeof(EstadoDocumento), objDocumentoBE.Estado);
                    TipoAprobador tipoAprobador = (TipoAprobador)Enum.Parse(typeof(TipoAprobador), objPerfilUsuarioBE.TipoAprobador);
                    switch (estadoDocumento)
                    {
                        case EstadoDocumento.PorAprobarNivel1:
                        case EstadoDocumento.PorAprobarNivel2:
                        case EstadoDocumento.PorAprobarNivel3:
                            switch (tipoAprobador)
                            {
                                case TipoAprobador.Aprobador:
                                case TipoAprobador.AprobadorYCreador:
                                    habilitarBotonesDeAprobacion = new ValidationHelper().UsuarioPuedeAprobarDocumento(estadoDocumento, objUsuarioSolicitanteBE);
                                    break;
                            }
                            break;
                        case EstadoDocumento.ObservacionNivel1:
                        case EstadoDocumento.ObservacionNivel2:
                        case EstadoDocumento.ObservacionNivel3:
                            if (objDocumentoBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario
                                || objDocumentoBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
                                habilitarOBservacion = true;
                            break;
                    }

                    bCrear.Visible = false;
                    bCancelar.Visible = false;
                    bAprobar.Visible = false;
                    bObservacion.Visible = false;
                    bRechazar.Visible = false;
                    bCancelar2.Visible = true;
                    txtComentario.Enabled = true;

                    if (habilitarBotonesDeAprobacion)
                    {
                        bAprobar.Visible = true;
                        bObservacion.Visible = true;
                        bRechazar.Visible = true;
                    }
                    if (habilitarOBservacion)
                    {
                        bAprobar.Text = "Enviar";
                        bAprobar.Visible = true;
                    }
                    break;
            }
        }
    }

    private void LimpiarCampos()
    {
        txtIdDocumento.Text = "";
        txtCodigoDocumento.Text = "";
        txtAsunto.Text = "";
        txtMontoInicial.Text = "";
        txtComentario.Text = "";
    }

    private void EditarDocumento_Fill()
    {
        DocumentBE objDocumentoBE = new DocumentBC(_TipoDocumentoWeb).ObtenerDocumento(_IdDocumento, 0);

        ListarCentroCostos(objDocumentoBE.IdEmpresa);
        ListarMetodosPago(objDocumentoBE.IdEmpresa);

        txtIdDocumento.Text = objDocumentoBE.IdDocumento.ToString();
        txtCodigoDocumento.Text = objDocumentoBE.CodigoDocumento;
        txtAsunto.Text = objDocumentoBE.Asunto;
        txtMontoInicial.Text = objDocumentoBE.MontoInicial;
        txtComentario.Text = objDocumentoBE.Comentario;
        txtMotivoDetalle.Text = objDocumentoBE.MotivoDetalle;

        ddlIdEmpresa.SelectedValue = objDocumentoBE.IdEmpresa.ToString();
        ddlIdUsuarioSolicitante.SelectedValue = objDocumentoBE.IdUsuarioSolicitante.ToString();
        ddlMoneda.SelectedValue = objDocumentoBE.Moneda.ToString();
        ddlCentroCostos1.SelectedValue = objDocumentoBE.IdCentroCostos1.ToString();
        ddlCentroCostos2.SelectedValue = objDocumentoBE.IdCentroCostos2.ToString();
        ddlCentroCostos3.SelectedValue = objDocumentoBE.IdCentroCostos3.ToString();
        ddlCentroCostos4.SelectedValue = objDocumentoBE.IdCentroCostos4.ToString();
        ddlCentroCostos5.SelectedValue = objDocumentoBE.IdCentroCostos5.ToString();
        ddlIdMetodoPago.SelectedValue = objDocumentoBE.IdMetodoPago.ToString();
    }

    #endregion

    #region Listar Selects

    private void ListarUsuarioSolicitante()
    {
        try
        {
            UsuarioBC objUsuarioBC = new UsuarioBC();
            UsuarioBE objUsuarioBE = new UsuarioBE();
            List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();

            if (_Modo == Modo.Crear)
            {
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                lstUsuarioBE = objUsuarioBC.ListarUsuario(1, objUsuarioBE.IdUsuario, 0);
            }
            else
            {
                DocumentBE objDocumentoBE = new DocumentBC(_TipoDocumentoWeb).ObtenerDocumento(_IdDocumento, 0);
                lstUsuarioBE = objUsuarioBC.ListarUsuario(1, objDocumentoBE.IdUsuarioCreador, 0);
            }

            ddlIdUsuarioSolicitante.DataSource = lstUsuarioBE;
            ddlIdUsuarioSolicitante.DataTextField = "CardName";
            ddlIdUsuarioSolicitante.DataValueField = "IdUsuario";
            ddlIdUsuarioSolicitante.DataBind();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    private void ListarMoneda()
    {
        try
        {
            MonedaBC objMonedaBC = new MonedaBC();
            ddlMoneda.DataSource = objMonedaBC.ListarMoneda(0, 1);
            ddlMoneda.DataTextField = "Descripcion";
            ddlMoneda.DataValueField = "IdMoneda";
            ddlMoneda.DataBind();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    private void ListarEmpresa()
    {
        try
        {
            EmpresaBC objEmpresaBC = new EmpresaBC();
            List<EmpresaBE> lstEmpresaBE = new List<EmpresaBE>();
            lstEmpresaBE = objEmpresaBC.ListarEmpresa();

            ddlIdEmpresa.DataSource = lstEmpresaBE;
            ddlIdEmpresa.DataTextField = "Descripcion";
            ddlIdEmpresa.DataValueField = "IdEmpresa";
            ddlIdEmpresa.DataBind();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    private void ListarCentroCostos(int idEmpresa)
    {
        CentroCostosBC objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos1.DataSource = objCentroCostosBC.ListarCentroCostos(idEmpresa, 1);
        ddlCentroCostos1.DataTextField = "Descripcion";
        ddlCentroCostos1.DataValueField = "IdCentroCostos";
        ddlCentroCostos1.DataBind();
        ddlCentroCostos1.Enabled = true;

        ddlCentroCostos2.DataSource = objCentroCostosBC.ListarCentroCostos(idEmpresa, 2);
        ddlCentroCostos2.DataTextField = "Descripcion";
        ddlCentroCostos2.DataValueField = "IdCentroCostos";
        ddlCentroCostos2.DataBind();
        ddlCentroCostos2.Enabled = true;

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos3.DataSource = objCentroCostosBC.ListarCentroCostos(idEmpresa, 3);
        ddlCentroCostos3.DataTextField = "Descripcion";
        ddlCentroCostos3.DataValueField = "IdCentroCostos";
        ddlCentroCostos3.DataBind();
        ddlCentroCostos3.Enabled = true;

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos4.DataSource = objCentroCostosBC.ListarCentroCostos(idEmpresa, 4);
        ddlCentroCostos4.DataTextField = "Descripcion";
        ddlCentroCostos4.DataValueField = "IdCentroCostos";
        ddlCentroCostos4.DataBind();
        ddlCentroCostos4.Enabled = true;

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(idEmpresa, 5);
        ddlCentroCostos5.DataTextField = "Descripcion";
        ddlCentroCostos5.DataValueField = "IdCentroCostos";
        ddlCentroCostos5.DataBind();
        ddlCentroCostos5.Enabled = true;
    }

    private void ListarMetodosPago(int idEmpresa)
    {
        MetodoPagoBC objMetodoPagoBC = new MetodoPagoBC();
        ddlIdMetodoPago.DataSource = objMetodoPagoBC.ListarMetodoPago(idEmpresa, 1, 0);
        ddlIdMetodoPago.DataTextField = "Descripcion";
        ddlIdMetodoPago.DataValueField = "IdMetodoPago";
        ddlIdMetodoPago.DataBind();
        ddlIdMetodoPago.Enabled = true;
    }

    #endregion

    #region Submit Buttons

    public Boolean CamposSonValidos(out String errorMessage)
    {
        Int32[] indexNoValidos = { 0, -1 };
        errorMessage = String.Empty;
        if (indexNoValidos.Contains(ddlIdUsuarioSolicitante.SelectedIndex))
            errorMessage = "Debe ingresar el usuario solicitante";
        else if (indexNoValidos.Contains(ddlIdEmpresa.SelectedIndex))
            errorMessage = "Debe ingresar la empresa";
        else if (indexNoValidos.Contains(ddlMoneda.SelectedIndex))
            errorMessage = "Debe ingresar la  moneda";
        else if (String.IsNullOrWhiteSpace(txtMontoInicial.Text))
            errorMessage = "Debe ingresar el monto inicial";
        else if (!txtMontoInicial.Text.IsNumeric())
            errorMessage = "El importe inicial no es válido";
        else if (indexNoValidos.Contains(ddlCentroCostos1.SelectedIndex))
            errorMessage = "Debe ingresar el centro de costo nivel 1";
        else if (String.IsNullOrWhiteSpace(txtAsunto.Text))
            errorMessage = "Debe ingresar el asunto.";
        else if (String.IsNullOrWhiteSpace(txtMotivoDetalle.Text))
            errorMessage = "Debe ingresar el motivo";
        else if (new ValidationHelper().UsuarioExcedeCantMaxDocumento(_TipoDocumentoWeb, Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value)))
            errorMessage = "El usuario ha excedido la cantidad máxima de documentos.";

        if (!String.IsNullOrEmpty(errorMessage))
            return false;
        else
            return true;
    }

    protected void Crear_Click(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
            Response.Redirect("~/Login.aspx");

        try
        {
            _TipoDocumentoWeb = (TipoDocumentoWeb)ViewState[ConstantHelper.Keys.TipoDocumentoWeb];
            _Modo = (Modo)ViewState[ConstantHelper.Keys.Modo];

            Int32 idDocumento = Convert.ToInt32(ViewState[ConstantHelper.Keys.IdDocumento]);
            Int32 idUsuario = ((UsuarioBE)Session["Usuario"]).IdUsuario;

            String errorMessage;
            CamposSonValidos(out errorMessage);
            if (!String.IsNullOrEmpty(errorMessage))
            {
                Mensaje(errorMessage);
                return;
            }

            DocumentBE objDocumentoBE = new DocumentBE();
            objDocumentoBE.CodigoDocumento = String.Empty;
            objDocumentoBE.IdUsuarioSolicitante = Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value);
            objDocumentoBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
            objDocumentoBE.IdCentroCostos1 = ddlCentroCostos1.SelectedItem.Value.ToString();
            objDocumentoBE.IdCentroCostos2 = ddlCentroCostos2.SelectedItem.Value.ToString();
            objDocumentoBE.IdCentroCostos3 = ddlCentroCostos3.SelectedItem.Value.ToString();
            objDocumentoBE.IdCentroCostos4 = ddlCentroCostos4.SelectedItem.Value.ToString();
            objDocumentoBE.IdCentroCostos5 = ddlCentroCostos5.SelectedItem.Value.ToString();
            objDocumentoBE.IdMetodoPago = Convert.ToInt32(ddlIdMetodoPago.SelectedItem.Value);
            objDocumentoBE.IdArea = 0;
            objDocumentoBE.Asunto = txtAsunto.Text;
            objDocumentoBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
            objDocumentoBE.MontoGastado = "0.00";
            objDocumentoBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
            objDocumentoBE.Moneda = ddlMoneda.SelectedItem.Value;
            objDocumentoBE.Comentario = "";
            objDocumentoBE.MotivoDetalle = txtMotivoDetalle.Text;
            objDocumentoBE.FechaSolicitud = DateTime.Now;
            objDocumentoBE.FechaContabilizacion = DateTime.Now;
            objDocumentoBE.Estado = EstadoDocumento.PorAprobarNivel1.IdToString();
            objDocumentoBE.IdUsuarioCreador = idUsuario;
            objDocumentoBE.UserCreate = idUsuario.ToString();
            objDocumentoBE.CreateDate = DateTime.Now;
            objDocumentoBE.UserUpdate = idUsuario.ToString();
            objDocumentoBE.UpdateDate = DateTime.Now;


            DocumentBC objDocumentoBC = new DocumentBC(_TipoDocumentoWeb);
            switch (_Modo)
            {
                case Modo.Crear:
                    Int32 idDocumentoInsertado = objDocumentoBC.InsertarDocumento(objDocumentoBE);
                    DocumentBE documentoInsertado = objDocumentoBC.ObtenerDocumento(idDocumentoInsertado, 0);
                    EnviarMensajeParaAprobar(idDocumentoInsertado, _TipoDocumentoWeb.GetName(), documentoInsertado.MontoGastado, txtAsunto.Text, documentoInsertado.CodigoDocumento, ddlIdUsuarioSolicitante.SelectedItem.Text, EstadoDocumento.PorAprobarNivel1.IdToString(), documentoInsertado.IdEmpresa);
                    break;

                case Modo.Editar:
                    objDocumentoBE.IdDocumento = idDocumento;
                    objDocumentoBC.ModificarDocumento(objDocumentoBE);
                    break;
            }

            Response.Redirect("~/ListadoDocumentos.aspx?TipoDocumentoWeb=" + (Int32)_TipoDocumentoWeb);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    protected void Cancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ListadoDocumentos.aspx?TipoDocumentoWeb=" + (Int32)_TipoDocumentoWeb);
    }

    protected void Aprobar_Click(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
            Response.Redirect("~/Login.aspx");

        try
        {
            _TipoDocumentoWeb = (TipoDocumentoWeb)ViewState[ConstantHelper.Keys.TipoDocumentoWeb];
            _Modo = (Modo)ViewState[ConstantHelper.Keys.Modo];
            _IdDocumento = (Int32)base.ViewState[ConstantHelper.Keys.IdDocumento];

            Int32 idUsuario = ((UsuarioBE)Session["Usuario"]).IdUsuario;

            bAprobar.Enabled = false;

            DocumentBE objDocumentoBE = new DocumentBC(_TipoDocumentoWeb).ObtenerDocumento(_IdDocumento, 0);

            String estado = String.Empty;
            if (objDocumentoBE.Estado == EstadoDocumento.ObservacionNivel1.IdToString()
            || objDocumentoBE.Estado == EstadoDocumento.ObservacionNivel2.IdToString()
            || objDocumentoBE.Estado == EstadoDocumento.ObservacionNivel3.IdToString())
            {
                estado = objDocumentoBE.Estado;
                objDocumentoBE.Estado = Convert.ToString(Convert.ToInt32(objDocumentoBE.Estado) - 7);
            }
            else
            {
                switch ((EstadoDocumento)Enum.Parse(typeof(EstadoDocumento), objDocumentoBE.Estado))
                {
                    case EstadoDocumento.PorAprobarNivel1:
                        objDocumentoBE.Estado = EstadoDocumento.PorAprobarNivel2.IdToString();
                        break;
                    case EstadoDocumento.PorAprobarNivel2:
                        objDocumentoBE.Estado = EstadoDocumento.Aprobado.IdToString();
                        break;
                }
            }

            new DocumentBC(_TipoDocumentoWeb).ModificarDocumento(objDocumentoBE);
            EnviarMensajeParaAprobar(objDocumentoBE.IdDocumento, _TipoDocumentoWeb.GetName(), objDocumentoBE.MontoGastado, txtAsunto.Text, objDocumentoBE.CodigoDocumento, ddlIdUsuarioSolicitante.SelectedItem.Text, objDocumentoBE.Estado, objDocumentoBE.IdEmpresa);

            Response.Redirect("~/ListadoDocumentos.aspx?TipoDocumentoWeb=" + (Int32)_TipoDocumentoWeb);

        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
            bAprobar.Enabled = true;
        }
    }

    protected void Rechazar_Click(object sender, EventArgs e)
    {
        try
        {
            _TipoDocumentoWeb = (TipoDocumentoWeb)ViewState[ConstantHelper.Keys.TipoDocumentoWeb];
            _Modo = (Modo)ViewState[ConstantHelper.Keys.Modo];
            _IdDocumento = (Int32)base.ViewState[ConstantHelper.Keys.IdDocumento];

            Int32 idUsuario = ((UsuarioBE)Session["Usuario"]).IdUsuario;

            bRechazar.Enabled = true;

            UsuarioBE objUsuarioBE = new UsuarioBE();
            DocumentBE objDocumentoBC = new DocumentBC(_TipoDocumentoWeb).ObtenerDocumento(Convert.ToInt32(_IdDocumento), 0);

            objDocumentoBC.Estado = EstadoDocumento.Rechazado.IdToString();
            objDocumentoBC.Comentario = txtComentario.Text;
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                objDocumentoBC.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                objDocumentoBC.CreateDate = DateTime.Now;
                objDocumentoBC.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                objDocumentoBC.UpdateDate = DateTime.Now;
            }

            new DocumentBC(_TipoDocumentoWeb).ModificarDocumento(objDocumentoBC);

            Response.Redirect("~/ListadoDocumentos.aspx?TipoDocumentoWeb=" + (Int32)_TipoDocumentoWeb);

        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
            bRechazar.Enabled = true;
        }
    }

    protected void Observacion_Click(object sender, EventArgs e)
    {
        try
        {
            _TipoDocumentoWeb = (TipoDocumentoWeb)ViewState[ConstantHelper.Keys.TipoDocumentoWeb];
            _Modo = (Modo)ViewState[ConstantHelper.Keys.Modo];
            _IdDocumento = (Int32)ViewState[ConstantHelper.Keys.IdDocumento];

            Int32 idUsuario = ((UsuarioBE)Session["Usuario"]).IdUsuario;

            bObservacion.Enabled = false;

            String estado = "";

            UsuarioBE objUsuarioBE = new UsuarioBE();
            DocumentBE objDocumentoBE = new DocumentBC(_TipoDocumentoWeb).ObtenerDocumento(Convert.ToInt32(_IdDocumento), 0);

            objDocumentoBE.Asunto = txtAsunto.Text;
            objDocumentoBE.Moneda = ddlMoneda.SelectedItem.Value;
            objDocumentoBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.000000");
            objDocumentoBE.MontoGastado = "0";
            objDocumentoBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.000000");
            objDocumentoBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
            objDocumentoBE.IdArea = 0;//Convert.ToInt32(ddlIdArea.SelectedItem.Value);
            objDocumentoBE.IdCentroCostos3 = ddlCentroCostos3.SelectedItem.Value;
            objDocumentoBE.IdCentroCostos4 = ddlCentroCostos4.SelectedItem.Value;
            objDocumentoBE.IdCentroCostos5 = ddlCentroCostos5.SelectedItem.Value;

            estado = objDocumentoBE.Estado;
            if (Convert.ToInt32(estado) > 3)
            {
                estado = Convert.ToString(Convert.ToInt32(estado) - 7 - 1);
                objDocumentoBE.Estado = Convert.ToString(Convert.ToInt32(objDocumentoBE.Estado) - 1);
            }
            else
                objDocumentoBE.Estado = Convert.ToString(Convert.ToInt32(objDocumentoBE.Estado) + 7);

            objDocumentoBE.Comentario = txtComentario.Text;
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                objDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                objDocumentoBE.CreateDate = DateTime.Now;
                objDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                objDocumentoBE.UpdateDate = DateTime.Now;
            }

            new DocumentBC(_TipoDocumentoWeb).ModificarDocumento(objDocumentoBE);
            EnviarMensajeObservacion(objUsuarioBE.IdUsuario, objDocumentoBE.IdDocumento, objDocumentoBE.IdUsuarioSolicitante, objDocumentoBE.IdUsuarioCreador, _TipoDocumentoWeb.GetName(), txtAsunto.Text, objDocumentoBE.CodigoDocumento, objUsuarioBE.CardName, estado);

            Response.Redirect("~/ListadoDocumentos.aspx?TipoDocumentoWeb=" + (Int32)_TipoDocumentoWeb);

        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
            Response.Redirect("~/ListadoDocumentos.aspx?TipoDocumentoWeb=" + (Int32)_TipoDocumentoWeb);

            bObservacion.Enabled = true;

        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }

    #endregion

    #region Envio Correos

    private void EnviarMensajeParaAprobar(int IdDocumento, string Documento, String Monto, string Asunto, string codigoDocumento, string UsuarioSolicitante, string estado, int IdEmpresa)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        UsuarioBE objUsuarioBE2 = new UsuarioBE();
        if (estado == "1" || estado == "2" || estado == "3")
            lstUsuarioBE = objUsuarioBC.ListarUsuario(4, IdDocumento, Convert.ToInt32(estado));
        else
        {
            if (estado == "7" || estado == "8" || estado == "9")
                lstUsuarioBE = objUsuarioBC.ListarUsuario(4, IdDocumento, Convert.ToInt32(estado) - 7);
            else //4
            {
                DocumentBE objDocumentoBE = new DocumentBC(_TipoDocumentoWeb).ObtenerDocumento(IdDocumento, 0);
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(objDocumentoBE.IdUsuarioSolicitante, 0);
                objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(objDocumentoBE.IdUsuarioCreador, 0);
            }
        }

        if (estado != "4")
        {
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + UsuarioSolicitante + " ha solicitado la aprobacion de una " + Documento + " Codigo: " + codigoDocumento, _TipoDocumentoWeb.GetName() + codigoDocumento + ": " + Asunto, lstUsuarioBE[i].Mail);

            }
        }
        else
        {
            EmpresaBC objEmpresaBC = new EmpresaBC();
            EmpresaBE objEmpresaBE = new EmpresaBE();
            objEmpresaBE = objEmpresaBC.ObtenerEmpresa(IdEmpresa);

            if (objUsuarioBE.Mail.Trim() != "")
            {
                MensajeEmail("La " + Documento + " Codigo: " + codigoDocumento + " fue aprobada", _TipoDocumentoWeb.GetName() + codigoDocumento + ": " + Asunto, objUsuarioBE.Mail);
                List<UsuarioBE> lstUsuarioTesoreriaBE = new List<UsuarioBE>();
                lstUsuarioTesoreriaBE = objUsuarioBC.ListarUsuarioCorreosTesoreria();

                CorreosBE objCorreoBE = new CorreosBE();
                CorreosBC objCorreosBC = new CorreosBC();
                List<CorreosBE> lstCorreosBE = new List<CorreosBE>();



                String moneda = "";
                if (ddlMoneda.SelectedValue.ToString() == "1")
                    moneda = "S/. ";
                else
                    moneda = "USD. ";


                for (int x = 0; x < lstUsuarioTesoreriaBE.Count; x++)
                {


                    if (lstUsuarioTesoreriaBE[x].Mail.ToString() != "")
                    {
                        lstCorreosBE = objCorreosBC.ObtenerCorreos(1);
                        MensajeEmail(lstCorreosBE[0].TextoCorreo.ToString() + ": La " + Documento + " con Codigo: " + codigoDocumento + "<br/>" + "<br/>"
                        + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                        + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                        + "Importe a Pagar :" + moneda + txtMontoInicial.Text + "<br/>"
                        + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                        , _TipoDocumentoWeb.GetName() + codigoDocumento, lstUsuarioTesoreriaBE[x].Mail.ToString());
                    }

                }

                if (objUsuarioBE.Mail.Trim() != "")
                {
                    lstCorreosBE = objCorreosBC.ObtenerCorreos(1);
                    MensajeEmail(lstCorreosBE[0].TextoCorreo.ToString() + ": La " + Documento + " con Codigo: " + codigoDocumento + "<br/>" + "<br/>"
                    + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                    + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                    + "Importe a Pagar :" + moneda + txtMontoInicial.Text + "<br/>"
                    + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                   , _TipoDocumentoWeb.GetName() + codigoDocumento, objUsuarioBE.Mail.ToString());
                }
            }
            else
            {
                MensajeEmail("La " + Documento + " Codigo: " + codigoDocumento + " fue aprobada", _TipoDocumentoWeb.GetName() + " " + codigoDocumento + ": " + Asunto, objUsuarioBE2.Mail);
                List<UsuarioBE> lstUsuarioTesoreriaBE = new List<UsuarioBE>();
                lstUsuarioTesoreriaBE = objUsuarioBC.ListarUsuarioCorreosTesoreria();
                CorreosBE objCorreoBE = new CorreosBE();
                CorreosBC objCorreosBC = new CorreosBC();
                List<CorreosBE> lstCorreosBE = new List<CorreosBE>();

                String moneda = "";
                if (ddlMoneda.SelectedValue.ToString() == "1")
                    moneda = "S/. ";
                else
                    moneda = "USD. ";

                for (int x = 0; x < lstUsuarioTesoreriaBE.Count; x++)
                {
                    lstCorreosBE = objCorreosBC.ObtenerCorreos(1);
                    MensajeEmail("La " + Documento + " con Codigo: " + codigoDocumento + " , " + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>" + "<br/>"
                    + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                    + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                    + "Importe a Pagar :" + txtMontoInicial.Text + Monto + "<br/>"
                    + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                    , _TipoDocumentoWeb.GetName() + " " + codigoDocumento, lstUsuarioTesoreriaBE[x].Mail.ToString());
                }

                if (objUsuarioBE.Mail.Trim() != "")
                {
                    MensajeEmail("La " + Documento + " con Codigo: " + codigoDocumento + " , " + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>" + "<br/>"
                   + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                   + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                   + "Importe a Pagar :" + moneda + txtMontoInicial.Text + "<br/>"
                   + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                   , _TipoDocumentoWeb.GetName() + " " + codigoDocumento, objUsuarioBE.Mail.ToString());
                }
            }
        }
    }

    private void EnviarMensajeObservacion(int IdUsuarioAprobador, int idDocumento, int IdUsuarioSolicitante, int IdUsuarioCreador, string Documento, string Asunto, string codigoDocumento, string UsuarioAprobador, string estado)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        UsuarioBE objUsuarioBE2 = new UsuarioBE();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();

        objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioAprobador, 0);
        if (estado == "1")
        {
            objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);
            if (objUsuarioBE2.Mail.Trim() != "")
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + codigoDocumento, _TipoDocumentoWeb.GetName() + " " + codigoDocumento + ": " + Asunto, objUsuarioBE2.Mail);
            else
            {
                objUsuarioBE2 = new UsuarioBE();
                objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioCreador, 0);
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + codigoDocumento, _TipoDocumentoWeb.GetName() + " " + codigoDocumento + ": " + Asunto, objUsuarioBE2.Mail);
            }
        }
        if (estado == "2")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(4, idDocumento, 1);

            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + codigoDocumento, _TipoDocumentoWeb.GetName() + " " + codigoDocumento + ": " + Asunto, lstUsuarioBE[i].Mail);
            }
        }
        if (estado == "3")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(4, idDocumento, 2);

            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + codigoDocumento, _TipoDocumentoWeb.GetName() + " " + codigoDocumento + ": " + Asunto, lstUsuarioBE[i].Mail);
            }
        }

    }

    private void EnviarMensajeRechazado(int IdUsuarioAprobador, int IdUsuarioCreador, int IdUsuarioSolicitante, string Documento, string Asunto, string codigoDocumento, string UsuarioAprobador, string estado)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        UsuarioBE objUsuarioBE2 = new UsuarioBE();

        objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioAprobador, 0);
        objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);

        objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioCreador, 0);

        if (objUsuarioBE2.Mail.Trim() != "")
            MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha rechazado la solicitud de una " + Documento + " Codigo: " + codigoDocumento, _TipoDocumentoWeb.GetName() + " " + codigoDocumento + ": " + Asunto, objUsuarioBE2.Mail);
        else
        {
            objUsuarioBE2 = new UsuarioBE();
            objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioCreador, 0);
            MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha rechazado la solicitud de una " + Documento + " Codigo: " + codigoDocumento, _TipoDocumentoWeb.GetName() + " " + codigoDocumento + ": " + Asunto, objUsuarioBE2.Mail);
        }
    }

    private void MensajeEmail(string Cuerpo, string Asunto, string Destino)//string UsuarioSolicitante, string Documento, string Asunto, string CodigoEntregaRendir, string Destino)
    {
        if (Destino.Trim() != "")
        {
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            String email_body = "";
            correo.From = new System.Net.Mail.MailAddress("procesos.peru@tawa.com.pe");
            correo.To.Add(Destino.Trim());
            correo.Subject = Asunto;
            email_body = Cuerpo + ". Por favor ingresar al Portal Web para continuar con el proceso si fuera necesario.";
            correo.Body = email_body;
            correo.IsBodyHtml = true;
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "mailhost1.tawa.com.pe";
            smtp.EnableSsl = false;

            try
            {
                smtp.Send(correo);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Mensaje("Ocurrió un error: " + ex.Message);
                ExceptionHelper.LogException(ex);
            }
        }
    }

    #endregion

    #region Select Change

    protected void ddlIdEmpresa_SelectedIndexChanged1(object sender, EventArgs e)
    {
        Int32 idEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedValue.ToString());
        if (idEmpresa != 0)
        {
            ddlCentroCostos1.Enabled = true;
            ddlCentroCostos2.Enabled = true;
            ddlCentroCostos3.Enabled = true;
            ddlCentroCostos4.Enabled = true;
            ddlCentroCostos5.Enabled = true;
            ddlIdMetodoPago.Enabled = true;

            ListarCentroCostos(idEmpresa);
            ListarMetodosPago(idEmpresa);
        }
        else
        {
            ddlCentroCostos1.SelectedValue = "0";
            ddlCentroCostos1.Enabled = false;
            ddlCentroCostos2.SelectedValue = "0";
            ddlCentroCostos2.Enabled = false;
            ddlCentroCostos3.SelectedValue = "0";
            ddlCentroCostos3.Enabled = false;
            ddlCentroCostos4.SelectedValue = "0";
            ddlCentroCostos4.Enabled = false;
            ddlCentroCostos5.SelectedValue = "0";
            ddlCentroCostos5.Enabled = false;

            ddlIdMetodoPago.SelectedValue = "0";
            ddlIdMetodoPago.Enabled = false;
        }
    }

    #endregion

    #region Validaciones

    private bool validaDecimales(string p)
    {
        string[] words = p.Split('.');
        int cantidad = words.Length;
        string decimales = "000";

        if (cantidad == 2) decimales = words[1];

        if (decimales.Length == 2) return true;
        else return false;
    }

    #endregion
}