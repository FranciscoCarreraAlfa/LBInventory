using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Umbrella.WEB;
using System.Data.Entity;

namespace Negocio
{
    public class RNUsuario
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public int SucursalId { get; set; }
        public string CveVendedor { get; set; }
        public bool SNActivo { get; set; }
        public List<RNPerfil> catPerfil { get; set; }

        public static RNUsuario Acceso(string usuario, string pass, out bool snAcceso)
        {
            RNUsuario logeado = new RNUsuario();
            ctrlUsuario Usuario = null;
            RNPerfil perfil = new RNPerfil();
            using (var ctx = new LBInventoryEntities())
            {
                //var pass = Encriptacion.Encrypt(pass, true);
                Usuario = ctx.ctrlUsuario.Where(x => x.Usuario == usuario && x.Password == pass).FirstOrDefault();
                if (Usuario != null)
                {
                    //Validar si el usuario tiene un perfil asignado, de lo contrario, no se le permite el acceso
                    var perfiles = perfil.ObtenerPerfilesUsuario(Usuario.Id);
                    if (perfiles.Count > 0)
                    {
                        snAcceso= true;
                    }
                    else
                    {
                        snAcceso= false;
                    }
                    logeado.Obtener(Usuario.Id);
                }
                else
                {
                    snAcceso= false;
                }
            }
            return logeado;
        }
        public List<RNUsuario> Listar()
        {
            List<RNUsuario> usuarios = new List<RNUsuario>();
            ctrlUsuario ctrlUsuario = new ctrlUsuario();
            List<ctrlUsuario> ctrlUsuarios = new List<ctrlUsuario>();
            try
            {
                using (var ctx = new LBInventoryEntities())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ctrlUsuarios = ctx.ctrlUsuario.Include("relUsuarioPerfil").ToList();
                    foreach (var item in ctrlUsuarios)
                    {
                        usuarios.Add(llenarRNUsuario(item));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return usuarios;
        }

        public RNUsuario llenarRNUsuario(ctrlUsuario ctrlUsuario)
        {
            RNUsuario usuario = new RNUsuario();
            usuario.catPerfil = new List<RNPerfil>();
            RNPerfil per = new RNPerfil();
            usuario.Id = ctrlUsuario.Id;
            usuario.Usuario = ctrlUsuario.Usuario;
            usuario.Nombre = ctrlUsuario.Nombre;
            usuario.SNActivo = ctrlUsuario.SNActivo;
            usuario.CveVendedor = ctrlUsuario.CveVendedor;
            using (var ctx = new LBInventoryEntities())
            {
                foreach (var item in ctx.relUsuarioPerfil.Include("CatPerfil").Where(x => x.UsuarioId == ctrlUsuario.Id))
                {
                    per = new RNPerfil();
                    per.Id = item.PerfilId;
                    per.Perfil = item.catPerfil.Perfil;
                    usuario.catPerfil.Add(per);
                }
            }
            return usuario;
        }


        public ctrlUsuario Obtener(int id)
        {
            var ctrlUsuario = new ctrlUsuario();
            try
            {
                if (id == 0)
                {
                    ctrlUsuario = new ctrlUsuario();
                }
                else
                {
                    using (var ctx = new LBInventoryEntities())
                    {

                        ctrlUsuario = ctx.ctrlUsuario
                                       .Include("relUsuarioPerfil")
                                       .Where(x => x.Id == id).SingleOrDefault();
                        if (ctrlUsuario != null)
                        {
                            ctrlUsuario.Password = null;
                        }
                        else
                            ctrlUsuario = new ctrlUsuario();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return ctrlUsuario;
        }

        public RNUsuario Consultar(int id)
        {
            RNUsuario usuario = new RNUsuario();
            usuario = llenarRNUsuario(Obtener(id));
            return usuario;
        }
        public ResponseModel Guardar()
        {
            ctrlUsuario ctrlUsuario = new ctrlUsuario();
            var rm = new ResponseModel();
            try
            {
                ctrlUsuario = llenarCtrlUsuario(this);
                using (var ctx = new LBInventoryEntities())
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    var eUsuario = ctx.Entry(ctrlUsuario);
                    if (ctrlUsuario.Id == 0)
                    {
                        //verificamos si el nombre de usuario ya esta en uso
                        if (ctx.ctrlUsuario.Where(x => x.Usuario == this.Usuario).Count() > 0)
                        {
                            rm.SetResponse(false, "Nombre de Usuario existente, favor de usar otro");
                            return rm;
                        }
                        eUsuario.State = EntityState.Added;
                    }
                    else
                    {
                        eUsuario.State = EntityState.Modified;
                        eUsuario.Property(x => x.Usuario).IsModified = false;
                    }
                    //Verifica si el password viene en nulo
                    if (ctrlUsuario.Password == null || ctrlUsuario.Password == "")
                    {
                        eUsuario.Property(x => x.Password).IsModified = false;
                    }
                    else
                    {
                        var passMD5 = Encriptacion.Encrypt(ctrlUsuario.Password, true);
                        ctrlUsuario.Password = passMD5;
                    }
                    if (catPerfil != null && catPerfil.Count > 0)
                    {
                        var perfiles = ctx.relUsuarioPerfil.Where(x => x.UsuarioId == eUsuario.Entity.Id).ToList();
                        foreach (var per in perfiles)
                        {
                            if (per.UsuarioId == Id)
                            {
                                QuitarPerfil(Id, per.PerfilId);
                            }
                        }
                        foreach (var per in catPerfil)
                        {
                            relUsuarioPerfil relUsuarioPerfil = new relUsuarioPerfil();
                            relUsuarioPerfil.PerfilId = per.Id;
                            relUsuarioPerfil.UsuarioId = eUsuario.Entity.Id;
                            eUsuario.Entity.relUsuarioPerfil.Add(relUsuarioPerfil);
                        }
                    }
                    ctx.SaveChanges();
                    rm.SetResponse(true, "Usuario Guardado con exito!");
                    rm.function = "Obtener";
                    rm.funcParams = ctrlUsuario.Id.ToString();
                }
            }
            catch (DbEntityValidationException ex)
            {
                rm.SetResponse(false, "Error al guardar el Usuario " + ex.Message);
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Error al guardar el Usuario " + ex.Message);
            }
            return rm;
        }
        //Solo los datos del usuario, el perfil se guardara en la clase correspondiente
        private ctrlUsuario llenarCtrlUsuario(RNUsuario usuario)
        {
            ctrlUsuario ctrlUsuario = new ctrlUsuario();
            try
            {
                ctrlUsuario.Id = usuario.Id;
                ctrlUsuario.Usuario = usuario.Usuario;
                ctrlUsuario.Password = usuario.Password;
                ctrlUsuario.Nombre = usuario.Nombre;
                ctrlUsuario.SNActivo = usuario.SNActivo;
                ctrlUsuario.CveVendedor = usuario.CveVendedor;
            }
            catch (Exception)
            {
                throw;
            }
            return ctrlUsuario;
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
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Error al intentar desasignar el perfil" + ex.Message);
            }
            return rm;
        }
    }
}
