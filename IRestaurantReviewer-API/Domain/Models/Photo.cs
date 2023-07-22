namespace IRestaurantReviewer_API.Domain.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public virtual Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
    }
}
