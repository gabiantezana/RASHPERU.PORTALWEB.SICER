using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;
using MssTawaCer.App_Code.Helper;

public partial class EntregasRendir : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Server.Transfer("~/Login.aspx");
        }

        if (!this.IsPostBack)
        {
            ListarFiltro();
            ListarEstado();
            ListarUsuario();
            ListarEsFacturable();
            
            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlEsFacturable.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = false;

            ValidarMenu();
        }
    }
    private void ListarEntregaRendirTodo()
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];
        EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
        gvEntregaRendir.DataSource = objEntregaRendirBC.ListarEntregaRendir(objUsuarioBE.IdUsuario, 1, 0, "", "", "", "", "");
        gvEntregaRendir.DataBind();
    }
    private void ListarEntregaRendir(int idPerfil)
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];
        

        EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
        if (idPerfil != 2 && idPerfil != 1002 && idPerfil != 1008)
            gvEntregaRendir.DataSource = objEntregaRendirBC.ListarEntregaRendir(objUsuarioBE.IdUsuario, 1, 0, "", "", "", "", "");
        else
        {
            ddlFiltro .SelectedIndex = 5;
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEsFacturable.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 7;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlEsFacturable.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = true;
            bBuscar.Enabled = true;
            gvEntregaRendir.DataSource = objEntregaRendirBC.ListarEntregaRendir(objUsuarioBE.IdUsuario, 3, 0, "", "", "", "", "17");
        }

        gvEntregaRendir.DataBind();
    }

    private void ListarFiltros()
    {
        ddlFiltro.Items.Clear();
        ListItem oItem = new ListItem("Todos", "0");
        ddlFiltro.Items.Add(oItem);
        oItem = new ListItem("Por Aprobar Solicitud", "1"); //"Por Aprobar Nivel 1" "Por Aprobar Nivel 2" "Por Aprobar Nivel 3" 
        ddlFiltro.Items.Add(oItem);
        oItem = new ListItem("Aprobado", "4");
        ddlFiltro.Items.Add(oItem);
        oItem = new ListItem("Rechazado", "5");
        ddlFiltro.Items.Add(oItem);
        oItem = new ListItem("Observaciones a Solicitud", "8"); //"Observaciones Nivel 1" "Observaciones Nivel 2" "Observaciones Nivel 3" 
        ddlFiltro.Items.Add(oItem);      
        oItem = new ListItem("Por Aprobar Rendicion", "11"); //"Rendir: Por Aprobar Nivel 1" "Rendir: Por Aprobar Nivel 2" "Rendir: Por Aprobar Nivel 3"        
        ddlFiltro.Items.Add(oItem);
        oItem = new ListItem("Observaciones Rendicion", "12"); //"Rendir: Observaciones Nivel 1" "Rendir: Observaciones Nivel 2" "Rendir: Observaciones Nivel 3"        
        ddlFiltro.Items.Add(oItem);
        oItem = new ListItem("Por Aprobar Contabilidad", "17");
        ddlFiltro.Items.Add(oItem);
        oItem = new ListItem("Observaciones Contabilidad", "18");
        ddlFiltro.Items.Add(oItem);
        oItem = new ListItem("Rendicion Aprobadas", "19");
        ddlFiltro.Items.Add(oItem);
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
            ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (CajaChica): " + ex.Message);
        }
    }

    private void ListarFiltro()
    {
        try
        {
            ddlFiltro.Items.Clear();
            ListItem oItem = new ListItem("Filtrar Por", "0");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Código Documento", "1");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("DNI", "2");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Nombre Solicitante", "3");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Es Facturable", "4");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Estado", "5");
            ddlFiltro.Items.Add(oItem);
            oItem = new ListItem("Sin Filtro", "6");
            ddlFiltro.Items.Add(oItem);

        }
        catch (Exception ex)
        {
            ExceptionHelper.LogException(ex);
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

    private void ListarEstado()
    {

        //EstadoBC objEstadoBC = new EstadoBC();
        //List<EstadoBE> lstEstadosBE = new List<EstadoBE>();

        //ddlEstado.DataSource = objEstadoBC.ListarEntregaRendir();
        //ddlEstado.DataTextField = "EstadoNombre";
        //ddlEstado.DataValueField = "EstadoCode";
        //ddlEstado.DataBind();
        ddlEstado.Items.Clear();
        ListItem oItem = new ListItem("Todos", "0");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Por Aprobar Solicitud", "1"); //"Por Aprobar Nivel 1" "Por Aprobar Nivel 2" "Por Aprobar Nivel 3" 
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Aprobado", "4");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Rechazado", "5");
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

    private void ValidarMenu()
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];
        objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);

        PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
        PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBE();
        objPerfilUsuarioBE = objPerfilUsuarioBC.ObtenerPerfilUsuario(objUsuarioBE.IdPerfilUsuario);

        ListarEntregaRendir(objPerfilUsuarioBE.IdPerfilUsuario);

        if (objPerfilUsuarioBE.CreaEntregaRendir == "1") lnkNuevaEntregaRendir.Visible = true;
        else lnkNuevaEntregaRendir.Visible = false;
    }

    protected void gvEntregaRendir_RowCommand(object sender, GridViewCommandEventArgs e)    
    {
        int IdEntregaRendir;

        try
        {
            EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
            EntregaRendirBE objEntregaRendirBE = new EntregaRendirBE();
            IdEntregaRendir = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("Aprobacion"))
            {
                objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(IdEntregaRendir, 0);

                if (objEntregaRendirBE.Estado == "4" || objEntregaRendirBE.Estado == "11" || objEntregaRendirBE.Estado == "12" ||
                    objEntregaRendirBE.Estado == "13" || objEntregaRendirBE.Estado == "14" || objEntregaRendirBE.Estado == "15" ||
                    objEntregaRendirBE.Estado == "16" || objEntregaRendirBE.Estado == "17" || objEntregaRendirBE.Estado == "18" ||
                    objEntregaRendirBE.Estado == "19")
                {
                    Context.Items.Add("Modo", 1);
                    Context.Items.Add("IdEntregaRendir", IdEntregaRendir);
                    Server.Transfer("~/RendirEntregaRendir.aspx");
                }
                else
                {
                    Context.Items.Add("Modo", 2);
                    Context.Items.Add("IdEntregaRendir", IdEntregaRendir);
                    Server.Transfer("~/EntregaRendir.aspx");
                }

            }
            if (e.CommandName.Equals("Rendir"))
            {
                objEntregaRendirBE = objEntregaRendirBC.ObtenerEntregaRendir(IdEntregaRendir, 0);

                if (objEntregaRendirBE.Estado == "4" || objEntregaRendirBE.Estado == "11" || objEntregaRendirBE.Estado == "12" ||
                    objEntregaRendirBE.Estado == "13" || objEntregaRendirBE.Estado == "14" || objEntregaRendirBE.Estado == "15" ||
                    objEntregaRendirBE.Estado == "16" || objEntregaRendirBE.Estado == "17" || objEntregaRendirBE.Estado == "18" ||
                    objEntregaRendirBE.Estado == "19")
                {
                    Context.Items.Add("Modo", 1);
                    Context.Items.Add("IdEntregaRendir", IdEntregaRendir);
                    Server.Transfer("~/RendirEntregaRendir.aspx");
                }
                else
                    Mensaje("La Entrega a Rendir aun no ah sido Aprobada");
            }
            if (e.CommandName.Equals("Detalle"))
            {
                Context.Items.Add("Modo", 2);
                Context.Items.Add("IdEntregaRendir", IdEntregaRendir);
                Server.Transfer("~/EntregaRendir.aspx");
            }
            if (e.CommandName.Equals("Solicitud"))
            {
                Context.Items.Add("Modo", 2);
                Context.Items.Add("IdEntregaRendir", IdEntregaRendir);
                Server.Transfer("~/EntregaRendir.aspx");
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
        gvEntregaRendir.PageIndex = e.NewPageIndex;
        Buscar_Click(null, null);
    } 

    public String SetearIdUsuarioSolicitante(String sId)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = objUsuarioBC.ObtenerUsuario(Convert.ToInt32(sId), 0);
        return objUsuarioBE.CardName;
    }

    public String SetearIdArea(String sId)
    {
        AreaBC objAreaBC = new AreaBC();
        AreaBE objAreaBE = new AreaBE();
        objAreaBE = objAreaBC.ObtenerArea(Convert.ToInt32(sId));
        return objAreaBE.Descripcion;
    }

    public String SetearMoneda(String sId)
    {
        MonedaBC objMonedaBC = new MonedaBC();
        MonedaBE objMonedaBE = new MonedaBE();
        objMonedaBE = objMonedaBC.ObtenerMoneda(Convert.ToInt32(sId));
        return objMonedaBE.Descripcion;
    }    

    public String SetearEsFacturable(String sId)
    {
        String texto = "";
        switch (sId)
        {
            case "0": texto = "Falta Seleccionar"; break;
            case "1": texto = "Si"; break;
            case "2": texto = "No"; break;
        }
        return texto;
    }

    public String SetearMomentoFacturable(String sId)
    {
        String texto = "";
        switch (sId)
        {
            case "0": texto = "Falta Seleccionar"; break;
            case "1": texto = "Al entregar el dinero"; break;
            case "2": texto = "Al rendir los documentos"; break;
        }
        return texto;
    }

    public String SetearEstado(String sId)
    {
        String texto = "";
        switch (sId)
        {
            case "1": texto = "Por Aprobar Nivel 1"; break;
            case "2": texto = "Por Aprobar Nivel 2"; break;
            case "3": texto = "Por Aprobar Nivel 3"; break;
            case "4": texto = "Aprobado"; break;
            case "5": texto = "Rechazado"; break;
            //case "5": texto = "Rechazado Nivel 1"; break;
            //case "6": texto = "Rechazado Nivel 2"; break;
            //case "7": texto = "Rechazado Nivel 3"; break;
            case "8": texto = "Observaciones Nivel 1"; break;
            case "9": texto = "Observaciones Nivel 2"; break;
            case "10": texto = "Observaciones Nivel 3"; break;
            case "11": texto = "Rendir: Por Aprobar Nivel 1"; break;
            case "12": texto = "Rendir: Observaciones Nivel 1"; break;
            case "13": texto = "Rendir: Por Aprobar Nivel 2"; break;
            case "14": texto = "Rendir: Observaciones Nivel 2"; break;
            case "15": texto = "Rendir: Por Aprobar Nivel 3"; break;
            case "16": texto = "Rendir: Observaciones Nivel 3"; break;
            case "17": texto = "Rendir: Por Aprobar Contabilidad"; break;
            case "18": texto = "Rendir: Observaciones Contabilidad"; break;
            case "19": texto = "Rendir: Aprobado"; break;
        }
        return texto;
    }

    public String SetearIdEmpresa(String sId)
    {
        EmpresaBC objEmpresaBC = new EmpresaBC();
        EmpresaBE objEmpresaBE = new EmpresaBE();
        objEmpresaBE = objEmpresaBC.ObtenerEmpresa(Convert.ToInt32(1));
        return objEmpresaBE.Descripcion;
    }        

    protected void lnkNuevaEntregaRendir_Click(object sender, EventArgs e)
    {
        Context.Items.Add("Modo", 1);
        Context.Items.Add("IdEntregaRendir", 0);
        Server.Transfer("~/EntregaRendir.aspx");
    }

    protected void Buscar_Click(object sender, EventArgs e)
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];

        EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
        if (ddlFiltro.SelectedItem.Value == "1")
        {
            gvEntregaRendir.DataSource = objEntregaRendirBC.ListarEntregaRendir(objUsuarioBE.IdUsuario, 1, 0, txtCodigo.Text, "", "", "", "");
            gvEntregaRendir.DataBind();
        }
        else if (ddlFiltro.SelectedItem.Value == "2")
        {
            gvEntregaRendir.DataSource = objEntregaRendirBC.ListarEntregaRendir(objUsuarioBE.IdUsuario, 1, 0, "", txtDni.Text, "", "", "");
            gvEntregaRendir.DataBind();
        }
        else if (ddlFiltro.SelectedItem.Value == "3")
        {
            gvEntregaRendir.DataSource = objEntregaRendirBC.ListarEntregaRendir(objUsuarioBE.IdUsuario, 1, 0, "", "", ddlNombre_Solicitante.SelectedValue, "", "");
            gvEntregaRendir.DataBind();
        }
        else if (ddlFiltro.SelectedItem.Value == "4")
        {
            gvEntregaRendir.DataSource = objEntregaRendirBC.ListarEntregaRendir(objUsuarioBE.IdUsuario, 1, 0, "", "", "", ddlEsFacturable.SelectedValue, "");
            gvEntregaRendir.DataBind();
        }

        else if (ddlFiltro.SelectedItem.Value == "5")
        {
            gvEntregaRendir.DataSource = objEntregaRendirBC.ListarEntregaRendir(objUsuarioBE.IdUsuario, 3, 0, "", "", "", "", ddlEstado.SelectedValue);
            gvEntregaRendir.DataBind();
        }
        else
        {
            ListarEntregaRendirTodo();
        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
    private void LimpiarEntrega()
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];
        gvEntregaRendir.DataSource = null;
        gvEntregaRendir.DataBind();
    }
    private void ListarEntrega()
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];

        EntregaRendirBC objEntregaRendirBC = new EntregaRendirBC();
        gvEntregaRendir.DataSource = objEntregaRendirBC.ListarEntregaRendir(objUsuarioBE.IdUsuario, 1, 0, "", "", "", "", "");
        gvEntregaRendir.DataBind();
    }
    protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFiltro.SelectedValue == "0")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEsFacturable.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlEsFacturable.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = false;
            LimpiarEntrega();
            ListarEntregaRendirTodo();


        }
        if (ddlFiltro.SelectedValue == "1")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEsFacturable.SelectedIndex = 0;

            ddlEstado.SelectedIndex = 0;
            txtCodigo.Enabled = true;
            txtDni.Enabled = false;
            ddlEsFacturable.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarEntrega();
        }
        if (ddlFiltro.SelectedValue == "2")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEsFacturable.SelectedIndex = 0;

            ddlEstado.SelectedIndex = 0;
            txtCodigo.Enabled = false;
            txtDni.Enabled = true;
            ddlEsFacturable.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarEntrega();
        }
        if (ddlFiltro.SelectedValue == "3")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEsFacturable.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlEsFacturable.Enabled = false;
            ddlNombre_Solicitante.Enabled = true;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarEntrega();
        }
        if (ddlFiltro.SelectedValue == "4")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEsFacturable.SelectedIndex = 0;

            ddlEstado.SelectedIndex = 0;
            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlEsFacturable.Enabled = true;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarEntrega();
        }
        if (ddlFiltro.SelectedValue == "5")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEsFacturable.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlEsFacturable.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = true;
            bBuscar.Enabled = true;
            LimpiarEntrega();
        }
        if (ddlFiltro.SelectedValue == "6")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEsFacturable.SelectedIndex = 0;

            ddlEstado.SelectedIndex = 0;
            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlEsFacturable.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = false;
            ListarEntregaRendirTodo();
        }
    }
}
