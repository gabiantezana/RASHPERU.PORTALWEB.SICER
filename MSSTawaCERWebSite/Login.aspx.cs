using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] != null)
        {
            Response.Redirect("~/Default.aspx");
        }


    }

    protected void Login_Click(object sender, EventArgs e)
    {
        String userName = "";
        String password = "";

        try
        {
            UsuarioBC objUsuarioBC = new UsuarioBC();
            UsuarioBE objUsuarioBE;

            userName = txtusuario.Text;
            password = txtpassword.Text;

            objUsuarioBE = objUsuarioBC.LoginUsuario(userName, password);

            if (objUsuarioBE != null)
            {
                if (objUsuarioBE.Estado == "1")
                {
                    var str = password;
                    var CuentaNumerico = 0;
                    var CuentaMayusculas = 0;
                    var CuentaEspeciales = 0;

                    for (int itChar = 0; itChar < str.Length; itChar++)
                    {
                        if (char.IsNumber(str[itChar]))
                        {
                            CuentaNumerico++;
                        }
                        if (char.IsUpper(str[itChar]))
                        {
                            CuentaMayusculas++;
                        }
                        if (!char.IsUpper(str[itChar]) && !char.IsNumber(str[itChar]) && !char.IsLower(str[itChar]) && !char.IsLetter(str[itChar]))
                        {
                            CuentaEspeciales++;
                        }
                    }

                    if (objUsuarioBC.VerificarContrasena(password, userName, CuentaNumerico, CuentaMayusculas, CuentaEspeciales) == "La Contraseña ha caducado, por favor cambiar la contraseña")
                    {
                        Mensaje("(Login): La contraseña ha caducado, por favor cambie la contraseña");
                        
                    }
                    else
                    {
                        Session["Usuario"] = objUsuarioBE;

                        FormsAuthentication.RedirectFromLoginPage(userName, false);
                        objUsuarioBC.InsertarAcceso(0, DateTime.Today, txtusuario.Text, txtpassword.Text, "Ingreso");
                        Server.Transfer("~/Default.aspx");
                    }
                }
                else
                {
                    double totalminutes = 15 - Math.Round((DateTime.Now - objUsuarioBE.HoraMinutoLogin).TotalMinutes, 0);

                    switch (objUsuarioBE.Estado)
                    {
                        case "2": Mensaje("(Login): El usuario esta inhabilitado por favor comunicarse con el area de sistema"); break;
                        case "3": Mensaje("(Login): El usuario esta bloqueado, por favor espere " + Convert.ToString(totalminutes) + " minutos y vuelva a intentarlo, en caso contrario, comunicarse con el area de sistema"); break;
                    }
                }
            }
            else
                Mensaje("(Login): Usuario o password son incorrectos, si ocurre esto 3 veces sera bloqueado por 15 minutos.");
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Login): " + ex.Message);
        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }

    protected void lnkRecuperarContrasena_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/RecuperarContrasena.aspx");
        //lblDni.Visible = true;
        //lblCorreo.Visible = true;
        //txtDni.Visible = true;
        //txtCorreo.Visible = true;
        //btnRecuperarContrasena.Visible = true;
        //btnCancelar.Visible = true;
        //lblErrorDatos.Visible = true;



    }
    protected void btnRecuperarContrasena_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {

    }
    protected void btnCambiarContrasena_Click(object sender, EventArgs e)
    {

    }
}