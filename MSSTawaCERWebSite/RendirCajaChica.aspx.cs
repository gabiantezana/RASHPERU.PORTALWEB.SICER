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

public partial class RendirCajaChica : System.Web.UI.Page
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

                ListarTipoDocumento();
                ListarProveedor();
                ListarProveedorCrear();
                ListarCentroCostos();
                ListarConcepto();
                ListarRendicion();
                ListarMoneda(Convert.ToInt32(strIdCajaChica));
                Modalidad(Convert.ToInt32(strModo));
                ModalidadCampo(Convert.ToInt32(strModo), Convert.ToInt32(strIdCajaChica));
                LlenarCamposCaberaExcel1();

                CajaChicaBC objCajaChicaBC = new CajaChicaBC();
                CajaChicaBE objCajaChicaBE = new CajaChicaBE();
                objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);

                if (objCajaChicaBE.Estado == "19")
                    txtFechaContabilizacion.Text = txtFechaContabilizacion.Text = (objCajaChicaBE.FechaContabilizacion).ToString("dd/MM/yyyy");
                else
                    txtFechaContabilizacion.Text = txtFechaContabilizacion.Text = (DateTime.Today).ToString("dd/MM/yyyy");

                txtComentario.Text = objCajaChicaBE.Comentario;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
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
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
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
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
        }
    }

    private void ListarCentroCostos()
    {
        try
        {
            CentroCostosBC objCentroCostosBC = new CentroCostosBC();

            String strIdCajaChica = "";
            strIdCajaChica = Context.Items["IdCajaChica"].ToString();
            CajaChicaBC objCajaChicaBC = new CajaChicaBC();
            CajaChicaBE objCajaChicaBE = new CajaChicaBE();
            objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);

            ddlCentroCostos3.DataSource = objCentroCostosBC.ListarCentroCostos(objCajaChicaBE.IdUsuarioSolicitante, 8, objCajaChicaBE.IdEmpresa);
            ddlCentroCostos3.DataTextField = "Descripcion";
            ddlCentroCostos3.DataValueField = "IdCentroCostos";
            ddlCentroCostos3.DataBind();

            ddlCentroCostos4.DataSource = objCentroCostosBC.ListarCentroCostos(objCajaChicaBE.IdCentroCostos3, 9, objCajaChicaBE.IdEmpresa);
            ddlCentroCostos4.DataTextField = "Descripcion";
            ddlCentroCostos4.DataValueField = "IdCentroCostos";
            ddlCentroCostos4.DataBind();

            ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(objCajaChicaBE.IdCentroCostos4, 11, objCajaChicaBE.IdEmpresa);
            ddlCentroCostos5.DataTextField = "Descripcion";
            ddlCentroCostos5.DataValueField = "IdCentroCostos";
            ddlCentroCostos5.DataBind();

            ddlCentroCostos3.SelectedValue = objCajaChicaBE.IdCentroCostos3.ToString();
            ddlCentroCostos4.SelectedValue = objCajaChicaBE.IdCentroCostos4.ToString();
            ddlCentroCostos5.SelectedValue = objCajaChicaBE.IdCentroCostos5.ToString();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
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
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
        }
    }

    private void ListarRendicion()
    {
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
        gvCajaChicas.DataSource = objCajaChicaDocumentoBC.ListarCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 1, 0);
        gvCajaChicas.DataBind();

        //if (gvCajaChicas.Rows.Count > 0) bEnviar.Visible = true;
        //else bEnviar.Visible = false;
    }

    private void ListarMoneda(int IdCajaChica)
    {
        MonedaBC objMonedaBC = new MonedaBC();

        ddlIdMonedaDoc.DataSource = objMonedaBC.ListarMoneda(0, 1);
        ddlIdMonedaDoc.DataTextField = "Descripcion";
        ddlIdMonedaDoc.DataValueField = "IdMoneda";
        ddlIdMonedaDoc.DataBind();

        ddlIdMonedaOriginal.DataSource = objMonedaBC.ListarMoneda(IdCajaChica, 2);
        ddlIdMonedaOriginal.DataTextField = "Descripcion";
        ddlIdMonedaOriginal.DataValueField = "IdMoneda";
        ddlIdMonedaOriginal.DataBind();
    }

    private void ListarProveedorCrear()
    {
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        ProveedorBC objProveedorBC = new ProveedorBC();
        gvProveedor.DataSource = objProveedorBC.ListarProveedor(Convert.ToInt32(strIdCajaChica), 2);
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
                    LimpiarCampos();
                    break;
                case 2:
                    //lblCabezera.Text = "Aprobar Caja Chica";
                    //bCrear.Text = "Guardar";
                    //LlenarCampos(Convert.ToInt32(ViewState["IdCajaChica"].ToString()));
                    break;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
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

            //UsuarioAreaNivelBC objUsuarioAreaNivelBC = new UsuarioAreaNivelBC();
            //UsuarioAreaNivelBE objUsuarioAreaNivelBE = new UsuarioAreaNivelBE();
            //objUsuarioAreaNivelBE = objUsuarioAreaNivelBC.ObtenerUsuarioAreaNivel(objUsuarioBE.IdUsuario, 1, IdCajaChica);

            //NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
            //NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();
            //if (objUsuarioAreaNivelBE != null)
            //    objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(objUsuarioAreaNivelBE.IdNivelAprobacion, 0);

            gvCajaChicas.Columns[0].Visible = false;
            gvCajaChicas.Columns[1].Visible = false;
            gvCajaChicas.Columns[2].Visible = false;
            bAgregar.Visible = false;
            bGuardar.Visible = false;
            bCancelar.Visible = true;
            bMasivo.Visible = false;
            bAgregar2.Visible = false;
            bGuardar2.Visible = false;
            lblComentario.Visible = true;
            txtComentario.Visible = true;
            bEnviar.Visible = false;
            bAprobar.Visible = false;
            bLiquidar.Visible = false;
            bObservacion.Visible = false;

            if (objCajaChicaBE.EsFacturable == "1") //Si
            {
                ddlCentroCostos3.Enabled = false;
                ddlCentroCostos4.Enabled = false;
                ddlCentroCostos5.Enabled = false;
            }
            else//No
            {
                ddlCentroCostos3.Enabled = true;
                ddlCentroCostos4.Enabled = true;
                ddlCentroCostos5.Enabled = true;
            }
            //11, Rendir: Por Aprobar Jefe Area //12, Rendir: Observaciones Nivel 1 
            //13, Rendir: Por Aprobar Contabilidad //14, Rendir: Observaciones Contabilidad //15, Rendir: Aprobado

            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objCajaChicaBE.Estado == "4")//Aprobado
            {
                if (objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                {
                    if (objUsuarioSesionBE.IdUsuario == objCajaChicaBE.IdUsuarioCreador)
                    {
                        gvCajaChicas.Columns[1].Visible = true; gvCajaChicas.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objCajaChicaBE.Estado == "11")//11, Rendir: Por Aprobar Nivel 1
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                {
                    if (objUsuarioSolicitanteBE.IdUsuarioCC1 == objUsuarioSesionBE.IdUsuario)
                    {
                        gvCajaChicas.Columns[1].Visible = true; gvCajaChicas.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
                    }
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objCajaChicaBE.Estado == "12")//12, Rendir: Observaciones  Nivel 1
            {
                if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "4" || objPerfilUsuarioBE.TipoAprobador == "5")
                {
                    if (objCajaChicaBE.IdUsuarioSolicitante == objUsuarioSesionBE.IdUsuario)
                    {
                        gvCajaChicas.Columns[1].Visible = true; gvCajaChicas.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                    if (objCajaChicaBE.IdUsuarioCreador == objUsuarioSesionBE.IdUsuario)
                    {
                        gvCajaChicas.Columns[1].Visible = true; gvCajaChicas.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                        bMasivo.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                    }
                }
            }
            ////1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            //if (objCajaChicaBE.Estado == "13")//13, Rendir: Por Aprobar Nivel 2
            //{
            //    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
            //    {
            //        if (objUsuarioSolicitanteBE.IdUsuarioER2 == objUsuarioSesionBE.IdUsuario)
            //        {
            //            gvCajaChicas.Columns[1].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true; 
            //            bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
            //        }
            //    }
            //}
            ////1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            //if (objCajaChicaBE.Estado == "14")//14, Rendir: Observaciones  Nivel 2
            //{
            //    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
            //    {
            //        if (objUsuarioSolicitanteBE.IdUsuarioER1 == objUsuarioSesionBE.IdUsuario)
            //        {
            //            gvCajaChicas.Columns[1].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true; 
            //            bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
            //        }
            //    }
            //}
            ////1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            //if (objCajaChicaBE.Estado == "15")//15, Rendir: Por Aprobar Nivel 3
            //{
            //    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
            //    {
            //        if (objUsuarioSolicitanteBE.IdUsuarioER3 == objUsuarioSesionBE.IdUsuario)
            //        {
            //            gvCajaChicas.Columns[1].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true; 
            //            bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
            //        }
            //    }
            //}
            ////1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            //if (objCajaChicaBE.Estado == "16")//14, Rendir: Observaciones  Nivel 3
            //{
            //    if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
            //    {
            //        if (objUsuarioSolicitanteBE.IdUsuarioER2 == objUsuarioSesionBE.IdUsuario)
            //        {
            //            gvCajaChicas.Columns[1].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true; 
            //            bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
            //        }
            //    }
            //}
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objCajaChicaBE.Estado == "13")//13, Rendir: Por Aprobar Contabilidad
            {
                if (objPerfilUsuarioBE.TipoAprobador == "2" || objPerfilUsuarioBE.TipoAprobador == "5")
                {
                    gvCajaChicas.Columns[0].Visible = true; gvCajaChicas.Columns[1].Visible = true; gvCajaChicas.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                    bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bLiquidar.Visible = true; bObservacion.Visible = true; txtFechaContabilizacion.Enabled = true;
                }
            }
            //1: Aprobador/ 2: Contabilidad/ 3: Creador/ 4: Aprobador y Creador/ 5: Contabilidad y Creador
            if (objCajaChicaBE.Estado == "14")//14, Rendir: Observaciones Contabilidad
            {
                if (objUsuarioSesionBE.IdUsuario == objCajaChicaBE.IdUsuarioCreador)
                {
                    gvCajaChicas.Columns[1].Visible = true; gvCajaChicas.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true;
                    bMasivo.Visible = true; bAgregar2.Visible = true; bEnviar.Visible = true;
                }
                //if (objPerfilUsuarioBE.TipoAprobador == "1" || objPerfilUsuarioBE.TipoAprobador == "4")
                //{
                //    if (objUsuarioSolicitanteBE.IdUsuarioER3 == objUsuarioSesionBE.IdUsuario)
                //    {
                //        gvCajaChicas.Columns[1].Visible = true; gvCajaChicas.Columns[2].Visible = true; bAgregar.Visible = true; bCancelar.Visible = true; 
                //        bMasivo.Visible = true; bAgregar2.Visible = true; bAprobar.Visible = true; bObservacion.Visible = true;
                //    }
                //}
            }
        }
    }

    protected void gvCajaChicas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int IdCajaChicaDocumento;

        try
        {
            IdCajaChicaDocumento = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("Editar"))
            {
                lblIdCajaChicaDocumento.Text = IdCajaChicaDocumento.ToString();

                CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
                CajaChicaDocumentoBE objCajaChicaDocumentoBE = new CajaChicaDocumentoBE();
                objCajaChicaDocumentoBE = objCajaChicaDocumentoBC.ObtenerCajaChicaDocumento(IdCajaChicaDocumento, 0);
                txtSerie.Text = objCajaChicaDocumentoBE.SerieDoc;
                txtNumero.Text = objCajaChicaDocumentoBE.CorrelativoDoc;
                txtFecha.Text = objCajaChicaDocumentoBE.FechaDoc.ToString().Substring(0, 10);
                txtMontoTotal.Text = Convert.ToDouble(objCajaChicaDocumentoBE.MontoTotal).ToString("0.00");
                txtMontoDoc.Text = Convert.ToDouble(objCajaChicaDocumentoBE.MontoDoc).ToString("0.00");
                txtMontoAfecta.Text = Convert.ToDouble(objCajaChicaDocumentoBE.MontoAfecto).ToString("0.00");
                txtMontoNoAfecta.Text = Convert.ToDouble(objCajaChicaDocumentoBE.MontoNoAfecto).ToString("0.00");
                txtMontoIGV.Text = Convert.ToDouble(objCajaChicaDocumentoBE.MontoIGV).ToString("0.00");
                txtTasaCambio.Text = Convert.ToDouble(objCajaChicaDocumentoBE.TasaCambio).ToString("0.0000");
                if (objCajaChicaDocumentoBE.IdMonedaDoc == objCajaChicaDocumentoBE.IdMonedaOriginal) txtTasaCambio.Enabled = false;
                else txtTasaCambio.Enabled = false;
                ddlTipo.SelectedValue = objCajaChicaDocumentoBE.TipoDoc.ToString();

                ProveedorBC objProveedorBC = new ProveedorBC();
                ProveedorBE objProveedorBE = new ProveedorBE();
                objProveedorBE = objProveedorBC.ObtenerProveedor(objCajaChicaDocumentoBE.IdProveedor, 0, "");
                txtProveedor.Text = objProveedorBE.Documento;
                lblProveedor.Text = objProveedorBE.CardName;

                ddlIdMonedaDoc.SelectedValue = objCajaChicaDocumentoBE.IdMonedaDoc.ToString();
                ddlIdMonedaOriginal.SelectedValue = objCajaChicaDocumentoBE.IdMonedaOriginal.ToString();

                CajaChicaBC objCajaChicaBC = new CajaChicaBC();
                CajaChicaBE objCajaChicaBE = new CajaChicaBE();
                objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(objCajaChicaDocumentoBE.IdCajaChica, 0);
                CentroCostosBC objCentroCostosBC = new CentroCostosBC();
                ddlCentroCostos3.DataSource = objCentroCostosBC.ListarCentroCostos(objCajaChicaBE.IdUsuarioSolicitante, 8, objCajaChicaBE.IdEmpresa);
                ddlCentroCostos3.DataTextField = "Descripcion";
                ddlCentroCostos3.DataValueField = "IdCentroCostos";
                ddlCentroCostos3.DataBind();

                ddlCentroCostos4.DataSource = objCentroCostosBC.ListarCentroCostos(objCajaChicaDocumentoBE.IdCentroCostos3, 9, objCajaChicaBE.IdEmpresa);
                ddlCentroCostos4.DataTextField = "Descripcion";
                ddlCentroCostos4.DataValueField = "IdCentroCostos";
                ddlCentroCostos4.DataBind();

                ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(objCajaChicaDocumentoBE.IdCentroCostos4, 11, objCajaChicaBE.IdEmpresa);
                ddlCentroCostos5.DataTextField = "Descripcion";
                ddlCentroCostos5.DataValueField = "IdCentroCostos";
                ddlCentroCostos5.DataBind();

                ddlCentroCostos3.SelectedValue = objCajaChicaDocumentoBE.IdCentroCostos3.ToString();
                ddlCentroCostos4.SelectedValue = objCajaChicaDocumentoBE.IdCentroCostos4.ToString();
                ddlCentroCostos5.SelectedValue = objCajaChicaDocumentoBE.IdCentroCostos5.ToString();

                try
                {
                    ddlConcepto.SelectedValue = objCajaChicaDocumentoBE.IdConcepto.ToString();
                }
                catch
                {
                    ConceptoBC objConceptoBC = new ConceptoBC();
                    ddlConcepto.DataSource = objConceptoBC.ListarConcepto(objCajaChicaDocumentoBE.IdCentroCostos5, 1);
                    ddlConcepto.DataTextField = "Descripcion";
                    ddlConcepto.DataValueField = "IdConcepto";
                    ddlConcepto.DataBind();
                    ddlConcepto.SelectedValue = objCajaChicaDocumentoBE.IdConcepto.ToString();
                }

                bAgregar.Visible = false;
                bGuardar.Visible = true;
            }
            if (e.CommandName.Equals("Eliminar"))
            {
                CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
                objCajaChicaDocumentoBC.EliminarCajaChicaDocumento(IdCajaChicaDocumento);
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
        gvCajaChicas.PageIndex = e.NewPageIndex;
        ListarRendicion();
    }

    private void LlenarCabecera()
    {
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaBC objCajaChicaBC = new CajaChicaBC();
        CajaChicaBE objCajaChicaBE = new CajaChicaBE();
        objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);

        CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
        CajaChicaDocumentoBE objCajaChicaDocumentoBE = new CajaChicaDocumentoBE();
        objCajaChicaDocumentoBE = objCajaChicaDocumentoBC.ObtenerCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 1);
        string montoCCD = "0.00";
        if (objCajaChicaDocumentoBE != null) montoCCD = objCajaChicaDocumentoBE.MontoTotal;
        lblCabezera.Text = "Caja Chica: " + objCajaChicaBE.CodigoCajaChica + " - Monto: " + montoCCD + "/" + Convert.ToDouble(objCajaChicaBE.MontoInicial).ToString("0.00");

        if (objCajaChicaBE.Estado == "19")
            txtFechaContabilizacion.Text = txtFechaContabilizacion.Text = (objCajaChicaBE.FechaContabilizacion).ToString("dd/MM/yyyy");
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
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaBC objCajaChicaBC = new CajaChicaBC();
        CajaChicaBE objCajaChicaBE = new CajaChicaBE();
        objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);

        if (ddlCentroCostos3.SelectedValue != "0")
        {
            CentroCostosBC objConceptoBC = new CentroCostosBC();
            List<CentroCostosBE> lstConceptoBE = new List<CentroCostosBE>();
            lstConceptoBE = objConceptoBC.ListarCentroCostos(Convert.ToInt32(ddlCentroCostos3.SelectedValue), 9, objCajaChicaBE.IdEmpresa);
            ddlCentroCostos4.DataSource = lstConceptoBE;
            ddlCentroCostos4.DataTextField = "Descripcion";
            ddlCentroCostos4.DataValueField = "IdCentroCostos";
            ddlCentroCostos4.DataBind();

            ddlCentroCostos4.Enabled = true;
            ddlCentroCostos5.SelectedValue = "0";
            ddlCentroCostos5.Enabled = false;
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
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaBC objCajaChicaBC = new CajaChicaBC();
        CajaChicaBE objCajaChicaBE = new CajaChicaBE();
        objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);

        if (ddlCentroCostos4.SelectedValue != "0")
        {
            CentroCostosBC objCentroCostosBC = new CentroCostosBC();
            ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlCentroCostos4.SelectedValue), 11, objCajaChicaBE.IdEmpresa);
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

    protected void ddlCentroCosto5_SelectedIndexChanged(object sender, EventArgs e)
    {
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaBC objCajaChicaBC = new CajaChicaBC();
        CajaChicaBE objCajaChicaBE = new CajaChicaBE();
        objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);

        if (ddlCentroCostos5.SelectedValue != "0")
        {
            ConceptoBC objConceptoBC = new ConceptoBC();
            ddlConcepto.DataSource = objConceptoBC.ListarConcepto(Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value), 1);
            ddlConcepto.DataTextField = "Descripcion";
            ddlConcepto.DataValueField = "IdConcepto";
            ddlConcepto.DataBind();
            ddlConcepto.SelectedValue = "0";
        }
        else
        {
            ddlConcepto.SelectedValue = "0";
        }
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
        if (gvCajaChicas.Columns[0].Visible == true)
        {
            System.Web.UI.WebControls.CheckBox checkbox = (System.Web.UI.WebControls.CheckBox)sender;
            GridViewRow row = (GridViewRow)checkbox.NamingContainer;
            int Id = Convert.ToInt32(gvCajaChicas.Rows[row.DataItemIndex].Cells[2].Text);

            CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
            CajaChicaDocumentoBE objCajaChicaDocumentoBE = new CajaChicaDocumentoBE();
            objCajaChicaDocumentoBE = objCajaChicaDocumentoBC.ObtenerCajaChicaDocumento(Id, 0);

            if (checkbox.Checked == true) objCajaChicaDocumentoBE.Estado = "1";
            else objCajaChicaDocumentoBE.Estado = "2";
            objCajaChicaDocumentoBC.ModificarCajaChicaDocumento(objCajaChicaDocumentoBE);
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
                            //||ddlCentroCostos3.SelectedItem.Value == "0"
                            //||ddlCentroCostos4.SelectedItem.Value == "0"
                            //||ddlCentroCostos5.SelectedItem.Value == "0" 
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
                String strIdCajaChica = "";
                strIdCajaChica = ViewState["IdCajaChica"].ToString();

                CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
                CajaChicaDocumentoBE objCajaChicaDocumentoBE = new CajaChicaDocumentoBE();
                objCajaChicaDocumentoBE.IdCajaChica = Convert.ToInt32(strIdCajaChica);

                objCajaChicaDocumentoBE.IdProveedor = Convert.ToInt32(objProveedorBE.IdProveedor);

                objCajaChicaDocumentoBE.IdConcepto = Convert.ToInt32(ddlConcepto.SelectedItem.Value);
                objCajaChicaDocumentoBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objCajaChicaDocumentoBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objCajaChicaDocumentoBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objCajaChicaDocumentoBE.TipoDoc = ddlTipo.SelectedItem.Value;
                objCajaChicaDocumentoBE.SerieDoc = txtSerie.Text;
                objCajaChicaDocumentoBE.CorrelativoDoc = txtNumero.Text;
                objCajaChicaDocumentoBE.FechaDoc = Convert.ToDateTime(txtFecha.Text);
                objCajaChicaDocumentoBE.IdMonedaOriginal = Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value);
                objCajaChicaDocumentoBE.IdMonedaDoc = Convert.ToInt32(ddlIdMonedaDoc.SelectedItem.Value);
                objCajaChicaDocumentoBE.MontoDoc = Convert.ToDouble(txtMontoDoc.Text).ToString("0.00");
                objCajaChicaDocumentoBE.MontoIGV = Convert.ToDouble(txtMontoIGV.Text).ToString("0.00");
                objCajaChicaDocumentoBE.MontoAfecto = Convert.ToDouble(txtMontoAfecta.Text).ToString("0.00");
                objCajaChicaDocumentoBE.MontoNoAfecto = Convert.ToDouble(txtMontoNoAfecta.Text).ToString("0.00");
                objCajaChicaDocumentoBE.MontoTotal = Convert.ToDouble(txtMontoTotal.Text).ToString("0.00");

                if (ddlIdMonedaOriginal.SelectedValue == ddlIdMonedaDoc.SelectedValue) objCajaChicaDocumentoBE.TasaCambio = "1.0000";
                else objCajaChicaDocumentoBE.TasaCambio = Convert.ToDouble(txtTasaCambio.Text).ToString("0.0000");

                objCajaChicaDocumentoBE.Estado = "1";

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

                    objCajaChicaDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objCajaChicaDocumentoBE.CreateDate = DateTime.Now;
                    objCajaChicaDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objCajaChicaDocumentoBE.UpdateDate = DateTime.Now;
                }
                int Id;
                Id = objCajaChicaDocumentoBC.InsertarCajaChicaDocumento(objCajaChicaDocumentoBE);
                ListarRendicion();
                LlenarCabecera();
                LimpiarCampos();
            }
            else
                Mensaje(mensajeError);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
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
                String strIdCajaChica = "";
                strIdCajaChica = ViewState["IdCajaChica"].ToString();

                CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
                CajaChicaDocumentoBE objCajaChicaDocumentoBE = new CajaChicaDocumentoBE();
                objCajaChicaDocumentoBE.IdCajaChicaDocumento = Convert.ToInt32(lblIdCajaChicaDocumento.Text);
                objCajaChicaDocumentoBE.IdCajaChica = Convert.ToInt32(strIdCajaChica);

                objCajaChicaDocumentoBE.IdProveedor = Convert.ToInt32(objProveedorBE.IdProveedor);

                objCajaChicaDocumentoBE.IdConcepto = Convert.ToInt32(ddlConcepto.SelectedItem.Value);
                objCajaChicaDocumentoBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                objCajaChicaDocumentoBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                objCajaChicaDocumentoBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                objCajaChicaDocumentoBE.TipoDoc = ddlTipo.SelectedItem.Value;
                objCajaChicaDocumentoBE.SerieDoc = txtSerie.Text;
                objCajaChicaDocumentoBE.CorrelativoDoc = txtNumero.Text;
                objCajaChicaDocumentoBE.FechaDoc = Convert.ToDateTime(txtFecha.Text);
                objCajaChicaDocumentoBE.IdMonedaOriginal = Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value);
                objCajaChicaDocumentoBE.IdMonedaDoc = Convert.ToInt32(ddlIdMonedaDoc.SelectedItem.Value);
                objCajaChicaDocumentoBE.MontoDoc = Convert.ToDouble(txtMontoDoc.Text).ToString("0.00");
                objCajaChicaDocumentoBE.MontoIGV = Convert.ToDouble(txtMontoIGV.Text).ToString("0.00");
                objCajaChicaDocumentoBE.MontoAfecto = Convert.ToDouble(txtMontoAfecta.Text).ToString("0.00");
                objCajaChicaDocumentoBE.MontoNoAfecto = Convert.ToDouble(txtMontoNoAfecta.Text).ToString("0.00");
                objCajaChicaDocumentoBE.MontoTotal = Convert.ToDouble(txtMontoTotal.Text).ToString("0.00");

                if (ddlIdMonedaOriginal.SelectedValue == ddlIdMonedaDoc.SelectedValue) objCajaChicaDocumentoBE.TasaCambio = "1.0000";
                else objCajaChicaDocumentoBE.TasaCambio = Convert.ToDouble(txtTasaCambio.Text).ToString("0.0000");

                objCajaChicaDocumentoBE.Estado = "1";

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

                    objCajaChicaDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objCajaChicaDocumentoBE.CreateDate = DateTime.Now;
                    objCajaChicaDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    objCajaChicaDocumentoBE.UpdateDate = DateTime.Now;
                }

                objCajaChicaDocumentoBC.ModificarCajaChicaDocumento(objCajaChicaDocumentoBE);
                ListarRendicion();
                LlenarCabecera();
                LimpiarCampos();

                lblIdCajaChicaDocumento.Text = "";
                bAgregar.Visible = true;
                bGuardar.Visible = false;

                CajaChicaBC objCajaChicaBC = new CajaChicaBC();
                CajaChicaBE objCajaChicaBE = new CajaChicaBE();
                objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);
                if (objCajaChicaBE.Estado == "4") bEnviar.Visible = true;
                else bEnviar.Visible = false;
            }
            else
                Mensaje(mensajeError);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
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
        Response.Redirect("~/CajaChicas.aspx");
    }

    protected void Enviar_Click(object sender, EventArgs e)
    {
        try
        {
            bEnviar.Enabled = false;

            if (gvCajaChicas.Rows.Count > 0)
            {
                String strIdCajaChica = "";
                strIdCajaChica = ViewState["IdCajaChica"].ToString();
                String estado = "0";

                //VALIDAR TASA DE CAMBIO
                CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
                CajaChicaDocumentoBE objCajaChicaDocumentoBE = new CajaChicaDocumentoBE();
                objCajaChicaDocumentoBE = objCajaChicaDocumentoBC.ObtenerCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 2);
                //VALIDAR TASA DE CAMBIO
                if (objCajaChicaDocumentoBE == null)
                {
                    CajaChicaBC objCajaChicaBC = new CajaChicaBC();
                    CajaChicaBE objCajaChicaBE = new CajaChicaBE();
                    objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);
                    estado = objCajaChicaBE.Estado;
                    if (objCajaChicaBE.Estado == "4") objCajaChicaBE.Estado = "11";
                    if (objCajaChicaBE.Estado == "12") objCajaChicaBE.Estado = "11";
                    if (objCajaChicaBE.Estado == "14") objCajaChicaBE.Estado = "13";

                    //CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
                    //List<CajaChicaDocumentoBE> lstCajaChicaDocumentoBE = new List<CajaChicaDocumentoBE>();
                    //lstCajaChicaDocumentoBE = objCajaChicaDocumentoBC.ListarCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 2, 0);
                    //if (Convert.ToDouble(lstCajaChicaDocumentofBE[0].MontoTotal) > (Convert.ToDouble(objCajaChicaBE.MontoInicial))
                    //{
                    objCajaChicaBC.ModificarCajaChica(objCajaChicaBE);

                    UsuarioBC objUsuarioBC = new UsuarioBC();
                    UsuarioBE objUsuarioBE = new UsuarioBE();
                    objUsuarioBE = objUsuarioBC.ObtenerUsuario(objCajaChicaBE.IdUsuarioSolicitante, 0);
                    EnviarMensajeParaAprobador(objCajaChicaBE.IdCajaChica, "Caja Chica", "Rendicion Caja Chica: " + objCajaChicaBE.CodigoCajaChica, objCajaChicaBE.CodigoCajaChica, objUsuarioBE.CardName, estado, objCajaChicaBE.IdUsuarioSolicitante);

                    Response.Redirect("~/CajaChicas.aspx");
                    //}
                    //else
                    //    Mensaje("No es posible rendir ");
                }
                else
                    Mensaje("El documento Serie: " + objCajaChicaDocumentoBE.SerieDoc + " Numero: " + objCajaChicaDocumentoBE.CorrelativoDoc + " presenta la fecha de documento: " + objCajaChicaDocumentoBE.FechaDoc + " la cual aun no existe SAP y su tasa de cambio tampoco. Por favor contactarse con Contabilidad y/o Sistemas.");
            }
            else
                Mensaje("Aun no se ah rendido ningun documento.");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
        }
        finally
        {
            bEnviar.Enabled = true;
        }
    }

    private void EnviarMensajeParaAprobador(int IdCajaChica, string Documento, string Asunto, string CodigoCajaChica, string UsuarioSolicitante, string estado, int IdUsuarioSolicitante)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();

        if (estado == "4" || estado == "12")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(4, IdCajaChica, 1);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El usuario " + UsuarioSolicitante + " a realizado la rendicion de una " + Documento + " Codigo: " + CodigoCajaChica, Asunto, lstUsuarioBE[i].Mail);
            }
        }
        else
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(3, 0, 0);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El usuario " + UsuarioSolicitante + " a realizado la rendicion de una " + Documento + " Codigo: " + CodigoCajaChica, Asunto, lstUsuarioBE[i].Mail);
            }
        }
    }

    protected void Aprobar_Click(object sender, EventArgs e)
    {
        try
        {


            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }

            String Hostname = host.HostName.ToString();
            String IP = localIP;


            bAprobar.Enabled = false;
            bool validar = true;
            String mensaje = "";

            String strIdCajaChica = "";
            strIdCajaChica = ViewState["IdCajaChica"].ToString();
            String estado = "0";

            CajaChicaBC objCajaChicaBC = new CajaChicaBC();
            CajaChicaBE objCajaChicaBE = new CajaChicaBE();
            objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);
            estado = objCajaChicaBE.Estado;
            if (objCajaChicaBE.Estado == "11") objCajaChicaBE.Estado = "13";
            else if (objCajaChicaBE.Estado == "13")
            {
                if (txtFechaContabilizacion.Text.Trim() != "")
                {
                    objCajaChicaBE.FechaContabilizacion = Convert.ToDateTime(txtFechaContabilizacion.Text);
                    objCajaChicaBE.Estado = "15";

                    CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
                    CajaChicaDocumentoBE objCajaChicaDocumentoBE = new CajaChicaDocumentoBE();
                    objCajaChicaDocumentoBE = objCajaChicaDocumentoBC.ObtenerCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 1);
                    objCajaChicaBE.MontoGastado = objCajaChicaDocumentoBE.MontoTotal;
                    objCajaChicaBE.MontoActual = (Convert.ToDouble(objCajaChicaBE.MontoInicial) - Convert.ToDouble(objCajaChicaDocumentoBE.MontoTotal)).ToString("0.00");
                }
                else
                {
                    validar = false;
                    mensaje = "Debe ingresar un fecha de contabilizacion.";
                }
            }

            if (validar)
            {
                objCajaChicaBE.Comentario = "";


                objCajaChicaBC.ModificarCajaChica(objCajaChicaBE);

                UsuarioBC objUsuarioBC = new UsuarioBC();
                UsuarioBE objUsuarioBE = new UsuarioBE();
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(objCajaChicaBE.IdUsuarioSolicitante, 0);
                EnviarMensajeAprobado(objCajaChicaBE.IdCajaChica, "Caja Chica", "Rendicion Caja Chica: " + objCajaChicaBE.CodigoCajaChica, objCajaChicaBE.CodigoCajaChica, objUsuarioBE.CardName, estado, objCajaChicaBE.IdUsuarioSolicitante);

                if (estado == "13")
                {
                    objCajaChicaBE.Estado = "4"; //setear a 0 el gasto cuando el contador apruebe y validar en solicitud caja chica q aparezca el monto a reembolsar
                    objCajaChicaBC.ModificarCajaChica(objCajaChicaBE);
                    //EnviarMensajeReembolso(objCajaChicaBE.IdCajaChica, "Caja Chica", "Reembolso Caja Chica: " + objCajaChicaBE.CodigoCajaChica, objCajaChicaBE.CodigoCajaChica, objUsuarioBE.CardName, estado, objCajaChicaBE.IdUsuarioSolicitante);
                }


            }
            else
            {


                Mensaje(mensaje);
            }
        }

        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
        }
        finally
        {
            bAprobar.Enabled = true;
            Response.Redirect("~/CajaChicas.aspx");
        }
    }

    private void EnviarMensajeAprobado(int IdCajaChica, string Documento, string Asunto, string CodigoCajaChica, string UsuarioSolicitante, string estado, int IdUsuarioSolicitante)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();
        if (estado == "11")
        {
            lstUsuarioBE = objUsuarioBC.ListarUsuario(3, 0, 0);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El usuario " + UsuarioSolicitante + " a realizado la rendicion de una " + Documento + " Codigo: " + CodigoCajaChica, Asunto, lstUsuarioBE[i].Mail);
            }
        }
        if (estado == "13")
        {
            UsuarioBE objUsuarioBE = new UsuarioBE();
            objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);
            MensajeMail("La " + Documento + " Codigo: " + CodigoCajaChica + " fue Aprobada", Asunto + " Aprobada", objUsuarioBE.Mail);


            String strIdCajaChica = ViewState["IdCajaChica"].ToString();


            CajaChicaBC objCajaChicaBC = new CajaChicaBC();
            CajaChicaBE objCajaChicaBE = new CajaChicaBE();
            objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);




            List<UsuarioBE> lstUsuarioTesoreriaBE = new List<UsuarioBE>();
            lstUsuarioTesoreriaBE = objUsuarioBC.ListarUsuarioCorreosTesoreria();

            CorreosBE objCorreoBE = new CorreosBE();
            CorreosBC objCorreosBC = new CorreosBC();
            List<CorreosBE> lstCorreosBE = new List<CorreosBE>();



            String moneda = "";
            if (objCajaChicaBE.Moneda.ToString() == "1")
                moneda = "S/. ";
            else
                moneda = "USD. ";



            for (int x = 0; x < lstUsuarioTesoreriaBE.Count; x++)
            {


                if (lstUsuarioTesoreriaBE[x].Mail.ToString() != "")
                {
                    lstCorreosBE = objCorreosBC.ObtenerCorreos(1);
                    MensajeMail(lstCorreosBE[0].TextoCorreo.ToString() + ": La " + Documento + " con Codigo: " + CodigoCajaChica + "<br/>" + "<br/>"
                    // + "Empresa: " + objEmpresaBE.Descripcion + "<br/>"
                    + "Beneficiario :" + objUsuarioBE.CardCode + " - " + objUsuarioBE.CardName + "<br/>"
                    + "Importe a Pagar :" + moneda + objCajaChicaBE.MontoGastado + "<br/>"
                    + lstCorreosBE[0].TextoCorreo.ToString() + "<br/>"
                    , "Caja Chica " + CodigoCajaChica, lstUsuarioTesoreriaBE[x].Mail.ToString());
                }

            }

        }
    }

    private void EnviarMensajeReembolso(int IdCajaChica, string Documento, string Asunto, string CodigoCajaChica, string UsuarioSolicitante, string estado, int IdUsuarioSolicitante)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();
        lstUsuarioBE = objUsuarioBC.ListarUsuario(4, IdCajaChica, 1);
        for (int i = 0; i < lstUsuarioBE.Count; i++)
        {
            MensajeMail("El usuario " + UsuarioSolicitante + " a solicitado el Reembolso de una " + Documento + " Codigo: " + CodigoCajaChica, Asunto, lstUsuarioBE[i].Mail);
        }
    }

    protected void Observacion_Click(object sender, EventArgs e)
    {
        try
        {
            bObservacion.Enabled = false;

            if (txtComentario.Text.Trim() != "")
            {
                String strIdCajaChica = "";
                strIdCajaChica = ViewState["IdCajaChica"].ToString();
                String estado = "0";

                CajaChicaBC objCajaChicaBC = new CajaChicaBC();
                CajaChicaBE objCajaChicaBE = new CajaChicaBE();
                objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);
                estado = objCajaChicaBE.Estado;
                if (objCajaChicaBE.Estado == "11") objCajaChicaBE.Estado = "12";
                else if (objCajaChicaBE.Estado == "13") objCajaChicaBE.Estado = "14";
                objCajaChicaBE.Comentario = txtComentario.Text;
                objCajaChicaBC.ModificarCajaChica(objCajaChicaBE);

                UsuarioBC objUsuarioBC = new UsuarioBC();
                UsuarioBE objUsuarioBE = new UsuarioBE();
                objUsuarioBE = (UsuarioBE)Session["Usuario"];
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);
                EnviarMensajeObservacion(objCajaChicaBE.IdCajaChica, "Caja Chica", "Rendicion Caja Chica: " + objCajaChicaBE.CodigoCajaChica, objCajaChicaBE.CodigoCajaChica, objUsuarioBE.CardName, estado, objCajaChicaBE.IdUsuarioSolicitante);

                Response.Redirect("~/CajaChicas.aspx");
            }
            else
                Mensaje("No a colocado ninguna observacion");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
        }
        finally
        {
            Response.Redirect("~/CajaChicas.aspx");
            bObservacion.Enabled = true;
        }
    }

    private void EnviarMensajeObservacion(int IdCajaChica, string Documento, string Asunto, string CodigoCajaChica, string UsuarioAprobador, string estado, int IdUsuarioSolicitante)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();

        if (estado == "11")
        {
            objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);
            MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoCajaChica, Asunto + " Observacion", objUsuarioBE.Mail);
        }

        //for (int i = 0; i < lstUsuarioBE.Count; i++)
        //{
        //    MensajeMail("El usuario " + UsuarioSolicitante + " a realizado la rendicion de una " + Documento + " Codigo: " + CodigoCajaChica, Asunto, lstUsuarioBE[i].Mail);
        //}

        if (estado == "13")
        {
            objUsuarioBE = objUsuarioBC.ObtenerUsuario(IdUsuarioSolicitante, 0);
            MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoCajaChica, Asunto + " Observacion", objUsuarioBE.Mail);

            lstUsuarioBE = objUsuarioBC.ListarUsuario(4, IdCajaChica, 1);
            for (int i = 0; i < lstUsuarioBE.Count; i++)
            {
                MensajeMail("El Usuario " + UsuarioAprobador + " a colocado una Observacion en la aprobacion de una " + Documento + " Codigo: " + CodigoCajaChica, Asunto + " Observacion", lstUsuarioBE[i].Mail);
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
            }
        }
    }

    private bool ValidarDatosExcel(List<CajaChicaDocumentoBE> lstCajaChicaDocumentoBE)
    {
        for (int i = 0; i <= lstCajaChicaDocumentoBE.Count - 1; i++)
        {
            if (lstCajaChicaDocumentoBE[i].TipoDoc.Trim() == "" ||
               lstCajaChicaDocumentoBE[i].SerieDoc.Trim() == "" ||
               lstCajaChicaDocumentoBE[i].CorrelativoDoc.Trim() == "" ||
               //lstCajaChicaDocumentoBE[i].FechaDoc.Trim() == "" ||
               //lstCajaChicaDocumentoBE[i].IdProveedor.Trim() == "" ||
               //lstCajaChicaDocumentoBE[i].IdConcepto.Trim() == "" ||
               //lstCajaChicaDocumentoBE[i].IdCentroCostos3.Trim() == "" ||
               //lstCajaChicaDocumentoBE[i].IdCentroCostos4.Trim() == "" ||
               //lstCajaChicaDocumentoBE[i].IdCentroCostos5.Trim() == "" ||
               //lstCajaChicaDocumentoBE[i].IdMonedaOriginal.Trim() == "" ||
               //lstCajaChicaDocumentoBE[i].IdMonedaDoc.Trim() == "" ||
               lstCajaChicaDocumentoBE[i].TasaCambio.Trim() == "" ||
               lstCajaChicaDocumentoBE[i].MontoDoc.Trim() == "" ||
               lstCajaChicaDocumentoBE[i].MontoIGV.Trim() == "" ||
               lstCajaChicaDocumentoBE[i].MontoAfecto.Trim() == "" ||
               lstCajaChicaDocumentoBE[i].MontoNoAfecto.Trim() == "" ||
               lstCajaChicaDocumentoBE[i].MontoTotal.Trim() == ""
                )
                return false;
        }

        return true;
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
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

                    String strIdCajaChica = "";
                    strIdCajaChica = ViewState["IdCajaChica"].ToString();

                    objProveedorBE.Proceso = 1;
                    objProveedorBE.IdProceso = Convert.ToInt32(strIdCajaChica);
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
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
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

                String strIdCajaChica = "";
                strIdCajaChica = ViewState["IdCajaChica"].ToString();

                objProveedorBE.Proceso = 1;
                objProveedorBE.IdProceso = Convert.ToInt32(strIdCajaChica);
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
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
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
        Label7.Text = objCajaChicaBE.MontoInicial;
        Label8.Text = "";
        Label9.Text = objCajaChicaBE.CodigoCajaChica;
    }

    private void LlenarCamposCaberaExcel2()
    {
        String strIdCajaChica = "";
        strIdCajaChica = ViewState["IdCajaChica"].ToString();

        CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
        List<CajaChicaDocumentoBE> lstCajaChicaDocumentoBE = new List<CajaChicaDocumentoBE>();
        lstCajaChicaDocumentoBE = objCajaChicaDocumentoBC.ListarCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 4, 0);

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
        gvReporte.DataSource = objCajaChicaDocumentoBC.ListarCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 3, 0);
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

                        if (i == 4)
                        {
                            if (cell.Length > 11)
                                throw new Exception("El RUC Contiene mas de 11 caracteres en la fila :" + row.ToString());

                            long ruc = 0;
                            bool resultado = long.TryParse(cell, out ruc);

                            if (!resultado)
                                throw new Exception("El RUC contiene caracteres no numericos");

                        }

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

            String strIdCajaChica = "";
            strIdCajaChica = ViewState["IdCajaChica"].ToString();
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
                        objProveedorBE.Proceso = 1;
                        objProveedorBE.IdProceso = Convert.ToInt32(strIdCajaChica);
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
                CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
                CajaChicaDocumentoBE objCajaChicaDocumentoBE;

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    objCajaChicaDocumentoBE = new CajaChicaDocumentoBE();
                    objCajaChicaDocumentoBE.IdCajaChica = Convert.ToInt32(strIdCajaChica);
                    objCajaChicaDocumentoBE.IdProveedor = Convert.ToInt32(sIdProveedor[i]);
                    objCajaChicaDocumentoBE.IdConcepto = Convert.ToInt32(GridView1.Rows[i].Cells[6].Text);
                    objCajaChicaDocumentoBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                    objCajaChicaDocumentoBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                    objCajaChicaDocumentoBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                    objCajaChicaDocumentoBE.TipoDoc = GridView1.Rows[i].Cells[0].Text;
                    objCajaChicaDocumentoBE.SerieDoc = GridView1.Rows[i].Cells[1].Text;
                    objCajaChicaDocumentoBE.CorrelativoDoc = GridView1.Rows[i].Cells[2].Text;
                    objCajaChicaDocumentoBE.FechaDoc = Convert.ToDateTime(GridView1.Rows[i].Cells[3].Text);
                    objCajaChicaDocumentoBE.IdMonedaOriginal = Convert.ToInt32(ddlIdMonedaOriginal.SelectedItem.Value);
                    objCajaChicaDocumentoBE.IdMonedaDoc = Convert.ToInt32(GridView1.Rows[i].Cells[7].Text);
                    objCajaChicaDocumentoBE.TasaCambio = Convert.ToDouble(GridView1.Rows[i].Cells[8].Text).ToString("0.0000");
                    objCajaChicaDocumentoBE.MontoNoAfecto = Convert.ToDouble(GridView1.Rows[i].Cells[9].Text).ToString("0.00");
                    objCajaChicaDocumentoBE.MontoAfecto = Convert.ToDouble(GridView1.Rows[i].Cells[10].Text).ToString("0.00");
                    objCajaChicaDocumentoBE.MontoIGV = Convert.ToDouble(GridView1.Rows[i].Cells[11].Text).ToString("0.00");
                    objCajaChicaDocumentoBE.MontoTotal = Convert.ToDouble(GridView1.Rows[i].Cells[12].Text).ToString("0.00");
                    objCajaChicaDocumentoBE.MontoDoc = Convert.ToDouble(GridView1.Rows[i].Cells[13].Text).ToString("0.00");
                    objCajaChicaDocumentoBE.Estado = "1";

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

                        objCajaChicaDocumentoBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                        objCajaChicaDocumentoBE.CreateDate = DateTime.Now;
                        objCajaChicaDocumentoBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                        objCajaChicaDocumentoBE.UpdateDate = DateTime.Now;
                    }
                    int Id;
                    Id = objCajaChicaDocumentoBC.InsertarCajaChicaDocumento(objCajaChicaDocumentoBE);
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
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
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


    //Arturo Rodriguez Liquidar
    protected void bLiquidar_Click(object sender, EventArgs e)
    {
        try
        {

            bLiquidar.Enabled = false;
            bool validar = true;
            String mensaje = "";

            String strIdCajaChica = "";
            strIdCajaChica = ViewState["IdCajaChica"].ToString();
            String estado = "0";

            CajaChicaBC objCajaChicaBC = new CajaChicaBC();
            CajaChicaBE objCajaChicaBE = new CajaChicaBE();
            objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(Convert.ToInt32(strIdCajaChica), 0);
            estado = objCajaChicaBE.Estado;
            if (objCajaChicaBE.Estado == "11") objCajaChicaBE.Estado = "13";
            else if (objCajaChicaBE.Estado == "13")
            {
                if (txtFechaContabilizacion.Text.Trim() != "")
                {
                    CajaChicaDocumentoBC objCajaChicaDocumentoBC = new CajaChicaDocumentoBC();
                    CajaChicaDocumentoBE objCajaChicaDocumentoBE = new CajaChicaDocumentoBE();
                    objCajaChicaDocumentoBE = objCajaChicaDocumentoBC.ObtenerCajaChicaDocumento(Convert.ToInt32(strIdCajaChica), 1);

                    if (objCajaChicaBE.MontoInicial == objCajaChicaDocumentoBE.MontoTotal)
                    {

                        objCajaChicaBE.FechaContabilizacion = Convert.ToDateTime(txtFechaContabilizacion.Text);
                        objCajaChicaBE.Estado = "15";



                        objCajaChicaBE.MontoGastado = objCajaChicaDocumentoBE.MontoTotal;
                        objCajaChicaBE.MontoActual = (Convert.ToDouble(objCajaChicaBE.MontoInicial) - Convert.ToDouble(objCajaChicaDocumentoBE.MontoTotal)).ToString("0.00");
                    }
                    else
                    {
                        validar = false;
                        mensaje = "La caja chica aún cuenta con saldo";
                    }
                }
                else
                {
                    validar = false;
                    mensaje = "Debe ingresar un fecha de contabilizacion.";
                }
            }

            if (validar)
            {
                objCajaChicaBE.Comentario = "";
                objCajaChicaBC.ModificarCajaChica(objCajaChicaBE);

                UsuarioBC objUsuarioBC = new UsuarioBC();
                UsuarioBE objUsuarioBE = new UsuarioBE();
                objUsuarioBE = objUsuarioBC.ObtenerUsuario(objCajaChicaBE.IdUsuarioSolicitante, 0);
                EnviarMensajeAprobado(objCajaChicaBE.IdCajaChica, "Caja Chica", "Rendicion Caja Chica: " + objCajaChicaBE.CodigoCajaChica, objCajaChicaBE.CodigoCajaChica, objUsuarioBE.CardName, estado, objCajaChicaBE.IdUsuarioSolicitante);

                if (estado == "13")
                {
                    objCajaChicaBE.Estado = "16"; //setear estado a liquidada
                    objCajaChicaBC.ModificarCajaChica(objCajaChicaBE);
                    //EnviarMensajeReembolso(objCajaChicaBE.IdCajaChica, "Caja Chica", "Reembolso Caja Chica: " + objCajaChicaBE.CodigoCajaChica, objCajaChicaBE.CodigoCajaChica, objUsuarioBE.CardName, estado, objCajaChicaBE.IdUsuarioSolicitante);
                }


            }
            else
            {


                Mensaje(mensaje);
            }
        }


        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (RendirCajaChica): " + ex.Message);
        }
        finally
        {
            Response.Redirect("~/CajaChicas.aspx");
            bLiquidar.Enabled = true;
        }

    }
    // Arturo Rodriguez Liquidar


    protected void gvReporte_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlConcepto_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}