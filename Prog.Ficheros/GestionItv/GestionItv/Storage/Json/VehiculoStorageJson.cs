using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using GestionItv.Config;
using GestionItv.Dto;
using GestionItv.Mapper;
using GestionItv.Models;
using GestionItv.Storage.Common;

namespace GestionItv.Storage.Json;

public class VehiculoStorageJson : IStorage<Vehiculo> {
    
    private readonly JsonSerializerOptions _options = new() {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Convierte las propiedades a camelCase en el JSON
        DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter() },
        Encoder = JavaScriptEncoder
            .UnsafeRelaxedJsonEscaping
    };
    
    public VehiculoStorageJson() {
        InitStorage();
    }
    
    public void WriteToFile(IEnumerable<Vehiculo> items, string path) {
        try {
            var json = JsonSerializer.Serialize((items.Select(p => p.ToDto()).ToList()), _options);
            File.WriteAllText(path, json, Encoding.UTF8);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<Vehiculo> ReadFromFile(string path) {
        if (!Path.Exists(path)) {
            throw new FileNotFoundException($"El archivo '{path}' no existe.");
        }
        try {
            var json = File.ReadAllText(path, Encoding.UTF8);
            var dtos = JsonSerializer.Deserialize<List<VehiculoDto>>(json, _options);
            return dtos?.Select(dto => dto.ToModel()).ToList() ??
                   throw new InvalidOperationException("No se pudieron deserializar los DTOs.");
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private void InitStorage()
    {
        if (Directory.Exists(Configuracion.DataFolder))
            return;
        Directory.CreateDirectory(Configuracion.DataFolder);
    }
}