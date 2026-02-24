
using System.Text.RegularExpressions;
using Vehiculos.Models;
using Vehiculos.Service;
using Vehiculos.Repository;
using Vehiculos.Validator;
using Vehiculos.Cache;
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
        
        // Validación del menú con Regex
        string opcion = LeerCadenaRegex("\nSeleccione una opción: ", "^[0-8]$", "Opción no válida. Debe ser un número del 0 al 8.");
        
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
            }
        }
        catch (Exception ex)
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine($"\n❌ [ERROR]: {ex.Message}");
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

void ListarTodos(IVehiculoService service) {
    WriteLine("--- LISTADO DE TODOS LOS VEHÍCULOS ---");
    var vehiculos = service.GetAll().ToList();
    
    if (!vehiculos.Any()) {
        WriteLine("No hay vehículos registrados.");
        return;
    }
    foreach (var v in vehiculos) ImprimirVehiculo(v);
}

void BuscarPorMatricula(IVehiculoService service) {
    WriteLine("--- BUSCAR POR MATRÍCULA ---");
    string matricula = LeerCadenaRegex("Introduzca la matrícula a buscar (Ej: 1234ABC): ", "^[0-9]{4}[A-Z]{3}$", "Formato inválido. Deben ser 4 números y 3 letras mayúsculas.");
    
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
        WriteLine($"\n✅ Vehículo con matrícula {guardado.Matricula} guardado correctamente.");
        ResetColor();
    }
}

void ActualizarVehiculo(IVehiculoService service)
{
    WriteLine("--- ACTUALIZAR VEHÍCULO ---");
    string matricula = LeerCadenaRegex("Introduzca la matrícula del vehículo a actualizar: ", "^[0-9]{4}[A-Z]{3}$", "Formato inválido.");
    
    var existente = service.GetByMatricula(matricula);
    WriteLine($"Actualizando {existente.GetType().Name} - {existente.Marca} {existente.Modelo}...");
    
    Vehiculo? vehiculoActualizado = CrearVehiculoDesdeConsola(matricula);
    
    if (vehiculoActualizado is Vehiculo vActualizado)
    {
        service.Update(matricula, vActualizado);
        ForegroundColor = ConsoleColor.Green;
        WriteLine($"\n✅ Vehículo actualizado correctamente.");
        ResetColor();
    }
}

void EliminarVehiculo(IVehiculoService service)
{
    WriteLine("--- ELIMINAR VEHÍCULO ---");
    string matricula = LeerCadenaRegex("Introduzca la matrícula del vehículo a eliminar: ", "^[0-9]{4}[A-Z]{3}$", "Formato inválido.");
    
    var eliminado = service.Delete(matricula);
    ForegroundColor = ConsoleColor.Green;
    WriteLine($"\n✅ Vehículo eliminado correctamente.");
    ResetColor();
}

void GenerarInforme(IVehiculoService service)
{
    WriteLine("--- INFORME DE VEHÍCULOS ---");
    try {
        var informe = service.GenerarInformaDeVehiculos();
        WriteLine($"Total Coches: {informe.TotalCoches}");
        WriteLine($"Total Motos: {informe.TotalMotos}");
        WriteLine($"Total Camiones: {informe.TotalCamiones}");
        WriteLine($"Marca más famosa: {informe.MarcaMasFamosa}");
        WriteLine($"Peso medio camiones (>2015): {informe.PesoMaximoCamion2015:F2} kg");
    } catch (Exception ex) {
        WriteLine($"No se pudo generar el informe (probablemente no hay datos suficientes): {ex.Message}");
    }
}

