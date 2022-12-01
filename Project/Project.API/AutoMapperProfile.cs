using AutoMapper;
using Project.Common;
using Project.Model.DatabaseModels;

namespace Project.WebAPI
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
