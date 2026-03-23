namespace GestionItv.Exceptions.Vehiculos;

public class VehiculosExceptions(string message) : DomainException(message) {
    public sealed class Validation(IEnumerable<string> errors)
        : VehiculosExceptions("Se han detectado errores de validación en la entidad.") {
        public IEnumerable<string> Errores { get; init; } = errors;
    }
}