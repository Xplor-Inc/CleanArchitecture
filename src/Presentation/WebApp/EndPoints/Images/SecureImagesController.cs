using CleanArchitecture.Core.Models.Entities.Users;
using CleanArchitecture.Core.Models.Configuration;
using CleanArchitecture.WebApp.Models.Dtos.Users;

namespace CleanArchitecture.WebApp.EndPoints;

[Route("dynamic/images")]
[AppAuthorize]
public class SecureImagesController : CleanArchitectureController
{
    #region Properties

    private string                     FolderPath        { get { return Path.Combine(Environment.CurrentDirectory, FileConfiguration.RootFolder, FileConfiguration.SubFolder); } }
    private StaticFileConfiguration    FileConfiguration { get; }
    private IRepositoryConductor<User> UserConductor     { get; }
    public IMapper Mapper { get; }
    #endregion

    public SecureImagesController(
        StaticFileConfiguration     fileConfiguration,
        IMapper mapper,
        IRepositoryConductor<User>  userConductor)
    {
        FileConfiguration = fileConfiguration;
        UserConductor = userConductor;
        Mapper = mapper;
    }
    
    [HttpGet("{imagePath}")]
    public IActionResult Get(string imagePath)
    {
        var type = imagePath.Split('.').Last();
        string filePath = @$"{FolderPath}\{imagePath}";
        if (System.IO.File.Exists(filePath))
            return PhysicalFile(filePath, $"image/{type}");
        else
            return File("/Images/No_image_available.svg", "image/svg");
    }    
    
    [HttpPost("profile")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null)
        {
            return InternalError<UserDto>("Invalid file");
        }
        var extention = Path.GetExtension(file.FileName);
        if (!FileConfiguration.AllowedExtention.Contains(extention) || file.Length == 0)
        {
            return InternalError<UserDto>($"{extention} file type is not allowed");
        }
        if (file.Length > FileConfiguration.MaxFileSize) { return InternalError<UserDto>($"File size limit excceded"); }

        DirectoryInfo di = new(FolderPath);
        if (!di.Exists) 
        {
            di.Create();
        }
        //Delete old files if they exists
        var files = di.GetFiles().Where(e => e.Name.StartsWith($"{CurrentUserId:N}"));
        if(files.Any())
        {
            files.ToList().ForEach(f =>f.Delete()); 
        }
        var name = $"{CurrentUserId:N}{extention}";

        var path = Path.Combine(FolderPath, name);
        using var stream = System.IO.File.Create(path);
        await file.CopyToAsync(stream);
        
        var userResult = UserConductor.FindById(CurrentUserId);
        if (userResult.HasErrors)
        {
            return InternalError<UserDto>(userResult.Errors);
        }
        if(userResult.ResultObject != null)
        {
            userResult.ResultObject.ImagePath = name;
            var userUpdateResult = UserConductor.Update(userResult.ResultObject, CurrentUserId);
            if (userUpdateResult.HasErrors)
            {
                return InternalError<UserDto>(userUpdateResult.Errors);
            }
        }
        var userDto = Mapper.Map<UserDto>(userResult.ResultObject);
        return Ok(userDto, null);
    }
}