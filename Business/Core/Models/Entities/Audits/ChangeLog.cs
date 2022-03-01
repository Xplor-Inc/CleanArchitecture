namespace CleanArchitecture.Core.Models.Entities.Audits;
public class ChangeLog : Entity
{
    public Guid     PrimaryKey          { get; set; }
    public string   EntityName          { get; set; }
    public string   PropertyName        { get; set; }
    public string?  OldValue            { get; set; }
    public string?  NewValue            { get; set; }
}