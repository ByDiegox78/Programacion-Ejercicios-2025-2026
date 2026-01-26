
using Ficha.Collections.Lista;

namespace Ficha.Repository.Dvd;
using Ficha.Models;

public class DvdRepository : IDvdRepository
{
    private static int _idCounter;
    private readonly ILista<Dvd> _listado = new Lista<Dvd>();

    public int TotalDvd => _listado.Contar();

    private static int GetNextId() {
        return _idCounter++;
    }
    
    
    public ILista<Dvd> GetAll() {
        return _listado;
    }

    public Dvd? GetById(int id) {
        foreach (var dvd in _listado) {
            if (dvd.Id == id) {
                return dvd;
            }
        }
        return null;
    }

    public Dvd? Create(Dvd entity) {
        if (Existe(entity)) {
            return null;
        }

        var salvado = entity with {
            Id = GetNextId(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        _listado.AgregarFinal(salvado);
        return salvado;
    }

    public Dvd? Update(Dvd entity, int id) {
        var index = IndexOf(id);
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
        var index = IndexOf(id);
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

    private int IndexOf(int id) {
        for (int i = 0; i < _listado.Contar(); i++) {
            if (_listado.Obtener(i).Id == id) {
                return i;
            }
        }

        return -1;
    }

    
}
