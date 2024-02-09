using Microsoft.AspNetCore.Mvc;

namespace StudyChat.Web.Areas.Teacher.Controllers
{
	public class AnswerController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
