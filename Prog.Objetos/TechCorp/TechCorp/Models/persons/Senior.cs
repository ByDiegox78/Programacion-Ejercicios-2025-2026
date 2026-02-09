using TechCorp.Models.Interface;

namespace TechCorp.Models;

public class Senior : Trabajador, ITrazarRuta, IConfirmarEntrega, ILocalizar, IActualizarStock{
    public int AñosDeServicio { get; init; }
    public void TrazarRuta() {
        throw new NotImplementedException();
    }

    public void ConfirmarEntrega() {
        throw new NotImplementedException();
    }

    public void Localizar() {
        throw new NotImplementedException();
    }

    public void ActualizarStock() {
        throw new NotImplementedException();
    }
}