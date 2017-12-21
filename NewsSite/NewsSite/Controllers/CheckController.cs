using NewsSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using NewsSite.Models;

namespace NewsSite.Controllers
{

    [Route("check")]
    public class CheckController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private ApplicationDbContext applicationDbContext;

        public CheckController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            applicationDbContext.Database.EnsureCreated();
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet, Route("Recreate")]
        async public Task<IActionResult> Recreate()
        {
            ApplicationUser user1 = new ApplicationUser();
            ApplicationUser user2 = new ApplicationUser();
            ApplicationUser user3 = new ApplicationUser();
            ApplicationUser user4 = new ApplicationUser();
            ApplicationUser user5 = new ApplicationUser();

            foreach (var userDelete in userManager.Users.ToList())
            {
                await userManager.DeleteAsync(userDelete);

            }

            user1.Email = "adam@gmail.com";
            user1.UserName = "Adam";
          

            var result1 = await userManager.CreateAsync(user1);
            
            await userManager.AddClaimAsync(user1, new Claim("NewsAppRole", "Administrator"));
            await userManager.AddClaimAsync(user1, new Claim("NewsAppRole", "Publisher"));
            await userManager.AddClaimAsync(user1, new Claim("NewsAppRole", "Subscriber"));
            await userManager.AddClaimAsync(user1, new Claim("NewsAppRole", "Anonymous"));

            user2.Email = "peter@gmail.com";
            user2.UserName = "Peter";

            var result2 = await userManager.CreateAsync(user2);

            await userManager.AddClaimAsync(user2, new Claim("NewsAppRole", "Publisher"));
            await userManager.AddClaimAsync(user2, new Claim("NewsAppRole", "Subscriber"));
            await userManager.AddClaimAsync(user2, new Claim("NewsAppRole", "Anonymous"));
            await userManager.AddClaimAsync(user2, new Claim("PublishRights", "Sport,Economy"));


            user3.Email = "susan@gmail.com";
            user3.UserName = "Susan";
            

            var result3 = await userManager.CreateAsync(user3);

            await userManager.AddClaimAsync(user3, new Claim("NewsAppRole", "Subscriber"));
            await userManager.AddClaimAsync(user3, new Claim("NewsAppRole", "Anonymous"));
            await userManager.AddClaimAsync(user3, new Claim("Age", "48"));

            user4.Email = "viktor@gmail.com";
            user4.UserName = "Viktor";

            var result4 = await userManager.CreateAsync(user4);

            await userManager.AddClaimAsync(user4, new Claim("NewsAppRole", "Subscriber"));
            await userManager.AddClaimAsync(user4, new Claim("NewsAppRole", "Anonymous"));
            await userManager.AddClaimAsync(user4, new Claim("Age", "15"));


            user5.Email = "xerxes@gmail.com";
            user5.UserName = "Xerxes";

            var result5 = await userManager.CreateAsync(user5);
            await userManager.AddClaimAsync(user5, new Claim("NewsAppRole", "Anonymous"));



            return Ok();
        }
        [HttpGet, Route("GetEmails")]
        public IActionResult GetEmails()
        {
            var list = userManager.Users.ToList();
            return Ok(list);
        }
        
        [HttpGet, Route("OpenNews")]
        async public Task<IActionResult> OpenNews()
        {
            var hiddenNews = DataManager.newsItems;
            return Ok(hiddenNews);
        }

        

        [Authorize(Policy = "SubscriberRights")]
        [HttpGet, Route("HiddenNews")]
        async public Task<IActionResult> HiddenNews()
        {
            var hiddenNews = DataManager.newsItems;
            return Ok(hiddenNews);
        }


        [Authorize(Policy ="SubscriberRights")]
        [Authorize(Policy ="ViewRights")]
        [HttpGet, Route("OldHiddenNews")]
        async public Task<IActionResult> OldHiddenNews()
        {
            var hiddenNews = DataManager.newsItems;
            return Ok(hiddenNews);
        }

        [Authorize(Policy ="PublisherRights")]
        [HttpGet, Route("canpublish")]
        async public Task<IActionResult> CanPublish ()
        {
            return Ok();
        }

       

    }
}
