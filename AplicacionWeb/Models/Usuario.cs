using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionWeb.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Campo Usuario es requerido")]
        [Display(Name ="Usuario")]
        public string Usuario1 { get; set; } = null!;
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo Contraseña es requerido")]
        public string Contraseña { get; set; } = null!;
        public int? Persona { get; set; }

        public virtual Persona? PersonaNavigation { get; set; } = null;
    }
}
