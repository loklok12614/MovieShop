using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Infra;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;

        public UserController(ICurrentUser currentUser, IUserService userService)
        {
            _currentUser = currentUser;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases(int pageSize=30, int page=1)
        {
            // get all the movies purchased by the user, user id
            // httpcontext.user.claims and then call the database and get the information to the view
            var pagedMoviesPurchased = await _userService.GetAllPurchasesByUserIdPagination(_currentUser.UserId, pageSize, page);
            return View(pagedMoviesPurchased);
        }
        
        [HttpGet]
        public async Task<IActionResult> Favorites(int pageSize=30, int page=1)
        {
            var pagedMoviesFavorited =
                await _userService.GetAllFavoritesByUserPagination(_currentUser.UserId, pageSize, page);
            return View(pagedMoviesFavorited);
        }
        
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEditModel model)
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> BuyMovie(int movieId, int userId, decimal totalPrice)
        {
            PurchaseRequestModel model = new PurchaseRequestModel
            {
                MovieId = movieId, UserId = userId, TotalPrice = totalPrice
            };
            var purchasedNumber = await _userService.PurchaseMovie(model);
            return RedirectToAction("Details", "Movies", new { id = movieId, purchaseNumber = purchasedNumber });
        }
        
        [HttpPost]
        public async Task<IActionResult> FavoriteMovie(int movieId, int userId)
        {
            FavoriteRequestModel model = new FavoriteRequestModel
            {
                MovieId = movieId, UserId = userId
            };
            var favoriteMovieId = await _userService.FavoriteMovie(model);
            return RedirectToAction("Details", "Movies", new { id = favoriteMovieId });
        }
        
        [HttpPost]
        public async Task<IActionResult> RemoveFavoriteMovie(int movieId, int userId)
        {
            FavoriteRequestModel model = new FavoriteRequestModel
            {
                MovieId = movieId, UserId = userId
            };
            var removedMovieId = await _userService.RemoveFavoriteMovie(model);
            return RedirectToAction("Details", "Movies", new { id = removedMovieId });
        }

        [HttpPost]
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
            return RedirectToAction("Details", "Movies", new { id = reviewedMovieId });
        }
    }
}