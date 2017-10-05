using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Prueba : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void PasteToGridView(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
            //            new DataColumn("Name", typeof(string)),
            //            new DataColumn("Country",typeof(string)) });
            dt.Columns.AddRange(new DataColumn[14] { 
                    new DataColumn("Tipo_Documento", typeof(int)),
                    new DataColumn("Serie", typeof(string)),
                    new DataColumn("Numero",typeof(int)),
                    new DataColumn("Fecha",typeof(DateTime)),
                    new DataColumn("Ruc",typeof(string)),
                    new DataColumn("Razon_Social",typeof(string)),
                    new DataColumn("Concepto",typeof(int)),
                    new DataColumn("Moneda_Documento",typeof(int)),
                    new DataColumn("Tasa_Cambio",typeof(decimal)),
                    new DataColumn("No_Afecta",typeof(decimal)),
                    new DataColumn("Afecta",typeof(decimal)),
                    new DataColumn("IGV",typeof(decimal)),
                    new DataColumn("Total_Documento",typeof(decimal)),
                    new DataColumn("Total_Moneda_Origen",typeof(decimal))  });

            string copiedContent = Request.Form[txtCopied.UniqueID];
            foreach (string row in copiedContent.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split('\t'))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            txtCopied.Text = "";
        }
        catch (Exception ex)
        {
            Mensaje("Ocurrió un error (Prueba): " + ex.Message);
        }
    }

    private void Mensaje(String mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "MessageBox", "alert('" + mensaje + "')", true);
    }
}