
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ML;
using System.ComponentModel.DataAnnotations;

namespace PL.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RolController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        public RolController(RoleManager<IdentityRole> roleMgr)
        {
            roleManager = roleMgr;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var Roles = roleManager.Roles.ToList();
            return View(Roles);
        }

        [HttpGet]
        public IActionResult Form(Guid IdRole, string Name)
        {
            IdentityRole role = new IdentityRole();
            if (Name != null)
            {
                role.Name = Name;
                role.Id = IdRole.ToString();
                ViewBag.Accion = "Actualizar";
                return View(role);
            }
            else
            {
                ViewBag.Accion = "Agregar";
                return View(role);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Form([Required] Microsoft.AspNetCore.Identity.IdentityRole rol)
        {
            if (ModelState.IsValid) //Validar si lo ingresado en el form cumple
            {
                IdentityRole role = await roleManager.FindByIdAsync(rol.Id.ToString());
                //Add O Update
                if (role == null)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(rol.Name)); //Crear

                    if (result.Succeeded)
                    {
                        ViewBag.Mensaje = "Rol añadido correctamente";
                        return View("Modal");

                    }
                    else
                    {

                    }
                }
                else //Actualizar
                {
                    role.Id = await roleManager.GetRoleIdAsync(rol);
                    role.Name = await roleManager.GetRoleNameAsync(rol);

                    IdentityResult result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        ViewBag.Mensaje = "Rol actualizado correctamente";
                        return View("Modal");
                    }
                }
            }
            return View(rol);
        }

        [HttpGet]
        public IActionResult Asignar(Guid IdRole, string Name)
        {
            ML.Result result = BL.IdentityUser.GetAll();
            ML.UserIdentity user = new ML.UserIdentity();

            if (result.Correct)
            {
                user.IdentityUsers = result.Objects;
            }

            user.Rol = new Rol(); //Inicializar variable de navegación
            user.Rol.Name = Name;
            ViewBag.Name = Name;
            user.Rol.RoleId = IdRole;

            return View(user);
        }

        [HttpPost]
        public IActionResult Asignar(ML.UserIdentity user)
        {
            ML.Result result = BL.IdentityUser.Asignar(user);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Rol añadido correctamente a usuario";
            }
            else
            {
                ViewBag.Mensaje = "Problemas al añadir rol a usuario " + result.ErrorMessage;
            }
            return PartialView("Modal");
        }

        public async Task<IActionResult> Delete(Guid IdRole)
        {
            IdentityRole role = await roleManager.FindByIdAsync(IdRole.ToString());

            if (role != null) 
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    ViewBag.Mensaje = "Rol eliminado correctamente";
                    return View("Modal");
                }
            }
            else
            {
                ModelState.AddModelError("", "Rol no encontrado");
            }

            return View("GetAll", roleManager.Roles);
        }

    }
}
