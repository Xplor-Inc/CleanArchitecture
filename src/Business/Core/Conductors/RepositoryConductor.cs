using ExpressCargo.Core.Interfaces.Conductor;
using ExpressCargo.Core.Interfaces.Data;
using ExpressCargo.Core.Models.Entities;

namespace ExpressCargo.Core.Conductors;
public class RepositoryConductor<T> : IRepositoryConductor<T>  where T : Entity
{
    public IRepository<T> Repository { get; }
    public RepositoryConductor(IRepository<T> repository)
    {
        Repository = repository;
    }


    public virtual Result<T> Create(T item, Guid createdById) => Repository.Create(item, createdById);
    public virtual Result<List<T>> Create(IEnumerable<T> items, Guid createdById) => Repository.Create(items, createdById);

    public virtual Result<IQueryable<T>> FindAll(
        Expression<Func<T, bool>>?                  filter              = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>?  orderBy             = null,
        string?                                     includeProperties   = null,
        int?                                        skip                = default,
        int?                                        take                = default,
        bool?                                       ignoreQueryFilters  = false,
        bool                                        asNoTracking        = false
    ) => Repository.FindAll(filter, orderBy, includeProperties, skip, take, ignoreQueryFilters, asNoTracking);

    public Result<T> FindById(Guid id, bool includeDeleted = false, params string[] includeProperties) => Repository.FindById(id: id, includeDeleted: includeDeleted, includeProperties: includeProperties);
    public virtual Result<bool> Update(T item, Guid updatedBy) => Repository.Update(item, updatedBy);
    public virtual Result<bool> Update(IEnumerable<T> items, Guid updatedBy) => Repository.Update(items, updatedBy);
    public virtual Result<bool> Delete(Guid id, Guid deletedById, bool soft = true) => Repository.Delete(id, deletedById, soft);
    public virtual Result<bool> Delete(T o, Guid deletedById, bool soft = true) => Repository.Delete(o, deletedById, soft);
}