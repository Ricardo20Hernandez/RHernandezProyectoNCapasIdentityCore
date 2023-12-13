using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        //Se calcula solo
        public string NumeroEmpleado { get; set; }
        //[Required(ErrorMessage = "RFC Requerido")]
        //[StringLength(20,ErrorMessage ="Longitud no aceptada")]
        //[DisplayName("RFC: ")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Z\d]{13}$", ErrorMessage = "Usar mayusculas y números")]
        public string RFC { get; set; }
        //[Required(ErrorMessage = "Nombre requerido")]
        //[StringLength(50,ErrorMessage ="Longitud no aceptada")]
        //[DisplayName("Nombre:")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Usar solo letras")]
        public string Nombre { get; set; }
        //[Required(ErrorMessage = "Apellido Paterno requerido")]
        //[StringLength(50,ErrorMessage ="Longitud no aceptada")]
        //[DisplayName("Apellido Paterno: ")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Usar solo letras")]
        public string ApellidoPaterno { get; set; }
        //[Required(ErrorMessage = "Apellido Materno requerido")]
        //[StringLength(50,ErrorMessage = "Longitud no aceptada")]
        //[DisplayName("Apellido Materno: ")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Usar solo letras")]
        public string ApellidoMaterno { get; set; }
        //[Required(ErrorMessage = "Email requerido")]
        //[StringLength(254,ErrorMessage = "Longitud no aceptada")]
        //[DisplayName("Email: ")]
        //[EmailAddress]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Telefono requerido")]
        //[StringLength(20,ErrorMessage = "Longitud no aceptada")]
        //[DisplayName("Telefono: ")]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Solo numeros de tel. de 10 digitos")]
        public string Telefono { get; set; }
        //[Required(ErrorMessage = "Fecha de Nacimiento requerida")]
        //[DisplayName("Fecha Nacimiento: ")]
        public string FechaNacimiento { get; set; }
        //[Required(ErrorMessage = "NSS requerido")]
        //[StringLength(20,ErrorMessage = "Longitud no aceptada")]
        //[DisplayName("NSS: ")]
        //[RegularExpression(@"^\d{11}$", ErrorMessage = "Solo 11 digitos")]
        public string NSS { get; set; }
        //[Required(ErrorMessage = "Fecha de Ingreso requerida")]
        //[DisplayName("Fecha Ingreso: ")]
        public string FechaIngreso { get; set; }
        public byte[] Foto { get; set; }
        //[Required(ErrorMessage = "Id Empresa Requerido")]
        //[DisplayName("IdEmpresa")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo numeros enteros")]
        public int IdEmpresa { get; set; }
        public List<object> Empleados { get; set; }
        public ML.Dependiente Dependiente { get; set; } //Propiedad de navegacion para Dependiente

    }
}
