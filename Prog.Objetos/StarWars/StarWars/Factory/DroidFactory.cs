using StarWars.Models;

namespace StarWars.Factory;

public static class DroidFactory {
    public static Droide RandDroid() {
        var random = new Random().Next(1, 101);
        return random switch {
            <= 30 => new Droide(50, Droide.TipoDroide.Sw348, new Random().Next(9, 13), 0, 0),
            <= 80 => new Droide(100, Droide.TipoDroide.Sw447, 0, new Random().Next(9, 13), 0),
            _ => new Droide(new Random().Next(100, 151), Droide.TipoDroide.Sw421, 0, 0, new Random().Next(10, 31))
        };
    }
}