using AutoMapper;
using SalePortal.Models;

namespace SalePortal.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CommodityInputModel, CommodityEntity>();

            CreateMap<CommodityEntity, CommodityViewModel>();

            CreateMap<CommodityEntity, CommodityInputModel>();
            CreateMap<UserInputModel, UserEntity>();
            
        }
    }
}
