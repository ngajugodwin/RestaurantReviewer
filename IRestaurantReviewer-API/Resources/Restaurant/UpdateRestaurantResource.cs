using IRestaurantReviewer_API.Domain.Models;

namespace IRestaurantReviewer_API.Resources.Restaurant
{
    public class UpdateRestaurantResource
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string PostCode { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
    }
}
