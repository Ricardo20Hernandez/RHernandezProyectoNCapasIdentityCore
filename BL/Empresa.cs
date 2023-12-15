using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BL
{
    public class Empresa
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from company in context.Empresas
                                 select new
                                 {
                                     IdEmpresa = company.IdEmpresa,
                                     Nombre = company.Nombre,
                                     Telefono = company.Telefono,
                                     Email = company.Email,
                                     DireccionWeb = company.DireccionWeb,
                                     Logo = company.Logo
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var company in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();

                            empresa.IdEmpresa = company.IdEmpresa;
                            empresa.Nombre = company.Nombre;
                            empresa.Telefono = company.Telefono;
                            empresa.Email = company.Email;
                            empresa.DireccionWeb = company.DireccionWeb;
                            empresa.Logo = company.Logo;

                            result.Objects.Add(empresa);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                //throw;
            }
            return result;
        }

        public static ML.Result GetById(int IdEmpresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from company in context.Empresas
                                 where company.IdEmpresa == IdEmpresa
                                 select new
                                 {
                                     IdEmpresa = company.IdEmpresa,
                                     Nombre = company.Nombre,
                                     Telefono = company.Telefono,
                                     Email = company.Email,
                                     DireccionWeb = company.DireccionWeb,
                                     Logo = company.Logo
                                 });

                    if (query != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();
                        var company = query.FirstOrDefault();

                        empresa.IdEmpresa = company.IdEmpresa;
                        empresa.Nombre = company.Nombre;
                        empresa.Telefono = company.Telefono;
                        empresa.Email = company.Email;
                        empresa.DireccionWeb = company.DireccionWeb;
                        empresa.Logo = company.Logo;

                        result.Object = empresa;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Error al realizar la consulta a la BD" + result.Ex;
                //throw;
            }
            return result;
        }


        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    DL.Empresa company = new DL.Empresa();

                    company.IdEmpresa = empresa.IdEmpresa;
                    company.Nombre = empresa.Nombre;
                    company.Telefono = empresa.Telefono;
                    company.Email = empresa.Email;
                    company.DireccionWeb = empresa.DireccionWeb;
                    company.Logo = empresa.Logo;

                    context.Empresas.Add(company);

                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error al agregar la empresa";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Problemas al añadir la empresa en la BD" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from company in context.Empresas
                                 where company.IdEmpresa == empresa.IdEmpresa
                                 select company).SingleOrDefault();

                    if(query != null)
                    {
                        query.Nombre = empresa.Nombre;
                        query.Telefono = empresa.Telefono;
                        query.Email = empresa.Email;
                        query.DireccionWeb = empresa.DireccionWeb;
                        query.Logo = empresa.Logo;

                        int RowsAffected = context.SaveChanges();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "La empresa no se agrego correctamente";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Problemas al actualizar la empresa en la BD" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Delete(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from empresa in context.Empresas
                                 where empresa.IdEmpresa == IdEmpresa
                                 select empresa).First();

                    if(query != null)
                    {
                        context.Empresas.Remove(query);
                        int RowsAffected = context.SaveChanges();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.ErrorMessage = "La empresa no se ha eliminado";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Problemas al actualizar la empresa en la BD" + result.Ex;
                //throw;
            }
            return result;
        }

    }
}
