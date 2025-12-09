using FunkoPop.Config;
using FunkoPop.Enums;
using FunkoPop.Models;
using Serilog;

namespace FunkoPop.Validator;

public class FunkoValidator {
    public static readonly string RegexMenu = @$"^[{(int)OpcionMenu.Salir}-{(int)OpcionMenu.Eliminar}]$";
    public Funko Validators(Funko funko) {
        Log.Debug("Entrando en validador de funko...");
        if (string.IsNullOrEmpty(funko.Nombre) || string.IsNullOrWhiteSpace(funko.Nombre) || funko.Nombre.Length < 3) {
            throw new ArgumentException(
                "Nombre invalido. No se acepta nombre vacio o nulo, o con espacion vacios o con menos de 3 letras");
        }
        return funko.Precio < Configuracion.PrecioMinimo ? throw new ArgumentOutOfRangeException($"El precio no puede ser inferior a {Configuracion.PrecioMinimo}€") : funko;
    }
}