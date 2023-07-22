using System.ComponentModel.DataAnnotations;

namespace IRestaurantReviewer_API.Resources.User
{
    public class SaveUserResource
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public SaveUserResource()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
