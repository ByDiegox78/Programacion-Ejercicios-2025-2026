
using Ficha.Collections.Lista;

namespace Ficha.Repository.Dvd;
using Ficha.Models;

public class DvdRepository : IDvdRepository {
    private static int _idCounter;
    private readonly ILista<Dvd> _listado = new Lista<Dvd>();
    public int TotalDvd => _listado.Contar();

    public Dvd? GetDvdByDirector(string director) {
        var findDirector = _listado.Find(d => d.Director == director);
        return findDirector;
    } 
    public Dvd? GetById(int id) {
        var findDvd = _listado.Find(d => d.Id == id);
        return findDvd;
    }
    
    private static int GetNextId() {
        return _idCounter++;
    }
    public ILista<Dvd> GetAll() {
        return _listado;
    } public Dvd? Create(Dvd entity) {
        if (Existe(entity)) return null;
        var salvado = entity with {
            Id = GetNextId(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _listado.AgregarFinal(salvado);
        return salvado;
    }
    public Dvd? Update(Dvd entity, int id) {
        var index = _listado.FindIndex(d => d.Id == id);
        if (index == -1) {
            return null;
        }
        var updated = entity with {
            Id = id,
            UpdatedAt = DateTime.Now
        };
        _listado.AgregarEn(updated, index);
        return updated;
    }
    public Dvd? Delete(int id) {
        var index = _listado.FindIndex(d => d.Id == id);
        if (index == -1) {
            return null;
        }
        var alumnoDeleted = _listado.Obtener(index) with {
            IsDeleted = true
        };
        _listado.EliminarEn(index);
        return alumnoDeleted;
    }
    private bool Existe(Dvd dvd) {
        foreach (var d in _listado) {
            if (d.Equals(dvd)) {
                return false;
            }
        }
        return true;
    }
}
