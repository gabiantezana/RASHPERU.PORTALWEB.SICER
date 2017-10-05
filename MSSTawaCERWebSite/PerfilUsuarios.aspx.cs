using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;

public partial class PerfilUsuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Server.Transfer("~/Login.aspx");
        }

        if (!this.IsPostBack)
        {
            ListarPerfilUsuario();
        }
    }

    private void ListarPerfilUsuario()
    {
        try
        {
            PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
            gvPerfilUsuarios.DataSource = objPerfilUsuarioBC.ListarPerfilUsuario();
            gvPerfilUsuarios.DataBind();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (PerfilUsuario): " + ex.Message);
        }
    }

    protected void lnkNuevaPerfilUsuario_Click(object sender, EventArgs e)
    {
        Context.Items.Add("Modo", 1);
        Context.Items.Add("IdPerfilUsuario", 0);
        Server.Transfer("~/PerfilUsuario.aspx");
    }

    protected void gvPerfilUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int IdPerfilUsuario;

        try
        {
            IdPerfilUsuario = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("Editar"))
            {
                Context.Items.Add("Modo", 2);
                Context.Items.Add("IdPerfilUsuario", IdPerfilUsuario);
                Server.Transfer("~/PerfilUsuario.aspx");
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (PerfilUsuario): " + ex.Message);
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPerfilUsuarios.PageIndex = e.NewPageIndex;
        ListarPerfilUsuario();
    }

    public String SetearTipo(String sTipoAprobador)
    {
        String texto = "";
        switch (sTipoAprobador)
        {
            case "1": texto = "Aprobador"; break;
            case "2": texto = "Contabilidad"; break;
            case "3": texto = "Creador"; break;
            case "4": texto = "Aprobador y Creador"; break;
            case "5": texto = "Contabilidad y Creador"; break;
            case "6": texto = "Administrador Web"; break;
        }
        return texto;
    }

    public String SetearMod(String sArea)
    {
        String texto = "";
        switch (sArea)
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