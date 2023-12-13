using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DependienteTipo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.RhernandezProyectoNcapasIdentityCoreContext context = new DL.RhernandezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from dependienteTipo in context.DependienteTipos
                                 select new
                                 {
                                     IdDependienteTipo = dependienteTipo.IdDependienteTipo,
                                     Nombre = dependienteTipo.Nombre
                                 }).ToList();
                    if(query.Count > 0 ) {

                        result.Objects = new List<object>();

                        foreach(var typeOfDependent in query) {
                            ML.DependienteTipo dependienteTipo = new ML.DependienteTipo();

                            dependienteTipo.IdDependienteTipo = typeOfDependent.IdDependienteTipo;
                            dependienteTipo.Nombre = typeOfDependent.Nombre;

                            result.Objects.Add(dependienteTipo);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Error al realizar la consulta en la base de datos" + result.Ex;
                //throw;
            }
            return result;
        }
    }
}
