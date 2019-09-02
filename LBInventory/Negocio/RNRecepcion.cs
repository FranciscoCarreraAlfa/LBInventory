using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using System.Data;
using Umbrella.Aspel.Sae.Datos;

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

        public static bool ValidarLote (string lte,string pedimento,int NumEmpresa)
        {
            try // regresa true si no existe false si existe
            {
                RNConexion conexion = new RNConexion(NumEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@Lote", lte);
                conexion.baseDatos.AgregarParametro("@pedimento", pedimento);
                return conexion.baseDatos.ObtenerTabla("select lote from Ltpd{0} where lote = @Lote and pedimento = @pedimento").Rows.Count == 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al validar Lotes:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        public static void GenerarRecepcion(DataGridView dataGridCompras,string cveDoc ,int numEmpresa, RNPedimento pedimento)
        {
            try
            {
                List<Lotes> lotes = new List<Lotes>();
                // se obtiene el folio y la clave de la Recepción
                int folio = 0;
                string cveDocRecpcion = GenerarRecepcion("r",null, numEmpresa, out folio);
                List<RNPartidasRecepcion> partidas = new List<RNPartidasRecepcion>();
                // se crea el documento de recepción y sus partidas
                //registrar cabecera y patidas
                RNOrdenRecepcion recepcion = new RNOrdenRecepcion().GenerarRecepcion(cveDoc,cveDocRecpcion,folio,numEmpresa,out partidas);
                

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
                            lotes1.partida = Convert.ToInt32(dataGridCompras.Rows[fila].Cells[0].Value.ToString());
                            lotes.Add(lotes1);
                        }                       
                    }                    
                }
                foreach (var l in lotes)
                {
                    if (ValidarLote(l.lote,pedimento.numPedimento ,numEmpresa))
                    {
                        l.reg_ltpd = AgregarLote(l.lote, pedimento,l.articulo, l.caducidad, l.contador, 1, "A", DateTime.Now, numEmpresa);
                        l.reg_ltpd_Enlace = RNLbInventory.RegistrarEnlace(l.reg_ltpd,l.contador,numEmpresa);
                        l.minve_id = RNLbInventory.RegistrarMovInveRecepcion(partidas.Where(x => x.NUM_PAR == l.partida).FirstOrDefault(), recepcion, l.reg_ltpd_Enlace,1 ,numEmpresa);
                    }
                    else
                    {
                        l.reg_ltpd = ActualizarLote(l.lote, pedimento.numPedimento, l.articulo, l.caducidad, l.contador, 1, "A", DateTime.Now, numEmpresa);
                        l.reg_ltpd_Enlace = RNLbInventory.RegistrarEnlace(l.reg_ltpd, l.contador, numEmpresa);
                        l.minve_id = RNLbInventory.RegistrarMovInveRecepcion(partidas.Where(x=> x.NUM_PAR == l.partida).FirstOrDefault(), recepcion, l.reg_ltpd_Enlace, 1,numEmpresa);
                    }

                    RNPartidasRecepcion.ActualizarPartida(recepcion.CVE_DOC,l.partida, l.reg_ltpd_Enlace, l.minve_id, numEmpresa);
                    RNLbInventory.ActualizarExistencias(partidas.Where(x => x.NUM_PAR == l.partida).FirstOrDefault().CVE_ART, recepcion.NUM_ALMA, 1, partidas.Where(x => x.NUM_PAR == l.partida).FirstOrDefault().CANT, numEmpresa);
                }
                if (RNLbInventory.ObtenerImportadora().NumEmpresa == numEmpresa)
                {
                    RNRemision.GenerarRemision(recepcion, partidas, lotes);
                    GenerarRecepcionComercializadora(recepcion,partidas,lotes,pedimento);
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al generar la recepción:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public static void GenerarRecepcionComercializadora(RNOrdenRecepcion recepcion,List<RNPartidasRecepcion> partidas,List<Lotes> lotes,RNPedimento pedimento)
        {
            int numEmpresa = RNLbInventory.ObtenerComercializadora().NumEmpresa;
            int folio = 0;
            string cveDocRecpcion = GenerarRecepcion("r", null, numEmpresa, out folio);
            RNOrdenRecepcion recepcioncomercializadora = new RNOrdenRecepcion().GenerarRecepcion(recepcion, partidas ,cveDocRecpcion, folio, numEmpresa,lotes);

        }


        public static string GenerarRecepcion(string tipoDoc,string serie,int numEmpresa, out int folio)
        {
            serie = string.IsNullOrEmpty(serie) ? "STAND." : serie;
            string cveDoc = RNLbInventory.ObtenerCveDoc(tipoDoc,serie,numEmpresa,"C",out folio);
            return cveDoc;
        }



        public static int AgregarLote(string lote, RNPedimento pedimento, string articulo, DateTime caducidad, int contador,int cve_Almacen,string status,DateTime modificacion,int NumEmpresa)
        {
            try
            {
                RNConexion conexion = new RNConexion(NumEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.LimpiarParametros();
                int indice = RNLbInventory.ObtenerIndice(48, NumEmpresa);                                         
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@articulo", articulo);
                conexion.baseDatos.AgregarParametro("@Lote", lote);
                conexion.baseDatos.AgregarParametro("@pedimento", pedimento.numPedimento);
                conexion.baseDatos.AgregarParametro("@NOM_ADUAN", pedimento.Aduana);
                conexion.baseDatos.AgregarParametro("@CIUDAD", pedimento.Ciudad);
                conexion.baseDatos.AgregarParametro("@FRONTERA", pedimento.Frontera);
                conexion.baseDatos.AgregarParametro("@GLN", pedimento.GLN);
                conexion.baseDatos.AgregarParametro("@FCHADUANA", pedimento.Fecha);
                conexion.baseDatos.AgregarParametro("@reg_ltpd", indice);
                conexion.baseDatos.AgregarParametro("@cve_Almacen", cve_Almacen);
                conexion.baseDatos.AgregarParametro("@caducidad", caducidad);
                conexion.baseDatos.AgregarParametro("@modificacion", modificacion);
                conexion.baseDatos.AgregarParametro("@contador", contador);
                conexion.baseDatos.AgregarParametro("@status", status);
               var response =  conexion.baseDatos.EjecutarSinConsulta("insert into ltpd{0} (    CVE_ART ,LOTE,PEDIMENTO,CVE_ALM ,CANTIDAD,FCHCADUC ,FCHULTMOV,REG_LTPD, CVE_OBS, STATUS,PEDIMENTOSAT,NOM_ADUAN,CIUDAD,FRONTERA,GLN,FCHADUANA) " +
                    "VALUES(@articulo, @lote, @pedimento, @cve_Almacen,@contador,@caducidad, @modificacion,@reg_ltpd ,0, @status, @pedimento,@NOM_ADUAN,@CIUDAD,@FRONTERA,@GLN,@FCHADUANA)") > 0;
                return indice;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al validar Lotes:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public static int ActualizarLote(string lote, string pedimento, string articulo, DateTime caducidad, int contador,int cve_Almacen,string status,DateTime modificacion,int NumEmpresa)
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
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@articulo", articulo);
                conexion.baseDatos.AgregarParametro("@Lote", lote);
                conexion.baseDatos.AgregarParametro("@pedimento", pedimento);
               return conexion.baseDatos.EjecutarEscalar("select coalesce(reg_ltpd,0) as reg_ltpd from Ltpd{0}  where Lote =@Lote and cve_art = @articulo  and pedimento =@pedimento").ToInt32().Value;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al validar Lotes:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }


        public static int ObtenerExistencias(string CVE_ART,int NUM_ALM,int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@CVE_ART", CVE_ART);
            conexion.baseDatos.AgregarParametro("@NUM_ALM", NUM_ALM);
            var result = conexion.baseDatos.ObtenerTabla("select Exist from MULT{0} where CVE_ART = @CVE_ART and CVE_ALM=@NUM_ALM");
            int indice = 1;
            foreach (DataRow row in result.Rows)
            {
                indice =Convert.ToInt32( row["Exist"].ToString());
            }
            return indice;
        }

        public class Lotes
            {
            public int contador { get; set; }
            public string lote { get; set; }
            public DateTime caducidad { get; set; }
            public string articulo { get; set; }
            public int partida { get; set; }
            public int reg_ltpd_Enlace { get; set; }
            public int minve_id { get; set; }
            public int reg_ltpd { get; set; }
        }
    }

    
}
