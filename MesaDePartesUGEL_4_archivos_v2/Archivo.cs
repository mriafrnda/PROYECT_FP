using System;
using System.IO;

namespace MesaDePartesUGEL
{
    public static class Archivo
    {
        private const string RUTA = "expedientes.txt";

        public static void CargarArchivo()
        {
            try
            {
                using (StreamReader lector = new StreamReader(RUTA))
                {
                    string linea = lector.ReadLine();
                    while (linea != null)
                    {
                        if (linea.Trim() != "" && Expedientes.Cantidad < Expedientes.MAX)
                        {
                            CargarLinea(linea);
                        }
                        linea = lector.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No se encontró archivo previo, iniciando sin expedientes.");
            }
        }

        private static void CargarLinea(string linea)
        {
            string[] campos = linea.Split('|');
            if (campos.Length == 11)
            {
                int codigo;
                int oficina;
                int folios;
                bool codigoOk = int.TryParse(campos[0], out codigo);
                bool oficinaOk = int.TryParse(campos[8], out oficina);
                bool foliosOk = int.TryParse(campos[9], out folios);

                if (codigoOk && oficinaOk && foliosOk && oficina >= 0 && oficina < Expedientes.Oficinas.Length)
                {
                    int i = Expedientes.Cantidad;
                    Expedientes.Codigos[i] = codigo;
                    Expedientes.Tipos[i] = campos[1];
                    Expedientes.Nombres[i] = campos[2];
                    Expedientes.Documentos[i] = campos[3];
                    Expedientes.Direcciones[i] = campos[4];
                    Expedientes.Telefonos[i] = campos[5];
                    Expedientes.Correos[i] = campos[6];
                    Expedientes.Asuntos[i] = campos[7];
                    Expedientes.OficinasDestino[i] = oficina;
                    Expedientes.Folios[i] = folios;
                    Expedientes.Fechas[i] = campos[10];
                    Expedientes.Cantidad++;
                }
            }
        }

        public static void GuardarArchivo()
        {
            try
            {
                using (StreamWriter escritor = new StreamWriter(RUTA))
                {
                    for (int i = 0; i < Expedientes.Cantidad; i++)
                    {
                        string linea = $"{Expedientes.Codigos[i]}|{Expedientes.Tipos[i]}|{Expedientes.Nombres[i]}|"
                                     + $"{Expedientes.Documentos[i]}|{Expedientes.Direcciones[i]}|{Expedientes.Telefonos[i]}|"
                                     + $"{Expedientes.Correos[i]}|{Expedientes.Asuntos[i]}|{Expedientes.OficinasDestino[i]}|"
                                     + $"{Expedientes.Folios[i]}|{Expedientes.Fechas[i]}";
                        escritor.WriteLine(linea);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("No se pudo guardar: el archivo está protegido o abierto en otro programa.");
            }
        }
    }
}
