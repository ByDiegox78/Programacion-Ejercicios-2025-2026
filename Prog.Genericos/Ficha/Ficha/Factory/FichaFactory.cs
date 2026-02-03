using Ficha.Collections.Lista;
using Ficha.Models;
using Ficha.Enums;

namespace Ficha.Factories;

public static class FichasFactory {

    public static ILista<Dvd> SeedDvds() {
        var lista = new Lista<Dvd>();

        // Nombres simples para evitar fallos de Regex y facilitar pruebas
        lista.AgregarFinal(new Dvd { 
            Nombre = "Inception", Director = "Nolan", Anio = 2010, Tipo = TipoDvd.Pelicula 
        });
        lista.AgregarFinal(new Dvd { 
            Nombre = "Matrix", Director = "Wachowski", Anio = 1999, Tipo = TipoDvd.Pelicula 
        });
        lista.AgregarFinal(new Dvd { 
            Nombre = "Avatar", Director = "Cameron", Anio = 2009, Tipo = TipoDvd.Pelicula 
        });
        lista.AgregarFinal(new Dvd { 
            Nombre = "Gladiador", Director = "Scott", Anio = 2000, Tipo = TipoDvd.Pelicula 
        });
        lista.AgregarFinal(new Dvd { 
            Nombre = "Titanic", Director = "Cameron", Anio = 1997, Tipo = TipoDvd.Pelicula 
        });

        return lista;
    }

    public static ILista<Libro> SeedLibros() {
        var lista = new Lista<Libro>();

        lista.AgregarFinal(new Libro { 
            Nombre = "Quijote", Autor = "Cervantes", Editorial = "Planeta" 
        });
        lista.AgregarFinal(new Libro { 
            Nombre = "Odisea", Autor = "Homero", Editorial = "Gredos" 
        });
        lista.AgregarFinal(new Libro { 
            Nombre = "Iliada", Autor = "Homero", Editorial = "Gredos" 
        });
        lista.AgregarFinal(new Libro { 
            Nombre = "Hamlet", Autor = "Shakespeare", Editorial = "Austral" 
        });
        lista.AgregarFinal(new Libro { 
            Nombre = "Dracula", Autor = "Stoker", Editorial = "Debolsillo" 
        });

        return lista;
    }

    public static ILista<Revista> SeedRevistas() {
        var lista = new Lista<Revista>();

        lista.AgregarFinal(new Revista { 
            Nombre = "Nature", AnioPublicacion = 2023, NumeroLista = 101 
        });
        lista.AgregarFinal(new Revista { 
            Nombre = "Time", AnioPublicacion = 2022, NumeroLista = 50 
        });
        lista.AgregarFinal(new Revista { 
            Nombre = "Vogue", AnioPublicacion = 2024, NumeroLista = 200 
        });
        lista.AgregarFinal(new Revista { 
            Nombre = "Forbes", AnioPublicacion = 2023, NumeroLista = 305 
        });
        lista.AgregarFinal(new Revista { 
            Nombre = "Wired", AnioPublicacion = 2021, NumeroLista = 15 
        });

        return lista;
    }
}