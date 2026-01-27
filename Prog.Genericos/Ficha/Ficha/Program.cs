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
    
}

void ListarLibro(IFichaService service) {
    Console.WriteLine("\n--- LISTADO DE LIBRO ---");
    if (service.TotalLibros == 0) {
        Console.WriteLine("No hay libros almacecados");
        return;
    }
    var listado = service.GetAllLibro();
    
}
