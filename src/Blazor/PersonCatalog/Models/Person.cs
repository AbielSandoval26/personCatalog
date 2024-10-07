using System.ComponentModel.DataAnnotations;

namespace PersonCatalog.Models
{
    public class Person
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string? Nombre { get; set; }
        
        [Required(ErrorMessage = "El apellido es requerido.")]
        public string? Apellido { get; set; }
        
        [Required(ErrorMessage = "El email es requerido.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La dirección es requerida.")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido.")]
        public string? Telefono { get; set; }
    }
}
