using System.Text.RegularExpressions;

namespace Ficha.Validator.Dvd;
using Ficha.Models;



public class DvdValidator : IDvdValidate
{
    private const int MinStringLength = 3;
    private const int MinAnio = 1975;
    private const int MaxAnio = 2027;
    public static readonly string NombreRegexValidate = @"^[A-Za-zñÑ]{3,}";
    public static readonly string DirectorRegexValidate = @"^[A-Za-zñÑ]{3,}";

    
    
    public Dvd Validate(Dvd dvd) {
       //Nombre validate
        if (string.IsNullOrWhiteSpace(dvd.nombre)) {
            throw new ArgumentException("El nombre no puede estar vacio");
        }

        var nombreLongitud = dvd.nombre.Length;

        if (nombreLongitud < MinStringLength) {
            throw new ArgumentOutOfRangeException(nameof(dvd.nombre),
                $"El nombre no puede tener menos de ${MinStringLength} letras.");
        }

        if (!NombreValidate(dvd.nombre)) {
            throw new ArgumentException("El nombre correcto no es acorde al formato");
        }
        
        //Director validator
        if (string.IsNullOrWhiteSpace(dvd.Director)) {
            throw new ArgumentException("El director no puede estar vacio");
        }
        

        if (dvd.Director.Length < MinStringLength) {
            throw new ArgumentOutOfRangeException(nameof(dvd.Director),
                $"El director no puede tener menos de ${MinStringLength} letras.");
        }

        if (!NombreValidate(dvd.nombre)) {
            throw new ArgumentException("El director correcto no es acorde al formato");
        }
        
        //Año validate
        if (dvd.Anio < MinAnio || dvd.Anio > MaxAnio) {
            throw new ArgumentOutOfRangeException(nameof(dvd),
                $"El año de publicacion no puede ser antes de {MinAnio} o mayor a {MaxAnio}");
        }

        return dvd;

    }
    
    private bool NombreValidate(string nombre) {
        bool isOk = Regex.IsMatch(nombre, NombreRegexValidate);
        return isOk;
    }
    
    private bool DirectorValidate(string director) {
        bool isOk = Regex.IsMatch(director, DirectorRegexValidate);
        return isOk;
    }
}