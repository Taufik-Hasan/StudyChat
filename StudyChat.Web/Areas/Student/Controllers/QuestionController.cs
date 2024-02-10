using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyChat.Services.Interface;
using StudyChat.Services;
using StudyChat.Core.Entities;

namespace StudyChat.Web.Areas.Student.Controllers
{
	[Authorize(Roles = "Student")]
	[Area("Student")]
	public class QuestionController : Controller
	{
		private readonly IQuestionService _questionService;
		private readonly IUserService _userService;

		public QuestionController(IQuestionService questionService, IUserService userService)
		{
			_questionService = questionService;
			_userService = userService;
		}

		public async Task<ViewResult> Index()
		{
			var questions = await _questionService.GetAllQuestionsByUserID(_userService.GetUserId);
			questions = questions?.Reverse();
			return View(questions);
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
				TempData["Message"] = "Question has been added successfully";

				return RedirectToAction("Index", "Question", new { area = "Student" });
			}
			return View();
		}

		public async Task<IActionResult> DeleteQuestion(int id)
		{
			await _questionService.DeleteQuestionByQuestionID(id);
			TempData["Message"] = "Question has been deleted successfully";
			return RedirectToAction("Index", "Question", new { area = "Student" });
		}

	}
}
