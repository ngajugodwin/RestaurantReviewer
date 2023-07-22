using IRestaurantReviewer_API.Domain.Models;

namespace IRestaurantReviewer_API.Extensions
{
    public static class EntityExtension
    {
        public static IEnumerable<string> GetRestaurantPhoto(this IEnumerable<Photo> photos)
        {
            var photoUrl = new List<string>();

            if (photos.Count() > 0)
            {
                foreach (var item in photos.Skip(1))
                {
                    photoUrl.Add(item.Url);
                }

                return photoUrl;
            }
              

            return photoUrl;

        }

        public static string GetMainPhoto(this IEnumerable<Photo> photos)
        {
            if(photos.Count() > 0)
            {
                return photos.First().Url;
            }

            return String.Empty;
        }
    }
}
