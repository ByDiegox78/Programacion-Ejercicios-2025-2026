using Ficha.Collections.Lista;
using Lol.Models;

namespace Lol.Repository.Common;

public interface ICrudRepository<in TKey, TEntity> where TEntity : class {
 
    ILista<TEntity> GetAll();
    
    TEntity? GetById(TKey id);

    TEntity? Create(TEntity entity);
    
    TEntity? Update(TKey id, TEntity entity);
    
    TEntity? Delete(TKey id);
    
}