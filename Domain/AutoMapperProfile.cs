using AutoMapper;
using SalePortal.Entities;
using SalePortal.Models;

namespace SalePortal.Domain
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
