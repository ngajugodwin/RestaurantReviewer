using AutoMapper;
using IRestaurantReviewer_API.Domain.Models;
using IRestaurantReviewer_API.Extensions;
using IRestaurantReviewer_API.Resources.Restaurant;

namespace IRestaurantReviewer_API.Mappings
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantResource>()
                 .ForMember(dest => dest.MainPhoto, opt =>
                 {
                     opt.MapFrom(src => src.Photos.GetMainPhoto());
                 })
                 .ForMember(dest => dest.Photos, opt =>
                 {
                     opt.MapFrom(src => src.Photos.GetRestaurantPhoto());
                 });

            CreateMap<RestaurantResource, Restaurant>();

            CreateMap<SaveRestaurantResource, Restaurant>();

            CreateMap<UpdateRestaurantResource, Restaurant>();
        }
    }
}
