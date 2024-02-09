using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyChat.Core.Entities;
using StudyChat.DataAccess.Repository;
using StudyChat.Web.Areas.Admin.Models;
using StudyChat.Web.Models;

namespace StudyChat.Web.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class HomeController : Controller
	{

		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public HomeController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}


		public IActionResult Index()
		{
			return View();
		}


		public IActionResult Moderator()
		{
			return View();
		}

		public IActionResult Setting()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Index(string name, string email, string password)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					Name = name,
					Email = email,
					UserName = email,
				};


				var result = await userManager.CreateAsync(user, password);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, "Moderator");
					TempData["Message"] = "Moderator Added Successfully";

					return RedirectToAction("index", "Home", new { area = "Admin" });
				}
				TempData["ErrorMessage"] = "Something went wrong";
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View();
		}




















	}
}
