using Ficha.Collections.Lista;

namespace TechCorp.Repository;

public interface ICrudRepository<TEntity, TKey> where TEntity : class {
    ILista<TEntity>
}