using ExpressCargo.Core.Models.Entities;

namespace ExpressCargo.Core.Interfaces.Data;
public interface IRepository<T> where T :  Entity
{
    Result<T>             Create(T item, Guid createdById);
    Result<List<T>>       Create(IEnumerable<T> items, Guid createdById);
    Result<bool>          Delete(Guid id, Guid deletedById, bool soft = true);
    Result<bool>          Delete(T o, Guid deletedById, bool soft = true);
    Result<IQueryable<T>> FindAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, int? skip = null, int? take = null, bool? ignoreQueryFilters = false, bool asNoTracking = false);
    Result<T>             FindById(Guid id, bool includeDeleted = false, params string[] includeProperties);
    Result<bool>          Update(T item, Guid updatedBy);
    Result<bool>          Update(IEnumerable<T> entities, Guid updatedBy);
}