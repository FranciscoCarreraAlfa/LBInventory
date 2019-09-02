using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio
{
    public class RNVenta
    {
        public string CVE_DOC { get; set; }
        public string CLAVE { get; set; }
        public string NOMBRE { get; set; }
        public string STATUS { get; set; }
        public string SU_REFER { get; set; }
        public decimal IMPORTE { get; set; }
        public string MONEDA { get; set; }

        public static void ObtenerVentas(DataGridView dataGridCompras)
        {
            try
            {
                var configuracion = RNConfiguracion.Listar().Where(x => x.SNComercializadora).FirstOrDefault();
                RNConexion conexion = new RNConexion(configuracion.NumEmpresa);
                conexion.baseDatos.AbrirConexion();
                dataGridCompras.DataSource = conexion.baseDatos.ObtenerTabla("select c.CVE_DOC,p.CLAVE as Cve_Cliente, p.NOMBRE as Nom_Cliente ,  c.IMPORTE , m.DESCR as MONEDA"
                    + " from factp{0} as c inner join  clie{0} p on p.CLAVE = c.CVE_CLPV inner join MONED{0} m on m.NUM_MONED = c.NUM_MONED "
                    + " where c.STATUS <> 'C' and(c.TIP_DOC_SIG is null or c.TIP_DOC_SIG = ''); ");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar los datos " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static RNVenta ObtenerVenta(string cve, DataGridView dataGridCompras)
        {
            RNVenta venta = new RNVenta();
            try
            {

                var configuracion = RNConfiguracion.Listar().Where(x => x.SNComercializadora).FirstOrDefault();
                RNConexion conexion = new RNConexion(configuracion.NumEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.AgregarParametro("@cve", cve);
                var result = conexion.baseDatos.ObtenerTabla("select c.CVE_DOC,p.CLAVE , p.NOMBRE ,c.IMPORTE, m.DESCR as MONEDA "
                    + " from factp{0} as c inner join  clie{0} p on p.CLAVE = c.CVE_CLPV inner join MONED{0} m on m.NUM_MONED = c.NUM_MONED "
                    + " where c.CVE_DOC = @cve; ");

                foreach (DataRow row in result.Rows)
                {
                    venta.CVE_DOC = row["CVE_DOC"].ToString();
                    venta.CLAVE = row["CLAVE"].ToString();
                    venta.IMPORTE = Convert.ToDecimal(row["IMPORTE"].ToString());
                    venta.MONEDA = row["MONEDA"].ToString();
                    venta.NOMBRE = row["NOMBRE"].ToString();
                }

                for (int i = dataGridCompras.Columns.Count - 1; i > 7; i--)
                {
                    if (i > 7)
                        dataGridCompras.Columns.Remove(dataGridCompras.Columns[i]);
                }
                dataGridCompras.DataSource = RNPartidaVenta.ObtenerPartidas(cve);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar los datos " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return venta;
        }

    }
}
