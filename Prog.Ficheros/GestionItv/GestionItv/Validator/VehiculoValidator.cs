using System.Text.RegularExpressions;
using GestionItv.Config;
using GestionItv.Models;

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

        if (entidad.cilindrada < Configuracion.MinCilindrada && entidad.cilindrada > Configuracion.MaxCilindrada) {
            errores.Add($"La cilidrada no puede ser menor de {Configuracion.MaxCilindrada} y mayor de {Configuracion.MaxCilindrada}");
        }
            
    }
    
}