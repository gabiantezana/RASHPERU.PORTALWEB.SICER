using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;

public partial class GrupoTrabajo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Server.Transfer("~/Login.aspx");
        }

        try
        {
            String strModo = "";
            String strIdUsuario = "";

            if (!this.IsPostBack)
            {
                strModo = Context.Items["Modo"].ToString();
                strIdUsuario = Context.Items["IdUsuario"].ToString();

                ViewState["Modo"] = strModo;
                ViewState["IdUsuario"] = strIdUsuario;

                ListarUsuario();
                ListarGrupoTrabajo();
                ListarFiltro();
                Modalidad(Convert.ToInt32(strModo));
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (GrupoTrabajo): " + ex.Message);
        }
    }

    private void ListarUsuario()
    {
        try
        {
            String strIdUsuario = "";
            strIdUsuario = ViewState["IdUsuario"].ToString();

            UsuarioBC objUsuarioBC = new UsuarioBC();
            List<UsuarioBE> lstUsuarioBE = new List<UsuarioBE>();
            lstUsuarioBE = objUsuarioBC.ListarUsuario(8, Convert.ToInt32(strIdUsuario), 0);
            ddlEmpleados.Items.Clear();
            ddlEmpleados.DataSource = lstUsuarioBE;
            ddlEmpleados.DataTextField = "CardName";
            ddlEmpleados.DataValueField = "IdUsuario";
            ddlEmpleados.DataBind();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (GrupoTrabajo): " + ex.Message);
        }
    }

    private void ListarGrupoTrabajo()
    {
        try
        {
            String strIdUsuario = "";
            strIdUsuario = ViewState["IdUsuario"].ToString();

            UsuarioBC objUsarioBC = new UsuarioBC();
            gvGrupoTrabajos.DataSource = objUsarioBC.ListarUsuario(9, Convert.ToInt32(strIdUsuario), 0);
            gvGrupoTrabajos.DataBind();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (GrupoTrabajo): " + ex.Message);
        }
    }

    private void ListarFiltro()
    {
        try
        {
            ddlFiltro.Items.Clear();
            ListItem oItem = new ListItem("Seleccionar", "0");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Nombre", "1");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("DNI", "2");
            ddlFiltro.Items.Add(oItem);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
        }
    }

    private void Modalidad(int p)
    {
        try
        {
            switch (p)
            {
                case 1:                    
                    String strIdUsuario = "";
                    strIdUsuario = ViewState["IdUsuario"].ToString();
                    UsuarioBC objUsuarioBC = new UsuarioBC();
                    UsuarioBE objUsuarioBE = new UsuarioBE();
                    objUsuarioBE = objUsuarioBC.ObtenerUsuario(Convert.ToInt32(strIdUsuario), 0);
                    lblCabezera.Text = "Empleados al cargo de " + objUsuarioBE.CardName;
                    //LimpiarCampos();
                    break;
                //case 2:
                    //lblCabezera.Text = "Modificar Usuario";
                    //bCrear.Text = "Guardar";
                    //LlenarCampos(Convert.ToInt32(ViewState["IdUsuario"].ToString()));
                    //break;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (GrupoTrabajo): " + ex.Message);
        }
    }

    protected void gvGrupoTrabajos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int IdUsuario;

        try
        {
            IdUsuario = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("Eliminar"))
            {
                UsuarioBC objUsuarioBC = new UsuarioBC();
                objUsuarioBC.EliminarUsuario(1, IdUsuario);
                ListarUsuario();
                ListarGrupoTrabajo();
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (GrupoTrabajo): " + ex.Message);
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGrupoTrabajos.PageIndex = e.NewPageIndex;

        String strIdUsuario = "";
        strIdUsuario = ViewState["IdUsuario"].ToString();

        UsuarioBC objUsarioBC = new UsuarioBC();
        gvGrupoTrabajos.DataSource = objUsarioBC.ListarUsuario2(Convert.ToInt32(ddlFiltro.SelectedValue), 1, Convert.ToInt32(strIdUsuario), txtPalabra.Text);
        gvGrupoTrabajos.DataBind();

        //ListarGrupoTrabajo();
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

    protected void bAgregar_Click(object sender, EventArgs e)
    {
        if (ddlEmpleados.SelectedItem.Value != "0")
        {
            String strIdUsuario = "";
            strIdUsuario = ViewState["IdUsuario"].ToString();

            GrupoTrabajoBC objGrupoTrabajoBC = new GrupoTrabajoBC();
            GrupoTrabajoBE objGrupoTrabajoBE = new GrupoTrabajoBE();

            objGrupoTrabajoBE.IdUsuarioNivel = Convert.ToInt32(strIdUsuario);
            objGrupoTrabajoBE.IdUsuarioSubNivel = Convert.ToInt32(ddlEmpleados.SelectedItem.Value);

            if (Session["Usuario"] == null)
            {
                Server.Transfer("~/Login.aspx");
            }
            else
            {
                UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                objGrupoTrabajoBE.UserCreate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                objGrupoTrabajoBE.CreateDate = DateTime.Now;
                objGrupoTrabajoBE.UserUpdate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                objGrupoTrabajoBE.UpdateDate = DateTime.Now;
            }

            int id = objGrupoTrabajoBC.InsertarGrupoTrabajo(objGrupoTrabajoBE);

            ListarUsuario();
            ListarGrupoTrabajo();
            ddlEmpleados.SelectedValue = "0";
        }
        else
            Mensaje("Debe seleccionar un empleado");
    }

    protected void Buscar2_Click(object sender, EventArgs e)
    {
        String strIdUsuario = "";
        strIdUsuario = ViewState["IdUsuario"].ToString();

        UsuarioBC objUsarioBC = new UsuarioBC();
        gvGrupoTrabajos.DataSource = objUsarioBC.ListarUsuario2(Convert.ToInt32(ddlFiltro.SelectedValue), 1, Convert.ToInt32(strIdUsuario), txtPalabra.Text);
        gvGrupoTrabajos.DataBind();
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
}