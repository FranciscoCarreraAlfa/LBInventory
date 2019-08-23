using Negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LBInventory
{
    public partial class FormCapturacodigo : Form
    {
        public delegate void ActualizarPartida(DataGridView data, List<string> list);
        public event ActualizarPartida Enviar;
        public DataGridView DataGrid;
        private int codigoCapturados;
        public FormCapturacodigo(List<string> listacodigos)
        {
            InitializeComponent();
            this.txtCaptura.Focus();
            if (listacodigos != null)
            {
                foreach (string codi in listacodigos)
                {
                    this.listCodigos.Items.Add(codi);
                }
            }
            codigoCapturados = listacodigos.Count;
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {
                if (listCodigos.FindString(txtCaptura.Text) == -1)
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

        }

        private void BtnValidar_Click(object sender, EventArgs e)
        {

            var codigos = RNCodigo.Desfracmentar(listCodigos, codigoCapturados);
            foreach (DataGridViewRow dgvRenglon in DataGrid.Rows)
            {
                foreach (var c in codigos)
                {
                    if (c.producto.Contains(dgvRenglon.Cells[6].Value.ToString()) && !String.IsNullOrEmpty(dgvRenglon.Cells[6].Value.ToString()))
                    {
                        // contador de productos escaneados
                        int contador = Convert.ToInt32(dgvRenglon.Cells[7].Value) + 1;
                        dgvRenglon.Cells[7].Value = contador;
                        // validar si el tamaño de las columnas es igual a 7 +2
                        if (DataGrid.Columns.Count + 2 == (8 + (contador * 2)))
                        {
                            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
                            column1.HeaderText = "lote" + contador;
                            column1.Width = 200;
                            column1.Visible = false;
                            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                            column.HeaderText = "caducidad" + contador;
                            column.Width = 200;
                            column.Visible = false;
                            DataGrid.Columns.Add(column1);
                            DataGrid.Columns.Add(column);

                        }
                        dgvRenglon.Cells[7 + (contador * 2) - 1].Value = c.lote;
                        dgvRenglon.Cells[7 + (contador * 2)].Value = c.caducidad;

                    }
                }
            }
            List<string> codigosLeidos = new List<string>();
            foreach (var lc in listCodigos.Items)
            {
                codigosLeidos.Add(lc.ToString());
            }

            Enviar(DataGrid, codigosLeidos);
            this.Close();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
