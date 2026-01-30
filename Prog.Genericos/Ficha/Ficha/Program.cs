using System.Text.RegularExpressions;
using Ficha.Models;
using Ficha.Repository.Dvd;
using Ficha.Repository.Libro;
using Ficha.Repository.Revista;
using Ficha.Service;
using Ficha.Utils;
using Ficha.Validator.Dvd;
using Ficha.Validator.LibroValidate;
using Ficha.Validator.RevistasValidator;

Main();

void Main() {

    IFichaService service = new FichaService(
        new DvdRepository(),     
        new LibroRepository(),     
        new RevistaRepository(),    
        new RevistaValidator(),    
        new DvdValidator(),        
        new LibroValidate()         
    );
    
    
    
    
    
    
}

void ListarDvd(IFichaService service) {
    Console.WriteLine("\n--- LISTADO DE DVD ---");
    if (service.TotalDvd == 0) {
        Console.WriteLine("No hay Dvd almacecados");
        return;
    }
    var listado = service.GetAllDvd();
    Utilities.ImprimirListadoDvd(listado);
    
}

void ListarRevista(IFichaService service) {
    Console.WriteLine("\n--- LISTADO DE REVISTAS ---");
    if (service.TotalRevistas == 0) {
        Console.WriteLine("No hay revistas almacecados");
        return;
    }
    var listado = service.GetAllRevista();
    Utilities.ImprimirListadoRevistas(listado);
    
}

void ListarLibro(IFichaService service) {
    Console.WriteLine("\n--- LISTADO DE LIBRO ---");
    if (service.TotalLibros == 0) {
        Console.WriteLine("No hay libros almacecados");
        return;
    }
    var listado = service.GetAllLibro();
    Utilities.ImprimirListadoLibro(listado);
}

void MostrarPorIdDvd(IFichaService service) {
    const string regexId = @"^\d+$";

    Console.WriteLine("--- INFORMACIÓN DE DVD POR ID ---");
    var idS = LeerCadenaValidada("Dime el id del dvd que quiere buscar: ", regexId, "Formato de id erroneo");

    var id = int.Parse(idS);

    try {
        var dvd = service.GetByIdDvd(id);
        Utilities.ImprimirInfoDvd(dvd);
    }
    catch (KeyNotFoundException e) {
        Console.WriteLine($"❌ ERROR: {e.Message}");
        throw;
    }
}

void MostrarPorIdLibro(IFichaService service) {
    const string regexId = @"^\d+$";

    Console.WriteLine("--- INFORMACIÓN DE LIBRO POR ID ---");
    var idS = LeerCadenaValidada("Dime el id del libro que quiere buscar: ", regexId, "Formato de id erroneo");

    var id = int.Parse(idS);

    try {
        var libro = service.GetByIdLibro(id);
        Utilities.ImprimirInfoLibro(libro);
    }
    catch (KeyNotFoundException e) {
        Console.WriteLine($"❌ ERROR: {e.Message}");
        throw;
    }
}

void MostrarPorIdLibro(IFichaService service) {
    const string regexId = @"^\d+$";

    Console.WriteLine("--- INFORMACIÓN DE REVISTA POR ID ---");
    var idS = LeerCadenaValidada("Dime el id del revista que quiere buscar: ", regexId, "Formato de id erroneo");

    var id = int.Parse(idS);

    try {
        var revista = service.GetByIdRevista(id);
        Utilities.ImprimirInfoRevista(revista);
    }
    catch (KeyNotFoundException e) {
        Console.WriteLine($"❌ ERROR: {e.Message}");
        throw;
    }
}
/*
void AñadirNuevoDvd(IFichaService service) {
    Console.WriteLine("\n--- AÑADIR NUEVO DVD ---");
    Console.WriteLine("Introduzca los datos del nuevo DVD:");

    var nombre = LeerCadenaValidada(
        "Introduce el nombre del dvd", DvdValidator.NombreRegexValidate, "Cadena invalida");
    var director = LeerCadenaValidada("Introduce el nombre del director", DvdValidator.DirectorRegexValidate,
        "Cadena invalida");
    var anio = LeerAnioValidado("Introduce un año valido", DvdValidator.MinAnio, DvdValidator.MaxAnio);
    
    
}*/

