using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio
{
    public class RNPartidaCompra
    {
        public int NUM_PAR { get; set; }

        public string CVE_PRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public int CANTIDAD { get; set; }
        public decimal PRECIO { get; set; }
        public decimal TOT_PARTIDA { get; set; }
        public string codigo_Compania { get; set; }
        public string codigo_Corto { get; set; }
        public string digito_Control { get; set; }
        public int contador { get; set; }
        public DateTime fecha_Caducidad { get; set; }
        public int num_Lote { get; set; }

        public static List<RNPartidaCompra> ObtenerPartidas( string cve_doc )
        {
            List<RNPartidaCompra> partidas = new List<RNPartidaCompra>();
            try
            {
                //var tabla = null;
                //if (tipo_doc == "Compra")
                //{
                //    var configuracion = RNConfiguracion.Listar().Where(x => x.SNImportadora).FirstOrDefault();
                //    RNConexion conexion = new RNConexion(configuracion.NumEmpresa);
                //    conexion.baseDatos.AbrirConexion();
                //    conexion.baseDatos.AgregarParametro("@cve_doc", cve_doc);
                //    tabla = conexion.baseDatos.ObtenerTabla("select p.NUM_PAR,p.CVE_ART,pr.DESCR ,p.CANT, p.PREC, p.TOT_PARTIDA  from PAR_COMPO{0} as P inner join INVE{0} pr on pr.CVE_ART = p.CVE_ART  where p.CVE_DOC = @cve_doc; ");
                //}
                //else if (tipo_doc == "Venta")
                //{
                //    var configuracion = RNConfiguracion.Listar().Where(x => x.SNImportadora).FirstOrDefault();
                //    RNConexion conexion = new RNConexion(configuracion.NumEmpresa);
                //    conexion.baseDatos.AbrirConexion();
                //    conexion.baseDatos.AgregarParametro("@cve_doc", cve_doc);
                //    dataGridCompras.DataSource = conexion.baseDatos.ObtenerTabla("select f.NUM_PAR ,f.CVE_ART ,pr.DESCR,f.CANT,f.PREC, f.TOT_PARTIDA from PAR_FACTP{0} f inner join INVE{0} pr on pr.CVE_ART = f.CVE_ART where p.CVE_DOC = @cve_doc; ;");
                //}
               
                var configuracion = RNConfiguracion.Listar().Where(x => x.SNImportadora).FirstOrDefault();
                RNConexion conexion = new RNConexion(configuracion.NumEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.AgregarParametro("@cve_doc", cve_doc);
                var result = conexion.baseDatos.ObtenerTabla("select p.NUM_PAR,p.CVE_ART,pr.DESCR ,p.CANT, p.PREC, p.TOT_PARTIDA,ca.CVE_ALTER      from PAR_COMPO{0} as P inner join INVE{0} pr on pr.CVE_ART = p.CVE_ART left join CVES_ALTER{0} ca on ca.CVE_ART = p.CVE_ART where p.CVE_DOC = @cve_doc; ");
                
                foreach (DataRow row in result.Rows)
                {
                    RNPartidaCompra par = new RNPartidaCompra();
                    par.NUM_PAR = Convert.ToInt32( row["NUM_PAR"].ToString());
                    par.CVE_PRODUCTO = row["CVE_ART"].ToString();
                    par.DESCRIPCION = row["DESCR"].ToString(); 
                    par.CANTIDAD = Convert.ToInt32(row["CANT"].ToString());
                    par.PRECIO = Convert.ToDecimal(row["PREC"].ToString());
                    par.TOT_PARTIDA = Convert.ToDecimal(row["TOT_PARTIDA"].ToString());
                    par.codigo_Corto = row["CVE_ALTER"].ToString();
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
