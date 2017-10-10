using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;
using MssTawaCer.App_Code.Helper;

public partial class CajaChicas : System.Web.UI.Page
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
            
            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = false;

            ValidarMenu();

        }
    }
    private void ListarCajaChicaTodo()
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];
        CajaChicaBC objCajaChicaBC = new CajaChicaBC();
        gvCajaChicas.DataSource = objCajaChicaBC.ListarCajaChica(objUsuarioBE.IdUsuario, 1, 0, "", "", "", "", "");
        gvCajaChicas.DataBind();
    }

    private void ListarCajaChica(int idPerfil)
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];

        CajaChicaBC objCajaChicaBC = new CajaChicaBC();
        if (idPerfil != 2 && idPerfil != 1002 && idPerfil != 1008)
            gvCajaChicas.DataSource = objCajaChicaBC.ListarCajaChica(objUsuarioBE.IdUsuario, 1, 0, "", "", "", "", "");
        else
        {
            ddlFiltro.SelectedIndex = 5;
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 8;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = true;
            bBuscar.Enabled = true;
            gvCajaChicas.DataSource = objCajaChicaBC.ListarCajaChica(objUsuarioBE.IdUsuario, 3, 0, "", "", "", "", "17");
        }

        gvCajaChicas.DataBind();
    }
    private void LimpiarCajaChica()
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];

        CajaChicaBC objCajaChicaBC = new CajaChicaBC();
        gvCajaChicas.DataSource = null;
        gvCajaChicas.DataBind();
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
            ExceptionHelper.LogException(ex);
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
        ddlEstado.Items.Clear();
        ListItem oItem = new ListItem("Todos", "0");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Por Aprobar Solicitud", "1"); //"Por Aprobar Nivel 1" "Por Aprobar Nivel 2" "Por Aprobar Nivel 3" 
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Aprobado", "4");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Rechazado", "5");
        ddlEstado.Items.Add(oItem);
        oItem = new ListItem("Liquidada", "16");
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

        //EstadoBC objEstadoBC = new EstadoBC();
        //List<EstadoBE> lstEstadosBE = new List<EstadoBE>();

        //ddlEstado.DataSource = objEstadoBC.ListarCajaChica();
        //ddlEstado.DataTextField = "EstadoNombre";
        //ddlEstado.DataValueField = "EstadoCode";
        //ddlEstado.DataBind();
    }

    //private void ListarEstado()
    //{
    //    ddlEstado.Items.Clear();
    //    ListItem oItem = new ListItem("Todos", "0");
    //    ddlEstado.Items.Add(oItem);
    //    oItem = new ListItem("Por Aprobar Solicitud", "1"); //"Por Aprobar Nivel 1" "Por Aprobar Nivel 2" "Por Aprobar Nivel 3" 
    //    ddlEstado.Items.Add(oItem);
    //    oItem = new ListItem("Aprobado", "4");
    //    ddlEstado.Items.Add(oItem);
    //    oItem = new ListItem("Rechazado", "5");
    //    ddlEstado.Items.Add(oItem);
    //    oItem = new ListItem("Observaciones a Solicitud", "8"); //"Observaciones Nivel 1" "Observaciones Nivel 2" "Observaciones Nivel 3" 
    //    ddlEstado.Items.Add(oItem);
    //    oItem = new ListItem("Por Aprobar Rendicion", "11"); //"Rendir: Por Aprobar Nivel 1" "Rendir: Por Aprobar Nivel 2" "Rendir: Por Aprobar Nivel 3"        
    //    ddlEstado.Items.Add(oItem);
    //    oItem = new ListItem("Observaciones Rendicion", "12"); //"Rendir: Observaciones Nivel 1" "Rendir: Observaciones Nivel 2" "Rendir: Observaciones Nivel 3"        
    //    ddlEstado.Items.Add(oItem);
    //    oItem = new ListItem("Por Aprobar Contabilidad", "17");
    //    ddlEstado.Items.Add(oItem);
    //    oItem = new ListItem("Observaciones Contabilidad", "18");
    //    ddlEstado.Items.Add(oItem);
    //    oItem = new ListItem("Rendicion Aprobadas", "19");
    //    ddlEstado.Items.Add(oItem);
    //}

    private void ValidarMenu()
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];
        objUsuarioBE = objUsuarioBC.ObtenerUsuario(objUsuarioBE.IdUsuario, 0);

        PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
        PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBE();
        objPerfilUsuarioBE = objPerfilUsuarioBC.ObtenerPerfilUsuario(objUsuarioBE.IdPerfilUsuario);

        ListarCajaChica(objPerfilUsuarioBE.IdPerfilUsuario);

        if (objPerfilUsuarioBE.CreaCajaChica == "1") lnkNuevaCajaChica.Visible = true;
        else lnkNuevaCajaChica.Visible = false;
    }

    protected void gvCajaChicas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int IdCajaChica;

        try
        {
            CajaChicaBC objCajaChicaBC = new CajaChicaBC();
            CajaChicaBE objCajaChicaBE = new CajaChicaBE();
            IdCajaChica = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("Aprobacion"))
            {
                objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(IdCajaChica, 0);

                if (objCajaChicaBE.Estado == "4" || objCajaChicaBE.Estado == "11" || objCajaChicaBE.Estado == "12" ||
                    objCajaChicaBE.Estado == "13" || objCajaChicaBE.Estado == "14")
                {
                    Context.Items.Add("Modo", 1);
                    Context.Items.Add("IdCajaChica", IdCajaChica);
                    Server.Transfer("~/RendirCajaChica.aspx");
                }
                else
                {
                    Context.Items.Add("Modo", 2);
                    Context.Items.Add("IdCajaChica", IdCajaChica);
                    Server.Transfer("~/CajaChica.aspx");
                }
            }
            if (e.CommandName.Equals("Rendir"))
            {
                objCajaChicaBE = objCajaChicaBC.ObtenerCajaChica(IdCajaChica, 0);

                if (objCajaChicaBE.Estado == "4" || objCajaChicaBE.Estado == "11" || objCajaChicaBE.Estado == "12" ||
                    objCajaChicaBE.Estado == "13" || objCajaChicaBE.Estado == "14")
                {
                    Context.Items.Add("Modo", 1);
                    Context.Items.Add("IdCajaChica", IdCajaChica);
                    Server.Transfer("~/RendirCajaChica.aspx");
                }
                else
                    Mensaje("La Caja Chica aun no ah sido Aprobada");
            }
            if (e.CommandName.Equals("Historial"))
            {
                Context.Items.Add("Modo", 2);
                Context.Items.Add("IdCajaChica", IdCajaChica);
                Server.Transfer("~/CajaChicasH.aspx");
            }
            if (e.CommandName.Equals("Solicitud"))
            {
                Context.Items.Add("Modo", 2);
                Context.Items.Add("IdCajaChica", IdCajaChica);
                Server.Transfer("~/CajaChica.aspx");
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
        gvCajaChicas.PageIndex = e.NewPageIndex;
        Buscar_Click(null, null);
      
    }

    public String SetearIdUsuarioSolicitante(String sId)
    {
        UsuarioBC objUsuarioBC = new UsuarioBC();
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = objUsuarioBC.ObtenerUsuario(Convert.ToInt32(sId), 0);
        return objUsuarioBE.CardName;
    }

    protected void ddlIdEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    public String SetearFiltro(String sId)
    {
        String texto = "";
        switch (sId)
        {
            case "0": texto = "Filtrar por"; break;
            case "1": texto = "Código Documento"; break;
            case "2": texto = "DNI"; break;
            case "3": texto = "Nombre Solicitante"; break;
            case "4": texto = "Es Facturable"; break;
            case "5": texto = "Estado"; break;


        }
        return texto;
    }

    public String SetearMomentoFacturable(String sId)
    {
        String texto = "";
        switch (sId)
        {
            case "0": texto = "Falta Seleccionar"; break;
            case "1": texto = "En la apertura y la rendicion."; break;
            case "2": texto = "En la rendicion."; break;
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
            case "13": texto = "Rendir: Por Aprobar Contabilidad"; break;
            case "14": texto = "Rendir: Observaciones Contabilidad"; break;
            case "15": texto = "Rendir: Aprobado"; break;
            case "16": texto = "Caja Chica Liquidada"; break;
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

    protected void lnkNuevaCajaChica_Click(object sender, EventArgs e)
    {
        Context.Items.Add("Modo", 1);
        Context.Items.Add("IdCajaChica", 0);
        Server.Transfer("~/CajaChica.aspx");
    }

    protected void Buscar_Click(object sender, EventArgs e)
    {
        UsuarioBE objUsuarioBE = new UsuarioBE();
        objUsuarioBE = (UsuarioBE)Session["Usuario"];

        CajaChicaBC objCajaChicaBC = new CajaChicaBC();
        if (ddlFiltro.SelectedItem.Value == "1")
        {
            gvCajaChicas.DataSource = objCajaChicaBC.ListarCajaChica(objUsuarioBE.IdUsuario, 1, 0, txtCodigo.Text, "", "", "", "");
            gvCajaChicas.DataBind();
        }
        else if (ddlFiltro.SelectedItem.Value == "2")
        {
            gvCajaChicas.DataSource = objCajaChicaBC.ListarCajaChica(objUsuarioBE.IdUsuario, 1, 0, "", txtDni.Text, "", "", "");
            gvCajaChicas.DataBind();
        }
        else if (ddlFiltro.SelectedItem.Value == "3")
        {
            gvCajaChicas.DataSource = objCajaChicaBC.ListarCajaChica(objUsuarioBE.IdUsuario, 1, 0, "", "", ddlNombre_Solicitante.SelectedValue, "", "");
            gvCajaChicas.DataBind();
        }
   
        else if (ddlFiltro.SelectedItem.Value == "5")
        {
            gvCajaChicas.DataSource = objCajaChicaBC.ListarCajaChica(objUsuarioBE.IdUsuario, 3, 0, "", "", "", "", ddlEstado.SelectedValue);
            gvCajaChicas.DataBind();
        }
        else
        {
            ListarCajaChicaTodo(); 
        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
    protected void gvCajaChicas_SelectedIndexChanged(object sender, EventArgs e)
    {

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
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = false;
            LimpiarCajaChica();
            ListarCajaChicaTodo(); 


        }
        if (ddlFiltro.SelectedValue == "1")
        {

            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = true;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarCajaChica();
        }
        if (ddlFiltro.SelectedValue == "2")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = true;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarCajaChica();
        }
        if (ddlFiltro.SelectedValue == "3")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = true;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarCajaChica();
        }
        if (ddlFiltro.SelectedValue == "4")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = true;
            LimpiarCajaChica();
        }
        if (ddlFiltro.SelectedValue == "5")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = true;
            bBuscar.Enabled = true;
            LimpiarCajaChica();
        }
        if (ddlFiltro.SelectedValue == "6")
        {
            txtDni.Text = "";
            txtCodigo.Text = "";
            ddlNombre_Solicitante.SelectedIndex = 0;

            ddlEstado.SelectedIndex = 0;
            txtCodigo.Enabled = false;
            txtDni.Enabled = false;
            ddlNombre_Solicitante.Enabled = false;
            ddlEstado.Enabled = false;
            bBuscar.Enabled = false;
            ListarCajaChicaTodo(); 
        }
    }
}
