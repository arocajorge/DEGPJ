﻿using System;
namespace APPPJ.Helpers
{
    using APPPJ.Models;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    public class DataAccess
    {
        private SQLiteConnection connection;
        public DataAccess(string dbPath)
        {
            this.connection = new SQLiteConnection(dbPath);
                    //Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DBREC.db3"));
            connection.CreateTable<UsuarioModel>();
            connection.CreateTable<ProductoModel>();
            connection.CreateTable<ProveedorModel>();
            connection.CreateTable<ProductoDetalleModel>();
            connection.CreateTable<CompraModel>();
            connection.CreateTable<ProveedorProductoModel>();
        }
        #region Funciones generales
        public void Insert<T>(T model)
        {
            this.connection.Insert(model);
        }

        public void DeleteAll<T>()
        {
            this.connection.DeleteAll<T>();
        }

        public void InsertAll<T>(List<T> model)
        {
            this.connection.InsertAll(model);
        }

        public void Update<T>(T model)
        {
            this.connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            this.connection.Delete(model);
        }

        public void Dispose()
        {
            connection.Dispose();
        }
        #endregion

        #region Funciones varias
        public UsuarioModel GetUsuario(string IdUsuario, string Clave)
        {
            return this.connection.Table<UsuarioModel>().Where(q => q.IdUsuario.ToLower() == IdUsuario.ToLower() && q.Clave.ToLower() == Clave.ToLower()).FirstOrDefault();
        }

        public List<CatalogoModel> GetListCatalogo(Enumeradores.eTipoCombo TipoCombo,string CodigoProveedor)
        {
            switch (TipoCombo)
            {
                case Enumeradores.eTipoCombo.Producto:
                    return this.connection.Table<ProveedorProductoModel>().ToList().Where(q=> q.Codigo == CodigoProveedor).Select(q => new CatalogoModel
                    {
                        Id = q.IdProducto,
                        Descripcion = q.Descripcion,
                        TipoCombo = Enumeradores.eTipoCombo.Producto
                    }).ToList();
                case Enumeradores.eTipoCombo.Proveedor:
                    return this.connection.Table<ProveedorModel>().ToList().Select(q => new CatalogoModel
                    {
                        Codigo = q.Codigo,
                        CedulaRuc = q.Ruc,
                        Descripcion = q.Nombre,
                        TipoCombo = Enumeradores.eTipoCombo.Proveedor
                    }).ToList();
                default:
                    return new List<CatalogoModel>();
            }
        }

        public List<ProveedorProductoModel> GetListProveedorProducto(string Codigo)
        {
            return this.connection.Table<ProveedorProductoModel>().ToList().Where(q => q.Codigo == Codigo).ToList();
        }

        public ProductoModel GetProducto(int IdProducto)
        {
            return this.connection.Table<ProductoModel>().ToList().Where(q => q.IdProducto == IdProducto).FirstOrDefault();
        }

        public List<ProductoDetalleModel> GetListProducto(int IdProducto)
        {
           var Lista = this.connection.Table<ProductoDetalleModel>().ToList().Where(q => q.IdProducto == IdProducto).ToList();
            Lista.ForEach(q => q.Descripcion += " (" + q.Minimo.ToString() + "-" + q.Maximo.ToString() + ")");
            return Lista;
        }

        public List<CompraModel> GetListCompras()
        {
            return this.connection.Table<CompraModel>().ToList();
        }

        public int GetId()
        {
            var Lista = this.connection.Table<CompraModel>().ToList();
            if (Lista.Count == 0)
                return 1;
            else
                return Lista.Max(q => q.PKSQLite) + 1;
        }

        public bool Guardar(CompraModel model)
        {
            if (model.PKSQLite == 0)
            {
                model.PKSQLite = GetId();
                Settings.IdCompra = (Convert.ToInt32(string.IsNullOrEmpty(Settings.IdCompra) ? "0" : Settings.IdCompra ) + 1).ToString("0000000000");
                model.Codigo = string.IsNullOrEmpty(Settings.IdCompra) ? "0" : Settings.IdCompra;
                this.connection.Insert(model);
            }else
            {
                var Entity = this.connection.Table<CompraModel>().ToList().Where(q => q.PKSQLite == model.PKSQLite).FirstOrDefault();
                if (Entity != null)
                {
                    Entity.Comentario = model.Comentario;
                    this.connection.Update(Entity);
                }
            }
            return true;
        }
        #endregion
    }
}
