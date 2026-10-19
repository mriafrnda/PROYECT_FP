namespace MesaDePartesUGEL
{
    public static class Validaciones
    {
        public static bool EsDniValido(string dni)
        {
            return SoloDigitos(dni) && dni.Length == 8;
        }

        public static bool EsRucValido(string ruc)
        {
            return SoloDigitos(ruc) && ruc.Length == 11;
        }

        public static bool EsTelefonoValido(string telefono)
        {
            return SoloDigitos(telefono) && telefono.Length == 9;
        }

        public static bool EsCorreoValido(string correo)
        {
            if (!EsTextoValido(correo) || correo.Contains(" "))
            {
                return false;
            }
            int arroba = correo.IndexOf('@');
            int punto = correo.LastIndexOf('.');
            return arroba > 0 && punto > arroba + 1 && punto < correo.Length - 1;
        }

        public static bool EsTextoValido(string texto)
        {
            return texto.Trim() != "" && !texto.Contains("|");
        }

        // Formato dd/mm/aaaa: se separa con Split y se revisa cada parte
        public static bool EsFechaValida(string fecha)
        {
            string[] partes = fecha.Split('/');
            if (partes.Length != 3)
            {
                return false;
            }
            if (!SoloDigitos(partes[0]) || !SoloDigitos(partes[1]) || !SoloDigitos(partes[2]))
            {
                return false;
            }
            if (partes[0].Length != 2 || partes[1].Length != 2 || partes[2].Length != 4)
            {
                return false;
            }
            int dia = int.Parse(partes[0]);
            int mes = int.Parse(partes[1]);
            int anio = int.Parse(partes[2]);
            return dia >= 1 && dia <= 31 && mes >= 1 && mes <= 12 && anio >= 2000 && anio <= 2100;
        }

        private static bool SoloDigitos(string texto)
        {
            if (texto.Length == 0)
            {
                return false;
            }
            foreach (char c in texto)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
