using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio
{
    public class RNCompra
    {
        public string CVE_DOC { get; set; }
        public string CLAVE { get; set; }
        public string NOMBRE { get; set; }
        public string STATUS { get; set; }
        public string SU_REFER { get; set; }
        public decimal IMPORTE { get; set; }
        public string MONEDA { get; set; }

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

        public static RNCompra ObtenerCompra(string cve, DataGridView dataGridCompras)
        {
            RNCompra compra = new RNCompra();
            try
            {
                
                var configuracion = RNConfiguracion.Listar().Where(x => x.SNImportadora).FirstOrDefault();
                RNConexion conexion = new RNConexion(configuracion.NumEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.AgregarParametro("@cve",cve);
                var result = conexion.baseDatos.ObtenerTabla("select c.CVE_DOC,p.CLAVE , p.NOMBRE , c.STATUS, c.SU_REFER, c.IMPORTE, m.DESCR as MONEDA "
                    + " from compo{0} as c inner join  PROV{0} p on p.CLAVE = c.CVE_CLPV inner join MONED{0} m on m.NUM_MONED = c.NUM_MONED "
                    + " where c.CVE_DOC = @cve; ");

                foreach (DataRow row in result.Rows)
                {
                    compra.CVE_DOC = row["CVE_DOC"].ToString();
                    compra.CLAVE = row["CLAVE"].ToString();
                    compra.IMPORTE =Convert.ToDecimal( row["IMPORTE"].ToString());
                    compra.MONEDA = row["MONEDA"].ToString();
                    compra.NOMBRE = row["NOMBRE"].ToString();
                    compra.STATUS = row["STATUS"].ToString();
                    compra.SU_REFER = row["SU_REFER"].ToString();
                }

                dataGridCompras.DataSource = RNPartidaCompra.ObtenerPartidas(cve);



            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar los datos " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return compra;
        }

    }
}
