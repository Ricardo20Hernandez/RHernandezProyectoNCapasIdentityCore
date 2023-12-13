using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Dependiente
    {
        public int IdDependiente { get; set; }
        public ML.Empleado Empleado { get; set; } //Propiedad de navegación (Preguntar si debe ir)
        [Required(ErrorMessage = "Nombre requerido")]
        [StringLength(50, ErrorMessage = "Longitud no aceptada")]
        [DisplayName("Nombre:")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Usar solo letras")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Apellido paterno requerido")]
        [StringLength(50, ErrorMessage = "Longitud no aceptada")]
        [DisplayName("Apellido Paterno:")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Usar solo letras")]
        public string ApellidoPaterno { get; set; }
        [StringLength(50, ErrorMessage = "Longitud no aceptada")]
        [DisplayName("Apellido Materno:")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Usar solo letras")]
        public string ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "Fecha requerida")]
        [DisplayName("Fecha de Nacimiento:")]
        //[RegularExpression("^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/(19\\d{2}|200[0-3])$", ErrorMessage = "Fecha invalida (DD/MM/YYYY)")]
        public string FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Estado civil requerido")]
        [StringLength(50, ErrorMessage = "Longitud no aceptada")]
        [DisplayName("Estado civil:")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Usar solo letras")]
        public string EstadoCivil { get; set; }
        [Required(ErrorMessage = "Gemero requerido")]
        [DisplayName("Genero:")]
        [RegularExpression(@"^[MF]$", ErrorMessage = "Masculino (M), Femenino (F)")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "Telefono requerido")]
        [StringLength(20, ErrorMessage = "Longitud no aceptada")]
        [DisplayName("Telefono:")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Solo numeros de tel. de 10 digitos")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "RFC requerido")]
        [StringLength(20, ErrorMessage = "Longitud no aceptada")]
        [DisplayName("RFC:")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Z\d]{13}$", ErrorMessage = "RFC Invalido")]
        public string RFC { get; set; }
        public List<object> Dependientes { get; set; } //Lista para mostrar todos
        public ML.DependienteTipo DependienteTipo { get; set; }
    }
}
