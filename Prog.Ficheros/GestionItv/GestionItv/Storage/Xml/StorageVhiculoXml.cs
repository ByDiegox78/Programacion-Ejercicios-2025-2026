using GestionItv.Models;

namespace GestionItv.Storage;

public class StorageVhiculoXml : IStorageVehiculoXml {
   
    
    public void WriteToFile(IEnumerable<Vehiculo> items, string path) {
        throw new NotImplementedException();
    }

    public IEnumerable<Vehiculo> ReadFromFile(string path) {
        throw new NotImplementedException();
    }
}