using ExpressCargo.Core.Enumerations;

namespace ExpressCargo.WebApp.Models.Dtos.Users;
public class UserDto : AuditableDto
{
    #region Properties
    public DateTimeOffset?  AccountActivateDate     { get; set; }
    public string           EmailAddress            { get; set; }
    public string           FirstName               { get; set; }
    public Gender           Gender                  { get; set; }
    public bool             IsActive                { get; set; }
    public string           LastName                { get; set; }
    public UserRole         Role                    { get; set; }
    public string           ImagePath               { get; set; }


    #endregion Properties
}
