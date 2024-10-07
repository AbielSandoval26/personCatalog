using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, List<Person>>
    {
        private readonly IPersonRepository _repository;

        public GetAllPersonsQueryHandler(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Person>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
