using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Domain.Entities;

namespace TaskManagerSystem.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem task);
    }
}
