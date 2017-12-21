using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NewsSite.Data;

namespace NewsSite.Controllers
{
    [Route("login")]
    //[Authorize(Policy ="Anonymous")]
    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet, Route("signin")]
        async public Task<IActionResult> Signin(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            await signInManager.SignInAsync(user, true);
            return Ok(await userManager.GetClaimsAsync(user));
        }
        [Authorize]
        [HttpGet, Route("test")]
        public IActionResult Test()
        {
            return Ok("Fönka");
        }

        public IActionResult OpenNews()
        {
            return Ok();
        }
        [Authorize(Policy ="SubscriberRights")]
        public IActionResult HiddenNews()
        {
            return Ok();
        }

        [Authorize(Policy ="PublisherRights")]
        public IActionResult OldHiddenNews()
        {
            return Ok();
        }
    }
}