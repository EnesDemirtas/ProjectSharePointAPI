﻿using System.ComponentModel.DataAnnotations;

namespace PSP.Api.Contracts.UserProfile.Requests {
    public record UserProfileCreateUpdate {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}