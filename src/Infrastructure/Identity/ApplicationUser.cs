using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace NanoCell.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(255)]
        public string Surname { get; set; }

        public string AvatarURL { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
