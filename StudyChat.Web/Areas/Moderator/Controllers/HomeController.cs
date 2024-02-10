using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyChat.Services.Interface;
using StudyChat.Services;
using StudyChat.Core.Entities;

namespace StudyChat.Web.Areas.Moderator.Controllers
{
	[Authorize(Roles="Moderator")]
	[Area("Moderator")]
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

		public async Task<IActionResult> ShowAnswer(int id)
		{
			Question question = new Question();
			

			List<(string, string, string)> questionAnswer = new List<(string, string, string)>();

			try
			{
				if (id == 0)
				{
					return RedirectToAction("index", "Home", new { area = "Teacher" });
				}
				else
				{
					List<Answer> answers = new List<Answer>();

					question = await _questionService.GetQuestionById(id);
					answers = await _answerService.GetAnswerByQuestionId(id);

					if (question == null)
					{
						TempData["ErrorMessage"] = "Something went wrong";
						return NotFound();
					}

					foreach (var answer in answers)
					{
						answer.UserId = _userService.GetUserName(answer.UserId);
						questionAnswer.Add((question.Content, answer.Content, answer.UserId));
					}
				}
			}
			catch (Exception e)
			{
				throw;
			}
			return View(questionAnswer);
		}


		public async Task<IActionResult> DeleteQuestion(int id)
		{
			await _questionService.DeleteQuestionByQuestionID(id);
			TempData["Message"] = "Question has been deleted successfully";
			return RedirectToAction("Index", "Home", new { area = "Moderator" });
		}


	}
}
