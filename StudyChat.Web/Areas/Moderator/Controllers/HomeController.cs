using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyChat.Services.Interface;
using StudyChat.Services;

namespace StudyChat.Web.Areas.Moderator.Controllers
{
	[Authorize(Roles="Moderator")]
	[Area("Moderator")]
	public class HomeController : Controller
	{
		private readonly IQuestionService _questionService;
		private readonly IUserService _userService;

		public HomeController(IQuestionService questionService, IUserService userService)
		{
			_questionService = questionService;
			_userService = userService;
		}

		public async Task<ViewResult> Index()
		{
			var questions = await _questionService.GetAllQuestions();
			foreach (var question in questions)
			{
				question.UserId = _userService.GetUserName(question.UserId);

			}
			return View(questions);
		}

		public IActionResult Setting()
        {
            return View();
        }
    }
}
