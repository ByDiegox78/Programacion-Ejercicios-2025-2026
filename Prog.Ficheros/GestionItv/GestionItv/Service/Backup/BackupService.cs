using System.IO.Compression;
using GestionItv.Config;
using GestionItv.Exceptions.Backup;
using GestionItv.Models;
using GestionItv.Storage.Common;
using Serilog;

namespace GestionItv.Service.Backup;

public class BackupService(
    IStorage<Vehiculo> storage
    ) : IBackupService {
    private readonly string _backDirectory = Configuracion.BackupDirectory;
    private readonly ILogger _logger = Log.ForContext<BackupService>();
    public string RealizarBackup(IEnumerable<Vehiculo> vehiculos) {
        _logger.Information("Iniciando proceso de backup.");

        var list = vehiculos.ToList();
        if (list.Count == 0) {
            _logger.Warning("No hay datos para respaldar.");
            throw new BackupException.CreationError("No hay datos para respaldar.");
        }
        try {
            Directory.CreateDirectory(_backDirectory);
        }
        catch (Exception ex) {
            _logger.Error(ex, "Error al crear el directorio de backup: {dir}", _backDirectory);
            throw new BackupException.DirectoryError($"No se pudo crear el directorio: {_backDirectory}");
        }

        var tempDir = Path.Combine(Path.GetTempPath(), $"backup-{Guid.NewGuid()}");
        Directory.CreateDirectory(tempDir);

        try {
            var jsonPath = Path.Combine(tempDir, "data.json");
            try {
                storage.WriteToFile(list, jsonPath);
            }
            catch (Exception ex) {
                _logger.Error(ex, "Error al serializar los datos.");
                throw new BackupException.CreationError("Error al serializar los datos.");
            }

            var fecha = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            var zipPath = Path.Combine(_backDirectory, $"{fecha}-back.zip");

            try {
                ZipFile.CreateFromDirectory(tempDir, zipPath);
            }
            catch (Exception ex) {
                _logger.Error(ex, "Error al crear el archivo ZIP.");
                throw new BackupException.CreationError("Error al comprimir el backup.");
            }

            _logger.Information("Backup creado correctamente: {zipPath}", zipPath);
            return zipPath;
        }
        finally {
            if (Directory.Exists(tempDir)) {
                Directory.Delete(tempDir, true);
                _logger.Debug("Directorio temporal limpiado.");
            }
        }
    }

    public IEnumerable<Vehiculo> RestaurarBackup(string archivoBackup) {
        _logger.Information("Iniciando restauración desde: {archivo}", archivoBackup);

        if (!File.Exists(archivoBackup)) {
            _logger.Warning("Archivo de backup no encontrado: {path}", archivoBackup);
            throw new BackupException.FileNotFound(archivoBackup);
        }

        var tempDir = Path.Combine(Path.GetTempPath(), $"restore-{Guid.NewGuid()}");
        Directory.CreateDirectory(tempDir);

        try {
            try {
                ZipFile.ExtractToDirectory(archivoBackup, tempDir);
            }
            catch (Exception ex) {
                _logger.Error(ex, "Error al extraer el archivo ZIP.");
                throw new BackupException.InvalidBackupFile("No se pudo extraer el archivo ZIP.");
            }

            var jsonPath = Path.Combine(tempDir, "data.json");
            if (!File.Exists(jsonPath)) {
                _logger.Warning("El archivo de backup no contiene datos válidos (data.json no encontrado).");
                throw new BackupException.InvalidBackupFile("El archivo de backup no contiene datos válidos.");
            }

            try {
                var personas = storage.ReadFromFile(jsonPath);
                _logger.Information("Datos extraídos del backup correctamente.");
                return personas;
            }
            catch (Exception ex) {
                _logger.Error(ex, "Error al deserializar los datos del backup.");
                throw new BackupException.InvalidBackupFile("El archivo de backup contiene datos corruptos.");
            }
        }
        finally {
            if (Directory.Exists(tempDir)) {
                Directory.Delete(tempDir, true);
                _logger.Debug("Directorio temporal limpiado.");
            }
        }
    }

    public IEnumerable<string> ListarBackups() {
        if (!Directory.Exists(_backDirectory)) return Enumerable.Empty<string>();

        return Directory.GetFiles(_backDirectory, "*.zip")
            .OrderByDescending(f => File.GetCreationTime(f));
    }
}