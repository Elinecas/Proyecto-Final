using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Modelos
{
    public class Pacientes
    {

        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Fumador { get; set; }
        public string Alergias { get; set; }
        public string Foto { get; set; }

    }
}
