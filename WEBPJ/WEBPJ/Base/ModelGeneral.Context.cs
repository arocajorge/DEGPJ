﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WEBPJ.Base
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesGeneral : DbContext
    {
        public EntitiesGeneral()
            : base("name=EntitiesGeneral")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<LogError> LogError { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<vwCompra> vwCompra { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<ProveedorProducto> ProveedorProducto { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<ProductoDetalle> ProductoDetalle { get; set; }
        public virtual DbSet<vwProveedorProducto> vwProveedorProducto { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<CompraDetalle> CompraDetalle { get; set; }
    }
}
