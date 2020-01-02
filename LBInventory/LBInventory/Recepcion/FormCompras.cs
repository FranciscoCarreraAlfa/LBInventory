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
using ClosedXML.Excel;
using System.Diagnostics;

namespace LBInventory
{
    public partial class FormCompras : Form
    {
        List<string> codigosEscaneados = new List<string>();
        private string cveOrden;
        private string proveedor;
        private string importe;
        private RNPedimento pedimento;
        
        public FormCompras()
        {
            InitializeComponent();
            this.txtOrdenCompra.Focus();
            
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
            codigosEscaneados = new List<string>();
            FormOrdenes ordenes = new FormOrdenes("Compras") ;
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
            proveedor =compra.NOMBRE;
            cveOrden =compra.CVE_DOC;
            importe = compra.IMPORTE.ToString();
        }

        private void DataGridCompras_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void CapturarCodigos_Click(object sender, EventArgs e)
        {
            bool msgVacio = false;
            foreach (DataGridViewRow dgvRenglon in dataGridCompras.Rows)
            {
                if (string.IsNullOrEmpty(dgvRenglon.Cells[6].Value.ToString()))
                {
                    msgVacio = true;
                }
            }
            if (msgVacio)
            {
                MessageBox.Show("Existen Productos sin codigo corto, valida por favor tu información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FormCapturacodigo captura = new FormCapturacodigo(codigosEscaneados) { DataGrid = dataGridCompras};
                captura.Enviar += new FormCapturacodigo.ActualizarPartida(Partidas_Enviar);
                captura.Owner = this;
                captura.ShowDialog();
                btnReporte.Enabled = true;
                btnGenTRecepcion.Enabled = true;
            }
        }
        private void Partidas_Enviar(DataGridView data, List<string> listacodigos)
        {
            dataGridCompras = data;
            codigosEscaneados = listacodigos;
        }

        private void DataGridCompras_DataSourceChanged(object sender, EventArgs e)
        {
            btnCapturarCodigo.Enabled = true;
        }

        private void BtnReporte_Click(object sender, EventArgs e)
        {
            var fileName = System.IO.Path.GetTempPath() + @"\ReportedeIncidenciasCompras" + DateTime.Now.ToString("ddMMyyhhmmss") + ".xlsx";
            RNReporteCompra.Reporte(dataGridCompras, codigosEscaneados, proveedor,cveOrden,importe).SaveAs(fileName);
            Process.Start(fileName);
        }

        private void BtnGenTRecepcion_Click(object sender, EventArgs e)
        {
            bool msgVacio = false;
            foreach (DataGridViewRow dgvRenglon in dataGridCompras.Rows)
            {
                if (dgvRenglon.Cells[3].Value.ToString()!=dgvRenglon.Cells[7].Value.ToString())
                {
                    msgVacio = true;
                }
            }
            if (msgVacio)
            {
                MessageBox.Show("Error al validar las existencias, valida por favor tu información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // cambiar por una pantalla mas acorde con el desarrollo
                //  se crea un nuevo form para el form modal
                FormPedimento pedimento = new FormPedimento();
                pedimento.Enviar += new FormPedimento.EnviarPedimento(Pedimento_Enviar);
                pedimento.Owner = this;
                pedimento.ShowDialog();
                // obtener el numero de la empresa importadora;
                int nunEmpresa = RNLbInventory.ObtenerImportadora().NumEmpresa;
                RNRecepcion.GenerarRecepcion(dataGridCompras, cveOrden, nunEmpresa, this.pedimento);
                ClearForm();
            }



        }

        private void Pedimento_Enviar(RNPedimento cve)
        {
            pedimento = cve;
        }

        private void ClearForm()
        {
            txtOrdenCompra.Text = "";
            dataGridCompras.DataSource = null;
            lblNombre.Text = "Nombre Proveedor: ";
            lblOrden.Text = "Orden de Compra: ";
            lblRFC.Text = "Importe Total: " ;
            btnGenTRecepcion.Enabled = false;
            btnReporte.Enabled = false;
        }


    }

}
