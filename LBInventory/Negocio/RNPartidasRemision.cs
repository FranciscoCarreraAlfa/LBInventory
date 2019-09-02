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
    public class RNPartidasRemision
    {
        public string CVE_DOC { get; set; }
        public int NUM_PAR { get; set; }
        public string CVE_ART { get; set; }
        public decimal CANT { get; set; }
        public decimal PXS { get; set; }
        public decimal PREC { get; set; }
        public decimal COST { get; set; }
        public decimal IMPU1 { get; set; }
        public decimal IMPU2 { get; set; }
        public decimal IMPU3 { get; set; }
        public decimal IMPU4 { get; set; }
        public int IMP1APLA { get; set; }
        public int IMP2APLA { get; set; }
        public int IMP3APLA { get; set; }
        public int IMP4APLA { get; set; }
        public decimal TOTIMP1 { get; set; }
        public decimal TOTIMP2 { get; set; }
        public decimal TOTIMP3 { get; set; }
        public decimal TOTIMP4 { get; set; }
        public decimal DESC1 { get; set; }
        public decimal DESC2 { get; set; }
        public decimal DESC3 { get; set; }
        public decimal COMI { get; set; }
        public decimal APAR { get; set; }
        public string ACT_INV { get; set; }
        public int NUM_ALM { get; set; }
        public string POLIT_APLI { get; set; }
        public decimal TIP_CAM { get; set; }
        public string UNI_VENTA { get; set; }
        public string TIPO_PROD { get; set; }
        public int CVE_OBS { get; set; }
        public int REG_SERIE { get; set; }
        public int E_LTPD { get; set; }
        public string TIPO_ELEM { get; set; }
        public int NUM_MOV { get; set; }
        public decimal TOT_PARTIDA { get; set; }
        public string IMPRIMIR { get; set; }
        public string UUID { get; set; }
        public DateTime VERSION_SINC { get; set; }
        public string MAN_IEPS { get; set; }
        public int APL_MAN_IMP { get; set; }
        public decimal CUOTA_IEPS { get; set; }
        public string APL_MAN_IEPS { get; set; }
        public decimal MTO_PORC { get; set; }
        public decimal MTO_CUOTA { get; set; }
        public int CVE_ESQ { get; set; }
        public string DESCR_ART { get; set; }

        public static bool GenerarPartidas(RNOrdenRemision orden,string cvedoc, int folio, int numEmpresa, List<RNPartidasRecepcion> partidas, List<RNRecepcion.Lotes> lotes ,out decimal cant_total, out decimal imp1, out decimal imp2, out decimal imp3, out decimal imp4, out decimal total, out decimal desc, out decimal descind, out decimal desfin)
        {
            
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            bool result = false;
            cant_total = 0;
            imp1 = 0;
            imp2 = 0;
            imp3 = 0;
            imp4 = 0;
            total = 0;
            desc = 0;
            descind = 0;
            desfin = 0;
            foreach (var p in partidas)
                {
                RNProducto prod = RNProducto.ObtenerProductoPorClave(numEmpresa, p.CVE_ART);
                decimal precio = RNProducto.ObtenerPrecioProducto(p.CVE_ART, 1, numEmpresa);
                decimal IMPU1 = 0.0M;
                decimal IMPU2 = 0.0M;
                decimal IMPU3 = 0.0M;
                decimal IMPU4 = 0.0M;
                int IMP1APLA = 0;
                int IMP2APLA = 0;
                int IMP3APLA = 0;
                int IMP4APLA = 0;
                RNProducto.ObtenerImpuestos(p.CVE_ART, prod.CVE_ESQIMPU.Value, numEmpresa, out IMPU1, out IMPU2, out IMPU3, out IMPU4, out IMP1APLA, out IMP2APLA, out IMP3APLA, out IMP4APLA);
                cant_total += (precio * p.CANT);
                imp1 += precio * (IMPU1 / 100) * p.CANT;
                imp2 += precio * (IMPU2 / 100) * p.CANT;
                imp3 += precio * (IMPU3 / 100) * p.CANT;
                imp4 += precio * (IMPU4 / 100) * p.CANT;
                total += (precio  + imp1 + imp2 + imp3 + imp4 )* p.CANT;
                desc += 0;
                descind += 0;
                desfin += 0;
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@CVE_DOC", cvedoc);
                conexion.baseDatos.AgregarParametro("@NUM_PAR",p.NUM_PAR);
                conexion.baseDatos.AgregarParametro("@CVE_ART",p.CVE_ART);
                conexion.baseDatos.AgregarParametro("@CANT",p.CANT);
                conexion.baseDatos.AgregarParametro("@PXS",p.CANT);
                conexion.baseDatos.AgregarParametro("@PREC", precio);
                conexion.baseDatos.AgregarParametro("@COST",p.COST);
                conexion.baseDatos.AgregarParametro("@IMPU1",IMPU1);
                conexion.baseDatos.AgregarParametro("@IMPU2",IMPU2);
                conexion.baseDatos.AgregarParametro("@IMPU3",IMPU3);
                conexion.baseDatos.AgregarParametro("@IMPU4",IMPU4);
                conexion.baseDatos.AgregarParametro("@IMP1APLA",IMP1APLA);
                conexion.baseDatos.AgregarParametro("@IMP2APLA",IMP2APLA);
                conexion.baseDatos.AgregarParametro("@IMP3APLA",IMP3APLA);
                conexion.baseDatos.AgregarParametro("@IMP4APLA",IMP4APLA);
                conexion.baseDatos.AgregarParametro("@TOTIMP1", IMP2APLA == 1 ? (precio * (IMPU1 / 100)) * p.CANT : 0.00M);
                conexion.baseDatos.AgregarParametro("@TOTIMP2", IMP3APLA == 1 ? (precio * (IMPU2 / 100)) * p.CANT : 0.00M);
                conexion.baseDatos.AgregarParametro("@TOTIMP3", IMP4APLA == 1 ? (precio * (IMPU3 / 100)) * p.CANT : 0.00M);
                conexion.baseDatos.AgregarParametro("@TOTIMP4", IMP1APLA == 1 ? (precio * (IMPU4 / 100)) * p.CANT : 0.00M);
                conexion.baseDatos.AgregarParametro("@DESC1",0);
                conexion.baseDatos.AgregarParametro("@DESC2",0);
                conexion.baseDatos.AgregarParametro("@DESC3",0);
                conexion.baseDatos.AgregarParametro("@COMI",0);
                conexion.baseDatos.AgregarParametro("@APAR",0);
                conexion.baseDatos.AgregarParametro("@ACT_INV","S");
                conexion.baseDatos.AgregarParametro("@NUM_ALM",p.NUM_ALM);
                conexion.baseDatos.AgregarParametro("@POLIT_APLI","");
                conexion.baseDatos.AgregarParametro("@TIP_CAM",p.TIP_CAM);
                conexion.baseDatos.AgregarParametro("@UNI_VENTA",p.UNI_VENTA);
                conexion.baseDatos.AgregarParametro("@TIPO_PROD",p.TIPO_PROD);
                conexion.baseDatos.AgregarParametro("@CVE_OBS",0);
                conexion.baseDatos.AgregarParametro("@REG_SERIE",0);
                conexion.baseDatos.AgregarParametro("@E_LTPD", RNLbInventory.RegistrarEnlace(lotes.FirstOrDefault(x => x.partida == p.NUM_PAR).reg_ltpd, lotes.FirstOrDefault(x => x.partida == p.NUM_PAR).contador, numEmpresa));
                conexion.baseDatos.AgregarParametro("@TIPO_ELEM","N");
                conexion.baseDatos.AgregarParametro("@NUM_MOV",RNLbInventory.RegistrarMovInveRemision(p,orden ,lotes.FirstOrDefault(x=> x.partida == p.NUM_PAR).reg_ltpd ,-1,numEmpresa));
                conexion.baseDatos.AgregarParametro("@TOT_PARTIDA",precio* p.CANT);
                conexion.baseDatos.AgregarParametro("@IMPRIMIR","S");
                conexion.baseDatos.AgregarParametro("@UUID","");
                conexion.baseDatos.AgregarParametro("@VERSION_SINC",DateTime.Today);
                conexion.baseDatos.AgregarParametro("@MAN_IEPS",p.MAN_IEPS);
                conexion.baseDatos.AgregarParametro("@APL_MAN_IMP",p.APL_MAN_IMP);
                conexion.baseDatos.AgregarParametro("@CUOTA_IEPS",p.CUOTA_IEPS);
                conexion.baseDatos.AgregarParametro("@APL_MAN_IEPS", p.APL_MAN_IEPS);
                conexion.baseDatos.AgregarParametro("@MTO_PORC", p.MTO_PORC);
                conexion.baseDatos.AgregarParametro("@MTO_CUOTA",p.MTO_CUOTA);
                conexion.baseDatos.AgregarParametro("@CVE_ESQ",p.CVE_ESQ);
                conexion.baseDatos.AgregarParametro("@DESCR_ART", p.DESCR_ART);
                result = conexion.baseDatos.EjecutarSinConsulta("insert into PAR_FACTR{0} (CVE_DOC,NUM_PAR,CVE_ART,CANT,PXS,PREC,COST,IMPU1,IMPU2,IMPU3,IMPU4,IMP1APLA,IMP2APLA,IMP3APLA,IMP4APLA,TOTIMP1,TOTIMP2,TOTIMP3,TOTIMP4,DESC1,DESC2,DESC3,COMI,APAR,ACT_INV,NUM_ALM,POLIT_APLI,TIP_CAM,UNI_VENTA,TIPO_PROD,CVE_OBS,REG_SERIE,E_LTPD,TIPO_ELEM,NUM_MOV,TOT_PARTIDA,IMPRIMIR,UUID,VERSION_SINC,MAN_IEPS,APL_MAN_IMP,CUOTA_IEPS,APL_MAN_IEPS,MTO_PORC,MTO_CUOTA,CVE_ESQ,DESCR_ART) values " +
                    "(@CVE_DOC,@NUM_PAR,@CVE_ART,@CANT,@PXS,@PREC,@COST,@IMPU1,@IMPU2,@IMPU3,@IMPU4,@IMP1APLA,@IMP2APLA,@IMP3APLA,@IMP4APLA,@TOTIMP1,@TOTIMP2,@TOTIMP3,@TOTIMP4,@DESC1,@DESC2,@DESC3,@COMI,@APAR,@ACT_INV,@NUM_ALM,@POLIT_APLI,@TIP_CAM,@UNI_VENTA,@TIPO_PROD,@CVE_OBS,@REG_SERIE,@E_LTPD,@TIPO_ELEM,@NUM_MOV,@TOT_PARTIDA,@IMPRIMIR,@UUID,@VERSION_SINC,@MAN_IEPS,@APL_MAN_IMP,@CUOTA_IEPS,@APL_MAN_IEPS,@MTO_PORC,@MTO_CUOTA,@CVE_ESQ,@DESCR_ART)") > 0;
            bool b = InsertarPartidasCL(cvedoc, p.NUM_PAR, numEmpresa);

                }                                         
                return result;                            
        }


        public static bool InsertarPartidasCL(String cvedoc, int partidas, int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            bool result = false;
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@CVE_DOC", cvedoc);
                conexion.baseDatos.AgregarParametro("@NUM_PAR", partidas);
                result = conexion.baseDatos.EjecutarSinConsulta("insert into PAR_FACTR_CLIB{0} (CLAVE_DOC,NUM_PART) values " +
                    "(@CVE_DOC,@NUM_PAR)") > 0;
            return result;
        }

        public static List<RNPartidasRemision> ObtenerPartidasRemision(String cvedoc, int numEmpresa)
        {
            List<RNPartidasRemision> partidas = new List<RNPartidasRemision>();
            try
            {              
                RNConexion conexion = new RNConexion(numEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@cveOrden", cvedoc);
                var result = conexion.baseDatos.ObtenerTabla("select * from PAR_FACTR{0} where cve_doc =@cveOrden");
                if (result != null && result.Rows.Count > 0)
                {
                    partidas.AddRange(result.AsEnumerable().Select(a => new RNPartidasRemision()
                    {
                        CVE_DOC=a["CVE_DOC"].ToString(),
                        NUM_PAR=Convert.ToInt32(a["NUM_PAR"].ToString()),
                        CVE_ART=a["CVE_ART"].ToString(),
                        CANT= Convert.ToDecimal(a["CANT"].ToString()),
                        PXS= Convert.ToDecimal(a["PXS"].ToString()),
                        PREC=Convert.ToDecimal(a["PREC"].ToString()),
                        COST= Convert.ToDecimal(a["COST"].ToString()),
                        IMPU1=Convert.ToDecimal(a["IMPU1"].ToString()),
                        IMPU2=Convert.ToDecimal(a["IMPU2"].ToString()),
                        IMPU3=Convert.ToDecimal(a["IMPU3"].ToString()),
                        IMPU4= Convert.ToDecimal(a["IMPU4"].ToString()),
                        IMP1APLA=Convert.ToInt32(a["IMP1APLA"].ToString()),
                        IMP2APLA=Convert.ToInt32(a["IMP2APLA"].ToString()),
                        IMP3APLA= Convert.ToInt32(a["IMP3APLA"].ToString()),
                        IMP4APLA = Convert.ToInt32(a["IMP4APLA"].ToString()),
                        TOTIMP1=Convert.ToDecimal(a["TOTIMP1"].ToString()),
                        TOTIMP2=Convert.ToDecimal(a["TOTIMP2"].ToString()),
                        TOTIMP3=Convert.ToDecimal(a["TOTIMP3"].ToString()),
                        TOTIMP4= Convert.ToDecimal(a["TOTIMP4"].ToString()),
                        DESC1=Convert.ToDecimal(a["DESC1"].ToString()),
                        DESC2=Convert.ToDecimal(a["DESC2"].ToString()),
                        DESC3= Convert.ToDecimal(a["DESC3"].ToString()),
                        COMI= Convert.ToDecimal(a["COMI"].ToString()),
                        APAR= Convert.ToDecimal(a["APAR"].ToString()),
                        ACT_INV=a["ACT_INV"].ToString(),
                        NUM_ALM= Convert.ToInt32(a["NUM_ALM"].ToString()),
                        POLIT_APLI=a["POLIT_APLI"].ToString(),
                        TIP_CAM= Convert.ToDecimal(a["TIP_CAM"].ToString()),
                        UNI_VENTA=a["UNI_VENTA"].ToString(),
                        TIPO_PROD=a["TIPO_PROD"].ToString(),
                        CVE_OBS= Convert.ToInt32(a["CVE_OBS"].ToString()),
                        REG_SERIE= Convert.ToInt32(a["REG_SERIE"].ToString()),
                        E_LTPD= Convert.ToInt32(a["E_LTPD"].ToString()),
                        TIPO_ELEM=a["TIPO_ELEM"].ToString(),
                        NUM_MOV= Convert.ToInt32(a["NUM_MOV"].ToString()),
                        TOT_PARTIDA= Convert.ToDecimal(a["TOT_PARTIDA"].ToString()),
                        IMPRIMIR=a["IMPRIMIR"].ToString(),
                        UUID=a["UUID"].ToString(),
                        VERSION_SINC= Convert.ToDateTime(a["VERSION_SINC"].ToString()),
                        MAN_IEPS=a["MAN_IEPS"].ToString(),
                        APL_MAN_IMP= Convert.ToInt32(a["APL_MAN_IMP"].ToString()),
                        CUOTA_IEPS= Convert.ToDecimal(a["CUOTA_IEPS"].ToString()),
                        APL_MAN_IEPS=a["APL_MAN_IEPS"].ToString(),
                        MTO_PORC= Convert.ToDecimal(a["MTO_PORC"].ToString()),
                        MTO_CUOTA= Convert.ToDecimal(a["MTO_CUOTA"].ToString()),
                        CVE_ESQ= Convert.ToInt32(a["CVE_ESQ"].ToString()),
                        DESCR_ART=a["DESCR_ART"].ToString(),
                    }));
                }
                return partidas;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al generar la recepción:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return partidas;
            }
        }

    }
}
