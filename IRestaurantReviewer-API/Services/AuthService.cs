using AutoMapper;
using IRestaurantReviewer_API.Domain.Models;
using IRestaurantReviewer_API.Domain.Models.Identity;
using IRestaurantReviewer_API.Domain.Repositories;
using IRestaurantReviewer_API.Domain.Services;
using IRestaurantReviewer_API.Domain.Services.Communications;
using IRestaurantReviewer_API.Helpers;
using IRestaurantReviewer_API.Persistence.Repositories;
using IRestaurantReviewer_API.Resources.Auth;
using IRestaurantReviewer_API.Resources.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace IRestaurantReviewer_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;

        public AuthService(ApplicationUserManager applicationUserManager,
                           SignInManager<User> signInManager,
                           IMapper mapper,
                           IOptions<AppSettings> appSettings,
                           IGenericRepository genericRepository,
                           IUnitOfWork unitOfWork)
        {
            _applicationUserManager = applicationUserManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            var user = await _applicationUserManager.FindByEmailAsync(email);

            if (user == null)
                return new AuthResponse("User not found");

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                var newRefreshToken = CreateRefreshToken(_appSettings.ClientId, user.Id);

                var oldUserTokenFromRepo = await _genericRepository.ListAsync<Token>(t => t.UserId == user.Id);

                if (oldUserTokenFromRepo.Count() > 0)
                {
                    foreach (var token in oldUserTokenFromRepo)
                    {
                        _genericRepository.Remove<Token>(token);
                    }
                }

                await _genericRepository.AddAsync<Token>(newRefreshToken);
                await _unitOfWork.CompleteAsync();

                return new AuthResponse(
                        new LoginResource
                        {
                            User = _mapper.Map<UserResource>(user)
                        });
            }

            return new AuthResponse("Invalid email or password");
        }

        private Token CreateRefreshToken(string clientId, long userId)
        {
            return new Token
            {
                ClientId = clientId,
                UserId = userId,
                Value = Guid.NewGuid().ToString("N"),
                CreatedAt = DateTime.Now,
                ExpiryTime = DateTime.Now.AddMinutes(86400) //60days
            };
        }
    }
}
