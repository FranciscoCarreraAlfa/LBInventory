using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Negocio;

namespace LBInventory
{
    public partial class FormPedimento : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hand, int wmsg, int wparam, int lparam);
        public delegate void EnviarPedimento(RNPedimento cve);
        public event EnviarPedimento Enviar;

        public FormPedimento()
        {
            InitializeComponent();
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            RNPedimento pe = new RNPedimento();
            pe.numPedimento = txtPedimento.Text;
            pe.Aduana = txtAduana.Text;
            pe.Fecha = txtFecha.Text;
            pe.Ciudad = txtCiudad.Text;
            pe.Frontera = txtFrontera.Text;
            pe.GLN = txtGln.Text;
            Enviar(pe);
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
