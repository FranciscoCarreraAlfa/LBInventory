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

namespace LBInventory
{
    public partial class FormPrincipal : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hand, int wmsg, int wparam, int lparam);

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void Panel_Load(object sender, EventArgs e)
        {

        }

        private void Configuracion_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Configuracion>();
        }

        //METODO PARA ABRIR FORM DENTRO DE PANEL-----------------------------------------------------
        private void AbrirFormEnPanel<Forms>() where Forms : Form, new()
        {
            Form formulario;
            formulario = PanelContendor.Controls.OfType<Forms>().FirstOrDefault();

            //si el formulario/instancia no existe, creamos nueva instancia y mostramos
            if (formulario == null)
            {
                formulario = new Forms();
                formulario.TopLevel = false;
                formulario.Dock = DockStyle.Fill;
                PanelContendor.Controls.Add(formulario);
                PanelContendor.Tag = formulario;
                formulario.Show();

                formulario.BringToFront();
                // formulario.FormClosed += new FormClosedEventHandler(CloseForms);               
            }
            else
            {

                //si la Formulario/instancia existe, lo traemos a frente
                formulario.BringToFront();

                //Si la instancia esta minimizada mostramos
                if (formulario.WindowState == FormWindowState.Minimized)
                {
                    formulario.WindowState = FormWindowState.Normal;
                }

            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            PanelConf.Width = PanelConf.Width == 250? 40: 250;
            pictureBox3.Visible = !pictureBox3.Visible;

        }
        int lx, ly, sh, sw;
        private void Maximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            restaurar.Visible = true;
            Maximizar.Visible = false;
        }

        private void Restaurar_Click(object sender, EventArgs e)
        {
            this.Size = new Size(sw,sh);
            this.Location = new Point(lx, ly);
            restaurar.Visible = false;
            Maximizar.Visible = true;
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            Sesion.Cerrar();
            this.Hide();

            FormLogin frm = new FormLogin();

            frm.Show();

        }

        private void Minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
