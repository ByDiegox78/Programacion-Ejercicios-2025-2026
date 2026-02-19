using Vehiculos.Models;

namespace Vehiculos.Factory;

public static class VehiculoFactory {
    public static List<Vehiculo> DemoData() {
        var vehiculos = new List<Vehiculo>
        {
            new Coche("1234ABC", "Toyota", "Corolla", 2019, 5, DateTime.UtcNow, DateTime.UtcNow),
            new Coche("5678DEF", "Ford", "Focus", 2018, 5, DateTime.UtcNow, DateTime.UtcNow),
            new Moto("9012GHI", "Honda", "CBR", 2020, 600, DateTime.UtcNow, DateTime.UtcNow),
            new Camion("3456JKL", "Mercedes", "Actros", 2016, 20000, DateTime.UtcNow, DateTime.UtcNow),
            new Coche("7890MNO", "BMW", "Serie 3", 2021, 4, DateTime.UtcNow, DateTime.UtcNow),
            new Moto("1112PQR", "Yamaha", "MT-07", 2022, 700, DateTime.UtcNow, DateTime.UtcNow),
            new Camion("2223STU", "Volvo", "FH", 2017, 25000, DateTime.UtcNow, DateTime.UtcNow),
            new Coche("3334VWX", "Audi", "A4", 2020, 4, DateTime.UtcNow, DateTime.UtcNow),
            new Moto("4445YZA", "Kawasaki", "Ninja", 2019, 400, DateTime.UtcNow, DateTime.UtcNow),
            new Camion("5556BCD", "MAN", "TGX", 2018, 18000, DateTime.UtcNow, DateTime.UtcNow)
        };
        return vehiculos;
    }
}