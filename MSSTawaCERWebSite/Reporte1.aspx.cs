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

public partial class Reporte1 : System.Web.UI.Page
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

            if (!this.IsPostBack)
            {
                ListarEmpresa();
                ListarUsuario();
                ListarEsFacturable();
                ListarEstado();
                ListarDocumento();
                //ListarRendicion();
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Reporte1): " + ex.Message);
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

    private void ListarEsFacturable()
    {
        try
        {
            ddlEsFacturable.Items.Clear();
            ListItem oItem = new ListItem("Seleccionar", "0");
            ddlEsFacturable.Items.Add(oItem);
            oItem = new ListItem("Si", "1");
            ddlEsFacturable.Items.Add(oItem);
            oItem = new ListItem("No", "2");
            ddlEsFacturable.Items.Add(oItem);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
        }
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
        oItem = new ListItem("Caja Chica Liquidada", "16");
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

    private void ListarDocumento()
    {
        try
        {
            ddlDocumento.Items.Clear();
            ListItem oItem = new ListItem("Seleccionar", "0");
            ddlDocumento.Items.Add(oItem);
            oItem = new ListItem("Caja Chica", "1");
            ddlDocumento.Items.Add(oItem);
            oItem = new ListItem("Entrega a Rendir", "2");
            ddlDocumento.Items.Add(oItem);
            oItem = new ListItem("Reembolso", "3");
            ddlDocumento.Items.Add(oItem);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
        }
    }

    private void ListarRendicion()
    {
        ReporteBC objReporteBC = new ReporteBC();
        gvReporte1.DataSource = objReporteBC.ListarReporte("0", "0", "0", "0", "0", "", "", "0", "0", "0", "0", "0", "0");
        gvReporte1.DataBind();
    }

    protected void ddlIdEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdEmpresa.SelectedValue != "0")
        {
            CentroCostosBC objCentroCostosBC = new CentroCostosBC();
            ddlCentroCostos3.DataSource = objCentroCostosBC.ListarCentroCostos(0, 12, Convert.ToInt32(ddlIdEmpresa.SelectedValue));
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

    protected void ddlCentroCostos3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCentroCostos3.SelectedValue != "0")
        {
            CentroCostosBC objConceptoBC = new CentroCostosBC();
            ddlCentroCostos4.DataSource = objConceptoBC.ListarCentroCostos(Convert.ToInt32(ddlCentroCostos3.SelectedValue), 9, Convert.ToInt32(ddlIdEmpresa.SelectedValue));
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
            ddlCentroCostos5.DataSource = objCentroCostosBC.ListarCentroCostos(Convert.ToInt32(ddlCentroCostos4.SelectedValue), 11, Convert.ToInt32(ddlIdEmpresa.SelectedValue));
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

    protected void Buscar_Click(object sender, EventArgs e)
    {
        ReporteBC objReporteBC = new ReporteBC();
        gvReporte1.DataSource = objReporteBC.ListarReporte(ddlNombre_Solicitante.SelectedValue, ddlCentroCostos3.SelectedValue, ddlCentroCostos4.SelectedValue, ddlCentroCostos5.SelectedValue, ddlEsFacturable.SelectedValue, txtFechaSolicitudIni.Text, txtFechaSolicitudFin.Text, ddlEstado.SelectedValue, ddlDocumento.SelectedValue, ddlIdEmpresa.SelectedValue, "", "", "");
        gvReporte1.DataBind();
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReporte1.PageIndex = e.NewPageIndex;

        ReporteBC objReporteBC = new ReporteBC();
        gvReporte1.DataSource = objReporteBC.ListarReporte(ddlNombre_Solicitante.SelectedValue, ddlCentroCostos3.SelectedValue, ddlCentroCostos4.SelectedValue, ddlCentroCostos5.SelectedValue, ddlEsFacturable.SelectedValue, txtFechaSolicitudIni.Text, txtFechaSolicitudFin.Text, ddlEstado.SelectedValue, ddlDocumento.SelectedValue, ddlIdEmpresa.SelectedValue, "", "", "");
        gvReporte1.DataBind();
    }

    protected void lnkExportarReporte_Click(object sender, EventArgs e)
    {
        try
        {
            ListarReporte();

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pageToRender = new Page();
            HtmlForm form = new HtmlForm();
            form.Controls.Add(gvReporte);
            pageToRender.Controls.Add(form);
            String nameReport = "Reporte_WEB";
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Pragma", "no-cache");
            Response.AddHeader("Content-Disposition", "attachment;filename=" + nameReport + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            pageToRender.RenderControl(htw);
            Response.Write(sw.ToString().Normalize());
            Response.End();
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

    private void ListarReporte()
    {
        ReporteBC objReporteBC = new ReporteBC();
        gvReporte.DataSource = objReporteBC.ListarReporte(ddlNombre_Solicitante.SelectedValue, ddlCentroCostos3.SelectedValue, ddlCentroCostos4.SelectedValue, ddlCentroCostos5.SelectedValue, ddlEsFacturable.SelectedValue, txtFechaSolicitudIni.Text, txtFechaSolicitudFin.Text, ddlEstado.SelectedValue, ddlDocumento.SelectedValue, ddlIdEmpresa.SelectedValue, "", "", "");
        gvReporte.DataBind();
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
}