using System;
using FunkoPop.Enums;
using FunkoPop.Models;
using FunkoPop.Repository;
using FunkoPop.Service;
using FunkoPop.Utils;
using FunkoPop.Validator;
using Serilog;

Console.Clear();

Main();

void Main() {
    // Creamos la instancia del repositorio (singleton)
    var repository = FunkoRepository.GetInstance();

    // Creamos el validador (aunque no se use en este ejemplo)
    var validator = new FunkoValidator();

    // Creamos el service
    var service = new FunkoService(repository, validator);

    OpcionMenu menu;
    do {
        Utilities.ImprimirMenu();
        menu = (OpcionMenu)int.Parse(Utilities.ValidarMenu("--- Elije una opcion ---", FunkoValidator.RegexMenu));
        switch (menu) {
            case (int)OpcionMenu.Salir:
                break;
            case OpcionMenu.MostrarTodos:
                MostrarTodos(service);
                break;
            case OpcionMenu.InfoById:
                GetFunkoById(service);
                break;
            case OpcionMenu.Anadir:
                break;
            case OpcionMenu.Actualizar:
                break;
            case OpcionMenu.Eliminar:
                break;
            case OpcionMenu.OrdenarNombreAsc:
                Order(service, TipoOrdenamiento.NombreAsc);
                break;
            case OpcionMenu.OrdenarNombredesc:
                Order(service, TipoOrdenamiento.NombreDesc);
                break;
            case OpcionMenu.OrdenarPrecioAsc:
                Order(service, TipoOrdenamiento.PrecioAsc);
                break;
            case OpcionMenu.OrdenarPrecioDesc:
                Order(service, TipoOrdenamiento.PrecioDesc);
                break;

        }
    }while (menu != OpcionMenu.Salir);
}

void MostrarTodos(FunkoService service) {
    if (service.TotalFunkos != 0) {
        Log.Error("No hay funkos registrados");
        return;
    }
    
    var listado = service.GetAllFunko();
    Utilities.ImprimirListado(listado);
}

void Order(FunkoService service, TipoOrdenamiento orden) {
    service.Order(orden);
}

void GetFunkoById(FunkoService service) {
    var id = Utilities.ValidarId("Introduce el id del Funko", FunkoValidator.RegexId);

    try {
        var funko = service.GetByIdFunko(id);
        Utilities.ImprimirInfoFunko(funko);
    }
    catch (KeyNotFoundException ex) {
        Log.Error(ex, "Error al obtener Funko por ID.");
        Console.WriteLine($" ERROR: {ex.Message}");
    }
}

void AñadirFunko(FunkoService service) {
    
}


