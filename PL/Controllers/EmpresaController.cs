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

        [HttpGet]
        public IActionResult CargaMasiva()
        {
            ML.Empresa empresa = new ML.Empresa();

            return View(empresa);
        }

        [HttpPost]
        public IActionResult CargaMasivaTXT(IFormFile txt)
        {
            //Verificar que se insertará un archivo.
            if(txt != null)
            {
                //Validar la extensión del archivo insertado.
                string extensionArchivo = Path.GetExtension(txt.FileName);
                string extensionValida = ".txt";

                if(extensionArchivo == extensionValida)
                {
                    using (StreamReader file = new StreamReader(txt.OpenReadStream())) //Abrir archivo en CORE
                    {
                        string row = file.ReadLine(); //Sacar primera linea que es encabezado (titulos)

                        while (!file.EndOfStream) //Iterar hasta que sea diferente del final.
                        {
                            row = file.ReadLine();

                            string[] rowFila = row.Split('|');

                            ML.Empresa empresa = new ML.Empresa();

                            empresa.Nombre = rowFila[0];
                            empresa.Telefono = rowFila[1];
                            empresa.Email = rowFila[2];
                            empresa.DireccionWeb = rowFila[3];

                            ML.Result result = BL.Empresa.Add(empresa);

                            if (result.Correct)
                            {
                                ViewBag.Mensaje = "Archivo cargado correctamente";
                            }
                            else
                            {
                                ViewBag.Mensaje = "Hubo errores al cargar el archivo";
                            }

                        }
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Por favor elige un archivo TXT";
                }   
            }
            else
            {
                ViewBag.Mensaje = "Por favor seleccione un archivo";
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
