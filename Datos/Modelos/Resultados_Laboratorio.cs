using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Modelos
{
    public class Resultados_Laboratorio
    {

        public int Id { get; set; }
        public int Id_Paciente { get; set; }
        public int Id_Cita { get; set; }
        public int Id_PruebaLaboratorio { get; set; }
        public int Id_Medico { get; set; }
        public string Resultado_Prueba { get; set; }
        public string Estado_Resultado { get; set; }

    }
}
