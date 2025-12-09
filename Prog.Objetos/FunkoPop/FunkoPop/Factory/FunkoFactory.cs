using FunkoPop.Models;

namespace FunkoPop.Factory;

public static class FunkoFactory {
    public static Funko[] DemoData() {
        var lista = new Funko[4];

        var f1 = new Funko { Id = 1, Nombre = "Spider-Man No Way Home", Precio = 15.99m, Categoria = Funko.CategoriaFunko.Heroe };

        var f2 = new Funko { Id = 2, Nombre = "Goku Super Saiyan", Precio = 20.99m, Categoria = Funko.CategoriaFunko.Anime };

        var f3 = new Funko { Id = 3, Nombre = "Mickey Mouse Classic", Precio = 12.99m, Categoria = Funko.CategoriaFunko.Disnay };

        var f4 = new Funko { Id = 4, Nombre = "Iron Man Mark 85", Precio = 100.99m, Categoria = Funko.CategoriaFunko.Heroe };
        lista[0] = f1; // ID 1
        lista[1] = f2; // ID 2
        lista[2] = f3;
        lista[3] = f4;
        return lista;
    }
}