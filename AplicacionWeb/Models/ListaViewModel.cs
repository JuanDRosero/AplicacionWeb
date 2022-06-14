/*
 * Vista para facilitar la visualización de los familiares de una persona
 */
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AplicacionWeb.Models
{
    public class ListaViewModel
    {
        public List<Persona> familiares { get; set; }
        public List<Parentesco> relaciones { get; set; }
    }
}
