using GestionItv.Models;

namespace GestionItv.Service;

public interface IVehiculoService {
    
    int TotalVehiculos { get; }

    IEnumerable<Vehiculo> GetAll();

    Vehiculo GetById(int id);

    Vehiculo GetByMatricula(string matricula);

    Vehiculo Save(Vehiculo vehiculo);

    Vehiculo Update(int id, Vehiculo vehiculo);
    
      
    IEnumerable<Informe> GenerarTodosInformeVehiculo();

    Informe GenerarInformeVehiculPorId(int id);

    Vehiculo Delete(int id);

    Vehiculo HardDelete(int id);

    bool DeleteAll();

    int ImportarDatos();
 
    int ExportarDatos();
    
    string RealizarBackup();

    int RestaurarBackup(string archivoBackup);

    IEnumerable<string> ListarBackups();
    
}