void ListarPorTipo(IVehiculoService service)
{
    WriteLine("--- LISTAR POR TIPO ---");
    WriteLine("1. Coches");
    WriteLine("2. Motos");
    WriteLine("3. Camiones");
    string tipo = LeerCadenaRegex("Seleccione el tipo: ", "^[1-3]$", "Opción no válida.");
    WriteLine();
    
    switch (tipo)
    {
        case "1": service.GetAllCoches().ForEach(ImprimirVehiculo); break;
        case "2": service.GetAllMotos().ForEach(ImprimirVehiculo); break;
        case "3": service.GetAllCamiones().ForEach(ImprimirVehiculo); break;
    }
}

#endregion

#region Métodos Auxiliares de Validación y Creación

Vehiculo? CrearVehiculoDesdeConsola(string? matriculaForzada)
{
    WriteLine("Seleccione el tipo de vehículo:");
    WriteLine("1. Coche");
    WriteLine("2. Moto");
    WriteLine("3. Camión");
    string tipo = LeerCadenaRegex("Opción (1-3): ", "^[1-3]$", "Tipo inválido. Elija 1, 2 o 3.");

    string matricula = string.IsNullOrWhiteSpace(matriculaForzada) 
        ? LeerCadenaRegex("Matrícula (Ej: 1234ABC): ", "^[0-9]{4}[A-Z]{3}$", "Debe tener 4 números seguidos de 3 letras mayúsculas.") 
        : matriculaForzada;

    string marca = LeerCadenaNoVacia("Marca: ", "La marca no puede estar vacía.");
    string modelo = LeerCadenaNoVacia("Modelo: ", "El modelo no puede estar vacío.");
    
    int anio = LeerEnteroRango("Año de Matriculación (2000-2025): ", 2000, 2025, "El año debe estar entre 2000 y 2025.");

    DateTime ahora = DateTime.UtcNow;

    switch (tipo)
    {
        case "1":
            int puertas = LeerEnteroRango("Número de puertas (1-7): ", 1, 7, "El número de puertas debe ser entre 1 y 7.");
            return new Coche(matricula, marca, modelo, anio, puertas, ahora, ahora);
        
        case "2":
            int cilindrada = LeerEnteroRango("Cilindrada (CC): ", 49, 2500, "Cilindrada inválida (49 - 2500).");
            return new Moto(matricula, marca, modelo, anio, cilindrada, ahora, ahora);
        
        case "3":
            double peso = LeerDoublePositivo("Peso Máximo (kg): ", "El peso debe ser un número positivo (ej: 3500,5).");
            return new Camion(matricula, marca, modelo, anio, peso, ahora, ahora);
        
        default:
            return null;
    }
}

// ---- MÉTODOS DE LECTURA ROBUSTA ----

string LeerCadenaRegex(string mensaje, string patron, string mensajeError)
{
    string input;
    var regex = new Regex(patron);
    do
    {
        Write(mensaje);
        input = ReadLine() ?? string.Empty;
        if (!regex.IsMatch(input))
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine($"❌ {mensajeError}");
            ResetColor();
        }
    } while (!regex.IsMatch(input));
    
    return input;
}

string LeerCadenaNoVacia(string mensaje, string mensajeError)
{
    string input;
    do
    {
        Write(mensaje);
        input = ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(input))
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine($"❌ {mensajeError}");
            ResetColor();
        }
    } while (string.IsNullOrWhiteSpace(input));
    
    return input;
}

int LeerEnteroRango(string mensaje, int min, int max, string mensajeError)
{
    int numero;
    bool esValido;
    do
    {
        Write(mensaje);
        esValido = int.TryParse(ReadLine(), out numero) && numero >= min && numero <= max;
        
        if (!esValido)
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine($"❌ {mensajeError}");
            ResetColor();
        }
    } while (!esValido);
    
    return numero;
}

double LeerDoublePositivo(string mensaje, string mensajeError)
{
    double numero;
    bool esValido;
    do
    {
        Write(mensaje);
        esValido = double.TryParse(ReadLine(), out numero) && numero > 0;
        
        if (!esValido)
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine($"❌ {mensajeError}");
            ResetColor();
        }
    } while (!esValido);
    
    return numero;
}

// ------------------------------------

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