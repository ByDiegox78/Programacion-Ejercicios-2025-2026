using CsvJsonXmlStorae.Config;
using CsvJsonXmlStorae.Exceptions;
using CsvJsonXmlStorae.Models;
using CsvJsonXmlStorae.Repository;
using CsvJsonXmlStorae.Storage;

namespace CsvJsonXmlStorae.Service;

public class CiudadanosSerivice(
    ICiudadanosRepository repository,
    IStorage<Ciudadano> storage
    ) : ICiudadanosService {

    public int TotalPersonas => repository.GetAll().Count();
    
    public IEnumerable<Ciudadano> GetAll() {
        return repository.GetAll();
    }

    public Ciudadano GetById(int id) {
        return repository.GetById(id) ?? throw new CiudadanosExceptions.NoTFound(id.ToString());
    }

    public Ciudadano Save(Ciudadano entity) {
        var nuevo = repository.Create(entity) ?? throw new CiudadanosExceptions.AlreadyExists(entity.Telefono);

        return nuevo;
    }

    public Ciudadano Update(Ciudadano entity, int id) {
        var actualizado = repository.Update(id, entity) ?? throw new CiudadanosExceptions.NoTFound(id.ToString());

        return actualizado;
    }

    public Ciudadano Delete(int id) {
        var eliminado = repository.Delete(id) ?? throw new CiudadanosExceptions.NoTFound(id.ToString());

        return eliminado;
    }

    public int ImportarDatos() {
        try {
            var personas = storage.Cargar(Configuracion.CiudadanosFile).ToList();
            if (!personas.Any()) {
                throw new Exception($"No se encontraron datos para importar en {Configuracion.CiudadanosFile}");
            }

            repository.DeleteAll();
            personas.ForEach(p => Save(p));
            return personas.Count;
        }
        catch (Exception e) {
            throw new CiudadanosExceptions.StorageError(e.Message);
        }
    }
    public int ExportarDatos() {
        try {
            var personas = repository.GetAll().ToList();
            storage.Salvar(personas, Configuracion.CiudadanosFile);
            return personas.Count;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw new CiudadanosExceptions.StorageError(e.Message);
        }
    }
}