﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LBInventoryEntities : DbContext
    {
        public LBInventoryEntities()
            : base("name=LBInventoryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<catPerfil> catPerfil { get; set; }
        public virtual DbSet<ctrlSucursal> ctrlSucursal { get; set; }
        public virtual DbSet<ctrlUsuario> ctrlUsuario { get; set; }
        public virtual DbSet<relUsuarioPerfil> relUsuarioPerfil { get; set; }
        public virtual DbSet<catTipoConexion> catTipoConexion { get; set; }
        public virtual DbSet<ctrlConexion> ctrlConexion { get; set; }
    }
}
