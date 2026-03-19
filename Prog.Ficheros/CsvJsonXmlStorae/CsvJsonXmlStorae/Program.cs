using static System.Console;
using CsvJsonXmlStorae.Config;
using CsvJsonXmlStorae.Enums;
using CsvJsonXmlStorae.Factories;
using CsvJsonXmlStorae.Models;
using CsvJsonXmlStorae.Repository;
using CsvJsonXmlStorae.Service;
using System.Text;
using System.Text.RegularExpressions;
using CsvJsonXmlStorae.Storage.bin;
Console.OutputEncoding = Encoding.UTF8;

Main();

void Main() {
    ICiudadanosService service = new CiudadanosSerivice(CiudadanosRepository.Instance, new CiudadanoStorageBin());

    CiudadanosFactory.Seed().ToList().ForEach(p => service.Save(p));
    bool salir = false;

    while (!salir) {
        Clear(); // Limpia la consola en cada iteración para que sea más legible
        WriteLine("\n========================================");
        WriteLine("   🏛️ GESTIÓN DE CIUDADANOS 🏛️");
        WriteLine("========================================");
        WriteLine("1. 📋 Listar todos los ciudadanos");
        WriteLine("2. ➕ Añadir nuevo ciudadano");
        WriteLine("3. 🔍 Buscar ciudadano por Id");
        WriteLine("4. 📝 Actualizar ciudadano");
        WriteLine("5. 🗑️ Eliminar ciudadano");
        WriteLine("6. 📊 Mostrar total de ciudadanos");
        WriteLine("7. 📤 Exportar datos a fichero");
        WriteLine("8. 📥 Importar datos desde fichero");
        WriteLine("9. 🌱 Cargar datos de prueba (Seed)");
        WriteLine("0. ❌ Salir");
        WriteLine("========================================");

        int opcion = LeerEnteroConRango("\n👉 Seleccione una opción: ", 0, 9);

       // Clear(); // Limpiamos antes de mostrar el resultado de la acción

        switch (opcion) {
            case 1:
                ListarCiudadanos(service);
                break;
            case 2:
                AnadirNuevoCiudadano(service);
                break;
            case 3:
                BuscarCiudadanoPorId(service);
                break;
            case 4:
                ActualizarCiudadano(service);
                break;
            case 5:
                EliminarCiudadano(service);
                break;
            case 6:
                MostrarTotalCiudadanos(service);
                break;
            case 7:
                ExportarDatos(service);
                break;
            case 8:
                ImportarDatos(service);
                break;
            case 0:
                salir = true;
                WriteLine("👋 ¡Hasta la próxima!");
                break;
        }

        if (!salir) {
            WriteLine("\nPress any key to continue...");
            ReadKey(true);
        }
    }
}
void ListarCiudadanos(ICiudadanosService service) {
    WriteLine("\n📋 --- LISTA DE CIUDADANOS ---");
    
    try {
        var lista = service.GetAll().ToList();

        if (lista.Any()) {
            foreach (var l in lista) {
                WriteLine(
                    $"🆔 Id: {l.Id} | 👤 Nombre: {l.Nombre} {l.Apellido} | 💍 Estado: {l.EstadoCivil} | 🎂 Edad: {l.Edad}");
            }
        }
        else {
            WriteLine("⚠️ No hay ciudadanos registrados actualmente.");
        }
    }
    catch (Exception ex) {
        WriteLine($"☠️ ERROR AL OBTENER LA LISTA: {ex.Message}");
    }
}

void ExportarDatos(ICiudadanosService service) {
    WriteLine("\n📤 --- EXPORTAR DATOS A FICHERO ---");
    try {
        var exportados = service.ExportarDatos();
        WriteLine($"✅ Exportados {exportados} registros.");
    }
    catch (Exception ex) {
        WriteLine($"☠️ ERROR AL EXPORTAR: {ex.Message}");
    }
}

