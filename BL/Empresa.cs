using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
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
                                 });

                    if(query.Count() > 0 )
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

                            result.Objects.Add( empresa );
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

    }
}
