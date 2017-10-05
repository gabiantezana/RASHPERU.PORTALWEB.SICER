using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;

public partial class PerfilUsuario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
           Response.Redirect("~/Login.aspx");
        }

        try
        {
            String strModo = "";
            String strIdPerfilUsuario = "";

            if (!this.IsPostBack)
            {
                strModo = Context.Items["Modo"].ToString();
                strIdPerfilUsuario = Context.Items["IdPerfilUsuario"].ToString();

                ViewState["Modo"] = strModo;
                ViewState["IdPerfilUsuario"] = strIdPerfilUsuario;

                ListarTipo();
                ListarModulo();
                Modalidad(Convert.ToInt32(strModo));
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (PerfilUsuario): " + ex.Message);
        }
    }

    private void ListarTipo()
    {
        try
        {
            ddlTipoAprobador.Items.Clear();
            ListItem oItem = new ListItem("Seleccionar", "0");
            ddlTipoAprobador.Items.Add(oItem);
            oItem = new ListItem("Aprobador", "1");
            ddlTipoAprobador.Items.Add(oItem);
            oItem = new ListItem("Contabilidad", "2");
            ddlTipoAprobador.Items.Add(oItem);
            oItem = new ListItem("Creador", "3");
            ddlTipoAprobador.Items.Add(oItem);
            oItem = new ListItem("Aprobador y Creador", "4");
            ddlTipoAprobador.Items.Add(oItem);
            oItem = new ListItem("Contabilidad y Creador", "5");
            ddlTipoAprobador.Items.Add(oItem);
            oItem = new ListItem("Administrador Web", "6");
            ddlTipoAprobador.Items.Add(oItem);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (PerfilUsuario): " + ex.Message);
        }
    }

    private void ListarModulo()
    {
        try
        {
            ddlModAdministrador.Items.Clear();
            ddlModCajaChica.Items.Clear();
            ddlModEntregaRendir.Items.Clear();
            ddlModReembolso.Items.Clear();

            ListItem oItem1 = new ListItem("Seleccionar", "0");
            ddlModAdministrador.Items.Add(oItem1);
            oItem1 = new ListItem("Si", "1");
            ddlModAdministrador.Items.Add(oItem1);
            oItem1 = new ListItem("No", "2");
            ddlModAdministrador.Items.Add(oItem1);

            ListItem oItem2 = new ListItem("Seleccionar", "0");
            ddlModCajaChica.Items.Add(oItem2);
            oItem2 = new ListItem("Si", "1");
            ddlModCajaChica.Items.Add(oItem2);
            oItem2 = new ListItem("No", "2");
            ddlModCajaChica.Items.Add(oItem2);

            ListItem oItem3 = new ListItem("Seleccionar", "0");
            ddlModEntregaRendir.Items.Add(oItem3);
            oItem3 = new ListItem("Si", "1");
            ddlModEntregaRendir.Items.Add(oItem3);
            oItem3 = new ListItem("No", "2");
            ddlModEntregaRendir.Items.Add(oItem3);

            ListItem oItem4 = new ListItem("Seleccionar", "0");
            ddlModReembolso.Items.Add(oItem4);
            oItem4 = new ListItem("Si", "1");
            ddlModReembolso.Items.Add(oItem4);
            oItem4 = new ListItem("No", "2");
            ddlModReembolso.Items.Add(oItem4);

            ListItem oItem5 = new ListItem("Seleccionar", "0");
            ddlCreaCajaChica.Items.Add(oItem5);
            oItem5 = new ListItem("Si", "1");
            ddlCreaCajaChica.Items.Add(oItem5);
            oItem5 = new ListItem("No", "2");
            ddlCreaCajaChica.Items.Add(oItem5);

            ListItem oItem6 = new ListItem("Seleccionar", "0");
            ddlCreaEntregaRendir.Items.Add(oItem6);
            oItem6 = new ListItem("Si", "1");
            ddlCreaEntregaRendir.Items.Add(oItem6);
            oItem6 = new ListItem("No", "2");
            ddlCreaEntregaRendir.Items.Add(oItem6);

            ListItem oItem7 = new ListItem("Seleccionar", "0");
            ddlCreaReembolso.Items.Add(oItem7);
            oItem7 = new ListItem("Si", "1");
            ddlCreaReembolso.Items.Add(oItem7);
            oItem7 = new ListItem("No", "2");
            ddlCreaReembolso.Items.Add(oItem7);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (PerfilUsuario): " + ex.Message);
        }
    }
        
    private void Modalidad(int p)
    {
        try
        {
            switch (p)
            {
                case 1:
                    lblCabezera.Text = "Crear Nuevo Perfil Usuario";
                    LimpiarCampos();
                    break;
                case 2:
                    lblCabezera.Text = "Modificar Perfil Usuario";
                    bCrear.Text = "Guardar";
                    LlenarCampos(Convert.ToInt32(ViewState["IdPerfilUsuario"].ToString()));
                    break;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (PerfilUsuario): " + ex.Message);
        }
    }

    private void LimpiarCampos()
    {
        txtIdPerfilUsuario.Text = "";
        txtDescripcion.Text = "";
    }

    private void LlenarCampos(int p)
    {
        try
        {
            PerfilUsuarioBC ObjPerfilUsuarioBC = new PerfilUsuarioBC();
            PerfilUsuarioBE ObjPerfilUsuarioBE = new PerfilUsuarioBE();
            ObjPerfilUsuarioBE = ObjPerfilUsuarioBC.ObtenerPerfilUsuario(p);
            txtIdPerfilUsuario.Text = Convert.ToString(ObjPerfilUsuarioBE.IdPerfilUsuario);
            txtDescripcion.Text = ObjPerfilUsuarioBE.Descripcion;
            ddlModAdministrador.SelectedValue = ObjPerfilUsuarioBE.ModAdministrador;
            ddlTipoAprobador.SelectedValue = ObjPerfilUsuarioBE.TipoAprobador;
            ddlModCajaChica.SelectedValue = ObjPerfilUsuarioBE.ModCajaChica;
            ddlModEntregaRendir.SelectedValue = ObjPerfilUsuarioBE.ModEntregaRendir;
            ddlModReembolso.SelectedValue = ObjPerfilUsuarioBE.ModReembolso;
            ddlCreaCajaChica.SelectedValue = ObjPerfilUsuarioBE.CreaCajaChica;
            ddlCreaEntregaRendir.SelectedValue = ObjPerfilUsuarioBE.CreaEntregaRendir;
            ddlCreaReembolso.SelectedValue = ObjPerfilUsuarioBE.CreaReembolso;

            switch (ddlTipoAprobador.SelectedItem.Value)
            {
                case "1": ddlModCajaChica.Enabled = true;//Aprobador
                    ddlModEntregaRendir.Enabled = true;
                    ddlModReembolso.Enabled = true;
                    ddlCreaCajaChica.Enabled = false;
                    ddlCreaCajaChica.SelectedValue = "2";
                    ddlCreaEntregaRendir.Enabled = false;
                    ddlCreaEntregaRendir.SelectedValue = "2";
                    ddlCreaReembolso.Enabled = false;
                    ddlCreaReembolso.SelectedValue = "2";
                    break;
                case "2": ddlModCajaChica.Enabled = false;//Contabilidad
                    ddlModCajaChica.SelectedValue = "1";
                    ddlModEntregaRendir.Enabled = false;
                    ddlModEntregaRendir.SelectedValue = "1";
                    ddlModReembolso.Enabled = false;
                    ddlModReembolso.SelectedValue = "1";
                    ddlCreaCajaChica.Enabled = false;
                    ddlCreaCajaChica.SelectedValue = "2";
                    ddlCreaEntregaRendir.Enabled = false;
                    ddlCreaEntregaRendir.SelectedValue = "2";
                    ddlCreaReembolso.Enabled = false;
                    ddlCreaReembolso.SelectedValue = "2";
                    break;
                case "3": ddlModCajaChica.Enabled = false;// Creador
                    ddlModCajaChica.SelectedValue = "2";
                    ddlModEntregaRendir.Enabled = false;
                    ddlModEntregaRendir.SelectedValue = "2";
                    ddlModReembolso.Enabled = false;
                    ddlModReembolso.SelectedValue = "2";
                    ddlCreaCajaChica.Enabled = true;
                    ddlCreaEntregaRendir.Enabled = true;
                    ddlCreaReembolso.Enabled = true;
                    break;
                case "4": ddlModCajaChica.Enabled = true;//Aprobador y Creador
                    ddlModEntregaRendir.Enabled = true;
                    ddlModReembolso.Enabled = true;
                    ddlCreaCajaChica.Enabled = true;
                    ddlCreaEntregaRendir.Enabled = true;
                    ddlCreaReembolso.Enabled = true;
                    break;
                case "5": ddlModCajaChica.Enabled = false;//Contador y Creado
                    ddlModCajaChica.SelectedValue = "1";
                    ddlModEntregaRendir.Enabled = false;
                    ddlModEntregaRendir.SelectedValue = "1";
                    ddlModReembolso.Enabled = false;
                    ddlModReembolso.SelectedValue = "1";
                    ddlCreaCajaChica.Enabled = true;
                    ddlCreaEntregaRendir.Enabled = true;
                    ddlCreaReembolso.Enabled = true;
                    break;
                case "6": ddlModCajaChica.Enabled = false;//Administrador Web
                    ddlModCajaChica.SelectedValue = "2";
                    ddlModEntregaRendir.Enabled = false;
                    ddlModEntregaRendir.SelectedValue = "2";
                    ddlModReembolso.Enabled = false;
                    ddlModReembolso.SelectedValue = "2";
                    ddlCreaCajaChica.Enabled = false;
                    ddlCreaCajaChica.SelectedValue = "2";
                    ddlCreaEntregaRendir.Enabled = false;
                    ddlCreaEntregaRendir.SelectedValue = "2";
                    ddlCreaReembolso.Enabled = false;
                    ddlCreaReembolso.SelectedValue = "2";
                    break;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (PerfilUsuario): " + ex.Message);
        }
    }

    protected void Crear_Click(object sender, EventArgs e)
    {
        int cod;
        try
        {
            if (txtDescripcion.Text.Trim() != "" &&
                ddlModAdministrador.SelectedItem.Value != "0" &&
                ddlTipoAprobador.SelectedItem.Value != "0" &&
                ddlModCajaChica.SelectedItem.Value != "0" &&
                ddlModEntregaRendir.SelectedItem.Value != "0" &&
                ddlModReembolso.SelectedItem.Value != "0" &&
                ddlCreaCajaChica.SelectedItem.Value != "0" &&
                ddlCreaEntregaRendir.SelectedItem.Value != "0" &&
                ddlCreaReembolso.SelectedItem.Value != "0")
            {
                PerfilUsuarioBE ObjPerfilUsuarioBE = new PerfilUsuarioBE();
                PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();

                //ObjPerfilUsuarioBE.IdPerfilUsuario = Convert.ToInt32(txtIdPerfilUsuario.Text);
                ObjPerfilUsuarioBE.Descripcion = txtDescripcion.Text;
                ObjPerfilUsuarioBE.ModAdministrador = ddlModAdministrador.SelectedItem.Value;
                ObjPerfilUsuarioBE.TipoAprobador = ddlTipoAprobador.SelectedItem.Value;
                ObjPerfilUsuarioBE.ModCajaChica = ddlModCajaChica.SelectedItem.Value;
                ObjPerfilUsuarioBE.ModEntregaRendir = ddlModEntregaRendir.SelectedItem.Value;
                ObjPerfilUsuarioBE.ModReembolso = ddlModReembolso.SelectedItem.Value;
                ObjPerfilUsuarioBE.CreaCajaChica = ddlCreaCajaChica.SelectedItem.Value;
                ObjPerfilUsuarioBE.CreaEntregaRendir = ddlCreaEntregaRendir.SelectedItem.Value;
                ObjPerfilUsuarioBE.CreaReembolso = ddlCreaReembolso.SelectedItem.Value;

                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    UsuarioBE objUsuarioBE = new UsuarioBE();
                    objUsuarioBE = (UsuarioBE)Session["Usuario"];
                    ObjPerfilUsuarioBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    ObjPerfilUsuarioBE.CreateDate = DateTime.Now;
                    ObjPerfilUsuarioBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    ObjPerfilUsuarioBE.UpdateDate = DateTime.Now;
                }

                int Modo = Convert.ToInt32(ViewState["Modo"].ToString());
                int idPerfilUsuario = Convert.ToInt32(ViewState["IdPerfilUsuario"].ToString());
                if (Modo == 1)
                {
                    cod = objPerfilUsuarioBC.InsertarPerfilUsuario(ObjPerfilUsuarioBE);
                }
                else
                {
                    ObjPerfilUsuarioBE.IdPerfilUsuario = idPerfilUsuario;
                    objPerfilUsuarioBC.ModificarPerfilUsuario(ObjPerfilUsuarioBE);
                }

                Response.Redirect("PerfilUsuarios.aspx");
            }
            else
            {
                Mensaje("Alerta: Es necesario llenar toda la informacion");
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (PerfilUsuario): " + ex.Message);
        }
    }

    protected void Cancelar_Click(object sender, EventArgs e)
    {
       Response.Redirect("~/PerfilUsuarios.aspx");
    }

    protected void ddlTipoAprobador_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (ddlTipoAprobador.SelectedItem.Value)
        {
            case "1": ddlModCajaChica.Enabled = true;//Aprobador
                      ddlModEntregaRendir.Enabled = true;
                      ddlModReembolso.Enabled = true;
                      ddlCreaCajaChica.Enabled = false;
                      ddlCreaCajaChica.SelectedValue = "2";
                      ddlCreaEntregaRendir.Enabled = false;
                      ddlCreaEntregaRendir.SelectedValue = "2";
                      ddlCreaReembolso.Enabled = false;
                      ddlCreaReembolso.SelectedValue = "2";
                      break;
            case "2": ddlModCajaChica.Enabled = false;//Contabilidad
                      ddlModCajaChica.SelectedValue = "1";
                      ddlModEntregaRendir.Enabled = false;
                      ddlModEntregaRendir.SelectedValue = "1";
                      ddlModReembolso.Enabled = false;
                      ddlModReembolso.SelectedValue = "1";
                      ddlCreaCajaChica.Enabled = false;
                      ddlCreaCajaChica.SelectedValue = "2";
                      ddlCreaEntregaRendir.Enabled = false;
                      ddlCreaEntregaRendir.SelectedValue = "2";
                      ddlCreaReembolso.Enabled = false;
                      ddlCreaReembolso.SelectedValue = "2";
                      break;
            case "3": ddlModCajaChica.Enabled = false;// Creador
                      ddlModCajaChica.SelectedValue = "2";
                      ddlModEntregaRendir.Enabled = false;
                      ddlModEntregaRendir.SelectedValue = "2";
                      ddlModReembolso.Enabled = false;
                      ddlModReembolso.SelectedValue = "2";
                      ddlCreaCajaChica.Enabled = true;
                      ddlCreaEntregaRendir.Enabled = true;
                      ddlCreaReembolso.Enabled = true;
                      break;
            case "4": ddlModCajaChica.Enabled = true;//Aprobador y Creador
                      ddlModEntregaRendir.Enabled = true;
                      ddlModReembolso.Enabled = true;
                      ddlCreaCajaChica.Enabled = true;
                      ddlCreaEntregaRendir.Enabled = true;
                      ddlCreaReembolso.Enabled = true;
                      break;
            case "5": ddlModCajaChica.Enabled = false;//Contador y Creado
                      ddlModCajaChica.SelectedValue = "1";
                      ddlModEntregaRendir.Enabled = false;
                      ddlModEntregaRendir.SelectedValue = "1";
                      ddlModReembolso.Enabled = false;
                      ddlModReembolso.SelectedValue = "1";
                      ddlCreaCajaChica.Enabled = true;
                      ddlCreaEntregaRendir.Enabled = true;
                      ddlCreaReembolso.Enabled = true;
                      break;
            case "6": ddlModCajaChica.Enabled = false;//Administrador Web
                      ddlModCajaChica.SelectedValue = "1";
                      ddlModEntregaRendir.Enabled = false;
                      ddlModEntregaRendir.SelectedValue = "1";
                      ddlModReembolso.Enabled = false;
                      ddlModReembolso.SelectedValue = "1";
                      ddlCreaCajaChica.Enabled = false;
                      ddlCreaCajaChica.SelectedValue = "1";
                      ddlCreaEntregaRendir.Enabled = false;
                      ddlCreaEntregaRendir.SelectedValue = "1";
                      ddlCreaReembolso.Enabled = false;
                      ddlCreaReembolso.SelectedValue = "1";
                      break;
        }
    } 

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }

}