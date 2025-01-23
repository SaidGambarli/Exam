using AutoMapper;
using Makaan.MVC.Models;
using Makaan.MVC.ViewModels.AuthVM;

namespace Makaan.MVC.Profiles;

public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<LoginVM,AppUser>().ReverseMap();
        CreateMap<RegisterVM,AppUser>().ReverseMap();
    }
}
