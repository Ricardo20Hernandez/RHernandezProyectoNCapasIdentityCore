using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from empleado in context.Empleados
                                 select new
                                 {
                                     NumeroEmpleado = empleado.NumeroEmpleado,
                                     RFC = empleado.Rfc,
                                     Nombre = empleado.Nombre,
                                     ApellidoPaterno = empleado.ApellidoPaterno,
                                     ApellidoMaterno = empleado.ApellidoMaterno,
                                     Email = empleado.Email,
                                     Telefono = empleado.Telefono,
                                     FechaNacimiento = empleado.FechaNacimiento,
                                     NSS = empleado.Nss,
                                     FechaIngreso = empleado.FechaIngreso,
                                     Foto = empleado.Foto,
                                     IdEmpresa = empleado.IdEmpresa
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var employeer in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = employeer.NumeroEmpleado;
                            empleado.RFC = employeer.RFC;
                            empleado.Nombre = employeer.Nombre;
                            empleado.ApellidoPaterno = employeer.ApellidoPaterno;
                            empleado.ApellidoMaterno = employeer.ApellidoMaterno;
                            empleado.Email = employeer.Email;
                            empleado.Telefono = employeer.Telefono;
                            empleado.FechaNacimiento = (employeer.FechaNacimiento == null) ? "" : employeer.FechaNacimiento.Value.ToString("dd/MM/yyyy");
                            empleado.NSS = employeer.NSS;
                            empleado.FechaIngreso = (employeer.FechaIngreso == null) ? "" : employeer.FechaIngreso.Value.ToString("dd/MM/yyyy");
                            empleado.Foto = employeer.Foto;
                            empleado.IdEmpresa = (employeer.IdEmpresa == null) ? 0 : employeer.IdEmpresa.Value;

                            result.Objects.Add(empleado);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Error al realizar la consulta" + result.Ex;
                //throw;
            }

            return result;
        }

        public static ML.Result GetById(string numeroEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var queryLINQ = (from empleado in context.Empleados
                                     where empleado.NumeroEmpleado == numeroEmpleado
                                     select new
                                     {
                                         NumeroEmpleado = empleado.NumeroEmpleado,
                                         RFC = empleado.Rfc,
                                         Nombre = empleado.Nombre,
                                         ApellidoPaterno = empleado.ApellidoPaterno,
                                         ApellidoMaterno = empleado.ApellidoMaterno,
                                         Email = empleado.Email,
                                         Telefono = empleado.Telefono,
                                         FechaNacimiento = empleado.FechaNacimiento,
                                         NSS = empleado.Nss,
                                         FechaIngreso = empleado.FechaIngreso,
                                         Foto = empleado.Foto,
                                         IdEmpresa = empleado.IdEmpresa
                                     });

                    if(queryLINQ != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        var employeer = queryLINQ.FirstOrDefault();
                        
                        empleado.NumeroEmpleado = employeer.NumeroEmpleado;
                        empleado.RFC = employeer.RFC;
                        empleado.Nombre = employeer.Nombre;
                        empleado.ApellidoPaterno = employeer.ApellidoPaterno;
                        empleado.ApellidoMaterno = employeer.ApellidoMaterno;
                        empleado.Email = employeer.Email;
                        empleado.Telefono = employeer.Telefono;
                        empleado.FechaNacimiento = (employeer.FechaNacimiento == null) ? "" : employeer.FechaNacimiento.Value.ToString("dd/MM/yyyy");
                        empleado.NSS = employeer.NSS;
                        empleado.FechaIngreso = (employeer.FechaIngreso == null) ? "" : employeer.FechaIngreso.Value.ToString("dd/MM/yyyy");
                        empleado.Foto = employeer.Foto;
                        empleado.IdEmpresa = (employeer.IdEmpresa == null) ? 0 : employeer.IdEmpresa.Value;

                        result.Object = empleado;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Empleado no encontrado" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    DL.Empleado employer = new DL.Empleado();

                    string primerNombre = empleado.Nombre.Substring(0,1);
                    string primerPaterno = empleado.ApellidoPaterno.Substring(0,1);
                    string primerMaterno = empleado.ApellidoMaterno.Substring(0,1);
                    string fechaNacimiento = empleado.FechaNacimiento.Replace("/","");
                    string fechaactual = DateTime.Now.ToString("ddMMyyHHmmss");

                    string NumeroEmleadoCreado = primerNombre + primerPaterno + primerMaterno + fechaNacimiento + fechaactual;

                    employer.NumeroEmpleado = NumeroEmleadoCreado;
                    employer.Rfc = empleado.RFC;
                    employer.Nombre = empleado.Nombre;
                    employer.ApellidoPaterno = empleado.ApellidoPaterno;
                    employer.ApellidoMaterno = empleado.ApellidoMaterno;
                    employer.Email = empleado.Email;
                    employer.Telefono = empleado.Telefono;
                    employer.FechaNacimiento = DateTime.Parse(empleado.FechaNacimiento);
                    employer.Nss = empleado.NSS;
                    employer.FechaIngreso = DateTime.Parse(empleado.FechaIngreso);
                    employer.Foto = empleado.Foto;
                    employer.IdEmpresa = empleado.IdEmpresa;

                    context.Empleados.Add(employer);

                    int RowsAffected = context.SaveChanges();

                    if(RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El empleado no se pudo agregar";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Hubo problemas al agregar al empleado" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from employer in context.Empleados
                                    where employer.NumeroEmpleado == empleado.NumeroEmpleado
                                    select employer).SingleOrDefault();

                    if(query != null)
                    {
                        //query.NumeroEmpleado = empleado.NumeroEmpleado;
                        query.Rfc = empleado.RFC;
                        query.Nombre = empleado.Nombre;
                        query.ApellidoPaterno = empleado.ApellidoPaterno;
                        query.ApellidoMaterno = empleado.ApellidoMaterno;
                        query.Email = empleado.Email;
                        query.Telefono = empleado.Telefono;
                        query.FechaNacimiento = DateTime.Parse(empleado.FechaNacimiento);
                        query.Nss = empleado.NSS;
                        query.FechaIngreso = DateTime.Parse(empleado.FechaIngreso);
                        query.Foto = empleado.Foto;
                        query.IdEmpresa = empleado.IdEmpresa;

                        int RowsAffected = context.SaveChanges();

                        if(RowsAffected > 0 )
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
                result.ErrorMessage = result.Ex.Message;
                //throw;
            }
            return result;
        }

        public static ML.Result Delete(string numeroEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from queryLINQ in context.Empleados
                                 where queryLINQ.NumeroEmpleado == numeroEmpleado
                                 select queryLINQ).First();

                    if(query != null)
                    {
                        context.Empleados.Remove(query);
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
                result.Ex= ex;
                result.ErrorMessage = "Error al eliminar el usuario" + result.Ex;
                //throw;
            }
            return result;
        }

        //Método del lado del cliente, obtener Número de Empleado para encontrar sus dependientes.
        public static ML.Result GetEmpleado(string email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from empleado in context.Empleados
                                 where empleado.Email == email
                                 select new
                                 {
                                     NumeroEmpleado = empleado.NumeroEmpleado,
                                     Nombre = empleado.Nombre,
                                     ApellidoPaterno = empleado.ApellidoPaterno
                                 });

                    if(query != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();

                        var employer = query.FirstOrDefault();
                        empleado.NumeroEmpleado = employer.NumeroEmpleado;
                        empleado.Nombre = employer.Nombre;
                        empleado.ApellidoPaterno = employer.ApellidoPaterno;

                        result.Object = empleado;
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
    }
}
