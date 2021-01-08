using Prueba.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Prueba.Servicios
{
    public class HistoriaClinicaServicio
    {
        private static string nombreArchivo = $@"{Environment.CurrentDirectory}\HistoriaClinica.txt";

        public HistoriaClinicaServicio()
        {

        }
        public static List<HistoriaClinica> ObtenerMovimientosDelArchivo(string identificacion)
        {
             
            List<HistoriaClinica> HistoriasClinicas = new List<HistoriaClinica>();

            string[] archivoHistoriaClinica = File.ReadAllLines(nombreArchivo);

            for (int i = 0; i < archivoHistoriaClinica.Length; i++)
            {
                var linea = archivoHistoriaClinica[i];

                if (!string.IsNullOrEmpty(linea))
                {
                    var historiaClinicaNueva = new HistoriaClinica();

                    historiaClinicaNueva.IdentificacionPaciente = linea.Substring(0, 8);
                    historiaClinicaNueva.IdentificacionMedico = linea.Substring(8, 10);
                    historiaClinicaNueva.MotivoIngreso = linea.Substring(18, 70);
                    historiaClinicaNueva.Diagnostico = linea.Substring(88, 80);
                    historiaClinicaNueva.Tratamiento = linea.Substring(168, 100);
                    historiaClinicaNueva.Fecha = DateTime.Parse(linea.Substring(268, 10));

                    HistoriasClinicas.Add(historiaClinicaNueva);
                }

            }

            return HistoriasClinicas
                .Where(x => x.IdentificacionPaciente == identificacion)
                .ToList();
        }


        public static void Add(Entidades.HistoriaClinica historiaClinica)
        {
            
            var archivo = new StreamWriter(nombreArchivo, true);


            var crearLinea = $"{historiaClinica.IdentificacionPaciente.PadLeft(8, '0')}" +
                             $"{historiaClinica.IdentificacionMedico.PadRight(10, ' ')}" +
                             $"{historiaClinica.MotivoIngreso.PadRight(70, ' ')}" +
                              $"{historiaClinica.Diagnostico.PadRight(80, ' ')}" +
                              $"{historiaClinica.Tratamiento.PadRight(100, ' ')}" +
                              $"{historiaClinica.Fecha.ToString().PadRight(10, ' ')}";


            archivo.WriteLine(crearLinea);
            archivo.Close();

        }




    }
}
