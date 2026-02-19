using System.Text.RegularExpressions;
using Vehiculos.Models;

namespace Vehiculos.Validator;

public class CamionValdator: IValidador<Vehiculo> {
    public IEnumerable<string> Validar(Vehiculo vehiculo) {
        var errores = new List<string>();

        if (vehiculo is not Camion camion) {
            errores.Add("El vehiculo no es un camion");
            return errores;
        }
        if (camion.Matricula.Length != 7 || string.IsNullOrWhiteSpace(camion.Matricula)) {
            errores.Add("La Matricula no contiene 7 caracteres o esta vacia");
        }
        var regex = new Regex("^[0-9]{4}[A-Z]{3}$");
        if (!regex.IsMatch(camion.Matricula)) {
            errores.Add("La matrícula debe tener 4 números y 3 letras mayúsculas");
        }
        if (camion.AñoMatriculacion is < 2000 or > 2025) {
            errores.Add("El año debe ser entre 2000 y 2025");
        }
        return errores;
    }
}
