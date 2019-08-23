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
                int reg_ltpd = 0;
                foreach (var l in lotes)
                {
                    if (ValidarLote(l.lote, numEmpresa))
                    {
                        reg_ltpd = AgregarLote(l.lote, pedimento,l.articulo, l.caducidad, l.contador, 1, "A", DateTime.Now, numEmpresa);
                        l.reg_ltpd_Enlace = RNOrdenRecepcion.RegistrarEnlace(reg_ltpd,l.contador,numEmpresa);
                        l.minve_id = RNOrdenRecepcion.RegistrarMovInve(partidas.Where(x => x.NUM_PAR == l.partida).FirstOrDefault(), recepcion, l.reg_ltpd_Enlace, numEmpresa);
                    }
                    else
                    {
                        reg_ltpd = ActualizarLote(l.lote, pedimento.numPedimento, l.articulo, l.caducidad, l.contador, 1, "A", DateTime.Now, numEmpresa);
                        l.reg_ltpd_Enlace =RNOrdenRecepcion.RegistrarEnlace(reg_ltpd, l.contador, numEmpresa);
                        l.minve_id = RNOrdenRecepcion.RegistrarMovInve(partidas.Where(x=> x.NUM_PAR == l.partida).FirstOrDefault(), recepcion, l.reg_ltpd_Enlace, numEmpresa);
                    }

                    RNPartidasRecepcion.ActualizarPartida(recepcion.CVE_DOC,l.partida, l.reg_ltpd_Enlace, l.minve_id, numEmpresa);
                    ActualizarExistencias(partidas.Where(x => x.NUM_PAR == l.partida).FirstOrDefault().CVE_ART, recepcion.NUM_ALMA, partidas.Where(x => x.NUM_PAR == l.partida).FirstOrDefault().CANT, numEmpresa);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al generar la recepción:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string GenerarRecepcion(string tipoDoc,string serie,int numEmpresa, out int folio)
        {
            serie = string.IsNullOrEmpty(serie) ? "STAND." : serie;
            string cveDoc = ObtenerCveDoc(tipoDoc,serie,numEmpresa,out folio);
            return cveDoc;
        }

        public static string ObtenerCveDoc(string tipoDoc,string serie,int numEmpresa,out int folio)
        {
            folio = 1;
            string aux = "";
            try
            {
                RNConexion conexion = new RNConexion(numEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@tipoDoc", tipoDoc);
                conexion.baseDatos.AgregarParametro("@serie", serie);
                var result = conexion.baseDatos.ObtenerTabla("SELECT ULT_DOC+1 as ULT_DOC FROM FOLIOSC{0} WHERE TIP_DOC =@tipoDoc AND SERIE = @serie AND FOLIODESDE = 1");
                foreach (DataRow row in result.Rows)
                {
                    folio = Convert.ToInt32(row["ULT_DOC"].ToString());
                }
                ActualizarFolio(tipoDoc, serie, folio, numEmpresa);
                string mascara = ObtenerMascara(tipoDoc, serie, numEmpresa);
                aux = mascara.Insert(10 - (folio.ToString().Length), folio.ToString());
                aux = aux.Substring(0,10);
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al generar la recepción:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "          "+aux;
        }

        public static bool ActualizarFolio(string tipoDoc, string serie,int ultimoId,int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@tipoDoc", tipoDoc);
            conexion.baseDatos.AgregarParametro("@serie", serie);
            conexion.baseDatos.AgregarParametro("@LAST_ID_DATE", DateTime.Now);
            conexion.baseDatos.AgregarParametro("@LAST_ID", ultimoId);
            return conexion.baseDatos.EjecutarSinConsulta(" UPDATE FOLIOSC{0} SET ULT_DOC = (CASE WHEN ULT_DOC < @LAST_ID THEN @LAST_ID ELSE ULT_DOC END)," +
            "FECH_ULT_DOC = @LAST_ID_DATE WHERE TIP_DOC = @tipoDoc AND SERIE = @serie AND FOLIODESDE = 1; ", CommandType.Text) > 0;
        }
        public static string ObtenerMascara(string tipoDoc, string serie, int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@tipoDoc", tipoDoc.ToUpper());
            conexion.baseDatos.AgregarParametro("@serie", serie);
            var result = conexion.baseDatos.ObtenerTabla(" SELECT CASE MASCARA WHEN '0' THEN '          ' WHEN '1' THEN '0000000000' WHEN '2' THEN '' ELSE '' END"+
                            " FROM PARAM_FOLIOSC{0} WHERE TIPODOCTO = @tipoDoc AND SERIE = @serie ; ");
            string mascara = "";
            foreach (DataRow row in result.Rows)
            {
                mascara = row["CASE"].ToString();
            }
            return mascara;
        }

        public static int AgregarLote(string lote, RNPredimento pedimento, string articulo, DateTime caducidad, int contador,int cve_Almacen,string status,DateTime modificacion,int NumEmpresa)
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
                    "VALUES(@articulo, @lote, @pedimento, @cve_Almacen,@contador,@caducidad, @modificacion,@reg_ltpd ,0, @status, @pedimento,@NOM_ADUAN,@CIUDAD,@FRONTERA,@GLN,FCHADUANA)") > 0;
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

        public static int ObtenerIndice(int idtabla, int numempresa)
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

        private static bool ActualizarIndice(int idtabla, int numEmpresa,int nuevo)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@idtabla", idtabla);
            conexion.baseDatos.AgregarParametro("@indice", nuevo);
            return conexion.baseDatos.EjecutarSinConsulta("update tblcontrol{0} set ult_cve=@indice    where id_tabla =@idtabla", CommandType.Text) > 0;
        }

        private static bool ActualizarExistencias(string CVE_ART, int NUM_ALMA, decimal CANT,int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@CVE_ART", CVE_ART);
            conexion.baseDatos.AgregarParametro("@NUM_ALM", NUM_ALMA);
            conexion.baseDatos.AgregarParametro("@CANT", CANT);
            bool ban = conexion.baseDatos.EjecutarSinConsulta("update MULT{0} set Exist=Exist+@CANT    where CVE_ART =@CVE_ART and cve_alm=@NUM_ALM", CommandType.Text) > 0;
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@CVE_ART", CVE_ART);
            conexion.baseDatos.AgregarParametro("@CANT", CANT);
            conexion.baseDatos.AgregarParametro("@fecha", DateTime.Now);
            return conexion.baseDatos.EjecutarSinConsulta("update INVE{0} set Exist=Exist+@CANT, version_sinc=@fecha    where CVE_ART =@CVE_ART ", CommandType.Text) > 0;
        }

        private class Lotes
            {
            public int contador { get; set; }
            public string lote { get; set; }
            public DateTime caducidad { get; set; }
            public string articulo { get; set; }
            public int partida { get; set; }
            public int reg_ltpd_Enlace { get; set; }
            public int minve_id { get; set; }
        }
    }

    
}
