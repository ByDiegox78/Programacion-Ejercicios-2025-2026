using System.Text.RegularExpressions;
using GestionItv.Config;
using GestionItv.Models;
using GestionItv.Utils;

namespace GestionItv.Validator;

public class VehiculoValidator : IVehiculoValidator<Vehiculo> {
    
    public IEnumerable<string> Validar(Vehiculo entidad) {
        
        var errores = new List<string>();
        if (Configuracion.RegexMatricula.IsMatch(entidad.Matricula)) {
            errores.Add("La matricula no cumple la regla de NNNNLLL");
            return errores;
        }

        if (string.IsNullOrWhiteSpace(entidad.Marca) || entidad.Marca.Length < 2) {
            errores.Add("La marca debe contener al manos 2 carazteres");
            return errores;
        }

        if (entidad.Cilindrada < Configuracion.MinCilindrada && entidad.Cilindrada > Configuracion.MaxCilindrada) {
            errores.Add($"La cilidrada no puede ser menor de {Configuracion.MaxCilindrada} y mayor de {Configuracion.MaxCilindrada}");
            return errores;
        }

        if (!Enum.IsDefined(entidad.TipoMotor)) {
            errores.Add("El tipo de motor no coindice con los disponibles");
            return errores;
        }

        if (!Utilities.ValidarDni(entidad.DniPropietario)) {
            errores.Add("El dni del propietario no es el correcto");
            return errores;
        }

        return errores;
    }
    
}