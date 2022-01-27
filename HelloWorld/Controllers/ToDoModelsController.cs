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
        private readonly ToDoDbContext _context;
        private readonly IMapper _mapper;
        private readonly IToDoService _service;

        public ToDoModelsController(ToDoDbContext context, IMapper mapper, IToDoService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        // GET: api/ToDoModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoModel>>> GetToDos()
        {
            return await _context.ToDos.ToListAsync();
        }

        // GET: api/ToDoModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoModel>> GetToDoModel(string id)
        {
            var toDoModel = await _context.ToDos.FindAsync(id);

            if (toDoModel == null)
            {
                return NotFound();
            }

            return toDoModel;
        }

        // PUT: api/ToDoModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoModel(string id, ToDoModel toDoModel)
        {
            if (id != toDoModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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
            if (result == false) return BadRequest();
            return Ok(result);
        }

        private bool ToDoModelExists(string id)
        {
            return _context.ToDos.Any(e => e.Id == id);
        }
    }
}
