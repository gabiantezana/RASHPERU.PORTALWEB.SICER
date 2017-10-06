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

public partial class RendirEntregaRendir : System.Web.UI.Page
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
            String strIdEntregaRendir = "";

            if (!this.IsPostBack)
            {
                strModo = Context.Items["Modo"].ToString();
                strIdEntregaRendir = Context.Items["IdEntregaRendir"].ToString();

                ViewState["Modo"] = strModo;
                ViewState["IdEntregaRendir"] = strIdEntregaRendir;

                ListarTipoDocumento();
                ListarProveedor();
                ListarProveedorCrear();
                ListarCentroCostos();
                ListarConcepto();
                ListarRendicion();
                ListarMoneda(Convert.ToInt32(strIdEntregaRendir));
                ListarProveedorCrear();
                Modalidad(Convert.ToInt32(strModo));
                ModalidadCampo(Convert.ToInt32(strModo), Convert.ToInt32(strIdEntregaRendir));
                LlenarCamposCaberaExcel1();

                EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
                EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
                objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);
                txtComentario.Text = objEntregaRendirBE.Comentario;

                if (objEntregaRendirBE.Estado == "19")
                    txtFechaContabilizacion.Text = txtFechaContabilizacion.Text = (objEntregaRendirBE.FechaContabilizacion).ToString("dd/MM/yyyy");
                else
                    txtFechaContabilizacion.Text = txtFechaContabilizacion.Text = (DateTime.Today).ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
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
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
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
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
        }
    }

    private void ListarCentroCostos()
    {
        try
        {
            CentroCostosBC objCentroCostosBC = new CentroCostosBC();

            String strIdEntregaRendir = "";
            strIdEntregaRendir = Context.Items["IdEntregaRendir"].ToString();
            EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
            EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
            objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);

            ddlCentroCostos3.DataSource = objCentroCostosBC.ListarCentroCostos(objEntregaRendirBE.IdUsuarioSolicitante, 8, objEntregaRendirBE.IdEmpresa);
            ddlCentroCostos3.DataTextField = "Descripcion";
            ddlCentroCostos3.DataValueField = "IdCentroCostos";
            ddlCentroCostos3.DataBind();

            ddlCentroCostos4.DataSource = objCentroCostosBC.ListarCentroCostos(objEntregaRendirBE.IdCentroCostos3, 9, objEntregaRendirBE.IdEmpresa);
            ddlCentroCostos4.DataTextField = "Descripcion";
            ddlCentroCostos4.DataValueField = "IdCentroCostos";
            ddlCentroCostos4.DataBind();

            ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(objEntregaRendirBE.IdCentroCostos4, 11, objEntregaRendirBE.IdEmpresa);
            ddlCentroCostos5.DataTextField = "Descripcion";
            ddlCentroCostos5.DataValueField = "IdCentroCostos";
            ddlCentroCostos5.DataBind();

            ddlCentroCostos3.SelectedValue = objEntregaRendirBE.IdCentroCostos3.ToString();
            ddlCentroCostos4.SelectedValue = objEntregaRendirBE.IdCentroCostos4.ToString();
            ddlCentroCostos5.SelectedValue = objEntregaRendirBE.IdCentroCostos5.ToString();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
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
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
        }
    }

    private void ListarRendicion()
    {
        String strIdEntregaRendir = "";
        strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

        EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
        gvEntregaRendir.DataSource = objEntregaRendirDocumentoBC.ListarEntregaRendirDocumento(Convert.ToInt32(strIdEntregaRendir), 1);
        gvEntregaRendir.DataBind();

        if (gvEntregaRendir.Rows.Count > 0) bEnviar.Visible = true;
        else bEnviar.Visible = false;
    }

    private void ListarMoneda(int IdEntregaRendir)
    {
        MonedaBC objMonedaBC = new MonedaBC();

        ddlIdMonedaDoc.DataSource = objMonedaBC.ListarMoneda(0, 1);
        ddlIdMonedaDoc.DataTextField = "Descripcion";
        ddlIdMonedaDoc.DataValueField = "IdMoneda";
        ddlIdMonedaDoc.DataBind();

        ddlIdMonedaPago.DataSource = objMonedaBC.ListarMoneda(0, 1);
        ddlIdMonedaPago.DataTextField = "Descripcion";
        ddlIdMonedaPago.DataValueField = "IdMoneda";
        ddlIdMonedaPago.DataBind();

        ddlIdMonedaOriginal.DataSource = objMonedaBC.ListarMoneda(IdEntregaRendir, 3);
        ddlIdMonedaOriginal.DataTextField = "Descripcion";
        ddlIdMonedaOriginal.DataValueField = "IdMoneda";
        ddlIdMonedaOriginal.DataBind();
    }

    private void ListarProveedorCrear()
    {
        String strIdEntregaRendir = "";
        strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

        ProveedorBC objProveedorBC = new ProveedorBC();
        gvProveedor.DataSource = objProveedorBC.ListarProveedor(Convert.ToInt32(strIdEntregaRendir), 3);
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
                    //lblCabezera.Text = "Aprobar Entrega Rendir";
                    //bCrear.Text = "Guardar";
                    //LlenarCampos(Convert.ToInt32(ViewState["IdEntregaRendir"].ToString()));
                    break;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
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

            NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
            NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();
            objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(objEntregaRendirBE.IdEntregaRendir, 5);

            //UsuarioAreaNivelBC objUsuarioAreaNivelBC = new UsuarioAreaNivelBC();
            //UsuarioAreaNivelBE objUsuarioAreaNivelBE = new UsuarioAreaNivelBE();
            //objUsuarioAreaNivelBE = objUsuarioAreaNivelBC.ObtenerUsuarioAreaNivel(objUsuarioBE.IdUsuario, 2, IdEntregaRendir);

            //NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
            //NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();
            //NivelAprobacionBE objNivelAprobacionBE2 = new NivelAprobacionBE();
            //if (objUsuarioAreaNivelBE != null)
            //{
            //    objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(objUsuarioAreaNivelBE.IdNivelAprobacion, 0);
            //    objNivelAprobacionBE2 = objNivelAprobacionBC.ObtenerNivelAprobacion(objEntregaRendirBE.IdEntregaRendir, 5); //ultimo nivel ER
            //}

            gvEntregaRendir.Columns[0].Visible = false;
            gvEntregaRendir.Columns[1].Visible = false;
            gvEntregaRendir.Columns[2].Visible = false;
            bAgregar.Visible = false;
            bGuardar.Visible = false;
            bCancelar.Visible = true;
            bMasivo.Visible = false;
            bAgregar3.Visible = false;
            bGuardar3.Visible = false;
            bAgregar2.Visible = false;
            bGuardar2.Visible = false;
            lblComentario.Visible = true;
            txtComentario.Visible = true;
            bEnviar.Visible = false;
            bAprobar.Visible = false;
            bObservacion.Visible = false;

            //11, Rendir: Por Aprobar Nivel 1/ 12, Rendir: Observaciones  Nivel 1
            //13, Rendir: Por Aprobar Nivel 2/ 14, Rendir: Observaciones  Nivel 2
            //15, Rendir: Por Aprobar Nivel 3/ 16, Rendir: Observaciones  Nivel 3
            //17, Rendir: Por Aprobar Contabilidad/ 18, Rendir: Observaciones Contabilidad/ 19, Rendir: Aprobado

            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objEntregaRendirBE.Estado == "4")//Aprobado
            {
                if (objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                {
                    if (objUsuarioSesionBE.IdUsuario == objEntregaRendirBE.IdUsuarioCreador)
                    {
                        gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objEntregaRendirBE.Estado == "11")//11, Rendir: Por Aprobar Nivel 1
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                {
                    if (objUsuarioSolicitanteBE.IdUsuarioER1 == objUsuarioSesionBE.IdUsuario)
                    {
                        gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objEntregaRendirBE.Estado == "12")//12, Rendir: Observaciones  Nivel 1
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                {
                    if (objEntregaRendirBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario)
                    {
                        gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                    if (objEntregaRendirBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
                    {
                        gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objEntregaRendirBE.Estado == "13")//13, Rendir: Por Aprobar Nivel 2
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                {
                    if (objUsuarioSolicitanteBE.IdUsuarioER2 == objUsuarioSesionBE.IdUsuario)
                    {
                        gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objEntregaRendirBE.Estado == "14")//14, Rendir: Observaciones  Nivel 2
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                {
                    if (objEntregaRendirBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario)
                    {
                        gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                    if (objEntregaRendirBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
                    {
                        gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objEntregaRendirBE.Estado == "15")//15, Rendir: Por Aprobar Nivel 3
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                {
                    if (objUsuarioSolicitanteBE.IdUsuarioER3 == objUsuarioSesionBE.IdUsuario)
                    {
                        gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objEntregaRendirBE.Estado == "16")//14, Rendir: Observaciones  Nivel 3
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                {
                    if (objEntregaRendirBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario)
                    {
                        gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                    if (objEntregaRendirBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
                    {
                        gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objEntregaRendirBE.Estado == "17")//17, Rendir: Por Aprobar Contabilidad
            {
                if (objPerfilUsuarioBE.TipoAprobador == "2" || objPerfilUsuarioBE.TipoAprobador == "5")
                {
                    gvEntregaRendir.Columns[0].Visible = true; gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                    bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true; txtFechaContabilizacion.Enabled = true;
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objEntregaRendirBE.Estado == "18")//18, Rendir: Observaciones Contabilidad
            {
                if (objUsuarioSesionBE.IdUsuario == objEntregaRendirBE.IdUsuarioCreador)
                {
                    gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                    bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                }
                //if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                //{
                //    if (objNivelAprobacionBE.Nivel == "3")
                //        if (objUsuarioSolicitanteBE.IdUsuarioER3 == objUsuarioSesionBE.IdUsuario)
                //        {
                //            gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                //            bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
                //        }
                //    if (objNivelAprobacionBE.Nivel == "2")
                //        if (objUsuarioSolicitanteBE.IdUsuarioER2 == objUsuarioSesionBE.IdUsuario)
                //        {
                //            gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                //            bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
                //        }
                //    if (objNivelAprobacionBE.Nivel == "1")
                //        if (objUsuarioSolicitanteBE.IdUsuarioER1 == objUsuarioSesionBE.IdUsuario)
                //        {
                //            gvEntregaRendir.Columns[1].Visible = true; gvEntregaRendir.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                //            bMasivo.Visible = true; bAgregar3.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
                //        }
                //}
            }
        }
    }

    protected void gvEntregaRendir_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int IdEntregaRendirDocumento;

        try
        {
            IdEntregaRendirDocumento = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("Editar"))
            {
                lblIdEntregaRendirDocumento.Text = IdEntregaRendirDocumento.ToString();

                EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
                EntregaRendirDocumentoBE objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
                objEntregaRendirDocumentoBE = objEntregaRendirDocumentoBC.ObtenerEntregaRendirDocumento(IdEntregaRendirDocumento, 0);

                if (objEntregaRendirDocumentoBE.TipoDoc != "7")
                {
                    txtSerie.Text = objEntregaRendirDocumentoBE.SerieDoc;
                    txtNumero.Text = objEntregaRendirDocumentoBE.CorrelativoDoc;
                    txtFecha.Text = objEntregaRendirDocumentoBE.FechaDoc.ToString().Substring(0, 10);
                    txtMontoTotal.Text = Convert.ToDouble(objEntregaRendirDocumentoBE.MontoTotal).ToString("0.00");
                    txtMontoDoc.Text = Convert.ToDouble(objEntregaRendirDocumentoBE.MontoDoc).ToString("0.00");
                    txtMontoAfecta.Text = Convert.ToDouble(objEntregaRendirDocumentoBE.MontoAfecto).ToString("0.00");
                    txtMontoNoAfecta.Text = Convert.ToDouble(objEntregaRendirDocumentoBE.MontoNoAfecto).ToString("0.00");
                    txtMontoIGV.Text = Convert.ToDouble(objEntregaRendirDocumentoBE.MontoIGV).ToString("0.00");
                    txtTasaCambio.Text = Convert.ToDouble(objEntregaRendirDocumentoBE.TasaCambio).ToString("0.0000");
                    if (objEntregaRendirDocumentoBE.IdMonedaDoc == objEntregaRendirDocumentoBE.IdMonedaOriginal) txtTasaCambio.Enabled = false;
                    else txtTasaCambio.Enabled = false;
                    ddlTipo.SelectedValue = objEntregaRendirDocumentoBE.TipoDoc.ToString();

                    ProveedorBC objProveedorBC = new ProveedorBC();
                    ProveedorBE objProveedorBE = new ProveedorBE();
                    objProveedorBE = objProveedorBC.ObtenerProveedor(objEntregaRendirDocumentoBE.IdProveedor, 0, "");
                    txtProveedor.Text = objProveedorBE.Documento;
                    lblProveedor.Text = objProveedorBE.CardName;

                    try { ddlConcepto.SelectedValue = objEntregaRendirDocumentoBE.IdConcepto.ToString(); }
                    catch { ddlConcepto.SelectedValue = "0"; }
                    ddlIdMonedaDoc.SelectedValue = objEntregaRendirDocumentoBE.IdMonedaDoc.ToString();
                    ddlIdMonedaOriginal.SelectedValue = objEntregaRendirDocumentoBE.IdMonedaOriginal.ToString();

                    ddlCentroCostos3.SelectedValue = objEntregaRendirDocumentoBE.IdCentroCostos3.ToString();
                    ddlCentroCostos4.SelectedValue = objEntregaRendirDocumentoBE.IdCentroCostos4.ToString();
                    ddlCentroCostos5.SelectedValue = objEntregaRendirDocumentoBE.IdCentroCostos5.ToString();

                    bAgregar.Visible = false;
                    bGuardar.Visible = true;
                }
                else
                {
                    ddlIdMonedaPago.SelectedValue = objEntregaRendirDocumentoBE.IdMonedaDoc.ToString();
                    txtTasaCambioPago.Text = objEntregaRendirDocumentoBE.TasaCambio;
                    txtMontoPago.Text = objEntregaRendirDocumentoBE.MontoDoc;
                    txtMontoTotalPago.Text = objEntregaRendirDocumentoBE.MontoTotal;
                    txtFechaPago.Text = objEntregaRendirDocumentoBE.FechaDoc.ToString().Substring(0, 10);

                    BancoBC objBancoBC = new BancoBC();
                    List<BancoBE> lstBancoBE = new List<BancoBE>();
                    lstBancoBE = objBancoBC.ListarBanco(objEntregaRendirDocumentoBE.IdEntregaRendir, 1, Convert.ToInt32(ddlIdMonedaPago.SelectedItem.Value));
                    ddlIdBanco.Items.Clear();
                    ddlIdBanco.DataSource = lstBancoBE;
                    ddlIdBanco.DataTextField = "Descripcion";
                    ddlIdBanco.DataValueField = "IdBanco";
                    ddlIdBanco.DataBind();
                    ddlIdBanco.Enabled = true;
                    ddlIdBanco.SelectedValue = objEntregaRendirDocumentoBE.IdProveedor.ToString();

                    bAgregar3.Visible = false;
                    bGuardar3.Visible = true;
                }
            }
            if (e.CommandName.Equals("Eliminar"))
            {
                EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
                objEntregaRendirDocumentoBC.EliminarEntregaRendirDocumento(IdEntregaRendirDocumento);
                ListarRendicion();
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEntregaRendir.PageIndex = e.NewPageIndex;
        ListarRendicion();
    }

    private void LlenarCabecera()
    {
        String strIdEntregaRendir = "";
        strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

        EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
        EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
        objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);

        EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
        EntregaRendirDocumentoBE objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
        objEntregaRendirDocumentoBE = objEntregaRendirDocumentoBC.ObtenerEntregaRendirDocumento(Convert.ToInt32(strIdEntregaRendir), 1);
        string montoCCD = "0.00";
        if (objEntregaRendirDocumentoBE != null) montoCCD = objEntregaRendirDocumentoBE.MontoTotal;
        lblCabezera.Text = "Entrega Rendir: " + objEntregaRendirBE.CodigoEntregaRendir + " - Monto: " + montoCCD + "/" + Convert.ToDouble(objEntregaRendirBE.MontoInicial).ToString("0.00");

        if (objEntregaRendirBE.Estado == "19")
            txtFechaContabilizacion.Text = txtFechaContabilizacion.Text = (objEntregaRendirBE.FechaContabilizacion).ToString("dd/MM/yyyy");
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

    private void LimpiarCampos2()
    {
        ddlIdMonedaPago.SelectedValue = "0";
        txtTasaCambioPago.Text = "";
        txtMontoPago.Text = "";
        txtMontoTotalPago.Text = "";
        txtFechaPago.Text = "";
        ddlIdBanco.SelectedValue = "0";
        ddlIdBanco.Enabled = false;
    }

    protected void ddlCentroCosto3_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCentroCostos3.SelectedValue != "0")
        //{
        //    CentroCostosBC objConceptoBC = new CentroCostosBC();
        //    ddlCentroCostos4.DataSource = objConceptoBC.ListarCentroCostos(Convert.ToInt32(ddlCentroCostos3.SelectedValue), 2);
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

    protected void ddlIdMonedaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdMonedaPago.SelectedValue != "0")
        {
            if (ddlIdMonedaOriginal.SelectedValue == ddlIdMonedaPago.SelectedValue)
            {
                txtTasaCambioPago.Text = "1.0000";
                txtTasaCambioPago.Enabled = false;
            }
            else
            {
                txtTasaCambioPago.Enabled = true;
            }

            String strIdEntregaRendir = "";
            strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();
            BancoBC objBancoBC = new BancoBC();
            List<BancoBE> lstBancoBE = new List<BancoBE>();
            lstBancoBE = objBancoBC.ListarBanco(Convert.ToInt32(strIdEntregaRendir), 1, Convert.ToInt32(ddlIdMonedaPago.SelectedItem.Value));
            ddlIdBanco.Items.Clear();
            ddlIdBanco.DataSource = lstBancoBE;
            ddlIdBanco.DataTextField = "Descripcion";
            ddlIdBanco.DataValueField = "IdBanco";
            ddlIdBanco.DataBind();
            ddlIdBanco.Enabled = true;
        }
        else
        {
            ddlIdBanco.Enabled = false;
            txtTasaCambioPago.Enabled = true;
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
        if (gvEntregaRendir.Columns[0].Visible == true)
        {
            System.Web.UI.WebControls.CheckBox checkbox = (System.Web.UI.WebControls.CheckBox)sender;
            GridViewRow row = (GridViewRow)checkbox.NamingContainer;
            int Id = Convert.ToInt32(gvEntregaRendir.Rows[row.DataItemIndex].Cells[2].Text);

            EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
            EntregaRendirDocumentoBE objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
            objEntregaRendirDocumentoBE = objEntregaRendirDocumentoBC.ObtenerEntregaRendirDocumento(Id, 0);

            if (checkbox.Checked == true) objEntregaRendirDocumentoBE.Estado = "1";
            else objEntregaRendirDocumentoBE.Estado = "2";
            objEntregaRendirDocumentoBC.ModificarEntregaRendirDocumento(objEntregaRendirDocumentoBE);
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

    public String SetearProveedorRUC(String sIdProveedor, String sTipoDoc)
    {
        if (sTipoDoc == "7")
        {
            BancoBC objBancoBC = new BancoBC();
            BancoBE objBancoBE = new BancoBE();
            objBancoBE = objBancoBC.ObtenerBanco(Convert.ToInt32(sIdProveedor));
            return objBancoBE.Cuenta;
        }
        else
        {
            ProveedorBC objProveedorBC = new ProveedorBC();
            ProveedorBE objProveedorBE = new ProveedorBE();
            objProveedorBE = objProveedorBC.ObtenerProveedor(Convert.ToInt32(sIdProveedor), 0, "");
            if (objProveedorBE != null) return objProveedorBE.Documento;
            else return "";
        }
    }

    public String SetearProveedor(String sIdProveedor, String sTipoDoc)
    {
        if (sTipoDoc == "7")
        {
            BancoBC objBancoBC = new BancoBC();
            BancoBE objBancoBE = new BancoBE();
            objBancoBE = objBancoBC.ObtenerBanco(Convert.ToInt32(sIdProveedor));
            return objBancoBE.Descripcion;
        }
        else
        {
            ProveedorBC objProveedorBC = new ProveedorBC();
            ProveedorBE objProveedorBE = new ProveedorBE();
            objProveedorBE = objProveedorBC.ObtenerProveedor(Convert.ToInt32(sIdProveedor), 0, "");
            if (objProveedorBE != null) return objProveedorBE.CardName;
            else return "";
        }
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
                String strIdEntregaRendir = "";
                strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

                EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
                EntregaRendirDocumentoBE objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
                objEntregaRendirDocumentoBE.IdEntregaRendir = Convert.ToInt32(strIdEntregaRendir);

                objEntregaRendirDocumentoBE.IdProveedor = Convert.ToInt32(objProveedorBE.IdProveedor);

                objEntregaRendirDocumentoBE.IdConcepto = Convert.ToInt32(ddlConcepto.SelectedItem.Value);
                objEntregaRendirDocumentoBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objEntregaRendirDocumentoBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objEntregaRendirDocumentoBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objEntregaRendirDocumentoBE.TipoDoc = ddlTipo.SelectedItem.Value;
                objEntregaRendirDocumentoBE.SerieDoc = txtSerie.Text;
                objEntregaRendirDocumentoBE.CorrelativoDoc = txtNumero.Text;
                objEntregaRendirDocumentoBE.FechaDoc = Convert.ToDateTime(txtFecha.Text);
                objEntregaRendirDocumentoBE.IdMonedaOriginal = Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value);
                objEntregaRendirDocumentoBE.IdMonedaDoc = Convert.ToInt32(ddlIdMonedaDoc.SelectedItem.Value);
                objEntregaRendirDocumentoBE.MontoDoc = Convert.ToDouble(txtMontoDoc.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoIGV = Convert.ToDouble(txtMontoIGV.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoAfecto = Convert.ToDouble(txtMontoAfecta.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoNoAfecto = Convert.ToDouble(txtMontoNoAfecta.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoTotal = Convert.ToDouble(txtMontoTotal.Text).ToString("0.00");

                if (ddlIdMonedaOriginal.SelectedValue == ddlIdMonedaDoc.SelectedValue) objEntregaRendirDocumentoBE.TasaCambio = "1.0000";
                else objEntregaRendirDocumentoBE.TasaCambio = Convert.ToDouble(txtTasaCambio.Text).ToString("0.0000");

                objEntregaRendirDocumentoBE.Estado = "1";

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

                    objEntregaRendirDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirDocumentoBE.CreateDate = DateTime.Now;
                    objEntregaRendirDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirDocumentoBE.UpdateDate = DateTime.Now;
                }
                int Id;
                Id = objEntregaRendirDocumentoBC.InsertarEntregaRendirDocumento(objEntregaRendirDocumentoBE);
                ListarRendicion();
                LlenarCabecera();
                LimpiarCampos();
            }
            else
                Mensaje(mensajeError);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
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
                objDocumentoBE = objDocumentoBC.ObtenerDocumento(Convert.ToInt32(ddlTipo.SelectedItem.Value));
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
                String strIdEntregaRendir = "";
                strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

                EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
                EntregaRendirDocumentoBE objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
                objEntregaRendirDocumentoBE.IdEntregaRendirDocumento = Convert.ToInt32(lblIdEntregaRendirDocumento.Text);
                objEntregaRendirDocumentoBE.IdEntregaRendir = Convert.ToInt32(strIdEntregaRendir);

                objEntregaRendirDocumentoBE.IdProveedor = Convert.ToInt32(objProveedorBE.IdProveedor);

                objEntregaRendirDocumentoBE.IdConcepto = Convert.ToInt32(ddlConcepto.SelectedItem.Value);
                objEntregaRendirDocumentoBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objEntregaRendirDocumentoBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objEntregaRendirDocumentoBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objEntregaRendirDocumentoBE.TipoDoc = ddlTipo.SelectedItem.Value;
                objEntregaRendirDocumentoBE.SerieDoc = txtSerie.Text;
                objEntregaRendirDocumentoBE.CorrelativoDoc = txtNumero.Text;
                objEntregaRendirDocumentoBE.FechaDoc = Convert.ToDateTime(txtFecha.Text);
                objEntregaRendirDocumentoBE.IdMonedaOriginal = Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value);
                objEntregaRendirDocumentoBE.IdMonedaDoc = Convert.ToInt32(ddlIdMonedaDoc.SelectedItem.Value);
                objEntregaRendirDocumentoBE.MontoDoc = Convert.ToDouble(txtMontoDoc.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoIGV = Convert.ToDouble(txtMontoIGV.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoAfecto = Convert.ToDouble(txtMontoAfecta.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoNoAfecto = Convert.ToDouble(txtMontoNoAfecta.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoTotal = Convert.ToDouble(txtMontoTotal.Text).ToString("0.00");

                if (ddlIdMonedaOriginal.SelectedValue == ddlIdMonedaDoc.SelectedValue) objEntregaRendirDocumentoBE.TasaCambio = "1.0000";
                else objEntregaRendirDocumentoBE.TasaCambio = Convert.ToDouble(txtTasaCambio.Text).ToString("0.0000");

                objEntregaRendirDocumentoBE.Estado = "1";

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

                    objEntregaRendirDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirDocumentoBE.CreateDate = DateTime.Now;
                    objEntregaRendirDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirDocumentoBE.UpdateDate = DateTime.Now;
                }

                objEntregaRendirDocumentoBC.ModificarEntregaRendirDocumento(objEntregaRendirDocumentoBE);
                ListarRendicion();
                LlenarCabecera();
                LimpiarCampos();

                lblIdEntregaRendirDocumento.Text = "";

                bAgregar.Visible = true;
                bGuardar.Visible = false;

                EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
                EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
                objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);
                if (objEntregaRendirBE.Estado == "4") bEnviar.Visible = true;
                else bEnviar.Visible = false;
            }
            else
                Mensaje(mensajeError);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
        }
        finally
        {
            bGuardar.Enabled = true;
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

    protected void Enviar_Click(object sender, EventArgs e)
    {
        try
        {
            bEnviar.Enabled = false;
            bool validarcion1 = true;
            if (gvEntregaRendir.Rows.Count > 0)
            {
                String strIdEntregaRendir = "";
                strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();
                String estado = "0";

                //VALIDAR TASA DE CAMBIO
                EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
                EntregaRendirDocumentoBE objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
                objEntregaRendirDocumentoBE = objEntregaRendirDocumentoBC.ObtenerEntregaRendirDocumento(Convert.ToInt32(strIdEntregaRendir), 2);
                //VALIDAR TASA DE CAMBIO
                if (objEntregaRendirDocumentoBE == null)
                {
                    EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
                    EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
                    objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);
                    estado = objEntregaRendirBE.Estado;
                    if (objEntregaRendirBE.Estado == "4")
                    {
                        objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
                        objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
                        objEntregaRendirDocumentoBE = objEntregaRendirDocumentoBC.ObtenerEntregaRendirDocumento(Convert.ToInt32(strIdEntregaRendir), 1);
                        if (Convert.ToDouble(objEntregaRendirDocumentoBE.MontoTotal) < Convert.ToDouble(objEntregaRendirBE.MontoInicial))
                            validarcion1 = false;

                        objEntregaRendirBE.Estado = "11";
                    }

                    if (validarcion1)
                    {
                        if (objEntregaRendirBE.Estado == "12") objEntregaRendirBE.Estado = "11";
                        if (objEntregaRendirBE.Estado == "14") objEntregaRendirBE.Estado = "13";
                        if (objEntregaRendirBE.Estado == "16") objEntregaRendirBE.Estado = "15";
                        if (objEntregaRendirBE.Estado == "18") objEntregaRendirBE.Estado = "17";
                        objEntregaRendirBC.ModificarEntregaRendir(objEntregaRendirBE);

                        UsuarioBC objUsuarioBC = new UsuarioBC();
                        UsuarioBE objUsuarioBE = new UsuarioBE();
                        objUsuarioBE = objUsuarioBC.ObtenerUsuario(objEntregaRendirBE.IdUsuarioSolicitante, 0);
                        EnviarMensajeParaAprobador(objEntregaRendirBE.IdEntregaRendir, "Entrega Rendir", "Rendicion Entrega Rendir: " + objEntregaRendirBE.CodigoEntregaRendir, objEntregaRendirBE.CodigoEntregaRendir, objUsuarioBE.CardName, estado, objEntregaRendirBE.IdUsuarioSolicitante);

                        Response.Redirect("~/EntregasRendir.aspx");
                    }
                    else
                        Mensaje("El monto rendido es menor al monto solicitado.");
                }
                else
                    Mensaje("El documento Serie: " + objEntregaRendirDocumentoBE.SerieDoc + " Numero: " + objEntregaRendirDocumentoBE.CorrelativoDoc + " presenta la fecha de documento: " + objEntregaRendirDocumentoBE.FechaDoc + " la cual aun no existe SAP y su tasa de cambio tampoco. Por favor contactarse con Contabilidad y/o Sistemas.");
            }
            else
                Mensaje("Aun no se ah rendido ningun documento.");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
        }
        finally
        {
            bEnviar.Enabled = true;
        }
    }

    private void EnviarMensajeParaAprobador(int IdEntregaRendir, string Documento, string Asunto, string CodigoEntregaRendir, string UsuarioSolicitante, string estado, int IdUsuarioSolicitante)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();

        if (estado == "4" || estado == "12")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, 1);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El usuario " + UsuarioSolicitante + " ha realizado la rendicion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto, lstUsuarioBE[i].Mail);
            }
        }
        else
        {
            if (estado == "14")
            {
                lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, 2);
                for (int i = 0; i < lstUsuarioBE.Count; i++)
                {
                    MensajeMail("El usuario " + UsuarioSolicitante + " ha realizado la rendicion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto, lstUsuarioBE[i].Mail);
                }
            }
            else
            {
                if (estado == "16")
                {
                    lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, 3);
                    for (int i = 0; i < lstUsuarioBE.Count; i++)
                    {
                        MensajeMail("El usuario " + UsuarioSolicitante + " ha realizado la rendicion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto, lstUsuarioBE[i].Mail);
                    }
                }
                else
                {
                    lstUsuarioBE = objUsuarioBC.ListarUsuario(5, 0, 0);
                    for (int i = 0; i < lstUsuarioBE.Count; i++)
                    {
                        MensajeMail("El usuario " + UsuarioSolicitante + " ha realizado la rendicion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto, lstUsuarioBE[i].Mail);
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

            bAprobar.Enabled = false;
            bool validar = true;
            String mensaje = "";

            String strIdEntregaRendir = "";
            strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();
            String estado = "0";

            EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
            EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
            objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);
            estado = objEntregaRendirBE.Estado;

            NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
            NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();
            objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(objEntregaRendirBE.IdEntregaRendir, 5);
            if (estado == "11")
            {
                if (objNivelAprobacionBE.Nivel == "1") objEntregaRendirBE.Estado = "17";
                else objEntregaRendirBE.Estado = "13";
            }
            if (estado == "12")
            {
                objEntregaRendirBE.Estado = "11";
                estado = "11";
            }
            if (estado == "13")
            {
                if (objNivelAprobacionBE.Nivel == "2") objEntregaRendirBE.Estado = "17";
                else objEntregaRendirBE.Estado = "15";
            }
            if (estado == "15")
            {
                if (objNivelAprobacionBE.Nivel == "3") objEntregaRendirBE.Estado = "17";
                else objEntregaRendirBE.Estado = "17";
            }
            if (estado == "17")
            {

                if (txtFechaContabilizacion.Text.Trim() != "")
                {
                    objEntregaRendirBE.FechaContabilizacion = Convert.ToDateTime(txtFechaContabilizacion.Text);
                    objEntregaRendirBE.Estado = "19";

                    EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
                    EntregaRendirDocumentoBE objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
                    objEntregaRendirDocumentoBE = objEntregaRendirDocumentoBC.ObtenerEntregaRendirDocumento(Convert.ToInt32(strIdEntregaRendir), 1);
                    objEntregaRendirBE.MontoGastado = objEntregaRendirDocumentoBE.MontoTotal;
                    objEntregaRendirBE.MontoReembolsado = (Convert.ToDouble(objEntregaRendirDocumentoBE.MontoTotal) - Convert.ToDouble(objEntregaRendirBE.MontoInicial)).ToString("0.00");
                    objEntregaRendirBE.MontoActual = "0.00";//(Convert.ToDouble(objEntregaRendirBE.MontoInicial) - Convert.ToDouble(objEntregaRendirDocumentoBE.MontoTotal)).ToString("0.00");


                    //Recupero el usuario conectado
                    UsuarioBE objUsuarioBE1 = new UsuarioBE();
                    objUsuarioBE1 = (UsuarioBE)Session["Usuario"];
                    String UsuarioConectado = objUsuarioBE1.IdUsuario.ToString();

                    objEntregaRendirBE.UserUpdate = UsuarioConectado;


                    //Recupero la IP del cliente
                    String IP = GetIpAddress();
                    //Recupero el hostname del cliente
                    string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
                    String HOSTNAME = System.Environment.MachineName;
                    String HOSTNAME1 = computer_name[0].ToString();

                    ControlContabilidadBE oControlContabilidadBE = new ControlContabilidadBE();
                    ControlContabilidadBC oControlContabilidadBC = new ControlContabilidadBC();

                    oControlContabilidadBE.IdDocumento = objEntregaRendirBE.IdEntregaRendir;
                    oControlContabilidadBE.CodigoDocumento = objEntregaRendirBE.CodigoEntregaRendir;
                    oControlContabilidadBE.UserUpdate = Convert.ToInt32(UsuarioConectado);
                    oControlContabilidadBE.FechaContabilizacion = DateTime.Now;
                    oControlContabilidadBE.Getdate = DateTime.Now;
                    oControlContabilidadBE.Documento = "ER";
                    oControlContabilidadBE.CreateDate = objEntregaRendirBE.CreateDate;
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
                objEntregaRendirBE.Comentario = "";
                objEntregaRendirBC.ModificarEntregaRendir(objEntregaRendirBE);

                UsuarioBC objUsuarioBC = new UsuarioBC();
                UsuarioBE objUsuarioBE = new UsuarioBE();
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(objEntregaRendirBE.IdUsuarioSolicitante, 0);
                EnviarMensajeAprobado(objEntregaRendirBE.IdEntregaRendir, "Entrega Rendir", "Rendicion Entrega Rendir: " + objEntregaRendirBE.CodigoEntregaRendir, objEntregaRendirBE.CodigoEntregaRendir, objUsuarioBE.CardName, objEntregaRendirBE.Estado, objEntregaRendirBE.IdUsuarioSolicitante);


            }
            else
            {
                Mensaje(mensaje);
            }
        }

        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
        }
        finally
        {
            bAprobar.Enabled = true;
            Response.Redirect("~/EntregasRendir.aspx");
        }
    }

    private void EnviarMensajeAprobado(int IdEntregaRendir, string Documento, string Asunto, string CodigoEntregaRendir, string UsuarioSolicitante, string estado, int IdUsuarioSolicitante)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();
        if (estado == "13")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, 2);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El usuario " + UsuarioSolicitante + " a realizado la rendicion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto, lstUsuarioBE[i].Mail);
            }
        }
        if (estado == "15")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, 3);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El usuario " + UsuarioSolicitante + " a realizado la rendicion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto, lstUsuarioBE[i].Mail);
            }
        }
        if (estado == "17")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(5, 0, 0);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El usuario " + UsuarioSolicitante + " a realizado la rendicion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto, lstUsuarioBE[i].Mail);
            }




            /////////////
            String strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

            EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
            EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
            objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);

            UsuarioBE objUsuarioBE = new UsuarioBE();
            objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);


            List<UsuarioBE> lstUsuarioTesoreriaBE = new List<UsuarioBE>();
            lstUsuarioTesoreriaBE = objUsuarioBC.ListarUsuarioCorreosTesoreria();

            CorreosBE objCorreoBE = new CorreosBE();
            CorreosBC objCorreosBC = new CorreosBC();
            List<CorreosBE> lstCorreosBE = new List<CorreosBE>();



            String moneda = "";
            if (objEntregaRendirBE.Moneda.ToString() == "1")
                moneda = "S/. ";
            else
                moneda = "USD. ";



            for (int x = 0; x < lstUsuarioTesoreriaBE.Count; x++)
            {


                if (lstUsuarioTesoreriaBE[x].Mail.ToString() != "")
                {
                    lstCorreosBE = objCorreosBC.ObtenerCorreos(1);
                    MensajeMail(lstCorreosBE[0].TextoCorreo.ToString() + ": La " + Documento + " con Codigo: " + CodigoEntregaRendir + "<br/>" + "<br/>"
                    // + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                    + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                    + "Importe a Pagar :" + moneda + objEntregaRendirBE.MontoInicial + "<br/>"
                    + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                    , "Entrega a Rendir" + CodigoEntregaRendir, lstUsuarioTesoreriaBE[x].Mail.ToString());
                }

            }




        }
        if (estado == "19")
        {
            UsuarioBE objUsuarioBE = new UsuarioBE();
            objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);
            MensajeMail("La " + Documento + " Codigo: " + CodigoEntregaRendir + " fue Aprobada", Asunto + " Aprobada", objUsuarioBE.Mail);
        }
    }

    protected void Observacion_Click(object sender, EventArgs e)
    {
        try
        {
            bObservacion.Enabled = false;

            if (txtComentario.Text.Trim() != "")
            {
                String strIdEntregaRendir = "";
                strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();
                String estado = "0";

                EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
                EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
                objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);
                estado = objEntregaRendirBE.Estado;
                if (estado == "11") objEntregaRendirBE.Estado = "12";
                if (estado == "13") objEntregaRendirBE.Estado = "14";
                if (estado == "15") objEntregaRendirBE.Estado = "16";
                if (estado == "17") objEntregaRendirBE.Estado = "18";

                if (estado == "18") { objEntregaRendirBE.Estado = "16"; estado = "15"; }
                if (estado == "16") { objEntregaRendirBE.Estado = "14"; estado = "13"; }
                if (estado == "14") { objEntregaRendirBE.Estado = "12"; estado = "11"; }
                objEntregaRendirBE.Comentario = txtComentario.Text;
                objEntregaRendirBC.ModificarEntregaRendir(objEntregaRendirBE);

                UsuarioBC objUsuarioBC = new UsuarioBC();
                UsuarioBE objUsuarioBE = new UsuarioBE();
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);
                EnviarMensajeObservacion(objEntregaRendirBE.IdEntregaRendir, "Entrega Rendir", "Rendicion Entrega Rendir: " + objEntregaRendirBE.CodigoEntregaRendir, objEntregaRendirBE.CodigoEntregaRendir, objUsuarioBE.CardName, estado, objEntregaRendirBE.IdUsuarioSolicitante);

                Response.Redirect("~/EntregasRendir.aspx");
            }
            else
                Mensaje("No a colocado ninguna observacion");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
        }
        finally
        {
            Response.Redirect("~/EntregasRendir.aspx");
            bObservacion.Enabled = true;
        }
    }

    private void EnviarMensajeObservacion(int IdEntregaRendir, string Documento, string Asunto, string CodigoEntregaRendir, string UsuarioAprobador, string estado, int IdUsuarioSolicitante)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();

        if (estado == "11")
        {
            objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);
            MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto + " Observacion", objUsuarioBE.Mail);
        }

        if (estado == "13")
        {
            //objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante);
            //MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto + " Observacion", objUsuarioBE.Mail);

            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, 1);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto + " Observacion", lstUsuarioBE[i].Mail);
            }
        }

        if (estado == "15")
        {
            //objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante);
            //MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto + " Observacion", objUsuarioBE.Mail);

            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, 2);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto + " Observacion", lstUsuarioBE[i].Mail);
            }
        }
        else
        {
            NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
            NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();
            objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(IdEntregaRendir, 5);

            lstUsuarioBE = objUsuarioBC.ListarUsuario(6, IdEntregaRendir, Convert.ToInt32(objNivelAprobacionBE.Nivel));
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoEntregaRendir, Asunto + " Observacion", lstUsuarioBE[i].Mail);
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
                Mensaje("Ocurrió un error (EntregaRendir): " + ex.Message);
            }
        }
    }

    private bool ValidarDatosExcel(List<EntregaRendirDocumentoBE> lstEntregaRendirDocumentoBE)
    {
        for (int i = 0; i <= lstEntregaRendirDocumentoBE.Count - 1; i++)
        {
            if (lstEntregaRendirDocumentoBE[i].TipoDoc.Trim() == "" ||
               lstEntregaRendirDocumentoBE[i].SerieDoc.Trim() == "" ||
               lstEntregaRendirDocumentoBE[i].CorrelativoDoc.Trim() == "" ||
               //lstEntregaRendirDocumentoBE[i].FechaDoc.Trim() == "" ||
               //lstEntregaRendirDocumentoBE[i].IdProveedor.Trim() == "" ||
               //lstEntregaRendirDocumentoBE[i].IdConcepto.Trim() == "" ||
               //lstEntregaRendirDocumentoBE[i].IdCentroCostos3.Trim() == "" ||
               //lstEntregaRendirDocumentoBE[i].IdCentroCostos4.Trim() == "" ||
               //lstEntregaRendirDocumentoBE[i].IdCentroCostos5.Trim() == "" ||
               //lstEntregaRendirDocumentoBE[i].IdMonedaOriginal.Trim() == "" ||
               //lstEntregaRendirDocumentoBE[i].IdMonedaDoc.Trim() == "" ||
               lstEntregaRendirDocumentoBE[i].TasaCambio.Trim() == "" ||
               lstEntregaRendirDocumentoBE[i].MontoDoc.Trim() == "" ||
               lstEntregaRendirDocumentoBE[i].MontoIGV.Trim() == "" ||
               lstEntregaRendirDocumentoBE[i].MontoAfecto.Trim() == "" ||
               lstEntregaRendirDocumentoBE[i].MontoNoAfecto.Trim() == "" ||
               lstEntregaRendirDocumentoBE[i].MontoTotal.Trim() == ""
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

                    String strIdEntregaRendir = "";
                    strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

                    objProveedorBE.Proceso = 2;
                    objProveedorBE.IdProceso = Convert.ToInt32(strIdEntregaRendir);
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
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
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

                String strIdEntregaRendir = "";
                strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

                objProveedorBE.Proceso = 2;
                objProveedorBE.IdProceso = Convert.ToInt32(strIdEntregaRendir);
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
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
        }
        finally
        {
            bGuardar2.Enabled = true;
        }
    }

    protected void Agregar3_Click(object sender, EventArgs e)
    {
        try
        {
            bAgregar3.Enabled = false;

            string mensajeError = "";
            bool validacion = true;

            validacion = ValidarImporte2();
            if (validacion == false) Mensaje("Usted a ingresado los importes erroneamente.");

            if (validacion)
            {
                if (ddlIdMonedaPago.SelectedItem.Value == "0" || txtMontoPago.Text.Trim() == "" ||
                    txtTasaCambioPago.Text.Trim() == "" || ddlIdBanco.SelectedItem.Value == "0" ||
                    txtFechaPago.Text.Trim() == "")
                {
                    validacion = false;
                    mensajeError = "Debe ingresar toda la informacion.";
                }

                if (validacion)
                {
                    decimal n;
                    bool isNumeric1, isNumeric2;
                    isNumeric1 = decimal.TryParse(txtMontoPago.Text, out n);
                    if (isNumeric1 == false) validacion = false;
                    isNumeric2 = decimal.TryParse(txtTasaCambioPago.Text, out n);
                    if (isNumeric2 == false) validacion = false;
                    mensajeError = "Usted a ingresado los importes erroneamente.";
                }
            }

            if (validacion)
            {
                String strIdEntregaRendir = "";
                strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

                EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
                EntregaRendirDocumentoBE objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
                objEntregaRendirDocumentoBE.IdEntregaRendir = Convert.ToInt32(strIdEntregaRendir);

                objEntregaRendirDocumentoBE.IdProveedor = Convert.ToInt32(ddlIdBanco.SelectedItem.Value);

                objEntregaRendirDocumentoBE.IdConcepto = 0;
                objEntregaRendirDocumentoBE.IdCentroCostos3 = 0;
                objEntregaRendirDocumentoBE.IdCentroCostos4 = 0;
                objEntregaRendirDocumentoBE.IdCentroCostos5 = 0;
                objEntregaRendirDocumentoBE.TipoDoc = "7";
                objEntregaRendirDocumentoBE.SerieDoc = "";
                objEntregaRendirDocumentoBE.CorrelativoDoc = "";
                objEntregaRendirDocumentoBE.FechaDoc = Convert.ToDateTime(txtFechaPago.Text);
                objEntregaRendirDocumentoBE.IdMonedaOriginal = Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value);
                objEntregaRendirDocumentoBE.IdMonedaDoc = Convert.ToInt32(ddlIdMonedaPago.SelectedItem.Value);
                objEntregaRendirDocumentoBE.MontoDoc = Convert.ToDouble(txtMontoPago.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoIGV = "0.00";
                objEntregaRendirDocumentoBE.MontoAfecto = "0.00";
                objEntregaRendirDocumentoBE.MontoNoAfecto = Convert.ToDouble(txtMontoPago.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoTotal = Convert.ToDouble(txtMontoTotalPago.Text).ToString("0.00");

                if (ddlIdMonedaOriginal.SelectedValue == ddlIdMonedaPago.SelectedValue) objEntregaRendirDocumentoBE.TasaCambio = "1.0000";
                else objEntregaRendirDocumentoBE.TasaCambio = Convert.ToDouble(txtTasaCambioPago.Text).ToString("0.0000");

                objEntregaRendirDocumentoBE.Estado = "1";

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

                    objEntregaRendirDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirDocumentoBE.CreateDate = DateTime.Now;
                    objEntregaRendirDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirDocumentoBE.UpdateDate = DateTime.Now;
                }
                int Id;
                Id = objEntregaRendirDocumentoBC.InsertarEntregaRendirDocumento(objEntregaRendirDocumentoBE);
                ListarRendicion();
                LlenarCabecera();
                LimpiarCampos2();
            }
            else
                Mensaje(mensajeError);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
        }
        finally
        {
            bAgregar3.Enabled = true;
        }
    }

    protected void Guardar3_Click(object sender, EventArgs e)
    {
        try
        {
            bGuardar3.Enabled = false;

            string mensajeError = "";
            bool validacion = true;

            validacion = ValidarImporte2();
            if (validacion == false) Mensaje("Usted a ingresado los importes erroneamente.");

            if (validacion)
            {
                if (ddlIdMonedaPago.SelectedItem.Value == "0" || txtMontoPago.Text.Trim() == "" ||
                    txtTasaCambioPago.Text.Trim() == "" || ddlIdBanco.SelectedItem.Value == "0" ||
                    txtFechaPago.Text.Trim() == "")
                {
                    validacion = false;
                    mensajeError = "Debe ingresar toda la informacion.";
                }

                if (validacion)
                {
                    decimal n;
                    bool isNumeric1, isNumeric2;
                    isNumeric1 = decimal.TryParse(txtMontoPago.Text, out n);
                    if (isNumeric1 == false) validacion = false;
                    isNumeric2 = decimal.TryParse(txtTasaCambioPago.Text, out n);
                    if (isNumeric2 == false) validacion = false;
                    mensajeError = "Usted a ingresado los importes erroneamente.";
                }
            }

            if (validacion)
            {
                String strIdEntregaRendir = "";
                strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

                EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
                EntregaRendirDocumentoBE objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
                objEntregaRendirDocumentoBE.IdEntregaRendirDocumento = Convert.ToInt32(lblIdEntregaRendirDocumento.Text);
                objEntregaRendirDocumentoBE.IdEntregaRendir = Convert.ToInt32(strIdEntregaRendir);

                objEntregaRendirDocumentoBE.IdProveedor = Convert.ToInt32(ddlIdBanco.SelectedItem.Value);

                objEntregaRendirDocumentoBE.IdConcepto = 0;
                objEntregaRendirDocumentoBE.IdCentroCostos3 = 0;
                objEntregaRendirDocumentoBE.IdCentroCostos4 = 0;
                objEntregaRendirDocumentoBE.IdCentroCostos5 = 0;
                objEntregaRendirDocumentoBE.TipoDoc = "7";
                objEntregaRendirDocumentoBE.SerieDoc = "";
                objEntregaRendirDocumentoBE.CorrelativoDoc = "";
                objEntregaRendirDocumentoBE.FechaDoc = Convert.ToDateTime(txtFechaPago.Text);
                objEntregaRendirDocumentoBE.IdMonedaOriginal = Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value);
                objEntregaRendirDocumentoBE.IdMonedaDoc = Convert.ToInt32(ddlIdMonedaPago.SelectedItem.Value);
                objEntregaRendirDocumentoBE.MontoDoc = Convert.ToDouble(txtMontoPago.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoIGV = "0.00";
                objEntregaRendirDocumentoBE.MontoAfecto = "0.00";
                objEntregaRendirDocumentoBE.MontoNoAfecto = Convert.ToDouble(txtMontoPago.Text).ToString("0.00");
                objEntregaRendirDocumentoBE.MontoTotal = Convert.ToDouble(txtMontoTotalPago.Text).ToString("0.00");

                if (ddlIdMonedaOriginal.SelectedValue == ddlIdMonedaPago.SelectedValue) objEntregaRendirDocumentoBE.TasaCambio = "1.0000";
                else objEntregaRendirDocumentoBE.TasaCambio = Convert.ToDouble(txtTasaCambioPago.Text).ToString("0.0000");

                objEntregaRendirDocumentoBE.Estado = "1";

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

                    objEntregaRendirDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirDocumentoBE.CreateDate = DateTime.Now;
                    objEntregaRendirDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objEntregaRendirDocumentoBE.UpdateDate = DateTime.Now;
                }

                objEntregaRendirDocumentoBC.ModificarEntregaRendirDocumento(objEntregaRendirDocumentoBE);

                ListarRendicion();
                LlenarCabecera();
                LimpiarCampos2();

                ddlIdBanco.Enabled = false;
                bAgregar3.Visible = true;
                bGuardar3.Visible = false;
            }
            else
                Mensaje(mensajeError);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
        }
        finally
        {
            bGuardar3.Enabled = true;
        }
    }

    protected void ValidarImporte2_Click(object sender, EventArgs e)
    {
        bool validacion = true;
        decimal n;
        bool isNumeric1, isNumeric2;
        if (txtTasaCambioPago.Text.Trim() != "")
        {
            isNumeric1 = decimal.TryParse(txtTasaCambioPago.Text, out n);
            if (isNumeric1 == false) validacion = false;
        }
        else txtTasaCambio.Text = "1.00";

        if (txtMontoPago.Text.Trim() != "")
        {
            isNumeric2 = decimal.TryParse(txtMontoPago.Text, out n);
            if (isNumeric2 == false) validacion = false;
        }
        else txtMontoAfecta.Text = "0.00";

        if (validacion)
        {
            double MontoPago = Math.Round(Convert.ToDouble(txtMontoPago.Text), 2);
            double MontoTotalPago = Math.Round(MontoPago * Convert.ToDouble(txtTasaCambioPago.Text), 2);

            txtMontoPago.Text = Math.Round(MontoPago, 2).ToString("0.00");
            txtMontoTotalPago.Text = Math.Round(MontoTotalPago, 2).ToString("0.00");

            if (MontoTotalPago == 0) Mensaje("Monto Total debe ser mayor a 0.");
        }
        else
            Mensaje("Usted a ingresado los importes erroneamente.");
    }

    private bool ValidarImporte2()
    {
        bool validacion = true;
        decimal n;
        bool isNumeric1, isNumeric2;
        if (txtTasaCambioPago.Text.Trim() != "")
        {
            isNumeric1 = decimal.TryParse(txtTasaCambioPago.Text, out n);
            if (isNumeric1 == false) validacion = false;
        }
        else txtTasaCambio.Text = "1.00";

        if (txtMontoPago.Text.Trim() != "")
        {
            isNumeric2 = decimal.TryParse(txtMontoPago.Text, out n);
            if (isNumeric2 == false) validacion = false;
        }
        else txtMontoAfecta.Text = "0.00";

        if (validacion)
        {
            double MontoPago = Math.Round(Convert.ToDouble(txtMontoPago.Text), 2);
            double MontoTotalPago = Math.Round(MontoPago * Convert.ToDouble(txtTasaCambioPago.Text), 2);

            txtMontoPago.Text = Math.Round(MontoPago, 2).ToString("0.00");
            txtMontoTotalPago.Text = Math.Round(MontoTotalPago, 2).ToString("0.00");

            if (MontoTotalPago == 0) validacion = false;
        }
        else
            validacion = false;

        return validacion;
    }
    //
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
            headerTable = headerTable + @"<tr><td><b>Total Solicitado:</b></td><td colspan=4>" + Label7.Text + "</td></tr>";
            headerTable = headerTable + @"<tr><td><b>Total Gastado:</b></td><td colspan=4>" + Label8.Text + "</td></tr>";
            headerTable = headerTable + @"<tr><td><b>Entrega a Rendir:</b></td><td colspan=4>" + Label9.Text + "</td></tr>";
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
        }
    }

    //public override void VerifyRenderingInServerForm(System.Windows.Forms.Control control)
    //{
    //    return;
    //}

    private void LlenarCamposCaberaExcel1()
    {
        String strIdEntregaRendir = "";
        strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

        EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
        EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
        objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(Convert.ToInt32(strIdEntregaRendir), 0);

        EmpresaBC objEmpresaBC = new EmpresaBC();
        EmpresaBE objEmpresaBE = new EmpresaBE();
        objEmpresaBE = objEmpresaBC.ObtenerEmpresa(objEntregaRendirBE.IdEmpresa);

        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = objUsuarioBC.ObtenerUsuario(objEntregaRendirBE.IdUsuarioSolicitante, 0);

        MotivoBC objMotivoBC = new MotivoBC();
        MotivoBE objMotivoBE = new MotivoBE();
        objMotivoBE = objMotivoBC.ObtenerMotivo(objEntregaRendirBE.IdMotivo);

        MonedaBC objMonedaBC = new MonedaBC();
        MonedaBE objMonedaBE = new MonedaBE();
        objMonedaBE = objMonedaBC.ObtenerMoneda(Convert.ToInt32(objEntregaRendirBE.Moneda));

        Label1.Text = objEmpresaBE.Descripcion;
        Label2.Text = objUsuarioBE.CardName;
        Label3.Text = objMotivoBE.Descripcion;
        Label4.Text = (objEntregaRendirBE.FechaSolicitud).ToString("dd/MM/yyyy");
        Label5.Text = (objEntregaRendirBE.UpdateDate).ToString("dd/MM/yyyy");
        Label6.Text = objMonedaBE.Descripcion;
        Label7.Text = objEntregaRendirBE.MontoInicial;
        Label8.Text = "";
        Label9.Text = objEntregaRendirBE.CodigoEntregaRendir;
    }

    private void LlenarCamposCaberaExcel2()
    {
        String strIdEntregaRendir = "";
        strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

        EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
        List<EntregaRendirDocumentoBE> lstEntregaRendirDocumentoBE = new List<EntregaRendirDocumentoBE>();
        lstEntregaRendirDocumentoBE = objEntregaRendirDocumentoBC.ListarEntregaRendirDocumento(Convert.ToInt32(strIdEntregaRendir), 3);

        Label8.Text = lstEntregaRendirDocumentoBE[0].MontoTotal;
        Label10.Text = lstEntregaRendirDocumentoBE[0].MontoTotal;
        Label11.Text = lstEntregaRendirDocumentoBE[0].MontoNoAfecto;
        Label12.Text = lstEntregaRendirDocumentoBE[0].MontoAfecto;
        Label13.Text = lstEntregaRendirDocumentoBE[0].MontoIGV;
        Label14.Text = lstEntregaRendirDocumentoBE[0].MontoDoc;
    }

    private void ListarRendicion2()
    {
        String strIdEntregaRendir = "";
        strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();

        EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
        gvReporte.DataSource = objEntregaRendirDocumentoBC.ListarEntregaRendirDocumento(Convert.ToInt32(strIdEntregaRendir), 2);
        gvReporte.DataBind();
    }
    //
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

            String strIdEntregaRendir = "";
            strIdEntregaRendir = ViewState["IdEntregaRendir"].ToString();
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
                        objProveedorBE.Proceso = 2;
                        objProveedorBE.IdProceso = Convert.ToInt32(strIdEntregaRendir);
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
                EntregaRendirDocumentoBC objEntregaRendirDocumentoBC = new EntregaRendirDocumentoBC();
                EntregaRendirDocumentoBE objEntregaRendirDocumentoBE;

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
                    objEntregaRendirDocumentoBE.IdEntregaRendir = Convert.ToInt32(strIdEntregaRendir);
                    objEntregaRendirDocumentoBE.IdProveedor = Convert.ToInt32(sIdProveedor[i]);
                    objEntregaRendirDocumentoBE.IdConcepto = Convert.ToInt32(GridView1.Rows[i].Cells[6].Text);
                    objEntregaRendirDocumentoBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                    objEntregaRendirDocumentoBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                    objEntregaRendirDocumentoBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                    objEntregaRendirDocumentoBE.TipoDoc = GridView1.Rows[i].Cells[0].Text;
                    objEntregaRendirDocumentoBE.SerieDoc = GridView1.Rows[i].Cells[1].Text;
                    objEntregaRendirDocumentoBE.CorrelativoDoc = GridView1.Rows[i].Cells[2].Text;
                    objEntregaRendirDocumentoBE.FechaDoc = Convert.ToDateTime(GridView1.Rows[i].Cells[3].Text);
                    objEntregaRendirDocumentoBE.IdMonedaOriginal = Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value);
                    objEntregaRendirDocumentoBE.IdMonedaDoc = Convert.ToInt32(GridView1.Rows[i].Cells[7].Text);
                    objEntregaRendirDocumentoBE.TasaCambio = Convert.ToDouble(GridView1.Rows[i].Cells[8].Text).ToString("0.0000");
                    objEntregaRendirDocumentoBE.MontoNoAfecto = Convert.ToDouble(GridView1.Rows[i].Cells[9].Text).ToString("0.00");
                    objEntregaRendirDocumentoBE.MontoAfecto = Convert.ToDouble(GridView1.Rows[i].Cells[10].Text).ToString("0.00");
                    objEntregaRendirDocumentoBE.MontoIGV = Convert.ToDouble(GridView1.Rows[i].Cells[11].Text).ToString("0.00");
                    objEntregaRendirDocumentoBE.MontoTotal = Convert.ToDouble(GridView1.Rows[i].Cells[12].Text).ToString("0.00");
                    objEntregaRendirDocumentoBE.MontoDoc = Convert.ToDouble(GridView1.Rows[i].Cells[13].Text).ToString("0.00");
                    objEntregaRendirDocumentoBE.Estado = "1";

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

                        objEntregaRendirDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                        objEntregaRendirDocumentoBE.CreateDate = DateTime.Now;
                        objEntregaRendirDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                        objEntregaRendirDocumentoBE.UpdateDate = DateTime.Now;
                    }
                    int Id;
                    Id = objEntregaRendirDocumentoBC.InsertarEntregaRendirDocumento(objEntregaRendirDocumentoBE);
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
            Mensaje("Ocurrió un error (RendirEntregaRendir): " + ex.Message);
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

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
}