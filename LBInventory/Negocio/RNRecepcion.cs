using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using System.Windows.Forms;
using System.Data;

namespace Negocio
{
    public class RNRecepcion
    {
        public string Cve_art { get; set; }
        public string Lote { get; set; }
        public int Cve_alm { get; set; }
        public DateTime fchcaduc { get; set; }
        public decimal cantidad { get; set; }
        public int reg_ltpd { get; set; }
        public int cve_obs { get; set; }
        public string status { get; set; }
        public DateTime fchultmov { get; set; }
        public string pedimento { get; set; }

        public static bool ValidarLote (string lte,int NumEmpresa)
        {
            try // regresa true si no existe false si existe
            {
                RNConexion conexion = new RNConexion(NumEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@Lote", lte);
                return conexion.baseDatos.ObtenerTabla("select lote from Ltpd{0} where lote = @Lote ").Rows.Count == 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al validar Lotes:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        public static void GenerarRecepcion(DataGridView dataGridCompras, int numEmpresa, string pedimento)
        {
            try
            {
                List<Lotes> lotes = new List<Lotes>();
                
                for (int fila = 0; fila < dataGridCompras.Rows.Count ; fila++)
                {
                    for (int col = 8; col < dataGridCompras.Rows[fila].Cells.Count; col = col +2)
                    {
                        if (lotes.Any(x=> x.lote == dataGridCompras.Rows[fila].Cells[col].Value.ToString()))
                        {
                            foreach(var l in lotes)
                            {
                                if (l.lote == dataGridCompras.Rows[fila].Cells[col].Value.ToString())
                                {
                                    l.contador++;
                                }
                            }
                        }
                        else
                        {
                            Lotes lotes1 = new Lotes();
                            lotes1.articulo = dataGridCompras.Rows[fila].Cells[6].Value.ToString();
                            lotes1.contador = 1;
                            lotes1.lote = dataGridCompras.Rows[fila].Cells[col].Value.ToString();
                            lotes1.caducidad = Convert.ToDateTime(dataGridCompras.Rows[fila].Cells[col + 1].Value.ToString());
                            lotes.Add(lotes1);
                        }                       
                    }
                    
                }
                foreach (var l in lotes)
                {
                    if (ValidarLote(l.lote, numEmpresa))
                    {
                        AgregarLote(l.lote, pedimento,l.articulo, l.caducidad, l.contador, 1, "A", DateTime.Now, numEmpresa);
                    }
                    else
                    {
                        ActualizarLote(l.lote, pedimento, l.articulo, l.caducidad, l.contador, 1, "A", DateTime.Now, numEmpresa);
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al generar la recepción:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void AgregarLote(string lote, string pedimento, string articulo, DateTime caducidad, int contador,int cve_Almacen,string status,DateTime modificacion,int NumEmpresa)
        {
            try
            {
                RNConexion conexion = new RNConexion(NumEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.LimpiarParametros();
                int indice = ObtenerIndice(48, NumEmpresa);                                         
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@articulo", articulo);
                conexion.baseDatos.AgregarParametro("@Lote", lote);
                conexion.baseDatos.AgregarParametro("@pedimento", pedimento);
                conexion.baseDatos.AgregarParametro("@reg_ltpd", indice);
                conexion.baseDatos.AgregarParametro("@cve_Almacen", cve_Almacen);
                conexion.baseDatos.AgregarParametro("@caducidad", caducidad);
                conexion.baseDatos.AgregarParametro("@modificacion", modificacion);
                conexion.baseDatos.AgregarParametro("@contador", contador);
                conexion.baseDatos.AgregarParametro("@status", status);
               var response =  conexion.baseDatos.EjecutarSinConsulta("insert into ltpd{0} (    CVE_ART ,LOTE,PEDIMENTO,CVE_ALM ,CANTIDAD,FCHCADUC ,FCHULTMOV,REG_LTPD, CVE_OBS, STATUS,PEDIMENTOSAT) " +
                    "VALUES(@articulo, @lote, @pedimento, @cve_Almacen,@contador,@caducidad, @modificacion,@reg_ltpd ,0, @status, @pedimento)") > 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al validar Lotes:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ActualizarLote(string lote, string pedimento, string articulo, DateTime caducidad, int contador,int cve_Almacen,string status,DateTime modificacion,int NumEmpresa)
        {
            try
            {
                RNConexion conexion = new RNConexion(NumEmpresa);
                conexion.baseDatos.AbrirConexion();                                    
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@articulo", articulo);
                conexion.baseDatos.AgregarParametro("@Lote", lote);
                conexion.baseDatos.AgregarParametro("@pedimento", pedimento);
                conexion.baseDatos.AgregarParametro("@contador", contador);

               var response =  conexion.baseDatos.EjecutarSinConsulta("update ltpd{0} set cantidad=(cantidad +  @contador) where Lote =@Lote and cve_art = @articulo  and pedimento =@pedimento") > 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al validar Lotes:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static int ObtenerIndice(int idtabla, int numempresa)
        {
            RNConexion conexion = new RNConexion(numempresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@idtabla", idtabla);
            var result = conexion.baseDatos.ObtenerTabla("select (ult_cve +1 ) as ult_cve from tblcontrol{0} where id_tabla = @idtabla");
            int indice = 1;
            foreach (DataRow row in result.Rows)
            {
                indice =Convert.ToInt32( row["ult_cve"].ToString());
            }
            ActualizarIndice(idtabla, numempresa, indice);
            return indice;
        }

        private static bool ActualizarIndice(int idtabla, int numEmpresa,int nuevo)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@idtabla", idtabla);
            conexion.baseDatos.AgregarParametro("@indice", nuevo);
            return conexion.baseDatos.EjecutarSinConsulta("update tblcontrol{0} set ult_cve=@indice    where id_tabla =@idtabla", CommandType.Text) > 0;
        }

        private class Lotes
            {
            public int contador { get; set; }
            public string lote { get; set; }
            public DateTime caducidad { get; set; }
            public string articulo { get; set; }
        }
    }
}
