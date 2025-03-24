using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EGNS20250324.AppWebMVC.Models;

public partial class Warehouse
{
    [Display(Name = "ID de la Bodega")]
    public int Id { get; set; }

    [Required(ErrorMessage = "*Este dato es obligatorio.")]
    [Display(Name = "Nombre de la Bodega")]
    public string WarehouseName { get; set; } = null!;

    [Display(Name = "Notas adicionales")]
    public string? Notes { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
