namespace BandaRock.Models;

public sealed record Baterista: Musico {
    public override void Ensayar() {
        Console.WriteLine("Ensaya la baterista...");
    }

    public override void Afinar() {
        Console.WriteLine("Afinando la bateria... ");
    }
    public void Aporrear() {
        Console.WriteLine("Aporrear el bayeria...");
    }
}