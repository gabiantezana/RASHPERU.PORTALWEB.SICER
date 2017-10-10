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

public partial class EntregaRendir : System.Web.UI.Page
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
            String strIdEntregaRendir = "";

            if (!this.IsPostBack)
            {
                strModo = Context.Items["Modo"].ToString();
                strIdEntregaRendir = Context.Items["IdEntregaRendir"].ToString();

                ViewState["Modo"] = strModo;
                ViewState["IdEntregaRendir"] = strIdEntregaRendir;

                ListarUsuarioSolicitante(Convert.ToInt32(strModo), Convert.ToInt32(strIdEntregaRendir));
                ListarMoneda();
                ListarEmpresa();
                ListarCentroCostos();
                ListarMotivo();
                ModalidadCampo(Convert.ToInt32(strModo), Convert.ToInt32(strIdEntregaRendir));
                Modalidad(Convert.ToInt32(strModo));
            }
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (EntregaRendir): " + ex.Message);
        }
    }

    private void ListarUsuarioSolicitante(int Modo, int IdEntregaRendir)
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
                EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
                EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
                objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(IdEntregaRendir, 0);

                lstUsuarioBE = objUsuarioBC.ListarUsuario(1, objEntregaRendirBE.IdUsuarioCreador, 0);
            }

            ddlIdUsuarioSolicitante.DataSource = lstUsuarioBE;
            ddlIdUsuarioSolicitante.DataTextField = "CardName";
            ddlIdUsuarioSolicitante.DataValueField = "IdUsuario";
            ddlIdUsuarioSolicitante.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (EntregaRendir): " + ex.Message);
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
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (EntregaRendir): " + ex.Message);
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
            Mensaje("Ocurrió un error (EntregaRendir): " + ex.Message);
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
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
        }
    }

    private void Modalidad(int p)
    {
        try
        {
            switch (p)
            {
                case 1:
                    lblCabezera.Text = "Crear Nueva Entrega Rendir";
                    LimpiarCampos();
                    break;
                case 2:
                    lblCabezera.Text = "Entrega Rendir";
                    bCrear.Text = "Guardar";
                    LlenarCampos(Convert.ToInt32(ViewState["IdEntregaRendir"].ToString()));
                    break;
            }
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (EntregaRendir): " + ex.Message);
        }
    }

    private void ModalidadCampo(int Modo, int IdEntregaRendir)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        else
        {
            //UsuarioAreaNivelBC objUsuarioAreaNivelBC = new UsuarioAreaNivelBC();
            //UsuarioAreaNivelBE objUsuarioAreaNivelBE = new UsuarioAreaNivelBE();
            //objUsuarioAreaNivelBE = objUsuarioAreaNivelBC.ObtenerUsuarioAreaNivel(objUsuarioBE.IdUsuario, 2, IdEntregaRendir);

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

                EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
                EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
                objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(IdEntregaRendir, 0);
                objUsuarioSolicitanteBE = objUsuarioBC.ObtenerUsuario(objEntregaRendirBE.IdUsuarioSolicitante, 0);

                bCrear.Visible = false;
                bCancelar.Visible = false;
                bAprobar.Visible = false;
                bObservacion.Visible = false;
                bRechazar.Visible = false;
                bCancelar2.Visible = true;
                txtComentario.Enabled = true;
                //1: Por Aprobar Nivel 1 //2: Por Aprobar Nivel 2 //3: Por Aprobar Nivel 3 //4: Aprobado 
                //5: Rechazado //8: Observaciones Nivel 1 //9: Observaciones Nivel 2 //10: Observaciones Nivel 3

                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objEntregaRendirBE.Estado == "1")//1: Por Aprobar Nivel 1
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioER1 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objEntregaRendirBE.Estado == "2")//2: Por Aprobar Nivel 2
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioER2 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objEntregaRendirBE.Estado == "3")//3: Por Aprobar Nivel 3
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                    {
                        if (objUsuarioSolicitanteBE.IdUsuarioER3 == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Visible = true; bObservacion.Visible = true; bRechazar.Visible = true;
                        }
                    }
                }
                //if (objEntregaRendirBE.Estado == "4" || objEntregaRendirBE.Estado == "5") bCancelar2.Visible = true;
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objEntregaRendirBE.Estado == "8")//8: Observaciones Nivel 1
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                    {
                        if (objEntregaRendirBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true;
                        }
                        if (objEntregaRendirBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objEntregaRendirBE.Estado == "9")//8: Observaciones Nivel 2
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                    {
                        if (objEntregaRendirBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true;
                        }
                        if (objEntregaRendirBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true;
                        }
                    }
                }
                //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
                if (objEntregaRendirBE.Estado == "10")//8: Observaciones Nivel 3
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                    {
                        if (objEntregaRendirBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true;
                        }
                        if (objEntregaRendirBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
                        {
                            bAprobar.Text = "Enviar"; bAprobar.Visible = true;
                        }
                    }
                }
            }
        }
    }

    private void LimpiarCampos()
    {
        txtIdEntregaRendir.Text = "";
        txtCodigoEntregaRendir.Text = "";
        txtAsunto.Text = "";
        txtMontoInicial.Text = "";
        txtComentario.Text = "";
    }

    private void LlenarCampos(int p)
    {
        EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
        EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
        objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(p, 0);

        txtIdEntregaRendir.Text = objEntregaRendirBE.IdEntregaRendir.ToString();
        txtCodigoEntregaRendir.Text = objEntregaRendirBE.CodigoEntregaRendir;
        ddlIdUsuarioSolicitante.SelectedValue = objEntregaRendirBE.IdUsuarioSolicitante.ToString();
        ddlIdEmpresa.SelectedValue = objEntregaRendirBE.IdEmpresa.ToString();

        CentroCostosBC objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos1.DataSource = objCentroCostosBC.ListarCentroCostos(objEntregaRendirBE.IdEmpresa, 1);
        ddlCentroCostos1.DataTextField = "Descripcion";
        ddlCentroCostos1.DataValueField = "IdCentroCostos";
        ddlCentroCostos1.DataBind();
        ddlCentroCostos1.Enabled = true;
        ddlCentroCostos1.SelectedValue = objEntregaRendirBE.IdCentroCostos1.ToString();

        ddlCentroCostos2.DataSource = objCentroCostosBC.ListarCentroCostos(objEntregaRendirBE.IdEmpresa, 2);
        ddlCentroCostos2.DataTextField = "Descripcion";
        ddlCentroCostos2.DataValueField = "IdCentroCostos";
        ddlCentroCostos2.DataBind();
        ddlCentroCostos2.Enabled = true;
        ddlCentroCostos2.SelectedValue = objEntregaRendirBE.IdCentroCostos2.ToString();

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos3.DataSource = objCentroCostosBC.ListarCentroCostos(objEntregaRendirBE.IdEmpresa, 3);
        ddlCentroCostos3.DataTextField = "Descripcion";
        ddlCentroCostos3.DataValueField = "IdCentroCostos";
        ddlCentroCostos3.DataBind();
        ddlCentroCostos3.Enabled = true;
        ddlCentroCostos3.SelectedValue = objEntregaRendirBE.IdCentroCostos3.ToString();

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos4.DataSource = objCentroCostosBC.ListarCentroCostos(objEntregaRendirBE.IdEmpresa, 4);
        ddlCentroCostos4.DataTextField = "Descripcion";
        ddlCentroCostos4.DataValueField = "IdCentroCostos";
        ddlCentroCostos4.DataBind();
        ddlCentroCostos4.Enabled = true;
        ddlCentroCostos4.SelectedValue = objEntregaRendirBE.IdCentroCostos4.ToString();

        objCentroCostosBC = new CentroCostosBC();
        ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(objEntregaRendirBE.IdEmpresa, 5);
        ddlCentroCostos5.DataTextField = "Descripcion";
        ddlCentroCostos5.DataValueField = "IdCentroCostos";
        ddlCentroCostos5.DataBind();
        ddlCentroCostos5.Enabled = true;
        ddlCentroCostos5.SelectedValue = objEntregaRendirBE.IdCentroCostos5.ToString();

        MetodoPagoBC objMetodoPagoBC = new MetodoPagoBC();
        ddlIdMetodoPago.DataSource = objMetodoPagoBC.ListarMetodoPago(objEntregaRendirBE.IdEmpresa, 1, 0);
        ddlIdMetodoPago.DataTextField = "Descripcion";
        ddlIdMetodoPago.DataValueField = "IdMetodoPago";
        ddlIdMetodoPago.DataBind();
        ddlIdMetodoPago.Enabled = true;
        ddlIdMetodoPago.SelectedValue = objEntregaRendirBE.IdMetodoPago.ToString();

        //ddlIdArea.SelectedValue = objEntregaRendirBE.IdArea.ToString();
        ddlIdMotivo.SelectedValue = objEntregaRendirBE.IdMotivo.ToString();
        txtAsunto.Text = objEntregaRendirBE.Asunto;
        ddlMoneda.SelectedValue = objEntregaRendirBE.Moneda.ToString();
        txtMontoInicial.Text = Convert.ToDouble(objEntregaRendirBE.MontoInicial).ToString("0.00");
        txtComentario.Text = objEntregaRendirBE.Comentario;
        txtMotivoDetalle.Text = objEntregaRendirBE.MotivoDetalle;

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

            bool validacion = true;
            string mensajeAlerta = "";

            if (validacion)
            {
                decimal n;
                bool isNumeric = decimal.TryParse(txtMontoInicial.Text, out n);
                if (isNumeric == false) { validacion = false; mensajeAlerta = " El monto inicial no es un numero"; }
            }

            if (validacion)
            {
                validacion = validaDecimales(txtMontoInicial.Text);
                if (validacion == false)
                    mensajeAlerta = "Los importes deben tener solo 2 decimales";
            }

            //INI: VALIDACION CANTIDAD MAXIMA Entrega Rendir
            EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
            EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
            List<EntregaRendirBE> lstEntregaRendirBE = new List<EntregaRendirBE>();
            UsuarioBC objUsuarioBC = new UsuarioBC();
            UsuarioBE objUsuarioBE = new UsuarioBE();
            if (validacion)
            {
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value), 0);

                if (objUsuarioBE.Estado != "4")
                {
                    lstEntregaRendirBE = objEntregaRendirBC.ListarEntregaRendir(Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value), 2, 10, "", "", "", "", "");
                    if (lstEntregaRendirBE.Count >= Convert.ToInt32(objUsuarioBE.CantMaxER))
                    {
                        validacion = false;
                        mensajeAlerta = "No es posible solicitar mas Entrega a Rendir porque se llego al maximo permitido.";
                    }
                    //FIN: VALIDACION CANTIDAD MAXIMA Entrega Rendir

                    //INI: VALIDACION FECHA RENDICION Entrega Rendir
                    DateTime fechaActual = DateTime.Now;
                    objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value), 1);
                    if (objEntregaRendirBE != null)
                    {
                        validacion = false;
                        mensajeAlerta = "Usted paso los dias maximos para rendir una Entrega a Rendir";
                    }
                    //FIN: VALIDACION FECHA RENDICION Entrega Rendir
                }
            }

            if (validacion)
            {
                objEntregaRendirBE = new EntregaRendirBE();
                objEntregaRendirBE.CodigoEntregaRendir = "";
                objEntregaRendirBE.IdUsuarioSolicitante = Convert.ToInt32(ddlIdUsuarioSolicitante.SelectedItem.Value);
                objEntregaRendirBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
                objEntregaRendirBE.IdCentroCostos1 = Convert.ToInt32(ddlCentroCostos1.SelectedItem.Value);
                objEntregaRendirBE.IdCentroCostos2 = 0;// Convert.ToInt32(ddlCentroCostos2.SelectedItem.Value);
                objEntregaRendirBE.IdCentroCostos3 = 0;//Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objEntregaRendirBE.IdCentroCostos4 = 0;//Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objEntregaRendirBE.IdCentroCostos5 = 0;//Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objEntregaRendirBE.IdMotivo = Convert.ToInt32(ddlIdMotivo.SelectedItem.Value);
                objEntregaRendirBE.IdMetodoPago = Convert.ToInt32(ddlIdMetodoPago.SelectedItem.Value);
                objEntregaRendirBE.IdArea = 0;//Convert.ToInt32(ddlIdArea.SelectedItem.Value);
                objEntregaRendirBE.Asunto = txtAsunto.Text;
                objEntregaRendirBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objEntregaRendirBE.MontoGastado = "0.00";
                objEntregaRendirBE.MontoReembolsado = "0.00";
                objEntregaRendirBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objEntregaRendirBE.Moneda = ddlMoneda.SelectedItem.Value;
                objEntregaRendirBE.Comentario = "";
                objEntregaRendirBE.MotivoDetalle = txtMotivoDetalle.Text;
                objEntregaRendirBE.FechaSolicitud = DateTime.Now;
                objEntregaRendirBE.FechaContabilizacion = DateTime.Now;
                objEntregaRendirBE.Estado = "1";

                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    objUsuarioBE = (UsuarioBE)Session["Usuario"];
                    objEntregaRendirBE.IdUsuarioCreador = objUsuarioBE.IdUsuario;
                    objEntregaRendirBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirBE.CreateDate = DateTime.Now;
                    objEntregaRendirBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirBE.UpdateDate = DateTime.Now;
                }

                int Modo = Convert.ToInt32(ViewState["Modo"].ToString());
                int IdEntregaRendir = Convert.ToInt32(ViewState["IdEntregaRendir"].ToString());
                if (Modo == 1)
                {
                    Id = objEntregaRendirBC.InsertarEntregaRendir(objEntregaRendirBE);
                    objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Id, 0);
                    EnviarMensajeParaAprobar(Id, "Entrega a Rendir", objEntregaRendirBE.MontoInicial, txtAsunto.Text, objEntregaRendirBE.CodigoEntregaRendir, ddlIdUsuarioSolicitante.SelectedItem.Text, "1", objEntregaRendirBE.IdEmpresa);
                }
                else
                {
                    objEntregaRendirBE.IdEntregaRendir = IdEntregaRendir;
                    objEntregaRendirBC.ModificarEntregaRendir(objEntregaRendirBE);
                }

                Response.Redirect("EntregasRendir.aspx");
            }
            else
            {
                Mensaje(mensajeAlerta);
            }
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (EntregaRendir): " + ex.Message);
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
        Response.Redirect("~/EntregasRendir.aspx");
    }

    protected void Aprobar_Click(object sender, EventArgs e)
    {
        try
        {
            bAprobar.Enabled = false;

            bool validacion = true;

            if (validacion == true)
            {
                String strIdEntregaRendir = "";
                strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

                EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
                EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
                objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);

                objEntregaRendirBE.Asunto = txtAsunto.Text;
                objEntregaRendirBE.Moneda = ddlMoneda.SelectedItem.Value;
                objEntregaRendirBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objEntregaRendirBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.00");
                objEntregaRendirBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
                objEntregaRendirBE.IdArea = 0;//Convert.ToInt32(ddlIdArea.SelectedItem.Value);
                objEntregaRendirBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objEntregaRendirBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objEntregaRendirBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objEntregaRendirBE.IdMotivo = Convert.ToInt32(ddlIdMotivo.SelectedItem.Value);
                objEntregaRendirBE.IdMetodoPago = Convert.ToInt32(ddlIdMetodoPago.SelectedItem.Value);
                objEntregaRendirBE.Comentario = "";
                objEntregaRendirBE.MotivoDetalle = txtMotivoDetalle.Text;

                String estado = "";
                if (objEntregaRendirBE.Estado == "8" || objEntregaRendirBE.Estado == "9" || objEntregaRendirBE.Estado == "10")//revisado
                {
                    estado = objEntregaRendirBE.Estado;
                    objEntregaRendirBE.Estado = Convert.ToString(Convert.ToInt32(objEntregaRendirBE.Estado) - 7);
                }
                else
                {
                    NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
                    NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();

                    objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(objEntregaRendirBE.IdEntregaRendir, 5); //ultimo nivel ER
                    if ((Convert.ToInt32(objEntregaRendirBE.Estado) + 1) > Convert.ToInt32(objNivelAprobacionBE.Nivel))
                        objEntregaRendirBE.Estado = "4";
                    else
                        objEntregaRendirBE.Estado = Convert.ToString(Convert.ToInt32(objEntregaRendirBE.Estado) + 1);

                    estado = objEntregaRendirBE.Estado;
                }



                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    UsuarioBE objUsuarioBE = new UsuarioBE();
                    objUsuarioBE = (UsuarioBE)Session["Usuario"];
                    objEntregaRendirBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirBE.CreateDate = DateTime.Now;
                    objEntregaRendirBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirBE.UpdateDate = DateTime.Now;
                }

                objEntregaRendirBC.ModificarEntregaRendir(objEntregaRendirBE);
                EnviarMensajeParaAprobar(objEntregaRendirBE.IdEntregaRendir, "Entrega a Rendir", objEntregaRendirBE.MontoInicial, txtAsunto.Text, objEntregaRendirBE.CodigoEntregaRendir, ddlIdUsuarioSolicitante.SelectedItem.Text, estado, objEntregaRendirBE.IdEmpresa);

                Response.Redirect("~/EntregasRendir.aspx");
            }
            else
                Mensaje("Alerta: Es necesario llenar toda la informacion");
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (EntregaRendir): " + ex.Message);

        }
        finally
        {
            bAprobar.Enabled = true;
            Response.Redirect("~/EntregasRendir.aspx");
        }
    }

    private void EnviarMensajeParaAprobar(int IdEntregaRendir, string Documento, String Monto, string Asunto, string CodigoEntregaRendir, string UsuarioSolicitante, string estado, int IdEmpresa)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        UsuarioBE objUsuarioBE2 = new UsuarioBE();
        if (estado == "1" || estado == "2" || estado == "3")
            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, Convert.ToInt32(estado));
        else
        {
            if (estado == "8" || estado == "9" || estado == "10")
                lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, Convert.ToInt32(estado) - 7);
            else //4
            {
                EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
                EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
                objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(IdEntregaRendir, 0);
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(objEntregaRendirBE.IdUsuarioSolicitante, 0);
                objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(objEntregaRendirBE.IdUsuarioCreador, 0);
            }
        }

        if (estado != "4")
        {
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + UsuarioSolicitante + " ha solicitado la aprobacion de una " + Documento + " Codigo: " + CodigoEntregaRendir, "Entrega a Rendir " + CodigoEntregaRendir + ": " + Asunto, lstUsuarioBE[i].Mail);
            }
        }
        else
        {

            EmpresaBC objEmpresaBC = new EmpresaBC();
            EmpresaBE objEmpresaBE = new EmpresaBE();
            objEmpresaBE = objEmpresaBC.ObtenerEmpresa(IdEmpresa);

            if (objUsuarioBE.Mail.Trim() != "")
            {
                MensajeEmail("La " + Documento + " Codigo: " + CodigoEntregaRendir + " fue aprobada", "Entrega a Rendir " + CodigoEntregaRendir + ": " + Asunto, objUsuarioBE.Mail);


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
                    MensajeEmail("La " + Documento + " con Codigo: " + CodigoEntregaRendir + " , " + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>" + "<br/>"
                    + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                    + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                    + "Importe a Pagar :" + moneda + Monto + "<br/>"
                    + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                    , "Entrega a Rendir " + CodigoEntregaRendir, lstUsuarioTesoreriaBE[x].Mail.ToString());
                }
                if (objUsuarioBE.Mail.Trim() != "")
                {
                    MensajeEmail("La " + Documento + " con Codigo: " + CodigoEntregaRendir + " , " + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>" + "<br/>"
                   + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                   + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                   + "Importe a Pagar :" + moneda + Monto + "<br/>"
                   + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                   , "Caja Chica " + CodigoEntregaRendir, objUsuarioBE.Mail.ToString());
                }

            }
            else
            {
                MensajeEmail("La " + Documento + " Codigo: " + CodigoEntregaRendir + " fue aprobada", "Entrega a Rendir " + CodigoEntregaRendir + ": " + Asunto, objUsuarioBE2.Mail);

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
                    MensajeEmail("La " + Documento + " con Codigo: " + CodigoEntregaRendir + " , " + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>" + "<br/>"
                    + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                    + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                    + "Importe a Pagar :" + moneda + txtMontoInicial.Text + "<br/>"
                    + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                    , "Entrega a Rendir " + CodigoEntregaRendir, lstUsuarioTesoreriaBE[x].Mail.ToString());
                }
                if (objUsuarioBE.Mail.Trim() != "")
                {
                    MensajeEmail("La " + Documento + " con Codigo: " + CodigoEntregaRendir + " , " + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>" + "<br/>"
                   + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                   + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                   + "Importe a Pagar :" + moneda + txtMontoInicial.Text + "<br/>"
                   + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                   , "Caja Chica " + CodigoEntregaRendir, objUsuarioBE.Mail.ToString());
                }
            }
        }
    }

    protected void Observacion_Click(object sender, EventArgs e)
    {
        try
        {
            bObservacion.Enabled = false;

            String strIdEntregaRendir = "";
            strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();
            String estado = "";

            UsuarioBE objUsuarioBE = new UsuarioBE();
            EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
            EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
            objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);

            objEntregaRendirBE.Asunto = txtAsunto.Text;
            objEntregaRendirBE.Moneda = ddlMoneda.SelectedItem.Value;
            objEntregaRendirBE.MontoInicial = Convert.ToDouble(txtMontoInicial.Text).ToString("0.000000");
            objEntregaRendirBE.MontoActual = Convert.ToDouble(txtMontoInicial.Text).ToString("0.000000");
            objEntregaRendirBE.IdEmpresa = Convert.ToInt32(ddlIdEmpresa.SelectedItem.Value);
            objEntregaRendirBE.IdArea = 0;//Convert.ToInt32(ddlIdArea.SelectedItem.Value);
            objEntregaRendirBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
            objEntregaRendirBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
            objEntregaRendirBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
            objEntregaRendirBE.IdMotivo = Convert.ToInt32(ddlIdMotivo.SelectedItem.Value);

            estado = objEntregaRendirBE.Estado;
            if (Convert.ToInt32(estado) > 3)
            {
                estado = Convert.ToString(Convert.ToInt32(estado) - 7 - 1);
                objEntregaRendirBE.Estado = Convert.ToString(Convert.ToInt32(objEntregaRendirBE.Estado) - 1);
            }
            else
                objEntregaRendirBE.Estado = Convert.ToString(Convert.ToInt32(objEntregaRendirBE.Estado) + 7);

            objEntregaRendirBE.Comentario = txtComentario.Text;
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                objEntregaRendirBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                objEntregaRendirBE.CreateDate = DateTime.Now;
                objEntregaRendirBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                objEntregaRendirBE.UpdateDate = DateTime.Now;
            }

            objEntregaRendirBC.ModificarEntregaRendir(objEntregaRendirBE);
            EnviarMensajeObservacion(objUsuarioBE.IdUsuario, objEntregaRendirBE.IdEntregaRendir, objEntregaRendirBE.IdUsuarioSolicitante, objEntregaRendirBE.IdUsuarioCreador, "Entrega a Rendir", txtAsunto.Text, objEntregaRendirBE.CodigoEntregaRendir, objUsuarioBE.CardName, estado);

            Response.Redirect("~/EntregasRendir.aspx");
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (EntregaRendir): " + ex.Message);
        }
        finally
        {
            Response.Redirect("~/EntregasRendir.aspx");
            bObservacion.Enabled = true;

        }
    }

    private void EnviarMensajeObservacion(int IdUsuarioAprobador, int IdEntregaRendir, int IdUsuarioSolicitante, int IdUsuarioCreador, string Documento, string Asunto, string CodigoEntregaRendir, string UsuarioAprobador, string estado)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        UsuarioBE objUsuarioBE2 = new UsuarioBE();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();

        objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioAprobador, 0);
        //objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioCreador);
        //MensajeGmail(objUsuarioBE.CardName + " a colocado una Observacion en la aprobacion de una ", Documento, "Observacion Solicitud de Entrega Rendir: " + Asunto, CodigoEntregaRendir, objUsuarioBE2.Mail);
        if (estado == "1")
        {
            objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);
            if (objUsuarioBE2.Mail.Trim() != "")
            {
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoEntregaRendir, "Entrega a Rendir " + CodigoEntregaRendir + ": " + Asunto, objUsuarioBE2.Mail);
            }
            else
            {
                objUsuarioBE2 = new UsuarioBE();
                objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioCreador, 0);
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoEntregaRendir, "Entrega a Rendir " + CodigoEntregaRendir + ": " + Asunto, objUsuarioBE2.Mail);
            }
        }
        if (estado == "2")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, 1);

            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoEntregaRendir, "Entrega a Rendir " + CodigoEntregaRendir + ": " + Asunto, lstUsuarioBE[i].Mail);
            }
        }
        if (estado == "3")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, 2);

            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha colocado una observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoEntregaRendir, "Entrega a Rendir " + CodigoEntregaRendir + ": " + Asunto, lstUsuarioBE[i].Mail);
            }
        }

    }

    protected void Rechazar_Click(object sender, EventArgs e)
    {
        try
        {
            bRechazar.Enabled = false;

            String strIdEntregaRendir = "";
            strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

            UsuarioBE objUsuarioBE = new UsuarioBE();
            EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
            EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
            objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);

            objEntregaRendirBE.Estado = "5";
            objEntregaRendirBE.Comentario = txtComentario.Text;
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                objEntregaRendirBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                objEntregaRendirBE.CreateDate = DateTime.Now;
                objEntregaRendirBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                objEntregaRendirBE.UpdateDate = DateTime.Now;
            }

            objEntregaRendirBC.ModificarEntregaRendir(objEntregaRendirBE);
            EnviarMensajeRechazado(objUsuarioBE.IdUsuario, objEntregaRendirBE.IdUsuarioCreador, objEntregaRendirBE.IdUsuarioSolicitante, "Entrega a Rendir", txtAsunto.Text, objEntregaRendirBE.CodigoEntregaRendir, objUsuarioBE.CardName);

            Response.Redirect("~/EntregasRendir.aspx");
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (EntregaRendir): " + ex.Message);
        }
        finally
        {
            bRechazar.Enabled = true;
        }
    }

    private void EnviarMensajeRechazado(int IdUsuarioAprobador, int IdUsuarioCreador, int IdUsuarioSolicitante, string Documento, string Asunto, string CodigoEntregaRendir, string UsuarioAprobador)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        UsuarioBE objUsuarioBE2 = new UsuarioBE();

        objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioAprobador, 0);
        objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);

        if (objUsuarioBE2.Mail.Trim() != "")
            MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha rechazado la solicitud de una " + Documento + " Codigo: " + CodigoEntregaRendir, "Entrega a Rendir " + CodigoEntregaRendir + ": " + Asunto, objUsuarioBE2.Mail);
        else
        {
            objUsuarioBE2 = new UsuarioBE();
            objUsuarioBE2 = objUsuarioBC.ObtenerUsuario(IdUsuarioCreador, 0);
            MensajeEmail("El usuario " + objUsuarioBE.CardName + " ha rechazado la solicitud de una " + Documento + " Codigo: " + CodigoEntregaRendir, "Entrega a Rendir " + CodigoEntregaRendir + ": " + Asunto, objUsuarioBE2.Mail);
        }
    }

    private void MensajeEmail(string Cuerpo, string Asunto, string Destino)//string UsuarioSolicitante, string Documento, string Asunto, string CodigoEntregaRendir, string Destino)
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
            email_body = Cuerpo + "<br/> . Por favor ingresar al Portal Web para continuar con el proceso si fuera necesario.";
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
                ExceptionHelper.LogException(ex);
                Mensaje("Ocurrió un error (EntregaRendir): " + ex.Message);
                //sMemoryStream.Close();
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
}