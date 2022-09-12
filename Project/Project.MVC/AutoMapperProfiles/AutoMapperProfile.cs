using AutoMapper;
using Project.MVC.Models;
using Project.Service.Models;

namespace Project.MVC.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        { 
        CreateMap<VehicleModel, VehicleModelDTO>().ReverseMap();
        CreateMap<VehicleMake, VehicleMakeDTO>().ReverseMap();
        }
    }
}
