
using System.Text;
using Conquistadores_de_Catán.Service;
using Serilog;
using static System.Console;

// 1. INICIALIZACIÓN DE SERILOG Y CULTURA
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
Log.Information("Serilog iniciado y configurado para consola.");

// 2. INICIO (Top-level Execution)
Title = "Conquistadores De Catan";
OutputEncoding = Encoding.UTF8;
Clear();

// Menú principal
Main(args);
return;

void Main(string[] args) {
    var patan = new ServicioPatan();
    patan.Simulacion();
}



















