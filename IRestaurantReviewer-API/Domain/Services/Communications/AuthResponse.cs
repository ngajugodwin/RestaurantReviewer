using IRestaurantReviewer_API.Resources.Auth;
using IRestaurantReviewer_API.Resources.User;
using Microsoft.Identity.Client;

namespace IRestaurantReviewer_API.Domain.Services.Communications
{
    public class AuthResponse : BaseResponse<LoginResource>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="user">User response.</param>
        /// <returns>Response.</returns>
        public AuthResponse(LoginResource loginResource) : base(loginResource)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public AuthResponse(string message) : base(message)
        { }
    }
}
