using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
	[Authorize]
	public class ExperienceController : Controller
	{
		private readonly MyPortfolioContext context;
        public ExperienceController(MyPortfolioContext _context)
        {
            context = _context;
        }
		public IActionResult ExperienceList()
		{
			var values = context.Experiences.ToList();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateExperience()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateExperience(Experience experience)
		{
			context.Experiences.Add(experience);
			context.SaveChanges();
			return RedirectToAction("ExperienceList");
		}

		public IActionResult DeleteExperience(int id)
		{
			var value = context.Experiences.Find(id);
			context.Experiences.Remove(value);
			context.SaveChanges();
			return RedirectToAction("ExperienceList");
		}

		[HttpGet]
		public IActionResult UpdateExperience(int id)
		{
			var value = context.Experiences.Find(id);
			return View(value);
		}

		[HttpPost]
		public IActionResult UpdateExperience(Experience experience)
		{
			context.Experiences.Update(experience);
			context.SaveChanges();
            return RedirectToAction("ExperienceList");
        }
	}
}
