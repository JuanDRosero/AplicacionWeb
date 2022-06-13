using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionWeb.Models
{
    public partial class Persona
    {
        public Persona()
        {
            InverseFamiliarNavigation = new HashSet<Persona>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public int TipoI { get; set; }
        [Required(ErrorMessage ="Campo Identificiación es requerido")]
        public int Identificacion { get; set; }
        [Display(Name ="Primer Nombre")]
        [Required(ErrorMessage = "Campo Primer Nombre es requerido")]
        [StringLength(25,ErrorMessage ="Maximo 25 caracteres")]
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
        public int? Relacion { get; set; }
        public int? Familiar { get; set; }

        public virtual Persona? FamiliarNavigation { get; set; }
        public virtual Parentesco? RelacionNavigation { get; set; }
        public virtual Tipo? TipoINavigation { get; set; } = null!;
        public virtual ICollection<Persona>? InverseFamiliarNavigation { get; set; }
        public virtual ICollection<Usuario>? Usuarios { get; set; }
    }
}
