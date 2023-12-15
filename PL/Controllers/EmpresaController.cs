using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();

            ML.Result result = BL.Empresa.GetAll();
            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                result.ErrorMessage = result.ErrorMessage;
            }
            return View(empresa);
        }

        [HttpGet]
        public IActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();

            if (IdEmpresa == null)
            {
                ViewBag.Accion = "Agregar";
            }
            else
            {
                ML.Result result = BL.Empresa.GetById(IdEmpresa.Value);
                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;
                    ViewBag.Accion = "Actualizar";
                }
            }
            return View(empresa);
        }

        [HttpPost]
        public IActionResult Form(ML.Empresa empresa, IFormFile fuImagen)
        {
            if (fuImagen != null)
            {
                empresa.Logo = convertFileToByteArray(fuImagen);
            }

            if (empresa.IdEmpresa == 0)
            {
                ML.Result add = BL.Empresa.Add(empresa);
                if (add.Correct)
                {
                    ViewBag.Mensaje = "Empresa agregada con exito";
                }
                else
                {
                    ViewBag.Mensaje = add.ErrorMessage;
                }
            }
            else //Actualizar
            {
                ML.Result update = BL.Empresa.Update(empresa);
                if (update.Correct)
                {
                    ViewBag.Mensaje = "Empresa actualizada correctamente";
                }
                else
                {
                    ViewBag.Mensaje = update.ErrorMessage;
                }

            }
            return View("Modal");
        }

        public IActionResult Delete(int IdEmpresa)
        {
            ML.Result result = BL.Empresa.Delete(IdEmpresa);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Empresa eliminada correctamente";
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
