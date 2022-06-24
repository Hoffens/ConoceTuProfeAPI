using System;
using System.Collections.Generic;

namespace EligeTuProfe.Models
{
    public partial class Profesor
    {
        public Profesor()
        {
            Comentarios = new HashSet<Comentario>();
            InscripcionProfesor = new HashSet<InscripcionProfesor>();
        }

        public int Rut { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<InscripcionProfesor> InscripcionProfesor { get; set; }
    }
}
