namespace IRestaurantReviewer_API.Domain.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string PostCode { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public Restaurant()
        {
            Photos = new HashSet<Photo>();
        }


    }
}
