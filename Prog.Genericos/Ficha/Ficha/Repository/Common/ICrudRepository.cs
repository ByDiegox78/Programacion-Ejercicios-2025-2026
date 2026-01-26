
using Ficha.Collections.Lista;

namespace Ficha.Repository.Common;


public interface ICrudRepository<TEntity, TId> where TEntity : class
{
    ILista<TEntity> GetAll();
    
    TEntity? GetById(TId id);

    TEntity? Create(TEntity entity);

    TEntity? Update(TEntity entity, TId id);

    TEntity? Delete(TId id);

}