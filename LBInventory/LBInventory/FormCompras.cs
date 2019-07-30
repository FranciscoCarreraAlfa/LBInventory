using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Datos;

namespace LBInventory
{
    public partial class FormCompras : Form
    {
        public FormCompras()
        {
            InitializeComponent();
        }
        private void FormCompras_Load(object sender, EventArgs e)
        {
        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DataGridCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            // se crea un nuevo form para el form modal
            FormOrdenes ordenes = new FormOrdenes() ;
            ordenes.Enviar += new FormOrdenes.EnviarOrden(Ordenes_Enviar);
            ordenes.Owner = this;
            ordenes.ShowDialog();
        }

        private void Ordenes_Enviar(string cve)
        {
            txtOrdenCompra.Text = cve;
        }

        private void TxtOrdenCompra_TextChanged(object sender, EventArgs e)
        {
            RNCompra compra = RNCompra.ObtenerCompra(txtOrdenCompra.Text, dataGridCompras);
            lblNombre.Text ="Nombre Proveedor: "+ compra.NOMBRE;
            lblOrden.Text = "Orden de Compra: "+compra.CVE_DOC;
            lblRFC.Text = "Importe Total: "+compra.IMPORTE.ToString();
        }

        private void DataGridCompras_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void CapturarCodigos_Click(object sender, EventArgs e)
        {
            FormCapturacodigo captura = new FormCapturacodigo() { DataGrid = dataGridCompras };
            captura.Enviar += new FormCapturacodigo.ActualizarPartida(Partidas_Enviar); 
            captura.Owner = this;
            captura.ShowDialog();
        }
        private void Partidas_Enviar(DataGridView data)
        {
            dataGridCompras = data;
        }

        private void DataGridCompras_DataSourceChanged(object sender, EventArgs e)
        {
            btnCapturarCodigo.Enabled = true;
        }
    }
}
