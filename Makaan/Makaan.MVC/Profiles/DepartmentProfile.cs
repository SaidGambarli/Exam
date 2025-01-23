using AutoMapper;
using Makaan.MVC.Models;
using Makaan.MVC.ViewModels.AgentVM;
using Makaan.MVC.ViewModels.DepartmentVM;

namespace Makaan.MVC.Profiles;

public class DepartmentProfile: Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentCreateVM, Department>().ReverseMap();
        CreateMap<DepartmentUpdateVM, Department>().ReverseMap();
    }
}
