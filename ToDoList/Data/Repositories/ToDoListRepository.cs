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
            IEnumerable<ToDoListEntity> toDoListEntities = await _context.ToDoLists.ToListAsync();
            return toDoListEntities.Select(m => _mapper.Map(m));
        }

        public async Task<int> Create(ToDoListModel toDoList)
        {
            ToDoListEntity toDoListEntity = _mapper.Map(toDoList);
            _context.Add(toDoListEntity);
            await _context.SaveChangesAsync();

            return toDoListEntity.Id;
        }
    }
}
