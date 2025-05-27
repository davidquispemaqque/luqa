using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace luqa_backend.Models;

[Table("usuarios")] // <-- Esto asegura que EF lo relacione con tu tabla
public class Usuarios
{
    [Key]
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

    // Relación con Userprogress
    public virtual ICollection<Userprogress> Userprogress { get; set; } = new List<Userprogress>();
}