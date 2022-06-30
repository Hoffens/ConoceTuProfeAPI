using System;
using System.Collections.Generic;

namespace EligeTuProfeAPI.Models
{
    public partial class InscripcionProfesor
    {
        public int Rut { get; set; }
        public int CodigoAsignatura { get; set; }
        public int Year { get; set; }
        public int Periodo { get; set; }

        public virtual Asignatura CodigoAsignaturaNavigation { get; set; } = null!;
        public virtual Profesor RutNavigation { get; set; } = null!;
    }
}
