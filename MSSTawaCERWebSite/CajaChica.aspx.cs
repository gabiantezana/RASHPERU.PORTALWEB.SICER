using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;
using System.Net.Mail;
using MssTawaCer.App_Code.Helper;

public partial class CajaChica : System.Web.UI.Page
{
    #region On Load Page

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
            Response.Redirect("~/Login.aspx");
        try
        {
            if (!this.IsPostBack)
            {
                Int32 idModo = Convert.ToInt32(Context.Items["Modo"].ToString());
                Int32 idDocumento = Convert.ToInt32(Context.Items["IdCajaChica"].ToString());

                ViewState["Modo"] = idModo;
                ViewState["IdCajaChica"] = idDocumento;

                SetCrearOEditar(idModo, idDocumento);
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    private void SetCrearOEditar(int idModo, Int32 idDocumento)
    {
        try
        {
            ListarUsuarioSolicitante(idModo, idDocumento);
            ListarMoneda();
            ListarEmpresa();

            switch (idModo)
            {
                case 1: //Crear
                    lblCabezera.Text = "Crear Nueva Caja Chica";
                    LimpiarCampos();
                    break;
                case 2: //Editar
                    lblCabezera.Text = "Caja Chica";
                    bCrear.Text = "Guardar";
                    EditarCajaChica_Fill(idDocumento);
                    break;
            }

            SetModadlidadBotones(idModo, idDocumento);
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
        }
    }

    private void SetModadlidadBotones(int Modo, int IdCajaChica)
    {
        if (Session["Usuario"] == null)
            Response.Redirect("~/Login.aspx");
        else
        {
            if (Modo == 1)//TODO?
            {
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
            }
            else
            {
                UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                objUsuarioSesionBE = new UsuarioBC().ObtenerUsuario(objUsuarioSesionBE.IdUsuario, 0);

                Boolean habilitarBotonesDeAprobacion = false;
                Boolean habilitarOBservacion = false;

                CajaChicaBE objCajaChicaBE = new CajaChicaBC().ObtenerCajaChica(IdCajaChica, 0);
                UsuarioBE objUsuarioSolicitanteBE = new UsuarioBC().ObtenerUsuario(objCajaChicaBE.IdUsuarioSolicitante, 0);
                PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBC().ObtenerPerfilUsuario(objUsuarioSesionBE.IdPerfilUsuario);

                EstadoDocumento estadoDocumento = (EstadoDocumento)Enum.Parse(typeof(EstadoDocumento), objCajaChicaBE.Estado);
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
                        if (objCajaChicaBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario
                            || objCajaChicaBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
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

            }
        }
    }

    private void LimpiarCampos()
    {
        txtIdCajaChica.Text = "";
        txtCodigoCajaChica.Text = "";
        txtAsunto.Text = "";
        txtMontoInicial.Text = "";
        txtComentario.Text = "";
    }

    private void EditarCajaChica_Fill(int idCajaChica)
    {
        CajaChicaBE objCajaChicaBE = new CajaChicaBC().ObtenerCajaChica(idCajaChica, 0);

        ListarCentroCostos(objCajaChicaBE.IdEmpresa);
        ListarMetodosPago(objCajaChicaBE.IdEmpresa);

        txtIdCajaChica.Text = objCajaChicaBE.IdCajaChica.ToString();
        txtCodigoCajaChica.Text = objCajaChicaBE.CodigoCajaChica;
        txtAsunto.Text = objCajaChicaBE.Asunto;
        txtMontoInicial.Text = objCajaChicaBE.MontoInicial;
        txtComentario.Text = objCajaChicaBE.Comentario;
        txtMotivoDetalle.Text = objCajaChicaBE.MotivoDetalle;

        ddlIdEmpresa.SelectedValue = objCajaChicaBE.IdEmpresa.ToString();
        ddlIdUsuarioSolicitante.SelectedValue = objCajaChicaBE.IdUsuarioSolicitante.ToString();
        ddlMoneda.SelectedValue = objCajaChicaBE.Moneda.ToString();
        ddlCentroCostos1.SelectedValue = objCajaChicaBE.IdCentroCostos1.ToString();
        ddlCentroCostos2.SelectedValue = objCajaChicaBE.IdCentroCostos2.ToString();
        ddlCentroCostos3.SelectedValue = objCajaChicaBE.IdCentroCostos3.ToString();
        ddlCentroCostos4.SelectedValue = objCajaChicaBE.IdCentroCostos4.ToString();
        ddlCentroCostos5.SelectedValue = objCajaChicaBE.IdCentroCostos5.ToString();
        ddlIdMetodoPago.SelectedValue = objCajaChicaBE.IdMetodoPago.ToString();
    }
    #endregion

    #region Listar Selects

    private void ListarUsuarioSolicitante(int Modo, int IdCajaChica)
    {
        try
        {
            UsuarioBC objUsuarioBC = new UsuarioBC();
            UsuarioBE objUsuarioBE = new UsuarioBE();
            List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();

            if (Modo == 1)
            {
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                lstUsuarioBE = objUsuarioBC.ListarUsuario(1, objUsuarioBE.IdUsuario, 0);
            }
            else
            {
                CajaChicaBC objCajaChicaBC = new CajaChicaBC();
                CajaChicaBE objCajaChicaBE = new CajaChicaBE();
                objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(IdCajaChica, 0);

                lstUsuarioBE = objUsuarioBC.ListarUsuario(1, objCajaChicaBE.IdUsuarioCreador, 0);
            }

            ddlIdUsuarioSolicitante.DataSource = lstUsuarioBE;
            ddlIdUsuarioSolicitante.DataTextField = "CardName";
            ddlIdUsuarioSolicitante.DataValueField = "IdUsuario";
            ddlIdUsuarioSolicitante.DataBind();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
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
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
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
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
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
        else if (new ValidationHelper().UsuarioExcedeCantMaxDocumento(TipoDocumento.CajaChica, Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value)))
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
            String errorMessage;
            CamposSonValidos(out errorMessage);
            if (!String.IsNullOrEmpty(errorMessage))
            {
                Mensaje(errorMessage);
                return;
            }

            Int32 idUsuario = ((UsuarioBE)Session["Usuario"]).IdUsuario;
            Int32 idModo = Convert.ToInt32(ViewState["Modo"].ToString());
            Int32 idDocumento = Convert.ToInt32(ViewState["IdCajaChica"].ToString());

            CajaChicaBE objCajaChicaBE = new CajaChicaBE();
            objCajaChicaBE.CodigoCajaChica = String.Empty;
            objCajaChicaBE.IdUsuarioSolicitante = Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value);
            objCajaChicaBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
            objCajaChicaBE.IdCentroCostos1 = Convert.ToInt32(ddlCentroCostos1.SelectedItem.Value);
            objCajaChicaBE.IdCentroCostos2 = Convert.ToInt32(ddlCentroCostos2.SelectedItem.Value);
            objCajaChicaBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
            objCajaChicaBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
            objCajaChicaBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
            objCajaChicaBE.IdMetodoPago = Convert.ToInt32(ddlIdMetodoPago.SelectedItem.Value);
            objCajaChicaBE.IdArea = 0;
            objCajaChicaBE.Asunto = txtAsunto.Text;
            objCajaChicaBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
            objCajaChicaBE.MontoGastado = "0.00";
            objCajaChicaBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
            objCajaChicaBE.Moneda = ddlMoneda.SelectedItem.Value;
            objCajaChicaBE.Comentario = "";
            objCajaChicaBE.MotivoDetalle = txtMotivoDetalle.Text;
            objCajaChicaBE.FechaSolicitud = DateTime.Now;
            objCajaChicaBE.FechaContabilizacion = DateTime.Now;
            objCajaChicaBE.Estado = EstadoDocumento.PorAprobarNivel1.IdToString();
            objCajaChicaBE.IdUsuarioCreador = idUsuario;
            objCajaChicaBE.UserCreate = idUsuario.ToString();
            objCajaChicaBE.CreateDate = DateTime.Now;
            objCajaChicaBE.UserUpdate = idUsuario.ToString();
            objCajaChicaBE.UpdateDate = DateTime.Now;


            CajaChicaBC objCajaChicaBC = new CajaChicaBC();
            if (idModo == 1) //INSERT
            {
                Int32 idDocumentoInsertado = objCajaChicaBC.InsertarCajaChica(objCajaChicaBE);
                CajaChicaBE documentoInsertado = objCajaChicaBC.ObtenerCajaChica(idDocumentoInsertado, 0);
                EnviarMensajeParaAprobar(idDocumentoInsertado, "Caja Chica", documentoInsertado.MontoGastado, txtAsunto.Text, documentoInsertado.CodigoCajaChica, ddlIdUsuarioSolicitante.SelectedItem.Text, EstadoDocumento.PorAprobarNivel1.IdToString(), documentoInsertado.IdEmpresa);
            }
            else //UPDATE
            {
                objCajaChicaBE.IdCajaChica = idDocumento;
                objCajaChicaBC.ModificarCajaChica(objCajaChicaBE);
            }

            Response.Redirect("CajaChicas.aspx");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    protected void Cancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CajaChicas.aspx");
    }

    protected void Aprobar_Click(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
            Response.Redirect("~/Login.aspx");

        try
        {
            bAprobar.Enabled = false;

            Int32 idUsuario = ((UsuarioBE)Session["Usuario"]).IdUsuario;
            Int32 idModo = Convert.ToInt32(ViewState["Modo"].ToString());
            Int32 idDocumento = Convert.ToInt32(ViewState["IdCajaChica"].ToString());

            CajaChicaBE objCajaChicaBE = new CajaChicaBC().ObtenerCajaChica(idDocumento, 0);

            String estado = String.Empty;
            if (objCajaChicaBE.Estado == EstadoDocumento.ObservacionNivel1.IdToString()
            || objCajaChicaBE.Estado == EstadoDocumento.ObservacionNivel2.IdToString()
            || objCajaChicaBE.Estado == EstadoDocumento.ObservacionNivel3.IdToString())
            {
                estado = objCajaChicaBE.Estado;
                objCajaChicaBE.Estado = Convert.ToString(Convert.ToInt32(objCajaChicaBE.Estado) - 7);
            }
            else
            {
                switch ((EstadoDocumento)Enum.Parse(typeof(EstadoDocumento), objCajaChicaBE.Estado))
                {
                    case EstadoDocumento.PorAprobarNivel1:
                        objCajaChicaBE.Estado = EstadoDocumento.PorAprobarNivel2.IdToString();
                        break;
                    case EstadoDocumento.PorAprobarNivel2:
                        objCajaChicaBE.Estado = EstadoDocumento.Aprobado.IdToString();
                        break;
                }
            }

            new CajaChicaBC().ModificarCajaChica(objCajaChicaBE);
            EnviarMensajeParaAprobar(objCajaChicaBE.IdCajaChica, "Caja Chica", objCajaChicaBE.MontoGastado, txtAsunto.Text, objCajaChicaBE.CodigoCajaChica, ddlIdUsuarioSolicitante.SelectedItem.Text, objCajaChicaBE.Estado, objCajaChicaBE.IdEmpresa);

            Response.Redirect("~/CajaChicas.aspx");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
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
            bRechazar.Enabled = true;

            String strIdCajaChica = "";
            strIdCajaChica = ViewState["IdCajaChica"].ToString();

            UsuarioBE objUsuarioBE = new UsuarioBE();
            CajaChicaBC objCajaChicaBC = new CajaChicaBC();
            CajaChicaBE objCajaChicaBE = new CajaChicaBE();
            objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);

            objCajaChicaBE.Estado = "5";
            objCajaChicaBE.Comentario = txtComentario.Text;
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                objCajaChicaBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                objCajaChicaBE.CreateDate = DateTime.Now;
                objCajaChicaBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                objCajaChicaBE.UpdateDate = DateTime.Now;
            }

            objCajaChicaBC.ModificarCajaChica(objCajaChicaBE);
            //EnviarMensajeRechazado(objUsuarioBE.IdUsuario, objCajaChicaBE.IdUsuarioCreador, objCajaChicaBE.IdUsuarioSolicitante, "Caja Chica", txtAsunto.Text, objCajaChicaBE.CodigoCajaChica, objUsuarioBE.CardName);

            Response.Redirect("~/CajaChicas.aspx");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
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
            bObservacion.Enabled = false;

            String strIdCajaChica = "";
            strIdCajaChica = ViewState["IdCajaChica"].ToString();
            String estado = "";

            UsuarioBE objUsuarioBE = new UsuarioBE();
            CajaChicaBC objCajaChicaBC = new CajaChicaBC();
            CajaChicaBE objCajaChicaBE = new CajaChicaBE();
            objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);

            objCajaChicaBE.Asunto = txtAsunto.Text;
            objCajaChicaBE.Moneda = ddlMoneda.SelectedItem.Value;
            objCajaChicaBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.000000");
            objCajaChicaBE.MontoGastado = "0.000000";
            //objCajaChicaBE.MontoReembolsado = "0.000000";
            objCajaChicaBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.000000");
            objCajaChicaBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
            objCajaChicaBE.IdArea = 0;//Convert.ToInt32(ddlIdArea.SelectedItem.Value);
            objCajaChicaBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
            objCajaChicaBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
            objCajaChicaBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);

