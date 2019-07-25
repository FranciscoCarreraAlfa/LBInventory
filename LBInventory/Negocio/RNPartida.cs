using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio
{
    public class RNPartida
    {
        public static void ObtenerPartidas(DataGridView dataGridCompras, string cve_doc, string tipo_doc )
        {
            try
            {
                if (tipo_doc == "Compra")
                {
                    var configuracion = RNConfiguracion.Listar().Where(x => x.SNImportadora).FirstOrDefault();
                    RNConexion conexion = new RNConexion(configuracion.NumEmpresa);
                    conexion.baseDatos.AbrirConexion();
                    conexion.baseDatos.AgregarParametro("@cve_doc", cve_doc);
                    dataGridCompras.DataSource = conexion.baseDatos.ObtenerTabla("select p.CVE_DOC, p.NUM_PAR,pr.DESCR ,p.CANT, p.PXR, p.PREC, p.COST, p.DESCU, p.NUM_ALM, p.TOT_PARTIDA  from PAR_COMPO{0} as P inner join INVE{0} pr on pr.CVE_ART = p.CVE_ART  where p.CVE_DOC = @cve_doc; ");
                }
                else if (tipo_doc == "Venta")
                {
                    var configuracion = RNConfiguracion.Listar().Where(x => x.SNImportadora).FirstOrDefault();
                    RNConexion conexion = new RNConexion(configuracion.NumEmpresa);
                    conexion.baseDatos.AbrirConexion();
                    conexion.baseDatos.AgregarParametro("@cve_doc", cve_doc);
                    dataGridCompras.DataSource = conexion.baseDatos.ObtenerTabla("select f.CVE_DOC, f.NUM_PAR , pr.DESCR,f.CANT,f.PXS,f.PREC, f.COST, f.UNI_VENTA, f.TOT_PARTIDA from PAR_FACTP{0} f inner join INVE{0} pr on pr.CVE_ART = f.CVE_ART where p.CVE_DOC = @cve_doc; ;");
                }
                    

               

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar los datos " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
