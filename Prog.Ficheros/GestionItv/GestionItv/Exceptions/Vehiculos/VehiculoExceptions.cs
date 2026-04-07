namespace GestionItv.Exceptions.Vehiculos;

public abstract class VehiculoException(string message) : DomainException(message) {
    
    /// <summary>
    /// Se lanza cuando no existe el registro solicitado
    /// </summary>
    /// <param name="id"></param>
    public sealed class NotFound(string id)
        : VehiculoException($"No se ha encontrado ningun vehiculo con el id: {id}");
    
    /// <summary>
    /// Se lanza cuando fallan las reglas de validacion
    /// </summary>
    /// <param name="errors"></param>
    public sealed class Validation(IEnumerable<string> errors)
        : VehiculoException("No han detectado errores de validacion en la entidad") {
        public IEnumerable<string> Errores { get; init; } = errors;
    }
    
    /// <summary>
    /// Se laza cuanado hay conflicto de duplicidad
    /// </summary>
    /// <param name="matricula"></param>
    public sealed class AlreadyExists (string matricula)
        : VehiculoException($"Conflicto de integridad: La matricula '{matricula}' ya existe");
    
    
    /// <summary>
    /// Se lanza ante errores relacionado con el almacenamiento de datos
    /// </summary>
    /// <param name="details"></param>
    public sealed class StorageError(string details)
        : VehiculoException($"Error de almacenamiento: {details}");
}