namespace CleanArchitecture.Core.Models.Entities.Users;
public class User : Auditable
{
    #region Properties
    public DateTimeOffset?  AccountActivateDate     { get; set; }
    public string           EmailAddress            { get; set; }
    public string           FirstName               { get; set; }
    public string           ImagePath               { get; set; }
    public bool             IsAccountActivated      { get; set; }
    public bool             IsActive                { get; set; }
    public DateTimeOffset?  LastLoginDate           { get; set; }
    public string           LastName                { get; set; }
    public DateTimeOffset?  PasswordChangeDate      { get; set; }
    public string           PasswordHash            { get; set; }
    public string           PasswordSalt            { get; set; }
    public UserRole         Role                    { get; set; }
    public string           SecurityStamp           { get; set; }

    #endregion Properties

    #region Virtual Users

    public virtual List<User>?       UsersCreatedBy          { get; set; }
    public virtual List<User>?      UsersDeletedBy          { get; set; }
    public virtual List<User>?      UsersUpdatedBy          { get; set; }
    #endregion

}