using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MSS.TAWA.BC;
using MSS.TAWA.BE;

public partial class NivelAprobacion : System.Web.UI.Page
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
            String strIdNivel = "";

            if (!this.IsPostBack)
            {
                //strModo = Context.Items["Modo"].ToString();
                //strIdNivel = Context.Items["IdNivel"].ToString();

                //ViewState["Modo"] = strModo;
                //ViewState["IdNivel"] = strIdNivel;
                this.txtNivelSeguridad.Enabled = false;
                ListarNivelSeguridad();

                //Modalidad(Convert.ToInt32(strModo));
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    protected void Crear_Click(object sender, EventArgs e)
    {
        try
        {



            NivelSeguridadBE objNivelSeguridadBE = new NivelSeguridadBE();
            NivelSeguridadBC objNivelSeguridadBC = new NivelSeguridadBC();

            int numCaracteres = 0;

            objNivelSeguridadBE.IdNivel = Convert.ToInt32(txtIdNivel.Text);
            objNivelSeguridadBE.NivelSeguridad = txtNivelSeguridad.Text;
            objNivelSeguridadBE.NoRepetirContrasena = Convert.ToInt32(txtRepContrsena.Text);
            objNivelSeguridadBE.DiasVencimiento = Convert.ToInt32(txtDiasVencimiento.Text);

            objNivelSeguridadBE.Activo = ChkActivo.Checked;
            objNivelSeguridadBE.CaracterMayuscula = ChkMayuscula.Checked;
            objNivelSeguridadBE.CaracterNumerico = ChkNumerico.Checked;
            objNivelSeguridadBE.CaracterEspecial = ChkEspecial.Checked;


            objNivelSeguridadBE.NumNumericos = Convert.ToInt32(txtNumNumericos.Text);
            objNivelSeguridadBE.NumMayusculas = Convert.ToInt32(txtNumMayusculas.Text);
            objNivelSeguridadBE.NumEspeciales = Convert.ToInt32(txtNumEspeciales.Text);
            objNivelSeguridadBE.NumCarContrasena = Convert.ToInt32(txtNumCarContrasena.Text);

            if (ChkActivo.Checked)
            {
                if (ChkMayuscula.Checked) { numCaracteres = numCaracteres + objNivelSeguridadBE.NumMayusculas; }
                if (ChkNumerico.Checked) { numCaracteres = numCaracteres + objNivelSeguridadBE.NumNumericos; }
                if (ChkEspecial.Checked) { numCaracteres = numCaracteres + objNivelSeguridadBE.NumEspeciales; }

                if (objNivelSeguridadBE.NumCarContrasena < numCaracteres)
                    throw new Exception("El total de caracteres es menos al detallado");
            }

            objNivelSeguridadBC.ModificarNivelAprobacion(objNivelSeguridadBE);

            ListarNivelSeguridad();
        }
        catch (Exception ex)
        {
            Mensaje(ex.Message);
        }
    }

    private void ListarNivelSeguridad()
    {
        try
        {
            NivelSeguridadBE objNivelSeguridadBE = new NivelSeguridadBE();
            NivelSeguridadBC objNivelSeguridadBC = new NivelSeguridadBC();

            objNivelSeguridadBE = objNivelSeguridadBC.ObtenerNivelSeguridad();

            txtIdNivel.Text = objNivelSeguridadBE.IdNivel.ToString();
            txtNivelSeguridad.Text = objNivelSeguridadBE.NivelSeguridad.ToString();
            txtRepContrsena.Text = objNivelSeguridadBE.NoRepetirContrasena.ToString();
            txtDiasVencimiento.Text = objNivelSeguridadBE.DiasVencimiento.ToString();

            ChkActivo.Checked = objNivelSeguridadBE.Activo;
            ChkMayuscula.Checked = objNivelSeguridadBE.CaracterMayuscula;
            ChkNumerico.Checked = objNivelSeguridadBE.CaracterNumerico;
            ChkEspecial.Checked = objNivelSeguridadBE.CaracterEspecial;

            txtNumNumericos.Text = objNivelSeguridadBE.NumNumericos.ToString();
            txtNumMayusculas.Text = objNivelSeguridadBE.NumMayusculas.ToString();
            txtNumEspeciales.Text = objNivelSeguridadBE.NumEspeciales.ToString();
            txtNumCarContrasena.Text = objNivelSeguridadBE.NumCarContrasena.ToString();

        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    private void Modalidad(int p)
    {
        try
        {
            switch (p)
            {
                case 1:
                    lblCabezera.Text = "Crear Nuevo Nivel Aprobacion";
                    LimpiarCampos();
                    break;
                case 2:
                    lblCabezera.Text = "Modificar Nivel Aprobacion";
                    bCrear.Text = "Guardar";

                    break;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    private void LimpiarCampos()
    {

    }

    protected void Cancelar_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/default.aspx");
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        //if (ChkActivo.Checked = false)
        //{
        //    ChkMayuscula.Enabled = false;
        //    ChkNumerico.Enabled = false;
        //    ChkEspecial.Enabled = false;
        //    txtDiasVencimiento.Enabled = false;
        //    txtRepContrsena.Enabled = false;
        //}
        //else
        //{
        //    ChkMayuscula.Enabled = true;
        //    ChkNumerico.Enabled = true;
        //    ChkEspecial.Enabled = true;
        //    txtDiasVencimiento.Enabled = true;
        //    txtRepContrsena.Enabled = true;
        //}
    }
    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {

    }
}