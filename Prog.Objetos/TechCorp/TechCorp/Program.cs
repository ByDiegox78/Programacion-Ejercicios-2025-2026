using TechCorp;
using TechCorp.Models;
using TechCorp.Models.Interface;
using TechCorp.Repository;

Main();

void Main() {
    var service = new TrabajadorService(TrabajadorRepository.GetInstance());
    Save(service);
    Save(service);
    Acciones(service);
}



void Save(TrabajadorService service) {
    try {
        Console.WriteLine("Tipo: 1. Repartidor | 2. Reponedor | 3. Senior");
        string tipo = Console.ReadLine() ?? "";
    
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? "";

        Trabajador? nuevo = null;

        switch (tipo) {
            case "1":
                Console.Write("Barrio: ");
                string barrio = Console.ReadLine() ?? "";
                nuevo = new Repartidor { Nombre = nombre, Barrio = barrio };
                service.Save(nuevo);
                break;
            case "2":
                Console.Write("Sector (Letra): ");
                char sector = char.Parse(Console.ReadLine() ?? "Z");
                nuevo = new Reponedor { Nombre = nombre, Sector = sector };
                service.Save(nuevo);
                break;
            case "3":
                Console.Write("Años de Servicio: ");
                int años = int.Parse(Console.ReadLine() ?? "0");
                nuevo = new Senior { Nombre = nombre, AñosDeServicio = años };
                service.Save(nuevo);
                break;
        }
    }
    catch (Exception e) {
        Console.WriteLine(e); }
}

void Acciones(TrabajadorService service) {
    var lista = service.GetAllTrabajadores();
    foreach (var t in lista) {
        service.EjecutarAccionEspecial(t);
    }
}