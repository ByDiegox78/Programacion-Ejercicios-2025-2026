namespace Vehiculos.Validator;

public interface IValidador<in T> {
    IEnumerable<string> Validar(T entidad);
}