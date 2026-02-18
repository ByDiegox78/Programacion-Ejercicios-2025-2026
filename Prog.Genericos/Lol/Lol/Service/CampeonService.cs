using Ficha.Collections.Lista;
using Lol.Cache;
using Lol.Errors;
using Lol.Models;
using Lol.Repository;
using Lol.Validator.Common;
using Serilog;

namespace Lol.Service;

public class CampeonService (
    ICampeonRepository repository,
    IValidador<Campeon> valAsesinos,
    IValidador<Campeon> valMagos,
    IValidador<Campeon> valAdcs,
    IValidador<Campeon> valLuchadores,
    ICached<int, Campeon> cache) : ICampeonService {
    private readonly ILogger _logger = Log.ForContext<CampeonService>();
    

    public int totalCampeones => repository.GetAll().Contar();

    public ILista<Campeon> GetAll() {
        _logger.Information("Obteniendo todos los Campeones.");
        return repository.GetAll();
    }

    public Campeon GetById(int id) {
        _logger.Information("Obteniendo campeon por Id.");
        var cached = cache.Get(id);
        if (cached != null) 
            return cached;
        var campeon = repository.GetById(id) ?? throw new CampeonExceptions.NotFound(id.ToString());
        cache.Add(id, campeon);
        return campeon;
    }

    public Campeon GetByNombre(string nombre) {
        _logger.Information("Obteniendo campeon por nombre.");    
        return repository.GetByNombre(nombre) ?? throw new CampeonExceptions.NotFound($"Campeon {nombre} not found.");
    }

    public Campeon Save(Campeon campeon) {
        _logger.Information("Guardando campeon.");
        
    }

    public Campeon Update(Campeon campeon, int id) {
        throw new NotImplementedException();
    }

    public Campeon Delete(Campeon campeon) {
        throw new NotImplementedException();
    }

    public ILista<Campeon> GetAllOrderBy(TipoOrdenamiento ordenamiento) {
        throw new NotImplementedException();
    }

    public ILista<Asesino> GetAsesinosOrderBy(TipoOrdenamiento ordenamiento) {
        throw new NotImplementedException();
    }

    public ILista<Mago> GetMagosOrderBy(TipoOrdenamiento ordenamiento) {
        throw new NotImplementedException();
    }

    public ILista<Luchador> GetLuchadorsOrderBy(TipoOrdenamiento ordenamiento) {
       
    }

    public ILista<Adc> GetAdcsOrderBy(TipoOrdenamiento ordenamiento) {
        throw new NotImplementedException();
    }

    private ValidarCampeonPorTipo(Campeon campeon) {
        var errores = campeon switch {
            Asesino => valAsesinos.Validar(campeon)
                
        }
        var erroress = campeon switch {
            Asesino => valAsesino.Validar(persona),
            Docente => valDocente.Validar(persona),
            // Caso por defecto (raro que ocurra, pero por seguridad)
            _ => ["Tipo de entidad no soportada para validación."]
        };
    }
}