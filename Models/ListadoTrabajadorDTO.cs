using System.ComponentModel.DataAnnotations;

namespace TrabajadoresPrueba.Models
{
    public class ListadoTrabajadorDTO
    {
        public int Id { get; set; }

       
        public string? TipoDocumento { get; set; } = string.Empty;

       
        public string? NumeroDocumento { get; set; } = string.Empty;

        
        public string? Nombres { get; set; } = string.Empty;

   
        public string? Apellido { get; set; } = string.Empty;


        public string? Sexo { get; set; } = string.Empty;

        public string? Direccion { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public string? Foto { get; set; } 
    }
}
