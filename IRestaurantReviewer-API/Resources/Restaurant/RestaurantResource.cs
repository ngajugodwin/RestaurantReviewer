namespace IRestaurantReviewer_API.Resources.Restaurant
{
    public class RestaurantResource
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PostCode { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string MainPhoto { get; set; } = string.Empty;

        public IEnumerable<string> Photos { get; set; }
    }
}
