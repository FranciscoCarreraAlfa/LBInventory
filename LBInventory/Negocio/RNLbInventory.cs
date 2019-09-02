using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio
{
    public class RNLbInventory
    {

        public static RNConfiguracion ObtenerImportadora()
        {
            var configuracion = RNConfiguracion.Listar().Where(x => x.SNImportadora).FirstOrDefault();
            return configuracion;
        }

        public static RNConfiguracion ObtenerComercializadora()
        {
            var configuracion = RNConfiguracion.Listar().Where(x => x.SNComercializadora).FirstOrDefault();
            return configuracion;
        }

        public static string ObtenerCveDoc(string tipoDoc, string serie, int numEmpresa, string tabla, out int folio)
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
                if (tabla == "C")
                {
                    var result = conexion.baseDatos.ObtenerTabla("SELECT ULT_DOC+1 as ULT_DOC FROM FOLIOSC{0} WHERE TIP_DOC =@tipoDoc AND SERIE = @serie AND FOLIODESDE = 1");
                    foreach (DataRow row in result.Rows)
                    {
                        folio = Convert.ToInt32(row["ULT_DOC"].ToString());
                    }
                }
                else
                {
                    var result = conexion.baseDatos.ObtenerTabla("SELECT ULT_DOC+1 as ULT_DOC FROM FOLIOSF{0} WHERE TIP_DOC =@tipoDoc AND SERIE = @serie AND FOLIODESDE = 1");
                    foreach (DataRow row in result.Rows)
                    {
                        folio = Convert.ToInt32(row["ULT_DOC"].ToString());
                    }
                }

                ActualizarFolio(tipoDoc, serie, folio, tabla, numEmpresa);
                string mascara = ObtenerMascara(tipoDoc, serie, tabla, numEmpresa);
                aux = mascara.Insert(10 - (folio.ToString().Length), folio.ToString());
                aux = aux.Substring(0, 10);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al generar la recepción:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "          " + aux;
        }

        public static bool ActualizarFolio(string tipoDoc, string serie, int ultimoId,string tabla ,int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@tipoDoc", tipoDoc);
            conexion.baseDatos.AgregarParametro("@serie", serie);
            conexion.baseDatos.AgregarParametro("@LAST_ID_DATE", DateTime.Now);
            conexion.baseDatos.AgregarParametro("@LAST_ID", ultimoId);
            if (tabla == "C")
            {
                return conexion.baseDatos.EjecutarSinConsulta(" UPDATE FOLIOSC{0} SET ULT_DOC = (CASE WHEN ULT_DOC < @LAST_ID THEN @LAST_ID ELSE ULT_DOC END)," +
            "FECH_ULT_DOC = @LAST_ID_DATE WHERE TIP_DOC = @tipoDoc AND SERIE = @serie AND FOLIODESDE = 1; ", CommandType.Text) > 0;
            }
            else if (tabla == "F")
            {
                return conexion.baseDatos.EjecutarSinConsulta(" UPDATE FOLIOSF{0} SET ULT_DOC = (CASE WHEN ULT_DOC < @LAST_ID THEN @LAST_ID ELSE ULT_DOC END)," +
            "FECH_ULT_DOC = @LAST_ID_DATE WHERE TIP_DOC = @tipoDoc AND SERIE = @serie AND FOLIODESDE = 1; ", CommandType.Text) > 0;
            }
            else
            {
                return false;
            }
        }
        public static string ObtenerMascara(string tipoDoc, string serie,string tabla ,int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@tipoDoc", tipoDoc.ToUpper());
            conexion.baseDatos.AgregarParametro("@serie", serie);
            if (tabla == "C")
            {
                var result = conexion.baseDatos.ObtenerTabla(" SELECT CASE MASCARA WHEN '0' THEN '          ' WHEN '1' THEN '0000000000' WHEN '2' THEN '' ELSE '' END" +
                            " FROM PARAM_FOLIOSC{0} WHERE TIPODOCTO = @tipoDoc AND SERIE = @serie ; ");
                string mascara = "";
                foreach (DataRow row in result.Rows)
                {
                    mascara = row["CASE"].ToString();
                }
                return mascara;
            }
            else if (tabla == "F")
            {
                var result = conexion.baseDatos.ObtenerTabla(" SELECT CASE MASCARA WHEN '0' THEN '          ' WHEN '1' THEN '0000000000' WHEN '2' THEN '' ELSE '' END" +
                            " FROM PARAM_FOLIOSF{0} WHERE TIPODOCTO = @tipoDoc AND SERIE = @serie ; ");
                string mascara = "";
                foreach (DataRow row in result.Rows)
                {
                    mascara = row["CASE"].ToString();
                }
                return mascara;
            }else
            {
                return "          ";
            }
            

        }

        public static RNCliente ObtenerClienteRemicion(int numEmpresa)
        {
            //Crear metodo para obtener la clave del cliente al que que hace la remision
            return RNCliente.ObtenerCliente("LCM",numEmpresa);
        }

        public static RNProveedor ObtenerProveedorRecepcion(int numEmpresa)
        {
            //Crear metodo para obtener la clave del cliente al que que hace la remision
            return RNProveedor.ObtenerProveedor("         4", numEmpresa);
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
                indice = Convert.ToInt32(row["ult_cve"].ToString());
            }
            ActualizarIndice(idtabla, numempresa, indice);
            return indice;
        }

        private static bool ActualizarIndice(int idtabla, int numEmpresa, int nuevo)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@idtabla", idtabla);
            conexion.baseDatos.AgregarParametro("@indice", nuevo);
            return conexion.baseDatos.EjecutarSinConsulta("update tblcontrol{0} set ult_cve=@indice    where id_tabla =@idtabla", CommandType.Text) > 0;
        }

        public static int ObtenerBita(string cveCliente,string cveDoc,string cveActividad,decimal total,int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            int indice = ObtenerIndice(62,numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@CVE_BITA", indice);
            conexion.baseDatos.AgregarParametro("@CVE_CLIE", cveCliente);
            conexion.baseDatos.AgregarParametro("@CVE_CAMPANIA", "_SAE_");
            conexion.baseDatos.AgregarParametro("@CVE_ACTIVIDAD", cveActividad);
            conexion.baseDatos.AgregarParametro("@FECHAHORA", DateTime.Today);
            conexion.baseDatos.AgregarParametro("@CVE_USUARIO", 0);
            conexion.baseDatos.AgregarParametro("@OBSERVACIONES", "No. ["+ cveDoc + "] $ "+ total.ToString());
            conexion.baseDatos.AgregarParametro("@STATUS", "F");
            conexion.baseDatos.AgregarParametro("@NOM_USUARIO", "Administrador");
            var result = conexion.baseDatos.EjecutarSinConsulta(" INSERT INTO BITA{0} (CVE_BITA,CVE_CLIE,CVE_CAMPANIA ,CVE_ACTIVIDAD,FECHAHORA ,CVE_USUARIO,OBSERVACIONES,STATUS,NOM_USUARIO) " +
                "VALUES (@CVE_BITA,@CVE_CLIE,@CVE_CAMPANIA ,@CVE_ACTIVIDAD,@FECHAHORA ,@CVE_USUARIO,@OBSERVACIONES,@STATUS,@NOM_USUARIO)", CommandType.Text) > 0;
            return result ? indice : 0;
        }

        public static int RegistrarMovInveRecepcion(RNPartidasRecepcion partida, RNOrdenRecepcion orden, int reg_ltpd, int signo,int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            int existencias = RNRecepcion.ObtenerExistencias(partida.CVE_ART, partida.NUM_ALM, numEmpresa);
            int indice = RNLbInventory.ObtenerIndice(44, numEmpresa);
            conexion.baseDatos.AgregarParametro("@CVE_ART", partida.CVE_ART);
            conexion.baseDatos.AgregarParametro("@ALMACEN", partida.NUM_ALM);
            conexion.baseDatos.AgregarParametro("@NUM_MOV", indice);
            conexion.baseDatos.AgregarParametro("@CVE_CPTO", 1);
            conexion.baseDatos.AgregarParametro("@FECHA_DOCU", orden.FECHA_DOC);
            conexion.baseDatos.AgregarParametro("@TIPO_DOC", orden.TIP_DOC);
            conexion.baseDatos.AgregarParametro("@REFER", orden.CVE_DOC);
            conexion.baseDatos.AgregarParametro("@CLAVE_CLPV", orden.CVE_CLPV);
            conexion.baseDatos.AgregarParametro("@VEND", null);
            conexion.baseDatos.AgregarParametro("@CANT", partida.CANT);
            conexion.baseDatos.AgregarParametro("@CANT_COST", partida.CANT);
            conexion.baseDatos.AgregarParametro("@PRECIO", partida.PREC);
            conexion.baseDatos.AgregarParametro("@COSTO", partida.COST);
            conexion.baseDatos.AgregarParametro("@AFEC_COI", null);
            conexion.baseDatos.AgregarParametro("@CVE_OBS", null);
            conexion.baseDatos.AgregarParametro("@REG_SERIE", partida.REG_SERIE);
            conexion.baseDatos.AgregarParametro("@UNI_VENTA", partida.UNI_VENTA);
            conexion.baseDatos.AgregarParametro("@E_LTPD", reg_ltpd);
            conexion.baseDatos.AgregarParametro("@EXIST_G", partida.CANT + existencias);//revisar la suma de las existencias
            conexion.baseDatos.AgregarParametro("@EXISTENCIA", partida.CANT + existencias);
            conexion.baseDatos.AgregarParametro("@TIPO_PROD", partida.TIPO_PROD);
            conexion.baseDatos.AgregarParametro("@FACTOR_CON", partida.FACTCONV);
            conexion.baseDatos.AgregarParametro("@FECHAELAB", orden.FECHAELAB);
            conexion.baseDatos.AgregarParametro("@CTLPOL", orden.CTLPOL);
            conexion.baseDatos.AgregarParametro("@CVE_FOLIO", orden.FOLIO);
            conexion.baseDatos.AgregarParametro("@SIGNO", signo);
            conexion.baseDatos.AgregarParametro("@COSTEADO", "S");
            conexion.baseDatos.AgregarParametro("@COSTO_PROM_INI", partida.COST);
            conexion.baseDatos.AgregarParametro("@COSTO_PROM_FIN", partida.COST);
            conexion.baseDatos.AgregarParametro("@COSTO_PROM_GRAL", partida.COST);
            conexion.baseDatos.AgregarParametro("@DESDE_INVE", "N");
            conexion.baseDatos.AgregarParametro("@MOV_ENLAZADO", 0);
            var result = conexion.baseDatos.EjecutarSinConsulta(" INSERT INTO MINVE{0} (CVE_ART,ALMACEN,NUM_MOV,CVE_CPTO,FECHA_DOCU,TIPO_DOC,REFER,CLAVE_CLPV,VEND,CANT,CANT_COST,PRECIO,COSTO,AFEC_COI,CVE_OBS,REG_SERIE,UNI_VENTA,E_LTPD,EXIST_G,EXISTENCIA,TIPO_PROD,FACTOR_CON,FECHAELAB,CTLPOL,CVE_FOLIO,SIGNO,COSTEADO,COSTO_PROM_INI,COSTO_PROM_FIN,COSTO_PROM_GRAL,DESDE_INVE,MOV_ENLAZADO) " +
                    "VALUES (@CVE_ART,@ALMACEN,@NUM_MOV,@CVE_CPTO,@FECHA_DOCU,@TIPO_DOC,@REFER,@CLAVE_CLPV,@VEND,@CANT,@CANT_COST,@PRECIO,@COSTO,@AFEC_COI,@CVE_OBS,@REG_SERIE,@UNI_VENTA,@E_LTPD,@EXIST_G,@EXISTENCIA,@TIPO_PROD,@FACTOR_CON,@FECHAELAB,@CTLPOL,@CVE_FOLIO,@SIGNO,@COSTEADO,@COSTO_PROM_INI,@COSTO_PROM_FIN,@COSTO_PROM_GRAL,@DESDE_INVE,@MOV_ENLAZADO) ", CommandType.Text) > 0;
            return indice;
        }

        // se reciben una partida de recepcion para no cambiar de una partida de recepcion a una de remision
        public static int RegistrarMovInveRemision(RNPartidasRecepcion partida, RNOrdenRemision orden, int reg_ltpd, int signo,int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            int existencias = RNRecepcion.ObtenerExistencias(partida.CVE_ART, partida.NUM_ALM, numEmpresa);
            int indice = RNLbInventory.ObtenerIndice(44, numEmpresa);
            conexion.baseDatos.AgregarParametro("@CVE_ART", partida.CVE_ART);
            conexion.baseDatos.AgregarParametro("@ALMACEN", partida.NUM_ALM);
            conexion.baseDatos.AgregarParametro("@NUM_MOV", indice);
            conexion.baseDatos.AgregarParametro("@CVE_CPTO", 51);
            conexion.baseDatos.AgregarParametro("@FECHA_DOCU", orden.FECHA_DOC);
            conexion.baseDatos.AgregarParametro("@TIPO_DOC", orden.TIP_DOC);
            conexion.baseDatos.AgregarParametro("@REFER", orden.CVE_DOC);
            conexion.baseDatos.AgregarParametro("@CLAVE_CLPV", orden.CVE_CLPV);
            conexion.baseDatos.AgregarParametro("@VEND", null);
            conexion.baseDatos.AgregarParametro("@CANT", partida.CANT);
            conexion.baseDatos.AgregarParametro("@CANT_COST", partida.CANT);
            conexion.baseDatos.AgregarParametro("@PRECIO", partida.PREC);
            conexion.baseDatos.AgregarParametro("@COSTO", partida.COST);
            conexion.baseDatos.AgregarParametro("@AFEC_COI", null);
            conexion.baseDatos.AgregarParametro("@CVE_OBS", null);
            conexion.baseDatos.AgregarParametro("@REG_SERIE", partida.REG_SERIE);
            conexion.baseDatos.AgregarParametro("@UNI_VENTA", partida.UNI_VENTA);
            conexion.baseDatos.AgregarParametro("@E_LTPD", reg_ltpd);
            conexion.baseDatos.AgregarParametro("@EXIST_G", partida.CANT + existencias);//revisar la suma de las existencias
            conexion.baseDatos.AgregarParametro("@EXISTENCIA", partida.CANT + existencias);
            conexion.baseDatos.AgregarParametro("@TIPO_PROD", partida.TIPO_PROD);
            conexion.baseDatos.AgregarParametro("@FACTOR_CON", 1.00);
            conexion.baseDatos.AgregarParametro("@FECHAELAB", orden.FECHAELAB);
            conexion.baseDatos.AgregarParametro("@CTLPOL", orden.CTLPOL);
            conexion.baseDatos.AgregarParametro("@CVE_FOLIO", orden.FOLIO);
            conexion.baseDatos.AgregarParametro("@SIGNO", signo);
            conexion.baseDatos.AgregarParametro("@COSTEADO", "S");
            conexion.baseDatos.AgregarParametro("@COSTO_PROM_INI", partida.COST);
            conexion.baseDatos.AgregarParametro("@COSTO_PROM_FIN", partida.COST);
            conexion.baseDatos.AgregarParametro("@COSTO_PROM_GRAL", partida.COST);
            conexion.baseDatos.AgregarParametro("@DESDE_INVE", "N");
            conexion.baseDatos.AgregarParametro("@MOV_ENLAZADO", 0);
            var result = conexion.baseDatos.EjecutarSinConsulta(" INSERT INTO MINVE{0} (CVE_ART,ALMACEN,NUM_MOV,CVE_CPTO,FECHA_DOCU,TIPO_DOC,REFER,CLAVE_CLPV,VEND,CANT,CANT_COST,PRECIO,COSTO,AFEC_COI,CVE_OBS,REG_SERIE,UNI_VENTA,E_LTPD,EXIST_G,EXISTENCIA,TIPO_PROD,FACTOR_CON,FECHAELAB,CTLPOL,CVE_FOLIO,SIGNO,COSTEADO,COSTO_PROM_INI,COSTO_PROM_FIN,COSTO_PROM_GRAL,DESDE_INVE,MOV_ENLAZADO) " +
                    "VALUES (@CVE_ART,@ALMACEN,@NUM_MOV,@CVE_CPTO,@FECHA_DOCU,@TIPO_DOC,@REFER,@CLAVE_CLPV,@VEND,@CANT,@CANT_COST,@PRECIO,@COSTO,@AFEC_COI,@CVE_OBS,@REG_SERIE,@UNI_VENTA,@E_LTPD,@EXIST_G,@EXISTENCIA,@TIPO_PROD,@FACTOR_CON,@FECHAELAB,@CTLPOL,@CVE_FOLIO,@SIGNO,@COSTEADO,@COSTO_PROM_INI,@COSTO_PROM_FIN,@COSTO_PROM_GRAL,@DESDE_INVE,@MOV_ENLAZADO) ", CommandType.Text) > 0;
            return indice;
        }

        public static int RegistrarEnlace(int Reg_ltpd, int cantidad, int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            int indice = RNLbInventory.ObtenerIndice(67, numEmpresa);
            conexion.baseDatos.AgregarParametro("@E_LTPD", indice);
            conexion.baseDatos.AgregarParametro("@REG_LTPD", Reg_ltpd);
            conexion.baseDatos.AgregarParametro("@CANTIDAD", cantidad);
            conexion.baseDatos.AgregarParametro("@PXRS", cantidad);
            var result = conexion.baseDatos.EjecutarSinConsulta(" INSERT INTO ENLACE_LTPD{0} (E_LTPD,REG_LTPD,CANTIDAD,PXRS) " +
                    "VALUES (@E_LTPD,@REG_LTPD,@CANTIDAD,@PXRS) ", CommandType.Text) > 0;
            return indice;
        }

        public static bool ActualizarExistencias(string CVE_ART, int NUM_ALMA,int signo ,decimal CANT, int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@CVE_ART", CVE_ART);
            conexion.baseDatos.AgregarParametro("@NUM_ALM", NUM_ALMA);
            conexion.baseDatos.AgregarParametro("@CANT", (CANT*signo));
            bool ban = conexion.baseDatos.EjecutarSinConsulta("update MULT{0} set Exist=Exist+@CANT    where CVE_ART =@CVE_ART and cve_alm=@NUM_ALM", CommandType.Text) > 0;
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@CVE_ART", CVE_ART);
            conexion.baseDatos.AgregarParametro("@CANT", CANT);
            conexion.baseDatos.AgregarParametro("@fecha", DateTime.Now);
            return conexion.baseDatos.EjecutarSinConsulta("update INVE{0} set Exist=Exist+@CANT, version_sinc=@fecha    where CVE_ART =@CVE_ART ", CommandType.Text) > 0;
        }
    }
}


