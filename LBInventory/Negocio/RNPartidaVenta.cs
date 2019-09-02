using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio
{
    public class RNPartidaVenta
    {
        public int NUM_PAR { get; set; }
        public string CVE_PRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public int CANTIDAD { get; set; }
        public decimal PRECIO { get; set; }
        public decimal TOT_PARTIDA { get; set; }
        public string codigo_Corto { get; set; }
        public int contador { get; set; }
        public List<DateTime> fecha_Caducidad { get; set; }
        public List<string> num_Lote { get; set; }

        public static List<RNPartidaVenta> ObtenerPartidas(string cve_doc)
        {
            List<RNPartidaVenta> partidas = new List<RNPartidaVenta>();
            try
            {
                var configuracion = RNConfiguracion.Listar().Where(x => x.SNComercializadora).FirstOrDefault();
                RNConexion conexion = new RNConexion(configuracion.NumEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.AgregarParametro("@cve_doc", cve_doc);
                var result = conexion.baseDatos.ObtenerTabla("select p.NUM_PAR,p.CVE_ART,pr.DESCR ,p.CANT, p.PREC, p.TOT_PARTIDA,coalesce(ca.CVE_ALTER,p.CVE_ART ) as CVE_ALTER      from PAR_factp{0} as P inner join INVE{0} pr on pr.CVE_ART = p.CVE_ART left join CVES_ALTER{0} ca on ca.CVE_ART = p.CVE_ART where p.CVE_DOC = @cve_doc; ");

                foreach (DataRow row in result.Rows)
                {
                    RNPartidaVenta par = new RNPartidaVenta();
                    par.NUM_PAR = Convert.ToInt32(row["NUM_PAR"].ToString());
                    par.CVE_PRODUCTO = row["CVE_ART"].ToString();
                    par.DESCRIPCION = row["DESCR"].ToString();
                    par.CANTIDAD = Convert.ToInt32(row["CANT"].ToString());
                    par.PRECIO = Convert.ToDecimal(row["PREC"].ToString());
                    par.TOT_PARTIDA = Convert.ToDecimal(row["TOT_PARTIDA"].ToString());
                    par.codigo_Corto = row["CVE_ALTER"].ToString();
                    par.fecha_Caducidad = new List<DateTime>();
                    par.num_Lote = new List<string>();
                    partidas.Add(par);
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar los datos " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return partidas;
        }
    }
}
