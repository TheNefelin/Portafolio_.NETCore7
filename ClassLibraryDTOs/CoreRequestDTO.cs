﻿using System.ComponentModel.DataAnnotations;

namespace ClassLibraryDTOs
{
    public class CoreRequestDTO<T>
    {
        [Required]
        public string Sql_Token { get; set; }
        [Required]
        public string Id_Usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public T? CoreData { get; set; } // Solo para operaciones de inserción/actualización
    }
}
