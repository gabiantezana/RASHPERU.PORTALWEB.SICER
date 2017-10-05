using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;

public partial class Usuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Server.Transfer("~/Login.aspx");
        }

        if (!this.IsPostBack)
        {
            ListarUsuario();
            //ListarUsuarioFiltro();
            ListarFiltro();
        }
    }

    private void ListarUsuario()
    {
        try
        {
            UsuarioBC objUsarioBC = new UsuarioBC();
            gvUsuarios.DataSource = objUsarioBC.ListarUsuario(0, 0, 0);
            gvUsuarios.DataBind();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuarios): " + ex.Message);
        }
    }

    //private void ListarUsuarioFiltro()
    //{
    //    UsuarioBC objUsarioBC = new UsuarioBC();
    //    List<UsuarioBE> lstUsarioBE = new List<UsuarioBE>();

    //    ddlNombre.DataSource = objUsarioBC.ListarUsuario(12, 0, 0);
    //    ddlNombre.DataTextField = "CardName";
    //    ddlNombre.DataValueField = "IdUsuario";
    //    ddlNombre.DataBind();
    //}

    private void ListarFiltro()
    {
        try
        {
            ddlFiltro.Items.Clear();
            ListItem oItem = new ListItem("Seleccionar", "0");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Nombre Usuario", "1");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Nombre Usuario a Cargo", "2");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("DNI", "3");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Tipo Usuario", "4");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Estado", "5");
            ddlFiltro.Items.Add(oItem);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
        }
    }

    protected void lnkNuevaUsuario_Click(object sender, EventArgs e)
    {
        Context.Items.Add("Modo", 1);
        Context.Items.Add("IdUsuario", 0);
        Server.Transfer("~/Usuario.aspx");
    }

    protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int IdUsuario;

        try
        {
            IdUsuario = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("Editar"))
            {
                Context.Items.Add("Modo", 2);
                Context.Items.Add("IdUsuario", IdUsuario);
                Server.Transfer("~/Usuario.aspx");
            }

            if (e.CommandName.Equals("GrupoTrabajo"))
            {
                Context.Items.Add("Modo", 1);
                Context.Items.Add("IdUsuario", IdUsuario);
                Server.Transfer("~/GrupoTrabajo.aspx");
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuarios): " + ex.Message);
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsuarios.PageIndex = e.NewPageIndex;

        UsuarioBC objUsarioBC = new UsuarioBC();
        if (ddlFiltro.SelectedValue == "4" || ddlFiltro.SelectedValue == "5")
        {
            gvUsuarios.DataSource = objUsarioBC.ListarUsuario2(Convert.ToInt32(ddlFiltro.SelectedValue), 0, Convert.ToInt32(ddlFiltro2.SelectedValue), txtPalabra.Text);
            gvUsuarios.DataBind();
        }
        else
        {
            gvUsuarios.DataSource = objUsarioBC.ListarUsuario2(Convert.ToInt32(ddlFiltro.SelectedValue), 0, 0, txtPalabra.Text);
            gvUsuarios.DataBind();
        }
        //ListarUsuario();
    }

    public String SetearTipo(String sTipo)
    {
        String Tipo = "";
        try
        {
            switch (sTipo)
            {
                case "0": Tipo = "Falta Seleccionar"; break;
                case "1": Tipo = "Usuario Interno WEB"; break;
                case "2": Tipo = "Empleado"; break;
                case "3": Tipo = "Usuario Externo WEB"; break;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuarios): " + ex.Message);
        }
        return Tipo;
    }

    public String SetearPerfilUsuario(String sId)
    {
        String Tipo = "";
        PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
        PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBE();
        objPerfilUsuarioBE = objPerfilUsuarioBC.ObtenerPerfilUsuario(Convert.ToInt32(sId));
        if (objPerfilUsuarioBE != null) Tipo = objPerfilUsuarioBE.Descripcion;
        else Tipo = "";
        return Tipo;
    }

    public String SetearEstado(String sEstado)
    {
        String Estado = "";
        try
        {
            switch (sEstado)
            {
                case "1": Estado = "Habilitado"; break;
                case "2": Estado = "Deshabilitado"; break;
                case "3": Estado = "Bloqueado"; break;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuarios): " + ex.Message);
        }
        return Estado;
    }

    public String SetearArea(String sId)
    {
        String Tipo = "";
        AreaBC objAreaBC = new AreaBC();
        AreaBE objAreaBE = new AreaBE();
        objAreaBE = objAreaBC.ObtenerArea(Convert.ToInt32(sId));
        if (objAreaBE != null) Tipo = objAreaBE.Descripcion;
        else Tipo = "";
        return Tipo;
    }

    public String SetearCargoUsuario(String sId)
    {
        String Tipo = "";
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = objUsuarioBC.ObtenerUsuario(Convert.ToInt32(sId), 1);
        if (objUsuarioBE != null) Tipo = objUsuarioBE.CardName;
        else Tipo = "";
        return Tipo;
    }    

    protected void Buscar_Click(object sender, EventArgs e)
    {
        //UsuarioBC objUsarioBC = new UsuarioBC();
        //gvUsuarios.DataSource = objUsarioBC.ListarUsuario(13, Convert.ToInt32(ddlNombre.SelectedValue), 0);
        //gvUsuarios.DataBind();
    }

    protected void Buscar2_Click(object sender, EventArgs e)
    {
        UsuarioBC objUsarioBC = new UsuarioBC();
        if (ddlFiltro.SelectedValue == "4" || ddlFiltro.SelectedValue == "5")
        {
            gvUsuarios.DataSource = objUsarioBC.ListarUsuario2(Convert.ToInt32(ddlFiltro.SelectedValue), 0, Convert.ToInt32(ddlFiltro2.SelectedValue), txtPalabra.Text);
            gvUsuarios.DataBind();
        }
        else
        {
            gvUsuarios.DataSource = objUsarioBC.ListarUsuario2(Convert.ToInt32(ddlFiltro.SelectedValue), 0, 0, txtPalabra.Text);
            gvUsuarios.DataBind();
        }
    }

    protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFiltro.SelectedValue == "4")
        {
            txtPalabra.Visible = false;
            ddlFiltro2.Visible = true;

            ddlFiltro2.Items.Clear();
            ListItem oItem = new ListItem("Seleccionar", "0");
            ddlFiltro2.Items.Add(oItem);
            oItem = new ListItem("Usuario Interno WEB", "1");
            ddlFiltro2.Items.Add(oItem);
            oItem = new ListItem("Empleado", "2");
            ddlFiltro2.Items.Add(oItem);
            oItem = new ListItem("Usuario Externo WEB", "3");
            ddlFiltro2.Items.Add(oItem);
        }
        else
        {
            if (ddlFiltro.SelectedValue == "5")
            {
                txtPalabra.Visible = false;
                ddlFiltro2.Visible = true;

                ddlFiltro2.Items.Clear();
                ListItem oItem = new ListItem("Seleccionar", "0");
                ddlFiltro2.Items.Add(oItem);
                oItem = new ListItem("Habilitado", "1");
                ddlFiltro2.Items.Add(oItem);
                oItem = new ListItem("Deshabilitado", "2");
                ddlFiltro2.Items.Add(oItem);
                oItem = new ListItem("Bloqueado", "3");
                ddlFiltro2.Items.Add(oItem);
            }
            else
            {
                txtPalabra.Visible = true;
                ddlFiltro2.Visible = false;
            }
        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
}