using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyChat.Core.Entities;
using StudyChat.Services;
using StudyChat.Services.Interface;

namespace StudyChat.Web.Areas.Student.Controllers
{
	[Authorize(Roles="Student")]
	[Area("Student")]
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


		[HttpPost]
		public IActionResult Index(string content)
		{
			if (ModelState.IsValid)
			{
				Question question = new()
				{
					Content = content,
					UserId = _userService.GetUserId,
					IsAnswered = false
				};

				_questionService.CreateQuestion(question);
				TempData["AlertMessage"] = "Question has been added successfully";

				return RedirectToAction("Index", "Home", new { area = "Student" });
			}
			return View();
		}
	}
}
