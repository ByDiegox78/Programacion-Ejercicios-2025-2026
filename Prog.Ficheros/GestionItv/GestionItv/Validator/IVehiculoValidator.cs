namespace GestionItv.Validator;

public interface IVehiculoValidator<in T> {
    IEnumerable<string> Validar(T entidad);
    
}