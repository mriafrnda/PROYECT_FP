using System;

namespace MesaDePartesUGEL
{
    public static class Expedientes
    {
        public const int MAX = 500;

        public static string[] Oficinas =
            { "Administración", "AGP", "AGI", "Dirección", "Asesoría", "Personal" };

        public static int[] Codigos = new int[MAX];
        public static string[] Tipos = new string[MAX];
        public static string[] Nombres = new string[MAX];
        public static string[] Documentos = new string[MAX];
        public static string[] Direcciones = new string[MAX];
        public static string[] Telefonos = new string[MAX];
        public static string[] Correos = new string[MAX];
        public static string[] Asuntos = new string[MAX];
        public static int[] OficinasDestino = new int[MAX];
        public static int[] Folios = new int[MAX];
        public static string[] Fechas = new string[MAX];

        public static int Cantidad = 0;

        public static void RegistrarExpediente()
        {
            if (Cantidad >= MAX)
            {
                Console.WriteLine("Capacidad llena. No se pueden registrar más expedientes.");
            }
            else
            {
                Console.WriteLine("\n--- Registro de expediente ---");
                string tipo = LeerTipo();
                string nombre;
                if (tipo == "Natural")
                {
                    nombre = LeerTexto("Apellidos y nombres: ");
                }
                else
                {
                    nombre = LeerTexto("Razón social: ");
                }
                string documento = LeerDocumento(tipo);
                string direccion = LeerTexto("Dirección: ");
                string telefono = LeerTelefono();
                string correo = LeerCorreo();
                string asunto = LeerTexto("Asunto del pedido: ");
                string fecha = LeerFecha();
                int oficina = LeerOficina();
                int folios = LeerFolios();

                int codigo = GenerarCodigo();

                Codigos[Cantidad] = codigo;
                Tipos[Cantidad] = tipo;
                Nombres[Cantidad] = nombre;
                Documentos[Cantidad] = documento;
                Direcciones[Cantidad] = direccion;
                Telefonos[Cantidad] = telefono;
                Correos[Cantidad] = correo;
                Asuntos[Cantidad] = asunto;
                OficinasDestino[Cantidad] = oficina;
                Folios[Cantidad] = folios;
                Fechas[Cantidad] = fecha;
                Cantidad++;

                Console.WriteLine($"\nExpediente registrado. Su código es: {FormatearCodigo(codigo)}");
            }
        }

        public static int GenerarCodigo()
        {
            int mayor = 0;
            for (int i = 0; i < Cantidad; i++)
            {
                if (Codigos[i] > mayor)
                {
                    mayor = Codigos[i];
                }
            }
            return mayor + 1;
        }

        // Muestra el código con 8 dígitos, rellenando con ceros a la izquierda
        public static string FormatearCodigo(int codigo)
        {
            string texto = "" + codigo;
            while (texto.Length < 8)
            {
                texto = "0" + texto;
            }
            return texto;
        }

        // Búsqueda secuencial: devuelve la posición del expediente o -1 si no existe
        public static int BuscarIndice(int codigoBuscado)
        {
            int indice = -1;
            for (int i = 0; i < Cantidad; i++)
            {
                if (Codigos[i] == codigoBuscado)
                {
                    indice = i;
                    break;
                }
            }
            return indice;
        }

        public static void ConsultarPorCodigo()
        {
            Console.Write("Código del expediente: ");
            string texto = Console.ReadLine().Trim();
            int codigoBuscado;
            if (!int.TryParse(texto, out codigoBuscado))
            {
                Console.WriteLine("Código inválido.");
            }
            else
            {
                int indice = BuscarIndice(codigoBuscado);
                if (indice == -1)
                {
                    Console.WriteLine("Expediente no encontrado.");
                }
                else
                {
                    MostrarExpediente(indice);
                }
            }
        }

        public static void OrdenarListado()
        {
            if (Cantidad == 0)
            {
                Console.WriteLine("No hay expedientes registrados.");
            }
            else
            {
                Console.Write("Ordenar por (1 = código, 2 = nombre): ");
                string criterio = Console.ReadLine().Trim();
                if (criterio != "1" && criterio != "2")
                {
                    Console.WriteLine("Opción inválida.");
                }
                else
                {
                    // Ordenamiento por selección
                    for (int i = 0; i < Cantidad - 1; i++)
                    {
                        int menor = i;
                        for (int j = i + 1; j < Cantidad; j++)
                        {
                            bool esMenor;
                            if (criterio == "1")
                            {
                                esMenor = Codigos[j] < Codigos[menor];
                            }
                            else
                            {
                                esMenor = Nombres[j].ToLower().CompareTo(Nombres[menor].ToLower()) < 0;
                            }
                            if (esMenor)
                            {
                                menor = j;
                            }
                        }
                        if (menor != i)
                        {
                            IntercambiarPosiciones(i, menor);
                        }
                    }
                    ListarExpedientes();
                }
            }
        }

        // Al ordenar se intercambia la posición en TODOS los arreglos paralelos
        private static void IntercambiarPosiciones(int a, int b)
        {
            IntercambiarEnteros(Codigos, a, b);
            IntercambiarTextos(Tipos, a, b);
            IntercambiarTextos(Nombres, a, b);
            IntercambiarTextos(Documentos, a, b);
            IntercambiarTextos(Direcciones, a, b);
            IntercambiarTextos(Telefonos, a, b);
            IntercambiarTextos(Correos, a, b);
            IntercambiarTextos(Asuntos, a, b);
            IntercambiarEnteros(OficinasDestino, a, b);
            IntercambiarEnteros(Folios, a, b);
            IntercambiarTextos(Fechas, a, b);
        }

