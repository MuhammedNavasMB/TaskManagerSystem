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
    public class TaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskItem> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            // Retrieve the user from the database using UserId
            var user = await _unitOfWork.UserRepository.GetByIdAsync(createTaskDto.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                DueDate = createTaskDto.DueDate,
                UserId = createTaskDto.UserId,
                AssignedUser = user,
                IsCompleted = false
            };

            // Save the task to the database
            await _unitOfWork.TaskRepository.AddAsync(task);
            await _unitOfWork.CompleteAsync();

            return task;
        }
    }
}