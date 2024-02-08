﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudyChat.Web.Areas.Teacher.Controllers
{
	[Authorize(Roles="Teacher")]
	[Area("Teacher")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
