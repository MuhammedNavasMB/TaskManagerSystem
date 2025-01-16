using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Application.DTOs;
using TaskManagerSystem.Domain.Entities;
using TaskManagerSystem.Domain.Interfaces;


namespace TaskManagerSystem.Application.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork; // Interface for database operations

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
        {
            // Validate data (for simplicity, skipping validation logic here)

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = userRegistrationDto.FirstName,
                LastName = userRegistrationDto.LastName,
                Email = userRegistrationDto.Email,
                PhoneNumber = userRegistrationDto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegistrationDto.Password), // Hashed password
                CreatedDate = DateTime.UtcNow
            };

            // Save the user to the database using a UnitOfWork or repository pattern
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync(); // Commit the transaction

            return user;
        }
    }
}