void AñadirNuevoLibro(IFichaService service) {
    Console.WriteLine("\n--- AÑADIR NUEVO LIBRO ---");
    Console.WriteLine("Introduzca los datos del nuevo DVD:");
    
    var nombre = LeerCadenaValidada(
        "Introduce el nombre del libro", LibroValidate.AutorEditorialRegexValidate, "Cadena invalida");
    
    var editorial = LeerCadenaValidada(
        "Introduce la editorial del libro", LibroValidate.AutorEditorialRegexValidate, "Cadena invalida");
    
    var autor = LeerCadenaValidada(
        "Introduce el autor del libro", LibroValidate.AutorEditorialRegexValidate, "Cadena invalida");

    var newLibro = new Libro {
        Nombre = nombre,
        Editorial = editorial,
        Autor = autor,
    };
    try {
        service.SaveLibro(newLibro);
        Console.WriteLine($"✅ INFO: Libro (ID: {newLibro.Id}) añadido exitosamente.");
    }
    catch (Exception ex) {
        Console.WriteLine($"❌ ERROR: No se pudo añadir el libro. {ex.Message}");
    }
    
}

void AñadirNuevaRevista(IFichaService service)
{
    Console.WriteLine("\n--- AÑADIR NUEVA REVISTA ---");
    Console.WriteLine("Introduzca los datos de la nueva revista:");

    var nombre = LeerCadenaValidada(
        "Introduce el nombre de la revista: ",
        RevistaValidator.NombreRegexValidate,
        "Cadena inválida");

    var anioPublicacion = LeerAnioValidado(
        "Introduce el año de publicación: ",
        1975,
        2027);

    var numeroLista = LeerAnioValidado(
        "Introduce el número de la revista: ",
        1975,
        2027);

    var newRevista = new Revista {
        Nombre = nombre,
        AnioPublicacion = anioPublicacion,
        NumeroLista = numeroLista
    };

    try {
        service.SaveRevista(newRevista);
        Console.WriteLine($"✅ INFO: Revista (ID: {newRevista.Id}) añadida exitosamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ ERROR: No se pudo añadir la revista. {ex.Message}");
    }
}


// ------------------------------------------------------------------------------------------------------------------------------

string LeerCadenaValidada(string prompt, string regex, string error) {
    string input;
    var valido = false;
    do {
        Console.Write(prompt);
        input = (Console.ReadLine() ?? "").Trim();
        if (ValidarEntrada(regex, input))
            valido = true;
        else
            Console.WriteLine($"❌ ERROR: {error}");
    } while (!valido);

    return input;
}

bool ValidarEntrada(string patron, string input) {
    return Regex.IsMatch(input, patron);
}

int LeerAnioValidado(string prompt, int min, int max) {
    int anio;
    bool valido;
    do
    {
        Console.Write(prompt);
        var input = (Console.ReadLine() ?? "").Trim();
        valido = int.TryParse(input, out anio) && anio >= min && anio <= max;
        if (!valido)
            Console.WriteLine($"❌ ERROR: El año debe estar entre {min} y {max}");
    }
    while (!valido);

    return anio;
}
      

    // Lambdas también funcionan
    EjecutarOperacion(10, 5, (x, y) => x + y);  // 15
    EjecutarOperacion(10, 5, (x, y) => x - y);  // 5

    void EjecutarOperacion(int a, int b, Func<int, int, int> operacion)
    {
        int resultado = operacion(a, b);
        Console.WriteLine($"Resultado: {resultado}");
    }


