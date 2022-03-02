namespace CleanArchitecture.Core.Models.Configuration;
public class StaticFileConfiguration
{
    public List<string> AllowedExtention    { get; set; } = new List<string>();
    public string       ProfileImageName    { get; set; } = string.Empty;
    public int          MaxFileSize         { get; set; }
    public string       RootFolder          { get; set; } = string.Empty;
    public string       SubFolder           { get; set; } = string.Empty;
}