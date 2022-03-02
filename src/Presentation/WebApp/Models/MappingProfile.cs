using CleanArchitecture.WebApp.Models.Dtos.Users;
using CleanArchitecture.Core.Models.Entities.Users;
using CleanArchitecture.Core.Models.Entities.Enquiries;
using CleanArchitecture.WebApp.Models.Dtos.Enquiries;

namespace CleanArchitecture.WebApp.Models;
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