void ImportarDatos(ICiudadanosService service) {
    WriteLine("\n📥 --- IMPORTAR DATOS DESDE FICHERO ---");
    if (PedirConfirmacion(
            $"Desea importar los datos desde el fichero: {Configuracion.CiudadanosFile}\nEsta acción puede sobrescribir datos existentes. ¿Desea continuar?"))
        try {
            var importados = service.ImportarDatos();
            WriteLine($"✅ Importados {importados} registros.");
        }
        catch (Exception ex) {
            WriteLine($"☠️ ERROR AL IMPORTAR: {ex.Message}");
        }
}
bool PedirConfirmacion(string mensaje) {
        Write($"\n⚠️  {mensaje} (S para confirmar): ");
        var res = char.ToUpper(ReadKey(false).KeyChar) == 'S';
        WriteLine();
        return res;
}
void AnadirNuevoCiudadano(ICiudadanosService service) {
    WriteLine("\n➕ --- ALTA DE NUEVO CIUDADANO ---");

    var id = LeerEntero("🆔 Id: ");
    var nombre = LeerCadenaValidada("👤 Nombre: ", @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{2,30}$", "Mínimo 2 caracteres.");
    var apellido = LeerCadenaValidada("👤 Apellidos: ", @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{2,50}$", "Mínimo 2 caracteres.");
    var edad = LeerEntero("🎂 Edad: ");
    var email = LeerCadenaValidada("📧 Email: ", @"^[^@\s]+@[^@\s]+\.[^@\s]+$", "Formato de email inválido.");
    var telefono = LeerEntero("📱 Teléfono: ");
    var direccion = LeerCadena("🏠 Dirección: ");
    var ciudad = LeerCadena("🌍 Ciudad: ");
    var pais = LeerCadena("🌎 País: ");
    var codigoPostal = LeerEntero("📮 Código Postal: ");
    var profesion = LeerCadena("💼 Profesión: ");
    var empresa = LeerCadena("🏢 Empresa: ");
    var salario = LeerEntero("💰 Salario: ");
    var fechaNacimiento = LeerFecha("🎂 Fecha nacimiento (yyyy-MM-dd): ");
    var genero = LeerGenero();
    var estadoCivil = LeerEstadoCivil();
    var numHijos = LeerEntero("👶 Número de hijos: ");
    var fechaRegistro = DateTime.Now;
    var activo = LeerBooleano("✅ ¿Activo? (s/n): ");

    var temp = new Ciudadano(
        id, nombre, apellido, edad, email, telefono,
        direccion, ciudad, pais, codigoPostal,
        profesion, empresa, salario, fechaNacimiento,
        genero, estadoCivil, numHijos, fechaRegistro, activo
    );

    WriteLine("\n👀 REVISE LOS DATOS:");
    ImprimirFichaCiudadano(temp);

    if (!PedirConfirmacion("¿Confirmar alta?")) return;
    try {
        var creado = service.Save(temp);
        WriteLine("✅ Guardado con éxito.");
        ImprimirFichaCiudadano(creado);
    }
    catch (Exception ex) {
        WriteLine($"☠️ ERROR: {ex.Message}");
    }
}
void MostrarTotalCiudadanos(ICiudadanosService service)
{
    WriteLine("\n📊 --- TOTAL DE CIUDADANOS ---");
    WriteLine($"Hay un total de {service.TotalPersonas} ciudadanos registrados en el sistema.");
}

void BuscarCiudadanoPorId(ICiudadanosService service)
{
    WriteLine("\n🔍 --- BUSCAR CIUDADANO ---");
    var id = LeerEntero("🆔 Introduzca el Id del ciudadano a buscar: ");
    
    try
    {
        var ciudadano = service.GetById(id);
        WriteLine("\n✅ Ciudadano encontrado:");
        ImprimirFichaCiudadano(ciudadano);
    }
    catch (Exception ex)
    {
        WriteLine($"☠️ ERROR: {ex.Message}");
    }
}

void ActualizarCiudadano(ICiudadanosService service)
{
    WriteLine("\n📝 --- ACTUALIZAR CIUDADANO ---");
    var id = LeerEntero("🆔 Introduzca el Id del ciudadano a actualizar: ");
    
    try {
        var existente = service.GetById(id);
        WriteLine("\nDatos actuales del ciudadano:");
        ImprimirFichaCiudadano(existente);

        if (!PedirConfirmacion("¿Desea modificar este registro?")) return;

        WriteLine("\nIntroduzca los nuevos datos (debe rellenar todo de nuevo):");
        
        // Reutilizamos las funciones de validación que ya creaste
        var nombre = LeerCadenaValidada("👤 Nombre: ", @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{2,30}$", "Mínimo 2 caracteres.");
        var apellido = LeerCadenaValidada("👤 Apellidos: ", @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]{2,50}$", "Mínimo 2 caracteres.");
        var edad = LeerEntero("🎂 Edad: ");
        var email = LeerCadenaValidada("📧 Email: ", @"^[^@\s]+@[^@\s]+\.[^@\s]+$", "Formato de email inválido.");
        var telefono = LeerEntero("📱 Teléfono: ");
        var direccion = LeerCadena("🏠 Dirección: ");
        var ciudad = LeerCadena("🌍 Ciudad: ");
        var pais = LeerCadena("🌎 País: ");
        var codigoPostal = LeerEntero("📮 Código Postal: ");
        var profesion = LeerCadena("💼 Profesión: ");
        var empresa = LeerCadena("🏢 Empresa: ");
        var salario = LeerEntero("💰 Salario: ");
        var fechaNacimiento = LeerFecha("🎂 Fecha nacimiento (yyyy-MM-dd): ");
        var genero = LeerGenero();
        var estadoCivil = LeerEstadoCivil();
        var numHijos = LeerEntero("👶 Número de hijos: ");
        var activo = LeerBooleano("✅ ¿Activo? (s/n): ");

        var ciudadanoActualizado = new Ciudadano(
            id, nombre, apellido, edad, email, telefono,
            direccion, ciudad, pais, codigoPostal,
            profesion, empresa, salario, fechaNacimiento,
            genero, estadoCivil, numHijos, existente.FechaRegistro, activo 
        );

        var resultado = service.Update(ciudadanoActualizado, id);
        WriteLine("\n✅ Ciudadano actualizado con éxito. Nuevos datos:");
        ImprimirFichaCiudadano(resultado);
    }
    catch (Exception ex)
    {
         WriteLine($"☠️ ERROR: {ex.Message}");
    }
}

void EliminarCiudadano(ICiudadanosService service)
{
    WriteLine("\n🗑️ --- ELIMINAR CIUDADANO ---");
    var id = LeerEntero("🆔 Introduzca el Id del ciudadano a eliminar: ");
    
    try
    {
        var ciudadano = service.GetById(id);
        ImprimirFichaCiudadano(ciudadano);
        
        if (PedirConfirmacion("⚠️ ¿Está completamente seguro de que desea eliminar este ciudadano? Esta acción es irreversible."))
        {
            service.Delete(id);
            WriteLine("✅ Ciudadano eliminado correctamente.");
        }
        else
        {
            WriteLine("❌ Operación de eliminación cancelada.");
        }
    }
    catch (Exception ex)
    {
        WriteLine($"☠️ ERROR: {ex.Message}");
    }
}
void ImprimirFichaCiudadano(Ciudadano c)
{
    WriteLine($"""
               📋 --- FICHA CIUDADANO ---
               🆔 Id: {c.Id}
               👤 Nombre: {c.Nombre} {c.Apellido}
               🎂 Edad: {c.Edad}
               📧 Email: {c.Email}
               📱 Teléfono: {c.Telefono}
               🏠 Dirección: {c.Direccion}
               🌍 Ciudad: {c.Ciudad}
               🌎 País: {c.Pais}
               📮 Código Postal: {c.CodigoPostal}
               💼 Profesión: {c.Profesion}
               🏢 Empresa: {c.Empresa}
               💰 Salario: {c.Salario}
               🎂 Nacimiento: {c.FechaNacimiento}
               👶 Hijos: {c.NumHijos}
               📅 Registro: {c.FechaRegistro}
               ✅ Activo: {c.Activo}
               """);
}
string LeerCadena(string mensaje) {
    string? valor;
    do {
        Write(mensaje);
        valor = ReadLine();
    } while (string.IsNullOrWhiteSpace(valor));

    return valor.Trim();
}
string LeerCadenaValidada(string mensaje, string patron, string error) {
    var regex = new Regex(patron);
    string? valor;

    do {
        Write(mensaje);
        valor = ReadLine();

        if (valor != null && regex.IsMatch(valor))
            return valor.Trim();

        WriteLine($"❌ {error}");
    }
    while (true);
}

int LeerEntero(string mensaje) {
    int valor;
    do {
        Write(mensaje);
    } while (!int.TryParse(ReadLine(), out valor));

    return valor;
}
DateTime LeerFecha(string mensaje) {
    do {
        Write(mensaje);

        if (DateTime.TryParse(ReadLine(), out var fecha))
            return fecha;

        WriteLine("❌ Fecha inválida.");
    }
    while (true);
}
int LeerEnteroConRango(string mensaje, int min, int max) {
    int valor;
    do {
        Write(mensaje);

        if (int.TryParse(ReadLine(), out valor) && valor >= min && valor <= max)
            return valor;

        WriteLine($"❌ Introduce un número entre {min} y {max}");
    }
    while (true);
}
Genero LeerGenero() {
    WriteLine("Seleccione género:1. Masculino2. Femenino3. Otro ");

    int op = LeerEnteroConRango("Opción: ", 1, 3);
    return op switch {
        1 => Genero.Masculino,
        2 => Genero.Femenino,
        _ => Genero.Masculino
    };
}
Estado LeerEstadoCivil() {
    WriteLine("""
              Estado civil:
              1. Soltero
              2. Casado
              3. Divorciado
              4. Viudo
              """);

    int op = LeerEnteroConRango("Opción: ", 1, 4);

    return op switch {
        1 => Estado.Soltero,
        2 => Estado.Casado,
        3 => Estado.Soltera,
        _ => Estado.Soltero
    };
}
bool LeerBooleano(string mensaje) {
    string? valor;
    do {
        Write(mensaje);
        valor = ReadLine()?.ToLower();

        switch (valor) {
            case "s":
            case "si":
                return true;
            case "n":
            case "no":
                return false;
            default:
                WriteLine("❌ Introduce s/n");
                break;
        }
    }
    while (true);
}