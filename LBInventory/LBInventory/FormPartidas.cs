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
    public partial class FormPartidas : Form
    {
        public FormPartidas()
        {
            InitializeComponent();
        }
        public string cve_doc;
        public string tipo_doc;

        private void FormPartidas_Load(object sender, EventArgs e)
        {
            RNPartida.ObtenerPartidas(dataGridPartida,cve_doc, tipo_doc);
            lblPartida.Text = "Partidas "+tipo_doc+" documento "+cve_doc;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
