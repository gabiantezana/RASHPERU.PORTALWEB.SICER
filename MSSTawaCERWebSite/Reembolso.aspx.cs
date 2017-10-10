using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;
using System.IO;
using MssTawaCer.App_Code.Helper;
//using System.Net.Mail;

public partial class Reembolso : System.Web.UI.Page
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
            String strIdReembolso = "";

            if (!this.IsPostBack)
            {
                strModo = Context.Items["Modo"].ToString();
                strIdReembolso = Context.Items["IdReembolso"].ToString();

                ViewState["Modo"] = strModo;
                ViewState["IdReembolso"] = strIdReembolso;

                ListarUsuarioSolicitante(Convert.ToInt32(strModo), Convert.ToInt32(strIdReembolso));
                ListarMoneda();
                ListarEmpresa();
                ListarCentroCostos();
                ListarMotivo();
                ModalidadCampo(Convert.ToInt32(strModo), Convert.ToInt32(strIdReembolso));
                Modalidad(Convert.ToInt32(strModo));
            }
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (Reembolso): " + ex.Message);
        }
    }

    private void ListarUsuarioSolicitante(int Modo, int IdReembolso)
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
                ReembolsoBC objReembolsoBC = new ReembolsoBC();
                ReembolsoBE objReembolsoBE = new ReembolsoBE();
                objReembolsoBE = objReembolsoBC.ObtenerReembolso(IdReembolso, 0);

                lstUsuarioBE = objUsuarioBC.ListarUsuario(1, objReembolsoBE.IdUsuarioCreador, 0);
            }

            ddlIdUsuarioSolicitante.DataSource = lstUsuarioBE;
            ddlIdUsuarioSolicitante.DataTextField = "CardName";
            ddlIdUsuarioSolicitante.DataValueField = "IdUsuario";
            ddlIdUsuarioSolicitante.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (Reembolso): " + ex.Message);
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
            Mensaje("Ocurrió un error (Reembolso): " + ex.Message);
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
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (Reembolso): " + ex.Message);
        }
    }

    private void ListarCentroCostos()
    {
    }

    private void ListarMotivo()
    {
        try
        {
            MotivoBC objMotivoBC = new MotivoBC();
            ddlIdMotivo.DataSource = objMotivoBC.ListarMotivo(0, 0);
            ddlIdMotivo.DataTextField = "Descripcion";
            ddlIdMotivo.DataValueField = "IdMotivo";
            ddlIdMotivo.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
        }
    }

    private void Modalidad(int p)
    {
        try
        {
            switch (p)
            {
                case 1:
                    lblCabezera.Text = "Crear Nuevo Reembolso";
                    LimpiarCampos();
                    break;
                case 2:
                    lblCabezera.Text = "Reembolso";
                    bCrear.Text = "Guardar";
                    LlenarCampos(Convert.ToInt32(ViewState["IdReembolso"].ToString()));
                    break;
            }
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (Reembolso): " + ex.Message);
        }
    }

    private void ModalidadCampo(int Modo, int IdReembolso)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        else
        {
            //UsuarioAreaNivelBC objUsuarioAreaNivelBC = new UsuarioAreaNivelBC();
            //UsuarioAreaNivelBE objUsuarioAreaNivelBE = new UsuarioAreaNivelBE();
            //objUsuarioAreaNivelBE = objUsuarioAreaNivelBC.ObtenerUsuarioAreaNivel(objUsuarioBE.IdUsuario, 3, IdReembolso);

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

            //txtComentario.Enabled = false;
            //ddlCentroCostos5.Enabled = false;      

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
                bCrear.Text = "Rendir Rembolso";
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

                ReembolsoBC objReembolsoBC = new ReembolsoBC();
                ReembolsoBE objReembolsoBE = new ReembolsoBE();
                objReembolsoBE = objReembolsoBC.ObtenerReembolso(IdReembolso, 0);
                objUsuarioSolicitanteBE = objUsuarioBC.ObtenerUsuario(objReembolsoBE.IdUsuarioSolicitante, 0);

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
                if (objReembolsoBE.Estado == "1")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioRE1 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objReembolsoBE.Estado == "2")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioRE2 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objReembolsoBE.Estado == "3")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioRE3 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //if (objReembolsoBE.Estado == "4" || objReembolsoBE.Estado == "5") bCancelar2.Visible = true;
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objReembolsoBE.Estado == "8")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioRE1 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                        if (objUsuarioSolicitanteBE.IdUsuario == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objReembolsoBE.Estado == "9")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioRE1 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objReembolsoBE.Estado == "10")
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioRE2 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
            }
        }
    }

    private void LimpiarCampos()
    {
        txtIdReembolso.Text = "";
        txtCodigoReembolso.Text = "";
        txtAsunto.Text = "";
        txtMontoInicial.Text = "";
        txtComentario.Text = "";
    }

    private void LlenarCampos(int p)
    {
        ReembolsoBC objReembolsoBC = new ReembolsoBC();
        ReembolsoBE objReembolsoBE = new ReembolsoBE();
        objReembolsoBE = objReembolsoBC.ObtenerReembolso(p, 0);

        txtIdReembolso.Text = objReembolsoBE.IdReembolso.ToString();
        txtCodigoReembolso.Text = objReembolsoBE.CodigoReembolso;
        ddlIdUsuarioSolicitante.SelectedValue = objReembolsoBE.IdUsuarioSolicitante.ToString();
        ddlIdEmpresa.SelectedValue = objReembolsoBE.IdEmpresa.ToString();

        CentroCostosBC objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos1.DataSource = objCentroCostosBC.ListarCentroCostos(objReembolsoBE.IdEmpresa, 1);
        ddlCentroCostos1.DataTextField = "Descripcion";
        ddlCentroCostos1.DataValueField = "IdCentroCostos";
        ddlCentroCostos1.DataBind();
        ddlCentroCostos1.Enabled = true;
        ddlCentroCostos1.SelectedValue = objReembolsoBE.IdCentroCostos1.ToString();

        ddlCentroCostos2.DataSource = objCentroCostosBC.ListarCentroCostos(objReembolsoBE.IdEmpresa, 2);
        ddlCentroCostos2.DataTextField = "Descripcion";
        ddlCentroCostos2.DataValueField = "IdCentroCostos";
        ddlCentroCostos2.DataBind();
        ddlCentroCostos2.Enabled = true;
        ddlCentroCostos2.SelectedValue = objReembolsoBE.IdCentroCostos2.ToString();

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos3.DataSource = objCentroCostosBC.ListarCentroCostos(objReembolsoBE.IdEmpresa, 3);
        ddlCentroCostos3.DataTextField = "Descripcion";
        ddlCentroCostos3.DataValueField = "IdCentroCostos";
        ddlCentroCostos3.DataBind();
        ddlCentroCostos3.Enabled = true;
        ddlCentroCostos3.SelectedValue = objReembolsoBE.IdCentroCostos3.ToString();

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos4.DataSource = objCentroCostosBC.ListarCentroCostos(objReembolsoBE.IdEmpresa, 4);
        ddlCentroCostos4.DataTextField = "Descripcion";
        ddlCentroCostos4.DataValueField = "IdCentroCostos";
        ddlCentroCostos4.DataBind();
        ddlCentroCostos4.Enabled = true;
        ddlCentroCostos4.SelectedValue = objReembolsoBE.IdCentroCostos4.ToString();

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(objReembolsoBE.IdEmpresa, 5);
        ddlCentroCostos5.DataTextField = "Descripcion";
        ddlCentroCostos5.DataValueField = "IdCentroCostos";
        ddlCentroCostos5.DataBind();
        ddlCentroCostos5.Enabled = true;
        ddlCentroCostos5.SelectedValue = objReembolsoBE.IdCentroCostos5.ToString();

        MetodoPagoBC objMetodoPagoBC = new MetodoPagoBC();
        ddlIdMetodoPago.DataSource = objMetodoPagoBC.ListarMetodoPago(objReembolsoBE.IdEmpresa, 1, 0);
        ddlIdMetodoPago.DataTextField = "Descripcion";
        ddlIdMetodoPago.DataValueField = "IdMetodoPago";
        ddlIdMetodoPago.DataBind();
        ddlIdMetodoPago.Enabled = true;
        ddlIdMetodoPago.SelectedValue = objReembolsoBE.IdMetodoPago.ToString();

        //ddlIdArea.SelectedValue = objReembolsoBE.IdArea.ToString();
        ddlIdMotivo.SelectedValue = objReembolsoBE.IdMotivo.ToString();
        txtAsunto.Text = objReembolsoBE.Asunto;
        ddlMoneda.SelectedValue = objReembolsoBE.Moneda.ToString();
        txtMontoInicial.Text = Convert.ToDouble(objReembolsoBE.MontoInicial).ToString("0.00");
        txtComentario.Text = objReembolsoBE.Comentario;
        txtMotivoDetalle.Text = objReembolsoBE.MotivoDetalle;

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
            bCrear.Enabled = false;

            txtMontoInicial.Text = "0.00";
            bool validacion = true;
            string mensajeAlerta = "";

            if (validacion)
            {
                decimal n;
                bool isNumeric = decimal.TryParse(txtMontoInicial.Text, out n);
                if (isNumeric == false) { validacion = false; mensajeAlerta = " El monto inicial no es un numero"; }
            }

            //INI: VALIDACION CANTIDAD MAXIMA Reembolso
            ReembolsoBC objReembolsoBC = new ReembolsoBC();
            ReembolsoBE objReembolsoBE = new ReembolsoBE();
            List<ReembolsoBE> lstReembolsoBE = new List<ReembolsoBE>();
            UsuarioBC objUsuarioBC = new UsuarioBC();
            UsuarioBE objUsuarioBE = new UsuarioBE();
            if (validacion)
            {
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value), 0);

                if (objUsuarioBE.Estado != "4")
                {
                    lstReembolsoBE = objReembolsoBC.ListarReembolso(Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value), 2, 10, "", "", "", "", "");
                    if (lstReembolsoBE.Count >= Convert.ToInt32(objUsuarioBE.CantMaxRE))
                    {
                        validacion = false;
                        mensajeAlerta = "No es posible solicitar mas Reembolsos porque se llego al maximo permitido.";
                    }
                    //FIN: VALIDACION CANTIDAD MAXIMA Reembolso

                    //INI: VALIDACION FECHA RENDICION Reembolso
                    //DateTime fechaActual = DateTime.Now;
                    //objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value), 1);
                    //if (objReembolsoBE != null)
                    //{
                    //    validacion = false;
                    //    mensajeAlerta = "Usted paso los dias maximos para rendir un Reembolso.";
                    //}
                    //FIN: VALIDACION FECHA RENDICION Reembolso
                }
            }

            if (validacion)
            {
                objReembolsoBE = new ReembolsoBE();
                objReembolsoBE.CodigoReembolso = "";
                objReembolsoBE.IdUsuarioSolicitante = Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value);
                objReembolsoBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
                objReembolsoBE.IdCentroCostos1 = Convert.ToInt32(ddlCentroCostos1.SelectedItem.Value);
                objReembolsoBE.IdCentroCostos2 = 0;// Convert.ToInt32(ddlCentroCostos2.SelectedItem.Value);
                objReembolsoBE.IdCentroCostos3 = 0;// Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objReembolsoBE.IdCentroCostos4 = 0;// Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objReembolsoBE.IdCentroCostos5 = 0;// Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objReembolsoBE.IdMotivo = Convert.ToInt32(ddlIdMotivo.SelectedItem.Value);
                objReembolsoBE.IdMetodoPago = Convert.ToInt32(ddlIdMetodoPago.SelectedItem.Value);
                objReembolsoBE.IdArea = 0;//Convert.ToInt32(ddlIdArea.SelectedItem.Value);
                objReembolsoBE.Asunto = txtAsunto.Text;
                objReembolsoBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objReembolsoBE.MontoGastado = "0.00";
                objReembolsoBE.MontoReembolsado = "0.00";
                objReembolsoBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objReembolsoBE.Moneda = ddlMoneda.SelectedItem.Value;
                objReembolsoBE.Comentario = "";
                objReembolsoBE.MotivoDetalle = txtMotivoDetalle.Text;
                objReembolsoBE.FechaSolicitud = DateTime.Now;
                objReembolsoBE.FechaContabilizacion = DateTime.Now;
                objReembolsoBE.Estado = "4";

                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    objUsuarioBE = (UsuarioBE)Session["Usuario"];
                    objReembolsoBE.IdUsuarioCreador = objUsuarioBE.IdUsuario;
                    objReembolsoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objReembolsoBE.CreateDate = DateTime.Now;
                    objReembolsoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objReembolsoBE.UpdateDate = DateTime.Now;
                }

                int Modo = Convert.ToInt32(ViewState["Modo"].ToString());
                int IdReembolso = Convert.ToInt32(ViewState["IdReembolso"].ToString());
                if (Modo == 1)
                {
                    Id = objReembolsoBC.InsertarReembolso(objReembolsoBE);
                    objReembolsoBE = objReembolsoBC.ObtenerReembolso(Id, 0);
                    //EnviarMensajeParaAprobar(Id, "Reembolso", txtAsunto.Text, objReembolsoBE.CodigoReembolso, ddlIdUsuarioSolicitante.SelectedItem.Text, "1");

                    Context.Items.Add("Modo", 1);
                    Context.Items.Add("IdReembolso", Id);
                    Server.Transfer("~/RendirReembolso.aspx");
                }
                else
                {
                    objReembolsoBE.IdReembolso = IdReembolso;
                    objReembolsoBC.ModificarReembolso(objReembolsoBE);
                }

                //Response.Redirect("Reembolsos.aspx");
            }
            else
            {
                Mensaje(mensajeAlerta);
            }
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (Reembolso): " + ex.Message);
        }
        finally
        {
            bCrear.Enabled = true;
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
        Response.Redirect("~/Reembolsos.aspx");
    }

    protected void Aprobar_Click(object sender, EventArgs e)
    {
        try
        {
            bool validacion = true;

            if (validacion == true)
            {
                String strIdReembolso = "";
                strIdReembolso = ViewState["IdReembolso"].ToString();

                ReembolsoBC objReembolsoBC = new ReembolsoBC();
                ReembolsoBE objReembolsoBE = new ReembolsoBE();
                objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);

                objReembolsoBE.Asunto = txtAsunto.Text;
                objReembolsoBE.Moneda = ddlMoneda.SelectedItem.Value;
                objReembolsoBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objReembolsoBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objReembolsoBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
                objReembolsoBE.IdArea = 0;//Convert.ToInt32(ddlIdArea.SelectedItem.Value);
                objReembolsoBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objReembolsoBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objReembolsoBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objReembolsoBE.IdMotivo = Convert.ToInt32(ddlIdMotivo.SelectedItem.Value);
                objReembolsoBE.IdMetodoPago = Convert.ToInt32(ddlIdMetodoPago.SelectedItem.Value);
                objReembolsoBE.Comentario = "";
                objReembolsoBE.MotivoDetalle = txtMotivoDetalle.Text;

                String estado = "";
                if (objReembolsoBE.Estado == "8" || objReembolsoBE.Estado == "9" || objReembolsoBE.Estado == "10")//revisado
                {
                    estado = objReembolsoBE.Estado;
                    objReembolsoBE.Estado = Convert.ToString(Convert.ToInt32(objReembolsoBE.Estado) - 7);
                }
                else
                {
                    NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
                    NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();

                    objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(objReembolsoBE.IdReembolso, 5); //ultimo nivel ER
                    if ((Convert.ToInt32(objReembolsoBE.Estado) + 1) > Convert.ToInt32(objNivelAprobacionBE.Nivel))
                        objReembolsoBE.Estado = "4";
                    else
                        objReembolsoBE.Estado = Convert.ToString(Convert.ToInt32(objReembolsoBE.Estado) + 1);

                    estado = objReembolsoBE.Estado;
                }



                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    UsuarioBE objUsuarioBE = new UsuarioBE();
                    objUsuarioBE = (UsuarioBE)Session["Usuario"];
                    objReembolsoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objReembolsoBE.CreateDate = DateTime.Now;
                    objReembolsoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objReembolsoBE.UpdateDate = DateTime.Now;
                }

                objReembolsoBC.ModificarReembolso(objReembolsoBE);
                EnviarMensajeParaAprobar(objReembolsoBE.IdReembolso, "Reembolso", objReembolsoBE.MontoInicial, txtAsunto.Text, objReembolsoBE.CodigoReembolso, ddlIdUsuarioSolicitante.SelectedItem.Text, estado, objReembolsoBE.IdEmpresa);

                Response.Redirect("~/Reembolsos.aspx");
            }
            else
                Mensaje("Alerta: Es necesario llenar toda la informacion");
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (Reembolso): " + ex.Message);
        }
    }

    private void EnviarMensajeParaAprobar(int IdReembolso, string Documento, String Monto, string Asunto, string CodigoReembolso, string UsuarioSolicitante, string estado, int IdEmpresa)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        if (estado == "1" || estado == "2" || estado == "3")
            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdReembolso, Convert.ToInt32(estado));
        else
        {
            if (estado == "7" || estado == "8" || estado == "9")
                lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdReembolso, Convert.ToInt32(estado) - 7);
            else //4
            {
                ReembolsoBE objReembolsoBE = new ReembolsoBE();
                ReembolsoBC objReembolsoBC = new ReembolsoBC();
                objReembolsoBE = objReembolsoBC.ObtenerReembolso(IdReembolso, 0);
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(objReembolsoBE.IdUsuarioSolicitante, 0);
            }
        }

        if (estado != "4")
        {
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + UsuarioSolicitante + " a solicitado la aprobacion de un " + Documento + "Codigo: " + CodigoReembolso, "Aprobacion Reembolso: " + Asunto, lstUsuarioBE[i].Mail);
            }
        }
        else
        {
            EmpresaBC objEmpresaBC = new EmpresaBC();
            EmpresaBE objEmpresaBE = new EmpresaBE();
            objEmpresaBE = objEmpresaBC.ObtenerEmpresa(IdEmpresa);
            MensajeEmail("La " + Documento + " Codigo: " + CodigoReembolso + " fue Aprobada", "Aprobacion Reembolso: " + Asunto, objUsuarioBE.Mail);

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
                MensajeEmail("La " + Documento + " con Codigo: " + CodigoReembolso + " , " + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>" + "<br/>"
                + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                + "Importe a Pagar :" + moneda + Monto + "<br/>"
                + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                , "Reembolso" + CodigoReembolso, lstUsuarioTesoreriaBE[x].Mail.ToString());
            }

            if (objUsuarioBE.Mail.Trim() != "")
            {
                MensajeEmail("La " + Documento + " con Codigo: " + CodigoReembolso + " , " + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>" + "<br/>"
               + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
               + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
               + "Importe a Pagar :" + moneda + Monto + "<br/>"
               + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
               , "Caja Chica " + CodigoReembolso, objUsuarioBE.Mail.ToString());
            }


        }
    }

    protected void Observacion_Click(object sender, EventArgs e)
    {
        try
        {
            String strIdReembolso = "";
            strIdReembolso = ViewState["IdReembolso"].ToString();
            String estado = "";

            UsuarioBE objUsuarioBE = new UsuarioBE();
            ReembolsoBC objReembolsoBC = new ReembolsoBC();
            ReembolsoBE objReembolsoBE = new ReembolsoBE();
            objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);

            objReembolsoBE.Asunto = txtAsunto.Text;
            objReembolsoBE.Moneda = ddlMoneda.SelectedItem.Value;
            objReembolsoBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.000000");
            objReembolsoBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.000000");
            objReembolsoBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
            objReembolsoBE.IdArea = 0;//Convert.ToInt32(ddlIdArea.SelectedItem.Value);
            objReembolsoBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
            objReembolsoBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
            objReembolsoBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
            objReembolsoBE.IdMotivo = Convert.ToInt32(ddlIdMotivo.SelectedItem.Value);

            estado = objReembolsoBE.Estado;
            if (Convert.ToInt32(estado) > 3)
            {
                estado = Convert.ToString(Convert.ToInt32(estado) - 7 - 1);
                objReembolsoBE.Estado = Convert.ToString(Convert.ToInt32(objReembolsoBE.Estado) - 1);
            }
            else
                objReembolsoBE.Estado = Convert.ToString(Convert.ToInt32(objReembolsoBE.Estado) + 7);

            objReembolsoBE.Comentario = txtComentario.Text;
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                objReembolsoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                objReembolsoBE.CreateDate = DateTime.Now;
                objReembolsoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                objReembolsoBE.UpdateDate = DateTime.Now;
            }

            objReembolsoBC.ModificarReembolso(objReembolsoBE);
            EnviarMensajeObservacion(objUsuarioBE.IdUsuario, objReembolsoBE.IdReembolso, objReembolsoBE.IdUsuarioSolicitante, "Reembolso", txtAsunto.Text, objReembolsoBE.CodigoReembolso, objUsuarioBE.CardName, estado);

            Response.Redirect("~/Reembolsos.aspx");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Reembolsos.aspx");
            Mensaje("Ocurrió un error (Reembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    private void EnviarMensajeObservacion(int IdUsuarioAprobador, int IdReembolso, int IdUsuarioSolicitante, string Documento, string Asunto, string CodigoReembolso, string UsuarioAprobador, string estado)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        UsuarioBE objUsuarioBE2 = new UsuarioBE();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();

        objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioAprobador, 0);
        //objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioCreador);
        //MensajeGmail(objUsuarioBE.CardName + " a colocado una Observacion en la aprobacion de un ", Documento, "Observacion Solicitud de Reembolso: " + Asunto, CodigoReembolso, objUsuarioBE2.Mail);
        if (estado == "1")
        {
            objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);
            MensajeEmail("El usuario " + objUsuarioBE.CardName + " a colocado una Observacion en la aprobacion de un " + Documento + "Codigo: " + CodigoReembolso, "Aprobacion Reembolso: " + Asunto, objUsuarioBE2.Mail);
            //MensajeEmail(objUsuarioBE.CardName + " a colocado una Observacion en la aprobacion de un ", Documento, "Observacion Solicitud de Reembolso: " + Asunto, CodigoReembolso, objUsuarioBE2.Mail);
        }
        if (estado == "2")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdReembolso, 1);

            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " a colocado una Observacion en la aprobacion de un " + Documento + "Codigo: " + CodigoReembolso, "Aprobacion Reembolso: " + Asunto, lstUsuarioBE[i].Mail);
                //MensajeEmail(objUsuarioBE.CardName + " a colocado una Observacion en la aprobacion de un ", Documento, "Observacion Solicitud de Reembolso: " + Asunto, CodigoReembolso, objUsuarioBE2.Mail);
            }
        }
        if (estado == "3")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdReembolso, 2);

            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " a colocado una Observacion en la aprobacion de un " + Documento + "Codigo: " + CodigoReembolso, "Aprobacion Reembolso: " + Asunto, lstUsuarioBE[i].Mail);
                //MensajeEmail(objUsuarioBE.CardName + " a colocado una Observacion en la aprobacion de un ", Documento, "Observacion Solicitud de Reembolso: " + Asunto, CodigoReembolso, objUsuarioBE2.Mail);
            }
        }

    }

    protected void Rechazar_Click(object sender, EventArgs e)
    {
        try
        {
            String strIdReembolso = "";
            strIdReembolso = ViewState["IdReembolso"].ToString();

            UsuarioBE objUsuarioBE = new UsuarioBE();
            ReembolsoBC objReembolsoBC = new ReembolsoBC();
            ReembolsoBE objReembolsoBE = new ReembolsoBE();
            objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);

            objReembolsoBE.Estado = "5";
            objReembolsoBE.Comentario = txtComentario.Text;
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                objReembolsoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                objReembolsoBE.CreateDate = DateTime.Now;
                objReembolsoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                objReembolsoBE.UpdateDate = DateTime.Now;
            }

            objReembolsoBC.ModificarReembolso(objReembolsoBE);
            EnviarMensajeRechazado(objUsuarioBE.IdUsuario, objReembolsoBE.IdUsuarioCreador, objReembolsoBE.IdUsuarioSolicitante, "Reembolso", txtAsunto.Text, objReembolsoBE.CodigoReembolso, objUsuarioBE.CardName);

            Response.Redirect("~/Reembolsos.aspx");
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (Reembolso): " + ex.Message);
        }
    }

    private void EnviarMensajeRechazado(int IdUsuarioAprobador, int IdUsuarioCreador, int IdUsuarioSolicitante, string Documento, string Asunto, string CodigoReembolso, string UsuarioAprobador)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        UsuarioBE objUsuarioBE2 = new UsuarioBE();

        objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioAprobador, 0);
        objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioCreador, 0);
        MensajeEmail("El usuario " + objUsuarioBE.CardName + " a Rechazado la aprobacion de un " + Documento + "Codigo: " + CodigoReembolso, "Aprobacion Reembolso: " + Asunto, objUsuarioBE2.Mail);
        //MensajeEmail(objUsuarioBE.CardName + " a Rechazado la aprobacion de un ", Documento, "Rechazo Solicitud de Reembolso: " + Asunto, CodigoReembolso, objUsuarioBE2.Mail);
        objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);
        MensajeEmail("El usuario " + objUsuarioBE.CardName + " a Rechazado la aprobacion de un " + Documento + "Codigo: " + CodigoReembolso, "Aprobacion Reembolso: " + Asunto, objUsuarioBE2.Mail);
        //MensajeEmail(objUsuarioBE.CardName + " a Rechazado la aprobacion de un ", Documento, "Rechazo Solicitud de Reembolso: " + Asunto, CodigoReembolso, objUsuarioBE2.Mail);
    }

    protected void ddlIdUsuarioSolicitante_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdUsuarioSolicitante.SelectedValue != "0" && ddlIdEmpresa.SelectedValue != "0")
        {
            CentroCostosBC objCentroCostosBC = new CentroCostosBC();
            ddlCentroCostos3.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlIdEmpresa.SelectedValue), 3);
            ddlCentroCostos3.DataTextField = "Descripcion";
            ddlCentroCostos3.DataValueField = "IdCentroCostos";
            ddlCentroCostos3.DataBind();
            ddlCentroCostos3.Enabled = true;
        }
        else
        {
            ddlCentroCostos3.SelectedValue = "0";
            ddlCentroCostos3.Enabled = false;
            ddlCentroCostos4.SelectedValue = "0";
            ddlCentroCostos4.Enabled = false;
            ddlCentroCostos5.SelectedValue = "0";
            ddlCentroCostos5.Enabled = false;
        }
    }

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
            ddlCentroCostos4.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlIdEmpresa.SelectedValue), 4);
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
            ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlIdEmpresa.SelectedValue),5 );
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

    private void MensajeEmail(string Cuerpo, string Asunto, string Destino)//string UsuarioSolicitante, string Documento, string Asunto, string CodigoReembolso, string Destino)
    {
        if (Destino.Trim() != "")
        {
            //Public Shared Sub EnviarCorreoMemoria(ByVal Usuario As String, ByVal Email As String, ByVal xSubject As String, ByVal Mensaje As String, ByVal ReportName As String, ByVal Xbyte As Byte())

            //String concepto = "";
            //String texto = "Se adjunta la lista de Ordenes de Facturación pendientes por Aprobar.";
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            String email_body = "";
            //MemoryStream sMemoryStream = new MemoryStream(Xbyte);
            //sMemoryStream.Position = 0
            //Dim attach As System.Net.Mail.Attachment = New System.Net.Mail.Attachment(sMemoryStream, ReportName)
            correo.From = new System.Net.Mail.MailAddress("procesos.peru@tawa.com.pe");
            correo.To.Add(Destino.Trim());
            correo.Subject = Asunto;
            //correo.Attachments.Add(attach)
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
                //sMemoryStream.Close();
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Mensaje("Ocurrió un error (Reembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
                //sMemoryStream.Close();
            }
        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
}