/*
 * Clase para facilitar la validación del registro de un usuario
 */
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AplicacionWeb.Models
{
    public class RegistroViewModel
    {
        public Persona persona { get; set; }
        public Usuario usuario { get; set; }
        public List<SelectListItem>? tipos { get; set; }
        [Required(ErrorMessage ="El tipo es requerido")]
        public string tipo { get; set; }
    }
}
