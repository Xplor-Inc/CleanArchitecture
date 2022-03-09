namespace CleanArchitecture.Core.Models.Entities.Audits;
public class ChangeLog : Entity
{
    public string   EntityName          { get; set; } = string.Empty;
    public string?  NewValue            { get; set; }
    public string?  OldValue            { get; set; }
    public long     PrimaryKey          { get; set; }
    public string   PropertyName        { get; set; } = string.Empty;
}