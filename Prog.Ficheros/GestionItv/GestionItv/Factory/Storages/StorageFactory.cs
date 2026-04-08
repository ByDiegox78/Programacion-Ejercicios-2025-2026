using GestionItv.Models;
using GestionItv.Storage;
using GestionItv.Storage.Binary;
using GestionItv.Storage.Common;
using GestionItv.Storage.Common.Csv;
using GestionItv.Storage.Json;

namespace GestionItv.Factory;

public static class StorageFactory {

    public static IStorage<Vehiculo> GetStorage(StorageType type) {
        return type switch {
            StorageType.Csv => new StorageCsvVehiculo(),
            StorageType.Json => new VehiculoStorageJson(),
            StorageType.Xml => new StorageVhiculoXml(),
            StorageType.Bin => new StorageVehiculoBinary(),
            _ => throw new ArgumentException($"Tipo de almacenamiento desconocido: {type}")
        };
    }
    
    public static IStorage<Vehiculo> GetDefaultStorage(string configType) {
        var type = configType.ToLower() switch {
            "csv" => StorageType.Csv,
            "json" => StorageType.Json,
            "xml" => StorageType.Xml,
            "bin" => StorageType.Bin,
            _ => throw new ArgumentException($"Tipo configurado desconocido: {configType}")
        };
        return GetStorage(type);
    }
}