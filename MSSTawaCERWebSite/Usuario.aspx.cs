using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using MSS.TAWA.BC;
using MSS.TAWA.BE;

public partial class Usuario : System.Web.UI.Page
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

                ListarEstado();
                ListarTipoUsuario();
                ListarPerfil();
                ListarAreaAprobacion();
                ListarAreaSolicitante();
                ListarNivelAprobacion();
                ListarCentroCostos();
                ListarUsuarioAprobador();
                Modalidad(Convert.ToInt32(strModo));
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuario): " + ex.Message);
        }
    }

    private void ListarEstado()
    {
        try
        {
            ddlEstado.Items.Clear();
            ListItem oItem = new ListItem("Seleccionar", "0");
            ddlEstado.Items.Add(oItem);
            oItem = new ListItem("Habilitado", "1");
            ddlEstado.Items.Add(oItem);
            oItem = new ListItem("Deshabilitado", "2");
            ddlEstado.Items.Add(oItem);
            oItem = new ListItem("Bloqueado", "3");
            ddlEstado.Items.Add(oItem);
            oItem = new ListItem("Permitir CC o ER o RE", "4");
            ddlEstado.Items.Add(oItem);

        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuario): " + ex.Message);
        }
    }

    private void ListarTipoUsuario()
    {
        try
        {
            ddlTipoUsuario.Items.Clear();
            ListItem oItem = new ListItem("Seleccionar", "0");
            ddlTipoUsuario.Items.Add(oItem);
            oItem = new ListItem("Usuario Interno WEB", "1");
            ddlTipoUsuario.Items.Add(oItem);
            oItem = new ListItem("Empleado", "2");
            ddlTipoUsuario.Items.Add(oItem);
            oItem = new ListItem("Usuario Externo WEB", "3");
            ddlTipoUsuario.Items.Add(oItem);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuario): " + ex.Message);
        }
    }

    private void ListarAreaAprobacion()
    {
        //try
        //{
        //    AreaBC objAreaBC = new AreaBC();
        //    List<AreaBE> lstAreaBE = new List<AreaBE>();
        //    lstAreaBE = objAreaBC.ListarArea(0, 0);

        //    cblArea.DataSource = lstAreaBE;
        //    cblArea.DataTextField = "Descripcion";
        //    cblArea.DataValueField = "IdArea";
        //    cblArea.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    Mensaje("Ocurrió un error (Usuario): " + ex.Message);
        //}
    }

    private void ListarAreaSolicitante()
    {
        //try
        //{
        //    AreaBC objAreaBC = new AreaBC();
        //    List<AreaBE> lstAreaBE = new List<AreaBE>();
        //    lstAreaBE = objAreaBC.ListarArea(0, 1);

        //    ddlArea1.DataSource = lstAreaBE;
        //    ddlArea1.DataTextField = "Descripcion";
        //    ddlArea1.DataValueField = "IdArea";
        //    ddlArea1.DataBind();

        //    ddlArea2.DataSource = lstAreaBE;
        //    ddlArea2.DataTextField = "Descripcion";
        //    ddlArea2.DataValueField = "IdArea";
        //    ddlArea2.DataBind();

        //    ddlArea3.DataSource = lstAreaBE;
        //    ddlArea3.DataTextField = "Descripcion";
        //    ddlArea3.DataValueField = "IdArea";
        //    ddlArea3.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    Mensaje("Ocurrió un error (Usuario): " + ex.Message);
        //}
    }

    private void ListarPerfil()
    {
        try
        {
            PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
            List<PerfilUsuarioBE> lstPerfilUsuarioBE = new List<PerfilUsuarioBE>();
            lstPerfilUsuarioBE = objPerfilUsuarioBC.ListarPerfilUsuario();

            ddlPerfilUsuario.DataSource = lstPerfilUsuarioBE;
            ddlPerfilUsuario.DataTextField = "Descripcion";
            ddlPerfilUsuario.DataValueField = "IdPerfilUsuario";
            ddlPerfilUsuario.DataBind();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuario): " + ex.Message);
        }
    }

    private void ListarNivelAprobacion()
    {
        //try
        //{
        //    AreaBC objAreaBC = new AreaBC();
        //    List<AreaBE> lstAreaBE = new List<AreaBE>();
        //    lstAreaBE = objAreaBC.ListarArea(0, 0);

        //    NivelAprobacionBC objNivelAprobacionBC = new NivelAprobacionBC();
        //    NivelAprobacionBE objNivelAprobacionBE = new NivelAprobacionBE();

        //    objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(1, 1);
        //    if (objNivelAprobacionBE != null)
        //    {
        //        ListItem oItem;
        //        for (int i = 0; i < lstAreaBE.Count; i++)
        //        {
        //            oItem = new ListItem("Nivel 1", objNivelAprobacionBE.IdNivel.ToString());
        //            //oItem = new ListItem("Nivel 1", (i + 1).ToString());
        //            cblCC1.Items.Add(oItem);
        //        }

        //        objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(1, 2);
        //        if (objNivelAprobacionBE != null)
        //        {
        //            for (int i = 0; i < lstAreaBE.Count; i++)
        //            {
        //                oItem = new ListItem("Nivel 2", objNivelAprobacionBE.IdNivel.ToString());
        //                cblCC2.Items.Add(oItem);
        //            }

        //            objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(1, 3);
        //            if (objNivelAprobacionBE != null)
        //            {
        //                for (int i = 0; i < lstAreaBE.Count; i++)
        //                {
        //                    oItem = new ListItem("Nivel 3", objNivelAprobacionBE.IdNivel.ToString());
        //                    cblCC3.Items.Add(oItem);
        //                }
        //            }
        //        }
        //    }

        //    objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(2, 1);
        //    if (objNivelAprobacionBE != null)
        //    {
        //        ListItem oItem;
        //        for (int i = 0; i < lstAreaBE.Count; i++)
        //        {
        //            oItem = new ListItem("Nivel 1", objNivelAprobacionBE.IdNivel.ToString());
        //            cblER1.Items.Add(oItem);
        //        }

        //        objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(2, 2);
        //        if (objNivelAprobacionBE != null)
        //        {
        //            for (int i = 0; i < lstAreaBE.Count; i++)
        //            {
        //                oItem = new ListItem("Nivel 2", objNivelAprobacionBE.IdNivel.ToString());
        //                cblER2.Items.Add(oItem);
        //            }

        //            objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(2, 3);
        //            if (objNivelAprobacionBE != null)
        //            {
        //                for (int i = 0; i < lstAreaBE.Count; i++)
        //                {
        //                    oItem = new ListItem("Nivel 3", objNivelAprobacionBE.IdNivel.ToString());
        //                    cblER3.Items.Add(oItem);
        //                }
        //            }
        //        }
        //    }

        //    objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(3, 1);
        //    if (objNivelAprobacionBE != null)
        //    {
        //        ListItem oItem;
        //        for (int i = 0; i < lstAreaBE.Count; i++)
        //        {
        //            oItem = new ListItem("Nivel 1", objNivelAprobacionBE.IdNivel.ToString());
        //            cblRE1.Items.Add(oItem);
        //        }

        //        objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(3, 2);
        //        if (objNivelAprobacionBE != null)
        //        {
        //            for (int i = 0; i < lstAreaBE.Count; i++)
        //            {
        //                oItem = new ListItem("Nivel 2", objNivelAprobacionBE.IdNivel.ToString());
        //                cblRE2.Items.Add(oItem);
        //            }

        //            objNivelAprobacionBE = objNivelAprobacionBC.ObtenerNivelAprobacion(3, 3);
        //            if (objNivelAprobacionBE != null)
        //            {
        //                for (int i = 0; i < lstAreaBE.Count; i++)
        //                {
        //                    oItem = new ListItem("Nivel 3", objNivelAprobacionBE.IdNivel.ToString());
        //                    cblRE3.Items.Add(oItem);
        //                }
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Mensaje("Ocurrió un error (Usuario): " + ex.Message);
        //}
    }

    private void ListarCentroCostos()
    {
        try
        {
            CentroCostosBC objCentroCostosBC = new CentroCostosBC();
            List<CentroCostosBE> lstCentroCostosBE = new List<CentroCostosBE>();
            lstCentroCostosBE = objCentroCostosBC.ListarCentroCostos(0, 10, 0);

            ddlCentroCostos1.DataSource = lstCentroCostosBE;
            ddlCentroCostos1.DataTextField = "Descripcion";
            ddlCentroCostos1.DataValueField = "IdCentroCostos";
            ddlCentroCostos1.DataBind();

            ddlCentroCostos2.DataSource = lstCentroCostosBE;
            ddlCentroCostos2.DataTextField = "Descripcion";
            ddlCentroCostos2.DataValueField = "IdCentroCostos";
            ddlCentroCostos2.DataBind();

            ddlCentroCostos3.DataSource = lstCentroCostosBE;
            ddlCentroCostos3.DataTextField = "Descripcion";
            ddlCentroCostos3.DataValueField = "IdCentroCostos";
            ddlCentroCostos3.DataBind();

            ddlCentroCostos4.DataSource = lstCentroCostosBE;
            ddlCentroCostos4.DataTextField = "Descripcion";
            ddlCentroCostos4.DataValueField = "IdCentroCostos";
            ddlCentroCostos4.DataBind();

            ddlCentroCostos5.DataSource = lstCentroCostosBE;
            ddlCentroCostos5.DataTextField = "Descripcion";
            ddlCentroCostos5.DataValueField = "IdCentroCostos";
            ddlCentroCostos5.DataBind();

            ddlCentroCostos6.DataSource = lstCentroCostosBE;
            ddlCentroCostos6.DataTextField = "Descripcion";
            ddlCentroCostos6.DataValueField = "IdCentroCostos";
            ddlCentroCostos6.DataBind();

            ddlCentroCostos7.DataSource = lstCentroCostosBE;
            ddlCentroCostos7.DataTextField = "Descripcion";
            ddlCentroCostos7.DataValueField = "IdCentroCostos";
            ddlCentroCostos7.DataBind();

            ddlCentroCostos8.DataSource = lstCentroCostosBE;
            ddlCentroCostos8.DataTextField = "Descripcion";
            ddlCentroCostos8.DataValueField = "IdCentroCostos";
            ddlCentroCostos8.DataBind();

            ddlCentroCostos9.DataSource = lstCentroCostosBE;
            ddlCentroCostos9.DataTextField = "Descripcion";
            ddlCentroCostos9.DataValueField = "IdCentroCostos";
            ddlCentroCostos9.DataBind();

            ddlCentroCostos10.DataSource = lstCentroCostosBE;
            ddlCentroCostos10.DataTextField = "Descripcion";
            ddlCentroCostos10.DataValueField = "IdCentroCostos";
            ddlCentroCostos10.DataBind();

            ddlCentroCostos11.DataSource = lstCentroCostosBE;
            ddlCentroCostos11.DataTextField = "Descripcion";
            ddlCentroCostos11.DataValueField = "IdCentroCostos";
            ddlCentroCostos11.DataBind();

            ddlCentroCostos12.DataSource = lstCentroCostosBE;
            ddlCentroCostos12.DataTextField = "Descripcion";
            ddlCentroCostos12.DataValueField = "IdCentroCostos";
            ddlCentroCostos12.DataBind();

            ddlCentroCostos13.DataSource = lstCentroCostosBE;
            ddlCentroCostos13.DataTextField = "Descripcion";
            ddlCentroCostos13.DataValueField = "IdCentroCostos";
            ddlCentroCostos13.DataBind();

            ddlCentroCostos14.DataSource = lstCentroCostosBE;
            ddlCentroCostos14.DataTextField = "Descripcion";
            ddlCentroCostos14.DataValueField = "IdCentroCostos";
            ddlCentroCostos14.DataBind();

            ddlCentroCostos15.DataSource = lstCentroCostosBE;
            ddlCentroCostos15.DataTextField = "Descripcion";
            ddlCentroCostos15.DataValueField = "IdCentroCostos";
            ddlCentroCostos15.DataBind();
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuario): " + ex.Message);
        }
    }

    private void ListarUsuarioAprobador()
    {
        UsuarioBC objUsarioBC = new UsuarioBC();
        List<UsuarioBE> lstUsarioBE = new List<UsuarioBE>();
        lstUsarioBE = objUsarioBC.ListarUsuario(11, 0, 0);

        ddlIdUsuarioCC1.DataSource = lstUsarioBE;
        ddlIdUsuarioCC1.DataTextField = "CardName";
        ddlIdUsuarioCC1.DataValueField = "IdUsuario";
        ddlIdUsuarioCC1.DataBind();

        ddlIdUsuarioCC2.DataSource = lstUsarioBE;
        ddlIdUsuarioCC2.DataTextField = "CardName";
        ddlIdUsuarioCC2.DataValueField = "IdUsuario";
        ddlIdUsuarioCC2.DataBind();

        //ddlIdUsuarioCC3.DataSource = lstUsarioBE;
        //ddlIdUsuarioCC3.DataTextField = "CardName";
        //ddlIdUsuarioCC3.DataValueField = "IdUsuario";
        //ddlIdUsuarioCC3.DataBind();

        ddlIdUsuarioER1.DataSource = lstUsarioBE;
        ddlIdUsuarioER1.DataTextField = "CardName";
        ddlIdUsuarioER1.DataValueField = "IdUsuario";
        ddlIdUsuarioER1.DataBind();

        ddlIdUsuarioER2.DataSource = lstUsarioBE;
        ddlIdUsuarioER2.DataTextField = "CardName";
        ddlIdUsuarioER2.DataValueField = "IdUsuario";
        ddlIdUsuarioER2.DataBind();

        ddlIdUsuarioER3.DataSource = lstUsarioBE;
        ddlIdUsuarioER3.DataTextField = "CardName";
        ddlIdUsuarioER3.DataValueField = "IdUsuario";
        ddlIdUsuarioER3.DataBind();

        ddlIdUsuarioRE1.DataSource = lstUsarioBE;
        ddlIdUsuarioRE1.DataTextField = "CardName";
        ddlIdUsuarioRE1.DataValueField = "IdUsuario";
        ddlIdUsuarioRE1.DataBind();

        ddlIdUsuarioRE2.DataSource = lstUsarioBE;
        ddlIdUsuarioRE2.DataTextField = "CardName";
        ddlIdUsuarioRE2.DataValueField = "IdUsuario";
        ddlIdUsuarioRE2.DataBind();

        ddlIdUsuarioRE3.DataSource = lstUsarioBE;
        ddlIdUsuarioRE3.DataTextField = "CardName";
        ddlIdUsuarioRE3.DataValueField = "IdUsuario";
        ddlIdUsuarioRE3.DataBind();
    }

    private void Modalidad(int p)
    {
        try
        {
            switch (p)
            {
                case 1:
                    lblCabezera.Text = "Crear Nuevo Usuario";
                    LimpiarCampos();
                    break;
                case 2:
                    lblCabezera.Text = "Modificar Usuario";
                    bCrear.Text = "Guardar";
                    LlenarCampos(Convert.ToInt32(ViewState["IdUsuario"].ToString()));
                    break;
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuario): " + ex.Message);
        }
    }

    private void LimpiarCampos()
    {
        txtIdUsuario.Text = "";
        //ddlTipoUsuario.Items.Clear();
        txtCardCode.Text = "";
        txtPass.Text = "";
        txtCardName.Text = "";
        //ddlPerfilUsuario.Items.Clear();
        txtCantMaxCC.Text = "";
        txtCantMaxER.Text = "";
        txtCantMaxRE.Text = "";
        txtPhone.Text = "";
        txtMail.Text = "";
    }

    private void LlenarCampos(int p)
    {
        try
        {
            //llenar campos de la tabla Usuario
            UsuarioBC objUsuarioBC = new UsuarioBC();
            UsuarioBE objUsuarioBE = new UsuarioBE();
            objUsuarioBE = objUsuarioBC.ObtenerUsuario(p, 0);
            txtIdUsuario.Text = objUsuarioBE.IdUsuario.ToString();
            ddlEstado.SelectedValue = objUsuarioBE.Estado;
            ddlTipoUsuario.SelectedValue = objUsuarioBE.Tipo;
            txtCardCode.Text = objUsuarioBE.CardCode;
            txtPass.Text = objUsuarioBE.Pass;
            txtCardName.Text = objUsuarioBE.CardName;
            ddlPerfilUsuario.SelectedValue = objUsuarioBE.IdPerfilUsuario.ToString();
            //ddlArea1.SelectedValue = objUsuarioBE.IdArea1.ToString();
            //ddlArea2.SelectedValue = objUsuarioBE.IdArea2.ToString();
            //ddlArea3.SelectedValue = objUsuarioBE.IdArea3.ToString();
            ddlCentroCostos1.SelectedValue = objUsuarioBE.IdCentroCostos1.ToString();
            ddlCentroCostos2.SelectedValue = objUsuarioBE.IdCentroCostos2.ToString();
            ddlCentroCostos3.SelectedValue = objUsuarioBE.IdCentroCostos3.ToString();
            ddlCentroCostos4.SelectedValue = objUsuarioBE.IdCentroCostos4.ToString();
            ddlCentroCostos5.SelectedValue = objUsuarioBE.IdCentroCostos5.ToString();
            ddlCentroCostos6.SelectedValue = objUsuarioBE.IdCentroCostos6.ToString();
            ddlCentroCostos7.SelectedValue = objUsuarioBE.IdCentroCostos7.ToString();
            ddlCentroCostos8.SelectedValue = objUsuarioBE.IdCentroCostos8.ToString();
            ddlCentroCostos9.SelectedValue = objUsuarioBE.IdCentroCostos9.ToString();
            ddlCentroCostos10.SelectedValue = objUsuarioBE.IdCentroCostos10.ToString();
            ddlCentroCostos11.SelectedValue = objUsuarioBE.IdCentroCostos11.ToString();
            ddlCentroCostos12.SelectedValue = objUsuarioBE.IdCentroCostos12.ToString();
            ddlCentroCostos13.SelectedValue = objUsuarioBE.IdCentroCostos13.ToString();
            ddlCentroCostos14.SelectedValue = objUsuarioBE.IdCentroCostos14.ToString();
            ddlCentroCostos15.SelectedValue = objUsuarioBE.IdCentroCostos15.ToString();
            ddlIdUsuarioCC1.SelectedValue = objUsuarioBE.IdUsuarioCC1.ToString();
            ddlIdUsuarioER1.SelectedValue = objUsuarioBE.IdUsuarioER1.ToString();
            ddlIdUsuarioRE1.SelectedValue = objUsuarioBE.IdUsuarioRE1.ToString();
            ddlIdUsuarioCC2.SelectedValue = objUsuarioBE.IdUsuarioCC2.ToString(); if (objUsuarioBE.IdUsuarioCC2 != 0) ddlIdUsuarioCC2.Enabled = true;
            ddlIdUsuarioER2.SelectedValue = objUsuarioBE.IdUsuarioER2.ToString(); if (objUsuarioBE.IdUsuarioER2 != 0) ddlIdUsuarioER2.Enabled = true;
            ddlIdUsuarioRE2.SelectedValue = objUsuarioBE.IdUsuarioRE2.ToString(); if (objUsuarioBE.IdUsuarioRE2 != 0) ddlIdUsuarioRE2.Enabled = true;
            ddlIdUsuarioCC3.SelectedValue = objUsuarioBE.IdUsuarioCC3.ToString(); if (objUsuarioBE.IdUsuarioCC3 != 0) ddlIdUsuarioCC3.Enabled = true;
            ddlIdUsuarioER3.SelectedValue = objUsuarioBE.IdUsuarioER3.ToString(); if (objUsuarioBE.IdUsuarioER3 != 0) ddlIdUsuarioER3.Enabled = true;
            ddlIdUsuarioRE3.SelectedValue = objUsuarioBE.IdUsuarioRE3.ToString(); if (objUsuarioBE.IdUsuarioRE3 != 0) ddlIdUsuarioRE3.Enabled = true;
            txtCantMaxCC.Text = objUsuarioBE.CantMaxCC;
            txtCantMaxER.Text = objUsuarioBE.CantMaxER;
            txtCantMaxRE.Text = objUsuarioBE.CantMaxRE;
            txtPhone.Text = objUsuarioBE.Phone;
            txtMail.Text = objUsuarioBE.Mail;

            PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
            PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBE();

            objPerfilUsuarioBE = objPerfilUsuarioBC.ObtenerPerfilUsuario(objUsuarioBE.IdPerfilUsuario);
            if (objPerfilUsuarioBE.CreaCajaChica == "1") txtCantMaxCC.Enabled = true;
            else { txtCantMaxCC.Enabled = false; txtCantMaxCC.Text = "0"; }
            if (objPerfilUsuarioBE.CreaEntregaRendir == "1") txtCantMaxER.Enabled = true;
            else { txtCantMaxER.Enabled = false; txtCantMaxER.Text = "0"; }
            if (objPerfilUsuarioBE.CreaReembolso == "1") txtCantMaxRE.Enabled = true;
            else { txtCantMaxRE.Enabled = false; txtCantMaxRE.Text = "0"; }

            /*
            //llenar nivel de aprobacion de la tabla UsuarioAreaNivel
            UsuarioAreaNivelBC objUsuarioAreaNivelBC = new UsuarioAreaNivelBC();
            List<UsuarioAreaNivelBE> lstUsuarioAreaNivelCCBE = new List<UsuarioAreaNivelBE>();
            List<UsuarioAreaNivelBE> lstUsuarioAreaNivelERBE = new List<UsuarioAreaNivelBE>();
            List<UsuarioAreaNivelBE> lstUsuarioAreaNivelREBE = new List<UsuarioAreaNivelBE>();
            lstUsuarioAreaNivelCCBE = objUsuarioAreaNivelBC.ListarUsuarioAreaNivel(p, 1, 1);
            lstUsuarioAreaNivelERBE = objUsuarioAreaNivelBC.ListarUsuarioAreaNivel(p, 1, 2);
            lstUsuarioAreaNivelREBE = objUsuarioAreaNivelBC.ListarUsuarioAreaNivel(p, 1, 3);

            int CC = 0, ER = 0, RE = 0;

            for (int i = 0; i < cblArea.Items.Count; i++)
            {
                if (lstUsuarioAreaNivelCCBE.Count > CC)
                {
                    if (cblArea.Items[i].Value == lstUsuarioAreaNivelCCBE[CC].IdArea.ToString())
                    {
                        cblArea.Items[i].Selected = true;
                        switch (lstUsuarioAreaNivelCCBE[CC].IdNivelAprobacion.ToString())
                        {
                            case "1": cblCC1.Items[i].Selected = true; break;
                            case "2": cblCC2.Items[i].Selected = true; break;
                            case "3": cblCC3.Items[i].Selected = true; break;
                        }
                        CC++;
                    }
                }

                if (lstUsuarioAreaNivelERBE.Count > ER)
                {
                    if (cblArea.Items[i].Value == lstUsuarioAreaNivelERBE[ER].IdArea.ToString())
                    {
                        cblArea.Items[i].Selected = true;
                        switch (lstUsuarioAreaNivelERBE[ER].IdNivelAprobacion.ToString())
                        {
                            case "1": cblER1.Items[i].Selected = true; break;
                            case "2": cblER2.Items[i].Selected = true; break;
                            case "3": cblER3.Items[i].Selected = true; break;
                        }
                        ER++;
                    }
                }

                if (lstUsuarioAreaNivelREBE.Count > RE)
                {
                    if (cblArea.Items[i].Value == lstUsuarioAreaNivelREBE[RE].IdArea.ToString())
                    {
                        cblArea.Items[i].Selected = true;
                        switch (lstUsuarioAreaNivelREBE[RE].IdNivelAprobacion.ToString())
                        {
                            case "1": cblRE1.Items[i].Selected = true; break;
                            case "2": cblRE2.Items[i].Selected = true; break;
                            case "3": cblRE3.Items[i].Selected = true; break;
                        }
                        RE++;
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////
            if (ddlTipoUsuario.SelectedValue == "1") //usuario web y empleado
            {
                //ddlPerfilUsuario.Enabled = true;
                txtCantMaxCC.Enabled = true;
                txtCantMaxER.Enabled = true;
                txtCantMaxRE.Enabled = true;

                lblAA.Visible = true;
                lblCC.Visible = true;
                lblER.Visible = true;
                lblRE.Visible = true;
                cblArea.Visible = true;
                cblCC1.Visible = true;
                cblCC2.Visible = true;
                cblCC3.Visible = true;
                cblER1.Visible = true;
                cblER2.Visible = true;
                cblER3.Visible = true;
                cblRE1.Visible = true;
                cblRE2.Visible = true;
                cblRE3.Visible = true;
            }
            else if (ddlTipoUsuario.SelectedValue == "2") //empleado
            {
                //ddlPerfilUsuario.Enabled = false;
                txtCantMaxCC.Enabled = false;
                txtCantMaxER.Enabled = false;
                txtCantMaxRE.Enabled = false;

                lblAA.Visible = false;
                lblCC.Visible = false;
                lblER.Visible = false;
                lblRE.Visible = false;
                cblArea.Visible = false;
                cblCC1.Visible = false;
                cblCC2.Visible = false;
                cblCC3.Visible = false;
                cblER1.Visible = false;
                cblER2.Visible = false;
                cblER3.Visible = false;
                cblRE1.Visible = false;
                cblRE2.Visible = false;
                cblRE3.Visible = false;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////
            if (ddlPerfilUsuario.SelectedValue != "0")
            {
                PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
                PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBE();

                objPerfilUsuarioBE = objPerfilUsuarioBC.ObtenerPerfilUsuario(Convert.ToInt32(ddlPerfilUsuario.SelectedValue));
                if (objPerfilUsuarioBE.ModCajaChica == "1" && objPerfilUsuarioBE.TipoAprobador != "2" && objPerfilUsuarioBE.TipoAprobador != "5")
                {
                    cblCC1.Visible = true; cblCC2.Visible = true; cblCC3.Visible = true;
                }
                else
                {
                    cblCC1.Visible = false; cblCC2.Visible = false; cblCC3.Visible = false;
                    for (int i = 0; i < cblArea.Items.Count; i++)
                    {
                        if (cblCC1.Items.Count > 0) cblCC1.Items[i].Selected = false;
                        if (cblCC2.Items.Count > 0) cblCC2.Items[i].Selected = false;
                        if (cblCC3.Items.Count > 0) cblCC3.Items[i].Selected = false;
                    }
                }

                if (objPerfilUsuarioBE.ModEntregaRendir == "1" && objPerfilUsuarioBE.TipoAprobador != "2" && objPerfilUsuarioBE.TipoAprobador != "5")
                {
                    cblER1.Visible = true; cblER2.Visible = true; cblER3.Visible = true;
                }
                else
                {
                    cblER1.Visible = false; cblER2.Visible = false; cblER3.Visible = false;
                    for (int i = 0; i < cblArea.Items.Count; i++)
                    {
                        if (cblER1.Items.Count > 0) cblER1.Items[i].Selected = false;
                        if (cblER2.Items.Count > 0) cblER2.Items[i].Selected = false;
                        if (cblER3.Items.Count > 0) cblER3.Items[i].Selected = false;
                    }
                }

                if (objPerfilUsuarioBE.ModReembolso == "1" && objPerfilUsuarioBE.TipoAprobador != "2" && objPerfilUsuarioBE.TipoAprobador != "5")
                {
                    cblRE1.Visible = true; cblRE2.Visible = true; cblRE3.Visible = true;
                }
                else
                {
                    cblRE1.Visible = false; cblRE2.Visible = false; cblRE3.Visible = false;
                    for (int i = 0; i < cblArea.Items.Count; i++)
                    {
                        if (cblRE1.Items.Count > 0) cblRE1.Items[i].Selected = false;
                        if (cblRE2.Items.Count > 0) cblRE2.Items[i].Selected = false;
                        if (cblRE3.Items.Count > 0) cblRE3.Items[i].Selected = false;
                    }
                }

                if (objPerfilUsuarioBE.CreaCajaChica == "1") txtCantMaxCC.Enabled = true;
                else txtCantMaxCC.Enabled = false;
                if (objPerfilUsuarioBE.CreaEntregaRendir == "1") txtCantMaxER.Enabled = true;
                else txtCantMaxER.Enabled = false;
                if (objPerfilUsuarioBE.CreaReembolso == "1") txtCantMaxRE.Enabled = true;
                else txtCantMaxRE.Enabled = false;

                if (objPerfilUsuarioBE.ModCajaChica == "2" && objPerfilUsuarioBE.ModEntregaRendir == "2" && objPerfilUsuarioBE.ModReembolso == "2")
                {
                    cblArea.Visible = false;
                }
                else
                {
                    if (objPerfilUsuarioBE.TipoAprobador == "2" || objPerfilUsuarioBE.TipoAprobador == "3" || objPerfilUsuarioBE.TipoAprobador == "5" || objPerfilUsuarioBE.TipoAprobador == "6")
                    {
                        cblArea.Visible = false;
                    }
                    else
                    {
                        cblArea.Visible = true;
                    }
                }
            }
            */
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuario): " + ex.Message);
        }
    }

    protected void Crear_Click(object sender, EventArgs e)
    {
        int Id;
        try
        {
            UsuarioBC objUsarioBC = new UsuarioBC();
            List<UsuarioBE> lstUsarioBE = new List<UsuarioBE>();
            if (Convert.ToInt32(ViewState["Modo"].ToString()) == 1)
                lstUsarioBE = objUsarioBC.ListarUsuario2(0, 2, 0, txtCardCode.Text);

            if (bCrear.Text == "Crear")
            {

                if (objUsarioBC.VerificarUsuario(txtCardCode.Text) == 0)
                {
                    throw new Exception("DNI no pertenece a la nomina..validar");
                }
            }


            if (ddlEstado.SelectedItem.Value != "0" && ddlTipoUsuario.SelectedItem.Value != "0" && txtCardCode.Text.Trim() != "" &&
                /*txtPass.Text.Trim() != "" &&*/ txtCardName.Text.Trim() != "" && ddlPerfilUsuario.SelectedItem.Value != "0" &&
                txtCantMaxCC.Text.Trim() != "" && txtCantMaxER.Text.Trim() != "" && txtCantMaxRE.Text.Trim() != "" && /*txtMail.Text.Trim() != "" &&*/
                ValidarAreaNivel() && lstUsarioBE.Count == 0)
            {
                UsuarioBE ObjUsuarioBE = new UsuarioBE();
                UsuarioBC objUsuarioBC = new UsuarioBC();

                ObjUsuarioBE.Tipo = Convert.ToString(ddlTipoUsuario.SelectedItem.Value);
                ObjUsuarioBE.Estado = Convert.ToString(ddlEstado.SelectedItem.Value);
                ObjUsuarioBE.CardCode = txtCardCode.Text;
                ObjUsuarioBE.Pass = txtPass.Text;
                ObjUsuarioBE.CardName = txtCardName.Text;
                ObjUsuarioBE.IdPerfilUsuario = Convert.ToInt32(ddlPerfilUsuario.SelectedItem.Value);
                ObjUsuarioBE.IdArea1 = 0;//Convert.ToInt32(ddlArea1.SelectedItem.Value);
                ObjUsuarioBE.IdArea2 = 0;//Convert.ToInt32(ddlArea2.SelectedItem.Value);
                ObjUsuarioBE.IdArea3 = 0;//Convert.ToInt32(ddlArea3.SelectedItem.Value);
                ObjUsuarioBE.IdArea4 = 0;//Convert.ToInt32(ddlArea4.SelectedItem.Value);
                ObjUsuarioBE.IdArea5 = 0;//Convert.ToInt32(ddlArea5.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos1 = Convert.ToInt32(ddlCentroCostos1.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos2 = Convert.ToInt32(ddlCentroCostos2.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos3 = Convert.ToInt32(ddlCentroCostos3.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos4 = Convert.ToInt32(ddlCentroCostos4.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos5 = Convert.ToInt32(ddlCentroCostos5.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos6 = Convert.ToInt32(ddlCentroCostos6.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos7 = Convert.ToInt32(ddlCentroCostos7.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos8 = Convert.ToInt32(ddlCentroCostos8.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos9 = Convert.ToInt32(ddlCentroCostos9.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos10 = Convert.ToInt32(ddlCentroCostos10.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos11 = Convert.ToInt32(ddlCentroCostos11.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos12 = Convert.ToInt32(ddlCentroCostos12.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos13 = Convert.ToInt32(ddlCentroCostos13.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos14 = Convert.ToInt32(ddlCentroCostos14.SelectedItem.Value);
                ObjUsuarioBE.IdCentroCostos15 = Convert.ToInt32(ddlCentroCostos15.SelectedItem.Value);
                ObjUsuarioBE.IdUsuarioCC1 = Convert.ToInt32(ddlIdUsuarioCC1.SelectedItem.Value);
                ObjUsuarioBE.IdUsuarioCC2 = Convert.ToInt32(ddlIdUsuarioCC2.SelectedItem.Value);
                ObjUsuarioBE.IdUsuarioCC3 = 0;//Convert.ToInt32(ddlIdUsuarioCC3.SelectedItem.Value);
                ObjUsuarioBE.IdUsuarioER1 = Convert.ToInt32(ddlIdUsuarioER1.SelectedItem.Value);
                ObjUsuarioBE.IdUsuarioER2 = Convert.ToInt32(ddlIdUsuarioER2.SelectedItem.Value);
                ObjUsuarioBE.IdUsuarioER3 = Convert.ToInt32(ddlIdUsuarioER3.SelectedItem.Value);
                ObjUsuarioBE.IdUsuarioRE1 = Convert.ToInt32(ddlIdUsuarioRE1.SelectedItem.Value);
                ObjUsuarioBE.IdUsuarioRE2 = Convert.ToInt32(ddlIdUsuarioRE2.SelectedItem.Value);
                ObjUsuarioBE.IdUsuarioRE3 = Convert.ToInt32(ddlIdUsuarioRE3.SelectedItem.Value);
                ObjUsuarioBE.CantMaxCC = txtCantMaxCC.Text;
                ObjUsuarioBE.CantMaxER = txtCantMaxER.Text;
                ObjUsuarioBE.CantMaxRE = txtCantMaxRE.Text;
                ObjUsuarioBE.Phone = txtPhone.Text;
                ObjUsuarioBE.Mail = txtMail.Text;
                ObjUsuarioBE.Comentario = "";

                if (Session["Usuario"] == null)
                {
                    Server.Transfer("~/Login.aspx");
                }
                else
                {
                    UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                    objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                    ObjUsuarioBE.UserCreate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    ObjUsuarioBE.CreateDate = DateTime.Now;
                    ObjUsuarioBE.UserUpdate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    ObjUsuarioBE.UpdateDate = DateTime.Now;
                }

                int Modo = Convert.ToInt32(ViewState["Modo"].ToString());
                int idUsuario = Convert.ToInt32(ViewState["IdUsuario"].ToString());
                if (Modo == 1)
                {
                    Id = objUsuarioBC.InsertarUsuario(ObjUsuarioBE, 0, 0);
                    InsertarAreaNivelAprobacion(ObjUsuarioBE);
                }
                else
                {
                    ObjUsuarioBE.IdUsuario = idUsuario;
                    objUsuarioBC.ModificarUsuario(ObjUsuarioBE);
                    ModificarAreaNivelAprobacion(ObjUsuarioBE);
                    EliminarUsuarioAreaNivel(ObjUsuarioBE.IdUsuario);
                }

                Response.Redirect("Usuarios.aspx");
            }
            else
            {
                Mensaje("Alerta: El DNI ya existe / Es necesario llenar toda la informacion / si selecciono un area asegurese de seleccionar al menos un nivel de CC o ER o RE / por Area solo puede seleccionar un nivel de CC o ER o RE");
            }
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuario): " + ex.Message);
        }
    }

    private void InsertarAreaNivelAprobacion(UsuarioBE ObjUsuarioBE)
    {
        /*
        UsuarioAreaNivelBC objUsuarioAreaNivelBC = new UsuarioAreaNivelBC();
        UsuarioAreaNivelBE objUsuarioAreaNivelBE = new UsuarioAreaNivelBE();

        int IdNivelAprobacion = 0;
        int Id = 0;
        for (int i = 0; i < cblArea.Items.Count; i++)
        {
            if (cblArea.Items[i].Selected == true)
            {
                //caja chica
                if (cblCC1.Items.Count > 0) if (cblCC1.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblCC1.Items[i].Value);
                if (cblCC2.Items.Count > 0) if (cblCC2.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblCC2.Items[i].Value);
                if (cblCC3.Items.Count > 0) if (cblCC3.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblCC3.Items[i].Value);

                if (IdNivelAprobacion != 0)
                {
                    objUsuarioAreaNivelBE.Id = 0;
                    objUsuarioAreaNivelBE.IdUsuario = ObjUsuarioBE.IdUsuario;
                    objUsuarioAreaNivelBE.IdArea = Convert.ToInt32(cblArea.Items[i].Value);
                    objUsuarioAreaNivelBE.IdNivelAprobacion = IdNivelAprobacion;

                    UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                    objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                    objUsuarioAreaNivelBE.UserCreate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.CreateDate = DateTime.Now;
                    objUsuarioAreaNivelBE.UserUpdate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.UpdateDate = DateTime.Now;

                    Id = objUsuarioAreaNivelBC.InsertarUsuarioAreaNivel(objUsuarioAreaNivelBE);
                    IdNivelAprobacion = 0;
                }

                //entrega a rendir
                if (cblER1.Items.Count > 0) if (cblER1.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblER1.Items[i].Value);
                if (cblER2.Items.Count > 0) if (cblER2.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblER2.Items[i].Value);
                if (cblER3.Items.Count > 0) if (cblER3.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblER3.Items[i].Value);

                if (IdNivelAprobacion != 0)
                {
                    objUsuarioAreaNivelBE.Id = 0;
                    objUsuarioAreaNivelBE.IdUsuario = ObjUsuarioBE.IdUsuario;
                    objUsuarioAreaNivelBE.IdArea = Convert.ToInt32(cblArea.Items[i].Value);
                    objUsuarioAreaNivelBE.IdNivelAprobacion = IdNivelAprobacion;

                    UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                    objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                    objUsuarioAreaNivelBE.UserCreate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.CreateDate = DateTime.Now;
                    objUsuarioAreaNivelBE.UserUpdate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.UpdateDate = DateTime.Now;

                    Id = objUsuarioAreaNivelBC.InsertarUsuarioAreaNivel(objUsuarioAreaNivelBE);
                    IdNivelAprobacion = 0;
                }

                //reembolso
                if (cblRE1.Items.Count > 0) if (cblRE1.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblRE1.Items[i].Value);
                if (cblRE2.Items.Count > 0) if (cblRE2.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblRE2.Items[i].Value);
                if (cblRE3.Items.Count > 0) if (cblRE3.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblRE3.Items[i].Value);

                if (IdNivelAprobacion != 0)
                {
                    objUsuarioAreaNivelBE.Id = 0;
                    objUsuarioAreaNivelBE.IdUsuario = ObjUsuarioBE.IdUsuario;
                    objUsuarioAreaNivelBE.IdArea = Convert.ToInt32(cblArea.Items[i].Value);
                    objUsuarioAreaNivelBE.IdNivelAprobacion = IdNivelAprobacion;

                    UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                    objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                    objUsuarioAreaNivelBE.UserCreate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.CreateDate = DateTime.Now;
                    objUsuarioAreaNivelBE.UserUpdate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.UpdateDate = DateTime.Now;

                    Id = objUsuarioAreaNivelBC.InsertarUsuarioAreaNivel(objUsuarioAreaNivelBE);
                    IdNivelAprobacion = 0;
                }
            }
        }
        */
    }

    private bool ValidarAreaNivel()
    {
        bool retorno = true;

        //int checkCC = 0, checkER = 0, checkRE = 0;
        //for (int i = 0; i < cblArea.Items.Count; i++)
        //{
        //    if (cblArea.Items[i].Selected == true)
        //    {
        //        if (cblCC1.Items.Count > 0) if (cblCC1.Items[i].Selected == true) checkCC++;
        //        if (cblCC2.Items.Count > 0) if (cblCC2.Items[i].Selected == true) checkCC++;
        //        if (cblCC3.Items.Count > 0) if (cblCC3.Items[i].Selected == true) checkCC++;
        //        if (cblER1.Items.Count > 0) if (cblER1.Items[i].Selected == true) checkER++;
        //        if (cblER2.Items.Count > 0) if (cblER2.Items[i].Selected == true) checkER++;
        //        if (cblER3.Items.Count > 0) if (cblER3.Items[i].Selected == true) checkER++;
        //        if (cblRE1.Items.Count > 0) if (cblRE1.Items[i].Selected == true) checkRE++;
        //        if (cblRE2.Items.Count > 0) if (cblRE2.Items[i].Selected == true) checkRE++;
        //        if (cblRE3.Items.Count > 0) if (cblRE3.Items[i].Selected == true) checkRE++;

        //        if (checkCC > 1 && checkER > 1 && checkRE > 1) return false;

        //        if (checkCC == 1)
        //        {
        //            if (checkER == 1 || checkER == 0)
        //            {
        //                if (checkRE == 1 || checkER == 0) retorno = true;
        //                else return false;
        //            }
        //            else
        //                return false;
        //        }
        //        else
        //        {
        //            if (checkER == 1)
        //            {
        //                if (checkCC == 1 || checkCC == 0)
        //                {
        //                    if (checkRE == 1 || checkRE == 0) retorno = true;
        //                    else return false;
        //                }
        //                else
        //                    return false;
        //            }
        //            else
        //            {
        //                if (checkRE == 1)
        //                {
        //                    if (checkCC == 1 || checkCC == 0)
        //                    {
        //                        if (checkER == 1 || checkER == 0) retorno = true;
        //                        else return false;
        //                    }
        //                    else
        //                        return false;
        //                }
        //            }
        //        }

        //        checkCC = 0; checkER = 0; checkRE = 0;
        //    }
        //}

        return retorno;
    }

    private void ModificarAreaNivelAprobacion(UsuarioBE ObjUsuarioBE)
    {
        /*
        UsuarioAreaNivelBC objUsuarioAreaNivelBC = new UsuarioAreaNivelBC();
        UsuarioAreaNivelBE objUsuarioAreaNivelBE = new UsuarioAreaNivelBE();

        int IdNivelAprobacion = 0;
        for (int i = 0; i < cblArea.Items.Count; i++)
        {
            if (cblArea.Items[i].Selected == true)
            {
                //caja chica
                if (cblCC1.Items.Count > 0) if (cblCC1.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblCC1.Items[i].Value);
                if (cblCC2.Items.Count > 0) if (cblCC2.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblCC2.Items[i].Value);
                if (cblCC3.Items.Count > 0) if (cblCC3.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblCC3.Items[i].Value);

                if (IdNivelAprobacion != 0)
                {
                    objUsuarioAreaNivelBE.Id = 0;
                    objUsuarioAreaNivelBE.IdUsuario = ObjUsuarioBE.IdUsuario;
                    objUsuarioAreaNivelBE.IdArea = Convert.ToInt32(cblArea.Items[i].Value);
                    objUsuarioAreaNivelBE.IdNivelAprobacion = IdNivelAprobacion;

                    UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                    objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                    objUsuarioAreaNivelBE.UserCreate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.CreateDate = DateTime.Now;
                    objUsuarioAreaNivelBE.UserUpdate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.UpdateDate = DateTime.Now;

                    objUsuarioAreaNivelBC.ModificarUsuarioAreaNivel(objUsuarioAreaNivelBE);
                    IdNivelAprobacion = 0;
                }

                //entrega a rendir
                if (cblER1.Items.Count > 0) if (cblER1.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblER1.Items[i].Value);
                if (cblER2.Items.Count > 0) if (cblER2.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblER2.Items[i].Value);
                if (cblER3.Items.Count > 0) if (cblER3.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblER3.Items[i].Value);

                if (IdNivelAprobacion != 0)
                {
                    objUsuarioAreaNivelBE.Id = 0;
                    objUsuarioAreaNivelBE.IdUsuario = ObjUsuarioBE.IdUsuario;
                    objUsuarioAreaNivelBE.IdArea = Convert.ToInt32(cblArea.Items[i].Value);
                    objUsuarioAreaNivelBE.IdNivelAprobacion = IdNivelAprobacion;

                    UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                    objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                    objUsuarioAreaNivelBE.UserCreate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.CreateDate = DateTime.Now;
                    objUsuarioAreaNivelBE.UserUpdate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.UpdateDate = DateTime.Now;

                    objUsuarioAreaNivelBC.ModificarUsuarioAreaNivel(objUsuarioAreaNivelBE);
                    IdNivelAprobacion = 0;
                }

                //reembolso
                if (cblRE1.Items.Count > 0) if (cblRE1.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblRE1.Items[i].Value);
                if (cblRE2.Items.Count > 0) if (cblRE2.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblRE2.Items[i].Value);
                if (cblRE3.Items.Count > 0) if (cblRE3.Items[i].Selected == true) IdNivelAprobacion = Convert.ToInt32(cblRE3.Items[i].Value);

                if (IdNivelAprobacion != 0)
                {
                    objUsuarioAreaNivelBE.Id = 0;
                    objUsuarioAreaNivelBE.IdUsuario = ObjUsuarioBE.IdUsuario;
                    objUsuarioAreaNivelBE.IdArea = Convert.ToInt32(cblArea.Items[i].Value);
                    objUsuarioAreaNivelBE.IdNivelAprobacion = IdNivelAprobacion;

                    UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                    objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                    objUsuarioAreaNivelBE.UserCreate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.CreateDate = DateTime.Now;
                    objUsuarioAreaNivelBE.UserUpdate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                    objUsuarioAreaNivelBE.UpdateDate = DateTime.Now;

                    objUsuarioAreaNivelBC.ModificarUsuarioAreaNivel(objUsuarioAreaNivelBE);
                    IdNivelAprobacion = 0;
                }
            }
        }
        */
    }

    private void EliminarUsuarioAreaNivel(int IdUsuario)
    {
        UsuarioAreaNivelBC objUsuarioAreaNivelBC = new UsuarioAreaNivelBC();
        objUsuarioAreaNivelBC.EliminarUsuarioAreaNivel(IdUsuario);
    }

    protected void Cancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Usuarios.aspx");
    }

    protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*
        if (ddlTipoUsuario.SelectedValue == "1") //usuario web y empleado
        {
            //ddlPerfilUsuario.Enabled = true;
            txtCantMaxCC.Enabled = true;
            txtCantMaxER.Enabled = true;
            txtCantMaxRE.Enabled = true;

            lblAA.Visible = true;
            lblCC.Visible = true;
            lblER.Visible = true;
            lblRE.Visible = true;
            cblArea.Visible = true;
            cblCC1.Visible = true;
            cblCC2.Visible = true;
            cblCC3.Visible = true;
            cblER1.Visible = true;
            cblER2.Visible = true;
            cblER3.Visible = true;
            cblRE1.Visible = true;
            cblRE2.Visible = true;
            cblRE3.Visible = true;
        }
        else if (ddlTipoUsuario.SelectedValue == "2") //empleado
        {
            //ddlPerfilUsuario.Enabled = false;
            txtCantMaxCC.Enabled = false;
            txtCantMaxER.Enabled = false;
            txtCantMaxRE.Enabled = false;

            lblAA.Visible = false;
            lblCC.Visible = false;
            lblER.Visible = false;
            lblRE.Visible = false;
            cblArea.Visible = false;
            cblCC1.Visible = false;
            cblCC2.Visible = false;
            cblCC3.Visible = false;
            cblER1.Visible = false;
            cblER2.Visible = false;
            cblER3.Visible = false;
            cblRE1.Visible = false;
            cblRE2.Visible = false;
            cblRE3.Visible = false;
        }
        */
    }

    protected void ddlPerfilUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPerfilUsuario.SelectedValue != "0")
        {
            PerfilUsuarioBC objPerfilUsuarioBC = new PerfilUsuarioBC();
            PerfilUsuarioBE objPerfilUsuarioBE = new PerfilUsuarioBE();

            objPerfilUsuarioBE = objPerfilUsuarioBC.ObtenerPerfilUsuario(Convert.ToInt32(ddlPerfilUsuario.SelectedValue));

            if (objPerfilUsuarioBE.CreaCajaChica == "2")
            {
                ddlIdUsuarioCC1.Enabled = false; ddlIdUsuarioCC1.SelectedValue = "0";
                ddlIdUsuarioCC2.Enabled = false; ddlIdUsuarioCC2.SelectedValue = "0";
                txtCantMaxCC.Enabled = false; txtCantMaxCC.Text = "0";
            }
            else
            {
                ddlIdUsuarioCC1.Enabled = true;
                txtCantMaxCC.Enabled = true; txtCantMaxCC.Text = "3";
            }

            if (objPerfilUsuarioBE.CreaEntregaRendir == "2")
            {
                ddlIdUsuarioER1.Enabled = false; ddlIdUsuarioER1.SelectedValue = "0";
                ddlIdUsuarioER2.Enabled = false; ddlIdUsuarioER2.SelectedValue = "0";
                ddlIdUsuarioER3.Enabled = false; ddlIdUsuarioER3.SelectedValue = "0";
                txtCantMaxER.Enabled = false; txtCantMaxER.Text = "0";
            }
            else
            {
                ddlIdUsuarioER1.Enabled = true;
                txtCantMaxER.Enabled = true; txtCantMaxER.Text = "3";
            }

            if (objPerfilUsuarioBE.CreaReembolso == "2")
            {
                ddlIdUsuarioRE1.Enabled = false; ddlIdUsuarioRE1.SelectedValue = "0";
                ddlIdUsuarioRE2.Enabled = false; ddlIdUsuarioRE2.SelectedValue = "0";
                ddlIdUsuarioRE3.Enabled = false; ddlIdUsuarioRE3.SelectedValue = "0";
                txtCantMaxRE.Enabled = false; txtCantMaxRE.Text = "0";
            }
            else
            {
                ddlIdUsuarioRE1.Enabled = true;
                txtCantMaxRE.Enabled = true; txtCantMaxRE.Text = "3";
            }
        }
    }

    protected void ddlIdUsuarioCC1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdUsuarioCC1.SelectedValue != "0")
        {
            ddlIdUsuarioCC2.Enabled = true;
        }
        else
        {
            ddlIdUsuarioCC2.Enabled = false;
            ddlIdUsuarioCC2.SelectedValue = "0";
            ddlIdUsuarioCC3.Enabled = false;
            ddlIdUsuarioCC3.SelectedValue = "0";
        }
    }

    protected void ddlIdUsuarioCC2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdUsuarioCC2.SelectedValue != "0")
        {
            ddlIdUsuarioCC3.Enabled = true;
        }
        else
        {
            ddlIdUsuarioCC3.Enabled = false;
            ddlIdUsuarioCC3.SelectedValue = "0";
        }
    }

    protected void ddlIdUsuarioER1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdUsuarioER1.SelectedValue != "0")
        {
            ddlIdUsuarioER2.Enabled = true;
        }
        else
        {
            ddlIdUsuarioER2.Enabled = false;
            ddlIdUsuarioER2.SelectedValue = "0";
            ddlIdUsuarioER3.Enabled = false;
            ddlIdUsuarioER3.SelectedValue = "0";
        }
    }

    protected void ddlIdUsuarioER2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdUsuarioER2.SelectedValue != "0")
        {
            ddlIdUsuarioER3.Enabled = true;
        }
        else
        {
            ddlIdUsuarioER3.Enabled = false;
            ddlIdUsuarioER3.SelectedValue = "0";
        }
    }

    protected void ddlIdUsuarioRE1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdUsuarioRE1.SelectedValue != "0")
        {
            ddlIdUsuarioRE2.Enabled = true;
        }
        else
        {
            ddlIdUsuarioRE2.Enabled = false;
            ddlIdUsuarioRE2.SelectedValue = "0";
            ddlIdUsuarioRE3.Enabled = false;
            ddlIdUsuarioRE3.SelectedValue = "0";
        }
    }

    protected void ddlIdUsuarioRE2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdUsuarioRE2.SelectedValue != "0")
        {
            ddlIdUsuarioRE3.Enabled = true;
        }
        else
        {
            ddlIdUsuarioRE3.Enabled = false;
            ddlIdUsuarioRE3.SelectedValue = "0";
        }
    }

    protected void cblCC_SelectedIndexChanged(object sender, EventArgs e)
    {
        //CheckBoxList checkboxlist = (CheckBoxList)sender;
        //if (checkboxlist.SelectedItem != null)
        //{
        //    string a = checkboxlist.SelectedItem.Text;
        //    if (a == "Nivel 1")
        //    {
        //        for (int i = 0; i < cblArea.Items.Count; i++)
        //        {
        //            if (cblCC1.Items[i].Selected == true)
        //            {
        //                cblCC2.Items[i].Selected = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < cblArea.Items.Count; i++)
        //        {
        //            if (cblCC2.Items[i].Selected == true)
        //            {
        //                cblCC1.Items[i].Selected = false;
        //            }
        //        }
        //    }
        //}
    }

    protected void cblER_SelectedIndexChanged(object sender, EventArgs e)
    {
        //CheckBoxList checkboxlist = (CheckBoxList)sender;
        //if (checkboxlist.SelectedItem != null)
        //{
        //    string a = checkboxlist.SelectedItem.Text;
        //    if (a == "Nivel 1")
        //    {
        //        for (int i = 0; i < cblArea.Items.Count; i++)
        //        {
        //            if (cblER1.Items[i].Selected == true)
        //            {
        //                cblER2.Items[i].Selected = false;
        //                cblER3.Items[i].Selected = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (a == "Nivel 2")
        //        {
        //            for (int i = 0; i < cblArea.Items.Count; i++)
        //            {
        //                if (cblER2.Items[i].Selected == true)
        //                {
        //                    cblER1.Items[i].Selected = false;
        //                    cblER3.Items[i].Selected = false;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (int i = 0; i < cblArea.Items.Count; i++)
        //            {
        //                if (cblER3.Items[i].Selected == true)
        //                {
        //                    cblER1.Items[i].Selected = false;
        //                    cblER2.Items[i].Selected = false;
        //                }
        //            }
        //        }
        //    }
        //}
    }

    protected void cblRE_SelectedIndexChanged(object sender, EventArgs e)
    {
        //CheckBoxList checkboxlist = (CheckBoxList)sender;
        //if (checkboxlist.SelectedItem != null)
        //{
        //    string a = checkboxlist.SelectedItem.Text;
        //    if (a == "Nivel 1")
        //    {
        //        for (int i = 0; i < cblArea.Items.Count; i++)
        //        {
        //            if (cblRE1.Items[i].Selected == true)
        //            {
        //                cblRE2.Items[i].Selected = false;
        //                cblRE3.Items[i].Selected = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (a == "Nivel 2")
        //        {
        //            for (int i = 0; i < cblArea.Items.Count; i++)
        //            {
        //                if (cblRE2.Items[i].Selected == true)
        //                {
        //                    cblRE1.Items[i].Selected = false;
        //                    cblRE3.Items[i].Selected = false;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (int i = 0; i < cblArea.Items.Count; i++)
        //            {
        //                if (cblRE3.Items[i].Selected == true)
        //                {
        //                    cblRE1.Items[i].Selected = false;
        //                    cblRE2.Items[i].Selected = false;
        //                }
        //            }
        //        }
        //    }
        //}
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
    protected void Masivo_Click(object sender, EventArgs e)
    {
        blbResultadoMasivo.Visible = true;
        txtCopied.Visible = true;
        GridView1.Visible = true;
        //bAgregar4.Visible = true;
        bPreliminar4.Visible = true;
        bCancelar4.Visible = true;
        bMasivo.Visible = false;
    }
    protected void Preliminar4_Click(object sender, EventArgs e)
    {
        try
        {
            int dni = 0;

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[23] { 
                    new DataColumn("Estado", typeof(int)),
                    new DataColumn("Tipo_Usuario", typeof(int)),
                    new DataColumn("Codigo",typeof(Int32)),
                    new DataColumn("Contrasena",typeof(string)),
                    new DataColumn("Nombre_Usuario",typeof(string)),
                    new DataColumn("Perfil_Usuario",typeof(int)),
                    new DataColumn("Cantidad_CC",typeof(int)),
                    new DataColumn("Cantidad_ER",typeof(int)),
                    new DataColumn("Cantidad_RE",typeof(int)),
                    new DataColumn("Telefono",typeof(int)),
                    new DataColumn("Correo",typeof(string)),
                    new DataColumn("Centro_Costos3_1",typeof(string)),
                    new DataColumn("Centro_Costos3_2",typeof(string)),
                    new DataColumn("Centro_Costos3_3",typeof(string)),
                    new DataColumn("Nivel_Aprobacion_1_CC",typeof(string)),
                    new DataColumn("Nivel_Aprobacion_2_CC",typeof(string)),
                    new DataColumn("Nivel_Aprobacion_3_CC",typeof(string)),
                    new DataColumn("Nivel_Aprobacion_1_ER",typeof(string)),
                    new DataColumn("Nivel_Aprobacion_2_ER",typeof(string)),
                    new DataColumn("Nivel_Aprobacion_3_ER",typeof(string)),
                    new DataColumn("Nivel_Aprobacion_1_RE",typeof(string)),
                    new DataColumn("Nivel_Aprobacion_2_RE",typeof(string)),
                    new DataColumn("Nivel_Aprobacion_3_RE",typeof(string))});

            string copiedContent = Request.Form[txtCopied.UniqueID];
            foreach (string row in copiedContent.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;



                    foreach (string cell in row.Split('\t'))
                    {

                        //if (i == 4)
                        //{
                        //if (cell.Length > 11)
                        //    throw new Exception("El RUC Contiene mas de 11 caracteres en la fila :" + row.ToString());

                        //long ruc = 0;
                        //bool resultado = long.TryParse(cell, out ruc);

                        //if (!resultado)
                        //    throw new Exception("El RUC contiene caracteres no numericos");

                        //}
                        
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        if (dt.Rows[dt.Rows.Count - 1][i].ToString()  == "#N/A")
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = "0";
                        }
                        i++;

                    }
                }
            }


            GridView1.DataSource = dt;
            GridView1.DataBind();
            txtCopied.Text = "";


            for (int x = 0; x < GridView1.Rows.Count; x++)
            {
                String dni1 = GridView1.Rows[x].Cells[2].Text;
                dni = Convert.ToInt32(GridView1.Rows[x].Cells[2].Text);

                UsuarioBC objUsarioBC = new UsuarioBC();
                List<UsuarioBE> lstUsarioBE = new List<UsuarioBE>();


                if (objUsarioBC.VerificarUsuario(dni1) == 0)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    txtCopied.Text = "";
                    throw new Exception("DNI de la fila:" + (x + 1).ToString() + " no pertenece a la nomina..validar");
                }


                if (objUsarioBC.VerificarUsuarioExiste (dni) > 0)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    txtCopied.Text = "";
                    throw new Exception("DNI de la fila:" + (x + 1).ToString() + " ya se encuentra registrado... validar");
                }
            }

            blbResultadoMasivo.Text = "Vista Preliminar cargada correctamente.";

            bAgregar4.Visible = true;
            bPreliminar4.Visible = false;
        }
        catch (Exception ex)
        {
            //Mensaje("Ocurrió un error (Prueba): " + ex.Message);
            blbResultadoMasivo.Text = "Ocurrió un error (Prueba): " + ex.Message;
        }

    }
    protected void Agregar4_Click(object sender, EventArgs e)
    {
        try
        {
            string mensajeError = "";
            bool validacion = true;
            int dni = 0;
            int id = 0;

            int iRows = GridView1.Rows.Count;
            if (iRows <= 0)
            {
                validacion = false;
                mensajeError = "No existe informacion que subir.";
            }          

           if (validacion)
            {
                for (int x = 0; x < GridView1.Rows.Count; x++)
                {
                    dni = Convert.ToInt32(GridView1.Rows[x].Cells[2].Text);
                    UsuarioBE ObjUsuarioBE = new UsuarioBE();
                    UsuarioBC objUsuarioBC = new UsuarioBC();

                    ObjUsuarioBE.Tipo =                 Convert.ToString(GridView1.Rows[x].Cells[1].Text);
                    ObjUsuarioBE.Estado =               Convert.ToString(GridView1.Rows[x].Cells[0].Text);
                    ObjUsuarioBE.CardCode =             Convert.ToString(GridView1.Rows[x].Cells[2].Text);
                    ObjUsuarioBE.Pass =                 Convert.ToString(GridView1.Rows[x].Cells[3].Text);
                    ObjUsuarioBE.CardName =             Convert.ToString(GridView1.Rows[x].Cells[4].Text); ;
                    ObjUsuarioBE.IdPerfilUsuario =      Convert.ToInt32(GridView1.Rows[x].Cells[5].Text);
                    ObjUsuarioBE.IdArea1 =              0;//Convert.ToInt32(ddlArea1.SelectedItem.Value);
                    ObjUsuarioBE.IdArea2 =              0;//Convert.ToInt32(ddlArea2.SelectedItem.Value);
                    ObjUsuarioBE.IdArea3 =              0;//Convert.ToInt32(ddlArea3.SelectedItem.Value);
                    ObjUsuarioBE.IdArea4 =              0;//Convert.ToInt32(ddlArea4.SelectedItem.Value);
                    ObjUsuarioBE.IdArea5 =              0;//Convert.ToInt32(ddlArea5.SelectedItem.Value);
                    ObjUsuarioBE.IdCentroCostos1 =      Convert.ToInt32(GridView1.Rows[x].Cells[11].Text);
                    ObjUsuarioBE.IdCentroCostos2 =      Convert.ToInt32(GridView1.Rows[x].Cells[12].Text);
                    ObjUsuarioBE.IdCentroCostos3 =      Convert.ToInt32(GridView1.Rows[x].Cells[13].Text);
                    ObjUsuarioBE.IdCentroCostos4 =      0;
                    ObjUsuarioBE.IdCentroCostos5 =      0;
                    ObjUsuarioBE.IdCentroCostos6 =      0;
                    ObjUsuarioBE.IdCentroCostos7 =      0;
                    ObjUsuarioBE.IdCentroCostos8 =      0;
                    ObjUsuarioBE.IdCentroCostos9 =      0;
                    ObjUsuarioBE.IdCentroCostos10 =     0;
                    ObjUsuarioBE.IdCentroCostos11 =     0;
                    ObjUsuarioBE.IdCentroCostos12 =     0;
                    ObjUsuarioBE.IdCentroCostos13 =     0;
                    ObjUsuarioBE.IdCentroCostos14 =     0;
                    ObjUsuarioBE.IdCentroCostos15 =     0;
                    ObjUsuarioBE.IdUsuarioCC1 =         Convert.ToInt32(GridView1.Rows[x].Cells[14].Text);
                    ObjUsuarioBE.IdUsuarioCC2 =         Convert.ToInt32(GridView1.Rows[x].Cells[15].Text);
                    ObjUsuarioBE.IdUsuarioCC3 =         Convert.ToInt32(GridView1.Rows[x].Cells[16].Text);
                    ObjUsuarioBE.IdUsuarioER1 =         Convert.ToInt32(GridView1.Rows[x].Cells[17].Text);
                    ObjUsuarioBE.IdUsuarioER2 =         Convert.ToInt32(GridView1.Rows[x].Cells[18].Text);
                    ObjUsuarioBE.IdUsuarioER3 =         Convert.ToInt32(GridView1.Rows[x].Cells[19].Text);
                    ObjUsuarioBE.IdUsuarioRE1 =         Convert.ToInt32(GridView1.Rows[x].Cells[20].Text);
                    ObjUsuarioBE.IdUsuarioRE2 =         Convert.ToInt32(GridView1.Rows[x].Cells[21].Text);
                    ObjUsuarioBE.IdUsuarioRE3 =         Convert.ToInt32(GridView1.Rows[x].Cells[22].Text);
                    ObjUsuarioBE.CantMaxCC =            Convert.ToString(GridView1.Rows[x].Cells[6].Text);
                    ObjUsuarioBE.CantMaxER =            Convert.ToString(GridView1.Rows[x].Cells[7].Text);
                    ObjUsuarioBE.CantMaxRE =            Convert.ToString(GridView1.Rows[x].Cells[8].Text);
                    ObjUsuarioBE.Phone =                Convert.ToString(GridView1.Rows[x].Cells[9].Text);
                    ObjUsuarioBE.Mail =                 Convert.ToString(GridView1.Rows[x].Cells[10].Text);
                    ObjUsuarioBE.Comentario =           "Cargado Masivamente";

                    if (Session["Usuario"] == null)
                    {
                        Server.Transfer("~/Login.aspx");
                    }
                    else
                    {
                        UsuarioBE objUsuarioSesionBE = new UsuarioBE();
                        objUsuarioSesionBE = (UsuarioBE)Session["Usuario"];
                        ObjUsuarioBE.UserCreate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                        ObjUsuarioBE.CreateDate = DateTime.Now;
                        ObjUsuarioBE.UserUpdate = Convert.ToString(objUsuarioSesionBE.IdUsuario);
                        ObjUsuarioBE.UpdateDate = DateTime.Now;
                    }

                    int Modo = Convert.ToInt32(ViewState["Modo"].ToString());
                    int idUsuario = Convert.ToInt32(ViewState["IdUsuario"].ToString());
                    if (Modo == 1)
                    {
                        id = objUsuarioBC.InsertarUsuario(ObjUsuarioBE, 0, 0);
                        InsertarAreaNivelAprobacion(ObjUsuarioBE);
                    }
                    else
                    {
                        ObjUsuarioBE.IdUsuario = idUsuario;
                        objUsuarioBC.ModificarUsuario(ObjUsuarioBE);
                        ModificarAreaNivelAprobacion(ObjUsuarioBE);
                        EliminarUsuarioAreaNivel(ObjUsuarioBE.IdUsuario);
                    }
                }

                
            }
            else
            {
                Mensaje("Alerta: El DNI ya existe / Es necesario llenar toda la informacion / si selecciono un area asegurese de seleccionar al menos un nivel de CC o ER o RE / por Area solo puede seleccionar un nivel de CC o ER o RE");
            }
               
            //}
            //else
            //    Mensaje(mensajeError);
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Usuarios): " + ex.Message);
        }
        finally
        {
            Response.Redirect("Usuarios.aspx");
        }


    }
    protected void Cancelar4_Click(object sender, EventArgs e)
    {

        blbResultadoMasivo.Visible = false;
        blbResultadoMasivo.Text = "";
        txtCopied.Visible = false;
        txtCopied.Text = "";
        GridView1.Visible = false;
        bPreliminar4.Visible = false;
        bAgregar4.Visible = false;
        bCancelar4.Visible = false;
        bMasivo.Visible = true;

        GridView1.DataSource = null;
        GridView1.DataBind();
    }
}