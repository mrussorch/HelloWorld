using AutoMapper;
using HelloWorld.DTO;
using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Mappers
{
    public class ToDoMapper : Profile
    {
        public ToDoMapper()
        {
            CreateMap<ToDoModel,ToDoDTO>();
        }

    }
}
