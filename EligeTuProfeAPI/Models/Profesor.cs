using System;
using System.Collections.Generic;

namespace EligeTuProfeAPI.Models
{
    public partial class Profesor
    {
        public Profesor()
        {
            Comentarios = new HashSet<Comentario>();
            InscripcionProfesors = new HashSet<InscripcionProfesor>();
        }

        public int Rut { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<InscripcionProfesor> InscripcionProfesors { get; set; }
    }
}
