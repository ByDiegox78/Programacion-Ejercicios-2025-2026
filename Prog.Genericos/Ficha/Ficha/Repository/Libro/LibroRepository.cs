using Ficha.Collections.Lista;


namespace Ficha.Repository.Libro;
using Ficha.Models;



public class LibroRepository : ILibrosRepository
{
    private static int _idCounter;
    private readonly ILista<Libro> _listado = new Lista<Libro>();

    public int TotalLibro => _listado.Contar();

    private static int GetNextId() {
        return _idCounter++;
    }


    public ILista<Libro> GetAll() {
        return _listado;
    }

    public Libro? GetById(int id) {
        foreach (var libro in _listado) {
            if (libro.Id == id) {
                return libro;
            }
        }

        return null;
    }

    public Libro? Create(Libro entity) {
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

    public Libro? Update(Libro entity, int id) {
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

    public Libro? Delete(int id) {
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

    private bool Existe(Libro libro) {
        foreach (var libro1 in _listado) {
            if (libro1.Equals(libro)) {
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