using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class RNCliente
    {
        public string CLAVE { get; set; }
        public string STATUS { get; set; }
        public string NOMBRE { get; set; }
        public string RFC { get; set; }
        public string CALLE { get; set; }
        public string NUMINT { get; set; }
        public string NUMEXT { get; set; }
        public string CRUZAMIENTOS { get; set; }
        public string CRUZAMIENTOS2 { get; set; }
        public string COLONIA { get; set; }
        public string CODIGO { get; set; }
        public string LOCALIDAD { get; set; }
        public string MUNICIPIO { get; set; }
        public string ESTADO { get; set; }
        public string PAIS { get; set; }
        public string NACIONALIDAD { get; set; }
        public string REFERDIR { get; set; }
        public string TELEFONO { get; set; }
        public string CLASIFIC { get; set; }
        public string FAX { get; set; }
        public string PAG_WEB { get; set; }
        public string CURP { get; set; }
        public string CVE_ZONA { get; set; }
        public string IMPRIR { get; set; }
        public string MAIL { get; set; }
        public int NIVELSEC { get; set; }
        public string ENVIOSILEN { get; set; }
        public string EMAILPRED { get; set; }
        public string DIAREV { get; set; }
        public string DIAPAGO { get; set; }
        public string CON_CREDITO { get; set; }
        public int DIASCRED { get; set; }
        public decimal LIMCRED { get; set; }
        public decimal SALDO { get; set; }
        public int LISTA_PREC { get; set; }
        public int CVE_BITA { get; set; }
        public string ULT_PAGOD { get; set; }
        public decimal ULT_PAGOM { get; set; }
        public DateTime ULT_PAGOF { get; set; }
        public decimal DESCUENTO { get; set; }
        public string ULT_VENTAD { get; set; }
        public decimal ULT_COMPM { get; set; }
        public DateTime FCH_ULTCOM { get; set; }
        public decimal VENTAS { get; set; }
        public string CVE_VEND { get; set; }
        public int CVE_OBS { get; set; }
        public string TIPO_EMPRESA { get; set; }
        public string MATRIZ { get; set; }
        public string PROSPECTO { get; set; }
        public string CALLE_ENVIO { get; set; }
        public string NUMINT_ENVIO { get; set; }
        public string NUMEXT_ENVIO { get; set; }
        public string CRUZAMIENTOS_ENVIO { get; set; }
        public string CRUZAMIENTOS_ENVIO2 { get; set; }
        public string COLONIA_ENVIO { get; set; }
        public string LOCALIDAD_ENVIO { get; set; }
        public string MUNICIPIO_ENVIO { get; set; }
        public string ESTADO_ENVIO { get; set; }
        public string PAIS_ENVIO { get; set; }
        public string CODIGO_ENVIO { get; set; }
        public string CVE_ZONA_ENVIO { get; set; }
        public string REFERENCIA_ENVIO { get; set; }
        public string CUENTA_CONTABLE { get; set; }
        public string ADDENDAF { get; set; }
        public string ADDENDAD { get; set; }
        public string NAMESPACE { get; set; }
        public string METODODEPAGO { get; set; }
        public string NUMCTAPAGO { get; set; }
        public string MODELO { get; set; }
        public string DES_IMPU1 { get; set; }
        public string DES_IMPU2 { get; set; }
        public string DES_IMPU3 { get; set; }
        public string DES_IMPU4 { get; set; }
        public string DES_PER { get; set; }
        public decimal LAT_GENERAL { get; set; }
        public decimal LON_GENERAL { get; set; }
        public decimal LAT_ENVIO { get; set; }
        public decimal LON_ENVIO { get; set; }
        public string UUID { get; set; }
        public DateTime VERSION_SINC { get; set; }
        public string USO_CFDI { get; set; }
        public string CVE_PAIS_SAT { get; set; }
        public string NUMIDREGFISCAL { get; set; }
        public string FORMADEPAGOSAT { get; set; }
        public string ADDENDAG { get; set; }
        public string ADDENDAE { get; set; }

        public static RNCliente ObtenerCliente(string cveCliente, int numEmpresa)
        {
            RNCliente cliente = new RNCliente();
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@cveCliente", cveCliente);
            var result = conexion.baseDatos.ObtenerTabla("select * from Clie{0} where CLAVE =@cveCliente");
            foreach (DataRow row in result.Rows)
            {
                cliente.CLAVE=row["CLAVE"].ToString();
                cliente.STATUS=row["STATUS"].ToString();
                cliente.NOMBRE=row["NOMBRE"].ToString();
                cliente.RFC=row["RFC"].ToString();
                cliente.CALLE=row["CALLE"].ToString();
                cliente.NUMINT=row["NUMINT"].ToString();
                cliente.NUMEXT=row["NUMEXT"].ToString();
                cliente.CRUZAMIENTOS=row["CRUZAMIENTOS"].ToString();
                cliente.CRUZAMIENTOS2=row["CRUZAMIENTOS2"].ToString();
                cliente.COLONIA=row["COLONIA"].ToString();
                cliente.CODIGO=row["CODIGO"].ToString();
                cliente.LOCALIDAD=row["LOCALIDAD"].ToString();
                cliente.MUNICIPIO=row["MUNICIPIO"].ToString();
                cliente.ESTADO=row["ESTADO"].ToString();
                cliente.PAIS=row["PAIS"].ToString();
                cliente.NACIONALIDAD=row["NACIONALIDAD"].ToString();
                cliente.REFERDIR=row["REFERDIR"].ToString();
                cliente.TELEFONO=row["TELEFONO"].ToString();
                cliente.CLASIFIC=row["CLASIFIC"].ToString();
                cliente.FAX=row["FAX"].ToString();
                cliente.PAG_WEB=row["PAG_WEB"].ToString();
                cliente.CURP=row["CURP"].ToString();
                cliente.CVE_ZONA=row["CVE_ZONA"].ToString();
                cliente.IMPRIR=row["IMPRIR"].ToString();
                cliente.MAIL=row["MAIL"].ToString();
                cliente.NIVELSEC=Convert.ToInt32(row["NIVELSEC"].ToString());
                cliente.ENVIOSILEN=row["ENVIOSILEN"].ToString();
                cliente.EMAILPRED=row["EMAILPRED"].ToString();
                cliente.DIAREV=row["DIAREV"].ToString();
                cliente.DIAPAGO=row["DIAPAGO"].ToString();
                cliente.CON_CREDITO=row["CON_CREDITO"].ToString();
                cliente.DIASCRED=Convert.ToInt32(row["DIASCRED"].ToString());
                cliente.LIMCRED=Convert.ToDecimal(row["LIMCRED"].ToString());
                cliente.SALDO=Convert.ToDecimal(row["SALDO"].ToString());
                cliente.LISTA_PREC=0;
                cliente.CVE_BITA=0;
                cliente.ULT_PAGOD=row["ULT_PAGOD"].ToString();
                cliente.ULT_PAGOM=Convert.ToDecimal(row["ULT_PAGOM"].ToString());
                cliente.ULT_PAGOF=Convert.ToDateTime(row["ULT_PAGOF"].ToString());
                cliente.DESCUENTO=Convert.ToDecimal(row["DESCUENTO"].ToString());
                cliente.ULT_VENTAD=row["ULT_VENTAD"].ToString();
                cliente.ULT_COMPM=Convert.ToDecimal(row["ULT_COMPM"].ToString());
                cliente.FCH_ULTCOM=Convert.ToDateTime(row["FCH_ULTCOM"].ToString());
                cliente.VENTAS=Convert.ToDecimal(row["VENTAS"].ToString());
                cliente.CVE_VEND=row["CVE_VEND"].ToString();
                cliente.CVE_OBS=Convert.ToInt32(row["CVE_OBS"].ToString());
                cliente.TIPO_EMPRESA=row["TIPO_EMPRESA"].ToString();
                cliente.MATRIZ=row["MATRIZ"].ToString();
                cliente.PROSPECTO=row["PROSPECTO"].ToString();
                cliente.CALLE_ENVIO=row["CALLE_ENVIO"].ToString();
                cliente.NUMINT_ENVIO=row["NUMINT_ENVIO"].ToString();
                cliente.NUMEXT_ENVIO=row["NUMEXT_ENVIO"].ToString();
                cliente.CRUZAMIENTOS_ENVIO=row["CRUZAMIENTOS_ENVIO"].ToString();
                cliente.CRUZAMIENTOS_ENVIO2=row["CRUZAMIENTOS_ENVIO2"].ToString();
                cliente.COLONIA_ENVIO=row["COLONIA_ENVIO"].ToString();
                cliente.LOCALIDAD_ENVIO=row["LOCALIDAD_ENVIO"].ToString();
                cliente.MUNICIPIO_ENVIO=row["MUNICIPIO_ENVIO"].ToString();
                cliente.ESTADO_ENVIO=row["ESTADO_ENVIO"].ToString();
                cliente.PAIS_ENVIO=row["PAIS_ENVIO"].ToString();
                cliente.CODIGO_ENVIO=row["CODIGO_ENVIO"].ToString();
                cliente.CVE_ZONA_ENVIO=row["CVE_ZONA_ENVIO"].ToString();
                cliente.REFERENCIA_ENVIO=row["REFERENCIA_ENVIO"].ToString();
                cliente.CUENTA_CONTABLE=row["CUENTA_CONTABLE"].ToString();
                cliente.ADDENDAF=row["ADDENDAF"].ToString();
                cliente.ADDENDAD=row["ADDENDAD"].ToString();
                cliente.NAMESPACE=row["NAMESPACE"].ToString();
                cliente.METODODEPAGO=row["METODODEPAGO"].ToString();
                cliente.NUMCTAPAGO=row["NUMCTAPAGO"].ToString();
                cliente.MODELO=row["MODELO"].ToString();
                cliente.DES_IMPU1=row["DES_IMPU1"].ToString();
                cliente.DES_IMPU2=row["DES_IMPU2"].ToString();
                cliente.DES_IMPU3=row["DES_IMPU3"].ToString();
                cliente.DES_IMPU4=row["DES_IMPU4"].ToString();
                cliente.DES_PER=row["DES_PER"].ToString();
                cliente.LAT_GENERAL=Convert.ToDecimal(row["LAT_GENERAL"].ToString());
                cliente.LON_GENERAL=Convert.ToDecimal(row["LON_GENERAL"].ToString());
                cliente.LAT_ENVIO=Convert.ToDecimal(row["LAT_ENVIO"].ToString());
                cliente.LON_ENVIO=Convert.ToDecimal(row["LON_ENVIO"].ToString());
                cliente.UUID=row["UUID"].ToString();
                cliente.VERSION_SINC=Convert.ToDateTime(row["VERSION_SINC"].ToString());
                cliente.USO_CFDI=row["USO_CFDI"].ToString();
                cliente.CVE_PAIS_SAT=row["CVE_PAIS_SAT"].ToString();
                cliente.NUMIDREGFISCAL=row["NUMIDREGFISCAL"].ToString();
                cliente.FORMADEPAGOSAT=row["FORMADEPAGOSAT"].ToString();
                cliente.ADDENDAG=row["ADDENDAG"].ToString();
                cliente.ADDENDAE=row["ADDENDAG"].ToString();

            } // actualizar la orden de compra

            return cliente;
        }
    }
}
