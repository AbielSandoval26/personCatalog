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
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, int>
    {
        private readonly IPersonRepository _repository;

        public UpdatePersonCommandHandler(IPersonRepository repository)
        {
            _repository = repository;            
        }

        public async Task<int> Handle (UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var persona = new Person
            {
                Id = request.Id,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                Direccion = request.Direccion,
                Telefono = request.Telefono
            };

            return await _repository.UpdateAsync(persona);
           
        }
    }
}
