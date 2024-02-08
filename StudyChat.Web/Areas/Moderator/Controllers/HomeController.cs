using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudyChat.Web.Areas.Moderator.Controllers
{
	[Authorize(Roles="Moderator")]
	[Area("Moderator")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
