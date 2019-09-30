using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPJ.Base;
using WEBPJ.Info;

namespace WEBPJ.Data
{
    public class ProveedorProducto_Data
    {
        public List<ProveedorProducto_Info> GetList(string Codigo)
        {
            try
            {
                List<ProveedorProducto_Info> Lista = new List<ProveedorProducto_Info>();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Lista = db.ProveedorProducto.Where(q => q.Codigo == Codigo).Select(q => new ProveedorProducto_Info
                    {
                        Codigo = q.Codigo,
                        Tipo = q.Tipo,
                        IdProducto = q.IdProducto,
                        Secuencia = q.Secuencia
                    }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Producto_Info> GetList_Combo(string Codigo)
        {
            try
            {
                List<Producto_Info> Lista = new List<Producto_Info>();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Lista = db.vwProveedorProducto.Where(q => q.Codigo == Codigo).Select(q => new Producto_Info
                    {
                        IdProducto = q.IdProducto,
                        Descripcion = q.Descripcion
                    }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GetInfo_ProveedorProducto(string Codigo, int IdProducto)
        {
            try
            {
                ProveedorProducto_Info info = new ProveedorProducto_Info();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    ProveedorProducto Entity = db.ProveedorProducto.Where(q => q.Codigo == Codigo && q.IdProducto == IdProducto).FirstOrDefault();

                    if (Entity == null)
                    {
                        return false;
                    }
                    else
                    {
                        info = new ProveedorProducto_Info
                        {
                            Tipo = Entity.Tipo,
                            Codigo = Entity.Codigo,
                            IdProducto = Entity.IdProducto,
                            Secuencia = Entity.Secuencia
                        };
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}