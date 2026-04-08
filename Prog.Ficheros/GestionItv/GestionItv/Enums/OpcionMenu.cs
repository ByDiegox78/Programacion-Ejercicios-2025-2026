namespace GestionItv.Enums;

public enum OpcionMenu {
    Salir = 0,
    
    // Bloque General 
    ListarTodos = 1,
    BuscarPorId = 2,
    BuscarPoDniPropietario = 3,
    
    // Bloque de Vehiculos
    ListarVehiculos = 4,
    AnadirVehiculo = 5,
    ActualizarVehiculo = 6,
    EliminarVehiculo = 7,
    InformeVehiculo = 8,
    InformeIndivudial = 9,
    
    // Importar/Exportar
    ImportarDatos = 10,
    ExportarDatos = 11,
    
    // Backup 
    RealizarBackup = 12,
    RestaurarBackup = 13
}