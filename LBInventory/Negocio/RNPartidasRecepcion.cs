using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Windows.Forms;
using System.Data;
using Umbrella.Aspel.Sae.Negocio;
using Umbrella.Aspel.Sae.Datos;
using Umbrella.Aspel.Sae.Datos.Entidades;
using static Negocio.RNRecepcion;

namespace Negocio
{
    public class RNPartidasRecepcion
    {
        public string CVE_DOC { get; set; }
        public int NUM_PAR { get; set; }
         public string CVE_ART     { get; set; }
        public decimal CANT { get; set; }
        public decimal PXR         { get; set; }
        public decimal PREC        { get; set; }
        public decimal COST        { get; set; }
        public decimal IMPU1       { get; set; }
        public decimal IMPU2       { get; set; }
        public decimal IMPU3       { get; set; }
        public decimal IMPU4       { get; set; }
        public int IMP1APLA    { get; set; }
        public int IMP2APLA    { get; set; }
        public int IMP3APLA    { get; set; }
        public int IMP4APLA    { get; set; }
        public decimal TOTIMP1     { get; set; }
        public decimal TOTIMP2     { get; set; }
        public decimal TOTIMP3     { get; set; }
        public decimal TOTIMP4     { get; set; }
        public decimal DESCU       { get; set; }
        public string ACT_INV     { get; set; }
        public decimal TIP_CAM     { get; set; }
        public string UNI_VENTA   { get; set; }
        public string TIPO_ELEM   { get; set; }
        public string TIPO_PROD   { get; set; }
        public int? CVE_OBS     { get; set; }
        public int E_LTPD      { get; set; }
        public int REG_SERIE   { get; set; }
        public decimal FACTCONV    { get; set; }
        public decimal COST_DEV    { get; set; }
        public int NUM_ALM     { get; set; }
        public decimal MINDIRECTO { get; set; }
        public int NUM_MOV { get; set; }
        public decimal TOT_PARTIDA { get; set; }
        public string MAN_IEPS    { get; set; }
        public int APL_MAN_IMP { get; set; }
        public decimal CUOTA_IEPS  { get; set; }
        public string APL_MAN_IEPS{ get; set; }
        public decimal? MTO_PORC    { get; set; }
        public decimal? MTO_CUOTA   { get; set; }
        public int CVE_ESQ     { get; set; }
        public string DESCR_ART   { get; set; }


