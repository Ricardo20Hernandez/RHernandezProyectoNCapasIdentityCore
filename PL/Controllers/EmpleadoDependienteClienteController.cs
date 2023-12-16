using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PL.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class EmpleadoDependienteClienteController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Dependiente dependiente = new ML.Dependiente();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName

            // For ASP.NET Core >= 5.0
            string userEmail = User.FindFirstValue(ClaimTypes.Email); // will give the user's Email

            ML.Result result = BL.Empleado.GetEmpleado(userEmail);

            if (result.Correct)
            {
                empleado = (ML.Empleado)result.Object;
                string numeroEmpleado = empleado.NumeroEmpleado;
                ViewBag.Nombre = empleado.Nombre;
                ViewBag.ApellidoPaterno = empleado.ApellidoPaterno;

                ML.Result resultdependientes = BL.Dependiente.GetByNumeroEmpleado(numeroEmpleado);
        
                if (resultdependientes.Correct)
                {
                    dependiente.Dependientes = resultdependientes.Objects;
                }
                else
                {
                    ViewBag.Mensaje = "Aún no cuentas con dependientes, contacta a un administrador para que te los asigne si lo requieres";
                }
            }
            else
            {
                ViewBag.Mensaje = "El empleado no pudo ser encontrado";
            }

            return View(dependiente);
        }
    }
}
