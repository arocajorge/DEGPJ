using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPJ.Info;
using WEBPJ.Base;
namespace WEBPJ.Data
{
    public class LogError_Data
    {
        public bool GuardarDB(LogError_Info info)
        {
            try
            {
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    db.LogError.Add(new LogError
                    {
                        ID = GetID(),
                        Controlador = info.Controlador ?? " ",
                        Error = info.Error ?? " ",
                        IdUsuario = info.IdUsuario ?? " ",
                        Fecha = DateTime.Now
                    });
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private decimal GetID()
        {
            try
            {
                decimal ID = 1;
                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    int cont = db.LogError.Count();
                    if (cont > 0)
                        ID = db.LogError.Max(q => q.ID) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}