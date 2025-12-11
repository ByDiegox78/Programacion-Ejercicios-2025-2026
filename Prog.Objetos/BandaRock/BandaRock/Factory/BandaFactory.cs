using BandaRock.Models;

namespace BandaRock.Factory;

public static class BandaFactory {
    
    public static Musico[] DemoDataMejorado() {
        return new Musico[] {
            new Baterista { Id = 1, Nombre = "b", Tiempo = 12 },
            new Guitarrista { Id = 2, Nombre = "g", Tiempo = 12 },
            new Bajista { Id = 3, Nombre = "ba", Tiempo = 12 },
            new Cantante { Id = 4, Nombre = "c", Tiempo = 12 },
            new Guitarrista { Id = 5, Nombre = "g2", Tiempo = 12 }
        };
    }
}