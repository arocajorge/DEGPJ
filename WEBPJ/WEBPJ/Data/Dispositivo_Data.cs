using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPJ.Base;
using WEBPJ.Info;

namespace WEBPJ.Data
{
    public class Dispositivo_Data
    {
        public List<Dispositivo_Info> get_list()
        {
            try
            {
                List<Dispositivo_Info> Lista;

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Lista = db.Dispositivo.Select(q => new Dispositivo_Info
                    {
                        IdDispositivo = q.IdDispositivo,
                        Dispositivo1 = q.Dispositivo1,
                        di_Descripcion = q.di_Descripcion
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Dispositivo_Info get_info(string Dispositivo)
        {
            try
            {
                Dispositivo_Info info = new Dispositivo_Info();
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Dispositivo Entity = Context.Dispositivo.Where(q => q.Dispositivo1 == Dispositivo).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new Dispositivo_Info
                    {
                        IdDispositivo = Entity.IdDispositivo,
                        Dispositivo1 = Entity.Dispositivo1,
                        di_Descripcion= Entity.di_Descripcion
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dispositivo_Info get_info_by_ID(int IdDispositivo)
        {
            try
            {
                Dispositivo_Info info = new Dispositivo_Info();
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Dispositivo Entity = Context.Dispositivo.Where(q => q.IdDispositivo == IdDispositivo).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new Dispositivo_Info
                    {
                        IdDispositivo = Entity.IdDispositivo,
                        Dispositivo1 = Entity.Dispositivo1,
                        di_Descripcion = Entity.di_Descripcion
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int get_id()
        {

            try
            {
                int ID = 1;
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var Lista = db.Dispositivo.Select(q => q.IdDispositivo);

                    if (Lista.Count() > 0)
                        ID = Lista.Max() + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarBD(Dispositivo_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    Dispositivo entity = db.Dispositivo.Where(q => q.IdDispositivo == info.IdDispositivo).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Dispositivo1 = info.Dispositivo1;
                    entity.di_Descripcion = info.di_Descripcion;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}