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
    public partial class FormConfiguracion : Form
    {
        public FormConfiguracion()
        {
            InitializeComponent();
        }

        private void Configuracion_Load(object sender, EventArgs e)
        {
            var configuraciones = RNConfiguracion.Listar();
            foreach(var c in configuraciones)
            {
                EmpComer.Items.Add(c.Empresa,c.SNComercializadora );
                EmpImpor.Items.Add(Text = c.Empresa,c.SNImportadora);
            }
        }

        private void EmpComer_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                if (EmpComer.CheckedItems.Count >= 1)
                {
                    for(int i=0;i< EmpComer.Items.Count;i++)
                    {
                        EmpComer.SetItemChecked(i, false);
                    }

                }
            }
                
        }
        private void EmpImpor_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                if (EmpImpor.CheckedItems.Count >= 1)
                {
                    for(int i=0;i< EmpImpor.Items.Count;i++)
                    {
                        EmpImpor.SetItemChecked(i, false);
                    }

                }
            }
                
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (EmpComer.SelectedItem == EmpImpor.SelectedItem && EmpComer.SelectedItem != null && EmpImpor.SelectedItem != null)
            {
                MessageBox.Show("No puedes selecionar la misma empresa comercializadora como importadora", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (EmpComer.SelectedItems.Count >= 1&& EmpImpor.SelectedItems.Count >= 1)
            {
                var banComer = RNConfiguracion.GuardarComercializadora(EmpComer.SelectedItems[0].ToString(), true, out string msgcomer);
                var banImpor = RNConfiguracion.GuardarImportadora(EmpImpor.SelectedItems[0].ToString(), true, out string msgimpor);
                if (banComer && banImpor)
                {
                    MessageBox.Show("Cambios guardados con exito", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!banComer)
                {
                    MessageBox.Show("Error al guardar Comercializadora: "+ msgcomer, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!banImpor)
                {
                    MessageBox.Show("Error al guardar Importadora"+ msgimpor, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }else if (EmpComer.SelectedItem == null || EmpImpor.SelectedItem == null)// --- Falta hacer validaciones
            {
                
            }
        }

        private void PictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
