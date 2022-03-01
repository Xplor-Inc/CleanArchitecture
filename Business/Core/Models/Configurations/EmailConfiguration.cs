namespace CleanArchitecture.Core.Models.Configuration;
public class EmailConfiguration
{
    public bool     EnableSsl   { get; set; }
    public string   From        { get; set; }
    public string   Header      { get; set; }
    public string   Host        { get; set; }
    public string   Password    { get; set; }
    public int      Port        { get; set; }
    public bool     SendEmail   { get; set; }
    public string   ReplyTo     { get; set; }
    public string   UserName    { get; set; }

    public Template Templates   { get; set; }
}
public class Template
{
    public string AccountActivationLink         { get; set; }
    public string AccountActivationTemplate     { get; set; }
    public string EnquiryTemplate               { get; set; }
    public string ResetPasswordLink             { get; set; }
    public string ResetPasswordTemplate         { get; set; }
}