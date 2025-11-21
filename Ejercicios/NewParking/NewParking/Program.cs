
using System.Text.RegularExpressions;
using NewParking.Enum;
using NewParking.Struct;

void Main(string[] args) {
    
};

Main(args);{

    var parking = new Vehiculo?[2,5];
    int opcion;
    do {
        opcion = LeerOpcion("Introduce la opcion del parkin:...");
        switch (opcion) {
            case (int)MenuParking.VerParking:
                //VerParking();
                break;
            case (int)MenuParking.AparcarCoche:
                //AparcarCoche();
                break;
            case (int)MenuParking.ActualizarMatricula:
                //ActualizarMatricula
                break;
            case (int)MenuParking.DesaparcarCoche:
                //DesaparcarCoche();
                break;
            case (int)MenuParking.BuscarCoche:
                //BuscarCoche();
                break;
            case (int)MenuParking.OrdenarPorMatricula:
                //OrdenarPorMatricula
                break;
            case (int)MenuParking.OrdenarPorFechaMatriculacion:
                //OrdenarPorMatriculacion();
                break;
            case (int)MenuParking.Salir:
                break;
        }
    } while (opcion != 8);
}

int LeerOpcion(string msj) {
    var regex = new Regex("^[1-8]$");
    var isOk = false;
    string? input;
    var opcion = 0;
    do {
        Console.Write(msj);
        input = (Console.ReadLine() ?? "").Trim();
        isOk  = regex.IsMatch(input);
        if (isOk) {
            opcion = int.Parse(input);
        }
    } while (!isOk);
    return opcion;
}