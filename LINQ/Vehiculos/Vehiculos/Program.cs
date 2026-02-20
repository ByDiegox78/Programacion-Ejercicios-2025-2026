using System;
using System.Linq;
using System.Collections.Generic;
using Vehiculos.Models;
using Vehiculos.Service;
using Vehiculos.Repository;
using Vehiculos.Validator;
using Vehiculos.Cache;
using Vehiculos.Errors;
using Vehiculos.Factory; 
using static System.Console;

Main();

void Main() 
{
    IVehiculoService service = InicializarServicio();
    
    // --- CARGA DE DATOS INICIALES (SEEDING) ---
    WriteLine("Cargando datos iniciales del Factory...");
    foreach (var v in VehiculoFactory.DemoData()) 
    {
        try 
        {
            service.Save(v);
        }
        catch (Exception ex) 
        {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine($"⚠️ Vehículo ignorado ({v.Matricula}): {ex.Message}");
            ResetColor();
        }
    }
    WriteLine("Carga completada. Presione una tecla para iniciar...");
    ReadKey();
    // -----------------------------------------

    bool salir = false;

    while (!salir)
    {
        Clear();
        WriteLine("=== GESTIÓN DE VEHÍCULOS ===");
        WriteLine("1. Ver el total de vehículos");
        WriteLine("2. Listar todos los vehículos");
        WriteLine("3. Buscar vehículo por matrícula");
        WriteLine("4. Añadir nuevo vehículo");
        WriteLine("5. Actualizar vehículo");
        WriteLine("6. Eliminar vehículo");
        WriteLine("7. Generar informe de vehículos");
        WriteLine("8. Listar vehículos por tipo");
        WriteLine("0. Salir");
        Write("\nSeleccione una opción: ");
        
        string opcion = ReadLine() ?? string.Empty;
        
        try 
        {
            Clear();
            switch (opcion)
            {
                case "1": VerTotalVehiculos(service); break;
                case "2": ListarTodos(service); break;
                case "3": BuscarPorMatricula(service); break;
                case "4": AnadirVehiculo(service); break;
                case "5": ActualizarVehiculo(service); break;
                case "6": EliminarVehiculo(service); break;
                case "7": GenerarInforme(service); break;
                case "8": ListarPorTipo(service); break;
                case "0": 
                    salir = true; 
                    WriteLine("¡Hasta pronto!");
                    break;
                default: 
                    WriteLine("Opción no válida. Inténtelo de nuevo."); 
                    break;
            }
        }
        catch (Exception ex)
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine($"\n[ERROR]: {ex.Message}");
            ResetColor();
        }

        if (!salir)
        {
            WriteLine("\nPulse cualquier tecla para continuar...");
            ReadKey();
        }
    }
}

#region Métodos del Menú

void VerTotalVehiculos(IVehiculoService service)
{
    WriteLine("--- TOTAL DE VEHÍCULOS ---");
    WriteLine($"Hay un total de {service.TotalVehiculos} vehículos registrados en el sistema.");
}

void ListarTodos(IVehiculoService service)
{
    WriteLine("--- LISTADO DE TODOS LOS VEHÍCULOS ---");
    var vehiculos = service.GetAll().ToList();
    
    if (!vehiculos.Any())
    {
        WriteLine("No hay vehículos registrados.");
        return;
    }

    foreach (var v in vehiculos)
    {
        ImprimirVehiculo(v);
    }
}

void BuscarPorMatricula(IVehiculoService service)
{
    WriteLine("--- BUSCAR POR MATRÍCULA ---");
    Write("Introduzca la matrícula a buscar: ");
    string matricula = ReadLine() ?? string.Empty;
    
    var vehiculo = service.GetByMatricula(matricula);
    WriteLine("\nVehículo encontrado:");
    ImprimirVehiculo(vehiculo);
}

void AnadirVehiculo(IVehiculoService service)
{
    WriteLine("--- AÑADIR NUEVO VEHÍCULO ---");
    Vehiculo? nuevoVehiculo = CrearVehiculoDesdeConsola(null);
    
    if (nuevoVehiculo is Vehiculo vehiculoValido)
    {
        var guardado = service.Save(vehiculoValido);
        ForegroundColor = ConsoleColor.Green;
        WriteLine($"\nVehículo con matrícula {guardado.Matricula} guardado correctamente.");
        ResetColor();
    }
}

void ActualizarVehiculo(IVehiculoService service)
{
    WriteLine("--- ACTUALIZAR VEHÍCULO ---");
    Write("Introduzca la matrícula del vehículo a actualizar: ");
    string matricula = ReadLine() ?? string.Empty;
    
    var existente = service.GetByMatricula(matricula);
    WriteLine($"Actualizando {existente.GetType().Name} - {existente.Marca} {existente.Modelo}...");
    
    Vehiculo? vehiculoActualizado = CrearVehiculoDesdeConsola(matricula);
    
    if (vehiculoActualizado is Vehiculo vActualizado)
    {
        service.Update(matricula, vActualizado);
        ForegroundColor = ConsoleColor.Green;
        WriteLine($"\nVehículo actualizado correctamente.");
        ResetColor();
    }
}

