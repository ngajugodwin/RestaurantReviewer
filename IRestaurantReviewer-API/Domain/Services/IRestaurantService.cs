using IRestaurantReviewer_API.Domain.Models;
using IRestaurantReviewer_API.Domain.Services.Communications;

namespace IRestaurantReviewer_API.Domain.Services
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurant>> ListAsync();

        Task<RestaurantResponse> GetRestaurantByIdAsync(int restaurantId);

        Task<RestaurantResponse> SaveAsync(Restaurant restaurant);

        Task<RestaurantResponse> UpdateAsync(int restaurantId, Restaurant restaurant);
    }
}
