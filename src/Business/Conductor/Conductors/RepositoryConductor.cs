using FinanceManager.Core.Interfaces.Conductor;
using FinanceManager.Core.Interfaces.Data;
using FinanceManager.Core.Interfaces.Errors;
using FinanceManager.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FinanceManager.Conductor
{
    public class RepositoryConductor<T> : Conductor, IRepositoryConductor<T>
        where T : Entity
    {
        public IRepository<T> Repository { get; }
        public RepositoryConductor(IRepository<T> repository)
        {
            Repository = repository;
        }


        public virtual IResult<T> Create(T item, long createdById) => Repository.Create(item, createdById);
        public virtual IResult<List<T>> Create(IEnumerable<T> items, long createdById) => Repository.Create(items, createdById);

        public virtual IResult<IQueryable<T>> FindAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = default,
            int? take = default,
            bool? ignoreQueryFilters = false,
            bool asNoTracking = false
        ) => Repository.FindAll(filter, orderBy, includeProperties, skip, take, ignoreQueryFilters, asNoTracking);
        public IResult<T> FindById(long id, bool includeDeleted = false, params string[] includeProperties) => Repository.FindById(id: id, includeDeleted: includeDeleted, includeProperties: includeProperties);

        public IResult<T> FindById(Guid id, bool includeDeleted = false, params string[] includeProperties) => Repository.FindById(id: id, includeDeleted: includeDeleted, includeProperties: includeProperties);
        public virtual IResult<bool> Update(T item, long updatedBy) => Repository.Update(item, updatedBy);
        public virtual IResult<bool> Update(IEnumerable<T> items, long updatedBy) => Repository.Update(items, updatedBy);
        public virtual IResult<bool> Delete(long id, long deletedById, bool soft = true) => Repository.Delete(id, deletedById, soft);
        public virtual IResult<bool> Delete(T o, long deletedById, bool soft = true) => Repository.Delete(o, deletedById, soft);
    }
}
