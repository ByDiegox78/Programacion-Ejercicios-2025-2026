namespace Ficha.Validator.Common;

public interface IValidator<T>
{
    T Validate(T item);
}