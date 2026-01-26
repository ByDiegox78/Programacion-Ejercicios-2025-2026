namespace FunkoPop.Config;

public sealed class Configuracion {
    
    private static readonly Configuracion instancia = new Configuracion();
    
    private Configuracion() {}
    
    public static Configuracion ObtenerInstancia() => instancia;

    public static readonly int TamanoInicial = 10;
    public static readonly decimal PrecioMinimo = 5.0m;
}