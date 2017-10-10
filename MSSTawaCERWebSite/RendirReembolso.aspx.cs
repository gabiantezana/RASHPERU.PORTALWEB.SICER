using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

using MSS.TAWA.BC;
using MSS.TAWA.BE;
using System.Net.Mail;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;

using System.Net;
using System.Net.NetworkInformation;
using MssTawaCer.App_Code.Helper;

public partial class RendirReembolso : System.Web.UI.Page
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
            String strIdReembolso = "";

            if (!this.IsPostBack)
            {
                strModo = Context.Items["Modo"].ToString();
                strIdReembolso = Context.Items["IdReembolso"].ToString();

                ViewState["Modo"] = strModo;
                ViewState["IdReembolso"] = strIdReembolso;

                ListarTipoDocumento();
                ListarProveedor();
                ListarProveedorCrear();
                ListarCentroCostos();
                ListarConcepto();
                ListarRendicion();
                ListarMoneda(Convert.ToInt32(strIdReembolso));
                Modalidad(Convert.ToInt32(strModo));
                ModalidadCampo(Convert.ToInt32(strModo), Convert.ToInt32(strIdReembolso));
                LlenarCamposCaberaExcel1();

                ReembolsoBC objReembolsoBC = new ReembolsoBC();
                ReembolsoBE objReembolsoBE = new ReembolsoBE();
                objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);
                txtComentario.Text = objReembolsoBE.Comentario;

                if (objReembolsoBE.Estado == "19")
                    txtFechaContabilizacion.Text = txtFechaContabilizacion.Text = (objReembolsoBE.FechaContabilizacion).ToString("dd/MM/yyyy");
                else
                    txtFechaContabilizacion.Text = txtFechaContabilizacion.Text = (DateTime.Today).ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
        }
    }

    private void ListarTipoDocumento()
    {
        try
        {
            DocumentoBC objDocumentoBC = new DocumentoBC();
            ddlTipo.DataSource = objDocumentoBC.ListarDocumento(0, 0);
            ddlTipo.DataTextField = "Descripcion";
            ddlTipo.DataValueField = "IdDocumento";
            ddlTipo.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
        }
    }

    private void ListarProveedor()
    {
        try
        {
            //ProveedorBC objProveedorBC = new ProveedorBC();
            //ddlProveedor.DataSource = objProveedorBC.ListarProveedor(0, 0);
            //ddlProveedor.DataTextField = "CardName";
            //ddlProveedor.DataValueField = "IdProveedor";
            //ddlProveedor.DataBind();

            //ddlRUC.DataSource = objProveedorBC.ListarProveedor(0, 1);
            //ddlRUC.DataTextField = "Documento";
            //ddlRUC.DataValueField = "IdProveedor";
            //ddlRUC.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
        }
    }

    private void ListarCentroCostos()
    {
        try
        {
            CentroCostosBC objCentroCostosBC = new CentroCostosBC();

            String strIdReembolso = "";
            strIdReembolso = Context.Items["IdReembolso"].ToString();
            ReembolsoBC objReembolsoBC = new ReembolsoBC();
            ReembolsoBE objReembolsoBE = new ReembolsoBE();
            objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);

            ddlCentroCostos3.DataSource = objCentroCostosBC.ListarCentroCostos(objReembolsoBE.IdEmpresa, 3);
            ddlCentroCostos3.DataTextField = "Descripcion";
            ddlCentroCostos3.DataValueField = "IdCentroCostos";
            ddlCentroCostos3.DataBind();

            ddlCentroCostos4.DataSource = objCentroCostosBC.ListarCentroCostos(objReembolsoBE.IdEmpresa, 4);
            ddlCentroCostos4.DataTextField = "Descripcion";
            ddlCentroCostos4.DataValueField = "IdCentroCostos";
            ddlCentroCostos4.DataBind();

            ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(objReembolsoBE.IdEmpresa, 5);
            ddlCentroCostos5.DataTextField = "Descripcion";
            ddlCentroCostos5.DataValueField = "IdCentroCostos";
            ddlCentroCostos5.DataBind();

            ddlCentroCostos3.SelectedValue = objReembolsoBE.IdCentroCostos3.ToString();
            ddlCentroCostos4.SelectedValue = objReembolsoBE.IdCentroCostos4.ToString();
            ddlCentroCostos5.SelectedValue = objReembolsoBE.IdCentroCostos5.ToString();
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
        }
    }

    private void ListarConcepto()
    {
        try
        {
            ConceptoBC objConceptoBC = new ConceptoBC();
            ddlConcepto.DataSource = objConceptoBC.ListarConcepto(Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value), 1);
            ddlConcepto.DataTextField = "Descripcion";
            ddlConcepto.DataValueField = "IdConcepto";
            ddlConcepto.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
        }
    }

    private void ListarRendicion()
    {
        String strIdReembolso = "";
        strIdReembolso = ViewState["IdReembolso"].ToString();

        ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
        gvReembolso.DataSource = objReembolsoDocumentoBC.ListarReembolsoDocumento(Convert.ToInt32(strIdReembolso), 1);
        gvReembolso.DataBind();

        if (gvReembolso.Rows.Count > 0) bEnviar.Visible = true;
        else bEnviar.Visible = false;
    }

    private void ListarMoneda(int IdReembolso)
    {
        MonedaBC objMonedaBC = new MonedaBC();

        ddlIdMonedaDoc.DataSource = objMonedaBC.ListarMoneda(0, 1);
        ddlIdMonedaDoc.DataTextField = "Descripcion";
        ddlIdMonedaDoc.DataValueField = "IdMoneda";
        ddlIdMonedaDoc.DataBind();

        ddlIdMonedaOriginal.DataSource = objMonedaBC.ListarMoneda(IdReembolso, 4);
        ddlIdMonedaOriginal.DataTextField = "Descripcion";
        ddlIdMonedaOriginal.DataValueField = "IdMoneda";
        ddlIdMonedaOriginal.DataBind();
    }

    private void ListarProveedorCrear()
    {
        String strIdReembolso = "";
        strIdReembolso = ViewState["IdReembolso"].ToString();

        ProveedorBC objProveedorBC = new ProveedorBC();
        gvProveedor.DataSource = objProveedorBC.ListarProveedor(Convert.ToInt32(strIdReembolso), 3);
        gvProveedor.DataBind();
    }

    private void Modalidad(int p)
    {
        try
        {
            switch (p)
            {
                case 1:
                    LlenarCabecera();
                    //LimpiarCampos();
                    break;
                case 2:
                    //lblCabezera.Text = "Aprobar Reembolso";
                    //bCrear.Text = "Guardar";
                    //LlenarCampos(Convert.ToInt32(ViewState["IdReembolso"].ToString()));
                    break;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
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

            NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
            NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();
            objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(objReembolsoBE.IdReembolso, 6);

            //UsuarioAreaNivelBC objUsuarioAreaNivelBC = new UsuarioAreaNivelBC();
            //UsuarioAreaNivelBE objUsuarioAreaNivelBE = new UsuarioAreaNivelBE();
            //objUsuarioAreaNivelBE = objUsuarioAreaNivelBC.ObtenerUsuarioAreaNivel(objUsuarioBE.IdUsuario, 3, IdReembolso);

            //NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
            //NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();
            //NivelAprobacionBE objNivelAprobacionBE2 = new NivelAprobacionBE();
            //if (objUsuarioAreaNivelBE != null)
            //{
            //    objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(objUsuarioAreaNivelBE.IdNivelAprobacion, 0);
            //    objNivelAprobacionBE2 = objNivelAprobacionBC.ObtenerNivelAprobacion(objReembolsoBE.IdReembolso, 6); //ultimo nivel RE
            //}

            gvReembolso.Columns[0].Visible = false;
            gvReembolso.Columns[1].Visible = false;
            gvReembolso.Columns[2].Visible = false;
            bAgregar.Visible = false;
            bGuardar.Visible = false;
            bCancelar.Visible = true;
            //FileUpload1.Visible = false;
            bMasivo.Visible = false;
            bAgregar2.Visible = false;
            bGuardar2.Visible = false;
            lblComentario.Visible = true;
            txtComentario.Visible = true;
            bEnviar.Visible = false;
            bAprobar.Visible = false;
            bRechazar.Visible = false;
            bObservacion.Visible = false;

            //11, Rendir: Por Aprobar Nivel 1/ 12, Rendir: Observaciones  Nivel 1
            //13, Rendir: Por Aprobar Nivel 2/ 14, Rendir: Observaciones  Nivel 2
            //15, Rendir: Por Aprobar Nivel 3/ 16, Rendir: Observaciones  Nivel 3
            //17, Rendir: Por Aprobar Contabilidad/ 18, Rendir: Observaciones Contabilidad/ 19, Rendir: Aprobado

            if (objReembolsoBE.Estado == "4")//Aprobado
            {
                if (objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                {
                    if (objUsuarioSesionBE.IdUsuario == objReembolsoBE.IdUsuarioCreador)
                    {
                        gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objReembolsoBE.Estado == "11")//11, Rendir: Por Aprobar Nivel 1
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                {
                    if (objUsuarioSolicitanteBE.IdUsuarioRE1 == objUsuarioSesionBE.IdUsuario)
                    {
                        gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bRechazar.Visible = true; bObservacion.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objReembolsoBE.Estado == "12")//12, Rendir: Observaciones  Nivel 1
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                {
                    if (objUsuarioSolicitanteBE.IdUsuarioRE1 == objUsuarioSesionBE.IdUsuario)
                    {
                        gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                    if (objUsuarioSolicitanteBE.IdUsuario == objUsuarioSesionBE.IdUsuario)
                    {
                        gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objReembolsoBE.Estado == "13")//13, Rendir: Por Aprobar Nivel 2
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                {
                    if (objUsuarioSolicitanteBE.IdUsuarioRE2 == objUsuarioSesionBE.IdUsuario)
                    {
                        gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bRechazar.Visible = true; bObservacion.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objReembolsoBE.Estado == "14")//14, Rendir: Observaciones  Nivel 2
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                {
                    if (objUsuarioSolicitanteBE.IdUsuarioRE1 == objUsuarioSesionBE.IdUsuario)
                    {
                        gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bRechazar.Visible = true; bObservacion.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objReembolsoBE.Estado == "15")//15, Rendir: Por Aprobar Nivel 3
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                {
                    if (objUsuarioSolicitanteBE.IdUsuarioRE3 == objUsuarioSesionBE.IdUsuario)
                    {
                        gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bRechazar.Visible = true; bObservacion.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objReembolsoBE.Estado == "16")//14, Rendir: Observaciones  Nivel 3
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                {
                    if (objUsuarioSolicitanteBE.IdUsuarioRE2 == objUsuarioSesionBE.IdUsuario)
                    {
                        gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bRechazar.Visible = true; bObservacion.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objReembolsoBE.Estado == "17")//17, Rendir: Por Aprobar Contabilidad
            {
                if (objPerfilUsuarioBE.TipoAprobador == "2" || objPerfilUsuarioBE.TipoAprobador == "5")
                {
                    gvReembolso.Columns[0].Visible = true; gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                    bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bRechazar.Visible = true; bObservacion.Visible = true; txtFechaContabilizacion.Enabled = true;
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objReembolsoBE.Estado == "18")//18, Rendir: Observaciones Contabilidad
            {
                if (objUsuarioSesionBE.IdUsuario == objReembolsoBE.IdUsuarioCreador)
                {
                    gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                    bMasivo.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                }
                //if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                //{
                //    if (objNivelAprobacionBE.Nivel == "3")
                //        if (objUsuarioSolicitanteBE.IdUsuarioRE3 == objUsuarioSesionBE.IdUsuario)
                //        {
                //            gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                //            bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
                //        }
                //    if (objNivelAprobacionBE.Nivel == "2")
                //        if (objUsuarioSolicitanteBE.IdUsuarioRE2 == objUsuarioSesionBE.IdUsuario)
                //        {
                //            gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                //            bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
                //        }
                //    if (objNivelAprobacionBE.Nivel == "1")
                //        if (objUsuarioSolicitanteBE.IdUsuarioRE1 == objUsuarioSesionBE.IdUsuario)
                //        {
                //            gvReembolso.Columns[1].Visible = true; gvReembolso.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                //            bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
                //        }
                //}
            }
        }
    }

    protected void gvReembolso_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int IdReembolsoDocumento;

        try
        {
            IdReembolsoDocumento = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("Editar"))
            {
                lblIdReembolsoDocumento.Text = IdReembolsoDocumento.ToString();

                ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
                ReembolsoDocumentoBE objReembolsoDocumentoBE = new ReembolsoDocumentoBE();
                objReembolsoDocumentoBE = objReembolsoDocumentoBC.ObtenerReembolsoDocumento(IdReembolsoDocumento, 0);
                txtSerie.Text = objReembolsoDocumentoBE.SerieDoc;
                txtNumero.Text = objReembolsoDocumentoBE.CorrelativoDoc;
                txtFecha.Text = objReembolsoDocumentoBE.FechaDoc.ToString().Substring(0, 10);
                txtMontoTotal.Text = Convert.ToDouble(objReembolsoDocumentoBE.MontoTotal).ToString("0.00");
                txtMontoDoc.Text = Convert.ToDouble(objReembolsoDocumentoBE.MontoDoc).ToString("0.00");
                txtMontoAfecta.Text = Convert.ToDouble(objReembolsoDocumentoBE.MontoAfecto).ToString("0.00");
                txtMontoNoAfecta.Text = Convert.ToDouble(objReembolsoDocumentoBE.MontoNoAfecto).ToString("0.00");
                txtTasaCambio.Text = Convert.ToDouble(objReembolsoDocumentoBE.TasaCambio).ToString("0.00");
                if (objReembolsoDocumentoBE.IdMonedaDoc == objReembolsoDocumentoBE.IdMonedaOriginal) txtTasaCambio.Enabled = false;
                else txtTasaCambio.Enabled = false;
                ddlTipo.SelectedValue = objReembolsoDocumentoBE.TipoDoc.ToString();

                ProveedorBC objProveedorBC = new ProveedorBC();
                ProveedorBE objProveedorBE = new ProveedorBE();
                objProveedorBE = objProveedorBC.ObtenerProveedor(objReembolsoDocumentoBE.IdProveedor, 0, "");
                txtProveedor.Text = objProveedorBE.Documento;
                lblProveedor.Text = objProveedorBE.CardName;

                try { ddlConcepto.SelectedValue = objReembolsoDocumentoBE.IdConcepto.ToString(); }
                catch { ddlConcepto.SelectedValue = "0"; }
                ddlIdMonedaDoc.SelectedValue = objReembolsoDocumentoBE.IdMonedaDoc.ToString();
                ddlIdMonedaOriginal.SelectedValue = objReembolsoDocumentoBE.IdMonedaOriginal.ToString();

                ddlCentroCostos3.SelectedValue = objReembolsoDocumentoBE.IdCentroCostos3.ToString();
                ddlCentroCostos4.SelectedValue = objReembolsoDocumentoBE.IdCentroCostos4.ToString();
                ddlCentroCostos5.SelectedValue = objReembolsoDocumentoBE.IdCentroCostos5.ToString();

                bAgregar.Visible = false;
                bGuardar.Visible = true;
            }
            if (e.CommandName.Equals("Eliminar"))
            {
                ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
                objReembolsoDocumentoBC.EliminarReembolsoDocumento(IdReembolsoDocumento);
                ListarRendicion();
            }
        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReembolso.PageIndex = e.NewPageIndex;
        ListarRendicion();
    }

    private void LlenarCabecera()
    {
        String strIdReembolso = "";
        strIdReembolso = ViewState["IdReembolso"].ToString();

        ReembolsoBC objReembolsoBC = new ReembolsoBC();
        ReembolsoBE objReembolsoBE = new ReembolsoBE();
        objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);

        ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
        ReembolsoDocumentoBE objReembolsoDocumentoBE = new ReembolsoDocumentoBE();
        objReembolsoDocumentoBE = objReembolsoDocumentoBC.ObtenerReembolsoDocumento(Convert.ToInt32(strIdReembolso), 1);
        string montoCCD = "0.00";
        if (objReembolsoDocumentoBE != null) montoCCD = objReembolsoDocumentoBE.MontoTotal;
        lblCabezera.Text = "Reembolso: " + objReembolsoBE.CodigoReembolso + " - Monto: " + montoCCD + "/" + Convert.ToDouble(objReembolsoBE.MontoInicial).ToString("0.00");

        if (objReembolsoBE.Estado == "19")
            txtFechaContabilizacion.Text = txtFechaContabilizacion.Text = (objReembolsoBE.FechaContabilizacion).ToString("dd/MM/yyyy");
        else
            txtFechaContabilizacion.Text = txtFechaContabilizacion.Text = (DateTime.Today).ToString("dd/MM/yyyy");
    }

    private void LimpiarCampos()
    {
        txtSerie.Text = "";
        txtNumero.Text = "";
        txtFecha.Text = "";
        txtMontoTotal.Text = "";
        txtMontoDoc.Text = "";
        txtMontoAfecta.Text = "";
        txtMontoNoAfecta.Text = "";
        txtMontoIGV.Text = "";
        txtTasaCambio.Text = "";
        ddlTipo.SelectedValue = "0";
        ddlConcepto.SelectedValue = "0";
        ddlIdMonedaDoc.SelectedValue = "0";
        txtProveedor.Text = "";
        lblProveedor.Text = "sin validar";
    }

    protected void ddlCentroCosto3_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCentroCostos3.SelectedValue != "0")
        //{
        //    CentroCostosBC objConceptoBC = new CentroCostosBC();
        //    List<CentroCostosBE> lstConceptoBE = new List<CentroCostosBE>();
        //    lstConceptoBE = objConceptoBC.ListarCentroCostos(Convert.ToInt32(ddlCentroCostos3.SelectedValue), 2);
        //    ddlCentroCostos4.DataSource = lstConceptoBE;
        //    ddlCentroCostos4.DataTextField = "Descripcion";
        //    ddlCentroCostos4.DataValueField = "IdCentroCostos";
        //    ddlCentroCostos4.DataBind();

        //    ddlCentroCostos4.Enabled = true;
        //}
        //else
        //{
        //    ddlCentroCostos4.SelectedValue = "0";
        //    ddlCentroCostos4.Enabled = false;
        //}
    }

    protected void ddlIdMonedaDoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdMonedaDoc.SelectedValue != "0")
        {
            if (ddlIdMonedaOriginal.SelectedValue == ddlIdMonedaDoc.SelectedValue)
            {
                txtTasaCambio.Text = "1.0000";
                txtTasaCambio.Enabled = false;
            }
            else
                txtTasaCambio.Enabled = true;
        }
        else
        {
            txtTasaCambio.Enabled = true;
        }
    }

    protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList dropdownlist = (DropDownList)sender;
        //string a = dropdownlist.ID;

        //if (a == "ddlProveedor")
        //{
        //    if (ddlProveedor.SelectedValue != "0") ddlRUC.SelectedValue = ddlProveedor.SelectedValue;
        //    else ddlRUC.SelectedValue = "0";
        //}
        //else
        //{
        //    if (ddlRUC.SelectedValue != "0") ddlProveedor.SelectedValue = ddlRUC.SelectedValue;
        //    else ddlProveedor.SelectedValue = "0";
        //}
    }

    protected void chkRow_OnCheckedChanged(Object sender, EventArgs args)
    {
        if (gvReembolso.Columns[0].Visible == true)
        {
            System.Web.UI.WebControls.CheckBox checkbox = (System.Web.UI.WebControls.CheckBox)sender;
            GridViewRow row = (GridViewRow)checkbox.NamingContainer;
            int Id = Convert.ToInt32(gvReembolso.Rows[row.DataItemIndex].Cells[2].Text);

            ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
            ReembolsoDocumentoBE objReembolsoDocumentoBE = new ReembolsoDocumentoBE();
            objReembolsoDocumentoBE = objReembolsoDocumentoBC.ObtenerReembolsoDocumento(Id, 0);

            if (checkbox.Checked == true) objReembolsoDocumentoBE.Estado = "1";
            else objReembolsoDocumentoBE.Estado = "2";
            objReembolsoDocumentoBC.ModificarReembolsoDocumento(objReembolsoDocumentoBE);
            LlenarCabecera();
        }
    }

    public String SetearTipo(String sId)
    {
        DocumentoBC objDocumentoBC = new DocumentoBC();
        DocumentoBE objDocumentoBE = new DocumentoBE();
        objDocumentoBE = objDocumentoBC.ObtenerDocumento(Convert.ToInt32(sId));
        if (objDocumentoBE != null) return objDocumentoBE.Descripcion;
        else return "";
    }

    public String SetearProveedorRUC(String sIdProveedor)
    {
        ProveedorBC objProveedorBC = new ProveedorBC();
        ProveedorBE objProveedorBE = new ProveedorBE();
        objProveedorBE = objProveedorBC.ObtenerProveedor(Convert.ToInt32(sIdProveedor), 0, "");
        if (objProveedorBE != null) return objProveedorBE.Documento;
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

    public bool SetearCheck(String sId)
    {
        if (sId == "1") return true;
        else return false;
    }

    protected void Agregar_Click(object sender, EventArgs e)
    {
        try
        {
            bAgregar.Enabled = false;

            string mensajeError = "";
            bool validacion = true;

            validacion = ValidarImporte();
            if (validacion == false) Mensaje("Usted a ingresado los importes erroneamente.");

            if (validacion)
            {
                DocumentoBC objDocumentoBC = new DocumentoBC();
                DocumentoBE objDocumentoBE = new DocumentoBE();
                objDocumentoBE = objDocumentoBC.ObtenerDocumento(Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value));
                if (objDocumentoBE != null)
                {
                    if (objDocumentoBE.CodigoSunat != "99")
                    {
                        if (ddlTipo.SelectedItem.Value == "0" || txtSerie.Text.Trim() == "" || txtNumero.Text.Trim() == "" || txtFecha.Text.Trim() == "" ||
                            ddlConcepto.SelectedItem.Value == "0" ||
                            //ddlCentroCostos3.SelectedItem.Value == "0" ||
                            //ddlCentroCostos4.SelectedItem.Value == "0" || 
                            //ddlCentroCostos5.SelectedItem.Value == "0" || 
                            ddlIdMonedaDoc.SelectedItem.Value == "0" ||
                            ddlIdMonedaOriginal.SelectedItem.Value == "0" || txtMontoDoc.Text.Trim() == "" || txtTasaCambio.Text.Trim() == "")
                        {
                            validacion = false;
                            mensajeError = "Usted no ha ingresado toda la informacion necesaria";
                        }
                    }
                    else
                    {
                        if (ddlTipo.SelectedItem.Value == "0" || txtFecha.Text.Trim() == "" ||
                            ddlConcepto.SelectedItem.Value == "0" ||
                            //ddlCentroCostos3.SelectedItem.Value == "0" ||
                            //ddlCentroCostos4.SelectedItem.Value == "0" || 
                            //ddlCentroCostos5.SelectedItem.Value == "0" || 
                            ddlIdMonedaDoc.SelectedItem.Value == "0" ||
                            ddlIdMonedaOriginal.SelectedItem.Value == "0" || txtMontoDoc.Text.Trim() == "" || txtTasaCambio.Text.Trim() == "")
                        {
                            validacion = false;
                            mensajeError = "Usted no ha ingresado toda la informacion necesaria";
                        }
                    }
                }
                else
                {
                    validacion = false;
                    mensajeError = "Debe seleccionar el Tipo de Documento.";
                }
            }

            if (validacion)
            {
                decimal n;
                bool isNumeric1, isNumeric2, isNumeric3, isNumeric4;
                isNumeric1 = decimal.TryParse(txtMontoDoc.Text, out n);
                if (isNumeric1 == false) validacion = false;
                isNumeric2 = decimal.TryParse(txtTasaCambio.Text, out n);
                if (isNumeric2 == false) validacion = false;
                isNumeric3 = decimal.TryParse(txtMontoAfecta.Text, out n);
                if (isNumeric3 == false) validacion = false;
                isNumeric4 = decimal.TryParse(txtMontoNoAfecta.Text, out n);
                if (isNumeric4 == false) validacion = false;
                mensajeError = "Usted a ingresado los importes erroneamente.";
            }

            if (validacion)
            {
                double MontoDoc = Convert.ToDouble(txtMontoDoc.Text);
                double MontoIGV = Convert.ToDouble(txtMontoIGV.Text);
                double MontoAfecto = Convert.ToDouble(txtMontoAfecta.Text);
                double MontoNoAfecto = Convert.ToDouble(txtMontoNoAfecta.Text);
                if (Math.Round(MontoDoc, 2) != Math.Round(MontoIGV + MontoAfecto + MontoNoAfecto, 2)) validacion = false;
                mensajeError = "La suma del IGV, Afecata y NoAfecta no es igual al Total.";
            }

            ProveedorBC objProveedorBC = new ProveedorBC();
            ProveedorBE objProveedorBE = new ProveedorBE();
            if (validacion)
            {
                objProveedorBE = objProveedorBC.ObtenerProveedor(0, 1, txtProveedor.Text);
                if (objProveedorBE == null) validacion = false;
                mensajeError = "El proveedor no existe.";
            }

            if (validacion)
            {
                String strIdReembolso = "";
                strIdReembolso = ViewState["IdReembolso"].ToString();

                ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
                ReembolsoDocumentoBE objReembolsoDocumentoBE = new ReembolsoDocumentoBE();
                objReembolsoDocumentoBE.IdReembolso = Convert.ToInt32(strIdReembolso);

                objReembolsoDocumentoBE.IdProveedor = Convert.ToInt32(objProveedorBE.IdProveedor); ;

                objReembolsoDocumentoBE.IdConcepto = Convert.ToInt32(ddlConcepto.SelectedItem.Value);
                objReembolsoDocumentoBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objReembolsoDocumentoBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objReembolsoDocumentoBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objReembolsoDocumentoBE.TipoDoc = ddlTipo.SelectedItem.Value;
                objReembolsoDocumentoBE.SerieDoc = txtSerie.Text;
                objReembolsoDocumentoBE.CorrelativoDoc = txtNumero.Text;
                objReembolsoDocumentoBE.FechaDoc = Convert.ToDateTime(txtFecha.Text);
                objReembolsoDocumentoBE.IdMonedaOriginal = Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value);
                objReembolsoDocumentoBE.IdMonedaDoc = Convert.ToInt32(ddlIdMonedaDoc.SelectedItem.Value);
                objReembolsoDocumentoBE.MontoDoc = Convert.ToDouble(txtMontoDoc.Text).ToString("0.00");
                objReembolsoDocumentoBE.MontoIGV = Convert.ToDouble(txtMontoIGV.Text).ToString("0.00");
                objReembolsoDocumentoBE.MontoAfecto = Convert.ToDouble(txtMontoAfecta.Text).ToString("0.00");
                objReembolsoDocumentoBE.MontoNoAfecto = Convert.ToDouble(txtMontoNoAfecta.Text).ToString("0.00");
                objReembolsoDocumentoBE.MontoTotal = Convert.ToDouble(txtMontoTotal.Text).ToString("0.00");

                if (ddlIdMonedaOriginal.SelectedValue == ddlIdMonedaDoc.SelectedValue) objReembolsoDocumentoBE.TasaCambio = "1.0000";
                else objReembolsoDocumentoBE.TasaCambio = Convert.ToDouble(txtTasaCambio.Text).ToString("0.0000");

                objReembolsoDocumentoBE.Estado = "1";

                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    UsuarioBC objUsuarioBC = new UsuarioBC();
                    UsuarioBE objUsuarioBE = new UsuarioBE();
                    objUsuarioBE = (UsuarioBE)Session["Usuario"];
                    objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);

                    objReembolsoDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objReembolsoDocumentoBE.CreateDate = DateTime.Now;
                    objReembolsoDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objReembolsoDocumentoBE.UpdateDate = DateTime.Now;
                }
                int Id;
                Id = objReembolsoDocumentoBC.InsertarReembolsoDocumento(objReembolsoDocumentoBE);
                ListarRendicion();
                LlenarCabecera();
                LimpiarCampos();
            }
            else
                Mensaje(mensajeError);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
            bAgregar.Enabled = true;
        }
    }

    protected void Guardar_Click(object sender, EventArgs e)
    {
        try
        {
            bGuardar.Enabled = false;

            string mensajeError = "";
            bool validacion = true;

            validacion = ValidarImporte();
            if (validacion == false) Mensaje("Usted a ingresado los importes erroneamente.");

            if (validacion)
            {
                DocumentoBC objDocumentoBC = new DocumentoBC();
                DocumentoBE objDocumentoBE = new DocumentoBE();
                objDocumentoBE = objDocumentoBC.ObtenerDocumento(Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value));
                if (objDocumentoBE != null)
                {
                    if (objDocumentoBE.CodigoSunat != "99")
                    {
                        if (ddlTipo.SelectedItem.Value == "0"
                            || txtSerie.Text.Trim() == ""
                            || txtNumero.Text.Trim() == ""
                            || txtFecha.Text.Trim() == ""
                            || ddlConcepto.SelectedItem.Value == "0"
                            //|| ddlCentroCostos3.SelectedItem.Value == "0"
                            //|| ddlCentroCostos4.SelectedItem.Value == "0"
                            //|| ddlCentroCostos5.SelectedItem.Value == "0"
                            || ddlIdMonedaDoc.SelectedItem.Value == "0"
                            || ddlIdMonedaOriginal.SelectedItem.Value == "0"
                            || txtMontoDoc.Text.Trim() == ""
                            || txtTasaCambio.Text.Trim() == "")
                        {
                            validacion = false;
                            mensajeError = "Usted no ha ingresado toda la informacion necesaria";
                        }
                    }
                    else
                    {
                        if (ddlTipo.SelectedItem.Value == "0"
                            || txtFecha.Text.Trim() == ""
                            || ddlConcepto.SelectedItem.Value == "0"
                            //|| ddlCentroCostos3.SelectedItem.Value == "0"
                            //|| ddlCentroCostos4.SelectedItem.Value == "0"
                            //|| ddlCentroCostos5.SelectedItem.Value == "0"
                            || ddlIdMonedaDoc.SelectedItem.Value == "0"
                            || ddlIdMonedaOriginal.SelectedItem.Value == "0"
                            || txtMontoDoc.Text.Trim() == ""
                            || txtTasaCambio.Text.Trim() == "")
                        {
                            validacion = false;
                            mensajeError = "Usted no ha ingresado toda la informacion necesaria";
                        }
                    }
                }
                else
                {
                    validacion = false;
                    mensajeError = "Debe seleccionar el Tipo de Documento.";
                }
            }

            if (validacion)
            {
                decimal n;
                bool isNumeric1, isNumeric2, isNumeric3, isNumeric4;
                isNumeric1 = decimal.TryParse(txtMontoDoc.Text, out n);
                if (isNumeric1 == false) validacion = false;
                isNumeric2 = decimal.TryParse(txtTasaCambio.Text, out n);
                if (isNumeric2 == false) validacion = false;
                isNumeric3 = decimal.TryParse(txtMontoAfecta.Text, out n);
                if (isNumeric3 == false) validacion = false;
                isNumeric4 = decimal.TryParse(txtMontoNoAfecta.Text, out n);
                if (isNumeric4 == false) validacion = false;
                mensajeError = "Usted a ingresado los importes erroneamente.";
            }

            if (validacion)
            {
                double MontoDoc = Convert.ToDouble(txtMontoDoc.Text);
                double MontoIGV = Convert.ToDouble(txtMontoIGV.Text);
                double MontoAfecto = Convert.ToDouble(txtMontoAfecta.Text);
                double MontoNoAfecto = Convert.ToDouble(txtMontoNoAfecta.Text);
                if (Math.Round(MontoDoc, 2) != Math.Round(MontoIGV + MontoAfecto + MontoNoAfecto, 2)) validacion = false;
                mensajeError = "La suma del IGV, Afecata y NoAfecta no es igual al Total.";
            }

            ProveedorBC objProveedorBC = new ProveedorBC();
            ProveedorBE objProveedorBE = new ProveedorBE();
            if (validacion)
            {
                objProveedorBE = objProveedorBC.ObtenerProveedor(0, 1, txtProveedor.Text);
                if (objProveedorBE == null) validacion = false;
                mensajeError = "El proveedor no existe.";
            }

            if (validacion)
            {
                String strIdReembolso = "";
                strIdReembolso = ViewState["IdReembolso"].ToString();

                ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
                ReembolsoDocumentoBE objReembolsoDocumentoBE = new ReembolsoDocumentoBE();
                objReembolsoDocumentoBE.IdReembolsoDocumento = Convert.ToInt32(lblIdReembolsoDocumento.Text);
                objReembolsoDocumentoBE.IdReembolso = Convert.ToInt32(strIdReembolso);

                objReembolsoDocumentoBE.IdProveedor = Convert.ToInt32(objProveedorBE.IdProveedor); ;

                objReembolsoDocumentoBE.IdConcepto = Convert.ToInt32(ddlConcepto.SelectedItem.Value);
                objReembolsoDocumentoBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objReembolsoDocumentoBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objReembolsoDocumentoBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objReembolsoDocumentoBE.TipoDoc = ddlTipo.SelectedItem.Value;
                objReembolsoDocumentoBE.SerieDoc = txtSerie.Text;
                objReembolsoDocumentoBE.CorrelativoDoc = txtNumero.Text;
                objReembolsoDocumentoBE.FechaDoc = Convert.ToDateTime(txtFecha.Text);
                objReembolsoDocumentoBE.IdMonedaOriginal = Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value);
                objReembolsoDocumentoBE.IdMonedaDoc = Convert.ToInt32(ddlIdMonedaDoc.SelectedItem.Value);
                objReembolsoDocumentoBE.MontoDoc = Convert.ToDouble(txtMontoDoc.Text).ToString("0.00");
                objReembolsoDocumentoBE.MontoIGV = Convert.ToDouble(txtMontoIGV.Text).ToString("0.00");
                objReembolsoDocumentoBE.MontoAfecto = Convert.ToDouble(txtMontoAfecta.Text).ToString("0.00");
                objReembolsoDocumentoBE.MontoNoAfecto = Convert.ToDouble(txtMontoNoAfecta.Text).ToString("0.00");
                objReembolsoDocumentoBE.MontoTotal = Convert.ToDouble(txtMontoTotal.Text).ToString("0.00");

                if (ddlIdMonedaOriginal.SelectedValue == ddlIdMonedaDoc.SelectedValue) objReembolsoDocumentoBE.TasaCambio = "1.0000";
                else objReembolsoDocumentoBE.TasaCambio = Convert.ToDouble(txtTasaCambio.Text).ToString("0.0000");

                objReembolsoDocumentoBE.Estado = "1";

                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    UsuarioBC objUsuarioBC = new UsuarioBC();
                    UsuarioBE objUsuarioBE = new UsuarioBE();
                    objUsuarioBE = (UsuarioBE)Session["Usuario"];
                    objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);

                    objReembolsoDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objReembolsoDocumentoBE.CreateDate = DateTime.Now;
                    objReembolsoDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objReembolsoDocumentoBE.UpdateDate = DateTime.Now;
                }

                objReembolsoDocumentoBC.ModificarReembolsoDocumento(objReembolsoDocumentoBE);
                ListarRendicion();
                LlenarCabecera();
                LimpiarCampos();

                lblIdReembolsoDocumento.Text = "";
                bAgregar.Visible = true;
                bGuardar.Visible = false;

                ReembolsoBC objReembolsoBC = new ReembolsoBC();
                ReembolsoBE objReembolsoBE = new ReembolsoBE();
                objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);
                if (objReembolsoBE.Estado == "4") bEnviar.Visible = true;
                else bEnviar.Visible = false;
            }
            else
                Mensaje(mensajeError);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
            bGuardar.Enabled = true;
        }
    }

    protected void Cancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reembolsos.aspx");
    }

    protected void Enviar_Click(object sender, EventArgs e)
    {
        try
        {
            bEnviar.Enabled = false;

            if (gvReembolso.Rows.Count > 0)
            {
                String strIdReembolso = "";
                strIdReembolso = ViewState["IdReembolso"].ToString();
                String estado = "0";

                //VALIDAR TASA DE CAMBIO
                ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
                ReembolsoDocumentoBE objReembolsoDocumentoBE = new ReembolsoDocumentoBE();
                objReembolsoDocumentoBE = objReembolsoDocumentoBC.ObtenerReembolsoDocumento(Convert.ToInt32(strIdReembolso), 2);
                //VALIDAR TASA DE CAMBIO
                if (objReembolsoDocumentoBE == null)
                {
                    ReembolsoBC objReembolsoBC = new ReembolsoBC();
                    ReembolsoBE objReembolsoBE = new ReembolsoBE();
                    objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);
                    estado = objReembolsoBE.Estado;
                    if (objReembolsoBE.Estado == "4")
                    {
                        objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
                        objReembolsoDocumentoBE = new ReembolsoDocumentoBE();
                        objReembolsoDocumentoBE = objReembolsoDocumentoBC.ObtenerReembolsoDocumento(Convert.ToInt32(strIdReembolso), 1);
                        objReembolsoBE.MontoInicial = Convert.ToDouble(objReembolsoDocumentoBE.MontoTotal).ToString("0.00");
                        objReembolsoBE.MontoActual = Convert.ToDouble(objReembolsoDocumentoBE.MontoTotal).ToString("0.00");
                        objReembolsoBE.Estado = "11";
                    }
                    if (objReembolsoBE.Estado == "12") objReembolsoBE.Estado = "11";
                    if (objReembolsoBE.Estado == "14") objReembolsoBE.Estado = "13";
                    if (objReembolsoBE.Estado == "16") objReembolsoBE.Estado = "15";
                    if (objReembolsoBE.Estado == "18") objReembolsoBE.Estado = "17";
                    objReembolsoBC.ModificarReembolso(objReembolsoBE);

                    UsuarioBC objUsuarioBC = new UsuarioBC();
                    UsuarioBE objUsuarioBE = new UsuarioBE();
                    objUsuarioBE = objUsuarioBC.ObtenerUsuario(objReembolsoBE.IdUsuarioSolicitante, 0);
                    EnviarMensajeParaAprobador(objReembolsoBE.IdReembolso, "Reembolso", "Rendicion Reembolso: " + objReembolsoBE.CodigoReembolso, objReembolsoBE.CodigoReembolso, objUsuarioBE.CardName, estado, objReembolsoBE.IdUsuarioSolicitante);

                    Response.Redirect("~/Reembolsos.aspx");
                }
                else
                    Mensaje("El documento Serie: " + objReembolsoDocumentoBE.SerieDoc + " Numero: " + objReembolsoDocumentoBE.CorrelativoDoc + " presenta la fecha de documento: " + objReembolsoDocumentoBE.FechaDoc + " la cual aun no existe SAP y su tasa de cambio tampoco. Por favor contactarse con Contabilidad y/o Sistemas.");
            }
            else
                Mensaje("Aun no se ah rendido ningun documento.");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
            bEnviar.Enabled = true;
        }
    }

    private void EnviarMensajeParaAprobador(int IdReembolso, string Documento, string Asunto, string CodigoReembolso, string UsuarioSolicitante, string estado, int IdUsuarioSolicitante)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();

        if (estado == "4" || estado == "12")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(10, IdReembolso, 1);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El usuario " + UsuarioSolicitante + " a realizado la rendicion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto, lstUsuarioBE[i].Mail);
            }
        }
        else
        {
            if (estado == "14")
            {
                lstUsuarioBE = objUsuarioBC.ListarUsuario(10, IdReembolso, 2);
                for (int i = 0; i < lstUsuarioBE.Count; i++)
                {
                    MensajeMail("El usuario " + UsuarioSolicitante + " a realizado la rendicion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto, lstUsuarioBE[i].Mail);
                }
            }
            else
            {
                if (estado == "16")
                {
                    lstUsuarioBE = objUsuarioBC.ListarUsuario(10, IdReembolso, 3);
                    for (int i = 0; i < lstUsuarioBE.Count; i++)
                    {
                        MensajeMail("El usuario " + UsuarioSolicitante + " a realizado la rendicion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto, lstUsuarioBE[i].Mail);
                    }
                }
                else
                {
                    lstUsuarioBE = objUsuarioBC.ListarUsuario(7, 0, 0);
                    for (int i = 0; i < lstUsuarioBE.Count; i++)
                    {
                        MensajeMail("El usuario " + UsuarioSolicitante + " a realizado la rendicion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto, lstUsuarioBE[i].Mail);
                    }
                }
            }
        }
    }
    public string GetIpAddress()
    {
        return ClbHelpers.IPHelper.GetIPAddress(HttpContext.Current.Request.ServerVariables["HTTP_VIA"],
                        HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"],
                        HttpContext.Current.Request.UserHostAddress);

    }



    protected void Aprobar_Click(object sender, EventArgs e)
    {
        try
        {
            UsuarioBC objUsuarioBC = new UsuarioBC();
            UsuarioBE objUsuarioSesionBE = new UsuarioBE();
            UsuarioBE objUsuarioSolicitanteBE = new UsuarioBE();
            objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
            objUsuarioSesionBE = objUsuarioBC.ObtenerUsuario(objUsuarioSesionBE.IdUsuario, 0);

            PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
            PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBE();
            objPerfilUsuarioBE = objPerfilUsuarioBC.ObtenerPerfilUsuario(objUsuarioSesionBE.IdPerfilUsuario);

            bAprobar.Enabled = false;
            bool validar = true;
            String mensaje = "";

            String strIdReembolso = "";
            strIdReembolso = ViewState["IdReembolso"].ToString();
            String estado = "0";

            ReembolsoBC objReembolsoBC = new ReembolsoBC();
            ReembolsoBE objReembolsoBE = new ReembolsoBE();
            objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);
            estado = objReembolsoBE.Estado;

            NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
            NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();
            objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(objReembolsoBE.IdReembolso, 6);
            if (estado == "11")
            {
                if (objNivelAprobacionBE.Nivel == "1") objReembolsoBE.Estado = "17";
                else objReembolsoBE.Estado = "13";
            }
            //if (estado == "12")
            //{
            //    objReembolsoBE.Estado = "11";
            //    estado = "11";
            //}
            if (estado == "13")
            {
                if (objNivelAprobacionBE.Nivel == "2") objReembolsoBE.Estado = "17";
                else objReembolsoBE.Estado = "15";
            }
            if (estado == "15")
            {
                if (objNivelAprobacionBE.Nivel == "3") objReembolsoBE.Estado = "17";
                else objReembolsoBE.Estado = "17";
            }
            if (estado == "17" && (objPerfilUsuarioBE.IdPerfilUsuario == 2 || objPerfilUsuarioBE.IdPerfilUsuario == 1002 || objPerfilUsuarioBE.IdPerfilUsuario == 1008))
            {
                if (txtFechaContabilizacion.Text.Trim() != "")
                {
                    objReembolsoBE.FechaContabilizacion = Convert.ToDateTime(txtFechaContabilizacion.Text);
                    objReembolsoBE.Estado = "19";

                    ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
                    ReembolsoDocumentoBE objReembolsoDocumentoBE = new ReembolsoDocumentoBE();
                    objReembolsoDocumentoBE = objReembolsoDocumentoBC.ObtenerReembolsoDocumento(Convert.ToInt32(strIdReembolso), 1);
                    objReembolsoBE.MontoInicial = objReembolsoDocumentoBE.MontoTotal;
                    objReembolsoBE.MontoGastado = objReembolsoDocumentoBE.MontoTotal;
                    objReembolsoBE.MontoActual = "0.00";//(Convert.ToDouble(objReembolsoBE.MontoInicial) - Convert.ToDouble(objReembolsoDocumentoBE.MontoTotal)).ToString("0.000000");


                    //Recupero el usuario conectado
                    UsuarioBE objUsuarioBE1 = new UsuarioBE();
                    objUsuarioBE1 = (UsuarioBE)Session["Usuario"];
                    String UsuarioConectado = objUsuarioBE1.IdUsuario.ToString();
                    //Recupero la IP del cliente
                    String IP = GetIpAddress();
                    //Recupero el hostname del cliente
                    string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
                    String HOSTNAME = System.Environment.MachineName;
                    String HOSTNAME1 = computer_name[0].ToString();


                    objReembolsoBE.UserUpdate = UsuarioConectado;


                    ControlContabilidadBE oControlContabilidadBE = new ControlContabilidadBE();
                    ControlContabilidadBC oControlContabilidadBC = new ControlContabilidadBC();

                    oControlContabilidadBE.IdDocumento = objReembolsoBE.IdReembolso;
                    oControlContabilidadBE.CodigoDocumento = objReembolsoBE.CodigoReembolso;
                    oControlContabilidadBE.UserUpdate = Convert.ToInt32(UsuarioConectado);
                    oControlContabilidadBE.FechaContabilizacion = DateTime.Now;
                    oControlContabilidadBE.Getdate = DateTime.Now;
                    oControlContabilidadBE.Documento = "RE";
                    oControlContabilidadBE.CreateDate = objReembolsoBE.CreateDate;
                    oControlContabilidadBE.Hostname = HOSTNAME1;
                    oControlContabilidadBE.IP = IP;

                    oControlContabilidadBC.InsertarReembolso(oControlContabilidadBE);


                }
                else
                {
                    validar = false;
                    mensaje = "Debe ingresar un fecha de contabilizacion.";
                }
            }

            if (validar)
            {

                objReembolsoBE.Comentario = "";
                objReembolsoBC.ModificarReembolso(objReembolsoBE);

                UsuarioBC objUserBC = new UsuarioBC();
                UsuarioBE objUsuarioBE = new UsuarioBE();
                objUsuarioBE = objUserBC.ObtenerUsuario(objReembolsoBE.IdUsuarioSolicitante, 0);
                EnviarMensajeAprobado(objReembolsoBE.IdReembolso, "Reembolso", "Rendicion Reembolso: " + objReembolsoBE.CodigoReembolso, objReembolsoBE.CodigoReembolso, objUsuarioBE.CardName, objReembolsoBE.Estado, objReembolsoBE.IdUsuarioSolicitante);


            }
            else
            {
                Mensaje(mensaje);
            }
        }

        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
            bAprobar.Enabled = true;
            Response.Redirect("~/Reembolsos.aspx");
        }
    }

    private void EnviarMensajeAprobado(int IdReembolso, string Documento, string Asunto, string CodigoReembolso, string UsuarioSolicitante, string estado, int IdUsuarioSolicitante)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();
        if (estado == "13")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(10, IdReembolso, 2);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El usuario " + UsuarioSolicitante + " ha realizado la rendicion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto, lstUsuarioBE[i].Mail);
            }
        }
        if (estado == "15")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(10, IdReembolso, 3);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El usuario " + UsuarioSolicitante + " ha realizado la rendicion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto, lstUsuarioBE[i].Mail);
            }
        }
        if (estado == "17")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(7, 0, 0);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El usuario " + UsuarioSolicitante + " ha realizado la rendicion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto, lstUsuarioBE[i].Mail);
            }




            /////////////
            String strIdReembolso = ViewState["IdReembolso"].ToString();

            ReembolsoBE objReembolsoBE = new ReembolsoBE();
            ReembolsoBC objReembolsoBC = new ReembolsoBC();
            objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);

            UsuarioBE objUsuarioBE = new UsuarioBE();
            objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);


            List<UsuarioBE> lstUsuarioTesoreriaBE = new List<UsuarioBE>();
            lstUsuarioTesoreriaBE = objUsuarioBC.ListarUsuarioCorreosTesoreria();

            CorreosBE objCorreoBE = new CorreosBE();
            CorreosBC objCorreosBC = new CorreosBC();
            List<CorreosBE> lstCorreosBE = new List<CorreosBE>();



            String moneda = "";
            if (objReembolsoBE.Moneda.ToString() == "1")
                moneda = "S/. ";
            else
                moneda = "USD. ";



            for (int x = 0; x < lstUsuarioTesoreriaBE.Count; x++)
            {


                if (lstUsuarioTesoreriaBE[x].Mail.ToString() != "")
                {
                    lstCorreosBE = objCorreosBC.ObtenerCorreos(1);
                    MensajeMail(lstCorreosBE[0].TextoCorreo.ToString() + ": eL " + Documento + " con Codigo: " + CodigoReembolso + "<br/>" + "<br/>"
                    // + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                    + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                    + "Importe a Pagar :" + moneda + objReembolsoBE.MontoInicial + "<br/>"
                    + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                    , "Reembolso " + CodigoReembolso, lstUsuarioTesoreriaBE[x].Mail.ToString());
                }

            }

        }
        if (estado == "19")
        {
            UsuarioBE objUsuarioBE = new UsuarioBE();
            objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);
            MensajeMail("El " + Documento + " Codigo: " + CodigoReembolso + " fue aprobada", Asunto + " Aprobada", objUsuarioBE.Mail);
        }
    }

    protected void Observacion_Click(object sender, EventArgs e)
    {
        try
        {
            bObservacion.Enabled = false;

            if (txtComentario.Text.Trim() != "")
            {
                String strIdReembolso = "";
                strIdReembolso = ViewState["IdReembolso"].ToString();
                String estado = "0";

                ReembolsoBC objReembolsoBC = new ReembolsoBC();
                ReembolsoBE objReembolsoBE = new ReembolsoBE();
                objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);
                estado = objReembolsoBE.Estado;
                if (estado == "11") objReembolsoBE.Estado = "12";
                if (estado == "13") objReembolsoBE.Estado = "14";
                if (estado == "15") objReembolsoBE.Estado = "16";
                if (estado == "17") objReembolsoBE.Estado = "18";

                if (estado == "18") { objReembolsoBE.Estado = "16"; estado = "15"; }
                if (estado == "16") { objReembolsoBE.Estado = "14"; estado = "13"; }
                if (estado == "14") { objReembolsoBE.Estado = "12"; estado = "11"; }
                objReembolsoBE.Comentario = txtComentario.Text;
                objReembolsoBC.ModificarReembolso(objReembolsoBE);

                UsuarioBC objUsuarioBC = new UsuarioBC();
                UsuarioBE objUsuarioBE = new UsuarioBE();
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);
                EnviarMensajeObservacion(objReembolsoBE.IdReembolso, "Reembolso", "Rendicion Reembolso: " + objReembolsoBE.CodigoReembolso, objReembolsoBE.CodigoReembolso, objUsuarioBE.CardName, estado, objReembolsoBE.IdUsuarioSolicitante);


            }
            else
                Mensaje("No a colocado ninguna observacion");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
            Response.Redirect("~/Reembolsos.aspx");
            bObservacion.Enabled = true;
        }
    }

    private void EnviarMensajeObservacion(int IdReembolso, string Documento, string Asunto, string CodigoReembolso, string UsuarioAprobador, string estado, int IdUsuarioSolicitante)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();

        if (estado == "11")
        {
            objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);
            MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto + " Observacion", objUsuarioBE.Mail);
        }

        if (estado == "13")
        {
            //objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante);
            //MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto + " Observacion", objUsuarioBE.Mail);

            lstUsuarioBE = objUsuarioBC.ListarUsuario(10, IdReembolso, 1);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto + " Observacion", lstUsuarioBE[i].Mail);
            }
        }

        if (estado == "15")
        {
            //objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante);
            //MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto + " Observacion", objUsuarioBE.Mail);

            lstUsuarioBE = objUsuarioBC.ListarUsuario(10, IdReembolso, 2);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto + " Observacion", lstUsuarioBE[i].Mail);
            }
        }
        else
        {
            //objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante);
            //MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto + " Observacion", objUsuarioBE.Mail);

            NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
            NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();
            objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(IdReembolso, 6);

            lstUsuarioBE = objUsuarioBC.ListarUsuario(10, IdReembolso, Convert.ToInt32(objNivelAprobacionBE.Nivel));
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de un " + Documento + " Codigo: " + CodigoReembolso, Asunto + " Observacion", lstUsuarioBE[i].Mail);
            }
        }
    }

    private void MensajeMail(string Cuerpo, string Asunto, string Destino)
    {
        if (Destino.Trim() != "")
        {
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            String email_body = "";
            correo.From = new System.Net.Mail.MailAddress("procesos.peru@tawa.com.pe");
            correo.To.Add(Destino.Trim());
            correo.Subject = Asunto;
            email_body = Cuerpo + ". Por favor ingresar al Portal Web para continuar con el proceso si fuera necesario.";
            correo.IsBodyHtml = true;
            correo.Body = email_body;
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
                Mensaje("Ocurrió un error (Reembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
            }
        }
    }

    private bool ValidarDatosExcel(List<ReembolsoDocumentoBE> lstReembolsoDocumentoBE)
    {
        for (int i = 0; i <= lstReembolsoDocumentoBE.Count - 1; i++)
        {
            if (lstReembolsoDocumentoBE[i].TipoDoc.Trim() == "" ||
               lstReembolsoDocumentoBE[i].SerieDoc.Trim() == "" ||
               lstReembolsoDocumentoBE[i].CorrelativoDoc.Trim() == "" ||
               //lstReembolsoDocumentoBE[i].FechaDoc.Trim() == "" ||
               //lstReembolsoDocumentoBE[i].IdProveedor.Trim() == "" ||
               //lstReembolsoDocumentoBE[i].IdConcepto.Trim() == "" ||
               //lstReembolsoDocumentoBE[i].IdCentroCostos3.Trim() == "" ||
               //lstReembolsoDocumentoBE[i].IdCentroCostos4.Trim() == "" ||
               //lstReembolsoDocumentoBE[i].IdCentroCostos5.Trim() == "" ||
               //lstReembolsoDocumentoBE[i].IdMonedaOriginal.Trim() == "" ||
               //lstReembolsoDocumentoBE[i].IdMonedaDoc.Trim() == "" ||
               lstReembolsoDocumentoBE[i].TasaCambio.Trim() == "" ||
               lstReembolsoDocumentoBE[i].MontoDoc.Trim() == "" ||
               lstReembolsoDocumentoBE[i].MontoIGV.Trim() == "" ||
               lstReembolsoDocumentoBE[i].MontoAfecto.Trim() == "" ||
               lstReembolsoDocumentoBE[i].MontoNoAfecto.Trim() == "" ||
               lstReembolsoDocumentoBE[i].MontoTotal.Trim() == ""
                )
                return false;
        }

        return true;
    }

    protected void Validar_Click(object sender, EventArgs e)
    {
        ProveedorBC objProveedorBC = new ProveedorBC();
        ProveedorBE objProveedorBE = new ProveedorBE();

        objProveedorBE = objProveedorBC.ObtenerProveedor(0, 1, txtProveedor.Text);
        if (objProveedorBE != null)
            lblProveedor.Text = objProveedorBE.CardName;
        else
            lblProveedor.Text = "Proveedor no existe.";
    }

    protected void ValidarImporte_Click(object sender, EventArgs e)
    {
        bool validacion = true;
        decimal n;
        bool isNumeric1, isNumeric2, isNumeric3;
        if (txtTasaCambio.Text.Trim() != "")
        {
            isNumeric1 = decimal.TryParse(txtTasaCambio.Text, out n);
            if (isNumeric1 == false) validacion = false;
        }
        else txtTasaCambio.Text = "1.00";

        if (txtMontoAfecta.Text.Trim() != "")
        {
            isNumeric2 = decimal.TryParse(txtMontoAfecta.Text, out n);
            if (isNumeric2 == false) validacion = false;
        }
        else txtMontoAfecta.Text = "0.00";

        if (txtMontoNoAfecta.Text.Trim() != "")
        {
            isNumeric3 = decimal.TryParse(txtMontoNoAfecta.Text, out n);
            if (isNumeric3 == false) validacion = false;
        }
        else txtMontoNoAfecta.Text = "0.00";

        if (validacion)
        {
            double MontoAfecta = Math.Round(Convert.ToDouble(txtMontoAfecta.Text), 2);
            double MontoNoAfecta = Math.Round(Convert.ToDouble(txtMontoNoAfecta.Text), 2);
            double MontoIGV = Math.Round(MontoAfecta * 0.18, 2);
            double MontoDoc = Math.Round(MontoAfecta + MontoNoAfecta + MontoIGV, 2);
            double MontoTotal = Math.Round(MontoDoc * Convert.ToDouble(txtTasaCambio.Text), 2);

            txtMontoIGV.Text = Math.Round(MontoIGV, 2).ToString("0.00");
            txtMontoDoc.Text = Math.Round(MontoDoc, 2).ToString("0.00");
            txtMontoTotal.Text = Math.Round(MontoTotal, 2).ToString("0.00");
            txtMontoAfecta.Text = Math.Round(Convert.ToDouble(txtMontoAfecta.Text), 2).ToString("0.00");
            txtMontoNoAfecta.Text = Math.Round(Convert.ToDouble(txtMontoNoAfecta.Text), 2).ToString("0.00");

            if (MontoDoc == 0) Mensaje("Monto Total debe ser mayor a 0.");
        }
        else
            Mensaje("Usted a ingresado los importes erroneamente.");
    }

    private bool ValidarImporte()
    {
        bool validacion = true;
        decimal n;
        bool isNumeric1, isNumeric2, isNumeric3;
        if (txtTasaCambio.Text.Trim() != "")
        {
            isNumeric1 = decimal.TryParse(txtTasaCambio.Text, out n);
            if (isNumeric1 == false) validacion = false;
        }
        else txtTasaCambio.Text = "1.0000";

        if (txtMontoAfecta.Text.Trim() != "")
        {
            isNumeric2 = decimal.TryParse(txtMontoAfecta.Text, out n);
            if (isNumeric2 == false) validacion = false;
        }
        else txtMontoAfecta.Text = "0.00";

        if (txtMontoNoAfecta.Text.Trim() != "")
        {
            isNumeric3 = decimal.TryParse(txtMontoNoAfecta.Text, out n);
            if (isNumeric3 == false) validacion = false;
        }
        else txtMontoNoAfecta.Text = "0.00";

        if (validacion)
        {
            double MontoAfecta = Math.Round(Convert.ToDouble(txtMontoAfecta.Text), 2);
            double MontoNoAfecta = Math.Round(Convert.ToDouble(txtMontoNoAfecta.Text), 2);
            double MontoIGV = Math.Round(MontoAfecta * 0.18, 2);
            double MontoDoc = Math.Round(MontoAfecta + MontoNoAfecta + MontoIGV, 2);
            double MontoTotal = Math.Round(MontoDoc * Convert.ToDouble(txtTasaCambio.Text), 2);

            txtMontoIGV.Text = Math.Round(MontoIGV, 2).ToString("0.00");
            txtMontoDoc.Text = Math.Round(MontoDoc, 2).ToString("0.00");
            txtMontoTotal.Text = Math.Round(MontoTotal, 2).ToString("0.00");
            txtMontoAfecta.Text = Math.Round(Convert.ToDouble(txtMontoAfecta.Text), 2).ToString("0.00");
            txtMontoNoAfecta.Text = Math.Round(Convert.ToDouble(txtMontoNoAfecta.Text), 2).ToString("0.00");

            if (MontoDoc == 0) validacion = false;
        }
        else validacion = false;

        return validacion;
    }

    protected void gvProveedor_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int IdProveedor;

        try
        {
            IdProveedor = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("Editar"))
            {
                lblIdProveedor.Text = IdProveedor.ToString();

                ProveedorBC objProveedorBC = new ProveedorBC();
                ProveedorBE objProveedorBE = new ProveedorBE();
                objProveedorBE = objProveedorBC.ObtenerProveedor(IdProveedor, 0, "");
                txtCardName.Text = objProveedorBE.CardName;
                txtDocumento.Text = objProveedorBE.Documento;

                bAgregar2.Visible = false;
                bGuardar2.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    protected void gridViewP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProveedor.PageIndex = e.NewPageIndex;
        ListarProveedorCrear();
    }

    protected void Agregar2_Click(object sender, EventArgs e)
    {
        try
        {
            bAgregar2.Enabled = false;

            if (txtCardName.Text.Trim() != "" && txtDocumento.Text.Trim() != "")
            {
                ProveedorBC objProveedorBC = new ProveedorBC();
                ProveedorBE objProveedorBE = new ProveedorBE();
                objProveedorBE = objProveedorBC.ObtenerProveedor(0, 1, txtDocumento.Text);

                if (objProveedorBE == null)
                {
                    objProveedorBE = new ProveedorBE();
                    objProveedorBE.CardCode = "P" + txtDocumento.Text;
                    objProveedorBE.CardName = txtCardName.Text;
                    objProveedorBE.TipoDocumento = "6";
                    objProveedorBE.Documento = txtDocumento.Text;

                    String strIdReembolso = "";
                    strIdReembolso = ViewState["IdReembolso"].ToString();

                    objProveedorBE.Proceso = 3;
                    objProveedorBE.IdProceso = Convert.ToInt32(strIdReembolso);
                    objProveedorBE.Estado = 1;

                    if (Session["Usuario"] == null)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    else
                    {
                        UsuarioBC objUsuarioBC = new UsuarioBC();
                        UsuarioBE objUsuarioBE = new UsuarioBE();
                        objUsuarioBE = (UsuarioBE)Session["Usuario"];
                        objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);

                        objProveedorBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                        objProveedorBE.CreateDate = DateTime.Now;
                        objProveedorBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                        objProveedorBE.UpdateDate = DateTime.Now;
                    }
                    int Id;
                    Id = objProveedorBC.InsertarProveedor(objProveedorBE);
                    ListarProveedorCrear();
                    txtCardName.Text = ""; txtDocumento.Text = "";
                }
                else
                    Mensaje("El RUC ya existe.");
            }
            else
                Mensaje("Es necesario ingresar toda la informacion;");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
            bAgregar2.Enabled = true;
        }
    }

    protected void Guardar2_Click(object sender, EventArgs e)
    {
        try
        {
            bGuardar2.Enabled = false;

            if (txtCardName.Text.Trim() != "" && txtDocumento.Text.Trim() != "")
            {
                ProveedorBC objProveedorBC = new ProveedorBC();
                ProveedorBE objProveedorBE = new ProveedorBE();

                objProveedorBE.IdProveedor = Convert.ToInt32(lblIdProveedor.Text);
                objProveedorBE.CardCode = "P" + txtDocumento.Text;
                objProveedorBE.CardName = txtCardName.Text;
                objProveedorBE.TipoDocumento = "6";
                objProveedorBE.Documento = txtDocumento.Text;

                String strIdReembolso = "";
                strIdReembolso = ViewState["IdReembolso"].ToString();

                objProveedorBE.Proceso = 3;
                objProveedorBE.IdProceso = Convert.ToInt32(strIdReembolso);
                objProveedorBE.Estado = 1;

                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    UsuarioBC objUsuarioBC = new UsuarioBC();
                    UsuarioBE objUsuarioBE = new UsuarioBE();
                    objUsuarioBE = (UsuarioBE)Session["Usuario"];
                    objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);

                    objProveedorBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objProveedorBE.CreateDate = DateTime.Now;
                    objProveedorBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objProveedorBE.UpdateDate = DateTime.Now;
                }
                objProveedorBC.ModificarProveedor(objProveedorBE);
                ListarProveedorCrear();
                txtCardName.Text = ""; txtDocumento.Text = "";

                bGuardar2.Visible = false;
                bAgregar2.Visible = true;
            }
            else
                Mensaje("Es necesario ingresar toda la informacion;");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
            bGuardar2.Enabled = true;
        }
    }

    protected void lnkExportarReporte_Click(object sender, EventArgs e)
    {
        try
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
            headerTable = headerTable + @"<tr><td><b>Total a Reembolsar:</b></td><td colspan=4>" + Label7.Text + "</td></tr>";
            headerTable = headerTable + @"<tr><td><b>Total Gastado:</b></td><td colspan=4>" + Label8.Text + "</td></tr>";
            headerTable = headerTable + @"<tr><td><b>Reembolso:</b></td><td colspan=4>" + Label9.Text + "</td></tr>";
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
        catch (Exception ex)
        {
            Mensaje("El Excel a guardar no debe estar abierto: " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    //public override void VerifyRenderingInServerForm(System.Windows.Forms.Control control)
    //{
    //    return;
    //}

    private void LlenarCamposCaberaExcel1()
    {
        String strIdReembolso = "";
        strIdReembolso = ViewState["IdReembolso"].ToString();

        ReembolsoBC objReembolsoBC = new ReembolsoBC();
        ReembolsoBE objReembolsoBE = new ReembolsoBE();
        objReembolsoBE = objReembolsoBC.ObtenerReembolso(Convert.ToInt32(strIdReembolso), 0);

        EmpresaBC objEmpresaBC = new EmpresaBC();
        EmpresaBE objEmpresaBE = new EmpresaBE();
        objEmpresaBE = objEmpresaBC.ObtenerEmpresa(objReembolsoBE.IdEmpresa);

        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = objUsuarioBC.ObtenerUsuario(objReembolsoBE.IdUsuarioSolicitante, 0);

        MotivoBC objMotivoBC = new MotivoBC();
        MotivoBE objMotivoBE = new MotivoBE();
        objMotivoBE = objMotivoBC.ObtenerMotivo(objReembolsoBE.IdMotivo);

        MonedaBC objMonedaBC = new MonedaBC();
        MonedaBE objMonedaBE = new MonedaBE();
        objMonedaBE = objMonedaBC.ObtenerMoneda(Convert.ToInt32(objReembolsoBE.Moneda));

        Label1.Text = objEmpresaBE.Descripcion;
        Label2.Text = objUsuarioBE.CardName;
        Label3.Text = objMotivoBE.Descripcion;
        Label4.Text = (objReembolsoBE.FechaSolicitud).ToString("dd/MM/yyyy");
        Label5.Text = (objReembolsoBE.UpdateDate).ToString("dd/MM/yyyy");
        Label6.Text = objMonedaBE.Descripcion;
        Label7.Text = objReembolsoBE.MontoInicial;
        Label8.Text = "";
        Label9.Text = objReembolsoBE.CodigoReembolso;
    }

    private void LlenarCamposCaberaExcel2()
    {
        String strIdReembolso = "";
        strIdReembolso = ViewState["IdReembolso"].ToString();

        ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
        List<ReembolsoDocumentoBE> lstReembolsoDocumentoBE = new List<ReembolsoDocumentoBE>();
        lstReembolsoDocumentoBE = objReembolsoDocumentoBC.ListarReembolsoDocumento(Convert.ToInt32(strIdReembolso), 3);

        Label8.Text = lstReembolsoDocumentoBE[0].MontoTotal;
        Label10.Text = lstReembolsoDocumentoBE[0].MontoTotal;
        Label11.Text = lstReembolsoDocumentoBE[0].MontoNoAfecto;
        Label12.Text = lstReembolsoDocumentoBE[0].MontoAfecto;
        Label13.Text = lstReembolsoDocumentoBE[0].MontoIGV;
        Label14.Text = lstReembolsoDocumentoBE[0].MontoDoc;
    }

    private void ListarRendicion2()
    {
        String strIdReembolso = "";
        strIdReembolso = ViewState["IdReembolso"].ToString();

        ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
        gvReporte.DataSource = objReembolsoDocumentoBC.ListarReembolsoDocumento(Convert.ToInt32(strIdReembolso), 2);
        gvReporte.DataBind();
    }

    protected void Masivo_Click(object sender, EventArgs e)
    {
        blbResultadoMasivo.Visible = true;
        txtCopied.Visible = true;
        GridView1.Visible = true;
        //bAgregar4.Visible = true;
        bPreliminar4.Visible = true;
        bCancelar4.Visible = true;
        bMasivo.Visible = false;
    }

    protected void Preliminar4_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[14] {
                    new DataColumn("Tipo_Documento", typeof(int)),
                    new DataColumn("Serie", typeof(string)),
                    new DataColumn("Numero",typeof(Int32)),
                    new DataColumn("Fecha",typeof(DateTime)),
                    new DataColumn("Ruc",typeof(string)),
                    new DataColumn("Razon_Social",typeof(string)),
                    new DataColumn("Concepto",typeof(int)),
                    new DataColumn("Moneda_Documento",typeof(int)),
                    new DataColumn("Tasa_Cambio",typeof(decimal)),
                    new DataColumn("No_Afecta",typeof(decimal)),
                    new DataColumn("Afecta",typeof(decimal)),
                    new DataColumn("IGV",typeof(decimal)),
                    new DataColumn("Total_Documento",typeof(decimal)),
                    new DataColumn("Total_Moneda_Origen",typeof(decimal))  });

            string copiedContent = Request.Form[txtCopied.UniqueID];
            foreach (string row in copiedContent.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split('\t'))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            txtCopied.Text = "";
            blbResultadoMasivo.Text = "Vista Preliminar cargada correctamente.";

            bAgregar4.Visible = true;
            bPreliminar4.Visible = false;
        }
        catch (Exception ex)
        {
            //Mensaje("Ocurrió un error (Prueba): " + ex.Message);
            blbResultadoMasivo.Text = "Ocurrió un error (Prueba): " + ex.Message;
            ExceptionHelper.LogException(ex);
        }
    }

    protected void Agregar4_Click(object sender, EventArgs e)
    {
        try
        {
            string mensajeError = "";
            bool validacion = true;

            int iRows = GridView1.Rows.Count;
            if (iRows <= 0)
            {
                validacion = false;
                mensajeError = "No existe informacion que subir.";
            }

            List<string> sRUC = new List<string>();
            List<string> sProveedor = new List<string>();
            List<int> sIdProveedor = new List<int>();
            if (validacion)
            {
                double TasaCambio, No_Afecta, Afecta, IGV, Total_Documento, Total_Moneda_Origen;

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    TasaCambio = Convert.ToDouble(GridView1.Rows[i].Cells[8].Text);
                    No_Afecta = Convert.ToDouble(GridView1.Rows[i].Cells[9].Text);
                    Afecta = Convert.ToDouble(GridView1.Rows[i].Cells[10].Text);
                    IGV = Convert.ToDouble(GridView1.Rows[i].Cells[11].Text);
                    Total_Documento = Convert.ToDouble(GridView1.Rows[i].Cells[12].Text);
                    Total_Moneda_Origen = Convert.ToDouble(GridView1.Rows[i].Cells[13].Text);

                    sRUC.Add(GridView1.Rows[i].Cells[4].Text);
                    sProveedor.Add(GridView1.Rows[i].Cells[5].Text);
                    if (Math.Round(Total_Documento, 2) != Math.Round(IGV + Afecta + No_Afecta, 2))
                    {
                        validacion = false;
                        mensajeError = "La suma del IGV, Afecata y NoAfecta no es igual al Total.";
                    }
                }
            }

            String strIdReembolso = "";
            strIdReembolso = ViewState["IdReembolso"].ToString();
            ProveedorBC objProveedorBC = new ProveedorBC();
            ProveedorBE objProveedorBE = new ProveedorBE();
            if (validacion)
            {
                for (int i = 0; i < sRUC.Count; i++)
                {
                    objProveedorBE = objProveedorBC.ObtenerProveedor(0, 1, sRUC[i]);
                    if (objProveedorBE == null)
                    {
                        objProveedorBE = new ProveedorBE();
                        objProveedorBE.CardCode = "P" + sRUC[i];
                        objProveedorBE.CardName = sProveedor[i];
                        objProveedorBE.TipoDocumento = "6";
                        objProveedorBE.Documento = sRUC[i];
                        objProveedorBE.Proceso = 3;
                        objProveedorBE.IdProceso = Convert.ToInt32(strIdReembolso);
                        objProveedorBE.Estado = 1;

                        if (Session["Usuario"] == null)
                        {
                            Response.Redirect("~/Login.aspx");
                        }
                        else
                        {
                            UsuarioBC objUsuarioBC = new UsuarioBC();
                            UsuarioBE objUsuarioBE = new UsuarioBE();
                            objUsuarioBE = (UsuarioBE)Session["Usuario"];
                            objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);

                            objProveedorBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                            objProveedorBE.CreateDate = DateTime.Now;
                            objProveedorBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                            objProveedorBE.UpdateDate = DateTime.Now;
                        }
                        int Id;
                        Id = objProveedorBC.InsertarProveedor(objProveedorBE);
                        sIdProveedor.Add(Id);
                    }
                    else
                    {
                        sIdProveedor.Add(objProveedorBE.IdProveedor);
                    }
                }
                ListarProveedorCrear();
            }

            if (validacion)
            {
                ReembolsoDocumentoBC objReembolsoDocumentoBC = new ReembolsoDocumentoBC();
                ReembolsoDocumentoBE objReembolsoDocumentoBE;

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    objReembolsoDocumentoBE = new ReembolsoDocumentoBE();
                    objReembolsoDocumentoBE.IdReembolso = Convert.ToInt32(strIdReembolso);
                    objReembolsoDocumentoBE.IdProveedor = Convert.ToInt32(sIdProveedor[i]);
                    objReembolsoDocumentoBE.IdConcepto = Convert.ToInt32(GridView1.Rows[i].Cells[6].Text);
                    objReembolsoDocumentoBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                    objReembolsoDocumentoBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                    objReembolsoDocumentoBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                    objReembolsoDocumentoBE.TipoDoc = GridView1.Rows[i].Cells[0].Text;
                    objReembolsoDocumentoBE.SerieDoc = GridView1.Rows[i].Cells[1].Text;
                    objReembolsoDocumentoBE.CorrelativoDoc = GridView1.Rows[i].Cells[2].Text;
                    objReembolsoDocumentoBE.FechaDoc = Convert.ToDateTime(GridView1.Rows[i].Cells[3].Text);
                    objReembolsoDocumentoBE.IdMonedaOriginal = Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value);
                    objReembolsoDocumentoBE.IdMonedaDoc = Convert.ToInt32(GridView1.Rows[i].Cells[7].Text);
                    objReembolsoDocumentoBE.TasaCambio = Convert.ToDouble(GridView1.Rows[i].Cells[8].Text).ToString("0.0000");
                    objReembolsoDocumentoBE.MontoNoAfecto = Convert.ToDouble(GridView1.Rows[i].Cells[9].Text).ToString("0.00");
                    objReembolsoDocumentoBE.MontoAfecto = Convert.ToDouble(GridView1.Rows[i].Cells[10].Text).ToString("0.00");
                    objReembolsoDocumentoBE.MontoIGV = Convert.ToDouble(GridView1.Rows[i].Cells[11].Text).ToString("0.00");
                    objReembolsoDocumentoBE.MontoTotal = Convert.ToDouble(GridView1.Rows[i].Cells[12].Text).ToString("0.00");
                    objReembolsoDocumentoBE.MontoDoc = Convert.ToDouble(GridView1.Rows[i].Cells[13].Text).ToString("0.00");
                    objReembolsoDocumentoBE.Estado = "1";

                    if (Session["Usuario"] == null)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    else
                    {
                        UsuarioBC objUsuarioBC = new UsuarioBC();
                        UsuarioBE objUsuarioBE = new UsuarioBE();
                        objUsuarioBE = (UsuarioBE)Session["Usuario"];
                        objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);

                        objReembolsoDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                        objReembolsoDocumentoBE.CreateDate = DateTime.Now;
                        objReembolsoDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                        objReembolsoDocumentoBE.UpdateDate = DateTime.Now;
                    }
                    int Id;
                    Id = objReembolsoDocumentoBC.InsertarReembolsoDocumento(objReembolsoDocumentoBE);
                }

                ListarRendicion();
                LlenarCabecera();
                LimpiarCampos();

                blbResultadoMasivo.Visible = false;
                blbResultadoMasivo.Text = "";
                txtCopied.Visible = false;
                txtCopied.Text = "";
                GridView1.Visible = false;
                bPreliminar4.Visible = false;
                bAgregar4.Visible = false;
                bCancelar4.Visible = false;
                bMasivo.Visible = true;

                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else
                Mensaje(mensajeError);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
        finally
        {
        }
    }

    protected void Cancelar4_Click(object sender, EventArgs e)
    {
        blbResultadoMasivo.Visible = false;
        blbResultadoMasivo.Text = "";
        txtCopied.Visible = false;
        txtCopied.Text = "";
        GridView1.Visible = false;
        bPreliminar4.Visible = false;
        bAgregar4.Visible = false;
        bCancelar4.Visible = false;
        bMasivo.Visible = true;

        GridView1.DataSource = null;
        GridView1.DataBind();
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
            //EnviarMensajeRechazado(objUsuarioBE.IdUsuario, objEntregaRendirBE.IdUsuarioCreador, objEntregaRendirBE.IdUsuarioSolicitante, "Entrega a Rendir", txtAsunto.Text, objEntregaRendirBE.CodigoEntregaRendir, objUsuarioBE.CardName);

            Response.Redirect("~/Reembolsos.aspx");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirReembolso): " + ex.Message);
            ExceptionHelper.LogException(ex);
        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
}