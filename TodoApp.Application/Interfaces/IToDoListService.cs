using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Common;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces
{
    public interface IToDoListService
    {
        Task<Result<IEnumerable<ToDoList>>> GetAllListsAsync();
        Task<Result<IEnumerable<ToDoList>>> GetListsByUserIdAsync(int userId);
        Task<Result<ToDoList>> GetListByIdAsync(int id);
        Task<Result> AddListAsync(ToDoList list);
        Task<Result> UpdateListAsync(ToDoList list);
        Task<Result> DeleteListAsync(int id);
    }

}
