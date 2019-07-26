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

namespace LBInventory
{
    public partial class FormOrdenes : Form
    {
        public delegate void EnviarOrden(string cve);
        public event EnviarOrden Enviar;

        public FormOrdenes()
        {
            InitializeComponent();
        }

        private void FormPartidas_Load(object sender, EventArgs e)
        {
            RNCompra.ObtenerCompras(dataGridOrdenes);
            lblPartida.Text = "Compras";
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridPartida_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Enviar(dataGridOrdenes.CurrentCell.Value.ToString());
            this.Close();
        }
    }
}
