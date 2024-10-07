using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeletePersonCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeletePersonCommand(int id)
        {
            Id = id;
        }
    }
}
