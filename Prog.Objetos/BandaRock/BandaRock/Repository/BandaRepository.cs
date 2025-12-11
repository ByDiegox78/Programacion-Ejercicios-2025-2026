using BandaRock.Config;
using BandaRock.Factory;
using BandaRock.Models;
using Serilog;

namespace BandaRock.Repository;

public class BandaRepository {
    private static BandaRepository? _instance;

    private static int _idCounter;
    private readonly ILogger _log = Log.ForContext<BandaRepository>();
    private Musico?[] _lista = new Musico?[Configuracion.TamanoInicial];

    private BandaRepository() {
        _log.Debug("Creando instancia unica del repositorio");
        InitBanda();
    }
    public int TotalMusicos { get; private set; }
    public static BandaRepository GetInstance() {
        return _instance ??= new BandaRepository();
    }

    private void InitBanda() {
        _log.Debug("Inicializando Repositorio");
        var bandaPrueba = BandaFactory.DemoDataMejorado();
        foreach (var banda in bandaPrueba) {
            Save(banda);
        }
    }
    
    private static int GetNextId() {
        return ++_idCounter;
    }
    public Musico[] GetAll() {
        _log.Information("Obteniendo todos los Musicos. Total: {Total}", TotalMusicos);
        return ObtenerVectorCompacto();
    }
    
    public Musico? GetById(int id) {
        _log.Information("Buscando Musico por ID: {Id}", id);
        foreach (var musico in _lista)
            if (musico?.Id == id)
                return musico;

        return null;
    }
    public Musico Save(Musico musico) {
        var newId = musico with { Id = GetNextId() };
        _log.Debug("Musico guardado: {musico}",  newId);
        for (var i = 0; i < _lista.GetLength(0); i++) {
            if (_lista[i] != null) continue;
            _lista[i] = newId;
            _log.Debug("Musico guardado en la posicion: {index}",  i);
            break;
        }
        return newId;
    }
    
    
    
    public Musico? Delete(int id) {
        _log.Information("Eliminando Musico con ID: {Id}", id);
        for (var i = 0; i < _lista.Length; i++)
            if (_lista[i]?.Id == id) {
                _lista[i] = null; 
                ObtenerVectorCompacto();
                _log.Information("Musico con ID {Id} eliminado exitosamente.", id);
                return _lista[i];
            }

        _log.Warning("Musico con ID {Id} no encontrado para eliminación.", id);
        return null;
    }
    
    public Musico? Update(Musico musico) {
        _log.Information("Actualizando Musico: {Musico}", musico);
        for (var i = 0; i < _lista.Length; i++) {
            if (_lista[i]?.Id != musico.Id) continue;
            _lista[i] = musico;
            _log.Information("Musico con ID {Id} actualizado exitosamente.", musico.Id);
            return musico;
        }

        return null;
    }

    public Guitarrista[] GetAllGuitarrista() { 
        Musico?[] musicos = GetAll();
        var numGuitarristas = 0;
        for (int i = 0; i < musicos.GetLength(0); i++) {
            if (musicos[i] is not Guitarrista) {
                musicos[i] =  null;
            }
            else {
                ++numGuitarristas;
            }
        }
    }
    private Musico[] ObtenerVectorCompacto() {
        var cantidadMusicos = 0;
        for (int i = 0; i < _lista.GetLength(0); i++) {
            if (_lista[i] != null) {
                cantidadMusicos += 1;
            }
        }
        var newLista = new Musico[cantidadMusicos];
        
        var index = 0;
        foreach (var l in _lista) {
            if (l is { } funko) {
                newLista[index++] = funko;
            }
        }
        return newLista;
    }  
    
    private Musico[] AumentarVector() {
        var nuevoCatalogo = new Musico[_lista.Length + 1];
        var index = 0;
        foreach (var f in _lista) {
            if (f is { } musico)
                nuevoCatalogo[index++] = musico;
        }
        return nuevoCatalogo;
    }
}