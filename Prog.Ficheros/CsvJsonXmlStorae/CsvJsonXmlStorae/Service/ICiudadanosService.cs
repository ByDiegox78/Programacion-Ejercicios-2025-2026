using CsvJsonXmlStorae.Models;

namespace CsvJsonXmlStorae.Service;

public interface ICiudadanosService {
    
    int TotalPersonas { get; }
    
    IEnumerable<Ciudadano> GetAll();
    
    Ciudadano GetById(int id);

    Ciudadano Save(Ciudadano entity);

    Ciudadano Update(Ciudadano entity, int id);

    Ciudadano Delete(int id);

    int ImportarDatos();

    int ExportarDatos();
    
    

}