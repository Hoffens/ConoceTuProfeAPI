using System;
using System.Collections.Generic;

namespace EligeTuProfeAPI.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Comentarios = new HashSet<Comentario>();
            InscripcionAlumnos = new HashSet<InscripcionAlumno>();
        }

        public int Rut { get; set; }
        public string? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public int? CodigoCarrera { get; set; }
        public int? Ingreso { get; set; }
        public bool Estado { get; set; }

        public virtual Carrera? CodigoCarreraNavigation { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<InscripcionAlumno> InscripcionAlumnos { get; set; }
    }
}
