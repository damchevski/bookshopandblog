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
using Microsoft.AspNetCore.Authorization;

namespace BSB.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService productService)
        {
            _moviesService = productService;
        }

        public async Task<IActionResult> GroupMovies(string? Genre)
        {
            if (Genre != null && Genre.Contains("Choose"))
                Genre = null;

            GroupMoviesDto res = new GroupMoviesDto()
            {
                Movies = await this._moviesService.GroupByGenres(Genre),
                Genres = await this._moviesService.GetAllGenres()
            };

            return View(res);
        }

        // GET: Products
        public async Task<IActionResult> Index(string? SearchString)
        {
            return View(await this._moviesService.GetAllMovies(SearchString));
        }

        public async Task<IActionResult> AddToFavourites(Guid? id)
        {
            var movie = await this._moviesService.GetMovie(id);
            AddToFavouritesDto model = new AddToFavouritesDto
            {
                SelectedMovie = movie,
                MovieId = movie.Id,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovieToFavourites([Bind("MovieId")] AddToFavouritesDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await this._moviesService.AddToFavourites(item, userId);

            if (result)
            {
                // TODO shopping cart view
                return RedirectToAction("GetFavMovies", "FavMovies");
            }

            //todo add error
            return RedirectToAction("Index", "Products");
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {

            var product = await this._moviesService.GetMovie(id);

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
        public async Task<IActionResult> Create([Bind("Name,Image,Price,Genre,Description,Director,Id")] Movie product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();
                Movie res = await this._moviesService.AddMovie(product);

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

            var product = await this._moviesService.GetMovie(id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Image,Price,Genre,Description,Director,Id")] Movie product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   var res = await  this._moviesService.EditMovie(product);

                    if (res == null)
                        return NotFound();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await this._moviesService.GetMovie(product.Id) == null)
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

            var product = await this._moviesService.GetMovie(id);

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
            var res = await this._moviesService.GetMovie(id);

            if (res == null)
                return NotFound();

            Movie mov = await this._moviesService.DeleteMovie(id);

            return RedirectToAction(nameof(Index));
        }


    }
}
