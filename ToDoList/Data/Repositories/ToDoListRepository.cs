using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Data.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoListContext _context;
        private readonly IToDoListMapper _mapper;
        
        public ToDoListRepository(ToDoListContext context, IToDoListMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToDoListModel>> Get()
        {
            IEnumerable<ToDoListItemEntity> toDoListEntities = await _context.ToDoLists.ToListAsync();
            return toDoListEntities.Select(m => _mapper.MapToModel(m));
        }

        public async Task<int> Create(ToDoListModel toDoList)
        {
            ToDoListItemEntity toDoListEntity = _mapper.MapToEntity(toDoList);
            _context.Add(toDoListEntity);
            await _context.SaveChangesAsync();

            return toDoListEntity.Id;
        }
    }
}
