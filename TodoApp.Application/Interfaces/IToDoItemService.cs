using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces
{
      public interface IToDoItemService
    {
        Task<Result<IEnumerable<ToDoItemDto>>> GetAllItemsAsync();
        Task<Result<ToDoItemDto>> GetItemByIdAsync(int id);
        Task<Result> AddItemAsync(ToDoItemDto dto);
        Task<Result> UpdateItemAsync(int id, ToDoItemDto dto);
        Task<Result> DeleteItemAsync(int id);
    }

}
