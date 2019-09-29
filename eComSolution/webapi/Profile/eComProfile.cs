using AutoMapper;
using webapi.Models;

namespace webapi.Mapper
{
    public class eComProfile : Profile
    {
        public eComProfile()
        {
            CreateMap<order.queryservices.Models.Product, ProductModel>()
               .ForMember(map => map.ProductId, des => des.MapFrom(src => src.Id))
               .ForMember(map => map.Catergory, des => des.MapFrom(src => src.Category))
               .ForMember(map => map.Description, des => des.MapFrom(src => src.Description))
               .ForMember(map => map.Manufaturer, des => des.MapFrom(src => src.Manufacturer))
               .ForMember(map => map.Name, des => des.MapFrom(src => src.Name))
               .ForMember(map => map.Rating, des => des.MapFrom(src => src.StarRating))
               .ForMember(map => map.StockQuantity, des => des.MapFrom(src => src.InStock))
               .ForMember(map => map.UnitPrice, des => des.MapFrom(src => src.UnitPrice));

            CreateMap<order.queryservices.Models.Cart, CartModel>()
               .ForMember(map => map.UserEmail, des => des.MapFrom(src => src.UserId))
               .ForMember(map => map.Price, des => des.MapFrom(src => src.Price))
               .ForMember(map => map.ProductId, des => des.MapFrom(src => src.ProductId))
               .ForMember(map => map.Quantity, des => des.MapFrom(src => src.Quantity))
               .ForMember(map => map.ProductName, des => des.MapFrom(src => src.Product.Name));

            CreateMap<CartModel, order.queryservices.Models.Cart>()
               .ForMember(map => map.UserId, des => des.MapFrom(src => src.UserEmail))
               .ForMember(map => map.ProductId, des => des.MapFrom(src => src.ProductId))
               .ForMember(map => map.Quantity, des => des.MapFrom(src => src.Quantity));
        }
    }
}
