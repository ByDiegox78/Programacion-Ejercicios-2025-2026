namespace GestionItv.Repository.Common;

public interface ICrudRepository<in TKey, TEntity> where TEntity : class {
    IEnumerable<TEntity> GetAll();
    TEntity? GetByMatricula(TKey matricula);
    TEntity? Create(TEntity entity);
    TEntity? Update(TKey matricula, TEntity entity);
    TEntity? Delete(TKey matricula);
}