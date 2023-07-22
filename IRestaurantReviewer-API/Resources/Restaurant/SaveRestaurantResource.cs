using IRestaurantReviewer_API.Domain.Models;

namespace IRestaurantReviewer_API.Resources.Restaurant
{
    public class SaveRestaurantResource
    {
        public string Name { get; set; } = string.Empty;

        public string PostCode { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public IEnumerable<IFormFile> Files { get; set; }

        // public virtual ICollection<Photo> Photos { get; set; }

        public SaveRestaurantResource()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
