using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CsvJsonXmlStorae.Config;
using CsvJsonXmlStorae.Dto;
using CsvJsonXmlStorae.Mapper;
using CsvJsonXmlStorae.Models;

namespace CsvJsonXmlStorae.Storage.Xml;

public class CiudadanosStorageXml : ICiudadanoStorageXml {
    
    private readonly XmlSerializerNamespaces XmlSerializerNamespaces = new();
    
    private readonly XmlWriterSettings XmlWriterSettings = new() {
        Indent = true, 
        Encoding = Encoding.UTF8 
    };

    public CiudadanosStorageXml() {
        InitStorage();
    }

    public void Salvar(IEnumerable<Ciudadano> items, string path) {
        try
        {
            var dto = items.Select(p => p.ToDto()).ToList();
            var serializer = new XmlSerializer(typeof(List<CiudadanoDto>));
            using var streamWriter = new StreamWriter(path);
            var xmlWriter = XmlWriter.Create(streamWriter, XmlWriterSettings);
            serializer.Serialize(xmlWriter, dto, XmlSerializerNamespaces);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public IEnumerable<Ciudadano> Cargar(string path) {
        if (!Path.Exists(path)) {
            throw new FileNotFoundException($"El archivo '{path}' no existe.");
        }

        try {
            var serializer = new XmlSerializer(typeof(List<CiudadanoDto>));
            using var streamReader = new StreamReader(path);
            var dtos = serializer.Deserialize(streamReader) as List<CiudadanoDto>;
            return dtos?.Select(dto => dto.ToModel()).ToList() ??
                   throw new InvalidOperationException("No se pudieron deserializar los DTOs.");
        }
        catch (Exception e) {
            if (e.InnerException != null) {
                // ESTO ES LO IMPORTANTE. Te dirá si falla por un tipo de dato, un formato, etc.
                Console.WriteLine($"Detalle del error: {e.InnerException.Message}"); 
            }
            throw;
        }
    }
    
    private void InitStorage() {
        if (Directory.Exists(Configuracion.DataFolderXml))
            return;
        Directory.CreateDirectory(Configuracion.DataFolderXml);
    }
}