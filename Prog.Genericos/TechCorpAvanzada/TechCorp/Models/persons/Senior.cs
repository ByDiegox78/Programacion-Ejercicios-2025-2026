using TechCorp.Models.Interface;

namespace TechCorp.Models;

public record Senior : Trabajador, ITrazarRuta, IConfirmarEntrega, ILocalizar, IActualizarStock{
    public int AñosDeServicio { get; init; }
    public void TrazarRuta() {
        Console.WriteLine(" Trazando ruta");
    }

    public void ConfirmarEntrega() {
        Console.WriteLine(" Confirmando entrega");
    }

    public void Localizar() {
        Console.WriteLine(" Localizando entrega");
    }

    public void ActualizarStock() {
        Console.WriteLine("  Actualizando stock");
    }
}