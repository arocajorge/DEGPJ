using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPJ.Info;
using WEBPJ.Base;

namespace WEBPJ.Data
{
    public class Usuario_Data
    {
        public List<Usuario_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<Usuario_Info> Lista;

                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.Usuario
                                 select new Usuario_Info
                                 {
                                     IdUsuario = q.IdUsuario,
                                     Estado = q.Estado,
                                     Nombre = q.Nombre
                                 }).ToList();
                    else
                        Lista = (from q in Context.Usuario
                                 where q.Estado == true
                                 select new Usuario_Info
                                 {
                                     IdUsuario = q.IdUsuario,
                                     Estado = q.Estado,
                                     Nombre = q.Nombre
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_login(string IdUsuario, string Clave)
        {
            try
            {
                Usuario_Info info = new Usuario_Info();
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Usuario Entity = Context.Usuario.FirstOrDefault(q => q.IdUsuario == IdUsuario && q.Clave == Clave && q.Estado == true);
                    if (Entity == null) return false;
                    info = new Usuario_Info
                    {
                        IdUsuario = Entity.IdUsuario,
                        Clave = Entity.Clave                      
                    };

                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool validar_existe_usuario(string IdUsuario)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    var lst = from q in Context.Usuario
                              where q.IdUsuario == IdUsuario
                              select q;

                    if (lst.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Usuario_Info get_info(string IdUsuario)
        {
            try
            {
                Usuario_Info info = new Usuario_Info();

                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Usuario Entity = Context.Usuario.FirstOrDefault(q => q.IdUsuario.ToLower() == IdUsuario.ToLower());
                    if (Entity == null) return null;
                    info = new Usuario_Info
                    {
                        IdUsuario = Entity.IdUsuario,
                        Clave = Entity.Clave,
                        Estado = Entity.Estado,
                        Nombre = Entity.Nombre,
                        TipoUsuario = Entity.TipoUsuario
                    };                      
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Usuario_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Usuario Entity = new Usuario
                    {
                        IdUsuario = info.IdUsuario,
                        Clave = info.Clave,
                        Nombre = info.Nombre,
                        TipoUsuario = info.TipoUsuario,
                        Estado = true
                    };
                    Context.Usuario.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(Usuario_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Usuario Entity = Context.Usuario.FirstOrDefault(q => q.IdUsuario == info.IdUsuario);
                    if (Entity == null) return false;
                    Entity.Nombre = info.Nombre;
                    Entity.Clave = info.Clave;
                    Entity.TipoUsuario = info.TipoUsuario;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(Usuario_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Usuario Entity = Context.Usuario.FirstOrDefault(q => q.IdUsuario == info.IdUsuario);
                    if (Entity == null) return false;
                    Entity.Estado = false;

                    Context.SaveChanges();
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