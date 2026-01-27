using Ficha.Collections.Lista;
using Ficha.Enums;
using Ficha.Models;

namespace Ficha.Utils;

public static class Utilities {
    public static void ImrimirMenuPrincipal() {
        Console.WriteLine("\n--- 📚 MENU PRINCIPAL 📚 ---");
        Console.WriteLine($"{(int)OpcionMenu.Dvd}.-  Ver Menu de Dvd.");
        Console.WriteLine($"{(int)OpcionMenu.Libros}.-  Ver Menu de Libros.");
        Console.WriteLine($"{(int)OpcionMenu.Revistas}.-  Ver Menu de Revistas.");
        Console.WriteLine($"{(int)OpcionMenu.Salir}.-  Salir.");
        Console.WriteLine("--------------------------------");
    }
    
    public static void ImprimirMenuDvd()
    {
        Console.WriteLine("\n--- 📀 MENÚ DVD 📀 ---");
        Console.WriteLine($"{(int)OpcionMenuDvd.ListarTodos}.- Listar todos los DVDs");
        Console.WriteLine($"{(int)OpcionMenuDvd.InfoId}.- Ver DVD por ID");
        Console.WriteLine($"{(int)OpcionMenuDvd.Anadir}.- Añadir DVD");
        Console.WriteLine($"{(int)OpcionMenuDvd.Actualizar}.- Actualizar DVD");
        Console.WriteLine($"{(int)OpcionMenuDvd.Eliminar}.- Eliminar DVD");
        Console.WriteLine($"{(int)OpcionMenuDvd.Salir}.- Volver al menú principal");
        Console.WriteLine("-----------------------------");
    }
    
    public static void ImprimirMenuLibro()
    {
        Console.WriteLine("\n--- 📚 MENÚ LIBROS 📚 ---");
        Console.WriteLine($"{(int)OpcionMenuLibro.ListarTodos}.- Listar todos los libros");
        Console.WriteLine($"{(int)OpcionMenuLibro.InfoId}.- Ver libro por ID");
        Console.WriteLine($"{(int)OpcionMenuLibro.Anadir}.- Añadir libro");
        Console.WriteLine($"{(int)OpcionMenuLibro.Actualizar}.- Actualizar libro");
        Console.WriteLine($"{(int)OpcionMenuLibro.Eliminar}.- Eliminar libro");
        Console.WriteLine($"{(int)OpcionMenuLibro.Salir}.- Volver al menú principal");
        Console.WriteLine("-----------------------------");
    }

    public static void ImprimirMenuRevista()
    {
        Console.WriteLine("\n--- 📰 MENÚ REVISTAS 📰 ---");
        Console.WriteLine($"{(int)OpcionMenuRevista.ListarTodos}.- Listar todas las revistas");
        Console.WriteLine($"{(int)OpcionMenuRevista.InfoId}.- Ver revista por ID");
        Console.WriteLine($"{(int)OpcionMenuRevista.Anadir}.- Añadir revista");
        Console.WriteLine($"{(int)OpcionMenuRevista.Actualizar}.- Actualizar revista");
        Console.WriteLine($"{(int)OpcionMenuRevista.Eliminar}.- Eliminar revista");
        Console.WriteLine($"{(int)OpcionMenuRevista.Salir}.- Volver al menú principal");
        Console.WriteLine("-----------------------------");
    }
    
    public static void ImprimirListadoDvd(ILista<Dvd> dvds)
    {
        Console.WriteLine("---------------------------------------------------------------------");
        Console.WriteLine($"{"ID",-4} {"Nombre",-20} {"Director",-20} {"Año",-5} {"Tipo",-10}");
        Console.WriteLine("---------------------------------------------------------------------");

        for (var i = 0; i < dvds.Contar(); i++)
        {
            var dvd = dvds.Obtener(i);
            Console.WriteLine($"{dvd.Id,-4} {dvd.Nombre,-20} {dvd.Director,-20} {dvd.Anio,-5} {dvd.Tipo,-10}");
        }

        Console.WriteLine("---------------------------------------------------------------------");
    }
    public static void ImprimirListadoLibro(ILista<Libro> libros)
    {
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine($"{"ID",-4} {"Nombre",-20} {"Autor",-20} {"Editorial",-15}");
        Console.WriteLine("---------------------------------------------------------------");

        for (var i = 0; i < libros.Contar(); i++)
        {
            var libro = libros.Obtener(i);
            Console.WriteLine($"{libro.Id,-4} {libro.Nombre,-20} {libro.Autor,-20} {libro.Editorial,-15}");
        }

        Console.WriteLine("---------------------------------------------------------------");
    }
    public static void ImprimirListadoRevistas(ILista<Revista> revistas)
    {
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine($"{"ID",-4} {"Nombre",-20} {"Número",-8} {"Año",-5}");
        Console.WriteLine("---------------------------------------------------------------");

        for (var i = 0; i < revistas.Contar(); i++)
        {
            var revista = revistas.Obtener(i);
            Console.WriteLine($"{revista.Id,-4} {revista.Nombre,-20} {revista.NumeroLista,-8} {revista.AnioPublicacion,-5}");
        }

        Console.WriteLine("---------------------------------------------------------------");
    }



    
    


}