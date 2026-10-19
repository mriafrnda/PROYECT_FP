using System;

namespace MesaDePartesUGEL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Archivo.CargarArchivo();

            string opcion;
            do
            {
                Console.WriteLine("\n=== MESA DE PARTES - UGEL CAJAMARCA ===");
                Console.WriteLine("1. Registrar expediente");
                Console.WriteLine("2. Consultar por código");
                Console.WriteLine("3. Ordenar y listar expedientes");
                Console.WriteLine("4. Guardar y salir");
                Console.Write("Elija una opción: ");
                opcion = Console.ReadLine().Trim();

                switch (opcion)
                {
                    case "1":
                        Expedientes.RegistrarExpediente();
                        break;
                    case "2":
                        Expedientes.ConsultarPorCodigo();
                        break;
                    case "3":
                        Expedientes.OrdenarListado();
                        break;
                    case "4":
                        Archivo.GuardarArchivo();
                        Console.WriteLine("Datos guardados. Hasta pronto.");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            } while (opcion != "4");
        }
    }
}
