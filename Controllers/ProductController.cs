using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop_app.Models;
using shop_app.Services;

namespace shop_app.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServiceProduct _serviceProduct;
        public ProductController(IServiceProduct serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }
        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var products = await _serviceProduct.ReadAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _serviceProduct.GetByIdAsync(id);
            return View(product);
        }
        [HttpGet]
        public IActionResult NotFound() => View();
        [HttpGet]
        public IActionResult Create() => (User.IsInRole("admin")) ? View() : RedirectToAction("NotFound");
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price, Description")] Product product)
        {
            if (ModelState.IsValid && (User.IsInRole("admin") || User.IsInRole("moderator")))
            {
                _ = await _serviceProduct.CreateAsync(product);
                return RedirectToAction(nameof(Read));
            }
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id) 
        {
            var product = await _serviceProduct.GetByIdAsync(id);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Name,Price, Description")] Product product)
        {
            if (ModelState.IsValid && (User.IsInRole("admin") || User.IsInRole("moderator")))
            {
                _ = await _serviceProduct.UpdateAsync(id, product);
                return RedirectToAction(nameof(Read));
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var productId = await _serviceProduct.GetByIdAsync(id);
            if (productId is null)
            {
                return NotFound();
            }
            return View(productId);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.IsInRole("admin"))
            {
                _ = await _serviceProduct.DeleteAsync(id);
                return RedirectToAction(nameof(Read));
            }
            return RedirectToAction(nameof(Read));
        }

    }
}
