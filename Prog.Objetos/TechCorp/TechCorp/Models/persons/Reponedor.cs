using TechCorp.Models.Interface;

namespace TechCorp.Models;

public class Reponedor : Trabajador, ILocalizar, IActualizarStock {
    public char Sector { get; init; }

    public void Localizar() {
        throw new NotImplementedException();
    }

    public void ActualizarStock() {
        throw new NotImplementedException();
    }
}