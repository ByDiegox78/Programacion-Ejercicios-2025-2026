using Ficha.Collections.Lista;

namespace Ficha.Repository.Revista;
using Ficha.Models;


public class RevistaRepository : IRevistasRepository
{
    private static int _idCounter;
    private readonly ILista<Revista> _listado = new Lista<Revista>();

    public int TotalRevista => _listado.Contar();

    private static int GetNextId() {
        return _idCounter++;
    }
    
    
    public ILista<Revista> GetAll() {
        return _listado;
    }

    public Revista? GetRevistaByNumber(int number) {
        var findNumero = _listado.Find(r => r.NumeroLista == number);
        return findNumero;
    }

    public Revista? GetById(int id) {
        var revistaFind = _listado.Find(r => r.Id == id);
        return revistaFind;
    }

    public Revista? Create(Revista entity) {
        if (_listado.Existe(entity)) return null;
        var salvado = entity with {
            Id = GetNextId(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        
        _listado.AgregarFinal(salvado);
        return salvado;
    }

    public Revista? Update(Revista entity, int id) {
        var index = IndexOf(id);
        if (index == -1) {
            return null;
        }
        var updated = entity with {
            Id = id,
            UpdatedAt = DateTime.Now
        };
        _listado.EliminarEn(index);
        _listado.AgregarEn(updated, index);
        return updated;
    }

    public Revista? Delete(int id) {
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

    private bool Existe(Revista revista) {
        foreach (var revistas in _listado) {
            if (revistas.Equals(revista)) {
                return false;
            }
        }

        return true;
    }

    private int IndexOf(int id) => _listado.FindIndex(d => d.Id == id);
}