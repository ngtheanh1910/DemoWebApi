using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repository;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IMapper _mapper;

        public TodoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            TodoRepository TodoRepo = new TodoRepository();
            var lstTodo = TodoRepo.GetAllTodo();
            var lstTodoDTO = _mapper.Map<List<TodoDTO>>(lstTodo);
            return Ok(lstTodoDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            TodoRepository TodoRepo = new TodoRepository();
            var todo = TodoRepo.GetById(id);
            var todoDTO = _mapper.Map<TodoDTO>(todo);
            return Ok(todoDTO);
        }

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
           
            TodoRepository TodoRepo = new TodoRepository();

            if (TodoRepo.AddTodo(todo))
            {
                return Ok(new { Success = true, Data = todo});
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Todo todo)
        {
            TodoRepository TodoRepo = new TodoRepository();           

            if (GetById(id) != null)
            {
                if (TodoRepo.UpdateTodo(todo))
                {
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TodoRepository TodoRepo = new TodoRepository();
            if (GetById(id) != null)
            {
                if (TodoRepo.DeleteTodo(id))
                {
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
