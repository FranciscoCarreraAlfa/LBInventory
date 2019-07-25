using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Umbrella.WEB;
using Umbrella.Aspel.Sae.Datos;
using System.Data.Entity;

namespace Negocio
{
    public class RNPerfil
    {
        public int Id { get; set; }
        public string Perfil { get; set; }

        public List<RNPerfil> Listar()
        {
            List<catPerfil> catPerfiles = new List<catPerfil>();
            List<RNPerfil> perfiles = new List<RNPerfil>();
            RNPerfil RNPerfil = new RNPerfil();
            try
            {
                using (var ctx = new LBInventoryEntities())
                {
                    catPerfiles = ctx.catPerfil.ToList();
                }
                foreach (var p in catPerfiles)
                {
                    perfiles.Add(llenarRNPerfil(p));
                }
            }
            catch (Exception)
            {
                perfiles = new List<RNPerfil>();
            }
            return perfiles;
        }

        public RNPerfil llenarRNPerfil(catPerfil catPerfil)
        {
            RNPerfil perfil = new RNPerfil();
            perfil.Id = catPerfil.Id;
            perfil.Perfil = catPerfil.Perfil;
            return perfil;
        }

        public List<RNPerfil> ObtenerPerfilesUsuario(int idUsuario)
        {
            List<relUsuarioPerfil> relperfiles = new List<relUsuarioPerfil>();
            List<RNPerfil> perfiles = new List<RNPerfil>();
            try
            {
                using (var ctx = new LBInventoryEntities())
                {
                    relperfiles = ctx.relUsuarioPerfil.
                                       Include("catPerfil")
                                       .Where(x => x.UsuarioId == idUsuario).ToList();
                }
                foreach (var p in relperfiles)
                {
                    perfiles.Add(llenarRNPerfil(p.catPerfil));
                }
            }
            catch (Exception)
            {
                perfiles = new List<RNPerfil>();
            }
            return perfiles;
        }

        public object ObtenerPerfilesSinAsignar(int idUsuario)
        {
            RNUsuario usuario = new RNUsuario();
            List<RNPerfil> perfiles = new List<RNPerfil>();
            List<RNPerfil> perfilesAsignados = new List<RNPerfil>();
            try
            {
                perfilesAsignados = ObtenerPerfilesUsuario(idUsuario);
                perfiles = Listar();
                foreach (var per in perfilesAsignados)
                {
                    var itemToRemove = perfiles.Single(r => r.Id == per.Id);
                    perfiles.Remove(itemToRemove);
                }
            }
            catch (Exception)
            {
                perfiles = new List<RNPerfil>();
            }
            return perfiles;
        }
        public ResponseModel AsignarPerfil(int idUsuario, int idPerfil)
        {
            ResponseModel rm = new ResponseModel();
            relUsuarioPerfil relUsuarioPerfil = new relUsuarioPerfil();
            try
            {
                relUsuarioPerfil.PerfilId = idPerfil;
                relUsuarioPerfil.UsuarioId = idUsuario;
                using (var ctx = new LBInventoryEntities())
                {
                    ctx.Entry(relUsuarioPerfil).State = EntityState.Added;
                    ctx.SaveChanges();
                }
                rm.SetResponse(true, "Perfil asignado correctamente");
                rm.function = "CargarPerfiles";
                rm.funcParams = idUsuario.ToString();
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Error al intentar asignar el perfil" + ex.Message);

            }
            return rm;
        }

        public ResponseModel QuitarPerfil(int idUsuario, int idPerfil)
        {
            ResponseModel rm = new ResponseModel();
            relUsuarioPerfil relUsuarioPerfil = new relUsuarioPerfil();
            try
            {
                using (var ctx = new LBInventoryEntities())
                {
                    relUsuarioPerfil = ctx.relUsuarioPerfil.Where(x => x.PerfilId == idPerfil && x.UsuarioId == idUsuario).SingleOrDefault();
                    ctx.Entry(relUsuarioPerfil).State = EntityState.Deleted;
                    ctx.SaveChanges();
                }
                rm.SetResponse(true, "Perfil desasignado correctamente");
                rm.function = "CargarPerfiles";
                rm.funcParams = idUsuario.ToString();
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Error al intentar desasignar el perfil" + ex.Message);

            }
            return rm;
        }
    }
}
