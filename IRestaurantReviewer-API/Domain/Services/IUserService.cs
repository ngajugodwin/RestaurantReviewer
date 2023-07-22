using IRestaurantReviewer_API.Domain.Models.Identity;
using IRestaurantReviewer_API.Domain.Services.Communications;

namespace IRestaurantReviewer_API.Domain.Services
{
    public interface IUserService
    {
        Task<UserResponse> SaveAsync(User user, string password);
        Task<UserResponse> GetUserByIdAsync(long id);
        Task<IEnumerable<User>> ListAsync();
    }
}
