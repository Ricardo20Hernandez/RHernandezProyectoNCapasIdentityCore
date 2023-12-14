using Microsoft.AspNetCore.Mvc;

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
    }
}
