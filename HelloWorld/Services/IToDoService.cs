using HelloWorld.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Services
{
    public interface IToDoService
    {
        Task<IEnumerable<ToDoDTO>> List();
        Task<ToDoDTO> Get(string id);
        Task<ToDoDTO> AddOrUpdate(ToDoDTO toDoDTO);
        Task<bool> Delete(string id);
        Task<ToDoDTO> Complete(string id);
    }
}
