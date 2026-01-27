using System.Text.RegularExpressions;
using Ficha.Models;

namespace Ficha.Validator.RevistasValidator;

public class RevistaValidator : IRevistaValidate
{
    private const int MinNombreLength = 3;
    private const int MinAñoPublicacion = 1975;
    private const int MaxAñoPublicacion = 2027;
    private const int MinNumRevista = 3;
    public static readonly string NombreRegexValidate = @"^[A-Za-zñÑ]{3,}";
    
    public Revista Validate(Revista revista) {
        if (string.IsNullOrWhiteSpace(revista.nombre)) {
            throw new ArgumentException("El nombre no puede estar vacio");
        }

        var nombreLongitud = revista.nombre.Length;

        if (nombreLongitud < MinNombreLength) {
            throw new ArgumentOutOfRangeException(nameof(revista.nombre),
                $"El nombre no puede tener menos de ${MinNombreLength} letras.");
        }

        if (!NombreValidate(revista.nombre)) {
            throw new ArgumentException("El nombre correcto no es acorde al formato");
        }

        if (revista.AnioPublicacion < MinAñoPublicacion || revista.AnioPublicacion > MaxAñoPublicacion) {
            throw new ArgumentOutOfRangeException(nameof(revista),
                $"El año de publicacion no puede ser antes de {MinAñoPublicacion} o mayor a {MaxAñoPublicacion}");
        }
        
        if (revista.NumeroLista < MinNumRevista) {
            throw new ArgumentOutOfRangeException(nameof(revista),
                $"El numero de revista no puede ser menor que {MinNumRevista}");
        }

        return revista;
    }
    private bool NombreValidate(string nombre) {
        bool isOk = Regex.IsMatch(nombre, NombreRegexValidate);
        return isOk;
    }
}