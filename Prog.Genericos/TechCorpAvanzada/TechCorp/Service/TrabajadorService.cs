using TechCorp.Models;
using TechCorp.Models.Interface;
using TechCorp.Repository;

namespace TechCorp;

public class TrabajadorService(TrabajadorRepository repository) {
    
    public Trabajador[] GetAllTrabajadores() {
        var trabajador = repository.GetAll();
        return trabajador;
    }
    private Trabajador?[] _array = new Trabajador?[Utils.TamañoMaximo];

    public Trabajador? GetById(int id) => repository.GetById(id) ?? throw new KeyNotFoundException($"No se encontró el trabajador con ID: {id}");

    public Trabajador Save(Trabajador trabajador) {
        return repository.Save(trabajador) ?? throw new ArgumentException(
            $"No se pudo guardar el Trabajador con ID {trabajador.Id}, puede que ya exista");
    }
    
    public Trabajador Delete(int id) {
        var eliminado = repository.Delete(id);
        return eliminado ?? throw new KeyNotFoundException($"Trabajador con ID {id} no encontrado para eliminar.");
    }
    
    public Trabajador Update(Trabajador trabajador) => repository.Delete(trabajador.Id)  ?? throw new KeyNotFoundException($"Trabajador con ID {trabajador.Id} no encontrado para actualización.");
    
    public void EjecutarAccionEspecial(Trabajador t) {

        if (t is ITrazarRuta tr) {
            tr.TrazarRuta(); // Lo ejecutan Repartidor y Senior
        }
        if (t is ILocalizar l) {
            l.Localizar(); // Lo ejecutan Reponedor y Senior
        }
        if (t is IActualizarStock a) {
            a.ActualizarStock(); // Lo ejecutan Reponedor y Senior
        }
        if(t is IConfirmarEntrega ce) {
            ce.ConfirmarEntrega(); // Lo ejecutan Repartidor y Senior
        }
    
        Console.WriteLine("-----------------------------------");
    }

    public Senior? BucarMayorSenior(Trabajador t) {
        if (IsEmptyStorage()) {
            throw new ArgumentException("No hay trabajadores en la empresa");
        }
        var trabajador = repository.GetAll();
        Senior? seniorMayor = null;
        for (int i = 0; i < trabajador.GetLength(0); i++) {
            if (trabajador[i] is Senior s) {
                if (seniorMayor == null || s.AñosDeServicio > seniorMayor.AñosDeServicio) {
                    seniorMayor = s;
                }
            }
        }
        return seniorMayor;
    }
    public Senior? BucarMayorSeniorAs() {
        Senior? elMasVeterano = null;
        for (int i = 0; i < _array.Length; i++) {
            Senior? seniorActual = _array[i] as Senior; //
            if (seniorActual != null) {
                if (elMasVeterano == null || seniorActual.AñosDeServicio > elMasVeterano.AñosDeServicio) {
                    // Actualizamos el máximo encontrado
                    elMasVeterano = seniorActual;
                }
            }
        }
        return elMasVeterano;
    }
    public Reponedor? BuscarPorSectorAs(char letra) {
        for (int i = 0; i < _array.Length; i++) {

            Reponedor? r = _array[i] as Reponedor;
            if (r != null) {
                if (r.Sector == letra) {
                    return r;
                }
            }
        }
        throw new KeyNotFoundException($"No hay reponedores en el sector {letra}.");
    }
    public Reponedor? BuscarPorSector(char letra) {
        if (!IsEmptyStorage()) {
            throw new ArgumentException("No hay trabajadore en el array");
        }
        for (int i = 0; i < _array.GetLength(0); i++) {
            if (_array[i] is Reponedor r) {
                if (r.Sector == letra) {
                    return r;
                }
            }
            
        }
        throw new ArgumentException("No hemos encontrado ningun reponedor ")
    }
    
    
    
    private bool IsEmptyStorage() {
        bool isEmpty = false;
        for (int i = 0; i < _array.GetLength(0); i++) {
            for (int j = 0; j < _array.GetLength(1); j++) {
                if (_array[i] is {} paquete) {
                    isEmpty = true;
                }
            } 
        }
        return isEmpty;
    }
}

