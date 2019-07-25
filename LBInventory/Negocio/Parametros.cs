using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Parametros
    {

        public static class IdTipoConexion
        {
            public const int SQL = 1;
            public const int Firebird = 2;
        }
        public static class IdEstatus
        {
            public const int Pendiente = 0;
            public const int Apartado = 1;
            public const int Pagado = 2;
            public const int Entregado = 3;
            public const int Cancelado = 4;
            public const int Cambio = 5;
        }
        public static class IdPerfil
        {
            public const int AdministradorMaster = 1;
            public const int Administrador = 2;
            public const int Gerente = 3;
            public const int Auditor = 4;
            public const int Vendedor = 5;
        }

        public static class Entidad
        {
            public const string Cliente = "CLIENTE";
        }


        public static class FormaPagoSAT
        {
            public const string Efectivo = "01";
            public const string Cheque = "02";
            public const string Transferencia = "03";
            public const string TarjetaCredito = "04";
            public const string Condonacion = "15";
            public const string PorDefinir = "99";

        }
        public static class FormaPago
        {

            public const int Cheque = 11;
            public const int Efectivo = 10;
            public const string sEfectivo = "Efectivo";



        }
        public static class MetodoPagoSAT
        {
            public const string Parcialidades = "PPD";
            public const string PagoUnaEx = "PUE";

        }

        public static class UsoCFDISAT
        {
            public const string AdquisicionMerca = "G01";
            public const string DevolucionMerca = "G02";
            public const string GastosEnGeneral = "G03";
            public const string PorDefinir = "P01";

        }
        //public static string CveMostrador = "MOSTR";





        public static class Moneda
        {
            public const int Peso = 1;
            public const int Dolar = 2;

        }

        public const int CveImpuesto = 1;
        public const string AnticipoUnidMed = "ACT";
        public const string ServicioTipoProducto = "S";
        public const int ServicioEsquemaImpuesto = 1;


        public const string TipoFacturaApartado = "F";


    }
}
