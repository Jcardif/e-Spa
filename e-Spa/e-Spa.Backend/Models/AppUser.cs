﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace e_Spa.Backend.Models
{
    /// <inheritdoc />
    public class AppUser : IdentityUser<int>
    {
        /// <summary>
        /// First Name of the User
        /// </summary>
        [Required, Column(TypeName = "VARCHAR(64)")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of the user
        /// </summary>
        [Required, Column(TypeName = "VARCHAR(64)")]
        public string LastName { get; set; }

        /// <summary>
        /// Url of the Profile photo
        /// </summary>
        [Required, Column(TypeName = "VARCHAR(64)")]
        public string ProfilePhotoUrl { get; set; }
    }
}
