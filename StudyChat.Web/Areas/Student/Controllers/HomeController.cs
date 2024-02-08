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
	}
}
