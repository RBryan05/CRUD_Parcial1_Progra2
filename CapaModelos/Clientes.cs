using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Clientes
    {
        public int Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string EstadoCivil { get; set; }
        public bool Activo { get; set; }
    }
}
