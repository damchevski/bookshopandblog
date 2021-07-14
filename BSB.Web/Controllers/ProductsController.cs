using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BSB.Data;
using BSB.Data.Entity;
using BSB.Service.Interface;
using BSB.Data.Dto;
using System.Security.Claims;

namespace BSB.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> GroupBooks(string? Genre)
        {
            if (Genre != null && Genre.Contains("Choose"))
                Genre = null;

            GroupBooksDto res = new GroupBooksDto()
            {
                Products = await this._productService.GroupByGenres(Genre),
                Genres = await this._productService.GetAllGenres()
            };

            return View(res);
        }

        // GET: Products
        public async Task<IActionResult> Index(string? SearchString, bool? rent)
        {
            return View(await this._productService.GetAllProducts(SearchString, rent));
        }

        public async Task<IActionResult> AddToShoppingCart(Guid? id)
        {
            var product = await this._productService.GetProduct(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                SelectedProduct = product,
                ProductId = product.Id,
                Quantity = 1
            };
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductToCart([Bind("ProductId", "Quantity")] AddToShoppingCartDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await this._productService.AddToShoppingCart(item, userId);

            if (result)
            {
                // TODO shopping cart view
                return RedirectToAction("GetCartInfo", "ShoppingCart");
            }

            //todo add error
            return RedirectToAction("Index", "Products");
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {

            var product = await this._productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Image,Price,Genre,IsForBuy,Id")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();
                Product res = await this._productService.AddProduct(product);

                if (res == null)
                    throw new Exception("All Fields Required");

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this._productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Image,Price,Genre,IsForBuy,Id")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   var res = await  this._productService.EditProduct(product);

                    if (res == null)
                        return NotFound();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await this._productService.GetProduct(product.Id) == null)
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this._productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var res = await this._productService.DeleteProduct(id);

            if (res == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }


    }
}
