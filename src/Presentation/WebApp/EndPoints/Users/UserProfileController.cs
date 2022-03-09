using CleanArchitecture.Core.Models.Entities.Users;
using CleanArchitecture.WebApp.Models.Dtos.Users;
namespace CleanArchitecture.WebApp.EndPoints.Users;

[Route("api/1.0/users/profile")]
[AppAuthorize]
public class UserProfileController : CleanArchitectureController
{
    public IMapper                      Mapper          { get; }
    public IRepositoryConductor<User>   UserConductor   { get; }

    public UserProfileController(
        IMapper                     mapper,
        IRepositoryConductor<User>  userConductor)
    {
        Mapper          = mapper;
        UserConductor   = userConductor;
    }


    public IActionResult Get()
    {
        var userResult = UserConductor.FindById(CurrentUserId);
        if (userResult.HasErrors)
        {
            return InternalError<UserDto>(userResult.Errors);
        }
        var dto = Mapper.Map<UserDto>(userResult.ResultObject);
        return Ok(dto, null);
    }

    [HttpPut("{id:Guid}")]
    public IActionResult Post(Guid id, [FromBody] UserDto dto)
    {
        var userResult = UserConductor.FindAll(e => e.UniqueId == id);
        if (userResult.HasErrors)
        {
            return InternalError<UserDto>(userResult.Errors);
        }
        var user = userResult.ResultObject.FirstOrDefault();
        if (user == null)
        {
            return InternalError<UserDto>("Invalid user");
        }
        if (user.Id != CurrentUserId) { return BadRequest("Not allowd to update profile"); }
        user.FirstName = dto.FirstName;
        user.LastName  = dto.LastName;

        var updateResult = UserConductor.Update(user, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<UserDto>(updateResult.Errors);
        }
        return Ok(dto, null);
    }
}
