namespace CleanArchitecture.WebApp.Models.Dtos;
public class AuditableDto
{
    public Guid             Id              { get; set; }
    public Guid?            CreatedById     { get; set; }
    public DateTimeOffset?  CreatedOn       { get; set; }
    public Guid?            DeletedById     { get; set; }
    public DateTimeOffset?  DeletedOn       { get; set; }
    public Guid?            UpdatedById     { get; set; }
    public DateTimeOffset?  UpdatedOn       { get; set; }

}
