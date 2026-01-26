using System.Text.RegularExpressions;

namespace Ficha.Validator.Dvd;
using Ficha.Models;



public class DvdValidator : IDvdValidate
{
    private const int MinAnio = 1975;
    private const int MaxAnio = 2027;
    public static readonly string NombreRegexValidate = @"^[A-Za-zñÑ]{3,}";

    
    
    public Dvd Vaidate(Dvd dvd) {
        if (dvd.Anio < MinAnio || dvd.Anio > MaxAnio) {
            throw new ArgumentOutOfRangeException(nameof(dvd),
                $"El año de publicacion no puede ser antes de {MinAio} o mayor a {MaxAñoPublicacion}");
        }
        
    }
    
    private bool NombreValidate(string nombre) {
        bool isOk = Regex.IsMatch(nombre, NombreRegexValidate);
        return isOk;
    }
}