using System.Text.RegularExpressions;
using GestionItv.Config;
using GestionItv.Models;
using GestionItv.Utils;

namespace GestionItv.Validator;

public class VehiculoValidator : IVehiculoValidator<Vehiculo> {
    
    public IEnumerable<string> Validar(Vehiculo entidad) {
        
        var errores = new List<string>();
        if (!Configuracion.RegexMatricula.IsMatch(entidad.Matricula)) {
            errores.Add("La matricula no cumple la regla de NNNNLLL");
            
        }

        if (string.IsNullOrWhiteSpace(entidad.Marca) || entidad.Marca.Length < 2) {
            errores.Add("La marca debe contener al manos 2 carazteres");
            
        }

        bool esElectricoConCilindradaCero = entidad.TipoMotor == Motor.Electrico && entidad.Cilindrada == 0;
        bool estaEnRangoValido = entidad.Cilindrada >= Configuracion.MinCilindrada && entidad.Cilindrada <= Configuracion.MaxCilindrada;

        if (!esElectricoConCilindradaCero && !estaEnRangoValido) {
            errores.Add($"La cilindrada debe estar entre {Configuracion.MinCilindrada} y {Configuracion.MaxCilindrada} (excepto vehículos eléctricos)");
        }

        if (!Enum.IsDefined(typeof(Motor), entidad.TipoMotor)) {
            errores.Add("El tipo de motor no coincide con los disponibles");
        }

        if (!Utilities.ValidarDni(entidad.DniPropietario)) {
            // Cálculo rápido para el log de error
            int num = int.Parse(entidad.DniPropietario.Substring(0, 8));
            char letraEsperada = Configuracion.LetrasDniPermitidas[num % 23];
            errores.Add($"DNI {entidad.DniPropietario} inválido. Para el número {num} la letra debería ser {letraEsperada}");
        }
        return errores;
    }
    
}