﻿using System.ComponentModel.DataAnnotations;

namespace ClassLibraryApplication.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        //[SwaggerSchema(Description = "Contraseña", Format = "password")]
        public string Password1 { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password2 { get; set; } = string.Empty;
    }
}