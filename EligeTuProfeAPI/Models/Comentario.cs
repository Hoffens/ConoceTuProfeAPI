using System;
using System.Collections.Generic;

namespace EligeTuProfe.Models
{
    public partial class Comentario
    {
        public int Id { get; set; }
        public int RutAlumno { get; set; }
        public int RutProfesor { get; set; }
        public int CodigoAsignatura { get; set; }
        public int Year { get; set; }
        public int Periodo { get; set; }
        public string Texto { get; set; } = null!;
        public int Calificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }

        public virtual Asignatura CodigoAsignaturaNavigation { get; set; } = null!;
        public virtual Usuario RutAlumnoNavigation { get; set; } = null!;
        public virtual Profesor RutProfesorNavigation { get; set; } = null!;
    }
}
