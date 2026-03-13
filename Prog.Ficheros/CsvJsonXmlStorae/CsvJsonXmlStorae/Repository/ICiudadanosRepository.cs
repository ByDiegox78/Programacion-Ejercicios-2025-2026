using CsvJsonXmlStorae.Models;

namespace CsvJsonXmlStorae.Repository;

public interface ICiudadanosRepository : ICrudRepository<int, Ciudadano> {
    bool DeleteAll();

    Ciudadano? GetByTelefono(int telefono);
}