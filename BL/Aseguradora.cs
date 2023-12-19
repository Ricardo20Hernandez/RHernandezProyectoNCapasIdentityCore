using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace BL
{
    public class Aseguradora
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from aseguradora in context.Aseguradoras
                                 join user in context.AspNetUsers
                                 on aseguradora.Id equals user.Id
                                 select new
                                 {
                                     IdAseguradora = aseguradora.IdAseguradora,
                                     Nombre = aseguradora.Nombre,
                                     FechaCreacion = aseguradora.FechaCreacion,
                                     FechaModificacion = aseguradora.FechaModificación,
                                     IdUser = user.Id,
                                     UserName = user.UserName
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var asegura in query)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();

                            aseguradora.IdAseguradora = asegura.IdAseguradora;
                            aseguradora.Nombre = asegura.Nombre;
                            aseguradora.FechaCreacion = asegura.FechaCreacion.Value.ToString("dd/MM/yyyy HH:mm:ss");
                            aseguradora.FechaModificacion = asegura.FechaModificacion.Value.ToString("dd/MM/yyyy HH:mm:ss");
                            aseguradora.UserIdentity = new ML.UserIdentity();
                            aseguradora.UserIdentity.IdUsuario = asegura.IdUser;
                            aseguradora.UserIdentity.UserName = asegura.UserName;

                            result.Objects.Add(aseguradora);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = result.Ex.Message;
                //throw;
            }
            return result;
        }

        public static ML.Result GetById(int idAseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from aseguradora in context.Aseguradoras
                                 join user in context.AspNetUsers
                                 on aseguradora.Id equals user.Id
                                 where aseguradora.IdAseguradora == idAseguradora
                                 select new
                                 {
                                     IdAseguradora = aseguradora.IdAseguradora,
                                     Nombre = aseguradora.Nombre,
                                     FechaCreacion = aseguradora.FechaCreacion,
                                     FechaModificacion = aseguradora.FechaModificación,
                                     IdUser = user.Id,
                                     UserName = user.UserName
                                 });

                    if(query != null)
                    {
                        ML.Aseguradora aseguradora = new ML.Aseguradora();
                        var asegura = query.FirstOrDefault();

                        aseguradora.IdAseguradora = asegura.IdAseguradora;
                        aseguradora.Nombre = asegura.Nombre;
                        aseguradora.FechaCreacion = asegura.FechaCreacion.Value.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                        aseguradora.FechaModificacion = asegura.FechaModificacion.Value.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                        aseguradora.UserIdentity = new ML.UserIdentity();
                        aseguradora.UserIdentity.IdUsuario = asegura.IdUser;
                        aseguradora.UserIdentity.UserName = asegura.UserName;

                        result.Object = aseguradora;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = result.Ex.Message;
                //throw;
            }
            return result;
        }

        public static ML.Result Add(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    DL.Aseguradora asegura = new DL.Aseguradora();

                    asegura.IdAseguradora = aseguradora.IdAseguradora;
                    asegura.Nombre = aseguradora.Nombre;
                    asegura.FechaCreacion = DateTime.Parse(aseguradora.FechaCreacion);
                    asegura.FechaModificación = DateTime.Parse(aseguradora.FechaModificacion);
                    asegura.Id = aseguradora.UserIdentity.IdUsuario;

                    context.Aseguradoras.Add(asegura);

                    int RowsAffected = context.SaveChanges();

                    if(RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La aseguradora no se pudo añadir correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = result.Ex.Message;
                //throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from asegura in context.Aseguradoras
                                 where asegura.IdAseguradora == aseguradora.IdAseguradora
                                 select asegura).SingleOrDefault();

                    if(query != null)
                    {
                        query.IdAseguradora = aseguradora.IdAseguradora;
                        query.Nombre = aseguradora.Nombre;
                        query.FechaCreacion = DateTime.Parse(aseguradora.FechaCreacion);
                        query.FechaModificación = DateTime.Parse(aseguradora.FechaModificacion);
                        query.Id = aseguradora.UserIdentity.IdUsuario;

                        int RowsAffected = context.SaveChanges();

                        if(RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "La aseguradora no ha podido actualizarse correctamente";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = result.Ex.Message;
                //throw;
            }
            return result;
        }

        public static ML.Result Delete(int idAseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from asegura in context.Aseguradoras
                                 where asegura.IdAseguradora == idAseguradora
                                 select asegura).First();

                    if(query!= null)
                    {
                        context.Aseguradoras.Remove(query);

                        int RowsAffected = context.SaveChanges();

                        if(RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex; 
                result.ErrorMessage = "Problemas al eliminar la aseguradora " + result.Ex.Message; 
                //throw;
            }
            return result;
        }


        public static ML.Result GetByUserId(string userId)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from aseguradora in context.Aseguradoras
                                 where aseguradora.Id == userId
                                 select new
                                 {
                                     IdAseguradora = aseguradora.IdAseguradora,
                                     Nombre = aseguradora.Nombre,
                                     FechaCreacion = aseguradora.FechaCreacion,
                                     FechaModificacion = aseguradora.FechaModificación                                  

                                 }).ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach(var asegura in query)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();

                            aseguradora.IdAseguradora = asegura.IdAseguradora;
                            aseguradora.Nombre = asegura.Nombre;
                            aseguradora.FechaCreacion = asegura.FechaCreacion.Value.ToString("dd/MM/yyyy HH:mm:ss");
                            aseguradora.FechaModificacion = asegura.FechaModificacion.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            result.Objects.Add(aseguradora);    
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Error al realizar la consulta " + result.Ex.Message;
                //throw;
            }
            return result;
        }
    }
}
