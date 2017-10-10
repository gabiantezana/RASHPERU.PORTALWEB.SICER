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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        try
        {
            String strModo = "";
            String strIdCajaChica = "";

            if (!this.IsPostBack)
            {
                strModo = Context.Items["Modo"].ToString();
                strIdCajaChica = Context.Items["IdCajaChica"].ToString();

                ViewState["Modo"] = strModo;
                ViewState["IdCajaChica"] = strIdCajaChica;

                ListarUsuarioSolicitante(Convert.ToInt32(strModo), Convert.ToInt32(strIdCajaChica));
                ListarMoneda();
                ListarEmpresa();
                ListarCentroCostos();
                Modalidad(Convert.ToInt32(strModo));
                ModalidadCampo(Convert.ToInt32(strModo), Convert.ToInt32(strIdCajaChica));
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

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

    private void ListarCentroCostos()
    {
    }

    private void Modalidad(int p)
    {
        try
        {
            switch (p)
            {
                case 1:
                    lblCabezera.Text = "Crear Nueva Caja Chica";
                    LimpiarCampos();
                    break;
                case 2:
                    lblCabezera.Text = "Caja Chica";
                    bCrear.Text = "Guardar";
                    LlenarCampos(Convert.ToInt32(ViewState["IdCajaChica"].ToString()));
                    break;
            }
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
        }
    }

    private void ModalidadCampo(int Modo, int IdCajaChica)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        else
        {
            //UsuarioAreaNivelBC objUsuarioAreaNivelBC = new UsuarioAreaNivelBC();
            //UsuarioAreaNivelBE objUsuarioAreaNivelBE = new UsuarioAreaNivelBE();
            //objUsuarioAreaNivelBE = objUsuarioAreaNivelBC.ObtenerUsuarioAreaNivel(objUsuarioBE.IdUsuario, 1, IdCajaChica);

            //NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
            //NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();
            //if (objUsuarioAreaNivelBE != null)
            //    objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(objUsuarioAreaNivelBE.IdNivelAprobacion, 0);

            //bCrear.Visible = false;
            //bCancelar.Visible = false;
            //bAprobar.Visible = false;
            //bObservacion.Visible = false;
            //bRechazar.Visible = false;
            //bCancelar2.Visible = false;

            //ddlEsFacturable.Enabled = false;
            //ddlMomentoFacturable.Enabled = false;
            //txtComentario.Enabled = false;

            if (Modo == 1)
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
                UsuarioBC objUsuarioBC = new UsuarioBC();
                UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                UsuarioBE objUsuarioSolicitanteBE = new UsuarioBE();
                objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                objUsuarioSesionBE = objUsuarioBC.ObtenerUsuario(objUsuarioSesionBE.IdUsuario, 0);

                PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
                PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBE();
                objPerfilUsuarioBE = objPerfilUsuarioBC.ObtenerPerfilUsuario(objUsuarioSesionBE.IdPerfilUsuario);

                CajaChicaBC objCajaChicaBC = new CajaChicaBC();
                CajaChicaBE objCajaChicaBE = new CajaChicaBE();
                objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(IdCajaChica, 0);
                objUsuarioSolicitanteBE = objUsuarioBC.ObtenerUsuario(objCajaChicaBE.IdUsuarioSolicitante, 0);

                bCrear.Visible = false;
                bCancelar.Visible = false;
                bAprobar.Visible = false;
                bObservacion.Visible = false;
                bRechazar.Visible = false;
                bCancelar2.Visible = true;
                txtComentario.Enabled = true;
                //1: Por Aprobar Nivel 1 /2: Por Aprobar Nivel 2 /3: Por Aprobar Nivel 3 /4: Aprobado 
                //5: Rechazado /8: Observaciones Nivel 1 /9: Observaciones Nivel 2 /10: Observaciones Nivel 3

                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objCajaChicaBE.Estado == "1")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioCC1 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objCajaChicaBE.Estado == "2")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioCC2 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objCajaChicaBE.Estado == "3")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioCC3 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //if (objCajaChicaBE.Estado == "4" || objCajaChicaBE.Estado == "5") bCancelar2.Visible = true;
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objCajaChicaBE.Estado == "8")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                    {
                        if (objCajaChicaBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true; //bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                        if (objCajaChicaBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true; //bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objCajaChicaBE.Estado == "9")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                    {
                        if (objCajaChicaBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true; //bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                        if (objCajaChicaBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true; //bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objCajaChicaBE.Estado == "10")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                    {
                        if (objCajaChicaBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true; //bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                        if (objCajaChicaBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true; //bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
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

    private void LlenarCampos(int p)
    {
        CajaChicaBC objCajaChicaBC = new CajaChicaBC();
        CajaChicaBE objCajaChicaBE = new CajaChicaBE();
        objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(p, 0);

        txtIdCajaChica.Text = objCajaChicaBE.IdCajaChica.ToString();
        txtCodigoCajaChica.Text = objCajaChicaBE.CodigoCajaChica;
        ddlIdUsuarioSolicitante.SelectedValue = objCajaChicaBE.IdUsuarioSolicitante.ToString();
        ddlIdEmpresa.SelectedValue = objCajaChicaBE.IdEmpresa.ToString();

        CentroCostosBC objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos1.DataSource = objCentroCostosBC.ListarCentroCostos(objCajaChicaBE.IdEmpresa, 1);
        ddlCentroCostos1.DataTextField = "Descripcion";
        ddlCentroCostos1.DataValueField = "IdCentroCostos";
        ddlCentroCostos1.DataBind();
        ddlCentroCostos1.Enabled = true;
        ddlCentroCostos1.SelectedValue = objCajaChicaBE.IdCentroCostos1.ToString();

        ddlCentroCostos2.DataSource = objCentroCostosBC.ListarCentroCostos(objCajaChicaBE.IdEmpresa, 2);
        ddlCentroCostos2.DataTextField = "Descripcion";
        ddlCentroCostos2.DataValueField = "IdCentroCostos";
        ddlCentroCostos2.DataBind();
        ddlCentroCostos2.Enabled = true;
        ddlCentroCostos2.SelectedValue = objCajaChicaBE.IdCentroCostos2.ToString();

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos3.DataSource = objCentroCostosBC.ListarCentroCostos(objCajaChicaBE.IdEmpresa, 3);
        ddlCentroCostos3.DataTextField = "Descripcion";
        ddlCentroCostos3.DataValueField = "IdCentroCostos";
        ddlCentroCostos3.DataBind();
        ddlCentroCostos3.Enabled = true;
        ddlCentroCostos3.SelectedValue = objCajaChicaBE.IdCentroCostos3.ToString();

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos4.DataSource = objCentroCostosBC.ListarCentroCostos(objCajaChicaBE.IdEmpresa, 4);
        ddlCentroCostos4.DataTextField = "Descripcion";
        ddlCentroCostos4.DataValueField = "IdCentroCostos";
        ddlCentroCostos4.DataBind();
        ddlCentroCostos4.Enabled = true;
        ddlCentroCostos4.SelectedValue = objCajaChicaBE.IdCentroCostos4.ToString();

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(objCajaChicaBE.IdEmpresa, 5);
        ddlCentroCostos5.DataTextField = "Descripcion";
        ddlCentroCostos5.DataValueField = "IdCentroCostos";
        ddlCentroCostos5.DataBind();
        ddlCentroCostos5.Enabled = true;
        ddlCentroCostos5.SelectedValue = objCajaChicaBE.IdCentroCostos5.ToString();

        MetodoPagoBC objMetodoPagoBC = new MetodoPagoBC();
        ddlIdMetodoPago.DataSource = objMetodoPagoBC.ListarMetodoPago(objCajaChicaBE.IdEmpresa, 1, 0);
        ddlIdMetodoPago.DataTextField = "Descripcion";
        ddlIdMetodoPago.DataValueField = "IdMetodoPago";
        ddlIdMetodoPago.DataBind();
        ddlIdMetodoPago.Enabled = true;
        ddlIdMetodoPago.SelectedValue = objCajaChicaBE.IdMetodoPago.ToString();

        //ddlIdArea.SelectedValue = objCajaChicaBE.IdArea.ToString();
        txtAsunto.Text = objCajaChicaBE.Asunto;
        ddlMoneda.SelectedValue = objCajaChicaBE.Moneda.ToString();
        txtMontoInicial.Text = objCajaChicaBE.MontoInicial;
        txtComentario.Text = objCajaChicaBE.Comentario;
        txtMotivoDetalle.Text = objCajaChicaBE.MotivoDetalle;

    }

    protected void Crear_Click(object sender, EventArgs e)
    {
        int Id;
        try
        {
            /*---------------------------------------VALIDA CAMPOS REQUERIDOS------------------------------------------------*/
            Int32[] indexNoValidos = { 0, -1 };
            String errorMessage = null;
            if (indexNoValidos.Contains(ddlIdUsuarioSolicitante.SelectedIndex))
                errorMessage = "Debe ingresar el usuario solicitante";
            else if (indexNoValidos.Contains(ddlIdEmpresa.SelectedIndex))
                errorMessage = "Debe ingresar la empresa";
            else if (indexNoValidos.Contains(ddlMoneda.SelectedIndex))
                errorMessage = "Debe ingresar la  moneda";
            else if (String.IsNullOrWhiteSpace(txtMontoInicial.Text))
                errorMessage = "Debe ingresar el monto inicial";
            else if (indexNoValidos.Contains(ddlCentroCostos1.SelectedIndex))
                errorMessage = "Debe ingresar el centro de costo nivel 1";
            else if (String.IsNullOrWhiteSpace(txtAsunto.Text))
                errorMessage = "Debe ingresar el asunto.";
            else if (String.IsNullOrWhiteSpace(txtMotivoDetalle.Text))
                errorMessage = "Debe ingresar el motivo";

            if (!String.IsNullOrEmpty(errorMessage))
            {
                Mensaje(errorMessage);
                return;
            }
            /*-------------------------------------FIN VALIDA CAMPOS REQUERIDOS----------------------------------------------*/
            else
            {

            }

            bool validacion = true;
            string mensajeAlerta = "";

            if (validacion)
            {
                decimal n;
                bool isNumeric = decimal.TryParse(txtMontoInicial.Text, out n);
                if (isNumeric == false)
                {
                    validacion = false;
                    mensajeAlerta = " El monto inicial no es un numero";
                }
            }

            if (validacion)
            {
                validacion = validaDecimales(txtMontoInicial.Text);
                if (validacion == false)
                    mensajeAlerta = "Los importes deben tener solo 2 decimales";
            }


            //INI: VALIDACION CANTIDAD MAXIMA CAJA CHICA
            CajaChicaBC objCajaChicaBC = new CajaChicaBC();
            List<CajaChicaBE> lstCajaChicaBE = new List<CajaChicaBE>();
            UsuarioBC objUsuarioBC = new UsuarioBC();
            UsuarioBE objUsuarioBE = new UsuarioBE();
            if (validacion)
            {
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value), 0);

                lstCajaChicaBE = objCajaChicaBC.ListarCajaChica(Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value), 2, 10, "", "", "", "", "");
                if (lstCajaChicaBE.Count >= Convert.ToInt32(objUsuarioBE.CantMaxCC))
                {
                    validacion = false;
                    mensajeAlerta = "No es posible solicitar mas Cajas Chicas porque se llego al maximo permitido.";
                }
            }
            //FIN: VALIDACION CANTIDAD MAXIMA CAJA CHICA

            if (validacion)
            {
                CajaChicaBE objCajaChicaBE = new CajaChicaBE();
                objCajaChicaBE.CodigoCajaChica = "";
                objCajaChicaBE.IdUsuarioSolicitante = Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value);
                objCajaChicaBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
                objCajaChicaBE.IdCentroCostos1 = Convert.ToInt32(ddlCentroCostos1.SelectedItem.Value);
                objCajaChicaBE.IdCentroCostos2 = Convert.ToInt32(ddlCentroCostos2.SelectedItem.Value);
                objCajaChicaBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objCajaChicaBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objCajaChicaBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objCajaChicaBE.IdMetodoPago = Convert.ToInt32(ddlIdMetodoPago.SelectedItem.Value);
                objCajaChicaBE.IdArea = 0; //Convert.ToInt32(ddlIdArea.SelectedItem.Value);
                objCajaChicaBE.Asunto = txtAsunto.Text;
                objCajaChicaBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objCajaChicaBE.MontoGastado = "0.00";
                //objCajaChicaBE.MontoReembolsado = "0.000000";
                objCajaChicaBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objCajaChicaBE.Moneda = ddlMoneda.SelectedItem.Value;
                objCajaChicaBE.Comentario = "";
                objCajaChicaBE.MotivoDetalle = txtMotivoDetalle.Text;
                objCajaChicaBE.FechaSolicitud = DateTime.Now;
                objCajaChicaBE.FechaContabilizacion = DateTime.Now;
                objCajaChicaBE.Estado = "1";

                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    objUsuarioBE = (UsuarioBE)Session["Usuario"];
                    objCajaChicaBE.IdUsuarioCreador = objUsuarioBE.IdUsuario;
                    objCajaChicaBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objCajaChicaBE.CreateDate = DateTime.Now;
                    objCajaChicaBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objCajaChicaBE.UpdateDate = DateTime.Now;
                }

                int Modo = Convert.ToInt32(ViewState["Modo"].ToString());
                int IdCajaChica = Convert.ToInt32(ViewState["IdCajaChica"].ToString());
                if (Modo == 1)
                {
                    Id = objCajaChicaBC.InsertarCajaChica(objCajaChicaBE);
                    objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Id, 0);
                    EnviarMensajeParaAprobar(Id, "Caja Chica", objCajaChicaBE.MontoGastado, txtAsunto.Text, objCajaChicaBE.CodigoCajaChica, ddlIdUsuarioSolicitante.SelectedItem.Text, "1", objCajaChicaBE.IdEmpresa);
                }
                else
                {
                    objCajaChicaBE.IdCajaChica = IdCajaChica;
                    objCajaChicaBC.ModificarCajaChica(objCajaChicaBE);
                }

                Response.Redirect("CajaChicas.aspx");
            }
            else
            {
                Mensaje(mensajeAlerta);
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
        }
    }

    private bool validaDecimales(string p)
    {
        string[] words = p.Split('.');
        int cantidad = words.Length;
        string decimales = "000";

        if (cantidad == 2) decimales = words[1];

        if (decimales.Length == 2) return true;
        else return false;
    }

    protected void Cancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CajaChicas.aspx");
    }

    protected void Aprobar_Click(object sender, EventArgs e)
    {
        try
        {
            bAprobar.Enabled = false;

            bool validacion = true;

            if (validacion == true)
            {
                String strIdCajaChica = "";
                strIdCajaChica = ViewState["IdCajaChica"].ToString();

                CajaChicaBC objCajaChicaBC = new CajaChicaBC();
                CajaChicaBE objCajaChicaBE = new CajaChicaBE();
                objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);

                objCajaChicaBE.Asunto = txtAsunto.Text;
                objCajaChicaBE.Moneda = ddlMoneda.SelectedItem.Value;
                objCajaChicaBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objCajaChicaBE.MontoGastado = "0.00";
                //objCajaChicaBE.MontoReembolsado = "0.000000";
                objCajaChicaBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objCajaChicaBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
                objCajaChicaBE.IdArea = 0;// Convert.ToInt32(ddlIdArea.SelectedItem.Value);
                objCajaChicaBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objCajaChicaBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objCajaChicaBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objCajaChicaBE.IdMetodoPago = Convert.ToInt32(ddlIdMetodoPago.SelectedItem.Value);
                objCajaChicaBE.Comentario = "";
                objCajaChicaBE.MotivoDetalle = txtMotivoDetalle.Text;
                objCajaChicaBE.FechaSolicitud = DateTime.Now;

                String estado = "";
                if (objCajaChicaBE.Estado == "8" || objCajaChicaBE.Estado == "9" || objCajaChicaBE.Estado == "10")//revisado
                {
                    estado = objCajaChicaBE.Estado;
                    objCajaChicaBE.Estado = Convert.ToString(Convert.ToInt32(objCajaChicaBE.Estado) - 7);
                }
                else
                {
                    NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
                    NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();
                    objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(Convert.ToInt32(strIdCajaChica), 4); //ultimo nivel CC
                    if ((Convert.ToInt32(objCajaChicaBE.Estado) + 1) > Convert.ToInt32(objNivelAprobacionBE.Nivel))
                        objCajaChicaBE.Estado = "4";
                    else
                        objCajaChicaBE.Estado = Convert.ToString(Convert.ToInt32(objCajaChicaBE.Estado) + 1);

                    estado = objCajaChicaBE.Estado;
                }


                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    UsuarioBE objUsuarioBE = new UsuarioBE();
                    objUsuarioBE = (UsuarioBE)Session["Usuario"];
                    objCajaChicaBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objCajaChicaBE.CreateDate = DateTime.Now;
                    objCajaChicaBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objCajaChicaBE.UpdateDate = DateTime.Now;
                }

                objCajaChicaBC.ModificarCajaChica(objCajaChicaBE);
                EnviarMensajeParaAprobar(objCajaChicaBE.IdCajaChica, "Caja Chica", objCajaChicaBE.MontoGastado, txtAsunto.Text, objCajaChicaBE.CodigoCajaChica, ddlIdUsuarioSolicitante.SelectedItem.Text, estado, objCajaChicaBE.IdEmpresa);

                Response.Redirect("~/CajaChicas.aspx");
            }
            else
                Mensaje("Alerta: Es necesario llenar toda la informacion");
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

    protected void ddlIdUsuarioSolicitante_SelectedIndexChanged(object sender, EventArgs e) { }

    protected void ddlIdEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdEmpresa.SelectedValue != "0")
        {
            ddlCentroCostos1.Enabled = true;
            ddlCentroCostos2.Enabled = true;
            ddlCentroCostos3.Enabled = true;
            ddlCentroCostos4.Enabled = true;
            ddlCentroCostos5.Enabled = true;
            ddlIdMetodoPago.Enabled = true;

            CentroCostosBC objCentroCostosBC = new CentroCostosBC();

            ddlCentroCostos1.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlIdEmpresa.SelectedValue), 1);
            ddlCentroCostos1.DataTextField = "Descripcion";
            ddlCentroCostos1.DataValueField = "IdCentroCostos";
            ddlCentroCostos1.DataBind();

            ddlCentroCostos2.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlIdEmpresa.SelectedValue), 2);
            ddlCentroCostos2.DataTextField = "Descripcion";
            ddlCentroCostos2.DataValueField = "IdCentroCostos";
            ddlCentroCostos2.DataBind();

            ddlCentroCostos3.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlIdEmpresa.SelectedValue), 3);
            ddlCentroCostos3.DataTextField = "Descripcion";
            ddlCentroCostos3.DataValueField = "IdCentroCostos";
            ddlCentroCostos3.DataBind();

            ddlCentroCostos4.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlIdEmpresa.SelectedValue), 4);
            ddlCentroCostos4.DataTextField = "Descripcion";
            ddlCentroCostos4.DataValueField = "IdCentroCostos";
            ddlCentroCostos4.DataBind();

            ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlIdEmpresa.SelectedValue), 5);
            ddlCentroCostos5.DataTextField = "Descripcion";
            ddlCentroCostos5.DataValueField = "IdCentroCostos";
            ddlCentroCostos5.DataBind();

            MetodoPagoBC objMetodoPagoBC = new MetodoPagoBC();
            ddlIdMetodoPago.DataSource = objMetodoPagoBC.ListarMetodoPago(Convert.ToInt32(ddlIdEmpresa.SelectedValue), 1, 0);
            ddlIdMetodoPago.DataTextField = "Descripcion";
            ddlIdMetodoPago.DataValueField = "IdMetodoPago";
            ddlIdMetodoPago.DataBind();


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

    protected void ddlCentroCosto3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCentroCostos3.SelectedValue != "0")
        {
            CentroCostosBC objCentroCostosBC = new CentroCostosBC();
            ddlCentroCostos4.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlIdEmpresa.SelectedValue), 3);
            ddlCentroCostos4.DataTextField = "Descripcion";
            ddlCentroCostos4.DataValueField = "IdCentroCostos";
            ddlCentroCostos4.DataBind();

            ddlCentroCostos4.Enabled = true;
        }
        else
        {
            ddlCentroCostos4.SelectedValue = "0";
            ddlCentroCostos4.Enabled = false;
            ddlCentroCostos5.SelectedValue = "0";
            ddlCentroCostos5.Enabled = false;
        }
    }

    protected void ddlCentroCosto4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCentroCostos4.SelectedValue != "0")
        {
            CentroCostosBC objCentroCostosBC = new CentroCostosBC();
            ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlIdEmpresa.SelectedValue), 5);
            ddlCentroCostos5.DataTextField = "Descripcion";
            ddlCentroCostos5.DataValueField = "IdCentroCostos";
            ddlCentroCostos5.DataBind();
            ddlCentroCostos5.Enabled = true;
        }
        else
        {
            ddlCentroCostos5.SelectedValue = "0";
            ddlCentroCostos5.Enabled = false;
        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
    protected void ddlCentroCostos1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlMoneda_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}