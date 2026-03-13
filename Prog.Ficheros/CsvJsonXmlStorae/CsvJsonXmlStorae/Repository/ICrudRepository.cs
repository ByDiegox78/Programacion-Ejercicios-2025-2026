namespace CsvJsonXmlStorae.Repository;

public interface ICrudRepository<TKey, TEntity> where TEntity : class {
    IEnumerable<TEntity> GetAll();
    TEntity? GetById(TKey id);
    TEntity? Create(TEntity entity);
    TEntity? Update(TKey id, TEntity entity);
    TEntity? Delete(TKey id);
}