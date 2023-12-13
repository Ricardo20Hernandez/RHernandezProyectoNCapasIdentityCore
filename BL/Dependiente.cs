using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
        public static ML.Result GetByNumeroEmpleado(string numeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from d in context.Dependientes
                                 join dt in context.DependienteTipos
                                 on d.IdDependienteTipo equals dt.IdDependienteTipo
                                 where d.NumeroEmpleado == numeroEmpleado
                                 select new
                                 {
                                     IdDependiente = d.IdDependiente,
                                     NumeroEmpleado = d.NumeroEmpleado,
                                     Nombre = d.Nombre,
                                     ApellidoPaterno = d.ApellidoPaterno,
                                     ApellidoMaterno = d.ApellidoMaterno,
                                     FechaNacimiento = d.FechaNacimiento,
                                     EstadoCivil = d.EstadoCivil,
                                     Genero = d.Genero,
                                     Telefono = d.Telefono,
                                     RFC = d.Rfc,
                                     IdDependienteTipo = dt.IdDependienteTipo,
                                     DependienteTipoNombre = dt.Nombre
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var persona in query)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();

                            dependiente.IdDependiente = persona.IdDependiente;
                            dependiente.Nombre = persona.Nombre;
                            dependiente.ApellidoPaterno = persona.ApellidoPaterno;
                            dependiente.ApellidoMaterno = persona.ApellidoMaterno;
                            dependiente.FechaNacimiento = persona.FechaNacimiento.Value.ToString("dd/MM/yyyy");
                            dependiente.EstadoCivil = persona.EstadoCivil;
                            dependiente.Genero = persona.Genero;
                            dependiente.Telefono = persona.Telefono;
                            dependiente.RFC = persona.RFC;
                            dependiente.DependienteTipo = new ML.DependienteTipo(); //Iniciar propiedad de navegación
                            dependiente.DependienteTipo.IdDependienteTipo = persona.IdDependienteTipo;
                            dependiente.DependienteTipo.Nombre = persona.DependienteTipoNombre;

                            result.Objects.Add(dependiente);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Hubo un problema al realizar la consulta" + result.Ex;
                //throw;
            }
            return result;

        }

        public static ML.Result GetById(int IdDependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from d in context.Dependientes
                                 join dt in context.DependienteTipos
                                 on d.IdDependienteTipo equals dt.IdDependienteTipo
                                 where d.IdDependiente == IdDependiente
                                 select new
                                 {
                                     IdDependiente = d.IdDependiente,
                                     NumeroEmpleado = d.NumeroEmpleado,
                                     Nombre = d.Nombre,
                                     ApellidoPaterno = d.ApellidoPaterno,
                                     ApellidoMaterno = d.ApellidoMaterno,
                                     FechaNacimiento = d.FechaNacimiento,
                                     EstadoCivil = d.EstadoCivil,
                                     Genero = d.Genero,
                                     Telefono = d.Telefono,
                                     RFC = d.Rfc,
                                     IdDependienteTipo = dt.IdDependienteTipo,
                                     DependienteTipoNombre = dt.Nombre

                                 });

                    if (query != null)
                    {
                        ML.Dependiente dependiente = new ML.Dependiente();
                        var dependent = query.FirstOrDefault();

                        dependiente.IdDependiente = dependent.IdDependiente;
                        dependiente.Nombre = dependent.Nombre;
                        dependiente.ApellidoPaterno = dependent.ApellidoPaterno;
                        dependiente.ApellidoMaterno = dependent.ApellidoMaterno;
                        dependiente.FechaNacimiento = dependent.FechaNacimiento.Value.ToString("dd/MM/yyyy");
                        dependiente.EstadoCivil = dependent.EstadoCivil;
                        dependiente.Genero = dependent.Genero;
                        dependiente.Telefono = dependent.Telefono;
                        dependiente.RFC = dependent.RFC;
                        dependiente.DependienteTipo = new ML.DependienteTipo(); //Iniciar propiedad de navegación
                        dependiente.DependienteTipo.IdDependienteTipo = dependent.IdDependienteTipo;
                        dependiente.DependienteTipo.Nombre = dependent.DependienteTipoNombre;

                        result.Object = dependiente;
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

        public static ML.Result Add(ML.Dependiente dependiente, string numeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    DL.Dependiente dependent = new DL.Dependiente();

                    dependent.NumeroEmpleado = numeroEmpleado;
                    dependent.Nombre = dependiente.Nombre;
                    dependent.ApellidoPaterno = dependiente.ApellidoPaterno;
                    dependent.ApellidoMaterno = dependiente.ApellidoMaterno;
                    dependent.FechaNacimiento = DateTime.Parse(dependiente.FechaNacimiento);
                    dependent.EstadoCivil = dependiente.EstadoCivil;
                    dependent.Genero = dependiente.Genero;
                    dependent.Telefono = dependiente.Telefono;
                    dependent.Rfc = dependiente.RFC;
                    dependent.IdDependienteTipo = dependiente.DependienteTipo.IdDependienteTipo;

                    context.Dependientes.Add(dependent);

                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El dependiente no se ha añadido";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Problemas al añadir al dependiente" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Dependiente dependiente, string numeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from dependent in context.Dependientes
                                 where dependent.IdDependiente == dependiente.IdDependiente
                                 select dependent).SingleOrDefault();
                    
                    if(query != null )
                    {
                        query.IdDependiente = dependiente.IdDependiente;
                        query.NumeroEmpleado = numeroEmpleado;
                        query.Nombre = dependiente.Nombre;
                        query.ApellidoPaterno = dependiente.ApellidoPaterno;
                        query.ApellidoMaterno = dependiente.ApellidoMaterno;
                        query.FechaNacimiento = DateTime.Parse(dependiente.FechaNacimiento);
                        query.EstadoCivil = dependiente.EstadoCivil;
                        query.Genero = dependiente.Genero;
                        query.Telefono = dependiente.Telefono;
                        query.Rfc = dependiente.RFC;
                        query.IdDependienteTipo = dependiente.DependienteTipo.IdDependienteTipo;

                        int RowsAffected = context.SaveChanges();

                        if (RowsAffected > 0)
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
                result.ErrorMessage = "Problemas al actualizar al dependiente" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Delete(int IdDependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from dependiente in context.Dependientes
                                 where dependiente.IdDependiente == IdDependiente
                                 select dependiente).First();

                    if (query != null)
                    {
                        context.Dependientes.Remove(query);
                        int RowsAffected = context.SaveChanges();

                        if (RowsAffected > 0)
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
                result.ErrorMessage = "Problemas al eliminar el dependiente " + result.Ex;
                //throw;
            }

            return result;
        }
    }
}
