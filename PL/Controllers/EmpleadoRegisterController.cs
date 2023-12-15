using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoRegisterController : Controller
    {
        [HttpGet]
        public IActionResult Form(string Email)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Email = Email;
            empleado.IdEmpresa = 0;
            return View(empleado);
        }

        [HttpPost]
        public IActionResult Form(ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Add(empleado);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Cuenta generada exitosamente";
            }
            return View("Modal");
        }
    }
}
