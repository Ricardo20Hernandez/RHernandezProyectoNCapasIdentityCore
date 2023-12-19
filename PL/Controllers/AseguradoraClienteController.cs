using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PL.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class AseguradoraClienteController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Empleado empleado = new ML.Empleado();   

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName

            // For ASP.NET Core >= 5.0
            string userEmail = User.FindFirstValue(ClaimTypes.Email); // will give the user's Email

            //Obtener Nombre y Apellido de Empleado.
            ML.Result resultPersonal = BL.Empleado.GetEmpleado(userEmail);
            empleado = (ML.Empleado)resultPersonal.Object;
            ViewBag.Nombre = empleado.Nombre;
            ViewBag.ApellidoPaterno = empleado.ApellidoPaterno;

            //Busqueda de aseguradora

            ML.Result result = BL.Aseguradora.GetByUserId(userId);
            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Aún no se te asigna una aseguradora, contacta al administrador para que te asigne si es requerido";
            }

            return View(aseguradora);
        }
    }
}
