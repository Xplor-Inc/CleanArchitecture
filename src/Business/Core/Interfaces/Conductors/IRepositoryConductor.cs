namespace CleanArchitecture.Core.Interfaces.Conductor;
public interface IRepositoryConductor<T>   where T : Models.Entities.Entity
{
    Result<T>              Create(T item, long createdById);
    Result<List<T>>        Create(IEnumerable<T> items, long createdById);
    Result<bool>           Delete(long id, long deletedById, bool soft = true);
    Result<bool>           Delete(T o, long deletedById, bool soft = true);
    Result<IQueryable<T>>  FindAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, int? skip = null, int? take = null, bool? ignoreQueryFilters = false, bool asNoTracking = false);
    Result<T>              FindById(long id, bool includeDeleted = false, params string[] includeProperties);
    Result<bool>           Update(T item, long updatedBy);
    Result<bool>           Update(IEnumerable<T> items, long updatedBy);
}