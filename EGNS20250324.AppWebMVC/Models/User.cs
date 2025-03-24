using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EGNS20250324.AppWebMVC.Models;

public partial class User
{
    [Display(Name = "ID del Usuario")]
    public int Id { get; set; }

    [Required(ErrorMessage = "*Este dato es obligatorio.")]
    [Display(Name = "Nombre del Usuario")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "*Este dato es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene formato valido.")]
    [Display(Name = "Email del Usuario")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "*Este dato es obligatorio.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña del Usuario")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "*Este dato es obligatorio.")]
    [Display(Name = "Rol del Usuario")]
    public string Role { get; set; } = null!;

    [Display(Name = "Notas adicionales")]
    public string? Notes { get; set; }
}
