using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EGNS20250324.AppWebMVC.Models;

public partial class Product
{
    [Display(Name = "ID del Producto")]
    public int Id { get; set; }

    [Required(ErrorMessage = "*Este dato es obligatorio.")]
    [Display(Name = "Nombre del Producto")]
    public string ProductName { get; set; } = null!;

    [Display(Name = "Descripción del Producto")]
    public string? Description { get; set; }

    [Display(Name = "Precio del Producto")]
    public decimal Price { get; set; }

    [Display(Name = "Precio compra del Producto")]
    public decimal PurchasePrice { get; set; }

    [Display(Name = "ID de la Bodega")]
    public int? WarehouseId { get; set; }

    [Display(Name = "ID de la Marca")]
    public int? BrandId { get; set; }

    [Display(Name = "Notas adicionales")]
    public string? Notes { get; set; }

    [Display(Name = "Marcas")]
    public virtual Brand? Brand { get; set; }

    [Display(Name = "Bodegas")]
    public virtual Warehouse? Warehouse { get; set; }
}
