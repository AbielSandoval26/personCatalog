using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;
        private string connectionString = "Server=localhost;Database=personcatalog;User=Abiel;Password=1234;Port=3306;";


        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetAllAsync()
        {
            return await _context.VistaPersonas.ToListAsync();

        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _context.VistaPersonas
              .Where(p => p.Id == id) 
              .FirstOrDefaultAsync();
        }

        public async Task<int> AddAsync(Person persona)
        {
            return await _context.Database.ExecuteSqlInterpolatedAsync($"CALL sp_AgregarPersona({persona.Nombre}, {persona.Apellido}, {persona.Email}, {persona.Direccion}, {persona.Telefono})");

        }

        public async Task<int> UpdateAsync(Person persona)
        {
            return await _context.Database.ExecuteSqlInterpolatedAsync($"CALL sp_ActualizarPersona({persona.Id}, {persona.Nombre}, {persona.Apellido}, {persona.Email}, {persona.Direccion}, {persona.Telefono})");
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _context.Database.ExecuteSqlInterpolatedAsync($"CALL sp_EliminarPersona({id})");

        }
    }
}
