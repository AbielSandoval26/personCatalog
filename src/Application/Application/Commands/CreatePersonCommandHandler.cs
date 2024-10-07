using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
    {
        private readonly IPersonRepository _repository;
        public CreatePersonCommandHandler(IPersonRepository repository)
        {
            _repository= repository; 
        }
        public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var persona = new Person
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                Direccion = request.Direccion,
                Telefono = request.Telefono
            };

            await _repository.AddAsync(persona);
            return persona.Id;

        }

    }
}
