using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        // User Details
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetUserDetailsById(int userId)
        {
            var userDetails = await _userService.GetUserDetailsById(userId);
            if (userDetails == null)
            {
                return NotFound(new { errorMessage = $"User {userId} not found" });
            }

            return Ok(userDetails);
        }
        
        // Purchase
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> Purchases(int userId, int pageSize=30, int page=1)
        {
            // get all the movies purchased by the user, user id
            // httpcontext.user.claims and then call the database and get the information to the view
            var pagedMoviesPurchased = await _userService.GetAllPurchasesByUserIdPagination(userId, pageSize, page);
            if (pagedMoviesPurchased.Data == null || !pagedMoviesPurchased.Data.Any())
            {
                return NoContent();
            }

            return Ok(pagedMoviesPurchased);
        }
        
        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> BuyMovie(int movieId, int userId, decimal totalPrice)
        {
            PurchaseRequestModel model = new PurchaseRequestModel
            {
                MovieId = movieId, UserId = userId, TotalPrice = totalPrice
            };
            var purchasedNumber = await _userService.PurchaseMovie(model);
            if (purchasedNumber == Guid.Empty)
            {
                return BadRequest(new { errorMessage = $"You already owned this movie" });
            }

            return Ok(new { movieId = movieId, purchasedNumber = purchasedNumber });
        }
        
        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> CheckPurchased(int movieId, int userId)
        {
            var isMoviePurchased = await _userService.IsMoviePurchased(movieId, userId);
            return Ok(isMoviePurchased);
        }
        
        
        // Favorite
        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> Favorites(int userId, int pageSize=30, int page=1)
        {
            // get all the movies purchased by the user, user id
            // httpcontext.user.claims and then call the database and get the information to the view
            var pagedMoviesFavorited = await _userService.GetAllFavoritesByUserPagination(userId, pageSize, page);
            if (pagedMoviesFavorited.Data == null || !pagedMoviesFavorited.Data.Any())
            {
                return NoContent();
            }

            return Ok(pagedMoviesFavorited);
        }
        
        [HttpPost]
        [Route("favorite-movie")]
        public async Task<IActionResult> FavoriteMovie(int movieId, int userId)
        {
            FavoriteRequestModel model = new FavoriteRequestModel
            {
                MovieId = movieId, UserId = userId
            };
            var favoriteMovieId = await _userService.FavoriteMovie(model);
            if (favoriteMovieId == 0)
            {
                return BadRequest(new { errorMessage = $"You already saved this movie to favorites" });
            }

            return Ok(new { movieId = favoriteMovieId });
        }
        
        [HttpDelete]
        [Route("un-favorite")]
        public async Task<IActionResult> RemoveFavoriteMovie(int movieId, int userId)
        {
            FavoriteRequestModel model = new FavoriteRequestModel
            {
                MovieId = movieId, UserId = userId
            };
            var removedMovieId = await _userService.RemoveFavoriteMovie(model);
            if (removedMovieId == 0)
            {
                return BadRequest(new { errorMessage = $"This movie is not in your favorites' list" });
            }
            return Ok(new { movieId = removedMovieId});
        }

        [HttpGet]
        [Route("check-movie-favorite/{movieId:int}")]
        public async Task<IActionResult> CheckFavorite(int movieId, int userId)
        {
            var isMovieFavorited = await _userService.IsMovieFavorited(movieId, userId);
            return Ok(isMovieFavorited);
        }
        
        // Review
        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> WriteReview(int movieId, int userId, int rating, string reviewText)
        {
            ReviewRequestModel model = new ReviewRequestModel
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating,
                ReviewText = reviewText
            };
            var reviewedMovieId = await _userService.ReviewMovie(model);
            if(reviewedMovieId == 0)
            {
                return BadRequest(new { errorMessage = $"You have already reviewed this movie" });
            }
            return Ok(reviewedMovieId);
        }
        
        [HttpDelete]
        [Route("delete-review/{movieId:int}")]
        public async Task<IActionResult> DeleteReview(int movieId, int userId)
        {
            var deletedReviewMovieId = await _userService.DeleteReview(movieId, userId);
            if (deletedReviewMovieId == 0)
            {
                return BadRequest(new { errorMessage = $"You have not reviewed this movie" });
            }

            return Ok(deletedReviewMovieId);
        }
        
        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> EditReview(int movieId, int userId, int rating, string reviewText)
        {
            ReviewRequestModel model = new ReviewRequestModel
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating,
                ReviewText = reviewText
            };
            var reviewedMovieId = await _userService.EditReview(model);
            if (reviewedMovieId == 0)
            {
                return BadRequest(new { errorMessage = $"You have not reviewed this movie" });
            }

            return Ok(reviewedMovieId);
        }

        [HttpGet]
        [Route("movie-review/{movieId:int}")]
        public async Task<IActionResult> GetMovieReviewByUser(int movieId, int userId)
        {
            var review = await _userService.GetReviewByUserIdAndMovieId(userId, movieId);
            if (review.UserId == 0)
            {
                return NotFound(new { errorMessage = $"You have not reviewed this movie" });
            }

            return Ok(review);
        }
        
        [HttpGet]
        [Route("movie-reviews")]
        public async Task<IActionResult> GetAllReviewsByUser(int userId)
        {
            var reviews = await _userService.GetAllReviewsByUser(userId);
            if (reviews == null || !reviews.Any())
            {
                return NotFound(new { errorMessage = $"You have not made any review" });
            }

            return Ok(reviews);
        }
    }
}
