using Ficha.Collections.Lista;
using Lol.Models;
using Lol.Validator.Common;

namespace Lol.Validator;

public class AsesinoValidate : IValidador<Campeon> {
    private static readonly decimal[] PreciosValidos = { 450, 1350, 3150, 4800, 6300, 7800, 4444, 3141 };
    public IEnumerable<string> Validar(Campeon campeon) {
        var errores = new List<string>();
        

        if (campeon is not Asesino asesino) {
            errores.Add("El campeon no es un asesino");
            return errores;
        }
        
        if (string.IsNullOrWhiteSpace(campeon.Nombre) || campeon.Nombre.Length < 2)
            errores.Add("El nombre del campeon es obligatorio (mín. 2 car.).");

        if (campeon.Nivel is < 0 or > 18) {
            errores.Add("El nivel no puede ser menor que 0 o mayor que 18.");
        }

        if (campeon.PrecioEsencias is not (450 or 1350 or 3150 or 4800 or 6300 or 7800)) {
            errores.Add("El precio intricucido es valido; Precios validos: 450, 1350, 3150, 4800, 6300, 7800, 4444, 3141");
        }

        if (asesino.Letalidad is < 0 or > 100) {
            errores.Add("La letalidad introducida no puede ser negativa");
        }

        if (campeon.HabilidadCampeon.Count != 4) {
            errores.Add("El campeón debe tener exactamente 4 habilidades (Q, W, E, R)");
        }

        foreach (var habilidad in campeon.HabilidadCampeon) {
            if (string.IsNullOrWhiteSpace(habilidad.Nombre) || habilidad.Nombre.Length < 2)
                errores.Add("El nombre del docente es obligatorio (mín. 2 car.).");
            if (habilidad.Cooldawn is < 0 ) {
                errores.Add("El cooldawn no puede ser negativo");
            }
        }


        return errores;

    }
}