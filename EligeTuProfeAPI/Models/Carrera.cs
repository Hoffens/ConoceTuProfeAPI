using System;
using System.Collections.Generic;

namespace EligeTuProfeAPI.Models
{
    public partial class Carrera
    {
        public Carrera()
        {
            Asignaturas = new HashSet<Asignatura>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Codigo { get; set; }
        public int CodigoPlan { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Modalidad { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Asignatura> Asignaturas { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
