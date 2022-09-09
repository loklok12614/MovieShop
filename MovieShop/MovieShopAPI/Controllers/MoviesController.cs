using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        
        [HttpGet]
        [Route("top-grossing")] // Attribute Routing
        public async Task<IActionResult> GetTopGrossingMovies()
        {
            // call my service
            var movies = await _movieService.GetTop30GrossingMovies();
            // return the movies in JSON Format
            // asp.net core automatically serializes C# object to json objects
            // System.Text.Json .NET 3
            // older version of .NET to work with JSON Newtonsoft.JSON
            // return data(json format) along with it return the HTTP status code

            if (movies == null || !movies.Any())
            {
                return NotFound(new { errorMessage = "No Movies Found" });
            }

            return Ok(movies);
        }
        
        [HttpGet]
        [Route("top-rated")] // Attribute Routing
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTop30RatedMovies();
            if (movies == null || !movies.Any())
            {
                return NotFound(new { errorMessage = "No Movies Found" });
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("details/{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movieDetails = await _movieService.GetMovieDetails(id);
            if (movieDetails == null)
            {
                return NotFound(new { errorMessage = $"No Movie with Id : {id} Was Found" });
            }

            return Ok(movieDetails);
        }
        
        [HttpGet]
        public async Task<IActionResult> All(int pageSize=30, int page=1)
        {
            var movies = await _movieService.GetAllMovies(pageSize, page);
            if (movies.Data == null || !movies.Data.Any())
            {
                return NotFound(new { errorMessage = $"No Movies Found" });
            }
            return Ok(movies);
        }
        
        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GenreMovies(int genreId, int pageSize = 30, int page = 1)
        {
            var pagedMovies = await _movieService.GetMoviesByGenrePagination(genreId, pageSize, page);
            if (pagedMovies.Data == null || !pagedMovies.Data.Any())
            {
                return NotFound(new { errorMessage = $"No Movies Found In This Genre" });
            }
            return Ok(pagedMovies);
        }

        [HttpGet]
        [Route("{movieId:int}/reviews")]
        public async Task<IActionResult> GetReviewsByMovieId(int movieId, int pageSize = 30, int page = 1)
        {
            var pagedReviews = await _movieService.GetAllReviewsByMovieId(movieId, pageSize, page);
            if (pagedReviews.Data == null || !pagedReviews.Data.Any())
            {
                return NotFound(new { errorMessage = $"No Reviews Yet" });
            }

            return Ok(pagedReviews);
        }
    }
}
