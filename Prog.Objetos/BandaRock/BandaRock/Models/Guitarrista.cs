namespace BandaRock.Models;

public sealed record Guitarrista: Musico, ICantar, ITocarGuitarra {
    public override void Ensayar() {
        Console.WriteLine("Ensaya el guitarra...");
    }

    public override void Afinar() {
        Console.WriteLine("Afinar la guitarra...");
    }

    public void Cantar() {
        Console.WriteLine("Cantar con la guitarra...");
    }
    public void TocarGuitarra() {
        Console.WriteLine("TocarGuitarra...");
    }
}