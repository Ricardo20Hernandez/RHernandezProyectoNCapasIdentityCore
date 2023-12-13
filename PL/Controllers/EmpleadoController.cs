using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult GetAll()
        {
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
        public IActionResult Form(string numeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();

            if (numeroEmpleado == null)
            { //Agregar

                ViewBag.Accion = "Agregar";
            }
            else //Actualizar
            {
                ML.Result result = BL.Empleado.GetById(numeroEmpleado);

                if (result.Correct)
                {
                    empleado = (ML.Empleado)result.Object;
                }
                ViewBag.Accion = "Actualizar";
            }
            return View(empleado);
        }

        [HttpPost]
        public IActionResult Form(ML.Empleado empleado, IFormFile fuImagen)
        {
            if (fuImagen != null)
            {
                empleado.Foto = convertFileToByteArray(fuImagen);
            }

            if (empleado.NumeroEmpleado == null)
            {
                ML.Result add = BL.Empleado.Add(empleado);
                if (add.Correct)
                {
                    ViewBag.Mensaje = "Empleado insertado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Hubo problemas al insertar el empleado" + add.ErrorMessage;
                }
            }
            else
            {
                ML.Result update = BL.Empleado.Update(empleado);
                if (update.Correct)
                {
                    ViewBag.Mensaje = "Empleado actualizado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Hubo problemas al actualizar el empleado" + update.ErrorMessage;
                }
            }
            return View("Modal");
        }

        public IActionResult Delete(string numeroEmpleado)
        {
            ML.Result result = BL.Empleado.Delete(numeroEmpleado);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Empleado Eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = result.ErrorMessage;
            }
            return View("Modal");
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
