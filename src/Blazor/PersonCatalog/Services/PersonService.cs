using PersonCatalog.Models;
using System.Net.Http.Json;

namespace PersonCatalog.Services
{
    public class PersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Person>> GetPersonsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Person>>("Persons");
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Person>($"Persons/{id}");
        }

        public async Task AddPersonAsync(Person person)
        {
            await _httpClient.PostAsJsonAsync("Persons", person);
        }

        public async Task UpdatePersonAsync(Person person)
        {
            await _httpClient.PutAsJsonAsync($"Persons/{person.Id}", person);
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Persons/{id}");
            return response.IsSuccessStatusCode;
        }       
    }
}
