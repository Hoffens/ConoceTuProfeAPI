using System;
using System.Collections.Generic;

namespace EligeTuProfeAPI.Models
{
    public partial class Asignatura
    {
        public Asignatura()
        {
            Comentarios = new HashSet<Comentario>();
            InscripcionAlumnos = new HashSet<InscripcionAlumno>();
            InscripcionProfesors = new HashSet<InscripcionProfesor>();
        }

        public int Codigo { get; set; }
        public int CodigoCarrera { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual Carrera CodigoCarreraNavigation { get; set; } = null!;
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<InscripcionAlumno> InscripcionAlumnos { get; set; }
        public virtual ICollection<InscripcionProfesor> InscripcionProfesors { get; set; }
    }
}
