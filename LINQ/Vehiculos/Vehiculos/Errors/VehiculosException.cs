using Vehiculos.Errors.Common;

namespace Vehiculos.Errors;

public class VehiculosException(string mensajes) : DomainException(mensajes) {
    public sealed class VehiculoNotFoundException(string matricula)
        : VehiculosException($"No se ha encontrado ningun vehiculo con el identificador: {matricula}");

   
    public sealed class Validation(List<string> errors)
        : VehiculosException("Errores de validación.") {
        public List<string> Errores { get; init; } = errors;
    }
    
    public sealed class InvalidMatriculaException(string matricula)
        : VehiculosException($"Conflicto de integridad: La matricula {matricula} ya está registrado en el sistema.");
}