            estado = objCajaChicaBE.Estado;
            if (Convert.ToInt32(estado) > 3)
            {
                estado = Convert.ToString(Convert.ToInt32(estado) - 7 - 1);
                objCajaChicaBE.Estado = Convert.ToString(Convert.ToInt32(objCajaChicaBE.Estado) - 1);
            }
            else
                objCajaChicaBE.Estado = Convert.ToString(Convert.ToInt32(objCajaChicaBE.Estado) + 7);

            objCajaChicaBE.Comentario = txtComentario.Text;
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                objCajaChicaBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                objCajaChicaBE.CreateDate = DateTime.Now;
                objCajaChicaBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                objCajaChicaBE.UpdateDate = DateTime.Now;
            }

            objCajaChicaBC.ModificarCajaChica(objCajaChicaBE);
            EnviarMensajeObservacion(objUsuarioBE.IdUsuario, objCajaChicaBE.IdCajaChica, objCajaChicaBE.IdUsuarioSolicitante, objCajaChicaBE.IdUsuarioCreador, "Caja Chica", txtAsunto.Text, objCajaChicaBE.CodigoCajaChica, objUsuarioBE.CardName, estado);

            Response.Redirect("~/CajaChicas.aspx");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
            Response.Redirect("~/CajaChicas.aspx");
            bObservacion.Enabled = true;

        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }

    #endregion

    #region Envio Correos

    private void EnviarMensajeParaAprobar(int IdCajaChica, string Documento, String Monto, string Asunto, string CodigoCajaChica, string UsuarioSolicitante, string estado, int IdEmpresa)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        UsuarioBE objUsuarioBE2 = new UsuarioBE();
        if (estado == "1" || estado == "2" || estado == "3")
            lstUsuarioBE = objUsuarioBC.ListarUsuario(4, IdCajaChica, Convert.ToInt32(estado));
        else
        {
            if (estado == "7" || estado == "8" || estado == "9")
                lstUsuarioBE = objUsuarioBC.ListarUsuario(4, IdCajaChica, Convert.ToInt32(estado) - 7);
            else //4
            {
                CajaChicaBC objCajaChicaBC = new CajaChicaBC();
                CajaChicaBE objCajaChicaBE = new CajaChicaBE();
                objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(IdCajaChica, 0);
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(objCajaChicaBE.IdUsuarioSolicitante, 0);
                objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(objCajaChicaBE.IdUsuarioCreador, 0);
            }
        }

        if (estado != "4")
        {
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + UsuarioSolicitante + " ha solicitado la aprobacion de una " + Documento + " Codigo: " + CodigoCajaChica, "Caja Chica " + CodigoCajaChica + ": " + Asunto, lstUsuarioBE[i].Mail);

            }
        }
        else
        {
            EmpresaBC objEmpresaBC = new EmpresaBC();
            EmpresaBE objEmpresaBE = new EmpresaBE();
            objEmpresaBE = objEmpresaBC.ObtenerEmpresa(IdEmpresa);

            if (objUsuarioBE.Mail.Trim() != "")
            {
                MensajeEmail("La " + Documento + " Codigo: " + CodigoCajaChica + " fue aprobada", "Caja Chica " + CodigoCajaChica + ": " + Asunto, objUsuarioBE.Mail);
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
                        MensajeEmail(lstCorreosBE[0].TextoCorreo.ToString() + ": La " + Documento + " con Codigo: " + CodigoCajaChica + "<br/>" + "<br/>"
                        + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                        + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                        + "Importe a Pagar :" + moneda + txtMontoInicial.Text + "<br/>"
                        + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                        , "Caja Chica " + CodigoCajaChica, lstUsuarioTesoreriaBE[x].Mail.ToString());
                    }

                }

                if (objUsuarioBE.Mail.Trim() != "")
                {
                    lstCorreosBE = objCorreosBC.ObtenerCorreos(1);
                    MensajeEmail(lstCorreosBE[0].TextoCorreo.ToString() + ": La " + Documento + " con Codigo: " + CodigoCajaChica + "<br/>" + "<br/>"
                    + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                    + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                    + "Importe a Pagar :" + moneda + txtMontoInicial.Text + "<br/>"
                    + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                   , "Caja Chica " + CodigoCajaChica, objUsuarioBE.Mail.ToString());
                }
            }
            else
            {
                MensajeEmail("La " + Documento + " Codigo: " + CodigoCajaChica + " fue aprobada", "Caja Chica " + CodigoCajaChica + ": " + Asunto, objUsuarioBE2.Mail);
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
                    MensajeEmail("La " + Documento + " con Codigo: " + CodigoCajaChica + " , " + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>" + "<br/>"
                    + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                    + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                    + "Importe a Pagar :" + txtMontoInicial.Text + Monto + "<br/>"
                    + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                    , "Caja Chica " + CodigoCajaChica, lstUsuarioTesoreriaBE[x].Mail.ToString());
                }

                if (objUsuarioBE.Mail.Trim() != "")
                {
                    MensajeEmail("La " + Documento + " con Codigo: " + CodigoCajaChica + " , " + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>" + "<br/>"
                   + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                   + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                   + "Importe a Pagar :" + moneda + txtMontoInicial.Text + "<br/>"
                   + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                   , "Caja Chica " + CodigoCajaChica, objUsuarioBE.Mail.ToString());
                }
            }
        }
    }

    private void EnviarMensajeObservacion(int IdUsuarioAprobador, int IdCajaChica, int IdUsuarioSolicitante, int IdUsuarioCreador, string Documento, string Asunto, string CodigoCajaChica, string UsuarioAprobador, string estado)
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
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoCajaChica, "Caja Chica " + CodigoCajaChica + ": " + Asunto, objUsuarioBE2.Mail);
            else
            {
                objUsuarioBE2 = new UsuarioBE();
                objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioCreador, 0);
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoCajaChica, "Caja Chica " + CodigoCajaChica + ": " + Asunto, objUsuarioBE2.Mail);
            }
        }
        if (estado == "2")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(4, IdCajaChica, 1);

            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoCajaChica, "Caja Chica " + CodigoCajaChica + ": " + Asunto, lstUsuarioBE[i].Mail);
            }
        }
        if (estado == "3")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(4, IdCajaChica, 2);

            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoCajaChica, "Caja Chica " + CodigoCajaChica + ": " + Asunto, lstUsuarioBE[i].Mail);
            }
        }

    }

    private void EnviarMensajeRechazado(int IdUsuarioAprobador, int IdUsuarioCreador, int IdUsuarioSolicitante, string Documento, string Asunto, string CodigoCajaChica, string UsuarioAprobador, string estado)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        UsuarioBE objUsuarioBE2 = new UsuarioBE();

        objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioAprobador, 0);
        objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);

        objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioCreador, 0);

        if (objUsuarioBE2.Mail.Trim() != "")
            MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha rechazado la solicitud de una " + Documento + " Codigo: " + CodigoCajaChica, "Caja Chica " + CodigoCajaChica + ": " + Asunto, objUsuarioBE2.Mail);
        else
        {
            objUsuarioBE2 = new UsuarioBE();
            objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioCreador, 0);
            MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha rechazado la solicitud de una " + Documento + " Codigo: " + CodigoCajaChica, "Caja Chica " + CodigoCajaChica + ": " + Asunto, objUsuarioBE2.Mail);
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
                Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
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