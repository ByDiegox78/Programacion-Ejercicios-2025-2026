using System.Text;
using System.Xml;
using System.Xml.Serialization;
using GestionItv.Config;
using GestionItv.Dto;
using GestionItv.Mapper;
using GestionItv.Models;

namespace GestionItv.Storage;

public class StorageVhiculoXml : IStorageVehiculoXml {
    private readonly XmlSerializerNamespaces XmlSerializerNamespaces = new();
    
    private readonly XmlWriterSettings XmlWriterSettings = new() {
        Indent = true, 
        Encoding = Encoding.UTF8 
    };
    public StorageVhiculoXml() {
        InitStorage();
    }
    
    public void WriteToFile(IEnumerable<Vehiculo> items, string path) {
        try {
            var dtos = items.Select(p => p.ToDto()).ToList();
            var serializer = new XmlSerializer(typeof(List<VehiculoDto>));
            using var streamWriter = new StreamWriter(path);
            var xmlWriter = XmlWriter.Create(streamWriter, XmlWriterSettings);
            serializer.Serialize(xmlWriter, dtos, XmlSerializerNamespaces);
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