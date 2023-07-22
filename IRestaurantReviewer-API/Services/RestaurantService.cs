using IRestaurantReviewer_API.Domain.Models;
using IRestaurantReviewer_API.Domain.Repositories;
using IRestaurantReviewer_API.Domain.Services;
using IRestaurantReviewer_API.Domain.Services.Communications;

namespace IRestaurantReviewer_API.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IUnitOfWork unitOfWork, IRestaurantRepository restaurantRepository)
        {
            _unitOfWork = unitOfWork;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<RestaurantResponse> GetRestaurantByIdAsync(int restaurantId)
        {
            var restaurantFromRepo = await _restaurantRepository.GetRestaurant(restaurantId);

            if (restaurantFromRepo == null)
                return new RestaurantResponse("Restaurant not found");

            return new RestaurantResponse(restaurantFromRepo);
        }

        public async Task<IEnumerable<Restaurant>> ListAsync()
        {
            return await _restaurantRepository.ListAsync();
        }

        public async Task<RestaurantResponse> SaveAsync(Restaurant restaurant)
        {
            var existingRestaurant = await _restaurantRepository.IsExistRestaurant(restaurant);

            if (existingRestaurant)
            {
                return new RestaurantResponse("Similar restaurant exisit");
            }

            await _restaurantRepository.CreateRestaurant(restaurant);
            await _unitOfWork.CompleteAsync();

            return new RestaurantResponse(restaurant);
        }

        public async Task<RestaurantResponse> UpdateAsync(int restaurantId, Restaurant restaurant)
        {
            var restaurantFromRepo = await _restaurantRepository.GetRestaurant(restaurantId);

            if (restaurantFromRepo == null)
                return new RestaurantResponse("Restaurant not found");

            var isExist = await _restaurantRepository.IsExistRestaurant(restaurant);

            if (isExist)
                return new RestaurantResponse("Restaurant already exist");

            restaurantFromRepo.Name = restaurant.Name;

            try
            {  
                await _unitOfWork.CompleteAsync();
                return new RestaurantResponse(restaurantFromRepo);
            }
            catch (Exception ex) 
            { 
                return new RestaurantResponse($"An error occured when updating restaurant: {ex.Message}");
            }
        }
    }
}
