using Ficha.Collections.Lista;

namespace Ficha.Repository.Revista;
using Ficha.Models;


public class RevistaRepository : IRevistasRepository
{
    private static int _idCounter;
    private readonly ILista<Revistas> _listado = new Lista<Revistas>();

    public int TotalDvd => _listado.Contar();

    private static int GetNextId() {
        return _idCounter++;
    }
    
    
    public ILista<Revistas> GetAll() {
        return _listado;
    }

    public Revistas? GetById(int id) {
        foreach (var revistas in _listado) {
            if (revistas.Id == id) {
                return revistas;
            }
        }
        return null;
    }

    public Revistas? Create(Revistas entity) {
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

    public Revistas? Update(Revistas entity, int id) {
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

    public Revistas? Delete(int id) {
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

    private bool Existe(Revistas revista) {
        foreach (var revistas in _listado) {
            if (revistas.Equals(revista)) {
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