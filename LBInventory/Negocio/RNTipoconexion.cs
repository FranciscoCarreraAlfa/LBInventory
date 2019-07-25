using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class RNTipoConexion
    {
        public int Id { get; set; }
        public string TipoConexion { get; set; }




        public static List<RNTipoConexion> Obtener()
        {
            RNTipoConexion tipo = null;
            List<RNTipoConexion> RNTipos = new List<RNTipoConexion>();
            try
            {
                using (var ctx = new LBInventoryEntities())
                {

                    var catTipos = ctx.catTipoConexion.ToList();

                    foreach (var item in catTipos)
                    {
                        tipo = llenarRNTipoConexion(item);

                        RNTipos.Add(tipo);
                    }


                }


            }
            catch (Exception)
            {

                RNTipos = new List<RNTipoConexion>();
            }
            return RNTipos;
        }

        private static RNTipoConexion llenarRNTipoConexion(catTipoConexion item)
        {
            RNTipoConexion RNTipo = null;
            try
            {

                RNTipo = new RNTipoConexion()
                {
                    Id = item.Id,
                    TipoConexion = item.TipoConexion
                };
            }
            catch (Exception)
            {
                RNTipo = new RNTipoConexion();
            }
            return RNTipo;

        }
    }
}
