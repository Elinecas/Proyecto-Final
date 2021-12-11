using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Modelos
{
    public class Citas
    {

        public int Id { get; set; }
        public int Id_Paciente { get; set; }
        public int Id_Medico { get; set; }
        public DateTime Fecha_Cita { get; set; }
        public string Causa_Cita { get; set; }
        public string Estado_Cita { get; set; }

    }
}
