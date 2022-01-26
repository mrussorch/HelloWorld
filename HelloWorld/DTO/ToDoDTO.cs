using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.DTO
{
    public class ToDoDTO
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Completed { get; set; }
        public bool IsOpen => !Completed; 
    }
}
