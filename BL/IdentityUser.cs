using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class IdentityUser
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from user in context.AspNetUsers
                                 select new
                                 {
                                     IdUser = user.Id,
                                     UserName = user.UserName
                                 }).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var user in query)
                        {
                            ML.UserIdentity usuario = new ML.UserIdentity();
                            usuario.IdUsuario = user.IdUser;
                            usuario.UserName = user.UserName;
                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Hubo problemas al realizar la consulta a la BD" + result.Ex;
                throw;
            }
            return result;
        }

        public static ML.Result Asignar(ML.UserIdentity user)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddAspNetUserRoles '{user.IdUsuario}', '{user.Rol.RoleId}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al asignar el rol al usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Problemas al hacer la inserción" + result.Ex;
                //throw;
            }

            return result;
        }

        public static ML.Result GetMissingUsers() //Usuaarios faltantes
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from user in context.AspNetUsers
                                 where !(from aseguradora in context.Aseguradoras
                                         select aseguradora.Id).Contains(user.Id)
                                 select new
                                 {
                                     IdUser = user.Id,
                                     UserName = user.UserName

                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var userIdentity in query)
                        {
                            ML.UserIdentity usuario = new ML.UserIdentity();
                            usuario.IdUsuario = userIdentity.IdUser;
                            usuario.UserName = userIdentity.UserName;

                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;
                    }

                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.Ex = Ex;
                result.ErrorMessage = "Error en la consulta en la BD" + result.Ex.Message;
                //throw;
            }
            return result;
        }
    }
}