void EliminarVehiculo(IVehiculoService service)
{
    WriteLine("--- ELIMINAR VEHÍCULO ---");
    Write("Introduzca la matrícula del vehículo a eliminar: ");
    string matricula = ReadLine() ?? string.Empty;
    
    var eliminado = service.Delete(matricula);
    ForegroundColor = ConsoleColor.Green;
    WriteLine($"\nVehículo eliminado correctamente.");
    ResetColor();
}

void GenerarInforme(IVehiculoService service)
{
    WriteLine("--- INFORME DE VEHÍCULOS ---");
    var informe = service.GenerarInformaDeVehiculos();
    
    WriteLine($"Total Coches: {informe.TotalCoches}");
    WriteLine($"Total Motos: {informe.TotalMotos}");
    WriteLine($"Total Camiones: {informe.TotalCamiones}");
    WriteLine($"Marca más famosa: {informe.MarcaMasFamosa}");
    WriteLine($"Peso medio camiones (>2015): {informe.PesoMaximoCamion2015:F2} kg");
}

void ListarPorTipo(IVehiculoService service)
{
    WriteLine("--- LISTAR POR TIPO ---");
    WriteLine("1. Coches");
    WriteLine("2. Motos");
    WriteLine("3. Camiones");
    Write("Seleccione el tipo: ");
    
    string tipo = ReadLine() ?? string.Empty;
    WriteLine();
    
    switch (tipo)
    {
        case "1":
            service.GetAllCoches().ForEach(ImprimirVehiculo);
            break;
        case "2":
            service.GetAllMotos().ForEach(ImprimirVehiculo);
            break;
        case "3":
            service.GetAllCamiones().ForEach(ImprimirVehiculo);
            break;
        default:
            WriteLine("Tipo no reconocido.");
            break;
    }
}

#endregion

#region Métodos Auxiliares

Vehiculo? CrearVehiculoDesdeConsola(string? matriculaForzada)
{
    WriteLine("Seleccione el tipo de vehículo:");
    WriteLine("1. Coche");
    WriteLine("2. Moto");
    WriteLine("3. Camión");
    Write("Opción: ");
    string tipo = ReadLine() ?? string.Empty;

    string matricula = string.IsNullOrWhiteSpace(matriculaForzada) 
        ? PedirDato("Matrícula: ") 
        : matriculaForzada;

    string marca = PedirDato("Marca: ");
    string modelo = PedirDato("Modelo: ");
    
    Write("Año de Matriculación: ");
    int.TryParse(ReadLine() ?? "0", out int anio);

    DateTime ahora = DateTime.UtcNow;

    switch (tipo)
    {
        case "1":
            Write("Número de puertas: ");
            int.TryParse(ReadLine() ?? "0", out int puertas);
            return new Coche(matricula, marca, modelo, anio, puertas, ahora, ahora);
        
        case "2":
            Write("Cilindrada (CC): ");
            int.TryParse(ReadLine() ?? "0", out int cilindrada);
            return new Moto(matricula, marca, modelo, anio, cilindrada, ahora, ahora);
        
        case "3":
            Write("Peso Máximo (kg): ");
            double.TryParse(ReadLine() ?? "0", out double peso);
            return new Camion(matricula, marca, modelo, anio, peso, ahora, ahora);
        
        default:
            WriteLine("Tipo no válido. Operación cancelada.");
            return null;
    }
}

string PedirDato(string mensaje)
{
    Write(mensaje);
    return ReadLine() ?? string.Empty;
}

void ImprimirVehiculo(Vehiculo vehiculo)
{
    string detallesExtra = vehiculo switch
    {
        Coche c => $"Puertas: {c.NumPuertas}",
        Moto m => $"Cilindrada: {m.Cilindrada}cc",
        Camion c => $"Peso Máximo: {c.PesoMaximo}kg",
        _ => "Desconocido"
    };

    WriteLine($"[{vehiculo.GetType().Name}] Matrícula: {vehiculo.Matricula} | {vehiculo.Marca} {vehiculo.Modelo} ({vehiculo.AñoMatriculacion}) | {detallesExtra}");
}

IVehiculoService InicializarServicio()
{
    ICached<string, Vehiculo> cache = new CacheLru<string, Vehiculo>(); 
    IValidador<Vehiculo> cocheValidador = new CocheValidator();
    IValidador<Vehiculo> motoValidador = new MotosValidator();
    IValidador<Vehiculo> camionValidador = new CamionValdator();
    IVehiculosRepository repository = new VehiculoRepository();

    return new VehiculoService(
        cache,
        cocheValidador,
        motoValidador,
        camionValidador,
        repository
    );
}

#endregion