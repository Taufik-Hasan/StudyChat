using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyChat.Core.Entities;
using StudyChat.Web.Models;

namespace StudyChat.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(LoginVM model)
		{
			if (ModelState.IsValid)
			{
				//login
				var result = signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
				if (result.Result.Succeeded)
				{
					if (model.UserRole == "Teacher")
					{
						return RedirectToAction("index", "Home", new { area = "Teacher" });
					}
					else if (model.UserRole == "Student")
					{
						return RedirectToAction("index", "Home", new { area = "Student" });
					}
					else if (model.UserRole == "Moderator")
					{
						return RedirectToAction("index", "Home", new { area = "Moderator" });
					}
					else
					{
						return RedirectToAction("index", "Home");
					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
				return View(model);
			}
			return View(model);
		}
	}
}
