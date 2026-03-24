using System.Text.RegularExpressions;
using GestionItv.Config;

namespace GestionItv.Utils;

public static class Utilities {
    public static bool ValidarDni(string dni) {
        if (!Configuracion.RegexDni.IsMatch(dni))
            return false;
        int numero = int.Parse(dni.Substring(0, 8));
        char letraCorrecta = Configuracion.LetrasDniPermitidas[numero % 23];
        return dni[8] == letraCorrecta;
    }
}