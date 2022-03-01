namespace CleanArchitecture.Core.Models.Entities.Enquiries;
public class Enquiry : Auditable
{
    public new Guid?        CreatedById         { get; set; }
    public string           Email               { get; set; }
    public string           Name                { get; set; }
    public string           Message             { get; set; }
    public string?          Resolution          { get; set; }
    public bool             IsResolved          { get; set; }
    public string           Subject             { get; set; }
}