using Microsoft.AspNetCore.Mvc;
using ToDoList.Dtos;
using ToDoList.Dtos.Requests;
using ToDoList.Dtos.Responses;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoListItemsController : ControllerBase
    {
        private readonly IToDoListItemMapper _mapper;
        private readonly IToDoListItemService _service;

        public ToDoListItemsController(IToDoListItemMapper mapper, IToDoListItemService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<GetToDoListResponse> Get()
        {
            IEnumerable<ToDoListItem> items = await _service.GetAll();
            IEnumerable<ToDoListItemDto> itemsDto = items.Select(m => _mapper.MapToDto(m));
            GetToDoListResponse response = new GetToDoListResponse()
            {
                ToDoListItems = itemsDto
            };
            return response;
        }

        [HttpPost]
        public async Task<int> Create([FromBody] CreateToDoListItemRequest request)
        {
            ToDoListItem item = _mapper.MapToModel(request);
            return await _service.CreateItem(item);
        }
    }
}
