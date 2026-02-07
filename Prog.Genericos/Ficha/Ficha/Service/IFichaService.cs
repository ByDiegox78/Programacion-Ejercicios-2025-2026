using Ficha.Collections.Lista;

namespace Ficha.Service;
using Ficha.Models;

public interface IFichaService {
    int TotalDvd { get; }
    int TotalLibros { get; }
    int TotalRevistas { get; }
    

    ILista<Dvd> GetAllDvd();
    
    ILista<Libro> GetAllLibro();
    
    ILista<Revista> GetAllRevista();
    
    Dvd GetByIdDvd(int id);
    
    Libro GetByIdLibro(int id);
    Revista GetByIdRevista(int id);

    Libro GetLibroByAutor(string autor);
    Dvd SaveDvd(Dvd dvd);
    
    
    Libro SaveLibro(Libro libro);
    Revista SaveRevista(Revista revista);

    Dvd UpdateDvd(Dvd dvd);
    Libro UpdateLibro(Libro libro);
    Revista UpdateRevista(Revista revista);

    Dvd DeleteDvd(int id);
    Libro DeleteLibro(int id);
    Revista DeleteRevista(int id);
    

}