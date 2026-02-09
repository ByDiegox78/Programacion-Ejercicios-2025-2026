using TechCorp.Models.Interface;

namespace TechCorp.Models;

public class Repartidor: Trabajador, ITrazarRuta, IConfirmarEntrega {
    public string Barrio { get; init; }
    public void TrazarRuta() {
        throw new NotImplementedException();
    }

    public void ConfirmarEntrega() {
        throw new NotImplementedException();
    }
}