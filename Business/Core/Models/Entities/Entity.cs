using CleanArchitecture.Core.Models.Entities.Users;

namespace CleanArchitecture.Core.Models.Entities;
public abstract class Entity 
{
    public Guid                 Id               { get; set; }
    public Guid                 CreatedById      { get; set; }
    public DateTimeOffset       CreatedOn        { get; set; }
    
    public virtual User? CreatedBy { get; set; }
}

public class Auditable : Entity
{
    public Guid?                DeletedById      { get; set; }
    public DateTimeOffset?      DeletedOn        { get; set; }
    public Guid?                UpdatedById      { get; set; }
    public DateTimeOffset?      UpdatedOn        { get; set; }

    public virtual User? DeletedBy { get; set; }
    public virtual User? UpdatedBy { get; set; }
}