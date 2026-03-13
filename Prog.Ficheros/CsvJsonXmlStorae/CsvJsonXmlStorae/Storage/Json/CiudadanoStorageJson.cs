using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using CsvJsonXmlStorae.Config;
using CsvJsonXmlStorae.Dto;
using CsvJsonXmlStorae.Mapper;
using CsvJsonXmlStorae.Models;

namespace CsvJsonXmlStorae.Storage.Json;
/// <summary>
/// Almacenamiento de aldeanos Json
/// </summary>
public class CiudadanoStorageJson : ICiudadanoJson {

    private readonly JsonSerializerOptions _options = new() {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Convierte las propiedades a camelCase en el JSON
        DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter() },
        Encoder = JavaScriptEncoder
            .UnsafeRelaxedJsonEscaping
    };

    public CiudadanoStorageJson() {
        InitStorage();
    }
    
    public void Salvar(IEnumerable<Ciudadano> items, string path) {
        try {
            var json = JsonSerializer.Serialize((items.Select(p => p.ToDto()).ToList()), _options);
            File.WriteAllText(path, json, Encoding.UTF8);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<Ciudadano> Cargar(string path) {
        if (!Path.Exists(path)) {
            throw new FileNotFoundException($"El archivo '{path}' no existe.");
        }

        try {
            var json = File.ReadAllText(path, Encoding.UTF8);
            var dtos = JsonSerializer.Deserialize<List<CiudadanoDto>>(json, _options);
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