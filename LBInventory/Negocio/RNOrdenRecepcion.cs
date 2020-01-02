using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Windows.Forms;
using System.Data;

namespace Negocio
{
    public class RNOrdenRecepcion
    {
        public string TIP_DOC = "r"; 
        public string CVE_DOC { get; set; }
        public string CVE_CLPV { get; set; }
        public string STATUS { get; set; }
        public string SU_REFER { get; set; }
        public DateTime FECHA_DOC { get; set; }
        public DateTime FECHA_REC { get; set; }
        public DateTime FECHA_PAG { get; set; }
        public DateTime? FECHA_CANCELA { get; set; }        
        public decimal CAN_TOT { get; set; }
        public decimal IMP_TOT1 { get; set; }
        public decimal IMP_TOT2{ get; set; }
        public decimal IMP_TOT3{ get; set; }
        public decimal IMP_TOT4 { get; set; }
        public decimal DES_TOT { get; set; }
        public decimal DES_FIN { get; set; }
        public decimal TOT_IND { get; set; }
        public string OBS_COND = "";
        public int CVE_OBS = 0;
        public int NUM_ALMA { get; set; }
        public string ACT_CXP { get; set; }
        public string ACT_COI { get; set; }
        public string ENLAZADO { get; set; }
        public string TIP_DOC_E { get; set; }
        public int NUM_MONED { get; set; }
        public decimal TIPCAMB { get; set; }
        public int NUM_PAGOS { get; set; }
        public DateTime FECHAELAB { get; set; }
        public string SERIE { get; set; }
        public int FOLIO { get; set; }
        public int CTLPOL { get; set; }
        public string ESCFD { get; set; }
        public string CONTADO { get; set; }
        public string BLOQ { get; set; }
        public decimal DES_FIN_PORC { get; set; }
        public decimal DES_TOT_PORC { get; set; }
        public decimal IMPORTE { get; set; }
        public string TIP_DOC_ANT { get; set; }
        public string DOC_ANT { get; set; }
        public string TIP_DOC_SIG { get; set; }
        public string DOC_SIG { get; set; }
        public string FORMAENVIO { get; set; }


