using CleanArchitecture.Core.Models.Entities.Users;

namespace CleanArchitecture.Core.Models.Entities;
public abstract class Entity 
{
    public long                 Id               { get; set; }
    public long                 CreatedById      { get; set; }
    public DateTimeOffset       CreatedOn        { get; set; }
    public Guid                 UniqueId         { get; set; }
    public virtual User?        CreatedBy        { get; set; }
}

public class Auditable : Entity
{
    public long?                DeletedById      { get; set; }
    public DateTimeOffset?      DeletedOn        { get; set; }
    public long?                UpdatedById      { get; set; }
    public DateTimeOffset?      UpdatedOn        { get; set; }

    public virtual User? DeletedBy { get; set; }
    public virtual User? UpdatedBy { get; set; }
}