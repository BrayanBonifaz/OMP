using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebPatentes.Models;

namespace WebPatentes.Controllers
{
    public class HomeController : Controller
    {
        IServiceProvider servicesProvider;
        public HomeController(IServiceProvider servicesProvider)
        {
            this.servicesProvider = servicesProvider;
        }
        
        public IActionResult Index()
        {
            //await CreateRoles(servicesProvider);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginViewModels models)
        {

            if (ModelState.IsValid)
            {


            }

            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                String[] rolesName = { "Admin", "User" };
                foreach (var item in rolesName)
                {
                    var roleExist = await roleManager.RoleExistsAsync(item);

                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(item));
                    }
                }
                var user = await userManager.FindByIdAsync("2d14fd4f-1b6e-4ab3-b455-d7823fd92954");
                await userManager.AddToRoleAsync(user, "Admin");
            }
            catch (Exception ex)
            {

                var _f = ex.Message;    
            }
            

        }
    }
}
