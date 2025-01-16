using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerSystem.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } // Unique identifier for the user
        public string FirstName { get; set; } // User's first name
        public string LastName { get; set; } // User's last name
        public string Email { get; set; } // User's email address
        public string PhoneNumber { get; set; } // Hashed password for security
        public string PasswordHash { get; set; } // Hashed password for security
        public DateTime CreatedDate { get; set; } // Account creation date

        // Navigation property for tasks assigned to this user
        public ICollection<TaskItem> Tasks { get; set; }
    }
}
