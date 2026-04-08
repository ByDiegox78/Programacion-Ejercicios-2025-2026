using GestionItv.Repository.Binary;
using GestionItv.Repository.Common;
using GestionItv.Repository.Json;
using GestionItv.Repository.Memory;

namespace GestionItv.Factory.Repositories;

public static class RepositoryFactory {
    public static IVehiculosRepository GetRepository(RepositoryType type) {
        return type switch {
            RepositoryType.Memory => VehiculosRepositoryMemory.Instance,
            RepositoryType.Binary =>  VehiculoBinSecRepository.Instance,
            RepositoryType.Json => VehiculoJsonRepository.Instance,
            _ => throw new ArgumentException($"Tipo de repositorio desconocido: {type}")
        };
    }
    
    public static IVehiculosRepository GetDefaultRepository(string configType) {
        var type = configType.ToLower() switch {
            "memory" => RepositoryType.Memory,
            "binary" => RepositoryType.Binary,
            "json" => RepositoryType.Json,
            _ => throw new ArgumentException($"Tipo configurado desconocido: {configType}")
        };
        return GetRepository(type);
    }
}