using IRestaurantReviewer_API.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace IRestaurantReviewer_API.Domain.Services.Communications
{
    public class UserResponse : BaseResponse<User>
    {
        public IEnumerable<IdentityError> Errors { get; private set; }
        public string UploadMsg { get; set; }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="user">Saved user.</param>
        /// <returns>Response.</returns>
        public UserResponse(User user)
           : base(user)
        { }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="users">Saved users.</param>
        /// <returns>Response.</returns>
        public UserResponse(List<User> users, string msg)
           : base(users)
        {
            UploadMsg = msg;
        }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public UserResponse(string message, IEnumerable<IdentityError> errors)
           : base(message)
        {
            Errors = errors;
        }

        public UserResponse(string message)
           : base(message)
        { }
    }
}
