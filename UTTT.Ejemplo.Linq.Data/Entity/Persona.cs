using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTTT.Ejemplo.Linq.Data.Entity
{
    public partial class Persona
    {
        private String nombreCompleto;
        public string NombreCompleto { get => this.strNombre + " " + this.strAPaterno + ((this.strAMaterno.Length > 0) ? " " + this.strAMaterno : ""); set => NombreCompleto = value; }
    }
}
