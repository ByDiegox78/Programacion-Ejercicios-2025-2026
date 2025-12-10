using FunkoPop.Enums;
using FunkoPop.Models;
using FunkoPop.Repository;
using FunkoPop.Utils;
using FunkoPop.Validator;
using Serilog;

namespace FunkoPop.Service;

public class FunkoService(FunkoRepository repository, FunkoValidator validator) {
    private readonly ILogger _log = Log.ForContext<FunkoService>();

    public int TotalFunkos = repository.TotalFunkos;
    public Funko[] GetAllFunko(/*TipoOrdenamiento tipoOrdenamiento*/) {
        var funkos = repository.GetAll();
        //ShellShort(funkos, tipoOrdenamiento);
        return funkos;
    }

    public Funko GetByIdFunko(int idFunko) {
        _log.Information("Obteniendo Funko con ID: {Id}", idFunko);
        var funko = repository.GetById(idFunko);
        if (funko == null) {
            _log.Information("Funko con ID {Id} no encontrado.",  idFunko);
            throw new KeyNotFoundException($"Funko con ID {idFunko} no encontrado.");
        }
        return funko;
    }

    public Funko SaveFunko(Funko funko) {
        _log.Information("Guardando nuevo funko: {Funko}", funko);
        var funkoValido = validator.Validators(funko);
        try {
            GetByIdFunko(funkoValido.Id);
            _log.Warning("Ya existe un funko con ID: {FunkoId}", funkoValido.Id);
            throw new InvalidOperationException($"Ya existe un funko con ID: {funkoValido.Id}");
        }
        catch (KeyNotFoundException) {
        }
        return repository.Save(funkoValido);
    }

    public Funko? DeleteFunko(int idFunko) {
        _log.Information("Eliminando Funko con ID: {Id}", idFunko);
        return repository.Delete(idFunko) ??
               throw new KeyNotFoundException($"Funko con ID {idFunko} no existe.");
    }

    public Funko UpdateFunko(Funko funko) {
        _log.Information("Actualziando nuevo funko: {Funko}", funko);
        
        var funkoValido = validator.Validators(funko);
        return repository.Update(funkoValido) ?? throw new KeyNotFoundException(
            $"Funko con id {funkoValido.Id} no encontrado para actualizar");
        
    }
    public void Order(TipoOrdenamiento orden) {
        var funkos = repository.GetAll();
        ShellShort(funkos, orden);
    }
    
    
    
    
    private void ShellShort(Funko[] arr, TipoOrdenamiento tipoOrdenamiento) {
        var n = arr.Length;

        var gap = n / 2;

        while (gap > 0) {
            for (int i = gap; i < n; i++) {
                var temp = arr[i];
                var j = i;

                while (j >= gap && OrdenarPor(arr[j - gap], temp, tipoOrdenamiento)) {
                    arr[j] = arr[j - gap];
                    j = j - gap;
                }
                arr[j] = temp;
            }
            gap = gap / 2;
        }
        Utilities.ImprimirListado(arr);
    }
    private static bool OrdenarPor(Funko a, Funko b, TipoOrdenamiento orden) {
        return orden switch {
            TipoOrdenamiento.PrecioDesc => a.Precio < b.Precio,
            TipoOrdenamiento.PrecioAsc => a.Precio > b.Precio,
            TipoOrdenamiento.NombreAsc => string.Compare(a.Nombre, b.Nombre, StringComparison.OrdinalIgnoreCase) > 0,
            TipoOrdenamiento.NombreDesc => string.Compare(a.Nombre, b.Nombre, StringComparison.OrdinalIgnoreCase) < 0,
            _ => false
        };
    }
}