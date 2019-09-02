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
    public class RNOrdenRemision
    {
        public string TIP_DOC = "R";
        public string CVE_DOC        {get; set;}
        public string CVE_CLPV       {get; set;}
        public string STATUS         {get; set;}
        public int DAT_MOSTR         {get; set;}
        public string CVE_VEND       {get; set;}
        public string CVE_PEDI       {get; set;}
        public DateTime FECHA_DOC    {get; set;}
        public DateTime FECHA_ENT    {get; set;}
        public DateTime FECHA_VEN    {get; set;}
        public DateTime? FECHA_CANCELA{get; set;}
        public decimal CAN_TOT { get; set; }
        public decimal IMP_TOT1  {get;set;}
        public decimal IMP_TOT2  {get;set;}
        public decimal IMP_TOT3  {get;set;}
        public decimal IMP_TOT4  {get;set;}
        public decimal DES_TOT   {get;set;}
        public decimal DES_FIN   {get;set;}
        public decimal COM_TOT   {get;set;}
        public string CONDICION  {get;set;}
        public int CVE_OBS = 0; 
        public int NUM_ALMA      {get;set;}
        public string ACT_CXC    {get;set;}
        public string ACT_COI    {get;set;}
        public string ENLAZADO   {get;set;}
        public string TIP_DOC_E  {get;set;}
        public int NUM_MONED     {get;set;}
        public decimal TIPCAMB   {get;set;}
        public int NUM_PAGOS     {get;set;}
        public DateTime FECHAELAB {get;set;}
        public decimal  PRIMERPAGO {get;set;}
        public string RFC { get; set; }
        public int CTLPOL            {get;set;}
        public string ESCFD          {get;set;}
        public int AUTORIZA          {get;set;}
        public string SERIE          {get;set;}
        public int FOLIO             {get;set;}
        public string AUTOANIO       {get;set;}
        public int DAT_ENVIO         {get;set;}
        public string CONTADO        {get;set;}
        public int CVE_BITA          {get;set;}
        public string BLOQ           {get;set;}
        public string FORMAENVIO     {get;set;}
        public decimal DES_FIN_PORC  {get;set;}
        public decimal DES_TOT_PORC  {get;set;}
        public decimal IMPORTE       {get;set;}
        public decimal COM_TOT_PORC  {get;set;}
        public string METODODEPAGO   {get;set;}
        public string NUMCTAPAGO     {get;set;}
        public string TIP_DOC_ANT    {get;set;}
        public string DOC_ANT        {get;set;}
        public string TIP_DOC_SIG    {get;set;}
        public string DOC_SIG        {get;set;}
        public string UUID           {get;set;}
        public DateTime VERSION_SINC {get;set;}
        public string FORMADEPAGOSAT {get;set;}
        public string USO_CFDI       {get;set;}

        public static RNOrdenRemision GenerarRemision(string cvedoc,RNOrdenRecepcion recpecion, int folio, int numEmpresa, List<RNPartidasRecepcion> pRecpcion, List<RNRecepcion.Lotes> lotes, out List<RNPartidasRemision> partidas)
        {
            partidas = new List<RNPartidasRemision>();
            RNOrdenRemision remision = new RNOrdenRemision();
            bool bpartidas;
            if (InsertarRemision(cvedoc,recpecion,folio, numEmpresa,pRecpcion,lotes ,out bpartidas) && bpartidas)
            {
                remision = ObtenerRemision(cvedoc,numEmpresa);
                partidas = RNPartidasRemision.ObtenerPartidasRemision(cvedoc, numEmpresa);
            }
            return remision;
        }

        public static bool InsertarRemision (string cvedoc, RNOrdenRecepcion recpecion, int folio, int numEmpresa, List<RNPartidasRecepcion> pRecpcion, List<RNRecepcion.Lotes> lotes, out bool bpartidas)
        {
            bpartidas = false;
            try
            {
                RNConexion conexion = new RNConexion(numEmpresa);
                //obtenerelclienterecepecion               
                RNCliente cliente=RNLbInventory.ObtenerClienteRemicion(numEmpresa);
                int cve_bita = RNLbInventory.ObtenerBita(cliente.CLAVE, cvedoc, "    3", recpecion.IMPORTE, numEmpresa);
                conexion.baseDatos.AbrirConexion();
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@TIP_DOC","R");
                conexion.baseDatos.AgregarParametro("@CVE_DOC",cvedoc);
                conexion.baseDatos.AgregarParametro("@CVE_CLPV",cliente.CLAVE);
                conexion.baseDatos.AgregarParametro("@STATUS","E");
                conexion.baseDatos.AgregarParametro("@DAT_MOSTR",0);
                conexion.baseDatos.AgregarParametro("@CVE_VEND","");
                conexion.baseDatos.AgregarParametro("@CVE_PEDI","");
                conexion.baseDatos.AgregarParametro("@FECHA_DOC",DateTime.Today);
                conexion.baseDatos.AgregarParametro("@FECHA_ENT",DateTime.Today);
                conexion.baseDatos.AgregarParametro("@FECHA_VEN",DateTime.Today);
                conexion.baseDatos.AgregarParametro("@CAN_TOT",recpecion.CAN_TOT);
                conexion.baseDatos.AgregarParametro("@IMP_TOT1",0.00M);
                conexion.baseDatos.AgregarParametro("@IMP_TOT2",0.00M);
                conexion.baseDatos.AgregarParametro("@IMP_TOT3",0.00M);
                conexion.baseDatos.AgregarParametro("@IMP_TOT4", 0.00M);
                conexion.baseDatos.AgregarParametro("@DES_TOT",0.00M);
                conexion.baseDatos.AgregarParametro("@DES_FIN", 0.00M);
                conexion.baseDatos.AgregarParametro("@COM_TOT",0);
                conexion.baseDatos.AgregarParametro("@CONDICION","");
                conexion.baseDatos.AgregarParametro("@CVE_OBS",0);
                conexion.baseDatos.AgregarParametro("@NUM_ALMA",recpecion.NUM_ALMA);
                conexion.baseDatos.AgregarParametro("@ACT_CXC","S");
                conexion.baseDatos.AgregarParametro("@ACT_COI","N");
                conexion.baseDatos.AgregarParametro("@ENLAZADO","O");
                conexion.baseDatos.AgregarParametro("@TIP_DOC_E","O");
                conexion.baseDatos.AgregarParametro("@NUM_MONED",recpecion.NUM_MONED);
                conexion.baseDatos.AgregarParametro("@TIPCAMB",recpecion.TIPCAMB);
                conexion.baseDatos.AgregarParametro("@NUM_PAGOS",recpecion.NUM_PAGOS);
                conexion.baseDatos.AgregarParametro("@FECHAELAB",DateTime.Today);
                conexion.baseDatos.AgregarParametro("@PRIMERPAGO",0.00);
                conexion.baseDatos.AgregarParametro("@RFC",cliente.RFC);
                conexion.baseDatos.AgregarParametro("@CTLPOL",0);
                conexion.baseDatos.AgregarParametro("@ESCFD","N");
                conexion.baseDatos.AgregarParametro("@AUTORIZA",0);
                conexion.baseDatos.AgregarParametro("@SERIE","");
                conexion.baseDatos.AgregarParametro("@FOLIO",folio);
                conexion.baseDatos.AgregarParametro("@AUTOANIO","");
                conexion.baseDatos.AgregarParametro("@DAT_ENVIO",0);
                conexion.baseDatos.AgregarParametro("@CONTADO","N");
                conexion.baseDatos.AgregarParametro("@CVE_BITA",cve_bita);
                conexion.baseDatos.AgregarParametro("@BLOQ","N");
                conexion.baseDatos.AgregarParametro("@FORMAENVIO","");
                conexion.baseDatos.AgregarParametro("@DES_FIN_PORC",0.000);
                conexion.baseDatos.AgregarParametro("@DES_TOT_PORC",0.000);
                conexion.baseDatos.AgregarParametro("@IMPORTE", 0.00M);
                conexion.baseDatos.AgregarParametro("@COM_TOT_PORC",0.00);
                conexion.baseDatos.AgregarParametro("@METODODEPAGO",null);
                conexion.baseDatos.AgregarParametro("@NUMCTAPAGO",null);
                conexion.baseDatos.AgregarParametro("@TIP_DOC_ANT","");
                conexion.baseDatos.AgregarParametro("@DOC_ANT","");
                conexion.baseDatos.AgregarParametro("@TIP_DOC_SIG",null);
                conexion.baseDatos.AgregarParametro("@DOC_SIG",null);
                conexion.baseDatos.AgregarParametro("@UUID",null);//obtener el uuid
                conexion.baseDatos.AgregarParametro("@VERSION_SINC",DateTime.Today);
                conexion.baseDatos.AgregarParametro("@FORMADEPAGOSAT",null);
                conexion.baseDatos.AgregarParametro("@USO_CFDI",null);
                var result = conexion.baseDatos.EjecutarSinConsulta(" INSERT INTO FACTR{0} (TIP_DOC,CVE_DOC,CVE_CLPV,STATUS,DAT_MOSTR,CVE_VEND,CVE_PEDI,FECHA_DOC,FECHA_ENT,FECHA_VEN,CAN_TOT,IMP_TOT1,IMP_TOT2,IMP_TOT3,IMP_TOT4,DES_TOT,DES_FIN,COM_TOT,CONDICION,CVE_OBS,NUM_ALMA,ACT_CXC,ACT_COI,ENLAZADO,TIP_DOC_E,NUM_MONED,TIPCAMB,NUM_PAGOS,FECHAELAB,PRIMERPAGO,RFC,CTLPOL,ESCFD,AUTORIZA,SERIE,FOLIO,AUTOANIO,DAT_ENVIO,CONTADO,CVE_BITA,BLOQ,FORMAENVIO,DES_FIN_PORC,DES_TOT_PORC,IMPORTE,COM_TOT_PORC,METODODEPAGO,NUMCTAPAGO,TIP_DOC_ANT,DOC_ANT,TIP_DOC_SIG,DOC_SIG,UUID,VERSION_SINC,FORMADEPAGOSAT,USO_CFDI) " +
                    "VALUES (@TIP_DOC,@CVE_DOC,@CVE_CLPV,@STATUS,@DAT_MOSTR,@CVE_VEND,@CVE_PEDI,@FECHA_DOC,@FECHA_ENT,@FECHA_VEN,@CAN_TOT,@IMP_TOT1,@IMP_TOT2,@IMP_TOT3,@IMP_TOT4,@DES_TOT,@DES_FIN,@COM_TOT,@CONDICION,@CVE_OBS,@NUM_ALMA,@ACT_CXC,@ACT_COI,@ENLAZADO,@TIP_DOC_E,@NUM_MONED,@TIPCAMB,@NUM_PAGOS,@FECHAELAB,@PRIMERPAGO,@RFC,@CTLPOL,@ESCFD,@AUTORIZA,@SERIE,@FOLIO,@AUTOANIO,@DAT_ENVIO,@CONTADO,@CVE_BITA,@BLOQ,@FORMAENVIO,@DES_FIN_PORC,@DES_TOT_PORC,@IMPORTE,@COM_TOT_PORC,@METODODEPAGO,@NUMCTAPAGO,@TIP_DOC_ANT,@DOC_ANT,@TIP_DOC_SIG,@DOC_SIG,@UUID,@VERSION_SINC,@FORMADEPAGOSAT,@USO_CFDI)", CommandType.Text) > 0;
                // agregar los campos libres 
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@CVE_DOC", cvedoc);
                var resultcl = conexion.baseDatos.EjecutarSinConsulta(" insert into FACTR_CLIB{0}(CLAVE_DOC) values(@CVE_DOC); ", CommandType.Text) > 0;
                //agregar las partidas
                decimal cant_total, imp1, imp2, imp3, imp4, total, desc, descind, desfin;
                bpartidas = RNPartidasRemision.GenerarPartidas(ObtenerRemision(cvedoc, numEmpresa) ,cvedoc,  folio,  numEmpresa, pRecpcion, lotes ,out cant_total, out imp1, out imp2, out imp3, out imp4, out total, out desc, out descind, out desfin);
                conexion.baseDatos.LimpiarParametros();
                conexion.baseDatos.AgregarParametro("@IMP_TOT1", imp1);
                conexion.baseDatos.AgregarParametro("@IMP_TOT2", imp2);
                conexion.baseDatos.AgregarParametro("@IMP_TOT3", imp3);
                conexion.baseDatos.AgregarParametro("@IMP_TOT4", imp4);
                conexion.baseDatos.AgregarParametro("@IMPORTE", total);
                conexion.baseDatos.AgregarParametro("@CVE_DOC", cvedoc);
                conexion.baseDatos.AgregarParametro("@CAN_TOT", cant_total);
                result = conexion.baseDatos.EjecutarSinConsulta("update FACTR{0} set  IMP_TOT1=@IMP_TOT1 ,IMP_TOT2=@IMP_TOT2,IMP_TOT3=@IMP_TOT3,IMP_TOT4=@IMP_TOT4,IMPORTE=@IMPORTE, CAN_TOT=@CAN_TOT " +
                   "where CVE_DOC = @CVE_DOC", CommandType.Text) > 0;


                return result;
            }
            catch (Exception e)
            {
                
                return false;
            }
        }

        public static RNOrdenRemision ObtenerRemision(String cvedoc, int numEmpresa)
        {
            RNOrdenRemision remision = new RNOrdenRemision();
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@cveOrden", cvedoc);
            var result = conexion.baseDatos.ObtenerTabla("select * from FACTR{0} where cve_doc =@cveOrden");
            foreach (DataRow row in result.Rows)
            {
                remision.TIP_DOC=row["TIP_DOC"].ToString();
                remision.CVE_DOC=row["CVE_DOC"].ToString();
                remision.CVE_CLPV=row["CVE_CLPV"].ToString();
                remision.STATUS=row["STATUS"].ToString();
                remision.DAT_MOSTR=Convert.ToInt32(row["DAT_MOSTR"].ToString());
                remision.CVE_VEND=row["CVE_VEND"].ToString();
                remision.CVE_PEDI=row["CVE_PEDI"].ToString();
                remision.FECHA_DOC=Convert.ToDateTime(row["FECHA_DOC"].ToString());
                remision.FECHA_ENT=Convert.ToDateTime(row["FECHA_ENT"].ToString());
                remision.FECHA_VEN= Convert.ToDateTime(row["FECHA_VEN"].ToString());
                remision.FECHA_CANCELA= null;
                remision.CAN_TOT= Convert.ToDecimal(row["CAN_TOT"].ToString());
                remision.IMP_TOT1=Convert.ToDecimal(row["IMP_TOT1"].ToString());
                remision.IMP_TOT2=Convert.ToDecimal(row["IMP_TOT2"].ToString());
                remision.IMP_TOT3=Convert.ToDecimal(row["IMP_TOT3"].ToString());
                remision.IMP_TOT4= Convert.ToDecimal(row["IMP_TOT4"].ToString());
                remision.DES_TOT=Convert.ToDecimal(row["DES_TOT"].ToString());
                remision.DES_FIN=Convert.ToDecimal(row["DES_FIN"].ToString());
                remision.COM_TOT= Convert.ToDecimal(row["COM_TOT"].ToString());
                remision.CONDICION=row["CONDICION"].ToString();
                remision.CVE_OBS= Convert.ToInt32(row["CVE_OBS"].ToString());
                remision.NUM_ALMA= Convert.ToInt32(row["NUM_ALMA"].ToString());
                remision.ACT_CXC=row["ACT_CXC"].ToString();
                remision.ACT_COI=row["ACT_COI"].ToString();
                remision.ENLAZADO=row["ENLAZADO"].ToString();
                remision.TIP_DOC_E=row["TIP_DOC_E"].ToString();
                remision.NUM_MONED= Convert.ToInt32(row["NUM_MONED"].ToString());
                remision.TIPCAMB= Convert.ToDecimal(row["TIPCAMB"].ToString());
                remision.NUM_PAGOS= Convert.ToInt32(row["NUM_PAGOS"].ToString());
                remision.FECHAELAB= Convert.ToDateTime(row["FECHAELAB"].ToString());
                remision.PRIMERPAGO= Convert.ToDecimal(row["PRIMERPAGO"].ToString());
                remision.RFC=row["RFC"].ToString();
                remision.CTLPOL= Convert.ToInt32(row["CTLPOL"].ToString());
                remision.ESCFD=row["ESCFD"].ToString();
                remision.AUTORIZA= Convert.ToInt32(row["AUTORIZA"].ToString());
                remision.SERIE=row["SERIE"].ToString();
                remision.FOLIO= Convert.ToInt32(row["FOLIO"].ToString());
                remision.AUTOANIO=row["AUTOANIO"].ToString();
                remision.DAT_ENVIO= Convert.ToInt32(row["DAT_ENVIO"].ToString());
                remision.CONTADO=row["CONTADO"].ToString();
                remision.CVE_BITA= Convert.ToInt32(row["CVE_BITA"].ToString());
                remision.BLOQ=row["BLOQ"].ToString();
                remision.FORMAENVIO=row["FORMAENVIO"].ToString();
                remision.DES_FIN_PORC=Convert.ToDecimal(row["DES_FIN_PORC"].ToString());
                remision.DES_TOT_PORC= Convert.ToDecimal(row["DES_TOT_PORC"].ToString());
                remision.IMPORTE= Convert.ToDecimal(row["IMPORTE"].ToString());
                remision.COM_TOT_PORC= Convert.ToDecimal(row["COM_TOT_PORC"].ToString());
                remision.METODODEPAGO=row["METODODEPAGO"].ToString();
                remision.NUMCTAPAGO=row["NUMCTAPAGO"].ToString();
                remision.TIP_DOC_ANT=row["TIP_DOC_ANT"].ToString();
                remision.DOC_ANT=row["DOC_ANT"].ToString();
                remision.TIP_DOC_SIG=row["TIP_DOC_SIG"].ToString();
                remision.DOC_SIG=row["DOC_SIG"].ToString();
                remision.UUID=row["UUID"].ToString();
                remision.VERSION_SINC= Convert.ToDateTime(row["VERSION_SINC"].ToString());
                remision.FORMADEPAGOSAT=row["FORMADEPAGOSAT"].ToString();
                remision.USO_CFDI=row["USO_CFDI"].ToString();
               
            } // actualizar la orden de compra
            return remision;
        }
    }
}













































































































































