using ExpressCargo.Core.Models.Entities.Enquiries;
using ExpressCargo.Core.Models.Entities.Users;

namespace ExpressCargo.Core.Interfaces.Emails.Templates;
public interface IHtmlTemplate
{
    string AccountActivation(User user);
    string EnquiryThanks(Enquiry enquiry);
    string ResetPassword(User user);

}
