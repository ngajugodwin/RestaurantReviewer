using IRestaurantReviewer_API.Persistence.Contexts;

namespace IRestaurantReviewer_API.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
