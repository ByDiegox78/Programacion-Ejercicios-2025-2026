namespace BandaRock.Models;

public sealed record Cantante : Musico, ICantar, ITocarGuitarra {
    public override void Ensayar() {
        Console.WriteLine("Enseyar...");
    }

    public override void Afinar() {
        Console.WriteLine("Afinar la voz...");
    }

    public void Cantar() {
        Console.WriteLine("Cantar...");
    }

    public void TocarGuitarra() {
        Console.WriteLine("TocarGuitarra...");
    }
}