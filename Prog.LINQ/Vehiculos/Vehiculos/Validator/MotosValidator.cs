using System.Text.RegularExpressions;
using Vehiculos.Models;

namespace Vehiculos.Validator;

public class MotosValidator: IValidador<Vehiculo> {
    public IEnumerable<string> Validar(Vehiculo vehiculo) {
         var errores = new List<string>();

        if (vehiculo is not Moto moto) {
            errores.Add("El vehiculo no es una moto");
            return errores;
        }
        if (moto.Matricula.Length != 7 || string.IsNullOrWhiteSpace(moto.Matricula)) {
            errores.Add("La Matricula no contiene 7 caracteres o esta vacia");
        }
        var regex = new Regex("^[0-9]{4}[A-Z]{3}$");
        if (!regex.IsMatch(moto.Matricula)) {
            errores.Add("La matrícula debe tener 4 números y 3 letras mayúsculas");
        }

        if (moto.AñoMatriculacion is < 2000 or > 2025) {
            errores.Add("El año debe ser entre 2000 y 2025");
        }
        return errores;
    }
}
