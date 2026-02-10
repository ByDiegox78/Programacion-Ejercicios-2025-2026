using TechCorp.Models;

namespace TechCorp.Factories;

public static class TrabajadorFactory {
    public static Trabajador[] DemoTrabajadores() {
        return [
            new Repartidor { Nombre = "Lars Ulrich", Barrio = "Retiro" }, //
            new Reponedor { Nombre = "Kirk Hammett", Sector = 'A' },    //
            new Senior { Nombre = "James Hetfield", AñosDeServicio = 16 }, //
            new Repartidor { Nombre = "Robert Trujillo", Barrio = "Usera" }
        ];
    }
}