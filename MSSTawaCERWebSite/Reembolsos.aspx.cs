using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;

public partial class Reembolsos : System.Web.UI.Page
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

    private void ListarReembolsoTodo()
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];

        ReembolsoBC objReembolsoBC = new ReembolsoBC();
        gvReembolso.DataSource = objReembolsoBC.ListarReembolso(objUsuarioBE.IdUsuario, 1, 0, "", "", "", "", "");
        gvReembolso.DataBind();
    }


    private void ListarReembolso(int idPerfil)
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];

        ReembolsoBC objReembolsoBC = new ReembolsoBC();
        if (idPerfil != 2 && idPerfil != 1002 && idPerfil != 1008)
            gvReembolso.DataSource = objReembolsoBC.ListarReembolso(objUsuarioBE.IdUsuario, 3, 0, "", "", "", "", "0");
        else
        {
            ddlFiltro.SelectedIndex = 5;
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEsFacturable.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 6;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlEsFacturable.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = true;
            bBuscar.Enabled = true;
            gvReembolso.DataSource = objReembolsoBC.ListarReembolso(objUsuarioBE.IdUsuario, 3, 0, "", "", "", "", "17");
        }
        gvReembolso.DataBind();
    }

    private void ListarFiltros()
    {
        ddlFiltro.Items.Clear();
        ListItem oItem = new ListItem("Todos", "0");
        ddlFiltro.Items.Add(oItem);
        oItem = new ListItem("Por Aprobar Solicitud", "1"); //"Por Aprobar Nivel 1" "Por Aprobar Nivel 2" "Por Aprobar Nivel 3" 
        ddlFiltro.Items.Add(oItem);
        oItem = new ListItem("Por Rendir", "4");
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

        //ddlEstado.DataSource = objEstadoBC.ListarReembolso();
        //ddlEstado.DataTextField = "EstadoNombre";
        //ddlEstado.DataValueField = "EstadoCode";
        //ddlEstado.DataBind();
        ddlEstado.Items.Clear();
        ListItem oItem = new ListItem("Todos", "0");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Por Aprobar Solicitud", "1"); //"Por Aprobar Nivel 1" "Por Aprobar Nivel 2" "Por Aprobar Nivel 3" 
        ddlEstado.Items.Add(oItem);
        //oItem = new ListItem("Aprobado", "4");
        //ddlEstado.Items.Add(oItem);
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

        ListarReembolso(objPerfilUsuarioBE.IdPerfilUsuario );

        if (objPerfilUsuarioBE.CreaReembolso == "1") lnkNuevaReembolso.Visible = true;
        else lnkNuevaReembolso.Visible = false;
    }

    protected void gvReembolso_RowCommand(object sender, GridViewCommandEventArgs e)    
    {
        int IdReembolso;

        try
        {
            ReembolsoBC objReembolsoBC = new ReembolsoBC();
            ReembolsoBE objReembolsoBE = new ReembolsoBE();
            IdReembolso = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("Aprobacion"))
            {
                objReembolsoBE = objReembolsoBC.ObtenerReembolso(IdReembolso, 0);

                if (objReembolsoBE.Estado == "4" || objReembolsoBE.Estado == "11" || objReembolsoBE.Estado == "12" ||
                    objReembolsoBE.Estado == "13" || objReembolsoBE.Estado == "14" || objReembolsoBE.Estado == "15" ||
                    objReembolsoBE.Estado == "16" || objReembolsoBE.Estado == "17" || objReembolsoBE.Estado == "18" ||
                    objReembolsoBE.Estado == "19")
                {
                    Context.Items.Add("Modo", 1);
                    Context.Items.Add("IdReembolso", IdReembolso);
                    Server.Transfer("~/RendirReembolso.aspx");
                }
                else
                {
                    Context.Items.Add("Modo", 2);
                    Context.Items.Add("IdReembolso", IdReembolso);
                    Server.Transfer("~/Reembolso.aspx");
                }
            }
            if (e.CommandName.Equals("Rendir"))
            {
                objReembolsoBE = objReembolsoBC.ObtenerReembolso(IdReembolso, 0);

                if (objReembolsoBE.Estado == "4" || objReembolsoBE.Estado == "11" || objReembolsoBE.Estado == "12" ||
                    objReembolsoBE.Estado == "13" || objReembolsoBE.Estado == "14" || objReembolsoBE.Estado == "15" ||
                    objReembolsoBE.Estado == "16" || objReembolsoBE.Estado == "17" || objReembolsoBE.Estado == "18" ||
                    objReembolsoBE.Estado == "19")
                {
                    Context.Items.Add("Modo", 1);
                    Context.Items.Add("IdReembolso", IdReembolso);
                    Server.Transfer("~/RendirReembolso.aspx");
                }
                else
                    Mensaje("La Entrega a Rendir aun no ah sido Aprobada");
            }
            if (e.CommandName.Equals("Detalle"))
            {
                Context.Items.Add("Modo", 2);
                Context.Items.Add("IdReembolso", IdReembolso);
                Server.Transfer("~/Reembolso.aspx");
            }
            if (e.CommandName.Equals("Solicitud"))
            {
                Context.Items.Add("Modo", 2);
                Context.Items.Add("IdReembolso", IdReembolso);
                Server.Transfer("~/Reembolso.aspx");
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReembolso.PageIndex = e.NewPageIndex;
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
        String texto = "";
        switch (sId)
        {
            case "0": texto = "Falta Seleccionar"; break;
            case "1": texto = "SOL"; break;
            case "2": texto = "USD"; break;
            case "3": texto = "EUR"; break;
        }
        return texto;
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
            case "4": texto = "Por Rendir"; break;//"Aprobado"; break;
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

    private void LimpiarReembolso()
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];

        ReembolsoBC objReembolsoBC = new ReembolsoBC();
        gvReembolso.DataSource = null;
        gvReembolso.DataBind();
    }

    protected void lnkNuevaReembolso_Click(object sender, EventArgs e)
    {
        Context.Items.Add("Modo", 1);
        Context.Items.Add("IdReembolso", 0);
        Server.Transfer("~/Reembolso.aspx");
    }

    protected void Buscar_Click(object sender, EventArgs e)
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];

        ReembolsoBC objReembolsoBC = new ReembolsoBC();
        if (ddlFiltro.SelectedItem.Value == "1")
        {
            gvReembolso.DataSource = objReembolsoBC.ListarReembolso(objUsuarioBE.IdUsuario, 1, 0, txtCodigo.Text, "", "", "", "");
            gvReembolso.DataBind();
        }
        else if (ddlFiltro.SelectedItem.Value == "2")
        {
            gvReembolso.DataSource = objReembolsoBC.ListarReembolso(objUsuarioBE.IdUsuario, 1, 0, "", txtDni.Text, "", "", "");
            gvReembolso.DataBind();
        }
        else if (ddlFiltro.SelectedItem.Value == "3")
        {
            gvReembolso.DataSource = objReembolsoBC.ListarReembolso(objUsuarioBE.IdUsuario, 1, 0, "", "", ddlNombre_Solicitante.SelectedValue, "", "");
            gvReembolso.DataBind();
        }
        else if (ddlFiltro.SelectedItem.Value == "4")
        {
            gvReembolso.DataSource = objReembolsoBC.ListarReembolso(objUsuarioBE.IdUsuario, 1, 0, "", "", "", ddlEsFacturable.SelectedValue, "");
            gvReembolso.DataBind();
        }

        else if (ddlFiltro.SelectedItem.Value == "5")
        {
            gvReembolso.DataSource = objReembolsoBC.ListarReembolso(objUsuarioBE.IdUsuario, 3, 0, "", "", "", "", ddlEstado.SelectedValue);
            gvReembolso.DataBind();
        }
        else
        {
            ListarReembolsoTodo(); 
        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
    protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFiltro.SelectedValue == "0"){
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
            LimpiarReembolso();
            ListarReembolsoTodo();
            

        }
        if (ddlFiltro.SelectedValue == "1"){
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
            LimpiarReembolso();
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
            LimpiarReembolso();
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
            LimpiarReembolso();
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
            LimpiarReembolso();
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
            LimpiarReembolso();
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
            LimpiarReembolso();
        }
    }
}
