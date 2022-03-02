namespace ExpressCargo.Core.Models.Configuration;
public class EmailConfiguration
{
    public bool     EnableSsl   { get; set; }
    public string   From        { get; set; } = string.Empty;
    public string   Header      { get; set; } = string.Empty;
    public string   Host        { get; set; } = string.Empty;
    public string   Password    { get; set; } = string.Empty;
    public int      Port        { get; set; }
    public bool     SendEmail   { get; set; }
    public string   ReplyTo     { get; set; } = string.Empty;
    public string   UserName    { get; set; } = string.Empty;

    public Template Templates   { get; set; } = new Template();
}
public class Template
{
    public string AccountActivationLink         { get; set; } = string.Empty;
    public string AccountActivationTemplate     { get; set; } = string.Empty;
    public string EnquiryTemplate               { get; set; } = string.Empty;
    public string ResetPasswordLink             { get; set; } = string.Empty;
    public string ResetPasswordTemplate         { get; set; } = string.Empty;
}