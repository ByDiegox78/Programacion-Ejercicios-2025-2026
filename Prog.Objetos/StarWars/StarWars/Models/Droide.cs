namespace StarWars.Models;

public class Droide(int maxEnergy, Droide.TipoDroide type, int defense, int shield, int velocity) {
   
    public enum TipoDroide {
        Sw348,
        Sw447,
        Sw421,
    }

    public TipoDroide Tipo { get; init; } = type;
    public int EnergiaMaxima { get; set; } = maxEnergy;
    public int Velocidad {get; init;} = velocity;
    public int DefensaPersonal {get; init;} = defense;
    public int Escudo { get; init; } = shield;
    public bool IsAlive => EnergiaMaxima > 0;
    
    public bool Move() {
        if (Tipo != TipoDroide.Sw421)
            throw new InvalidOperationException("Este tipo de droide no puede moverse");
        Console.WriteLine($"Este droide se mueve con velocidad {Velocidad}");
        return new Random().Next(1, 101) <= Velocidad;
    }
    public override string ToString() {
        return
            $"Droide [{Tipo}] | " +
            $"Energía: {EnergiaMaxima} | " +
            $"Defensa: {DefensaPersonal} | " +
            $"Escudo: {Escudo} | " +
            $"Velocidad: {Velocidad} | " +
            $"Estado: {(IsAlive ? "VIVO" : "DESTRUIDO")}";
    }

    public void UsarEscudo(int danio) {
        if (Tipo != TipoDroide.Sw447)
            throw new InvalidOperationException("Este droide no puede usar escudo");

        int danioFinal = danio - Escudo;

        if (danioFinal > 0)
            EnergiaMaxima -= danioFinal;
    }

    public void Defenderse(int danio) {
        if (Tipo != TipoDroide.Sw348) {
            throw new InvalidOperationException("Este droide no puede usar escudo");
        }
        EnergiaMaxima -= Math.Min(danio, DefensaPersonal);
    }
    
    


}