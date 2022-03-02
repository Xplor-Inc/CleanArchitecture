using CleanArchitecture.Core.Models.Entities.Enquiries;
using CleanArchitecture.Core.Models.Entities.Users;

namespace CleanArchitecture.Core.Interfaces.Emails.Templates;
public interface IHtmlTemplate
{
    string AccountActivation(User user);
    string EnquiryThanks(Enquiry enquiry);
    string ResetPassword(User user);

}
