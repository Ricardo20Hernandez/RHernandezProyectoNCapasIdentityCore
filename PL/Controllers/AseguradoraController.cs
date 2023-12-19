using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AseguradoraController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();

            ML.Result result = BL.Aseguradora.GetAll();
            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "No aseguradoras registradas aún";
            }
            return View(aseguradora);
        }

        [HttpGet]
        public IActionResult Form(int? idAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();

            ML.Result resultUsuarios = BL.IdentityUser.GetAll();
            aseguradora.UserIdentity = new ML.UserIdentity();
            aseguradora.UserIdentity.IdentityUsers = resultUsuarios.Objects;

            if(idAseguradora == null)
            {
                ViewBag.Accion = "Agregar";
            }
            else //Actualizar
            {
                //Buscar por id la aseguradora.
                ML.Result result = BL.Aseguradora.GetById(idAseguradora.Value);
                if (result.Correct)
                {
                    aseguradora = (ML.Aseguradora)result.Object;
                }
                ViewBag.Accion = "Actualizar";
                aseguradora.UserIdentity.IdentityUsers = resultUsuarios.Objects;
            }
            return View(aseguradora);
        }

        [HttpPost]
        public IActionResult Form(ML.Aseguradora aseguradora) 
        {
            ML.Result result = new ML.Result();

            if(aseguradora.IdAseguradora == 0)
            {
                result = BL.Aseguradora.Add(aseguradora);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Aseguradora añadida correctamente";
                }
                else
                {
                    ViewBag.Mensaje = result.ErrorMessage;
                }
            }
            else //Actualizar
            {
                result = BL.Aseguradora.Update(aseguradora);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Aseguradora actualizada correctamente";
                }
                else
                {
                    ViewBag.Mensaje = result.ErrorMessage;
                }
            }
            return View("Modal");
        }

        public IActionResult Delete(int idAseguradora) 
        {
            ML.Result result = BL.Aseguradora.Delete(idAseguradora);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Aseguradora eliminada correctamente";
            }
            else
            {
                ViewBag.Mensaje = result.ErrorMessage;
            }

            return View("Modal");
        }
    }
}
