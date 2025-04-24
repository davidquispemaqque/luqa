using System;
using System.ComponentModel.DataAnnotations;

namespace luqa_backend.Models;

public class Usuario
{
    public int UsuarioID { get; set; }

    [Required]
    [StringLength(100)]
    public string NombreCompleto { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string CorreoElectronico { get; set; }

    [Required]
    [StringLength(100)]
    public string Contraseña { get; set; }

    public DateTime FechaRegistro { get; set; }
}