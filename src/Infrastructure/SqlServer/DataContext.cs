using CleanArchitecture.Core.Interfaces.Data;
using CleanArchitecture.Core.Models.Entities;
using CleanArchitecture.Core.Models.Entities.Audits;
using CleanArchitecture.Core.Models.Entities.Users;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.SqlServer
{
    public class DataContext<TUser> : Context, IDataContext<TUser>
        where TUser : User
    {
        #region Properties

        public DbSet<ChangeLog>         ChangeLogs  { get; set; }
        public DbSet<User>              Users       { get; set; }

        #endregion Properties


        #region IDataContext Implementation

        IQueryable<User>    IDataContext<TUser>.Users  => Users;
        #endregion IDataContext Implementation


        #region Constructor

        public DataContext(string connectionString, ILoggerFactory loggerFactory)
            : base(connectionString, loggerFactory)
        {
        }
        public DataContext(IConnection connection, ILoggerFactory loggerFactory)
            : base(connection.ToString(), loggerFactory)
        {
        }

        #endregion Constructor


        #region Overrides of DataContext

        public override void ConfigureMappings(ModelBuilder modelBuilder)
        {
            base.ConfigureMappings(modelBuilder);
        }

        public override void CreateStructure()
        {
            Database.Migrate();
        }

        public override void DeleteDatabase()
        {
            Database.EnsureDeleted();
        }

        public override void DropStructure()
        {
            var migrator = this.GetInfrastructure().GetRequiredService<IMigrator>();
            migrator.Migrate("0");
        }
        public override int SaveChanges()
        {
            try
            {
                var modifiedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
                long actionBy = 0;
                foreach (var change in modifiedEntities)
                {
                    string entityName = change.Entity.GetType().Name;
                    long primaryKey = ((Entity)change.Entity).Id;
                    var changeLogs = new List<ChangeLog>();
                    if (change.CurrentValues["UpdatedById"] != null)
                    {
                        actionBy = (long)change.CurrentValues["UpdatedById"];
                    }

                    foreach (var prop in change.OriginalValues.Properties)
                    {
                        if (IgnoredProperties.Contains(prop.Name))
                        {
                            continue;
                        }
                        var originalValue = change.OriginalValues[prop]?.ToString();
                        var currentValue = change.CurrentValues[prop]?.ToString();
                        if (originalValue != currentValue) //Only create a log if the value changes
                        {
                            changeLogs.Add(new ChangeLog()
                            {
                                EntityName      = entityName,
                                PrimaryKey      = primaryKey,
                                PropertyName    = prop.Name,
                                OldValue        = originalValue,
                                NewValue        = currentValue,
                                CreatedOn       = DateTimeOffset.Now,
                                CreatedById     = actionBy
                            });
                        }
                    }
                    ChangeLogs.AddRange(changeLogs);
                }
            }
            catch (Exception)
            {
            }
            return base.SaveChanges();
        }

        public string[] IgnoredProperties => new string[] { "CreatedOn", "UpdatedOn", "Id" };


        #endregion Overrides of DataContext
    }
}
