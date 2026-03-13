namespace CsvJsonXmlStorae.Exceptions;

public abstract class CiudadanosExceptions(string mensaje) : DomainExceptions(mensaje) {
    public sealed class NoTFound(string id)
        : CiudadanosExceptions($"No se ha encontrado ningun ciudadano con el id: {id}");
    public sealed class StorageError(string details)
        : CiudadanosExceptions($"Error de almacenamiento: {details}");
    
    public sealed class AlreadyExists(int telefono)
        : CiudadanosExceptions($"Conflicto de integridad: El Telefono {telefono} ya está registrado en el sistema.");
}