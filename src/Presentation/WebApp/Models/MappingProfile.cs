using ExpressCargo.WebApp.Models.Dtos.Users;
using ExpressCargo.Core.Models.Entities.Users;
using ExpressCargo.Core.Models.Entities.Enquiries;
using ExpressCargo.WebApp.Models.Dtos.Enquiries;

namespace ExpressCargo.WebApp.Models;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Enquiry,              EnquiryDto>()
            .ReverseMap();

        CreateMap<User,                 UserDto>()
            .ReverseMap();
    }
}
