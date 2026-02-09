using TechCorp.Models;

namespace TechCorp.Factories;

public static class TrabajadorFactory {
    public static Equipo[] CrearEquiposDemo() {
        // Creamos un equipo de ejemplo
        var equipo1 = new Equipo {
            Jefe = new Senior { Nombre = "James Hetfield", AñosDeServicio = 16 },
            ListaDeEquipo = new List<Trabajador> {
                new Repartidor { Nombre = "Lars Ulrich", Barrio = "Retiro" },
                new Reponedor { Nombre = "Kirk Hammett", Sector = 'A' },
                new Repartidor { Nombre = "Robert Trujillo", Barrio = "Usera" }
            }
        };

        // Devolvemos un array de equipos (puedes añadir más si quieres)
        return [ equipo1 ];
    }
}