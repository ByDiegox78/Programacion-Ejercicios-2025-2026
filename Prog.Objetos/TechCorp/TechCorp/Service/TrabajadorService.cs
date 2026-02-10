using TechCorp.Models;
using TechCorp.Models.Interface;
using TechCorp.Repository;

namespace TechCorp;

public class TrabajadorService(TrabajadorRepository repository) {
    
    public Trabajador[] GetAllTrabajadores() {
        var trabajador = repository.GetAll();
        return trabajador;
    }
    
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
        // 1. Identificación por tipo (Uso de las propiedades de tus Records)
        string encabezado = t switch {
            Repartidor r => $"[REPARTIDOR] {r.Nombre} | Zona: {r.Barrio}",
            Reponedor rep => $"[REPONEDOR] {rep.Nombre} | Sector: {rep.Sector}",
            Senior s => $"[SENIOR] {s.Nombre} | {s.AñosDeServicio} años de experiencia",
            _ => $"[TRABAJADOR] {t.Nombre}"
        };
        Console.WriteLine(encabezado);
        
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
}
