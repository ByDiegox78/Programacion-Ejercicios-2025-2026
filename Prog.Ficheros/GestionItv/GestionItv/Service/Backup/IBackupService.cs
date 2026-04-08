using GestionItv.Models;

namespace GestionItv.Service.Backup;

public interface IBackupService {
 
    string RealizarBackup(IEnumerable<Vehiculo> vehiculos);

    IEnumerable<Vehiculo> RestaurarBackup(string archivoBackup);

    IEnumerable<string> ListarBackups();
}