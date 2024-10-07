using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreatePersonCommand : IRequest<int>
    {
     
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        public CreatePersonCommand()
        {
        }
    }
    
}
