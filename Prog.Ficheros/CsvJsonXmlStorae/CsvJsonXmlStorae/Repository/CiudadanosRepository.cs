using CsvJsonXmlStorae.Models;

namespace CsvJsonXmlStorae.Repository;

public class CiudadanosRepository :ICiudadanosRepository {
    private static Lazy<CiudadanosRepository> Lazy = new(() => new CiudadanosRepository());
    private readonly Dictionary<int, Ciudadano> _ciudadanos = new();
    
    private readonly Dictionary<int, Ciudadano> _ciudadanosDni = new();


    private int _idCounter;
    
    private CiudadanosRepository() { }

    public static CiudadanosRepository Instance => Lazy.Value;
    
    public IEnumerable<Ciudadano> GetAll() {
        return _ciudadanos.Values;
    }

    public Ciudadano? GetById(int id) {
        return _ciudadanos.GetValueOrDefault(id);
    }

    public Ciudadano? GetByTelefono(int telefono) {
        return _ciudadanos.Values.FirstOrDefault(c => c.Telefono == telefono) ?? null;
    }

    public Ciudadano? Create(Ciudadano entity) {
        if (_ciudadanos.ContainsValue(entity)) return null;
        var nueva = entity with {
            Id = ++_idCounter,
            //IsDelete = false
        };
        _ciudadanos.Add(nueva.Id, nueva);
        return nueva;
    }

    public Ciudadano? Update(int id, Ciudadano entity) {
        if (!_ciudadanos.TryGetValue(id, out var actual)) return null;

        var actualizado = actual with {
            Id = id
        };

        _ciudadanos[id] = actualizado;

        return actualizado;
    }

    public Ciudadano? Delete(int id) {
        if (!_ciudadanos.Remove(id, out var deleted)) return null;
        _ciudadanos.Remove(deleted.Id);
        return deleted;
    }

    public bool DeleteAll() {
        _ciudadanos.Clear();
        _idCounter = 0;
        return true;
    }
}