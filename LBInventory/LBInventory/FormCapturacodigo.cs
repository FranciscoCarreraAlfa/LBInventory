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

namespace LBInventory
{
    public partial class FormCapturacodigo : Form
    {
        public delegate void ActualizarPartida(DataGridView data);
        public event ActualizarPartida Enviar;
        public DataGridView DataGrid;
        public FormCapturacodigo()
        {
            InitializeComponent();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {
                if (listCodigos.FindString(txtCaptura.Text)== -1)
                {
                    listCodigos.Items.Add(txtCaptura.Text);
                }
                txtCaptura.Text = "";
                txtCaptura.Focus();
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCapturacodigo_Load(object sender, EventArgs e)
        {
            txtCaptura.Focus();
        }

        private void BtnValidar_Click(object sender, EventArgs e)
        {
            var codigos = RNCodigo.Desfracmentar(listCodigos);
            foreach (var c in codigos)
            {
                foreach(DataGridViewRow dgvRenglon in DataGrid.Rows)
                {
                    string celda7 = dgvRenglon.Cells[7].Value.ToString();
                    if (c.producto == dgvRenglon.Cells[7].Value.ToString() && !String.IsNullOrEmpty( dgvRenglon.Cells[7].Value.ToString()))
                    {
                        dgvRenglon.Cells[6].Value = c.compañia;
                        dgvRenglon.Cells[8].Value = c.control;
                        dgvRenglon.Cells[9].Value = c.contador;
                        dgvRenglon.Cells[10].Value = c.caducidad;
                        dgvRenglon.Cells[11].Value = c.lote;

                    }
                }
            }

            Enviar(DataGrid);
            this.Close();
        }
    }
}
