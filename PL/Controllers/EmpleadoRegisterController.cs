using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoRegisterController : Controller
    {

        public IActionResult Form(string email)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Email = email;
            empleado.IdEmpresa = 0;
            ViewBag.Accion = "Registrar";
            return View(empleado);
        }

        [HttpPost]
        public IActionResult Form(ML.Empleado empleado, IFormFile fuImagen)
        {
            if (fuImagen != null)
            {
                empleado.Foto = convertFileToByteArray(fuImagen);
            }

            ML.Result result = BL.Empleado.Add(empleado);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Proceso exitoso. Hemos enviado un correo a la cuenta registrada para que primero la validez y asi puedas iniciar sesión";
                return View("Modal");
            }
            return View(empleado);
        }

        public byte[] convertFileToByteArray(IFormFile fuImagen)
        {
            MemoryStream target = new MemoryStream();
            fuImagen.OpenReadStream().CopyTo(target);
            byte[] data = target.ToArray();
            return data;
        }
    }
}
