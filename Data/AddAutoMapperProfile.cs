using AutoMapper;
using SalePortal.Models;

namespace SalePortal.Data
{
    public class AddAutoMapperProfile : Profile
    {
        public AddAutoMapperProfile()
        {
            CreateMap<CommodityInputModel, CommodityEntity>();

            CreateMap<CommodityEntity, CommodityViewModel>();
        }
    }
}
