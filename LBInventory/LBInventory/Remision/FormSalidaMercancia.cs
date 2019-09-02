using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBInventory.Remision
{
    public partial class FormSalidaMercancia : Form
    {
        List<string> codigosEscaneados = new List<string>();
        private string cveOrden;
        private string cliente;
        private string importe;

        public FormSalidaMercancia()
        {
            InitializeComponent();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            // se crea un nuevo form para el form modal
            codigosEscaneados = new List<string>();
            FormOrdenes ordenes = new FormOrdenes("Ventas");
            ordenes.Enviar += new FormOrdenes.EnviarOrden(Ordenes_Enviar);
            ordenes.Owner = this;
            ordenes.ShowDialog();
        }

        private void Ordenes_Enviar(string cve)
        {
            txtOrdenVenta.Text = cve;
        }

        private void TxtOrdenVenta_TextChanged(object sender, EventArgs e)
        {
            RNVenta compra = RNVenta.ObtenerVenta(txtOrdenVenta.Text, dataGridVentas);
            lblNombre.Text = "Nombre Cliente: " + compra.NOMBRE;
            lblOrden.Text = "Orden de Compra: " + compra.CVE_DOC;
            lblRFC.Text = "Importe Total: " + compra.IMPORTE.ToString();
            cliente = compra.NOMBRE;
            cveOrden = compra.CVE_DOC;
            importe = compra.IMPORTE.ToString();
        }

        private void DataGridVentas_DataSourceChanged(object sender, EventArgs e)
        {
            btnCapturarCodigo.Enabled = true;
        }
    }
}
