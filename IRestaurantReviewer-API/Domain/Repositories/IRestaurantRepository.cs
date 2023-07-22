using IRestaurantReviewer_API.Domain.Models;

namespace IRestaurantReviewer_API.Domain.Repositories
{
    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetRestaurant(int id);
        Task<Restaurant> UpdateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);

        Task CreateRestaurant(Restaurant restaurant);

        Task<IEnumerable<Restaurant>> ListAsync();

        Task<bool> IsExistRestaurant(Restaurant restaurant);
    }
}
