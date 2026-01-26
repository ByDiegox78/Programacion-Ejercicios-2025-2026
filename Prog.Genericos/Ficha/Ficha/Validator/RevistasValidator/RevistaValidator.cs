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
    
    public Revistas Vaidate(Revistas revistas) {
        if (string.IsNullOrWhiteSpace(revistas.nombre)) {
            throw new ArgumentException("El nombre no puede estar vacio");
        }

        var nombreLongitud = revistas.nombre.Length;

        if (nombreLongitud < MinNombreLength) {
            throw new ArgumentOutOfRangeException(nameof(revistas.nombre),
                $"El nombre no puede tener menos de ${MinNombreLength} letras.");
        }

        if (!NombreValidate(revistas.nombre)) {
            throw new ArgumentException("El nombre correcto no es acorde al formato");
        }

        if (revistas.AnioPublicacion < MinAñoPublicacion || revistas.AnioPublicacion > MaxAñoPublicacion) {
            throw new ArgumentOutOfRangeException(nameof(revistas),
                $"El año de publicacion no puede ser antes de {MinAñoPublicacion} o mayor a {MaxAñoPublicacion}");
        }
        
        if (revistas.NumeroLista < MinNumRevista) {
            throw new ArgumentOutOfRangeException(nameof(revistas),
                $"El numero de revista no puede ser menor que {MinNumRevista}");
        }

        return revistas;
    }
    private bool NombreValidate(string nombre) {
        bool isOk = Regex.IsMatch(nombre, NombreRegexValidate);
        return isOk;
    }
}