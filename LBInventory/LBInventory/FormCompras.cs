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
            RNCompra.ObtenerCompras(dataGridCompras);
        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FormPartidas partidas = new FormPartidas() { tipo_doc = "Compra", cve_doc = dataGridCompras.CurrentCell.Value.ToString() };
        }

        private void DataGridCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FormPartidas partidas = new FormPartidas() { tipo_doc = "Compra", cve_doc = dataGridCompras.CurrentCell.Value.ToString() };
            partidas.Owner = this;
            partidas.ShowDialog();
        }
    }
}
