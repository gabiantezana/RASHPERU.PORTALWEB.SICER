using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;
using System.Web.Security;
using System.Configuration;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetNoStore();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        try
        {
            if (!this.IsPostBack)
            {
                SetEmpresaInterna();
                MenuVisible(false);
                Revisar_Menus();
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            MSS.TAWA.HP.ExceptionHelper.LogException(ex);
        }
    }

    private void SetEmpresaInterna()
    {
        try
        {

        Int32 IdEmpresaEnterna = Convert.ToInt32(ConfigurationManager.AppSettings[ConstantHelper.Keys.IdEmpresaInterna].ToString());
        Session.Add(ConstantHelper.Keys.IdEmpresaInterna, (EmpresasSICER)IdEmpresaEnterna);
        }
        catch(Exception ex)
        {
            Mensaje("Ocurrió un error al obtener el id de la empresa actual: " + ex.Message);
        }
    }

    private void MenuVisible(bool visible)
    {
        try
        {
            lnkBienvenido.Visible = visible;
            lnkPerfil.Visible = visible;
            lnkAdministrador.Visible = visible;
            lnkCajaChica.Visible = visible;
            lnkEntregaRendir.Visible = visible;
            lnkReembolso.Visible = visible;
        }
        catch (Exception ex)
        {
            MSS.TAWA.HP.ExceptionHelper.LogException(ex);
            Mensaje("Ocurrió un error (SiteMaster): " + ex.Message);
        }
    }

    private void Revisar_Menus()
    {
        int link = 0;

        try
        {
            UsuarioBE objUsuarioBE = (UsuarioBE)Session["Usuario"];
            if (objUsuarioBE == null)
            {
                lnkBienvenido.Visible = true;
                lnkPerfil.Visible = false;
                lnkAdministrador.Visible = false;
                lnkCajaChica.Visible = false;
                lnkEntregaRendir.Visible = false;
                lnkReembolso.Visible = false;

                lbBienvenido.Visible = false;
                lnkLogout.Visible = false;
                lnkCambiar.Visible = false;
            }
            else
            {
                lbBienvenido.Visible = true;
                lbBienvenido.Text = "Bienvenido: " + objUsuarioBE.CardName;
                lnkLogout.Visible = true;
                lnkLogout.Text = "Cerrar Sesion";

                lnkBienvenido.Visible = false;
                lnkPerfil.Visible = true;

                PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
                PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBE();
                objPerfilUsuarioBE = objPerfilUsuarioBC.ObtenerPerfilUsuario(objUsuarioBE.IdPerfilUsuario);

                if (objPerfilUsuarioBE.ModAdministrador == "1")
                    lnkAdministrador.Visible = true;
                else
                    lnkAdministrador.Visible = false;

                if (objPerfilUsuarioBE.ModCajaChica == "1"
                || objPerfilUsuarioBE.CreaCajaChica == "1")
                    lnkCajaChica.Visible = true;
                else
                    lnkCajaChica.Visible = false;

                if (objPerfilUsuarioBE.ModEntregaRendir == "1"
                || objPerfilUsuarioBE.CreaEntregaRendir == "1")
                    lnkEntregaRendir.Visible = true;
                else
                    lnkEntregaRendir.Visible = false;

                if (objPerfilUsuarioBE.ModReembolso == "1"
                || objPerfilUsuarioBE.CreaReembolso == "1")
                    lnkReembolso.Visible = true;
                else
                    lnkReembolso.Visible = false;

            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error: " + ex.Message);
            MSS.TAWA.HP.ExceptionHelper.LogException(ex);
        }
    }

    protected void lnkBienvenido_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/Login.aspx");
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Abandon();

        // clear authentication cookie
        HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
        cookie1.Expires = DateTime.Now.AddYears(-1);
        Response.Cookies.Add(cookie1);

        // clear session cookie 
        HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
        cookie2.Expires = DateTime.Now.AddYears(-1);
        Response.Cookies.Add(cookie2);

        //FormsAuthentication.RedirectToLoginPage();

        Response.Redirect("~/Login.aspx");
    }

    protected void lnkUsuario_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Usuarios.aspx");
    }

    protected void lnkPerfilUsuario_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PerfilUsuarios.aspx");
    }

    protected void lnkNivelAprobacion_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/NivelAprobaciones.aspx");
    }

    protected void lnkNivelSeguridad_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/NivelSeguridad.aspx");
    }

    protected void lnkReporte_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reporte1.aspx");
    }

    protected void lnkCajaChica_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ListadoDocumentos.aspx" + "?TipoDocumentoWeb=1");
    }

    protected void lnkEntregaRendir_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ListadoDocumentos.aspx" + "?TipoDocumentoWeb=2");
    }

    protected void lnkReembolso_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ListadoDocumentos.aspx" + "?TipoDocumentoWeb=3");
    }

    protected void lnkCambiar_Click(object sender, EventArgs e)
    {
        if (lnkCambiar.Text == "Cambiar Contraseña")
        {
            UsuarioBE objUsuarioBE = (UsuarioBE)Session["Usuario"];
            txtPassword.Visible = true;
            txtPassword.Text = objUsuarioBE.Pass;
            lnkCambiar.Text = "Guardar Contraseña";
        }
        else
        {
            if (txtPassword.Text.Trim() != "")
            {
                UsuarioBE objUsuarioBE = (UsuarioBE)Session["Usuario"];
                UsuarioBC objUsuarioBC = new UsuarioBC();
                objUsuarioBE.Pass = txtPassword.Text;
                objUsuarioBC.ModificarUsuario(objUsuarioBE);
                txtPassword.Visible = false;
                lnkCambiar.Text = "Cambiar Contraseña";
            }
            else
            {
                Mensaje("No ah ingresado su contraseña");
            }
        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }

}
