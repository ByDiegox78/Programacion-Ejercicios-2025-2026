using FunkoPop.Config;
using FunkoPop.Factory;
using FunkoPop.Models;
using Serilog;

namespace FunkoPop.Repository;

public class FunkoRepository {
    private static FunkoRepository? _instance;

    private static int _idCounter;
    private readonly ILogger _log = Log.ForContext<FunkoRepository>();
    private Funko?[] _lista = new Funko?[Configuracion.TamanoInicial];

    private FunkoRepository() {
        _log.Debug("Creando instancia unica del repositorio");
        InitFunKo();
    }
    public int TotalFunkos { get; private set; }
    public static FunkoRepository GetInstance() {
        return _instance ??= new FunkoRepository();
    }

    private void InitFunKo() {
        _log.Debug("Inicializando Repositorio");
        var funkosPrueba = FunkoFactory.DemoData();
        foreach (var funko in funkosPrueba) {
            Save(funko);
        }
    }
    
    private static int GetNextId() {
        return ++_idCounter;
    }
    public Funko[] GetAll() {
        _log.Information("Obteniendo todos los funkos. Total: {Total}", TotalFunkos);
        return ObtenerVectorCompacto();
    }
    
    public Funko? GetById(int id) {
        _log.Information("Buscando alumno por ID: {Id}", id);
        foreach (var alumno in _lista)
            if (alumno?.Id == id)
                return alumno;

        return null;
    }
    public Funko Save(Funko funko) {
        var newId = funko with { Id = GetNextId() };
        _log.Debug("Funko guardado: {funko}",  newId);
        for (var i = 0; i < _lista.GetLength(0); i++) {
            if (_lista[i] != null) continue;
            _lista[i] = newId;
            _log.Debug("Funko guardado en la posicion: {index}",  i);
            break;
        }
        return newId;
    }
    
    
    
    public Funko? Delete(int id) {
        _log.Information("Eliminando funko con ID: {Id}", id);
        for (var i = 0; i < _lista.Length; i++)
            if (_lista[i]?.Id == id) {
                _lista[i] = null; 
                ObtenerVectorCompacto();
                _log.Information("Alumno con ID {Id} eliminado exitosamente.", id);
                return _lista[i];
            }

        _log.Warning("Alumno con ID {Id} no encontrado para eliminación.", id);
        return null;
    }
    
    public Funko? Update(Funko funko) {
        _log.Information("Actualizando funko: {Funko}", funko);
        for (var i = 0; i < _lista.Length; i++) {
            if (_lista[i]?.Id != funko.Id) continue;
            _lista[i] = funko;
            _log.Information("Funko con ID {Id} actualizado exitosamente.", funko.Id);
            return funko;
        }

        return null;
    }
    

    private Funko[] ObtenerVectorCompacto() {
        var cantidadFunkos = 0;
        for (int i = 0; i < _lista.GetLength(0); i++) {
            if (_lista[i] != null) {
                cantidadFunkos += 1;
            }
        }
        var newLista = new Funko[cantidadFunkos];
        
        var index = 0;
        foreach (var l in _lista) {
            if (l is { } funko) {
                newLista[index++] = funko;
            }
        }
        return newLista;
    }  
    
    private Funko[] AumentarVector() {
        var nuevoCatalogo = new Funko[_lista.Length + 1];
        var index = 0;
        foreach (var f in _lista) {
            if (f is { } funko)
                nuevoCatalogo[index++] = funko;
        }
        return nuevoCatalogo;
    }
}