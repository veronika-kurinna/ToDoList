using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Data.Repositories
{
    public class ToDoListItemRepository : IToDoListItemRepository
    {
        private readonly ToDoListItemContext _context;
        private readonly IToDoListItemMapper _mapper;
        
        public ToDoListItemRepository(ToDoListItemContext context, IToDoListItemMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToDoListItem>> Get()
        {
            IEnumerable<ToDoListItemEntity> items = await _context.ToDoListItems.ToListAsync();
            return items.Select(i => _mapper.MapToModel(i));
        }

        public async Task Create(ToDoListItem item)
        {
            ToDoListItemEntity itemEntity = _mapper.MapToEntity(item);
            _context.Add(itemEntity);
            await _context.SaveChangesAsync();
        }
    }
}
