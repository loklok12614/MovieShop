using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register( [FromBody] UserRegisterModel model)
        {
            var userId = await _accountService.RegisterUser(model);
            if (userId == 0)
            {
                return BadRequest("User with this email already exist");
            }

            return Ok(userId);
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login()
        {
            return Ok(null);
        }
    }
}
