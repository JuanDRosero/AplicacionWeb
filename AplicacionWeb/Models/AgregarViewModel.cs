using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AplicacionWeb.Models
{
    public class AgregarViewModel
    {
        public int Id { get; set; }
        public int TipoI { get; set; }
        [Required(ErrorMessage = "Campo Identificiación es requerido")]
        public int Identificacion { get; set; }
        [Display(Name = "Primer Nombre")]
        [Required(ErrorMessage = "Campo Primer Nombre es requerido")]
        [StringLength(25, ErrorMessage = "Maximo 25 caracteres")]
        public string PrimerNombre { get; set; } = null!;
        [Display(Name = "Segundo Nombre")]
        [StringLength(25, ErrorMessage = "Maximo 25 caracteres")]
        public string? SegundoNombre { get; set; }
        [Display(Name = "Primer Apellido")]
        [StringLength(25, ErrorMessage = "Maximo 25 caracteres")]
        [Required(ErrorMessage = "Campo Primer Apellido es requerido")]
        public string PrimerApellido { get; set; } = null!;
        [Display(Name = "Segundo Apellido")]
        [StringLength(25, ErrorMessage = "Maximo 25 caracteres")]
        public string? SegundoApellido { get; set; }
        [Required(ErrorMessage = "Campo Edad es requerido")]
        public int Edad { get; set; }
        [Required(ErrorMessage = "Debe escoger una Relacion validad")]
        public int? Relacion { get; set; }
        public int? Familiar { get; set; }

        public List<SelectListItem>? tipos { get; set; }
        public List<SelectListItem>? relaciones { get; set; }
    }
}
