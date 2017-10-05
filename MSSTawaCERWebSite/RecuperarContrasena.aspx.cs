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
        txtDni.Enabled = true;
        txtCorreo.Enabled = true;
        lblDni.Visible = true;
        lblCorreo.Visible = true;
        txtDni.Visible = true;
        txtCorreo.Visible = true;
        btnRecuperarContrasena.Visible = true;
        btnCancelar.Visible = true;
        lblErrorDatos.Visible = true;
        lblNuevaContrasena1.Visible = false;
        lblNuevaContrasena2.Visible = false;
        txtNuevaContrasena.Visible = false;
        txtNuevaContrasena2.Visible = false;




    }

    protected void lnkRecuperarContrasena_Click(object sender, EventArgs e)
    {
        lblDni.Visible = true;
        lblCorreo.Visible = true;
        txtDni.Visible = true;
        txtCorreo.Visible = true;
        btnRecuperarContrasena.Visible = true;
        btnCancelar.Visible = true;
        lblErrorDatos.Visible = true;



    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }

    protected void btnRecuperarContrasena_Click(object sender, EventArgs e)
    {

        UsuarioBC objUsarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsarioBE = new List<UsuarioBE>();

        if (btnRecuperarContrasena.Text == "Validar Datos")
        {

            if (objUsarioBC.VerificarUsuario2(Convert.ToInt32(txtDni.Text), txtCorreo.Text.ToString()) == 0)
            {
                lblErrorDatos.Text = "Los datos no coinciden";
            }
            else
            {
                txtDni.Enabled = false;
                txtCorreo.Enabled = false;
                lblErrorDatos.Text = "";
                lblNuevaContrasena1.Visible = true;
                lblNuevaContrasena2.Visible = true;
                txtNuevaContrasena.Visible = true;
                txtNuevaContrasena2.Visible = true;
                btnRecuperarContrasena.Text = "Cambiar Contraseña";


            }
        }

        if (btnRecuperarContrasena.Text == "Cambiar Contraseña")
        {
            if (txtNuevaContrasena.Text.Length >= 4 && txtNuevaContrasena2.Text.Length >= 4)
            {
                if (txtNuevaContrasena.Text == txtNuevaContrasena2.Text)
                {
                    var str = txtNuevaContrasena.Text ;
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

                    if (objUsarioBC.VerificarContrasena(txtNuevaContrasena.Text, txtDni.Text, CuentaNumerico, CuentaMayusculas, CuentaEspeciales) == "Correcto")
                    {
                        if (objUsarioBC.ModificarContrasena(Convert.ToInt32(txtDni.Text), txtNuevaContrasena.Text))
                        {
                            UsuarioBC objUsuarioBC = new UsuarioBC();
                            objUsuarioBC.InsertarAcceso(0, DateTime.Today, txtDni.Text, txtNuevaContrasena.Text, "Cambio Contraseña");
                            Mensaje("La contraseña fue actualizada");
                            MensajeEmail("Su contraseña de usuario al portal SICER ha sido actualizada. ", "Cambio de contraseña SICER", txtCorreo.Text.Trim());
                            Response.Redirect("~/login.aspx");
                        }
                        else
                        {
                            Mensaje("Ocurrio un error actualizando la contraseña");
                            Response.Redirect("~/login.aspx");
                        }
                    }
                    else
                    {
                        txtDni.Enabled = false;
                        txtCorreo.Enabled = false;
                        lblErrorDatos.Text = "";
                        lblNuevaContrasena1.Visible = true;
                        lblNuevaContrasena2.Visible = true;
                        txtNuevaContrasena.Visible = true;
                        txtNuevaContrasena2.Visible = true;
                        btnRecuperarContrasena.Text = "Cambiar Contraseña";
                        lblErrorDatos.Text = objUsarioBC.VerificarContrasena(txtNuevaContrasena.Text, txtDni.Text, CuentaNumerico, CuentaMayusculas, CuentaEspeciales);
                    }
                }
                else
                {
                    txtDni.Enabled = false;
                    txtCorreo.Enabled = false;
                    lblErrorDatos.Text = "";
                    lblNuevaContrasena1.Visible = true;
                    lblNuevaContrasena2.Visible = true;
                    txtNuevaContrasena.Visible = true;
                    txtNuevaContrasena2.Visible = true;
                    btnRecuperarContrasena.Text = "Cambiar Contraseña";
                    lblErrorDatos.Text = "Las contraseñas no coinciden";
                }
            }
            else
            {
                txtDni.Enabled = false;
                txtCorreo.Enabled = false;
                lblErrorDatos.Text = "";
                lblNuevaContrasena1.Visible = true;
                lblNuevaContrasena2.Visible = true;
                txtNuevaContrasena.Visible = true;
                txtNuevaContrasena2.Visible = true;
                btnRecuperarContrasena.Text = "Cambiar Contraseña";
                //lblErrorDatos.Text = "La contraseña debe tener minimo 4 caracteres";
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/login.aspx");
    }

    protected void btnCambiarContrasena_Click(object sender, EventArgs e)
    {

    }

    private void MensajeEmail(string Cuerpo, string Asunto, string Destino)//string UsuarioSolicitante, string Documento, string Asunto, string CodigoEntregaRendir, string Destino)
    {
        if (Destino.Trim() != "")
        {
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            String email_body = "";
            correo.From = new System.Net.Mail.MailAddress("procesos.peru@tawa.com.pe");
            correo.To.Add(Destino.Trim());
            correo.Subject = Asunto;
            email_body = Cuerpo + ". Para ingresar al portal por favor ingrese aqui: http://sapb1.grupotawa.com:7585/sapweb/ ";
            correo.Body = email_body;
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "mailhost1.tawa.com.pe";
            smtp.EnableSsl = false;

            try
            {
                smtp.Send(correo);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Mensaje("Ocurrió un error (Recuperación Contraseña): " + ex.Message);
            }
        }
    }
}