using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrabajadoresPrueba.Models;

public partial class Trabajadores
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string? Nombres { get; set; }
    [Required(ErrorMessage = "El apellido es obligatorio")]
    public string? Apellido { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un tipo de documento")]
    public string? TipoDocumento { get; set; }

    [Required(ErrorMessage = "El número de documento es obligatorio")]
    [StringLength(15, ErrorMessage = "El número de documento no puede exceder 15 caracteres")]
    public string? NumeroDocumento { get; set; }

    public string? Sexo { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string? Direccion { get; set; }

    [StringLength(250)]
    public string? Foto { get; set; }
}
