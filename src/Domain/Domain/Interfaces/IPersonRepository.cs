using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync();
        Task<Person> GetByIdAsync(int id);
        Task<int> AddAsync(Person persona);
        Task<int> UpdateAsync(Person persona);
        Task<int> DeleteAsync(int id);
    }
}
