using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using FirebirdSql.Data.FirebirdClient;
using System.Data.SqlClient;
using Umbrella.Aspel.Sae.Datos;
using Umbrella.Aspel.Sae.Negocio;
using System.Data.Common;
using System.Configuration;

namespace Negocio
{
    public class RNConexion
    {

        //Obtener conexion BD SAE
        public BaseDatos baseDatos;
        private FbConnection FBConexion;
        private SqlConnection SQLConexion;
        private int numEmpresa { get; set; }
        public string nombreEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string RFC { get; set; }
        public Nullable<bool> SNImportadora { get; set; }
        public Nullable<bool> SNComercializadora { get; set; }

        public RNConexion()
        {
            baseDatos = new BaseDatos(ObtenerConexion(1), numEmpresa);
        }

        public RNConexion(int numEmpresa)
        {
            baseDatos = new BaseDatos(ObtenerConexion(numEmpresa), numEmpresa);
        }

        protected DbConnection ObtenerConexion(int numEmpresa)
        {

            ctrlConexion ctrlConexion = new ctrlConexion();
            string Conexion = "";
            try
            {
                //1.-Obetenemos los parametros de conexion que estan configurados en la BD
                using (var ctx = new LBInventoryEntities())
                {
                    //Depende del usuario, solo debe cargar los usuario segun el perfil y la sucursal
                    ctrlConexion = ctx.ctrlConexion.Where(x => x.SNActivo == true && x.NumEmpresa == numEmpresa).FirstOrDefault();
                }

                if (ctrlConexion != null)
                {
                    //creamos la cadena de conexion

                    string user = ctrlConexion.Usuario;
                    string password = ctrlConexion.Pass;
                    string dataBase = ctrlConexion.BaseDatos;
                    string dataSource = ctrlConexion.Servidor;
                    string puerto = ctrlConexion.Puerto.ToString();
                    numEmpresa = ctrlConexion.NumEmpresa;
                    nombreEmpresa = ctrlConexion.Empresa;
                    RFC = ctrlConexion.RFC;
                    TelefonoEmpresa = ctrlConexion.Telefono;
                    SNComercializadora = ctrlConexion.SNComercializadora;
                    SNImportadora = ctrlConexion.SNImportadora;
                    //Validamos el tipo de manejador SQL o FireBird

                    if (ctrlConexion.TipoConexionId == Parametros.IdTipoConexion.Firebird)
                    {
                        Conexion = string.Format(ConfigurationManager.ConnectionStrings["SAEFB"].ConnectionString, user, password, dataBase, dataSource, puerto);
                        FBConexion = new FbConnection(Conexion);
                        return FBConexion;
                    }
                    else
                    {
                        Conexion = string.Format(ConfigurationManager.ConnectionStrings["SAESQL"].ConnectionString, user, password, dataBase, dataSource);
                        SQLConexion = new SqlConnection(Conexion);
                        return SQLConexion;
                    }

                }
                else
                {
                    Conexion = "";
                }

                return null;

            }
            catch (Exception e)
            {
                return null;
                throw;
            }

        }


    }
}
