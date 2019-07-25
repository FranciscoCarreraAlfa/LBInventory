using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio
{
    public class RNCompra
    {

        public static void ObtenerCompras(DataGridView dataGridCompras)
        {
            try
            {
                var configuracion = RNConfiguracion.Listar().Where(x=> x.SNImportadora).FirstOrDefault();
                RNConexion conexion = new RNConexion(configuracion.NumEmpresa);
                conexion.baseDatos.AbrirConexion();
                dataGridCompras.DataSource=conexion.baseDatos.ObtenerTabla("select c.CVE_DOC,p.CLAVE as Cve_Proveedor, p.NOMBRE as Nom_Proveedor , c.STATUS, c.SU_REFER, c.IMPORTE, m.DESCR as MONEDA "
                    + " from compo{0} as c inner join  PROV{0} p on p.CLAVE = c.CVE_CLPV inner join MONED{0} m on m.NUM_MONED = c.NUM_MONED "
                    + " where c.STATUS <> 'C' and(c.TIP_DOC_SIG is null or c.TIP_DOC_SIG = ''); ");
            }
            catch(Exception e)
            {
                MessageBox.Show("Error al cargar los datos "+e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
