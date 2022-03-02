namespace ExpressCargo.Core.Models.Entities.Enquiries;
public class Enquiry : Auditable
{
    public new Guid?        CreatedById         { get; set; }
    public string           Email               { get; set; } = string.Empty;
    public string           Name                { get; set; } = string.Empty;
    public string           Message             { get; set; } = string.Empty;
    public string?          Resolution          { get; set; }
    public bool             IsResolved          { get; set; }
    public string           Subject             { get; set; } = string.Empty;
}