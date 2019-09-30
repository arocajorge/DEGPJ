using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIPJ.Data
{
    using APIPJ.Bases;
    using APIPJ.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class Metodos
    {
        EntitiesGeneral db = new EntitiesGeneral();
        public decimal GetIdCompra(string IdUsuario)
        {
            try
            {
                decimal ID = 1;

                if (db.Compra.Count() > 0)
                    ID = db.Compra.Max(q => q.IdCompra) + 1;

                return ID;
            }
            catch (Exception ex)
            {
                using (EntitiesGeneral error = new EntitiesGeneral())
                {
                    decimal ID = error.LogError.Count() == 0 ? 1 : (error.LogError.Max(q => q.ID) + 1);
                    error.LogError.Add(new LogError
                    {
                        ID = ID,
                        Controlador = "Compra/GetId",
                        Error = ex.Message,
                        Fecha = DateTime.Now,
                        IdUsuario = IdUsuario ?? ""
                    });
                }
                return 1;
            }
        }

        public int GetIdDispositivo(string IdUsuario, string Dispositivo)
        {
            try
            {
                int ID = 1;

                var lst = db.Compra.Where(q => q.IdUsuario == IdUsuario && q.Dispositivo == Dispositivo).ToList();

                if (lst.Count > 0)
                    ID = lst.Max(q => q.SecuenciaDispositivoComp) + 1;

                return ID;
            }
            catch (Exception ex)
            {
                using (EntitiesGeneral error = new EntitiesGeneral())
                {
                    decimal ID = error.LogError.Count() == 0 ? 1 : (error.LogError.Max(q => q.ID) + 1);
                    error.LogError.Add(new LogError
                    {
                        ID = ID,
                        Controlador = "Compra/GetIdDispositivo",
                        Error = ex.Message,
                        Fecha = DateTime.Now,
                        IdUsuario = IdUsuario ?? ""
                    });
                }
                return 1;
            }
        }
    }
}