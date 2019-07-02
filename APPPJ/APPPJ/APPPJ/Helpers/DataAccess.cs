using System;
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
        public DataAccess()
        {
            this.connection = new SQLiteConnection(
                    Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DBREC.db3"));
            connection.CreateTable<UsuarioModel>();
            connection.CreateTable<ProductoModel>();
            connection.CreateTable<ProveedorModel>();
            connection.CreateTable<ProductoDetalleModel>();
        }

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
    }
}
