using IRestaurantReviewer_API.Domain.Services.Communications;

namespace IRestaurantReviewer_API.Domain.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(string email, string password);
    }
}
