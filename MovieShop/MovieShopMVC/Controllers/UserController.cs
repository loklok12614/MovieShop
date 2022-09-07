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
        public async Task<IActionResult> Favorites()
        {
            return View();
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
            var result = await _userService.PurchaseMovie(model);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }
        
        [HttpPost]
        public async Task<IActionResult> FavoriteMovie()
        {
            return View();
        }
    }
}