using CleanArchitecture.WebApp.Models.Dtos.Users;

namespace CleanArchitecture.WebApp.Models.Dtos.Enquiries;
public class EnquiryDto : AuditableDto
{
    public string           Email               { get; set; }
    public string           Name                { get; set; }
    public string           Message             { get; set; }
    public string           Resolution          { get; set; }
    public bool             IsResolved          { get; set; }
    public string           Subject             { get; set; }
    public virtual UserDto  UpdatedBy           { get; set; }
}