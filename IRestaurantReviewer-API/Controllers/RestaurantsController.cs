using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using IRestaurantReviewer_API.Domain.Models;
using IRestaurantReviewer_API.Domain.Repositories;
using IRestaurantReviewer_API.Domain.Services;
using IRestaurantReviewer_API.Extensions;
using IRestaurantReviewer_API.Helpers;
using IRestaurantReviewer_API.Persistence.Repositories;
using IRestaurantReviewer_API.Resources.Restaurant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IRestaurantReviewer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRestaurantService _restaurantService;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public RestaurantsController(IMapper mapper,
            IUnitOfWork unitOfWork,
            IRestaurantService restaurantService,
            IRestaurantRepository restaurantRepository,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _restaurantService = restaurantService;
            _restaurantRepository = restaurantRepository;
            _cloudinaryConfig = cloudinaryConfig;

            Account acct = new Account(_cloudinaryConfig.Value.CloudName, _cloudinaryConfig.Value.ApiKey, _cloudinaryConfig.Value.ApiSecret);
            _cloudinary = new Cloudinary(acct);
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetRestaurantAsync(int restaurantId)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);

            if (restaurant.Resource == null)
                return BadRequest("Restaurant not found!");

            var restaurantToReturn = _mapper.Map<RestaurantResource>(restaurant.Resource);

            return Ok(restaurantToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurantAsync([FromForm] SaveRestaurantResource saveRestaurantResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var restaurantToSave = _mapper.Map<SaveRestaurantResource, Restaurant>(saveRestaurantResource);

            var result = await _restaurantRepository.IsExistRestaurant(restaurantToSave);

            if (result)
                return BadRequest("A restaurant with the same name exist");

            ProcessPhotos(saveRestaurantResource.Files, restaurantToSave);

            try
            {
                await _restaurantRepository.CreateRestaurant(restaurantToSave);
                await _unitOfWork.CompleteAsync();
                var restaurantToReturn = _mapper.Map<Restaurant, RestaurantResource>(restaurantToSave);
                return Ok(restaurantToReturn);
            }
            catch (Exception ex)
            {
                throw;
            }
        }    

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var restaurants = await _restaurantService.ListAsync();

            var restaurantToReturn = _mapper.Map<IEnumerable<RestaurantResource>>(restaurants);

            return Ok(restaurantToReturn);
        }

        private Restaurant ProcessPhotos(IEnumerable<IFormFile> files, Restaurant restaurant)
        {
            var uploadResult = new ImageUploadResult();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.Name, stream),
                            Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face") // transform image to capture relevant areas
                        };
                        uploadResult = _cloudinary.Upload(uploadParams);
                        restaurant.Photos.Add(new Photo
                        {
                            Url = uploadResult.Url.ToString(),
                            PublicId = uploadResult.PublicId,
                        }); ;

                    }
                }
            }

            return restaurant;
        }



    }
}
