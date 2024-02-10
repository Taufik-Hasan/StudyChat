using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyChat.Services.Interface;
using StudyChat.Services;
using StudyChat.Core.Entities;
using StudyChat.DataAccess.Repository;
using StudyChat.Web.Areas.Teacher.Models;

namespace StudyChat.Web.Areas.Teacher.Controllers
{
	[Authorize(Roles = "Teacher")]
	[Area("Teacher")]
	public class HomeController : Controller
	{
		private readonly IQuestionService _questionService;
		private readonly IUserService _userService;
		private readonly IAnswerService _answerService;

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

		public async Task<ViewResult> Respond()
		{
			var respondedQAs = await _userService.GetRespondedQA(_userService.GetUserId);
			Console.WriteLine(respondedQAs);
			return View(respondedQAs);
		}

		public async Task<IActionResult> ShowAnswer(int id)
		{
			Question question = new Question();
			Answer answer = new Answer();

			List<(string,string,string)> questionAnswer = new List<(string, string, string)>();

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


		public async Task<IActionResult> QuestionAnswer(int id)
		{
			Question question = new Question();
			try
			{
				if (id == 0)
				{
					return RedirectToAction("index", "Home", new { area = "Teacher" });
				}
				else
				{
					question = await _questionService.GetQuestionById(id);
					if (question == null)
					{
						TempData["ErrorMessage"] = "Something went wrong";
						return NotFound();
					}
					question.UserId = _userService.GetUserName(question.UserId);
				}
			}
			catch (Exception e)
			{
				throw;
			}
			return View(question);
		}

		[HttpPost]
		public IActionResult QuestionAnswer(int questionId, string content)
		{
			if (ModelState.IsValid)
			{
				Answer answer = new()
				{
					Content = content,
					QuestionId = questionId,
					UserId = _userService.GetUserId,
				};

				_answerService.CreateAnswer(answer);
				TempData["Message"] = "Answer Submitted Successfully";

				return RedirectToAction("Respond", "Home", new { area = "Teacher" });
			}
			TempData["ErrorMessage"] = "Something went wrong";
			return View();
		}


	}
}
