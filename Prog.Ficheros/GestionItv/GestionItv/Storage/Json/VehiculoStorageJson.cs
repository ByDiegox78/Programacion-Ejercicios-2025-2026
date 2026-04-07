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
        
        WriteIndented = true, //Hace el json mas visible
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Convierte las propiedades a camelCase en el JSON
        DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingNull, // Si un campo es nulo, no lo escribe en el json
        Converters = { new JsonStringEnumConverter() }, //Convierte cualquier enum que encuentre a string
        Encoder = JavaScriptEncoder //Permite caracteres especiales
            .UnsafeRelaxedJsonEscaping
    };
    
    public VehiculoStorageJson() {
        InitStorage();
    }
    
    public void WriteToFile(IEnumerable<Vehiculo> items, string path) {
        try {
            var dto = items.Select(p => p.ToDto()).ToList(); //Convierte cada vehiculo a vehiculoDto
            // Convierte la lista de dto al formato json usando la configuracion que le proporcionamos
            var json = JsonSerializer.Serialize(dto, _options);
            File.WriteAllText(path, json, Encoding.UTF8); //Escribe el json en el archivo
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
            var json = File.ReadAllText(path, Encoding.UTF8); // Carga todo el archivo Json
            var dtos = JsonSerializer.Deserialize<List<VehiculoDto>>(json, _options); // Convierte el json a una List<VehiculoDto>
            return dtos?.Select(dto => dto.ToModel()).ToList() ?? // Convierte la lista de dto a una lista de model
                   throw new InvalidOperationException("No se pudieron deserializar los DTOs."); // Si falla lanza error
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