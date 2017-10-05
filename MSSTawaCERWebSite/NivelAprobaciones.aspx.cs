using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;

public partial class NivelAprobaciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Server.Transfer("~/Login.aspx");
        }

        if (!this.IsPostBack)
        {
            ListarNivelAprobacion();
        }
    }

    private void ListarNivelAprobacion()
    {
        try
        {
            NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
            gvNivelAprobaciones.DataSource = objNivelAprobacionBC.ListarNivelAprobacion();
            gvNivelAprobaciones.DataBind();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    protected void lnkNuevaNivelAprobacion_Click(object sender, EventArgs e)
    {
        Context.Items.Add("Modo", 1);
        Context.Items.Add("IdNivel", 0);
        Server.Transfer("~/NivelAprobacion.aspx");
    }

    protected void gvNivelAprobaciones_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int IdNivelAprobacion;

        try
        {
            IdNivelAprobacion = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("Editar"))
            {
                Context.Items.Add("Modo", 2);
                Context.Items.Add("IdNivel", IdNivelAprobacion);
                Server.Transfer("~/NivelAprobacion.aspx");
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNivelAprobaciones.PageIndex = e.NewPageIndex;
        ListarNivelAprobacion();
    }

    public String SetearDocumento(String sTipoAprobador)
    {
        String texto = "";
        switch (sTipoAprobador)
        {
            case "0": texto = "Falta Seleccionar"; break;
            case "1": texto = "Caja Chica"; break;
            case "2": texto = "Entrega a Rendir"; break;
            case "3": texto = "Reembolso"; break;
        }
        return texto;
    }

    public String SetearEsDeMonto(String sTipoAprobador)
    {
        String texto = "";
        switch (sTipoAprobador)
        {
            case "0": texto = "Falta Seleccionar"; break;
            case "1": texto = "Si"; break;
            case "2": texto = "No"; break;
        }
        return texto;
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
}