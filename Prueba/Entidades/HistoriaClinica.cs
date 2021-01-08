using System;

namespace Prueba.Entidades
{
    public class HistoriaClinica
    {

        public DateTime Fecha { get; set; }

        public string IdentificacionPaciente { get; set; }

        public string IdentificacionMedico { get; set; }

        public string MotivoIngreso { get; set; }

        public string Diagnostico { get; set; }

        public string Tratamiento { get; set; }

    }
}