        public List<RNPartidasRecepcion> GenerarPartidas (string cveOrden, string cveDoc, int folio, int numEmpresa)
        {
            try
            {
                List<RNPartidasRecepcion> partidas = new List<RNPartidasRecepcion>();
                RNConexion conexion = new RNConexion(numEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@cveOrden", cveOrden);
                var result = conexion.baseDatos.ObtenerTabla("select * from par_compo{0} where cve_doc =@cveOrden");
                if (result != null && result.Rows.Count > 0)
                {
                    partidas.AddRange(result.AsEnumerable().Select(a => new RNPartidasRecepcion() {
                        CVE_DOC = cveDoc,
                        NUM_PAR = Convert.ToInt32(a["NUM_PAR"]),
                        CVE_ART = a["CVE_ART"].ToString(),
                        CANT = Convert.ToDecimal(a["CANT"]),
                        PXR = Convert.ToDecimal(a["CANT"]),
                        PREC = Convert.ToDecimal(a["PREC"]),
                        COST = Convert.ToDecimal(a["COST"]),
                        IMPU1 = Convert.ToDecimal(a["IMPU1"]),
                        IMPU2 = Convert.ToDecimal(a["IMPU2"]),
                        IMPU3 = Convert.ToDecimal(a["IMPU3"]),
                        IMPU4 = Convert.ToDecimal(a["IMPU4"]),
                        IMP1APLA = Convert.ToInt32(a["IMP1APLA"]),
                        IMP2APLA = Convert.ToInt32(a["IMP2APLA"]),
                        IMP3APLA = Convert.ToInt32(a["IMP3APLA"]),
                        IMP4APLA = Convert.ToInt32(a["IMP4APLA"]),
                        TOTIMP1 = Convert.ToDecimal(a["TOTIMP1"]),
                        TOTIMP2 = Convert.ToDecimal(a["TOTIMP2"]),
                        TOTIMP3 = Convert.ToDecimal(a["TOTIMP3"]),
                        TOTIMP4 = Convert.ToDecimal(a["TOTIMP4"]),
                        DESCU = Convert.ToDecimal(a["DESCU"]),
                        ACT_INV ="S",
                        TIP_CAM = Convert.ToDecimal(a["TIP_CAM"]),
                        UNI_VENTA = a["UNI_VENTA"].ToString(),
                        TIPO_ELEM = a["TIPO_ELEM"].ToString(),
                        TIPO_PROD = a["TIPO_PROD"].ToString(),
                        CVE_OBS = 0,
                        E_LTPD = Convert.ToInt32(a["E_LTPD"]),
                        REG_SERIE = Convert.ToInt32(a["REG_SERIE"]),
                        FACTCONV = Convert.ToDecimal(a["FACTCONV"]),
                        COST_DEV = Convert.ToDecimal(a["COST"]),
                        NUM_ALM = Convert.ToInt32(a["NUM_ALM"]),
                        MINDIRECTO = Convert.ToDecimal(a["MINDIRECTO"]),
                        NUM_MOV = Convert.ToInt32(a["NUM_MOV"]),
                        TOT_PARTIDA = Convert.ToDecimal(a["TOT_PARTIDA"]),
                        MAN_IEPS = a["MAN_IEPS"].ToString(),
                        APL_MAN_IMP = Convert.ToInt32(a["APL_MAN_IMP"]),
                        CUOTA_IEPS = Convert.ToDecimal(a["CUOTA_IEPS"]),
                        APL_MAN_IEPS = a["APL_MAN_IEPS"].ToString(),
                        MTO_PORC = Convert.ToDecimal(a["MTO_PORC"]),
                        MTO_CUOTA = Convert.ToDecimal(a["MTO_CUOTA"]),
                        CVE_ESQ = Convert.ToInt32(a["CVE_ESQ"]),
                        DESCR_ART = a["DESCR_ART"].ToString()
                    }));
                }
                // generar metodo para insertar las partidas
                InsertarPartidas(partidas,numEmpresa);
                //generar metodo para insertar los campos libres
                InsertarPartidasCL(partidas, numEmpresa);
                return partidas;
            }
            catch(Exception e)
                {
                MessageBox.Show("Error al generar la recepción:  " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                List<RNPartidasRecepcion> partidas = new List<RNPartidasRecepcion>();
                return partidas;
            }
        }

        public bool InsertarPartidas(List<RNPartidasRecepcion>  partidas,int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            bool result = false;
            foreach (var p in partidas)
            {
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@CVE_DOC", p.CVE_DOC);
                conexion.baseDatos.AgregarParametro("@NUM_PAR", p.NUM_PAR);
                conexion.baseDatos.AgregarParametro("@CVE_ART", p.CVE_ART);
                conexion.baseDatos.AgregarParametro("@CANT", p.CANT);
                conexion.baseDatos.AgregarParametro("@PXR", p.PXR);
                conexion.baseDatos.AgregarParametro("@PREC", p.PREC);
                conexion.baseDatos.AgregarParametro("@COST", p.COST);
                conexion.baseDatos.AgregarParametro("@IMPU1", p.IMPU1);
                conexion.baseDatos.AgregarParametro("@IMPU2", p.IMPU2);
                conexion.baseDatos.AgregarParametro("@IMPU3", p.IMPU3);
                conexion.baseDatos.AgregarParametro("@IMPU4", p.IMPU4);
                conexion.baseDatos.AgregarParametro("@IMP1APLA", p.IMP1APLA);
                conexion.baseDatos.AgregarParametro("@IMP2APLA", p.IMP2APLA);
                conexion.baseDatos.AgregarParametro("@IMP3APLA", p.IMP3APLA);
                conexion.baseDatos.AgregarParametro("@IMP4APLA", p.IMP4APLA);
                conexion.baseDatos.AgregarParametro("@TOTIMP1", p.TOTIMP1);
                conexion.baseDatos.AgregarParametro("@TOTIMP2", p.TOTIMP2);
                conexion.baseDatos.AgregarParametro("@TOTIMP3", p.TOTIMP3);
                conexion.baseDatos.AgregarParametro("@TOTIMP4", p.TOTIMP4);
                conexion.baseDatos.AgregarParametro("@DESCU", p.DESCU);
                conexion.baseDatos.AgregarParametro("@ACT_INV", p.ACT_INV);
                conexion.baseDatos.AgregarParametro("@TIP_CAM", p.TIP_CAM );
                conexion.baseDatos.AgregarParametro("@UNI_VENTA", p.UNI_VENTA);
                conexion.baseDatos.AgregarParametro("@TIPO_ELEM", p.TIPO_ELEM);
                conexion.baseDatos.AgregarParametro("@TIPO_PROD", p.TIPO_PROD);
                conexion.baseDatos.AgregarParametro("@CVE_OBS", p.CVE_OBS);
                conexion.baseDatos.AgregarParametro("@E_LTPD", p.E_LTPD);
                conexion.baseDatos.AgregarParametro("@REG_SERIE", p.REG_SERIE);
                conexion.baseDatos.AgregarParametro("@FACTCONV", p.FACTCONV);
                conexion.baseDatos.AgregarParametro("@COST_DEV", p.COST_DEV);
                conexion.baseDatos.AgregarParametro("@NUM_ALM", p.NUM_ALM);
                conexion.baseDatos.AgregarParametro("@MINDIRECTO", p.MINDIRECTO);
                conexion.baseDatos.AgregarParametro("@NUM_MOV", p.NUM_MOV);
                conexion.baseDatos.AgregarParametro("@TOT_PARTIDA", p.TOT_PARTIDA);
                conexion.baseDatos.AgregarParametro("@MAN_IEPS", p.MAN_IEPS);
                conexion.baseDatos.AgregarParametro("@APL_MAN_IMP", p.APL_MAN_IMP);
                conexion.baseDatos.AgregarParametro("@CUOTA_IEPS", p.CUOTA_IEPS);
                conexion.baseDatos.AgregarParametro("@APL_MAN_IEPS", p.APL_MAN_IEPS);
                conexion.baseDatos.AgregarParametro("@MTO_PORC", p.MTO_PORC);
                conexion.baseDatos.AgregarParametro("@MTO_CUOTA", p.MTO_CUOTA);
                conexion.baseDatos.AgregarParametro("@CVE_ESQ", p.CVE_ESQ);
                conexion.baseDatos.AgregarParametro("@DESCR_ART", p.DESCR_ART);
                result = conexion.baseDatos.EjecutarSinConsulta("insert into PAR_COMPR{0} (CVE_DOC,NUM_PAR,CVE_ART,CANT,PXR,PREC,COST,IMPU1,IMPU2,IMPU3,IMPU4,IMP1APLA,IMP2APLA,IMP3APLA,IMP4APLA,TOTIMP1,TOTIMP2,TOTIMP3,TOTIMP4,DESCU,ACT_INV,TIP_CAM,UNI_VENTA,TIPO_ELEM,TIPO_PROD,CVE_OBS,E_LTPD,REG_SERIE,FACTCONV,COST_DEV,NUM_ALM,MINDIRECTO,NUM_MOV,TOT_PARTIDA,MAN_IEPS,APL_MAN_IMP,CUOTA_IEPS,APL_MAN_IEPS,MTO_PORC,MTO_CUOTA,CVE_ESQ,DESCR_ART) values " +
                    "(@CVE_DOC,@NUM_PAR,@CVE_ART,@CANT,@PXR,@PREC,@COST,@IMPU1,@IMPU2,@IMPU3,@IMPU4,@IMP1APLA,@IMP2APLA,@IMP3APLA,@IMP4APLA,@TOTIMP1,@TOTIMP2,@TOTIMP3,@TOTIMP4,@DESCU,@ACT_INV,@TIP_CAM,@UNI_VENTA,@TIPO_ELEM,@TIPO_PROD,@CVE_OBS,@E_LTPD,@REG_SERIE,@FACTCONV,@COST_DEV,@NUM_ALM,@MINDIRECTO,@NUM_MOV,@TOT_PARTIDA,@MAN_IEPS,@APL_MAN_IMP,@CUOTA_IEPS,@APL_MAN_IEPS,@MTO_PORC,@MTO_CUOTA,@CVE_ESQ,@DESCR_ART)") > 0;
            }
            return result;
        }

        public bool InsertarPartidas(List<RNPartidasRecepcion>  partidas,string cveDoc,int folio, List<Lotes> lotes, int numEmpresa, out decimal cant_total, out decimal imp1, out decimal imp2, out decimal imp3, out decimal imp4, out decimal total, out decimal desc, out decimal descind, out decimal desfin)
        {
            cant_total = 0;
            imp1 = 0;
            imp2 = 0;
            imp3 = 0;
            imp4 = 0;
            total = 0;
            desc = 0;
            descind = 0;
            desfin = 0;
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            bool result = false;
            foreach (var p in partidas)
            {
                RNProducto prod = RNProducto.ObtenerProductoPorClave(numEmpresa,p.CVE_ART);
                decimal precio = RNProducto.ObtenerPrecioProducto(p.CVE_ART,1,numEmpresa);
                decimal IMPU1 = 0.0M;
                decimal IMPU2 = 0.0M;
                decimal IMPU3 = 0.0M;
                decimal IMPU4 = 0.0M;
                int IMP1APLA = 0;
                int IMP2APLA = 0;
                int IMP3APLA = 0;
                int IMP4APLA = 0;
                if (prod.CVE_ESQIMPU.Value > 0 && prod.CVE_ESQIMPU != null)
                RNProducto.ObtenerImpuestos(p.CVE_ART,prod.CVE_ESQIMPU.Value, numEmpresa,out IMPU1,out IMPU2, out IMPU3, out IMPU4, out IMP1APLA, out IMP2APLA,out IMP3APLA, out IMP4APLA);
                cant_total += (prod.ULT_COSTO.Value * p.CANT);
                imp1 += prod.ULT_COSTO.Value * (IMPU1 / 100)* p.CANT;
                imp2 += prod.ULT_COSTO.Value * (IMPU2 / 100)* p.CANT;
                imp3 += prod.ULT_COSTO.Value * (IMPU3 / 100)* p.CANT;
                imp4 += prod.ULT_COSTO.Value * (IMPU4 / 100) * p.CANT;
                total += (prod.ULT_COSTO.Value * p.CANT)+imp1+imp2+imp3+imp4;
                desc += 0;
                descind += 0;
                desfin += 0;
                conexion.baseDatos.LimpiarParametros();                                                                        
                conexion.baseDatos.AgregarParametro("@CVE_DOC", p.CVE_DOC);                                            
                conexion.baseDatos.AgregarParametro("@NUM_PAR", p.NUM_PAR);                                 
                conexion.baseDatos.AgregarParametro("@CVE_ART", p.CVE_ART);
                conexion.baseDatos.AgregarParametro("@CANT", p.CANT);
                conexion.baseDatos.AgregarParametro("@PXR", p.PXR);
                conexion.baseDatos.AgregarParametro("@PREC", precio);
                conexion.baseDatos.AgregarParametro("@COST",prod.ULT_COSTO);
                conexion.baseDatos.AgregarParametro("@IMPU1",IMPU1);
                conexion.baseDatos.AgregarParametro("@IMPU2",IMPU2 );
                conexion.baseDatos.AgregarParametro("@IMPU3",IMPU3 );
                conexion.baseDatos.AgregarParametro("@IMPU4",IMPU4 );
                conexion.baseDatos.AgregarParametro("@IMP1APLA", IMP1APLA);
                conexion.baseDatos.AgregarParametro("@IMP2APLA", IMP2APLA);
                conexion.baseDatos.AgregarParametro("@IMP3APLA", IMP3APLA);
                conexion.baseDatos.AgregarParametro("@IMP4APLA", IMP4APLA);
                conexion.baseDatos.AgregarParametro("@TOTIMP2", IMP2APLA==1?(prod.ULT_COSTO*(IMPU1/100)) * p.CANT : 0.00M);
                conexion.baseDatos.AgregarParametro("@TOTIMP3", IMP3APLA==1?(prod.ULT_COSTO*(IMPU2/100)) * p.CANT : 0.00M);
                conexion.baseDatos.AgregarParametro("@TOTIMP4", IMP4APLA==1?(prod.ULT_COSTO*(IMPU3/100)) * p.CANT : 0.00M);
                conexion.baseDatos.AgregarParametro("@TOTIMP1", IMP1APLA==1?(prod.ULT_COSTO*(IMPU4/100)) * p.CANT : 0.00M);
                conexion.baseDatos.AgregarParametro("@DESCU", p.DESCU);
                conexion.baseDatos.AgregarParametro("@ACT_INV", p.ACT_INV);
                conexion.baseDatos.AgregarParametro("@TIP_CAM", p.TIP_CAM );
                conexion.baseDatos.AgregarParametro("@UNI_VENTA", p.UNI_VENTA);
                conexion.baseDatos.AgregarParametro("@TIPO_ELEM", p.TIPO_ELEM);
                conexion.baseDatos.AgregarParametro("@TIPO_PROD", p.TIPO_PROD);
                conexion.baseDatos.AgregarParametro("@CVE_OBS", p.CVE_OBS);
                conexion.baseDatos.AgregarParametro("@E_LTPD", RNLbInventory.RegistrarEnlace(lotes.FirstOrDefault(x => x.partida == p.NUM_PAR).reg_ltpd, lotes.FirstOrDefault(x => x.partida == p.NUM_PAR).contador, numEmpresa));
                conexion.baseDatos.AgregarParametro("@REG_SERIE", p.REG_SERIE);
                conexion.baseDatos.AgregarParametro("@FACTCONV", p.FACTCONV);
                conexion.baseDatos.AgregarParametro("@COST_DEV", prod.ULT_COSTO);
                conexion.baseDatos.AgregarParametro("@NUM_ALM", p.NUM_ALM);
                conexion.baseDatos.AgregarParametro("@MINDIRECTO", p.MINDIRECTO);
                conexion.baseDatos.AgregarParametro("@NUM_MOV", RNLbInventory.RegistrarMovInveRemision(p,RNOrdenRemision.ObtenerRemision(  cveDoc,numEmpresa), lotes.FirstOrDefault(x => x.partida == p.NUM_PAR).reg_ltpd, 1, numEmpresa));
                conexion.baseDatos.AgregarParametro("@TOT_PARTIDA",prod.ULT_COSTO * p.CANT);
                conexion.baseDatos.AgregarParametro("@MAN_IEPS", p.MAN_IEPS);
                conexion.baseDatos.AgregarParametro("@APL_MAN_IMP", p.APL_MAN_IMP);
                conexion.baseDatos.AgregarParametro("@CUOTA_IEPS", p.CUOTA_IEPS);
                conexion.baseDatos.AgregarParametro("@APL_MAN_IEPS", p.APL_MAN_IEPS);
                conexion.baseDatos.AgregarParametro("@MTO_PORC", p.MTO_PORC);
                conexion.baseDatos.AgregarParametro("@MTO_CUOTA", p.MTO_CUOTA);
                conexion.baseDatos.AgregarParametro("@CVE_ESQ", p.CVE_ESQ);
                conexion.baseDatos.AgregarParametro("@DESCR_ART", p.DESCR_ART);
                result = conexion.baseDatos.EjecutarSinConsulta("insert into PAR_COMPR{0} (CVE_DOC,NUM_PAR,CVE_ART,CANT,PXR,PREC,COST,IMPU1,IMPU2,IMPU3,IMPU4,IMP1APLA,IMP2APLA,IMP3APLA,IMP4APLA,TOTIMP1,TOTIMP2,TOTIMP3,TOTIMP4,DESCU,ACT_INV,TIP_CAM,UNI_VENTA,TIPO_ELEM,TIPO_PROD,CVE_OBS,E_LTPD,REG_SERIE,FACTCONV,COST_DEV,NUM_ALM,MINDIRECTO,NUM_MOV,TOT_PARTIDA,MAN_IEPS,APL_MAN_IMP,CUOTA_IEPS,APL_MAN_IEPS,MTO_PORC,MTO_CUOTA,CVE_ESQ,DESCR_ART) values " +
                    "(@CVE_DOC,@NUM_PAR,@CVE_ART,@CANT,@PXR,@PREC,@COST,@IMPU1,@IMPU2,@IMPU3,@IMPU4,@IMP1APLA,@IMP2APLA,@IMP3APLA,@IMP4APLA,@TOTIMP1,@TOTIMP2,@TOTIMP3,@TOTIMP4,@DESCU,@ACT_INV,@TIP_CAM,@UNI_VENTA,@TIPO_ELEM,@TIPO_PROD,@CVE_OBS,@E_LTPD,@REG_SERIE,@FACTCONV,@COST_DEV,@NUM_ALM,@MINDIRECTO,@NUM_MOV,@TOT_PARTIDA,@MAN_IEPS,@APL_MAN_IMP,@CUOTA_IEPS,@APL_MAN_IEPS,@MTO_PORC,@MTO_CUOTA,@CVE_ESQ,@DESCR_ART)") > 0;
            }
            bool abn = InsertarPartidasCL(partidas,numEmpresa);
            return result;
        }

        public bool InsertarPartidasCL(List<RNPartidasRecepcion>  partidas,int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            bool result = false;
            foreach (var p in partidas)
            {
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@CVE_DOC", p.CVE_DOC);
                conexion.baseDatos.AgregarParametro("@NUM_PAR", p.NUM_PAR);               
                result = conexion.baseDatos.EjecutarSinConsulta("insert into PAR_COMPR_CLIB{0} (CLAVE_DOC,NUM_PART) values " +
                    "(@CVE_DOC,@NUM_PAR)") > 0;
            }
            return result;
        }

        public static bool ActualizarPartida(string orden,int num_partida, int reg_ltpd_Enlace, int minve_id,int numEmpresa)
        {
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@CVE_DOC", orden);
            conexion.baseDatos.AgregarParametro("@NUM_PAR", num_partida);
            conexion.baseDatos.AgregarParametro("@E_LTPD", reg_ltpd_Enlace);
            conexion.baseDatos.AgregarParametro("@NUM_MOV", minve_id);
            return conexion.baseDatos.EjecutarSinConsulta("update PAR_COMPR{0} set E_LTPD=@E_LTPD, NUM_MOV=@NUM_MOV    where CVE_DOC =@CVE_DOC and NUM_PAR= @NUM_PAR", CommandType.Text) > 0;
        }

    }
}
