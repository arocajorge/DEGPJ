using System;
namespace APPPJ.Helpers
{
    using APPPJ.Models;
    using Plugin.DeviceInfo;
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
            connection.CreateTable<UsuarioModel>();
            connection.CreateTable<ProductoModel>();
            connection.CreateTable<ProveedorModel>();
            connection.CreateTable<ProductoDetalleModel>();
            connection.CreateTable<CompraModel>();
            connection.CreateTable<CompraDetalleModel>();
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

        public List<CompraDetalleModel> GetCompraDetalle(decimal IdCompra)
        {
            return this.connection.Table<CompraDetalleModel>().Where(q => q.IdCompra == IdCompra).ToList();
        }
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

        public List<CompraModel> GetListModificar()
        {
            return this.connection.Table<CompraModel>().ToList();
        }

        public List<CompraSincronizacionModel> GetListCompras()
        {
           var lst = this.connection.Table<CompraModel>().ToList().Select(q=> new CompraSincronizacionModel
           {
               IdCompra = q.IdCompra,
               ProvCodigo = q.ProvCodigo,
               ProvCedulaRuc = q.ProvCedulaRuc,
               ProvNombre = q.ProvNombre,
               IdUsuario = q.IdUsuario,
               Codigo = q.Codigo,
               IdProducto = q.IdProducto,
               Calificacion = q.Calificacion,
               Fecha = q.Fecha,
               Precio = q.Precio,
               Cantidad = q.Cantidad,
               Total = q.Total,
               Comentario = q.Comentario,
               Estado = q.Estado,
               Dispositivo = q.Dispositivo
           }).ToList();
            foreach (var item in lst)
            {
                item.ListaDetalle = new List<CompraDetalleModel>(this.connection.Table<CompraDetalleModel>().ToList().Where(q => q.IdCompra == item.IdCompra).ToList());
            }
            return lst;
        }

        public int GetId()
        {
            var Lista = this.connection.Table<CompraModel>().ToList();
            if (Lista.Count == 0)
                return 1;
            else
                return Lista.Max(q => q.PKSQLite) + 1;
        }
        public int GetSecuencia()
        {
            var Lista = this.connection.Table<CompraDetalleModel>().ToList();
            if (Lista.Count == 0)
                return 1;
            else
                return Lista.Max(q => q.PKSQLite) + 1;
        }

        public bool Guardar(CompraModel model, List<CompraDetalleModel> ListaDetalle)
        {
            if (model.PKSQLite == 0)
            {
                Settings.IdCompra = Settings.IdCompra == "0.0" ? "0" : Settings.IdCompra;
                model.IdCompra = Convert.ToDecimal(Settings.IdCompra);
                model.Codigo = string.IsNullOrEmpty(Settings.IdCompra) ? "0" : Convert.ToDecimal(Settings.IdCompra).ToString("000000000");
                Settings.IdCompra = (Convert.ToDecimal(string.IsNullOrEmpty(Settings.IdCompra) ? "0" : Settings.IdCompra) + 1).ToString("n0");
                model.PKSQLite = GetId();
                model.Dispositivo = CrossDeviceInfo.Current.Id;

                this.connection.Insert(model);

                foreach (var item in ListaDetalle)
                {
                    item.IdCompra = model.IdCompra;
                    item.PKSQLite = GetSecuencia();
                    this.connection.Insert(item);
                }
            }
            else
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
