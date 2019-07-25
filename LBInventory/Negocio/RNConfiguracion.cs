using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Umbrella.WEB;

namespace Negocio
{
    public class RNConfiguracion
    {
        public int Id { get; set; }
        public string Servidor { get; set; }
        public string BaseDatos { get; set; }
        public int? Puerto { get; set; }
        public string Usuario { get; set; }
        public string Pass { get; set; }
        public bool SNActivo { get; set; }
        public int NumEmpresa { get; set; }
        public string Empresa { get; set; }
        public string RFC { get; set; }
        public decimal Factor { get; set; }
        public Decimal IVA { get; set; }

        public RNTipoConexion TipoConexion = new RNTipoConexion();
        public bool SNImportadora { get; set; }
        public bool SNComercializadora { get; set; }

        public static List<RNConfiguracion> Listar()
        {
            List<RNConfiguracion> configuracions = new List<RNConfiguracion>();
            try
            {
                using (var ctx = new LBInventoryEntities())
                {
                    var con = ctx.ctrlConexion.ToList();
                    foreach (var c in con)
                    {
                        configuracions.Add(llenaRNConfiguracion(c));
                    }
                   
                }
            }
            catch (Exception E)
            {

                throw;
            }

            return configuracions;
        }

        private static ctrlConexion llenaCtrlConexion(RNConfiguracion RNConfiguracion)
        {
            ctrlConexion conexion = null;
            try
            {

                conexion = new ctrlConexion()
                {
                    Id = RNConfiguracion.Id,
                    TipoConexionId = RNConfiguracion.TipoConexion.Id,
                    Servidor = RNConfiguracion.Servidor,
                    BaseDatos = RNConfiguracion.BaseDatos,
                    Puerto = RNConfiguracion.Puerto,
                    NumEmpresa = RNConfiguracion.NumEmpresa,
                    Empresa = RNConfiguracion.Empresa,
                    RFC = RNConfiguracion.RFC,
                    Usuario = RNConfiguracion.Usuario,
                    Pass = RNConfiguracion.Pass,
                    SNActivo = RNConfiguracion.SNActivo,
                    SNImportadora = RNConfiguracion.SNImportadora,
                    SNComercializadora = RNConfiguracion.SNComercializadora

                };
            }
            catch (Exception ex)
            {
                conexion = null;
            }
            return conexion;
        }




        public RNConfiguracion Obtener(int ConexionId)
        {
            RNConfiguracion configuracion = null;
            try
            {
                using (var ctx = new LBInventoryEntities())
                {

                    var ctrlConexion = ctx.ctrlConexion
                                    .Include("catTipoConexion")
                                    .Where(x => x.Id == ConexionId).FirstOrDefault();

                    configuracion = llenaRNConfiguracion(ctrlConexion);

                }


            }
            catch (Exception)
            {

                configuracion = new RNConfiguracion();
            }
            return configuracion;
        }

        public static RNConfiguracion Obtener(string empresa)
        {
            RNConfiguracion configuracion = null;
            try
            {
                using (var ctx = new LBInventoryEntities())
                {

                    var ctrlConexion = ctx.ctrlConexion
                                    .Include("catTipoConexion")
                                    .Where(x => x.Empresa.Equals(empresa)).FirstOrDefault();

                    configuracion = llenaRNConfiguracion(ctrlConexion);

                }


            }
            catch (Exception)
            {

                configuracion = new RNConfiguracion();
            }
            return configuracion;
        }

        private static RNConfiguracion llenaRNConfiguracion(ctrlConexion ctrlConexion)
        {
            RNConfiguracion configuracion = null;
            try
            {

                configuracion = new RNConfiguracion()
                {
                    Id = ctrlConexion.Id,
                    Servidor = ctrlConexion.Servidor,
                    BaseDatos = ctrlConexion.BaseDatos,
                    Puerto = ctrlConexion.Puerto,
                    Usuario = ctrlConexion.Usuario,
                    Pass = ctrlConexion.Pass,
                    SNActivo = ctrlConexion.SNActivo,
                    NumEmpresa = ctrlConexion.NumEmpresa,
                    Empresa = ctrlConexion.Empresa,
                    RFC = ctrlConexion.RFC,
                    SNComercializadora = ctrlConexion.SNComercializadora,
                    SNImportadora = ctrlConexion.SNImportadora
                };

                configuracion.TipoConexion = new RNTipoConexion()
                {
                    Id = ctrlConexion.catTipoConexion.Id,
                    TipoConexion = ctrlConexion.catTipoConexion.TipoConexion

                };


            }
            catch (Exception)
            {

                configuracion = new RNConfiguracion();
            }
            return configuracion;

        }

        public static bool GuardarComercializadora (string empresa, bool snComer, out string msg)
        {
            bool error = false;
            try
            {
                ctrlConexion conexion =llenaCtrlConexion(Obtener(empresa));
                conexion.SNComercializadora = true;
                conexion.SNImportadora = false;
                using (var ctx =new LBInventoryEntities())
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    var eComer = ctx.Entry(conexion).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
                
                msg = "";
                error = true;
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return error;
        }
        public static bool GuardarImportadora (string empresa, bool snComer, out string msg)
        {
            bool error = false;
            try
            {
                ctrlConexion conexion =llenaCtrlConexion(Obtener(empresa));
                conexion.SNComercializadora = false;
                conexion.SNImportadora = true;
                using (var ctx =new LBInventoryEntities())
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    var eComer = ctx.Entry(conexion).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
                
                msg = "";
                error = true;
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return error;
        }
    }
}
