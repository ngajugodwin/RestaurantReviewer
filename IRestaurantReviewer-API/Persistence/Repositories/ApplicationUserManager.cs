using IRestaurantReviewer_API.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IRestaurantReviewer_API.Persistence.Repositories
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, 
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public override Task<User?> FindByEmailAsync(string email)
        {
            return base.FindByEmailAsync(email);
        }

        public async Task<User?> FindByIdAsync(long userId)
        {
            var query = base.Users.AsQueryable();

            var user = await query.Include(ur => ur.UserRoles).ThenInclude(r => r.Role)
                    .FirstOrDefaultAsync(u => u.Id == userId);

            return (user != null) ? user : null;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            var users = await base.Users
                .Include(ur => ur.UserRoles)
                .ThenInclude(r => r.Role)
                .ToListAsync();

            return users;
        }
    }
}
