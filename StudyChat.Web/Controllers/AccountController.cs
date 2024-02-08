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

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(StudentRegisterVM model)
        {
            if (ModelState.IsValid)
            {
                Student user = new()
                {
                    Name = model.Name,
                    Email = model.Email,
                    UserName = model.Email,
                    InstituteName = model.InstituteName,
                    InstituteIdCardNumber = model.InstituteIdCardNumber
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Student");
                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("index", "Home", new { area = "Student" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public IActionResult TeacherRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TeacherRegister(TeacherRegistrationVM model)
        {
            if (ModelState.IsValid)
            {
                Teacher user = new()
                {
                    Name = model.Name,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Teacher");
                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("index", "Home", new { area = "Teacher" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public IActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Admin(AdminVM model)
        {
            if (ModelState.IsValid)
            {
                //login
                var result = signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Result.Succeeded)
                {
                    return RedirectToAction("index", "Home", new { area = "Admin" });
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }
    }
}
