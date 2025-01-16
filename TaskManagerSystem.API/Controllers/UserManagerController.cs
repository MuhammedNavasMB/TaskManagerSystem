using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.Application.DTOs;
using TaskManagerSystem.Domain.Entities;
using TaskManagerSystem.Domain.Interfaces;

namespace TaskManagerSystem.API.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class UserManagerController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserManagerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // Create User Endpoint
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserRegistrationDto createUserDto)
        {
            if (createUserDto == null)
            {
                return BadRequest("Invalid user data");
            }

            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = createUserDto.FirstName,
                    LastName = createUserDto.LastName,
                    Email = createUserDto.Email,
                    PhoneNumber = createUserDto.PhoneNumber,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password), // Example of password hashing
                    CreatedDate = DateTime.Now
                };

                // Save the user to the database
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating user: {ex.Message}");
            }
        }



        // Get User by Id Endpoint
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error fetching user: {ex.Message}");
            }
        }
    }
}
