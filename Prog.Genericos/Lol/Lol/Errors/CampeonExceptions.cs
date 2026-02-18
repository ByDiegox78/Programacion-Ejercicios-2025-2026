using Lol.Errors.Common;

namespace Lol.Errors;

public abstract class CampeonExceptions(string message): DomainExceptions(message) {
    public sealed class NotFound(string id)
        : CampeonExceptions("$\"No se ha encontrado ningun campeon con el identificador: {id}\"");
    
    public sealed class Validation(IEnumerable<string> errors)
        : CampeonExceptions("Se han detectado errores de validación en la entidad.") {
        public IEnumerable<string> Errores { get; init; } = errors;
    }

    /// <summary>Se lanza ante conflictos de duplicidad (DNI).</summary>
    public sealed class AlreadyExists(string nombre)
        : CampeonExceptions($"Conflicto de integridad: El DNI {nombre} ya está registrado en el sistema.");
}