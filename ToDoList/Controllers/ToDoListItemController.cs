﻿using Microsoft.AspNetCore.Mvc;
using ToDoList.Dtos;
using ToDoList.Dtos.Requests;
using ToDoList.Dtos.Responses;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoListItemController : ControllerBase
    {
        private readonly IToDoListItemMapper _mapper;
        private readonly IToDoListItemService _service;

        public ToDoListItemController(IToDoListItemMapper mapper, IToDoListItemService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<GetToDoListItemResponse> Get()
        {
            IEnumerable<ToDoListItem> items = await _service.GetAll();
            IEnumerable<ToDoListItemDto> itemsDto = items.Select(m => _mapper.MapToDto(m));
            GetToDoListItemResponse response = new GetToDoListItemResponse()
            {
                ToDoListItems = itemsDto
            };
            return response;
        }

        [HttpPost]
        public async Task Create([FromBody] CreateToDoListItemRequest request)
        {
            ToDoListItem item = _mapper.MapToModel(request);
            await _service.CreateItem(item);
        }
    }
}