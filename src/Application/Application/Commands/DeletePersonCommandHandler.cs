using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, int>
    {
        private readonly IPersonRepository _repository;

        public DeletePersonCommandHandler(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle (DeletePersonCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.Id);
        }
    }
}
