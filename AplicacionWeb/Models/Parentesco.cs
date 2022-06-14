/*
 * Clase de Entity autogenerada para la tabla de Parentesco
 */
using System;
using System.Collections.Generic;

namespace AplicacionWeb.Models
{
    public partial class Parentesco
    {
        public Parentesco()
        {
            Personas = new HashSet<Persona>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Persona> Personas { get; set; }
    }
}
