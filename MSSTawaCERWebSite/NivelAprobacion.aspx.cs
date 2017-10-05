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
                strModo = Context.Items["Modo"].ToString();
                strIdNivel = Context.Items["IdNivel"].ToString();

                ViewState["Modo"] = strModo;
                ViewState["IdNivel"] = strIdNivel;

                ListarNivel();
                ListarDocumento();
                ListarEsDeMonto();
                Modalidad(Convert.ToInt32(strModo));
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    private void ListarNivel()
    {
        try
        {
            ddlNivel.Items.Clear();
            ListItem oItem = new ListItem("Seleccionar", "0");
            ddlNivel.Items.Add(oItem);
            oItem = new ListItem("1", "1");
            ddlNivel.Items.Add(oItem);
            oItem = new ListItem("2", "2");
            ddlNivel.Items.Add(oItem);
            oItem = new ListItem("3", "3");
            ddlNivel.Items.Add(oItem);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    private void ListarDocumento()
    {
        try
        {
            ddlDocumento.Items.Clear();
            ListItem oItem = new ListItem("Seleccionar", "0");
            ddlDocumento.Items.Add(oItem);
            oItem = new ListItem("Caja Chica", "1");
            ddlDocumento.Items.Add(oItem);
            oItem = new ListItem("Entregas a Rendir", "2");
            ddlDocumento.Items.Add(oItem);
            oItem = new ListItem("Reembolso", "3");
            ddlDocumento.Items.Add(oItem);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (PerfilUsuario): " + ex.Message);
        }
    }

    private void ListarEsDeMonto()
    {
        try
        {
            ddlEsDeMonto.Items.Clear();
            ListItem oItem = new ListItem("Seleccionar", "0");
            ddlEsDeMonto.Items.Add(oItem);
            oItem = new ListItem("Si", "1");
            ddlEsDeMonto.Items.Add(oItem);
            oItem = new ListItem("No", "2");
            ddlEsDeMonto.Items.Add(oItem);
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
                    LlenarCampos(Convert.ToInt32(ViewState["IdNivel"].ToString()));
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
        txtIdPerfilUsuario.Text = "";
        txtDescripcion.Text = "";
    }

    private void LlenarCampos(int p)
    {
        try
        {
            NivelAprobacionBC ObjNivelAprobacionBC = new NivelAprobacionBC();
            NivelAprobacionBE ObjNivelAprobacionBE = new NivelAprobacionBE();
            ObjNivelAprobacionBE = ObjNivelAprobacionBC.ObtenerNivelAprobacion(p, 0);
            txtIdPerfilUsuario.Text = Convert.ToString(ObjNivelAprobacionBE.IdNivel);
            txtDescripcion.Text = ObjNivelAprobacionBE.Descripcion;
            ddlNivel.SelectedValue = ObjNivelAprobacionBE.Nivel;
            ddlDocumento.SelectedValue = ObjNivelAprobacionBE.Documento;
            ddlEsDeMonto.SelectedValue = ObjNivelAprobacionBE.EsDeMonto;
            txtMonto.Text = ObjNivelAprobacionBE.Monto;
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    protected void ddlEsDeMonto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEsDeMonto.SelectedValue == "1")
        {
            txtMonto.Enabled = true;
        }
        else if (ddlEsDeMonto.SelectedValue == "2")
        {
            txtMonto.Text = "";
            txtMonto.Enabled = false;
        }
    }

    protected void Crear_Click(object sender, EventArgs e)
    {
        int cod;
        try
        {
            if (txtDescripcion.Text.Trim() != "" &&
                ddlNivel.SelectedItem.Value != "0" &&
                ddlDocumento.SelectedItem.Value != "0" &&
                ddlEsDeMonto.SelectedItem.Value != "0")
            {
                NivelAprobacionBC ObjNivelAprobacionBC = new NivelAprobacionBC();
                NivelAprobacionBE ObjNivelAprobacionBE = new NivelAprobacionBE();

                ObjNivelAprobacionBE.Descripcion = txtDescripcion.Text;
                ObjNivelAprobacionBE.Nivel = ddlNivel.SelectedItem.Value;
                ObjNivelAprobacionBE.Documento = ddlDocumento.SelectedItem.Value;
                ObjNivelAprobacionBE.EsDeMonto = ddlEsDeMonto.SelectedItem.Value;
                ObjNivelAprobacionBE.Monto = txtMonto.Text;       

                if (Session["Usuario"] == null)
                {
                    Server.Transfer("~/Login.aspx");
                }
                else
                {
                    UsuarioBE objUsuarioBE = new UsuarioBE();
                    objUsuarioBE = (UsuarioBE)Session["Usuario"];
                    ObjNivelAprobacionBE.UserCreate = Convert.ToString(objUsuarioBE.IdUsuario);
                    ObjNivelAprobacionBE.CreateDate = DateTime.Now;
                    ObjNivelAprobacionBE.UserUpdate = Convert.ToString(objUsuarioBE.IdUsuario);
                    ObjNivelAprobacionBE.UpdateDate = DateTime.Now;
                }

                int Modo = Convert.ToInt32(ViewState["Modo"].ToString());
                int idPerfilUsuario = Convert.ToInt32(ViewState["IdNivel"].ToString());
                if (Modo == 1)
                {
                    cod = ObjNivelAprobacionBC.InsertarNivelAprobacion(ObjNivelAprobacionBE);
                }
                else
                {
                    ObjNivelAprobacionBE.IdNivel = idPerfilUsuario;
                    ObjNivelAprobacionBC.ModificarNivelAprobacion(ObjNivelAprobacionBE);
                }

                Server.Transfer("NivelAprobaciones.aspx");
            }
            else
            {
                Mensaje("Alerta: Es necesario llenar toda la informacion");
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (NivelAprobacion): " + ex.Message);
        }
    }

    protected void Cancelar_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/NivelAprobaciones.aspx");
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
}