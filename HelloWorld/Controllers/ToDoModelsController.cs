        using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Models;
using HelloWorld.DTO;
using AutoMapper;
using HelloWorld.Services;

namespace HelloWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoModelsController : ControllerBase
    {
        private readonly IToDoService _service;

        public ToDoModelsController(IToDoService service)
        {
            _service = service;
        }

        // GET: api/ToDoModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoModel>>> GetToDos()
        {
            return Ok(await _service.List());
        }

        // GET: api/ToDoModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoModel>> GetToDoModel(string id)
        {

            var result = await _service.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/ToDoModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> CompleteToDoModel(string id)
        {
            var result = await _service.Complete(id);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        // POST: api/ToDoModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoDTO>> PostToDoModel(ToDoDTO toDoDTO)
        {       
            var result = await _service.AddOrUpdate(toDoDTO);
            if(result is null) return BadRequest();
            return Ok(result);
        }

        // DELETE: api/ToDoModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoModel(string id)
        {
            var result = await _service.Delete(id);
            if (!result) return BadRequest();
            return Ok(result);
        }
    }
}
