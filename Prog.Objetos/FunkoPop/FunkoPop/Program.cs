using System;
using System.Text;
using FunkoPop.Config;
using FunkoPop.Enums;
using FunkoPop.Models;
using FunkoPop.Repository;
using FunkoPop.Service;
using FunkoPop.Utils;
using FunkoPop.Validator;
using Serilog;
Console.OutputEncoding = Encoding.UTF8;
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
                AñadirFunko(service);
                break;
            case OpcionMenu.Actualizar:
                ActualizarFunko(service);
                break;
            case OpcionMenu.Eliminar:
                EliminarFunkoById(service);
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
    var nombre = Utilities.ValidarNombre("Introduce el nombre del Funko", FunkoValidator.RegexNombreApellido);
    var tipo = Utilities.ValidarTipo("Untroduce un tipo valido: Anime|Superheroe|Disney");
    var precio = Utilities.PedirPrecio($"Introduce un precio mayor a: {Configuracion.PrecioMinimo}€");
    var funko = new Funko {
        Nombre = nombre,
        Categoria = tipo,
        Precio = precio
    };
    service.SaveFunko(funko);
    Utilities.ImprimirInfoFunko(funko);
}

void ActualizarFunko(FunkoService service) {
    Console.WriteLine("\n--- ACTUALIZAR ALUMNO ---");
    Console.WriteLine("Introduzca el ID del Fumko que desea actualizar:");
    
    var id = Utilities.ValidarId("Introduce el id del Funko", FunkoValidator.RegexId);

    Funko? oldFunko;

    try {
        oldFunko = service.GetByIdFunko(id);
    }
    catch (KeyNotFoundException ex) {
        Log.Error(ex, "Error al actualizar nuevo Funko.");
        Console.WriteLine($"ERROR: No se pudo actualizar el Funko. {ex.Message}");
        return;
    }
    Console.WriteLine("\n--- DATOS ACTUALES (ANTIGUOS) ---");
    Utilities.ImprimirInfoFunko(oldFunko);

    var updatedFunko = Utilities.UpdatedFunkoCreation(oldFunko);
    try {
        service.UpdateFunko(updatedFunko);
        Log.Information($"\n INFO: Datos del alumno con DNI {id} actualizados con éxito.");
    }
    catch (Exception ex) {
        Log.Error(ex, "Error al actualizar datos del Funko.");
        Console.WriteLine($"ERROR: No se pudo actualizar el Funko. {ex.Message}");
    }
    
}
void EliminarFunkoById(FunkoService service) {
    Console.WriteLine("\n--- ELIMINAR FUNKO ---");

    Console.WriteLine("Introduzca el ID del fUNKO a eliminar:");
    var id = Utilities.ValidarId("Introduce el id del Funko", FunkoValidator.RegexId);

    try {
        var funko = service.GetByIdFunko(id);
       
        Console.WriteLine("\n--- ALUMNO A ELIMINAR PERMANENTEMENTE ---");
        Utilities.ImprimirInfoFunko(funko);

        if (Utilities.PedirConfirmacion($"¿Quiere eliminar el Funko con id {id}? (Presiona s)")) {
            service.DeleteFunko(id);
            Console.WriteLine($"INFO: Funko con ID {id} eliminado con éxito.");
        } else {
            // 4. Si cancela, no hacer nada
            Console.WriteLine($"CANCELADO: Eliminación del Funko con ID {id} cancelada.");
        }
    }
    catch (KeyNotFoundException ex) {
        Log.Error(ex, "Error al obtener Funko por ID para eliminación.");
        Console.WriteLine($"ERROR: {ex.Message}");
    }
}



