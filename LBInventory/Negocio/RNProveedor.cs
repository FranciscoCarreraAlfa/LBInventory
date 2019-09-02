using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class RNProveedor
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
        public string CVE_PAIS { get; set; }
        public string NACIONALIDAD { get; set; }
        public string TELEFONO { get; set; }
        public string CLASIFIC { get; set; }
        public string FAX { get; set; }
        public string PAG_WEB { get; set; }
        public string CURP { get; set; }
        public string CVE_ZONA { get; set; }
        public string CON_CREDITO { get; set; }
        public int DIASCRED { get; set; }
        public decimal LIMCRED { get; set; }
        public int CVE_BITA { get; set; }
        public string ULT_PAGOD { get; set; }
        public decimal ULT_PAGOM { get; set; }
        public DateTime ULT_PAGOF { get; set; }
        public string ULT_COMPD { get; set; }
        public decimal ULT_COMPM { get; set; }
        public DateTime ULT_COMPF { get; set; }
        public decimal SALDO { get; set; }
        public decimal VENTAS { get; set; }
        public decimal DESCUENTO { get; set; }
        public int TIP_TERCERO { get; set; }
        public int TIP_OPERA { get; set; }
        public int CVE_OBS { get; set; }
        public string CUENTA_CONTABLE { get; set; }
        public int FORMA_PAGO { get; set; }
        public string BENEFICIARIO { get; set; }
        public string TITULAR_CUENTA { get; set; }
        public string BANCO { get; set; }
        public string SUCURSAL_BANCO { get; set; }
        public string CUENTA_BANCO { get; set; }
        public string CLABE { get; set; }
        public string DESC_OTROS { get; set; }
        public string IMPRIR { get; set; }
        public string MAIL { get; set; }
        public int NIVELSEC { get; set; }
        public string ENVIOSILEN { get; set; }
        public string EMAILPRED { get; set; }
        public string MODELO { get; set; }
        public decimal? LAT { get; set; }
        public decimal? LON { get; set; }

        public static RNProveedor ObtenerProveedor(string clave, int numEmpresa)
        {
            RNProveedor proveedor = new RNProveedor();
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@clave", clave);
            var result = conexion.baseDatos.ObtenerTabla("select * from PROV{0} where CLAVE =@clave");
            foreach (DataRow row in result.Rows)
            {
                proveedor.CLAVE=row["CLAVE"].ToString();
                proveedor.STATUS=row["STATUS"].ToString();
                proveedor.NOMBRE=row["NOMBRE"].ToString();
                proveedor.RFC=row["RFC"].ToString();
                proveedor.CALLE=row["CALLE"].ToString();
                proveedor.NUMINT=row["NUMINT"].ToString();
                proveedor.NUMEXT=row["NUMEXT"].ToString();
                proveedor.CRUZAMIENTOS=row["CRUZAMIENTOS"].ToString();
                proveedor.CRUZAMIENTOS2=row["CRUZAMIENTOS2"].ToString();
                proveedor.COLONIA=row["COLONIA"].ToString();
                proveedor.CODIGO=row["CODIGO"].ToString();
                proveedor.LOCALIDAD=row["LOCALIDAD"].ToString();
                proveedor.MUNICIPIO=row["MUNICIPIO"].ToString();
                proveedor.ESTADO=row["ESTADO"].ToString();
                proveedor.CVE_PAIS=row["CVE_PAIS"].ToString();
                proveedor.NACIONALIDAD=row["NACIONALIDAD"].ToString();
                proveedor.TELEFONO=row["TELEFONO"].ToString();
                proveedor.CLASIFIC=row["CLASIFIC"].ToString();
                proveedor.FAX=row["FAX"].ToString();
                proveedor.PAG_WEB=row["PAG_WEB"].ToString();
                proveedor.CURP=row["CURP"].ToString();
                proveedor.CVE_ZONA=row["CVE_ZONA"].ToString();
                proveedor.CON_CREDITO=row["CON_CREDITO"].ToString();
                proveedor.DIASCRED=Convert.ToInt32(row["DIASCRED"].ToString());
                proveedor.LIMCRED=Convert.ToDecimal(row["LIMCRED"].ToString());
                proveedor.CVE_BITA=0;
                proveedor.ULT_PAGOD=row["ULT_PAGOD"].ToString();
                proveedor.ULT_PAGOM=Convert.ToDecimal(row["ULT_PAGOM"].ToString());
                proveedor.ULT_PAGOF=Convert.ToDateTime(row["ULT_PAGOF"].ToString());
                proveedor.ULT_COMPD=row["ULT_COMPD"].ToString();
                proveedor.ULT_COMPM=Convert.ToDecimal(row["ULT_COMPM"].ToString());
                proveedor.ULT_COMPF=Convert.ToDateTime(row["ULT_COMPF"].ToString());
                proveedor.SALDO=Convert.ToDecimal(row["SALDO"].ToString());
                proveedor.VENTAS=Convert.ToDecimal(row["VENTAS"].ToString());
                proveedor.DESCUENTO=Convert.ToDecimal(row["DESCUENTO"].ToString());
                proveedor.TIP_TERCERO=Convert.ToInt32(row["TIP_TERCERO"].ToString());
                proveedor.TIP_OPERA=Convert.ToInt32(row["TIP_OPERA"].ToString());
                proveedor.CVE_OBS=Convert.ToInt32(row["CVE_OBS"].ToString());
                proveedor.CUENTA_CONTABLE=row["CUENTA_CONTABLE"].ToString();
                proveedor.FORMA_PAGO=Convert.ToInt32(row["FORMA_PAGO"].ToString());
                proveedor.BENEFICIARIO=row["BENEFICIARIO"].ToString();
                proveedor.TITULAR_CUENTA=row["TITULAR_CUENTA"].ToString();
                proveedor.BANCO=row["BANCO"].ToString();
                proveedor.SUCURSAL_BANCO=row["SUCURSAL_BANCO"].ToString();
                proveedor.CUENTA_BANCO=row["CUENTA_BANCO"].ToString();
                proveedor.CLABE=row["CLABE"].ToString();
                proveedor.DESC_OTROS=row["DESC_OTROS"].ToString();
                proveedor.IMPRIR=row["IMPRIR"].ToString();
                proveedor.MAIL=row["MAIL"].ToString();
                proveedor.NIVELSEC=0;
                proveedor.ENVIOSILEN=row["ENVIOSILEN"].ToString();
                proveedor.EMAILPRED=row["EMAILPRED"].ToString();
                proveedor.MODELO=row["MODELO"].ToString();


            } // actualizar la orden de compra

            return proveedor;
        }
    }
}

