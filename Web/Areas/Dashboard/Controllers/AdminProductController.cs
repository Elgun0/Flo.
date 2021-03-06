using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Entities;
using Microsoft.AspNetCore.Http;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AdminProductController : Controller
    {
        private readonly EcommerceContext _context;

        public AdminProductController(EcommerceContext context)
        {
            _context = context;
        }

        // GET: Dashboard/AdminProduct
        public async Task<IActionResult> Index()
        {
            var ecommerceContext = _context.Products.Include(p => p.Category);
            return View(await ecommerceContext.ToListAsync());
        }

        // GET: Dashboard/AdminProduct/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Dashboard/AdminProduct/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name");
            return View();
        }

        // POST: Dashboard/AdminProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProductActionViewModel Model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    Name = Model.Name,
                    CategoryID = Model.CategoryID,
                    Price = Model.Price,
                    Discount = Model.Discount,
                    SKU = Model.SKU,
                    Barcode = Model.Barcode,
                    Tags = Model.Tags,
                    Supplier = Model.Supplier,
                    isFeatured = Model.isFeatured,
                    ModifiedOn = DateTime.Now
                };
                if (!string.IsNullOrEmpty(Model.ProductPictures))
                {
                    var pictureIDs = Model.ProductPictures.Split(",").Select(x => int.Parse(x)).ToList();
                    if (pictureIDs.Count > 0)
                    {
                        product.ProductPictures = new List<ProductPicture>();
                        product.ProductPictures.AddRange(pictureIDs
                            .Select(x => new ProductPicture() { PictureID = x , ProductID = product.ID}).ToList());
                        product.ThumbnailPictureID = Model.ThumbnailPicture != 0 ? Model.ThumbnailPicture : pictureIDs.First();
                    }
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", Model.CategoryID);
            return View();
        }


        //public async Task<IActionResult> Create(IFormCollection form)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        ProductActionViewModel myModel = GetProductActionViewModelFromForm(form);
        //        Product product = new Product()
        //        {
        //            Name = myModel.Name,
        //            CategoryID = myModel.CategoryID,
        //            Price = myModel.Price,
        //            Discount = myModel.Discount,
        //            SKU = myModel.SKU,
        //            Barcode = myModel.Barcode,
        //            Tags = myModel.Tags,
        //            Supplier = myModel.Supplier,
        //            isFeatured = myModel.isFeatured,
        //            ModifiedOn = DateTime.Now
        //        };
        //        if (!string.IsNullOrEmpty(myModel.ProductPictures))
        //        {
        //            var pictureIDs = myModel.ProductPictures
        //                .Split(',')
        //                .Select(ID => int.Parse(ID)).ToList();
        //            if (pictureIDs.Count > 0)
        //            {
        //                product.ProductPictures = new List<ProductPicture>();
        //                product.ProductPictures.AddRange(pictureIDs
        //                    .Select(x => new ProductPicture() { ProductID = product.ID, PictureID = x }).ToList());
        //                product.ThumbnailPictureID = myModel.ThumbnailPicture != 0 ? myModel.ThumbnailPicture : pictureIDs.First();
        //            }
        //        }

        //        _context.Products.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    //ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", product.CategoryID);
        //    return View();
        //}


        //private ProductActionViewModel GetProductActionViewModelFromForm(IFormCollection formCollection)
        //{

        //    var model = new ProductActionViewModel
        //    {
        //        CategoryID = int.Parse(formCollection["CategoryID"]),
        //        Price = decimal.Parse(formCollection["Price"]),
        //        Discount = !string.IsNullOrEmpty(formCollection["Discount"]) ? decimal.Parse(formCollection["Discount"]) : 0,
        //        SKU = formCollection["SKU"],
        //        Tags = formCollection["Tags"],
        //        Barcode = formCollection["Barcode"],
        //        Supplier = formCollection["Supplier"],
        //        StockQuantity = !string.IsNullOrEmpty(formCollection["StockQuantity"]) ? int.Parse(formCollection["StockQuantity"]) : 0,
        //        isFeatured = formCollection["isFeatured"].Contains("true"),
        //        ProductPictures = formCollection["ProductPictures"],
        //        ThumbnailPicture = !string.IsNullOrEmpty(formCollection["ThumbnailPicture"]) ? int.Parse(formCollection["ThumbnailPicture"]) : 0,
        //        Name = formCollection["Name"],
        //        Description = formCollection["Description"],
        //    };
        //    return model;
        //}
        // GET: Dashboard/AdminProduct/Edit/5
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
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // POST: Dashboard/AdminProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,Name,Price,Description,Discount,isFeatured,ThumbnailPictureID,SKU,Tags,Barcode,Supplier,ID,IsActive,IsDeleted,ModifiedOn")] Product product)
        {
            if (id != product.ID)
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
                    if (!ProductExists(product.ID))
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
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // GET: Dashboard/AdminProduct/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Dashboard/AdminProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
