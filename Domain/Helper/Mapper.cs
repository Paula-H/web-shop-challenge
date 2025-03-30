using AutoMapper;
using Domain.Dto.Create;
using Domain.Dto.View;
using Domain.Entity;

namespace Domain.Helper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CreateCouponDto, Coupon>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>();

            CreateMap<CouponDto, Coupon>();
            CreateMap<Coupon, CouponDto>();

            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
