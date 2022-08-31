using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieService _movieService;

        public MoviesController(ILogger<MoviesController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _movieService.GetMovieDetails(id);
            return View(movieDetails);
        }

        public async Task<IActionResult> All(int pageNumber, int pageSize = 30)
        {
            var moviesTuple = await _movieService.GetAllMovies(pageNumber, pageSize);
            var (movies, totalMovies) = moviesTuple;
            ViewBag.pageNumber = pageNumber;
            ViewBag.totalMovies = totalMovies;
            ViewBag.totalPages = Math.Ceiling((decimal)(totalMovies / pageSize));
            return View(movies);
        }
    }
}