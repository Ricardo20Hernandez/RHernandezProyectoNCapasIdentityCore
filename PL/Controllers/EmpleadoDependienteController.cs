using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoDependienteController : Controller
    {
        public IActionResult GetAll()
        {
            HttpContext.Session.SetString("Empleado", "");
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAll();

            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = result.ErrorMessage;
            }
            return View(empleado);
        }

        [HttpGet]
        public IActionResult EmpleadoDependiente(string NumeroEmpleado, string Nombre, string ApellidoPaterno)
        {
            ML.Empleado empleado = new ML.Empleado();

            if (HttpContext.Session.GetString("Empleado") == "")
            {
                ML.Result result = BL.Dependiente.GetByNumeroEmpleado(NumeroEmpleado);
                if (result.Correct)
                {
                    empleado.Dependiente = new ML.Dependiente(); //Iniciar propiedad de navegación
                    empleado.Dependiente.Dependientes = result.Objects;
                    empleado.Nombre = Nombre;
                    empleado.ApellidoPaterno = ApellidoPaterno;

                    HttpContext.Session.SetString("Empleado", NumeroEmpleado);
                    HttpContext.Session.SetString("Nombre", empleado.Nombre);
                    HttpContext.Session.SetString("ApellidoPaterno", empleado.ApellidoPaterno);
                }
                else
                {
                    ViewBag.Mensaje = result.ErrorMessage;
                }
            }
            else
            {
                NumeroEmpleado = HttpContext.Session.GetString("Empleado");
                ML.Result result = BL.Dependiente.GetByNumeroEmpleado(NumeroEmpleado);

                if(result.Correct)
                {
                    empleado.Dependiente= new ML.Dependiente(); //Iniciar propiedad de navegación
                    empleado.Dependiente.Dependientes= result.Objects;
                    empleado.Nombre = HttpContext.Session.GetString("Nombre");
                    empleado.ApellidoPaterno = HttpContext.Session.GetString("ApellidoPaterno");

                    HttpContext.Session.SetString("Empleado", NumeroEmpleado);
                    HttpContext.Session.SetString("Nombre", empleado.Nombre);
                    HttpContext.Session.SetString("ApellidoPaterno", empleado.ApellidoPaterno);
                }
                else
                {
                    ViewBag.Mensaje = result.ErrorMessage;
                }
            }
            return View(empleado);
        }

        [HttpGet]
        public IActionResult Form(int? IdDependiente)
        {
            //Agregar
            ML.Dependiente dependiente = new ML.Dependiente();
            //Llenar Drop Down List
            ML.Result resultDependienteTipo = BL.DependienteTipo.GetAll();
            dependiente.DependienteTipo = new ML.DependienteTipo();
            dependiente.DependienteTipo.DependientesTipos = resultDependienteTipo.Objects;

            if(IdDependiente == null) // Agregar
            {
                ViewBag.Accion = "Agregar";
            }
            else // Actualizar
            { 
                //Buscar registro
                ML.Result result = BL.Dependiente.GetById(IdDependiente.Value);

                if (result.Correct)
                {
                    dependiente = (ML.Dependiente)result.Object;
                    
                }
                ViewBag.Accion = "Actualizar";
                dependiente.DependienteTipo.DependientesTipos = resultDependienteTipo.Objects;
            }
            return View(dependiente);
        }


        [HttpPost]
        public IActionResult Form(ML.Dependiente dependiente)
        {
            string NumeroEmpleado = HttpContext.Session.GetString("Empleado");

            if(dependiente.IdDependiente == 0) //Agregar
            {
                ML.Result result = BL.Dependiente.Add(dependiente, NumeroEmpleado);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Dependiente añadido correctamente";
                }
                else
                {
                    ViewBag.Mensaje = result.ErrorMessage;
                }
            }
            else //Actualizar
            {
                ML.Result result = BL.Dependiente.Update(dependiente,NumeroEmpleado);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Dependiente actualizado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = result.ErrorMessage;
                }
            }
            return View("Modal");
        }

        public IActionResult Delete(int IdDependiente)
        {
            ML.Result result = BL.Dependiente.Delete(IdDependiente);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado correctamente el dependiente";
            }
            else
            {
                ViewBag.Mensaje = "Problemas al eliminar el dependiente" + result.ErrorMessage;
            }

            return View("Modal");
        }
    }
}
