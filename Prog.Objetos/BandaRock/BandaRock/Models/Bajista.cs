namespace BandaRock.Models;

public sealed record Bajista: Musico {
    public override void Ensayar() {
        Console.WriteLine("Ensayando con el bajo...");
    }

    public override void Afinar() {
        Console.WriteLine("Afinando con el bajo...");    
    }

    public void TocarElBajo() {
        Console.WriteLine("Tocar el bajo...");
    }
}