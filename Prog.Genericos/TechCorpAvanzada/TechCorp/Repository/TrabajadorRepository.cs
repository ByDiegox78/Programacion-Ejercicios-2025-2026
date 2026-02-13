using System.Xml.Linq;
using TechCorp.Factories;
using TechCorp.Models;

namespace TechCorp.Repository;

public class TrabajadorRepository {
    private static TrabajadorRepository? _instance;

    public static TrabajadorRepository GetInstance() {
        return _instance ??= new TrabajadorRepository();
    }

    private Trabajador?[] _array = new Trabajador?[Utils.TamañoMaximo];

    private static int _idCounter;

    private TrabajadorRepository() {
        InitEquipo();
    }

    private void InitEquipo() {
        var init = TrabajadorFactory.DemoTrabajadores();
        foreach (var t in _array) {
            if (t is not { } trabajadorValido) continue; {
                Save(t);
            }
        }
    }
    private static int GetNextId() {
        return ++_idCounter;
    }
    
    public Trabajador[] GetAll() => ObtenerCatalogoCompacto();

    public Trabajador? GetById(int id) {
        foreach (var t in _array) {
            if (t is null) continue;
            if (t.Id == id) {
                return t;
            }
        }
        return null;
    }
    public Trabajador? Save(Trabajador trabajador) {
        if (Exist(trabajador)) return null;
        var newTrabajador = trabajador with { Id = GetNextId() };
        _array = AñadirUnEspacio();
        for (int i = 0; i < _array.GetLength(0); i++) {
            if (_array[i] == null) {
                _array[i] = newTrabajador;
                break;
            }
        }
        return newTrabajador;
    }

    public void Update(Trabajador trabajador) {
        for (int i = 0; i < _array.GetLength(0); i++) {
            if (_array[i]?.Id != trabajador.Id) continue; 
            _array[i] = trabajador;
            return;
        }
    }
    public Trabajador? Delete(int id) {
        for (int i = 0; i < _array.GetLength(0); i++) {
            if (_array[i]?.Id == id) {
                var trabajador = _array[i];
                _array[i] = null;
                ObtenerCatalogoCompacto();
                return trabajador;
            }
        }
        return null;
    }
// ----------------------------------------------------------------------------
    // Funcionalidad de acordeon
    private Trabajador[] ObtenerCatalogoCompacto() {
        var numEquipo = 0;
        foreach (var f in _array) {
            if (f != null) numEquipo++;
        }
        var catalogoCompacto = new Trabajador[numEquipo];
        var index = 0;
        foreach (var f in _array) {
            if (f is {} funkoValido)
                catalogoCompacto[index++] = funkoValido;
        }
        return catalogoCompacto;
    }
    
    private Trabajador[] AñadirUnEspacio() {
        var nuevoCatalogo = new Trabajador[ObtenerCatalogoCompacto().Length + 1];
        var index = 0;
        foreach (var f in _array) {
            if (f is { } equipoValido)
                nuevoCatalogo[index++] = equipoValido;
        }
        return nuevoCatalogo;
    }

    private bool Exist(Trabajador trabajador) {
        foreach (var t in _array) {
            if (t == null) continue;
            if (t!.Equals(trabajador)) {
                return true;
            }
        }

        return false;
    }
    
}