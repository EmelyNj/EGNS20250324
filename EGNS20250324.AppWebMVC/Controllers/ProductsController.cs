using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EGNS20250324.AppWebMVC.Models;

namespace EGNS20250324.AppWebMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Test20250324DbContext _context;

        public ProductsController(Test20250324DbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(Product producto, int topRegistro = 10)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(producto.ProductName))
                query = query.Where(s => s.ProductName.Contains(producto.ProductName));
            if (!string.IsNullOrWhiteSpace(producto.Description))
                query = query.Where(s => s.Description.Contains(producto.Description));
            if (producto.BrandId > 0)
                query = query.Where(s => s.BrandId == producto.BrandId);
            if (producto.WarehouseId > 0)
                query = query.Where(s => s.WarehouseId == producto.WarehouseId);
            if (topRegistro > 0)
                query = query.Take(topRegistro);
            query = query
                .Include(p => p.Warehouse).Include(p => p.Brand);

            var marcas = _context.Brands.ToList();
            marcas.Add(new Brand { BrandName = "SELECCIONAR", Id = 0 });
            var bodegas = _context.Warehouses.ToList();
            bodegas.Add(new Warehouse { WarehouseName = "SELECCIONAR", Id = 0 });
            ViewData["WarehouseId"] = new SelectList(bodegas, "Id", "WarehouseName", 0);
            ViewData["BrandId"] = new SelectList(marcas, "Id", "BrandName", 0);

            return View(await query.ToListAsync());
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "BrandName");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Description,Price,PurchasePrice,WarehouseId,BrandId,Notes")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "BrandName", product.BrandId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseName", product.WarehouseId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "BrandName", product.BrandId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseName", product.WarehouseId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Description,Price,PurchasePrice,WarehouseId,BrandId,Notes")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "BrandName", product.BrandId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseName", product.WarehouseId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
