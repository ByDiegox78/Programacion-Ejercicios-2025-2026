namespace Productos.Repository.Common;

public interface ICrudRepository<TKey, TValue> {
    IEnumerable<TValue> GetAll();
    TValue? GetById(TKey id);
    TValue? Save(TValue value);
}