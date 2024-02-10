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
		private readonly IAnswerService _answerService;
		private readonly IUserService _userService;

		public HomeController(IQuestionService questionService, IUserService userService, IAnswerService answerService)
		{
			_questionService = questionService;
			_userService = userService;
			_answerService = answerService;
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
				TempData["Message"] = "Question Created Successfully";

				return RedirectToAction("Index", "Home", new { area = "Student" });
			}
			TempData["ErrorMessage"] = "Something went wrong";
			return View();
		}


		public async Task<IActionResult> ShowAnswer(int id)
		{
			Question question = new Question();
			Answer answer = new Answer();

			List<(string, string, string)> questionAnswer = new List<(string, string, string)>();

			try
			{
				if (id == 0)
				{
					return RedirectToAction("index", "Home", new { area = "Teacher" });
				}
				else
				{
					question = await _questionService.GetQuestionById(id);
					answer = await _answerService.GetAnswerByQuestionId(id);
					if (question == null)
					{
						TempData["ErrorMessage"] = "Something went wrong";
						return NotFound();
					}

					answer.UserId = _userService.GetUserName(answer.UserId);

					questionAnswer.Add((question.Content, answer.Content, answer.UserId));

				}
			}
			catch (Exception e)
			{
				throw;
			}
			return View(questionAnswer);
		}


	}
}
