using CleanArchitecture.Core.Extensions;
using CleanArchitecture.Core.Interfaces.Data;
using CleanArchitecture.Core.Models.Entities;

namespace CleanArchitecture.SqlServer.Repositories;
public class Repository<T> : IRepository<T>
    where T : Entity
{
    #region Properties
    public  IContext      Context       { get; private set; }
    public  IQueryable<T> Query         { get; private set; }

    #endregion

    #region Constructors

    public Repository(IContext context)
    {
        Context     = context;
        Query       = context.Query<T>();
    }

    #endregion

    #region IRepository Implementation

    public virtual Result<T> Create(T entity, Guid createdById)
    {
        var result = new Result<T>();

        try
        {
            entity.CreatedOn    = DateTimeOffset.Now;
            if (createdById != Guid.Empty)
                entity.CreatedById = createdById;

            Context.Add(entity);
            Context.DetectChanges(); // Note: New to EF Core, #SaveChanges, #Add and other methods do NOT automatically call DetectChanges
            Context.SaveChanges();

            result.ResultObject = entity;
        }
        catch (Exception ex)
        {
            result.Errors = HandleException(ex);
        }

        return result;
    }


    public virtual Result<List<T>> Create(IEnumerable<T> entities, Guid createdById)
    {
        var result = new Result<List<T>> { ResultObject = new List<T>() };

        try
        {
            var numInserted = 0;

            foreach (var entity in entities)
            {
                entity.CreatedOn = DateTimeOffset.Now;
                if (createdById != Guid.Empty)
                    entity.CreatedById = createdById;
                
                Context.Add(entity);
                result.ResultObject.Add(entity);

                // Save in batches of 100, if there are at least 100 entities.
                if (++numInserted >= 100)
                {
                    numInserted = 0;

                    Context.DetectChanges(); // Note: New to EF Core, #SaveChanges, #Add and other methods do NOT automatically call DetectChanges
                    Context.SaveChanges();
                }
            }

            // Save whatever is left over.
            Context.DetectChanges(); // Note: New to EF Core, #SaveChanges, #Add and other methods do NOT automatically call DetectChanges
            Context.SaveChanges();
        }
        catch (Exception ex)
        {
            result.Errors = HandleException(ex);
        }

        return result;
    }
    public virtual Result<bool> Delete(Guid id, Guid deletedById, bool soft = true)
    {
        Result<T> findResult;
        if (soft == false)
        {
            findResult = FindById(id, true);
        }
        else
        {
            findResult = FindById(id);
        }
        if (findResult.HasErrors)
        {
            return new Result<bool>
            {
                Errors       = findResult.Errors,
                ResultObject = false
            };
        }

        return Delete(findResult.ResultObject, deletedById, soft);
    }
    public virtual Result<bool> Delete(T entity, Guid deletedById, bool soft = true)
    {
        var result = new Result<bool> { ResultObject = false };

        try
        {
            if (entity == null)
            {
                result.AddError($"{entity.GetType()} does not exist.");
                return result;
            }


            if (soft)
            {
                if(entity is Auditable auditable)
                {
                    auditable.DeletedById   = deletedById;
                    auditable.DeletedOn     = DateTimeOffset.Now;
                }
                else
                {
                    result.AddError($"{entity.GetType()} is not deleteatable.");
                    return result;
                }
            }
            else
            {
                Context.Delete(entity);
            }

            Context.SaveChanges();
            result.ResultObject = true;
        }
        catch (Exception ex)
        {
            result.Errors = HandleException(ex);
        }

        return result;
    }
    public virtual Result<IQueryable<T>> FindAll(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null,
        int? skip = null, int? take = null, bool? ignoreQueryFilters = false, bool asNoTracking = false)
    {
        var result = new Result<IQueryable<T>>();

        try
        {
            result.ResultObject = GetQueryable(filter, orderBy, includeProperties, skip, take, ignoreQueryFilters, asNoTracking);
        }
        catch (Exception ex)
        {
            result.Errors = HandleException(ex);
        }

        return result;
    }

    public virtual Result<T> FindById(Guid id, bool includeDeleted = false, params string[] includeProperties)
    {
        var result = new Result<T>();

        try
        {
            var query = Query;

            foreach (var property in includeProperties)
            {
                if (!string.IsNullOrEmpty(property))
                {
                    query = query.Include(property);
                }
            }
            if (includeDeleted)
            {
                result.ResultObject = query.FirstOrDefault(e => e.Id == id);
            }
            else
            {
                result.ResultObject = query.FirstOrDefault(e => e.Id == id);
            }
        }
        catch (Exception ex)
        {
            result.Errors = HandleException(ex);
        }

        return result;
    }
    public virtual Result<bool> Update(T entity, Guid updatedBy)
    {
        var result = new Result<bool> { ResultObject = false };

        try
        {
            if (entity is Auditable auditable)
            {
                auditable.UpdatedById   = updatedBy;
                auditable.UpdatedOn     = DateTimeOffset.Now;
            }
            else
            {
                result.AddError($"{entity.GetType()} is not auditable.");
                return result;
            }
            Context.Update(entity);
            Context.SaveChanges();

            result.ResultObject = true;
        }
        catch (Exception ex)
        {
            result.Errors = HandleException(ex);
        }

        return result;
    }
    public virtual Result<bool> Update(IEnumerable<T> entities, Guid updatedBy)
    {
        var result = new Result<bool> { ResultObject = false };
        try
        {
            foreach (var entity in entities)
            {
                if (entity is Auditable auditable)
                {
                    auditable.UpdatedById   = updatedBy;
                    auditable.UpdatedOn     = DateTimeOffset.Now;
                }
                else
                {
                    result.AddError($"{entity.GetType()} is not auditable.");
                    return result;
                }

                Context.Update(entity);
                Context.SaveChanges();
            }

            result.ResultObject = true;
        }
        catch (Exception ex)
        {
            result.Errors = HandleException(ex);
        }
        result.ResultObject = true;
        return result;
    }

    #endregion

    #region Protected Methods

    public virtual IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null,
        int? skip = null, int? take = null, bool? ignoreQueryFilters = false, bool asNoTracking = false)
    {
        includeProperties ??= string.Empty;
        var query         = Query;

        if (ignoreQueryFilters.HasValue && ignoreQueryFilters.Value)
        {
            query = query.IgnoreQueryFilters();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    #endregion

    #region Private Method
    private static List<string> HandleException(Exception ex)
    {
        var errors = new List<string>
        {
           $"Exception : {ex.Message} \n {ex.StackTrace}"
        };
        if (ex.InnerException != null)
            errors.Add($"InnerException : {ex.InnerException.Message} \n {ex.InnerException.StackTrace}");
        return errors;
    }

    #endregion
}
