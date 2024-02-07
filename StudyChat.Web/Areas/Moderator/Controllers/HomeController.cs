using Microsoft.AspNetCore.Mvc;

namespace StudyChat.Web.Areas.Moderator.Controllers
{
	[Area("Moderator")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
