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
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListMapper _mapper;
        private readonly IToDoListService _service;

        public ToDoListController(IToDoListMapper mapper, IToDoListService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<GetToDoListResponse> Get()
        {
            IEnumerable<ToDoListModel> toDoLists = await _service.GetAll();
            IEnumerable<ToDoListDto> toDoListDtos = toDoLists.Select(m => _mapper.MapDto(m));
            GetToDoListResponse response = new GetToDoListResponse()
            {
                ToDoLists = toDoListDtos
            };
            return response;
        }

        [HttpPost]
        public async Task<int> Create([FromBody] CreateToDoListRequest request)
        {
            ToDoListModel toDoList = _mapper.Map(request);
            return await _service.CreateTask(toDoList);
        }
    }
}
