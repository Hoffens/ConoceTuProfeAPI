using System;
using System.Collections.Generic;

namespace EligeTuProfe.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            InscripcionAlumno = new HashSet<InscripcionAlumno>();
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
        public virtual ICollection<InscripcionAlumno> InscripcionAlumno { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }

    }
}
