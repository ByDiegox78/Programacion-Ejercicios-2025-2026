// See https://aka.ms/new-console-template for more information
using static System.Console;
using CsvJsonXmlStorae.Config;
using CsvJsonXmlStorae.Enums;
using CsvJsonXmlStorae.Factories;
using CsvJsonXmlStorae.Models;
using CsvJsonXmlStorae.Repository;
using CsvJsonXmlStorae.Service;
using CsvJsonXmlStorae.Storage;
using CsvJsonXmlStorae.Storage.Json;
using CsvJsonXmlStorae.Storage.Xml;
using System.Text;
using System.Text.RegularExpressions;
using CsvJsonXmlStorae.Storage.Bin;

Console.OutputEncoding = Encoding.UTF8;

Main();

void Main() {
    ICiudadanosService service = new CiudadanosSerivice(CiudadanosRepository.Instance, new CiudadanoStorageBin());
    CiudadanosFactory.Seed().ToList().ForEach(p => service.Save(p));
    ExportarDatos(service);
    ImportarDatos(service);

    // Luego añadir nuevos
    AnadirNuevoCiudadano(service);

    // Y después exportar
    ExportarDatos(service);

    var lista = service.GetAll().ToList();
    foreach (var l in lista)
    {
        WriteLine($"Id: {l.Id} - Nombre: {l.Nombre} - Estado: {l.EstadoCivil}");
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
void AnadirNuevoCiudadano(ICiudadanosService service)
{
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
    try
    {
        var creado = service.Save(temp);
        WriteLine("✅ Guardado con éxito.");
        ImprimirFichaCiudadano(creado);
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
    DateTime fecha;

    do {
        Write(mensaje);

        if (DateTime.TryParse(ReadLine(), out fecha))
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