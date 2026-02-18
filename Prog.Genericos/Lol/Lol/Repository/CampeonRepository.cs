using System.Diagnostics;
using Ficha.Collections.Lista;
using Lol.Models;
using Serilog;

namespace Lol.Repository;

public class CampeonRepository : ICampeonRepository {
    private readonly ILogger _logger = Log.ForContext<CampeonRepository>();
    private readonly ILista<Campeon> _lista = new Lista<Campeon>();
    
    

    
    public ILista<Campeon> GetAll() {
        _logger.Information("Obteniendo todos los campeones");
        return _lista;
    }

    public Campeon? GetById(int id) {
        _logger.Information("Obteniendo campeon por Id");
        return _lista.Find(i => i.Id == id);
    }
    public Campeon? GetByHabilidad(Habilidad habilidad) {
        _logger.Information("Obteniendo campeon por Habilidad");
        return _lista.Find(i => i.HabilidadCampeon.Contains(habilidad));
    }

    public Campeon? Create(Campeon campeon) {
        _logger.Information("Creando nuevo campeon");
        if (ExisteNombre(campeon.Nombre)) return null;
        var nuevo = campeon with {
            Id = Id.NextId(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        return nuevo;
    }

    public Campeon? Update(int id, Campeon entity) {
        _logger.Information("Atualizando nuevo campeon");
        var index = _lista.FindIndex(i => i.Id == id);
        if (index == -1) return null;

        var actual = _lista.Obtener(index);
        var actualizada = entity with {
            Id = id,
            CreatedAt = actual.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        return actualizada;
    }

    public Campeon? Delete(int id) {
        _logger.Information("Deletando nuevo campeon");
        var index = _lista.FindIndex(i => i.Id == id);
        if (index == -1) return null;
        var actual = _lista.Obtener(index);
        _lista.EliminarEn(index);
        return actual with {
            IsDeleted = true,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public bool ExisteNombre(string nombre) {
        return GetByNombre(nombre) != null;
    }

    public Campeon? GetByNombre(string nombre) {
        _logger.Information("Obteniendo campeon por nombre");
        return _lista.Find(i => i.Nombre == nombre);
    }
}