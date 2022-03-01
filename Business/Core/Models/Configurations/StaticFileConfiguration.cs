namespace CleanArchitecture.Core.Models.Configuration;
public class StaticFileConfiguration
{
    public List<string> AllowedExtention    { get; set; }
    public string       ProfileImageName    { get; set; }
    public int          MaxFileSize         { get; set; }
    public string       RootFolder          { get; set; }
    public string       SubFolder           { get; set; }
}