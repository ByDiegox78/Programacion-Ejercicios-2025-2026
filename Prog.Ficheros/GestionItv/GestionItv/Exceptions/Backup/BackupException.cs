namespace GestionItv.Exceptions.Backup;

public abstract class BackupException(string message) : DomainException(message) {
    public sealed class FileNotFound(string filePath)
        : BackupException($"No se ha encontrado el archivo de backup: {filePath}");
    
    public sealed class InvalidBackupFile(string details)
        : BackupException($"El archivo de backup es inválido o está corrupto: {details}");
    
    public sealed class CreationError(string details)
        : BackupException($"Error al crear el backup: {details}");

    public sealed class RestorationError(string details)
        : BackupException($"Error al restaurar el backup: {details}");

    public sealed class DirectoryError(string details)
        : BackupException($"Error con el directorio de backup: {details}");
}