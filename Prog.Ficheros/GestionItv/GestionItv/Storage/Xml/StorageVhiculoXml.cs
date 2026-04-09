using System.Text;
using System.Xml;
using System.Xml.Serialization;
using GestionItv.Config;
using GestionItv.Dto;
using GestionItv.Mapper;
using GestionItv.Models;
using Serilog;

namespace GestionItv.Storage;

public class StorageVhiculoXml : IStorageVehiculoXml {
    private readonly XmlSerializerNamespaces XmlSerializerNamespaces = new(); //Gestiona el namespace del xml
    
    private readonly XmlWriterSettings XmlWriterSettings = new() { // Configura como se escribe el XML
        Indent = true, // Mete tabulazos
        Encoding = Encoding.UTF8 
    };
    public StorageVhiculoXml() {
        InitStorage();
    }
    private readonly ILogger _logger = Log.ForContext<StorageVhiculoXml>();

    public void WriteToFile(IEnumerable<Vehiculo> items, string path) {
        try {
            _logger.Debug("Guardando los items en el archivo '{path}'", path);
            
            var dtos = items.Select(p => p.ToDto()).ToList(); // Convertir a Dto
            var serializer = new XmlSerializer(typeof(List<VehiculoDto>)); // Convertir la lista a Xml 
            
            using var streamWriter = new StreamWriter(path, false, Encoding.UTF8); // Abrir el archivo para escribir
            var xmlWriter = XmlWriter.Create(streamWriter, XmlWriterSettings);  //Aplica la configuracion para el archivo
            serializer.Serialize(xmlWriter, dtos, XmlSerializerNamespaces); // Convierte la lista en xml y escribe el archivo
        }
        catch (Exception e) {
            _logger.Error(e, "Error al guardar los items en el archivo '{path}'", path);
            throw;
        }
    }

    public IEnumerable<Vehiculo> ReadFromFile(string path) {
        if (!Path.Exists(path)) {
            throw new FileNotFoundException($"El archivo '{path}' no existe.");
        }

        try {
            var serializer = new XmlSerializer(typeof(List<VehiculoDto>));
            using var streamReader = new StreamReader(path);
            var dtos = serializer.Deserialize(streamReader) as List<VehiculoDto>;
            return dtos?.Select(dto => dto.ToModel()).ToList() ??
                   throw new InvalidOperationException("No se pudieron deserializar los DTOs.");
        }
        catch (Exception e) {
            if (e.InnerException != null) {
                Console.WriteLine($"Detalle del error: {e.InnerException.Message}"); 
            }
            throw;
        }
    }
    private static void InitStorage() {
        if (Directory.Exists(Configuracion.DataFolder))
            return;
        Directory.CreateDirectory(Configuracion.DataFolder);
    }
}