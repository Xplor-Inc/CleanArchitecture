using CleanArchitecture.Core.Interfaces.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanArchitecture.SqlServer;
public abstract class Context : DbContext, IContext
{
    #region Member Variables

    private readonly string         _connectionString = "";
    private readonly ILoggerFactory _loggerFactory;

    #endregion

    #region Constructors
    public Context()
    {
    }
    public Context(string connectionString, ILoggerFactory loggerFactory)
    {
        _connectionString = connectionString;
        _loggerFactory    = loggerFactory;
    }

    #endregion

    #region Overrides

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_loggerFactory != null)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
        }

        optionsBuilder.UseSqlServer(connectionString: _connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Remove cascade delete
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Configure mappings
        ConfigureMappings(modelBuilder);

        // Call the base method
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges() => base.SaveChanges();

    #endregion

    #region IDataContext Implementation
    public IQueryable<T> Query<T>(string SPName) where T : class
    {
        return base.Set<T>().FromSqlRaw(SPName);
    }
    public new void Add<T>(T entity) where T : class => base.Add(entity);

    public virtual void CreateStructure()
    {
    }

    public virtual void DeleteDatabase()
    {
    }
    public void Delete<T>(T entity) where T : class
    {
        var set = base.Set<T>();
        set.Remove(entity);
    }

    public void DetectChanges() => base.ChangeTracker.DetectChanges();
    public virtual void DropStructure()
    {

    }
    public IQueryable<T> Query<T>() where T : class => base.Set<T>();
    public new void Update<T>(T entity) where T : class
    {
        var set = base.Set<T>();

        if (!set.Local.Any(e => e == entity))
        {
            set.Attach(entity);
            SetAsModified(entity);
        }
    }

    #endregion

    #region Public Methods

    public virtual void ConfigureMappings(ModelBuilder modelBuilder)
    {
    }

    public virtual long ExecuteCommand(string commandText) => base.Database.ExecuteSqlRaw(commandText);

    #endregion

    #region Private Methods
    private EntityEntry GetEntityEntry<T>(T entity) where T : class
    {
        var entry = Entry<T>(entity);

        if (entry.State == EntityState.Deleted)
        {
            Set<T>().Attach(entity);
        }

        return entry;
    }
    private void SetAsModified<T>(T entity) where T : class
    {
        UpdateEntityState(entity, EntityState.Modified);
    }
    private void UpdateEntityState<T>(T entity, EntityState state) where T : class
    {
        GetEntityEntry(entity).State = state;
    }

    #endregion
}