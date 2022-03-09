namespace CleanArchitecture.WebApp.Models.Dtos;
public class AuditableDto
{
    public long             Id              { get; set; }
    public long?            CreatedById     { get; set; }
    public DateTimeOffset?  CreatedOn       { get; set; }
    public long?            DeletedById     { get; set; }
    public DateTimeOffset?  DeletedOn       { get; set; }
    public long?            UpdatedById     { get; set; }
    public DateTimeOffset?  UpdatedOn       { get; set; }
    public Guid             UniqueId        { get; set; }
}
