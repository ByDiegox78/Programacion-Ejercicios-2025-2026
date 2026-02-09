using System.Xml.Linq;
using TechCorp.Factories;
using TechCorp.Models;

namespace TechCorp.Repository;

public class EquipoRepository {
    private static EquipoRepository? _instance;

    public static EquipoRepository GetInstance() {
        return _instance ??= new EquipoRepository();
    }

    private Equipo?[] _equipos = new Equipo?[Utils.TamañoMaximo];

    private static int _idCounter;

    private EquipoRepository() {
        InitEquipo();
    }

    private void InitEquipo() {
        var init = TrabajadorFactory.CrearEquiposDemo();
        foreach (var e in _equipos) {
            Save(e);
        }
    }
    private static int GetNextId() {
        return ++_idCounter;
    }
    public void Save()
    
}