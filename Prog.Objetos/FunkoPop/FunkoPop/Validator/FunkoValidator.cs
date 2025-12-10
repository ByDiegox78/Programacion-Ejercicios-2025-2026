using FunkoPop.Config;
using FunkoPop.Enums;
using FunkoPop.Models;
using Serilog;

namespace FunkoPop.Validator;

public class FunkoValidator {
    public static readonly string RegexMenu = @$"^[{(int)OpcionMenu.Salir}-{(int)OpcionMenu.OrdenarPrecioDesc}]$";
    public static readonly string RegexId = "^[1-9][0-9]*$";
    public static readonly string RegexNombreApellido = "^[A-Z]{1}[a-z]{2,}$";
    public static readonly string RegexTipo = @"^(Anime|Superheroe|Disney)$";
    public static readonly string RegexCambio = @$"^[{(int)Cambio.Nombre}-{(int)Cambio.Salir}]$";
    
    public Funko Validators(Funko funko) {
        Log.Debug("Entrando en validador de funko...");
        if (string.IsNullOrEmpty(funko.Nombre) || string.IsNullOrWhiteSpace(funko.Nombre) || funko.Nombre.Length < 3) {
            throw new ArgumentException(
                "Nombre invalido. No se acepta nombre vacio o nulo, o con espacion vacios o con menos de 3 letras");
        }
        return funko.Precio < Configuracion.PrecioMinimo ? throw new ArgumentOutOfRangeException($"El precio no puede ser inferior a {Configuracion.PrecioMinimo}€") : funko;
    }
}