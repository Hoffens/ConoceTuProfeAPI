using System;
using System.Collections.Generic;

namespace EligeTuProfeAPI.Models
{
    public partial class InscripcionAlumno
    {
        public int Rut { get; set; }
        public int CodigoAsignatura { get; set; }
        public int Year { get; set; }
        public int Periodo { get; set; }

        public virtual Asignatura CodigoAsignaturaNavigation { get; set; } = null!;
        public virtual Usuario RutNavigation { get; set; } = null!;
    }
}
