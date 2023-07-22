using IRestaurantReviewer_API.Domain.Models;
using IRestaurantReviewer_API.Domain.Repositories;
using IRestaurantReviewer_API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace IRestaurantReviewer_API.Persistence.Repositories
{
    public class RestaurantRepository : BaseRepository, IRestaurantRepository
    {
        public RestaurantRepository(ApplicationDbContext context)
            :base(context)
        {

        }

        public async Task CreateRestaurant(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
        }

        public async Task<Restaurant?> GetRestaurant(int id)
        {
            var photo = await _context.Restaurants
               .Include(x => x.Photos)
               .Where(x => x.Id == id).FirstOrDefaultAsync(x => x.Id == id);

            return (photo != null) ? photo : null;
        }


        public async Task<bool> IsExistRestaurant(Restaurant restaurant)
        {
            var result = await _context.Restaurants
                .FirstOrDefaultAsync(x => x.Name == restaurant.Name);

            if (result != null)
                return true;

            return false;           
        }

        public async Task<IEnumerable<Restaurant>> ListAsync()
        {
            return await _context.Restaurants
                .Include(x => x.Photos)
                .ToListAsync();
        }

        public Task<Restaurant> UpdateRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
    }
}
