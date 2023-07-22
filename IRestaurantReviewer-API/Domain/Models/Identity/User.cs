﻿using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace IRestaurantReviewer_API.Domain.Models.Identity
{
    public class User : IdentityUser<long>
    {
        public string FullName { get; set; }

        public override string Email { get; set; }

        public override string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }

        public string? UserPhotoUrl { get; set; }
        public string? PublicId { get; set; }

        public User()
        {
            Tokens = new HashSet<Token>();
            UserRoles = new HashSet<UserRole>();
            CreatedAt = DateTime.Now;
        }
    }
}
