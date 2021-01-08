using Prueba.Entidades;
using System;
using System.Collections.Generic;
using System.IO;

namespace Prueba.Servicios
{
    public class PacienteServicio 
    {
        private static string nombreArchivo = $@"{Environment.CurrentDirectory}\Pacientes.txt";
        
        public PacienteServicio()
        {

        }

        public static List<Paciente> ObtenerPacientesDelArchivo(string dni)
        {
            List<Paciente> Pacientes = new List<Paciente>();

            string[] archivoPaciente = File.ReadAllLines(nombreArchivo);

            for (int i = 0; i < archivoPaciente.Length; i++)
            {
                var linea = archivoPaciente[i];

                var pacienteNuevo = new Paciente();

                pacienteNuevo.Dni = linea.Substring(0, 8);
                pacienteNuevo.ApeyNom = linea.Substring(8, 50).Trim();
                pacienteNuevo.Telefono = linea.Substring(58, 10);
                pacienteNuevo.Direccion = linea.Substring(68, 60);
                pacienteNuevo.Mail = linea.Substring(128, 50);
                pacienteNuevo.EstaEliminado = bool.Parse(linea.Substring(178, 5));
                pacienteNuevo.Ficha = linea.Substring(183, 8);
                if (dni.Equals(""))
                {
                    Pacientes.Add(pacienteNuevo);
                }
                else
                {
                    if(pacienteNuevo.Dni == dni)
                    {
                        Pacientes.Add(pacienteNuevo);
                    }
                }
               
            }
            return Pacientes;
        }

        public static void Grabar(Paciente paciente)
        {
            var archivo = new StreamWriter(nombreArchivo, true);

            var crearLinea = $"{paciente.Dni.PadLeft(8, '0')}" +
                             $"{paciente.ApeyNom.PadRight(50, ' ')}" +
                             $"{paciente.Telefono.PadRight(10, ' ')}" +
                             $"{paciente.Direccion.PadRight(60, ' ')}" +
                             $"{paciente.Mail.PadRight(50, ' ')}" +
                             $"{paciente.EstaEliminado.ToString().PadRight(5, ' ')}" +
                             $"{paciente.Ficha.ToString().PadRight(8, ' ')}";

            archivo.WriteLine(crearLinea);
            archivo.Close();

        }



    }
}
