using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyChat.Services.Interface;
using StudyChat.Services;
using StudyChat.Core.Entities;
using StudyChat.DataAccess.Repository;

namespace StudyChat.Web.Areas.Teacher.Controllers
{
	[Authorize(Roles = "Teacher")]
	[Area("Teacher")]
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

		public IActionResult Respond()
		{
			return View();
		}

		public async Task<IActionResult> QuestionAnswer(int id)
		{
			Question question = new Question();
			try
			{
				if (id == 0)
				{
					return RedirectToAction("Setting", "Home", new { area = "Teacher" });
				}
				else
				{
					question = await _questionService.GetQuestionById(id);
					if (question == null)
					{
						return NotFound();
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			return View(question);
		}


	}
}
