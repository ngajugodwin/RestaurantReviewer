using IRestaurantReviewer_API.Domain.Models;

namespace IRestaurantReviewer_API.Domain.Services.Communications
{
    public class RestaurantResponse : BaseResponse<Restaurant>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="restaurant">Restaurant response.</param>
        /// <returns>Response.</returns>
        public RestaurantResponse(Restaurant restaurant) : base(restaurant)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public RestaurantResponse(string message) : base(message)
        { }

    }
}
