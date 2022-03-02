using ExpressCargo.Core.Enumerations;
using ExpressCargo.Core.Interfaces.Conductors.Accounts;
using ExpressCargo.Core.Models.Configuration;
using ExpressCargo.Core.Models.Entities.Users;
using ExpressCargo.WebApp.Models.Dtos.Users;

namespace ExpressCargo.WebApp.EndPoints.Users;

[Route("api/1.0/users")]
[AppAuthorize]
public class UsersController : ExpressCargoController
{
    public IAccountConductor            AccountConductor    { get; }
    private IMapper                     Mapper              { get; }
    public StaticFileConfiguration      StaticFile          { get; }
    private IRepositoryConductor<User>  UserConductor       { get; }

    public UsersController(
        IAccountConductor           accountConductor,
        IMapper                     mapper,
        StaticFileConfiguration     staticFile,
        IRepositoryConductor<User>  userConductor)
    {
        AccountConductor    = accountConductor;
        Mapper              = mapper;
        StaticFile          = staticFile;
        UserConductor       = userConductor;
    }


    [HttpGet]
    public IActionResult Index(
        string      searchText,
        bool        includeDeleted,
        UserRole?   userRole,
        string      sortBy      = "FirstName",
        string      sortOrder   = "ASC",
        int         skip        = 0,
        int         take        = 5)
    {
        Expression<Func<User, bool>> predicate = e => true;

        if (!includeDeleted)
        {
            predicate = predicate.AndAlso(e => e.DeletedOn == null);
        }
       
        if (userRole.HasValue)
        {
            predicate = predicate.AndAlso(e => e.Role == userRole.Value);
        }
        if (!string.IsNullOrEmpty(searchText))
        {
            predicate = predicate.AndAlso(e => $"{e.FirstName} {e.LastName}".Contains(searchText) || e.EmailAddress.Contains(searchText));
        }

        var userResult = UserConductor.FindAll(filter: predicate, e => e.OrderBy(sortBy, sortOrder), skip: skip, take: take);
        if (userResult.HasErrors)
        {
            return InternalError<UserDto>(userResult.Errors);
        }
        var users = userResult.ResultObject.ToList();
        var rowCount = UserConductor.FindAll(filter: predicate).ResultObject.Select(e => e.Id).Count();
        var dtos = Mapper.Map<List<UserDto>>(users);
        return Ok(dtos, rowCount);
    }

    [HttpPut("{id:Guid}")]
    public IActionResult GetById()
    {
        var userResult = UserConductor.FindById(CurrentUserId);
        if (userResult.HasErrors)
        {
            return InternalError<UserDto>(userResult.Errors);
        }
        var user = userResult.ResultObject;
        if (user == null)
        {
            return InternalError<UserDto>("Invalid user");
        }
        var userDto = Mapper.Map<UserDto>(user);

        return Ok(userDto, null);
    }

    [HttpPost()]
    public IActionResult Post([FromBody] UserDto dto)
    {       
        var user = Mapper.Map<User>(dto);
        user.Role       = UserRole.Member;
        user.ImagePath  = StaticFile.ProfileImageName;
        var createResult = AccountConductor.CreateAccount(user, CurrentUserId);
        if (createResult.HasErrors)
        {
            return InternalError<UserDto>(createResult.Errors);
        }
        return Ok(createResult.ResultObject);
    }

    [HttpPut("{id:Guid}")]
    public IActionResult Put(Guid id, [FromBody] UserDto dto)
    {
        if(id == CurrentUserId)
        {
            return InternalError<UserDto>("Please update your details in profile page");
        }

        var userResult = UserConductor.FindById(id);
        if (userResult.HasErrors)
        {
            return InternalError<UserDto>(userResult.Errors);
        }
        var user = userResult.ResultObject;
        if (user == null)
        {
            return InternalError<UserDto>("Invalid user");
        }

        user.IsActive   = dto.IsActive;
        user.FirstName  = dto.FirstName;
        user.LastName   = dto.LastName;

        var updateResult = UserConductor.Update(user, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<UserDto>(updateResult.Errors);
        }
        return Ok(updateResult.ResultObject);
    }

    [HttpDelete("{id:Guid}")]
    public IActionResult Delete(Guid id)
    {
        if (id == CurrentUserId)
        {
            return InternalError<UserDto>("You can't delete self account");
        }

        var updateResult = UserConductor.Delete(id, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<UserDto>(updateResult.Errors);
        }
        return Ok(updateResult.ResultObject);
    }
}