using System.ComponentModel.DataAnnotations;

namespace TrabajadoresPrueba.Models
{
    public class ListadoTrabajadorDTO
    {
        public int Id { get; set; }
        [Required]
        public string TipoDocumento { get; set; } = string.Empty;
        [Required]
        public string NumeroDocumento { get; set; } = string.Empty;
        [Required]
        public string Nombres { get; set; } = string.Empty;
        [Required]
        public string Sexo { get; set; } = string.Empty;
        [Required]
        public string Departamento { get; set; } = string.Empty;
        [Required]
        public string Provincia { get; set; } = string.Empty;
        [Required]
        public string Distrito { get; set; } = string.Empty;
    }
}