        // enviar en out list<RNPartidasRecepcion>
        public RNOrdenRecepcion GenerarRecepcion(string cveOrden, string cveDoc,int folio,int numEmpresa,out List<RNPartidasRecepcion> partidas)
        {
            RNOrdenRecepcion recepcion = new RNOrdenRecepcion().ObtenerOrdenCompra(cveOrden, cveDoc, folio,  numEmpresa);
            try
            {
                RNConexion conexion = new RNConexion(numEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@TIP_DOC", recepcion.TIP_DOC);
                conexion.baseDatos.AgregarParametro("@CVE_DOC",recepcion.CVE_DOC);
                conexion.baseDatos.AgregarParametro("@CVE_CLPV",recepcion.CVE_CLPV);
                conexion.baseDatos.AgregarParametro("@STATUS",recepcion.STATUS);
                conexion.baseDatos.AgregarParametro("@SU_REFER",recepcion.SU_REFER);
                conexion.baseDatos.AgregarParametro("@FECHA_DOC",recepcion.FECHA_DOC);
                conexion.baseDatos.AgregarParametro("@FECHA_REC",recepcion.FECHA_REC);
                conexion.baseDatos.AgregarParametro("@FECHA_PAG",recepcion.FECHA_PAG);
                conexion.baseDatos.AgregarParametro("@FECHA_CANCELA",recepcion.FECHA_CANCELA);
                conexion.baseDatos.AgregarParametro("@CAN_TOT",recepcion.CAN_TOT);
                conexion.baseDatos.AgregarParametro("@IMP_TOT1",recepcion.IMP_TOT1);
                conexion.baseDatos.AgregarParametro("@IMP_TOT2",recepcion.IMP_TOT2);
                conexion.baseDatos.AgregarParametro("@IMP_TOT3",recepcion.IMP_TOT3);
                conexion.baseDatos.AgregarParametro("@IMP_TOT4",recepcion.IMP_TOT4);
                conexion.baseDatos.AgregarParametro("@TOT_IND",recepcion.TOT_IND );
                conexion.baseDatos.AgregarParametro("@DES_TOT",recepcion.DES_TOT );
                conexion.baseDatos.AgregarParametro("@DES_FIN",recepcion.DES_FIN );
                conexion.baseDatos.AgregarParametro("@OBS_COND", recepcion.OBS_COND);
                conexion.baseDatos.AgregarParametro("@CVE_OBS", recepcion.CVE_OBS);
                conexion.baseDatos.AgregarParametro("@NUM_ALMA",recepcion.NUM_ALMA);
                conexion.baseDatos.AgregarParametro("@ACT_CXP",recepcion.ACT_CXP );
                conexion.baseDatos.AgregarParametro("@ACT_COI",recepcion.ACT_COI );
                conexion.baseDatos.AgregarParametro("@NUM_MONED",recepcion.NUM_MONED);
                conexion.baseDatos.AgregarParametro("@TIPCAMB",recepcion.TIPCAMB);
                conexion.baseDatos.AgregarParametro("@NUM_PAGOS",recepcion.NUM_PAGOS);
                conexion.baseDatos.AgregarParametro("@ENLAZADO",recepcion.ENLAZADO );
                conexion.baseDatos.AgregarParametro("@TIP_DOC_E",recepcion.TIP_DOC_E);
                conexion.baseDatos.AgregarParametro("@FECHAELAB",recepcion.FECHAELAB);
                conexion.baseDatos.AgregarParametro("@SERIE",recepcion.SERIE);
                conexion.baseDatos.AgregarParametro("@FOLIO",recepcion.FOLIO);
                conexion.baseDatos.AgregarParametro("@CTLPOL",recepcion.CTLPOL);
                conexion.baseDatos.AgregarParametro("@ESCFD",recepcion.ESCFD);
                conexion.baseDatos.AgregarParametro("@CONTADO",recepcion.CONTADO);
                conexion.baseDatos.AgregarParametro("@BLOQ",recepcion.BLOQ);
                conexion.baseDatos.AgregarParametro("@DES_FIN_PORC",recepcion.DES_FIN_PORC);
                conexion.baseDatos.AgregarParametro("@DES_TOT_PORC",recepcion.DES_TOT_PORC);
                conexion.baseDatos.AgregarParametro("@IMPORTE",recepcion.IMPORTE);
                conexion.baseDatos.AgregarParametro("@TIP_DOC_ANT",recepcion.TIP_DOC_ANT);
                conexion.baseDatos.AgregarParametro("@DOC_ANT",recepcion.DOC_ANT);
                conexion.baseDatos.AgregarParametro("@TIP_DOC_SIG",recepcion.TIP_DOC_SIG);
                conexion.baseDatos.AgregarParametro("@DOC_SIG",recepcion.DOC_SIG);
                conexion.baseDatos.AgregarParametro("@FORMAENVIO",recepcion.FORMAENVIO);
                var result = conexion.baseDatos.EjecutarSinConsulta(" INSERT INTO COMPR{0} (TIP_DOC,CVE_DOC,CVE_CLPV,STATUS,SU_REFER,FECHA_DOC,FECHA_REC,FECHA_PAG,FECHA_CANCELA,CAN_TOT,IMP_TOT1,IMP_TOT2,IMP_TOT3,IMP_TOT4,DES_TOT,DES_FIN,TOT_IND,OBS_COND,CVE_OBS,NUM_ALMA,ACT_CXP,ACT_COI,ENLAZADO,TIP_DOC_E,NUM_MONED,TIPCAMB,NUM_PAGOS,FECHAELAB,SERIE,FOLIO,CTLPOL,ESCFD,CONTADO,BLOQ,DES_FIN_PORC,DES_TOT_PORC,IMPORTE,TIP_DOC_ANT,DOC_ANT,TIP_DOC_SIG,DOC_SIG,FORMAENVIO) " +
                    "VALUES (@TIP_DOC,@CVE_DOC,@CVE_CLPV,@STATUS,@SU_REFER,@FECHA_DOC,@FECHA_REC,@FECHA_PAG,@FECHA_CANCELA,@CAN_TOT,@IMP_TOT1,@IMP_TOT2,@IMP_TOT3,@IMP_TOT4,@DES_TOT,@DES_FIN,@TOT_IND,@OBS_COND,@CVE_OBS,@NUM_ALMA,@ACT_CXP,@ACT_COI,@ENLAZADO,@TIP_DOC_E,@NUM_MONED,@TIPCAMB,@NUM_PAGOS,@FECHAELAB,@SERIE,@FOLIO,@CTLPOL,@ESCFD,@CONTADO,@BLOQ,@DES_FIN_PORC,@DES_TOT_PORC,@IMPORTE,@TIP_DOC_ANT,@DOC_ANT,@TIP_DOC_SIG,@DOC_SIG,@FORMAENVIO) ", CommandType.Text) >0;
                // agregar los campos libres 
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@CVE_DOC", recepcion.CVE_DOC);
                var resultcl = conexion.baseDatos.EjecutarSinConsulta(" insert into COMPR_CLIB{0} (CLAVE_DOC) values(@CVE_DOC); ", CommandType.Text) >0;
                //agregar las partidas
                partidas = new RNPartidasRecepcion().GenerarPartidas(cveOrden, cveDoc, folio, numEmpresa);
                return recepcion;
                //agregar observaciones

            } catch (Exception e)
            {
                MessageBox.Show("Error al generar la recepción:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                partidas = new List<RNPartidasRecepcion>();
                return recepcion;
            }
            
        }

        public RNOrdenRecepcion GenerarRecepcion(RNOrdenRecepcion recepcion, List<RNPartidasRecepcion> partidas, string cveDoc,int folio,int numEmpresa, List<RNRecepcion.Lotes> lotes)
        {
            //obtener la info del PROVEEDOR para recepcion
            RNProveedor prov = RNLbInventory.ObtenerProveedorRecepcion(numEmpresa);
            // INSETAR PARTIDAS Y REGRESAR  EL TORTAL
            //agregar las partidas
            
            try
            {
                RNConexion conexion = new RNConexion(numEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@TIP_DOC", recepcion.TIP_DOC);
                conexion.baseDatos.AgregarParametro("@CVE_DOC", cveDoc);
                conexion.baseDatos.AgregarParametro("@CVE_CLPV",prov.CLAVE);
                conexion.baseDatos.AgregarParametro("@STATUS",recepcion.STATUS);
                conexion.baseDatos.AgregarParametro("@SU_REFER",recepcion.SU_REFER);
                conexion.baseDatos.AgregarParametro("@FECHA_DOC",recepcion.FECHA_DOC);
                conexion.baseDatos.AgregarParametro("@FECHA_REC",recepcion.FECHA_REC);
                conexion.baseDatos.AgregarParametro("@FECHA_PAG",recepcion.FECHA_PAG);
                conexion.baseDatos.AgregarParametro("@FECHA_CANCELA",recepcion.FECHA_CANCELA);
                conexion.baseDatos.AgregarParametro("@CAN_TOT", recepcion.CAN_TOT);
                conexion.baseDatos.AgregarParametro("@IMP_TOT1", 0.00M);
                conexion.baseDatos.AgregarParametro("@IMP_TOT2", 0.00M);
                conexion.baseDatos.AgregarParametro("@IMP_TOT3", 0.00M);
                conexion.baseDatos.AgregarParametro("@IMP_TOT4", 0.00M);
                conexion.baseDatos.AgregarParametro("@TOT_IND",recepcion.TOT_IND);
                conexion.baseDatos.AgregarParametro("@DES_TOT",recepcion.DES_TOT);
                conexion.baseDatos.AgregarParametro("@DES_FIN",recepcion.DES_FIN);
                conexion.baseDatos.AgregarParametro("@OBS_COND", recepcion.OBS_COND);
                conexion.baseDatos.AgregarParametro("@CVE_OBS", recepcion.CVE_OBS);
                conexion.baseDatos.AgregarParametro("@NUM_ALMA",recepcion.NUM_ALMA);
                conexion.baseDatos.AgregarParametro("@ACT_CXP",recepcion.ACT_CXP );
                conexion.baseDatos.AgregarParametro("@ACT_COI",recepcion.ACT_COI );
                conexion.baseDatos.AgregarParametro("@NUM_MONED",recepcion.NUM_MONED);
                conexion.baseDatos.AgregarParametro("@TIPCAMB",recepcion.TIPCAMB);
                conexion.baseDatos.AgregarParametro("@NUM_PAGOS",recepcion.NUM_PAGOS);
                conexion.baseDatos.AgregarParametro("@ENLAZADO",recepcion.ENLAZADO );
                conexion.baseDatos.AgregarParametro("@TIP_DOC_E",recepcion.TIP_DOC_E);
                conexion.baseDatos.AgregarParametro("@FECHAELAB",recepcion.FECHAELAB);
                conexion.baseDatos.AgregarParametro("@SERIE",recepcion.SERIE);
                conexion.baseDatos.AgregarParametro("@FOLIO",folio);
                conexion.baseDatos.AgregarParametro("@CTLPOL",recepcion.CTLPOL);
                conexion.baseDatos.AgregarParametro("@ESCFD",recepcion.ESCFD);
                conexion.baseDatos.AgregarParametro("@CONTADO",recepcion.CONTADO);
                conexion.baseDatos.AgregarParametro("@BLOQ",recepcion.BLOQ);
                conexion.baseDatos.AgregarParametro("@DES_FIN_PORC",recepcion.DES_FIN_PORC);
                conexion.baseDatos.AgregarParametro("@DES_TOT_PORC",recepcion.DES_TOT_PORC);
                conexion.baseDatos.AgregarParametro("@IMPORTE", 0.00M);
                conexion.baseDatos.AgregarParametro("@TIP_DOC_ANT","");
                conexion.baseDatos.AgregarParametro("@DOC_ANT","");
                conexion.baseDatos.AgregarParametro("@TIP_DOC_SIG",null);
                conexion.baseDatos.AgregarParametro("@DOC_SIG",null);
                conexion.baseDatos.AgregarParametro("@FORMAENVIO",recepcion.FORMAENVIO);
                var result = conexion.baseDatos.EjecutarSinConsulta(" INSERT INTO COMPR{0} (TIP_DOC,CVE_DOC,CVE_CLPV,STATUS,SU_REFER,FECHA_DOC,FECHA_REC,FECHA_PAG,FECHA_CANCELA,CAN_TOT,IMP_TOT1,IMP_TOT2,IMP_TOT3,IMP_TOT4,DES_TOT,DES_FIN,TOT_IND,OBS_COND,CVE_OBS,NUM_ALMA,ACT_CXP,ACT_COI,ENLAZADO,TIP_DOC_E,NUM_MONED,TIPCAMB,NUM_PAGOS,FECHAELAB,SERIE,FOLIO,CTLPOL,ESCFD,CONTADO,BLOQ,DES_FIN_PORC,DES_TOT_PORC,IMPORTE,TIP_DOC_ANT,DOC_ANT,TIP_DOC_SIG,DOC_SIG,FORMAENVIO) " +
                    "VALUES (@TIP_DOC,@CVE_DOC,@CVE_CLPV,@STATUS,@SU_REFER,@FECHA_DOC,@FECHA_REC,@FECHA_PAG,@FECHA_CANCELA,@CAN_TOT,@IMP_TOT1,@IMP_TOT2,@IMP_TOT3,@IMP_TOT4,@DES_TOT,@DES_FIN,@TOT_IND,@OBS_COND,@CVE_OBS,@NUM_ALMA,@ACT_CXP,@ACT_COI,@ENLAZADO,@TIP_DOC_E,@NUM_MONED,@TIPCAMB,@NUM_PAGOS,@FECHAELAB,@SERIE,@FOLIO,@CTLPOL,@ESCFD,@CONTADO,@BLOQ,@DES_FIN_PORC,@DES_TOT_PORC,@IMPORTE,@TIP_DOC_ANT,@DOC_ANT,@TIP_DOC_SIG,@DOC_SIG,@FORMAENVIO) ", CommandType.Text) >0;
                // agregar los campos libres 
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@CVE_DOC", recepcion.CVE_DOC);
                var resultcl = conexion.baseDatos.EjecutarSinConsulta(" insert into COMPR_CLIB{0}(CLAVE_DOC) values(@CVE_DOC); ", CommandType.Text) >0;
                decimal cant_total, imp1, imp2, imp3, imp4, total, desc, descind, desfin;
                var bpartidas = new RNPartidasRecepcion().InsertarPartidas(partidas, cveDoc, folio, lotes, numEmpresa, out cant_total, out imp1, out imp2, out imp3, out imp4, out total, out desc, out descind, out desfin);
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@IMP_TOT1", imp1);
                conexion.baseDatos.AgregarParametro("@IMP_TOT2", imp2);
                conexion.baseDatos.AgregarParametro("@IMP_TOT3", imp3);
                conexion.baseDatos.AgregarParametro("@IMP_TOT4", imp4);
                conexion.baseDatos.AgregarParametro("@IMPORTE", total);
                conexion.baseDatos.AgregarParametro("@CVE_DOC", cveDoc);
                result = conexion.baseDatos.EjecutarSinConsulta("update COMPR{0} set  IMP_TOT1=@IMP_TOT1 ,IMP_TOT2=@IMP_TOT2,IMP_TOT3=@IMP_TOT3,IMP_TOT4=@IMP_TOT4,IMPORTE=@IMPORTE " +
                   "where CVE_DOC = @CVE_DOC", CommandType.Text) > 0;



                return recepcion;

            } catch (Exception e)
            {
                MessageBox.Show("Error al generar la recepción:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                partidas = new List<RNPartidasRecepcion>();
                return recepcion;
            }
            
        }

        public RNOrdenRecepcion ObtenerOrdenCompra(string cveOrden, string cveDoc, int folio, int numEmpresa)
        {
            RNOrdenRecepcion recepcion = new RNOrdenRecepcion();
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@cveOrden", cveOrden);
            var result = conexion.baseDatos.ObtenerTabla("select * from compo{0} where cve_doc =@cveOrden");
            foreach (DataRow row in result.Rows)
            {
                recepcion.CVE_DOC = cveDoc;
                recepcion.CVE_CLPV= row["CVE_CLPV"].ToString();
                recepcion.STATUS     = row["STATUS"].ToString();
                recepcion.SU_REFER  = row["SU_REFER"].ToString();
                recepcion.FECHA_DOC = DateTime.Today;
                recepcion.FECHA_REC = DateTime.Today;
                recepcion.FECHA_PAG = DateTime.Today;
                recepcion.FECHA_CANCELA = null;
                recepcion.CAN_TOT  = Convert.ToDecimal(row["CAN_TOT"].ToString());
                recepcion.IMP_TOT1 = Convert.ToDecimal(row["IMP_TOT1"].ToString());
                recepcion.IMP_TOT2 = Convert.ToDecimal(row["IMP_TOT2"].ToString());
                recepcion.IMP_TOT3 = Convert.ToDecimal(row["IMP_TOT3"].ToString());
                recepcion.IMP_TOT4 = Convert.ToDecimal(row["IMP_TOT4"].ToString());
                recepcion.TOT_IND  = Convert.ToDecimal(row["TOT_IND"].ToString());
                recepcion.DES_TOT   = Convert.ToDecimal(row["DES_TOT"].ToString());
                recepcion.DES_FIN  = Convert.ToDecimal(row["DES_FIN"].ToString());
                recepcion.NUM_ALMA = Convert.ToInt32(row["NUM_ALMA"].ToString());
                recepcion.ACT_CXP  = "S";
                recepcion.ACT_COI  = "N";
                recepcion.NUM_MONED= Convert.ToInt32(row["NUM_MONED"].ToString());
                recepcion.TIPCAMB  = Convert.ToDecimal(row["TIPCAMB"].ToString());
                recepcion.NUM_PAGOS= 1;
                recepcion.ENLAZADO = row["ENLAZADO"].ToString();
                recepcion.TIP_DOC_E= row["TIP_DOC_E"].ToString();
                recepcion.FECHAELAB= DateTime.Today;
                recepcion.SERIE    = "";
                recepcion.FOLIO    = folio;
                recepcion.CTLPOL   = 0;
                recepcion.ESCFD    = "N";
                recepcion.CONTADO  = "N";
                recepcion.BLOQ     = "N";
                recepcion.DES_FIN_PORC= 0.0M;
                recepcion.DES_TOT_PORC= 0.0M;
                recepcion.IMPORTE     = Convert.ToDecimal(row["IMPORTE"].ToString());
                recepcion.TIP_DOC_ANT = "o";
                recepcion.DOC_ANT     = cveOrden;
                recepcion.TIP_DOC_SIG = null;
                recepcion.DOC_SIG     = null;
                recepcion.FORMAENVIO  = "I";
            } // actualizar la orden de compra
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@cveRecepcion", cveDoc);
            conexion.baseDatos.AgregarParametro("@cveOrden", cveOrden);
            var resultcl = conexion.baseDatos.EjecutarSinConsulta("UPDATE compo{0} c set c.doc_sig = @cveRecepcion, c.tip_doc_sig = 'r', c.ENLAZADO = 'T',c.TIP_DOC_E='r' where c.cve_doc = @cveOrden", CommandType.Text) > 0;
            return recepcion;
        }




    }
}
