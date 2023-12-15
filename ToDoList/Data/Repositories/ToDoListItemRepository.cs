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
            return items.Select(item => _mapper.MapToModel(item));
        }

        public async Task<ToDoListItem> GetById(int id)
        {
            ToDoListItemEntity? itemEntity = await _context.ToDoListItems.Where(i => i.Id == id)
                                                                         .FirstOrDefaultAsync();
            if (itemEntity == null)
            {
                throw new ArgumentException($"Item with id {id} doesn't exist");
            }
            return _mapper.MapToModel(itemEntity);
        }

        public async Task Create(ToDoListItem item)
        {
            ToDoListItemEntity itemEntity = _mapper.MapToEntity(item);
            _context.Add(itemEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ToDoListItem item)
        {
            ToDoListItemEntity? itemToUpdate = await _context.ToDoListItems.Where(i => i.Id == item.Id)
                                                                           .FirstOrDefaultAsync();
            if (itemToUpdate == null)
            {
                throw new ArgumentException("This item doesn't exist");
            }
           
            if (item.Name != null)
            {
                itemToUpdate.Name = item.Name;
            }

            if (item.Status != null)
            {
                itemToUpdate.Status = (ToDoItemStatuses)item.Status;
            }

            _context.SaveChanges();
        }
    }
}
