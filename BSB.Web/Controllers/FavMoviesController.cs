using BSB.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BSB.Web.Controllers
{
    public class FavMoviesController : Controller
    {
        private readonly IFavMoviesService FavMoviesService;

        public FavMoviesController(IFavMoviesService FavMoviesService)
        {
            this.FavMoviesService = FavMoviesService;
        }
        public async Task< IActionResult> GetFavMovies()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(await this.FavMoviesService.GetUserFavMoviesInfo(userId));
        }
        public async Task<IActionResult> DeleteFromFavMovies(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await this.FavMoviesService.DeleteMovieFromUserFavMovies(userId, id);

            if (result)
            {
                return RedirectToAction("GetFavMovies", "FavMovies");
            }
            else
            {
                return RedirectToAction("GetFavMovies", "FavMovies");
            }
        }

    }
}