﻿namespace Cashcontrol.API.Models.Dtos.User
{
    public class UserResponseDto : ResponseBase
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
