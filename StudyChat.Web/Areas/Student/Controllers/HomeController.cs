using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudyChat.Web.Areas.Student.Controllers
{
	[Authorize(Roles="Student")]
	[Area("Student")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Setting()
		{
			return View();
		}


		[HttpPost]
		public IActionResult Index(string content)
		{
			return View();
		}
	}
}
