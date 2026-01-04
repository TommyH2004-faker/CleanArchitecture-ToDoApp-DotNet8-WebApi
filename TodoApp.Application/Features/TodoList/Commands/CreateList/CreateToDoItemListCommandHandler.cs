using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mappings;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.TodoList.Commands.CreateList
{
    public class CreateToDoListCommandHandler : IRequestHandler<CreateToDoItemListCommand, Result<ToDoListDto>>
    {
        private readonly IToDoListRepository _repository;
        private readonly IUserRepository _userRepository;
        public CreateToDoListCommandHandler(IToDoListRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        public async Task<Result<ToDoListDto>> Handle(CreateToDoItemListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.ToDoList.UserId);
                if (user == null)
                    return Result<ToDoListDto>.Failure("User not found");
                // Validation
                if (string.IsNullOrWhiteSpace(request.ToDoList.Title))
                    return Result<ToDoListDto>.Failure("Title is required");
                // kiem tra userId hop le
                if (request.ToDoList.UserId <= 0)
                    return Result<ToDoListDto>.Failure("Valid UserId is required");
                // Create entity
                var toDoList = ToDoList.Create(
                    request.ToDoList.UserId,
                    request.ToDoList.Title,
                    request.ToDoList.Description,
                    request.ToDoList.ToDoListId
                );

                await _repository.AddListAsync(toDoList);

                return Result<ToDoListDto>.Success(toDoList.ToDto());
            }
            catch (Exception ex)
            {
                return Result<ToDoListDto>.Failure($"Error: {ex.Message}");
            }
        }
    }
}