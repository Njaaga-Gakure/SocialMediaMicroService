﻿using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.DTOs
{
    public class ResponseUserDTO
    {
        public Guid Id { get; set; }    
        public string FirstName { get; set; } = string.Empty;
      
        public string LastName { get; set; } = string.Empty;
      
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
