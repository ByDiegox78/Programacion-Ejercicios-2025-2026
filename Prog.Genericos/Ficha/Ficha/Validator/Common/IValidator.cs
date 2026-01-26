namespace Ficha.Validator.Common;

public interface IValidator<T>
{
    T Vaidate(T item);
}