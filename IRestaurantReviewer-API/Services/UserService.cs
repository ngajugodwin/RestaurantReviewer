using IRestaurantReviewer_API.Domain.Models.Identity;
using IRestaurantReviewer_API.Domain.Repositories;
using IRestaurantReviewer_API.Domain.Services;
using IRestaurantReviewer_API.Domain.Services.Communications;
using IRestaurantReviewer_API.Persistence.Repositories;
using IRestaurantReviewer_API.Services.Constants;
using Microsoft.AspNetCore.Identity;

namespace IRestaurantReviewer_API.Services
{
    public class UserService : IUserService
    {
        private readonly RoleManager<Domain.Models.Identity.Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationUserManager _applicationUserManager;

        public UserService(IUnitOfWork unitOfWork, RoleManager<Domain.Models.Identity.Role> roleManager,
                           SignInManager<User> signInManager,
                           ApplicationUserManager applicationUserManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _applicationUserManager = applicationUserManager;
        }

        public async Task<UserResponse> SaveAsync(User user, string password)
        {
            try
            {
                var result = await _applicationUserManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    var savedUser = await _applicationUserManager.FindByIdAsync(user.Id);

                    if (savedUser != null)
                        await _applicationUserManager.AddToRoleAsync(savedUser, RoleName.USER);                    

                    return new UserResponse(user);
                }

                return new UserResponse("Failed to create user", result.Errors);

            }
            catch (Exception ex)
            {
                // Do some logging
                return new UserResponse($"An error occurred while saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> GetUserByIdAsync(long id)
        {
            var user = await _applicationUserManager.FindByIdAsync(id);

            if (user == null)
                return new UserResponse("User not found");

            return new UserResponse(user);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _applicationUserManager.ListAsync();
        }
    }
}
