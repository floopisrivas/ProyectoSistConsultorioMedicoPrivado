using System;
using System.Collections.Generic;
using System.IO;
using Prueba.Entidades;

namespace Prueba.Servicios
{
    public class MedicoServicio 
    {
        

        private static string nombreArchivo = $@"{Environment.CurrentDirectory}\Medicos.txt";
        public MedicoServicio()
        {

        }

        public static List<Medico> ObtenerMedicosDelArchivo(string dni)
        {
            List<Medico> Medicos = new List<Medico>();

            string[] archivoMedico = File.ReadAllLines(nombreArchivo);

            for (int i = 0; i < archivoMedico.Length; i++)
            {
                var linea = archivoMedico[i];

                var medicoNuevo = new Entidades.Medico();

                medicoNuevo.Dni = linea.Substring(0, 8);
                medicoNuevo.ApeyNom = linea.Substring(8, 50).Trim();
                medicoNuevo.Telefono = linea.Substring(58, 10);
                medicoNuevo.Direccion = linea.Substring(68, 60);
                medicoNuevo.Mail = linea.Substring(128, 50);
                medicoNuevo.EstaEliminado = bool.Parse(linea.Substring(178, 5));
                medicoNuevo.Matricula = int.Parse(linea.Substring(183, 8));


                if (dni.Equals(""))
                {
                    Medicos.Add(medicoNuevo);
                }
                else
                {
                    if (medicoNuevo.Dni == dni)
                    {
                        Medicos.Add(medicoNuevo);
                    }
                }

                

            }
            return Medicos;

        }


        public static void Grabar(Medico medico)
        {

            var archivo = new StreamWriter(nombreArchivo, true);

            var crearLinea = $"{medico.Dni.PadLeft(8, '0')}" +
                             $"{medico.ApeyNom.PadRight(50, ' ')}" +
                             $"{medico.Telefono.PadRight(10, ' ')}" +
                             $"{medico.Direccion.PadRight(60, ' ')}" +
                             $"{medico.Mail.PadRight(50, ' ')}" +
                             $"{medico.EstaEliminado.ToString().PadRight(5, ' ')}" +
                             $"{medico.Matricula.ToString().PadRight(8, ' ')}";

            archivo.WriteLine(crearLinea);

            archivo.Close();

        }


    }
}
