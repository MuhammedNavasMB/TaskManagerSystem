using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerSystem.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; } // Unique identifier for the task
        public string Title { get; set; } // Task title
        public string Description { get; set; } // Task description
        public bool IsCompleted { get; set; } // Completion status
        public DateTime StartDate { get; set; } // Due date for the task
        public DateTime DueDate { get; set; } // Due date for the task
        
        
        public Guid UserId { get; set; } // Foreign key to the User entity
        public User AssignedUser { get; set; } // Navigation property to the User entity

    }
}
