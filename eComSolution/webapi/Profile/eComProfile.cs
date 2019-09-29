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
               .ForMember(map => map.CartId, des => des.MapFrom(src => src.Id))
               .ForMember(map => map.UserEmail, des => des.MapFrom(src => src.UserId))
               .ForMember(map => map.Price, des => des.MapFrom(src => src.Price))
               .ForMember(map => map.ProductId, des => des.MapFrom(src => src.ProductId))
               .ForMember(map => map.Quantity, des => des.MapFrom(src => src.Quantity))
               .ForMember(map => map.ProductName, des => des.MapFrom(src => src.Product.Name));

            CreateMap<CartModel, order.queryservices.Models.Cart>()
               .ForMember(map => map.UserId, des => des.MapFrom(src => src.UserEmail))
               .ForMember(map => map.ProductId, des => des.MapFrom(src => src.ProductId))
               .ForMember(map => map.Quantity, des => des.MapFrom(src => src.Quantity));

            CreateMap<CartModel, order.commandservice.Models.Cart>()
             .ForMember(map => map.UserId, des => des.MapFrom(src => src.UserEmail))
             .ForMember(map => map.ProductId, des => des.MapFrom(src => src.ProductId))
             .ForMember(map => map.Quantity, des => des.MapFrom(src => src.Quantity));

            CreateMap<OrderModel, order.commandservice.Models.Order>()
             .ForMember(map => map.UserId, des => des.MapFrom(src => src.UserId))
             .ForMember(map => map.DeliveryCost, des => des.MapFrom(src => src.DeliveryCost))
             .ForMember(map => map.OrderNumber, des => des.MapFrom(src => src.OrderNumber))
             .ForMember(map => map.Status, des => des.MapFrom(src => src.Status))
             .ForMember(map => map.TotalItemCost, des => des.MapFrom(src => src.TotalPrice));

            CreateMap<order.queryservices.Models.Order, OrderModel>()
             .ForMember(map => map.TotalPrice, des => des.MapFrom(src => src.TotalItemCost));

        }
    }
}
