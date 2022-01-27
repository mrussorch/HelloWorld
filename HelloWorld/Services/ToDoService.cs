using AutoMapper;
using HelloWorld.DTO;
using HelloWorld.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Services
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoDbContext _context;
        private readonly IMapper _mapper;

        public ToDoService(ToDoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ToDoDTO> AddOrUpdate(ToDoDTO toDoDTO)
        {
            ToDoModel item;
            if(string.IsNullOrEmpty(toDoDTO.Id))
            {
                try
                {
                    item = new ToDoModel(toDoDTO.Text);
                    _context.ToDos.Add(item);
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                item = await _context.ToDos.FindAsync(toDoDTO.Id);
                if (item is null) return null;
                item.Update(toDoDTO.Text);
            }
            if (!await SaveContext()) return null;
            var result = _mapper.Map<ToDoDTO>(item);
            return result;
            
        }

        public async Task<ToDoDTO> Complete(string id)
        {
            var item = await _context.ToDos.FindAsync(id);
            if (item is null) return null;
            item.Complete();

            if (!await SaveContext()) return null;
            var result = _mapper.Map<ToDoDTO>(item);
            return result;

        }

        public async Task<bool> Delete(string id)
        {

            var toDoModel = await _context.ToDos.FindAsync(id);
            if (toDoModel == null)
            {
                return false;
            }

            _context.ToDos.Remove(toDoModel); 
            return await SaveContext();
        }

        public async Task<ToDoDTO> Get(string id)
        {
            return _mapper.Map<ToDoDTO>(await _context.ToDos.FindAsync(id));
        }

        public async Task<IEnumerable<ToDoDTO>> List()
        {
            return _mapper.Map<IEnumerable<ToDoDTO>>(await _context.ToDos.ToListAsync());
        }

        private async Task<bool> SaveContext()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
           
        }
    }
}
