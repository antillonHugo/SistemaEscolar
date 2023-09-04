using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SistemaEscolar.Helpers
{
    public static class LimpiarCampos
    {
        public static string EliminarCaracteresEspeciales(object input)
        {
            if (input == null)
            {
                return null;
            }

            // Convertir el valor de entrada en una cadena
            string inputString = input.ToString();

            // Definir una expresión regular para permitir solo letras y números
            // Esto reemplazará todos los caracteres no permitidos por una cadena vacía
            string pattern = @"[^a-zA-Z0-9@.]";
            return Regex.Replace(inputString, pattern, "");

        }

    }
}