        private static void IntercambiarEnteros(int[] arreglo, int a, int b)
        {
            int temp = arreglo[a];
            arreglo[a] = arreglo[b];
            arreglo[b] = temp;
        }

        private static void IntercambiarTextos(string[] arreglo, int a, int b)
        {
            string temp = arreglo[a];
            arreglo[a] = arreglo[b];
            arreglo[b] = temp;
        }

        private static void ListarExpedientes()
        {
            Console.WriteLine("\nCódigo   | Solicitante | Oficina | Fecha");
            for (int i = 0; i < Cantidad; i++)
            {
                Console.WriteLine($"{FormatearCodigo(Codigos[i])} | {Nombres[i]} | {Oficinas[OficinasDestino[i]]} | {Fechas[i]}");
            }
        }

        private static void MostrarExpediente(int i)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Código:      {FormatearCodigo(Codigos[i])}");
            Console.WriteLine($"Solicitante: {Nombres[i]} ({Tipos[i]})");
            Console.WriteLine($"Documento:   {Documentos[i]}");
            Console.WriteLine($"Dirección:   {Direcciones[i]}");
            Console.WriteLine($"Teléfono:    {Telefonos[i]}");
            Console.WriteLine($"Correo:      {Correos[i]}");
            Console.WriteLine($"Asunto:      {Asuntos[i]}");
            Console.WriteLine($"Oficina:     {Oficinas[OficinasDestino[i]]}");
            Console.WriteLine($"Folios:      {Folios[i]}");
            Console.WriteLine($"Fecha:       {Fechas[i]}");
            Console.WriteLine("-----------------------------------");
        }

        private static string LeerTexto(string mensaje)
        {
            string valor;
            bool valido;
            do
            {
                Console.Write(mensaje);
                valor = Console.ReadLine().Trim();
                valido = Validaciones.EsTextoValido(valor);
                if (!valido)
                {
                    Console.WriteLine("Dato inválido: no puede estar vacío ni contener el carácter |.");
                }
            } while (!valido);
            return valor;
        }

        private static string LeerTipo()
        {
            string opcion;
            bool valido;
            do
            {
                Console.Write("Tipo de solicitante (1 = Persona natural, 2 = Persona jurídica): ");
                opcion = Console.ReadLine().Trim();
                valido = opcion == "1" || opcion == "2";
                if (!valido)
                {
                    Console.WriteLine("Opción inválida.");
                }
            } while (!valido);

            if (opcion == "1")
            {
                return "Natural";
            }
            return "Juridica";
        }

        private static string LeerDocumento(string tipo)
        {
            string documento;
            bool valido;
            do
            {
                if (tipo == "Natural")
                {
                    Console.Write("DNI (8 dígitos): ");
                }
                else
                {
                    Console.Write("RUC (11 dígitos): ");
                }
                documento = Console.ReadLine().Trim();
                if (tipo == "Natural")
                {
                    valido = Validaciones.EsDniValido(documento);
                }
                else
                {
                    valido = Validaciones.EsRucValido(documento);
                }
                if (!valido)
                {
                    Console.WriteLine("Documento inválido. Revise la cantidad de dígitos.");
                }
            } while (!valido);
            return documento;
        }

        private static string LeerTelefono()
        {
            string telefono;
            bool valido;
            do
            {
                Console.Write("Teléfono (9 dígitos): ");
                telefono = Console.ReadLine().Trim();
                valido = Validaciones.EsTelefonoValido(telefono);
                if (!valido)
                {
                    Console.WriteLine("Teléfono inválido.");
                }
            } while (!valido);
            return telefono;
        }

        private static string LeerCorreo()
        {
            string correo;
            bool valido;
            do
            {
                Console.Write("Correo electrónico: ");
                correo = Console.ReadLine().Trim();
                valido = Validaciones.EsCorreoValido(correo);
                if (!valido)
                {
                    Console.WriteLine("Correo inválido. El correo es obligatorio.");
                }
            } while (!valido);
            return correo;
        }

        private static string LeerFecha()
        {
            string fecha;
            bool valido;
            do
            {
                Console.Write("Fecha de recepción (dd/mm/aaaa): ");
                fecha = Console.ReadLine().Trim();
                valido = Validaciones.EsFechaValida(fecha);
                if (!valido)
                {
                    Console.WriteLine("Fecha inválida. Use el formato dd/mm/aaaa.");
                }
            } while (!valido);
            return fecha;
        }

        private static int LeerOficina()
        {
            Console.WriteLine("Oficina de destino:");
            for (int i = 0; i < Oficinas.Length; i++)
            {
                Console.WriteLine($"  {i + 1}. {Oficinas[i]}");
            }
            int opcion;
            bool valido;
            do
            {
                Console.Write("Elija una oficina: ");
                valido = int.TryParse(Console.ReadLine().Trim(), out opcion) && opcion >= 1 && opcion <= Oficinas.Length;
                if (!valido)
                {
                    Console.WriteLine("Opción inválida.");
                }
            } while (!valido);
            return opcion - 1;
        }

        private static int LeerFolios()
        {
            int folios;
            bool valido;
            do
            {
                Console.Write("Cantidad de folios: ");
                valido = int.TryParse(Console.ReadLine().Trim(), out folios) && folios >= 1 && folios <= 9999;
                if (!valido)
                {
                    Console.WriteLine("Cantidad inválida (use un número entre 1 y 9999).");
                }
            } while (!valido);
            return folios;
        }
    }
}
