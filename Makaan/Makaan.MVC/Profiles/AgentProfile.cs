using AutoMapper;
using Makaan.MVC.Models;
using Makaan.MVC.ViewModels.AgentVM;

namespace Makaan.MVC.Profiles;

public class AgentProfile : Profile
{
    public AgentProfile()
    {
        CreateMap<AgentCreateVM,Agent>().ReverseMap();
        CreateMap<AgentUpdateVM,Agent>().ReverseMap();
    }
}
