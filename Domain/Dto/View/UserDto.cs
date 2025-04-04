﻿namespace Domain.Dto.View
{
    public class UserDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Surname { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
