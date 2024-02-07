using Microsoft.AspNetCore.Mvc;

namespace StudyChat.Web.Areas.Teacher.Controllers
{
	[Area("Teacher")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
