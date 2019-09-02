using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Negocio
{
    class RNProducto
    {
        public string CVE_ART { get; set; }
        public string DESCR { get; set; }
        public string LIN_PROD { get; set; }
        public string CON_SERIE { get; set; }
        public string UNI_MED { get; set; }
        public decimal? UNI_EMP { get; set; }
        public string CTRL_ALM { get; set; }
        public int? TIEM_SURT { get; set; }
        public decimal? STOCK_MIN { get; set; }
        public decimal? STOCK_MAX { get; set; }
        public string TIP_COSTEO { get; set; }
        public int? NUM_MON { get; set; }
        public DateTime? FCH_ULTCOM { get; set; }
        public decimal? COMP_X_REC { get; set; }
        public DateTime? FCH_ULTVTA { get; set; }
        public decimal? PEND_SURT { get; set; }
        public decimal? EXIST { get; set; }
        public decimal? COSTO_PROM { get; set; }
        public decimal? ULT_COSTO { get; set; }
        public int? CVE_OBS { get; set; }
        public string TIPO_ELE { get; set; }
        public string UNI_ALT { get; set; }
        public decimal? FAC_CONV { get; set; }
        public decimal? APART { get; set; }
        public string CON_LOTE { get; set; }
        public string CON_PEDIMENTO { get; set; }
        public decimal? PESO { get; set; }
        public decimal? VOLUMEN { get; set; }
        public int? CVE_ESQIMPU { get; set; }
        public int? CVE_BITA { get; set; }
        public decimal? VTAS_ANL_C { get; set; }
        public decimal? VTAS_ANL_M { get; set; }
        public decimal? COMP_ANL_C { get; set; }
        public decimal? COMP_ANL_M { get; set; }
        public string PREFIJO { get; set; }
        public string TALLA { get; set; }
        public string COLOR { get; set; }
        public string CUENT_CONT { get; set; }
        public string CVE_IMAGEN { get; set; }
        public string BLK_CST_EXT { get; set; }
        public string STATUS { get; set; }
        public string MAN_IEPS { get; set; }
        public int? APL_MAN_IMP = 1;
        public decimal? CUOTA_IEPS = 0.00M;
        public string APL_MAN_IEPS = "C";
        public string UUID { get; set; }
        public DateTime? VERSION_SINC { get; set; }
        public DateTime? VERSION_SINC_FECHA_IMG { get; set; }
        public string CVE_PRODSERV { get; set; }
        public string CVE_UNIDAD { get; set; }

        public static RNProducto ObtenerProductoPorClave(int numEmpresa, string clave)
        {
            
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@clave", clave);
            var result = conexion.baseDatos.ObtenerTabla("select * from INVE{0} where CVE_ART =@clave");
            RNProducto producto = new RNProducto();
            foreach (DataRow fila in result.AsEnumerable())
            {
                producto.CVE_ART = fila["CVE_ART"].ToString();
                producto.DESCR = fila["DESCR"].ToString();
                producto.LIN_PROD = fila["LIN_PROD"].ToString();
                producto.CON_SERIE = fila["CON_SERIE"].ToString();
                producto.UNI_MED = fila["UNI_MED"].ToString();
                producto.UNI_EMP = Convert.ToDecimal(fila["UNI_EMP"].ToString());
                producto.CTRL_ALM = fila["CTRL_ALM"].ToString();
                producto.TIEM_SURT =Convert.ToInt32( fila["TIEM_SURT"].ToString());
                producto.STOCK_MIN = Convert.ToDecimal(fila["STOCK_MIN"].ToString());
                producto.STOCK_MAX = Convert.ToDecimal(fila["STOCK_MAX"].ToString());
                producto.TIP_COSTEO = fila["TIP_COSTEO"].ToString();
                producto.NUM_MON = Convert.ToInt32(fila["NUM_MON"].ToString());
                producto.FCH_ULTCOM = Convert.ToDateTime(fila["FCH_ULTCOM"].ToString());
                producto.COMP_X_REC = Convert.ToDecimal(fila["COMP_X_REC"].ToString());
                producto.FCH_ULTVTA = Convert.ToDateTime(fila["FCH_ULTVTA"].ToString());
                producto.PEND_SURT = Convert.ToDecimal(fila["PEND_SURT"].ToString());
                producto.EXIST = Convert.ToDecimal(fila["EXIST"].ToString());
                producto.COSTO_PROM = Convert.ToDecimal(fila["COSTO_PROM"].ToString());
                producto.ULT_COSTO = Convert.ToDecimal(fila["ULT_COSTO"].ToString());
                producto.CVE_OBS = Convert.ToInt32(fila["CVE_OBS"].ToString());
                producto.TIPO_ELE = fila["TIPO_ELE"].ToString();
                producto.UNI_ALT = fila["UNI_ALT"].ToString();
                producto.FAC_CONV = Convert.ToDecimal(fila["FAC_CONV"].ToString());
                producto.APART = Convert.ToDecimal(fila["APART"].ToString());
                producto.CON_LOTE = fila["CON_LOTE"].ToString();
                producto.CON_PEDIMENTO = fila["CON_PEDIMENTO"].ToString();
                producto.PESO = Convert.ToDecimal(fila["PESO"].ToString());
                producto.VOLUMEN = Convert.ToDecimal(fila["VOLUMEN"].ToString());
                producto.CVE_ESQIMPU = Convert.ToInt32(fila["CVE_ESQIMPU"].ToString());
                producto.CVE_BITA = 0;
                producto.VTAS_ANL_C = Convert.ToDecimal(fila["VTAS_ANL_C"].ToString());
                producto.VTAS_ANL_M = Convert.ToDecimal(fila["VTAS_ANL_M"].ToString());
                producto.COMP_ANL_C = Convert.ToDecimal(fila["COMP_ANL_C"].ToString());
                producto.COMP_ANL_M = Convert.ToDecimal(fila["COMP_ANL_M"].ToString());
                producto.PREFIJO = fila["PREFIJO"].ToString();
                producto.TALLA = fila["TALLA"].ToString();
                producto.COLOR = fila["COLOR"].ToString();
                producto.CUENT_CONT = fila["CUENT_CONT"].ToString();
                producto.CVE_IMAGEN = fila["CVE_IMAGEN"].ToString();
                producto.BLK_CST_EXT = fila["BLK_CST_EXT"].ToString();
                producto.STATUS = fila["STATUS"].ToString();
                producto.MAN_IEPS = fila["MAN_IEPS"].ToString();
                producto.APL_MAN_IMP = Convert.ToInt32(fila["APL_MAN_IMP"].ToString());
                producto.CUOTA_IEPS = Convert.ToDecimal(fila["CUOTA_IEPS"].ToString());
                producto.APL_MAN_IEPS = fila["APL_MAN_IEPS"].ToString();
                producto.UUID = fila["UUID"].ToString();
                producto.CVE_PRODSERV = fila["CVE_PRODSERV"].ToString();
                producto.CVE_UNIDAD = fila["CVE_UNIDAD"].ToString();
            }
            return producto;
        }

        public static decimal ObtenerPrecioProducto(string clave,int listaprecio,int numEmpresa)
        {
            decimal precio = 0.0M;
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@clave", clave);
            conexion.baseDatos.AgregarParametro("@listaprecio", listaprecio);
            var result = conexion.baseDatos.ObtenerTabla("     select lp.precio "+
                "from precio_x_prod{0} lp where lp.CVE_ART =@clave and  lp.CVE_PRECIO = @listaprecio");

            foreach (DataRow row in result.Rows)
            {
                precio = Convert.ToDecimal(row["precio"].ToString());
            }
            return precio;
        }

        public static void ObtenerImpuestos(string clave,int esquema,int  numEmpresa, out decimal IMPU1, out decimal IMPU2, out decimal IMPU3, out decimal IMPU4, out int IMP1APLA, out int IMP2APLA, out int IMP3APLA, out int IMP4APLA)
        {
            IMPU1 = 0.00M;
            IMPU2 = 0.00M;
            IMPU3 = 0.00M;
            IMPU4 = 0.00M;
            IMP1APLA = 0;
            IMP2APLA = 0;
            IMP3APLA = 0;
            IMP4APLA = 0;
            RNConexion conexion = new RNConexion(numEmpresa);
            conexion.baseDatos.AbrirConexion();
            conexion.baseDatos.LimpiarParametros();
            conexion.baseDatos.AgregarParametro("@clave", clave);
            conexion.baseDatos.AgregarParametro("@esquema", esquema);
            var result = conexion.baseDatos.ObtenerTabla("      select i.IMPUESTO1,i.IMPUESTO2,i.IMPUESTO3,i.IMPUESTO4,"+
                "i.IMP1APLICA, i.IMP2APLICA, i.IMP3APLICA, i.IMP4APLICA "+
                "from INVE{0} p left join IMPU{0} i on i.CVE_ESQIMPU=@esquema  where p.CVE_ART =@clave");

            foreach (DataRow row in result.Rows)
            {
                IMPU1 = Convert.ToDecimal(row["IMPUESTO1"].ToString());
                IMPU2 = Convert.ToDecimal(row["IMPUESTO2"].ToString());
                IMPU3 = Convert.ToDecimal(row["IMPUESTO3"].ToString());
                IMPU4 = Convert.ToDecimal(row["IMPUESTO4"].ToString());
                IMP1APLA = Convert.ToInt32(row["IMP1APLICA"].ToString());
                IMP2APLA = Convert.ToInt32(row["IMP2APLICA"].ToString());
                IMP3APLA = Convert.ToInt32(row["IMP3APLICA"].ToString());
                IMP4APLA = Convert.ToInt32(row["IMP4APLICA"].ToString());
            }
        }

    }
}
